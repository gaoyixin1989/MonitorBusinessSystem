using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

using i3.View;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject;
using i3.BusinessLogic.Channels.Mis.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
namespace n21
{

    /// <summary>
    /// 功能描述：报告生成类
    /// 创建时间：2012-12-4
    /// 创建人：邵世卓
    /// 修改时间：2012-4-26
    /// 修改人：潘德军
    /// </summary>
    public partial class Channels_Rpt_Template_OfficeServer : PageBase
    {
        #region iWebOffice变量
        private int mFileSize;
        private byte[] mFileBody;
        private string mFileName;
        private string mFileType;
        private string mFileDate;
        //private string mFileID;
        private string mRecordID;
        private string mTemplate;
        private string mDateTime;
        private string mOption;
        private string mMarkName;
        private string mPassword;
        private string mMarkList;
        private string mBookmark;
        private string mDescript;
        private string mHostName;
        private string mMarkGuid;
        private string mDirectory;

        private string mHtmlName;
        private string mFilePath;

        private string mUserName;
        private string mCommand;
        private string mContractType;
        private string mConditionType;
        private string mTEST_ITEM_type;
        private string mContract;
        private string mContent;
        private string mTaskID;

        private string mLabelName;
        private string mImageName;
        private string mTableContent;
        private int mColumns;
        private int mCells;
        private string mLocalFile;
        private string mRemoteFile;
        private string mError;
        //打印控制
        private string mOfficePrints;
        private int mCopies;
        //自定义信息传递
        private string mInfo;
        //组件引用
        private DBstep.iMsgServer2000 MsgObj;

        //报告环节
        private string mReportWf;

        //监测类别   未出综合报告前，报告分类别出，临时修改
        private string mItemTypeID;
        //何海亮修改
        //采样时间或者（当分析时间为空时就是分析时间）
        string strSampleTime;
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //声明WebOffice对象
            WebOfficeLogic WebOffice = new WebOfficeLogic();
            TRptFileVo file = new TRptFileVo();

            //WebOffice相关属性
            MsgObj = new DBstep.iMsgServer2000();
            mFilePath = Server.MapPath(".");
            mTableContent = "";
            mColumns = 3;
            mCells = 8;

            //判断信息包的合法性
            MsgObj.MsgVariant(Request.BinaryRead(Request.ContentLength));

            if (MsgObj.GetMsgByName("DBSTEP").Equals("DBSTEP"))
            {
                mOption = MsgObj.GetMsgByName("OPTION");             //取得操作信息
                mUserName = MsgObj.GetMsgByName("USERNAME");           //取得操作用户名称  
                mReportWf = MsgObj.GetMsgByName("ReportWf");

                switch (mOption)
                {
                    #region 文档
                    //--------------加载文档-------------//
                    case "LOADFILE":
                        #region load file
                        mRecordID = MsgObj.GetMsgByName("RECORDID");   //取得文档编号
                        mFileName = MsgObj.GetMsgByName("FILENAME");   //取得文档名称
                        mFileType = MsgObj.GetMsgByName("FILETYPE");   //取得文档类型
                        MsgObj.MsgTextClear();                         //清除文本信息

                        //判断文件是否为空
                        if (String.IsNullOrEmpty(mRecordID) || mRecordID == ObjectBase.SerialType.NullSerialNumber)
                        {
                            MsgObj.SetMsgByName("STATUS", "初始化成功!");//设置状态信息      
                            MsgObj.MsgError("");		                 //清除错误信息  

                            break;
                        }
                        //调入文档
                        mFileBody = WebOffice.LoadFile(mRecordID);
                        if (null != mFileBody && mFileBody.Length > 0)
                        {
                            MsgObj.MsgFileBody(mFileBody);              //将文件信息打包
                            MsgObj.SetMsgByName("STATUS", "打开成功!.");//设置状态信息      
                            MsgObj.MsgError("");		                //清除错误信息                                            
                        }
                        else
                        {
                            MsgObj.MsgError("打开失败!");		        //设置错误信息                           
                        }
                        #endregion
                        break;
                    //--------------保存文档-------------//
                    case "SAVEFILE":
                        #region save file
                        mRecordID = MsgObj.GetMsgByName("RECORDID");	//取得文档编号
                        mTaskID = MsgObj.GetMsgByName("TASKID");	    //取得文档名称
                        mFileName = MsgObj.GetMsgByName("FILENAME");	//取得文档名称
                        mFileType = MsgObj.GetMsgByName("FILETYPE");	//取得文档类型
                        mFileSize = MsgObj.MsgFileSize();				//取得文档大小
                        mFileDate = DateTime.Now.ToString();            //取得文档时间
                        mFileBody = MsgObj.MsgFileBody();				//取得文档内容
                        mFilePath = "";                                 //如果保存为文件，则填写文件路径
                        mUserName = MsgObj.GetMsgByName("USERNAME");    //取得保存用户名称
                        mDescript = "";                                 //版本说明
                        MsgObj.MsgTextClear();                          //清除文本信息

                        if (String.IsNullOrEmpty(mRecordID))
                        {
                            file.ID = PageBase.GetSerialNumber("t_rpt_file_id");
                        }
                        else
                        {
                            file.ID = mRecordID;
                        }
                        file.CONTRACT_ID = mTaskID;
                        file.FILE_NAME = mFileName;
                        file.FILE_TYPE = mFileType;
                        file.FILE_SIZE = mFileSize.ToString();
                        file.ADD_TIME = mFileDate;
                        file.FILE_BODY = mFileBody;
                        file.ADD_USER = mUserName;
                        file.FILE_DESC = mDescript;

                        //保存文档内容
                        if (WebOffice.SaveFile(file))
                        {
                            MsgObj.SetMsgByName("STATUS", "保存成功!");	       //设置状态信息
                            //将报表ID及合同ID回传，防止二次保存ID重新生成问题
                            MsgObj.SetMsgByName("RECORDID", file.ID);          //报告ID
                            MsgObj.SetMsgByName("TASKID", file.CONTRACT_ID); //合同ID
                            MsgObj.MsgError("");						       //清除错误信息
                        }
                        else
                        {
                            MsgObj.MsgError("保存失败!");		        //设置错误信息
                        }
                        MsgObj.MsgFileClear();
                        #endregion
                        break;
                    #endregion

                    #region 模板
                    //--------------加载模板-------------//
                    case "LOADTEMPLATE":
                        #region LOADTEMPLATE
                        mTemplate = MsgObj.GetMsgByName("TEMPLATE");		                    //取得模板文档类型
                        mCommand = MsgObj.GetMsgByName("COMMAND");                              //取得客户端定义的变量COMMAND值
                        //判断模板是否为空
                        if (String.IsNullOrEmpty(mTemplate) || mTemplate == ObjectBase.SerialType.NullSerialNumber)
                        {
                            MsgObj.SetMsgByName("STATUS", "初始化成功!");//设置状态信息      
                            MsgObj.MsgError("");		                 //清除错误信息  

                            break;
                        }

                        //本段处理是否调用文档时打开模版，还是套用模版时打开模版
                        if (mCommand.Equals("INSERTFILE"))
                        {
                            MsgObj.MsgTextClear();                                               //清除文本信息

                            //调入模板文档
                            if (MsgObj.MsgFileLoad(mFilePath + "\\Document\\" + mTemplate))
                            {
                                MsgObj.SetMsgByName("STATUS", "打开模板成功!");		             //设置状态信息
                                MsgObj.MsgError("");		                                     //清除错误信息
                            }
                            else
                            {
                                MsgObj.MsgError("打开模板失败!");		                         //设置错误信息
                            }
                        }
                        else
                        {
                            MsgObj.MsgTextClear();

                            mFileBody = WebOffice.LoadTemplate(mTemplate);
                            //调入模板文档
                            if (null != mFileBody && mFileBody.Length > 0)
                            {
                                MsgObj.MsgFileBody(mFileBody);				                    //将文件信息打包
                                MsgObj.SetMsgByName("STATUS", "打开模板成功!");		            //设置状态信息
                                MsgObj.MsgError("");		                                    //清除错误信息
                            }
                            else
                            {
                                MsgObj.MsgError("打开模板失败!");		                        //设置错误信息
                            }
                        }
                        #endregion
                        break;
                    //--------------保存模板-------------//
                    case "SAVETEMPLATE":
                        #region SAVETEMPLATE
                        mTemplate = MsgObj.GetMsgByName("TEMPLATE");		                    //取得文档编号
                        mFileName = MsgObj.GetMsgByName("FILENAME");		                    //取得文档名称
                        mFileType = MsgObj.GetMsgByName("FILETYPE");		                    //取得文档类型
                        mFileSize = MsgObj.MsgFileSize();					                    //取得文档大小
                        mFileDate = DateTime.Now.ToString();                                    //取得文档时间
                        mFileBody = MsgObj.MsgFileBody();					                    //取得文档内容
                        mFilePath = "";                                                         //如果保存为文件，则填写文件路径
                        MsgObj.MsgTextClear();                                                  //清除文本信息

                        //填充模板对象
                        TRptTemplateVo template = new TRptTemplateVo();
                        if (String.IsNullOrEmpty(mTemplate))
                        {
                            template.ID = PageBase.GetSerialNumber("Template_Id");
                        }
                        else
                        {
                            template.ID = mTemplate;
                        }
                        template.FILE_TYPE = mFileType;
                        template.FILE_SIZE = mFileSize.ToString();
                        template.FILE_BODY = mFileBody;
                        template.FILE_PATH = mFilePath;

                        if (WebOffice.SaveTemplate(template)) 							        //保存文档内容
                        {
                            MsgObj.SetMsgByName("STATUS", "保存模板成功!");		                //设置状态信息
                            //将ID回传，防止文档二次保存重新生成ID问题
                            MsgObj.SetMsgByName("TEMPLATE", template.ID);		                //设置状态信息
                            MsgObj.MsgError("");						                        //清除错误信息

                        }
                        else
                        {
                            MsgObj.MsgError("保存模板失败!");					                //设置错误信息
                        }
                        MsgObj.MsgFileClear();
                        #endregion
                        break;
                    #endregion

                    #region 标签
                    //--------------取得文档标签-------------//
                    case "LOADBOOKMARKS":
                        #region LOADBOOKMARKS
                        mRecordID = MsgObj.GetMsgByName("RECORDID");		                  //取得文档编号
                        mTemplate = MsgObj.GetMsgByName("TEMPLATE");		                  //取得模板编号
                        mFileName = MsgObj.GetMsgByName("FILENAME");		                  //取得文档名称
                        mFileType = MsgObj.GetMsgByName("FILETYPE");		                  //取得文档类型
                        mTaskID = MsgObj.GetMsgByName("TASKID");                              //合同编号
                        mItemTypeID = MsgObj.GetMsgByName("ItemTypeID");                      //监测类别   综合报告选择类别

                        string strMonitorConten = getMonitorContent(mItemTypeID, mTaskID);
                        MsgObj.SetMsgByName("MONITOR_CONTENT1", strMonitorConten);//监测内容
                        MsgObj.SetMsgByName("MONITOR_CONTENT2", strMonitorConten);//监测内容

                        string strTmpItemTypeID = new TRptTemplateLogic().Details(mTemplate).FILE_DESC;//如果是单类别模版，取该类别模版对应类别
                        if (strTmpItemTypeID.Length > 0 && strTmpItemTypeID != "0")
                        {
                            mItemTypeID = strTmpItemTypeID;
                        }
                        else
                        {
                            mItemTypeID = "";
                        }

                        string strTemplateName = GetTemplateName();

                        #region 基本信息
                        //报告文件
                        TRptFileVo objFile = new TRptFileLogic().Details(mRecordID);
                        DataTable dtInfo = new ReportBuildLogic().getMonitorTaskInfo(mTaskID, mItemTypeID);
                        if (dtInfo.Rows.Count > 0)
                        {
                            string strContract_Type = dtInfo.Rows[0]["CONTRACT_TYPE_CODE"].ToString();
                            string strREPORT_SERIAL = dtInfo.Rows[0]["REPORT_SERIAL"].ToString();
                            string strREPORT_SERIAL_1 = strREPORT_SERIAL.Substring(strREPORT_SERIAL.Length - 4);
                            dtInfo.Rows[0]["REPORT_SERIAL"] = strREPORT_SERIAL_1;//清远取报告号后四位

                            for (int j = 0; j < dtInfo.Columns.Count; j++)
                            {
                                string strText = dtInfo.Columns[j].ColumnName.ToString();//插入的标签名 对应数据表列名
                                string strValue = dtInfo.Rows[0][j].ToString();//值
                                MsgObj.SetMsgByName(strText, strValue);
                            }

                            MsgObj.SetMsgByName("RPT_APP_YEAR", DateTime.Now.Year.ToString());//报告编制年
                            MsgObj.SetMsgByName("RPT_APP_MONTH", DateTime.Now.Month.ToString());//报告编制月
                            MsgObj.SetMsgByName("RPT_APP_DAY", DateTime.Now.Day.ToString());//报告编制日

                            MsgObj.SetMsgByName("REPORT_DATE", DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日");//报告日期

                            MsgObj.SetMsgByName("PROJECT_NAME_HEADER", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["PROJECT_NAME"].ToString() : "");//页眉项目名称
                            MsgObj.SetMsgByName("PROJECT_NAME_BODY", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["PROJECT_NAME"].ToString() : "");//正文项目名称

                            MsgObj.SetMsgByName("REPORT_YEAR_HEADER", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["REPORT_YEAR"].ToString() : "");//页眉报告年度
                            MsgObj.SetMsgByName("REPORT_SERIAL_HEADER", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["REPORT_SERIAL"].ToString() : "");//页眉报告编号

                            MsgObj.SetMsgByName("TESTED_COMPANY_TITLE", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["TESTED_COMPANY"].ToString() : "");//封面检测单位

                            if (strContract_Type == "05")
                            {
                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "验收监测");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "验收监测");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "YS");
                                MsgObj.SetMsgByName("REPORT_ZI", "YS");
                            }
                            else if (strContract_Type == "10")
                            {
                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "监督性监测（国控）");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "监督性监测（国控）");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "WR");
                                MsgObj.SetMsgByName("REPORT_ZI", "WR");
                            }
                            else if (strContract_Type == "11")
                            {
                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "监督性监测（省控）");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "监督性监测（省控）");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "WR");
                                MsgObj.SetMsgByName("REPORT_ZI", "WR");
                            }
                            else if (strContract_Type == "12")
                            {
                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "监督性监测（重金属）");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "监督性监测（重金属）");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "WR");
                                MsgObj.SetMsgByName("REPORT_ZI", "WR");
                            }
                            else if (strContract_Type == "13")
                            {
                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "监督性监测");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "监督性监测");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "WR");
                                MsgObj.SetMsgByName("REPORT_ZI", "WR");
                            }
                            else
                            {

                                MsgObj.SetMsgByName("CONTRACT_TYPE_QY", "污染源监测");
                                MsgObj.SetMsgByName("TEST_PURPOSE_QY", "污染源监测");
                                MsgObj.SetMsgByName("REPORT_ZI_HEAD", "WR");
                                MsgObj.SetMsgByName("REPORT_ZI", "WR");
                            }
                        }
                        #endregion

                        #region 天气情况
                        string strWeather = "";
                        string strWeatherNoise = "";

                        string strCLEAR_RAIN = "";
                        string strWIND_SPEED = "";
                        string strSAMPLE_MACHINE = "";
                        string strSAMPLE_METHOD = "";

                        //通用天气
                        DataTable dtWeather = new ReportBuildLogic().getWeatherInfo(mTaskID, mItemTypeID, "gerenal_weather");
                        //噪声天气情况
                        DataTable dtWeatherNoise = new ReportBuildLogic().getWeatherInfo(mTaskID, "000000004", "noise_weather");

                        if (dtWeather.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtWeather.Rows)
                            {
                                strWeather += dr["name"].ToString() + "-" + dr["value"].ToString() + "  ";
                            }
                            MsgObj.SetMsgByName("WEATHER_INFO", strWeather);
                        }
                        if (dtWeatherNoise.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtWeatherNoise.Rows)
                            {
                                strWeatherNoise += dr["name"].ToString() + "-" + dr["value"].ToString() + "  ";
                                if (dr["name"].ToString() == "天气")
                                    strCLEAR_RAIN = dr["value"].ToString();
                                if (dr["name"].ToString().Contains("风速"))
                                    strWIND_SPEED = dr["value"].ToString();
                                if (dr["name"].ToString() == "声级计型号")
                                    strSAMPLE_MACHINE = dr["value"].ToString() + "声级计";
                                if (dr["name"].ToString() == "采样方法依据")
                                    strSAMPLE_METHOD = dr["value"].ToString();
                            }
                            MsgObj.SetMsgByName("WEATHER_INFO_NOISE", strWeatherNoise);
                            MsgObj.SetMsgByName("CLEAR_RAIN", strCLEAR_RAIN);//晴雨
                            MsgObj.SetMsgByName("WIND_SPEED", strWIND_SPEED);//风速
                            MsgObj.SetMsgByName("SAMPLE_MACHINE", strSAMPLE_MACHINE);//采样仪器
                            MsgObj.SetMsgByName("SAMPLE_METHOD", strSAMPLE_METHOD);//采样依据
                        }
                        #endregion

                        #region 加载样品号、点位、样品性状
                        DataTable dtSampleinfoST = new ReportBuildLogic().SelSampleInfoWater_ST(mTaskID, mItemTypeID);

                        string strSampleCodeList = "";//样品编码
                        string strSampleinfoST = "";//样品号、点位、样品性状
                        string strSampleNameList = "";//样品名称集合
                        string strSampleStatusList = "";//样品状态说明（样品名称+状态）
                        string strPoint = "";//点位或者样品
                        if (null != dtSampleinfoST && dtSampleinfoST.Rows.Count > 0)
                        {
                            for (int v = 0; v < dtSampleinfoST.Rows.Count; v++)
                            {
                                string strsSample_code = dtSampleinfoST.Rows[v]["sample_code"].ToString();
                                string strsOutlet_name = dtSampleinfoST.Rows[v]["point_name"].ToString();
                                string strsatt_value = dtSampleinfoST.Rows[v]["ATTRBUTE_VALUE"].ToString();
                                string strSampleName = dtSampleinfoST.Rows[v]["sample_name"].ToString();

                                //strSampleinfoST += (strSampleinfoST.Trim().Length > 0 ? "\r\n\t\t" : "") + strsSample_code + "  " + strsOutlet_name + "  " + strsatt_value;
                                strSampleinfoST += (strSampleinfoST.Trim().Length > 0 ? "\r\n\t\t\t  " : "") + strsSample_code + "  " + strsatt_value;
                                strSampleCodeList += strsSample_code + "，";
                                strSampleNameList += (strSampleNameList.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName;
                                strSampleStatusList += (strSampleNameList.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName + "：" + strsatt_value;
                                strPoint += (strPoint.Trim().Length > 0 ? "\r\n\t\t" : "") + strsOutlet_name;
                            }

                            MsgObj.SetMsgByName("SAMPLE_REMARK", strSampleinfoST);
                            MsgObj.SetMsgByName("SAMPLE_CODELIST", strSampleCodeList.Length > 0 ? strSampleCodeList.Remove(strSampleCodeList.LastIndexOf("，")) : "");
                            MsgObj.SetMsgByName("SAMPLE_NAME", strSampleNameList);
                            MsgObj.SetMsgByName("SAMPLE_INFO", strSampleStatusList);

                            MsgObj.SetMsgByName("TESTED_OUTLET", strPoint);
                        }
                        else //没有则是送样
                        {
                            DataTable dtSampleinfoST_SendSample = new ReportBuildLogic().SelSampleInfoWater_ST_forSendSanple(mTaskID, mItemTypeID);
                            strSampleinfoST = "";
                            if (null != dtSampleinfoST_SendSample && dtSampleinfoST_SendSample.Rows.Count > 0)
                            {
                                for (int v = 0; v < dtSampleinfoST_SendSample.Rows.Count; v++)
                                {
                                    string strsSample_code = dtSampleinfoST_SendSample.Rows[v]["sample_code"].ToString();
                                    string strSampleName = dtSampleinfoST_SendSample.Rows[v]["sample_name"].ToString();

                                    strSampleinfoST += (strSampleinfoST.Trim().Length > 0 ? "\r\n\t\t" : "") + strsSample_code;
                                    strSampleNameList += (strSampleNameList.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName;
                                    strPoint += (strPoint.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName;
                                }

                                MsgObj.SetMsgByName("SAMPLE_REMARK", strSampleinfoST);
                                MsgObj.SetMsgByName("SAMPLE_CODELIST", strSampleCodeList.Length > 0 ? strSampleCodeList.Remove(strSampleCodeList.LastIndexOf("，")) : "");
                                MsgObj.SetMsgByName("SAMPLE_NAME", strSampleNameList);
                                MsgObj.SetMsgByName("SAMPLE_INFO", strSampleStatusList);

                                MsgObj.SetMsgByName("TESTED_OUTLET", strPoint);
                            }
                        }
                        #endregion

                        #region 采样日期、采样人、监测结论
                        string strContractMan = "";//监测人员（采样人+分析人）
                        string strSampleMan = "";//采样人
                        DateTime minSampleDate = new DateTime();//最早采样时间
                        DateTime maxSampleDate = new DateTime();//最迟采样时间
                        //string strSAMPLE_ACCESS_DATE = "";//接样时间
                        string strSampleAccessUser = ""; //接样人员
                        string strSampleAccessTime = ""; //接样时间

                        DataTable dtSubTask = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = mTaskID });
                        if (dtSubTask.Rows.Count > 0)
                        {
                            //strSAMPLE_ACCESS_DATE = dtSubTask.Rows[0]["SAMPLE_ACCESS_DATE"].ToString();
                            foreach (DataRow dr in dtSubTask.Rows)
                            {
                                #region add by:weilin 把水、气、声的采样人员和采样时间分开显示（清远）
                                if (dr["SAMPLE_ASK_DATE"].ToString() != "")
                                    strSampleTime = DateTime.Parse(dr["SAMPLE_ASK_DATE"].ToString()).ToString("yyyy年MM月dd日");
                                else
                                    strSampleTime = "";
                                string strSampleUser = (new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(dr["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME + "、" + dr["SAMPLING_MAN"].ToString().Replace(",", "、")).TrimEnd('、');

                                if (dr["SAMPLE_ACCESS_DATE"].ToString() != "")
                                    strSampleAccessTime = DateTime.Parse(dr["SAMPLE_ACCESS_DATE"].ToString()).ToString("yyyy年MM月dd日");
                                else
                                    strSampleAccessTime = "";
                                strSampleAccessUser = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(dr["SAMPLE_ACCESS_ID"].ToString()).REAL_NAME;

                                switch (dr["MONITOR_ID"].ToString())
                                {
                                    case "000000001":  //水
                                        MsgObj.SetMsgByName("SAMPLE_TIME_WATER", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_WATER", strSampleUser);

                                        MsgObj.SetMsgByName("SAMPLE_ACCESS_DATE_WATER", strSampleAccessTime);
                                        MsgObj.SetMsgByName("SAMPLE_ACCESS_USER_WATER", strSampleAccessUser);

                                        MsgObj.SetMsgByName("SAMPLE_SEND_DATE_WATER", dtInfo.Rows.Count > 0 ? DateTime.Parse(dtInfo.Rows[0]["CREATE_DATE"].ToString()).ToString("yyyy年MM月dd日") : "");
                                        MsgObj.SetMsgByName("SAMPLE_SEND_USER_WATER", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["SAMPLE_SEND_MAN"].ToString() : "");
                                        break;
                                    case "000000002":  //气
                                        MsgObj.SetMsgByName("SAMPLE_TIME_GAS", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_GAS", strSampleUser);

                                        MsgObj.SetMsgByName("SAMPLE_ACCESS_DATE_GAS", strSampleAccessTime);
                                        MsgObj.SetMsgByName("SAMPLE_ACCESS_USER_GAS", strSampleAccessUser);

                                        MsgObj.SetMsgByName("SAMPLE_SEND_DATE_GAS", dtInfo.Rows.Count > 0 ? DateTime.Parse(dtInfo.Rows[0]["CREATE_DATE"].ToString()).ToString("yyyy年MM月dd日") : "");
                                        MsgObj.SetMsgByName("SAMPLE_SEND_USER_GAS", dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["SAMPLE_SEND_MAN"].ToString() : "");
                                        //NOX、SO2、烟尘
                                        MsgObj.SetMsgByName("SAMPLE_TIME_GAS_SO2", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_GAS_SO2", strSampleUser);
                                        //油烟
                                        MsgObj.SetMsgByName("SAMPLE_TIME_GAS_YY", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_GAS_YY", strSampleUser);
                                        //烟气黑度
                                        MsgObj.SetMsgByName("SAMPLE_TIME_GAS_YH", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_GAS_YH", strSampleUser);
                                        break;
                                    case "000000004":  //声
                                        MsgObj.SetMsgByName("SAMPLE_TIME_NOISE", strSampleTime);
                                        MsgObj.SetMsgByName("SAMPLE_USER_NOISE", strSampleUser);
                                        break;
                                    default:
                                        break;
                                }

                                #endregion

                                if (mItemTypeID.IndexOf(dr["MONITOR_ID"].ToString()) >= 0)
                                {
                                    //采样负责人
                                    string strSAMPLING_MANAGER_ID = dr["SAMPLING_MANAGER_ID"].ToString();
                                    string strSAMPLING_MANAGER = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strSAMPLING_MANAGER_ID).REAL_NAME;
                                    if (strSAMPLING_MANAGER.Length > 0 && !strSampleMan.Contains(strSAMPLING_MANAGER))
                                    {
                                        strSampleMan += strSAMPLING_MANAGER + "、";
                                    }

                                    //监测人员&&采样人员
                                    string SampleMan = dr["SAMPLING_MAN"].ToString();
                                    if (SampleMan.Length > 0)
                                    {
                                        string[] arrSampleMan = SampleMan.Split(',');
                                        foreach (string str in arrSampleMan)
                                        {
                                            if (str.Length > 0 && !strSampleMan.Contains(str))
                                            {
                                                strSampleMan += str + "、";
                                            }
                                        }
                                    }
                                    try
                                    {
                                        DateTime time = DateTime.Parse(dr["SAMPLE_FINISH_DATE"].ToString());
                                        if (time > maxSampleDate)
                                        {
                                            maxSampleDate = time;
                                        }
                                        if (time < minSampleDate)
                                        {
                                            minSampleDate = time;
                                        }
                                    }
                                    catch { }
                                }
                            }
                            if (minSampleDate != maxSampleDate && minSampleDate != new DateTime() && maxSampleDate != new DateTime())
                            {
                                MsgObj.SetMsgByName("SAMPLE_TIME", minSampleDate.ToString("yyyy年MM月dd日") + "-" + maxSampleDate.ToString("yyyy年MM月dd日"));
                            }
                            else
                            {
                                MsgObj.SetMsgByName("SAMPLE_TIME", maxSampleDate.ToString("yyyy年MM月dd日"));
                            }
                            //if (strSAMPLE_ACCESS_DATE.Length > 0)
                            //{
                            //    MsgObj.SetMsgByName("SAMPLE_ACCESS_DATE", strSAMPLE_ACCESS_DATE);
                            //}
                            if (strSampleMan.Length > 0)
                            {
                                MsgObj.SetMsgByName("SAMPLE_USER", strSampleMan.Remove(strSampleMan.LastIndexOf('、')));
                            }
                            else
                            {
                                MsgObj.SetMsgByName("SAMPLE_USER", strSampleMan);
                            }

                            MsgObj.SetMsgByName("SAMPLE_USER", strSampleMan.Length > 0 ? strSampleMan.Remove(strSampleMan.LastIndexOf("、")) : "");//采样人员
                        }
                        else
                        {
                            MsgObj.SetMsgByName("SAMPLE_TIME", "");
                            MsgObj.SetMsgByName("SAMPLE_USER", "无。");
                        }
                        strContractMan += strSampleMan;
                        #endregion

                        #region 分析日期、分析人员（负责人+协同人）
                        string strAnalyseManList = "";//分析人员
                        DateTime minAnalysisDate = new DateTime();//最早分析时间
                        DateTime maxAnalysisDate = new DateTime();//最迟分析时间
                        DataTable dtAnalysisApp = new TMisMonitorResultAppLogic().SelectByTableByTaskID(mTaskID, mItemTypeID);
                        if (dtAnalysisApp.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtAnalysisApp.Rows)
                            {
                                //分析负责人员
                                string AnalyseHeadMan = dr["HEADER"].ToString();
                                if (AnalyseHeadMan.Length > 0)
                                {
                                    if (!strAnalyseManList.Contains(AnalyseHeadMan))
                                    {
                                        strAnalyseManList += AnalyseHeadMan + "、";
                                    }
                                }
                                //分析协同人员
                                string AnalyseMan = dr["ANALYSIER"].ToString();
                                if (AnalyseMan.Length > 0)
                                {
                                    if (!strAnalyseManList.Contains(AnalyseMan))
                                    {
                                        strAnalyseManList += AnalyseMan + "、";
                                    }
                                }
                                try
                                {
                                    string tempTime = dr["FINISH_DATE"].ToString();
                                    if (String.IsNullOrEmpty(tempTime.ToString()))
                                    {
                                        maxAnalysisDate = DateTime.Parse(strSampleTime);
                                        minAnalysisDate = DateTime.Parse(strSampleTime);
                                    }
                                    else
                                    {
                                        DateTime time = DateTime.Parse(tempTime);//分析实际完成时间
                                        if (time > maxAnalysisDate)
                                        {
                                            maxAnalysisDate = time;
                                        }
                                        if (time < minAnalysisDate)
                                        {
                                            minAnalysisDate = time;
                                        }
                                    }
                                }
                                catch { }
                            }
                            if (minAnalysisDate != maxAnalysisDate && minAnalysisDate != new DateTime() && maxAnalysisDate != new DateTime())
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME", minAnalysisDate.ToString("yyyy年MM月dd日") + "-" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                            }
                            else
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                            }
                            strContractMan += strAnalyseManList;
                            MsgObj.SetMsgByName("ANALYSE_USER", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            MsgObj.SetMsgByName("CONTRACT_PEOPLE", strContractMan.Length > 0 ? strContractMan.Remove(strContractMan.LastIndexOf("、")) : ""); //监测人员（采样人员+分析人员）
                            MsgObj.SetMsgByName("RPT_WRITER", base.LogInfo.UserInfo.REAL_NAME);//【报告人员】(报告编写人)
                        }
                        else
                        {
                            MsgObj.SetMsgByName("ANALYSE_TIME", "");
                            MsgObj.SetMsgByName("ANALYSE_USER", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            MsgObj.SetMsgByName("CONTRACT_PEOPLE", strContractMan.Length > 0 ? strContractMan.Remove(strContractMan.LastIndexOf("、")) : ""); //监测人员（采样人员+分析人员）
                            MsgObj.SetMsgByName("RPT_WRITER", base.LogInfo.UserInfo.REAL_NAME);//【报告人员】(报告编写人)
                        }
                        //清远综合报告的分析人员、分析时间
                        if (mItemTypeID == "")
                        {
                            DataTable dtAnalysisApp_Water = new TMisMonitorResultAppLogic().SelectByTableByTaskID(mTaskID, "000000001");
                            strAnalyseManList = "";//分析人员
                            minAnalysisDate = new DateTime(2990, 1, 1);//最早分析时间
                            maxAnalysisDate = new DateTime(1990, 1, 1);//最迟分析时间
                            if (dtAnalysisApp_Water.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtAnalysisApp_Water.Rows)
                                {
                                    //分析负责人员
                                    string AnalyseHeadMan = dr["HEADER"].ToString();
                                    if (AnalyseHeadMan.Length > 0)
                                    {
                                        if (!strAnalyseManList.Contains(AnalyseHeadMan))
                                        {
                                            strAnalyseManList += AnalyseHeadMan + "、";
                                        }
                                    }
                                    //分析协同人员
                                    string[] AnalyseManlist = dr["ASSISTANT_USERID"].ToString().Split(',');
                                    if (AnalyseManlist.Length > 0)
                                    {
                                        for (int i = 0; i < AnalyseManlist.Length; i++)
                                        {
                                            string AnalyseMan = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(AnalyseManlist[i].ToString()).REAL_NAME;
                                            if (AnalyseMan.Length > 0)
                                            {
                                                if (!strAnalyseManList.Contains(AnalyseMan))
                                                {
                                                    strAnalyseManList += AnalyseMan + "、";
                                                }
                                            }
                                        }

                                    }
                                    try
                                    {
                                        string tempTime = dr["FINISH_DATE"].ToString();

                                        if (String.IsNullOrEmpty(tempTime.ToString()))
                                        {
                                            maxAnalysisDate = DateTime.Parse(strSampleTime);
                                            minAnalysisDate = DateTime.Parse(strSampleTime);
                                        }
                                        else
                                        {
                                            DateTime time = DateTime.Parse(dr["FINISH_DATE"].ToString());//分析实际完成时间
                                            if (time > maxAnalysisDate)
                                            {
                                                maxAnalysisDate = time;
                                            }
                                            if (time < minAnalysisDate)
                                            {
                                                minAnalysisDate = time;
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                if (minAnalysisDate != maxAnalysisDate)
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_WATER", minAnalysisDate.ToString("yyyy年MM月dd日") + "-" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                else
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_WATER", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                strContractMan += strAnalyseManList;
                                MsgObj.SetMsgByName("ANALYSE_USER_WATER", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            }
                            else
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME_WATER", "");
                                MsgObj.SetMsgByName("ANALYSE_USER_WATER", "");//分析人员
                            }
                            //废气
                            DataTable dtAnalysisApp_Gas = new TMisMonitorResultAppLogic().SelectByTableByTaskID(mTaskID, "000000002");
                            strAnalyseManList = "";//分析人员
                            minAnalysisDate = new DateTime(2990, 1, 1);//最早分析时间
                            maxAnalysisDate = new DateTime(1990, 1, 1);//最迟分析时间
                            if (dtAnalysisApp_Gas.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtAnalysisApp_Gas.Rows)
                                {
                                    //分析负责人员
                                    string AnalyseHeadMan = dr["HEADER"].ToString();
                                    if (AnalyseHeadMan.Length > 0)
                                    {
                                        if (!strAnalyseManList.Contains(AnalyseHeadMan))
                                        {
                                            strAnalyseManList += AnalyseHeadMan + "、";
                                        }
                                    }
                                    //分析协同人员
                                    string[] AnalyseManlist = dr["ASSISTANT_USERID"].ToString().Split(',');
                                    if (AnalyseManlist.Length > 0)
                                    {
                                        for (int i = 0; i < AnalyseManlist.Length; i++)
                                        {
                                            string AnalyseMan = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(AnalyseManlist[i].ToString()).REAL_NAME;
                                            if (AnalyseMan.Length > 0)
                                            {
                                                if (!strAnalyseManList.Contains(AnalyseMan))
                                                {
                                                    strAnalyseManList += AnalyseMan + "、";
                                                }
                                            }
                                        }

                                    }
                                    try
                                    {
                                        string tempTime = dr["FINISH_DATE"].ToString();

                                        if (String.IsNullOrEmpty(tempTime.ToString()))
                                        {
                                            maxAnalysisDate = DateTime.Parse(strSampleTime);
                                            minAnalysisDate = DateTime.Parse(strSampleTime);
                                        }
                                        else
                                        {
                                            DateTime time = DateTime.Parse(dr["FINISH_DATE"].ToString());//分析实际完成时间
                                            if (time > maxAnalysisDate)
                                            {
                                                maxAnalysisDate = time;
                                            }
                                            if (time < minAnalysisDate)
                                            {
                                                minAnalysisDate = time;
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                if (minAnalysisDate != maxAnalysisDate)
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS", minAnalysisDate.ToString("yyyy年MM月dd日") + "-" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                else
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                strContractMan += strAnalyseManList;
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            }
                            else
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME_GAS", "");
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS", "");//分析人员
                            }
                            //NOX、SO2、烟尘
                            DataRow[] drAnalysisApp_Gas = dtAnalysisApp_Gas.Select("ITEM_ID in('000000108','000000114','000001827','000001945')");
                            strAnalyseManList = "";//分析人员
                            minAnalysisDate = new DateTime(2990, 1, 1);//最早分析时间
                            maxAnalysisDate = new DateTime(1990, 1, 1);//最迟分析时间
                            if (drAnalysisApp_Gas.Length > 0)
                            {
                                foreach (DataRow dr in drAnalysisApp_Gas)
                                {
                                    //分析负责人员
                                    string AnalyseHeadMan = dr["HEADER"].ToString();
                                    if (AnalyseHeadMan.Length > 0)
                                    {
                                        if (!strAnalyseManList.Contains(AnalyseHeadMan))
                                        {
                                            strAnalyseManList += AnalyseHeadMan + "、";
                                        }
                                    }
                                    //分析协同人员
                                    string[] AnalyseManlist = dr["ASSISTANT_USERID"].ToString().Split(',');
                                    if (AnalyseManlist.Length > 0)
                                    {
                                        for (int i = 0; i < AnalyseManlist.Length; i++)
                                        {
                                            string AnalyseMan = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(AnalyseManlist[i].ToString()).REAL_NAME;
                                            if (AnalyseMan.Length > 0)
                                            {
                                                if (!strAnalyseManList.Contains(AnalyseMan))
                                                {
                                                    strAnalyseManList += AnalyseMan + "、";
                                                }
                                            }
                                        }

                                    }
                                    try
                                    {
                                        string tempTime = dr["FINISH_DATE"].ToString();
                                        if (String.IsNullOrEmpty(tempTime.ToString()))
                                        {
                                            maxAnalysisDate = DateTime.Parse(strSampleTime);
                                            minAnalysisDate = DateTime.Parse(strSampleTime);
                                        }
                                        else
                                        {
                                            DateTime time = DateTime.Parse(dr["FINISH_DATE"].ToString());//分析实际完成时间
                                            if (time > maxAnalysisDate)
                                            {
                                                maxAnalysisDate = time;
                                            }
                                            if (time < minAnalysisDate)
                                            {
                                                minAnalysisDate = time;
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                if (minAnalysisDate != maxAnalysisDate)
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS_SO2", minAnalysisDate.ToString("yyyy年MM月dd日") + "-" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                else
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS_SO2", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                strContractMan += strAnalyseManList;
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS_SO2", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            }
                            else
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME_GAS_SO2", "");
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS_SO2", "");//分析人员
                            }
                            //油烟
                            drAnalysisApp_Gas = dtAnalysisApp_Gas.Select("ITEM_ID in('000000135')");
                            strAnalyseManList = "";//分析人员
                            minAnalysisDate = new DateTime(2990, 1, 1);//最早分析时间
                            maxAnalysisDate = new DateTime(1990, 1, 1);//最迟分析时间
                            if (drAnalysisApp_Gas.Length > 0)
                            {
                                foreach (DataRow dr in drAnalysisApp_Gas)
                                {
                                    //分析负责人员
                                    string AnalyseHeadMan = dr["HEADER"].ToString();
                                    if (AnalyseHeadMan.Length > 0)
                                    {
                                        if (!strAnalyseManList.Contains(AnalyseHeadMan))
                                        {
                                            strAnalyseManList += AnalyseHeadMan + "、";
                                        }
                                    }
                                    //分析协同人员
                                    string[] AnalyseManlist = dr["ASSISTANT_USERID"].ToString().Split(',');
                                    if (AnalyseManlist.Length > 0)
                                    {
                                        for (int i = 0; i < AnalyseManlist.Length; i++)
                                        {
                                            string AnalyseMan = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(AnalyseManlist[i].ToString()).REAL_NAME;
                                            if (AnalyseMan.Length > 0)
                                            {
                                                if (!strAnalyseManList.Contains(AnalyseMan))
                                                {
                                                    strAnalyseManList += AnalyseMan + "、";
                                                }
                                            }
                                        }

                                    }
                                    try
                                    {
                                        string tempTime = dr["FINISH_DATE"].ToString();

                                        if (String.IsNullOrEmpty(tempTime.ToString()))
                                        {
                                            maxAnalysisDate = DateTime.Parse(strSampleTime);
                                            minAnalysisDate = DateTime.Parse(strSampleTime);
                                        }
                                        else
                                        {
                                            DateTime time = DateTime.Parse(tempTime);//分析实际完成时间
                                            if (time > maxAnalysisDate)
                                            {
                                                maxAnalysisDate = time;
                                            }
                                            if (time < minAnalysisDate)
                                            {
                                                minAnalysisDate = time;
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                if (minAnalysisDate != maxAnalysisDate)
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS_YY", minAnalysisDate.ToString("yyyy年MM月dd日") + "-" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                else
                                {
                                    MsgObj.SetMsgByName("ANALYSE_TIME_GAS_YY", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                                }
                                strContractMan += strAnalyseManList;
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS_YY", strAnalyseManList.Length > 0 ? strAnalyseManList.Remove(strAnalyseManList.LastIndexOf("、")) : "");//分析人员
                            }
                            else
                            {
                                MsgObj.SetMsgByName("ANALYSE_TIME_GAS_YY", "");
                                MsgObj.SetMsgByName("ANALYSE_USER_GAS_YY", "");//分析人员
                            }
                        }
                        #endregion

                        #region 采样类别、监测点位、监测频次（郑州用）
                        //采样类别
                        string strSampleSource = "";
                        if (dtInfo.Rows.Count > 0)//update by ssz 数据存在性判断
                        {
                            if (dtInfo.Rows[0]["SAMPLE_SOURCE"].ToString().Contains("送样"))
                            {
                                strSampleSource = "送样■        抽样□        现场监测□";
                            }
                            else
                            {
                                strSampleSource = "送样□        抽样■        现场监测□";

                                DataTable objTableTemp = new ReportBuildLogic().getItemInfoForReport_andIsSampleDept(mTaskID, mItemTypeID);
                                DataRow[] objIsLocal = objTableTemp.Select("IS_SAMPLEDEPT='是'");

                                if (objTableTemp.Rows.Count > 0 && objIsLocal.Length > 0)
                                {
                                    if (objIsLocal.Length == objTableTemp.Rows.Count)
                                    {
                                        strSampleSource = "送样□        抽样□        现场监测■";
                                    }
                                    else
                                    {
                                        strSampleSource = "送样□        抽样■        现场监测■";
                                    }
                                }
                            }
                            MsgObj.SetMsgByName("MONITOR_WAY", strSampleSource);
                        }

                        //监测点位 
                        //"废水：总排口，处理设施前。" 
                        //"废气：总排口，处理设施前。"
                        string strTESTED_POINT = "";
                        string strTempMonitorID = "";
                        if (dtSampleinfoST.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtSampleinfoST.Rows.Count; i++)
                            {
                                if (strTempMonitorID == dtSampleinfoST.Rows[i]["MONITOR_ID"].ToString())
                                    continue;
                                strTempMonitorID = dtSampleinfoST.Rows[i]["MONITOR_ID"].ToString();

                                DataRow[] drTESTED_POINT = dtSampleinfoST.Select("MONITOR_ID='" + strTempMonitorID + "'");

                                string strTESTED_POINTs = "";
                                for (int j = 0; j < drTESTED_POINT.Length; j++)
                                {
                                    string strTmpTESTED_POINT = drTESTED_POINT[j]["POINT_NAME"].ToString();
                                    if (!strTESTED_POINTs.Contains(strTmpTESTED_POINT))
                                    {
                                        strTESTED_POINTs += (strTESTED_POINTs.Length > 0 ? "，" : "") + strTmpTESTED_POINT;
                                    }
                                }

                                string strTempMonitorName = new TBaseMonitorTypeInfoLogic().Details(strTempMonitorID).MONITOR_TYPE_NAME;
                                strTESTED_POINTs = strTempMonitorName + "：" + strTESTED_POINTs + "。\r\n\t\t";

                                strTESTED_POINT += strTESTED_POINTs;
                            }
                            MsgObj.SetMsgByName("TESTED_POINT", strTESTED_POINT);
                        }

                        //监测频次 
                        //"废水：总排口监测一天，一天监测3次。"
                        //"      处理设施前，一天监测2次。" 
                        //"废气：总排口监测一天，一天监测3次。"
                        //"      处理设施前，一天监测2次。"
                        if (dtSampleinfoST.Rows.Count > 0)
                        {
                            string strTESTED_FREQ = "";
                            strTempMonitorID = "";
                            for (int i = 0; i < dtSampleinfoST.Rows.Count; i++)
                            {
                                if (strTempMonitorID == dtSampleinfoST.Rows[i]["MONITOR_ID"].ToString())
                                    continue;
                                strTempMonitorID = dtSampleinfoST.Rows[i]["MONITOR_ID"].ToString();
                                string strTempMonitorName = new TBaseMonitorTypeInfoLogic().Details(strTempMonitorID).MONITOR_TYPE_NAME;

                                DataRow[] drTESTED_POINT = dtSampleinfoST.Select("MONITOR_ID='" + strTempMonitorID + "'");

                                string strTESTED_POINTs = "";
                                string strChkPoints = "";
                                for (int j = 0; j < drTESTED_POINT.Length; j++)
                                {
                                    //string strTmpTESTED_POINT = drTESTED_POINT[j]["POINT_NAME"].ToString();
                                    //if (!strChkPoints.Contains(strTmpTESTED_POINT))
                                    //{
                                    //    strChkPoints += "，" + strTmpTESTED_POINT + "，";

                                    //    DataRow[] drTESTED_Freq = dtSampleinfoST.Select("MONITOR_ID='" + strTempMonitorID + "' and POINT_NAME='" + strTmpTESTED_POINT + "'");

                                    //    strTESTED_POINTs += (strTESTED_POINTs.Length == 0 ? (strTempMonitorName + "：") : "      ") + strTmpTESTED_POINT + "监测一天，一天监测" + drTESTED_Freq.Length + "次。\r\n";
                                    //}
                                    string strTmpTESTED_POINT = drTESTED_POINT[j]["CONTRACT_POINT_ID"].ToString();
                                    string strTmpTESTED_POINTName = drTESTED_POINT[j]["POINT_NAME"].ToString();
                                    if (!strChkPoints.Contains(strTmpTESTED_POINT))
                                    {
                                        strChkPoints += "，" + strTmpTESTED_POINT + "，";

                                        TMisContractPointVo objContractPoint = new TMisContractPointLogic().Details(strTmpTESTED_POINT);
                                        strTESTED_POINTs += (strTESTED_POINTs.Length == 0 ? (strTempMonitorName + "：") : "      ") + strTmpTESTED_POINTName + "监测" + objContractPoint.SAMPLE_DAY + "天，一天监测" + objContractPoint.SAMPLE_FREQ + "次。\r\n";
                                    }
                                }

                                strTESTED_FREQ += strTESTED_POINTs;
                            }
                            MsgObj.SetMsgByName("TESTED_FREQ", strTESTED_FREQ);
                        }
                        #endregion
                        #endregion
                        break;
                    //--------------保存文档标签-------------//
                    case "SAVEBOOKMARKS":
                        #region SAVEBOOKMARKS
                        //取得模板编号
                        mTemplate = MsgObj.GetMsgByName("TEMPLATE");
                        StringBuilder MarkNameList = new StringBuilder();
                        //前6个为系统变量，从第7个开始为标签
                        if (MsgObj.GetFieldCount() > 6)
                        {
                            for (int i = 7; i <= MsgObj.GetFieldCount() - 1; i++)
                            {
                                MarkNameList.Append(MsgObj.GetFieldName(i) + "&");
                            }
                            if (MarkNameList.Length > 0)
                            {
                                //剔除末位的"&"字符
                                string MarkNameTemp = MarkNameList.ToString();
                                if (MarkNameTemp.EndsWith("&"))
                                {
                                    MarkNameTemp.Remove(MarkNameTemp.Length - 1, 1);
                                }
                                if (WebOffice.SaveBookMarks(MarkNameTemp, mTemplate))
                                {
                                    MsgObj.MsgError("保存标签信息成功!");
                                }
                                else
                                {
                                    MsgObj.MsgError("保存标签信息失败!");
                                }
                            }
                            else
                            {
                                MsgObj.MsgError("无标签可以保存!");
                            }
                        }
                        else
                        {
                            MsgObj.MsgError("保存标签信息失败!");
                        }
                        MsgObj.MsgTextClear();
                        #endregion
                        break;
                    //--------------显示标签列表-------------//
                    case "LISTBOOKMARKS":
                        #region LISTBOOKMARKS
                        //清除文本信息
                        MsgObj.MsgTextClear();
                        //定义输出变量
                        string lMarkName;
                        string lMarkDesc;
                        //读取相应信息
                        WebOffice.ListBookMarks(out lMarkName, out lMarkDesc);

                        MsgObj.SetMsgByName("BOOKMARK", lMarkName);	                  //将用户名列表打包
                        MsgObj.SetMsgByName("DESCRIPT", lMarkDesc);	                  //将说明信息列表打包
                        MsgObj.MsgError("");			                              //清除错误信息
                        #endregion
                        break;
                    #endregion

                    #region 印章 无用，不需要看
                    //--------------创建印章列表-------------//
                    case "LOADMARKLIST":
                        MsgObj.MsgTextClear();                                            //清除文本信息
                        if (WebOffice.LoadMarkList(out mMarkList))
                        {
                            MsgObj.SetMsgByName("MARKLIST", mMarkList);                    //显示签章列表
                            MsgObj.MsgError("");				                          //清除错误信息
                        }
                        else
                        {
                            MsgObj.MsgError("创建印章列表失败!");			              //设置错误信息
                        }
                        break;
                    //--------------打开印章文件-------------//
                    case "LOADMARKIMAGE":
                        mMarkName = MsgObj.GetMsgByName("IMAGENAME");	                     //取得签名名称
                        mUserName = MsgObj.GetMsgByName("USERNAME");		                 //取得用户名称
                        mPassword = MsgObj.GetMsgByName("PASSWORD");		                 //取得用户密码
                        mFileType = ".jpg";                                                  //默认为.jpg类型
                        MsgObj.MsgTextClear();
                        if (WebOffice.LoadMarkImage(mUserName, mPassword, out mFileBody, out mFileType)) 	                         //调入签名信息
                        {
                            MsgObj.SetMsgByName("IMAGETYPE", mFileType);                     //设置签名类型
                            MsgObj.MsgFileBody(mFileBody);			                         //将文件信息打包
                            MsgObj.SetMsgByName("STATUS", "打开成功!");  	                 //设置状态信息
                            MsgObj.MsgError("");				                             //清除错误信息
                        }
                        else
                        {
                            MsgObj.MsgError("签名或密码错误!");		                         //设置错误信息
                        }
                        break;
                    //--------------保存签章基本信息-------------//
                    case "SAVESIGNATURE":
                        mRecordID = MsgObj.GetMsgByName("RECORDID");		                //取得文档编号
                        mFileName = MsgObj.GetMsgByName("FILENAME");		                //取得标签文档内容
                        mMarkName = MsgObj.GetMsgByName("MARKNAME");		                //取得签名名称
                        mUserName = MsgObj.GetMsgByName("USERNAME");		                //取得用户名称
                        mDateTime = MsgObj.GetMsgByName("DATETIME");		                //取得签名时间
                        mHostName = Request.UserHostAddress.ToString();                     //取得用户IP
                        mMarkGuid = MsgObj.GetMsgByName("MARKGUID");                        //取得唯一编号
                        MsgObj.MsgTextClear();                                              //清除文本信息
                        TRptFileSignatureVo signature = new TRptFileSignatureVo();
                        signature.FILE_ID = mRecordID;
                        signature.MARK_NAME = mMarkName;
                        signature.ADD_USER = mUserName;
                        signature.ADD_TIME = mDateTime;
                        signature.ADD_IP = mHostName;
                        signature.MARK_GUID = mMarkGuid;

                        if (WebOffice.SaveSignature(signature)) 		        		    //保存印章
                        {
                            MsgObj.SetMsgByName("STATUS", "保存成功!");  	                //设置状态信息
                            MsgObj.MsgError("");				                            //清除错误信息
                        }
                        else
                        {
                            MsgObj.MsgError("保存印章失败!");		                        //设置错误信息
                        }
                        break;
                    //--------------调出签章基本信息-------------//
                    case "LOADSIGNATURE":
                        mRecordID = MsgObj.GetMsgByName("RECORDID");		            //取得文档编号
                        MsgObj.MsgTextClear();                                          //清除文本信息
                        TRptFileSignatureVo FileSignature = WebOffice.LoadSignature(mRecordID);
                        if (null != FileSignature) 		        	                        //调入文档
                        {
                            MsgObj.SetMsgByName("MARKNAME", FileSignature.MARK_NAME);       //将签名名称列表打包
                            MsgObj.SetMsgByName("USERNAME", FileSignature.ADD_USER);        //将用户名列表打包
                            MsgObj.SetMsgByName("DATETIME", FileSignature.ADD_TIME);        //将时间列表打包
                            MsgObj.SetMsgByName("HOSTNAME", FileSignature.ADD_IP);          //将说明信息列表打包
                            MsgObj.SetMsgByName("MARKGUID", FileSignature.MARK_GUID);
                            MsgObj.SetMsgByName("STATUS", "调入成功!");  	            //设置状态信息
                            MsgObj.MsgError("");				                        //清除错误信息
                        }
                        else
                        {
                            MsgObj.MsgError("调入印章失败!");		                    //设置错误信息
                        }
                        break;
                    #endregion

                    #region 文件操作 无用，不需要看
                    //--------------页面另存为HTML页面-------------//
                    case "SAVEASHTML":
                        mHtmlName = MsgObj.GetMsgByName("HTMLNAME");		            //取得标签文档内容
                        mDirectory = MsgObj.GetMsgByName("DIRECTORY");	                //取得标签文档内容
                        MsgObj.MsgTextClear();
                        if (mDirectory.Equals(""))
                        {
                            mFilePath = mFilePath + "\\HTML";
                        }
                        else
                        {
                            mFilePath = mFilePath + "\\HTML\\" + mDirectory;
                        }
                        MsgObj.MakeDirectory(mFilePath);							   //创建路径
                        if (MsgObj.MsgFileSave(mFilePath + "\\" + mHtmlName))
                        {

                            MsgObj.MsgError("");                                       //清除错误信息
                            MsgObj.SetMsgByName("STATUS", "保存成功");                 //设置状态信息
                        }
                        else
                        {
                            MsgObj.MsgError("保存失败");                               //设置错误信息
                        }
                        MsgObj.MsgFileClear();
                        break;
                    //--------------页面另存为HTML图片页面-------------//
                    case "SAVEIMAGE":
                        mFileName = MsgObj.GetMsgByName("HTMLNAME");	                  //取得文件名称
                        mDirectory = MsgObj.GetMsgByName("DIRECTORY");                    //取得目录名称
                        MsgObj.MsgTextClear();				                              //清除文本信息
                        if (mDirectory.Equals(""))
                        {
                            mFilePath = mFilePath + "\\HTMLIMAGE";
                        }
                        else
                        {
                            mFilePath = mFilePath + "\\HTMLIMAGE\\" + mDirectory;
                        }
                        MsgObj.MakeDirectory(mFilePath);                                  //创建路径
                        if (MsgObj.MsgFileSave(mFilePath + "\\" + mFileName))             //保存HTML图片文件
                        {
                            MsgObj.MsgError("");				                          //清除错误信息
                            MsgObj.SetMsgByName("STATUS", "保存HTML图片成功!");	          //设置状态信息
                        }
                        else
                        {
                            MsgObj.MsgError("保存HTML图片失败!");			              //设置错误信息
                        }
                        MsgObj.MsgFileClear();				                              //清除文档内容
                        break;
                    #endregion

                    #region 远程文件操作(停用，远程图片除外)---签名在此
                    case "INSERTIMAGE":
                        mTaskID = MsgObj.GetMsgByName("TASKID");                               //合同编号
                        mContractType = MsgObj.GetMsgByName("CONTRACT_TYPE");                  //合同类型
                        mConditionType = MsgObj.GetMsgByName("CONDITION_TYPE");                //监测条件类型
                        mLabelName = MsgObj.GetMsgByName("LABELNAME");                         //标签名
                        mImageName = MsgObj.GetMsgByName("IMAGENAME");                         //图片名

                        if (mContractType == "SKETCH_MAP")
                        {
                            DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = mTaskID, MONITOR_ID = "000000004" });
                            string strSubTaskId = "";
                            if (dt.Rows.Count > 0)
                                strSubTaskId = dt.Rows[0]["ID"].ToString();

                            if (strSubTaskId.Length > 0)
                            {
                                TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(new TOaAttVo { BUSINESS_ID = strSubTaskId, BUSINESS_TYPE = "PointMap" });
                                string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
                                string filePath = mastPath + '\\' + TOaAttVoTemp.UPLOAD_PATH;
                                if (filePath != "" && System.IO.File.Exists(filePath))
                                {
                                    #region 图片太大时缩小图片 Add by :weilin
                                    Bitmap bm = new Bitmap(filePath);
                                    if (bm.Width > 350 || bm.Height > 300)
                                    {
                                        Bitmap bm1 = new Bitmap(bm, 350, 300);
                                        bm.Dispose();
                                        File.Delete(filePath);
                                        bm1.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        bm1.Dispose();
                                    }
                                    else
                                    {
                                        bm.Dispose();
                                    }
                                    #endregion
                                    MsgObj.MsgTextClear();

                                    MsgObj.MsgFileLoad(filePath);

                                    MsgObj.SetMsgByName("POSITION", mLabelName);
                                    MsgObj.SetMsgByName("IMAGETYPE", ".jpg");

                                    MsgObj.SetMsgByName("TASKID", mTaskID);
                                }
                                else { MsgObj.MsgFileLoad("D:\\a.jpg"); }
                            }
                            else { MsgObj.MsgFileLoad("D:\\a.jpg"); }
                        }
                        else
                        {
                            //注意，测试时，务必将印章文件及其目录签出，并保证非只读，否则无法插入到报告
                            TRptSignatureVo rsv = new TRptSignatureVo();
                            rsv.USER_NAME = base.LogInfo.UserInfo.USER_NAME;
                            DataTable dt = new TRptSignatureLogic().SelectByTable(rsv, 0, 0);
                            if (dt.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[0]["MARK_PATH"].ToString()))
                                {
                                    string fileName = dt.Rows[0]["MARK_PATH"].ToString();
                                    if (fileName != "" && System.IO.File.Exists(fileName))
                                    {
                                        MsgObj.MsgTextClear();

                                        MsgObj.MsgFileLoad(fileName);

                                        MsgObj.SetMsgByName("POSITION", mLabelName);
                                        MsgObj.SetMsgByName("IMAGETYPE", ".jpg");
                                    }
                                }
                                else
                                {
                                    DataRow myRow;
                                    byte[] MyData = new byte[0];
                                    myRow = dt.Rows[0];
                                    MyData = (byte[])myRow["MARK_BODY"];
                                    MsgObj.MsgFileBody(MyData);

                                    MsgObj.SetMsgByName("IMAGETYPE", ".jpg");		         //指定图片的类型 
                                    MsgObj.SetMsgByName("POSITION", mLabelName);		     //设置插入的位置[书签对象名]
                                }
                            }
                            else
                            {
                                LigerDialogAlert("不存在印章文件！", "warn");
                            }
                        }
                        break;
                    #endregion

                    #region 扩展方法
                    //--------------请求取得服务器时间-------------//
                    case "DATETIME":
                        MsgObj.MsgTextClear();				                        //清除文本信息
                        MsgObj.SetMsgByName("DATETIME", "2006-01-01 10:24:24!");    //标准日期格式字串，如 2005-8-16 10:20:35
                        MsgObj.MsgError("");		                                //清除错误信息
                        break;

                    case "SENDMESSAGE":
                        mCommand = MsgObj.GetMsgByName("COMMAND");              //命令名称
                        mTemplate = MsgObj.GetMsgByName("TEMPLATE");		                  //取得模板编号
                        mTaskID = MsgObj.GetMsgByName("TASKID");             //合同编号
                        mContractType = MsgObj.GetMsgByName("CONTRACT_TYPE");        //合同类型
                        mConditionType = MsgObj.GetMsgByName("CONDITION_TYPE");       //监测条件类型
                        mTEST_ITEM_type = MsgObj.GetMsgByName("TEST_ITEM_type");      //模版对应的监测类别，综合类为0
                        mItemTypeID = MsgObj.GetMsgByName("ItemTypeID");//监测类别   综合报告选择类别，其他模版不用选
                        //如果是单类别模版，非综合类模版，取该类别模版对应的监测类别
                        strTmpItemTypeID = new TRptTemplateLogic().Details(mTemplate).FILE_DESC;
                        if (strTmpItemTypeID.Length > 0 && strTmpItemTypeID != "0")
                        {
                            mItemTypeID = strTmpItemTypeID;
                        }

                        //根据命令类型加载相关数据
                        switch (mCommand)
                        {
                            #region 监测参数 2
                            case "2":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                                {
                                    DataTable dtAttribute = new ReportBuildLogic().SelAttribute(mTaskID, mItemTypeID, "(attrbute_info.ATTRIBUTE_NAME like '%监测位置%' or attrbute_info.ATTRIBUTE_NAME like '%设施%' or attrbute_info.ATTRIBUTE_NAME like '%高度%' or attrbute_info.ATTRIBUTE_NAME like '%燃料种类%')");
                                    //point.id,POINT_NAME,SAMPLE_CODE,SAMPLE_NAME,SORT_NAME as SN,ATTRIBUTE_NAME,ATTRBUTE_VALUE 
                                    DataTable dt_Attribute_Return = new DataTable();
                                    DataRow[] drAttribute;
                                    string strPointName = "采样位置";
                                    bool iTitle = true;       //标题行是否显示
                                    if (mContractType == "ATTRIBUTE_TABLE")
                                    {
                                        drAttribute = dtAttribute.Select("1=1");
                                    }
                                    else if (mContractType == "ATTRIBUTE_TABLE_YY")
                                    {
                                        iTitle = false;
                                        dtAttribute.Clear();
                                        DataRow dr;
                                        TMisMonitorDustinforVo DustinfoVo = new TMisMonitorDustinforVo();
                                        DataTable dtYY = new ReportBuildLogic().SelAttribute_YY(mTaskID);
                                        for (int i = 0; i < dtYY.Rows.Count; i++)
                                        {
                                            DustinfoVo = new TMisMonitorDustinforVo();
                                            DustinfoVo.SUBTASK_ID = dtYY.Rows[i]["RESULT_ID"].ToString();
                                            DustinfoVo = new TMisMonitorDustinforLogic().SelectByObject(DustinfoVo);
                                            dr = dtAttribute.NewRow();
                                            dr["id"] = dtYY.Rows[i]["ID"].ToString();
                                            dr["MONITOR_ID"] = dtYY.Rows[i]["MONITOR_ID"].ToString();
                                            dr["POINT_NAME"] = dtYY.Rows[i]["POINT_NAME"].ToString();
                                            dr["SAMPLE_CODE"] = dtYY.Rows[i]["SAMPLE_CODE"].ToString();
                                            dr["SAMPLE_NAME"] = dtYY.Rows[i]["SAMPLE_NAME"].ToString();
                                            dr["SN"] = "油烟类";
                                            dr["ATTRIBUTE_NAME"] = "锅炉名称";
                                            dr["ATTRBUTE_VALUE"] = DustinfoVo.BOILER_NAME;
                                            dtAttribute.Rows.Add(dr);

                                            dr = dtAttribute.NewRow();
                                            dr["id"] = dtYY.Rows[i]["ID"].ToString();
                                            dr["MONITOR_ID"] = dtYY.Rows[i]["MONITOR_ID"].ToString();
                                            dr["POINT_NAME"] = dtYY.Rows[i]["POINT_NAME"].ToString();
                                            dr["SAMPLE_CODE"] = dtYY.Rows[i]["SAMPLE_CODE"].ToString();
                                            dr["SAMPLE_NAME"] = dtYY.Rows[i]["SAMPLE_NAME"].ToString();
                                            dr["SN"] = "油烟类";
                                            dr["ATTRIBUTE_NAME"] = "治理设施";
                                            dr["ATTRBUTE_VALUE"] = DustinfoVo.GOVERM_METHOLD;
                                            dtAttribute.Rows.Add(dr);

                                            dr = dtAttribute.NewRow();
                                            dr["id"] = dtYY.Rows[i]["ID"].ToString();
                                            dr["MONITOR_ID"] = dtYY.Rows[i]["MONITOR_ID"].ToString();
                                            dr["POINT_NAME"] = dtYY.Rows[i]["POINT_NAME"].ToString();
                                            dr["SAMPLE_CODE"] = dtYY.Rows[i]["SAMPLE_CODE"].ToString();
                                            dr["SAMPLE_NAME"] = dtYY.Rows[i]["SAMPLE_NAME"].ToString();
                                            dr["SN"] = "油烟类";
                                            dr["ATTRIBUTE_NAME"] = "燃料种类";
                                            dr["ATTRBUTE_VALUE"] = DustinfoVo.FUEL_TYPE;
                                            dtAttribute.Rows.Add(dr);

                                            dr = dtAttribute.NewRow();
                                            dr["id"] = dtYY.Rows[i]["ID"].ToString();
                                            dr["MONITOR_ID"] = dtYY.Rows[i]["MONITOR_ID"].ToString();
                                            dr["POINT_NAME"] = dtYY.Rows[i]["POINT_NAME"].ToString();
                                            dr["SAMPLE_CODE"] = dtYY.Rows[i]["SAMPLE_CODE"].ToString();
                                            dr["SAMPLE_NAME"] = dtYY.Rows[i]["SAMPLE_NAME"].ToString();
                                            dr["SN"] = "油烟类";
                                            dr["ATTRIBUTE_NAME"] = "排气筒高度(m)";//huangjinjun update
                                            dr["ATTRBUTE_VALUE"] = DustinfoVo.HEIGHT;
                                            dtAttribute.Rows.Add(dr);
                                        }
                                        drAttribute = dtAttribute.Select("SN like '%油烟%'");
                                    }
                                    else if (mContractType == "ATTRIBUTE_TABLE_YH")
                                    {
                                        drAttribute = dtAttribute.Select("SN like '%烟气黑度%'");
                                        iTitle = false;
                                        //strPointName = "监测位置";
                                    }
                                    else
                                    {
                                        drAttribute = dtAttribute.Select("1=1");
                                    }
                                    string strPointIDs = "";
                                    string strPointNames = "";
                                    string strAttriNames = "";
                                    for (int i = 0; i < drAttribute.Length; i++)
                                    {
                                        if (!strPointIDs.Contains(drAttribute[i]["id"].ToString()))
                                        {
                                            strPointIDs += (strPointIDs.Length > 0 ? "," : "") + drAttribute[i]["id"].ToString();
                                            strPointNames += (strPointNames.Length > 0 ? "@" : "") + drAttribute[i]["POINT_NAME"].ToString();
                                        }
                                        if (!strAttriNames.Contains(drAttribute[i]["ATTRIBUTE_NAME"].ToString()))
                                        {
                                            strAttriNames += (strAttriNames.Length > 0 ? "@" : "") + drAttribute[i]["ATTRIBUTE_NAME"].ToString();
                                        }
                                    }

                                    string[] arrPointIDs = strPointIDs.TrimStart(',').Split(',');
                                    string[] arrPointNames = strPointNames.Split('@');
                                    string[] arrAttriNames = strAttriNames.Split('@');
                                    if (arrPointIDs.Length > 0)
                                    {
                                        if (arrPointIDs.Length == 0)
                                        {
                                            #region 只有1个点位，2列展示
                                            dt_Attribute_Return.Columns.Add("A1");
                                            dt_Attribute_Return.Columns.Add("A2");
                                            dt_Attribute_Return.Columns.Add("A3");
                                            dt_Attribute_Return.Columns.Add("A4");

                                            for (int j = 0; j < drAttribute.Length; j++)
                                            {
                                                if (j % 2 == 0)
                                                {
                                                    DataRow attNewRow = dt_Attribute_Return.NewRow();

                                                    attNewRow["A1"] = drAttribute[j]["ATTRIBUTE_NAME"].ToString();
                                                    attNewRow["A2"] = drAttribute[j]["ATTRBUTE_VALUE"].ToString();
                                                    if (j < drAttribute.Length - 1)//如果是奇数行，以下无值
                                                    {
                                                        attNewRow["A3"] = drAttribute[j + 1]["ATTRIBUTE_NAME"].ToString();
                                                        attNewRow["A4"] = drAttribute[j + 1]["ATTRBUTE_VALUE"].ToString();
                                                    }

                                                    dt_Attribute_Return.Rows.Add(attNewRow);
                                                }
                                            }
                                            dt_Attribute_Return.AcceptChanges();

                                            if (dt_Attribute_Return.Rows.Count > 0)
                                            {
                                                MsgObj.SetMsgByName(mContractType + "TableCount", "1");
                                                MsgObj.SetMsgByName(mContractType + "ColumnsCount", dt_Attribute_Return.Columns.Count.ToString());
                                                MsgObj.SetMsgByName(mContractType + "CellsCount", dt_Attribute_Return.Rows.Count.ToString());

                                                for (int i = 0; i < dt_Attribute_Return.Columns.Count; i++)
                                                {
                                                    for (int j = 0; j < dt_Attribute_Return.Rows.Count; j++)
                                                    {
                                                        MsgObj.SetMsgByName(mContractType + "0-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Attribute_Return.Rows[j][i].ToString());
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 多个点位
                                            dt_Attribute_Return.Columns.Add(strPointName);
                                            for (int j = 0; j < arrPointNames.Length; j++)
                                            {
                                                dt_Attribute_Return.Columns.Add(arrPointNames[j]);
                                            }

                                            for (int k = 0; k < arrAttriNames.Length; k++)
                                            {
                                                DataRow attNewRow = dt_Attribute_Return.NewRow();
                                                attNewRow[strPointName] = arrAttriNames[k];
                                                dt_Attribute_Return.Rows.Add(attNewRow);
                                            }

                                            for (int j = 0; j < arrPointIDs.Length; j++)
                                            {
                                                var drs = drAttribute.Where(c => c["id"].Equals(arrPointIDs[j])).ToList();
                                                //DataRow[] drs = drAttribute.Select("id=" + arrPointIDs[j]);
                                                for (int k = 0; k < arrAttriNames.Length; k++)
                                                {
                                                    foreach (DataRow drAtt in drs)
                                                    {
                                                        if (drAtt["ATTRIBUTE_NAME"].ToString() == arrAttriNames[k])
                                                            dt_Attribute_Return.Rows[k][j + 1] = drAtt["ATTRBUTE_VALUE"].ToString();
                                                    }
                                                }
                                            }

                                            if (dt_Attribute_Return.Rows.Count > 0)
                                            {
                                                //结果6列，其他4列
                                                int intResult_ColumnCount = 4;
                                                int intPre_ColumnCount = 1;
                                                int intMod = 0;//最后一个分表的列数
                                                int intTableCount = 0;//几个分表

                                                //构造webOffice所需DataTable、参数
                                                getWebOfficeDatatable(ref dt_Attribute_Return, ref  intTableCount, ref  intMod, ref intResult_ColumnCount, intPre_ColumnCount, iTitle);

                                                // 构造webOffice表格
                                                makeWebOffice_msg(intTableCount, intPre_ColumnCount, intResult_ColumnCount, dt_Attribute_Return);
                                            }
                                            #endregion
                                        }
                                    }
                                    //if (mContractType == "ATTRIBUTE_TABLE")
                                    //{
                                    //    string strPointIDs = "";
                                    //    string strPointNames = "";
                                    //    string strAttriNames = "";
                                    //    for (int i = 0; i < dtAttribute.Rows.Count; i++)
                                    //    {
                                    //        if (!strPointIDs.Contains(dtAttribute.Rows[i]["id"].ToString()))
                                    //        {
                                    //            strPointIDs += (strPointIDs.Length > 0 ? "," : "") + dtAttribute.Rows[i]["id"].ToString();
                                    //            strPointNames += (strPointNames.Length > 0 ? "@" : "") + dtAttribute.Rows[i]["POINT_NAME"].ToString();
                                    //        }
                                    //        if (!strAttriNames.Contains(dtAttribute.Rows[i]["ATTRIBUTE_NAME"].ToString()))
                                    //        {
                                    //            strAttriNames += (strAttriNames.Length > 0 ? "@" : "") + dtAttribute.Rows[i]["ATTRIBUTE_NAME"].ToString();
                                    //        }
                                    //    }

                                    //    string[] arrPointIDs = strPointIDs.Split(',');
                                    //    string[] arrPointNames = strPointNames.Split('@');
                                    //    string[] arrAttriNames = strAttriNames.Split('@');
                                    //    if (arrPointIDs.Length > 0)
                                    //    {
                                    //        if (arrPointIDs.Length == 1)
                                    //        {
                                    //            #region 只有1个点位，2列展示
                                    //            dt_Attribute_Return.Columns.Add("A1");
                                    //            dt_Attribute_Return.Columns.Add("A2");
                                    //            dt_Attribute_Return.Columns.Add("A3");
                                    //            dt_Attribute_Return.Columns.Add("A4");

                                    //            for (int j = 0; j < dtAttribute.Rows.Count; j++)
                                    //            {
                                    //                if (j % 2 == 0)
                                    //                {
                                    //                    DataRow attNewRow = dt_Attribute_Return.NewRow();

                                    //                    attNewRow["A1"] = dtAttribute.Rows[j]["ATTRIBUTE_NAME"].ToString();
                                    //                    attNewRow["A2"] = dtAttribute.Rows[j]["ATTRBUTE_VALUE"].ToString();
                                    //                    if (j < dtAttribute.Rows.Count - 1)//如果是奇数行，以下无值
                                    //                    {
                                    //                        attNewRow["A3"] = dtAttribute.Rows[j + 1]["ATTRIBUTE_NAME"].ToString();
                                    //                        attNewRow["A4"] = dtAttribute.Rows[j + 1]["ATTRBUTE_VALUE"].ToString();
                                    //                    }

                                    //                    dt_Attribute_Return.Rows.Add(attNewRow);
                                    //                }
                                    //            }
                                    //            dt_Attribute_Return.AcceptChanges();

                                    //            if (dt_Attribute_Return.Rows.Count > 0)
                                    //            {
                                    //                MsgObj.SetMsgByName(mContractType + "TableCount", "1");
                                    //                MsgObj.SetMsgByName(mContractType + "ColumnsCount", dt_Attribute_Return.Columns.Count.ToString());
                                    //                MsgObj.SetMsgByName(mContractType + "CellsCount", dt_Attribute_Return.Rows.Count.ToString());

                                    //                for (int i = 0; i < dt_Attribute_Return.Columns.Count; i++)
                                    //                {
                                    //                    for (int j = 0; j < dt_Attribute_Return.Rows.Count; j++)
                                    //                    {
                                    //                        MsgObj.SetMsgByName(mContractType + "0-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Attribute_Return.Rows[j][i].ToString());
                                    //                    }
                                    //                }
                                    //            }
                                    //            #endregion
                                    //        }
                                    //        else
                                    //        {
                                    //            #region 多个点位
                                    //            dt_Attribute_Return.Columns.Add("监测点位");
                                    //            for (int j = 0; j < arrPointNames.Length; j++)
                                    //            {
                                    //                dt_Attribute_Return.Columns.Add(arrPointNames[j]);
                                    //            }

                                    //            for (int k = 0; k < arrAttriNames.Length; k++)
                                    //            {
                                    //                DataRow attNewRow = dt_Attribute_Return.NewRow();
                                    //                attNewRow["监测点位"] = arrAttriNames[k];
                                    //                dt_Attribute_Return.Rows.Add(attNewRow);
                                    //            }

                                    //            for (int j = 0; j < arrPointIDs.Length; j++)
                                    //            {
                                    //                DataRow[] drs = dtAttribute.Select("id=" + arrPointIDs[j]);
                                    //                for (int k = 0; k < arrAttriNames.Length; k++)
                                    //                {
                                    //                    foreach (DataRow drAtt in drs)
                                    //                    {
                                    //                        if (drAtt["ATTRIBUTE_NAME"].ToString() == arrAttriNames[k])
                                    //                            dt_Attribute_Return.Rows[k][j + 1] = drAtt["ATTRBUTE_VALUE"].ToString();
                                    //                    }
                                    //                }
                                    //            }

                                    //            if (dt_Attribute_Return.Rows.Count > 0)
                                    //            {
                                    //                //结果6列，其他4列
                                    //                int intResult_ColumnCount = 4;
                                    //                int intPre_ColumnCount = 1;
                                    //                int intMod = 0;//最后一个分表的列数
                                    //                int intTableCount = 0;//几个分表

                                    //                //构造webOffice所需DataTable、参数
                                    //                getWebOfficeDatatable(ref dt_Attribute_Return, ref  intTableCount, ref  intMod, ref intResult_ColumnCount, intPre_ColumnCount);

                                    //                // 构造webOffice表格
                                    //                makeWebOffice_msg(intTableCount, intPre_ColumnCount, intResult_ColumnCount, dt_Attribute_Return);
                                    //            }
                                    //            #endregion
                                    //        }
                                    //    }
                                    //}
                                }
                                break;
                            #endregion

                            #region 样品信息表格 4
                            case "4":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                                {
                                    DataTable dtSampleinfo = new ReportBuildLogic().SelSampleInfoWater_ST(mTaskID, mItemTypeID);

                                    DataTable dt_Sampleinfo_Return = new DataTable();
                                    if (mContractType == "SAMPLE_TABLE")
                                    {
                                        //设置Datatable的列
                                        string strItemName = "样品类型@样品编号@采样位置/时间@样品状态";
                                        AddDatatable_column("", strItemName, ref  dt_Sampleinfo_Return);

                                        DataRow[] drs = dtSampleinfo.Select("  '" + mItemTypeID + "' like '%'+MONITOR_ID+'%'", "MONITOR_ID ASC");

                                        for (int i = 0; i < drs.Length; i++)
                                        {
                                            string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(drs[i]["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                                            DataRow dr = dt_Sampleinfo_Return.NewRow();

                                            dr[0] = strMonitorType;
                                            dr[1] = drs[i]["sample_code"].ToString();
                                            dr[2] = drs[i]["point_name"].ToString();
                                            dr[3] = drs[i]["ATTRBUTE_VALUE"].ToString();

                                            dt_Sampleinfo_Return.Rows.Add(dr);
                                        }
                                    }
                                    else if (mContractType == "SAMPLE_TABLE_WATER")
                                    {
                                        dtSampleinfo = new ReportBuildLogic().SelSampleInfoWater(mTaskID, "000000001", "000000017");
                                        //设置Datatable的列
                                        string strItemName = "样品类型@样品编号@采样位置/时间@样品状态";
                                        AddDatatable_column("", strItemName, ref  dt_Sampleinfo_Return);

                                        DataRow[] drs = dtSampleinfo.Select("QC_TYPE='0'", "sample_code ASC");

                                        for (int i = 0; i < drs.Length; i++)
                                        {
                                            string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(drs[i]["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                                            DataRow dr = dt_Sampleinfo_Return.NewRow();

                                            dr[0] = strMonitorType;
                                            dr[1] = drs[i]["sample_code"].ToString();
                                            dr[2] = drs[i]["point_name"].ToString() + "(" + drs[i]["SAMPLE_ACCEPT_DATEORACC"].ToString() + ")";
                                            dr[3] = drs[i]["ATTRBUTE_VALUE"].ToString();

                                            dt_Sampleinfo_Return.Rows.Add(dr);
                                        }
                                    }

                                    if (mContractType == "SAMPLE_SEND_TABLE")
                                    {
                                        if (dtSampleinfo.Rows.Count == 0)
                                            dtSampleinfo = new ReportBuildLogic().SelSampleInfoWater_ST_forSendSanple(mTaskID, mItemTypeID);
                                        //设置Datatable的列
                                        string strItemName = "样品类型@委托样品原编号/名称@采样位置/时间@样品状态@统一编号";
                                        AddDatatable_column("", strItemName, ref  dt_Sampleinfo_Return);

                                        DataRow[] drs = dtSampleinfo.Select("  '" + mItemTypeID + "' like '%'+MONITOR_ID+'%'", "MONITOR_ID ASC");

                                        for (int i = 0; i < drs.Length; i++)
                                        {
                                            string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(drs[i]["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                                            DataRow dr = dt_Sampleinfo_Return.NewRow();

                                            dr[0] = strMonitorType;
                                            dr[1] = drs[i]["SRC_CODEORNAME"].ToString();
                                            dr[2] = drs[i]["SAMPLE_ACCEPT_DATEORACC"].ToString();
                                            dr[3] = drs[i]["SAMPLE_STATUS"].ToString();
                                            dr[4] = drs[i]["SAMPLE_CODE"].ToString();

                                            dt_Sampleinfo_Return.Rows.Add(dr);
                                        }
                                    }
                                    if (mContractType == "SAMPLE_SEND_TABLE_WATER")
                                    {
                                        if (dtSampleinfo.Rows.Count == 0)
                                            dtSampleinfo = new ReportBuildLogic().SelSampleInfoWater_ST_forSendSanple(mTaskID, mItemTypeID);
                                        //设置Datatable的列
                                        string strItemName = "来样编号@样品类型@样品状态@统一样品编号";
                                        AddDatatable_column("", strItemName, ref  dt_Sampleinfo_Return);

                                        DataRow[] drs = dtSampleinfo.Select("  '000000001' like '%'+MONITOR_ID+'%'", "MONITOR_ID ASC");

                                        for (int i = 0; i < drs.Length; i++)
                                        {
                                            string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(drs[i]["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                                            DataRow dr = dt_Sampleinfo_Return.NewRow();

                                            dr[0] = drs[i]["SRC_CODEORNAME"].ToString();
                                            dr[1] = strMonitorType;
                                            //dr[2] = drs[i]["SAMPLE_ACCEPT_DATEORACC"].ToString();
                                            dr[2] = drs[i]["SAMPLE_STATUS"].ToString();
                                            dr[3] = drs[i]["SAMPLE_CODE"].ToString();

                                            dt_Sampleinfo_Return.Rows.Add(dr);
                                        }
                                    }
                                    if (mContractType == "SAMPLE_SEND_TABLE_GAS")
                                    {
                                        if (dtSampleinfo.Rows.Count == 0)
                                            dtSampleinfo = new ReportBuildLogic().SelSampleInfoWater_ST_forSendSanple(mTaskID, mItemTypeID);
                                        //设置Datatable的列
                                        string strItemName = "来样编号@样品类型@样品状态@统一样品编号";
                                        AddDatatable_column("", strItemName, ref  dt_Sampleinfo_Return);

                                        DataRow[] drs = dtSampleinfo.Select("  '000000002' like '%'+MONITOR_ID+'%'", "MONITOR_ID ASC");

                                        for (int i = 0; i < drs.Length; i++)
                                        {
                                            string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(drs[i]["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                                            DataRow dr = dt_Sampleinfo_Return.NewRow();

                                            dr[0] = drs[i]["SRC_CODEORNAME"].ToString();
                                            dr[1] = strMonitorType;
                                            //dr[2] = drs[i]["SAMPLE_ACCEPT_DATEORACC"].ToString();
                                            dr[2] = drs[i]["SAMPLE_STATUS"].ToString();
                                            dr[3] = drs[i]["SAMPLE_CODE"].ToString();

                                            dt_Sampleinfo_Return.Rows.Add(dr);
                                        }
                                    }

                                    if (dt_Sampleinfo_Return.Rows.Count > 0)
                                    {
                                        getDatatable_forWebOffice(0, 0, ref dt_Sampleinfo_Return, true);

                                        MsgObj.SetMsgByName(mContractType + "TableCount", "1");
                                        MsgObj.SetMsgByName(mContractType + "ColumnsCount", dt_Sampleinfo_Return.Columns.Count.ToString());
                                        MsgObj.SetMsgByName(mContractType + "CellsCount", dt_Sampleinfo_Return.Rows.Count.ToString());

                                        for (int i = 0; i < dt_Sampleinfo_Return.Columns.Count; i++)
                                        {
                                            for (int j = 0; j < dt_Sampleinfo_Return.Rows.Count; j++)
                                            {
                                                MsgObj.SetMsgByName(mContractType + "0-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Sampleinfo_Return.Rows[j][i].ToString());
                                            }
                                        }
                                    }
                                }
                                break;
                            #endregion

                            #region 监测项目 1
                            case "1":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                                {
                                    DataTable objTableTemp = new DataTable();
                                    objTableTemp = new ReportBuildLogic().getItemInfoForReport(mTaskID, mItemTypeID);

                                    //去掉重复项目信息，取第一条
                                    DataTable dtNew = objTableTemp.Clone();
                                    if (null != objTableTemp && objTableTemp.Rows.Count > 0)
                                    {
                                        //去掉重复项目信息，取第一条
                                        foreach (DataRow dr in objTableTemp.Rows)
                                        {
                                            int intCount = 0;
                                            foreach (DataRow drNew in dtNew.Rows)
                                            {
                                                if (drNew["监测项目"].ToString() == dr["监测项目"].ToString())
                                                {
                                                    intCount++;
                                                }
                                            }
                                            if (intCount == 0)
                                            {
                                                dtNew.Rows.Add(dr.ItemArray);
                                            }
                                        }
                                        //手工添加标题行
                                        DataRow TitleRow = dtNew.NewRow();
                                        for (int p = 0; p < dtNew.Columns.Count; p++)
                                        {
                                            TitleRow[p] = dtNew.Columns[p].ColumnName;
                                        }

                                        dtNew.Rows.InsertAt(TitleRow, 0);

                                        #region 清远监测项目表格 特殊处理
                                        if (mContractType == "TEST_ITEM_TABLE_QY")
                                        {
                                            dtNew.Columns.RemoveAt(0);
                                            for (int j = 0; j < dtNew.Rows.Count; j++)
                                            {
                                                string str2 = dtNew.Rows[j][2].ToString();
                                                string str3 = dtNew.Rows[j][3].ToString();
                                                dtNew.Rows[j][2] = str3;
                                                dtNew.Rows[j][3] = str2;
                                            }
                                            dtNew.Rows[0][1] = "方法依据";
                                            dtNew.Rows[0][2] = "使用仪器";
                                        }
                                        #endregion

                                        dtNew.AcceptChanges();

                                        MsgObj.SetMsgByName(mContractType + "TableCount", "1");
                                        MsgObj.SetMsgByName(mContractType + "ColumnsCount", dtNew.Columns.Count.ToString());
                                        MsgObj.SetMsgByName(mContractType + "CellsCount", dtNew.Rows.Count.ToString());

                                        for (int i = 0; i < dtNew.Columns.Count; i++)
                                        {
                                            for (int j = 0; j < dtNew.Rows.Count; j++)
                                            {
                                                MsgObj.SetMsgByName(mContractType + "0-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dtNew.Rows[j][i].ToString().Replace("《水和废水监测分析方法》(第四版)", "*"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MsgObj.SetMsgByName(mContractType + "ColumnsCount", "");
                                        MsgObj.SetMsgByName(mContractType + "CellsCount", "");
                                    }
                                }
                                else
                                {
                                    MsgObj.MsgError("加载监测项目失败");
                                }
                                break;
                            #endregion

                            #region 监测结果 3
                            //监测结果
                            case "3":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID) && !String.IsNullOrEmpty(mContractType))
                                {
                                    //获得监测任务中所有原始样样品信息
                                    DataTable dtResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, mItemTypeID, "1=1");

                                    if (mContractType == "RESULT_WATER")
                                    {
                                        #region 【监测结果-废水】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000001", "监测日期,监测点位,样品编号,样品描述", "SAMPLE_FINISH_DATE,SAMPLE_NAME,SAMPLE_CODE,SAMPLE_REMARK", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 6, 4, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_GAS")
                                    {
                                        #region 【监测结果-废气】
                                        //获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000002", "监测日期,监测点位,样品编号", "SAMPLE_FINISH_DATE,SAMPLE_NAME,SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 7, 3, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_SOLID")
                                    {
                                        #region 【监测结果-固体】
                                        //获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000003", "监测日期,样品名称,样品编号", "SAMPLE_FINISH_DATE,SAMPLE_NAME,SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 7, 3, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_NOISE")
                                    {
                                        #region 【监测结果-噪声】
                                        //获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000004", "监测日期,监测点位", "SAMPLE_FINISH_DATE,SAMPLE_NAME", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 8, 2, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_QUAKE")
                                    {
                                        #region 【监测结果-振动】
                                        //获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000006", "监测日期,监测点位", "SAMPLE_FINISH_DATE,SAMPLE_NAME", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 8, 2, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_EMISSIVE")
                                    {
                                        #region 【监测结果-放射性】
                                        //获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000005", "监测日期,监测点位", "SAMPLE_FINISH_DATE,SAMPLE_NAME", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 8, 2, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_WATER_QY1")
                                    {
                                        #region 【监测结果-废水-清远-竖】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        //DataTable dt_Result_Return = getDatatable("000000001", "样品编号", "SAMPLE_CODE", dtResult);
                                        DataTable dt_Result_Return = getDatatableEx("000000001", "样品编号,监测项目,监测结果", "SAMPLE_CODE,ITEM_NAME,ITEM_RESULT", dtResult, "ITEM_UNIT", "mg/L");
                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 0, 3, "（mg/L）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_WATER_QY2")
                                    {
                                        #region 【监测结果-废水送样-清远-竖】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        //DataTable dt_Result_Return = getDatatable("000000001", "统一样品编号", "SAMPLE_CODE", dtResult);
                                        DataTable dt_Result_Return = getDatatableEx("000000001", "样品编号,监测项目,监测结果", "SAMPLE_CODE,ITEM_NAME,ITEM_RESULT", dtResult, "ITEM_UNIT", "mg/L");

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 0, 3, "（mg/L）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY5")
                                    {
                                        #region 【监测结果-废气送样-清远-竖】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000002", "统一样品编号", "SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 5, 1, "", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_NOISE_QY")
                                    {
                                        #region 【监测结果-噪声-清远-竖】
                                        //获取主要声源
                                        string strNoiseSrc_tmp = "";
                                        DataTable dtWeatherNoise_tmp = new ReportBuildLogic().getWeatherInfo(mTaskID, mItemTypeID, "noise_weather");
                                        if (dtWeatherNoise_tmp.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtWeatherNoise_tmp.Rows)
                                            {
                                                if (dr["name"].ToString() == "主要声源")
                                                    strNoiseSrc_tmp = dr["value"].ToString();
                                            }
                                        }
                                        dtResult = new ReportBuildLogic().getSampleResult_ForV2Ex(mTaskID, mItemTypeID);
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_ReturnEx = getDatatable("000000004", "测点编号,测点名称,昼间主要声源,夜间主要声源", "SAMPLE_CODE,SAMPLE_NAME,D_SOURCE,N_SOURCE", dtResult);

                                        //添加序号列“测点编号”及"主要声源"列
                                        DataTable dt_Result_Return = new DataTable();
                                        dt_Result_Return.Columns.Add("测点编号");
                                        dt_Result_Return.Columns.Add("测点名称");
                                        //dt_Result_Return.Columns.Add("主要声源");
                                        for (int j = 2; j < dt_Result_ReturnEx.Columns.Count; j++)
                                        {
                                            dt_Result_Return.Columns.Add(dt_Result_ReturnEx.Columns[j].ColumnName);
                                        }

                                        for (int i = 0; i < dt_Result_ReturnEx.Rows.Count; i++)
                                        {
                                            DataRow drtmp = dt_Result_Return.NewRow();
                                            drtmp["测点编号"] = dt_Result_ReturnEx.Rows[i][0].ToString();// +"#";黄进军注释
                                            drtmp["测点名称"] = dt_Result_ReturnEx.Rows[i][1].ToString();
                                            //drtmp["主要声源"] = strNoiseSrc_tmp;

                                            for (int j = 1; j < dt_Result_ReturnEx.Columns.Count - 1; j++)
                                            {
                                                drtmp[1 + j] = dt_Result_ReturnEx.Rows[i][j + 1].ToString();
                                            }

                                            dt_Result_Return.Rows.Add(drtmp);
                                        }
                                        dt_Result_Return.AcceptChanges();

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 6, 2, "（db(A)）@（dB(A)）@（dB）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_SOLID_QY1" || mContractType == "RESULT_SOLID_QY2" || mContractType == "RESULT_SOLID_QY3")
                                    {
                                        #region 【监测结果-土壤-清远-竖】\【监测结果-固废-清远-竖】\【监测结果-底泥-清远-竖】
                                        string strMonitorType = "";
                                        if (mContractType == "RESULT_SOLID_QY1")
                                            strMonitorType = "000000003";
                                        if (mContractType == "RESULT_SOLID_QY2")
                                            strMonitorType = "000000023";
                                        if (mContractType == "RESULT_SOLID_QY3")
                                            strMonitorType = "000000024";

                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable(strMonitorType, "采样位置,样品编号", "SAMPLE_NAME,SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 5, 2, "（mg/kg）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_SOLID_QY4")
                                    {
                                        #region 【监测结果-煤质-清远-竖】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000025", "采样位置,样品编号", "SAMPLE_NAME,SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 1, 2, "（mg/kg）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "ATTRIBUTE_TABLE_AIR")
                                    {
                                        #region 【测点信息-废气-清远】
                                        string strPointIDs = "";
                                        string strPointNames = "";  //采样位置
                                        string strHandles = "";     //治理设施
                                        string strHights = "";      //排气筒高度（m）
                                        dtResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, "000000002", "i.ITEM_NAME not in('氮氧化物','二氧化硫','烟尘','粉尘','饮食业油烟','烟气黑度') and isnull(r.REMARK_5,'')<>'Air'");
                                        for (int i = 0; i < dtResult.Rows.Count; i++)
                                        {
                                            if (!strPointIDs.Contains(dtResult.Rows[i]["POINT_ID"].ToString()))
                                            {
                                                strPointIDs += dtResult.Rows[i]["POINT_ID"].ToString() + ",";
                                                TMisMonitorDustinforVo objDustinforVo = new TMisMonitorDustinforVo();
                                                objDustinforVo.SUBTASK_ID = dtResult.Rows[i]["RESULT_ID"].ToString();
                                                objDustinforVo = new TMisMonitorDustinforLogic().Details(objDustinforVo);
                                                strPointNames += objDustinforVo.POSITION + "@";
                                                strHandles += objDustinforVo.GOVERM_METHOLD + "@";
                                                strHights += objDustinforVo.HEIGHT + "@";
                                            }
                                        }

                                        DataTable dt_Result_Return = new DataTable();
                                        DataRow dr;
                                        dt_Result_Return.Columns.Add("采样位置");
                                        dr = dt_Result_Return.NewRow();
                                        dr["采样位置"] = "治理设施";
                                        dt_Result_Return.Rows.Add(dr);
                                        dr = dt_Result_Return.NewRow();
                                        dr["采样位置"] = "排气筒高度（m）";
                                        dt_Result_Return.Rows.Add(dr);
                                        if (strPointNames.Length > 0)
                                        {
                                            string[] objPointName = strPointNames.Substring(0, (strPointNames.Length == 0 ? 1 : strPointNames.Length) - 1).Split('@');
                                            string[] objHandle = strHandles.Substring(0, (strHandles.Length == 0 ? 1 : strHandles.Length) - 1).Split('@');
                                            string[] objHight = strHights.Substring(0, (strHights.Length == 0 ? 1 : strHights.Length) - 1).Split('@');
                                            for (int i = 0; i < objPointName.Length; i++)
                                            {
                                                dt_Result_Return.Columns.Add(objPointName[i].ToString());
                                                dt_Result_Return.Rows[0][objPointName[i].ToString()] = objHandle[i].ToString();
                                                dt_Result_Return.Rows[1][objPointName[i].ToString()] = objHight[i].ToString();
                                            }
                                        }
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        //DataTable dt_Result_Return = getDatatable("000000002", "采样位置,样品编号,废气排放量(Nm³/h)", "SAMPLE_NAME,SAMPLE_CODE,EMISSIONS", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 3, 1, "（mg/m3）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY1")
                                    {
                                        #region 【监测结果-废气-清远-竖】
                                        //dtResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, mItemTypeID, "i.ITEM_NAME not in('氮氧化物','二氧化硫','烟尘','饮食业油烟','烟气黑度')");
                                        dtResult = new ReportBuildLogic().getSampleResult_Dustinfor(mTaskID, mItemTypeID, "i.ITEM_NAME not in('氮氧化物','二氧化硫','烟尘','粉尘','饮食业油烟','烟气黑度') and isnull(r.REMARK_5,'')<>'Air'");
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        //DataTable dt_Result_Return = getDatatable("000000002", "采样位置,样品编号,废气排放量(Nm³/h)", "SAMPLE_NAME,SAMPLE_CODE,EMISSIONS", dtResult);
                                        DataTable dt_Result_Return = getDatatable_Dustinfor("000000002", "采样位置,样品编号,废气排放量（Nm³/h）", "SAMPLE_NAME,SAMPLE_CODE,FQPFL", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 1, 3, "", false);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY4")
                                    {
                                        #region 【监测结果(无)-废气-清远】
                                        dtResult = new ReportBuildLogic().getSampleResult_Dustinfor(mTaskID, mItemTypeID, "i.ITEM_NAME not in('氮氧化物','二氧化硫','烟尘','粉尘','饮食业油烟','烟气黑度') and isnull(r.REMARK_5,'')='Air'");
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable_Dustinfor("000000002", "采样位置,样品编号", "SAMPLE_NAME,SAMPLE_CODE", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 1, 2, "", false);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY_YY")
                                    {
                                        #region 【监测结果-油烟-清远-竖】
                                        dtResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, mItemTypeID, "i.ITEM_NAME in('饮食业油烟')");
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000002", "采样位置,样品编号", "SAMPLE_NAME,SAMPLE_CODE", dtResult);
                                        dt_Result_Return = new DataTable();
                                        dt_Result_Return.Columns.Add("采样位置");
                                        dt_Result_Return.Columns.Add("样品编号");
                                        dt_Result_Return.Columns.Add("油烟排放浓度(mg/m³)");
                                        DataTable dtTemp = new DataTable();
                                        DataRow drTemp;
                                        for (int i = 0; i < dtResult.Rows.Count; i++)
                                        {
                                            TMisMonitorDustinforVo DustinforVo = new TMisMonitorDustinforVo();
                                            DustinforVo.SUBTASK_ID = dtResult.Rows[i]["RESULT_ID"].ToString();
                                            DustinforVo = new TMisMonitorDustinforLogic().SelectByObject(DustinforVo);
                                            if (DustinforVo.ID.Length > 0)
                                            {
                                                TMisMonitorDustattributeVo DustattributeVo = new TMisMonitorDustattributeVo();
                                                DustattributeVo.BASEINFOR_ID = DustinforVo.ID;
                                                dtTemp = new TMisMonitorDustattributeLogic().SelectByTable(DustattributeVo);
                                                for (int j = 0; j < dtTemp.Rows.Count; j++)
                                                {
                                                    drTemp = dt_Result_Return.NewRow();
                                                    if (j == 0)
                                                        drTemp["采样位置"] = DustinforVo.POSITION; //DustinforVo.BOILER_NAME +
                                                    else
                                                        drTemp["采样位置"] = " ";
                                                    drTemp["样品编号"] = dtTemp.Rows[j]["FITER_CODE"].ToString();// +"#"; huangjinjun update
                                                    drTemp["油烟排放浓度(mg/m³)"] = dtTemp.Rows[j]["SMOKE_POTENCY"].ToString();
                                                    dt_Result_Return.Rows.Add(drTemp);
                                                }
                                            }
                                        }

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 1, 2, "（mg/m3）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY_YH")
                                    {
                                        #region 【监测结果-烟气黑度-清远-竖】
                                        dtResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, mItemTypeID, "i.ITEM_NAME in('烟气黑度')");
                                        DataTable dtAttribute = new ReportBuildLogic().SelAttribute(mTaskID, mItemTypeID, "(attrbute_info.ATTRIBUTE_NAME like '%监测位置%' and attrbute_type.SORT_NAME like '%烟气黑度%')");
                                        for (int i = 0; i < dtResult.Rows.Count; i++)
                                        {
                                            for (int j = 0; j < dtAttribute.Rows.Count; j++)
                                            {
                                                if (dtResult.Rows[i]["POINT_ID"].ToString() == dtAttribute.Rows[j]["id"].ToString())
                                                    dtResult.Rows[i]["SAMPLE_NAME"] = dtAttribute.Rows[j]["ATTRBUTE_VALUE"].ToString();
                                            }
                                        }
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表

                                        //dtResult.Rows[0]["ITEM_NAME"] = "烟气黑度（林格曼黑度、级）";//何海亮修改

                                        DataTable dt_Result_Return = getDatatable("000000002", "监测位置", "SAMPLE_NAME", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 1, 1, "（mg/m3）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY2")
                                    {
                                        #region 【监测结果-烟气黑度-清远-竖】
                                        // 获取数据DataTable

                                        //dtResult.Rows[0]["ITEM_NAME"] = "烟气黑度（林格曼黑度、级）";//何海亮修改

                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000002", "采样位置", "SAMPLE_NAME", dtResult);

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 5, 1, "（mg/m3）", true);
                                        #endregion
                                    }
                                    else if (mContractType == "RESULT_AIR_QY3")
                                    {
                                        #region 【监测结果-SO2-清远-竖】
                                        // 获取数据DataTable
                                        //监测类别，固定列，固定列对应的字段名，结果数据表
                                        DataTable dt_Result_Return = getDatatable("000000002", "采样位置,采样序号", "SAMPLE_NAME,", dtResult);

                                        DataRow dr = dt_Result_Return.NewRow();
                                        dr["采样序号"] = "平均";
                                        dt_Result_Return.Rows.Add(dr);
                                        dt_Result_Return.AcceptChanges();

                                        //绘制结果表格
                                        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
                                        DrawTable(dt_Result_Return, 6, 2, "（mg/m3）", true);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    MsgObj.MsgError("加载监测结果失败");
                                }
                                break;
                            #endregion

                            #region 签名 无用，不需要看 5
                            //签名
                            case "5":
                                if (!String.IsNullOrEmpty(mCommand))
                                {
                                }
                                else
                                {
                                    MsgObj.MsgError("加载签名失败");
                                }
                                break;
                            #endregion

                            #region 【清远烟尘监测情况】 6
                            case "6":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                                {
                                    if (mContractType == "QY_SO2_TESTINFO" && mItemTypeID.Contains("000000002"))
                                    {
                                        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(new TMisMonitorSubtaskVo { TASK_ID = mTaskID, MONITOR_ID = "000000002" });
                                        //DataTable dtSampleinfo = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo { SUBTASK_ID = objSubTask.ID });
                                        DataTable dtSampleinfo = new TMisMonitorSampleInfoLogic().SelectByTableForSO2(objSubTask.ID);
                                        if (dtSampleinfo.Rows.Count > 0)
                                        {
                                            string strPointNameS = "";
                                            string strBOILER_NAMEs = "";
                                            string strFUEL_TYPEs = "";
                                            string strHEIGHTs = "";
                                            string strGOVERM_METHOLDs = "";
                                            string strMECHIE_WIND_MEASUREs = "";

                                            foreach (DataRow drSample in dtSampleinfo.Rows)
                                            {
                                                string strPointID = drSample["POINT_ID"].ToString();
                                                string strSampleID = drSample["ID"].ToString();
                                                bool bIsFirst = false;
                                                bIsFirst = strPointNameS.Length > 0;
                                                //strPointNameS += (bIsFirst ? "@" : "") + new TMisMonitorTaskPointLogic().Details(strPointID).POINT_NAME;
                                                DataTable dtResultForSo2 = new TMisMonitorSampleInfoLogic().SelectResultForSO2(strSampleID);
                                                string strDustSubID = "";
                                                for (int i = 0; i < dtResultForSo2.Rows.Count; i++)
                                                {
                                                    strDustSubID += dtResultForSo2.Rows[i]["ID"].ToString() + ",";
                                                }
                                                DataTable dtDustInfor = new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = strDustSubID.TrimEnd(',') });

                                                if (dtDustInfor.Rows.Count > 0)
                                                {
                                                    strPointNameS += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["POSITION"].ToString();//采样位置
                                                    strBOILER_NAMEs += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["BOILER_NAME"].ToString();//锅炉型号/名称 
                                                    strFUEL_TYPEs += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["FUEL_TYPE"].ToString();//燃料种类
                                                    strHEIGHTs += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["HEIGHT"].ToString();//排气筒高度（m）
                                                    strGOVERM_METHOLDs += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["GOVERM_METHOLD"].ToString();//处理设施
                                                    strMECHIE_WIND_MEASUREs += (bIsFirst ? "@" : "") + dtDustInfor.Rows[0]["MECHIE_WIND_MEASURE"].ToString();//风机风量
                                                }
                                                else
                                                {
                                                    strPointNameS += (bIsFirst ? "@" : "");//采样位置
                                                    strBOILER_NAMEs += (bIsFirst ? "@" : "");//锅炉型号/名称 
                                                    strFUEL_TYPEs += (bIsFirst ? "@" : "");//燃料种类
                                                    strHEIGHTs += (bIsFirst ? "@" : "");//排气筒高度（m）
                                                    strGOVERM_METHOLDs += (bIsFirst ? "@" : "");//处理设施
                                                    strMECHIE_WIND_MEASUREs += (bIsFirst ? "@" : "");//风机风量
                                                }
                                            }

                                            string[] arrPointNames = strPointNameS.Split('@');
                                            string[] arrBOILER_NAMEs = strBOILER_NAMEs.Split('@');
                                            string[] arrFUEL_TYPEs = strFUEL_TYPEs.Split('@');
                                            string[] arrHEIGHTs = strHEIGHTs.Split('@');
                                            string[] arrGOVERM_METHOLDs = strGOVERM_METHOLDs.Split('@');
                                            string[] arrMECHIE_WIND_MEASUREs = strMECHIE_WIND_MEASUREs.Split('@');

                                            DataTable dt_Attribute_Return = new DataTable();

                                            if (arrPointNames.Length > 0)
                                            {

                                                dt_Attribute_Return.Columns.Add("列名");
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    //dt_Attribute_Return.Columns.Add(arrBOILER_NAMEs[j]);
                                                    dt_Attribute_Return.Columns.Add(j.ToString());
                                                }

                                                DataRow attNewRow1 = dt_Attribute_Return.NewRow();
                                                attNewRow1["列名"] = "锅炉(炉窑)名称";
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    attNewRow1[j + 1] = arrBOILER_NAMEs[j];
                                                }
                                                dt_Attribute_Return.Rows.Add(attNewRow1);
                                                attNewRow1 = dt_Attribute_Return.NewRow();
                                                attNewRow1["列名"] = "采样位置";//黄进军修改
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    attNewRow1[j + 1] = arrPointNames[j];
                                                }
                                                dt_Attribute_Return.Rows.Add(attNewRow1);
                                                DataRow attNewRow2 = dt_Attribute_Return.NewRow();
                                                attNewRow2["列名"] = "燃料种类";
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    attNewRow2[j + 1] = arrFUEL_TYPEs[j];
                                                }
                                                dt_Attribute_Return.Rows.Add(attNewRow2);
                                                DataRow attNewRow3 = dt_Attribute_Return.NewRow();
                                                attNewRow3["列名"] = "排气筒高度（m）";
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    attNewRow3[j + 1] = arrHEIGHTs[j];
                                                }
                                                dt_Attribute_Return.Rows.Add(attNewRow3);
                                                DataRow attNewRow4 = dt_Attribute_Return.NewRow();
                                                attNewRow4["列名"] = "治理设施";
                                                for (int j = 0; j < arrPointNames.Length; j++)
                                                {
                                                    attNewRow4[j + 1] = arrGOVERM_METHOLDs[j];
                                                }
                                                dt_Attribute_Return.Rows.Add(attNewRow4);
                                                //DataRow attNewRow5 = dt_Attribute_Return.NewRow();
                                                //attNewRow5["采样位置"] = "风机风量(m3/h)";//黄进军修改
                                                // for (int j = 0; j < arrPointNames.Length; j++)
                                                // {
                                                //      attNewRow5[j + 1] = arrMECHIE_WIND_MEASUREs[j];
                                                //  }
                                                //dt_Attribute_Return.Rows.Add(attNewRow5);

                                                dt_Attribute_Return.AcceptChanges();

                                                if (dt_Attribute_Return.Rows.Count > 0)
                                                {
                                                    //结果6列，其他4列
                                                    int intResult_ColumnCount = 4;
                                                    int intPre_ColumnCount = 1;
                                                    int intMod = 0;//最后一个分表的列数
                                                    int intTableCount = 0;//几个分表

                                                    //构造webOffice所需DataTable、参数
                                                    getWebOfficeDatatable(ref dt_Attribute_Return, ref  intTableCount, ref  intMod, ref intResult_ColumnCount, intPre_ColumnCount, false);

                                                    // 构造webOffice表格
                                                    makeWebOffice_msg(intTableCount, intPre_ColumnCount, intResult_ColumnCount, dt_Attribute_Return);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            #endregion

                            #region 【清远烟尘监测结果】 7
                            case "7":
                                if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID) && !String.IsNullOrEmpty(mContractType))
                                {
                                    if (mContractType == "QY_SO2_RESULT" && mItemTypeID.Contains("000000002"))
                                    {
                                        //获得监测任务中所有原始样样品信息
                                        DataTable dtSampleResult = new ReportBuildLogic().getSampleResult_ForV2(mTaskID, mItemTypeID, "1=1");
                                        DataRow[] drSampleResult = dtSampleResult.Select("ITEM_NAME in('氮氧化物','二氧化硫','烟尘','粉尘')");
                                        if (drSampleResult.Length > 0)
                                        {
                                            string strPointS = "";
                                            foreach (DataRow dr in drSampleResult)
                                            {
                                                if (!strPointS.Contains(dr["POINT_ID"].ToString()))
                                                    strPointS += (strPointS.Length > 0 ? "," : "") + dr["POINT_ID"].ToString();
                                            }
                                            string[] arrPointS = strPointS.Split(',');

                                            MsgObj.SetMsgByName(mContractType + "TableCount", arrPointS.Length.ToString());
                                            MsgObj.SetMsgByName(mContractType + "ColumnsCount", "8");
                                            MsgObj.SetMsgByName(mContractType + "CellsCount", "10");

                                            for (int i = 0; i < arrPointS.Length; i++)
                                            {
                                                DataRow[] dr_ResultTmp = dtSampleResult.Select("POINT_ID='" + arrPointS[i] + "'");
                                                string strPointName = "";//new TMisMonitorTaskPointLogic().Details(arrPointS[i]).POINT_NAME;

                                                DataTable dtResultForSo2 = new TMisMonitorSampleInfoLogic().SelectResultForSO2(dr_ResultTmp[0]["SAMPLE_ID"].ToString());
                                                //DataTable dtDustInfor = new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = dr_ResultTmp[0]["SAMPLE_ID"].ToString() });

                                                string strSO2ItemIDs = "";
                                                string strNoxItemIDs = "";
                                                string strYcItemIDs = "";
                                                getQyItemID(dr_ResultTmp, ref strSO2ItemIDs, ref strNoxItemIDs, ref strYcItemIDs);

                                                DataRow[] drResultSO2 = strSO2ItemIDs.Length > 0 ? dtResultForSo2.Select("ITEM_ID in(" + strSO2ItemIDs + ")") : dtResultForSo2.Select("1=2"); //二氧化硫
                                                DataRow[] drResultNOX = strNoxItemIDs.Length > 0 ? dtResultForSo2.Select("ITEM_ID in(" + strNoxItemIDs + ")") : dtResultForSo2.Select("1=2"); //氮氧化物
                                                DataRow[] drResultYC = strYcItemIDs.Length > 0 ? dtResultForSo2.Select("ITEM_ID in(" + strYcItemIDs + ")") : dtResultForSo2.Select("1=2"); //烟尘

                                                DataTable dtSO2_D = drResultSO2.Length > 0 ? new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = drResultSO2[0]["ID"].ToString() }) : new DataTable();
                                                DataTable dtNOX_D = drResultNOX.Length > 0 ? new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = drResultNOX[0]["ID"].ToString() }) : new DataTable();
                                                DataTable dtYC_D = drResultYC.Length > 0 ? new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = drResultYC[0]["ID"].ToString() }) : new DataTable();

                                                string BoilerName = "";
                                                //SO2 原始记录
                                                DataTable dtSO2 = new DataTable();
                                                // NOX 原始记录
                                                DataTable dtNOX = new DataTable();
                                                //烟尘 原始记录
                                                DataTable dtYC = new DataTable();
                                                if (dtSO2_D.Rows.Count > 0)
                                                {
                                                    strPointName = dtSO2_D.Rows[0]["POSITION"].ToString();
                                                    BoilerName = dtSO2_D.Rows[0]["BOILER_NAME"].ToString();
                                                    dtSO2 = dtNOX = new TMisMonitorDustattributeSo2ornoxLogic().SelectByTable(new TMisMonitorDustattributeSo2ornoxVo { BASEINFOR_ID = dtSO2_D.Rows[0]["ID"].ToString() });
                                                }
                                                else if (dtNOX_D.Rows.Count > 0)
                                                {
                                                    strPointName = dtNOX_D.Rows[0]["POSITION"].ToString();
                                                    BoilerName = dtNOX_D.Rows[0]["BOILER_NAME"].ToString();
                                                    dtNOX = dtSO2 = new TMisMonitorDustattributeSo2ornoxLogic().SelectByTable(new TMisMonitorDustattributeSo2ornoxVo { BASEINFOR_ID = dtNOX_D.Rows[0]["ID"].ToString() });
                                                }
                                                if (dtYC_D.Rows.Count > 0)
                                                {
                                                    strPointName = strPointName == "" ? dtYC_D.Rows[0]["POSITION"].ToString() : strPointName;
                                                    BoilerName = BoilerName == "" ? dtYC_D.Rows[0]["BOILER_NAME"].ToString() : BoilerName;
                                                    dtYC = new TMisMonitorDustattributeLogic().SelectByTable(new TMisMonitorDustattributeVo { BASEINFOR_ID = dtYC_D.Rows[0]["ID"].ToString() });
                                                }
                                                MsgObj.SetMsgByName(mContractType + "CellsCount", (dtSO2.Rows.Count + dtYC.Rows.Count + 2).ToString());
                                                DataTable dt_Attribute_Return = new DataTable();

                                                #region add column
                                                for (int j = 1; j < 9; j++)
                                                {
                                                    dt_Attribute_Return.Columns.Add("A" + j.ToString());
                                                }
                                                #endregion

                                                #region add row
                                                DataRow attNewRow = dt_Attribute_Return.NewRow();
                                                attNewRow["A1"] = "采样位置";
                                                attNewRow["A2"] = "采样序号";
                                                attNewRow["A3"] = "SO2实测浓度(mg/Nm3)";
                                                attNewRow["A4"] = "SO2折算浓度(mg/Nm3)";
                                                attNewRow["A5"] = "SO2排放量(kg/h)";
                                                attNewRow["A6"] = "NOX实测浓度(mg/Nm3)";
                                                attNewRow["A7"] = "NOX折算浓度(mg/Nm3)";
                                                attNewRow["A8"] = "NOX排放量(kg/h)";
                                                dt_Attribute_Return.Rows.Add(attNewRow);
                                                for (int k = 1; k < dtSO2.Rows.Count + 1; k++)
                                                {
                                                    attNewRow = dt_Attribute_Return.NewRow();
                                                    dt_Attribute_Return.Rows.Add(attNewRow);
                                                }

                                                attNewRow = dt_Attribute_Return.NewRow();
                                                attNewRow["A1"] = "采样位置";
                                                attNewRow["A2"] = "采样序号";
                                                attNewRow["A3"] = "烟尘实测浓度(mg/Nm3)";
                                                attNewRow["A4"] = "烟尘折算浓度(mg/Nm3)";
                                                attNewRow["A5"] = "烟尘排放量(kg/h)";
                                                attNewRow["A6"] = "烟气含氧量(%)";
                                                attNewRow["A7"] = "标态流量(Nm3/h)";
                                                attNewRow["A8"] = "--";
                                                dt_Attribute_Return.Rows.Add(attNewRow);
                                                for (int k = 1; k < dtYC.Rows.Count + 1; k++)
                                                {
                                                    attNewRow = dt_Attribute_Return.NewRow();
                                                    dt_Attribute_Return.Rows.Add(attNewRow);
                                                }
                                                #endregion

                                                #region 采样位置 SO2 原始记录 NOX 原始记录  烟尘 原始记录
                                                if (dtSO2.Rows.Count > 0)
                                                {
                                                    dt_Attribute_Return.Rows[1]["A1"] = (BoilerName == "" ? "" : BoilerName) + strPointName;// +(BoilerName == "" ? "" : "(" + BoilerName + ")");黄进军注释20140901
                                                    for (int k = 1; k < (dtSO2.Rows.Count + 1); k++)
                                                    {
                                                        dt_Attribute_Return.Rows[k]["A2"] = dtSO2.Rows[k - 1]["SAMPLE_CODE"].ToString();//采样序号
                                                        dt_Attribute_Return.Rows[k]["A3"] = dtSO2.Rows[k - 1]["SO2_POTENCY"].ToString() != "" ? dtSO2.Rows[k - 1]["SO2_POTENCY"].ToString() : "--";//SO2实测浓度
                                                        dt_Attribute_Return.Rows[k]["A4"] = dtSO2.Rows[k - 1]["SO2_PER_POTENCY"].ToString() != "" ? dtSO2.Rows[k - 1]["SO2_PER_POTENCY"].ToString() : "--";//SO2折算浓度
                                                        dt_Attribute_Return.Rows[k]["A5"] = dtSO2.Rows[k - 1]["SO2_DISCHARGE"].ToString();//SO2排放量

                                                        dt_Attribute_Return.Rows[k]["A6"] = dtNOX.Rows[k - 1]["NOX_POTENCY"].ToString() != "" ? dtNOX.Rows[k - 1]["NOX_POTENCY"].ToString() : "--";//NOX实测浓度
                                                        dt_Attribute_Return.Rows[k]["A7"] = dtNOX.Rows[k - 1]["NOX_PER_POTENCY"].ToString() != "" ? dtNOX.Rows[k - 1]["NOX_PER_POTENCY"].ToString() : "--";//NOX折算浓度
                                                        dt_Attribute_Return.Rows[k]["A8"] = dtNOX.Rows[k - 1]["NOX_DISCHARGE"].ToString();//NOX排放量
                                                    }
                                                }
                                                if (dtYC.Rows.Count > 0)
                                                {
                                                    dt_Attribute_Return.Rows[dtSO2.Rows.Count + 2]["A1"] = (BoilerName == "" ? "" : BoilerName) + strPointName;// +(BoilerName == "" ? "" : "(" + BoilerName + ")");黄进军注释20140901
                                                    for (int k = dtSO2.Rows.Count + 2; k < (dtYC.Rows.Count + dtSO2.Rows.Count + 2); k++)
                                                    {
                                                        dt_Attribute_Return.Rows[k]["A2"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SAMPLE_CODE"].ToString();//采样序号
                                                        dt_Attribute_Return.Rows[k]["A3"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_POTENCY"].ToString() != "" ? dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_POTENCY"].ToString() : "--";//烟尘实测浓度
                                                        dt_Attribute_Return.Rows[k]["A4"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_POTENCY2"].ToString() != "" ? dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_POTENCY2"].ToString() : "--";//烟尘折算浓度
                                                        dt_Attribute_Return.Rows[k]["A5"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_DISCHARGE"].ToString();//烟尘排放量
                                                        dt_Attribute_Return.Rows[k]["A6"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["SMOKE_OXYGEN"].ToString();//烟气含氧量
                                                        dt_Attribute_Return.Rows[k]["A7"] = dtYC.Rows[k - (dtSO2.Rows.Count + 2)]["NM_SPEED"].ToString();//标态流量
                                                        dt_Attribute_Return.Rows[k]["A8"] = "--";
                                                    }
                                                }
                                                #endregion

                                                #region 结果值
                                                foreach (DataRow dr in dr_ResultTmp)
                                                {
                                                    #region 二氧化硫
                                                    if (dr["ITEM_NAME"].ToString().Contains("二氧化硫") && dr["ITEM_NAME"].ToString().Contains("实测"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A3"] = dr["ITEM_RESULT"].ToString();//SO2实测浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("二氧化硫") && dr["ITEM_NAME"].ToString().Contains("折算"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A4"] = dr["ITEM_RESULT"].ToString();//SO2折算浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("二氧化硫") && dr["ITEM_NAME"].ToString().Contains("排放量"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A5"] = dr["ITEM_RESULT"].ToString();//SO2排放量
                                                    #endregion

                                                    #region 氮氧化物
                                                    if (dr["ITEM_NAME"].ToString().Contains("氮氧化物") && dr["ITEM_NAME"].ToString().Contains("实测"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A6"] = dr["ITEM_RESULT"].ToString();//NOX实测浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("氮氧化物") && dr["ITEM_NAME"].ToString().Contains("折算"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A7"] = dr["ITEM_RESULT"].ToString();//NOX折算浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("氮氧化物") && dr["ITEM_NAME"].ToString().Contains("排放量"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count]["A8"] = dr["ITEM_RESULT"].ToString();//NOX排放量
                                                    #endregion

                                                    #region 烟尘
                                                    if (dr["ITEM_NAME"].ToString().Contains("烟尘") && dr["ITEM_NAME"].ToString().Contains("实测"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count + dtYC.Rows.Count + 1]["A3"] = dr["ITEM_RESULT"].ToString();//烟尘实测浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("烟尘") && dr["ITEM_NAME"].ToString().Contains("折算"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count + dtYC.Rows.Count + 1]["A4"] = dr["ITEM_RESULT"].ToString();//烟尘折算浓度

                                                    if (dr["ITEM_NAME"].ToString().Contains("烟尘") && dr["ITEM_NAME"].ToString().Contains("排放"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count + dtYC.Rows.Count + 1]["A5"] = dr["ITEM_RESULT"].ToString();//烟尘排放量
                                                    #endregion

                                                    if (dr["ITEM_NAME"].ToString().Contains("烟气含氧量"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count + dtYC.Rows.Count + 1]["A6"] = dr["ITEM_RESULT"].ToString();//烟气含氧量

                                                    if (dr["ITEM_NAME"].ToString().Contains("标态流量"))
                                                        dt_Attribute_Return.Rows[dtSO2.Rows.Count + dtYC.Rows.Count + 1]["A7"] = dr["ITEM_RESULT"].ToString();//标态流量
                                                }
                                                #endregion

                                                dt_Attribute_Return.AcceptChanges();

                                                for (int k = 0; k < dt_Attribute_Return.Columns.Count; k++)
                                                {
                                                    for (int j = 0; j < dt_Attribute_Return.Rows.Count; j++)
                                                    {
                                                        MsgObj.SetMsgByName(mContractType + i.ToString() + "-" + Convert.ToString(k + 1) + "-" + Convert.ToString(j + 1), dt_Attribute_Return.Rows[j][k].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MsgObj.MsgError("加载监测结果失败");
                                }
                                break;
                            #endregion

                            #region 默认
                            default:
                                MsgObj.MsgError("Error:packet message");
                                MsgObj.MsgTextClear();
                                MsgObj.MsgFileClear();
                                break;
                            #endregion
                        }

                        break;
                    #endregion

                    #region 默认
                    default:
                        MsgObj.MsgError("Error:packet message");
                        MsgObj.MsgTextClear();
                        MsgObj.MsgFileClear();
                        break;
                    #endregion
                }
            }
            else
            {
                MsgObj.MsgError("Error:packet message");
                MsgObj.MsgTextClear();
                MsgObj.MsgFileClear();
            }
            Response.BinaryWrite(MsgObj.MsgVariant());
        }

        #region QY function
        private void getQyItemID(DataRow[] dr_ResultTmp, ref string strSO2ItemIDs, ref string strNoxItemIDs, ref string strYcItemIDs)
        {
            foreach (DataRow drtmp in dr_ResultTmp)
            {
                if (drtmp["ITEM_NAME"].ToString().Contains("二氧化硫") || drtmp["ITEM_NAME"].ToString().ToUpper().Contains("SO2"))
                    strSO2ItemIDs += (strSO2ItemIDs.Length > 0 ? "," : "") + drtmp["ITEM_ID"].ToString();
                if (drtmp["ITEM_NAME"].ToString().Contains("氮氧化物") || drtmp["ITEM_NAME"].ToString().ToUpper().Contains("NOX"))
                    strNoxItemIDs += (strNoxItemIDs.Length > 0 ? "," : "") + drtmp["ITEM_ID"].ToString();
                if (drtmp["ITEM_NAME"].ToString().Contains("烟尘") || drtmp["ITEM_NAME"].ToString().Contains("粉尘"))
                    strYcItemIDs += (strYcItemIDs.Length > 0 ? "," : "") + drtmp["ITEM_ID"].ToString();
            }
            if (strSO2ItemIDs.Length > 0)
                strSO2ItemIDs = "'" + strSO2ItemIDs.Replace(",", "','") + "'";
            if (strNoxItemIDs.Length > 0)
                strNoxItemIDs = "'" + strNoxItemIDs.Replace(",", "','") + "'";
            if (strYcItemIDs.Length > 0)
                strYcItemIDs = "'" + strYcItemIDs.Replace(",", "','") + "'";
        }
        #endregion

        #region function
        /// <summary>
        /// 得到模板名称
        /// </summary>
        /// <returns></returns>
        private string GetTemplateName()
        {
            TRptTemplateVo rtv = new TRptTemplateLogic().Details(mTemplate);
            if (rtv == null)
            {
                return "";
            }
            return rtv.FILE_NAME;
        }

        //得到指定监测类别下的所有项目
        private void GetItems_UnderMonitor(string strMonitorType, DataTable dtResult, ref string strItemIDs, ref string strItemName)
        {
            DataRow[] drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");

            for (int i = 0; i < drs.Length; i++)
            {
                if (!strItemIDs.Contains(drs[i]["ITEM_ID"].ToString()))
                {
                    strItemIDs += (strItemIDs.Length > 0 ? "," : "") + drs[i]["ITEM_ID"].ToString();
                    strItemName += (strItemIDs.Length > 0 ? "@" : "") + drs[i]["ITEM_NAME"].ToString();
                    strItemName += drs[i]["ITEM_UNIT"].ToString().Length > 0 ? ("（" + drs[i]["ITEM_UNIT"].ToString() + "）") : "";
                }
            }
        }

        //获取数据DataTable
        //strMonitorType监测类别，strPreColumnName固定列，strPreColumnName_Src固定列对应的字段名，dtResult结果数据表
        private DataTable getDatatable(string strMonitorType, string strPreColumnName, string strPreColumnName_Src, DataTable dtResult)
        {
            //得到指定监测类别下的所有项目
            string strItemIDs = "";
            string strItemName = "";
            GetItems_UnderMonitor(strMonitorType, dtResult, ref  strItemIDs, ref  strItemName);

            DataTable dt_Result_Return = new DataTable();
            //设置Datatable的列
            AddDatatable_column(strPreColumnName, strItemName, ref  dt_Result_Return);
            //填充Datatable的值
            InsertDataTable_Value(strMonitorType, dtResult, strPreColumnName, strPreColumnName_Src, strItemIDs, ref  dt_Result_Return);

            return dt_Result_Return;
        }

        #region 监测结果数据竖着显示 weilin
        //获取数据DataTable Create By weilin
        //监测项目竖着显示
        //strMonitorType监测类别，strPreColumnName固定列，strPreColumnName_Src固定列对应的字段名，dtResult结果数据表
        private DataTable getDatatableEx(string strMonitorType, string strPreColumnName, string strPreColumnName_Src, DataTable dtResult, string strUnitName, string strUnit)
        {
            //得到指定监测类别下的所有测点
            string strPointIDs = "";
            string strPointName = "";
            GetItems_UnderMonitorEx(strMonitorType, dtResult, ref  strPointIDs, ref  strPointName);

            DataTable dt_Result_Return = new DataTable();
            //设置Datatable的列
            AddDatatable_column(strPreColumnName, "", ref  dt_Result_Return);
            //填充Datatable的值
            InsertDataTable_ValueEx(strMonitorType, dtResult, strPreColumnName, strPreColumnName_Src, strPointIDs, ref  dt_Result_Return, strUnitName, strUnit);

            return dt_Result_Return;
        }
        //得到指定监测类别下的所有项目
        private void GetItems_UnderMonitorEx(string strMonitorType, DataTable dtResult, ref string strPointIDs, ref string strPointName)
        {
            DataRow[] drs;
            if (strMonitorType.Length > 0)
                drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
            else
                drs = dtResult.Select("1=1");

            for (int i = 0; i < drs.Length; i++)
            {
                if (!strPointIDs.Contains(drs[i]["SAMPLE_ID"].ToString()))
                {
                    strPointIDs += (strPointIDs.Length > 0 ? "," : "") + drs[i]["SAMPLE_ID"].ToString();
                    strPointName += (strPointName.Length > 0 ? "@" : "") + drs[i]["SAMPLE_NAME"].ToString();
                }
            }
        }
        //填充Datatable的值
        private void InsertDataTable_ValueEx(string strMonitorType, DataTable dtResult, string strPreColumnName, string strPreColumnName_Src, string strPointIDs, ref DataTable dt_Result_Return, string strUnitName, string strUnit)
        {
            //过滤出指定监测类别的Datatable
            DataTable dtTmp = FilterDataTable_byMonitorType(strMonitorType, dtResult);

            string strTempItemID = "";
            string strTempItemIDs = "";
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                //if (strTempSampleID == dtTmp.Rows[i]["SAMPLE_ID"].ToString())
                //    continue;
                //strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();
                string strSrhStr = "," + dtTmp.Rows[i]["ITEM_ID"].ToString() + ",";
                //if (strTempItemIDs.Contains(strSrhStr))
                //    continue;
                strTempItemIDs += "," + dtTmp.Rows[i]["ITEM_ID"].ToString() + ",";
                strTempItemID = dtTmp.Rows[i]["ITEM_ID"].ToString();

                DataRow dr = dt_Result_Return.NewRow();
                DataRow drSrc = dtTmp.Rows[i];

                infullPreColumn_ValueEx(drSrc, strPreColumnName, strPreColumnName_Src, ref dr, strUnitName, strUnit);
                //infullResultColumn_ValueEx(dtTmp, strPreColumnName, strTempItemID, strPointIDs, ref dr);

                dt_Result_Return.Rows.Add(dr);
            }
        }
        //给前几个固定列赋值，比如“监测日期,监测点位,样品编号,样品描述”
        private void infullPreColumn_ValueEx(DataRow drSrc, string strPreColumnName, string strPreColumnName_Src, ref DataRow dr, string strUnitName, string strUnit)
        {
            string[] arrPre = strPreColumnName.Split(',');
            string[] arrPreSrc = strPreColumnName_Src.Split(',');
            for (int i = 0; i < arrPre.Length; i++)
            {
                if (arrPreSrc[i].Length > 0)
                {
                    if (arrPre[i].Contains("项目"))
                    {
                        if (drSrc[strUnitName].ToString() != "" && drSrc[strUnitName].ToString() != strUnit)
                        {
                            dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString() + "(" + drSrc[strUnitName].ToString() + ")";
                        }
                        else
                        {
                            dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString();
                        }
                    }
                    else
                        dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString();
                }
                else
                    dr[arrPre[i]] = "";
            }
        }
        //给结果列赋值
        private void infullResultColumn_ValueEx(DataTable dtTmp, string strPreColumnName, string strTempItemID, string strPointIDs, ref DataRow dr)
        {
            string[] arrPre = strPreColumnName.Split(',');
            string[] arrPointIds = strPointIDs.Split(',');
            int iPreColumnCount = arrPre.Length;

            DataRow[] drSrc = dtTmp.Select("ITEM_ID='" + strTempItemID + "'");

            for (int i = 0; i < drSrc.Length; i++)
            {
                string strTmpPointId = drSrc[i]["SAMPLE_ID"].ToString();
                //从column中找到对应该item的列，在该列该行填值
                for (int j = 0; j < arrPointIds.Length; j++)
                {
                    if (arrPointIds[j] == strTmpPointId)
                    {
                        int iColumnIdx = iPreColumnCount + j;//加上前面的固定列
                        dr[iColumnIdx] = drSrc[i]["ITEM_RESULT"].ToString();
                    }
                }
            }
        }
        #endregion

        //获取数据DataTable
        //strMonitorType监测类别，strPreColumnName固定列，strPreColumnName_Src固定列对应的字段名，dtResult结果数据表
        private DataTable getDatatable_Dustinfor(string strMonitorType, string strPreColumnName, string strPreColumnName_Src, DataTable dtResult)
        {
            //得到指定监测类别下的所有项目
            string strItemIDs = "";
            string strItemName = "";
            GetItems_UnderMonitor(strMonitorType, dtResult, ref  strItemIDs, ref  strItemName);

            DataTable dt_Result_Return = new DataTable();
            //设置Datatable的列
            AddDatatable_column(strPreColumnName, strItemName, ref  dt_Result_Return);
            //填充Datatable的值
            InsertDataTable_Value_Dustinfor(strMonitorType, dtResult, strPreColumnName, strPreColumnName_Src, strItemIDs, ref  dt_Result_Return);

            return dt_Result_Return;
        }
        //填充Datatable的值
        private void InsertDataTable_Value_Dustinfor(string strMonitorType, DataTable dtResult, string strPreColumnName, string strPreColumnName_Src, string strItemIDs, ref DataTable dt_Result_Return)
        {
            //过滤出指定监测类别的Datatable
            DataTable dtTmp = FilterDataTable_byMonitorType(strMonitorType, dtResult);

            string strID = "";
            string strTempSampleIDs = "";
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                //if (strTempSampleID == dtTmp.Rows[i]["SAMPLE_ID"].ToString())
                //    continue;
                //strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();
                string strSrhStr = "," + dtTmp.Rows[i]["SAMPLE_ID"].ToString() + ",";

                strTempSampleIDs += "," + dtTmp.Rows[i]["SAMPLE_ID"].ToString() + ",";
                strID = dtTmp.Rows[i]["ID"].ToString();

                DataRow dr = dt_Result_Return.NewRow();
                DataRow drSrc = dtTmp.Rows[i];

                infullPreColumn_Value(drSrc, strPreColumnName, strPreColumnName_Src, ref dr);
                infullResultColumn_Value_Dustinfor(dtTmp, strPreColumnName, strID, strItemIDs, ref dr);

                dt_Result_Return.Rows.Add(dr);
            }
        }
        //给结果列赋值
        private void infullResultColumn_Value_Dustinfor(DataTable dtTmp, string strPreColumnName, string strID, string strItemIDs, ref DataRow dr)
        {
            string[] arrPre = strPreColumnName.Split(',');
            string[] arrItemIds = strItemIDs.Split(',');
            int iPreColumnCount = arrPre.Length;

            DataRow[] drSrc = dtTmp.Select("ID='" + strID + "'");

            for (int i = 0; i < drSrc.Length; i++)
            {
                string strTmpItemId = drSrc[i]["ITEM_ID"].ToString();
                //从column中找到对应该item的列，在该列该行填值
                for (int j = 0; j < arrItemIds.Length; j++)
                {
                    if (arrItemIds[j] == strTmpItemId)
                    {
                        int iColumnIdx = iPreColumnCount + j;//加上前面的固定列
                        dr[iColumnIdx] = drSrc[i]["ITEM_RESULT"].ToString();
                    }
                }
            }
        }

        //设置Datatable的列
        private void AddDatatable_column(string strPreColumnName, string strItemNames, ref DataTable dt)
        {
            if (strPreColumnName.Length > 0)
            {
                string[] arrPreColumnName = strPreColumnName.Split(',');
                for (int i = 0; i < arrPreColumnName.Length; i++)
                {
                    dt.Columns.Add(arrPreColumnName[i], System.Type.GetType("System.String"));
                }
            }

            if (strItemNames.Length > 0)
            {
                string[] arrItemNames = strItemNames.Split('@');
                for (int i = 0; i < arrItemNames.Length; i++)
                {
                    if (arrItemNames[i].Length > 0)
                        dt.Columns.Add(arrItemNames[i], System.Type.GetType("System.String"));
                }
            }
        }

        //填充Datatable的值
        private void InsertDataTable_Value(string strMonitorType, DataTable dtResult, string strPreColumnName, string strPreColumnName_Src, string strItemIDs, ref DataTable dt_Result_Return)
        {
            //过滤出指定监测类别的Datatable
            DataTable dtTmp = FilterDataTable_byMonitorType(strMonitorType, dtResult);

            string strTempSampleID = "";
            string strTempSampleIDs = "";
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                //if (strTempSampleID == dtTmp.Rows[i]["SAMPLE_ID"].ToString())
                //    continue;
                //strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();
                string strSrhStr = "," + dtTmp.Rows[i]["SAMPLE_ID"].ToString() + ",";
                if (strTempSampleIDs.Contains(strSrhStr))
                    continue;
                strTempSampleIDs += "," + dtTmp.Rows[i]["SAMPLE_ID"].ToString() + ",";
                strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();

                DataRow dr = dt_Result_Return.NewRow();
                DataRow drSrc = dtTmp.Rows[i];

                infullPreColumn_Value(drSrc, strPreColumnName, strPreColumnName_Src, ref dr);
                infullResultColumn_Value(dtTmp, strPreColumnName, strTempSampleID, strItemIDs, ref dr);

                dt_Result_Return.Rows.Add(dr);
            }
        }

        //给前几个固定列赋值，比如“监测日期,监测点位,样品编号,样品描述”
        private void infullPreColumn_Value(DataRow drSrc, string strPreColumnName, string strPreColumnName_Src, ref DataRow dr)
        {
            string[] arrPre = strPreColumnName.Split(',');
            string[] arrPreSrc = strPreColumnName_Src.Split(',');
            for (int i = 0; i < arrPre.Length; i++)
            {
                if (arrPreSrc[i].Length > 0)
                    dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString().Replace("0:00:00", "");
                else
                    dr[arrPre[i]] = "";
            }
        }

        //给结果列赋值
        private void infullResultColumn_Value(DataTable dtTmp, string strPreColumnName, string strTempSampleID, string strItemIDs, ref DataRow dr)
        {
            string[] arrPre = strPreColumnName.Split(',');
            string[] arrItemIds = strItemIDs.Split(',');
            int iPreColumnCount = arrPre.Length;

            DataRow[] drSrc = dtTmp.Select("SAMPLE_ID='" + strTempSampleID + "'");

            for (int i = 0; i < drSrc.Length; i++)
            {
                string strTmpItemId = drSrc[i]["ITEM_ID"].ToString();
                //从column中找到对应该item的列，在该列该行填值
                for (int j = 0; j < arrItemIds.Length; j++)
                {
                    if (arrItemIds[j] == strTmpItemId)
                    {
                        int iColumnIdx = iPreColumnCount + j;//加上前面的固定列
                        dr[iColumnIdx] = drSrc[i]["ITEM_RESULT"].ToString();
                    }
                }
            }
        }

        //过滤出指定监测类别的Datatable
        private DataTable FilterDataTable_byMonitorType(string strMonitorType, DataTable dtResult)
        {
            DataRow[] drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
            DataTable dtTmp = new DataTable();

            for (int i = 0; i < dtResult.Columns.Count; i++)
            {
                dtTmp.Columns.Add(dtResult.Columns[i].ColumnName, System.Type.GetType("System.String"));
            }

            for (int i = 0; i < drs.Length; i++)
            {
                DataRow drtmp = dtTmp.NewRow();
                for (int j = 0; j < dtResult.Columns.Count; j++)
                {
                    drtmp[j] = drs[i][j].ToString();
                }

                dtTmp.Rows.Add(drtmp);
            }

            return dtTmp;
        }

        //构造webOffice所需DataTable、参数
        private void getWebOfficeDatatable(ref DataTable dt_Result_Return, ref int intTableCount, ref int intMod, ref int intResult_ColumnCount, int intPre_ColumnCount, bool iTitle)
        {
            if (dt_Result_Return.Columns.Count <= intResult_ColumnCount + intPre_ColumnCount)
            {
                intResult_ColumnCount = dt_Result_Return.Columns.Count - intPre_ColumnCount;
            }
            if (intResult_ColumnCount != 0)
            {
                intMod = (dt_Result_Return.Columns.Count - intPre_ColumnCount) % intResult_ColumnCount;//最后一个表多几列
                intTableCount = (dt_Result_Return.Columns.Count - intPre_ColumnCount) / intResult_ColumnCount;//分几个表
            }
            else
            {
                intTableCount = 1;
            }
            if (intMod > 0)
            {
                intTableCount += 1;
            }

            //为webOffice 填充，整理DataTable
            getDatatable_forWebOffice(intMod, intResult_ColumnCount, ref dt_Result_Return, iTitle);

            dt_Result_Return.AcceptChanges();
        }

        //绘制结果表格
        //要汇总的结果DataTable,结果列数intResult_ColumnCount，固定列数intPre_ColumnCount,默认单位strUnit
        private void DrawTable(DataTable dt_Result_Return, int intResult_ColumnCount, int intPre_ColumnCount, string strUnit, bool b)
        {
            if (dt_Result_Return.Rows.Count > 0)
            {
                int intMod = 0;//最后一个分表的列数
                int intTableCount = 0;//几个分表

                //构造webOffice所需DataTable、参数
                getWebOfficeDatatable(ref dt_Result_Return, ref  intTableCount, ref  intMod, ref intResult_ColumnCount, intPre_ColumnCount, true);
                if (strUnit.Length > 0)
                {
                    for (int i = 0; i < dt_Result_Return.Columns.Count; i++)
                    {
                        if (strUnit.Contains("@"))
                        {
                            string[] arrUnit = strUnit.Split('@');
                            for (int j = 0; j < arrUnit.Length; j++)
                            {
                                dt_Result_Return.Rows[0][i] = dt_Result_Return.Rows[0][i].ToString().Replace(arrUnit[j], "");
                            }
                        }
                        else
                        {
                            dt_Result_Return.Rows[0][i] = dt_Result_Return.Rows[0][i].ToString().Replace(strUnit, "");
                        }
                    }
                }

                // 构造webOffice表格
                if (b)
                    makeWebOffice_msg(intTableCount, intPre_ColumnCount, intResult_ColumnCount, dt_Result_Return);
                else
                    makeWebOffice_msgEx(intTableCount, intPre_ColumnCount, intResult_ColumnCount, dt_Result_Return);
            }
        }

        //为webOffice 填充，整理DataTable
        private void getDatatable_forWebOffice(int intMod, int intResult_ColumnCount, ref DataTable dt_Result_Return, bool iTitle)
        {
            //加空格列,凑足整倍数
            if (intMod > 0)
            {
                for (int y = intMod; y < intResult_ColumnCount; y++)
                {
                    dt_Result_Return.Columns.Add("");
                }
            }
            if (iTitle)
            {
                //标题行也作为1行，插入到顶部
                DataRow TitleRow = dt_Result_Return.NewRow();
                for (int p = 0; p < dt_Result_Return.Columns.Count; p++)
                {
                    if (intMod > 0)
                    {
                        if (p >= dt_Result_Return.Columns.Count - intResult_ColumnCount + intMod)
                            TitleRow[p] = "--";
                        else
                            TitleRow[p] = dt_Result_Return.Columns[p].ColumnName;
                    }
                    else
                        TitleRow[p] = dt_Result_Return.Columns[p].ColumnName;
                }
                dt_Result_Return.Rows.InsertAt(TitleRow, 0);
            }
            //空列填写“—”
            for (int i = 0; i < dt_Result_Return.Rows.Count; i++)
            {
                for (int j = 0; j < dt_Result_Return.Columns.Count; j++)
                {
                    if (dt_Result_Return.Rows[i][j].ToString() == "")
                    {
                        dt_Result_Return.Rows[i][j] = "--";
                    }
                }
            }
        }

        //构造webOffice表格
        private void makeWebOffice_msg(int intTableCount, int intPre_ColumnCount, int intResult_ColumnCount, DataTable dt_Result_Return)
        {
            MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());

            for (int x = 0; x < intTableCount; x++)
            {
                MsgObj.SetMsgByName(mContractType + "ColumnsCount", Convert.ToString(intPre_ColumnCount + intResult_ColumnCount));
                MsgObj.SetMsgByName(mContractType + "CellsCount", dt_Result_Return.Rows.Count.ToString());
                //固定列
                for (int i = 0; i < intPre_ColumnCount; i++)//列
                {
                    for (int j = 0; j < dt_Result_Return.Rows.Count; j++)//行
                    {
                        MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Result_Return.Rows[j][i].ToString());
                    }
                }
                for (int i = intPre_ColumnCount; i < intPre_ColumnCount + intResult_ColumnCount; i++)
                {
                    int iColumnIdx = i + x * intResult_ColumnCount;
                    for (int j = 0; j < dt_Result_Return.Rows.Count; j++)
                    {
                        MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Result_Return.Rows[j][iColumnIdx].ToString());
                    }
                }

            }
        }

        //构造webOffice表格
        private void makeWebOffice_msgEx(int intTableCount, int intPre_ColumnCount, int intResult_ColumnCount, DataTable dt_Result_Return)
        {
            MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());

            for (int x = 0; x < intTableCount; x++)
            {
                //固定列
                for (int i = 0; i < intPre_ColumnCount; i++)//列
                {
                    for (int j = 0; j < dt_Result_Return.Rows.Count; j++)//行
                    {
                        bool bM = false;
                        for (int k = intPre_ColumnCount; k < intPre_ColumnCount + intResult_ColumnCount; k++)
                        {
                            int iColumnIdx = k + x * intResult_ColumnCount;
                            if (dt_Result_Return.Rows[j][iColumnIdx].ToString() != "--")
                                bM = true;
                        }
                        if (!bM)
                        {
                            continue;
                        }
                        MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Result_Return.Rows[j][i].ToString());
                    }
                }
                for (int i = intPre_ColumnCount; i < intPre_ColumnCount + intResult_ColumnCount; i++)
                {
                    int iColumnIdx = i + x * intResult_ColumnCount;
                    for (int j = 0; j < dt_Result_Return.Rows.Count; j++)
                    {
                        bool bM = false;
                        for (int k = intPre_ColumnCount; k < intPre_ColumnCount + intResult_ColumnCount; k++)
                        {
                            int iColumn = k + x * intResult_ColumnCount;
                            if (dt_Result_Return.Rows[j][iColumn].ToString() != "--")
                                bM = true;
                        }
                        if (!bM)
                            continue;
                        MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dt_Result_Return.Rows[j][iColumnIdx].ToString());
                    }
                }
                MsgObj.SetMsgByName(mContractType + "ColumnsCount", Convert.ToString(intPre_ColumnCount + intResult_ColumnCount));
                MsgObj.SetMsgByName(mContractType + "CellsCount", (dt_Result_Return.Rows.Count).ToString());
            }
        }
        #endregion

        /// <summary>
        /// 获取报告的监测内容
        /// </summary>
        /// <param name="strMonitorIDs"></param>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        private string getMonitorContent(string strMonitorIDs, string strTaskID)
        {
            string strContent = "";
            string[] strMonitorID = strMonitorIDs.Split(';');
            TBaseMonitorTypeInfoVo MonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
            for (int i = 0; i < strMonitorID.Length; i++)
            {
                MonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(strMonitorID[i].ToString());
                if (strMonitorID[i].ToString() == "000000002")
                {
                    //strContent += MonitorTypeInfoVo.MONITOR_TYPE_NAME + "、";
                    DataTable dtItem = new ReportBuildLogic().getItemByTaskID(strTaskID, strMonitorID[i].ToString());
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        if (dtItem.Rows[j]["ITEM_NAME"].ToString().Contains("油烟"))
                        {
                            if (!strContent.Contains("油烟、"))
                                strContent += "油烟、";
                        }
                        else if (dtItem.Rows[j]["ITEM_NAME"].ToString().Contains("烟气黑度"))
                        {
                            if (!strContent.Contains("烟气黑度、"))
                                strContent += "烟气黑度、";
                        }
                        else
                        {
                            if (!strContent.Contains("废气、"))
                                strContent += "废气、";
                        }
                    }
                }
                else if (strMonitorID[i].ToString() == "000000004")
                {
                    DataTable dtItem = new ReportBuildLogic().getItemByTaskID(strTaskID, strMonitorID[i].ToString());
                    if (dtItem.Rows.Count > 0)
                    {
                        if (dtItem.Rows[0]["ITEM_NAME"].ToString().Contains("环境"))
                        {
                            if (!strContent.Contains("环境噪声、"))
                                strContent += "环境噪声、";
                        }
                        else if (dtItem.Rows[0]["ITEM_NAME"].ToString().Contains("建筑"))
                        {
                            if (!strContent.Contains("建筑施工场界噪声、"))
                                strContent += "建筑施工场界噪声、";
                        }
                        else if (dtItem.Rows[0]["ITEM_NAME"].ToString().Contains("铁路"))
                        {
                            if (!strContent.Contains("铁路边界噪声、"))
                                strContent += "铁路边界噪声、";
                        }
                        else if (dtItem.Rows[0]["ITEM_NAME"].ToString().Contains("声源"))
                        {
                            if (!strContent.Contains("声源噪声、"))
                                strContent += "声源噪声、";
                        }
                        else
                        {
                            if (!strContent.Contains("厂界噪声、"))
                                strContent += "厂界噪声、";
                        }
                    }
                }
                else
                {
                    strContent += MonitorTypeInfoVo.MONITOR_TYPE_NAME + "、";
                }
            }

            return strContent.TrimEnd('、');
        }
    }
}