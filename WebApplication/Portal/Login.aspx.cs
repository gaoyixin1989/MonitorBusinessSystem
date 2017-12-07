using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using System.Web.UI.HtmlControls;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
/// <summary>
/// 功能描述：系统登陆页面
/// 创建日期：2011-04-12 14:40
/// 创建人  ：郑义
/// </summary>
public partial class Portal_Login : PageBase
{
    protected string copyright = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        TSysConfigVo objSysConfigVo = new TSysConfigVo();
        objSysConfigVo.CONFIG_CODE = "CopyRight";
        objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
        copyright = objSysConfigVo.REMARK;
        if (Request.QueryString["logout"] != null)
        {
            base.WriteLog(i3.ValueObject.ObjectBase.LogType.LogOut, LogInfo.UserInfo.ID, LogInfo.UserInfo.USER_NAME + "登出系统成功!");
            if (LogInfo.UserInfo.ID != "")
            {
                new i3.BusinessLogic.Sys.General.TSysUserStatusLogic().Delete(LogInfo.UserInfo.ID);
            }
            Session.Remove(KEY_CACHEOPERATOR);
        }
    }
    /// <summary>
    /// 错误登录次数
    /// </summary>
    public const int iMaxErrorTimes = 4;

    /// <summary>
    /// 连续错误登录此次累计的时间间隔（小时）
    /// </summary>
    public const int iMaxHour = 24;
    #region 函数
    /// <summary>
    /// 登陆主函数
    /// </summary>
    protected void LogIn()
    {
        //先隐藏
        lblCount.Visible = false;
        //登录
        TSysUserVo userInfo = new TSysUserVo();
        userInfo.USER_NAME = username.Text;
        userInfo.USER_PWD = ToMD5(password.Text);
        if (string.IsNullOrEmpty(username.Text))
        {
            Alert("请输入帐号");
            return;
        }
        if (string.IsNullOrEmpty(password.Text))
        {
            Alert("请输入密码");
            return;
        }
        TSysUserLogic uil = new TSysUserLogic();
        DataTable dt = uil.SelectByTable(userInfo);
        if (dt.TableName != "")
        {
            string[] strMessage = dt.TableName.Replace("\n", "").Split(':');
            if (strMessage.Length == 3)
                Alert("连接异常!\\n" + "异常代码: " + strMessage[0] + "\\n" + "异常原因:" + strMessage[1] + strMessage[2]);
            else if (strMessage.Length == 2)
                Alert("连接异常!\\n" + "异常代码: " + strMessage[0] + "\\n" + "异常原因:" + strMessage[1]);
            else
                Alert("连接异常!\\n" + "异常原因: " + dt.TableName.Replace("\n", ""));
            return;
        }
        if (dt.Rows.Count > 0)
        {//登录成功,返回到指定地址
            if (dt.Rows[0][TSysUserVo.IS_USE_FIELD].ToString() == "0")
            {
                //已经锁定,提示已锁定
                Alert("用户已锁定,请联系管理员处理!");
            }
            else if (dt.Rows[0][TSysUserVo.IS_DEL_FIELD].ToString() == "1")
            {
                //已经删除,提示已删除
                Alert("用户已删除,请联系管理员处理!");
            }
            else
            {
                //寄存变量
                TSysUserVo userInfoLogin = uil.Details(dt.Rows[0][TSysUserVo.ID_FIELD].ToString());
                UserLogInfo userLog = new UserLogInfo();
                userLog.UserInfo = userInfoLogin;
                userLog.ClientInfo = new LoginClientData();
                Session[KEY_CACHEOPERATOR] = userLog;
                LogInfo.UserInfo = userInfoLogin;
                //消除登录错误列表中的相关数据
                if (null != Application[KEY_USER_LOGIN_ERROR])
                {
                    List<UserLogError> loginErrorList = (List<UserLogError>)Application[KEY_USER_LOGIN_ERROR];
                    foreach (UserLogError ule in loginErrorList)
                    {
                        if (ule._userName == userInfo.USER_NAME)
                        {
                            loginErrorList.Remove(ule);
                            break;
                        }
                    }
                }
                userLog.ClientInfo.UserHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                base.WriteLog(i3.ValueObject.ObjectBase.LogType.LogIn, userLog.UserInfo.ID, userLog.UserInfo.USER_NAME + "登陆系统成功!");

                WebApplication.CCFlowFacade.UserLogin(userInfo.USER_NAME);
                Response.Redirect("IndexNew.aspx");

                //if (LogInfo.UserInfo.ID == "000000001")//系统管理员初始化配置
                //{
                //    if (GetIintRegionCode() == "" || GetIintUnitID() == "")
                //    {
                //        Response.Redirect("../Sys/SysInit.aspx");
                //    }
                //    else
                //        Response.Redirect("Index.aspx");
                //}
                //else
                //{
                //    Response.Redirect("Index.aspx");
                //}
            }
        }
        else
        {
            //纠错,记录登录错误次数
            List<UserLogError> loginErrorList;
            int iErrorTimes = 0;
            if (null == Application[KEY_USER_LOGIN_ERROR])
            {
                Application[KEY_USER_LOGIN_ERROR] = new List<UserLogError>();
            }

            loginErrorList = (List<UserLogError>)Application[KEY_USER_LOGIN_ERROR];
            bool bIsHave = false;
            bool bIsLock = false;
            foreach (UserLogError ule in loginErrorList)
            {
                if (ule._userName == userInfo.USER_NAME)
                {
                    //哨兵已发现此用户登录错误过
                    bIsHave = true;
                    //看是否时间已过24小时
                    if (((TimeSpan)(DateTime.Now - ule._loginStartTime)).TotalSeconds > (iMaxHour * 60 * 60))
                    {
                        //清空原始登录错误信息
                        loginErrorList.Remove(ule);
                        return;
                    }
                    //假如超过某此,要停掉此用户
                    if (ule._loginTimes >= iMaxErrorTimes)
                    {
                        //超过5次要被记录入数据库,并锁定用户
                        //userInfo.IS_USE = "1";//2014-02-12 取消该限制
                        userInfo.USER_PWD = "";
                        //userInfo.REMARK = DateTime.Now.ToString() + "登录失败次数超过" + iMaxErrorTimes + "次,系统自动锁定";//2014-02-12 取消该限制

                        TSysUserVo objTemp = new TSysUserVo();
                        objTemp.USER_NAME = userInfo.USER_NAME;
                        objTemp = new TSysUserLogic().Details(objTemp);
                        if (objTemp.ID != "000000001")
                            new TSysUserLogic().EditByName(userInfo);
                        //bIsLock = true;//2014-02-12 取消该限制
                        // 每超过1此,重新置零,防止重复刷日志
                        //ule._loginTimes = 0;
                    }
                    //要是提示超前了,下面两句话,对调下就行了
                    ule._loginTimes = ule._loginTimes + 1;
                    iErrorTimes = ule._loginTimes;

                }
            }
            //哨兵没有发现此用户以前登录错误过,现在记录入
            if (!bIsHave)
            {
                UserLogError ule = new UserLogError();
                ule._loginIP = "";
                ule._loginStartTime = DateTime.Now;
                ule._loginTimes = 1;
                ule._loginType = "登录";
                ule._userName = userInfo.USER_NAME;
                loginErrorList.Add(ule);
                iErrorTimes = ule._loginTimes;

            }
            //提示出错信息
            if (!bIsLock)
            {
                Alert("用户名或密码错误," + "您已经是有" + iErrorTimes.ToString() + "次登录错误了," + "如果连续错误5次系统将锁定此用户!");
            }
            else
            {
                Alert("您尝试次数过多,此用户已锁定,请联系管理员处理!");
            }

        }
    }
    #endregion

    

    #region 事件
    /// <summary>
    /// 登陆按钮单击事件
    /// </summary>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LogIn();
    }
    #endregion
}