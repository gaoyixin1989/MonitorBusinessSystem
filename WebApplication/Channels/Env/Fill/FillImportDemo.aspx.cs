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

public partial class Channels_Env_Fill_FillImportDemo : PageBase
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
            //LigerDialogAlert("请选择文件", "error"); return;
            this.lable.Text = "请选择文件!";
            this.lable.Visible = true;
            return;
        }
        Stream stream = this.importFiles.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        switch (strType)
        {
            case "Air"://环境空气（天）
                //isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/AirTemple.xml"), sheet);
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/AirTemple.xml"), sheet);
                break;
            case "AirHour"://环境空气（小时）
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/AirHourTemple.xml"), sheet);
                break;
            case "AirKs"://环境空气科室（天）
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/AirKsTemple.xml"), sheet);
                break;
            case "Dust"://降尘
                // isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/DustTemple.xml"), sheet);
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/DustTemple.xml"), sheet);
                break;
            case "Metal"://底泥重金属
                // isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/Metal.xml"), sheet);
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/Metal.xml"), sheet);
                break;
            case "Alkali"://硫酸盐化速率
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/AlkaliTemple.xml"), sheet);
                break;
            case "Rain"://降水
                //isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/RainTemple.xml"), sheet);
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/RainTemple.xml"), sheet); 
                break;
            case "OffShore": //近岸直排
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/OffShoreTemple.xml"), sheet);
                break;
            case "Sea": //近岸海域
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/Import/SeaTemple.xml"), sheet);
                break;
            case "Estuaries": //入海河口
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/Import/EstuariesTemple.xml"), sheet);
                break; 
            case "River": //河流导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/QY_Import/RiverTemple.xml"), sheet);
                break;
            case "NoiseFun": //功能区噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/FunctionNoiseTemple.xml"), sheet);
                break;
            case "NoiseRoad": //道路交通噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/RoadNoiseTemple.xml"), sheet);
                break;
            case "NoiseArea": //区域环境噪声导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../xmlTemp/QY_Import/AreaNoiseTemple.xml"), sheet);
                break;
            case "DrinkSource": //饮用水源地导入
                //isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/Import/DrinkSourceTemple.xml"), sheet);
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/QY_Import/DrinkSourceTemple.xml"), sheet);
                break;
            case "DrinkUnder": //湖库饮用水导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/QY_Import/DrinkUnderTemple.xml"), sheet);
                //isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/Import/DrinkUnderTemple.xml"), sheet);
                break;
            case "Lake": //湖库导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../xmlTemp/Import/LakeTemple.xml"), sheet);
                break;
            case "PolluteWater": //污染源常规（废水）导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().ExcelSpecial(Server.MapPath("../xmlTemp/Import/PolluteWater.xml"), sheet); 
                break;
            case "PolluteAir": //污染源常规（废气）导入
                isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().ExcelSpecialAir(Server.MapPath("../xmlTemp/Import/PolluteAir.xml"), sheet);
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