using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;

using i3.View;
using System.Collections;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;

public partial class Channels_Base_Company_ImportDemo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {

        bool isSuccess = false;
        if (this.importFiles.PostedFile.ContentLength <= 0)
        {
            return;
        }
        Stream stream = this.importFiles.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        string strSql = "";
        ArrayList list = new ArrayList();
        DataTable dt = RenderFromExcel(sheet, 0);
        TBaseCompanyInfoVo CompanyInfoVo = new TBaseCompanyInfoVo();
        string strCompanyID = "";
        TBaseCompanyPointVo CompanyPointVo = new TBaseCompanyPointVo();
        string strCompanyPointID = "";
        TBaseCompanyPointItemVo CompanyPointItemVo = new TBaseCompanyPointItemVo();
        string strCompanyPointItemID = "";
        string[] strArr;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strCompanyID = "";
            strCompanyPointID = "";
            strCompanyPointItemID = "";

            CompanyInfoVo = new TBaseCompanyInfoVo();
            CompanyInfoVo.COMPANY_NAME = dt.Rows[i][0].ToString().Trim();
            CompanyInfoVo.IS_DEL = "0";
            CompanyInfoVo = new TBaseCompanyInfoLogic().SelectByObject(CompanyInfoVo);
            strCompanyID = CompanyInfoVo.ID;
            if (strCompanyID == "")
            {
                strCompanyID = GetSerialNumber("Company_Id");
                CompanyInfoVo.ID = strCompanyID;
                CompanyInfoVo.COMPANY_NAME = dt.Rows[i][0].ToString().Trim();
                CompanyInfoVo.IS_DEL = "0";
                new TBaseCompanyInfoLogic().Create(CompanyInfoVo);
            }

            CompanyPointVo = new TBaseCompanyPointVo();
            CompanyPointVo.COMPANY_ID = strCompanyID;
            CompanyPointVo.POINT_NAME = dt.Rows[i][3].ToString().Trim();
            CompanyPointVo.MONITOR_ID = dt.Rows[i][4].ToString().Trim();
            CompanyPointVo.IS_DEL = "0";
            CompanyPointVo = new TBaseCompanyPointLogic().SelectByObject(CompanyPointVo);
            strCompanyPointID = CompanyPointVo.ID;
            if (strCompanyPointID == "")
            {
                strCompanyPointID = GetSerialNumber("t_base_company_point_id");
                CompanyPointVo.ID = strCompanyPointID;
                CompanyPointVo.COMPANY_ID = strCompanyID;
                CompanyPointVo.POINT_NAME = dt.Rows[i][3].ToString().Trim();
                CompanyPointVo.MONITOR_ID = dt.Rows[i][4].ToString().Trim();
                CompanyPointVo.SAMPLE_DAY = dt.Rows[i][1].ToString().Trim();
                CompanyPointVo.SAMPLE_FREQ = dt.Rows[i][2].ToString().Trim();
                CompanyPointVo.IS_DEL = "0";
                new TBaseCompanyPointLogic().Create(CompanyPointVo);
            }
            else
            {
                CompanyPointVo.SAMPLE_DAY = dt.Rows[i][1].ToString().Trim();
                CompanyPointVo.SAMPLE_FREQ = dt.Rows[i][2].ToString().Trim();
                new TBaseCompanyPointLogic().Edit(CompanyPointVo);
            }

            strArr = dt.Rows[i][5].ToString().Trim().Split(',');
            for (int j = 0; j < strArr.Length; j++)
            {
                strCompanyPointItemID = "";

                CompanyPointItemVo = new TBaseCompanyPointItemVo();
                CompanyPointItemVo.POINT_ID = strCompanyPointID;
                CompanyPointItemVo.ITEM_ID = strArr[j].ToString().Trim();
                CompanyPointItemVo.IS_DEL = "0";
                CompanyPointItemVo = new TBaseCompanyPointItemLogic().SelectByObject(CompanyPointItemVo);
                strCompanyPointItemID = CompanyPointItemVo.ID;
                if (strCompanyPointItemID == "")
                {
                    strCompanyPointItemID = GetSerialNumber("t_base_company_point_item_id");
                    CompanyPointItemVo.ID = strCompanyPointItemID;
                    CompanyPointItemVo.POINT_ID = strCompanyPointID;
                    CompanyPointItemVo.ITEM_ID = strArr[j].ToString().Trim();
                    CompanyPointItemVo.IS_DEL = "0";
                    new TBaseCompanyPointItemLogic().Create(CompanyPointItemVo);
                }
            }
            
        }
        labMsg.Text = "导入成功!!!!!!!!!!!!!!!!!!";
    }
}