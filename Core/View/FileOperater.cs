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
    /// �����������ļ���������
    /// ����ʱ�䣺2011-4-6 20:50:43
    /// ������  ���¹�ӭ
    /// </summary>
    public class FileOperater : System.Web.UI.Page
    {
        /// <summary>
        /// ������������Table���ݵ���Ϊָ����ʽ�ļ�
        /// �����ˣ�  �¹�ӭ
        /// �������ڣ�2006-06-06
        /// </summary>
        /// <param name="objTable">����</param>
        /// <param name="strFileName">�ļ���</param>
        /// <param name="strFormat">��ʽ��(��ExportFileType�ṹ��)</param>
        public void ExportHtmlTableToFormatedFile(System.Web.UI.HtmlControls.HtmlTable objTable, string strFileName, string strFormat)
        {
            //��ռ�������
            Response.Clear();

            //���������ʽ
            string strStype          = @"<style>.text { mso-number-format:\@; } </style>";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("" + strFileName + "", System.Text.Encoding.UTF8) + "." + strFormat + "");
            Response.Charset         = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.ContentType     = "application/vnd." + strFormat + "";

            //���������
            System.IO.StringWriter stringWrite     = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            objTable.RenderControl(htmlWrite);


            Response.Write(strStype);
            Response.Write(stringWrite.ToString());

            //�������
            Response.End();
        }

        /// <summary>
        /// ������������GridView���ΪExcel
        /// �����ˣ����¹�ӭ
        /// �������ڣ�2006-06-06
        /// </summary>
        /// <param name="GridExport">GridView</param>
        /// <param name="strName">�ļ�����</param>
        /// <param name="ArrayParams">�ı������</param>
        public void ExportGridViewToFormatedExcel(GridView GridExport, string strName, params int[] ArrayParams)
        {
            //����DataGridָ����Ϊ�ı���ʽ
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

            //��ռ�������
            Response.Clear();

            //���������ʽ
            string strStype = @"<style>.text { mso-number-format:\@; } </style>";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("" + strName + "", System.Text.Encoding.UTF8) + ".xls");
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.ContentType = "application/vnd.xls";

            //���������
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridExport.RenderControl(htmlWrite);

            //����ı���
            if (ArrayParams.Length >= 1)
            {
                Response.Write(strStype);
            }
            Response.Write(stringWrite.ToString());

            //�������
            Response.End();
        }
    }
}
