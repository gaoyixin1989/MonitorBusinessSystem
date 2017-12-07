using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;

public partial class Channels_Rpt_Template_FileEdit_bak : System.Web.UI.Page
{
    #region iWebOffice控件变量
    #region 基础信息
    /// <summary>
    /// 文档标题
    /// </summary>
    public string mSubject;
    /// <summary>
    /// 文档状态
    /// </summary>
    public string mStatus;
    /// <summary>
    /// 文档作者
    /// </summary>
    public string mAuthor;
    /// <summary>
    /// 文档名称
    /// </summary>
    public string mFileName;
    /// <summary>
    /// 文档日期
    /// </summary>
    public string mFileDate;
    /// <summary>
    /// HTML路径
    /// </summary>
    public string mHTMLPath;
    /// <summary>
    /// 是否可用
    /// </summary>
    #endregion
    #region 控件全局变量
    public string mDisabled;
    /// <summary>
    /// HTTP路径
    /// </summary>
    public string mHttpUrl;
    /// <summary>
    /// 脚本位置
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
    #endregion
    #region 文档相关控件
    /// <summary>
    /// 文档编号
    /// </summary>
    public string mRecordID;
    /// <summary>
    /// 模板编号
    /// </summary>
    public string mTemplate;
    /// <summary>
    /// 文档类型
    /// </summary>
    public string mFileType;
    /// <summary>
    /// 编辑类别
    /// </summary>
    public string mEditType;
    /// <summary>
    /// 任务编号
    /// </summary>
    public string mTask;
    /// <summary>
    /// 是否保护文档
    /// </summary>
    public string mProtect;
    /// <summary>
    /// 用户名称
    /// </summary>
    public string mUserName;
    /// <summary>
    /// 是否Word
    /// </summary>
    public string mWord;
    /// <summary>
    /// 是否Excel
    /// </summary>
    public string mExcel;
    #endregion
    /// <summary>
    /// 环节信息
    /// </summary>
    public string mReportWf;
    /// <summary>
    /// 签发人职务
    /// </summary>
    public string mAppUser_Position;
    /// <summary>
    /// 监测类别   未出综合报告前，报告分类别出，临时修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public string mItemType;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWebOffice();
    }

    private void SetWebOffice()
    {
        try
        {
            mHTMLPath = "";
            mDisabled = "";
            mScriptName = "FileEdit.aspx";
            mServerName = "OfficeServer.aspx";
            mHttpUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["SCRIPT_NAME"];
            mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length);
            mServerUrl = mHttpUrl + mServerName;
            mRecordID = Request.QueryString["FILE_ID"];
            mTemplate = Request.QueryString["Template_Id"];
            mFileType = Request.QueryString["FILE_TYPE"];
            mEditType = Request.QueryString["EDIT_TYPE"];
            mUserName = Request.QueryString["USER_NAME"];
            mTask = Request.QueryString["Task_Id"];
            mProtect = Request.QueryString["PROTECT"];
            //未出综合报告前，报告分类别出，临时修改
            mItemType = Request.QueryString["ITEM_TYPE"];

            mReportWf = Request.QueryString["ReportWf"];

            mAppUser_Position = new TSysDictLogic().GetDictNameByDictCodeAndType(((UserLogInfo)Session["Cache:Operator"]).UserInfo.DUTY_CODE, "duty_code");
        }
        catch { }
        //环节信息
        if (mReportWf == null)
        {
            mReportWf = "";
        }

        //编号
        if (mRecordID == null)
        {
            mRecordID = "";
        }
        //模式
        if (mEditType == null)
        {
            //0-阅读 1-修改[无痕迹] 2-修改[有痕迹] 3-核稿
            mEditType = "1";
        }
        //类型
        if (mFileType == null)
        {
            // 默认为.doc文档
            mFileType = ".doc";
        }
        //用户名
        if (mUserName == null)
        {
            //默认为libertine
            mUserName = "libertine";
        }
        //模板
        if (mTemplate == null)
        {
            mTemplate = "";
        }
        //文档状态相关
        if (null != mStatus)
        {
            //只读状态
            if (mStatus.CompareTo("EDIT") == 0)
            {
                mEditType = "1";
            }
            //可读状态
            if (mStatus.CompareTo("READ") == 0)
            {
                mEditType = "0";
            }
            //起草
            if (mStatus.CompareTo("DERF") == 0)
            {
                mEditType = "1";
            }
        }
        //可用
        if (mEditType.CompareTo("0") == 0)
        {
            mDisabled = "disabled";
        }
        else
        {
            mDisabled = "";
        }
        //判断文档类型
        if (mFileType == ".doc")
        {
            mWord = "";
            mExcel = "disabled";
        }
        else
        {
            mWord = "disabled";
            mExcel = "";
        }
        //文档名称
        if (!String.IsNullOrEmpty(mRecordID) && !String.IsNullOrEmpty(mFileType))
        {
            mFileName = mRecordID + mFileType;
        }
        //是否保护
        if (String.IsNullOrEmpty(mProtect))
        {
            mProtect = "0";
        }
    }
}