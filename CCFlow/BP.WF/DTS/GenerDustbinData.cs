using System;
using System.Data;
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
    public class GenerDustbinData : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public GenerDustbinData()
        {
            this.Title = "�ҳ���Ϊccbpm�ڲ��Ĵ������������������";
            this.Help = "ϵͳ��ȥ�Զ��޸�������Ҫ�ֹ���ȷ��ԭ��";
            this.Icon = "<img src='/WF/Img/Btn/Card.gif'  border=0 />";
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

            string msg = "";
            Flows fls = new Flows();
            fls.RetrieveAll();
            foreach (Flow fl in fls)
            {
                string rptTable = "ND" + int.Parse(fl.No) + "Rpt";
                string fk_mapdata = "ND" + int.Parse(fl.No) + "01";
                MapData md = new MapData(fk_mapdata);

                string sql = "SELECT OID,Title,Rec,WFState,WFState FROM " + md.PTable + " WHERE WFState=" + (int)WFState.Runing + " AND OID IN (SELECT OID FROM " + rptTable + " WHERE WFState!=0 )";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    continue;

                msg += "@" + sql;
                //msg += "�޸�sql: UPDATE " + ndTable"  " ;
            }
            if (string.IsNullOrEmpty(msg))
                return "@�ܼ�⵽����������.";

            BP.DA.Log.DefaultLogWriteLineInfo(msg);
            return  "�������ݲ����쳣����ʼ�ڵ�ı�ʾ�Ĳݸ�״̬��ʵ�ʹ����б�ʾ�����Ѿ������:"+msg+" ���ϵ�����д����Log�ļ��У���򿪲鿴��";
        }
    }
}
