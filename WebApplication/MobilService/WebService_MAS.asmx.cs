using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.Item;
using System.Data;
using System.Text.RegularExpressions;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.ValueObject.Channels.Base.Item;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

namespace WebApplication.MobilService
{
    /// <summary>
    /// WebService_MAS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService_MAS : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string strBase64)
        {
            byte[] bytes = Convert.FromBase64String(strBase64);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            Image img = (Image)binFormatter.Deserialize(memStream);


            return "Hello World";
        }

        [WebMethod]
        public string Img(string strBase64)
        {
            //string str = @"";
            //byte[] bytes = Convert.FromBase64String(str);
            //MemoryStream memStream = new MemoryStream(bytes);
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //Image img = (Image)binFormatter.Deserialize(memStream);


            //FileStream fs = new FileStream("d:\\php.txt", FileMode.Open);
            //StreamReader m_streamReader = new StreamReader(fs); 
            //m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin); 
            ////string arry = ""; 
            //string strLine = m_streamReader.ReadLine(); 
            
            //m_streamReader.Close();
            //m_streamReader.Dispose(); 
            //fs.Close(); 
            //fs.Dispose();


            //string fileText = File.ReadAllText("d:\\php.txt"); 
            //byte[] arrByteFile = Encoding.UTF8.GetBytes(fileText);
            byte[] bytes = Convert.FromBase64String(strBase64);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            Image img = (Image)binFormatter.Deserialize(memStream);

            


            return "Hello World!";
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public int uploadResume()
        {
            string strImage = @"iVBORw0KGgoAAAANSUhEUgAAACkAAAAnCAMAAACltJG0AAAAD1BMVEX/////AAD0mAAAoekAxCAi
                                    laxRAAAABXRSTlMA/////xzQJlIAAAAnSURBVDjLY2AYpoARGQD5TMhgVOWoygEGzMgAyGdBBqMq
                                    R1UOJwAApEMGm6Q1FpkAAAAASUVORK5CYIIA
                                    ";
            int tag = 0;
            FileStream out1 = null;
            byte[] bs = Convert.FromBase64String(strImage);

            //获取主文件路径
            string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
            //获取业务Id
            string strBusinessId = "9999";
            //获取业务类型
            string strBusinessType = "PointPoto";
            //获取完整文件名称
            string strFullName = strBusinessId + ".png";
            
            //获取文件扩展名称
            string strExtendName = strFullName.Substring(strFullName.LastIndexOf("."));
            string strSerialNumber = GetSerialNumber("attFileId");
            //文件夹路径
            string strfolderPath = strBusinessType + "\\" + DateTime.Now.ToString("yyyyMMdd");
            //新命名的文件名称
            string strNewFileName = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + strSerialNumber + strExtendName;
            //上传的完整路径
            string strResultPath = mastPath + "\\" + strfolderPath + "\\" + strNewFileName;
            //开始上传附件
            try
            {
                //判断文件夹是否存在，如果不存在则创建
                if (Directory.Exists(mastPath + "\\" + strfolderPath) == false)
                    Directory.CreateDirectory(mastPath + "\\" + strfolderPath);
                
                //判断原来是否已经上传过文件，如果有的话则获取原来已经上传的文件路径
                TOaAttVo TOaAttVo = new TOaAttVo();
                TOaAttVo.BUSINESS_TYPE = strBusinessType;
                TOaAttVo.BUSINESS_ID = strBusinessId;
                //TOaAttVo.ATTACH_NAME = this.ATTACH_NAME.Text.Trim();
                DataTable objTable = new TOaAttLogic().SelectByTable(TOaAttVo);
                if (objTable.Rows.Count > 0)
                {
                    //如果存在记录
                    //获取该记录的ID
                    string strId = objTable.Rows[0]["ID"].ToString();
                    //获取原来文件的路径
                    string strOldFilePath = objTable.Rows[0]["UPLOAD_PATH"].ToString();
                    //如果存在的话，删除原来的文件
                    if (File.Exists(mastPath + "\\" + strOldFilePath))
                        File.Delete(mastPath + "\\" + strOldFilePath);
                    //将新的信息写入数据库
                    TOaAttVo TOaAttVoTemp = new TOaAttVo();
                    TOaAttVoTemp.ID = strId;
                    TOaAttVoTemp.ATTACH_NAME = "点位图";
                    TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                    TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                    TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                    TOaAttVoTemp.UPLOAD_PERSON = "";
                    TOaAttVoTemp.DESCRIPTION = "点位图";
                    TOaAttVoTemp.REMARKS = bs.Length + "KB";//文件的大小

                    new TOaAttLogic().Edit(TOaAttVoTemp);
                }
                else
                {
                    //如果不存在记录
                    TOaAttVo TOaAttVoTemp = new TOaAttVo();
                    TOaAttVoTemp.ID = strSerialNumber;
                    TOaAttVoTemp.BUSINESS_ID = strBusinessId;
                    TOaAttVoTemp.BUSINESS_TYPE = strBusinessType;
                    TOaAttVoTemp.ATTACH_NAME = "点位图";
                    TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                    TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                    TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                    TOaAttVoTemp.UPLOAD_PERSON = "";
                    TOaAttVoTemp.DESCRIPTION = "点位图";
                    TOaAttVoTemp.REMARKS = bs.Length + "KB";//文件的大小

                    new TOaAttLogic().Create(TOaAttVoTemp);
                }

                if (tag == 0)
                {
                    out1 = new FileStream(strResultPath, FileMode.CreateNew, FileAccess.Write);
                }
                else
                {
                    out1 = new FileStream(strResultPath, FileMode.Append, FileAccess.Write);
                }

                out1.Write(bs, 0, bs.Length);

                if (out1 != null)
                {
                    try
                    {
                        out1.Close();
                    }
                    catch (IOException e)
                    {
                        // TODO Auto-generated catch block 

                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return 1;
        } 



        [WebMethod]
        public string getSubTaskInfo(string workID, string strUser)
        {
            string strJson = "";

            var identification = CCFlowFacade.GetFlowIdentification(strUser, long.Parse(workID));

            TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(identification);
            TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubtaskVo.TASK_ID);

            strJson += "'任务编号':'" + objTaskVo.TICKET_NUM + "','监测类别':'" + new TBaseMonitorTypeInfoLogic().Details(objSubtaskVo.MONITOR_ID).MONITOR_TYPE_NAME + "'";

            TMisMonitorSampleInfoVo objSampleVo = new TMisMonitorSampleInfoVo();
            objSampleVo.SUBTASK_ID = objSubtaskVo.ID;
            List<TMisMonitorSampleInfoVo> listSample = new List<TMisMonitorSampleInfoVo>();
            listSample = new TMisMonitorSampleInfoLogic().SelectByObject(objSampleVo, 0, 0);

            strJson += ",'监测点位':[";

            for (int i = 0; i < listSample.Count; i++)
            {
                if (i == 0)
                    strJson += "{";
                else
                    strJson += ",{";

                strJson += "'点位名称':'" + listSample[i].SAMPLE_NAME + "','样品编号':'" + listSample[i].SAMPLE_CODE + "','条码':'" + listSample[i].SAMPLE_BARCODE + "'";
                strJson += ",'监测项目':[";

                TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                objResultVo.SAMPLE_ID = listSample[i].ID;
                List<TMisMonitorResultVo> listResult = new TMisMonitorResultLogic().SelectByObject(objResultVo, 0, 0);
                for (int j = 0; j < listResult.Count; j++)
                {
                    if (j == 0)
                        strJson += "{";
                    else
                        strJson += ",{";

                    strJson += "'项目名称':'" + new TBaseItemInfoLogic().Details(listResult[j].ITEM_ID).ITEM_NAME + "','现场项目':'" + new TBaseItemInfoLogic().Details(listResult[j].ITEM_ID).IS_SAMPLEDEPT + "'";

                    strJson += "}";
                }

                strJson += "]}";
            }

            strJson += "]";
            strJson = "[{" + strJson + "}]";

            return strJson;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workID"></param>
        /// <param name="strUser"></param>
        /// <param name="JsonSubTask">[{'TICKET_NUM':任务编号,'SAMPLE_ASK_DATE':采样日期,'gerenal_way':方法依据,'app_code':仪器名称,'app_name':仪器编号,'001':天气,'002':气温,'004':气压,'003':湿度,'005':采样方式}]</param>
        /// <param name="JsonPoint">[{'SAMPLE_NAME':点位名称,'SAMPLE_COUNT':样品份数,'颜色':颜色,'气体':气体,'性状':性状,'流速':流速,'流量':流量,'photo':图片}]</param>
        /// <param name="JsonItem">[{'SAMPLE_NAME':点位名称,'ITEM_NAME':项目名称,'ITEM_RESULT':监测结果}]</param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateSubTaskInfo(string workID, string strUser, string JsonSubTask, string JsonPoint, string JsonItem)
        {
            string strResult = "false";
            var identification = CCFlowFacade.GetFlowIdentification(strUser, long.Parse(workID));

            TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(identification);
            TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubtaskVo.TASK_ID);

            DataTable dtSubTask = JSONToDataTable2(JsonSubTask);
            DataTable dtPoint = JSONToDataTable2(JsonPoint);
            DataTable dtItem = JSONToDataTable2(JsonItem);

            //更新任务的现状信息
            if (dtSubTask.Rows.Count > 0)
            {
                objSubtaskVo.SAMPLE_ASK_DATE = dtSubTask.Rows[0]["SAMPLE_ASK_DATE"].ToString();
                new TMisMonitorSubtaskLogic().Edit(objSubtaskVo);

                for (int i = 0; i < dtSubTask.Columns.Count; i++)
                {
                    if (dtSubTask.Columns[i].ColumnName != "TICKET_NUM" && dtSubTask.Columns[i].ColumnName != "SAMPLE_ASK_DATE")
                    {
                        TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();
                        objSampleSky.SUBTASK_ID = objSubtaskVo.ID;
                        objSampleSky.WEATHER_ITEM = dtSubTask.Columns[i].ColumnName;
                        objSampleSky = new TMisMonitorSampleSkyLogic().Details(objSampleSky);
                        if (objSampleSky.ID.Length > 0)
                        {
                            objSampleSky.WEATHER_INFO = dtSubTask.Rows[0][i].ToString();
                            new TMisMonitorSampleSkyLogic().Edit(objSampleSky);
                        }
                        else
                        {
                            objSampleSky.ID = GetSerialNumber("TMisMonitorSampleSky");
                            objSampleSky.SUBTASK_ID = objSubtaskVo.ID;
                            objSampleSky.WEATHER_ITEM = dtSubTask.Columns[i].ColumnName;
                            objSampleSky.WEATHER_INFO = dtSubTask.Rows[0][i].ToString();
                            new TMisMonitorSampleSkyLogic().Create(objSampleSky);
                        }
                    }
                }
                strResult = "true";
            }
            //更新监测点位信息
            if (dtPoint.Rows.Count > 0)
            {
                for (int i = 0; i < dtPoint.Rows.Count; i++)
                {
                    TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoVo();
                    objSampleInfoVo.SUBTASK_ID = objSubtaskVo.ID;
                    objSampleInfoVo.SAMPLE_NAME = dtPoint.Rows[i]["SAMPLE_NAME"].ToString();
                    objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(objSampleInfoVo);
                    if (objSampleInfoVo.ID.Length > 0)
                    {
                        objSampleInfoVo.SAMPLE_COUNT = dtPoint.Rows[i]["SAMPLE_COUNT"].ToString();
                        new TMisMonitorSampleInfoLogic().Edit(objSampleInfoVo);

                        #region 更新点位图
                        if (dtPoint.Rows[i]["photo"].ToString().Length > 0)
                        {
                            string strImage = dtPoint.Rows[i]["photo"].ToString();
                            int tag = 0;
                            FileStream out1 = null;
                            byte[] bs = Convert.FromBase64String(strImage);

                            //获取主文件路径
                            string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
                            //获取业务Id
                            string strBusinessId = objSampleInfoVo.ID;
                            //获取业务类型
                            string strBusinessType = "PointPoto";
                            //获取完整文件名称
                            string strFullName = strBusinessId + ".png";

                            //获取文件扩展名称
                            string strExtendName = strFullName.Substring(strFullName.LastIndexOf("."));
                            string strSerialNumber = GetSerialNumber("attFileId");
                            //文件夹路径
                            string strfolderPath = strBusinessType + "\\" + DateTime.Now.ToString("yyyyMMdd");
                            //新命名的文件名称
                            string strNewFileName = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + strSerialNumber + strExtendName;
                            //上传的完整路径
                            string strResultPath = mastPath + "\\" + strfolderPath + "\\" + strNewFileName;
                            //开始上传附件
                            try
                            {
                                //判断文件夹是否存在，如果不存在则创建
                                if (Directory.Exists(mastPath + "\\" + strfolderPath) == false)
                                    Directory.CreateDirectory(mastPath + "\\" + strfolderPath);

                                //判断原来是否已经上传过文件，如果有的话则获取原来已经上传的文件路径
                                TOaAttVo TOaAttVo = new TOaAttVo();
                                TOaAttVo.BUSINESS_TYPE = strBusinessType;
                                TOaAttVo.BUSINESS_ID = strBusinessId;
                                //TOaAttVo.ATTACH_NAME = this.ATTACH_NAME.Text.Trim();
                                DataTable objTable = new TOaAttLogic().SelectByTable(TOaAttVo);
                                if (objTable.Rows.Count > 0)
                                {
                                    //如果存在记录
                                    //获取该记录的ID
                                    string strId = objTable.Rows[0]["ID"].ToString();
                                    //获取原来文件的路径
                                    string strOldFilePath = objTable.Rows[0]["UPLOAD_PATH"].ToString();
                                    //如果存在的话，删除原来的文件
                                    if (File.Exists(mastPath + "\\" + strOldFilePath))
                                        File.Delete(mastPath + "\\" + strOldFilePath);
                                    //将新的信息写入数据库
                                    TOaAttVo TOaAttVoTemp = new TOaAttVo();
                                    TOaAttVoTemp.ID = strId;
                                    TOaAttVoTemp.ATTACH_NAME = "点位图";
                                    TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                                    TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                                    TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                                    TOaAttVoTemp.UPLOAD_PERSON = strUser;
                                    TOaAttVoTemp.DESCRIPTION = "点位图";
                                    TOaAttVoTemp.REMARKS = bs.Length + "KB";//文件的大小

                                    new TOaAttLogic().Edit(TOaAttVoTemp);
                                }
                                else
                                {
                                    //如果不存在记录
                                    TOaAttVo TOaAttVoTemp = new TOaAttVo();
                                    TOaAttVoTemp.ID = strSerialNumber;
                                    TOaAttVoTemp.BUSINESS_ID = strBusinessId;
                                    TOaAttVoTemp.BUSINESS_TYPE = strBusinessType;
                                    TOaAttVoTemp.ATTACH_NAME = "点位图";
                                    TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                                    TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                                    TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                                    TOaAttVoTemp.UPLOAD_PERSON = strUser;
                                    TOaAttVoTemp.DESCRIPTION = "点位图";
                                    TOaAttVoTemp.REMARKS = bs.Length + "KB";//文件的大小

                                    new TOaAttLogic().Create(TOaAttVoTemp);
                                }

                                if (tag == 0)
                                {
                                    out1 = new FileStream(strResultPath, FileMode.CreateNew, FileAccess.Write);
                                }
                                else
                                {
                                    out1 = new FileStream(strResultPath, FileMode.Append, FileAccess.Write);
                                }

                                out1.Write(bs, 0, bs.Length);

                                if (out1 != null)
                                {
                                    try
                                    {
                                        out1.Close();
                                    }
                                    catch (IOException e)
                                    {
                                        // TODO Auto-generated catch block 

                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        #endregion
                        for (int j = 0; j < dtPoint.Columns.Count; j++)
                        {
                            if (dtPoint.Columns[j].ColumnName != "SAMPLE_NAME" && dtPoint.Columns[j].ColumnName != "SAMPLE_COUNT" && dtPoint.Columns[j].ColumnName != "photo")
                            {
                                TBaseAttributeInfoVo objAttributeInfoVo = new TBaseAttributeInfoVo();
                                objAttributeInfoVo.IS_DEL = "0";
                                objAttributeInfoVo.ATTRIBUTE_NAME = dtPoint.Columns[j].ColumnName;
                                objAttributeInfoVo = new TBaseAttributeInfoLogic().Details(objAttributeInfoVo);
                                if (objAttributeInfoVo.ID.Length > 0)
                                {
                                    string Attribute_Code = objAttributeInfoVo.ID;

                                    TBaseAttrbuteValue3Vo objAttValue = new TBaseAttrbuteValue3Vo();
                                    objAttValue.OBJECT_ID = objSampleInfoVo.POINT_ID;
                                    objAttValue.ATTRBUTE_CODE = Attribute_Code;
                                    objAttValue.IS_DEL = "0";
                                    objAttValue = new TBaseAttrbuteValue3Logic().Details(objAttValue);
                                    if (objAttValue.ID == "")
                                    {
                                        objAttValue.ID = GetSerialNumber("t_base_attribute_value3_id");
                                        objAttValue.IS_DEL = "0";
                                        objAttValue.OBJECT_ID = objSampleInfoVo.POINT_ID;
                                        objAttValue.OBJECT_TYPE = objAttributeInfoVo.CONTROL_NAME;
                                        objAttValue.ATTRBUTE_CODE = Attribute_Code;
                                        objAttValue.ATTRBUTE_VALUE = dtPoint.Rows[i][dtPoint.Columns[j].ColumnName.ToString()].ToString();
                                        new TBaseAttrbuteValue3Logic().Create(objAttValue);
                                    }
                                    else
                                    {
                                        objAttValue.ATTRBUTE_VALUE = dtPoint.Rows[i][dtPoint.Columns[j].ColumnName.ToString()].ToString();
                                        new TBaseAttrbuteValue3Logic().Edit(objAttValue);
                                    }
                                }
                            }
                        }
                    }
                }
                strResult = "true";
            }
            //更新监测项目的信息
            if (dtItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoVo();
                    objSampleInfoVo.SUBTASK_ID = objSubtaskVo.ID;
                    objSampleInfoVo.SAMPLE_NAME = dtItem.Rows[i]["SAMPLE_NAME"].ToString();
                    objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(objSampleInfoVo);
                    if (objSampleInfoVo.ID.Length > 0)
                    {
                        TBaseItemInfoVo objItemInfoVo = new TBaseItemInfoVo();
                        objItemInfoVo.ITEM_NAME = dtItem.Rows[i]["ITEM_NAME"].ToString();
                        objItemInfoVo.IS_DEL = "0";
                        objItemInfoVo = new TBaseItemInfoLogic().Details(objItemInfoVo);

                        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                        objResultVo.SAMPLE_ID = objSampleInfoVo.ID;
                        objResultVo.ITEM_ID = objItemInfoVo.ID;
                        TMisMonitorResultVo objResultSetVo = new TMisMonitorResultVo();
                        objResultSetVo.ITEM_RESULT = dtItem.Rows[i]["ITEM_RESULT"].ToString();
                        new TMisMonitorResultLogic().Edit(objResultSetVo, objResultVo);
                    }
                }
                strResult = "true";
            }

            return strResult;
        }

        /// <summary>
        /// 把JSON字符串转换成DATATABLE。基于胡方扬的方法，但去除列名上的双引号
        /// create by 钟杰华
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable JSONToDataTable2(string json)
        {
            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                DataTable dt = JsonToDataTable(json);
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnName = dc.ColumnName.Replace("'", "");
                }
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        #region 将JSON 序列化为DataTable数据 Create By 胡方扬 2013-02-01
        public static DataTable JsonToDataTable(string strJson)
        {
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;            //去除表名              
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));
            //获取数据               
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');
                //创建表                   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0];
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容                   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        #endregion

        /// <summary>
        /// 功能描述：获取序号
        /// 创建人：　陈国迎
        /// 创建日期：2007-1-22
        /// </summary>
        /// <param name="strSerialType">类型</param>
        /// <returns>序号</returns>
        public static string GetSerialNumber(string strSerialType)
        {
            return new i3.BusinessLogic.Sys.Resource.TSysSerialLogic().GetSerialNumber(strSerialType);
        }
    }
}
