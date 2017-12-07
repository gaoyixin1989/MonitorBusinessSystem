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
    /// Method ��ժҪ˵��
    /// </summary>
    public class ClearDB : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ClearDB()
        {
            this.Title = "����������е�����(�˹���Ҫ�ڲ��Ի���������)";
            this.Help = "��������������е����ݣ��������칤����";
            this.Warning = "�˹���Ҫ�ڲ��Ի�����ִ�У�ȷ���ǲ��Ի�����";
            this.Icon = "<img src='/WF/Img/Btn/Delete.gif'  border=0 />";
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
            if (BP.Web.WebUser.No != "admin")
                return "�Ƿ����û�ִ�С�";

            //DA.DBAccess.RunSQL("DELETE FROM WF_CHOfFlow");

            DA.DBAccess.RunSQL("DELETE FROM WF_Bill");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow");
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerFH");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper");
            DA.DBAccess.RunSQL("DELETE FROM WF_TransferCustom");
            DA.DBAccess.RunSQL("DELETE FROM WF_RememberMe");
            DA.DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB");
            DA.DBAccess.RunSQL("DELETE FROM WF_CCList");
            DA.DBAccess.RunSQL("DELETE FROM WF_CH"); //ɾ������.


            Flows fls = new Flows();
            fls.RetrieveAll();
            foreach (Flow item in fls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM ND" + int.Parse(item.No) + "Track");
                }
                catch
                {
                }
            }

            Nodes nds = new Nodes();
            foreach (Node nd in nds)
            {
                try
                {
                    Work wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE FROM " + wk.EnMap.PhysicsTable);
                }
                catch
                {
                }
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData nd in mds)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM " + nd.PTable);
                }
                catch
                {
                }
            }

            MapDtls dtls = new MapDtls();
            dtls.RetrieveAll();
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                }
                catch
                {
                }
            }
            return "ִ�гɹ�...";
        }
    }
}
