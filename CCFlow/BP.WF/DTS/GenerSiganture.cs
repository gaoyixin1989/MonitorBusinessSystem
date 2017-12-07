using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;

namespace BP.WF.DTS
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class GenerSiganture : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public GenerSiganture()
        {
            this.Title = "Ϊû����������ǩ�����û�����Ĭ�ϵ�����ǩ��";
            this.Help = "�˹�����Ҫ�û��� "+ BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\ �ж�дȨ�ޣ������ִ��ʧ�ܡ�";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            try
            {
                BP.Port.Emps emps = new Emps();
                emps.RetrieveAllFromDBSource();
                string path = BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\T.JPG";
                string fontName = "����";
                string empOKs = "";
                string empErrs = "";
                foreach (Emp emp in emps)
                {
                    string pathMe = BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\" + emp.No + ".JPG";
                    if (System.IO.File.Exists(pathMe))
                        continue;

                    File.Copy(BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\Templete.JPG",
                        path, true);

                    System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                    Font font = new Font(fontName, 15);
                    Graphics g = Graphics.FromImage(img);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical);//�ı�
                    g.DrawString(emp.Name, font, drawBrush, 3, 3);
                    img.Save(pathMe);
                    img.Dispose();
                    g.Dispose();

                    File.Copy(pathMe,
                    BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\" + emp.Name + ".JPG", true);
                }
                return "ִ�гɹ�...";
            }
            catch(Exception ex)
            {
                return "ִ��ʧ�ܣ���ȷ�϶� " + BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\ Ŀ¼�з���Ȩ�ޣ��쳣��Ϣ:"+ex.Message;
            }
        }
    }
}
