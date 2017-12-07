using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    #region 用户登录
    /// <summary>
    /// 用户登录的信息
    /// </summary>
    public class UserLogInfo
    {
        public TSysUserVo UserInfo;
        public LoginClientData ClientInfo;
        public UserLogInfo(TSysUserVo userInfo, LoginClientData clientData)
        {
            UserInfo = new TSysUserVo();
            ClientInfo = new LoginClientData();
            UserInfo = userInfo;
            ClientInfo = clientData;
        }
        public UserLogInfo()
        {
            UserInfo = new TSysUserVo();
            ClientInfo = new LoginClientData();
        }
    }
    /// <summary>
    /// 客户端数据
    /// </summary>
    public struct LoginClientData
    {
        /// <summary>
        /// 用户客户机的IP地址
        /// </summary>
        public string UserHostAddress;

        /// <summary>
        /// 穿透防火墙获得用户的真正IP
        /// </summary>
        public string UserTrueAddress;

        /// <summary>
        /// 获得用户客户机的Mac地址
        /// </summary>
        public string UserHostMac;

        /// <summary>
        /// 用户客户机的用户名称
        /// </summary>
        public string UserHostName;

        /// <summary>
        /// 用户客户机的浏览器代理方
        /// </summary>
        public string UserAgent;

        /// <summary>
        /// 用户客户机的语言情况
        /// </summary>
        public string UserLanguages;

        /// <summary>
        ///  是否使用安全套结字
        /// </summary>
        public bool IsSecureConnection;

        /// <summary>
        /// 是否支持Active控件
        /// </summary>
        public bool ActiveXControls;

        /// <summary>
        /// 获取客户机的匿名标识符（如果存在）
        /// </summary>
        public string AnonymousID;

        /// <summary>
        /// 用户代理机的操作系统
        /// </summary>
        public string OperateSystem;

        /// <summary>
        /// 判断用户客户机是否支持Cookies
        /// </summary>
        public bool CanUseCookies;

        /// <summary>
        /// 判断用户客户机是否支持FRAMES 框架
        /// </summary>
        public bool CanUseFrames;

        /// <summary>
        /// 获取用户使用无线网络网关访问服务器的无线网关的版本
        /// </summary>
        public string GatewayVersion;

        /// <summary>
        /// 获取浏览器主版本号 
        /// </summary>
        public string BrowserVersion;

        /// <summary>
        /// 浏览器的名称
        /// </summary>
        public string BrowserName;
        public LoginClientData(string strUserIP)
        {
            UserTrueAddress = "";
            UserHostAddress = strUserIP;
            UserHostMac = "";
            UserHostName = "";
            UserAgent = "";
            UserLanguages = "";
            IsSecureConnection = true;
            ActiveXControls = true;
            AnonymousID = "";
            OperateSystem = "";
            CanUseCookies = true;
            CanUseFrames = true;
            GatewayVersion = "";
            BrowserVersion = "";
            BrowserName = "";
        }
    }
    #endregion
}
