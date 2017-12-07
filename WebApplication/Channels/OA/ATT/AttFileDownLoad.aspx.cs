using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using i3.View;
using System.Data;
using System.Web.Services;

using System.IO;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

/// <summary>
/// 功能描述：附件下载功能
/// 创建日期：2011-11-27
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_ATT_AttFileDownLoad : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showDetail();
        }
    }
    protected void showDetail()
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        //======Begin 添加按照附加ID进行加载下载附件信息  胡方扬 2013-01-09========
        if (!String.IsNullOrEmpty(this.Request["strAttID"]))
        {
            TOaAttVo.ID = this.Request["strAttID"].Trim();
        }
        else
        {
            TOaAttVo.BUSINESS_ID = this.Request["id"].ToString();
            TOaAttVo.BUSINESS_TYPE = this.Request["filetype"].ToString();
        }
        //======End 添加按照附加ID进行加载下载附件信息  胡方扬 2013-01-09========
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        this.btnFileName.Text = TOaAttVoTemp.ATTACH_NAME + TOaAttVoTemp.ATTACH_TYPE;
        this.lblUploadDate.Text = TOaAttVoTemp.UPLOAD_DATE;
        this.lblUploadPerson.Text = TOaAttVoTemp.UPLOAD_PERSON;
        this.lblDescription.Text = TOaAttVoTemp.DESCRIPTION;
        this.hidden.Value = TOaAttVoTemp.UPLOAD_PATH;
    }
    protected void btnFileName_Click(object sender, EventArgs e)
    {
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string fileName = this.btnFileName.Text.Trim();//客户端保存的文件名 
        string filePath = mastPath + '\\' + this.hidden.Value;
        if (File.Exists(filePath) == false)
        {
            LigerDialogAlert("附件不存在，下载失败", "error"); return;
        }
        FileInfo fileInfo = new FileInfo(filePath);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(fileName));
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.WriteFile(fileInfo.FullName);
        Response.Flush();
        Response.End();
    }
}