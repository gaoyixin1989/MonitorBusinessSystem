using System;
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
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;

/// <summary>
/// 功能描述：报告生成类
/// 创建时间：2012-12-4
/// 创建人：邵世卓
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
    //private string mBookmark;
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
    //private string mContract;
    //private string mContent;
    private string mTaskID;

    private string mLabelName;
    private string mImageName;
    //private string mTableContent;
    //private int mColumns;
    //private int mCells;
    //private string mLocalFile;
    //private string mRemoteFile;
    //private string mError;
    //打印控制
    //private string mOfficePrints;
    //private int mCopies;
    //自定义信息传递
    //private string mInfo;
    //组件引用
    private DBstep.iMsgServer2000 MsgObj;

    //报告环节
    private string mReportWf;


    //是否有单位说明字符串，mHasFUnit废水，mHasDUnit地表水
    private string mHasFUnit;
    private string mHasDUnit;

    //监测类别   未出综合报告前，报告分类别出，临时修改
    private string mItemTypeID;
    #endregion

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //声明WebOffice对象
        WebOfficeLogic WebOffice = new WebOfficeLogic();
        TRptFileVo file = new TRptFileVo();

        //WebOffice相关属性
        MsgObj = new DBstep.iMsgServer2000();
        mFilePath = Server.MapPath(".");
        //mTableContent = "";
        //mColumns = 3;
        //mCells = 8;

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
                    mTaskID = MsgObj.GetMsgByName("TASKID");	//取得文档名称
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
                    break;
                //--------------保存模板-------------//
                case "SAVETEMPLATE":
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
                    break;
                #endregion

                #region 标签
                //--------------取得文档标签-------------//
                case "LOADBOOKMARKS":
                    #region LOADBOOKMARKS
                    mRecordID = MsgObj.GetMsgByName("RECORDID");		                    //取得文档编号
                    mTemplate = MsgObj.GetMsgByName("TEMPLATE");		                  //取得模板编号
                    mFileName = MsgObj.GetMsgByName("FILENAME");		                    //取得文档名称
                    mFileType = MsgObj.GetMsgByName("FILETYPE");		                      //取得文档类型
                    mTaskID = MsgObj.GetMsgByName("TASKID");                   //合同编号
                    mHasFUnit = MsgObj.GetMsgByName("mHasFUnit");
                    mHasDUnit = MsgObj.GetMsgByName("mHasDUnit");                             //是否有单位说明字符串，mHasFUnit废水，mHasDUnit地表水
                    mItemTypeID = MsgObj.GetMsgByName("ItemTypeID");                          //监测类别   未出综合报告前，报告分类别出，临时修改
                    //监测类别ID
                    mItemTypeID = new TRptTemplateLogic().Details(mTemplate).FILE_DESC;

                    #region 基本信息
                    //报告文件
                    TRptFileVo objFile = new TRptFileLogic().Details(mRecordID);
                    DataTable dtInfo = new ReportBuildLogic().getMonitorTaskInfo(mTaskID, mItemTypeID);
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtInfo.Columns.Count; j++)
                        {
                            string strText = dtInfo.Columns[j].ColumnName.ToString();//插入的标签名 对应数据表列名
                            string strValue = dtInfo.Rows[0][j].ToString();//值
                            MsgObj.SetMsgByName(strText, strValue);
                        }
                    }
                    //报告编制年
                    MsgObj.SetMsgByName("CREATE_YEAR", DateTime.Now.Year.ToString());
                    //页眉报告年度
                    MsgObj.SetMsgByName("REPORT_YEAR_HEADER", DateTime.Now.Year.ToString());
                    //报告编制月
                    MsgObj.SetMsgByName("CREATE_MONTH", DateTime.Now.Month.ToString());
                    //报告编制日
                    MsgObj.SetMsgByName("CREATE_DAY", DateTime.Now.Day.ToString());

                    #region 企业概况
                    //受检企业ID
                    string strTestedCompanyID = new TMisMonitorTaskLogic().Details(mTaskID).TESTED_COMPANY_ID;
                    DataTable dtCompanyInfo = new ReportBuildLogic().getCompanyInfo(strTestedCompanyID);
                    if (dtCompanyInfo.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtCompanyInfo.Columns.Count; j++)
                        {
                            string strText = dtCompanyInfo.Columns[j].ColumnName.ToString();//插入的标签名 对应数据表列名
                            string strValue = dtCompanyInfo.Rows[0][j].ToString();//值
                            MsgObj.SetMsgByName(strText, strValue);
                        }
                        //执行标准
                        string strStandard = dtCompanyInfo.Rows[0]["STANDARD"].ToString();
                        //最终格式的标准
                        string strOutStandard = "";
                        if (strStandard.Contains(";"))
                        {
                            string[] listStandard = strStandard.Split(';');
                            foreach (string str in listStandard)
                            {
                                if (str.Length > 0)
                                    strOutStandard += str + "\r\n";
                            }
                        }
                        else if (strStandard.Contains("；"))
                        {
                            string[] listStandard = strStandard.Split('；');
                            foreach (string str in listStandard)
                            {
                                if (str.Length > 0)
                                    strOutStandard += str + "\r\n";
                            }
                        }
                        else
                        {
                            strOutStandard = strStandard;
                        }
                        MsgObj.SetMsgByName("REPORT_STANDARD", strOutStandard.LastIndexOf("\r\n") > 0 ? strOutStandard.Remove(strOutStandard.LastIndexOf("\r\n")) : strOutStandard);
                        //主要环保设施 格式调整
                        string strProject = dtCompanyInfo.Rows[0]["MAIN_APPARATUS"].ToString();
                        //最终格式的标准
                        string strOutProject = "";
                        if (strProject.Contains(";"))
                        {
                            string[] listProject = strProject.Split(';');
                            foreach (string str in listProject)
                            {
                                if (str.Length > 0)
                                    strOutProject += str + "\r\n";
                            }
                        }
                        else if (strProject.Contains("；"))
                        {
                            string[] listProject = strProject.Split('；');
                            foreach (string str in listProject)
                            {
                                if (str.Length > 0)
                                    strOutProject += str + "\r\n";
                            }
                        }
                        else
                        {
                            strOutProject = strProject;
                        }
                        MsgObj.SetMsgByName("MAIN_APPARATUS", strOutProject.LastIndexOf("\r\n") > 0 ? strOutProject.Remove(strOutProject.LastIndexOf("\r\n")) : strOutProject);

                    }
                    #endregion

                    #endregion

                    #region 天气情况
                    string strWeather = "";
                    DataTable dtWeather = new DataTable();
                    if (isCustomNoiseST())
                    {
                        //噪声天气情况
                        dtWeather = new ReportBuildLogic().getWeatherInfo(mTaskID, mItemTypeID, "noise_weather");
                    }
                    else
                    {
                        //常规天气
                        dtWeather = new ReportBuildLogic().getWeatherInfo(mTaskID, mItemTypeID, "gerenal_weather");
                    }
                    if (dtWeather.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtWeather.Rows)
                        {
                            strWeather += dr["name"].ToString() + "-" + dr["value"].ToString() + "  ";
                        }
                    }
                    MsgObj.SetMsgByName("WEATHER_INFO", strWeather);
                    #endregion

                    #region 加载样品号、点位、样品性状
                    if (isCustomWaterST())
                    {
                        DataTable dtSampleinfoST = new ReportBuildLogic().SelSampleInfoWater_ST(mTaskID, mItemTypeID);
                        string strSampleCodeList = "";//样品编码
                        string strSampleinfoST = "";//样品号、点位、样品性状
                        string strSampleNameList = "";//样品名称集合
                        string strSampleStatusList = "";//样品状态说明（样品名称+状态）
                        if (null != dtSampleinfoST && dtSampleinfoST.Rows.Count > 0)
                        {
                            for (int v = 0; v < dtSampleinfoST.Rows.Count; v++)
                            {
                                string strsSample_code = dtSampleinfoST.Rows[v]["sample_code"].ToString();
                                string strsOutlet_name = dtSampleinfoST.Rows[v]["point_name"].ToString();
                                string strsatt_value = dtSampleinfoST.Rows[v]["ATTRBUTE_VALUE"].ToString();
                                string strSampleName = dtSampleinfoST.Rows[v]["sample_name"].ToString();
                                strSampleinfoST += (strSampleinfoST.Trim().Length > 0 ? "\r\n\t\t" : "") + strsSample_code + "  " + strsOutlet_name + "  " + strsatt_value;
                                strSampleCodeList += strsSample_code + "，";

                                strSampleNameList += (strSampleNameList.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName;
                                strSampleStatusList += (strSampleNameList.Trim().Length > 0 ? "\r\n\t\t" : "") + strSampleName + "：" + strsatt_value;
                            }
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
                                }
                            }
                        }
                        MsgObj.SetMsgByName("SAMPLE_REMARK", strSampleinfoST);
                        MsgObj.SetMsgByName("SAMPLE_CODELIST", strSampleCodeList.Length > 0 ? strSampleCodeList.Remove(strSampleCodeList.LastIndexOf("，")) : "");
                        MsgObj.SetMsgByName("SAMPLE_NAME", strSampleNameList);
                        MsgObj.SetMsgByName("SAMPLE_INFO", strSampleStatusList);
                    }
                    #endregion

                    #region 得到固废点位
                    GetSolidPoint();
                    #endregion

                    #region 采样日期、采样人、监测结论
                    string strContractMan = "";//监测人员（采样人+分析人）
                    string strSampleMan = "";//采样人
                    DateTime minSampleDate = new DateTime();//最早采样时间
                    DateTime maxSampleDate = new DateTime();//最迟采样时间
                    DataTable dtSubTask = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = mTaskID, MONITOR_ID = (mItemTypeID == "0" ? "" : mItemTypeID) });
                    if (dtSubTask.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSubTask.Rows)
                        {
                            //采样负责人员
                            string SampleHeadMan = dr["SAMPLING_MANAGER_ID"].ToString();
                            if (SampleHeadMan.Length > 0)
                            {
                                string strSampleHeadManName = new TSysUserLogic().Details(SampleHeadMan).REAL_NAME;
                                if (!strSampleMan.Contains(strSampleHeadManName))
                                {
                                    strSampleMan += strSampleHeadManName + "、";
                                }
                            }
                            //监测人员&&采样协同人员
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
                        if (minSampleDate != maxSampleDate && minSampleDate != new DateTime() && maxSampleDate != new DateTime())
                        {
                            MsgObj.SetMsgByName("SAMPLE_TIME", minSampleDate.ToString("yyyy年MM月dd日") + "~" + maxSampleDate.ToString("yyyy年MM月dd日"));
                        }
                        else
                        {
                            MsgObj.SetMsgByName("SAMPLE_TIME", maxSampleDate.ToString("yyyy年MM月dd日"));
                        }
                        if (strSampleMan.Length > 0)
                        {
                            MsgObj.SetMsgByName("SAMPLE_USER", strSampleMan.Remove(strSampleMan.LastIndexOf('、')));
                        }
                        else
                        {
                            MsgObj.SetMsgByName("SAMPLE_USER", strSampleMan);
                        }
                    }
                    else
                    {
                        MsgObj.SetMsgByName("SAMPLE_TIME", "");
                        MsgObj.SetMsgByName("SAMPLE_USER", "无。");
                    }
                    strContractMan += strSampleMan;
                    #endregion

                    #region 分析日期、分析人员（负责人+协同人）
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
                                if (!strContractMan.Contains(AnalyseHeadMan))
                                    strContractMan += AnalyseHeadMan + "、";
                            }
                            //分析协同人员
                            string AnalyseMan = dr["ANALYSIER"].ToString();
                            if (AnalyseMan.Length > 0)
                            {
                                if (!strContractMan.Contains(AnalyseMan))
                                    strContractMan += AnalyseMan + "、";
                            }
                            try
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
                            catch { }
                        }
                        if (minAnalysisDate != maxAnalysisDate && minAnalysisDate != new DateTime() && maxAnalysisDate != new DateTime())
                        {
                            MsgObj.SetMsgByName("ANALYSE_TIME", minAnalysisDate.ToString("yyyy年MM月dd日") + "~" + maxAnalysisDate.ToString("yyyy年MM月dd日"));
                        }
                        else
                        {
                            MsgObj.SetMsgByName("ANALYSE_TIME", maxAnalysisDate.ToString("yyyy年MM月dd日"));
                        }
                    }
                    else
                    {
                        MsgObj.SetMsgByName("ANALYSE_TIME", "");
                    }

                    //监测人员（采样人员+分析人员）
                    if (strContractMan.Length > 0)
                    {
                        MsgObj.SetMsgByName("CONTRACT_PEOPLE", strContractMan.Remove(strContractMan.LastIndexOf("、")));
                    }
                    else
                    {
                        MsgObj.SetMsgByName("CONTRACT_PEOPLE", "");
                    }
                    #endregion

                    #region 噪声
                    if (isCustomNoiseST())
                    {
                        DataTable TestInfo = new DataTable();
                        Get_NoiseST_Report_Result(ref TestInfo);

                        //报告名称

                        #region 加载标签
                        if (null != TestInfo && TestInfo.Rows.Count > 0)
                        {
                            for (int q = 0; q < TestInfo.Columns.Count; q++)
                            {
                                string MarkName = TestInfo.Columns[q].ColumnName;
                                string MarkValue = TestInfo.Rows[0][MarkName].ToString();
                                //string MarkName = WebOffice.GetBookMarkNameByAttribute(AttributeName);
                                MsgObj.SetMsgByName(MarkName, MarkValue.Replace("&#x0D;", "\r").Replace("&#x0A;", "\n"));
                            }
                        }
                        else
                        {
                            MsgObj.MsgError("替换标签信息失败!");
                        }
                        #endregion
                    }
                    #endregion

                    #region 秦皇岛噪声、固废
                    DataTable objTableNS = new ReportBuildLogic().getSampleResultForLicense(mTaskID);
                    ////固废
                    //DataRow[] drListSolid = objTableTemp.Select("监测类别='000000003'");
                    ////点位信息
                    //DataTable dtPointSolid = new TMisMonitorTaskPointLogic().SelectByTableByTaskIDForLicense(new TMisMonitorTaskPointVo() { TASK_ID = mTaskID, MONITOR_ID = "000000003" });
                    //foreach (DataRow drPoint in dtPointSolid.Rows)
                    //{
                    //    //获取该类别的所有监测项目
                    //    //DataTable dtItem = new TMisMonitorResultLogic().getItemInfoForReport(mTaskID, drPoint["ID"].ToString(), "000000003");
                    //    //foreach (DataRow drItem in dtItem.Rows)
                    //    //{
                    //    //    //插入新表
                    //    //    DataRow drNew = dtNew.NewRow();
                    //    //    drNew["监测点位及时间"] = drPoint["POINT_NAME"].ToString();
                    //    //    drNew["监测项目"] = drItem["ITEM_NAME"].ToString();
                    //    //}
                    //}
                    //噪声
                    DataRow[] drListNoise = objTableNS.Select("监测类别='000000004'");
                    foreach (DataRow drPoint in drListNoise)
                    {

                        if (drPoint["监测项目"].ToString().Contains("昼间"))
                        {
                            MsgObj.SetMsgByName("NOISE_DAY_TIME_QHD", drPoint["监测时间"].ToString());
                            MsgObj.SetMsgByName("NOISE_STANDARD_QHD", drPoint["执行标准号"].ToString());
                            MsgObj.SetMsgByName("NOISE_STANDARD_QHD1", drPoint["执行标准号"].ToString());
                        }
                        if (drPoint["监测项目"].ToString().Contains("夜间"))
                        {
                            MsgObj.SetMsgByName("NOISE_NIGHT_TIME_QHD", drPoint["监测时间"].ToString());
                            MsgObj.SetMsgByName("NOISE_STANDARD_QHD", drPoint["执行标准号"].ToString());
                            MsgObj.SetMsgByName("NOISE_STANDARD_QHD1", drPoint["执行标准号"].ToString());
                        }
                        if (drPoint["监测点位"].ToString() == "厂西边界" && drPoint["监测项目"].ToString().Contains("实测值"))
                        {
                            MsgObj.SetMsgByName("NOISE_OUTLET1_QHD", drPoint["监测点位"].ToString());
                            string strStandardValue = drPoint["标准值"].ToString();
                            if (strStandardValue.Contains(";"))
                            {
                                string[] str = strStandardValue.Split(';');
                                //上限
                                if (str[0].ToString().Contains(","))
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST1_QHD", str[0].Split(',')[0]);
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST1_QHD", str[0].Split(',')[1]);
                                }
                                else
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST1_QHD", str[0].ToString());
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST1_QHD", str[0].ToString());
                                }
                            }
                            else
                            {
                                //昼上限
                                MsgObj.SetMsgByName("NOISE_D_ST1_QHD", "");
                                //夜上限
                                MsgObj.SetMsgByName("NOISE_N_ST1_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("昼间"))
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT1_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT1_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("夜间"))
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT1_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT1_QHD", "");
                            }
                        }
                        if (drPoint["监测点位"].ToString() == "厂东边界" && drPoint["监测项目"].ToString() == "Leq（昼间实测值）")
                        {
                            MsgObj.SetMsgByName("NOISE_OUTLET2_QHD", drPoint["监测点位"].ToString());
                            string strStandardValue = drPoint["标准值"].ToString();
                            if (strStandardValue.Contains(";"))
                            {
                                string[] str = strStandardValue.Split(';');
                                //上限
                                if (str[0].ToString().Contains(","))
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST2_QHD", str[0].Split(',')[0]);
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST2_QHD", str[0].Split(',')[1]);
                                }
                                else
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST2_QHD", str[0].ToString());
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST2_QHD", str[0].ToString());
                                }
                            }
                            else
                            {
                                //昼上限
                                MsgObj.SetMsgByName("NOISE_D_ST2_QHD", "");
                                //夜上限
                                MsgObj.SetMsgByName("NOISE_N_ST2_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("昼间"))
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT2_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT2_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("夜间"))
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT2_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT2_QHD", "");
                            }
                        }
                        if (drPoint["监测点位"].ToString() == "厂北边界" && drPoint["监测项目"].ToString() == "Leq（昼间实测值）")
                        {
                            MsgObj.SetMsgByName("NOISE_OUTLET3_QHD", drPoint["监测点位"].ToString());
                            string strStandardValue = drPoint["标准值"].ToString();
                            if (strStandardValue.Contains(";"))
                            {
                                string[] str = strStandardValue.Split(';');
                                //上限
                                if (str[0].ToString().Contains(","))
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST3_QHD", str[0].Split(',')[0]);
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST3_QHD", str[0].Split(',')[1]);
                                }
                                else
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST3_QHD", str[0].ToString());
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST3_QHD", str[0].ToString());
                                }
                            }
                            else
                            {
                                //昼上限
                                MsgObj.SetMsgByName("NOISE_D_ST3_QHD", "");
                                //夜上限
                                MsgObj.SetMsgByName("NOISE_N_ST3_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("昼间"))
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT3_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT3_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("夜间"))
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT3_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT3_QHD", "");
                            }
                        }
                        if (drPoint["监测点位"].ToString() == "厂南边界" && drPoint["监测项目"].ToString() == "Leq（昼间实测值）")
                        {
                            MsgObj.SetMsgByName("NOISE_OUTLET4_QHD", drPoint["监测点位"].ToString());
                            string strStandardValue = drPoint["标准值"].ToString();
                            if (strStandardValue.Contains(";"))
                            {
                                string[] str = strStandardValue.Split(';');
                                //上限
                                if (str[0].ToString().Contains(","))
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST4_QHD", str[0].Split(',')[0]);
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST4_QHD", str[0].Split(',')[1]);
                                }
                                else
                                {
                                    //昼上限
                                    MsgObj.SetMsgByName("NOISE_D_ST4_QHD", str[0].ToString());
                                    //夜上限
                                    MsgObj.SetMsgByName("NOISE_N_ST4_QHD", str[0].ToString());
                                }
                            }
                            else
                            {
                                //昼上限
                                MsgObj.SetMsgByName("NOISE_D_ST4_QHD", "");
                                //夜上限
                                MsgObj.SetMsgByName("NOISE_N_ST4_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("昼间"))
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT4_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_DS_RESULT4_QHD", "");
                            }
                            if (drPoint["监测项目"].ToString().Contains("夜间"))
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT4_QHD", drPoint["监测结果"].ToString());
                            }
                            else
                            {
                                MsgObj.SetMsgByName("NOISE_NS_RESULT4_QHD", "");
                            }
                        }
                    }
                    #endregion


                    if (!String.IsNullOrEmpty(mTaskID))
                    {
                        //TOaAttVo objAtt = new TOaAttLogic().getImgByTask(mTaskID, "SubTask");
                        //if (objAtt != null)
                        //{
                        //    //客户端保存的文件路径
                        //    string fileName = objAtt.UPLOAD_PATH;
                        //    //获取主文件路径
                        //    string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
                        //    string filePath = mastPath + '\\' + fileName;
                        //    if (filePath != "")
                        //    {
                        //        //MsgObj.MsgTextClear();
                        //        MsgObj.MsgFileLoad(filePath);
                        //        MsgObj.SetMsgByName("POSITION", "FLOW_IMG");
                        //        MsgObj.SetMsgByName("IMAGETYPE", ".jpg");
                        //    }
                        //}
                    }
                    else
                    {
                        MsgObj.MsgError("加载流程示意图失败");
                    }
                    #endregion
                    break;
                //--------------保存文档标签-------------//
                #region SAVEBOOKMARKS
                case "SAVEBOOKMARKS":
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
                    break;
                #endregion
                //--------------显示标签列表-------------//
                #region LISTBOOKMARKS
                case "LISTBOOKMARKS":
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
                    break;
                #endregion
                #endregion

                #region 印章
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

                #region 文件操作
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

                #region 远程文件操作(停用，远程图片除外,签名在此)
                case "INSERTIMAGE":
                    mTaskID = MsgObj.GetMsgByName("TASKID");                           //合同编号
                    mContractType = MsgObj.GetMsgByName("CONTRACT_TYPE");                  //合同类型
                    mConditionType = MsgObj.GetMsgByName("CONDITION_TYPE");                //监测条件类型
                    mLabelName = MsgObj.GetMsgByName("LABELNAME");                         //标签名
                    mImageName = MsgObj.GetMsgByName("IMAGENAME");                         //图片名
                    //注意，测试时，务必将印章文件及其目录签出，并保证非只读，否则无法插入到报告
                    TRptSignatureVo rsv = new TRptSignatureVo();
                    rsv.USER_NAME = base.LogInfo.UserInfo.USER_NAME;
                    DataTable dt = new TRptSignatureLogic().SelectByTable(rsv, 0, 0);
                    if (mContractType == "Sign")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[0]["MARK_PATH"].ToString()))
                            {
                                string fileName = dt.Rows[0]["MARK_PATH"].ToString();
                                if (fileName != "" && System.IO.File.Exists(Server.MapPath("~/Channels/Rpt/Sign/") + fileName))
                                {
                                    string strImageUrl = "~/Channels/Rpt/Sign/" + fileName;
                                    strImageUrl = Server.MapPath("~/Channels/Rpt/Sign/" + fileName);

                                    MsgObj.MsgTextClear();

                                    MsgObj.MsgFileLoad(strImageUrl);

                                    MsgObj.SetMsgByName("POSITION", mLabelName);
                                    MsgObj.SetMsgByName("IMAGETYPE", ".jpg");
                                    //MsgObj.SetMsgByName("STATUS", "插入图片成功!");
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
                    }
                    if (mContractType == "FLOW_IMG")
                    {
                        //注意，测试时，务必将印章文件及其目录签出，并保证非只读，否则无法插入到报告
                        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskVo();
                        objSubTaskVo.TASK_ID = mTaskID;
                        //测点示意图数据集
                        DataTable dtTaskImg = new TMisMonitorSubtaskLogic().SelectByTable(objSubTaskVo, 0, 0);
                        if (dtTaskImg.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTaskImg.Rows)
                            {
                                string strImageUrl = GetPointImgPathBySubTask(dr["ID"].ToString());
                                MsgObj.MsgTextClear();
                                MsgObj.MsgFileLoad(strImageUrl);
                                if (mLabelName == "FLOW_IMG_WATER")//废水
                                    MsgObj.SetMsgByName("POSITION", mContractType);
                                if (mLabelName == "FLOW_IMG_GAS")//废气
                                    MsgObj.SetMsgByName("POSITION", mContractType);
                                if (mLabelName == "FLOW_IMG_NOISE")//噪声
                                    MsgObj.SetMsgByName("POSITION", mContractType);
                                if (mLabelName == "FLOW_IMG_SOLID")//固废
                                    MsgObj.SetMsgByName("POSITION", mContractType);
                                MsgObj.SetMsgByName("IMAGETYPE", ".jpg");
                                MsgObj.SetMsgByName("STATUS", "插入图片成功!");
                            }
                        }
                    }
                    else
                    {
                        LigerDialogAlert("不存在印章文件！", "warn");
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
                    mTEST_ITEM_type = MsgObj.GetMsgByName("TEST_ITEM_type");      //三同时的监测项目表格
                    mItemTypeID = MsgObj.GetMsgByName("ItemTypeID");//监测类别   未出综合报告前，报告分类别出，临时修改
                    //监测类别ID
                    mItemTypeID = new TRptTemplateLogic().Details(mTemplate).FILE_DESC;

                    //根据命令类型加载相关数据
                    switch (mCommand)
                    {
                        #region 监测项目
                        case "1":
                            if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                            {
                                DataTable objTableTemp = new DataTable();

                                if (mTEST_ITEM_type.Length > 0 && mTEST_ITEM_type == "TEST_ITEM_THREE_type")
                                {
                                    //objTableTemp = new ReportBuildSystem().GetTestItemForThree(mTaskID);
                                }
                                else if (mTEST_ITEM_type.Length > 0 && mTEST_ITEM_type == "G_TEST_ITEM")
                                {
                                    //objTableTemp = new ReportBuildSystem().GetTestItem_G(mTaskID);
                                }
                                else
                                {
                                    objTableTemp = new ReportBuildLogic().getItemInfoForReportQHD(mTaskID, mItemTypeID);
                                }
                                //秦皇岛项目
                                DataRow[] drQHD = new DataRow[] { };
                                if (mTEST_ITEM_type == "WATER_METHOD_INFO")//废水
                                {
                                    drQHD = objTableTemp.Select("MONITOR_ID='000000001'");
                                }
                                else if (mTEST_ITEM_type == "GAS_METHOD_INFO")//废气
                                {
                                    drQHD = objTableTemp.Select("MONITOR_ID='000000002'");
                                }
                                else if (mTEST_ITEM_type == "NOISE_METHOD_INFO")//噪声
                                {
                                    drQHD = objTableTemp.Select("MONITOR_ID='000000004'");
                                }
                                if (mTEST_ITEM_type == "WATER_METHOD_INFO" || mTEST_ITEM_type == "GAS_METHOD_INFO" || mTEST_ITEM_type == "NOISE_METHOD_INFO")
                                {
                                    //去掉重复项目信息，取第一条
                                    DataTable dtNew = objTableTemp.Clone();
                                    if (null != objTableTemp && objTableTemp.Rows.Count > 0)
                                    {
                                        //去掉重复项目信息，取第一条
                                        foreach (DataRow dr in drQHD)
                                        {
                                            int intCount = 0;
                                            foreach (DataRow drNew in dtNew.Rows)
                                            {
                                                if (drNew["项目名称"].ToString() == dr["项目名称"].ToString())
                                                {
                                                    intCount++;
                                                }
                                            }
                                            if (intCount == 0)
                                            {
                                                dtNew.Rows.Add(dr.ItemArray);
                                            }
                                        }
                                        //添加序号
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dtNew.Rows.Count; i++)
                                            {
                                                dtNew.Rows[i]["序号"] = (i + 1).ToString();
                                            }
                                        }
                                        //手工添加标题行
                                        DataRow TitleRow = dtNew.NewRow();
                                        for (int p = 0; p < dtNew.Columns.Count; p++)
                                        {
                                            TitleRow[p] = dtNew.Columns[p].ColumnName;
                                        }

                                        dtNew.Rows.InsertAt(TitleRow, 0);
                                        dtNew.AcceptChanges();
                                        //移除MONITOR_ID列
                                        dtNew.Columns.Remove("MONITOR_ID");

                                        MsgObj.SetMsgByName("ColumnsCount", dtNew.Columns.Count.ToString());
                                        MsgObj.SetMsgByName("CellsCount", dtNew.Rows.Count.ToString());

                                        for (int i = 0; i < dtNew.Columns.Count; i++)
                                        {
                                            for (int j = 0; j < dtNew.Rows.Count; j++)
                                            {
                                                MsgObj.SetMsgByName(Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dtNew.Rows[j][i].ToString().Replace("《水和废水监测分析方法》(第四版)", "*"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MsgObj.SetMsgByName("ColumnsCount", "");
                                        MsgObj.SetMsgByName("CellsCount", "");
                                    }
                                }
                                else
                                {
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
                                                if (drNew["项目名称"].ToString() == dr["项目名称"].ToString())
                                                {
                                                    intCount++;
                                                }
                                            }
                                            if (intCount == 0)
                                            {
                                                dtNew.Rows.Add(dr.ItemArray);
                                            }
                                        }
                                        //添加序号
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dtNew.Rows.Count; i++)
                                            {
                                                dtNew.Rows[i]["序号"] = (i + 1).ToString();
                                            }
                                        }
                                        //手工添加标题行
                                        DataRow TitleRow = dtNew.NewRow();
                                        for (int p = 0; p < dtNew.Columns.Count; p++)
                                        {
                                            TitleRow[p] = dtNew.Columns[p].ColumnName;
                                        }

                                        dtNew.Rows.InsertAt(TitleRow, 0);
                                        dtNew.AcceptChanges();
                                        //移除MONITOR_ID列
                                        dtNew.Columns.Remove("MONITOR_ID");

                                        MsgObj.SetMsgByName("ColumnsCount", dtNew.Columns.Count.ToString());
                                        MsgObj.SetMsgByName("CellsCount", dtNew.Rows.Count.ToString());

                                        for (int i = 0; i < dtNew.Columns.Count; i++)
                                        {
                                            for (int j = 0; j < dtNew.Rows.Count; j++)
                                            {
                                                MsgObj.SetMsgByName(Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1), dtNew.Rows[j][i].ToString().Replace("《水和废水监测分析方法》(第四版)", "*"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MsgObj.SetMsgByName("ColumnsCount", "");
                                        MsgObj.SetMsgByName("CellsCount", "");
                                    }
                                }
                            }
                            else
                            {
                                MsgObj.MsgError("加载监测项目失败");
                            }
                            break;
                        #endregion

                        #region 监测结果
                        //监测结果
                        case "3":
                            if (!String.IsNullOrEmpty(mCommand) && !String.IsNullOrEmpty(mTaskID))
                            {
                                //获取原始样样品号，通过该样品号，查询结果时，获取无平行的原始样结果值，及现场平行、实验室密码平行的平行样1（样品号与原始样一致）的结果值
                                List<TMisMonitorSampleInfoVo> listSourceSampleInfo = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTask(mTaskID);
                                string strSampleCode = "";
                                for (int i = 0; i < listSourceSampleInfo.Count; i++)
                                {
                                    string newSampleCode = ((TMisMonitorSampleInfoVo)listSourceSampleInfo[i]).SAMPLE_CODE;
                                    if (strSampleCode.IndexOf(newSampleCode) <= 0)
                                    {
                                        strSampleCode += (newSampleCode.Length > 0 ? ",'" + newSampleCode + "'" : "");
                                    }
                                }
                                strSampleCode = strSampleCode.Length > 0 ? strSampleCode.Remove(strSampleCode.IndexOf(','), 1) : "";
                                #region 常规
                                //int intRESULT_WATER_WIDTH = 3;//数据列数
                                //int intResultPreColumnCount = 2;//每个表的表头列数

                                TMisMonitorTaskVo civ = new TMisMonitorTaskLogic().Details(mTaskID);
                                //获取原始样样品号，通过该样品号，查询结果时，获取无平行的原始样结果值，及现场平行、实验室密码平行的平行样1（样品号与原始样一致）的结果值
                                DataTable objTableTemp;
                                if (civ.PLAN_ID.Length > 0)
                                {
                                    //秦皇岛许可证报告监测结果
                                    if (mContractType == "RESULT_LICENSE_WATER" || mContractType == "RESULT_LICENSE_GAS" || mContractType == "RESULT_LICENSE_NOISE")
                                    {
                                        objTableTemp = new ReportBuildLogic().getSampleResultForLicense(mTaskID);
                                    }
                                    else if (mContractType == "REPORT_ACCEPTANCE_WATER" || mContractType == "REPORT_ACCEPTANCE_GAS" || mContractType == "REPORT_ACCEPTANCE_NOISE")
                                    {
                                        objTableTemp = new ReportBuildLogic().getSampleResultForAcceptance(mTaskID);
                                    }
                                    else
                                    {
                                        objTableTemp = new ReportBuildLogic().getSampleResultForQHD(mTaskID, mItemTypeID);
                                    }
                                }
                                else
                                {
                                    objTableTemp = new ReportBuildLogic().getSampleResult(mTaskID, mItemTypeID, mContractType);
                                }
                                // 非自送样
                                if (mContractType != "RESULT_QHD"
                                    && mContractType != "RESULT_LICENSE_WATER" && mContractType != "RESULT_LICENSE_GAS" && mContractType != "RESULT_LICENSE_NOISE"
                                    && mContractType != "REPORT_ACCEPTANCE_WATER" && mContractType != "REPORT_ACCEPTANCE_GAS" && mContractType != "REPORT_ACCEPTANCE_NOISE")//秦皇岛
                                {
                                    Get_StStandard_fromOutlet(ref objTableTemp, mTaskID);
                                }
                                if (mContractType == "RESULT_QHD")
                                {
                                    //intRESULT_WATER_WIDTH = 4;
                                    //intResultPreColumnCount = 1;
                                    if (civ.PLAN_ID.Length > 0)
                                    {
                                        objTableTemp.Columns.Remove("采样位置");
                                    }
                                }
                                //秦皇岛许可证报告
                                //构造新表
                                DataTable dtLicense = new DataTable();
                                dtLicense.Columns.Add("监测点位及时间");
                                dtLicense.Columns.Add("监测项目");
                                dtLicense.Columns.Add("单位");
                                dtLicense.Columns.Add("监测结果-1");
                                dtLicense.Columns.Add("监测结果-2");
                                dtLicense.Columns.Add("监测结果-3");
                                dtLicense.Columns.Add("监测结果-4");
                                dtLicense.Columns.Add("监测结果-平均");
                                dtLicense.Columns.Add("执行标准号及标准值");
                                dtLicense.Columns.Add("达标情况");
                                //秦皇岛验收报告
                                //构造新表
                                //验收报告 废气
                                DataTable dtAcceptanceGas = new DataTable();
                                dtAcceptanceGas.Columns.Add("设施");
                                dtAcceptanceGas.Columns.Add("监测点位");
                                dtAcceptanceGas.Columns.Add("监测项目");
                                dtAcceptanceGas.Columns.Add("监测日期");
                                dtAcceptanceGas.Columns.Add("监测结果-1");
                                dtAcceptanceGas.Columns.Add("监测结果-2");
                                dtAcceptanceGas.Columns.Add("监测结果-3");
                                dtAcceptanceGas.Columns.Add("监测结果-4");
                                dtAcceptanceGas.Columns.Add("均值或范围");
                                dtAcceptanceGas.Columns.Add("处理效率");
                                dtAcceptanceGas.Columns.Add("执行标准标准值");
                                dtAcceptanceGas.Columns.Add("备注");
                                //验收报告 废水
                                DataTable dtAcceptanceWater = new DataTable();
                                dtAcceptanceWater = dtAcceptanceGas.Clone();
                                dtAcceptanceWater.Columns.Remove("备注");
                                dtAcceptanceWater.Columns.Add("参照标准标准值");
                                dtAcceptanceWater.Columns.Add("备注");
                                //验收报告 噪声
                                DataTable dtAcceptanceNoise = new DataTable();
                                dtAcceptanceNoise.Columns.Add("监测日期");
                                dtAcceptanceNoise.Columns.Add("监测点位");

                                //许可证 废水
                                if (mContractType == "RESULT_LICENSE_WATER")
                                {
                                    #region 废水
                                    //点位信息
                                    DataTable dtPointWater = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTaskForLicense(mTaskID, "000000001");
                                    //追加统计点位
                                    DataRow dtNewPoint = dtPointWater.NewRow();
                                    dtNewPoint["POINT_NAME"] = "全厂排放总量";
                                    dtPointWater.Rows.Add(dtNewPoint.ItemArray);

                                    for (int i = 0; i < dtPointWater.Rows.Count; i++)
                                    {
                                        DataRow drPoint = dtPointWater.Rows[i] as DataRow;
                                        DataRow drNew = dtLicense.NewRow();
                                        DataRow[] drItem1 = objTableTemp.Select("监测点位='" + (drPoint["POINT_NAME"].ToString() == "全厂排放总量" ? dtPointWater.Rows[0]["POINT_NAME"].ToString() : drPoint["POINT_NAME"].ToString()) + "1'");
                                        DataRow[] drItem2 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "2'");
                                        DataRow[] drItem3 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "3'");
                                        DataRow[] drItem4 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "4'");

                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            if (drPoint["POINT_NAME"].ToString() != "全厂排放总量")
                                            {
                                                drNew["监测点位及时间"] = drPoint["POINT_NAME"].ToString() + " " + ((dr1["监测点位"].ToString().Contains(drPoint["POINT_NAME"].ToString())) ? dr1["监测时间"].ToString() : "");
                                                drNew["监测项目"] = dr1["监测项目"].ToString();
                                                drNew["单位"] = dr1["单位"].ToString();
                                                drNew["监测结果-1"] = dr1["监测结果"].ToString();
                                                drNew["执行标准号及标准值"] = dr1["执行标准号"].ToString() + dr1["标准值"].ToString();
                                                dtLicense.Rows.Add(drNew.ItemArray);
                                            }
                                        }
                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            if (drPoint["POINT_NAME"].ToString() == "全厂排放总量")
                                            {
                                                drNew["监测点位及时间"] = "全厂排放总量";
                                                drNew["监测项目"] = dr1["监测项目"].ToString();
                                                drNew["单位"] = dr1["监测项目"].ToString() == "排水量" ? "万吨/年" : "吨/年";
                                                drNew["监测结果-1"] = "";
                                                drNew["执行标准号及标准值"] = "";
                                                dtLicense.Rows.Add(drNew.ItemArray);
                                            }
                                        }

                                        for (int a = 0; a < dtLicense.Rows.Count; a++)
                                        {
                                            if (a < drItem2.Length)
                                                dtLicense.Rows[a]["监测结果-2"] = drItem2[a]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int b = 0; b < dtLicense.Rows.Count; b++)
                                        {
                                            if (b < drItem3.Length)
                                                dtLicense.Rows[b]["监测结果-3"] = drItem3[b]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int c = 0; c < dtLicense.Rows.Count; c++)
                                        {
                                            if (c < drItem4.Length)
                                                dtLicense.Rows[c]["监测结果-4"] = drItem4[c]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int d = 0; d < dtLicense.Rows.Count; d++)
                                        {
                                            //小数位数
                                            int intPoint = dtLicense.Rows[d]["监测结果-1"].ToString().Contains(".") ? dtLicense.Rows[d]["监测结果-1"].ToString().IndexOf('.') : 0;
                                            dtLicense.Rows[d]["监测结果-平均"] = Math.Round(((!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-1"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-2"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-3"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-4"].ToString()) : 0)) / 4, intPoint);
                                        }
                                    }

                                    #endregion
                                }
                                //许可证 废气
                                if (mContractType == "RESULT_LICENSE_GAS")
                                {
                                    #region 废气

                                    //点位信息
                                    DataTable dtPointGas = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTaskForLicense(mTaskID, "000000002");
                                    //追加统计点位
                                    DataRow dtNewPoint = dtPointGas.NewRow();
                                    dtNewPoint["POINT_NAME"] = "全厂排放总量";
                                    dtPointGas.Rows.Add(dtNewPoint.ItemArray);

                                    for (int i = 0; i < dtPointGas.Rows.Count; i++)
                                    {
                                        DataRow drPoint = dtPointGas.Rows[i] as DataRow;
                                        DataRow drNew = dtLicense.NewRow();
                                        DataRow[] drItem1 = objTableTemp.Select("监测点位='" + (drPoint["POINT_NAME"].ToString() == "全厂排放总量" ? dtPointGas.Rows[0]["POINT_NAME"].ToString() : drPoint["POINT_NAME"].ToString()) + "1'");
                                        DataRow[] drItem2 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "2'");
                                        DataRow[] drItem3 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "3'");
                                        DataRow[] drItem4 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "4'");
                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            if (drPoint["POINT_NAME"].ToString() != "全厂排放总量")
                                            {
                                                drNew["监测点位及时间"] = drPoint["POINT_NAME"].ToString() + " " + ((dr1["监测点位"].ToString().Contains(drPoint["POINT_NAME"].ToString())) ? dr1["监测时间"].ToString() : "");
                                                drNew["监测项目"] = dr1["监测项目"].ToString();
                                                drNew["单位"] = dr1["单位"].ToString();
                                                drNew["监测结果-1"] = dr1["监测结果"].ToString();
                                                drNew["执行标准号及标准值"] = dr1["执行标准号"].ToString() + dr1["标准值"].ToString();
                                                dtLicense.Rows.Add(drNew.ItemArray);
                                            }
                                        }
                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            if (drPoint["POINT_NAME"].ToString() == "全厂排放总量")
                                            {
                                                drNew["监测点位及时间"] = "全厂排放总量";
                                                drNew["监测项目"] = dr1["监测项目"].ToString();
                                                drNew["单位"] = dr1["监测项目"].ToString() == "排水量" ? "万m3/a" : "t/a";
                                                drNew["监测结果-1"] = "";
                                                drNew["执行标准号及标准值"] = "";
                                                dtLicense.Rows.Add(drNew.ItemArray);
                                            }
                                        }
                                        for (int a = 0; a < dtLicense.Rows.Count; a++)
                                        {
                                            if (a < drItem2.Length)
                                                dtLicense.Rows[a]["监测结果-2"] = drItem2[a]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int b = 0; b < dtLicense.Rows.Count; b++)
                                        {
                                            if (b < drItem3.Length)
                                                dtLicense.Rows[b]["监测结果-3"] = drItem3[b]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int c = 0; c < dtLicense.Rows.Count; c++)
                                        {
                                            if (c < drItem4.Length)
                                                dtLicense.Rows[c]["监测结果-4"] = drItem4[c]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int d = 0; d < dtLicense.Rows.Count; d++)
                                        {
                                            //小数位数
                                            int intPoint = dtLicense.Rows[d]["监测结果-1"].ToString().Contains(".") ? dtLicense.Rows[d]["监测结果-1"].ToString().IndexOf('.') : 0;
                                            dtLicense.Rows[d]["监测结果-平均"] = Math.Round(((!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-1"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-2"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-3"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtLicense.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtLicense.Rows[d]["监测结果-4"].ToString()) : 0)) / 4, intPoint);
                                        }
                                    }

                                    #endregion
                                }
                                //验收报告 废水
                                if (mContractType == "REPORT_ACCEPTANCE_WATER")
                                {
                                    #region 废水
                                    //点位信息
                                    DataTable dtPointWater = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTaskForLicense(mTaskID, "000000001");

                                    for (int i = 0; i < dtPointWater.Rows.Count; i++)
                                    {
                                        DataRow drPoint = dtPointWater.Rows[i] as DataRow;
                                        DataRow drNew = dtAcceptanceWater.NewRow();
                                        DataRow[] drItem1 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "1'");
                                        DataRow[] drItem2 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "2'");
                                        DataRow[] drItem3 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "3'");
                                        DataRow[] drItem4 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "4'");

                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            drNew["监测点位"] = drPoint["POINT_NAME"].ToString();
                                            drNew["监测项目"] = dr1["监测项目"].ToString();
                                            drNew["监测日期"] = (dr1["监测点位"].ToString().Contains(drPoint["POINT_NAME"].ToString())) ? dr1["监测日期"].ToString() : "";
                                            drNew["监测结果-1"] = dr1["监测结果"].ToString();
                                            drNew["执行标准标准值"] = dr1["执行标准标准值"].ToString();
                                            dtAcceptanceWater.Rows.Add(drNew.ItemArray);
                                        }

                                        for (int a = 0; a < dtAcceptanceWater.Rows.Count; a++)
                                        {
                                            if (a < drItem2.Length)
                                                dtAcceptanceWater.Rows[a]["监测结果-2"] = drItem2[a]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int b = 0; b < dtAcceptanceWater.Rows.Count; b++)
                                        {
                                            if (b < drItem3.Length)
                                                dtAcceptanceWater.Rows[b]["监测结果-3"] = drItem3[b]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int c = 0; c < dtAcceptanceWater.Rows.Count; c++)
                                        {
                                            if (c < drItem4.Length)
                                                dtAcceptanceWater.Rows[c]["监测结果-4"] = drItem4[c]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int d = 0; d < dtAcceptanceWater.Rows.Count; d++)
                                        {
                                            //小数位数
                                            int intPoint = dtAcceptanceWater.Rows[d]["监测结果-1"].ToString().Contains(".") ? dtAcceptanceWater.Rows[d]["监测结果-1"].ToString().IndexOf('.') : 0;
                                            dtAcceptanceWater.Rows[d]["均值或范围"] = Math.Round(((!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) : 0)) / 4, intPoint);
                                            if (dtAcceptanceWater.Rows[d]["监测项目"].ToString().ToUpper().IndexOf("PH") > 0
                                                || dtAcceptanceWater.Rows[d]["监测项目"].ToString().ToUpper().IndexOf("PH值") > 0)//取范围值
                                            {
                                                dtAcceptanceWater.Rows[d]["均值或范围"] =
                                                    Math.Min(
                                                    Math.Min(
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) : 0)),
                                                    Math.Min(
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) : 0)))
                                                    + "～" +
                                                    Math.Max(
                                                    Math.Max(
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-1"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-2"].ToString()) : 0)),
                                                    Math.Max(
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-3"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceWater.Rows[d]["监测结果-4"].ToString()) : 0)));
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                //验收报告 废气
                                if (mContractType == "REPORT_ACCEPTANCE_GAS")
                                {
                                    #region 废气

                                    //点位信息
                                    DataTable dtPointGas = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTaskForLicense(mTaskID, "000000002");

                                    for (int i = 0; i < dtPointGas.Rows.Count; i++)
                                    {
                                        DataRow drPoint = dtPointGas.Rows[i] as DataRow;
                                        DataRow drNew = dtAcceptanceGas.NewRow();
                                        DataRow[] drItem1 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "1'");
                                        DataRow[] drItem2 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "2'");
                                        DataRow[] drItem3 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "3'");
                                        DataRow[] drItem4 = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "4'");
                                        foreach (DataRow dr1 in drItem1)
                                        {
                                            drNew["监测点位"] = drPoint["POINT_NAME"].ToString();
                                            drNew["监测项目"] = dr1["监测项目"].ToString();
                                            drNew["监测日期"] = (dr1["监测点位"].ToString().Contains(drPoint["POINT_NAME"].ToString())) ? dr1["监测日期"].ToString() : "";
                                            drNew["监测结果-1"] = dr1["监测结果"].ToString();
                                            drNew["执行标准标准值"] = dr1["执行标准标准值"].ToString();
                                            dtAcceptanceGas.Rows.Add(drNew.ItemArray);
                                        }
                                        for (int a = 0; a < dtAcceptanceGas.Rows.Count; a++)
                                        {
                                            if (a < drItem2.Length)
                                                dtAcceptanceGas.Rows[a]["监测结果-2"] = drItem2[a]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int b = 0; b < dtAcceptanceGas.Rows.Count; b++)
                                        {
                                            if (b < drItem3.Length)
                                                dtAcceptanceGas.Rows[b]["监测结果-3"] = drItem3[b]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int c = 0; c < dtAcceptanceGas.Rows.Count; c++)
                                        {
                                            if (c < drItem4.Length)
                                                dtAcceptanceGas.Rows[c]["监测结果-4"] = drItem4[c]["监测结果"].ToString();
                                            else
                                                break;
                                        }
                                        for (int d = 0; d < dtAcceptanceGas.Rows.Count; d++)
                                        {
                                            //小数位数
                                            int intPoint = dtAcceptanceGas.Rows[d]["监测结果-1"].ToString().Contains(".") ? dtAcceptanceGas.Rows[d]["监测结果-1"].ToString().IndexOf('.') : 0;
                                            dtAcceptanceGas.Rows[d]["均值或范围"] = Math.Round(((!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) : 0)
                                            + (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) : 0)) / 4, intPoint);
                                            if (dtAcceptanceGas.Rows[d]["监测项目"].ToString().ToUpper().IndexOf("PH") > 0
                                                 || dtAcceptanceGas.Rows[d]["监测项目"].ToString().ToUpper().IndexOf("PH值") > 0)//取范围值
                                            {
                                                dtAcceptanceGas.Rows[d]["均值或范围"] =
                                                    Math.Min(
                                                    Math.Min(
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) : 0)),
                                                    Math.Min(
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) : 0)))
                                                    + "～" +
                                                    Math.Max(
                                                    Math.Max(
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-1"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-2"].ToString()) : 0)),
                                                    Math.Max(
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-3"].ToString()) : 0),
                                                    (!string.IsNullOrEmpty(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) ? double.Parse(dtAcceptanceGas.Rows[d]["监测结果-4"].ToString()) : 0)));
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                //验收报告 噪声
                                if (mContractType == "REPORT_ACCEPTANCE_NOISE")
                                {
                                    #region 噪声

                                    //点位信息
                                    DataTable dtPointNoise = new TMisMonitorTaskPointLogic().SelectAllQcTypePoint(mTaskID, "000000004");
                                    for (int i = 0; i < dtPointNoise.Rows.Count; i++)
                                    {
                                        //构建新行数据
                                        DataRow drNew = dtAcceptanceNoise.NewRow();
                                        //点位信息
                                        DataRow drPoint = dtPointNoise.Rows[i] as DataRow;
                                        //对应点位下的项目信息
                                        DataRow[] drItem = objTableTemp.Select("监测点位='" + drPoint["POINT_NAME"].ToString() + "'");

                                        if (drItem != null)
                                        {
                                            //根据项目构建新的表结构
                                            foreach (DataRow dr in drItem)
                                            {
                                                if (!dtAcceptanceNoise.Columns.Contains(dr["监测项目"].ToString()))
                                                {
                                                    dtAcceptanceNoise.Columns.Add(dr["监测项目"].ToString());
                                                    dtAcceptanceNoise.Columns.Add(dr["监测项目"].ToString() + "标准");
                                                    drNew = dtAcceptanceNoise.NewRow();
                                                }
                                            }
                                            //根据新的结构进行赋值
                                            foreach (DataRow dr in drItem)
                                            {
                                                drNew["监测点位"] = drPoint["POINT_NAME"].ToString();
                                                drNew["监测日期"] = dr["监测日期"].ToString();
                                                drNew[dr["监测项目"].ToString()] = dr["监测结果"].ToString();
                                                //添加标准信息
                                                string strUp = "";//上限标准
                                                string strLow = "";//下限标准
                                                GetStandardValueByItemName(dr["监测项目"].ToString(), "000000004", ref strUp, ref strLow);
                                                drNew[dr["监测项目"].ToString() + "标准"] = strUp.Length > 0 ? strUp : (strLow.Length > 0 ? strLow : "--");
                                            }
                                        }
                                        dtAcceptanceNoise.Rows.Add(drNew.ItemArray);
                                    }

                                    #endregion
                                }
                                if (mContractType == "RESULT_WATER_WIDTH")
                                {
                                    if (civ.PLAN_ID.Length > 0)
                                    {
                                        objTableTemp.Columns.Remove("采样位置");
                                    }
                                    else//自送样结果表不显示样品名称
                                    {
                                        objTableTemp.Columns.Remove(objTableTemp.Columns["采样位置"]);
                                    }

                                    for (int m = 0; m < objTableTemp.Columns.Count; m++)
                                    {
                                        objTableTemp.Columns[m].ColumnName = objTableTemp.Columns[m].ColumnName.Replace("(mg/L)", "");
                                    }
                                }
                                //表格编号处理
                                if (mContractType == "RESULT_LICENSE_GAS" || mContractType == "RESULT_LICENSE_WATER")//许可证处理
                                {
                                    //int intMod = 0;
                                    int intTableCount = 1;//表数
                                    MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());
                                    MsgObj.SetMsgByName(mContractType + "ColumnsCount", dtLicense.Columns.Count.ToString());
                                    MsgObj.SetMsgByName(mContractType + "CellsCount", (dtLicense.Rows.Count + 1).ToString());

                                    //记录列名
                                    for (int j = 0; j < dtLicense.Columns.Count; j++)
                                    {
                                        MsgObj.SetMsgByName(mContractType + "1" + "-" + Convert.ToString(j + 1), dtLicense.Columns[j].ToString());
                                    }
                                    //记录数据
                                    for (int j = 0; j < dtLicense.Rows.Count; j++)
                                    {
                                        for (int i = 0; i < dtLicense.Columns.Count; i++)
                                        {
                                            MsgObj.SetMsgByName(mContractType + (j + 2).ToString() + "-" + Convert.ToString(i + 1), dtLicense.Rows[j][i].ToString());
                                        }
                                    }
                                }
                                if (mContractType == "REPORT_ACCEPTANCE_WATER")//验收报告处理
                                {
                                    #region 验收废水 表格处理
                                    int intPreRowCount = 1;//表头行数
                                    int intWidth = 4;//每一表格显示行数
                                    int intMod = dtAcceptanceWater.Rows.Count % intWidth;
                                    int intTableCount = dtAcceptanceWater.Rows.Count / intWidth;
                                    if (intMod > 0)
                                    {
                                        intTableCount += 1;
                                    }
                                    int intSubTableRowCount = intWidth + intPreRowCount;//每一表格显示的所有行数

                                    MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());
                                    MsgObj.SetMsgByName(mContractType + "ColumnsCount", dtAcceptanceWater.Columns.Count.ToString());
                                    MsgObj.SetMsgByName(mContractType + "CellsCount", intSubTableRowCount.ToString());

                                    for (int x = 0; x < intTableCount; x++)
                                    {
                                        //表头
                                        for (int i = 0; i < dtAcceptanceWater.Columns.Count; i++)
                                        {
                                            MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + "0" + "-" + i, dtAcceptanceWater.Columns[i].ColumnName.ToString());
                                        }
                                        for (int j = 0; j < dtAcceptanceWater.Columns.Count; j++)
                                        {
                                            //数据
                                            for (int k = 1; k < intWidth + intPreRowCount; k++)
                                            {
                                                MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + k + "-" + j, dtAcceptanceWater.Rows[(x * intWidth + k) > (dtAcceptanceWater.Rows.Count - 1) ? (dtAcceptanceWater.Rows.Count - 1) : (x * intWidth + k - 1)][j].ToString());
                                                //最后一续表处理
                                                if ((x * intWidth + k) > (dtAcceptanceWater.Rows.Count - 1))
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                if (mContractType == "REPORT_ACCEPTANCE_GAS")
                                {
                                    #region 验收废气 表格处理
                                    int intPreRowCount = 1;//表头行数
                                    int intWidth = 5;//每一表格显示行数
                                    int intMod = dtAcceptanceGas.Rows.Count % intWidth;
                                    int intTableCount = dtAcceptanceGas.Rows.Count / intWidth;
                                    if (intMod > 0)
                                    {
                                        intTableCount += 1;
                                    }
                                    int intSubTableRowCount = intWidth + intPreRowCount;//每一表格显示的所有行数

                                    MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());
                                    MsgObj.SetMsgByName(mContractType + "ColumnsCount", dtAcceptanceGas.Columns.Count.ToString());
                                    MsgObj.SetMsgByName(mContractType + "CellsCount", intSubTableRowCount.ToString());

                                    for (int x = 0; x < intTableCount; x++)
                                    {
                                        //表头
                                        for (int i = 0; i < dtAcceptanceGas.Columns.Count; i++)
                                        {
                                            MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + "0" + "-" + i, dtAcceptanceGas.Columns[i].ColumnName.ToString());
                                        }
                                        for (int j = 0; j < dtAcceptanceGas.Columns.Count; j++)
                                        {
                                            //数据
                                            for (int k = 1; k < intWidth + intPreRowCount; k++)
                                            {
                                                MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + k + "-" + j, dtAcceptanceGas.Rows[(x * intWidth + k) > (dtAcceptanceGas.Rows.Count - 1) ? (dtAcceptanceGas.Rows.Count - 1) : (x * intWidth + k - 1)][j].ToString());
                                                //最后一续表处理
                                                if ((x * intWidth + k) > (dtAcceptanceGas.Rows.Count - 1))
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                if (mContractType == "REPORT_ACCEPTANCE_NOISE")
                                {
                                    #region 验收噪声 表格处理
                                    int intTableCount = 1;//表数
                                    MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());
                                    MsgObj.SetMsgByName(mContractType + "ColumnsCount", dtAcceptanceNoise.Columns.Count.ToString());
                                    MsgObj.SetMsgByName(mContractType + "CellsCount", (dtAcceptanceNoise.Rows.Count + 1).ToString());

                                    //记录列名
                                    for (int j = 0; j < dtAcceptanceNoise.Columns.Count; j++)
                                    {
                                        MsgObj.SetMsgByName(mContractType + "1" + "-" + Convert.ToString(j + 1), dtAcceptanceNoise.Columns[j].ToString());
                                    }
                                    //记录数据
                                    for (int j = 0; j < dtAcceptanceNoise.Rows.Count; j++)
                                    {
                                        for (int i = 0; i < dtAcceptanceNoise.Columns.Count; i++)
                                        {
                                            MsgObj.SetMsgByName(mContractType + (j + 2).ToString() + "-" + Convert.ToString(i + 1), dtAcceptanceNoise.Rows[j][i].ToString());
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 其他格式（注释）
                                    //int intMod = (objTableTemp.Columns.Count - intResultPreColumnCount) % intRESULT_WATER_WIDTH;
                                    //int intTableCount = (objTableTemp.Columns.Count - intResultPreColumnCount) / intRESULT_WATER_WIDTH;
                                    //if (intMod > 0)
                                    //{
                                    //    intTableCount += 1;
                                    //}
                                    //MsgObj.SetMsgByName(mContractType + "TableCount", intTableCount.ToString());

                                    //int intTmpTableCount = intTableCount;
                                    //if (intMod == 0)
                                    //{
                                    //    //刚好满行，不需要加空格
                                    //    intTmpTableCount += 1;
                                    //}

                                    //DataRow TitleRow = objTableTemp.NewRow();
                                    //for (int p = 0; p < objTableTemp.Columns.Count; p++)
                                    //{
                                    //    TitleRow[p] = objTableTemp.Columns[p].ColumnName;
                                    //}
                                    //if (intMod > 0)
                                    //{
                                    //    //加空格列
                                    //    for (int y = intMod + 1; y <= intRESULT_WATER_WIDTH; y++)
                                    //    {
                                    //        objTableTemp.Columns.Add("");
                                    //        for (int b = 0; b < objTableTemp.Rows.Count; b++)
                                    //        {
                                    //            objTableTemp.Rows[b][objTableTemp.Columns.Count - 1] = "—";
                                    //        }
                                    //    }
                                    //}

                                    //objTableTemp.Rows.InsertAt(TitleRow, 0);
                                    //for (int p = 0; p < objTableTemp.Columns.Count; p++)
                                    //{
                                    //    if (objTableTemp.Rows[0][p].ToString() == "")
                                    //    {
                                    //        objTableTemp.Rows[0][p] = "—";
                                    //    }
                                    //}
                                    //objTableTemp.AcceptChanges();
                                    //for (int x = 0; x < intTableCount; x++)
                                    //{
                                    //    int intSubTableColumnCount = intRESULT_WATER_WIDTH + intResultPreColumnCount;
                                    //    //int intSendSubTableColumnCount = intSubTableColumnCount - 1;
                                    //    int intSendSubTableColumnCount = intSubTableColumnCount;
                                    //    MsgObj.SetMsgByName(mContractType + "ColumnsCount", intSendSubTableColumnCount.ToString());
                                    //    MsgObj.SetMsgByName(mContractType + "CellsCount", objTableTemp.Rows.Count.ToString());

                                    //    for (int j = 0; j < objTableTemp.Rows.Count; j++)
                                    //    {
                                    //        MsgObj.SetMsgByName(mContractType + x.ToString() + "-1-" + Convert.ToString(j + 1), objTableTemp.Rows[j][0].ToString());
                                    //        if (mContractType == "RESULT_WATER_WIDTH" || mContractType == "G_RESULT_WATER_WIDTH")
                                    //        {
                                    //            MsgObj.SetMsgByName(mContractType + x.ToString() + "-2-" + Convert.ToString(j + 1), objTableTemp.Rows[j][1].ToString());
                                    //        }
                                    //    }
                                    //    if (mContractType == "RESULT_QHD")
                                    //    {
                                    //        for (int i = x * intRESULT_WATER_WIDTH + intResultPreColumnCount; i < x * intRESULT_WATER_WIDTH + intResultPreColumnCount + intRESULT_WATER_WIDTH; i++)
                                    //        {
                                    //            for (int j = 0; j < objTableTemp.Rows.Count; j++)
                                    //            {
                                    //                MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1 - x * intRESULT_WATER_WIDTH) + "-" + Convert.ToString(j + 1), objTableTemp.Rows[j][i].ToString());
                                    //            }
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        for (int i = x * intRESULT_WATER_WIDTH + intResultPreColumnCount; i < x * intRESULT_WATER_WIDTH + intResultPreColumnCount + intRESULT_WATER_WIDTH; i++)
                                    //        {
                                    //            for (int j = 0; j < objTableTemp.Rows.Count; j++)
                                    //            {
                                    //                MsgObj.SetMsgByName(mContractType + x.ToString() + "-" + Convert.ToString(i + 1 - x * intRESULT_WATER_WIDTH) + "-" + Convert.ToString(j + 1), objTableTemp.Rows[j][i].ToString());
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                MsgObj.MsgError("加载监测结果失败");
                            }
                            break;
                        #endregion

                        #region 测点分布示意图
                        //测点分布示意图
                        case "4":
                            break;
                        #endregion

                        #region 签名
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

                        #region 默认
                        default:
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

    #region 是否噪声模板
    private bool isCustomNoiseST()
    {
        string str = GetTemplateName();
        if (str == "噪声")
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 是否废水模板
    private bool isCustomWaterST()
    {
        string str = GetTemplateName();
        if (str == "废水--多表" || str == "废水--单表" || str == "废水--监督性--单表" || str == "废水--监督性--单表" || str == "委托监测报告" || str == "排污许可证监测报告")
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 是否废气模板
    private bool isCustomGas()
    {
        string str = GetTemplateName();
        if (str == "工艺废气" || str == "厂界废气" || str == "颗粒物" || str == "厨房油烟")
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 是否固废模板
    private bool isCustomSolidST()
    {
        string str = GetTemplateName();
        if (str == "固废--多表" || str == "固废--单表")
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 得到固废点位
    private void GetSolidPoint()
    {
        if (!isCustomSolidST())
            return;

        DataTable dtSampleinfoST = new ReportBuildLogic().SelSampleInfoWater_ST(mTaskID, mItemTypeID);
        string strPoint = "";
        if (null != dtSampleinfoST && dtSampleinfoST.Rows.Count > 0)
        {
            for (int v = 0; v < dtSampleinfoST.Rows.Count; v++)
            {
                string strsOutlet_name = dtSampleinfoST.Rows[v]["POINT_NAME"].ToString();
                strPoint += (strPoint.Trim().Length > 0 ? "\r\n\t\t" : "") + strsOutlet_name;
            }
        }

        MsgObj.SetMsgByName("TESTED_OUTLET", strPoint);
    }
    #endregion


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

    /// <summary>
    /// 点位标准
    /// </summary>
    /// <param name="objTableTemp"></param>
    /// <param name="strTaskID"></param>
    private void Get_StStandard_fromOutlet(ref DataTable objTableTemp, string strTaskID)
    {
        //取得样品对应的样品ID，样品号，项目名称，下限，上限，标准名，标准号
        DataTable dtSelSt = new TMisMonitorTaskItemLogic().SelectStandard_byTask(strTaskID, mItemTypeID);
        string strSTs = "";
        for (int p = 0; p < dtSelSt.Rows.Count; p++)
        {
            string strSt = dtSelSt.Rows[p]["STANDARD_CODE"].ToString();
            if (strSTs.IndexOf(strSt) < 0)
            {
                strSTs += (strSTs.Length > 0 ? "," : "") + strSt;
            }
        }
        DataTable dtItemAndParent = new ReportBuildLogic().SelectItemAndParentItem(strTaskID, mItemTypeID);

        //所有限值增加方法，1、排口只能加父项目和子项目种的一种。
        //2、排口通过项目ID和标准交叉的项目ID对应，不论父项目或者子项目。
        //3、如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取。
        //4、标准交叉，不支持排口增加父项目，而交叉里要按子项目进行交叉
        string[] arrSt = strSTs.Split(',');
        for (int j = 0; j < arrSt.Length; j++)
        {
            if (objTableTemp.Rows.Count <= 0)
                break;
            DataRow dr = objTableTemp.NewRow();
            dr[0] = arrSt[j];
            dr[1] = arrSt[j];
            for (int k = 2; k < objTableTemp.Columns.Count; k++)
            {
                bool isHas = false;
                for (int h = 0; h < dtSelSt.Rows.Count; h++)
                {
                    if (objTableTemp.Columns[k].ColumnName.Contains(dtSelSt.Rows[h]["ITEM_NAME"].ToString()) && dtSelSt.Rows[h]["STANDARD_CODE"].ToString() == arrSt[j])
                    {
                        string strU = dtSelSt.Rows[h]["ST_UPPER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                        string strL = dtSelSt.Rows[h]["ST_LOWER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                        string strUL = "-";
                        if (strU.Length > 0 && strL.Length > 0)
                        {
                            strUL = strL + "~" + strU;
                        }
                        else if (strU.Length > 0)
                        {
                            strUL = strU;
                        }
                        else if (strL.Length > 0)
                        {
                            strUL = strL;
                        }
                        dr[k] = strUL;
                        isHas = true;
                    }
                }

                //如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取。
                if (!isHas)
                {
                    #region 如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取
                    for (int r = 0; r < dtItemAndParent.Rows.Count; r++)
                    {
                        if (dtItemAndParent.Rows[r]["SubName"].ToString() == objTableTemp.Columns[k].ColumnName)
                        {
                            string strParentName = dtItemAndParent.Rows[r]["ParentName"].ToString();
                            if (strParentName.Length > 0)
                            {
                                for (int q = 0; q < dtSelSt.Rows.Count; q++)
                                {
                                    if (dtSelSt.Rows[q]["ITEM_NAME"].ToString() == strParentName && dtSelSt.Rows[q]["STANDARD_CODE"].ToString() == arrSt[j])
                                    {
                                        string strU = dtSelSt.Rows[q]["ST_UPPER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                                        string strL = dtSelSt.Rows[q]["ST_LOWER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                                        string strUL = "-";
                                        if (strU.Length > 0 && strL.Length > 0)
                                        {
                                            strUL = strL + "~" + strU;
                                        }
                                        else if (strU.Length > 0)
                                        {
                                            strUL = strU;
                                        }
                                        else if (strL.Length > 0)
                                        {
                                            strUL = strL;
                                        }
                                        dr[k] = strUL;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            objTableTemp.Rows.Add(dr);
        }
    }

    #region 得到水、固标准限值
    private void Get_Water_Soild_St_Standard(ref DataTable objTableTemp)
    {
        //取得样品对应的样品ID，样品号，项目名称，下限，上限，标准名，标准号
        DataTable dtSelSt = new TMisMonitorTaskItemLogic().SelectStandard_byTask(mTaskID, mItemTypeID);
        string strSTs = "";
        for (int p = 0; p < dtSelSt.Rows.Count; p++)
        {
            string strSt = dtSelSt.Rows[p]["STANDARD_CODE"].ToString();
            if (strSTs.IndexOf(strSt) < 0)
            {
                strSTs += (strSTs.Length > 0 ? "," : "") + strSt;
            }
        }
        DataTable dtItemAndParent = new ReportBuildLogic().SelectItemAndParentItem(mTaskID, mItemTypeID);

        //所有限值增加方法，1、排口只能加父项目和子项目种的一种。
        //2、排口通过项目ID和标准交叉的项目ID对应，不论父项目或者子项目。
        //3、如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取。
        //4、标准交叉，不支持排口增加父项目，而交叉里要按子项目进行交叉
        string[] arrSt = strSTs.Split(',');
        for (int j = 0; j < arrSt.Length; j++)
        {
            DataRow dr = objTableTemp.NewRow();
            dr[0] = arrSt[j];
            dr[1] = arrSt[j];
            for (int k = 2; k < objTableTemp.Columns.Count; k++)
            {
                bool isHas = false;
                for (int h = 0; h < dtSelSt.Rows.Count; h++)
                {
                    if (dtSelSt.Rows[h]["ITEM_NAME"].ToString() == objTableTemp.Columns[k].ColumnName && dtSelSt.Rows[h]["STANDARD_CODE"].ToString() == arrSt[j])
                    {
                        string strU = dtSelSt.Rows[h]["ST_UPPER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                        string strL = dtSelSt.Rows[h]["ST_LOWER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                        string strUL = "-";
                        if (strU.Length > 0 && strL.Length > 0)
                        {
                            strUL = strL + "~" + strU;
                        }
                        else if (strU.Length > 0)
                        {
                            strUL = strU;
                        }
                        else if (strL.Length > 0)
                        {
                            strUL = strL;
                        }
                        dr[k] = strUL;
                        isHas = true;
                    }
                }

                //如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取。
                if (!isHas)
                {
                    for (int r = 0; r < dtItemAndParent.Rows.Count; r++)
                    {
                        if (dtItemAndParent.Rows[r]["SubName"].ToString() == objTableTemp.Columns[k].ColumnName)
                        {
                            string strParentName = dtItemAndParent.Rows[r]["ParentName"].ToString();
                            if (strParentName.Length > 0)
                            {
                                for (int q = 0; q < dtSelSt.Rows.Count; q++)
                                {
                                    if (dtSelSt.Rows[q]["ITEM_NAME"].ToString() == strParentName && dtSelSt.Rows[q]["STANDARD_CODE"].ToString() == arrSt[j])
                                    {
                                        string strU = dtSelSt.Rows[q]["ST_UPPER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                                        string strL = dtSelSt.Rows[q]["ST_LOWER"].ToString().Replace("<=", "≤").Replace(">=", "≥").Replace("<", "").Replace(">", "");
                                        string strUL = "-";
                                        if (strU.Length > 0 && strL.Length > 0)
                                        {
                                            strUL = strL + "~" + strU;
                                        }
                                        else if (strU.Length > 0)
                                        {
                                            strUL = strU;
                                        }
                                        else if (strL.Length > 0)
                                        {
                                            strUL = strL;
                                        }
                                        dr[k] = strUL;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            objTableTemp.Rows.Add(dr);
        }
    }
    #endregion

    #region 构造噪声监测结果表
    /// <summary>
    /// 构造噪声监测结果表
    /// </summary>
    /// <param name="TestInfo"></param>
    private void Get_NoiseST_Report_Result(ref DataTable TestInfo)
    {
        int iRowCount = 8;

        #region 给datatable 增加 column
        #region add 噪声点位
        for (int i = 1; i <= iRowCount; i++)
        {
            TestInfo.Columns.Add("NOISE_OUTLET" + i.ToString());
            TestInfo.Columns.Add("NOISE_OUTLET_INFO" + i.ToString());
        }
        #endregion

        #region add 项目 结果
        for (int i = 1; i <= iRowCount; i++)
        {
            //昼实测  Leq（昼间实测值）
            TestInfo.Columns.Add("NOISE_DS_RESULT" + i.ToString());
            //昼背景  Leq（昼间背景值）
            TestInfo.Columns.Add("NOISE_DB_RESULT" + i.ToString());
            //昼修正  Leq（昼间修正值）
            TestInfo.Columns.Add("NOISE_DX_RESULT" + i.ToString());
            //夜实测  Leq（夜间实测值）
            TestInfo.Columns.Add("NOISE_NS_RESULT" + i.ToString());
            //夜背景  Leq（夜间背景值）
            TestInfo.Columns.Add("NOISE_NB_RESULT" + i.ToString());
            //夜修正 Leq（夜间修正值）
            TestInfo.Columns.Add("NOISE_NX_RESULT" + i.ToString());
        }
        #endregion

        #region add 标准限值
        for (int i = 1; i <= iRowCount; i++)
        {
            //昼标   Leq（昼间修正值）
            TestInfo.Columns.Add("NOISE_D_ST" + i.ToString());
            //夜标   Leq（夜间修正值）
            TestInfo.Columns.Add("NOISE_N_ST" + i.ToString());
        }
        #endregion
        #endregion

        #region 得到噪声所有项目id和名称
        TBaseItemInfoVo tiiv = new TBaseItemInfoVo();
        tiiv.IS_DEL = "0";
        tiiv.MONITOR_ID = mItemTypeID;
        List<TBaseItemInfoVo> alItem = new TBaseItemInfoLogic().SelectByObject(tiiv, 0, 0);
        #endregion

        #region 添加点位名称、排口备注、标准限值、监测项目结果值
        //获得监测任务所有的样品和对应的点位
        DataTable dt = new ReportBuildLogic().GetSampleInfoSourceByTask(mTaskID);
        //最终生成的点位表
        DataTable dtNew = dt.Clone();

        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            DataRow dr = dt.Rows[i] as DataRow;
            // 监测子任务
            TMisMonitorSubtaskVo ctiv = new TMisMonitorSubtaskLogic().Details(dr["SUBTASK_ID"].ToString());
            if (ctiv.MONITOR_ID == mItemTypeID)
            {
                dtNew.Rows.Add(dr.ItemArray);
            }
        }
        for (int i = 0; i < dtNew.Rows.Count; i++)
        {
            int idx = i + 1;
            DataRow dr = dtNew.Rows[i] as DataRow;

            #region 添加点位名称 NOISE_OUTLET
            if (TestInfo.Rows.Count > 0)
            {
                TestInfo.Rows[0]["NOISE_OUTLET" + idx.ToString()] = dr["POINT_NAME"].ToString();
            }
            else
            {
                TestInfo.Rows.Add(TestInfo.NewRow());
            }
            #endregion

            #region 添加排口备注  OUTLET_INFO
            //该值在排口编辑或者临时排口增加两个界面设置
            TestInfo.Rows[0]["NOISE_OUTLET_INFO" + idx.ToString()] = dr["DESCRIPTION"].ToString();
            #endregion
            //采样结果表
            TMisMonitorResultVo sarv = new TMisMonitorResultVo();
            sarv.SAMPLE_ID = dr["ID"].ToString();
            List<TMisMonitorResultVo> alsarv = new TMisMonitorResultLogic().SelectByObject(sarv, 0, 0);

            #region 添加标准限值
            //所有限值增加方法，1、排口只能加父项目和子项目种的一种。
            //2、排口通过项目ID和标准交叉的项目ID对应，不论父项目或者子项目。
            //3、如果是子项目，而且该子项目在排口标准交叉中无子项目ID，按父项目ID取。
            //4、标准交叉，不支持排口增加父项目，而交叉里要按子项目进行交叉
            //5、噪声例外，如果是噪声的父项目，直接取父项目的标准交叉限值，按“,”分隔分别取 leq昼间修正值、leq夜间修正值
            //6、这里由于是定制模板，默认认定，该项目是"厂界噪声"等父项目的子项目，子项目包括 "昼、夜"的 "实测值"、"背景值"、"修正值", 共6个子项目
            //6-1、所以，必须通过样品号，取得该样品的所有项目ID，然后取得其父项目ID，并根据排口ID，取得标准交叉的限值
            //6-2、噪声"厂界噪声"等父项目的子项目，在标准管理中有标准限值的为 "昼修正值"、"夜修正值"
            //6-3、噪声"厂界噪声"等父项目的子项目，在标准交叉中有标准限值的为 "厂界噪声"等父项目，显示为 "昼修正值限值,夜修正值限值"
            if (alsarv.Count > 0)
            {
                string strItemID = ((TMisMonitorResultVo)alsarv[0]).ITEM_ID;
                TBaseItemInfoVo tiivChild = new TBaseItemInfoLogic().Details(strItemID);

                bool isNeedParent = false;

                TMisMonitorTaskItemVo oidvSrh1 = new TMisMonitorTaskItemVo();
                oidvSrh1.TASK_POINT_ID = dr["POINT_ID"].ToString();
                oidvSrh1.ITEM_ID = strItemID;
                List<TMisMonitorTaskItemVo> alOidv11 = new TMisMonitorTaskItemLogic().SelectByObject(oidvSrh1, 0, 0);
                if (alOidv11.Count > 0)
                {
                    string strStValue = ((TMisMonitorTaskItemVo)alOidv11[0]).ST_UPPER;
                    if (strStValue.Length > 0)
                    {
                        if (strStValue.IndexOf(",") >= 0)
                        {
                            string[] arrStValue = strStValue.Split(',');
                            TestInfo.Rows[0]["NOISE_D_ST" + idx.ToString()] = arrStValue[0].Replace("<", "").Replace(">", "").Replace("=", "");
                            TestInfo.Rows[0]["NOISE_N_ST" + idx.ToString()] = arrStValue[1].Replace("<", "").Replace(">", "").Replace("=", "");
                        }
                        else
                        {
                            TestInfo.Rows[0]["NOISE_D_ST" + idx.ToString()] = strStValue.Replace("<", "").Replace(">", "").Replace("=", "");
                            TestInfo.Rows[0]["NOISE_N_ST" + idx.ToString()] = strStValue.Replace("<", "").Replace(">", "").Replace("=", "");
                        }
                    }
                    else
                    {
                        isNeedParent = true;
                    }
                }
                else
                {
                    isNeedParent = true;
                }
                //项目子项对象
                TBaseItemSubItemVo objSubitem = new TBaseItemSubItemLogic().getParentIDByItem(strItemID);
                if (isNeedParent && objSubitem.PARENT_ITEM_ID.Length > 0)
                {
                    TMisMonitorTaskItemVo oidvSrh = new TMisMonitorTaskItemVo();
                    oidvSrh.TASK_POINT_ID = dr["POINT_ID"].ToString();
                    oidvSrh.ITEM_ID = objSubitem.PARENT_ITEM_ID;
                    List<TMisMonitorTaskItemVo> alOidv = new TMisMonitorTaskItemLogic().SelectByObject(oidvSrh, 0, 0);
                    if (alOidv.Count > 0)
                    {
                        string strStValue = ((TMisMonitorTaskItemVo)alOidv[0]).ST_UPPER;
                        if (strStValue.IndexOf(",") >= 0)
                        {
                            string[] arrStValue = strStValue.Split(',');
                            TestInfo.Rows[0]["NOISE_D_ST" + idx.ToString()] = arrStValue[0].Replace("<", "").Replace(">", "").Replace("=", "");
                            TestInfo.Rows[0]["NOISE_N_ST" + idx.ToString()] = arrStValue[1].Replace("<", "").Replace(">", "").Replace("=", "");
                        }
                        else
                        {
                            TestInfo.Rows[0]["NOISE_D_ST" + idx.ToString()] = strStValue.Replace("<", "").Replace(">", "").Replace("=", "");
                            TestInfo.Rows[0]["NOISE_N_ST" + idx.ToString()] = strStValue.Replace("<", "").Replace(">", "").Replace("=", "");
                        }
                    }
                }
            }
            #endregion

            #region 添加结果值
            for (int k = 0; k < alsarv.Count; k++)
            {
                TMisMonitorResultVo tiivResult = alsarv[k] as TMisMonitorResultVo;
                string strItemName = GetItemNameByID(alItem, tiivResult.ITEM_ID);
                if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("昼间实测值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_DS_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("昼间背景值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_DB_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("昼间修正值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_DX_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("夜间实测值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_NS_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("夜间背景值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_NB_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("Leq") >= 0 && strItemName.IndexOf("夜间修正值") >= 0)
                {
                    TestInfo.Rows[0]["NOISE_NX_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                }
                else if (strItemName.IndexOf("实测值") < 0 && strItemName.IndexOf("修正值") < 0 && strItemName.IndexOf("背景值") < 0)
                {
                    if (strItemName.IndexOf("昼间") >= 0)
                    {
                        TestInfo.Rows[0]["NOISE_DS_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                    }
                    else if (strItemName.IndexOf("夜间") >= 0)
                    {
                        TestInfo.Rows[0]["NOISE_NS_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                    }
                    else
                    {
                        TestInfo.Rows[0]["NOISE_DS_RESULT" + idx.ToString()] = tiivResult.ITEM_RESULT;
                    }
                }
            }
            #endregion
        }
        if (dtNew.Rows.Count < 8)
        {
            for (int p = dtNew.Rows.Count; p < 8; p++)
            {
                int idx = p + 1;
                if (TestInfo.Rows.Count == 0)
                {
                    TestInfo.Rows.Add(TestInfo.NewRow().ItemArray);
                }

                TestInfo.Rows[0]["NOISE_OUTLET" + idx.ToString()] = "————";
                TestInfo.Rows[0]["NOISE_OUTLET_INFO" + idx.ToString()] = "--";

                TestInfo.Rows[0]["NOISE_D_ST" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_N_ST" + idx.ToString()] = "--";

                TestInfo.Rows[0]["NOISE_DS_RESULT" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_DB_RESULT" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_DX_RESULT" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_NS_RESULT" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_NB_RESULT" + idx.ToString()] = "--";
                TestInfo.Rows[0]["NOISE_NX_RESULT" + idx.ToString()] = "--";
            }
        }
        #endregion
    }
    #endregion

    #region 取得项目ID对应的Name
    private string GetItemNameByID(List<TBaseItemInfoVo> alItem, string strItemID)
    {
        for (int i = 0; i < alItem.Count; i++)
        {
            TBaseItemInfoVo tiiv = alItem[i] as TBaseItemInfoVo;
            if (tiiv.ID == strItemID)
                return tiiv.ITEM_NAME;
        }
        return "";
    }
    #endregion

    #region 构造噪声监测结论

    #endregion

    /// <summary>
    /// 获取指定项目的标准上下限
    /// </summary>
    /// <param name="strItemName"></param>
    /// <param name="strUp"></param>
    /// <param name="strDown"></param>
    protected void GetStandardValueByItemName(string strItemName, string strItemType, ref string strUp, ref string strDown)
    {
        //项目ID
        string strItemID = new TBaseItemInfoLogic().Details(new TBaseItemInfoVo() { ITEM_NAME = strItemName, MONITOR_ID = strItemType, IS_DEL = "0" }).ID;
        TBaseEvaluationConItemVo EvaluationVo = new TBaseEvaluationConItemVo();
        EvaluationVo.ITEM_ID = strItemID;
        EvaluationVo.MONITOR_ID = strItemType;
        EvaluationVo.IS_DEL = "0";
        EvaluationVo = new TBaseEvaluationConItemLogic().Details(EvaluationVo);
        strUp = EvaluationVo.DISCHARGE_UPPER;
        strDown = EvaluationVo.DISCHARGE_LOWER;
    }

    #region 获得测点示意图路径
    protected string GetPointImgPathBySubTask(string strSubTaskID)
    {
        //服务器路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();

        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = strSubTaskID;
        TOaAttVo.BUSINESS_TYPE = "SubTask";
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        //文件存储位置
        string strfolderPath = TOaAttVoTemp.UPLOAD_PATH;
        //完整路径
        return mastPath + "\\" + strfolderPath;

    }
    #endregion
}