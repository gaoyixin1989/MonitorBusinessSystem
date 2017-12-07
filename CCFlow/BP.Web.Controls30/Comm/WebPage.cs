using System;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using BP.Web;
using BP.DA;
using BP.En;
using BP.Sys;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BP.Web
{
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public enum EnsShowType
    {
        /// <summary>
        /// table
        /// </summary>
        Table,
        /// <summary>
        /// ͼƬ��ʾ
        /// </summary>
        Card
    }
    /// <summary>
    /// PortalPage ��ժҪ˵����
    /// </summary>
    public class WebPage : PageBase
    {
        #region ����
        public string RequestParas
        {
            get
            {
                string urlExt = "";
                string rawUrl = this.Request.RawUrl;
                rawUrl = "&" + rawUrl.Substring(rawUrl.IndexOf('?') + 1);
                string[] paras = rawUrl.Split('&');
                foreach (string para in paras)
                {
                    if (para == null
                        || para == ""
                        || para.Contains("=") == false)
                        continue;
                    urlExt += "&" + para;
                }
                return urlExt;
            }
        }
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


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (this.Request.Browser.Cookies == false)
            //    throw new Exception("�����������֧��cookies���ܣ��޷�ʹ�ø�ϵͳ��");
        }
       
        public string FK_Sort
        {
            get
            {
                string s = this.Request.QueryString["FK_Sort"];
                if (s == null)
                    s = "01";
                return s;
            }
        }
        public int Step
        {
            get
            {
                try
                {
                    return int.Parse(this.Request.QueryString["Step"]);
                }
                catch
                {
                    return 1;
                }
            }
        }

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
        public string DoType
        {
            get
            {
                string str= this.Request.QueryString["DoType"];
                if (str == "")
                    str = null;
                return str;
            }
        }
        public string EnsName
        {
            get
            {
                string s = this.Request.QueryString["EnsName"];
                if (s == null)
                    s = this.Request.QueryString["EnsName"];

                return s;
            }
        }
        public string EnName
        {
            get
            {
                string s = this.Request.QueryString["EnName"];
                if (s == null)
                    s = this.Request.QueryString["EnName"];
                return s;
            }
        }
        public string RefPK
        {
            get
            {
                string s = this.Request.QueryString["RefPK"];
                if (s == null || s=="" )
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
                    return 1;
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
                if (s == null || s == "")
                    s = this.Request.QueryString["No"];

                if (s == null || s == "")
                    s = null;
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

        protected void ExportDGToExcelV1(System.Data.DataTable dt)
        {
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
            string filename = title + "_" + BP.DA.DataType.CurrentDataCNOfLong + "_" + WebUser.Name + ".xls";//"Ep" + this.Session.SessionID + ".xls";
            string file = filename;
            bool flag = true;
            string filepath = BP.Sys.SystemConfig.PathOfTemp;

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
                this.WinOpen("/DataUser/Temp/" + file);
                //this.Write_Javascript(" window.open('"+ Request.ApplicationPath + @"/Report/Exported/" + filename +"'); " );
                //this.Write_Javascript(" window.open('"+Request.ApplicationPath+"/Temp/" + file +"'); " );
            }

            return file;
        }
        public string ExportDGToExcel(System.Data.DataTable dt, string title)
        {
            title = title.Trim();
            string filename = "Ep" + title + ".xls";
            string file = filename;
            bool flag = true;
            string filepath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\";

            #region ��������������
            //			//����У��
            //			if (dg == null || dg.Items.Count <=0 || filename == null || filename == "" || filepath == null || filepath == "")
            //				return null;

            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

          

            filename = filepath + filename;

            //filename = HttpUtility.UrlEncode(filename);

            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            #region ���ɵ����ļ�
            try
            {
                objStreamWriter.WriteLine(Convert.ToChar(9) + title + Convert.ToChar(9));
                string strLine = "";
                //�����ļ�����
                foreach (DataColumn attr in dt.Columns)
                {
                    strLine = strLine + attr.ColumnName + Convert.ToChar(9);
                }

                objStreamWriter.WriteLine(strLine);
                strLine = "";
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn attr in dt.Columns)
                    {
                        strLine = strLine + dr[attr.ColumnName] + Convert.ToChar(9);
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                //    objStreamWriter.WriteLine();
                //   objStreamWriter.WriteLine(Convert.ToChar(9) + " �Ʊ��ˣ�" + Convert.ToChar(9) + WebUser.Name + Convert.ToChar(9) + "���ڣ�" + Convert.ToChar(9) + DateTime.Now.ToShortDateString());
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

                BP.Web.Ctrl.Glo MyFileDown = new BP.Web.Ctrl.Glo();

                MyFileDown.DownFileByPath(filename,
                     title+file);
                //this.WinOpen(this.Request.ApplicationPath + "/Temp/" + file,"down",90,90);
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

        protected void ExportDGToExcelDtl(Entities ens, string outFileName)
        {
            System.Web.HttpResponse resp;
            resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + outFileName);
            resp.ContentType = "application/ms-excel";

            string colHeaders = "", ls_item = "";

            //�����������ж���ͬʱ��DataSet����ֵ���г�ʼ�� 
            System.Data.DataTable dt = ens.ToDataTableDesc();
            dt.Columns.Remove("FID");
            dt.Columns.Remove("����ID");
            dt.Columns.Remove("����");

            //  dt.Columns.Remove("BatchID");

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
            //return WebPage.GenerExcelFile(dt);

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
            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

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
        /// <summary>
        /// ������excel
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected string ExportDGToExcel(System.Data.DataTable dt)
        {
            string filename = WebUser.No + ".xls";
            string file = filename;
            string filepath = SystemConfig.PathOfWebApp + "\\Temp\\";

            #region ��������������
            //�������Ŀ¼û�н���������.
            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            filename = filepath + filename;
            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

            FileStream objFileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.UTF32);
            #endregion

            string strLine = "";
            //�����ļ�����
            foreach (DataColumn dc in dt.Columns)
            {
                strLine = strLine + dc.ColumnName + Convert.ToChar(9);
            }

            objStreamWriter.WriteLine(strLine);
            foreach (DataRow dr in dt.Rows)
            {
                /*�����ļ�����*/
                strLine = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    string str = dr[dc.ColumnName] as string;
                    if (str == "" || str == null)
                        str = " ";
                    strLine = strLine + str + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine.Replace(Convert.ToChar(9).ToString() + Convert.ToChar(9).ToString(), Convert.ToChar(9).ToString()));
            }
            objStreamWriter.Close();
            objFileStream.Close();

            PubClass.WinOpen(this.Request.ApplicationPath + "/Temp/" + file);
            return file;
        }
        public void Page_Error(object sender, EventArgs e)
        {
            if (SystemConfig.IsDebug == false)
            {
                //    //Exception objErr = Server.GetLastError().GetBaseException();
                //    //string err = "<img alt=\"��������\" src=\"http://" + Request.Url.Host + "/" + Request.ApplicationPath + "/Images/err.gif\" />" + objErr.Message.ToString();
                //Exception objErr = Server.GetLastError().GetBaseException();
                //string err = "<body  style='width:100%;height:100%;text-align:center;'><div style='width:300px;height:220px;margin:auto auto;'><img alt=\"��������\" src=\"http://" 
                //    + Request.Url.Host+"/"+ Request.ApplicationPath+"/Images/err.gif\" /><br /><br />" 
                //    + objErr.Message.ToString()
                //    + "</div></body>";

                //Response.Write(err.ToString());
                //Server.ClearError();
                //DA.Log.DebugWriteError(objErr.Message);

                //Response.Write(err.ToString());
                //Server.ClearError();
                //DA.Log.DebugWriteError(objErr.Message);
            }
        }

    }
}

