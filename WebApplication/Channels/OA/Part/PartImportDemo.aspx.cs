using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using i3.ValueObject.Sys.Resource;

public partial class Channels_OA_Part_PartImportDemo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {

        bool isSuccess = false;
        if (this.importFiles.PostedFile.ContentLength <= 0)
        {
            this.lable.Text = "请选择文件!";
            this.lable.Visible = true;
            return;
        }
        Stream stream = this.importFiles.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.Autobreaks = true;
        int rowNumber = sheet.LastRowNum;

        for (int i = 1; i <= rowNumber; i++)
        {
            string strPART_CODE = sheet.GetRow(i).GetCell(2).ToString();
            string strPART_NAME = sheet.GetRow(i).GetCell(3).ToString();
            string strMODELS = sheet.GetRow(i).GetCell(6).ToString();

            if (string.IsNullOrEmpty(strPART_CODE) && string.IsNullOrEmpty(strPART_NAME)&&string.IsNullOrEmpty(strMODELS))
            {
                break;
            }
            TOaPartInfoVo obj = new TOaPartInfoVo();
            obj.PART_CODE = strPART_CODE;
            obj.PART_NAME = strPART_NAME;
            obj.MODELS = strMODELS;
            obj.IS_DEL = "0";
            obj = new TOaPartInfoLogic().SelectByObject(obj);
            if (obj.ID == "")
            {
                TOaPartInfoVo objPart = new TOaPartInfoVo();
                objPart.ID = GetSerialNumber("t_oa_part_info_id");
                objPart.IS_DEL = "0";
                objPart.PART_CODE = strPART_CODE;
                objPart.PART_NAME = strPART_NAME;
                objPart.MODELS = strMODELS;
                objPart.PART_TYPE = GetDict(sheet.GetRow(i).GetCell(5).ToString());
                objPart.UNIT = sheet.GetRow(i).GetCell(7).ToString();
                objPart.INVENTORY = sheet.GetRow(i).GetCell(10).ToString();
                objPart.MEDIUM = "";
                objPart.PURE = "";
                objPart.ALARM = sheet.GetRow(i).GetCell(11).ToString();
                objPart.USEING = "";
                objPart.REQUEST = "";
                objPart.NARURE = "";
                objPart.REMARK1 = sheet.GetRow(i).GetCell(4).ToString();
                objPart.REMARK2 = sheet.GetRow(i).GetCell(8).ToString();
                objPart.REMARK3 = sheet.GetRow(i).GetCell(9).ToString();
                isSuccess = new TOaPartInfoLogic().Create(objPart);
            }
            else {
                continue;
            }
        }
        if (isSuccess)
        {
            this.lable.Text = "导入成功！";
            this.lable.Visible = true;

        }
        else
        {
            this.lable.Text = "导入失败！";
            this.lable.Visible = true;
        }
    }

    //获取字典项
    private string GetDict(string strTYPE)
    {
        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.DICT_TYPE = "PART_TYPE";
        DataTable dt = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().SelectByTable(TSysDictVo);
        DataRow[] rows = dt.Select("DICT_TEXT='"+strTYPE+"'");
        string type = rows[0]["DICT_CODE"].ToString();
        return type;
    }

}