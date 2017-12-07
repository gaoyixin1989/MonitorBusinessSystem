using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class Channels_Env_ZZ_Fill_FillImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        bool isSuccess = false;
        string strType = Request["actions"];
        if (this.importFiles.PostedFile.ContentLength <= 0)
        {
            this.lable.Text = "请选择文件!";
            this.lable.Visible = true;
            return;
        }
        Stream stream = this.importFiles.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        switch (strType)
        {
            case "Lake": //湖库导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/LakeTemple.xml"), sheet);
                break;
            case "DrinkUnder": //地下饮用水导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/DrinkUnderTemple.xml"), sheet);
                break;
            case "River": //河流导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/RiverTemple.xml"), sheet);
                break;
            case "RiverCity"://城考
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/RiverCityTemple.xml"), sheet);
                break;
            case "RiverPlan"://规划断面
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/RiverPlanTemple.xml"), sheet);
                break;
            case "RiverTarget"://责任目标
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/RiverTarget.xml"), sheet);
                break;
            case "DrinkSource": //饮用水源地导入(地表)
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/DrinkSourceTemple.xml"), sheet);
                break;
            case "UnderDrinkSource": //饮用水源地导入(地下)
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/ZZ_Import/UnderDrinkSourceTemple.xml"), sheet);
                break;
            case "Rain"://降水
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/ZZ_Import/RainTemple.xml"), sheet);
                break;
            case "NoiseRoad": //道路交通噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/ZZ_Import/RoadNoiseTemple.xml"), sheet);
                break;
            case "NoiseFun": //功能区噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/ZZ_Import/FunctionNoiseTemple.xml"), sheet);
                break;
            case "NoiseArea": //区域环境噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/ZZ_Import/AreaNoiseTemple.xml"), sheet);
                break;
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
}