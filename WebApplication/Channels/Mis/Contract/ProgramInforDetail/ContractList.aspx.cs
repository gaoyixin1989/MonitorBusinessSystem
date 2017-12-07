using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using System.Configuration;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

public partial class Channels_Mis_Contract_ContractList : System.Web.UI.Page
{
    private string task_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
    }
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
    }

    #region 委托书导出 胡方扬 2013-04-23
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("TempFile/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        //插入委托书单号
        sheet.GetRow(2).GetCell(6).SetCellValue("No:" + dt.Rows[0]["CONTRACT_CODE"].ToString());

        sheet.GetRow(4).GetCell(2).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());
        sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["CONTACT_NAME"].ToString());
        sheet.GetRow(4).GetCell(8).SetCellValue(dt.Rows[0]["PHONE"].ToString());
        sheet.GetRow(5).GetCell(2).SetCellValue(dt.Rows[0]["CONTACT_ADDRESS"].ToString());
        sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["POST"].ToString());
        DataTable dtDict = PageBase.getDictList("RPT_WAY");
        DataTable dtSampleSource = PageBase.getDictList("SAMPLE_SOURCE");
        string strWay = "", strSampleWay = ""; ;
        if (dtDict != null)
        {
            foreach (DataRow dr in dtDict.Rows)
            {
                strWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_CODE"].ToString() == dt.Rows[0]["RPT_WAY"].ToString())
                {
                    strWay += "■ ";
                }
                else
                {
                    strWay += "□ ";
                }
            }
        }
        if (dtSampleSource != null)
        {
            foreach (DataRow dr in dtSampleSource.Rows)
            {
                strSampleWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_TEXT"].ToString() == dt.Rows[0]["SAMPLE_SOURCE"].ToString())
                {
                    strSampleWay += "■ ";
                }
                else
                {
                    strSampleWay += "□ ";
                }
            }
        }
        sheet.GetRow(5).GetCell(8).SetCellValue(strWay);
        sheet.GetRow(7).GetCell(2).SetCellValue(strSampleWay);
        sheet.GetRow(8).GetCell(2).SetCellValue(dt.Rows[0]["TEST_PURPOSE"].ToString());
        sheet.GetRow(9).GetCell(2).SetCellValue(dt.Rows[0]["PROVIDE_DATA"].ToString());
        sheet.GetRow(11).GetCell(2).SetCellValue(dt.Rows[0]["OTHER_ASKING"].ToString());
        sheet.GetRow(16).GetCell(1).SetCellValue(dt.Rows[0]["MONITOR_ACCORDING"].ToString());
        sheet.GetRow(20).GetCell(1).SetCellValue(dt.Rows[0]["REMARK2"].ToString());
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测协议书-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion

    #region 清远委托书导出 魏林 2014-02-16
    protected void btnExport_QY_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        string strWorkContent = "";

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("TempFile/QY/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        //插入委托书单号
        sheet.GetRow(2).GetCell(6).SetCellValue("No:" + dt.Rows[0]["CONTRACT_CODE"].ToString());

        sheet.GetRow(4).GetCell(2).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());   //委托单位
        sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["CONTACT_NAME"].ToString());   //联系人  
        sheet.GetRow(4).GetCell(8).SetCellValue(dt.Rows[0]["PHONE"].ToString());          //联系电话
        sheet.GetRow(5).GetCell(2).SetCellValue(dt.Rows[0]["CONTACT_ADDRESS"].ToString()); //地址
        sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["POST"].ToString());            //邮编

        sheet.GetRow(9).GetCell(2).SetCellValue(dt.Rows[0]["TESTED_COMPANY_NAME"].ToString());   //受检单位
        sheet.GetRow(9).GetCell(4).SetCellValue(dt.Rows[0]["TESTED_PHONE"].ToString());          //联系电话
        sheet.GetRow(9).GetCell(8).SetCellValue(dt.Rows[0]["TESTED_CONTACT_ADDRESS"].ToString()); //地址
        sheet.GetRow(9).GetCell(6).SetCellValue(dt.Rows[0]["TESTED_POST"].ToString());            //邮编

        DataTable dtDict = PageBase.getDictList("RPT_WAY");
        DataTable dtSampleSource = PageBase.getDictList("SAMPLE_SOURCE");
        string strWay = "", strSampleWay = ""; ;
        if (dtDict != null)
        {
            foreach (DataRow dr in dtDict.Rows)
            {
                strWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_CODE"].ToString() == dt.Rows[0]["RPT_WAY"].ToString())
                {
                    strWay += "■ ";
                }
                else
                {
                    strWay += "□ ";
                }
            }
        }
        if (dtSampleSource != null)
        {
            foreach (DataRow dr in dtSampleSource.Rows)
            {
                strSampleWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_TEXT"].ToString() == dt.Rows[0]["SAMPLE_SOURCE"].ToString())
                {
                    strSampleWay += "■ ";
                }
                else
                {
                    strSampleWay += "□ ";
                }
            }
        }
        sheet.GetRow(5).GetCell(8).SetCellValue(strWay);               //领取方式
        sheet.GetRow(6).GetCell(2).SetCellValue(strSampleWay);         //监测类型
        sheet.GetRow(7).GetCell(2).SetCellValue(dt.Rows[0]["TEST_PURPOSE"].ToString());   //监测目的
        sheet.GetRow(8).GetCell(2).SetCellValue(dt.Rows[0]["PROVIDE_DATA"].ToString());   //提供资料
        sheet.GetRow(10).GetCell(2).SetCellValue(dt.Rows[0]["OTHER_ASKING"].ToString());  //其他要求
        sheet.GetRow(15).GetCell(1).SetCellValue(dt.Rows[0]["MONITOR_ACCORDING"].ToString());//监测依据
        sheet.GetRow(20).GetCell(1).SetCellValue(dt.Rows[0]["REMARK2"].ToString());        //备注

        string strExplain = @"1.是否有分包：□是[□电话确认；□其它：         ] □否 
  是否使用非标准方法：  □是  □否
2.监测收费参照广东省物价局粤价函[1996]64号文规定执行。委托单位到本站办公室（603室）领取《清远市非税收入缴款通知书》限期到通知书列明的银行所属任何一个网点缴监测费{0}元（{1}），到颁行缴款后应将盖有银行收讫章的广东省非税收入（电子）票据执收单位联送回给本站综合室。
3.本站在确认已缴监测费用和委托方提供了必要的监测条件后60个工作日内完成监测。";
        string strBUDGET = dt.Rows[0]["BUDGET"].ToString() == "" ? "0" : dt.Rows[0]["BUDGET"].ToString();
        strExplain = string.Format(strExplain, strBUDGET, DaXie(strBUDGET));
        sheet.GetRow(18).GetCell(1).SetCellValue(strExplain);

        //监测内容
        string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPES"].ToString().Split(';');
        if (dt.Rows[0]["SAMPLE_SOURCE"].ToString() == "送样")
        {
            //strWorkContent += "地表水、地下水(送样)\n";
            int intLen = strMonitroTypeArr.Length;
            int INTSHOWLEN = 0;
            foreach (string strMonitor in strMonitroTypeArr)
            {
                INTSHOWLEN++;
                strWorkContent += GetMonitorName(strMonitor) + "、";
                if (INTSHOWLEN == intLen - 1)
                {
                    strWorkContent += "(送样)\n";
                }
            }
        }
        //获取当前监测点信息
        TMisContractPointVo ContractPointVo = new TMisContractPointVo();
        ContractPointVo.CONTRACT_ID = task_id;
        ContractPointVo.IS_DEL = "0";
        DataTable dtPoint = new TMisContractPointLogic().SelectByTable(ContractPointVo);
        string strOutValuePoint = "", strOutValuePointItems = "";
        if (strMonitroTypeArr.Length > 0)
        {
            foreach (string strMonitor in strMonitroTypeArr)
            {
                string strMonitorName = "", strPointName = "";
                DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + strMonitor + "'");
                if (drPoint.Length > 0)
                {

                    foreach (DataRow drrPoint in drPoint)
                    {
                        string strPointNameForItems = "", strPointItems = "";
                        strMonitorName = GetMonitorName(strMonitor) + "：";
                        strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                        //获取当前点位的监测项目
                        TMisContractPointitemVo ContractPointitemVo = new TMisContractPointitemVo();
                        ContractPointitemVo.CONTRACT_POINT_ID = drrPoint["ID"].ToString();
                        DataTable dtPointItems = new TMisContractPointitemLogic().GetItemsForPoint(ContractPointitemVo);
                        if (dtPointItems.Rows.Count > 0)
                        {
                            foreach (DataRow drItems in dtPointItems.Rows)
                            {
                                strPointNameForItems = strMonitorName.Substring(0, strMonitorName.Length - 1) + drrPoint["POINT_NAME"] + "(" + (drrPoint["SAMPLE_DAY"].ToString() == "" ? "1" : drrPoint["SAMPLE_DAY"].ToString()) + "天" + (drrPoint["SAMPLE_FREQ"].ToString() == "" ? "1" : drrPoint["SAMPLE_FREQ"].ToString()) + "次):";
                                strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                            }
                            strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                        }
                    }
                    //获取输出监测类型监测点位信息
                    strOutValuePoint += strMonitorName + strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                }
            }
        }
        strWorkContent += "监测点位：\n" + strOutValuePoint;
        strWorkContent += "监测因子与频次：\n" + strOutValuePointItems;
        sheet.GetRow(12).GetCell(1).SetCellValue(strWorkContent);

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测协议书-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    /// <summary>
    /// 小写数字金额转换成大写人民币金额(正则表达式)
    /// Add by: weilin
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    private string DaXie(string money) 
    { 
        string s = double.Parse(money).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A"); 
        string d = System.Text.RegularExpressions.Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}"); 
        return System.Text.RegularExpressions.Regex.Replace(d, ".", delegate(System.Text.RegularExpressions.Match m) { 
        return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万億兆京垓秭穰"[m.Value[0] - '-'].ToString(); }); 
    }
    /// <summary>
    /// 小写数字金额转换成大写人民币金额（数组）
    /// Add by: weilin
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public string chang(string money)
    {
        //将小写金额转换成大写金额           
        double MyNumber = Convert.ToDouble(money);
        String[] MyScale = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
        String[] MyBase = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        String M = "";
        bool isPoint = false;
        if (money.IndexOf(".") != -1)
        {
            money = money.Remove(money.IndexOf("."), 1);
            isPoint = true;
        }
        for (int i = money.Length; i > 0; i--)
        {
            int MyData = Convert.ToInt16(money[money.Length - i].ToString());
            M += MyBase[MyData];
            if (isPoint == true)
            {
                M += MyScale[i - 1];
            }
            else
            {
                M += MyScale[i + 1];
            }
        }
        return M;
    }

    /// <summary>
    /// 获取指定监测类别的类别名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetMonitorName(string strId)
    {
        TBaseMonitorTypeInfoVo objItems = new TBaseMonitorTypeInfoLogic().Details(new TBaseMonitorTypeInfoVo { ID = strId, IS_DEL = "0" });
        return objItems.MONITOR_TYPE_NAME;
    }
    #endregion
}