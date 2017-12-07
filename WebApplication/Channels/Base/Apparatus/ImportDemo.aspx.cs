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

using i3.BusinessLogic.Channels.Base.Apparatus;
using i3.ValueObject.Channels.Base.Apparatus;

public partial class Channels_Base_Apparatus_ImportDemo : PageBase
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

        TBaseApparatusInfoVo tav = new TBaseApparatusInfoVo();

        //string[] strArr;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tav.ID = GetSerialNumber("Apparatus_Id");//ID
            tav.IS_DEL = "0";//是否删除 0=否，1=是
            tav.APPARATUS_CODE = dt.Rows[i + 2][0].ToString().Trim();//仪器编号
            tav.NAME = dt.Rows[i + 2][1].ToString().Trim();//仪器名称
            tav.MODEL = dt.Rows[i + 2][3].ToString().Trim();//型号规格
            tav.MEASURING_RANGE = dt.Rows[i + 2][4].ToString().Trim();//测量范围
            tav.EXPANDED_UNCETAINTY = dt.Rows[i + 2][5].ToString().Trim();//不确定度
            tav.FITTINGS_PROVIDER = dt.Rows[i + 2][6].ToString().Trim();//供应商
            tav.SERIAL_NO = dt.Rows[i + 2][7].ToString().Trim();//出厂编号
            tav.BUY_TIME = dt.Rows[i + 2][8].ToString().Trim();//购买时间
            tav.REMARK2 = dt.Rows[i + 2][9].ToString().Trim();//金额
            tav.POSITION = dt.Rows[i + 2][10].ToString().Trim();//放置地点（新地点）
            tav.REMARK3 = dt.Rows[i + 2][11].ToString().Trim();//放置地点（旧地点）

            new TBaseApparatusInfoLogic().Create(tav);
        }
        labMsg.Text = "导入成功!!!!!!!!!!!!!!!!!!";
    }
}