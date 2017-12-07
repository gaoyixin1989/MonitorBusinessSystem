using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;

using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;

namespace i3.BusinessLogic.Channels.OA.Message
{
    /// <summary>
    /// 功能：手机短信发送
    /// 创建日期：2013-7-11
    /// 创建人：潘德军
    /// </summary>
    public class SendMobileMsg 
    {

        public string strConnection = ConfigurationManager.ConnectionStrings["SMS"].ToString();

        public SendMobileMsg()
	    {
	    }

        /// <summary>
        /// 自动发送 手机短信
        /// </summary>
        /// <param name="strMESSAGE_CONTENT">短消息内容</param>
        /// <param name="strSendUserID">发送人id</param>
        /// <param name="strAccUsers">接收人ID串，用逗号“,”分隔</param>
        /// <param name="isAtOnce">是否立即发送</param>
        /// <param name="strSEND_DATE">定期发送的时间，yyyy-MM-dd HH:mm:ss</param>
        /// <param name="strErrMsg">返回错误信息</param>
        /// <returns>成功与否</returns>
        public bool AutoSenMobilMsg(string strMESSAGE_CONTENT, string strSendUserID,
         string strAccUsers, bool isAtOnce, string strSEND_DATE, ref string strErrMsg)
        {
            // 发手机短信
            SendMobilMessage(strMESSAGE_CONTENT, strSendUserID, strAccUsers, isAtOnce, strSEND_DATE);

            strErrMsg = "短信发送成功！";
            return true;
        }

        /// <summary>
        /// 发手机短信
        /// </summary>
        /// <param name="strMESSAGE_CONTENT">短消息内容</param>
        /// <param name="strSendUserID">发送人id</param>
        /// <param name="strAccUsers">接收人ID串，用逗号“,”分隔</param>
        /// <param name="isAtOnce">是否立即发送</param>
        /// <param name="strSEND_DATE">定期发送的时间，yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public void SendMobilMessage(string strMESSAGE_CONTENT, string strSendUserID, string strAccUsers, bool isAtOnce, string strSEND_DATE)
        {
            SMS.Db.connectionstring = strConnection;

            DataTable dtUser = new TSysUserLogic().SelectByTable(new TSysUserVo { IS_DEL = "0", IS_HIDE = "0", IS_USE = "1" });

            DataRow[] drSenderS = dtUser.Select("id='" + strSendUserID + "'");
            string strSendUser = "";
            if (drSenderS.Length > 0)
            {
                strSendUser = drSenderS[0]["REAL_NAME"].ToString();
            }

            if (strAccUsers.Length == 0)
                return;

            string[] arrAccUsers = strAccUsers.Split(',');
            if (arrAccUsers.Length > 0)
            {
                for (int i = 0; i < arrAccUsers.Length; i++)
                {
                    if (arrAccUsers[i].Length > 0)
                    {
                        SMS.SmsVo sv = new SMS.SmsVo();

                        DataRow[] drAcceptS = dtUser.Select("id='" + arrAccUsers[i] + "'");
                        string strAcceptUser = "";
                        string strPhoneNumber = "";
                        if (drAcceptS.Length > 0)
                        {
                            strAcceptUser = drAcceptS[0]["REAL_NAME"].ToString();
                            strPhoneNumber = drAcceptS[0]["PHONE_MOBILE"].ToString();
                        }

                        if (!isAtOnce)//定时发送
                        {
                            sv.sendtime = strSEND_DATE;
                        }
                        sv.content = strMESSAGE_CONTENT;
                        sv.receiver = strPhoneNumber;
                        if (strPhoneNumber.Length > 0)
                        {
                            if (new SMS.SmsManager().IsDbConnect())
                            {
                                new SMS.SmsManager().SendSmsMessage(sv);
                            }
                        }
                    }
                }
            }
        }
    }
    
}
