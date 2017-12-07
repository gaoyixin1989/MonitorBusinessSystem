using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace i3.Core.View
{    
    /// <summary>
    /// 功能描述：文件操作基类
    /// 创建时间：2011-4-6 20:50:43
    /// 创建人  ：陈国迎
    /// </summary>
    public class FileOperater : System.Web.UI.Page
    {
        /// <summary>
        /// 功用描述：将Table内容导出为指定格式文件
        /// 创建人：  陈国迎
        /// 创建日期：2006-06-06
        /// </summary>
        /// <param name="objTable">表名</param>
        /// <param name="strFileName">文件名</param>
        /// <param name="strFormat">格式名(调ExportFileType结构体)</param>
        public void ExportHtmlTableToFormatedFile(System.Web.UI.HtmlControls.HtmlTable objTable, string strFileName, string strFormat)
        {
            //清空即有内容
            Response.Clear();

            //定义输出格式
            string strStype          = @"<style>.text { mso-number-format:\@; } </style>";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("" + strFileName + "", System.Text.Encoding.UTF8) + "." + strFormat + "");
            Response.Charset         = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.ContentType     = "application/vnd." + strFormat + "";

            //定义输出流
            System.IO.StringWriter stringWrite     = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            objTable.RenderControl(htmlWrite);


            Response.Write(strStype);
            Response.Write(stringWrite.ToString());

            //结束输出
            Response.End();
        }

        /// <summary>
        /// 功能描述：将GridView输出为Excel
        /// 创建人：　陈国迎
        /// 创建日期：2006-06-06
        /// </summary>
        /// <param name="GridExport">GridView</param>
        /// <param name="strName">文件名称</param>
        /// <param name="ArrayParams">文本输出列</param>
        public void ExportGridViewToFormatedExcel(GridView GridExport, string strName, params int[] ArrayParams)
        {
            //设置DataGrid指定列为文本格式
            if (ArrayParams.Length >= 1)
            {
                for (int i = 0; i < ArrayParams.Length; i++)
                {
                    for (int j = 0; j < GridExport.Rows.Count; j++)
                    {
                        GridExport.Rows[j].Cells[ArrayParams[i]].Attributes.Add("class", "text");
                    }
                }
            }

            //清空即有内容
            Response.Clear();

            //定义输出格式
            string strStype = @"<style>.text { mso-number-format:\@; } </style>";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("" + strName + "", System.Text.Encoding.UTF8) + ".xls");
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.ContentType = "application/vnd.xls";

            //定义输出流
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridExport.RenderControl(htmlWrite);

            //输出文本流
            if (ArrayParams.Length >= 1)
            {
                Response.Write(strStype);
            }
            Response.Write(stringWrite.ToString());

            //结束输出
            Response.End();
        }
    }
}
