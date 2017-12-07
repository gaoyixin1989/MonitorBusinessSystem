<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WebService  : System.Web.Services.WebService {

    
    [WebMethod]
    public string MobileUserLogin(string strUserName, string strPWD,string strMac)
    {
        //登录
        TSysUserVo userInfo = new TSysUserVo();
        userInfo.USER_NAME = strUserName;
        userInfo.USER_PWD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strPWD, "MD5"); ;
        TSysUserLogic uil = new TSysUserLogic();
        DataTable dt = uil.SelectByTable(userInfo);

        string strResultMsg = "";
        string strErrMsg = "";

        if (dt.TableName != "")
        {
            string[] strMessage = dt.TableName.Replace("\n", "").Split(':');
            if (strMessage.Length == 3)
                strErrMsg = "连接异常!\\n" + "异常代码: " + strMessage[0] + "\\n" + "异常原因:" + strMessage[1] + strMessage[2];
            else if (strMessage.Length == 2)
                strErrMsg = "连接异常!\\n" + "异常代码: " + strMessage[0] + "\\n" + "异常原因:" + strMessage[1];
            else
                strErrMsg = "连接异常!\\n" + "异常原因: " + dt.TableName.Replace("\n", "");
            strResultMsg = "{'Success':'0','returnValue':'" + strErrMsg + "'}";
            return strResultMsg;
        }

        if (dt.Rows.Count > 0)
        {
            //登录成功,返回到指定地址
            if (dt.Rows[0][TSysUserVo.IS_USE_FIELD].ToString() == "0")
            {
                strResultMsg = "{'Success':'0','returnValue':'3'}";//用户已锁定
            }
            else if (dt.Rows[0][TSysUserVo.IS_DEL_FIELD].ToString() == "1")
            {
                strResultMsg = "{'Success':'0','returnValue':'4'}";//用户已删除
            }
            else
            {
                string strIfMac = "1";
                if (dt.Rows[0][TSysUserVo.IOS_MAC_FIELD].ToString().Trim().Length + dt.Rows[0][TSysUserVo.ANDROID_MAC_FIELD].ToString().Trim().Length == 0)
                {
                    strIfMac = "0";
                }
                else
                {
                    if (dt.Rows[0][TSysUserVo.IOS_MAC_FIELD].ToString().ToLower().Trim() == strMac.ToLower().Trim())
                    {
                        if (dt.Rows[0][TSysUserVo.IF_IOS_FIELD].ToString() == "1")
                            strIfMac = "ok";
                        else
                            strIfMac = "2";
                    }
                    if (dt.Rows[0][TSysUserVo.ANDROID_MAC_FIELD].ToString().ToLower().Trim() == strMac.ToLower().Trim())
                    {
                        if (dt.Rows[0][TSysUserVo.IF_ANDROID_FIELD].ToString() == "1")
                            strIfMac = "ok";
                        else
                            if (strIfMac == "1")
                                strIfMac = "2";
                    }
                }

                //if (strIfMac == "ok")
                //{
                    string strUserID = dt.Rows[0][TSysUserVo.ID_FIELD].ToString();
                    string strRealName = dt.Rows[0][TSysUserVo.REAL_NAME_FIELD].ToString();
                    strResultMsg = "{'Success':'1','returnValue':'" + strUserID + "','RealName':'" + strRealName + "','right':" + GetUserRight(strUserID) + "}";
                //}
                //else
                //{
                //    strResultMsg = "{'Success':'0','returnValue':'" + strIfMac + "'}";
                //}
            }
        }
        else
        {
            strResultMsg = "{'Success':'0','returnValue':'5'}";//用户不存在
        }

        return strResultMsg;
    }

    [WebMethod]
    public string GetUserRight(string strUserID)
    {
        DataTable dtWf=getWfDataTable();
        DataTable dtMenu = getMenuDataTable(strUserID);

        return getRight(dtWf, dtMenu, strUserID);
    }

    private DataTable getWfDataTable()
    {
        string strWfID = "'FW','PARTPLAN','WorkSubmit'";
        return new TWfSettingTaskLogic().SelectByTable_byWFID(strWfID);
    }

    private string getRight(DataTable dtWf,DataTable dtMenu, string strUserID)
    {
        string strRe = "[";

        string strMenu = "WF1,质量负责人审核,01,AnalysisResultQcManagerAudit.aspx;";
        strMenu += "WF2,技术负责人审核,02,AnalysisResultQcManagerAudit.aspx;";
        strMenu += "WF3,任务督办,03,TaskSrh.aspx;";
        strMenu += "WF4,历史数据,04,TaskSrh.aspx;";
        strMenu += "WF5,数据统计,05,TaskSrh.aspx;";
        strMenu += "SW,收文,06,SWHandleList.aspx";
        string[] arrMenu = strMenu.Split(';');
        for (int i = 0; i < arrMenu.Length; i++)
        {
            if (arrMenu[i].Length > 0)
            {
                string[] arrarrMenu = arrMenu[i].Split(',');
                string strRight = ifMenuRight(dtMenu, arrarrMenu[0], arrarrMenu[1], arrarrMenu[2], arrarrMenu[3]);
                if (strRight.Length > 0)
                    strRe += (strRe.Length > 1 ? "," : "") + strRight;
            }
        }
        
        string strPostID = GetUserPostID(strUserID);

        string strWF = "FW,发文,07;WorkSubmit,物料采购,08";
        string[] arrWf = strWF.Split(';');
        for (int i = 0; i < arrWf.Length; i++)
        {
            if (arrWf[i].Length > 0)
            {
                string[] arrarrWf = arrWf[i].Split(',');
                string strRight = ifWfRight(dtWf, strUserID, strPostID, arrarrWf[0], arrarrWf[1], arrarrWf[2]);
                if (strRight.Length > 0)
                    strRe += (strRe.Length > 1 ? "," : "") + strRight;
            }
        }

        strRe += "]";

        return strRe;
    }

    private string GetUserPostID(string strUserID)
    {
        TSysUserPostVo tUserPost = new TSysUserPostVo();
        tUserPost.USER_ID = strUserID;
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(tUserPost);

        string strPostID = "";

        for (int i = 0; i < dtUserPost.Rows.Count; i++)
        {
            strPostID += (strPostID.Length > 0 ? "," : "") + dtUserPost.Rows[i]["POST_ID"].ToString();
        }

        return strPostID;
    }

    private string ifWfRight(DataTable dt, string strUserID, string strPostID, string strWFID,string strWFCation, string strWFOrder)
    {
        string strSql = "WF_ID='" + strWFID + "'";
        if (strWFID=="WorkSubmit")//WorkSubmit,物料采购,08;PARTPLAN,领料单,09
            strSql = "WF_ID in ('WorkSubmit','PARTPLAN')";
        DataRow[] drs = dt.Select(strSql);
        if (drs.Length == 0)
            return "";

        bool isHasRight = false;
        for (int m = 0; m < drs.Length; m++)
        {
            DataRow dr = drs[m];
            string strOPER_VALUE = dr["OPER_VALUE"].ToString();
            if (strOPER_VALUE.Length == 0)
                continue;
            
            string[] arrOPER_VALUE = strOPER_VALUE.Split('|');
            if (dr["OPER_TYPE"].ToString() == "01")//01.按人,02.按职位
            {
                for (int j = 0; j < arrOPER_VALUE.Length; j++)
                {
                    if (arrOPER_VALUE[j] == strUserID)
                        isHasRight = true;
                }
            }
            else
            {
                string[] arrPostIDs = strPostID.Split(',');
                for (int j = 0; j < arrOPER_VALUE.Length; j++)
                {
                    for (int k = 0; k < arrPostIDs.Length; k++)
                    {
                        if (arrPostIDs[k].Length > 0 && arrOPER_VALUE[j] == arrPostIDs[k])
                            isHasRight = true;
                    }
                }
            }
        }
        
        if (isHasRight)
        {
            return "{'FunID':'" + strWFID + "','Fun':'" + strWFCation + "','Fun_Order':'" + strWFOrder + "'}";
        }
        else
        {
            return "";
        }
    }
    
    private DataTable getMenuDataTable(string strUserID)
    {
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.IS_SHORTCUT = "0";
        menuvo.MENU_TYPE = "Menu";
        menuvo.IS_HIDE = "0";
        return new TSysMenuLogic().GetMenuByUserID(menuvo, strUserID);
    }

    private string ifMenuRight(DataTable dtMenu,string strMenuCode, string strMenuName, string strFun_Order,string strMenuUrl)
    {
        DataRow[] drs = dtMenu.Select("MENU_URL Like '%" + strMenuUrl + "%'");
        

        if (drs.Length>0)
        {
            return "{'FunID':'" + strMenuCode + "','Fun':'" + strMenuName + "','Fun_Order':'" + strFun_Order + "'}";
        }
        else
        {
            return "";
        }
    }
}