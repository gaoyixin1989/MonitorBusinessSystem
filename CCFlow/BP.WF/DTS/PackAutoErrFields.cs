using System;
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
    /// �޸��Ƿ��ֶ�����
    /// </summary>
    public class PackAutoErrFormatFieldTable : Method
    {
        /// <summary>
        /// �޸��Ƿ��ֶ�����
        /// </summary>
        public PackAutoErrFormatFieldTable()
        {
            this.Title = "�޸��Ƿ��ֶ�����,���������";
            this.Help = "����ǰ�İ汾�У��û�����������������ֶ����ĺϷ���û�м������ϵͳ���Զ�����������޸������ʱ���ִ��󡣴˲������������޸�ȫ�ֵı���";
            // this.Warning = "��ȷ��Ҫִ����";
            // this.HisAttrs.AddTBString("Path", "C:\\ccflow.Template", "���ɵ�·��", true, false, 1, 1900, 200);
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
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
            string keys = "~!@#$%^&*()+{}|:<>?`=[];,./�����������������������������������������������࣭���ۣݣ���������";
            char[] cc = keys.ToCharArray();
            foreach (char c in cc)
            {
                DBAccess.RunSQL("update sys_mapattr set keyofen=REPLACE(keyofen,'" + c + "' , '')");
            }

            BP.Sys.MapAttrs attrs = new Sys.MapAttrs();
            attrs.RetrieveAll();
            int idx = 0;
            string msg = "";
            foreach (BP.Sys.MapAttr item in attrs)
            {
                string f = item.KeyOfEn.Clone().ToString();
                try
                {
                    int i = int.Parse( item.KeyOfEn.Substring(0, 1) );
                    item.KeyOfEn = "F" + item.KeyOfEn;
                    try
                    {
                        MapAttr itemCopy = new MapAttr();
                        itemCopy.Copy(item);
                        itemCopy.Insert();
                        item.DirectDelete();
                    }
                    catch (Exception ex)
                    {
                        msg += "@" + ex.Message;
                    }
                }
                catch
                {
                    continue;
                }
                DBAccess.RunSQL("UPDATE sys_mapAttr set KeyOfEn='"+item.KeyOfEn+"', mypk=FK_MapData+'_'+keyofen where keyofen='"+item.KeyOfEn+"'");
                msg += "@��(" + idx + ")�������޸��ɹ���ԭ��"+f+"���޸���("+item.KeyOfEn+").";
                idx++;
            }

            BP.DA.DBAccess.RunSQL("UPDATE Sys_MapAttr SET MyPK=FK_MapData+'_'+KeyOfEn WHERE MyPK!=FK_MapData+'_'+KeyOfEn");
            return "�޸���Ϣ����:"+msg;
        }
    }
}
