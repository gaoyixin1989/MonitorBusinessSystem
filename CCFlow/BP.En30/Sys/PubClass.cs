using System;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;
using BP.Web;
using System.Text.RegularExpressions;
using BP.Port;

namespace BP.Sys
{
    /// <summary>
    /// PageBase ��ժҪ˵����
    /// </summary>
    public class PubClass
    {
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="maillAddr">��ַ</param>
        /// <param name="title">����</param>
        /// <param name="doc">����</param>
        public static void SendMail(string maillAddr, string title, string doc)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new System.Net.Mail.MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);

            myEmail.To.Add(maillAddr);
            myEmail.Subject = title;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//�ʼ��������

            myEmail.Body = doc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//�ʼ����ݱ���
            myEmail.IsBodyHtml = true;//�Ƿ���HTML�ʼ�

            myEmail.Priority = MailPriority.High;//�ʼ����ȼ�

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(SystemConfig.GetValByKey("SendEmailAddress", "ccflow.cn@gmail.com"),
                SystemConfig.GetValByKey("SendEmailPass", "ccflow123"));

            //����д������������
            client.Port = SystemConfig.GetValByKeyInt("SendEmailPort", 587); //ʹ�õĶ˿�
            client.Host = SystemConfig.GetValByKey("SendEmailHost", "smtp.gmail.com");
            client.EnableSsl = true; //����ssl����.
            object userState = myEmail;
            try
            {
                client.Send(myEmail);

                //   client.SendAsync(myEmail, userState);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
        public static string ToHtmlColor(string colorName)
        {
            try
            {
                if (colorName.StartsWith("#"))
                    colorName = colorName.Replace("#", string.Empty);
                int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);

                Color col = Color.FromArgb
               (
                     Convert.ToByte((v >> 24) & 255),
                     Convert.ToByte((v >> 16) & 255),
                     Convert.ToByte((v >> 8) & 255),
                     Convert.ToByte((v >> 0) & 255)
                );

                int alpha = col.A;
                var red = Convert.ToString(col.R, 16); ;
                var green = Convert.ToString(col.G, 16);
                var blue = Convert.ToString(col.B, 16);
                return string.Format("#{0}{1}{2}", red, green, blue);
            }
            catch
            {
                return "black";
            }
        }
        public static void InitFrm(string fk_mapdata)
        {
            // ɾ������.
            FrmLabs labs = new FrmLabs();
            labs.Delete(FrmLabAttr.FK_MapData, fk_mapdata);

            FrmLines lines = new FrmLines();
            lines.Delete(FrmLabAttr.FK_MapData, fk_mapdata);

            MapData md = new MapData();
            md.No = fk_mapdata;
            if (md.RetrieveFromDBSources() == 0)
            {
                MapDtl mdtl = new MapDtl();
                mdtl.No = fk_mapdata;
                if (mdtl.RetrieveFromDBSources() == 0)
                {
                    throw new Exception("@��:" + fk_mapdata + "��ӳ����Ϣ������.");
                }
                else
                {
                    md.Copy(mdtl);
                }
            }

            MapAttrs mattrs = new MapAttrs(fk_mapdata);
            GroupFields gfs = new GroupFields(fk_mapdata);

            int tableW = 700;
            int padingLeft = 3;
            int leftCtrlX = 700 / 100 * 20;
            int rightCtrlX = 700 / 100 * 60;

            string keyID = DateTime.Now.ToString("yyMMddhhmmss");
            // table ���⡣
            int currX = 0;
            int currY = 0;
            FrmLab lab = new FrmLab();
            lab.Text = md.Name;
            lab.FontSize = 20;
            lab.X = 200;
            currY += 30;
            lab.Y = currY;
            lab.FK_MapData = fk_mapdata;
            lab.FontWeight = "Bold";
            lab.MyPK = "Lab" + keyID + "1";
            lab.Insert();

            // ���ͷ���ĺ���.
            currY += 20;
            FrmLine lin = new FrmLine();
            lin.X1 = 0;
            lin.X2 = tableW;
            lin.Y1 = currY;
            lin.Y2 = currY;
            lin.BorderWidth = 2;
            lin.FK_MapData = fk_mapdata;
            lin.MyPK = "Lin" + keyID + "1";
            lin.Insert();
            currY += 5;

            bool isLeft = false;
            int i = 2;
            foreach (GroupField gf in gfs)
            {
                i++;
                lab = new FrmLab();
                lab.X = 0;
                lab.Y = currY;
                lab.Text = gf.Lab;
                lab.FK_MapData = fk_mapdata;
                lab.FontWeight = "Bold";
                lab.MyPK = "Lab" + keyID + i.ToString();
                lab.Insert();

                currY += 15;
                lin = new FrmLine();
                lin.X1 = padingLeft;
                lin.X2 = tableW;
                lin.Y1 = currY;
                lin.Y2 = currY;
                lin.FK_MapData = fk_mapdata;
                lin.BorderWidth = 3;
                lin.MyPK = "Lin" + keyID + i.ToString();
                lin.Insert();

                isLeft = true;
                int idx = 0;
                foreach (MapAttr attr in mattrs)
                {
                    if (gf.OID != attr.GroupID || attr.UIVisible == false)
                        continue;

                    idx++;
                    if (isLeft)
                    {
                        lin = new FrmLine();
                        lin.X1 = 0;
                        lin.X2 = tableW;
                        lin.Y1 = currY;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx;
                        lin.Insert();
                        currY += 14; /* ��һ���� .*/

                        lab = new FrmLab();
                        lab.X = lin.X1 + padingLeft;
                        lab.Y = currY;
                        lab.Text = attr.Name;
                        lab.FK_MapData = fk_mapdata;
                        lab.MyPK = "Lab" + keyID + i.ToString() + idx;
                        lab.Insert();

                        lin = new FrmLine();
                        lin.X1 = leftCtrlX;
                        lin.Y1 = currY - 14;

                        lin.X2 = leftCtrlX;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx + "R";
                        lin.Insert(); /*��һ ���� */

                        attr.X = leftCtrlX + padingLeft;
                        attr.Y = currY - 3;
                        attr.UIWidth = 150;
                        attr.Update();
                        currY += 14;
                    }
                    else
                    {
                        currY = currY - 14;
                        lab = new FrmLab();
                        lab.X = tableW / 2 + padingLeft;
                        lab.Y = currY;
                        lab.Text = attr.Name;
                        lab.FK_MapData = fk_mapdata;
                        lab.MyPK = "Lab" + keyID + i.ToString() + idx;
                        lab.Insert();

                        lin = new FrmLine();
                        lin.X1 = tableW / 2;
                        lin.Y1 = currY - 14;

                        lin.X2 = tableW / 2;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx;
                        lin.Insert(); /*��һ ���� */

                        lin = new FrmLine();
                        lin.X1 = rightCtrlX;
                        lin.Y1 = currY - 14;
                        lin.X2 = rightCtrlX;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx + "R";
                        lin.Insert(); /*��һ ���� */

                        attr.X = rightCtrlX + padingLeft;
                        attr.Y = currY - 3;
                        attr.UIWidth = 150;
                        attr.Update();
                        currY += 14;
                    }
                    isLeft = !isLeft;
                }
            }
            // table bottom line.
            lin = new FrmLine();
            lin.X1 = 0;
            lin.Y1 = currY;

            lin.X2 = tableW;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eR";
            lin.Insert();

            currY = currY - 28 - 18;
            // �����β. table left line
            lin = new FrmLine();
            lin.X1 = 0;
            lin.Y1 = 50;
            lin.X2 = 0;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eRr";
            lin.Insert();

            // table right line.
            lin = new FrmLine();
            lin.X1 = tableW;
            lin.Y1 = 50;
            lin.X2 = tableW;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eRr4";
            lin.Insert();
        }
        public static String ColorToStr(System.Drawing.Color color)
        {
            try
            {
                string color_s = System.Drawing.ColorTranslator.ToHtml(color);
                color_s = color_s.Substring(1, color_s.Length - 1);
                return "#" + Convert.ToString(Convert.ToInt32(color_s, 16) + 40000, 16);
            }
            catch
            {
                return "black";
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="fd"></param>
        /// <returns></returns>
        public static string DealToFieldOrTableNames(string fd)
        {
            string keys = "~!@#$%^&*()+{}|:<>?`=[];,./�����������������������������������������������࣭���ۣݣ���������";
            char[] cc = keys.ToCharArray();
            foreach (char c in cc)
                fd = fd.Replace(c.ToString(), "");
            string s = fd.Substring(0, 1);
            try
            {
                int a = int.Parse(s);
                fd = "F" + fd;
            }
            catch
            {
            }
            return fd;
        }
        private static string _KeyFields = null;
        public static string KeyFields
        {
            get
            {
                if (_KeyFields == null)
                    _KeyFields = BP.DA.DataType.ReadTextFile(SystemConfig.PathOfWebApp + SystemConfig.CCFlowWebPath + "WF\\Data\\Sys\\FieldKeys.txt");
                return _KeyFields;
            }
        }
        public static bool IsNum(string str)
        {
            Boolean strResult;
            String cn_Regex = @"^[\u4e00-\u9fa5]+$";
            if (Regex.IsMatch(str, cn_Regex))
            {
                strResult = true;
            }
            else
            {
                strResult = false;
            }
            return strResult;
        }

        public static bool IsCN(string str)
        {
            Boolean strResult;
            String cn_Regex = @"^[\u4e00-\u9fa5]+$";
            if (Regex.IsMatch(str, cn_Regex))
            {
                strResult = true;
            }
            else
            {
                strResult = false;
            }
            return strResult;
        }

        public static bool IsImg(string ext)
        {
            ext = ext.Replace(".", "").ToLower();
            switch (ext)
            {
                case "gif":
                    return true;
                case "jpg":
                    return true;
                case "bmp":
                    return true;
                case "png":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// ���ձ�����С
        /// </summary>
        /// <param name="ObjH">Ŀ��߶�</param>
        /// <param name="factH">ʵ�ʸ߶�</param>
        /// <param name="factW">ʵ�ʿ��</param>
        /// <returns>Ŀ����</returns>
        public static int GenerImgW_del(int ObjH, int factH, int factW, int isZeroAsWith)
        {
            if (factH == 0 || factW == 0)
                return isZeroAsWith;

            decimal d = decimal.Parse(ObjH.ToString()) / decimal.Parse(factH.ToString()) * decimal.Parse(factW.ToString());

            try
            {
                return int.Parse(d.ToString("0"));
            }
            catch (Exception ex)
            {
                throw new Exception(d.ToString() + ex.Message);
            }
        }

        /// <summary>
        /// ���ձ�����С
        /// </summary>
        /// <param name="ObjH">Ŀ��߶�</param>
        /// <param name="factH">ʵ�ʸ߶�</param>
        /// <param name="factW">ʵ�ʿ��</param>
        /// <returns>Ŀ����</returns>
        public static int GenerImgH(int ObjW, int factH, int factW, int isZeroAsWith)
        {
            if (factH == 0 || factW == 0)
                return isZeroAsWith;

            decimal d = decimal.Parse(ObjW.ToString()) / decimal.Parse(factW.ToString()) * decimal.Parse(factH.ToString());

            try
            {
                return int.Parse(d.ToString("0"));
            }
            catch (Exception ex)
            {
                throw new Exception(d.ToString() + ex.Message);
            }
        }


        public static string FilesViewStr(string enName, object pk)
        {
            string url = "/WF/Comm/FileManager.aspx?EnsName=" + enName + "&PK=" + pk.ToString();
            string strs = "";
            SysFileManagers ens = new SysFileManagers(enName, pk.ToString());
            string path = BP.Sys.Glo.Request.ApplicationPath;

            foreach (SysFileManager file in ens)
            {
                strs += "<img src='/WF/Img/FileType/" + file.MyFileExt.Replace(".", "") + ".gif' border=0 /><a href='" + path + file.MyFilePath + "' target='_blank' >" + file.MyFileName + file.MyFileExt + "</a>&nbsp;";
                if (file.Rec == WebUser.No)
                {
                    strs += "<a title='����' href=\"javascript:DoAction('" + path + "Comm/Do.aspx?ActionType=1&OID=" + file.OID + "&EnsName=" + enName + "&PK=" + pk + "','ɾ���ļ���" + file.MyFileName + file.MyFileExt + "��')\" ><img src='" + path + "/WF/Img/Btn/delete.gif' border=0 alt='ɾ���˸���' /></a>&nbsp;";
                }
            }
            return strs;
        }
        public static string GenerLabelStr(string title)
        {
            string path = BP.Sys.Glo.Request.ApplicationPath;
            if (path == "" || path == "/")
                path = "..";

            string str = "";
            str += "<TABLE  height='100%' cellPadding='0' background='" + path + "/Images/DG_bgright.gif'>";
            str += "<TBODY>";
            str += "<TR   >";
            str += "<TD  >";
            str += "<IMG src='" + path + "/Images/DG_Title_Left.gif' border='0'></TD>";
            str += "<TD style='font-size:14px'  vAlign='bottom' noWrap background='" + path + "/Images/DG_Title_BG.gif'>&nbsp;";
            str += " &nbsp;<b>" + title + "</b>&nbsp;&nbsp;";
            str += "</TD>";
            str += "<TD>";
            str += "<IMG src='" + path + "/Images/DG_Title_Right.gif' border='0'></TD>";
            str += "</TR>";
            str += "</TBODY>";
            str += "</TABLE>";
            return str;
            //return str;
        }
        /// <summary>
        /// ������ת����ƴ��
        /// </summary>
        /// <param name="str">Ҫת���ĺ���</param>
        /// <returns>���ص�ƴ��</returns>
        public string Chs2Pinyin(string str)
        {
            return BP.Tools.chs2py.convert(str);
        }

        public static string GenerTablePage(DataTable dt, string title)
        {

            string str = "<Table id='tb' class=Table >";

            str += "<caption>" + title + "</caption>";


            // ����
            str += "<TR>";
            foreach (DataColumn dc in dt.Columns)
            {
                str += "<TD class='DGCellOfHeader" + BP.Web.WebUser.Style + "' nowrap >" + dc.ColumnName + "</TD>";
            }
            str += "</TR>";

            //����
            foreach (DataRow dr in dt.Rows)
            {
                str += "<TR>";


                foreach (DataColumn dc in dt.Columns)
                {
                    //string doc=dr[dc.ColumnName];
                    str += "<TD nowrap=true >&nbsp;" + dr[dc.ColumnName] + "</TD>";
                }
                str += "</TR>";
            }
            str += "</Table>";
            return str;
        }
        /// <summary>
        /// ������ʱ�ļ�����
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static string GenerTempFileName(string hz)
        {
            return Web.WebUser.No + DateTime.Now.ToString("MMddhhmmss") + "." + hz;
        }
        public static void DeleteTempFiles()
        {
            //string[] strs = System.IO.Directory.GetFiles( MapPath( SystemConfig.TempFilePath )) ;
            string[] strs = System.IO.Directory.GetFiles(SystemConfig.PathOfTemp);

            foreach (string s in strs)
            {
                System.IO.File.Delete(s);
            }
        }
        /// <summary>
        /// ���½�������
        /// </summary>
        public static void ReCreateIndex()
        {
            ArrayList als = ClassFactory.GetObjects("BP.En.Entity");
            string sql = "";
            foreach (object obj in als)
            {
                Entity en = (Entity)obj;
                if (en.EnMap.EnType == EnType.View)
                    continue;
                sql += "IF EXISTS( SELECT name  FROM  sysobjects WHERE  name='" + en.EnMap.PhysicsTable + "') <BR> DROP TABLE " + en.EnMap.PhysicsTable + "<BR>";
                sql += "CREATE TABLE " + en.EnMap.PhysicsTable + " ( <BR>";
                sql += "";
            }


        }
        public static void DBIOToAccess()
        {
            ArrayList al = BP.En.ClassFactory.GetObjects("BP.En.Entities");
            PubClass.DBIO(DBType.Access, al, false);
        }
        /// <summary>
        /// ������е������
        /// </summary>
        public static void CheckAllPTable(string nameS)
        {
            ArrayList al = BP.En.ClassFactory.GetObjects("BP.En.Entities");
            foreach (Entities ens in al)
            {
                if (ens.ToString().Contains(nameS) == false)
                    continue;


                try
                {
                    Entity en = ens.GetNewEntity;
                    en.CheckPhysicsTable();
                }
                catch
                {

                }

            }

        }

        /// <summary>
        /// ���ݴ���
        /// </summary>
        /// <param name="dbtype">����</param>
        /// <returns></returns>
        public static void DBIO(DA.DBType dbtype, ArrayList als, bool creatTableOnly)
        {
            foreach (Entities ens in als)
            {
                Entity myen = ens.GetNewEntity;
                if (myen.EnMap.EnType == EnType.View)
                    continue;

                #region create table
                switch (dbtype)
                {

                    case DBType.Oracle:
                        try
                        {

                            DBAccessOfOracle.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DBAccessOfOracle.RunSQL(SqlBuilder.GenerCreateTableSQLOfOra_OK(myen));
                        }
                        catch
                        {

                        }
                        break;
                    case DBType.MSSQL:
                        try
                        {
                            if (myen.EnMap.PhysicsTable.Contains("."))
                                continue;

                            if (DBAccessOfMSMSSQL.IsExitsObject(myen.EnMap.PhysicsTable))
                                continue;

                            DBAccessOfMSMSSQL.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        DBAccessOfMSMSSQL.RunSQL(SqlBuilder.GenerCreateTableSQLOfMS(myen));
                        break;
                    case DBType.Informix:
                        try
                        {
                            if (myen.EnMap.PhysicsTable.Contains("."))
                                continue;

                            if (DBAccessOfMSMSSQL.IsExitsObject(myen.EnMap.PhysicsTable))
                                continue;

                            DBAccessOfMSMSSQL.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        DBAccessOfMSMSSQL.RunSQL(SqlBuilder.GenerCreateTableSQLOfInfoMix(myen));
                        break;
                    case DBType.Access:
                        try
                        {
                            DBAccessOfOLE.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        DBAccessOfOLE.RunSQL(SqlBuilder.GenerCreateTableSQLOf_OLE(myen));
                        break;
                    default:
                        throw new Exception("error :");

                }
                #endregion

                if (creatTableOnly)
                    return;

                try
                {
                    QueryObject qo = new QueryObject(ens);
                    qo.DoQuery();
                    // ens.RetrieveAll(1000);
                }
                catch
                {
                    continue;
                }

                #region insert data
                foreach (Entity en in ens)
                {
                    try
                    {
                        switch (dbtype)
                        {
                            case DBType.Oracle:
                            case DBType.Informix:
                                DBAccessOfOracle.RunSQL(SqlBuilder.Insert(en));
                                break;
                            case DBType.MSSQL:
                                DBAccessOfMSMSSQL.RunSQL(SqlBuilder.Insert(en));
                                break;
                            case DBType.Access:
                                DBAccessOfOLE.RunSQL(SqlBuilder.InsertOFOLE(en));
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.DefaultLogWriteLineError(dbtype.ToString() + "bak���ִ���" + ex.Message);
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// ��ȡdatatable.
        /// </summary>
        /// <param name="uiBindKey"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetDataTableByUIBineKey(string uiBindKey)
        {
            DataTable dt = new DataTable();
            if (uiBindKey.Contains("."))
            {
                Entities ens = BP.En.ClassFactory.GetEns(uiBindKey);
                if (ens == null)
                    ens = BP.En.ClassFactory.GetEns(uiBindKey);

                if (ens == null)
                    ens = BP.En.ClassFactory.GetEns(uiBindKey);
                if (ens == null)
                    throw new Exception("��������:" + uiBindKey + ",����ת����ens.");

                ens.RetrieveAllFromDBSource();
                dt = ens.ToDataTableField(uiBindKey);
                return dt;
            }
            else
            {

                string sql = "SELECT No,Name FROM " + uiBindKey;
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                dt.TableName = uiBindKey;
                return dt;
            }
        }
        /// <summary>
        /// ��ȡ����Դ
        /// </summary>
        /// <param name="uiBindKey">�󶨵��������ö��</param>
        /// <returns></returns>
        public static System.Data.DataTable GetDataTableByUIBineKeyForCCFormDesigner(string uiBindKey)
        {
            int topNum = 40;

            DataTable dt = new DataTable();
            if (uiBindKey.Contains("."))
            {
                Entities ens = BP.En.ClassFactory.GetEns(uiBindKey);
                if (ens == null)
                    ens = BP.En.ClassFactory.GetEns(uiBindKey);

                if (ens == null)
                    ens = BP.En.ClassFactory.GetEns(uiBindKey);
                if (ens == null)
                    throw new Exception("��������:" + uiBindKey + ",����ת����ens.");

                BP.En.QueryObject qo = new QueryObject(ens);
                return qo.DoQueryToTable(topNum);
            }
            else
            {
                string sql = "";
                switch (BP.Sys.SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle:
                        sql = "SELECT No,Name FROM " + uiBindKey + " where rowNum <= " + topNum;
                        break;
                    case DBType.MSSQL:
                        sql = "SELECT top " + topNum + " No,Name FROM " + uiBindKey;
                        break;
                    default:
                        sql = "SELECT  No,Name FROM " + uiBindKey;
                        break;
                }
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                dt.TableName = uiBindKey;
                return dt;
            }
        }

        #region ϵͳ����
        public static string GenerDBOfOreacle()
        {
            ArrayList als = ClassFactory.GetObjects("BP.En.Entity");
            string sql = "";
            foreach (object obj in als)
            {
                Entity en = (Entity)obj;
                sql += "IF EXISTS( SELECT name  FROM  sysobjects WHERE  name='" + en.EnMap.PhysicsTable + "') <BR> DROP TABLE " + en.EnMap.PhysicsTable + "<BR>";
                sql += "CREATE TABLE " + en.EnMap.PhysicsTable + " ( <BR>";
                sql += "";
            }
            //DA.Log.DefaultLogWriteLine(LogType.Error,msg.Replace("<br>@","\n") ); // 
            return sql;
        }
        public static string DBRpt(DBCheckLevel level)
        {
            // ȡ��ȫ����ʵ��
            ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
            string msg = "";
            foreach (object obj in als)
            {
                Entities ens = (Entities)obj;
                try
                {
                    msg += DBRpt1(level, ens);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + ens.ToString() + "���ʧ��:" + ex.Message;
                }
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAllFromDBSource();
            foreach (MapData md in mds)
            {
                try
                {
                    md.HisGEEn.CheckPhysicsTable();
                    PubClass.AddComment(md.HisGEEn);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + md.No + "���ʧ��:" + ex.Message;
                }
            }

            MapDtls dtls = new MapDtls();
            dtls.RetrieveAllFromDBSource();
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    dtl.HisGEDtl.CheckPhysicsTable();
                    PubClass.AddComment(dtl.HisGEDtl);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + dtl.No + "���ʧ��:" + ex.Message;
                }
            }

            #region ��鴦���Ҫ�Ļ������� Pub_Day .
            string sql = "";
            string sqls = "";
            sql = "SELECT count(*) Num FROM Pub_Day";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        string d = i.ToString().PadLeft(2, '0');
                        sqls += "@INSERT INTO Pub_Day(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {
            }

            sql = "SELECT count(*) Num FROM Pub_YF";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        string d = i.ToString().PadLeft(2, '0');
                        sqls += "@INSERT INTO Pub_YF(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {
            }

            sql = "SELECT count(*) Num FROM Pub_ND";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 2010; i < 2015; i++)
                    {
                        string d = i.ToString();
                        sqls += "@INSERT INTO Pub_ND(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {

            }
            sql = "SELECT count(*) Num FROM Pub_NY";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 2010; i < 2015; i++)
                    {

                        for (int yf = 1; yf <= 12; yf++)
                        {
                            string d = i.ToString() + "-" + yf.ToString().PadLeft(2, '0');
                            sqls += "@INSERT INTO Pub_NY(No,Name)VALUES('" + d + "','" + d + "')";
                        }
                    }
                }
            }
            catch
            {
            }

            DBAccess.RunSQLs(sqls);
            #endregion ��鴦���Ҫ�Ļ������ݡ�
            return msg;
        }
        private static void RepleaceFieldDesc(Entity en)
        {
            string tableId = DBAccess.RunSQLReturnVal("select ID from sysobjects WHERE name='" + en.EnMap.PhysicsTable + "' AND xtype='U'").ToString();

            if (tableId == null || tableId == "")
                return;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

            }
        }
        /// <summary>
        /// Ϊ������ע��
        /// </summary>
        /// <returns></returns>
        public static string AddComment()
        {
            // ȡ��ȫ����ʵ��
            ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
            string msg = "";
            Entity en = null;
            Entities ens = null;
            foreach (object obj in als)
            {
                try
                {
                    ens = (Entities)obj;
                    en = ens.GetNewEntity;
                    if (en.EnMap.EnType == EnType.View || en.EnMap.EnType == EnType.ThirdPartApp)
                        continue;
                }
                catch
                {
                    continue;
                }
                msg += AddComment(en);
            }
            return msg;
        }
        public static string AddComment(Entity en)
        {
            try
            {
                switch (en.EnMap.EnDBUrl.DBType)
                {
                    case DBType.Oracle:
                        AddCommentForTable_Ora(en);
                        break;
                    default:
                        AddCommentForTable_MS(en);
                        break;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<hr>" + en.ToString() + "���ʧ��:" + ex.Message;
            }
        }
        public static void AddCommentForTable_Ora(Entity en)
        {
            en.RunSQL("comment on table " + en.EnMap.PhysicsTable + " IS '" + en.EnDesc + "'");
            SysEnums ses = new SysEnums();
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyFieldType)
                {
                    case FieldType.PK:
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + " - ����'");
                        break;
                    case FieldType.Normal:
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + "'");
                        break;
                    case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ",ö������:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ", ����:ö������:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS " + attr.Desc + ", ���:��Ӧ�����:" + myen.EnMap.PhysicsTable + ",������:" + myen.EnDesc);
                        break;
                    case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ", �����:��Ӧ�����:" + myen1.EnMap.PhysicsTable + ",������:" + myen1.EnDesc + "'");
                        break;
                    default:
                        break;
                }
            }
        }
        private static void AddColNote(Entity en, string table, string col, string note)
        {
            try
            {
                string sql = "execute  sp_dropextendedproperty 'MS_Description','user',dbo,'table','" + table + "','column'," + col;
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {
            }

            try
            {
                string sql = "execute  sp_addextendedproperty 'MS_Description', '" + note + "', 'user', dbo, 'table', '" + table + "', 'column', '" + col + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Ϊ�����ӽ���
        /// </summary>
        /// <param name="en"></param>
        public static void AddCommentForTable_MS(Entity en)
        {
            if (en.EnMap.EnType == EnType.View || en.EnMap.EnType == EnType.ThirdPartApp)
            {
                return;
            }

            try
            {
                string sql = "execute  sp_dropextendedproperty 'MS_Description','user',dbo,'table','" + en.EnMap.PhysicsTable + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {
            }

            try
            {
                string sql = "execute  sp_addextendedproperty 'MS_Description', '" + en.EnDesc + "', 'user', dbo, 'table', '" + en.EnMap.PhysicsTable + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {

            }


            SysEnums ses = new SysEnums();
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                if (attr.Key == attr.Desc)
                    continue;

                switch (attr.MyFieldType)
                {
                    case FieldType.Normal:
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+"'");
                        break;
                    case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        //	en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"++"'" );
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ",ö������:" + ses.ToDesc());
                        break;
                    case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ",����:ö������:" + ses.ToDesc());
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+", ����:ö������:"+ses.ToDesc()+"'" );
                        break;
                    case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ", ���:��Ӧ�����:" + myen.EnMap.PhysicsTable + ",������:" + myen.EnDesc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS "+  );
                        break;
                    case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ", �����:��Ӧ�����:" + myen1.EnMap.PhysicsTable + ",������:" + myen1.EnDesc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+  );
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// ����ϵͳ��������������⣬��д����־���档
        /// </summary>
        /// <returns></returns>
        public static string DBRpt1(DBCheckLevel level, Entities ens)
        {
            Entity en = ens.GetNewEntity;
            if (en.EnMap.EnDBUrl.DBUrlType != DBUrlType.AppCenterDSN)
                return null;

            if (en.EnMap.EnType == EnType.ThirdPartApp)
                return null;

            if (en.EnMap.EnType == EnType.View)
                return null;

            if (en.EnMap.EnType == EnType.Ext)
                return null;

            // ����������ֶΡ�
            en.CheckPhysicsTable();

            PubClass.AddComment(en);

            string msg = "";
            //if (level == DBLevel.High)
            //{
            //    try
            //    {
            //        DBAccess.RunSQL("update pub_emp set AuthorizedAgent='1' WHERE AuthorizedAgent='0' ");
            //    }
            //    catch
            //    {
            //    }
            //}
            string table = en.EnMap.PhysicsTable;
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            if (fkAttrs.Count == 0)
                return msg;
            int num = 0;
            string sql;
            //string msg="";
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                string enMsg = "";
                try
                {
                    #region �������ǣ�ȥ�����ҿո���Ϊ������ܰ������ҿո�
                    if (level == DBCheckLevel.Middle || level == DBCheckLevel.High)
                    {
                        /*����Ǹ��м���,��ȥ�����ҿո�*/
                        if (attr.MyDataType == DataType.AppString)
                        {
                            DBAccess.RunSQL("UPDATE " + en.EnMap.PhysicsTable + " SET " + attr.Field + " = rtrim( ltrim(" + attr.Field + ") )");
                        }
                    }
                    #endregion

                    #region �������������.
                    Entities refEns = attr.HisFKEns; // ClassFactory.GetEns(attr.UIBindKey);
                    Entity refEn = refEns.GetNewEntity;

                    //ȡ�������ı�
                    string reftable = refEn.EnMap.PhysicsTable;
                    //sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+attr.Key+" is null or len("+attr.Key+") < 1 ";
                    // �ж�������Ƿ���ڡ�

                    sql = "SELECT COUNT(*) FROM  sysobjects  WHERE  name = '" + reftable + "'";
                    //num=DA.DBAccess.RunSQLReturnValInt(sql,0);
                    if (DBAccess.IsExitsObject(reftable) == false)
                    {
                        //���������Ϣ
                        enMsg += "<br>@���ʵ�壺" + en.EnDesc + ",�ֶ� " + attr.Key + " , �ֶ�����:" + attr.Desc + " , ��������:" + reftable + "������:" + sql;
                    }
                    else
                    {
                        Attr attrRefKey = refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue); // ȥ������������ �ո�
                        if (attrRefKey.MyDataType == DataType.AppString)
                        {
                            if (level == DBCheckLevel.Middle || level == DBCheckLevel.High)
                            {
                                /*����Ǹ��м���,��ȥ�����ҿո�*/
                                DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefKey.Field + " = rtrim( ltrim(" + attrRefKey.Field + ") )");
                            }
                        }

                        Attr attrRefText = refEn.EnMap.GetAttrByKey(attr.UIRefKeyText);  // ȥ������ Text ������ �ո�

                        if (level == DBCheckLevel.Middle || level == DBCheckLevel.High)
                        {
                            /*����Ǹ��м���,��ȥ�����ҿո�*/
                            DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefText.Field + " = rtrim( ltrim(" + attrRefText.Field + ") )");
                        }

                    }
                    #endregion

                    #region �����ʵ���Ƿ�Ϊ��
                    switch (en.EnMap.EnDBUrl.DBType)
                    {
                        case DBType.Oracle:
                            sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " is null or length(" + attr.Field + ") < 1 ";
                            break;
                        default:
                            sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " is null or len(" + attr.Field + ") < 1 ";
                            break;
                    }

                    num = DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    if (num == 0)
                    {
                    }
                    else
                    {
                        enMsg += "<br>@���ʵ�壺" + en.EnDesc + ",�����:" + en.EnMap.PhysicsTable + "����" + attr.Key + "," + attr.Desc + "����ȷ,����[" + num + "]�м�¼û�����ݡ�" + sql;
                    }
                    #endregion

                    #region �Ƿ��ܹ���Ӧ�����
                    //�Ƿ��ܹ���Ӧ�������
                    sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                    num = DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    if (num == 0)
                    {
                    }
                    else
                    {
                        /*����Ǹ��м���.*/
                        string delsql = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                        //int i =DBAccess.RunSQL(delsql);
                        enMsg += "<br>@" + en.EnDesc + ",�����:" + en.EnMap.PhysicsTable + "����" + attr.Key + "," + attr.Desc + "����ȷ,����[" + num + "]�м�¼û�й��������ݣ�����������������" + sql + "�������ɾ����Щ��Ӧ���ϵ���������������SQL: " + delsql + " ������ִ��.";
                    }
                    #endregion

                    #region �ж� ����
                    //DBAccess.IsExits("");
                    #endregion
                }
                catch (Exception ex)
                {
                    enMsg += "<br>@" + ex.Message;
                }

                if (enMsg != "")
                {
                    msg += "<BR><b>-- ���[" + en.EnDesc + "," + en.EnMap.PhysicsTable + "]������������,������:" + en.ToString() + "</b>";
                    msg += enMsg;
                }
            }
            return msg;
        }
        #endregion

        #region ת����ʽ  chen
        /// <summary>
        /// ��ĳ�ؼ��е�����ת��ΪExcel�ļ�
        /// </summary>
        /// <param name="ctl"></param>
        public static void ToExcel(System.Web.UI.Control ctl, string filename)
        {
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-excel";//"application/ms-excel";
            //image/JPEG;text/HTML;image/GIF;application/ms-msword
            ctl.Page.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// ��ĳ�ؼ��е�����ת��ΪWord�ļ�
        /// </summary>
        /// <param name="ctl"></param>
        public static void ToWord(System.Web.UI.Control ctl, string filename)
        {
            filename = HttpUtility.UrlEncode(filename);
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".doc");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-msword";//image/JPEG;text/HTML;image/GIF;application/ms-excel
            ctl.Page.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
        }

        public static void OpenExcel(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void DownloadFile(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            //HttpContext.Current.Response.ContentType = "application/ms-msword";  //image/JPEG;text/HTML;image/GIF;application/ms-excel
            //HttpContext.Current.EnableViewState =false;

            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void DownloadFileV2(string filepath, string tempName)
        {

            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                byte[] buffer = new byte[102400];
                HttpContext.Current.Response.Clear();
                using (FileStream iStream = File.OpenRead(fileInfo.FullName))
                {
                    long dataLengthToRead = iStream.Length; //��ȡ���ص��ļ��ܴ�С

                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" +
                                       HttpUtility.UrlEncode(tempName, System.Text.Encoding.UTF8));
                    while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                    {
                        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(102400));//'��ȡ�Ĵ�С

                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                        HttpContext.Current.Response.Flush();
                        dataLengthToRead = dataLengthToRead - lengthRead;
                    }
                    HttpContext.Current.Response.Close();
                    HttpContext.Current.Response.End();
                }
            }
        }
        public static void OpenWordDoc(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);

            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-msword";  //image/JPEG;text/HTML;image/GIF;application/ms-excel
            //HttpContext.Current.EnableViewState =false;
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void OpenWordDocV2(string filepath, string tempName)
        {
            //tempName = HttpUtility.UrlEncode(tempName);

            FileInfo fileInfo = new FileInfo(filepath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(tempName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AppendHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        #endregion

        #region

        #region
        public static void To(string url)
        {
            System.Web.HttpContext.Current.Response.Redirect(url, true);
        }
        public static void Print(string url)
        {
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','p','width=0,top=10,left=10,height=1,scrollbars=yes,resizable=yes,toolbar=yes,location=yes,menubar=yes') ; newWindow.focus(); </script> ");
        }
        public static BP.En.Entity CopyFromRequest(BP.En.Entity en)
        {
            return CopyFromRequest(en, BP.Sys.Glo.Request);
        }
        public static BP.En.Entity CopyFromRequest(BP.En.Entity en, HttpRequest reqest)
        {
            string allKeys = ";";
            foreach (string myK in reqest.Params.Keys)
                allKeys += myK + ";";

            // ��ÿ������ֵ.            
            Attrs attrs = en.EnMap.Attrs;
            foreach (Attr item in attrs)
            {
                string relKey = null;
                switch (item.UIContralType)
                {
                    case UIContralType.TB:
                        relKey = "TB_" + item.Key;
                        break;
                    case UIContralType.CheckBok:
                        relKey = "CB_" + item.Key;
                        break;
                    case UIContralType.DDL:
                        relKey = "DDL_" + item.Key;
                        break;
                    default:
                        break;
                }

                if (relKey == null)
                    continue;

                if (allKeys.Contains(relKey + ";"))
                {
                    /*˵���Ѿ��ҵ�������ֶ���Ϣ��*/
                    foreach (string myK in BP.Sys.Glo.Request.Params.Keys)
                    {
                        if (myK == null || myK == "")
                            continue;

                        if (myK.EndsWith(relKey))
                        {
                            if (item.UIContralType == UIContralType.CheckBok)
                            {
                                string val = BP.Sys.Glo.Request.Params[myK];
                                if (val == "on" || val == "1")
                                    en.SetValByKey(item.Key, 1);
                                else
                                    en.SetValByKey(item.Key, 0);
                            }
                            else
                            {
                                en.SetValByKey(item.Key, BP.Sys.Glo.Request.Params[myK]);
                            }
                        }
                        // if (myK.Contains(relKey+";" ))
                    }
                    continue;
                }
            }
            return en;
        }
        public static void WinClose(string returnVal)
        {
            string clientscript = "<script language='javascript'> window.returnValue = '" + returnVal + "'; window.close(); </script>";
            System.Web.HttpContext.Current.Response.Write(clientscript);
        }
        public static void WinCloseAndReParent(string returnVal)
        {
            string clientscript = "<script language='javascript'> window.opener.location.reload(); window.close(); </script>";
            System.Web.HttpContext.Current.Response.Write(clientscript);
        }
        public static void WinClose()
        {
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>  window.close(); </script> ");
        }
        public static void Open(string url)
        {
            //  System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",scrollbars=yes,resizable=yes,toolbar=false,location=false') ; newWindow.focus(); </script> ");
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','p' ) ; newWindow.focus(); </script> ");
        }
        public static void WinReload()
        {
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>window.parent.main.document.location.reload(); </script> ");
        }
        public static void WinOpen(string url)
        {
            PubClass.WinOpen(url, "", "msg" + DateTime.Now.ToString("MMddHHmmss"), 300, 300);
        }
        public static void WinOpen(string url, int w, int h)
        {
            PubClass.WinOpen(url, "", "msg" + DateTime.Now.ToString("MMddHHmmss"), w, h);
        }
        public static void WinOpen(string url, string title, string winName, int width, int height)
        {
            PubClass.WinOpen(url, title, winName, width, height, 100, 200);
        }
        public static void WinOpen(string url, string title, int width, int height)
        {
            PubClass.WinOpen(url, title, "ActivePage", width, height, 100, 200);
        }
        public static void WinOpen(string url, string title, string winName, int width, int height, int top, int left)
        {
            url = url.Replace("<", "[");
            url = url.Replace(">", "]");
            url = url.Trim();
            title = title.Replace("<", "[");
            title = title.Replace(">", "]");
            title = title.Replace("\"", "��");
            if (top == 0 && left == 0)
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",scrollbars=yes,resizable=yes,toolbar=false,location=false') ; </script> ");
            else
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",left=" + left + ",height=" + height + ",scrollbars=yes,resizable=yes,toolbar=false,location=false');</script>");
        }
        /// <summary>
        /// �����ҳ���Ϻ�ɫ�ľ��档
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        protected void ResponseWriteRedMsg(string msg)
        {
            //this.Response.Write("<BR><font color='red' size='"+MsgFontSize.ToString()+"' > <b>"+msg+"</b></font>");
            //if (msg.Length < 200)
            //	return ;
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            string url = "/WF/Comm/Port/ErrorPage.aspx";
            WinOpen(url, "����", msg + DateTime.Now.ToString("mmss"), 500, 400, 150, 270);
        }
        /// <summary>
        /// �����ҳ������ɫ����Ϣ��
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        public static void ResponseWriteBlueMsg(string msg)
        {

            if (SystemConfig.IsBSsystem)
            {
                msg = msg.Replace("@", "<BR>@");
                System.Web.HttpContext.Current.Session["info"] = msg;
                string url = "/WF/Comm/Port/InfoPage.aspx";
                WinOpen(url, "��Ϣ", "sysmsg", 500, 400, 150, 270);
            }
            else
            {
                Log.DebugWriteInfo(msg);
            }
        }
        /// <summary>
        /// ����ɹ�
        /// </summary>
        public static void ResponseWriteBlueMsg_SaveOK()
        {
            //this.Alert("����ɹ�!");

            ResponseWriteBlueMsg("����ɹ�!");
        }
        /// <summary>
        /// ResponseWriteBlueMsg_DeleteOK
        /// </summary>
        public static void ResponseWriteBlueMsg_DeleteOK()
        {
            //this.Alert("ɾ���ɹ�!");
            ResponseWriteBlueMsg("ɾ���ɹ�!");
        }
        /// <summary>
        /// ResponseWriteBlueMsg_UpdataOK
        /// </summary>
        public static void ResponseWriteBlueMsg_UpdataOK()
        {
            // this.Alert("���³ɹ�!");
            ResponseWriteBlueMsg("���³ɹ�!");
        }
        /// <summary>
        /// �����ҳ���Ϻ�ɫ����Ϣ��
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        public static void ResponseWriteBlackMsg(string msg)
        {
            System.Web.HttpContext.Current.Response.Write("<font color='Black' size=5 ><b>" + msg + "</b></font>");
        }
        public static void ResponseSript(string Sript)
        {
            System.Web.HttpContext.Current.Response.Write(Sript);
        }
        public static void ToSignInPage()
        {
            System.Web.HttpContext.Current.Response.Redirect(BP.Sys.Glo.Request.ApplicationPath + "/SignIn.aspx?url=/Wel.aspx");
        }
        public static void ToWelPage()
        {
            System.Web.HttpContext.Current.Response.Redirect(BP.Sys.Glo.Request.ApplicationPath + "/Wel.aspx");
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        public static void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            string path = BP.Sys.Glo.Request.ApplicationPath;
            if (path == "/" || path == "")
                path = "";

            System.Web.HttpContext.Current.Response.Redirect(path + "Comm/Port/InfoPage.aspx");
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        public static void ToMsgPage(string mess)
        {
            mess = mess.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect("/WF/Comm/Port/InfoPage.aspx?d=" + DateTime.Now.ToString(), false);

            //System.Web.HttpContext.Current.Session["info"]=mess;
            //System.Web.HttpContext.Current.Response.Redirect(BP.Sys.Glo.Request.ApplicationPath+"/Port/InfoPage.aspx",true);
        }
        #endregion

        /// <summary>
        ///ת��һ��ҳ���ϡ� '_top'
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="target">'_top'</param>
        public static void ToErrorPage(string mess, string target)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;

            string path = BP.Sys.Glo.Request.ApplicationPath;
            if (path == "/" || path == "")
                path = "";

            System.Web.HttpContext.Current.Response.Redirect(path + "Comm/Port/InfoPage.aspx target='_top'");
        }
        //public static void AlertSaveOK()
        //{
        //    "����ɹ�";
        //}


        /// <summary>
        /// ����page ������show message
        /// </summary>
        /// <param name="mess"></param>
        public static void Alert(string mess)
        {
            //string msg1 = "<script language=javascript>alert('" + msg + "');</script>";
            //if (! System.Web.HttpContext.Current.ClientScript.IsClientScriptBlockRegistered("a "))
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "a ", msg1);


            string script = "<script language=JavaScript>alert('" + mess + "');</script>";
            System.Web.HttpContext.Current.Response.Write(script);



            //	System.Web.HttpContext.Current.Response.aps ( script );
            //  System.Web.HttpContext.Current.Response.Write(script);
        }

        public static void ResponseWriteScript(string script)
        {
            script = "<script language=JavaScript> " + script + "</script>";
            System.Web.HttpContext.Current.Response.Write(script);
        }
        #endregion

    }
}
