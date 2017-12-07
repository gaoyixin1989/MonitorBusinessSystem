using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.Sys;
using BP.En;
namespace BP.WF.DTS
{
    /// <summary>
    /// �޸��ڵ��map ��ժҪ˵��
    /// </summary>
    public class RepariNodeFrmMap : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariNodeFrmMap()
        {
            this.Title = "�޸��ڵ��";
            this.Help = "���ڵ��ϵͳ�ֶ��Ƿ񱻷Ƿ�ɾ��������Ƿ�ɾ���Զ�������������Щ�ֶΰ���:Rec,Title,OID,FID,WFState,RDT,CDT";
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
            Nodes nds = new Nodes();
            nds.RetrieveAllFromDBSource();

            string info = "";
            foreach (Node nd in nds)
            {
                string msg = nd.RepareMap();
                if (msg != "")
                    info += "<b>������" + nd.FlowName + ",�ڵ�(" + nd.NodeID + ")(" + nd.Name + "), ���������:</b>" + msg + "<hr>";
            }
            return info + "ִ�����...";
        }
    }
}
