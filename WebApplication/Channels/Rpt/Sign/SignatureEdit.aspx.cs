using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using System.IO;
using i3.BusinessLogic.Sys.General;

/// <summary>
///功能描述：签名编辑
///创建时间：2012-12-3
///创建人：邵世卓
/// </summary>
public partial class Channels_Rpt_Sign_SignatureEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showDetail();
        }
    }

    /// <summary>
    /// 获得印章信息
    /// </summary>
    protected void showDetail()
    {
        TRptSignatureVo objSign = new TRptSignatureLogic().Details(this.Request["id"].ToString());
        this.MARK_NAME.Text = Request["real_name"].ToString();
        this.fileId.Value = objSign.ID;
        this.fileType.Value = objSign.MARK_TYPE;
    }

    /// <summary>
    /// 附件添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (this.MARK_NAME.Text.Trim() == "")
        {
            LigerDialogAlert("请输入印章名称", "error"); return;
        }
        if (this.fileUpload.PostedFile.ContentLength >= 20971520)
        {
            LigerDialogAlert("上传的文件不能大于20M", "error"); return;
        }
        if (Path.GetExtension(this.fileUpload.PostedFile.FileName).ToLower() != ".jpg")
        {
            LigerDialogAlert("请上传.jpg类型图片文件", "error"); return;
        }
        if (this.fileUpload.PostedFile.ContentLength <= 0)
        {
            LigerDialogAlert("请选择文件", "error"); return;
        }

        TRptSignatureVo objSign = new TRptSignatureLogic().Details(Request["id"]);
        //读取文件流
        byte[] UploadFile = new byte[fileUpload.PostedFile.ContentLength];
        fileUpload.PostedFile.InputStream.Read(UploadFile, 0, fileUpload.PostedFile.ContentLength);
        objSign.MARK_BODY = UploadFile;
        objSign.MARK_SIZE = fileUpload.PostedFile.ContentLength.ToString();
        objSign.MARK_TYPE = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);

        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        //完整路径
        string strResultPath = mastPath + "\\SignImg\\" + Request["user_id"].ToString() + ".jpg";
        objSign.MARK_PATH = strResultPath;

        string strid = Exist(Request["user_id"]);
        if (strid.Length > 0)
        {
            //设置ID
            //umuv.ID = strid;
            objSign.ID = strid;

            if (new TRptSignatureLogic().Edit(objSign))
            {
                WriteLog("编辑印章", "", LogInfo.UserInfo.USER_NAME + "编辑印章" + objSign.ID);
                LigerDialogAlert("印章编辑成功", "success");
                UploadAtt(strResultPath);
            }
            else
            {
                LigerDialogAlert("印章编辑失败", "error");
            }
        }
        else
        {
            //umuv.USER_ID = this.user_id.Value;
            objSign.USER_NAME = Request["user_id"];
            objSign.MARK_NAME = this.MARK_NAME.Text.Trim();
            objSign.ADD_TIME = DateTime.Now.ToString("yyyy-MM-dd");
            objSign.ADD_USER = LogInfo.UserInfo.REAL_NAME;
            objSign.ID = GetSerialNumber("Sign_Id");

            if (new TRptSignatureLogic().Create(objSign))
            {
                WriteLog("添加印章", "", LogInfo.UserInfo.USER_NAME + "添加印章" + objSign.ID);
                LigerDialogAlert("印章新增成功", "success");
            }
            else
            {
                LigerDialogAlert("印章新增失败", "error");
            }
        }
    }

    /// <summary>
    /// 数据存在 性
    /// </summary>
    /// <param name="strUser">用户名</param>
    /// <returns>数据ID</returns>
    protected string Exist(string strUser)
    {
        TRptSignatureVo objSign = new TRptSignatureVo();
        objSign.USER_NAME = strUser;
        objSign = new TRptSignatureLogic().Details(objSign);
        return objSign.ID;
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetDeptItems()
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysDictVo objVo = new TSysDictVo();
        objVo.DICT_TYPE = "dept";
        dt = new TSysDictLogic().SelectByTable(objVo);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获得用户数据集
    /// </summary>
    /// <param name="strDutyType">部门Code</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetUserList(string strDept)
    {
        DataTable dt = new TSysUserLogic().SelectByTableUnderDept(strDept, 0, 0);
        return LigerGridSelectDataToJson(dt, dt.Rows.Count);
    }

    protected void UploadAtt(string strResultPath)
    {
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string strfolderPath = mastPath + "\\SignImg\\";
        //删除原来文件
        //判断原来是否已经上传过文件，如果有的话则获取原来已经上传的文件路径
        TRptSignatureVo objSign = new TRptSignatureVo();
        objSign.USER_NAME = LogInfo.UserInfo.USER_NAME;
        DataTable objTable = new TRptSignatureLogic().SelectByTable(objSign);
        if (objTable.Rows.Count > 0)
        {
            //如果存在记录
            //获取该记录的ID
            string strId = objTable.Rows[0]["ID"].ToString();
            //获取原来文件的路径
            string strOldFilePath = objTable.Rows[0]["MARK_PATH"].ToString();
            //如果存在的话，删除原来的文件
            if (File.Exists(strOldFilePath))
                File.Delete(strOldFilePath);
        }
        //开始上传附件
        try
        {
            //判断文件夹是否存在，如果不存在则创建
            if (Directory.Exists(strfolderPath) == false)
                Directory.CreateDirectory(strfolderPath);
            this.fileUpload.SaveAs(strResultPath);
        }
        catch (Exception ex)
        {
            LigerDialogAlert(ex.Message, "error");
        }
    }
}