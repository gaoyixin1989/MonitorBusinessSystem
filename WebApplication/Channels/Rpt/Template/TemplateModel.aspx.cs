using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;

public partial class Channels_Rpt_Template_TemplateModel : System.Web.UI.Page
{
    #region 相关变量
    /// <summary>
    /// 文件描述
    /// </summary>
    public string mDescript;
    /// <summary>
    /// 文件名称
    /// </summary>
    public string mFileName;
    /// <summary>
    /// 添加日期
    /// </summary>
    public string mFileDate;
    /// <summary>
    /// 是否可用
    /// </summary>
    public string mDisabled;
    /// <summary>
    /// 服务器路径
    /// </summary>
    public string mHttpUrl;
    /// <summary>
    /// 页面路径
    /// </summary>
    public string mScriptName;
    /// <summary>
    /// 服务器名称
    /// </summary>
    public string mServerName;
    /// <summary>
    /// 服务器路径
    /// </summary>
    public string mServerUrl;
    /// <summary>
    /// 文件ID
    /// </summary>
    public string mRecordID;
    /// <summary>
    /// 模板ID
    /// </summary>
    public string mTemplate;
    /// <summary>
    /// 文件类型
    /// </summary>
    public string mFileType;
    /// <summary>
    /// 编码类型
    /// </summary>
    public string mEditType;
    /// <summary>
    /// 用户名称
    /// </summary>
    public string mUserName;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mDisabled = "";
        mScriptName = "TemplateModel.aspx";
        mServerName = "OfficeServer.aspx";
        mHttpUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["SCRIPT_NAME"];
        mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length);

        mServerUrl = mHttpUrl + mServerName;
        mRecordID = Request.QueryString["FILE_ID"];
        mTemplate = Request.QueryString["TEMPLATE_ID"];
        mFileType = Request.QueryString["FILE_TYPE"];
        mEditType = Request.QueryString["EDIT_TYPE"];
        //mUserName   = Request.QueryString["USER_NAME"];
        //mFileName   = Request.QueryString["TEMPLATE_NAME"];

        //取得编号
        if (mRecordID == null)
        {
            //默认编号为libertine
            mRecordID = ObjectBase.SerialType.NullSerialNumber;
        }
        //取得模式
        if (mEditType == null)
        {
            //1表示编辑，0表示阅读
            mEditType = "1";
        }
        //取得类型
        if (mFileType == null)
        {
            // 默认为.doc文档
            mFileType = ".doc";
        }
        //取得用户名
        if (mUserName == null)
        {
            //默认为libertine
            mUserName = "libertine";
        }
        //取得模板
        if (mTemplate == null)
        {
            //默认模板为libertine
            mTemplate = ObjectBase.SerialType.NullSerialNumber;
        }
        if (mFileName == null)
        {
            mFileName = mTemplate + mFileType;
        }
        //编辑按钮是否可用
        if (mEditType.CompareTo("0") == 0)
        {
            mDisabled = "disabled";
        }
        else
        {
            mDisabled = "";
        }
    }


}