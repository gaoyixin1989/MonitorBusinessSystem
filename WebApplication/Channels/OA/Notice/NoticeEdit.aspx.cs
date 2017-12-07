using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using System.IO;

using i3.ValueObject.Channels.OA.Notice;
using i3.BusinessLogic.Channels.OA.Notice;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：公告编辑功能
/// 创建日期：2013-02-25
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_Notice_NoticeEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] == null)
        {
            this.formStatus.Value = "add";
            this.RELEASE_TIME.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.RELIEASER.Text = LogInfo.UserInfo.REAL_NAME;
            //this.RELEASE_TIME.Enabled = false;
            //this.btnFileUp.Visible = false;//黄进军修改
        }
        else
        {
            this.formStatus.Value = "update";
            this.formId.Value = this.Request["id"].ToString();
            //this.RELEASE_TIME.Enabled = false;
            //this.btnFileUp.Visible = true;
        }
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
        //增加数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "add")
        {
            strResult = frmAdd();
            Response.Write(strResult);
            Response.End();
        }
        //修改数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "update")
        {
            strResult = frmUpdate();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = this.Request["id"].ToString();
        TOaAttVo.BUSINESS_TYPE = "OA_NOTICE";
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        string strUrl = TOaAttVoTemp.UPLOAD_PATH;
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string filePath = mastPath + '\\' + strUrl;

        TOaNoticeVo TOaNoticeVo = new TOaNoticeVo();
        TOaNoticeVo.ID = Request["id"].ToString();
        TOaNoticeVo TOaNoticeVoTemp = new TOaNoticeLogic().Details(TOaNoticeVo);
        return ToJson(TOaNoticeVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaNoticeVo TOaNoticeVo = autoBindRequest(Request, new TOaNoticeVo());
        TOaNoticeVo.ID = GetSerialNumber("oa_notice_id");
        bool isSuccess = new TOaNoticeLogic().Create(TOaNoticeVo);
        if (isSuccess)
        {
            WriteLog("新增公告信息", "", LogInfo.UserInfo.USER_NAME + "增加公告信息" + TOaNoticeVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TOaNoticeVo TOaNoticeVo = autoBindRequest(Request, new TOaNoticeVo());
        TOaNoticeVo.ID = Request["id"].ToString();
        bool isSuccess = new TOaNoticeLogic().Edit(TOaNoticeVo);
        if (isSuccess)
        {
            loadPicToTemp();
            WriteLog("修改公告信息", "", LogInfo.UserInfo.USER_NAME + "修改公告信息" + TOaNoticeVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 将显示序号前10条任务存储在临时目录
    /// </summary>
    /// <returns></returns>
    private void loadPicToTemp()
    {
        DataTable dt = new TOaNoticeLogic().getTopTenData();
        //清空Image临时文件夹下所有的临时文件
        string serverPath = HttpRuntime.AppDomainAppPath + "Channels\\OA\\Notice\\Image";
        DirectoryInfo di = new DirectoryInfo(serverPath);
        if (di.Exists)
        {
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                try
                {
                    fi.Delete();
                }
                catch (Exception ex) { }
            }
        }
        //将文件库中的文件复制到临时文件目录下
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        foreach (DataRow row in dt.Rows)
        {
            string id = row["ID"].ToString();
            TOaAttVo TOaAttVo = new TOaAttVo();
            TOaAttVo.BUSINESS_ID = id;
            TOaAttVo.BUSINESS_TYPE = "OA_NOTICE";
            TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
            string strExtent = TOaAttVoTemp.ATTACH_TYPE;
            string Path = TOaAttVoTemp.UPLOAD_PATH;
            string sumPath = mastPath + '\\' + Path;
            try
            {
                File.Copy(sumPath, serverPath + "\\" + id + strExtent, true);
            }
            catch (Exception ex) { }
        }
    }
}