using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.IO;
using BP.Sys;
namespace BP.DA
{

    /// <summary>
    /// DataType ��ժҪ˵����
    /// </summary>
    public class DataType
    {
        #region ��������صĲ���.
        /// <summary>
        /// ���ָ�����ڵ���1��һ������.
        /// </summary>
        /// <param name="dt">ָ��������</param>
        /// <returns></returns>
        public static DateTime WeekOfMonday(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Monday)
                return DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " 00:01");

            for (int i = 0; i < 7; i++)
            {
                DateTime mydt = dt.AddDays(-i);
                if (mydt.DayOfWeek == DayOfWeek.Monday)
                    return DataType.ParseSysDate2DateTime(mydt.ToString("yyyy-MM-dd") + " 00:01");
            }
            throw new Exception("@ϵͳ����.");
        }
        /// <summary>
        /// ���ָ�����ڵ���7��7������.
        /// </summary>
        /// <param name="dt">ָ��������</param>
        /// <returns></returns>
        public static DateTime WeekOfSunday(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                return DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " 00:01");

            for (int i = 0; i < 7; i++)
            {
                DateTime mydt = dt.AddDays(i);
                if (mydt.DayOfWeek == DayOfWeek.Sunday)
                    return DataType.ParseSysDate2DateTime(mydt.ToString("yyyy-MM-dd") + " 00:01");
            }
            throw new Exception("@ϵͳ����.");
        }
        /// <summary>
        /// ��������ȥ����ĩ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="days"></param>
        /// <returns>��������</returns>
        public static DateTime AddDaysBak(DateTime dt, int days)
        {
            if (BP.Sys.GloVar.Holidays == "")
            {
                // 2015����ǰ���㷨.
                dt = dt.AddDays(days);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    return dt.AddDays(2);

                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    return dt.AddDays(1);
            }

            /* ���û�����ã��ڼ���. */
            for (int i = 0; i < days; i++)
            {
                if (BP.Sys.GloVar.Holidays.Contains(dt.ToString("MM-dd")))
                {
                    i = i - 1;
                    dt = dt.AddDays(1);
                    continue;
                }
               dt= dt.AddDays(1);
            }
            return dt;
        }
        /// <summary> 
        /// ȡָ��������һ���еĵڼ��� 
        /// </summary> 
        /// <param name="dtime">����������</param> 
        /// <returns>���� һ���еĵڼ���</returns> 
        public static int WeekOfYear(DateTime dtime)
        {
            int weeknum = 0;
            DateTime tmpdate = DateTime.Parse(dtime.Year.ToString() + "-1" + "-1");
            DayOfWeek firstweek = tmpdate.DayOfWeek;
            //if(firstweek) 
            int i = dtime.DayOfYear - 1 + (int)firstweek;
            weeknum = i / 7;
            if (i > 0)
            {
                weeknum++;
            }
            return weeknum;
        }
        public static string TurnToJiDuByDataTime(string dt)
        {
            if (dt.Length <= 6)
                throw new Exception("@Ҫת�����ȵ����ڸ�ʽ����ȷ:" + dt);
            string yf = dt.Substring(5, 2);
            switch (yf)
            {
                case "01":
                case "02":
                case "03":
                    return dt.Substring(0, 4) + "-03";
                case "04":
                case "05":
                case "06":
                    return dt.Substring(0, 4) + "-06";
                case "07":
                case "08":
                case "09":
                    return dt.Substring(0, 4) + "-09";
                case "10":
                case "11":
                case "12":
                    return dt.Substring(0, 4) + "-12";
                default:
                    break;
            }
            return null;
        }
        #endregion

        #region Datatableת��ΪJson
        /// <summary>     
        /// Datatableת��ΪJson     
        /// </summary>    
        /// <param name="table">Datatable����</param>     
        /// <returns>Json�ַ���</returns>     
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    /**С�����޸�-2014/11/11----------------------------START**/
                    // BillNoFormat��Ӧvalue:{YYYY}-{MM}-{dd}-{LSH4} Formatʱ������쳣��
                    if (strKey.Equals("BillNoFormat"))
                    {
                        continue;
                    }
                    /**С�����޸�-2014/11/11----------------------------END**/
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");

                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + strValue + "\",");
                    }
                    else
                    {
                        jsonString.Append("\"" + strValue + "\"");
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        /// <summary>    
        /// DataTableת��ΪJson     
        /// </summary>    
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + String.Format(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        /// <summary>
        /// ����ͨ�õ����νṹ���������������νṹ
        /// </summary>
        /// <param name="dtTree">ͨ�ø�ʽ�����ݱ�No,Name,ParentNo��</param>
        /// <param name="dtTree">��Ŀ¼���ֵ</param>
        /// <returns></returns>
        public static DataTable PraseParentTree2TreeNo(DataTable dtTree, string parentNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Grade", typeof(string));
            dt.Columns.Add("IsDtl", typeof(string));

            dt.Columns.Add("RefNo", typeof(string));
            dt.Columns.Add("RefParentNo", typeof(string));

            dt = _PraseParentTree2TreeNo(dtTree, dt, parentNo);
            return dt;
        }
        private static DataTable _PraseParentTree2TreeNo(DataTable dtTree, DataTable newDt, string parentNo)
        {
            //��¼�Ѿ�ת��������
            System.Collections.Generic.List<DataRow> removeRows = new System.Collections.Generic.List<DataRow>();
            //DataTable newDtTree = dtTree.Copy();

            //newDtTree.DefaultView.RowFilter = " ParentNo=" + parentNo;
            //newDtTree = newDtTree.DefaultView.ToTable();

            foreach (DataRow row in dtTree.Rows)
            {
                if (row["ParentNo"].ToString().Equals(parentNo) || row["No"].ToString().Equals(parentNo))
                {
                    DataRow newRow = newDt.NewRow();

                    newRow["No"] = row["No"].ToString();
                    newRow["Name"] = row["Name"];
                    newRow["IsDtl"] = "0";


                    if (dtTree.Columns.Contains("Idx"))
                        newRow["Grade"] = row["Idx"];
                    if (dtTree.Columns.Contains("RefNo"))
                        newRow["RefNo"] = row["RefNo"];
                    else
                        newRow["RefNo"] = row["No"];    

                    newRow["RefParentNo"] = row["ParentNo"];

                    newDt.Rows.Add(newRow);
                    removeRows.Add(row);
                }
                
            }

            #region ��ԭ�ṹ����ת�����µ�datable ��
            //foreach (DataRow row in dtTree.Rows)
            //{
            //    if (newDt.Rows.Count == 0)
            //    {
            //        if (!row["IsRoot"].Equals("1"))
            //            continue;

            //        DataRow newRow = newDt.NewRow();

            //        newRow["No"] = row["No"];
            //        newRow["Name"] = row["Name"];
            //        newRow["Grade"] = row["Idx"];
            //        newRow["IsDtl"] = "";

            //        newRow["RefNo"] = row["RefNo"];
            //        newRow["RefParentNo"] = row["ParentNo"];

            //        newDt.Rows.Add(newRow);
            //        removeRows.Add(row);
            //    }
            //    else
            //    {
            //        foreach (DataRow newDtRow in newDt.Rows)
            //        {
            //            if (row["ParentNo"].Equals(newDtRow["No"]))
            //            {
            //                DataRow newRow = newDt.NewRow();

            //                newRow["No"] = row["No"];
            //                newRow["Name"] = row["Name"];
            //                newRow["Grade"] = row["Idx"];
            //                newRow["IsDtl"] = "";

            //                newRow["RefNo"] = row["RefNo"];
            //                newRow["RefParentNo"] = row["ParentNo"];

            //                newDt.Rows.Add(newRow);
            //                removeRows.Add(row);
            //            }
            //        }
            //    }
            //}
            #endregion ��ԭ�ṹ����ת�����µ�datable ��

            //�Ƴ��Ѿ�ת��������
            foreach (DataRow row in removeRows)
                dtTree.Rows.Remove(row);
            //���ԭ�ṹ�л������ݾͽ���ת��
            if (dtTree.Rows.Count != 0)
                _PraseParentTree2TreeNo(dtTree, newDt, dtTree.Rows[0]["No"].ToString());
            return newDt;
        }
        public static string PraseGB2312_To_utf8(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            //�����ַ���   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //����ת������ַ�   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// ת����MB
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float PraseToMB(long val)
        {
            try
            {
                return float.Parse(String.Format("{0:##.##}", val / 1048576));
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="isNumber"></param>
        /// <returns></returns>
        public static string PraseAtToInSql(string strs, bool isNumber)
        {
            strs = strs.Replace("@", "','");
            strs = strs + "'";
            strs = strs.Substring(2);
            if (isNumber)
                strs = strs.Replace("'", "");
            return strs;
        }
        /// <summary>
        /// ����������Ķ�������ɳ����ӡ�
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string DealSuperLink(string doc)
        {
            if (doc == null)
                return null;

            return doc;

            Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            doc = urlregex.Replace(doc, "<a href='' target='_blank'></a>");
            Regex emailregex = new Regex(@"([a-zA-Z_0-9.-]+@[a-zA-Z_0-9.-]+\.\w+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            doc = emailregex.Replace(doc, "<a href=mailto:></a>");
            return doc;
        }

        /// <summary>
        /// д�ļ�
        /// </summary>
        /// <param name="file">·��</param>
        /// <param name="Doc">����</param>
        public static void WriteFile(string file, string Doc)
        {
            System.IO.StreamWriter sr;
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);


            try
            {
                //sr = new System.IO.StreamWriter(file, false, System.Text.Encoding.GetEncoding("GB2312"));
                sr = new System.IO.StreamWriter(file, false, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("@�ļ���" + file + ",����:" + ex.Message);
            }

            sr.Write(Doc);
            sr.Close();
        }
        /// <summary>
        /// ��ȡURL����
        /// </summary>
        /// <param name="url">Ҫ��ȡ��url</param>
        /// <param name="timeOut">��ʱʱ��</param>
        /// <param name="encode">text code.</param>
        /// <returns>���ض�ȡ����</returns>
        public static string ReadURLContext(string url, int timeOut, Encoding encode)
        {
            HttpWebRequest webRequest = null;
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "get";
                webRequest.Timeout = timeOut;
                string str = webRequest.Address.AbsoluteUri;
                str = str.Substring(0, str.LastIndexOf("/"));
            }
            catch (Exception ex)
            {
                try
                {
                    BP.DA.Log.DefaultLogWriteLineWarning("@��ȡURL���ִ���:URL=" + url + "@������Ϣ��" + ex.Message);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }
            //	��Ϊ�����ص�ʵ��������WebRequest������HttpWebRequest,��˼ǵ�Ҫ����ǿ������ת��
            //  ����������һ��HttpWebResponse�Ա���շ��������͵���Ϣ�����ǵ���HttpWebRequest.GetResponse����ȡ�ģ�
            HttpWebResponse webResponse;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                try
                {
                    // ������������ӡ�
                    BP.DA.Log.DefaultLogWriteLineWarning("@��ȡurl=" + url + "ʧ�ܡ��쳣��Ϣ:" + ex.Message, true);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }

            //���webResponse.StatusCode��ֵΪHttpStatusCode.OK����ʾ�ɹ�������Ϳ��Խ��Ŷ�ȡ���յ��������ˣ�
            // ��ȡ���յ�����
            Stream stream = webResponse.GetResponseStream();
            System.IO.StreamReader streamReader = new StreamReader(stream, encode);
            string content = streamReader.ReadToEnd();
            webResponse.Close();
            return content;
        }
        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        /// <param name="file">·��</param>
        /// <returns>����</returns>
        public static string ReadTextFile(string file)
        {
            System.IO.StreamReader read = new System.IO.StreamReader(file, System.Text.Encoding.UTF8); // �ļ���.
            string doc = read.ReadToEnd();  //��ȡ��ϡ�
            read.Close(); // �رա�
            return doc;
        }
        public static bool SaveAsFile(string filePath, string doc)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false);
            sw.Write(doc);
            sw.Close();
            return true;
        }
        public static string ReadTextFile2Html(string file)
        {
            return DataType.ParseText2Html(ReadTextFile(file));
        }
        /// <summary>
        /// �ж��Ƿ�ȫ���Ǻ���
        /// </summary>
        /// <param name="htmlstr"></param>
        /// <returns></returns>
        public static bool CheckIsChinese(string htmlstr)
        {
            char[] chs = htmlstr.ToCharArray();
            foreach (char c in chs)
            {
                int i = c.ToString().Length;
                if (i == 1)
                    return false;
            }
            return true;
        }

        #region Ԫ�Ƿ�
        public static string TurnToFiels(float money)
        {
            string je = money.ToString("0.00");

            string strs = "";

            switch (je.Length)
            {
                case 7: //         ǧ                                ��                                  ʮ                              Ԫ                                ��                              ��;
                    strs = "D" + je.Substring(0, 1) + ".TW,THOU.TW,D" + je.Substring(1, 1) + ".TW,HUN.TW,D" + je.Substring(2, 1) + ".TW,TEN.TW,D" + je.Substring(3, 1) + ".TW,YUAN.TW,D" + je.Substring(5, 1) + ".TW,JIAO.TW,D" + je.Substring(6, 1) + ".TW,FEN.TW";
                    break;
                case 6: // ��;
                    strs = "D" + je.Substring(0, 1) + ".TW,HUN.TW,D" + je.Substring(1, 1) + ".TW,TEN.TW,D" + je.Substring(2, 1) + ".TW,YUAN.TW,D" + je.Substring(4, 1) + ".TW,JIAO.TW,D" + je.Substring(5, 1) + ".TW,FEN.TW";
                    break;
                case 5: // ʮ;
                    strs = "D" + je.Substring(0, 1) + ".TW,TEN.TW,D" + je.Substring(1, 1) + ".TW,YUAN.TW,D" + je.Substring(3, 1) + ".TW,JIAO.TW,D" + je.Substring(4, 1) + ".TW,FEN.TW";
                    break;
                case 4: // Ԫ;
                    if (money > 1)
                        strs = "D" + je.Substring(0, 1) + ".TW,YUAN.TW,D" + je.Substring(2, 1) + ".TW,JIAO.TW,D" + je.Substring(3, 1) + ".TW,FEN.TW";
                    else
                        strs = "D" + je.Substring(2, 1) + ".TW,JIAO.TW,D" + je.Substring(3, 1) + ".TW,FEN.TW";
                    break;
                default:
                    throw new Exception("û���漰����ô��Ľ���");
            }

            //			strs=strs.Replace(",D0.TW,JIAO.TW,D0.TW,FEN.TW",""); // �滻�� .0��0��;
            //			strs=strs.Replace("D0.TW,HUN.TW,D0.TW,TEN.TW","D0.TW"); // �滻�� .0��0ʮ Ϊ 0 ;
            //			strs=strs.Replace("D0.TW,THOU.TW","D0.TW");  // �滻����ǧ��
            //			strs=strs.Replace("D0.TW,HUN.TW","D0.TW");
            //			strs=strs.Replace("D0.TW,TEN.TW","D0.TW");
            //			strs=strs.Replace("D0.TW,JIAO.TW","D0.TW");
            //			strs=strs.Replace("D0.TW,FEN.TW","D0.TW");
            return strs;
        }
        #endregion

        public static string Html2Text(string htmlstr)
        {
            htmlstr = htmlstr.Replace("<BR>", "\n");
            return htmlstr.Replace("&nbsp;", " ");
            //	return htmlstr;
        }
        public static string ByteToString(byte[] bye)
        {
            string s = "";
            foreach (byte b in bye)
            {
                s += b.ToString();
            }
            return s;
        }
        public static byte[] StringToByte(string s)
        {
            byte[] bs = new byte[s.Length];
            char[] cs = s.ToCharArray();
            int i = 0;
            foreach (char c in cs)
            {
                bs[i] = Convert.ToByte(c);
                i++;
            }
            return bs;
        }

        /// <summary>
        /// ȡ���ٷֱ�
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetPercent(decimal a, decimal b)
        {
            decimal p = a / b;
            return p.ToString("0.00%");
        }
        public static string GetWeek(int weekidx)
        {
            switch (weekidx)
            {
                case 0:
                    return "������";
                case 1:
                    return "����һ";
                case 2:
                    return "���ڶ�";
                case 3:
                    return "������";
                case 4:
                    return "������";
                case 5:
                    return "������";
                case 6:
                    return "������";
                default:
                    throw new Exception("error weekidx=" + weekidx);
            }
        }

        public static string GetABC(string abc)
        {
            switch (abc)
            {
                case "A":
                    return "B";
                case "B":
                    return "C";
                case "C":
                    return "D";
                case "D":
                    return "E";
                case "E":
                    return "F";
                case "F":
                    return "G";
                case "G":
                    return "H";
                case "H":
                    return "I";
                case "I":
                    return "J";
                case "J":
                    return "K";
                case "K":
                    return "L";
                case "L":
                    return "M";
                case "M":
                    return "N";
                case "N":
                    return "O";
                case "Z":
                    return "A";
                default:
                    throw new Exception("abc error" + abc);
            }
        }
        public static string GetBig5(string text)
        {
            System.Text.Encoding e2312 = System.Text.Encoding.GetEncoding("GB2312");
            byte[] bs = e2312.GetBytes(text);
            System.Text.Encoding e5 = System.Text.Encoding.GetEncoding("Big5");
            byte[] bs5 = System.Text.Encoding.Convert(e2312, e5, bs);
            return e5.GetString(bs5);
        }
        /// <summary>
        /// ���� data1 - data2 ������.
        /// </summary>
        /// <param name="data1">fromday</param>
        /// <param name="data2">today</param>
        /// <returns>���������</returns>
        public static int SpanDays(string fromday, string today)
        {
            try
            {
                TimeSpan span = DateTime.Parse(today.Substring(0, 10)) - DateTime.Parse(fromday.Substring(0, 10));
                return span.Days;
            }
            catch
            {
                //throw new Exception(ex.Message +"" +fromday +"  " +today ) ; 
                return 0;
            }
        }
        /// <summary>
        /// ���� QuarterFrom - QuarterTo �ļ���.
        /// </summary>
        /// <param name="QuarterFrom">QuarterFrom</param>
        /// <param name="QuarterTo">QuarterTo</param>
        /// <returns>����ļ���</returns>
        public static int SpanQuarter(string _APFrom, string _APTo)
        {
            DateTime fromdate = Convert.ToDateTime(_APFrom + "-01");
            DateTime todate = Convert.ToDateTime(_APTo + "-01");
            int i = 0;
            if (fromdate > todate)
                throw new Exception("ѡ�������ʼʱ��" + _APFrom + "���ܴ�����ֹʱ��" + _APTo + "!");

            while (fromdate <= todate)
            {
                i++;
                fromdate = fromdate.AddMonths(1);
            }

            int j = (i + 2) / 3;
            return j;
        }
        /// <summary>
        /// �����ڵ�������
        /// </summary>
        /// <param name="data1"></param>
        /// <returns></returns>
        public static int SpanDays(string data1)
        {
            TimeSpan span = DateTime.Now - DateTime.Parse(data1.Substring(0, 10));
            return span.Days;
        }
        /// <summary>
        /// ����Ƿ���һ���ֶλ��߱�����
        /// </summary>
        /// <param name="str">Ҫ�����ֶλ��߱�����</param>
        /// <returns>�Ƿ�Ϸ�</returns>
        public static bool CheckIsFieldOrTableName(string str)
        {
            string s = str.Substring(0, 1);
            if (DataType.IsNumStr(s))
                return false;

            string chars = "~!@#$%^&*()_+`{}|:'<>?[];',./";
            if (chars.Contains(s) == true)
                return false;
            return true;
        }
        public static string ParseText2Html(string val)
        {
            //val = val.Replace("&", "&amp;");
            //val = val.Replace("<","&lt;");
            //val = val.Replace(">","&gt;");

            //val = val.Replace(char(34), "&quot;");
            //val = val.Replace(char(9), "&nbsp;&nbsp;&nbsp;");
            //val = val.Replace(" ", "&nbsp;");

            return val.Replace("\n", "<BR>").Replace("~", "'");

            //return val.Replace("\n", "<BR>&nbsp;&nbsp;").Replace("~", "'");

        }
        public static string ParseHtmlToText(string val)
        {
            if (val == null)
                return val;

            val = val.Replace("&nbsp;", " ");
            val = val.Replace("  ", " ");

            val = val.Replace("</td>", "");
            val = val.Replace("</TD>", "");

            val = val.Replace("</tr>", "");
            val = val.Replace("</TR>", "");

            val = val.Replace("<tr>", "");
            val = val.Replace("<TR>", "");

            val = val.Replace("</font>", "");
            val = val.Replace("</FONT>", "");

            val = val.Replace("</table>", "");
            val = val.Replace("</TABLE>", "");


            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("&nbsp;", " ");

            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR>", "<BR>");

            char[] chs = val.ToCharArray();

            bool isStartRec = false;
            string recStr = "";
            foreach (char c in chs)
            {
                if (c == '<')
                {
                    recStr = "";
                    isStartRec = true; /* ��ʼ��¼ */
                }

                if (isStartRec)
                {
                    recStr += c.ToString();
                }

                if (c == '>')
                {
                    isStartRec = false;

                    if (recStr == "")
                    {
                        isStartRec = false;
                        continue;
                    }

                    /* ��ʼ�����������ڵĶ�����*/
                    string market = recStr.ToLower();
                    if (market.Contains("<img"))
                    {
                        /* ����һ��ͼƬ��� */
                        isStartRec = false;
                        recStr = "";
                        continue;
                    }
                    else
                    {
                        val = val.Replace(recStr, "");
                        isStartRec = false;
                        recStr = "";
                    }
                }
            }


            val = val.Replace("���壺����С", "");
            val = val.Replace("����:����С", "");

            val = val.Replace("  ", " ");
            val = val.Replace("\t", "");
            val = val.Replace("\n", "");
            val = val.Replace("\r", "");
            return val;
        }
        /// <summary>
        /// ������ת����ƴ��
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string ParseStringToPinyin(string exp)
        {
            exp = exp.Trim();
            string pinYin = "", str = null;
            char[] chars = exp.ToCharArray();
            foreach (char c in chars)
            {
                try
                {
                    str = BP.Tools.chs2py.convert(c.ToString());
                    pinYin += str.Substring(0, 1).ToUpper() + str.Substring(1);
                }
                catch
                {
                    pinYin += c;
                }
            }
            return pinYin;
        }
        /// <summary>
        /// ת����ƴ����һ����ĸ����
        /// </summary>
        /// <param name="str">Ҫת�������Ĵ�</param>
        /// <returns>ƴ��</returns>
        public static string ParseStringToPinyinWordFirst(string str)
        {
            try
            {
                String _Temp = null;
                for (int i = 0; i < str.Length; i++)
                {
                    _Temp = _Temp + BP.DA.DataType.ParseStringToPinyin(str.Substring(i, 1));
                }
                return _Temp;
            }
            catch (Exception ex)
            {
                throw new Exception("@����" + str + "������ת����ƴ����");
            }
        }
        /// <summary>
        /// ת����ƴ����һ����ĸ����
        /// </summary>
        /// <param name="str">Ҫת�������Ĵ�</param>
        /// <returns>ƴ��</returns>
        public static string ParseStringToPinyinJianXie(string str)
        {
            try
            {
                String _Temp = null;
                var re = string.Empty;
                for (int i = 0; i < str.Length; i++)
                {
                    re = BP.DA.DataType.ParseStringToPinyin(str.Substring(i, 1));
                    _Temp += re.Length == 0 ? "" : re.Substring(0, 1);
                }
                return _Temp;
            }
            catch (Exception ex)
            {
                throw new Exception("@����" + str + "������ת����ƴ����");
            }
        }
        /// <summary>
        /// ת���� decimal
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static decimal ParseExpToDecimal(string exp)
        {
            if (exp.Trim() == "")
                throw new Exception("DataType.ParseExpToDecimalҪת���ı��ʽΪ�ա�");


            exp = exp.Replace("+-", "-");
            exp = exp.Replace("��", "");
            //exp=exp.Replace(" ",""); �����滻����Ϊ��sql��﹫ʽʱ�䣬����ִ���
            exp = exp.Replace("\n", "");
            exp = exp.Replace("\t", "");

            exp = exp.Replace("��", "+");
            exp = exp.Replace("��", "-");
            exp = exp.Replace("��", "*");
            exp = exp.Replace("��", "/");
            exp = exp.Replace("��", ")");
            exp = exp.Replace("��", "(");

            exp = exp.Replace(".00.00", "00");

            exp = exp.Replace("--", "- -");


            if (exp.IndexOf("@") != -1)
                return 0;

            string val = exp.Substring(0, 1);
            if (val == "-")
                exp = exp.Substring(1);

            //  exp = exp.Replace("*100%", "*100");

            exp = exp.Replace("*100%", "*1");

            try
            {
                return decimal.Parse(exp);
            }
            catch
            {
            }

            try
            {
                string sql = "SELECT  " + exp + " as Num  ";
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                    case DBType.Access:
                        sql = "SELECT  " + exp + " as Num  ";
                        return DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                    case DBType.Oracle:
                        sql = "SELECT  " + exp + " NUM from DUAL ";
                        return DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                    case DBType.Informix:
                        sql = "SELECT  " + exp + " NUM from  taa_onerow ";
                        return DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineInfo(ex.Message);
                /* ����׳��쳣�Ͱ� 0  ���㡣 */
                return 0;
            }

            exp = exp.Replace("-0", "");


            try
            {
                BP.Tools.StringExpressionCalculate sc = new BP.Tools.StringExpressionCalculate();
                return sc.TurnToDecimal(exp);
            }
            catch (Exception ex)
            {
                if (exp.IndexOf("/") != -1)
                    return 0;
                throw new Exception("���ʽ(\"" + exp + "\")�������" + ex.Message);
            }
        }
        public static string ParseFloatToCash(float money)
        {
            if (money == 0)
                return "��Բ������";
            BP.Tools.DealString d = new BP.Tools.DealString();
            d.InputString = money.ToString();
            d.ConvertToChineseNum();
            return d.OutString;
        }
        public static string ParseFloatToRMB(float money)
        {
            if (money == 0)
                return "��Բ������";
            BP.Tools.DealString d = new BP.Tools.DealString();
            d.InputString = money.ToString();
            d.ConvertToChineseNum();
            return d.OutString;
        }
        /// <summary>
        /// �õ�һ������,����ϵͳ
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        public DateTime Parse(string dataStr)
        {
            return DateTime.Parse(dataStr);
        }
        /// <summary>
        /// ϵͳ�����ʱ���ʽ yyyy-MM-dd .
        /// </summary>
        public static string SysDataFormat
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }
        /// <summary>
        /// ��ʽ����������
        /// </summary>
        /// <param name="dataStr">�����ַ���</param>
        /// <returns>��׼����������</returns>
        public static string FormatDataTime(string dataStr)
        {
           
            return dataStr;
        }
        /// <summary>
        /// ��ǰ������
        /// </summary>
        public static string CurrentData
        {
            get
            {
                return DateTime.Now.ToString(DataType.SysDataFormat);
            }
        }
        public static string CurrentTime
        {
            get
            {
                return DateTime.Now.ToString("hh:mm");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string CurrentTimeQuarter
        {
            get
            {
                return DateTime.Now.ToString("hh:mm");
            }
        }
        /// <summary>
        /// ��һ��ʱ�䣬����һ������ʱ�䡣
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ParseTime2TimeQuarter(string time)
        {
            string hh = time.Substring(0, 3);
            int mm = int.Parse(time.Substring(3, 2));
            if (mm == 0)
            {
                return hh + "00";
            }

            if (mm < 15)
            {
                return hh + "00";
            }
            if (mm >= 15 && mm < 30)
            {
                return hh + "15";
            }

            if (mm >= 30 && mm < 45)
            {
                return hh + "30";
            }

            if (mm >= 45 && mm < 60)
            {
                return hh + "45";
            }
            return time;
        }
        public static string CurrentDay
        {
            get
            {
                return DateTime.Now.ToString("dd");
            }
        }

        /// <summary>
        /// ��ǰ�Ļ���ڼ�
        /// </summary>
        public static string CurrentAP
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM");
            }
        }
        /// <summary>
        /// ��ǰ�Ļ���ڼ�
        /// </summary>
        public static string CurrentYear
        {
            get
            {
                return DateTime.Now.ToString("yyyy");
            }
        }
        public static string CurrentMonth
        {
            get
            {
                return DateTime.Now.ToString("MM");
            }
        }
        /// <summary>
        /// ��ǰ�Ļ���ڼ� yyyy-MM
        /// </summary>
        public static string CurrentYearMonth
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM");
            }
        }
        public static string GetJDByMM(string mm)
        {
            string jd = "01";
            switch (mm)
            {
                case "01":
                case "02":
                case "03":
                    jd = "01";
                    break;
                case "04":
                case "05":
                case "06":
                    jd = "04";
                    break;
                case "07":
                case "08":
                case "09":
                    jd = "07";
                    break;
                case "10":
                case "11":
                case "12":
                    jd = "10";
                    break;
                default:
                    throw new Exception("@������Ч���·ݸ�ʽ" + mm);
            }
            return jd;
        }
        /// <summary>
        /// ��ǰ�ļ����ڼ�yyyy-MM
        /// </summary>
        public static string CurrentAPOfJD
        {
            get
            {
                return DateTime.Now.ToString("yyyy") + "-" + DataType.GetJDByMM(DateTime.Now.ToString("MM"));
            }
        }
        /// <summary>
        /// ��ǰ�ļ��ȵ�ǰһ������.
        /// </summary>
        public static string CurrentAPOfJDOfFrontFamily
        {
            get
            {
                DateTime now = DateTime.Now.AddMonths(-3);
                return now.ToString("yyyy") + "-" + DataType.GetJDByMM(now.ToString("MM"));
            }
        }
        /// <summary>
        /// yyyy-JD
        /// </summary>
        public static string CurrentAPOfPrevious
        {
            get
            {
                int m = int.Parse(DateTime.Now.ToString("MM"));
                return DateTime.Now.ToString("yyyy-MM");
            }
        }
        /// <summary>
        /// ȡ����ǰ�·ݵ���һ���·�
        /// </summary>
        public static string CurrentNYOfPrevious
        {
            get
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMonths(-1);
                return dt.ToString("yyyy-MM");
            }
        }
        /// <summary>
        /// ��ǰ�ļ����ڼ�
        /// </summary>
        public static string CurrentAPOfYear
        {
            get
            {
                return DateTime.Now.ToString("yyyy");
            }
        }
        /// <summary>
        /// ��ǰ������ʱ��
        /// </summary>
        public static string CurrentDataTime
        {
            get
            {
                return DateTime.Now.ToString(DataType.SysDataTimeFormat);
            }
        }
        public static string CurrentDataTimeOfDef
        {
            get
            {
                switch (BP.Web.WebUser.SysLang)
                {
                    case "CH":
                    case "B5":
                        return CurrentDataTimeCNOfShort;
                    case "EN":
                        return DateTime.Now.ToString("MM/DD/YYYY");
                    default:
                        break;
                }
                return CurrentDataTimeCNOfShort;
            }
        }
        public static string CurrentDataTimeCNOfShort
        {
            get
            {
                return DateTime.Now.ToString("yy��MM��dd�� HHʱmm��");
            }
        }
        public static string CurrentDataCNOfShort
        {
            get
            {
                return DateTime.Now.ToString("yy��MM��dd��");
            }
        }
        public static string CurrentDataCNOfLong
        {
            get
            {
                return DateTime.Now.ToString("yyyy��MM��dd��");
            }
        }
        /// <summary>
        /// ��ǰ������ʱ��
        /// </summary>
        public static string CurrentDataTimeCN
        {
            get
            {
                return DateTime.Now.ToString(DataType.SysDataFormatCN) + "��" + GetWeekName(DateTime.Now.DayOfWeek);
            }
        }
        private static string GetWeekName(System.DayOfWeek dw)
        {
            switch (dw)
            {
                case DayOfWeek.Monday:
                    return "����һ";
                case DayOfWeek.Thursday:
                    return "������";
                case DayOfWeek.Friday:
                    return "������";
                case DayOfWeek.Saturday:
                    return "������";
                case DayOfWeek.Sunday:
                    return "������";
                case DayOfWeek.Tuesday:
                    return "���ڶ�";
                case DayOfWeek.Wednesday:
                    return "������";
                default:
                    return "";
            }
        }

        /// <summary>
        /// ��ǰ������ʱ��
        /// </summary>
        public static string CurrentDataTimess
        {
            get
            {
                return DateTime.Now.ToString(DataType.SysDataTimeFormat + ":ss");
            }
        }
        public static string ParseSysDateTime2SysDate(string sysDateformat)
        {
            try
            {
                return sysDateformat.Substring(0, 10);
            }
            catch (Exception ex)
            {
                throw new Exception("���ڸ�ʽ����:" + sysDateformat + " errorMsg=" + ex.Message);
            }
        }
        /// <summary>
        /// ��chichengsoft��ϵͳ���ڸ�ʽת��Ϊϵͳ���ڸ�ʽ��
        /// </summary>
        /// <param name="sysDateformat">yyyy-MM-dd</param>
        /// <returns>DateTime</returns>
        public static DateTime ParseSysDate2DateTime(string sysDateformat)
        {
            if (sysDateformat == null || sysDateformat.Trim().Length == 0)
                return DateTime.Now;


            try
            {
                if (sysDateformat.Length > 10)
                    return ParseSysDateTime2DateTime(sysDateformat);

                sysDateformat = sysDateformat.Trim();
                //DateTime.Parse(sysDateformat,
                string[] strs = null;
                if (sysDateformat.IndexOf("-") != -1)
                {
                    strs = sysDateformat.Split('-');
                }

                if (sysDateformat.IndexOf("/") != -1)
                {
                    strs = sysDateformat.Split('/');
                }

                int year = int.Parse(strs[0]);
                int month = int.Parse(strs[1]);
                int day = int.Parse(strs[2]);

                //DateTime dt= DateTime.Now;
                return new DateTime(year, month, day, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("����[" + sysDateformat + "]ת�����ִ���:" + ex.Message + "��Ч�������Ǹ�ʽ��");
            }
            //return dt;			 
        }
        /// <summary>
        /// 2005-11-04 09:12
        /// </summary>
        /// <param name="sysDateformat"></param>
        /// <returns></returns>
        public static DateTime ParseSysDateTime2DateTime(string sysDateformat)
        {
            try
            {
                //if (sysDateformat.Length==10)
                //    sysDateformat=sysDateformat+" 00:01";

                //int year = int.Parse(sysDateformat.Substring(0,4)) ;
                //int month = int.Parse(sysDateformat.Substring(5,2)) ;
                //int day = int.Parse(sysDateformat.Substring(8,2)) ;

                //int hh=int.Parse(sysDateformat.Substring(11,2)) ;
                //int mm=int.Parse(sysDateformat.Substring(14,2)) ;
                //DateTime dt = new DateTime(year,month,day,hh,mm,1) ;
                //return dt;
                return Convert.ToDateTime(sysDateformat);
            }
            catch (Exception ex)
            {
                throw new Exception("@ʱ���ʽ����ȷ:" + sysDateformat + "@������Ϣ:" + ex.Message);
            }
        }

        /// <summary>
        /// ��ȡ����ʱ��֮����ַ�����ʾ��ʽ���磺1��2ʱ34��
        /// <para>added by liuxc,2014-12-4</para>
        /// </summary>
        /// <param name="t1">��ʼʱ��</param>
        /// <param name="t2">����ʱ��</param>
        /// <returns>���أ�x��xʱx��</returns>
        public static string GetSpanTime(DateTime t1, DateTime t2)
        {
            var span = t2 - t1;
            var days = span.Days;
            var hours = span.Hours;
            var minutes = span.Minutes;

            if (days == 0 && hours == 0 && minutes == 0)
                minutes = span.Seconds > 0 ? 1 : 0;

            var spanStr = string.Empty;

            if (days > 0)
                spanStr += days + "��";

            if (hours > 0)
                spanStr += hours + "ʱ";

            if (minutes > 0)
                spanStr += minutes + "��";

            if (spanStr.Length == 0)
                spanStr = "0��";

            return spanStr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoffrom"></param>
        /// <param name="dtofto"></param>
        /// <returns></returns>
        public static float GetSpanDays(string dtoffrom, string dtofto)
        {
            DateTime dtfrom = DataType.ParseSysDate2DateTime(dtoffrom);
            DateTime dtto = DataType.ParseSysDate2DateTime(dtofto);

            TimeSpan ts = dtfrom - dtto;
            return (float)ts.TotalDays;
        }
        public static float GetSpanMinute(string fromdatetim, string toDateTime)
        {
            DateTime dtfrom = DataType.ParseSysDateTime2DateTime(fromdatetim);
            DateTime dtto = DataType.ParseSysDateTime2DateTime(toDateTime);

            TimeSpan ts = dtfrom - dtto;
            return (float)ts.TotalMinutes;
        }
        /// <summary>
        /// �����ڵ�ʱ��
        /// </summary>
        /// <param name="fromdatetim"></param>
        /// <returns>������</returns>
        public static int GetSpanMinute(string fromdatetim)
        {
            DateTime dtfrom = DataType.ParseSysDateTime2DateTime(fromdatetim);
            DateTime dtto = DateTime.Now;
            TimeSpan ts = dtfrom - dtto;
            return (int)ts.TotalMinutes + ts.Hours * 60;
        }
        /// <summary>
        /// ϵͳ��������ʱ���ʽ yyyy-MM-dd hh:mm
        /// </summary>
        public static string SysDataTimeFormat
        {
            get
            {
                return "yyyy-MM-dd HH:mm";
            }
        }
        public static string SysDataFormatCN
        {
            get
            {
                return "yyyy��MM��dd��";
            }
        }
        public static string SysDatatimeFormatCN
        {
            get
            {
                return "yyyy��MM��dd�� HHʱmm��";
            }
        }
        public static DBUrlType GetDBUrlByString(string strDBUrl)
        {
            switch (strDBUrl)
            {
                case "AppCenterDSN":
                    return DBUrlType.AppCenterDSN;
                case "DBAccessOfOracle":
                    return DBUrlType.DBAccessOfOracle;
                case "DBAccessOfMSMSSQL":
                    return DBUrlType.DBAccessOfMSMSSQL;
                case "DBAccessOfOLE":
                    return DBUrlType.DBAccessOfOLE;
                case "DBAccessOfODBC":
                    return DBUrlType.DBAccessOfODBC;
                default:
                    throw new Exception("@û�д�����[" + strDBUrl + "]");
            }
        }
        public static int GetDataTypeByString(string datatype)
        {
            switch (datatype)
            {
                case "AppBoolean":
                    return DataType.AppBoolean;
                case "AppDate":
                    return DataType.AppDate;
                case "AppDateTime":
                    return DataType.AppDateTime;
                case "AppDouble":
                    return DataType.AppDouble;
                case "AppFloat":
                    return DataType.AppFloat;
                case "AppInt":
                    return DataType.AppInt;
                case "AppMoney":
                    return DataType.AppMoney;
                case "AppString":
                    return DataType.AppString;
                default:
                    throw new Exception("@û�д�����" + datatype);
            }
        }
        public static string GetDataTypeDese(int datatype)
        {
            if (Web.WebUser.SysLang == "CH")
            {
                switch (datatype)
                {
                    case DataType.AppBoolean:
                        return "����(Int)";
                    case DataType.AppDate:
                        return "����nvarchar";
                    case DataType.AppDateTime:
                        return "����ʱ��nvarchar";
                    case DataType.AppDouble:
                        return "˫����(double)";
                    case DataType.AppFloat:
                        return "����(float)";
                    case DataType.AppInt:
                        return "����(int)";
                    case DataType.AppMoney:
                        return "����(float)";
                    case DataType.AppString:
                        return "�ַ�(nvarchar)";
                    default:
                        throw new Exception("@û�д�����");
                }
            }

            switch (datatype)
            {
                case DataType.AppBoolean:
                    return "Boolen";
                case DataType.AppDate:
                    return "Date";
                case DataType.AppDateTime:
                    return "Datetime";
                case DataType.AppDouble:
                    return "Double";
                case DataType.AppFloat:
                    return "Float";
                case DataType.AppInt:
                    return "Int";
                case DataType.AppMoney:
                    return "Money";
                case DataType.AppString:
                    return "Nvarchar";
                default:
                    throw new Exception("@û�д�����");
            }
        }
        /// <summary>
        /// ������Ӧ��ͼƬ��С
        /// ��;:�ڹ̶�������С��λ�ã���ʾ�̶���ͼƬ��
        /// </summary>
        /// <param name="height">�����߶�</param>
        /// <param name="width">�������</param>
        /// <param name="AdaptHeight">ԭʼͼƬ�߶�</param>
        /// <param name="AdaptWidth">ԭʼͼƬ���</param>
        /// <param name="isFull">�Ƿ����:��,СͼƬ����Ŵ��������. ��,СͼƬ���Ŵ���ԭ���Ĵ�С</param>
        public static void GenerPictSize(float panelHeight, float panelWidth, ref float AdaptHeight, ref float AdaptWidth, bool isFullPanel)
        {
            if (isFullPanel == false)
            {
                if (panelHeight <= AdaptHeight && panelWidth <= AdaptWidth)
                    return;
            }

            float zoom = 1;
            zoom = System.Math.Min(panelHeight / AdaptHeight, panelWidth / AdaptWidth);
            AdaptHeight = AdaptHeight * zoom;
            AdaptWidth = AdaptWidth * zoom;
        }


        #region �������͡�
        /// <summary>
        /// string
        /// </summary>
        public const int AppString = 1;
        /// <summary>
        /// int
        /// </summary>
        public const int AppInt = 2;
        /// <summary>
        /// float
        /// </summary>
        public const int AppFloat = 3;
        /// <summary>
        /// AppBoolean
        /// </summary>
        public const int AppBoolean = 4;
        /// <summary>
        /// AppDouble
        /// </summary>
        public const int AppDouble = 5;
        /// <summary>
        /// AppDate
        /// </summary>
        public const int AppDate = 6;
        /// <summary>
        /// AppDateTime
        /// </summary>
        public const int AppDateTime = 7;
        /// <summary>
        /// AppMoney
        /// </summary>
        public const int AppMoney = 8;
        /// <summary>
        /// �ʰٷֱȡ�
        /// </summary>
        public const int AppRate = 9;
        #endregion

        public static string StringToDateStr(string str)
        {
            try
            {
                DateTime dt = DateTime.Parse(str);
                string year = dt.Year.ToString();
                string month = dt.Month.ToString();
                string day = dt.Day.ToString();
                return year + "-" + month.PadLeft(2, '0') + "-" + day.PadLeft(2, '0');
                //return str;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static string GenerSpace(int spaceNum)
        {
            if (spaceNum <= 0)
                return "";

            string strs = "";
            while (spaceNum != 0)
            {
                strs += "&nbsp;&nbsp;";
                spaceNum--;
            }
            return strs;
        }
        public static string GenerBR(int spaceNum)
        {
            string strs = "";
            while (spaceNum != 0)
            {
                strs += "<BR>";
                spaceNum--;
            }
            return strs;
        }
        public static bool IsImgExt(string ext)
        {
            ext = ext.Replace(".", "").ToLower();
            switch (ext)
            {
                case "gif":
                case "jpg":
                case "jepg":
                case "jpeg":
                case "bmp":
                case "png":
                case "tif":
                case "gsp":
                case "mov":
                case "psd":
                case "tiff":
                case "wmf":
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsVoideExt(string ext)
        {
            ext = ext.Replace(".", "").ToLower();
            switch (ext)
            {
                case "mp3":
                case "mp4":
                case "asf":
                case "wma":
                case "rm":
                case "rmvb":
                case "mpg":
                case "wmv":
                case "quicktime":
                case "avi":
                case "flv":
                case "mpeg":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// �ж��Ƿ���Num �ִ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumStr(string str)
        {
            try
            {
                decimal d = decimal.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �ǲ�ʱ����
        /// </summary>
        /// <param name="num">will judg value</param>
        /// <returns></returns>
        public static bool IsQS(int num)
        {
            int ii = 0;
            for (int i = 0; i < 500; i++)
            {
                if (num == ii)
                    return false;
                ii = ii + 2;
            }
            return true;
        }

        public static bool StringToBoolean(string str)
        {
            if (str == null || str == "" || str == ",nbsp;")
                return false;

            if (str == "0" || str == "1")
            {
                if (str == "0")
                    return false;
                else
                    return true;
            }
            else if (str == "true" || str == "false")
            {
                if (str == "false")
                    return false;
                else
                    return true;

            }
            else if (str == "��" || str == "��")
            {
                if (str == "��")
                    return false;
                else
                    return true;
            }
            else
                throw new Exception("@Ҫת����[" + str + "]����bool ����");
        }
    }

}
