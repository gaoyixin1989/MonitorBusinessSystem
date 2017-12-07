<%@ WebService Language="C#" Class="SW" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Collections.Generic;

using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.ValueObject.Channels.OA.Message;
using i3.BusinessLogic.Channels.OA.Message;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class SW  : System.Web.Services.WebService {

    [WebMethod]
    public string selLst(string strUserID, string strIfConfirm, string strTaskName, string strSendUser, string strSendTimeBegin, string strSendTimeEnd, int intPageIndex, int intPageSize)
    {
        string where = "1=1";
        if (strTaskName.Length > 0)
            where += " and TASK_NAME like '%" + strTaskName.Trim() + "%'";
        if (strSendUser.Length > 0)
            where += " and SEND_USER like '%" + strSendUser.Trim() + "%'";
        if (strSendTimeBegin.Length > 0)
            where += " and SEND_DATE >= '" + strSendTimeBegin.Trim() + " 00:00:00'";
        if (strSendTimeEnd.Length > 0)
            where += " and SEND_DATE <= '" + strSendTimeEnd.Trim() + " 23:59:59'";

        DataTable dt = new TOaSwInfoLogic().SelectHandleTable_Mobile(strUserID, strIfConfirm, where, intPageIndex, intPageSize);
        int intTotalCount = new TOaSwInfoLogic().SelectHandleTable_Count(strUserID, strIfConfirm, where);

        string strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string GetSwAllInfo(string strID)
    {
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);
        
        string strReturn = "";
        strReturn = "{";
        //------基本信息
        strReturn += GetSwInfo(strID);
        strReturn += ",";

        //------可用的功能按钮
        strReturn += "'fun':'01|',";
        //------是否最后一环节
        strReturn += "'IsEnd':'" + (objVo.SW_STATUS == "5" ? "1" : "0") + "',";

        //------是否可以修改或者查看阅办人员，及已选择的阅办人员
        string strIfCanRead = "0";
        if (objVo.SW_STATUS == "2")
            strIfCanRead = "1";
        if (objVo.SW_STATUS == "3")
            strIfCanRead = "2";
        if (objVo.SW_STATUS == "4")
            strIfCanRead = "2";
        if (objVo.SW_STATUS == "5")
            strIfCanRead = "2";
        strReturn += "'IfCanRead':'" + strIfCanRead + "',";
        strReturn += "'ReadUser':" + getReadUser_HasSel(strID) + ",";

        //------是否可以修改或者查看办结人员，及已选择的办结人员
        string strIfCanMake = "0";
        if (objVo.SW_STATUS == "2")
            strIfCanMake = "1";
        if (objVo.SW_STATUS == "3")
            strIfCanMake = "1";
        if (objVo.SW_STATUS == "4")
            strIfCanMake = "2";
        if (objVo.SW_STATUS == "5")
            strIfCanMake = "2";
        strReturn += "'IfCanMake':'" + strIfCanMake + "',";
        strReturn += "'MakeUser':" + getMakeUser_HasSel(strID) + ",";

        //------是否可以修改或者查看阅办人员，及已选择的阅办人员
        string strIfSelNextUser = "0";
        if (objVo.SW_STATUS == "0" || objVo.SW_STATUS == "1" ||  objVo.SW_STATUS == "4")//登记、办公室主任、科室办结，都要有下一环节选择人
        {
            strIfSelNextUser = "1";
        }
        if (objVo.SW_STATUS == "2" || objVo.SW_STATUS == "5")//站长批办、归档，都无下一环节选择人
        {
            strIfSelNextUser = "0";
        }
        if (objVo.SW_STATUS == "3")//分管领导阅办，需根据是否有办结人，然后判定是否有下一环节选择人
        {
            DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
            string strMakeUserIDs = "";
            string strReadUserIDs = "";
            if (dt.Rows.Count > 0)
            {
                strReadUserIDs = dt.Rows[0]["ReadUserID"].ToString();
                strMakeUserIDs = dt.Rows[0]["MakeUserID"].ToString();
            }
            string NewStatus = getNewStatus(objVo.SW_STATUS, strReadUserIDs, strMakeUserIDs);
            if (NewStatus == "1" || NewStatus == "2" || NewStatus == "5")
            {
                strIfSelNextUser = "1";
            }
        }
        strReturn += "'IfSelNextUser':'" + strIfSelNextUser + "'";
        
        strReturn += "}";

        return strReturn;
    }

    [WebMethod]
    public string getFunction(string strID)
    {
        return "{'fun':'01|'}";
    }

    [WebMethod]
    public string isEndStep(string strID)
    {
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);
        return objVo.SW_STATUS == "5" ? "1" : "0";
    }

    [WebMethod]
    public string setTaskConfirm(string strID, string strUserID)
    {
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);
        bool bIsSuccess = new TOaSwHandleLogic().Edit(new TOaSwHandleVo {IS_OK="2" }, new TOaSwHandleVo { SW_ID = strID, SW_PLAN_ID = strUserID, SW_HANDER = objVo.SW_STATUS });

        return  bIsSuccess ? "1" : "0";
    }

    [WebMethod]
    public string needSelNextUser(string strID)
    {
        string strJson = "0";
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);
        
        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        string strMakeUserIDs = "";
        string strReadUserIDs = "";
        if (dt.Rows.Count > 0)
        {
            strReadUserIDs = dt.Rows[0]["ReadUserID"].ToString();
            strMakeUserIDs = dt.Rows[0]["MakeUserID"].ToString();
        }
        string NewStatus = getNewStatus(objVo.SW_STATUS, strReadUserIDs, strMakeUserIDs);
        if (NewStatus == "1" || NewStatus == "2" || NewStatus == "5")
        {
            strJson = "1";
        }

        return strJson;
    }
    
    [WebMethod]
    public string getUser(string strID)
    {
        string strJson = "";
        //TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);
        
        DataTable dtUser = new TSysUserLogic().SelectByTable(new TSysUserVo { IS_DEL = "0", IS_HIDE = "0", IS_USE = "1" });
        DataTable dtPost = new TSysPostLogic().SelectByTable(new TSysPostVo { IS_DEL = "0", IS_HIDE = "0" });
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo ());
        DataTable dtDept = new TSysDictLogic().SelectByTable(new TSysDictVo { DICT_TYPE = "dept" });

        string strReturn = "[";
        //if (objVo.SW_STATUS == "0" || objVo.SW_STATUS == "1" || objVo.SW_STATUS == "5")
        //{
            foreach (DataRow drUser in dtUser.Rows)
            {
                string strUserId = drUser["ID"].ToString();
                string strUserDept = getUserDept(strUserId, dtPost, dtUserPost, dtDept);
                strReturn += "{";
                strReturn += "'UserID':'" + strUserId + "',";
                strReturn += "'UserName':'" + drUser["REAL_NAME"].ToString() + "',";
                strReturn += "'Dept':'" + strUserDept + "'";
                strReturn += "},";
            }
            strReturn = strReturn.TrimEnd(',');
        //}
        strReturn += "]";

        return strReturn;
    }

    [WebMethod]
    public string getReadUser_HasSel(string strID)
    {
        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        string strReturn = "[]";
        if (dt.Rows.Count > 0)
        {
            string strReadUserIDs = dt.Rows[0]["ReadUserID"].ToString();
            string strReadUserNames = dt.Rows[0]["ReadUserName"].ToString();
            strReturn = "[";
            if (strReadUserIDs.Trim().Length > 0)
            {
                string[] arrReadUserIDs = strReadUserIDs.Split(',');
                string[] arrReadUserNames = strReadUserNames.Split(',');
                for (int i = 0; i < arrReadUserIDs.Length; i++)
                {
                    strReturn += "{";
                    strReturn += "'UserID':'" + arrReadUserIDs[i] + "',";
                    strReturn += "'UserName':'" + arrReadUserNames[i] + "'";
                    strReturn += "},";
                }
                strReturn = strReturn.TrimEnd(',');
            }
            strReturn += "]";
        }
        return strReturn;
    }

    [WebMethod]
    public string getMakeUser_HasSel(string strID)
    {
        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        string strReturn = "[]";
        if (dt.Rows.Count > 0)
        {
            string strMakeUserIDs = dt.Rows[0]["MakeUserID"].ToString();
            string strMakeUserNames = dt.Rows[0]["MakeUserName"].ToString();
            strReturn = "[";
            if (strMakeUserIDs.Trim().Length > 0)
            {
                string[] arrReadUserIDs = strMakeUserIDs.Split(',');
                string[] arrReadUserNames = strMakeUserNames.Split(',');
                for (int i = 0; i < arrReadUserIDs.Length; i++)
                {
                    strReturn += "{";
                    strReturn += "'UserID':'" + arrReadUserIDs[i] + "',";
                    strReturn += "'UserName':'" + arrReadUserNames[i] + "'";
                    strReturn += "},";
                }
                strReturn = strReturn.TrimEnd(',');
            }
            strReturn += "]";
        }
        return strReturn;
    }

    [WebMethod]
    public string ifCanReadUser(string strID)
    {
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);

        string strReturn = "0";
        
        if (objVo.SW_STATUS == "2")
        {
            strReturn = "1";
        }
        if (objVo.SW_STATUS == "3")
        {
            strReturn = "2";
        }

        return strReturn;
    }
    [WebMethod]
    public string ifCanMakeUser(string strID)
    {
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);

        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        string strReadUserIDs = "";
        if (dt.Rows.Count > 0)
        {
            strReadUserIDs = dt.Rows[0]["ReadUserID"].ToString();
        }

        string strReturn = "0";

        if (objVo.SW_STATUS == "2")
        {
            strReturn = "1";
        }
        if (objVo.SW_STATUS == "3" && strReadUserIDs.Length >0)//阅办环节，如果无办结人员设置，不允许增加；同理手机端发现有办结人员设置，则不允许清空
        {
            strReturn = "1";
        }

        return strReturn;
    }

    [WebMethod]
    public string getInfo(string strID)
    {
        string strReturn = GetSwInfo(strID);
        if (strReturn.Length > 0)
        {
            strReturn = "{" + strReturn + "}";
        }

        return strReturn;
    }

    private string GetSwInfo(string strID)
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = strID;
        TOaAttVo.BUSINESS_TYPE = "SWFile";
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string fileName = TOaAttVoTemp.ATTACH_NAME + TOaAttVoTemp.ATTACH_TYPE;//客户端保存的文件名 
        string filePath = mastPath + '\\' + TOaAttVoTemp.UPLOAD_PATH;
        string strHasAtt = "1";
        string strMobileFileName = "";
        if (File.Exists(filePath) == false)
        {
            strHasAtt = "0";
        }
        else
        {
            string serverPath = HttpRuntime.AppDomainAppPath + "TempFile";
            DateTime dtNow = System.DateTime.Now;
            string strDatetimeStr = dtNow.Year.ToString() + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');
            strMobileFileName = "SW" + strID + strDatetimeStr + TOaAttVoTemp.ATTACH_TYPE;
            try
            {
                File.Copy(filePath, serverPath + "\\" + strMobileFileName, true);
            }
            catch (Exception ex) { }
        }

        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        string strReturn = "";
        if (dt.Rows.Count > 0)
        {
            strReturn += "'原号':'" + dt.Rows[0]["FROM_CODE"].ToString() + "',";
            strReturn += "'来文机关':'" + dt.Rows[0]["SW_FROM"].ToString() + "',";
            strReturn += "'编号':'" + dt.Rows[0]["SW_CODE"].ToString() + "',";
            strReturn += "'登记人':'" + dt.Rows[0]["SW_SIGN_ID"].ToString() + "',";
            strReturn += "'登记日期':'" + dt.Rows[0]["SW_SIGN_DATE"].ToString() + "',";
            strReturn += "'标题':'" + dt.Rows[0]["SW_TITLE"].ToString() + "',";
            strReturn += "'主题词':'" + dt.Rows[0]["SUBJECT_WORD"].ToString() + "',";
            strReturn += "'HasAtt':'" + strHasAtt + "',";
            strReturn += "'AttFileName':'" + fileName + "',";
            strReturn += "'AttFilePath':'TempFile/" + strMobileFileName + "'";
        }

        return strReturn;
    }

    [WebMethod]
    public string getAppInfo(string strID)
    {
        string strReturn = "[";
        DataTable dtTemp = new TOaSwHandleLogic().SelectByTable(new TOaSwHandleVo { SW_ID = strID });
        DataRow[] drTemp;

        //主任阅示信息
        drTemp = dtTemp.Select("SW_HANDER='1' and IS_OK='1'");
        for (int i = 0; i < drTemp.Length; i++)
        {
            strReturn += "{";
            strReturn += "'step':'办公室主任阅示',";
            strReturn += "'办理意见':'" + drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "',";
            strReturn += "'办理人':'" + new TSysUserLogic().Details(drTemp[i]["SW_PLAN_ID"].ToString()).REAL_NAME + "',";
            strReturn += "'办理时间':'" + drTemp[i]["SW_PLAN_DATE"].ToString() + "'";
            strReturn += "},";
        }

        //站长阅示信息
        drTemp = dtTemp.Select("SW_HANDER='2' and IS_OK='1'");
        for (int i = 0; i < drTemp.Length; i++)
        {
            strReturn += "{";
            strReturn += "'step':'主要领导批示',";
            strReturn += "'办理意见':'" + drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "',";
            strReturn += "'办理人':'" + new TSysUserLogic().Details(drTemp[i]["SW_PLAN_ID"].ToString()).REAL_NAME + "',";
            strReturn += "'办理时间':'" + drTemp[i]["SW_PLAN_DATE"].ToString() + "'";
            strReturn += "},";
        }

        //分管阅办信息
        drTemp = dtTemp.Select("SW_HANDER='3' and IS_OK='1'");
        for (int i = 0; i < drTemp.Length; i++)
        {
            strReturn += "{";
            strReturn += "'step':'分管领导阅办',";
            strReturn += "'办理意见':'" + drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "',";
            strReturn += "'办理人':'" + new TSysUserLogic().Details(drTemp[i]["SW_PLAN_ID"].ToString()).REAL_NAME + "',";
            strReturn += "'办理时间':'" + drTemp[i]["SW_PLAN_DATE"].ToString() + "'";
            strReturn += "},";
        }

        //科室办结信息
        drTemp = dtTemp.Select("SW_HANDER='4' and IS_OK='1'");
        for (int i = 0; i < drTemp.Length; i++)
        {
            strReturn += "{";
            strReturn += "'step':'科室办结',";
            strReturn += "'办理意见':'" + drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "',";
            strReturn += "'办理人':'" + new TSysUserLogic().Details(drTemp[i]["SW_PLAN_ID"].ToString()).REAL_NAME + "',";
            strReturn += "'办理时间':'" + drTemp[i]["SW_PLAN_DATE"].ToString() + "'";
            strReturn += "},";
        }

        strReturn = strReturn.TrimEnd(',');

        strReturn += "]";

        return strReturn;
    }

    [WebMethod]
    public string sendTask(string strID, string strUserID, string strAppInfo, string strSendUserId, string Reader, string Maker)
    {
        bool b = false;
        TOaSwInfoVo objVo = new TOaSwInfoLogic().Details(strID);

        string Status = objVo.SW_STATUS;
        string NewStatus = getNewStatus(Status, Reader, Maker);

        if (NewStatus == "6")
        {
            b = new TOaSwInfoLogic().FinishSW(strID, NewStatus, Status, strUserID);
        }
        else
        {
            b = new TOaSwInfoLogic().SendSW(strID, NewStatus, Status, strUserID, strAppInfo, strSendUserId, Reader, Maker, "t_oa_SWHandleID");
        }

        if (b)
            return "1";
        else
            return "0";
    }

    private string getNewStatus(string Status, string Reader, string Maker)
    {
        string NewStatus = "";
        
        //主任阅示——》站长阅示
        if (Status == "1")
        {
            NewStatus = "2";
        }
        //站长阅示——》分管阅办
        if (Status == "2" && Reader.Trim() != "")
        {
            NewStatus = "3";
        }
        //站长阅示——》科室办结
        if (Status == "2" && Reader.Trim() == "" && Maker.Trim() != "")
        {
            NewStatus = "4";
        }
        //站长阅示——》科室办结
        if (Status == "2" && Reader.Trim() == "" && Maker.Trim() == "")
        {
            NewStatus = "5";
        }
        //分管阅办——》科室办结
        if (Status == "3" && Maker.Trim() != "")
        {
            NewStatus = "4";
        }
        //分管阅办——》文件完结
        if (Status == "3" && Maker.Trim() == "")
        {
            NewStatus = "5";
        }
        //科室办结——》文件完结
        if (Status == "4")
        {
            NewStatus = "5";
        }

        return NewStatus;
    }

    private string getUserDept(string strUserID, DataTable dtPost, DataTable dtUserPost, DataTable dtDept)
    {
        string strUserDept = "";

        DataRow[] drUserPostS = dtUserPost.Select("USER_ID='" + strUserID + "'");
        foreach (DataRow drUserPost in drUserPostS)
        {
            DataRow[] drPostS = dtPost.Select("ID='" + drUserPost["POST_ID"].ToString() + "'");
            foreach (DataRow drPost in drPostS)
            {
                DataRow[] drDeptS = dtDept.Select("DICT_CODE='" + drPost["POST_DEPT_ID"].ToString() + "'");
                foreach (DataRow drDept in drDeptS)
                {
                    strUserDept += (strUserDept.Length > 0 ? "，" : "") + drDept["DICT_TEXT"].ToString();
                } 
            }
        }

        return strUserDept;
    }

    private string getNextSatus(string strSW_STATUS)
    {
        string strReturn = "";
        switch (strSW_STATUS)
        {
            case "0":
                strReturn= "1";
                break;
            case "1"://办公室主任阅示
                strReturn = "2";
                break;
            case "2"://主要领导批示
                strReturn = "3";
                break;
            case "3"://分管领导阅办
                strReturn = "4";
                break;
            case "4"://科室办结
                strReturn = "5";
                break;
            case "5":
                strReturn = "6";
                break;
            default :
                strReturn = "";
                break;
        }
        return strReturn;
    }
}