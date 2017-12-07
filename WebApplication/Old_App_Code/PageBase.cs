using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading;
using i3.Core.View;
using i3.ValueObject;
using i3.ValueObject.Sys.General;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Contract;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Evaluation;
namespace i3.View
{
    /// <summary>
    /// 功能描述：页面基类所有页面必须实现此类
    /// 创建日期：2011-4-6 20:52:46
    /// 创建人  ：陈国迎
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 功能描述：POSTBACK后的回调JS
        /// 创建日期：2011-5-20 20:52:46
        /// 创建人  ：欧耀翔
        /// </summary>
        /// <param name="strScript"></param>
        public void PostBackShowScript(string strScript)
        {
            HtmlGenericControl jsScript = new HtmlGenericControl("script");
            jsScript.Attributes["type"] = "text/javascript";
            jsScript.InnerHtml = strScript;
            Page.Header.Controls.AddAt(0, jsScript);
        }

        /// <summary>
        /// 功能描述：弹出信息 Alert
        /// 创建日期：2011-5-20 20:52:46
        /// 创建人  ：欧耀翔
        /// </summary>
        /// <param name="msg"></param>
        public void Alert(string msg)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            jsAlert.InnerHtml = "$(document).ready(function(){setTimeout(function(){ alert('" + msg + "');},500);});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// 过滤没登录的用户
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {

            if (Request.RawUrl.ToLower().IndexOf("portal/login.aspx") == -1 && Session[KEY_CACHEOPERATOR] == null)
            {
                //throw new HttpException(500, "会话丢失，请重新登录！");
            }
            base.OnInit(e);
        }

        //日志变量
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Libertine");
        private string LogParam = "@Type:{0}@Object:{1}@User:{2}@IP:{3}@Result:{4}";
        #region 用户和权限使用变量
        /// <summary>
        /// 存放登陆用户变量在Session中的的标示
        /// </summary>
        public const String KEY_CACHEOPERATOR = "Cache:Operator";
        /// <summary>
        /// 存放登陆用户错误
        /// </summary>
        public const String KEY_USER_LOGIN_ERROR = "KEY_USER_LOGIN_ERROR";
        #endregion

        #region 初始化函数
        public PageBase()
        {
            //PageLoad事情委托
            this.Load += new EventHandler(PageBase_Load);
        }
        /// <summary>
        /// PageBase的Load事件
        /// </summary>
        public void PageBase_Load(object sender, EventArgs e)
        {
            //会话丢失处理
            if (Request.RawUrl.ToLower().IndexOf("portal/login.aspx") == -1 && Session[KEY_CACHEOPERATOR] == null)
            {
                string url = "http://" + Context.Request.Url.Authority + Context.Request.ApplicationPath;
                if (!url.EndsWith("/"))
                    url += "/";
                string strRootUrl = url + "Portal/Login.aspx";
                if (Request.RawUrl.ToLower().IndexOf("Portal/IndexNew.aspx") == -1)
                {
                    PostBackShowScript("alert('会话丢失，请重新登录!'); parent.window.parent.location ='" + strRootUrl + "';");
                }
                else
                {
                    //PostBackShowScript("alert('会话丢失，请重新登录!'); window.location ='" + strRootUrl + "';");
                    PostBackShowScript("window.location ='" + strRootUrl + "';");
                }
                    
                return;
            }
            if (!Page.IsPostBack)
            {

                //如果不是AjaxRequest,flash控件,才进行页面权限\记录日志\在线状态
                string sheader = Request.Headers["X-Requested-With"];
                bool isAjaxRequest = (sheader != null && sheader == "XMLHttpRequest") ? true : false;
                //string sheader2 = Request.Headers["x-flash-version"];
                //bool isAjaxRequest2 = (sheader2 != null) ? true : false;
                string strTypeRequest = (Request["type"] == null) ? "" : Request["type"];
                bool isFlashInitRequest = (strTypeRequest.Contains("flashinitdata")) ? true : false;//flash控件在firefox下没有Request.Headers["x-flash-version"]，所以统一定的type，解决访问页面时过滤掉flash控件访问的记录
                if (!isAjaxRequest && !isFlashInitRequest)
                {

                    //访问页面路径字符串
                    string strUrl = Request.AppRelativeCurrentExecutionFilePath;
                    if (strUrl.Length > 2)
                    {
                        strUrl = strUrl.Remove(0, 2);
                        strUrl = "../" + strUrl;
                    }
                    //通过路径得到菜单信息
                    TSysMenuVo objMenuVo = new TSysMenuVo();
                    objMenuVo.MENU_URL = strUrl;
                    objMenuVo.IS_DEL = "0";
                    objMenuVo.IS_USE = "1";
                    objMenuVo = new i3.BusinessLogic.Sys.General.TSysMenuLogic().SelectByObject(objMenuVo);
                    if (objMenuVo.ID.Length == 0 && Request["Type"] != null && !PagePermissions._listAllowPagePath.Contains(strUrl.ToLower()))
                    {
                        strUrl = strUrl + "?type=" + Request["Type"];
                        objMenuVo.MENU_URL = strUrl;
                        objMenuVo.IS_DEL = "0";
                        objMenuVo.IS_USE = "1";
                        objMenuVo = new i3.BusinessLogic.Sys.General.TSysMenuLogic().SelectByObject(objMenuVo);
                    }
                    //页面权限处理
                    if (LogInfo.UserInfo.ID != "000000001")
                    {
                        //判断用户是否有访问此页面的权限
                        bool isPass = PagePermissions.IsAllowVisitPage(strUrl, LogInfo.UserInfo.USER_NAME);
                        if (!isPass)
                        {
                            if (LogInfo.UserInfo.ID != "")
                            {
                                Response.Write("<script language=javascript>alert('您无访问此页面或功能的权限,请联系管理员配置菜单或用户组权限!'); history.back();</script>");
                                Response.End();
                                return;
                            }
                        }

                    }
                    //日志处理及在线状态处理 及页面标题
                    if (LogInfo.UserInfo.ID != "" && objMenuVo.ID.Length > 0)
                    {
                        //记录日志
                        WriteLogPage(i3.ValueObject.ObjectBase.LogType.ViewPage, "", "@PageName:" + objMenuVo.MENU_TEXT + "@PageURL:" + strUrl);
                        //记录状态
                        i3.BusinessLogic.Sys.General.TSysUserStatusLogic objUserStatusLogic = new i3.BusinessLogic.Sys.General.TSysUserStatusLogic();
                        i3.ValueObject.Sys.General.TSysUserStatusVo objUserStatusVo = new i3.ValueObject.Sys.General.TSysUserStatusVo();
                        objUserStatusVo = objUserStatusLogic.Details(LogInfo.UserInfo.ID);
                        objUserStatusVo.USER_ID = LogInfo.UserInfo.ID;
                        objUserStatusVo.LAST_OPTIME = DateTime.Now.ToString();
                        objUserStatusVo.LAST_PAGE = strUrl;
                        objUserStatusVo.LAST_OPERATION = objMenuVo.MENU_TEXT;//先把页面名称给操作
                        objUserStatusVo.LAST_LOGIN_IP = LogInfo.ClientInfo.UserHostAddress;
                        if (objUserStatusVo.ID != "")
                        {
                            objUserStatusLogic.Edit(objUserStatusVo);
                        }
                        else
                        {
                            objUserStatusVo.ID = LogInfo.UserInfo.ID;
                            objUserStatusLogic.Create(objUserStatusVo);
                        }
                        //页面标题
                        if (ISNomalMenu())//判断如果为非快捷菜单页面,标题从数据库中取
                        {
                            string strTitle = GetUrlName();
                            //如果标题在数据库中有值,并且模板中有标题控件LblTitle和以前的头容器控件cphModuleDesc
                            if (strTitle != "" && Page.Master != null && Page.Master.FindControl("LblTitle") != null && Page.Master.FindControl("cphModuleDesc") != null)
                            {
                                ((System.Web.UI.WebControls.Label)(Page.Master.FindControl("LblTitle"))).Text = strTitle;
                                Page.Master.FindControl("cphModuleDesc").Visible = false;//只做隐藏,为了不改动全部页面
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 通用信息输出
        /// <summary>
        /// 功能描述：重新Render方法，向页面输出系统统一内容，如Title、JS等
        /// 创建日期：2008.1.4
        /// 创建人  ：陈国迎
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            #region 通用信息输出
            //统一输出标题
            Page.Title = "欢迎使用环境监测业务管理系统V2.0";
            //统一描述
            HtmlMeta desc = new HtmlMeta();
            desc.Name = "Description";
            desc.Content = "珠海高凌信息科技有限公司 环境监测业务管理系统V2.0";
            //Page.Header.Controls.Add(desc);
            //关键字
            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "keywords";
            keywords.Content = "珠海高凌信息科技有限公司环境监测业务管理系统V2.0";
            //Page.Header.Controls.Add(keywords);
            #endregion
            base.Render(writer);
        }
        #endregion

        #region MD5
        /// <summary>
        /// 将指定的字符串进行MD5加密
        /// </summary>
        /// <param name="strCode">源字符串</param>
        /// <returns>加密后字符串</returns>
        public string ToMD5(string strCode)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strCode, "MD5");
        }
        #endregion

        #region 控件差异化处理
        /// <summary>
        /// 为dropdownlist和其它list添加一个"请选择"项
        /// </summary>
        /// <param name="control">需要添加项的控件</param>
        public void InsertTopItem(Control control)
        {
            if (control is ListControl)
            {
                if (((ListControl)control).Items.Count > 0)
                {
                    ((ListControl)control).Items.Insert(0, new ListItem("请选择", ""));
                }
                else
                {
                    ((ListControl)control).Items.Add(new ListItem("请选择", ""));
                }
            }
        }

        /// <summary>
        /// 为容器中的dropdownlist和其它list添加一个"请选择"项（不包括CheckBoxList）
        /// </summary>
        /// <param name="container">控件容器(page，或者自定义控件)</param>
        public static void InsertTopItemToContainer(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control == null)
                {
                    continue;
                }

                if (control is ListControl && control.GetType() != typeof(CheckBoxList))
                {
                    if (((ListControl)control).Items.Count > 0)
                    {
                        ((ListControl)control).Items.Insert(0, new ListItem("请选择", ""));
                    }
                    else
                    {
                        ((ListControl)control).Items.Add(new ListItem("请选择", ""));
                    }
                }
            }
        }


        /// <summary>
        /// 为单选按钮 做个首项选择
        /// </summary>
        /// <param name="li"></param>
        public void SelectTopIndex(ListControl li)
        {
            if (li is RadioButtonList)
            {
                if (((RadioButtonList)li).Items.Count > 0)
                    ((RadioButtonList)li).SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 锁定控件
        /// </summary>
        /// <param name="control"></param>
        protected void LoackControl(Control control)
        {
            foreach (Control c1 in control.Controls)
            {
                if (c1 is WebControl)
                {
                    ((WebControl)c1).Enabled = false;
                }
                LoackControl(c1);
            }
        }
        #endregion

        #region 将元素置为不可用
        /// <summary>
        /// 将容器内的所有元素置为不可用
        /// </summary>
        /// <param name="container">容器</param>
        protected void DisableAllControls(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control == null)
                {
                    continue;
                }

                if (control is TextBox)
                {
                    ((TextBox)control).Enabled = false;
                }
                if (control is DropDownList)
                {
                    ((DropDownList)control).Enabled = false;
                }
                if (control is RadioButton)
                {
                    ((RadioButton)control).Enabled = false;
                    return;
                }
                if (control is CheckBox)
                {
                    ((CheckBox)control).Enabled = false;
                }
            }
        }
        #endregion

        #region 用户信息存取
        /// <summary>
        /// 用户登陆信息
        /// </summary>
        public UserLogInfo LogInfo
        {
            get
            {
                if (null != Session[KEY_CACHEOPERATOR])
                {
                    return (UserLogInfo)Session[KEY_CACHEOPERATOR];
                    
                }
                else
                {
                    return new UserLogInfo();
                }
            }
            set
            {
                Session[KEY_CACHEOPERATOR] = value;
            }
        }
        #endregion

        #region 日志记录
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="LogType">日志类型</param>
        /// <param name="ObjectId">日志对象ID</param>
        /// <param name="Messange">日志消息</param>
        public void WriteLog(string LogType, string ObjectId, string Messange)
        {
            if (!IsOperationLog())
                return;
            //在登录未成功或者其他形式的没有合法Session时，要区分对待
            string strUserID = "";
            string strClientIP = "";
            if (null != Session[KEY_CACHEOPERATOR])
            {
                //Session信息存在的话 直接调用Session信息
                strUserID = String.IsNullOrEmpty(LogInfo.UserInfo.USER_NAME) ? "Libertine" : LogInfo.UserInfo.ID;
                strClientIP = String.IsNullOrEmpty(LogInfo.ClientInfo.UserHostAddress) ? "127.0.0.1" : LogInfo.ClientInfo.UserHostAddress;
            }
            else
            {
                //Session信息不存在则直接调用Request信息
                try
                {
                    strUserID = ObjectId;
                    strClientIP = Request.UserHostAddress;
                }
                catch { }
            }
            //写日志
            //log.InfoFormat(LogParam, LogType, ObjectId, strUserID, strClientIP, Messange + "@PageName:@PageURL:");
            log.InfoFormat(LogParam, LogType, ObjectId, strUserID, strClientIP, Messange);
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="LogType">日志类型</param>
        /// <param name="ObjectId">日志对象ID</param>
        /// <param name="Messange">日志消息</param>
        /// <param name="userLogInfo">用户登录信息</param>
        public static void WriteLog(string LogType, string ObjectId, string Messange, UserLogInfo userLogInfo)
        {
            //在登录未成功或者其他形式的没有合法Session时，要区分对待
            string strUserID = "";
            string strClientIP = "";
            if (null != userLogInfo.UserInfo)
            {
                //Session信息存在的话 直接调用Session信息
                strUserID = String.IsNullOrEmpty(userLogInfo.UserInfo.USER_NAME) ? "Libertine" : userLogInfo.UserInfo.ID;
                strClientIP = String.IsNullOrEmpty(userLogInfo.ClientInfo.UserHostAddress) ? "127.0.0.1" : userLogInfo.ClientInfo.UserHostAddress;
            }
            else
            {
                //Session信息不存在则直接调用Request信息
                strUserID = ObjectId;
                strClientIP = userLogInfo.ClientInfo.UserHostAddress;
            }
            //写日志
            log.InfoFormat("@Type:{0}@Object:{1}@User:{2}@IP:{3}@Result:{4}", LogType, ObjectId, strUserID, strClientIP, Messange + "@PageName:@PageURL:");
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="LogType">日志类型</param>
        /// <param name="ObjectId">日志对象ID</param>
        /// <param name="Messange">日志消息</param>
        public void WriteLogPage(string LogType, string ObjectId, string Messange)
        {
            if (!IsPageLog())
                return;
            //在登录未成功或者其他形式的没有合法Session时，要区分对待
            string strUserID = "";
            string strClientIP = "";
            if (null != Session[KEY_CACHEOPERATOR])
            {
                //Session信息存在的话 直接调用Session信息
                strUserID = String.IsNullOrEmpty(LogInfo.UserInfo.USER_NAME) ? "Libertine" : LogInfo.UserInfo.ID;
                strClientIP = String.IsNullOrEmpty(LogInfo.ClientInfo.UserHostAddress) ? "127.0.0.1" : LogInfo.ClientInfo.UserHostAddress;
            }
            else
            {
                //Session信息不存在则直接调用Request信息
                strUserID = ObjectId;
                strClientIP = Request.UserHostAddress;
            }
            //写日志
            log.InfoFormat(LogParam, LogType, ObjectId, strUserID, strClientIP, Messange);
        }



        /// <summary>
        /// 是否开始记录操作日志(系统变量中的系统开关量的"页面权限开关")
        /// </summary>
        /// <returns></returns>
        protected bool IsOperationLog()
        {
            i3.ValueObject.Sys.Resource.TSysConfigVo objSysConfigVo = new ValueObject.Sys.Resource.TSysConfigVo();
            objSysConfigVo.CONFIG_CODE = "OperationLog";
            objSysConfigVo = new i3.BusinessLogic.Sys.Resource.TSysConfigLogic().Details(objSysConfigVo);
            if (objSysConfigVo.CONFIG_VALUE != "1")
                return false;
            return true;
        }
        /// <summary>
        /// 是否开始记录页面日志(系统变量中的系统开关量的"页面权限开关")
        /// </summary>
        /// <returns></returns>
        protected bool IsPageLog()
        {
            i3.ValueObject.Sys.Resource.TSysConfigVo objSysConfigVo = new ValueObject.Sys.Resource.TSysConfigVo();
            objSysConfigVo.CONFIG_CODE = "PageLog";
            objSysConfigVo = new i3.BusinessLogic.Sys.Resource.TSysConfigLogic().Details(objSysConfigVo);
            if (objSysConfigVo.CONFIG_VALUE != "1")
                return false;
            return true;
        }
        #endregion

        #region 对象自动绑定

        /// <summary>
        /// 功能描述：将对象绑定到控件
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// 备注：    只针对套用母版页的页面
        /// </summary>
        /// <param name="obj">对象</param>
        public bool BindObjectToControlsMode(object obj)
        {
            return AutoBinder.BindObjectToControls(obj, Master.FindControl("cphInput"));
        }
        /// <summary>
        /// 功能描述：将控件的值填充至对象
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// 备注：    只针对套用母版页的页面
        /// </summary>
        /// <param name="obj">对象</param>
        public object BindControlsToObjectMode(object obj)
        {
            return AutoBinder.BindControlsToObjectWithNoNullValues(obj, Master.FindControl("cphInput"), ConstValues.SpecialCharacter.EmptyValuesFillChar);
        }

        /// <summary>
        /// 功能描述：将对象绑定到控件
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page或者自定义控件)</param>
        public static bool BindObjectToControls(object obj)
        {
            Page p = (Page)HttpContext.Current.Handler;
            return AutoBinder.BindObjectToControls(obj, p);
        }

        /// <summary>
        /// 功能描述：将对象绑定到控件
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page或者自定义控件)</param>
        public static bool BindObjectToControls(object obj, Control container)
        {
            return AutoBinder.BindObjectToControls(obj, container);
        }

        /// <summary>
        /// 功能描述：将控件的值填充至对象
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// </summary>
        /// <param name="obj">对象</param>
        public static object BindControlsToObject(object obj)
        {
            Page p = (Page)HttpContext.Current.Handler;
            return AutoBinder.BindControlsToObjectWithNoNullValues(obj, p, ConstValues.SpecialCharacter.EmptyValuesFillChar);
        }

        /// <summary>
        /// 功能描述：将控件的值填充至对象
        /// 创建日期：2008.1.4
        /// 创建人：  陈国迎
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page或者自定义控件)</param>
        public static object BindControlsToObject(object obj, Control container)
        {
            return AutoBinder.BindControlsToObjectWithNoNullValues(obj, container, ConstValues.SpecialCharacter.EmptyValuesFillChar);
        }

        /// <summary>
        /// 将指定的表及字段绑定至控件 by xwh 2010-09-03
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <param name="strDataTextField">文本列</param>
        /// <param name="strDataValueField">值例</param>
        /// <param name="bolAll">是否完全加载(针对is_del字段)</param>
        /// <param name="control">控件</param>
        public static void BindDataValuesToControl(string strTableName, string strDataTextField, string strDataValueField, bool bolAll, Control control)
        {
            if (bolAll)
            {
                ((ListControl)control).DataSource = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetDataDictByTableAndFieldsAll(strTableName, strDataTextField, strDataValueField);
            }
            else
            {
                ((ListControl)control).DataSource = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetDataDictByTableAndFieldsNotDeleted(strTableName, strDataTextField, strDataValueField);
            }
            //绑定数据
            ((ListControl)control).DataTextField = ObjectBase.DataDictBindFields.DataTextField;
            ((ListControl)control).DataValueField = ObjectBase.DataDictBindFields.DataValueField;
            ((ListControl)control).DataBind();
        }

        /// <summary>
        /// 将指定的字典项加载到指定的控件 by xwh 2010-09-03
        /// </summary>
        /// <param name="container">控件</param>
        public static void BindDataDictToControl(string strType, Control control)
        {
            //判断该类型在字典项表中是否存在
            if (new i3.BusinessLogic.Sys.Resource.TSysDictLogic().IsThisDataDictTypeExist(strType))
            {
                ((ListControl)control).DataSource = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetAutoLoadDataListByType(strType);
                ((ListControl)control).DataTextField = ObjectBase.DataDictBindFields.DataTextField;
                ((ListControl)control).DataValueField = ObjectBase.DataDictBindFields.DataValueField;
                ((ListControl)control).DataBind();
            }
        }
        public static void BindDeptDataDictToControl(string strType, Control control,string User_ID)
        {
            //判断该类型在字典项表中是否存在
            if (new i3.BusinessLogic.Sys.Resource.TSysDictLogic().IsThisDataDictTypeExist(strType))
            {
                ((ListControl)control).DataSource = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Dept_Info(User_ID);
                ((ListControl)control).DataTextField = ObjectBase.DataDictBindFields.DataTextField;
                ((ListControl)control).DataValueField = ObjectBase.DataDictBindFields.DataValueField;
                ((ListControl)control).DataBind();
            }
        }
        /// <summary>
        /// 获取下拉控件字典项JSON样式
        /// </summary>
        /// <returns></returns>
        public static string getDictJsonString(string strDictType)
        {
            TSysDictVo TSysDictVo = new ValueObject.Sys.Resource.TSysDictVo();
            TSysDictVo.DICT_TYPE = strDictType;
            DataTable dt = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().SelectByTable(TSysDictVo);
            return DataTableToJson(dt);
        }


        /// <summary>
        /// 根据字典项的类型和编码获取名称
        /// </summary>
        /// <param name="strDictCode">字典项编码</param>
        /// <param name="strDictType">字典项名称</param>
        /// <returns></returns>
        public static string getDictName(object strDictCode, object strDictType)
        {
            string strDictName = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetDictNameByDictCodeAndType(strDictCode.ToString(), strDictType.ToString());
            return strDictName;
        }
        /// <summary>
        /// 根据字典项的类型和编码获取名称
        /// </summary>
        /// <param name="strDictCode">字典项编码</param>
        /// <param name="strDictType">字典项名称</param>
        /// <returns></returns>
        public static string getDictNames(object strDictCode)
        {
            string strDictName = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetDictNameByDictCodeAndTypes(strDictCode.ToString());
            return strDictName;
        }
        /// <summary>
        /// 根据字典项类型获取符合条件的字典项列表
        /// </summary>
        /// <param name="strDictType">字典类型</param>
        /// <returns></returns>
        public static DataTable getDictList(string strDictType)
        {
            TSysDictVo objItems = new TSysDictVo();
            objItems.DICT_TYPE = strDictType;
            DataTable strDictdt = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().SelectByTable(objItems);
            return strDictdt;
        }
        /// <summary>
        /// 自动绑定字典项至容器
        /// 备注：只针对允许自动加载的字典项
        /// </summary>
        /// <param name="container">控件容器(page，或者自定义控件)</param>
        public static void BindDataDictToContainer(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control == null)
                {
                    continue;
                }

                if (control is ListControl)
                {
                    //判断该类型在字典项表中是否存在
                    if (new i3.BusinessLogic.Sys.Resource.TSysDictLogic().IsThisDataDictTypeExist(control.ID))
                    {
                        ((ListControl)control).DataSource = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().GetAutoLoadDataListByType(control.ID);
                        ((ListControl)control).DataTextField = ObjectBase.DataDictBindFields.DataTextField;
                        ((ListControl)control).DataValueField = ObjectBase.DataDictBindFields.DataValueField;
                        ((ListControl)control).DataBind();
                    }
                }
                else
                {
                    if (control.HasControls())
                    {
                        BindDataDictToContainer(control);
                    }
                }
            }
        }
        #endregion 对象自动绑定

        #region 获取指定类型的序号
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

        /// <summary>
        /// 功能描述：获取序号串
        /// 创建人：　潘德军
        /// 创建日期：2012-12-20
        /// </summary>
        /// <param name="strSerialType">类型</param>
        /// <param name="iSerialCount">序列号个数</param>
        /// <returns>序号</returns>
        public string GetSerialNumberList(string strSerialType, int iSerialCount)
        {
            return new i3.BusinessLogic.Sys.Resource.TSysSerialLogic().GetSerialNumberList(strSerialType, iSerialCount);
        }
        #endregion

        #region 替换界面不可用元素为标签
        /// <summary>
        /// 替换界面不可用元素为标签
        /// </summary>
        protected void ReplaceDisabledControlsToLabels()
        {
            //将页面不可用控件替换为Label
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Replace", "ReplaceDisabledControlsToLabels();", true);
        }
        #endregion

        /// <summary>
        /// 将Ajax提交到后台的数据自动绑定到对象【注意：未经同意不允许修改】，by 熊卫华 2012.11.05
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="request">request对象</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T autoBindRequest<T>(HttpRequest request, T obj)
        {
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();
            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                if (request[objProperty.Name] != null)
                    objProperty.SetValue(obj, request[objProperty.Name].ToString(), null);
            }

            return obj;
        }

        #region 将DataTable类型数据转成JSON数据
        /// <summary>
        /// JSon 序列化,将DataTable序列化为Json字符【注意：未经同意不允许修改】，by 熊卫华 2012.11.05
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="intRowsCount">总记录数</param>
        /// <returns></returns>
        public static string CreateToJson(DataTable dt, int intRowsCount)
        {
            if (dt == null) return "";
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> diRow = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    diRow.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(diRow);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(list);
            string json = @"{""Rows"":" + result + @",""Total"":""" + intRowsCount + @"""}";
            return json;
        }
        /// <summary>
        /// 转换为标准原生JSON 自动检索完成可识别 胡方扬 2012-11-28
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateMIMUJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            string value = "";
                            if (dt.Rows[i][j].GetType().FullName == "System.DateTime")
                            {
                                value = Convert.ToDateTime(dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                value = dt.Rows[i][j].ToString();
                            }

                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + value + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            string value = "";
                            if (dt.Rows[i][j].GetType().FullName == "System.DateTime")
                            {
                                value = Convert.ToDateTime(dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                value = dt.Rows[i][j].ToString();
                            }
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + value + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");

                return JsonString.ToString();
            }
            else
            {
                return "[]";
            }

        }
        /// <summary>
        /// 获取秦皇岛样品编号信息（质控使用）
        /// </summary>
        /// <param name="strSampleID">样品ＩＤ</param>
        /// <returns></returns>
        protected static string GetSampleCode_QHD(string strSampleID)
        {
            string strSampleCode = "";
            i3.ValueObject.Channels.Mis.Monitor.Sample.TMisMonitorSampleInfoVo objSample = new i3.BusinessLogic.Channels.Mis.Monitor.Sample.TMisMonitorSampleInfoLogic().Details(strSampleID);
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskVo objSubtask = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
            i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo objTask = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

            string strSerialName = "", strSampleCodeNum = "";
            if (objTask.SAMPLE_SOURCE == "2")
            {
                strSerialName = "G";
            }
            else
                strSerialName = "X";
            if (objSubtask.MONITOR_ID == "000000001")
            {
                strSerialName += "W";
            }
            else if (objSubtask.MONITOR_ID == "000000002")
            {
                strSerialName += "Q";
            }
            else if (objSubtask.MONITOR_ID == "000000003")
            {
                strSerialName += "F";
            }
            else
            {
                strSerialName += "W";
            }
            strSampleCodeNum = i3.View.PageBase.GetSerialNumber(strSerialName + DateTime.Now.Year.ToString());
            if (strSampleCodeNum.Length == 0)
            {
                TSysSerialVo sv = new TSysSerialVo();
                sv.SERIAL_CODE = strSerialName + DateTime.Now.Year.ToString();
                sv.SERIAL_NAME = strSerialName + DateTime.Now.Year.ToString();
                sv.SERIAL_NUMBER = "1";
                sv.LENGTH = "4";
                sv.GRANULARITY = "1";
                sv.MIN = "1";
                sv.MAX = new string('9', 4);
                sv.CREATE_TIME = System.DateTime.Now.ToString();
                new TSysSerialLogic().Create(sv);
                strSampleCodeNum = i3.View.PageBase.GetSerialNumber(strSerialName + DateTime.Now.Year.ToString());
            }

            strSampleCode = strSerialName + strSampleCodeNum;

            return strSampleCode;
        }

        /// <summary>
        /// 新增结果分析执行表信息
        /// </summary>
        /// <returns></returns>
        protected static void InsertResultAPP(string strResultID)
        {
            i3.ValueObject.Channels.Mis.Monitor.Result.TMisMonitorResultVo objResult = new ValueObject.Channels.Mis.Monitor.Result.TMisMonitorResultVo();
            objResult.ID = strResultID;
            DataTable dt = new i3.BusinessLogic.Channels.Mis.Monitor.Result.TMisMonitorResultLogic().SelectManagerByTable(objResult);

            i3.ValueObject.Channels.Mis.Monitor.Result.TMisMonitorResultAppVo objResultApp = new i3.ValueObject.Channels.Mis.Monitor.Result.TMisMonitorResultAppVo();
            objResultApp.ID = GetSerialNumber("MonitorResultAppId");
            objResultApp.RESULT_ID = strResultID;
            if (dt.Rows.Count > 0)
            {
                objResultApp.HEAD_USERID = dt.Rows[0]["ANALYSIS_MANAGER"].ToString();
                objResultApp.ASSISTANT_USERID = dt.Rows[0]["ANALYSIS_ID"].ToString();
            }
            new i3.BusinessLogic.Channels.Mis.Monitor.Result.TMisMonitorResultAppLogic().Create(objResultApp);
        }
        #endregion

        #region Excel操作类

        #endregion

        /// <summary>
        ///将DataTable 转换成liger UI Grid 使用的JSON数据【注意：未经同意不允许修改】 by 熊卫华 2012.10.31
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="recordCount">总计录数</param>
        /// <returns></returns>
        public static string gridDataToJson<T>(DataTable dt, int recordCount, T objVo)
        {
            List<T> list = new i3.Core.DataAccess.AutoTransformer().ConvertTableToOjbectList(dt, objVo);
            var griddata = new { Rows = list, Total = recordCount };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(griddata);
        }
        /// <summary>
        /// 获取下拉列表数据
        /// </summary>
        /// <returns></returns>
        public static string getYearInfo()
        {
            List<object> list = new List<object>();
            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 1; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("value", i.ToString());
                list.Add(dic);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(list);
            return result;
        }

        /// <summary>
        /// 获取下拉列表（季度）数据
        /// </summary>
        /// <returns></returns>
        public static string getSeasonInfo()
        {
            List<object> list = new List<object>();
            for (int i = 1; i <= 4; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("value", i.ToString());
                switch (i)
                {
                    case 1:
                        dic.Add("text", "一季度");
                        break;
                    case 2:
                        dic.Add("text", "二季度");
                        break;
                    case 3:
                        dic.Add("text", "三季度");
                        break;
                    case 4:
                        dic.Add("text", "四季度");
                        break;
                }
                list.Add(dic);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(list);
            return result;
        }

        /// <summary>
        /// 创建原因：获取当前年份的前N年，和后N年
        /// 创建人：胡方扬
        /// 创建日期：2013-06-26
        /// </summary>
        /// <returns></returns>
        public static string getYearInfo(int intBeforNum,int intAfterNum)
        {
            List<object> list = new List<object>();
            for (int i = DateTime.Now.Year - intBeforNum; i <= DateTime.Now.Year + intAfterNum; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("value", i.ToString());
                list.Add(dic);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(list);
            return result;
        }
        /// <summary>
        /// 获取下拉列表月度数据 Create By 魏林 2013-06-08
        /// </summary>
        /// <returns></returns>
        public static string getMonthInfo()
        {
            List<object> list = new List<object>();
            for (int i = 1; i <= 12; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("value", i.ToString());
                list.Add(dic);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(list);
            return result;
        }

        /// <summary>
        /// 对象转换为 Json
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(obj);
        }
        public object JsonToObject(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.DeserializeObject(jsonString);
        }
        public T JsonToObject<T>(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// DataTable 转换为 EasyUI Grid 的数据源
        /// </summary>
        /// <param name="dtSource">数据集</param>
        /// <returns>JSON 字符串</returns>
        public string DataTableToEasyUIGridSource(DataTable dtSource)
        {
            if (dtSource == null) return "";
            var rows = DataTableToJson(dtSource);
            string total = dtSource.ExtendedProperties.ContainsKey("total") ? dtSource.ExtendedProperties["total"].ToString() : dtSource.Rows.Count.ToString();
            return "{\"total\":" + total + ",\"rows\":" + rows + "}";
        }
        /// <summary>
        /// DataTable 转换为 Json 字符串
        /// </summary>
        /// <param name="dtSource">数据集</param>
        /// <returns>JSON 字符串</returns>
        public static string DataTableToJson(DataTable dtSource)
        {
            if (dtSource == null) return "";
            List<object> listRows = new List<object>();
            foreach (DataRow dr in dtSource.Rows)
            {
                Dictionary<string, object> diRow = new Dictionary<string, object>();
                foreach (DataColumn dc in dtSource.Columns)
                {
                    diRow.Add(dc.ColumnName, dr[dc].ToString());
                }
                listRows.Add(diRow);
            }
            return ToJson(listRows);
        }
        /// <summary>
        /// 功能描述：获取页面是否为普通页面(非快捷页面)
        /// 创建日期：2011-09-20
        /// 创建人：郑义
        /// </summary>
        /// <returns></returns>
        public bool ISNomalMenu()
        {
            bool blReturn = false;
            TSysMenuVo objMenuVo = new TSysMenuVo();
            objMenuVo.MENU_URL = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "");
            objMenuVo.IS_DEL = "0";
            objMenuVo = new i3.BusinessLogic.Sys.General.TSysMenuLogic().Details(objMenuVo);
            if (objMenuVo.IS_SHORTCUT == "0")
                blReturn = true;
            return blReturn;

        }
        /// <summary>
        /// 功能描述：获取页面文字名称
        /// 创建日期：2011-7-18
        /// 创建人：郑义
        /// </summary>
        /// <returns></returns>
        public string GetUrlName()
        {
            TSysMenuVo objMenuVo = new TSysMenuVo();
            objMenuVo.MENU_URL = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "");
            objMenuVo.IS_DEL = "0";
            objMenuVo = new i3.BusinessLogic.Sys.General.TSysMenuLogic().Details(objMenuVo);
            return objMenuVo.MENU_TEXT;
        }

        #region Demo模式
        /// <summary>
        /// 功能描述：获取Demo模式是否开启
        /// 创建日期：2011-7-19
        /// 创建人：郑义
        /// </summary>
        /// <returns></returns>
        public bool GetDemoMode()
        {
            TSysConfigVo objSysConfigVo = new TSysConfigVo();
            objSysConfigVo.CONFIG_CODE = "DemoMode";
            objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
            if (objSysConfigVo.CONFIG_VALUE == "1")
                return true;
            return false;
        }

        /// <summary>
        /// 功能描述：获取Demo参数
        /// 创建日期：2011-7-20
        /// 创建人：郑义
        /// </summary>
        /// <param name="strParameter">输入Demo参数</param>
        /// <returns></returns>
        public string GetDemoParameter(string strParameter)
        {
            string strResult = "";
            if (GetDemoMode())
            {
                TSysConfigVo objSysConfigVo = new TSysConfigVo();
                objSysConfigVo.CONFIG_CODE = strParameter;
                objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
                strResult = objSysConfigVo.CONFIG_VALUE;
            }
            else
            {
                switch (strParameter)
                {
                    case "DemoModeDateTime":
                        strResult = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "DemoModeDate":
                        strResult = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case "DemoModeYear":
                        strResult = DateTime.Now.ToString("yyyy");
                        break;
                    case "DemoModeMonth":
                        strResult = DateTime.Now.ToString("MM");
                        break;
                    case "DemoModeQuarter":
                        if (DateTime.Now.Month <= 3 && DateTime.Now.Month >= 1)
                            strResult = "01";
                        else if (DateTime.Now.Month <= 6 && DateTime.Now.Month >= 4)
                            strResult = "02";
                        else if (DateTime.Now.Month <= 9 && DateTime.Now.Month >= 7)
                            strResult = "03";
                        else if (DateTime.Now.Month <= 12 && DateTime.Now.Month >= 10)
                            strResult = "04";
                        break;
                    case "DemoModeDay":
                        strResult = DateTime.Now.ToString("dd");
                        break;
                    case "DemoModeTime":
                        strResult = DateTime.Now.ToString("HH:mm:ss");
                        break;
                }

            }
            return strResult;
        }

        # endregion

        #region 银行家算法舍入数据转换
        /// <summary>
        /// 将datatable中的小数数据按银行家算法保留一位
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="list">要转换保留一位小数的字段名</param>
        /// <returns></returns>
        public DataTable GetDTBankerAlgorithm(DataTable dt, params string[] list)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return dt;
            if (list.Length <= 0)
                return dt;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                foreach (string str in list)
                {
                    if (dt.Rows[i][str] != null && dt.Rows[i][str].ToString() != "")
                        dt.Rows[i][str] = Math.Round(double.Parse(dt.Rows[i][str].ToString()), 1, MidpointRounding.ToEven).ToString("f1");
                }
            }
            return dt;
        }

        #endregion

        #region 将左边对象与右边对象相同字段的值赋给右边对象
        /// <summary>
        /// 将左边对象与右边对象相同字段的值赋给右边对象
        /// </summary>
        /// <param name="obj1">左对象</param>
        /// <param name="obj2">右对象</param>
        public static void CopyObject(object obj1, object obj2)
        {
            Type objType1 = obj1.GetType();
            PropertyInfo[] propertyList1 = objType1.GetProperties();

            Type objType2 = obj2.GetType();
            PropertyInfo[] propertyList2 = objType2.GetProperties();

            foreach (PropertyInfo property1 in propertyList1)
            {
                foreach (PropertyInfo property2 in propertyList2)
                {
                    if (Equals(property1.Name.ToUpper(), property2.Name.ToUpper()))
                    {
                        property2.SetValue(obj2, property1.GetValue(obj1, null), null);
                    }
                }
            }
        }
        #endregion

        #region 不确定列的DATATABLE转化成JSON

        /// <summary>
        /// 不确定列的DATATABLE转化成JSON
        /// create by 钟杰华
        /// </summary>
        /// <param name="dt">数据源表格</param>
        /// <param name="unSureMark">不确定列标记</param>
        /// <returns></returns>
        public string DataTableToJsonUnsureCol(DataTable dt, string unSureMark)
        {
            if (dt.Rows.Count > 0)
            {
                string json = LigerGridDataToJson(dt, dt.Rows.Count);

                //拼接不确定的列
                StringBuilder unSureColumns = new StringBuilder();
                unSureColumns.Append("\"UnSureColumns\":[");

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Contains(unSureMark))
                    {
                        unSureColumns.Append("{\"columnName\":\"" + dc.ColumnName + "\"},");
                    }
                }
                if (unSureColumns.ToString() != "\"UnSureColumns\":[")
                    unSureColumns = unSureColumns.Remove(unSureColumns.Length - 1, 1);

                unSureColumns.Append("]");

                json = (json.TrimEnd('}') + "," + unSureColumns.ToString() + "}");

                return json;
            }
            else
                return "[]";
        }

        /// <summary>
        /// 不确定列的DATATABLE转化成JSON
        /// create by 邵世卓 无数据时返回空值
        /// </summary>
        /// <param name="dt">数据源表格</param>
        /// <param name="unSureMark">不确定列标记</param>
        /// <returns></returns>
        public string DataTableToJsonCreateCol(DataTable dt, string unSureMark)
        {
            if (dt.Rows.Count > 0)
            {
                string json = LigerGridDataToJson(dt, dt.Rows.Count);

                //拼接不确定的列
                StringBuilder unSureColumns = new StringBuilder();
                unSureColumns.Append("\"UnSureColumns\":[");

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Contains(unSureMark))
                    {
                        unSureColumns.Append("{\"columnName\":\"" + dc.ColumnName + "\"},");
                    }
                }
                if (unSureColumns.ToString() != "\"UnSureColumns\":[")
                    unSureColumns = unSureColumns.Remove(unSureColumns.Length - 1, 1);

                unSureColumns.Append("]");

                json = (json.TrimEnd('}') + "," + unSureColumns.ToString() + "}");

                return json;
            }
            else
                return "";
        }

        /// <summary>
        /// 不确定列的DATATABLE转化成JSON
        /// create by 魏林
        /// </summary>
        /// <param name="dt">数据源表格</param>
        /// <param name="unSureMark">不确定列标记</param>
        /// <returns></returns>
        public string DataTableToJsonUnsureCol(DataTable dt, DataTable dtAllItem, string unSureMark)
        {
            if (dt.Rows.Count > 0)
            {
                string json = LigerGridDataToJson(dt, dt.Rows.Count);

                //拼接不确定的列
                StringBuilder unSureColumns = new StringBuilder();
                unSureColumns.Append("\"UnSureColumns\":[");

                for (int i = 0; i < dtAllItem.Rows.Count; i++)
                {
                    unSureColumns.Append("{\"columnId\":\"" + dtAllItem.Rows[i]["ID"].ToString() + unSureMark + "\",\"columnName\":\"" + dtAllItem.Rows[i]["ITEM_NAME"].ToString() + "\",\"columnWidth\":\"" + dtAllItem.Rows[i]["ITEM_NAME"].ToString().Length * 25 + "\"}");
                    if (i != dtAllItem.Rows.Count - 1)
                        unSureColumns.Append(",");
                }

                unSureColumns.Append("]");

                json = (json.TrimEnd('}') + "," + unSureColumns.ToString() + "}");

                return json;
            }
            else
                return "[]";
        }

        /// <summary>
        /// DATATABLE转化成JSON 用于填报动态生成列和显示数据
        /// create by 魏林
        /// </summary>
        /// <returns></returns>
        public string DataTableToJsonUnsureCol(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string json = LigerGridDataToJson(dt, dt.Rows.Count);

                //拼接不确定的列
                StringBuilder unSureColumns = new StringBuilder();
                unSureColumns.Append("\"UnSureColumns\":[");

                string ColName = "";
                string[] Col;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ColName = dt.Columns[i].ColumnName;
                    if (ColName == "ID")
                    {
                        unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + ColName + "\",\"columnWidth\":\"" + ColName.Length * 2.5 + "\"}");
                    }
                    else
                    {
                        Col = ColName.Split('@');
                        if (ColName.Contains("ITEM"))
                            unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 2.5 + "\"}");
                        else
                            unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 3.5 + "\"}");
                    }
                    if (i != dt.Columns.Count - 1)
                        unSureColumns.Append(",");
                }

                unSureColumns.Append("]");

                json = (json.TrimEnd('}') + "," + unSureColumns.ToString() + "}");

                return json;
            }
            else
                return "[]";
        }

        /// <summary>
        /// DATATABLE转化成JSON 用于填报动态生成列和显示数据()
        /// create by 魏林
        /// </summary>
        /// <returns></returns>
        public string DataTableToJsonUnsureColEx(DataTable dt)
        {
            string json = "";
            //拼接不确定的列
            StringBuilder unSureColumns = new StringBuilder();
            unSureColumns.Append("\"UnSureColumns\":[");

            string ColName = "";
            string[] Col;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ColName = dt.Columns[i].ColumnName;
                Col = ColName.Split('@');
                if (ColName.Contains("ITEM"))
                    unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 2.5 + "\"}");
                else
                    unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 3.5 + "\"}");

                if (i != dt.Columns.Count - 1)
                    unSureColumns.Append(",");
            }

            unSureColumns.Append("]");

            json = "{" + unSureColumns.ToString() + "}";

            return json;
        }

        /// <summary>
        /// DATATABLE转化成JSON 用于填报小时
        /// create by ljn
        /// </summary>
        /// <returns></returns>
        public string DataTableToJsonUnsureColAirHour(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string json =  LigerGridDataToJson(dt, dt.Rows.Count);
                
                //拼接不确定的列
                StringBuilder unSureColumns = new StringBuilder();
                unSureColumns.Append("\"UnSureColumns\":[");

                string ColName = "";
                string[] Col;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ColName = dt.Columns[i].ColumnName;
                    if (ColName == "ID")
                    {
                        unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + ColName + "\",\"columnWidth\":\"" + ColName.Length * 2.5 + "\"}");
                    }
                    else
                    {
                        Col = ColName.Split('@');
                        if (ColName.Contains("ITEM"))
                            unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 2.5 + "\"}");
                        else
                            unSureColumns.Append("{\"columnId\":\"" + ColName + "\",\"columnName\":\"" + Col[2] + "\",\"columnWidth\":\"" + ColName.Length * 3.5 + "\"}");
                    }
                    if (i != dt.Columns.Count - 1)
                        unSureColumns.Append(",");
                }

                unSureColumns.Append("]");

                json = (json.TrimEnd('}') + "," + unSureColumns.ToString() + "}");

                return json;
            }
            else
                return "[]";
        }

        #endregion

        #region 获取日期JSON
        /// <summary>
        /// 获取日期JSON
        /// create by 钟杰华
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public string GetDayByMonth(string year, int month)
        {
            int dayCount = Convert.ToDateTime((!string.IsNullOrEmpty(year) ? year : DateTime.Now.Year.ToString()) + "/" + month + "/1").AddMonths(1).AddDays(-1).Day;

            StringBuilder json = new StringBuilder();
            json.Append("[");
            for (int i = 1; i <= dayCount; i++)
            {
                json.Append("{\"VALUE\":\"" + i.ToString() + "\",\"DAY\":\"" + i.ToString() + "\"},");
            }
            json = json.Remove(json.Length - 1, 1);
            json.Append("]");

            return json.ToString();
        }

        #endregion

        #region 把JSON字符串转换成DATATABLE。基于胡方扬的方法，但去除列名上的双引号

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
                    dc.ColumnName = dc.ColumnName.Replace("\"", "");
                }
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        #endregion

        #region 生成树型 Json数据
        /// <summary>
        /// 功能描述：构造LigerUI树Json
        /// 创建时间：2013-1-10
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="strFilter">select过滤条件</param>
        /// <param name="strParentID">父级ID项</param>
        /// <param name="strSonID">子级ID项(parent_id)</param>
        /// <returns>Json</returns>
        /// <summary>
        public static string LigerGridTreeDataToJson(DataTable dt, string strFilter, string ID_Name, string ParentID_Name, string strFileName)
        {
            List<Dictionary<string, object>> listbanktype = MoveArea(dt, strFilter, ID_Name, ParentID_Name, strFileName);
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(listbanktype);
        }

        /// <summary>
        /// 功能描述：构造LigerUI树Json
        /// 创建时间：2013-1-10
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="strFilter">select过滤条件</param>
        /// <param name="strParentID">父级ID项</param>
        /// <param name="strSonID">子级ID项(parent_id)</param>
        /// <returns>Json</returns>
        /// <summary>
        public static List<Dictionary<string, object>> LigerGridTreeDataToObj(DataTable dt, string strFilter, string ID_Name, string ParentID_Name, string strFileName)
        {
            List<Dictionary<string, object>> listbanktype = MoveArea(dt, strFilter, ID_Name, ParentID_Name, strFileName);
            return listbanktype;
        }

        /// <summary>
        /// 递归获取子级题库类型
        /// </summary>
        /// <param name="dtArea">数据集</param>
        /// <param name="strFilter">过滤字段</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> MoveArea(DataTable objItemTb, string strFilter, string ID_Name, string ParentID_Name, string strFileName)
        {
            var rowParent = objItemTb.Select(strFilter);
            if (rowParent.Count() == 0)
            {
                return null;
            }
            List<Dictionary<string, object>> listChilren = new List<Dictionary<string, object>>();
            for (int i = 0; i < rowParent.Count(); i++)
            {
                Dictionary<string, object> diTree = new Dictionary<string, object>();
                string id = rowParent[i][ID_Name].ToString();

                var next = objItemTb.Select(" " + ParentID_Name + " = '" + rowParent[i][ID_Name].ToString() + "'");

                diTree.Add("id", id);
                diTree.Add("text", rowParent[i][strFileName].ToString());
                List<Dictionary<string, object>> childrenRows = MoveArea(objItemTb, " " + ParentID_Name + " = '" + rowParent[i][ID_Name].ToString() + "'", ID_Name, ParentID_Name, strFileName);
                if (childrenRows != null)
                {
                    diTree.Add("children", childrenRows);
                }
                listChilren.Add(diTree);
            }
            return listChilren;
        }
        #endregion

        #region LigerUI 相关JSON数据处理方式 Create By 胡方扬 2012-11-05
        /// <summary>
        ///直接将DataTable 转换成ligerUI Grid 使用的JSON数据【注意：未经同意不允许修改】 by 胡方扬 2012.11.05
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="recordCount">总计录数</param>
        /// <returns></returns>
        public static string LigerGridDataToJson(DataTable dt, int recordCount)
        {
            if (dt == null) return "";
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> diRow = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    diRow.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(diRow);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = serializer.MaxJsonLength * 10;
            string result = serializer.Serialize(list);
            string json = @"{""Rows"":" + result + @",""Total"":""" + recordCount + @"""}";
            return json;
        }
        /// <summary>
        ///直接将DataTable 转换成ligerUI Grid 使用的JSON数据【注意：未经同意不允许修改】 by LJN
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="recordCount">总计录数</param>
        /// <returns></returns>
        public static string LigerGridDataToJsonAirHour(DataTable dt, int recordCount)
        {
            string result = string.Empty;
            if (dt == null) return "";
            result = "[";
            foreach (DataRow dr in dt.Rows)
            {
                result += "{";
                foreach (DataColumn dc in dt.Columns)
                {
                    result += "\"" + dc.ColumnName + "\":\"" + dr[dc].ToString() + "\",";
                }
                result = result.TrimEnd(',') + "},";
            }
            result = result.TrimEnd(',') + "]";
            string json = @"{""Rows"":" + result + @",""Total"":""" + recordCount + @"""}";
            return json;
        }
        /// <summary>
        /// 直接将DataTable 转换成ligerUI Grid 树形结构 使用的JSON数据【注意：未经同意不允许修改】 by 胡方扬 2012.12.28
        /// </summary>
        /// <param name="dt">父表数据集</param>
        /// <param name="dtChild">子表数据集</param>
        /// <param name="strKeyName">关键字名称，如ID</param>
        /// <param name="recordCount">主表记录条数</param>
        /// <returns></returns>
        public static string LigerGridTreeDataToJson(DataTable dt, DataTable dtChild, string strKeyName, int recordCount)
        {
            if (dt == null) return "";
            List<object> list = new List<object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (DataRow dr in dt.Rows)
            {
                List<object> listChild = new List<object>();
                Dictionary<string, object> diRow = new Dictionary<string, object>();
                Dictionary<string, object> diRowChild = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    diRow.Add(dc.ColumnName, dr[dc].ToString());
                }
                //以下动态生成父表记录的子记录
                DataRow[] drArr = dtChild.Select(" " + strKeyName + " ='" + dr[strKeyName].ToString() + "'");
                foreach (DataRow drr in drArr)
                {
                    diRowChild = LigerGridTreeChildDataToJson(dtChild, drr);
                    listChild.Add(diRowChild);
                }
                if (listChild.Count > 0)
                {
                    diRow.Add("children", listChild);
                }
                list.Add(diRow);
                recordCount = diRow.Count;
            }
            //生成JSON 数据传输到前台JS
            string result = serializer.Serialize(list);
            string json = @"{""Rows"":" + result + @",""Total"":""" + recordCount + @"""}";
            return json;
        }

        /// <summary>
        /// 获取指定ID的树表结构子节点 【注意：未经同意不允许修改】 by 胡方扬 2012.12.28
        /// </summary>
        /// <param name="dt">子表数据集</param>
        /// <param name="dRow">父表中的一行</param>
        /// <returns></returns>
        public static Dictionary<string, object> LigerGridTreeChildDataToJson(DataTable dt, DataRow dRow)
        {
            Dictionary<string, object> diRow = new Dictionary<string, object>();
            if (dt == null) return null;
            foreach (DataColumn dc in dt.Columns)
            {
                diRow.Add(dc.ColumnName, dRow[dc].ToString());
            }
            return diRow;
        }
        /// <summary>
        /// 返回JSON对象数据   Ajax请求后返回数据object取值方式为data.d  Create By Castle(胡方扬) 2012-11-06
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static List<object> LigerGridSelectDataToJson(DataTable dt, int recordCount)
        {
            if (dt == null) return null;
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> diRow = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    diRow.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(diRow);
            }

            return list;
        }

        /// <summary>
        /// json特殊字符处理 胡方扬
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string ToJsonString(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '/': sb.Append("\\/"); break;
                    case '\b': sb.Append("\\b"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    case '\0': sb.Append(""); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        #endregion

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

        #region yinchengyi 2015-9-9 把一组值逐个压入Json队列中一般用于填充前台Text控件
        protected string appendJson(string strBaseJson, string strIndex, string strVal)
        {
            strBaseJson = strBaseJson.TrimStart('{');

            strBaseJson = strBaseJson.TrimEnd('}');

            if ("" == strBaseJson)
            {
                strBaseJson = "{\"" + strIndex + "\":\"" + strVal + "\"}";
            }
            else
            {
                strBaseJson = "{" + strBaseJson + ",\"" + strIndex + "\":\"" + strVal + "\"}";
            }

            return strBaseJson;

        }
        #endregion

        #region LigerUIDialog服务端弹出调用方法 使用LigerUIDialogAlert之前确保页面上LigerDialog.js/ligerui-all.css/all.css已被引用 Create By 胡方扬 2012-11-05
        /// <summary>
        /// LigerDialog弹出类型 枚举
        /// </summary>
        public enum DialogMold
        {
            success, warn, question,
            error, warning,
            prompt, waitting
        };

        /// <summary>
        /// LigerDialog 弹出窗口提示方法  Castle(胡方扬) 2012-11-01
        /// </summary>
        /// <param name="Msg">提示内容</param>
        /// <param name="DialogMold">弹出类型（枚举）</param>
        /// <param name="SetTimeout">弹窗停留时间</param>
        public void LigerDialogAlert(string Msg, string DialogMold, string SetTimeout)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            jsAlert.InnerHtml = "$(document).ready(function(){var manager=$.ligerDialog." + DialogMold + "('" + Msg + "');setTimeout(function(){manager.close(); }," + SetTimeout + ");});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// LigerDialog 弹出窗口提示方法  Castle(胡方扬) 2012-11-01
        /// </summary>
        /// <param name="Msg">提示内容</param>
        /// <param name="DialogMold">弹出类型（枚举）</param>
        public void LigerDialogAlert(string Msg, string DialogMold)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog." + DialogMold + "('" + Msg + "');});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// LigerDialog 弹出窗口提示方法并关闭当前页面  Castle(胡方扬) 2012-11-01
        /// </summary>
        /// <param name="Msg">提示内容</param>
        /// <param name="DialogMold">弹出类型（枚举）</param>
        public void LigerDialogPageCloseAlert(string Msg, string DialogMold, string SetTimeout)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog." + DialogMold + "('" + Msg + "');setTimeout(function(){top.f_removeSelectedTabs(); }," + SetTimeout + ");});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// LigerDialog Confi弹出窗口提示方法  Castle(胡方扬) 2012-11-01
        /// </summary>
        /// <param name="Msg">提示内容</param>
        /// <param name="Action">为真时 执行的事件</param>
        public void LigerDialogConfirmAlert(string Msg, string ActionJavaScript)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirm('" + Msg + "',function(result){" + ActionJavaScript + "});});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// LigerDialog Confi弹出窗口提示方法【重载方法】，用于工作流操作成功后，提示且跳转页面
        /// </summary>
        /// <param name="Msg">提示内容</param>
        /// <param name="strTitle">提示标题</param>
        /// <param name="strReturnURL">跳转页面</param>
        /// <param name="Action">执行的事件</param>
        public void LigerDialogConfirmAlert(string Msg, string strTitle, string strReturnURL)
        {
            HtmlGenericControl jsAlert = new HtmlGenericControl("script");
            jsAlert.Attributes["type"] = "text/javascript";
            //jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirm('" + Msg + "',function(result){if(result) { top.tab.addTabItem({ url: " + strReturnURL + " })} });});";
            //jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirm('" + Msg + "',function(result){if(result) {self.location.href='" + strReturnURL + "';} });});";

            //jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirmWF('" + Msg + "','" + strTitle + "',function(result){if(result) {self.location.href='" + strReturnURL + "';} });});";
            jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirmWF('" + Msg + "','" + strTitle + "',function(result){if(result) {top.f_overTab('" + strTitle + "', '" + strReturnURL + "');} });});";
            //Response.Write("top.f_overTab('任务办理', '" + strFirstStepUrl + "');");

            //jsAlert.InnerHtml = "$(document).ready(function(){$.ligerDialog.confirm('" + Msg + "',function(result){if(result==true) {top.f_addTab('TaskListForJs', '任务办理22222222', "+strReturnURL+");}else{ dialog.close();} });});";
            Page.Header.Controls.Add(jsAlert);
        }

        /// <summary>
        /// LigerOpenWindow方法，用于工作流控件评论信息显示，提示且跳转页面
        /// </summary>
        /// <param name="Msg">提示信息</param>
        /// <param name="strURL">连接地址</param>
        public void LigerOpenWindow(string Msg, string strURL, string strWidth, string strHeight)
        {
            HtmlGenericControl jsOpenURL = new HtmlGenericControl("script");
            jsOpenURL.Attributes["type"] = "text/javascript";
            jsOpenURL.InnerHtml = "$(document).ready(function(){ $.ligerDialog.open({ url: '" + strURL + "', width: " + strWidth + ",height:" + strHeight + ",modal:false });});";
            Page.Header.Controls.Add(jsOpenURL);
        }
        #endregion

        #region 根据自定义规则生成样品编号统一编号规则 Create By 胡方扬 2012-12-04
        /// <summary>
        /// 生成委托单号  Create By Castle 胡方扬 2012-12-04
        /// </summary>
        /// <param name="ruleArr"></param>
        /// <returns></returns>
        public static string CreateSerialNumber(string[] ruleArr)
        {
            string result = "";
            if (ruleArr != null)
            {
                string ReturnRule = "";
                foreach (string strRule in ruleArr)
                {
                    if (!String.IsNullOrEmpty(strRule))
                    {
                        ReturnRule += strRule.ToString();
                    }
                }

                result = ReturnRule.ToString();
            }

            return result;
        }

        /// <summary>
        /// 根据编号规则设置，动态生成规则号 Create By Castle 胡方扬 2013-04-19
        /// </summary>
        /// <param name="strRule">生成规则,年月日时分秒格式必须为[yy]或[yyyy]、[MM]、[dd]、[HH]、[mi]、[ss]</param>
        /// <param name="NumberBit">序列号产生位数</param>
        /// <param name="strNumber">初始化序列号</param>
        /// <returns></returns>
        public static string CreateDefineCodeForYear(string strRule, int NumberBit, string strNumber)
        {
            string result = "";
            string strNowDate = DateTime.Now.ToString("yyyyMMdd");
            string strNowYear = DateTime.Now.Year.ToString();

            string strNowMonth = DateTime.Now.Month.ToString();
            if (strNowMonth.Length < 2)
            {
                strNowMonth = strNowMonth.PadLeft(2, '0');
            }
            string strNowDay = DateTime.Now.Day.ToString();
            if (strNowDay.Length < 2)
            {
                strNowDay = strNowDay.PadLeft(2, '0');
            }
            string strNowHours = DateTime.Now.Hour.ToString();
            if (strNowHours.Length < 2)
            {
                strNowHours = strNowHours.PadLeft(2, '0');
            }
            string strNowMinute = DateTime.Now.Minute.ToString();
            if (strNowMinute.Length < 2)
            {
                strNowMinute = strNowMinute.PadLeft(2, '0');
            }
            string strNowSecond = DateTime.Now.Second.ToString();
            if (strNowSecond.Length < 2)
            {
                strNowSecond = strNowSecond.PadLeft(2, '0');
            }
            if (strRule != null)
            {
                if (strRule.IndexOf("[yy]") >= 0)
                {
                    strRule = strRule.Replace("[yy]", strNowYear.Substring(2, 2));
                }
                if (strRule.IndexOf("[yyyy]") >= 0)
                {
                    strRule = strRule.Replace("[yyyy]", strNowYear);
                }
                if (strRule.IndexOf("[MM]") >= 0)
                {
                    strRule = strRule.Replace("[MM]", strNowMonth);
                }
                if (strRule.IndexOf("[dd]") >= 0)
                {
                    strRule = strRule.Replace("[dd]", strNowDay);
                }
                if (strRule.IndexOf("[HH]") >= 0)
                {
                    strRule = strRule.Replace("[HH]", strNowHours);
                }
                if (strRule.IndexOf("[mm]") >= 0)
                {
                    strRule = strRule.Replace("[mi]", strNowMinute);
                }
                if (strRule.IndexOf("[ss]") >= 0)
                {
                    strRule = strRule.Replace("[ss]", strNowSecond);
                }
                if (NumberBit > 0 && !String.IsNullOrEmpty(strNumber))
                {
                    if (strNumber != "0")
                    {
                        if (strNumber.Length < NumberBit)
                        {
                            strNumber = strNumber.PadLeft(NumberBit, '0');
                        }
                    }
                }
                else
                {
                    strNumber = "";
                }
                result = strRule + strNumber;
            }

            return result;
        }

        /// <summary>
        /// 根据编号规则设置，动态生成规则号 Create By Castle 胡方扬 2013-04-19
        /// </summary>
        /// <param name="strRule">生成规则,年月日时分秒格式必须为[yy]或[yyyy]、[MM]、[dd]、[HH]、[mi]、[ss]</param>
        /// <param name="NumberBit">序列号产生位数</param>
        /// <param name="strNumber">初始化序列号</param>
        /// <returns></returns>
        public static string CreateDefineCodeForYearAndUnion(string strRule, int NumberBit, string strNumber, string strUnionRule)
        {
            string result = "";
            string strNowDate = DateTime.Now.ToString("yyyyMMdd");
            string strNowYear = DateTime.Now.Year.ToString();

            string strNowMonth = DateTime.Now.Month.ToString();
            if (strNowMonth.Length < 2)
            {
                strNowMonth = strNowMonth.PadLeft(2, '0');
            }
            string strNowDay = DateTime.Now.Day.ToString();
            if (strNowDay.Length < 2)
            {
                strNowDay = strNowDay.PadLeft(2, '0');
            }
            string strNowHours = DateTime.Now.Hour.ToString();
            if (strNowHours.Length < 2)
            {
                strNowHours = strNowHours.PadLeft(2, '0');
            }
            string strNowMinute = DateTime.Now.Minute.ToString();
            if (strNowMinute.Length < 2)
            {
                strNowMinute = strNowMinute.PadLeft(2, '0');
            }
            string strNowSecond = DateTime.Now.Second.ToString();
            if (strNowSecond.Length < 2)
            {
                strNowSecond = strNowSecond.PadLeft(2, '0');
            }
            if (strRule != null)
            {
                if (strRule.IndexOf("[UU]") >= 0)
                {
                    strRule = strRule.Replace("[UU]", strUnionRule);
                }
                if (strRule.IndexOf("[yy]") >= 0)
                {
                    strRule = strRule.Replace("[yy]", strNowYear.Substring(2, 2));
                }
                if (strRule.IndexOf("[yyyy]") >= 0)
                {
                    strRule = strRule.Replace("[yyyy]", strNowYear);
                }
                if (strRule.IndexOf("[MM]") >= 0)
                {
                    strRule = strRule.Replace("[MM]", strNowMonth);
                }
                if (strRule.IndexOf("[dd]") >= 0)
                {
                    strRule = strRule.Replace("[dd]", strNowDay);
                }
                if (strRule.IndexOf("[HH]") >= 0)
                {
                    strRule = strRule.Replace("[HH]", strNowHours);
                }
                if (strRule.IndexOf("[mm]") >= 0)
                {
                    strRule = strRule.Replace("[mi]", strNowMinute);
                }
                if (strRule.IndexOf("[ss]") >= 0)
                {
                    strRule = strRule.Replace("[ss]", strNowSecond);
                }
                if (NumberBit > 0 && !String.IsNullOrEmpty(strNumber))
                {
                    if (strNumber != "0")
                    {
                        if (strNumber.Length < NumberBit)
                        {
                            strNumber = strNumber.PadLeft(NumberBit, '0');
                        }
                    }
                }
                else
                {
                    strNumber = "";
                }

                result = strRule + strNumber;
            }

            return result;
        }

        /// <summary>
        /// 生成委托书、任务、报告编号使用的方法
        /// </summary>
        /// <param name="objSerial"></param>
        /// <returns></returns>
        public static string CreateBaseDefineCode(TBaseSerialruleVo objSerial)
        {
            string result = "";
            DataTable dtSerialTask = new TBaseSerialruleLogic().SelectByTable(objSerial);
            foreach (DataRow dr in dtSerialTask.Rows)
            {
                TBaseSerialruleVo objSerialItemsTask = new TBaseSerialruleVo();
                string StartSerialNumTask = dr["SERIAL_START_NUM"].ToString();
                if (dr["STATUS"].ToString() == "1")
                {
                    objSerialItemsTask.ID = dr["ID"].ToString();
                    objSerialItemsTask.SERIAL_YEAR = dr["SERIAL_YEAR"].ToString();
                    if (new TBaseSerialruleLogic().UpdateInitStartNumForNewYear(objSerialItemsTask, DateTime.Now.Year.ToString()))
                    {
                        StartSerialNumTask = "1";
                    }
                }
                result = PageBase.CreateDefineCodeForYear(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNumTask);
                //判断是否为不用生成后续编号规则 只生成部分 当SERIAL_NUMBER_BIT=0，SERIAL_START_NUM=0
                bool blFlag = false;
                if (!String.IsNullOrEmpty(dr["SERIAL_NUMBER_BIT"].ToString()) && !String.IsNullOrEmpty(dr["SERIAL_START_NUM"].ToString())
                    && dr["SERIAL_NUMBER_BIT"].ToString() != "0" && dr["SERIAL_START_NUM"].ToString() != "0")
                {

                    blFlag = true;
                }

                if (!String.IsNullOrEmpty(result) && blFlag)
                {
                    //将变号自动加1;
                    TBaseSerialruleVo objSerialUp = new TBaseSerialruleVo();
                    objSerialUp.ID = dr["ID"].ToString();
                    new TBaseSerialruleLogic().UpdateSerialNum(objSerialUp);
                }
            }

            return result;
        }

        /// <summary>
        /// 生成委托书、任务、报告编号使用的方法
        /// </summary>
        /// <param name="objSerial"></param>
        /// <returns></returns>
        public static string CreateBaseDefineCode(TBaseSerialruleVo objSerial, TMisContractVo objContract)
        {
            string result = "";
            DataTable dtSerial = new TBaseSerialruleLogic().SelectByTable(objSerial);

            foreach (DataRow dr in dtSerial.Rows)
            {
                if (dr["SERIAL_TYPE_ID"].ToString().IndexOf(objContract.CONTRACT_TYPE) >= 0)
                {
                    TBaseSerialruleVo objSerialItems = new TBaseSerialruleVo();
                    string StartSerialNum = dr["SERIAL_START_NUM"].ToString();
                    if (dr["STATUS"].ToString() == "1")
                    {
                        objSerialItems.ID = dr["ID"].ToString();
                        objSerialItems.SERIAL_YEAR = dr["SERIAL_YEAR"].ToString();
                        if (new TBaseSerialruleLogic().UpdateInitStartNumForNewYear(objSerialItems, DateTime.Now.Year.ToString()))
                        {
                            StartSerialNum = "1";
                        }
                    }
                    result = CreateDefineCodeForYear(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNum);
                    //判断是否为不用生成后续编号规则 只生成部分 当SERIAL_NUMBER_BIT=0，SERIAL_START_NUM=0
                    bool blFlag = false;
                    if (!String.IsNullOrEmpty(dr["SERIAL_NUMBER_BIT"].ToString()) && !String.IsNullOrEmpty(dr["SERIAL_START_NUM"].ToString())
                        && dr["SERIAL_NUMBER_BIT"].ToString() != "0" && dr["SERIAL_START_NUM"].ToString() != "0")
                    {

                        blFlag = true;
                    }

                    if (!String.IsNullOrEmpty(result) && blFlag)
                    {
                        //将变号自动加1;
                        TBaseSerialruleVo objSerialUp = new TBaseSerialruleVo();
                        objSerialUp.ID = dr["ID"].ToString();
                        new TBaseSerialruleLogic().UpdateSerialNum(objSerialUp);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 生成样品编号
        /// </summary>
        /// <param name="objSerial"></param>
        /// <param name="objTask"></param>
        /// <param name="objSubtask"></param>
        /// <param name="objItems"></param>
        /// <returns></returns>
        public static string CreateBaseDefineCodeForSample(TBaseSerialruleVo objSerial, TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objSubtask)
        {
            string result = "";
            DataTable dt = new TBaseSerialruleLogic().SelectByTable(objSerial);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["SERIAL_TYPE_ID"].ToString().IndexOf(objSubtask.MONITOR_ID) >= 0)
                    {
                        string StartSerialNum = dr["SERIAL_START_NUM"].ToString();
                        //初始化使用缺省组合编号规则
                        string strUnionRule = dr["UNION_DEFAULT"].ToString();
                        TBaseSerialruleVo objSerialUnion = new TBaseSerialruleVo();
                        //判断当前类别是否是联合条件编码，如果是 则根据联合编码的 委托类型进行判断  胡方扬 2013-05-07
                        if (dr["IS_UNION"].ToString() == "1" && objTask.CONTRACT_TYPE != "")
                        {
                            objSerialUnion = new TBaseSerialruleLogic().Details(dr["UNION_SEARIAL_ID"].ToString());

                            //获取辅助组合编号规则
                            if (objSerialUnion.SERIAL_TYPE_ID.ToString().IndexOf(objTask.CONTRACT_TYPE) >= 0)
                            {
                                strUnionRule = objSerialUnion.SERIAL_RULE;
                                result = CreateDefineCodeForYearAndUnion(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNum.ToString(), strUnionRule);
                            }
                        }
                        else
                        {
                            result = CreateDefineCodeForYearAndUnion(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNum.ToString(), strUnionRule);
                        }
                        //判断是否为不用生成后续编号规则 只生成部分 当SERIAL_NUMBER_BIT=0，SERIAL_START_NUM=0
                        bool blFlag = false;
                        if (!String.IsNullOrEmpty(dr["SERIAL_NUMBER_BIT"].ToString()) && !String.IsNullOrEmpty(dr["SERIAL_START_NUM"].ToString())
                            && dr["SERIAL_NUMBER_BIT"].ToString() != "0" && dr["SERIAL_START_NUM"].ToString() != "0")
                        {

                            blFlag = true;
                        }

                        if (!String.IsNullOrEmpty(result) && blFlag)
                        {
                            //将变号自动加1;
                            TBaseSerialruleVo objSerialUp = new TBaseSerialruleVo();
                            objSerialUp.ID = dr["ID"].ToString();
                            objSerialUp.SERIAL_START_NUM = (Convert.ToInt32(StartSerialNum) + 1).ToString();

                            new TBaseSerialruleLogic().Edit(objSerialUp);
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        #region 字符验证方法 Create By 胡方扬 2013-01-10
        /// <summary>
        /// 判断是否为数字类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string strInput)
        {
            bool result = true;
            for (int i = 0; i < strInput.Length; i++)
            {
                if (strInput[i] > '9' || strInput[i] < '0')
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否为数字或大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUpperAlphaNumeric(string strInput)
        {
            bool result = true;
            for (int i = 0; i < strInput.Length; i++)
            {
                if (!((strInput[i] >= '0' && strInput[i] <= '9') || (strInput[i] >= 'A' && strInput[i] <= 'Z')))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否为大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUpper(string strInput)
        {
            bool result = true;
            for (int i = 0; i < strInput.Length; i++)
            {
                if (!(strInput[i] >= 'A' && strInput[i] <= 'Z'))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        ///<summary>
        ///转半角的函数(DBC case) 
        ///</summary>
        ///<param name="input">任意字符串</param>
        ///<returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string strInput)
        {
            char[] c = strInput.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 导出数据为CSV、EXL格式 Create By 胡方扬 2013-05-31
        /// <summary>
        /// 导出成CSV格式的文件,用户在没有安装Excle情况下，使用CSV格式可以Excle格式查看数据，同时避免office版本问题
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="fileName">保存的路径</param>
        public static void ExportCSV(DataTable dataTable, string fileName)
        {
            if (dataTable == null)
                return;
            if (dataTable.Rows.Count > 65535)
            {
                throw new Exception("数据超过65535行！");
            }
            StringBuilder sb = new StringBuilder();
            for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
            {
                sb.Append(dataTable.Columns[colIndex].ColumnName).Append(",");
            }
            sb.Append("\r\n");
            for (int index = 0; index < dataTable.Rows.Count; index++)
            {
                for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                {
                    object obj = dataTable.Rows[index][colIndex];
                    if (obj is DBNull || obj == null)
                    {
                        sb.Append(",");
                    }
                    else
                    {
                        sb.Append(obj.ToString().Replace(',', ';')).Append(",");
                    }
                }
                sb.Append("\r\n");
            }
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                sw = new StreamWriter(fs, Encoding.GetEncoding("GBK"));
                sw.Write(sb.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }

        }
        #endregion

        #region 冒泡排序算法 Create By 胡方扬
        /// <summary>
        /// 创建原因：冒泡排序算法 
        /// 创建人：胡方扬
        /// 创建时间：2013-07-23
        /// </summary>
        /// <param name="IntNum"></param>
        /// <returns></returns>
        public int[] BubblingOrderNum(int[] IntNum)
        {

            int temp = 0;
            for (int i = 0; i < IntNum.Length - 1; i++)
            {
                for (int m = i + 1; m < IntNum.Length; m++)
                {
                    if (IntNum[m] < IntNum[i])
                    {
                        temp = IntNum[i];
                        IntNum[i] = IntNum[m];
                        IntNum[m] = temp;
                    }
                }
            }

            return IntNum;
        }
        #endregion

        /// <summary>
        /// 获取监测项目信息 Create By 魏林 2013-06-08
        /// </summary>
        /// <param name="strID">监测项目ID</param>
        /// <returns></returns>
        public static DataTable getItemInfo(string strID)
        {
            i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic com = new BusinessLogic.Channels.Env.Point.Common.CommonLogic();

            return com.getItemInfo(strID);
        }

        /// <summary>
        /// 获取页面对象信息(用于获取点位复制页面上的两个年份) Create By 魏林 2013-06-09
        /// </summary>
        /// <param name="request">request对象</param>
        /// <param name="SY">复制年份的控件ID</param>
        /// <param name="EY">年份的控件ID</param>
        /// <returns></returns>
        public List<string> getYearRequest(HttpRequest request, string SY, string EY)
        {
            List<string> list = new List<string>();

            list.Add(request[SY].ToString());
            list.Add(request[EY].ToString());
            return list;
        }

        /// <summary>
        /// Excel表格转换成DataTable
        /// </summary>
        /// <param name="sheet">表格</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            //handling header.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = GetCellValue(row.GetCell(j));
                    }
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.BLANK:
                    return string.Empty;
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue.ToString();
                case CellType.ERROR:
                    return cell.ErrorCellValue.ToString();
                case CellType.NUMERIC:
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.STRING:
                    return cell.StringCellValue;
                case CellType.FORMULA:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
        /// <summary>
        /// 在噪声采样环节新增测点或设置项目时保存监测项目的方法 Create By weilin
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <param name="strSampleIDs">测点ID集</param>
        /// <param name="strItemIDs">监测项目ID集</param>
        /// <param name="b">true:新增测点，false:设置监测项目</param>
        /// <returns></returns>
        public static bool SaveDataItem(string strSubtaskID, string strSampleIDs, string strItemIDs, bool b)
        {
            bool isSuccess = true;

            string[] arrItemID = strItemIDs.Split(',');
            string[] arrSampleID = strSampleIDs.Split(',');
            TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
            TMisMonitorTaskItemVo objTaskItem = new TMisMonitorTaskItemVo();
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();

            for (int i = 0; i < arrSampleID.Length; i++)
            {
                objSample = new TMisMonitorSampleInfoLogic().Details(arrSampleID[i].ToString());
                objTaskPoint = new TMisMonitorTaskPointLogic().Details(objSample.POINT_ID);
                //点位采用的标准条件项ID
                string strConditionID = "";
                if (!string.IsNullOrEmpty(objTaskPoint.NATIONAL_ST_CONDITION_ID))
                {
                    strConditionID = objTaskPoint.NATIONAL_ST_CONDITION_ID;
                }
                if (!string.IsNullOrEmpty(objTaskPoint.LOCAL_ST_CONDITION_ID))
                {
                    strConditionID = objTaskPoint.LOCAL_ST_CONDITION_ID;
                }
                if (!string.IsNullOrEmpty(objTaskPoint.INDUSTRY_ST_CONDITION_ID))
                {
                    strConditionID = objTaskPoint.INDUSTRY_ST_CONDITION_ID;
                }

                if (!b)
                {
                    TMisMonitorTaskItemVo objPointItemSet = new TMisMonitorTaskItemVo();
                    objPointItemSet.IS_DEL = "1";
                    TMisMonitorTaskItemVo objPointItemWhere = new TMisMonitorTaskItemVo();
                    objPointItemWhere.IS_DEL = "0";
                    objPointItemWhere.TASK_POINT_ID = objSample.POINT_ID;
                    new TMisMonitorTaskItemLogic().Edit(objPointItemSet, objPointItemWhere);
                    objResult = new TMisMonitorResultVo();
                    objResult.SAMPLE_ID = arrSampleID[i].ToString();
                    new TMisMonitorResultLogic().Delete(objResult);
                }

                for (int j = 0; j < arrItemID.Length; j++)
                {
                    //项目采用的标准上限、下限
                    string strUp = "";
                    string strLow = "";
                    string strConditionType = "";
                    getStandardValue(arrItemID[j].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                    
                    objTaskItem = new TMisMonitorTaskItemVo();
                    objTaskItem.ID = GetSerialNumber("t_mis_monitor_task_item_id");
                    objTaskItem.IS_DEL = "0";
                    objTaskItem.TASK_POINT_ID = objSample.POINT_ID;
                    objTaskItem.ITEM_ID = arrItemID[j].ToString();
                    objTaskItem.CONDITION_ID = strConditionID;
                    objTaskItem.CONDITION_TYPE = strConditionType;
                    objTaskItem.ST_UPPER = strUp;
                    objTaskItem.ST_LOWER = strLow;
                    isSuccess = new TMisMonitorTaskItemLogic().Create(objTaskItem);

                    objResult = new TMisMonitorResultVo();
                    objResult.ID = GetSerialNumber("MonitorResultId");
                    objResult.SAMPLE_ID = objSample.ID;
                    objResult.ITEM_ID = arrItemID[j].ToString();
                    objResult.QC_TYPE = objSample.QC_TYPE;
                    objResult.RESULT_STATUS = "01";

                    //填充默认分析方法和方法依据
                    TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
                    objItemAnalysis.ITEM_ID = arrItemID[j].ToString();
                    objItemAnalysis.IS_DEFAULT = "是";
                    objItemAnalysis.IS_DEL = "0";
                    objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                    if (objItemAnalysis.ID.Length == 0)
                    {
                        objItemAnalysis.ITEM_ID = arrItemID[j].ToString();
                        objItemAnalysis.IS_DEL = "0";
                        objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                    }
                    //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                    objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                    objResult.RESULT_CHECKOUT = objItemAnalysis.LOWER_CHECKOUT;
                    //objResult.STANDARD_ID = objMethod.METHOD_ID;

                    isSuccess = new TMisMonitorResultLogic().Create(objResult);

                    string strAnalysisManagerID = "";
                    string strAnalysisManID = "";
                    TMisMonitorResultVo objResultTemp = new TMisMonitorResultVo();
                    objResultTemp.ID = objResult.ID;
                    DataTable dtManager = new TMisMonitorResultLogic().SelectManagerByTable(objResultTemp);
                    if (dtManager.Rows.Count > 0)
                    {
                        strAnalysisManagerID = dtManager.Rows[0]["ANALYSIS_MANAGER"].ToString();
                        strAnalysisManID = dtManager.Rows[0]["ANALYSIS_ID"].ToString();
                    }
                    TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                    objResultApp.ID = GetSerialNumber("MonitorResultAppId");
                    objResultApp.RESULT_ID = objResult.ID;
                    objResultApp.HEAD_USERID = strAnalysisManagerID;
                    objResultApp.ASSISTANT_USERID = strAnalysisManID;

                    isSuccess = new TMisMonitorResultAppLogic().Create(objResultApp);
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// 获取采用的标准项的上下限
        /// </summary>
        /// <param name="strItemID">项目ID</param>
        /// <param name="strConditionID">条件项ID</param>
        /// <param name="strUp">上限</param>
        /// <param name="strLow">下限</param>
        protected static void getStandardValue(string strItemID, string strConditionID, ref string strUp, ref string strLow, ref string strConditionType)
        {
            TBaseEvaluationConItemVo objConItemVo = new TBaseEvaluationConItemVo();
            objConItemVo.ITEM_ID = strItemID;
            objConItemVo.CONDITION_ID = strConditionID;
            objConItemVo.IS_DEL = "0";
            objConItemVo = new TBaseEvaluationConItemLogic().Details(objConItemVo);
            //上限处理
            if (objConItemVo.DISCHARGE_UPPER.Length > 0)
            {
                //上限单位
                string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.UPPER_OPERATOR, "logic_operator");
                if (strUnit.Length > 0)
                {
                    if (strUnit.IndexOf("≤") >= 0)
                    {
                        strUnit = "<=";
                    }
                    else if (strUnit.IndexOf("≥") >= 0)
                    {
                        strUnit = ">=";
                    }
                }
                if (objConItemVo.DISCHARGE_UPPER.Contains(","))
                {
                    string[] strValue = objConItemVo.DISCHARGE_UPPER.Split(',');
                    foreach (string str in strValue)
                    {
                        if (str.Length > 0)
                        {
                            strUp += (strUnit + str) + ",";
                        }
                    }
                    if (strUp.Length > 0)
                    {
                        strUp = strUp.Remove(strUp.LastIndexOf(","));
                    }
                }
                else
                {
                    strUp = strUnit + objConItemVo.DISCHARGE_UPPER;
                }
            }
            //下限处理
            if (objConItemVo.DISCHARGE_LOWER.Length > 0)
            {
                //下限单位
                string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.LOWER_OPERATOR, "logic_operator");
                if (strUnit.Length > 0)
                {
                    if (strUnit.IndexOf("≤") >= 0)
                    {
                        strUnit = "<=";
                    }
                    else if (strUnit.IndexOf("≥") >= 0)
                    {
                        strUnit = ">=";
                    }
                }
                if (objConItemVo.DISCHARGE_LOWER.Contains(","))
                {
                    string[] strValue = objConItemVo.DISCHARGE_LOWER.Split(',');
                    foreach (string str in strValue)
                    {
                        if (str.Length > 0)
                        {
                            strLow += (strUnit + str) + ",";
                        }
                    }
                    if (strLow.Length > 0)
                    {
                        strLow = strLow.Remove(strLow.LastIndexOf(","));
                    }
                }
                else
                {
                    strLow = strUnit + objConItemVo.DISCHARGE_LOWER;
                }
            }
            strConditionType = new TBaseEvaluationInfoLogic().Details(new TBaseEvaluationConInfoLogic().Details(strConditionID).STANDARD_ID).STANDARD_TYPE;
        }

        /// <summary>
        /// 获取下一报告的处理人ID Create by weilin 2014-04-04
        /// </summary>
        /// <param name="DictType"></param>
        /// <returns></returns>
        public static string getNextReportUserID(string DictType)
        {
            string nUserID = "";
            TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = "D2355FBCD1B545A", WF_ID = "RPT" });
            string[] strUserIDs = task.OPER_VALUE.TrimEnd('|').Split('|');

            TSysDictVo objDictVo = new TSysDictVo();
            objDictVo.DICT_CODE = "'" + DictType + "'";
            objDictVo = new TSysDictLogic().SelectByObject(objDictVo);
            string pUserID = objDictVo.REMARK1;

            for (int i = 0; i < strUserIDs.Length; i++)
            {
                if (pUserID == strUserIDs[i].ToString())
                {
                    if (i == strUserIDs.Length - 1)
                    {
                        nUserID = strUserIDs[0].ToString();
                        break;
                    }
                    else
                    {
                        nUserID = strUserIDs[i + 1].ToString();
                        break;
                    }
                }
                else
                {
                    nUserID = strUserIDs[0].ToString();
                }
            }
            objDictVo.REMARK1 = nUserID;
            new TSysDictLogic().Edit(objDictVo);

            return nUserID;
        }

        /// <summary>
        /// 根据频次拆分子任务 Create By weilin 2014-05-04
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public static bool SplitSampleByFreq(string strSubTaskID)
        {
            bool isSuccess = true;

            TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
            objSubtask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
            //子任务的REMARK5如果等于1表示该任务已经根据频次拆分过了
            if (objSubtask.REMARK5 != "1")
            {
                objSubtask.REMARK5 = "1";
                TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
                TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                int intFreq = 1, intSampleDay = 1;
                string strPointName = "";

                objSample.SUBTASK_ID = strSubTaskID;
                objSample.QC_TYPE = "0";
                DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTable(objSample);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["POINT_ID"].ToString() != "")
                    {
                        objSample = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());
                        strPointName = objSample.SAMPLE_NAME;
                        objResult = new TMisMonitorResultVo();
                        objResult.SAMPLE_ID = dt.Rows[i]["ID"].ToString();
                        List<TMisMonitorResultVo> lResultVo = new TMisMonitorResultLogic().SelectByObject(objResult, 0, 0);
                        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(dt.Rows[i]["POINT_ID"].ToString());
                        TMisContractPointVo objContractPoint = new TMisContractPointLogic().Details(objTaskPoint.CONTRACT_POINT_ID);
                        intSampleDay = int.Parse(objContractPoint.SAMPLE_DAY.ToString() == "" ? "1" : objContractPoint.SAMPLE_DAY.ToString());
                        intFreq = int.Parse(objContractPoint.SAMPLE_FREQ.ToString() == "" ? "1" : objContractPoint.SAMPLE_FREQ.ToString());
                        ///////////
                        if (intSampleDay != 1 || intFreq != 1)
                        {
                            for (int r = 0; r < intSampleDay; r++)
                            {
                                for (int s = 0; s < intFreq; s++)
                                {
                                    #region 样品
                                    if (r == 0 && s == 0)
                                    {
                                        objSample.SAMPLE_CODE = "";
                                        objSample.SAMPLE_NAME = strPointName + "(第" + (r + 1).ToString() + "天" + " 第" + (s + 1).ToString() + "次)";
                                        objSample.SAMPLE_COUNT = (r + 1).ToString();
                                        isSuccess = new TMisMonitorSampleInfoLogic().Edit(objSample);
                                    }
                                    else
                                    {
                                        objSample.ID = GetSerialNumber("MonitorSampleId");
                                        objSample.SAMPLE_CODE = "";
                                        objSample.SAMPLE_NAME = strPointName + "(第" + (r + 1).ToString() + "天" + " 第" + (s + 1).ToString() + "次)";
                                        objSample.SAMPLE_COUNT = (r + 1).ToString();
                                        isSuccess = new TMisMonitorSampleInfoLogic().Create(objSample);

                                        for (int u = 0; u < lResultVo.Count; u++)
                                        {
                                            objResult = lResultVo[u];
                                            objResultApp = new TMisMonitorResultAppVo();
                                            objResultApp.RESULT_ID = objResult.ID;
                                            objResultApp = new TMisMonitorResultAppLogic().Details(objResultApp);

                                            objResult.ID = GetSerialNumber("MonitorResultId");
                                            objResult.SAMPLE_ID = objSample.ID;
                                            isSuccess = new TMisMonitorResultLogic().Create(objResult);

                                            objResultApp.ID = GetSerialNumber("MonitorResultAppId");
                                            objResultApp.RESULT_ID = objResult.ID;
                                            isSuccess = new TMisMonitorResultAppLogic().Create(objResultApp);
                                        }
                                    }
                                    #endregion

                                }
                            }
                        }
                        ///////////
                    }
                }

                isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
            }
            return isSuccess;

        }

        /// <summary>
        /// 获取字符串中的数字 Create By weilin 2014-09-29
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>数字</returns>
        public static decimal GetNumber(string str)
        {
            decimal result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^\d.\d]", "");
                if (str != "")
                {
                    // 如果是数字，则转换为decimal类型
                    if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                    {
                        result = decimal.Parse(str);
                    }
                }
            }

            return result;
        }

        #region by yinchengyi 2015-9-7 关于日期的公共函数

        //获取时间日期方法
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        //推算日期
        public static string GetNextNDate(string strCurrentDate, string strSpace)
        {

            DateTime dt = Convert.ToDateTime(strCurrentDate);
            double dSpace = Convert.ToDouble(strSpace);

            dt = dt.AddDays(dSpace);

            return dt.ToString(("yyyy-MM-dd"));
        }
        #endregion

        #region Open Flash Chart报表方法
        /// <summary>
        /// 设置图表标题
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="Title">标题</param>
        public void SetFlashChartTitle(OpenFlashChart.OpenFlashChart chart, string Title)
        {
            chart.Title = new OpenFlashChart.Title(Title);
            chart.Title.Style = "{color:#505050;font-size:20px;font-family:Microsoft YaHei,Verdana; }";
            chart.Bgcolor = "#F1FFFF";
            chart.X_Axis.GridColour = "#FFFFFF";

        }
        /// <summary>
        /// 增加图表柱状图实例
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="source">数据源</param>
        /// <param name="legendname">图例名称</param>
        /// <param name="color">颜色</param>
        /// <param name="tooltip">鼠标提示</param>
        public void AddFlashChartBar(OpenFlashChart.OpenFlashChart chart, List<object> source, string legendname, string color, string tooltip)
        {
            OpenFlashChart.Bar bar = new OpenFlashChart.Bar();
            bar.Colour = color;
            bar.Alpha = 0.7;
            bar.Text = legendname;
            bar.FontSize = 12;
            bar.OnShowAnimation = new OpenFlashChart.Animation("grow-up", 1, 0.5);//动画效果
            bar.Values = source;
            chart.AddElement(bar);
            if (tooltip != "")
                bar.Tooltip = tooltip;
        }
        /// <summary>
        /// 增加图表线状图实例
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="source">数据源</param>
        /// <param name="legendname">图例名称</param>
        /// <param name="color">颜色</param>
        /// <param name="tooltip">鼠标提示</param>
        public void AddFlashChartLine(OpenFlashChart.OpenFlashChart chart, List<double> source, string legendname, string color, string tooltip)
        {
            OpenFlashChart.LineDot line = new OpenFlashChart.LineDot();
            //曲线绑定的数据
            line.Values = source;
            //曲线显示的动画效果
            /*"type":"shrink-in",    从大变小 */
            /* "type":"fade-in",     逐渐现形 */
            /* "type":"drop",        从上掉落 */
            /* "type":"mid-slide",   以中心线弯曲形成 */
            /* "type":"explode",     从中心弹出 */
            /* "type":"pop-up",      向上弹出 */
            line.OnShowAnimation = new OpenFlashChart.Animation("mid-slide", 1, 0.5);
            //线条颜色
            line.Colour = color;
            //线的粗细
            line.Width = 2;
            ////图例名称
            line.Text = legendname;
            chart.AddElement(line);
            if (tooltip != "")
                line.DotStyleType.Tip = tooltip;            //鼠标悬浮时显示
        }

        /// <summary>
        /// FlashChart的颜色集合
        /// </summary>
        public string[] OpenFlashChartColor = new string[12] {
           
            "#00A8F0",
            "#C0D800",
            "#CB4B4B",
            "#EEC139"
            ,"#A2BDD2"
            ,"#C6A33B"
            ,"#92A93F"
            ,"#C88656"
            ,"#389793"
            ,"#B15B5C"
            ,"#7B467A"
            ,"#6C884E"
        };
        /// <summary>
        /// 设置图表横轴对象
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="source">横轴对象数据源</param>
        public void SetFlashChartXAxis(OpenFlashChart.OpenFlashChart chart, OpenFlashChart.XAxisLabels source)
        {
            OpenFlashChart.XAxis xaxis = new OpenFlashChart.XAxis();
            xaxis.Colour = "#505050";
            xaxis.GridColour = "#FFFFFF";
            xaxis.Labels.FontSize = 13;
            xaxis.Labels = source;
            chart.X_Axis = xaxis;
        }
        /// <summary>
        /// 设置图表Y轴对象
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="minvalue">Y轴最小值</param>
        /// <param name="maxvalue">Y轴最大值</param>
        /// <param name="steps">每格数值增量</param>
        public void SetFlashChartYAxis(OpenFlashChart.OpenFlashChart chart, double minvalue, double maxvalue, int steps)
        {
            OpenFlashChart.YAxis yaxis = new OpenFlashChart.YAxis();
            yaxis.Colour = "#505050";
            yaxis.GridColour = "#C1C1C1";
            yaxis.Labels.Color = "#505050";
            yaxis.Min = minvalue;
            yaxis.Max = maxvalue;
            yaxis.Steps = steps;
            yaxis.Offset = false;
            chart.Y_Axis = yaxis;
        }
        /// <summary>
        /// 添加X轴名称
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="text">名称</param>
        public void AddFlashChartXLegend(OpenFlashChart.OpenFlashChart chart, string text)
        {
            chart.X_Legend = new OpenFlashChart.Legend(text);

            chart.X_Legend.Style = "{font-family:Microsoft YaHei,Verdana;font-size:18px;text-align:center;color:#5B7993}";

        }
        /// <summary>
        /// 添加Y轴名称
        /// </summary>
        /// <param name="chart">图表对象</param>
        /// <param name="text">名称</param>
        public void AddFlashChartYLegend(OpenFlashChart.OpenFlashChart chart, string text)
        {
            chart.Y_Legend = new OpenFlashChart.Legend(text);
            chart.Y_Legend.Style = "{font-family:Microsoft YaHei,Verdana;font-size:18px;text-align:center;color:#5B7993}";

        }
        /// <summary>
        /// 获取图表Y轴最大值与最小值
        /// </summary>
        /// <param name="minvalue">数据最小值</param>
        /// <param name="maxvalue">数据最大值</param>
        /// <param name="step">网格距离</param>
        public void GetFlashChartMinAndMaxValue(ref double minvalue, ref double maxvalue, ref int step)
        {
            int maxvaluetemp = (int)(maxvalue);
            int minvaluetemp = (int)minvalue;
            if (minvaluetemp < 10)
            {
                if (minvaluetemp < 0)
                {
                    //minvaluetemp = minvaluetemp;
                    int minvaluestand = int.Parse((minvaluetemp.ToString().Substring(0, minvaluetemp.ToString().Length - 1) + "5"));
                    if (minvaluetemp >= minvaluestand)
                    {
                        minvaluetemp = minvaluestand;
                    }
                    else
                    {
                        minvaluetemp = minvaluestand - 5;
                    }
                }
                else if (minvaluetemp < 5)
                    minvaluetemp = 0;
                else
                    minvaluetemp = 5;
            }
            else
            {
                int minvaluestand = int.Parse((minvaluetemp.ToString().Substring(0, minvaluetemp.ToString().Length - 1) + "5"));
                if (minvaluetemp >= minvaluestand)
                {
                    minvaluetemp = minvaluestand;
                }
                else
                {
                    minvaluetemp = minvaluestand - 5;
                }
            }
            if (maxvaluetemp < 10)
            {
                if (maxvaluetemp < 5)
                    maxvaluetemp = 5;
                else
                    maxvaluetemp = 10;
            }
            else
            {
                int maxvaluestand = int.Parse((maxvaluetemp.ToString().Substring(0, maxvaluetemp.ToString().Length - 1) + "5"));
                if (maxvaluetemp >= maxvaluestand)
                {
                    maxvaluetemp = maxvaluestand + 5;
                }
                else
                {
                    maxvaluetemp = maxvaluestand;
                }
            }
            int value = maxvaluetemp - minvaluetemp;
            if (value >= 100)
                step = value / 10;
            else if (value >= 30)
                step = 10;
            else if (value <= 10)
                step = 2;
            else
                step = 5;
            maxvalue = (double)maxvaluetemp;
            minvalue = (double)minvaluetemp;
        }
        #endregion
    }
}
