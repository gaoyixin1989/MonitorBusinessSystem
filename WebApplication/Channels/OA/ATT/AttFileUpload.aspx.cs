using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using System.IO;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

/// <summary>
/// 功能描述：附件上传功能
/// 创建日期：2011-11-26
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_ATT_AttFileUpload : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.status.Value = "3";
        if (!Page.IsPostBack)
        {
            showDetail();
        }
    }
    protected void showDetail()
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = this.Request["id"].ToString();
        TOaAttVo.BUSINESS_TYPE = this.Request["filetype"].ToString();
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        this.ATTACH_NAME.Text = TOaAttVoTemp.ATTACH_NAME;
        this.DESCRIPTION.Text = TOaAttVoTemp.DESCRIPTION;
    }
    protected void btnFileClear_Click(object sender, EventArgs e)
    {
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_TYPE = this.Request["filetype"].ToString();
        TOaAttVo.BUSINESS_ID = this.Request["id"].ToString();
        //获取路径信息
        DataTable objTable = new TOaAttLogic().SelectByTable(TOaAttVo);
        if (objTable.Rows.Count > 0)
        {
            try
            {
                //获取该记录的ID
                string strId = objTable.Rows[0]["ID"].ToString();
                //获取原来文件的路径
                string strOldFilePath = objTable.Rows[0]["UPLOAD_PATH"].ToString();
                //如果存在的话，删除原来的文件
                if (File.Exists(mastPath + "\\" + strOldFilePath))
                    File.Delete(mastPath + "\\" + strOldFilePath);
                //删除数据库信息
                if (new TOaAttLogic().Delete(TOaAttVo))
                {
                    this.status.Value = "2";
                    LigerDialogAlert("附件清除成功", "success");
                }
                else
                {
                    this.status.Value = "0";
                    LigerDialogAlert("附件清除失败", "error");
                }
            }
            catch (Exception ex)
            {
                LigerDialogAlert(ex.Message, "error");
            }
        }
        else
        {
            LigerDialogAlert("不存在附件信息", "error");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string fill_Row_ID = string.Empty;
        if (this.ATTACH_NAME.Text.Trim() == "")
        {
            LigerDialogAlert("请输入文件名称", "error"); return;
        }
        if (this.fileUpload.PostedFile.ContentLength >= 20971520)
        {
            LigerDialogAlert("上传的文件不能大于20M", "error"); return;
        }
        if (this.fileUpload.PostedFile.ContentLength <= 0)
        {
            LigerDialogAlert("请选择文件", "error"); return;
        }
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        //获取业务Id
        string strBusinessId = this.Request["id"].ToString();
        //获取业务类型
        string strBusinessType = this.Request["filetype"].ToString();
        //获取完整文件名称
        string strFullName = this.fileUpload.PostedFile.FileName;
        //获取填报行ID
        if (strBusinessType.Equals("SeaFill") || strBusinessType.Equals("DrinkSource") || strBusinessType.Equals("Payfor"))
        {
            fill_Row_ID = this.Request["ROW_ID"].ToString();
        }
        //获取文件扩展名称
        string strExtendName = strFullName.Substring(strFullName.LastIndexOf("."));
        string strSerialNumber = GetSerialNumber("attFileId");
        //文件夹路径
        string strfolderPath = strBusinessType + "\\" + DateTime.Now.ToString("yyyyMMdd");
        //新命名的文件名称
        string strNewFileName = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + strSerialNumber + strExtendName;
        //上传的完整路径
        string strResultPath = mastPath + "\\" + strfolderPath + "\\" + strNewFileName;
        //开始上传附件
        try
        {
            //判断文件夹是否存在，如果不存在则创建
            if (Directory.Exists(mastPath + "\\" + strfolderPath) == false)
                Directory.CreateDirectory(mastPath + "\\" + strfolderPath);
            this.fileUpload.SaveAs(strResultPath);

            //判断原来是否已经上传过文件，如果有的话则获取原来已经上传的文件路径
            TOaAttVo TOaAttVo = new TOaAttVo();
            TOaAttVo.BUSINESS_TYPE = strBusinessType;
            TOaAttVo.BUSINESS_ID = strBusinessId;
            //TOaAttVo.ATTACH_NAME = this.ATTACH_NAME.Text.Trim();
            DataTable objTable = new TOaAttLogic().SelectByTable(TOaAttVo);
            if (objTable.Rows.Count > 0)
            {
                //如果存在记录
                //获取该记录的ID
                string strId = objTable.Rows[0]["ID"].ToString();
                //获取原来文件的路径
                string strOldFilePath = objTable.Rows[0]["UPLOAD_PATH"].ToString();
                //如果存在的话，删除原来的文件
                if (File.Exists(mastPath + "\\" + strOldFilePath))
                    File.Delete(mastPath + "\\" + strOldFilePath);
                //将新的信息写入数据库
                TOaAttVo TOaAttVoTemp = new TOaAttVo();
                TOaAttVoTemp.ID = strId;
                TOaAttVoTemp.ATTACH_NAME = this.ATTACH_NAME.Text.Trim();
                TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                TOaAttVoTemp.UPLOAD_PERSON = LogInfo.UserInfo.REAL_NAME;
                TOaAttVoTemp.DESCRIPTION = this.DESCRIPTION.Text.Trim();
                TOaAttVoTemp.REMARKS = this.fileUpload.PostedFile.ContentLength.ToString() + "KB";//文件的大小
                TOaAttVoTemp.FILL_ID = fill_Row_ID;//行ID
                if (new TOaAttLogic().Edit(TOaAttVoTemp))
                {
                    this.status.Value = "1";
                    LigerDialogAlert("文件上传成功", "success");
                }
                else
                {
                    this.status.Value = "0";
                    LigerDialogAlert("文件上传失败", "error");
                }
            }
            else
            {
                //如果不存在记录
                TOaAttVo TOaAttVoTemp = new TOaAttVo();
                TOaAttVoTemp.ID = strSerialNumber;
                TOaAttVoTemp.BUSINESS_ID = strBusinessId;
                TOaAttVoTemp.BUSINESS_TYPE = strBusinessType;
                TOaAttVoTemp.ATTACH_NAME = this.ATTACH_NAME.Text.Trim();
                TOaAttVoTemp.ATTACH_TYPE = strExtendName;
                TOaAttVoTemp.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                TOaAttVoTemp.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                TOaAttVoTemp.UPLOAD_PERSON = LogInfo.UserInfo.REAL_NAME;
                TOaAttVoTemp.DESCRIPTION = this.DESCRIPTION.Text.Trim();
                TOaAttVoTemp.REMARKS = this.fileUpload.PostedFile.ContentLength.ToString() + "KB";//文件的大小
                TOaAttVoTemp.FILL_ID = fill_Row_ID;
                if (new TOaAttLogic().Create(TOaAttVoTemp))
                {
                    this.status.Value = "1";
                    LigerDialogAlert("文件上传成功", "success");
                }
                else
                {
                    this.status.Value = "0";
                    LigerDialogAlert("文件上传失败", "error");
                }
            }
        }
        catch (Exception ex)
        {
            LigerDialogAlert(ex.Message, "error");
        }
    }
}