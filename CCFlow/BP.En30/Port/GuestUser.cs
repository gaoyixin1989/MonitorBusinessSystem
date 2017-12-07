
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using System.Web;
using System.Data;
using BP.En;
using BP.DA;
using System.Configuration;
using BP.Port;
using BP.Pub;
using BP.Sys;


namespace BP.Web
{
    /// <summary>
    /// User ��ժҪ˵����
    /// </summary>
    public class GuestUser
    {
        /// <summary>
        /// ͨ�õĵ�½
        /// </summary>
        /// <param name="guestNo"></param>
        /// <param name="guestName"></param>
        /// <param name="lang"></param>
        /// <param name="isRememberMe"></param>
        public static void SignInOfGener(string guestNo, string guestName, string lang, bool isRememberMe)
        {
            SignInOfGener(guestNo, guestName, "deptNo", "deptName", lang, isRememberMe);
        }
        /// <summary>
        /// ͨ�õĵ�½
        /// </summary>
        /// <param name="guestNo">�ͻ����</param>
        /// <param name="guestName">�ͻ�����</param>
        /// <param name="deptNo">���ű��</param>
        /// <param name="deptName">��������</param>
        /// <param name="lang">����</param>
        /// <param name="isRememberMe">�Ƿ������</param>
        public static void SignInOfGener(string guestNo, string guestName, string deptNo, 
            string deptName,string lang, bool isRememberMe)
        {
            BP.Port.Emp em = new Emp();
            em.No = "Guest";
            if (em.RetrieveFromDBSources() == 0)
            {
                em.Name = "����";
                em.Insert();
            }

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //if (SystemConfig.IsBSsystem)
            //    BP.Sys.UserLog.AddLog("SignIn", em.No, "��¼", BP.Sys.Glo.Request.UserHostAddress);


            //��¼������Ϣ.
            GuestUser.No = guestNo;
            GuestUser.Name = guestName;
            GuestUser.DeptNo = deptNo;
            GuestUser.DeptName = deptName;

            //��¼�ڲ��ͻ���Ϣ.
            WebUser.No = em.No;
            WebUser.Name = em.Name;
            WebUser.FK_Dept = em.FK_Dept;
            WebUser.FK_DeptName = em.FK_DeptText;
            WebUser.HisDepts = null;
            WebUser.HisStations = null;
            WebUser.SysLang = lang;
            if (BP.Sys.SystemConfig.IsBSsystem)
            {
                // Guest  ��Ϣ.
                HttpCookie cookie = new HttpCookie("CCSGuest");
                //cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Expires = DateTime.Now.AddDays(2);
                cookie.Values.Add("GuestNo", guestNo);
                cookie.Values.Add("GuestName", HttpUtility.UrlEncode(guestName));
                cookie.Values.Add("DeptNo", deptNo);
                cookie.Values.Add("DeptName", HttpUtility.UrlEncode(deptName));
                System.Web.HttpContext.Current.Response.AppendCookie(cookie); //���뵽�Ự��



                HttpCookie cookie2 = new HttpCookie("CCS");
                cookie2.Expires = DateTime.Now.AddDays(2);
                // Guest  ��Ϣ.
                cookie2.Values.Add("GuestNo", guestNo);
                cookie2.Values.Add("GuestName", HttpUtility.UrlEncode(guestName));

                cookie2.Values.Add("DeptNo", deptNo);
                cookie2.Values.Add("DeptName", HttpUtility.UrlEncode(deptName));

                cookie2.Values.Add("No", "Guest");
                cookie2.Values.Add("Name", HttpUtility.UrlEncode(em.Name));

                if (isRememberMe)
                    cookie2.Values.Add("IsRememberMe", "1");
                else
                    cookie2.Values.Add("IsRememberMe", "0");

                cookie2.Values.Add("FK_Dept", em.FK_Dept);
                cookie2.Values.Add("FK_DeptName", HttpUtility.UrlEncode(em.FK_DeptText));

                cookie2.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);
                cookie2.Values.Add("SID", System.Web.HttpContext.Current.Session.SessionID);

                cookie2.Values.Add("Lang", lang);
                cookie2.Values.Add("Style", "0");
                cookie2.Values.Add("Auth", ""); //��Ȩ��.
                System.Web.HttpContext.Current.Response.AppendCookie(cookie2);
            }
        }
      
        #region ��̬����
        /// <summary>
        /// ͨ��key,ȡ��session.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="isNullAsVal">�����Null, ���ص�ֵ.</param>
        /// <returns></returns>
        public static string GetSessionByKey(string key, string isNullAsVal)
        {
            if (IsBSMode)
            {
                string str = System.Web.HttpContext.Current.Session[key] as string;
                if (string.IsNullOrEmpty(str))
                    str = isNullAsVal;
                return str;
            }
            else
            {
                if (BP.Port.Win.Current.Session[key] == null || BP.Port.Win.Current.Session[key].ToString() == "")
                {
                    BP.Port.Win.Current.Session[key] = isNullAsVal;
                    return isNullAsVal;
                }
                else
                    return (string)BP.Port.Win.Current.Session[key];
            }
        }
        public static object GetObjByKey(string key)
        {
            if (IsBSMode)
            {
                return System.Web.HttpContext.Current.Session[key];
            }
            else
            {
                return BP.Port.Win.Current.Session[key];
            }
        }
        #endregion

        /// <summary>
        /// �ǲ���b/s ����ģʽ��
        /// </summary>
        protected static bool IsBSMode
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                    return false;
                else
                    return true;
            }
        }
        public static object GetSessionByKey(string key, Object defaultObjVal)
        {
            if (IsBSMode)
            {
                if (System.Web.HttpContext.Current.Session[key] == null)
                    return defaultObjVal;
                else
                    return System.Web.HttpContext.Current.Session[key];
            }
            else
            {
                if (BP.Port.Win.Current.Session[key] == null)
                    return defaultObjVal;
                else
                    return BP.Port.Win.Current.Session[key];
            }
        }
        /// <summary>
        /// ����session
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="val">ֵ</param>
        public static void SetSessionByKey(string key, object val)
        {
            if (val == null)
                return;
            if (IsBSMode)
                System.Web.HttpContext.Current.Session[key] = val;
            else
                BP.Port.Win.Current.SetSession(key, val);
        }
        /// <summary>
        /// �˻�
        /// </summary>
        public static void Exit()
        {
            if (IsBSMode == false)
            {
                try
                {
                    string token = WebUser.Token;
                    System.Web.HttpContext.Current.Response.Cookies.Clear();
                    BP.Sys.Glo.Request.Cookies.Clear();


                    HttpCookie cookie = new HttpCookie("CCS", string.Empty);
                    cookie.Expires = DateTime.Now.AddDays(2);
                    cookie.Values.Add("No", string.Empty);
                    cookie.Values.Add("Name", string.Empty);
                    // 2013.06.07 H
                    cookie.Values.Add("Pass", string.Empty); 
                    cookie.Values.Add("IsRememberMe", "0");
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    WebUser.Token = token;
                    BP.Port.Win.Current.Session.Clear();


                    // Guest  ��Ϣ.
                    cookie = new HttpCookie("CCSGuest");
                    cookie.Expires = DateTime.Now.AddDays(2);
                    cookie.Values.Add("GuestNo", string.Empty);
                    cookie.Values.Add("GuestName", string.Empty);
                    cookie.Values.Add("DeptNo", string.Empty);
                    cookie.Values.Add("DeptName", string.Empty);
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie); //���뵽�Ự��

                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    string token = WebUser.Token;
                    System.Web.HttpContext.Current.Response.Cookies.Clear();
                    BP.Sys.Glo.Request.Cookies.Clear();
                 
                    System.Web.HttpContext.Current.Session.Clear();

                    HttpCookie cookie = new HttpCookie("CCS", string.Empty);
                    cookie.Expires = DateTime.Now.AddDays(2);
                    cookie.Values.Add("No", string.Empty);
                    cookie.Values.Add("Name", string.Empty);
                    // 2013.06.07 H
                    cookie.Values.Add("Pass",string.Empty);
                    cookie.Values.Add("IsRememberMe", "0");
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    WebUser.Token = token;

                    // Guest  ��Ϣ.
                    cookie = new HttpCookie("CCSGuest");
                    cookie.Expires = DateTime.Now.AddDays(2);
                    cookie.Values.Add("GuestNo", string.Empty);
                    cookie.Values.Add("GuestName", string.Empty);
                    cookie.Values.Add("DeptNo", string.Empty);
                    cookie.Values.Add("DeptName", string.Empty);
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie); //���뵽�Ự��

                }
                catch
                {
                }
            }
        }
        public static string GetValFromCookie(string valKey, string isNullAsVal, bool isChinese)
        {
            if (IsBSMode == false)
                return BP.Port.Win.Current.GetSessionStr(valKey, isNullAsVal);
            string key = "CCSGuest";
            HttpCookie hc = BP.Sys.Glo.Request.Cookies[key];
            if (hc == null)
                return null;
            try
            {
                string val = null;
                if (isChinese)
                    val = HttpUtility.UrlDecode(hc[valKey]);
                else
                    val = hc.Values[valKey];

                if (string.IsNullOrEmpty(val))
                    return isNullAsVal;
                return val;
            }
            catch
            {
                return isNullAsVal;
            }
            throw new Exception("@err-001 ��½��Ϣ��ʧ��");
        }
        /// <summary>
        /// ���
        /// </summary>
        public static string No
        {
            get
            {
                return GetValFromCookie("GuestNo", null, false);
                string no = null; // GetSessionByKey("No", null);
                if (no == null || no == "")
                {
                    if (IsBSMode == false)
                        return "admin";

                    string key = "CCSGuest";
                    HttpCookie hc = BP.Sys.Glo.Request.Cookies[key];
                    if (hc == null)
                        return null;

                    if (hc.Values["GuestNo"] != null)
                    {
                       GuestUser.No = hc["GuestNo"];
                       GuestUser.Name = hc["GuestName"];
                       return hc.Values["GuestNo"];
                    }
                    throw new Exception("@err-002 Guest ��½��Ϣ��ʧ��");
                }
                return no;
            }
            set
            {
                SetSessionByKey("GuestNo", value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string Name
        {
            get
            {
                string val = GetValFromCookie("GuestName", null, true);
                if (val == null)
                    throw new Exception("@err-001 GuestName ��½��Ϣ��ʧ��");
                return val;
            }
            set
            {
                SetSessionByKey("GuestName", value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public static string DeptNo
        {
            get
            {
                string val = GetValFromCookie("DeptNo", null, true);
                if (val == null)
                    throw new Exception("@err-003 DeptNo ��½��Ϣ��ʧ��");
                return val;
            }
            set
            {
                SetSessionByKey("DeptNo", value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public static string DeptName
        {
            get
            {
                string val = GetValFromCookie("DeptName", null, true);
                if (val == null)
                    throw new Exception("@err-002 DeptName ��½��Ϣ��ʧ��");
                return val;
            }
            set
            {
                SetSessionByKey("DeptName", value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public static string Style
        {
            get
            {
                return GetSessionByKey("Style", "0");
            }
            set
            {
                SetSessionByKey("Style", value);
            }
        }
    }
}
