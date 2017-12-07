using System;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using BP.Web;
//using BP.Rpt ; 
using BP.DA;
using BP.En;
using System.Reflection;
using System.Text.RegularExpressions;
using Excel;
using BP.Sys;

namespace BP.Web
{
    /// <summary>
    /// PortalPage ��ժҪ˵����
    /// </summary>
    public class MasterPage : System.Web.UI.MasterPage
    {
        #region ����
        /// <summary>
        /// key.
        /// </summary>
        public string Key
        {
            get
            {
                return this.Request.QueryString["Key"];
            }
        }
        /// <summary>
        /// _HisEns
        /// </summary>
        public Entities _HisEns = null;
        /// <summary>
        /// ������ع���
        /// </summary>
        public Entities HisEns
        {
            get
            {
                if (this.EnsName != null)
                {
                    if (this._HisEns == null)
                        _HisEns = BP.En.ClassFactory.GetEns(this.EnsName);
                }
                return _HisEns;
            }
        }
        private Entity _HisEn = null;
        /// <summary>
        /// ������ع���
        /// </summary>
        public Entity HisEn
        {
            get
            {
                if (_HisEn == null)
                    _HisEn = this.HisEns.GetNewEntity;
                return _HisEn;
            }
        }
        #endregion 

        public string ShowType
        {
            get
            {
                string s = this.Request.QueryString["ShowType"];
                if (s == null)
                    s = "1";
                return s;
            }
        }
        public string PageID
        {
            get
            {
                return this.CurrPage;
            }
        }
        public string CurrPage
        {
            get
            {
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                int i = url.LastIndexOf("/") + 1;
                int i2 = url.IndexOf(".aspx") - 6;
                try
                {
                    url = url.Substring(i);
                    url = url.Substring(0, url.IndexOf(".aspx"));
                    return url;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                }
            }
        }
        /// <summary>
        /// ִ������
        /// </summary>
        public string DoType
        {
            get
            {
                return this.Request.QueryString["DoType"];
            }
        }
        /// <summary>
        /// EnsName
        /// </summary>
        public string EnsName
        {
            get
            {
                return   this.Request.QueryString["EnsName"];
            }
        }
        /// <summary>
        /// EnName
        /// </summary>
        public string EnName
        {
            get
            {
                return this.Request.QueryString["EnName"];
            }
        }
        public string RefPK
        {
            get
            {
                string s = this.Request.QueryString["RefPK"];
                if (s == null)
                    s = this.Request.QueryString["PK"];

                return s;
            }
        }
        /// <summary>
        /// ҳ��Index.
        /// </summary>
        public int PageIdx
        {
            get
            {
                string str = this.Request.QueryString["PageIdx"];
                if (str == null || str == "")
                    str = "1";
                return int.Parse(str);
            }
            set
            {
                ViewState["PageIdx"] = value;
            }
        }

        public new void OpenFile(string fileFullName)
        {
            // string path = Server.MapPath(fileFullName);
            System.IO.FileInfo file = new System.IO.FileInfo(fileFullName);
            this.Response.Clear();
            this.Response.Charset = "GB2312";
            this.Response.ContentEncoding = System.Text.Encoding.UTF8;
            // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ��� 
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));

            // ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ��� 
            this.Response.AddHeader("Content-Length", file.Length.ToString());

            // ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻���� 
            this.Response.ContentType = "application/ms-excel";
            // ���ļ������͵��ͻ��� 
            this.Response.WriteFile(file.FullName);
            // ֹͣҳ���ִ�� 
            this.Response.End();
        }
        public void GenerExcel(System.Data.DataTable dt, string fileName)
        {
            try
            {
                GenerExcel_pri(dt, fileName);
            }
            catch (Exception ex)
            {
                Log.DebugWriteError(ex.Message);
                try
                {
                    GenerExcel_pri(dt, fileName);
                }
                catch (Exception ex1)
                {
                    Log.DebugWriteError(ex1.Message);
                    string docs = SystemConfig.PathOfWebApp + "Comm/Helper/ExcelErr.txt";

                    try
                    {
                        docs = DataType.ReadTextFile(docs);
                    }
                    catch
                    {
                    }
                    throw new Exception("@�˴���İ����ļ�λ�ã�" + docs + "@���� excel�ļ����ִ��󣬿�����ϵͳ����Աû����ȷ�İ�װexcel �쳣��Ϣ:" + ex1.Message + " <hr>������Ϣ��<hr>" + docs);
                    // GenerExcel_pri_Text(dt, fileName);
                }
            }
        }
        protected void GenerExcel_pri_Text(System.Data.DataTable dt, string fileName)
        {
            string myFileName = fileName.Clone().ToString();
            string path = this.Request.PhysicalApplicationPath + "\\Temp\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            fileName = path + fileName;

            #region ��������������

            FileStream objFileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            #region ���ɵ����ļ�
            try
            {
                string strLine = "";
                foreach (DataColumn dr in dt.Columns)
                {
                    strLine += dr.ColumnName + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strLine += dr[dc.ColumnName].ToString() + Convert.ToChar(9);
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
            }
            catch
            {

            }

            finally
            {
                objStreamWriter.Close();
                objFileStream.Close();
            }
            #endregion


            this.OpenFile(fileName);

            //this.Response.Redirect("/Front/Tmp/" + myFileName);
            //this.Response.Redirect("/Front/Tmp/" + myFileName);
            //this.WinOpen("/Front/Tmp/" + myFileName);
            //  PubClass.WinOpen(   "./../Tmp/" + myFileName);
            //PubClass.WinOpen();
        }

        private void GenerExcel_pri(System.Data.DataTable dt, string fileName)
        {
            if (dt == null)
                return;

            if (fileName.IndexOf(".xl") == -1)
                fileName = fileName + ".xls";

            string fullName = BP.Sys.SystemConfig.PathOfTemp + fileName;
            DataView dv = new DataView(dt);
            GC.Collect();
            Application excel;
            //int rowIndex = 4;
            //int colIndex = 1;

            int rowIndex = 1;
            int colIndex = 0;

            _Workbook xBk;
            _Worksheet xSt;

            excel = new ApplicationClass();
            excel.DefaultFilePath = BP.Sys.SystemConfig.PathOfTemp;
            xBk = excel.Workbooks.Add(true);

            xSt = (_Worksheet)xBk.ActiveSheet;

            // 
            // ȡ�ñ��� 
            // 
            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;

                //���ñ����ʽΪ���ж���.
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).Font.Bold = true;
                // xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).ClearFormats();// = true;
                // xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex])..Bold = true; 
            }

            // 
            //ȡ�ñ���е����� 
            // 
            foreach (DataRowView row in dv)
            {
                rowIndex++;
                colIndex = 1;
                foreach (DataColumn col in dv.Table.Columns)
                {
                    if (col.DataType == System.Type.GetType("System.DateTime"))
                    {
                        excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                        // xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignBottom;//���������͵��ֶθ�ʽΪ���ж��� 
                    }
                    else
                    {
                        if (col.DataType == System.Type.GetType("System.String"))
                        {
                            excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
                            // xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignBottom;//�����ַ��͵��ֶθ�ʽΪ���ж��� 
                        }
                        else
                        {
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }
                    }
                    colIndex++;
                }
            }

            excel.Visible = true;

            xBk.SaveCopyAs(fileName);

            // ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
            GC.Collect();

            // string path = Server.MapPath(BP.Sys.SystemConfig.PathOfTemp + fileName + ".xls");
            // string path = Server.MapPath( xBk.FullName );
            //  string path = Server.MapPath(fileName + ".xls");

            this.OpenFile(fullName);

        }

        public static string GenerExcelFile(System.Data.DataTable dt)
        {
            return null;

            /*
            Excel.Application excelApp = new Excel.ApplicationClass();  // Creates a new Excel Application
            excelApp.Visible = false;  // Makes Excel visible to the user.

            // The following line adds a new workBill
            Excel.WorkBill newWorkBill = excelApp.WorkBills.Add(Excel.XlWBATemplate.xlWBATWorksheet);
   

            string fileName=PubClass.GenerTempFileName("xls");
            string file = SystemConfig.PathOfTemp+fileName;

            // The following code opens an existing workBill
            //string workBillPath = "c:/SomeWorkBill.xls";  // Add your own path here

            Excel.WorkBill excelWorkBill = excelApp.WorkBills.Open(file, 0,
                false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, 
                false,  0, true, false, false);
   
            // The following gets the Worksheets collection
            Excel.Sheets excelSheets = excelWorkBill.Worksheets;

            // The following gets Sheet1 for editing
            string currentSheet = "Sheet1";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);

            // �����С�
            int i =0;
            foreach(DataColumn dc in dt.Columns)
            {
                excelWorksheet.Cells[0,i]= dc.ColumnName;
                i++;
            }
            excelWorksheet.Visible=Excel.XlSheetVisibility.xlSheetVisible;

            i = 1;
            foreach(DataRow dr in dt.Rows)
            {
                int rowIdx=0;
                foreach(DataColumn dc in dt.Columns)
                {
                    excelWorksheet.Cells[i,rowIdx]=  dr[dc.ColumnName];
                    rowIdx++;
                }
                i++;
            }

//			excelWorksheet.SaveAs((file,
//��������������Excel.Missing.Value,Excel.Missing.Value,Missing.Value,Missing.Value,Missing.Value, 
//��������������Excel.XlSaveAsAccessMode.xlNoChange,Missing.Value,Missing.Value,Missing.Value, 
//��������������Missing.Value,Missing.Value);

            excelWorkBill.Save();

            //excelWorksheet.SaveAs( file,Missing.Value,Missing.Value,Missing.Value,Missing.Value,Missing.Value,Missing.Value,Missing.Value,Missing.Value,Missing.Value); 

//			excelWorksheet.SaveAs((file,
//��������������Excel.Missing.Value,Excel.Missing.Value,Missing.Value,Missing.Value,Missing.Value, 
//��������������Excel.XlSaveAsAccessMode.xlNoChange,Missing.Value,Missing.Value,Missing.Value, 
//��������������Missing.Value,Missing.Value);


             xexcelWorksheet=null; 
      excelWorkBill=null; 
      excelApp.Quit(); //��һ���Ƿǳ���Ҫ�ģ�����Excel�����ܴ��ڴ����˳� 
      excelApp=null; 

            string url = "/"+System.Web.HttpContext.Current.Application+"/Temp/"+fileName;
            PubClass.WinOpen(url);
            return  file;
            */
        }

        public void ToFunc(string funcNo)
        {
        }


        #region ��ʵ�����ϵ�Ĳ���

        public void InvokeDataIO(string ensName, bool isOpenWindow)
        {
            string url = this.Request.ApplicationPath + "Comm/Pub/DataIO.aspx?EnsName=" + ensName;
            if (isOpenWindow)
            {
                this.WinOpen(url);
            }
            else
            {
                this.Response.Redirect(url, true);
            }
        }
        /// <summary>
        /// ���õ���ʵ�������
        /// </summary>
        /// <param name="className">������</param>
        /// <param name="pk">����</param>
        /// <param name="isOpenWindow">�ǲ�ʱ���µĴ���</param>
        public void InvokeEnManager(string enName, string pk, bool isOpenWindow)
        {
            string url = this.Request.ApplicationPath + "Comm/RefFunc/UIEn.aspx?EnsName=" + enName + "&PK=" + pk;
            if (isOpenWindow)
            {
                this.WinOpen(url, "", "card", 800, 400, 50, 50, false, false);
            }
            else
            {
                this.Response.Redirect(url, true);
            }
        }
        /// <summary>
        /// ���õ���ʵ�������
        /// </summary>
        /// <param name="className">������</param>		 
        /// <param name="isOpenWindow">�ǲ�ʱ���µĴ���</param>
        public void InvokeEnManager(string className, bool isOpenWindow)
        {
            string url = this.Request.ApplicationPath + "Comm/RefFunc/UIEn.aspx?EnsName=" + className;
            if (isOpenWindow)
            {
                this.WinOpenShowModalDialog(url, "��Ƭ", "card", 300, 400, 100, 100);
            }
            else
            {
                this.Response.Redirect(url, true);
            }
        }
        #endregion
        /// <summary>
        /// add html.
        /// </summary>
        /// <param name="html"></param>
        public void Add(string html)
        {
            this.Controls.Add(this.ParseControl(html));
        }
        ///// <summary>
        ///// UCSys1
        ///// </summary>
        //public BP.Web.Comm.UC.Migrated_UCSys UCSys1
        //{
        //    get
        //    {
        //        return (BP.Web.Comm.UC.Migrated_UCSys)this.FindControl("UCSys1");
        //    }
        //}
        //public BP.Web.Comm.UC.Migrated_UCSys UCSys2
        //{
        //    get
        //    {
        //        return (BP.Web.Comm.UC.Migrated_UCSys)this.FindControl("UCSys2");
        //    }
        //}
        //public BP.Web.Comm.UC.Migrated_UCSys UCSys3
        //{
        //    get
        //    {
        //        return (BP.Web.Comm.UC.Migrated_UCSys)this.FindControl("UCSys3");
        //    }
        //}
        //public BP.Web.Comm.UC.Migrated_UCSys UCSys4
        //{
        //    get
        //    {
        //        return (BP.Web.Comm.UC.Migrated_UCSys)this.FindControl("UCSys4");
        //    }
        //}
        //public BP.Web.Comm.UC.Migrated_UCEn UCEn1
        //{
        //    get
        //    {
        //        return (BP.Web.Comm.UC.Migrated_UCEn)this.FindControl("UCEn1");
        //    }
        //}
        public string RefNo
        {
            get
            {
                string s = this.Request.QueryString["RefNo"];
                if (s == null || s=="" || s==string.Empty)
                    s = this.Request.QueryString["No"];
                return s;
            }
        }
        /// <summary>
        /// ��ǰҳ��Ĳ�����
        /// </summary> 
        public string Paras
        {
            get
            {
                string str = "";
                foreach (string s in this.Request.QueryString)
                {
                    str += "&" + s + "=" + this.Request.QueryString[s];
                }
                return str;
            }
        }

        #region �ļ��ĵ��뵼��
        protected void ExportSHDGToReport_del(string file)
        {
            this.Response.WriteFile(file);
        }
        protected void InvokeDataCheck(En.Entities ens)
        {
            this.WinOpen(this.Request.ApplicationPath + "Comm/DataCheck.aspx?EnsName=" + ens.ToString(), "���ݼ��", "FileManager", 800, 600, 50, 50);
        }
        /// <summary>
        /// �����ļ�������
        /// </summary>
        /// <param name="en"></param>		 
        protected void InvokeFileManager(En.Entity en)
        {
            this.WinOpenShowModalDialog(this.Request.ApplicationPath + "Comm/FileManager.aspx?EnsName=" + en.ToString() + "&PK=" + en.PKVal, "�ļ�����Ա", "FileManager", 800, 600, 50, 50);
        }

        protected void ExportDGToExcel(System.Data.DataTable dt)
        {
            //this.Session["MyDT"] = dt;
            //this.WinOpen(this.Request.ApplicationPath + "Comm/ToExcel.aspx");
            //return;

            string fileNameS = "Ep" + DBAccess.GenerOID() + ".xls";
            string filename = this.Request.PhysicalApplicationPath + "\\Temp\\" + fileNameS;
            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);



            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);

            try
            {
                string strLine = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    strLine += dc.ColumnName + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);

                foreach (DataRow dr in dt.Rows)
                {
                    strLine = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string s = dr[dc.ColumnName].ToString();
                        if (s.Length > 14)
                        {
                            try
                            {
                                bool isValid = (new Regex(@"^-?(0|\d+)(\.\d+)?$")).IsMatch(s);
                                if (isValid)
                                    s = s + "z1z";
                            }
                            catch
                            {
                            }
                        }
                        strLine = strLine + s + Convert.ToChar(9);
                    }
                    objStreamWriter.WriteLine(strLine);
                }
                objStreamWriter.Close();
                objFileStream.Close();
            }
            catch
            {
            }
            finally
            {
                objStreamWriter.Close();
                objFileStream.Close();
            }
            this.WinOpen(this.Request.ApplicationPath + "/Temp/" + fileNameS);
        }
        protected string ExportDGToExcel(System.Data.DataTable dt, Map map, string title)
        {
            string filename = "Ep" + this.Session.SessionID + ".xls";
            string file = filename;
            bool flag = true;
            string filepath = this.Request.PhysicalApplicationPath + "\\Temp\\";

            #region ��������������
            //			//����У��
            //			if (dg == null || dg.Items.Count <=0 || filename == null || filename == "" || filepath == null || filepath == "")
            //				return null;

            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            filename = filepath + filename;

            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            #region ���ɵ����ļ�
            try
            {
                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine(Convert.ToChar(9) + title + Convert.ToChar(9));
                objStreamWriter.WriteLine();
                string strLine = "";
                //�����ļ�����
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //strLine = strLine + dt.Columns[i].HeaderText + Convert.ToChar(9);

                    foreach (Attr attr in map.Attrs)
                    {
                        if (attr.Key == dt.Columns[i].ColumnName)
                        {
                            strLine = strLine + attr.Desc + Convert.ToChar(9);
                        }
                    }
                }

                objStreamWriter.WriteLine(strLine);
                strLine = "";
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (Attr attr in map.Attrs)
                    {
                        strLine = strLine + dr[attr.Key] + Convert.ToChar(9);
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }


                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine(Convert.ToChar(9) + " �Ʊ��ˣ�" + Convert.ToChar(9) + WebUser.Name + Convert.ToChar(9) + "���ڣ�" + Convert.ToChar(9) + DateTime.Now.ToShortDateString());

            }
            catch
            {
                flag = false;
            }
            finally
            {
                objStreamWriter.Close();
                objFileStream.Close();
            }
            #endregion

            #region ɾ�����ɵ��ļ�
            //DelExportedTempFile(filepath);
            #endregion

            if (flag)
            {
                this.WinOpen(this.Request.ApplicationPath + "/Temp/" + file);
                //this.Write_Javascript(" window.open('"+ Request.ApplicationPath + @"/Report/Exported/" + filename +"'); " );
                //this.Write_Javascript(" window.open('"+Request.ApplicationPath+"/Temp/" + file +"'); " );
            }

            return file;
        }
        protected void ExportPageToExcel(string filename)
        {
            //��һ�������ĵ����͡��ַ�����
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.Charset = "utf-8";
            //�������к���Ҫ�� attachment ������ʾ��Ϊ�������أ������Ըĳ� online ���ߴ�
            //filename=FileFlow.xls ָ������ļ������ƣ�ע������չ����ָ���ļ��������������Ϊ��.doc ���� .xls ���� .txt ����.htm��

            this.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            this.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

            //Response.ContentTypeָ���ļ����� ����Ϊapplication/ms-excel ���� application/ms-word  application/ms-txt ���� application/ms-html
            //�������������ֱ��֧���ĵ�
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;

            //����������һ��������
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            //������Ŀ�����ݰ󶨵����������
            this.RenderControl(oHtmlTextWriter);

            //this ��ʾ�����ҳ����Ҳ���԰�datagrid,������֧��obj.RenderControl()���ԵĿؼ�
            this.Response.Write(oStringWriter.ToString());
            this.Response.End();

            // �ܽ᣺��������Microsoft Visual Studio .NET 2003ƽ̨�²���ͨ����������C#��VB��������VB��ʱ�� this �ؼ��ָĳ� me �� 
        }

        protected void ExportDGToExcelV2(Entities ens, string outFileName)
        {
            System.Web.HttpResponse resp;
            resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + outFileName);
            resp.ContentType = "application/ms-excel";

            string colHeaders = "", ls_item = "";

            //�����������ж���ͬʱ��DataSet����ֵ���г�ʼ�� 
            System.Data.DataTable dt = ens.ToDataTableDesc();
            DataRow[] myRow = dt.Select();//��������dt.Select("id>10")֮��ʽ�ﵽ����ɸѡĿ��
            int i = 0;
            int cl = dt.Columns.Count;

            //ȡ�����ݱ���б��⣬������֮����t�ָ���һ���б����ӻس��� 
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//���һ�У���n
                    colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                else
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            }
            resp.Write(colHeaders);

            ////��HTTP�������д��ȡ�õ�������Ϣ 

            ////���д�������   
            foreach (DataRow row in myRow)
            {
                //��ǰ������д��HTTP������������ÿ�ls_item�Ա���������     
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//���һ�У���n
                        ls_item += row[i].ToString() + "\n";
                    else
                        ls_item += row[i].ToString() + "\t";
                }
                resp.Write(ls_item);
                ls_item = "";
            }
            resp.End();
            return;
        }
        /// <summary>
        /// ExportDGToExcel
        /// </summary>
        /// <param name="ens"></param>
        /// <returns></returns>
        protected string ExportDGToExcel(Entities ens)
        {
            //System.Data.DataTable dt = ens.ToEmptyTableDesc();
            //return MasterPage.GenerExcelFile(dt);

            Map map = ens.GetNewEntity.EnMap;
            string filename = WebUser.No + ".xls";
            string file = filename;
            // bool flag = true;
            string filepath = SystemConfig.PathOfWebApp + "\\Temp\\";

            #region ��������������
            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            filename = filepath + filename;
            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.UTF32);
            #endregion
             
            string strLine = "";
            //�����ļ�����
            foreach (Attr attr in map.Attrs)
            {
                if (attr.Key.IndexOf("Text") == -1)
                {
                    if (attr.UIVisible == false)
                        continue;
                }

                if (attr.MyFieldType == FieldType.Enum
                    || attr.MyFieldType == FieldType.PKEnum
                    || attr.MyFieldType == FieldType.PKFK
                    || attr.MyFieldType == FieldType.FK)
                    continue;

                strLine = strLine + attr.Desc + Convert.ToChar(9);
            }

            objStreamWriter.WriteLine(strLine);
            foreach (Entity en in ens)
            {
                /*�����ļ�����*/
                strLine = "";
                foreach (Attr attr in map.Attrs)
                {
                    if (attr.Key.IndexOf("Text") == -1)
                    {
                        if (attr.UIVisible == false)
                            continue;
                    }

                    if (attr.MyFieldType == FieldType.Enum
                        || attr.MyFieldType == FieldType.PKEnum
                        || attr.MyFieldType == FieldType.PKFK
                        || attr.MyFieldType == FieldType.FK)
                        continue;

                    string str = en.GetValStringByKey(attr.Key);
                    if (str == "" || str == null)
                        str = " ";
                    strLine = strLine + str + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine.Replace(Convert.ToChar(9).ToString() + Convert.ToChar(9).ToString(), Convert.ToChar(9).ToString()));
            }
            objStreamWriter.Close();
            objFileStream.Close();
            #endregion

            PubClass.WinOpen(this.Request.ApplicationPath + "/Temp/" + file);
            return file;
        }
        public string MyPK
        {
            get
            {
                return this.Request.QueryString["MyPK"];
            }
        }
        public int RefOID
        {
            get
            {
                string s = this.Request.QueryString["RefOID"];
                if (s == null)
                    s = this.Request.QueryString["OID"];
                if (s == null)
                    return 0;
                return int.Parse(s);
            }
        }
        public string GenerTableStr(System.Data.DataTable dt)
        {
            string str = "<Table id='tb' border=1 >";
            // ����
            str += "<TR>";
            foreach (DataColumn dc in dt.Columns)
            {
                str += "<TD class='DGCellOfHeader" + BP.Web.WebUser.Style + "' >" + dc.ColumnName + "</TD>";
            }
            str += "</TR>";

            //����
            foreach (DataRow dr in dt.Rows)
            {
                str += "<TR>";

                foreach (DataColumn dc in dt.Columns)
                {
                    str += "<TD >" + dr[dc.ColumnName] + "</TD>";
                }
                str += "</TR>";
            }
            str += "</Table>";
            return str;
        }
        public string GenerTablePage(System.Data.DataTable dt, string title)
        {
            return PubClass.GenerTablePage(dt, title);
        }
        public string GenerLabelStr(string title)
        {
            return PubClass.GenerLabelStr(title);
            //return str;
        }
        public Control GenerLabel(string title)
        {
            string path = this.Request.ApplicationPath;
            string str = "";
            str += "<TABLE style='font-size:14px' cellpadding='0' cellspacing='0' background='/WF/Img/DG_bgright.gif'>";
            str += "<TR>";
            str += "<TD>";
            str += "<IMG src='/WF/Img/DG_Title_Left.gif' border='0' width='30' height='25'></TD>";

            str += "<TD  valign=bottom noWrap background='/WF/Img/DG_Title_BG.gif'   height='25' border=0>&nbsp;";
            str += " &nbsp;<b>" + title + "</b>&nbsp;&nbsp;";
            str += "</TD>";
            str += "<TD >";
            str += "<IMG src='/WF/Img/DG_Title_Right.gif' border='0' width='25' height='25'></TD>";
            str += "</TR>";
            str += "</TABLE>";
            return this.ParseControl(str);
        }
        public string GenerCaption(string title)
        {
            if (BP.Web.WebUser.Style == "2")
                return "<div class=Table_Title ><span>" + title + "</span></div>";
            return title;
        }
        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="i"></param>
        public void RefreshHeader_del(int i)
        {
            //this.Response.Write("hello");
            this.Response.Write("<script language='JavaScript'> window.parent.frames(" + i.ToString() + ").location.href='../WF/Port/Head.aspx'</script>");
        }

       
  
        protected override void OnLoad(EventArgs e)
        {
            //if (Web.WebUser.No == null)
            //    this.ToSignInPage();
            base.OnLoad(e);
        }
        /// <summary>
        /// ������һ��excel,�ļ����ڣ����ݵ��롣
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        protected void ExportEnToExcelModel_OpenWin(Attrs attrs, string sheetName)
        {
            string filename = sheetName + ".xls";
            string file = filename;
            //SystemConfig.PathOfTemp
            string filepath = Request.PhysicalApplicationPath + "\\Temp\\";

            #region ��������������
            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            filename = filepath + filename;
            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            #region ���ɵ����ļ�
            string strLine = "";
            foreach (Attr attr in attrs)
            {
                strLine += attr.Desc + Convert.ToChar(9);
            }

            objStreamWriter.WriteLine(strLine);
            objStreamWriter.Close();
            objFileStream.Close();
            #endregion

            //this.WinOpen(Request.ApplicationPath+"/Temp/" + file,"sss", 500,800);

            this.Write_Javascript(" window.open('" + Request.ApplicationPath + "/Temp/" + file + "'); ");
        }

        #region �û��ķ���Ȩ��
        /// <summary>
        /// ˭��ʹ�����ҳ��,���Ǳ����ɵ��ִ���
        /// such as ,admin,jww,002, 
        /// if return value is null, It's mean all emps can visit it . 
        /// </summary>
        protected virtual string WhoCanUseIt()
        {
            return null;
        }
        #endregion

        private void RP(string msg)
        {
            this.Response.Write(msg);
        }
        private void RPBR(string msg)
        {
            this.Response.Write(msg + "<br>");
        }
     
        public string GenerCreateTableSQL(string className)
        {
            ArrayList als = ClassFactory.GetObjects(className);
            int u = 0;
            string sql = "";
            foreach (Object obj in als)
            {
                u++;
                try
                {
                    Entity en = (Entity)obj;
                    switch (en.EnMap.EnDBUrl.DBType)
                    {
                        case DBType.Oracle:
                            sql += SqlBuilder.GenerCreateTableSQLOfOra_OK(en) + " \n GO \n";
                            break;
                        case DBType.Informix:
                            sql += SqlBuilder.GenerCreateTableSQLOfInfoMix(en) + " \n GO \n";
                            break;
                        default:
                            sql += SqlBuilder.GenerCreateTableSQLOfMS(en) + "\n GO \n";
                            break;
                    }
                }
                catch
                {
                    continue;
                }
                //Map map=en.EnMap;
                //objStreamWriter.WriteLine(Convert.ToChar(9)+"No:"+u.ToString()+Convert.ToChar(9) +map.EnDesc +Convert.ToChar(9) +map.PhysicsTable+Convert.ToChar(9) +map.EnType);
            }
            Log.DefaultLogWriteLineInfo(sql);
            return sql;
        }

        public void ExportEntityToExcel(string classbaseName)
        {
            #region �ļ�
            string filename = "DatabaseDesign.xls";
            string file = filename;
            //bool flag = true;
            string filepath = Request.PhysicalApplicationPath + "\\Temp\\";

            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            filename = filepath + filename;
            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            //string str="";
            ArrayList als = ClassFactory.GetObjects(classbaseName);
            int i = 0;
            objStreamWriter.WriteLine();
            objStreamWriter.WriteLine(Convert.ToChar(9) + "ϵͳʵ��[" + classbaseName + "]" + Convert.ToChar(9));
            objStreamWriter.WriteLine();
            //objStreamWriter.WriteLine(Convert.ToChar(9)+"��лʹ��ϵͳʵ��ṹ�Զ�������"+Convert.ToChar(9)+"��������"+Convert.ToChar(9)+DateTime.Now.ToString("yyyy��MM��dd��"));
            objStreamWriter.WriteLine(Convert.ToChar(9) + "��" + classbaseName + "�̳�������ʵ����[" + als.Count + "]��");


            #region ����Ŀ¼
            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "ϵͳʵ��Ŀ¼");
            objStreamWriter.WriteLine(Convert.ToChar(9) + "���" + Convert.ToChar(9) + "ʵ������" + Convert.ToChar(9) + "�����/��ͼ" + Convert.ToChar(9) + "����");
            int u = 0;
            foreach (Object obj in als)
            {
                try
                {
                    u++;
                    Entity en = (Entity)obj;
                    Map map = en.EnMap;
                    objStreamWriter.WriteLine(Convert.ToChar(9) + "No:" + u.ToString() + Convert.ToChar(9) + map.EnDesc + Convert.ToChar(9) + map.PhysicsTable + Convert.ToChar(9) + map.EnType);
                }
                catch
                {
                }
            }
            objStreamWriter.WriteLine();
            #endregion

            foreach (Object obj in als)
            {
                try
                {

                    i++;
                    Entity en = (Entity)obj;
                    Map map = en.EnMap;

                    #region ���ɵ����ļ�
                    objStreamWriter.WriteLine("���" + i);
                    objStreamWriter.WriteLine(Convert.ToChar(9) + "ʵ������" + Convert.ToChar(9) + map.EnDesc + Convert.ToChar(9) + "�����/��ͼ" + Convert.ToChar(9) + map.PhysicsTable + Convert.ToChar(9) + "ʵ������" + Convert.ToChar(9) + map.EnType);
                    if (map.CodeStruct == null)
                    {
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "����ṹ��Ϣ:��");
                    }
                    else
                    {
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "����ṹ" + Convert.ToChar(9) + map.CodeStruct +  "�Ƿ����ŵĳ���" + Convert.ToChar(9) + map.IsCheckNoLength );
                    }
                    //objStreamWriter.WriteLine(Convert.ToChar(9)+"������λ��"+map.EnDBUrl+Convert.ToChar(9)+"ʵ���ڴ���λ��"+Convert.ToChar(9)+map.DepositaryOfEntity+Convert.ToChar(9)+"Map �ڴ���λ��"+Convert.ToChar(9)+map.DepositaryOfMap);
                    objStreamWriter.WriteLine(Convert.ToChar(9) + "������λ��" + map.EnDBUrl + Convert.ToChar(9) + "Map �ڴ���λ��" + Convert.ToChar(9) + map.DepositaryOfMap);
                    objStreamWriter.WriteLine(Convert.ToChar(9) + "����Ȩ��" + Convert.ToChar(9) + "�Ƿ�鿴" + en.HisUAC.IsView + Convert.ToChar(9) + "�Ƿ��½�" + en.HisUAC.IsInsert + Convert.ToChar(9) + "�Ƿ�ɾ��" + en.HisUAC.IsDelete + "�Ƿ����" + en.HisUAC.IsUpdate + Convert.ToChar(9) + "�Ƿ񸽼�" + en.HisUAC.IsAdjunct);
                    if (map.Dtls.Count > 0)
                    {
                        /* output dtls */
                        EnDtls dtls = map.Dtls;
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "��ϸ/�ӱ���Ϣ:����" + dtls.Count);
                        int ii = 0;
                        foreach (EnDtl dtl in dtls)
                        {
                            ii++;
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "���:" + ii + "����:" + dtl.Desc + "��ϵ����ʵ����" + dtl.EnsName + "���" + dtl.RefKey);
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "�����:" + dtl.Ens.GetNewEntity.EnMap.PhysicsTable);
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "��ע:����" + dtl.Desc + "����ϸ����Ϣ,��ο�" + dtl.EnsName);
                        }
                    }
                    else
                    {
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "��ϸ/�ӱ���Ϣ:��");
                    }

                    if (map.AttrsOfOneVSM.Count > 0)
                    {
                        /* output dtls */
                        AttrsOfOneVSM dtls = map.AttrsOfOneVSM;
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "��Զ��ϵ:����" + dtls.Count);
                        int ii = 0;
                        foreach (AttrOfOneVSM dtl in dtls)
                        {
                            ii++;
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "���:" + ii + "����:" + dtl.Desc);
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "��Զ�ʵ����" + dtl.EnsOfMM.ToString() + "���" + dtl.AttrOfOneInMM);
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "��ʵ������������" + dtl.AttrOfOneInMM);
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "��ʵ����" + dtl.EnsOfMM.ToString() + "���" + dtl.AttrOfMValue);
                        }
                    }
                    else
                    {
                        objStreamWriter.WriteLine(Convert.ToChar(9) + "��Զ��ϵ:��");
                    }

                    objStreamWriter.WriteLine(Convert.ToChar(9) + "��/��ͼ�ṹ");
                    int iii = 0;
                    objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + "�������" + Convert.ToChar(9) + "��������" + Convert.ToChar(9) + "����" + Convert.ToChar(9) + "�����ֶ�" + Convert.ToChar(9) + "��������" + Convert.ToChar(9) + "Ĭ��ֵ" + Convert.ToChar(9) + "��ϵ����" + Convert.ToChar(9) + "��ע");

                    foreach (Attr attr in map.Attrs)
                    {
                        iii++;
                        if (attr.MyFieldType == FieldType.Enum)
                        {
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + iii + Convert.ToChar(9) + attr.Desc + Convert.ToChar(9) + attr.Key + Convert.ToChar(9) + attr.Field + Convert.ToChar(9) + attr.MyDataTypeStr + Convert.ToChar(9) + attr.DefaultVal + Convert.ToChar(9) + "ö��" + Convert.ToChar(9) + "ö��Key" + attr.UIBindKey + " ����ö�ٵ���Ϣ�뵽Sys_Enum�����ҵ�����ϸ����Ϣ.");
                            continue;
                        }
                        if (attr.MyFieldType == FieldType.PKEnum)
                        {
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + iii + Convert.ToChar(9) + attr.Desc + Convert.ToChar(9) + attr.Key + Convert.ToChar(9) + attr.Field + Convert.ToChar(9) + attr.MyDataTypeStr + Convert.ToChar(9) + attr.DefaultVal + Convert.ToChar(9) + "����ö��" + Convert.ToChar(9) + "ö��Key" + attr.UIBindKey + " ����ö�ٵ���Ϣ�뵽Sys_Enum�����ҵ�����ϸ����Ϣ.");

                            //objStreamWriter.WriteLine(Convert.ToChar(9)+" "+Convert.ToChar(9)+"No:"+iii+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Desc+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Key+Convert.ToChar(9)+"����Ĭ��ֵ"+Convert.ToChar(9)+attr.DefaultVal+Convert.ToChar(9)+"�����ֶ�"+Convert.ToChar(9)+attr.Field+Convert.ToChar(9)+"�ֶι�ϵ����"+Convert.ToChar(9)+"ö������"+Convert.ToChar(9)+"�ֶ��������� "+Convert.ToChar(9)+attr.MyDataTypeStr+"");
                            continue;
                        }
                        if (attr.MyFieldType == FieldType.FK)
                        {
                            Entity tmp = attr.HisFKEn;
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + iii + Convert.ToChar(9) + attr.Desc + Convert.ToChar(9) + attr.Key + Convert.ToChar(9) + attr.Field + Convert.ToChar(9) + attr.MyDataTypeStr + Convert.ToChar(9) + attr.DefaultVal + Convert.ToChar(9) + "���" + Convert.ToChar(9) + "������ʵ��:" + tmp.EnDesc + "�����:" + tmp.EnMap.PhysicsTable + " ����" + tmp.EnDesc + "��Ϣ�뵽��ʵ����Ϣ����ȥ��.");

                            //objStreamWriter.WriteLine(Convert.ToChar(9)+" "+Convert.ToChar(9)+"No:"+iii+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Desc+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Key+Convert.ToChar(9)+"����Ĭ��ֵ"+Convert.ToChar(9)+attr.DefaultVal+Convert.ToChar(9)+"�����ֶ�"+Convert.ToChar(9)+attr.Field+Convert.ToChar(9)+"�ֶι�ϵ����"+Convert.ToChar(9)+"���"+Convert.ToChar(9)+"�ֶ��������� "+Convert.ToChar(9)+attr.MyDataTypeStr+""+"��ϵ����ʵ������"+Convert.ToChar(9)+tmp.EnDesc+"�����"+Convert.ToChar(9)+tmp.EnMap.PhysicsTable+Convert.ToChar(9)+"����ϸ����Ϣ��ο�"+Convert.ToChar(9));
                            continue;
                        }
                        if (attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity tmp = attr.HisFKEn;
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + iii + Convert.ToChar(9) + attr.Desc + Convert.ToChar(9) + attr.Key + Convert.ToChar(9) + attr.Field + Convert.ToChar(9) + attr.MyDataTypeStr + Convert.ToChar(9) + attr.DefaultVal + Convert.ToChar(9) + "������" + Convert.ToChar(9) + "������ʵ��:" + tmp.EnDesc + "�����:" + tmp.EnMap.PhysicsTable + " ����" + tmp.EnDesc + "��Ϣ�뵽��ʵ����Ϣ����ȥ��.");
                            continue;
                        }

                        //���������.
                        if (attr.MyFieldType == FieldType.Normal || attr.MyFieldType == FieldType.PK)
                        {
                            objStreamWriter.WriteLine(Convert.ToChar(9) + " " + Convert.ToChar(9) + iii + Convert.ToChar(9) + attr.Desc + Convert.ToChar(9) + attr.Key + Convert.ToChar(9) + attr.Field + Convert.ToChar(9) + attr.MyDataTypeStr + Convert.ToChar(9) + attr.DefaultVal + Convert.ToChar(9) + "��ͨ" + Convert.ToChar(9) + attr.EnterDesc);
                            //objStreamWriter.WriteLine(Convert.ToChar(9)+" "+Convert.ToChar(9)+"No:"+iii+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Desc+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Key+Convert.ToChar(9)+"����Ĭ��ֵ"+Convert.ToChar(9)+attr.DefaultVal+Convert.ToChar(9)+"�����ֶ�"+Convert.ToChar(9)+attr.Field+Convert.ToChar(9)+"�ֶι�ϵ����"+Convert.ToChar(9)+"�ַ�"+Convert.ToChar(9)+"�ֶ���������"+Convert.ToChar(9)+attr.MyDataTypeStr+""+Convert.ToChar(9)+"����Ҫ��"+Convert.ToChar(9)+attr.EnterDesc);
                            continue;
                        }
                        //objStreamWriter.WriteLine("�������:"+iii+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Desc+Convert.ToChar(9)+"����"+Convert.ToChar(9)+attr.Key+Convert.ToChar(9)+"����Ĭ��ֵ"+Convert.ToChar(9)+attr.DefaultVal+Convert.ToChar(9)+"�����ֶ�"+Convert.ToChar(9)+attr.Field+"�ֶι�ϵ����"+Convert.ToChar(9)+"�ַ�"+Convert.ToChar(9)+"�ֶ���������"+Convert.ToChar(9)+attr.MyDataTypeStr+Convert.ToChar(9)+""+"����Ҫ��"+Convert.ToChar(9)+attr.EnterDesc+Convert.ToChar(9));
                    }
                }
                catch
                {
                }
            }
            objStreamWriter.WriteLine();
            objStreamWriter.WriteLine(Convert.ToChar(9) + Convert.ToChar(9) + " " + Convert.ToChar(9) + Convert.ToChar(9) + " �Ʊ��ˣ�" + Convert.ToChar(9) + WebUser.Name + Convert.ToChar(9) + "���ڣ�" + Convert.ToChar(9) + DateTime.Now.ToShortDateString());

            objStreamWriter.Close();
            objFileStream.Close();

            this.Write_Javascript(" window.open('" + this.Request.ApplicationPath + "Temp/" + file + "'); ");


                    #endregion



        }
        public void Helper(string htmlFile)
        {
            this.WinOpen(htmlFile);
        }

        public void Helper()
        {
            this.WinOpen(this.Request.ApplicationPath + "/" + SystemConfig.AppSettings["PageOfHelper"]);
        }
        /// <summary>
        /// ȡ������by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRequestStrByKey(string key)
        {
            return this.Request.QueryString[key];
        }

        #region ��������
        /// <summary>
        /// showmodaldialog 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="Height"></param>
        /// <param name="Width"></param>
        protected void ShowModalDialog(string url, string title, int Height, int Width)
        {
            string script = "<script language='JavaScript'>window.showModalDialog('" + url + "','','dialogHeight: " + Height.ToString() + "px; dialogWidth: " + Width.ToString() + "px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no'); </script> ";

            //this.RegisterStartupScript("key1s",script); // old .
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "K1", script); // new 

            //this.Response.Write( script );
            //this.RegisterClientScriptBlock("Dia",script);
        }
        /// <summary>
        /// �رմ���
        /// </summary>
        protected void WinClose()
        {
            this.Response.Write("<script language='JavaScript'> window.close();</script>");
        }
        protected void WinClose(string val)
        {
            string clientscript = "<script language='javascript'> window.returnValue = '" + val + "'; window.close(); </script>";
            this.Page.Response.Write(clientscript);
        }
        /// <summary>
        /// ��һ���µĴ���
        /// </summary>
        /// <param name="msg"></param>
        protected void WinOpen(string url)
        {
            this.WinOpen(url, "", "msg", 900, 500);
        }
        protected string dealUrl(string url)
        {
            if (url.IndexOf("?") == -1)
            {
                //url=url.Substring(0,url.IndexOf("",""));
                return url;
            }
            else
            {
                return url;
            }
        }
        protected void WinOpen(string url, string title, string winName, int width, int height)
        {
            this.WinOpen(url, title, winName, width, height, 0, 0);
        }
        protected void WinOpen(string url, string title, int width, int height)
        {
            this.WinOpen(url, title, "ActivePage", width, height, 0, 0);
        }
        protected void WinOpen(string url, string title, string winName, int width, int height, int top, int left)
        {
            WinOpen(url, title, winName, width, height, top, left, false, false);
        }
        protected void WinOpen(string url, string title, string winName, int width, int height, int top, int left, bool _isShowToolBar, bool _isShowAddress)
        {
            url = url.Replace("<", "[");
            url = url.Replace(">", "]");
            url = url.Trim();
            title = title.Replace("<", "[");
            title = title.Replace(">", "]");
            title = title.Replace("\"", "��");
            string isShowAddress = "no", isShowToolBar = "no";
            if (_isShowAddress)
                isShowAddress = "yes";
            if (_isShowToolBar)
                isShowToolBar = "yes";

            this.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",left=" + left + ",height=" + height + ",scrollbars=yes,resizable=yes,toolbar=" + isShowToolBar + ",location=" + isShowAddress + "'); newWindow.focus(); </script> ");
        }
        //private int MsgFontSize=1;
        /// <summary>
        /// �����ҳ���Ϻ�ɫ�ľ��档
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        protected void ResponseWriteRedMsg(string msg)
        {
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            System.Web.HttpContext.Current.Application["info" + WebUser.No] = msg;
            string url = "/WF/Comm/Port/ErrorPage.aspx";
            this.WinOpen(url, "����", "errmsg", 500, 400, 150, 270);
        }
        protected void ResponseWriteShowModalDialogRedMsg(string msg)
        {
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            //			string url="/WF/Comm/Port/ErrorPage.aspx";
            string url = "/WF/Comm/Port/ErrorPage.aspx?d=" + DateTime.Now.ToString();
            this.WinOpenShowModalDialog(url, "����", "msg", 500, 400, 120, 270);
        }
        protected void ResponseWriteShowModalDialogBlueMsg(string msg)
        {
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            string url = "/WF/Comm/Port/InfoPage.aspx?d=" + DateTime.Now.ToString();
            this.WinOpenShowModalDialog(url, "��ʾ", "msg", 500, 400, 120, 270);
        }

        protected void WinOpenShowModalDialog(string url, string title, string key, int width, int height, int top, int left)
        {
            //url=this.Request.ApplicationPath+"Comm/ShowModalDialog.htm?"+url;
            //this.RegisterStartupScript(key,"<script language='JavaScript'>window.showModalDialog('"+url+"','"+key+"' ,'dialogHeight: 500px; dialogWidth:"+width+"px; dialogTop: "+top+"px; dialogLeft: "+left+"px; center: yes; help: no' ) ;  </script> ");

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language='JavaScript'>window.showModalDialog('" + url + "','" + key + "' ,'dialogHeight: 500px; dialogWidth:" + width + "px; dialogTop: " + top + "px; dialogLeft: " + left + "px; center: yes; help: no' ) ;  </script> ");

        }
        protected void WinOpenShowModalDialogResponse(string url, string title, string key, int width, int height, int top, int left)
        {
            url = this.Request.ApplicationPath + "Comm/ShowModalDialog.htm?" + url;
            this.Response.Write("<script language='JavaScript'>window.showModalDialog('" + url + "','" + key + "' ,'dialogHeight: 500px; dialogWidth:" + width + "px; dialogTop: " + top + "px; dialogLeft: " + left + "px; center: yes; help: no' ) ;  </script> ");
        }

        protected void ResponseWriteRedMsg(Exception ex)
        {
            this.ResponseWriteRedMsg(ex.Message);
        }
        /// <summary>
        /// �����ҳ������ɫ����Ϣ��
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        protected void ResponseWriteBlueMsg(string msg)
        {
            msg = msg.Replace("@", "<br>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            System.Web.HttpContext.Current.Application["info" + WebUser.No] = msg;
            string url = "/WF/Comm/Port/InfoPage.aspx?d=" + DateTime.Now.ToString();
            //   string url = "/WF/Comm/Port/InfoPage.aspx?d=" + DateTime.Now.ToString();
            this.WinOpen(url, "��Ϣ", "d" + this.Session.SessionID, 500, 400, 150, 270);
        }

        protected void AlertHtmlMsg(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            msg = msg.Replace("@", "<br>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            string url = "MsgPage.aspx?d=" + DateTime.Now.ToString();
            this.WinOpen(url, "��Ϣ", this.Session.SessionID, 500, 400, 150, 270);
        }
        /// <summary>
        /// ����ɹ�
        /// </summary>
        protected void ResponseWriteBlueMsg_SaveOK()
        {
            Alert("����ɹ���", false);
        }
        /// <summary>
        /// ����ɹ�
        /// </summary>
        /// <param name="num">��¼������</param>
        protected void ResponseWriteBlueMsg_SaveOK(int num)
        {
            Alert("����[" + num + "]����¼����ɹ���", false);
        }
        /// <summary>
        /// ResponseWriteBlueMsg_DeleteOK
        /// </summary>
        protected void ResponseWriteBlueMsg_DeleteOK()
        {

            this.Alert("ɾ���ɹ���", false);
            //
            //���³ɹ�
            //			//this.Alert("ɾ���ɹ�!");
            //			ResponseWriteBlueMsg("ɾ���ɹ�!");
        }
        /// <summary>
        /// "����["+delNum+"]����¼ɾ���ɹ���"
        /// </summary>
        /// <param name="delNum">delNum</param>
        protected void ResponseWriteBlueMsg_DeleteOK(int delNum)
        {
            //this.Alert("ɾ���ɹ�!");
            this.Alert("����[" + delNum + "]����¼ɾ���ɹ���", false);

        }
        /// <summary>
        /// ResponseWriteBlueMsg_UpdataOK
        /// </summary>
        protected void ResponseWriteBlueMsg_UpdataOK()
        {
            //this.ResponseWriteBlueMsg("���³ɹ�",false);
            this.Alert("���³ɹ�!");
            // ResponseWriteBlueMsg("���³ɹ�!");
        }
        protected void ToSignInPage()
        {
            System.Web.HttpContext.Current.Response.Redirect(SystemConfig.PageOfLostSession);
        }
        protected void ToWelPage()
        {
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "Wel.aspx");
        }
        protected void ToErrorPage(Exception mess)
        {
            this.ToErrorPage(mess.Message);
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        protected void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect("/WF/Comm/Port/ToErrorPage.aspx?d=" + DateTime.Now.ToString(), false);
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        protected void ToMsgPage(string mess)
        {
            mess = mess.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = mess;
            if (SystemConfig.AppSettings["PageMsg"] == null)
                System.Web.HttpContext.Current.Response.Redirect("/WF/Comm/Port/InfoPage.aspx?d=" + DateTime.Now.ToString(), false);
            else
                System.Web.HttpContext.Current.Response.Redirect(SystemConfig.AppSettings["PageMsg"] + "?d=" + DateTime.Now.ToString(), false);
        }
        protected void ToMsgPage_Do(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect("./Port/InfoPage.aspx?d=" + DateTime.Now.ToString(), false);
        }
        #endregion

        /// <summary>
        ///ת��һ��ҳ���ϡ� '_top'
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="target">'_top'</param>
        protected void ToErrorPage(string mess, string target)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect("/WF/Comm/Port/InfoPage.aspx target='_top'");
        }

        /// <summary>
        /// ���ڵ�OnInit�¼����Զ���ҳ���ϼ�һ�¼�¼��ǰ�е�Hidden
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //ShowRuning();
            base.OnInit(e);

            if (this.WhoCanUseIt() != null)
            {
                if (this.WhoCanUseIt() == WebUser.No)
                    return;
                if (this.WhoCanUseIt().IndexOf("," + WebUser.No + ",") == -1)
                    this.ToErrorPage("��û��Ȩ�޷������ҳ�档");
            }




        }

        #region ��ؼ��й�ϵ�Ĳ���
        public void ShowDataTable(System.Data.DataTable dt)
        {
            this.Response.Write(this.DataTable2Html(dt, true));
        }
        /// <summary>
        /// ��ʾDataTable.
        /// </summary>
        public string DataTable2Html(System.Data.DataTable dt, bool isShowTitle)
        {
            string str = "";
            if (isShowTitle)
            {
                str = dt.TableName + " �ϼ�:" + dt.Rows.Count + "��¼.";
            }
            str += "<Table>";
            str += "<TR>";
            foreach (DataColumn dc in dt.Columns)
            {
                str += "  <TD warp=false >";
                str += dc.ColumnName;
                str += "  </TD>";
            }
            str += "</TR>";



            foreach (DataRow dr in dt.Rows)
            {
                str += "<TR>";

                foreach (DataColumn dc in dt.Columns)
                {
                    str += "  <TD>";
                    str += dr[dc.ColumnName];
                    str += "  </TD>";
                }
                str += "</TR>";
            }

            str += "</Table>";
            return str;

            //this.ResponseWriteBlueMsg(str);


        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void ShowRuning()
        {
            //if (this.IsPostBack==false)
            //	return ;		


            string str = "<script language=javascript><!-- function showRuning() {	sending.style.visibility='visible' } --> </script>";


            // if (!this.IsClientScriptBlockRegistered("ClientProxyScript"))
            //   this.RegisterClientScriptBlock("ClientProxyScript", str);

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ClientProxyScript"))
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ClientProxyScript", str);

            if (this.IsPostBack == false)
            {
                str = "<div id='sending' style='position: absolute; top: 126; left: -25; z-index: 10; visibility: hidden; width: 903; height: 74'><TABLE WIDTH=100% BORDER=0 CELLSPACING=0 CELLPADDING=0><TR><td width=30%></td><TD bgcolor=#ff9900><TABLE WIDTH=100% height=70 BORDER=0 CELLSPACING=2 CELLPADDING=0><TR><td bgcolor=#eeeeee align=center>ϵͳ������Ӧ��������, ���Ժ�...</td></tr></table></td><td width=30%></td></tr></table></div> ";
                this.Response.Write(str);
            }
        }

        #endregion

        #region ͼƬ����

        /// <summary>
        /// �Ƿ�Ҫ��鹦��
        /// </summary>
        protected bool IsCheckFunc
        {
            get
            {
                //if (this.SubPageMessage==null || this.SubPageTitle==null) 
                //return false;

                if (ViewState["IsCheckFunc"] != null)
                    return (bool)ViewState["IsCheckFunc"];
                else
                    return true;

            }
            set { ViewState["IsCheckFunc"] = value; }
        }


        #endregion

        #region ����session ������

        public static object GetSessionObjByKey(string key)
        {
            object val = System.Web.HttpContext.Current.Session[key];
            return val;
        }
        public static string GetSessionByKey(string key)
        {
            return (string)GetSessionObjByKey(key);
        }
        /// <summary>
        /// ȡ�����ַ����е� Key1:val1;Key2:val2;  ֵ. 
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public static string GetSessionByKey(string key1, string key2)
        {
            string str = GetSessionByKey(key1);
            if (str == null)
                throw new Exception("û��ȡ��" + key1 + "��ֵ.");

            string[] strs = str.Split(';');
            foreach (string s in strs)
            {
                string[] ss = s.Split(':');
                if (ss[0] == key2)
                    return ss[1];
            }
            return null;
        }
        public static void SetSessionByKey(string key, object obj)
        {
            System.Web.HttpContext.Current.Session[key] = obj;
        }
        public static void SetSessionByKey(string key1, string key2, object obj)
        {
            string str = GetSessionByKey(key1);
            string KV = key2 + ":" + obj.ToString() + ";";
            if (str == null)
            {
                SetSessionByKey(key1, KV);
                return;
            }



            string[] strs = str.Split(';');
            foreach (string s in strs)
            {
                string[] ss = s.Split(':');
                if (ss[0] == key2)
                {
                    SetSessionByKey(key1, str.Replace(s + ";", KV));
                    return;
                }
            }

            SetSessionByKey(key1, str + KV);
        }
        #endregion

        #region ���� ViewState �Ĳ�����
        /// <summary>
        /// ���� ViewState Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="DefaultVal"></param>
        public void SetValueByKey_del(string key, object val, object DefaultVal)
        {
            if (val == null)
                ViewState[key] = DefaultVal;
            else
                ViewState[key] = val;
        }
        /// <summary>
        /// ȡ��Val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValueByKey_del(string key)
        {
            try
            {
                return ViewState[key].ToString();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// ss
        /// </summary>
        /// <param name="key">ss</param>
        /// <param name="DefaultVal">ss</param>
        /// <returns></returns>
        public string GetValueByKey_del(string key, string DefaultVal)
        {
            try
            {
                return ViewState[key].ToString();
            }
            catch
            {
                return DefaultVal;
            }
        }
        /// <summary>
        /// ����key ȡ����,bool ��ֲ. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public bool GetBoolValusByKey_del(string key, bool DefaultValue)
        {
            try
            {
                return bool.Parse(this.GetValueByKey_del(key));
            }
            catch
            {
                return DefaultValue;
            }
        }
        /// <summary>
        /// ȡ��int valus , ���û�оͷ��� DefaultValue ;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetIntValueByKey_del(string key, int DefaultValue)
        {
            try
            {
                return int.Parse(ViewState[key].ToString());
            }
            catch
            {
                return DefaultValue;
            }
        }
        #endregion



        /// <summary>
        /// ���table ����������ҳ���ϵ�DataGride. 
        /// </summary>
        protected System.Data.DataTable Table
        {
            get
            {
                //DataTable dt = (System.Data.DataTable)ViewState["Table"];
                System.Data.DataTable dt = (System.Data.DataTable)ViewState["Table"];
                if (dt == null)
                    dt = new System.Data.DataTable();
                return dt;
            }
            set
            {
                ViewState["Table"] = value;
            }
        }
        protected System.Data.DataTable Table_bak
        {
            get
            {
                //DataTable dt = (System.Data.DataTable)ViewState["Table"];
                System.Data.DataTable dt = this.Session["Table"] as System.Data.DataTable;
                if (dt == null)
                    dt = new System.Data.DataTable();
                return dt;
            }
            set
            {
                this.Session["Table"] = value;
            }
        }
        protected System.Data.DataTable Table1
        {
            get
            {

                System.Data.DataTable dt = (System.Data.DataTable)ViewState["Table1"];
                if (dt == null)
                    dt = new System.Data.DataTable();
                return dt;
            }
            set
            {
                ViewState["Table1"] = value;
            }
        }
        /// <summary>
        /// Ӧ�ó�������
        /// </summary>
        protected string PK
        {
            get
            {
                try
                {
                    return ViewState["PK"].ToString();
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                ViewState["PK"] = value;
            }
        }
        /// <summary>
        /// ��������״̬��
        /// </summary>
        protected bool IsNew_del
        {
            get
            {
                try
                {
                    return (bool)ViewState["IsNew"];
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                ViewState["IsNew"] = value;
            }
        }
        /// <summary>
        /// PKOID if is null return 0 
        /// </summary>
        protected int PKint
        {
            get
            {
                try
                {
                    return int.Parse(ViewState["PKint"].ToString());
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                ViewState["PKint"] = value;
            }
        }
        //		protected void ShowMessage(string msg)
        //		{
        //			PubClass.ShowMessage(msg);
        //		}		
        //		protected void ShowMessage_SaveOK()
        //		{
        //			PubClass.ShowMessageMSG_SaveOK();
        //		}
        protected void ShowMessage_SaveUnsuccessful()
        {
            //PubClass.ShowMessage(msg);
        }

        //		protected void ShowMessage_UpdateSuccessful()
        //		{
        //			PubClass.ShowMessage("���³ɹ���");
        //		}
        protected void ShowMessage_UpdateUnsuccessful()
        {
            //PubClass.ShowMessage(msg);
        }
        protected void Write_Javascript(string script)
        {
            script = script.Replace("<", "[");
            script = script.Replace(">", "]");
            Response.Write("<script language=javascript> " + script + " </script>");
        }
        protected void ShowMessageWin(string url)
        {
            this.Response.Write("<script language='JavaScript'> window.open('" + url + "')</script>");
        }
        protected void Alert(string mess)
        {
            if (string.IsNullOrEmpty(mess))
                return;

            this.Alert(mess, false);
        }
        /// <summary>
        /// ����page ������show message
        /// </summary>
        /// <param name="mess"></param>
        protected void Alert(string mess, bool isClent)
        {
            if (string.IsNullOrEmpty(mess))
                return;

            //this.ResponseWriteRedMsg(mess);
            //return;
            mess = mess.Replace("'", "��");

            mess = mess.Replace("\"", "��");

            mess = mess.Replace(";", "��");
            mess = mess.Replace(")", "��");
            mess = mess.Replace("(", "��");

            mess = mess.Replace(",", "��");
            mess = mess.Replace(":", "��");


            mess = mess.Replace("<", "��");
            mess = mess.Replace(">", "��");

            mess = mess.Replace("[", "��");
            mess = mess.Replace("]", "��");


            mess = mess.Replace("@", "\\n@");
            string script = "<script language=JavaScript>alert('" + mess + "');</script>";
            if (isClent)
                System.Web.HttpContext.Current.Response.Write(script);
            else
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "kesy", script);
            //this.RegisterStartupScript("key1", script);
        }

        protected void Alert(Exception ex)
        {
            this.Alert(ex.Message, false);
        }
        #region �����ķ���

        #region ������������
        /// <summary>
        /// ����DataTable���ݵ�����Excel��  
        /// </summary>
        /// <param name="dt">Ҫ�������ݵ�DataTable</param>
        /// <param name="filepath">Ҫ�������ļ�·��</param>
        /// <param name="filename">Ҫ�������ļ�</param>
        /// <returns></returns>
        protected bool ExportDataTableToExcel_OpenWin_del(System.Data.DataTable dt, string title)
        {
            string filename = "Ep" + this.Session.SessionID + ".xls";
            string file = filename;
            bool flag = true;
            string filepath = SystemConfig.PathOfTemp;

            #region ���� datatable
            foreach (DataColumn dc in dt.Columns)
            {
                switch (dc.ColumnName)
                {
                    case "No":
                        dc.Caption = "���";
                        break;
                    case "Name":
                        dc.Caption = "����";
                        break;
                    case "Total":
                        dc.Caption = "�ϼ�";
                        break;
                    case "FK_Dept":
                        dc.Caption = "���ű��";
                        break;
                    case "ZSJGName":
                        dc.Caption = "��������";
                        break;
                    case "IncNo":
                        dc.Caption = "��˰�˱��";
                        break;
                    case "IncName":
                        dc.Caption = "��˰������";
                        break;
                    case "TaxpayerNo":
                        dc.Caption = "��˰�˱��";
                        break;
                    case "TaxpayerName":
                        dc.Caption = "��˰������";
                        break;
                    case "byrk":
                        dc.Caption = "�������";
                        break;
                    case "ljrk":
                        dc.Caption = "�ۼ����";
                        break;
                    case "qntq":
                        dc.Caption = "ȥ��ͬ��";
                        break;
                    case "jtqzje":
                        dc.Caption = "��ȥ��������";
                        break;
                    case "jtqzjl":
                        dc.Caption = "��ȥ��������";
                        break;
                    case "BenYueYiJiao":
                        dc.Caption = "�����ѽ�";
                        break;
                    case "BenYueYingJiao":
                        dc.Caption = "����Ӧ��";
                        break;
                    case "BenYueWeiJiao":
                        dc.Caption = "����δ��";
                        break;
                    case "LeiJiWeiJiao":
                        dc.Caption = "�ۼ�δ��";
                        break;
                    case "QuNianTongQiLeiJiYiJiao":
                        dc.Caption = "ȥ��ͬ��δ��";
                        break;

                    case "QianNianTongQiLeiJiYiJiao":
                        dc.Caption = "ǰ��ͬ���ۼ��ѽ�";
                        break;
                    case "QianNianTongQiLeiJiYingJiao":
                        dc.Caption = "ǰ��ͬ���ۼ�Ӧ��";
                        break;

                    case "JiaoQuNianTongQiZhengJian":
                        dc.Caption = "��ȥ��ͬ������";
                        break;
                    case "JiaoQuNianTongQiZhengJianLv":
                        dc.Caption = "��ȥ��ͬ��������";
                        break;

                    case "JiaoQianNianTongQiZhengJian":
                        dc.Caption = "��ȥ��ͬ������";
                        break;
                    case "JiaoQianNianTongQiZhengJianLv":
                        dc.Caption = "��ǰ��ͬ��������";
                        break;
                    case "LeiJiYiJiao":
                        dc.Caption = "�ۼ��ѽ�";
                        break;
                    case "LeiJiYingJiao":
                        dc.Caption = "�ۼ�Ӧ��";
                        break;
                    case "QuNianBenYueYiJiao":
                        dc.Caption = "ȥ�걾���ѽ�";
                        break;
                    case "QuNianBenYueYingJiao":
                        dc.Caption = "ȥ�걾��Ӧ��";
                        break;
                    case "QuNianLeiJiYiJiao":
                        dc.Caption = "ȥ���ۼ��ѽ�";
                        break;
                    case "QuNianLeiJiYingJiao":
                        dc.Caption = "ȥ���ۼ�Ӧ��";
                        break;
                    case "QianNianBenYueYiJiao":
                        dc.Caption = "ǰ�걾���ѽ�";
                        break;
                    case "QianNianBenYueYingJiao":
                        dc.Caption = "ǰ�걾��Ӧ��";
                        break;
                    case "QianNianLeiJiYiJiao":
                        dc.Caption = "ǰ��ͬ���ۼ��ѽ�";
                        break;
                    case "QianNianLeiJiYingJiao":
                        dc.Caption = "ǰ��ͬ���ۼ�Ӧ��";
                        break;
                    case "JiaoQuNianZhengJian":
                        dc.Caption = "��ȥ��ͬ������";
                        break;
                    case "JiaoQuNianZhengJianLv":
                        dc.Caption = "��ȥ��ͬ��������";
                        break;
                    case "JiaoQianNianZhengJian":
                        dc.Caption = "��ǰ��ͬ������";
                        break;
                    case "JiaoQianNianZhengJianLv":
                        dc.Caption = "��ǰ��ͬ��������";
                        break;
                    case "level":
                        dc.Caption = "����";
                        break;
                }
            }
            #endregion

            #region ��������������
            //����У��
            if (dt == null || dt.Rows.Count <= 0 || filename == null || filename == "" || filepath == null || filepath == "")
                return false;

            //�������Ŀ¼û�н���������
            if (Directory.Exists(filepath) == false) Directory.CreateDirectory(filepath);

            filename = filepath + filename;

            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            #region ���ɵ����ļ�
            try
            {
                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine(Convert.ToChar(9) + title + Convert.ToChar(9));
                objStreamWriter.WriteLine();
                string strLine = "";
                //�����ļ�����
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strLine = strLine + dt.Columns[i].Caption + Convert.ToChar(9);
                }

                objStreamWriter.WriteLine(strLine);

                strLine = "";

                //�����ļ�����
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        strLine = strLine + dt.Rows[row][col] + Convert.ToChar(9);
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine(Convert.ToChar(9) + "�Ʊ��ˣ�" + Convert.ToChar(9) + WebUser.Name + Convert.ToChar(9) + "���ڣ�" + Convert.ToChar(9) + DateTime.Now.ToShortDateString());

            }
            catch
            {
                flag = false;
            }
            finally
            {
                objStreamWriter.Close();
                objFileStream.Close();
            }
            #endregion

            #region ɾ�����ɵ��ļ�
            DelExportedTempFile(filepath);
            #endregion


            if (flag)
            {
                this.WinOpen("../Temp/" + file);
                //this.Write_Javascript(" window.open( ); " );
            }

            return flag;
        }
        /// <summary>
        /// ɾ��������ʱ��������ʱ�ļ� 2002.11.09 create by bluesky 
        /// </summary>
        /// <param name="filepath">��ʱ�ļ�·��</param>
        /// <returns></returns>
        public bool DelExportedTempFile(string filepath)
        {
            bool flag = true;
            try
            {
                string[] files = Directory.GetFiles(filepath);

                for (int i = 0; i < files.Length; i++)
                {
                    DateTime lastTime = File.GetLastWriteTime(files[i]);
                    TimeSpan span = DateTime.Now - lastTime;

                    if (span.Hours >= 1)
                        File.Delete(files[i]);
                }
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        #endregion ������


        #endregion

    }
}

