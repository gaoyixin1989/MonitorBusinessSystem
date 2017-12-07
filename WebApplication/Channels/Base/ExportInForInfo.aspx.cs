using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections;
using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;

public partial class Channels_Base_ExportInForInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 导入程序

    private string strFileUploadPath = "~/Channels/ExportInFile/";
    protected void Btn_UpLoadFile_COD_Click(object sender, EventArgs e)
    {
        if (!UploadFileAndInputResultFromExcel())
        {
            return;
        }
        ExcelExportIn(this.UploadFileName.Text);
    }

    protected void ExcelExportIn(string excelFilePath)
    {
        try
        {
            #region excel 打开工作簿
            FileStream file = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet mySheet = hssfworkbook.GetSheetAt(0);
            #endregion

            //选择导入的信息
            string strExportIndex = this.SelectExportInfo.SelectedValue;
            //定义错误日志
            //string strLog = "";
            //定义错误数
            int ErrorCount = 0;
            //定义导入对象数组
            ArrayList arrVo = new ArrayList();
            if (strExportIndex == "0")//仪器
            {
                for (int i = 1; ; i++)
                {
                    IRow row = mySheet.GetRow(i);
                    if (row == null)
                        break;
                    string cell1 = row.GetCell(1) != null ? row.GetCell(1).ToString() : "";//名称
                    string cell2 = row.GetCell(2) != null ? row.GetCell(2).ToString() : "";//性别
                    string cell3 = row.GetCell(3) != null ? row.GetCell(3).ToString() : "";//年龄
                    string cell4 = row.GetCell(4) != null ? row.GetCell(4).ToString() : "";//出生
                    string cell5 = row.GetCell(5) != null ? row.GetCell(5).ToString() : "";//身份证
                    string cell6 = row.GetCell(6) != null ? row.GetCell(6).ToString() : "";//政治面貌
                    string cell7 = row.GetCell(7) != null ? row.GetCell(7).ToString() : "";//学历
                    string cell8 = row.GetCell(8) != null ? row.GetCell(8).ToString() : "";//专业
                    string cell9 = row.GetCell(9) != null ? row.GetCell(9).ToString() : "";//职称
                    string cell10 = row.GetCell(10) != null ? row.GetCell(10).ToString() : "";//城市
                    string cell11 = row.GetCell(11) != null ? row.GetCell(11).ToString() : "";//单位
                    string cell12 = row.GetCell(12) != null ? row.GetCell(12).ToString() : "";//参加工作时间

                    TBaseApparatusInfoVo objvo = new TBaseApparatusInfoVo();
                    objvo.NAME = cell1.ToString().Replace("\n", "").Replace(" ", "");//仪器名称
                    objvo.MODEL = cell2.ToString().Replace("\n", "").Replace(" ", "");//规格
                    objvo.SERIAL_NO = cell3.ToString().Replace("\n", "").Replace(" ", "");//出厂编号
                    objvo.APPARATUS_PROVIDER = cell4.ToString().Replace("\n", "").Replace(" ", "");//生产商
                    objvo.BUY_TIME = cell6.ToString().Replace("\n", "").Replace(" ", "");//购置时间
                    objvo.APPARATUS_CODE = cell8.ToString().Replace("\n", "").Replace(" ", "");//管理编号
                    objvo.POSITION = cell10.ToString().Replace("\n", "").Replace(" ", "");//地点
                    objvo.KEEPER = cell11.ToString().Replace("\n", "").Replace(" ", "");//保管人
                    objvo.REMARK1 = cell12.ToString().Replace("\n", "").Replace(" ", "");//备注

                    objvo.IS_DEL = "0";

                    if (cell1.ToString() == "")
                        break;

                    if (!ItemExist(objvo))
                        arrVo.Add(objvo);
                }
                //批量保存数据
                if (ErrorCount <= 0)
                {
                    if (new TBaseApparatusInfoLogic().SaveData(arrVo))
                    {
                        Alert("导入完成！");
                    }
                }
            }
            else if (strExportIndex == "1")//点位
            {
                for (int i = 1; ; i++)
                {
                    IRow row = mySheet.GetRow(i);
                    if (row == null)
                        break;
                    string cell0 = row.GetCell(0) != null ? row.GetCell(0).ToString().Replace("\n", "").Replace(" ", "") : "";//企业ID
                    string cell1 = row.GetCell(1) != null ? row.GetCell(1).ToString().Replace("\n", "").Replace(" ", "") : "";//企业名称
                    string cell2 = row.GetCell(2) != null ? row.GetCell(2).ToString().Replace("\n", "").Replace(" ", "") : "";//点位信息

                    if (cell2.Length > 0)
                    {
                        int intNum = 0;//点位序号
                        string[] strMonitor = cell2.Split(';');//监测类别
                        foreach (string str in strMonitor)
                        {
                            string[] strPoint = str.Split(':');//监测类别与点位分隔
                            //点位信息
                            string[] strPointInfo = strPoint[1].ToString().Split('、');
                            foreach (string Point in strPointInfo)
                            {
                                //点位信息处理
                                TBaseCompanyPointVo objvo = new TBaseCompanyPointVo();

                                intNum++;
                                objvo.MONITOR_ID = strPoint[0].ToString();
                                objvo.POINT_NAME = Point;
                                objvo.POINT_TYPE = "01";
                                objvo.FREQ = "1";
                                objvo.COMPANY_ID = cell0.ToString();
                                objvo.IS_DEL = "0";
                                objvo.NUM = intNum.ToString();
                                arrVo.Add(objvo);
                            }
                        }
                    }
                    if (cell0.ToString() == "")
                        break;
                }
                //批量保存数据
                if (ErrorCount <= 0)
                {
                    if (new TBaseCompanyPointLogic().SaveData(arrVo))
                    {
                        Alert("导入完成！");
                    }
                }
            }
            #region excel 关闭 释放资源

            System.GC.Collect();

            #endregion
        }
        catch (Exception ex)
        {
            Alert("<script>alert('未能读取Excel，请稍候再试！" + ex.Message + "')</script>");
        }
    }


    /// <summary>
    /// 判断存在性
    /// </summary>
    /// <param name="strType"></param>
    /// <param name="strItem"></param>
    /// <param name="strMethod"></param>
    protected bool ItemExist(TBaseApparatusInfoVo objItemVo)
    {
        return new TBaseApparatusInfoLogic().GetSelectResultCount(objItemVo) > 0 ? true : false;
    }

    /// <summary>
    /// 判断存在性
    /// </summary>
    /// <param name="strType"></param>
    /// <param name="strItem"></param>
    /// <param name="strMethod"></param>
    protected bool PointExist(TBaseCompanyPointVo objItemVo)
    {
        return new TBaseCompanyPointLogic().GetSelectResultCount(objItemVo) > 0 ? true : false;
    }

    private bool UploadFileAndInputResultFromExcel()
    {
        //判断用户是否选择了文件
        if (FileUpload1.HasFile)
        {
            //调用自定义方法判断文件类型否符合
            if (IsAllowableFileType())
            {
                //如果上传文件夹不存在,则创建一个
                if (!Directory.Exists(Server.MapPath(strFileUploadPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(strFileUploadPath));
                }

                //从UploadFile控件中读取文件名
                string strFileName = FileUpload1.FileName;
                //组合成物理路径
                string strFilePhysicalPath = Server.MapPath(strFileUploadPath + "/") + GetNowTimeFileName() + strFileName;
                //判断文件是否存在
                if (!File.Exists(strFilePhysicalPath))
                {
                    //保存文件
                    FileUpload1.SaveAs(strFilePhysicalPath);
                    this.UploadFileName.Text = strFilePhysicalPath;
                    return true;
                }
                else
                {
                    base.Alert("文件已经存在！");
                    return false;
                }
            }
            else
            {
                base.Alert("上传的原始数据文件必须是Excel2003文件！");
                return false;
            }
        }
        else
        {
            base.Alert("请选择要上传的原始数据文件！");
            return false;
        }
    }

    private string GetNowTimeFileName()
    {
        string strY = DateTime.Now.Year.ToString();
        string strM = (DateTime.Now.Month.ToString().Length > 1 ? "" : "0") + DateTime.Now.Month.ToString();
        string strD = (DateTime.Now.Day.ToString().Length > 1 ? "" : "0") + DateTime.Now.Day.ToString();
        string strH = (DateTime.Now.Hour.ToString().Length > 1 ? "" : "0") + DateTime.Now.Hour.ToString();
        string strmm = (DateTime.Now.Minute.ToString().Length > 1 ? "" : "0") + DateTime.Now.Minute.ToString();
        string strS = (DateTime.Now.Second.ToString().Length > 1 ? "" : "0") + DateTime.Now.Second.ToString();

        string str = strY + strM + strD + strH + strmm + strS;
        return str;
    }

    #region 判断文件类型限制
    protected bool IsAllowableFileType()
    {
        string strFileTypeLimit = ".xls";
        //当前文件扩展名是否包含在这个字符串中
        if (strFileTypeLimit.IndexOf(Path.GetExtension(FileUpload1.FileName).ToLower()) >= 0)
            return true;
        else
            return false;
    }
    #endregion
    #endregion
}