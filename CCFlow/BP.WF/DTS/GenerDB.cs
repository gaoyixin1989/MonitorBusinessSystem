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
    /// ����ģ����������� 
    /// </summary>
    public class GenerDBTemplete : Method
    {
        /// <summary>
        /// ����ģ�����������
        /// </summary>
        public GenerDBTemplete()
        {
            this.Title = "����ģ�����������";
            this.Help = "�����ֹ��Ĳ鿴���������.";
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

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();

            string msg = "";
            Node nd = new Node();
            foreach (MapData item in mds)
            {
                if (item.No.Contains("ND") == false)
                    continue;

                string temp = item.No.Replace("ND", "");
                int nodeID = 0;
                try
                {
                    nodeID = int.Parse(temp);
                }
                catch
                {
                    continue;
                }

                nd.NodeID = nodeID;
                if (nd.IsExits == false)
                {
                    msg += "@" + item.No + "," + item.Name;
                    //ɾ����ģ��.
                    item.Delete();
                }
            }
            if (msg == "")
                msg = "��������.";
            else
                msg = "���½ڵ��Ѿ�ɾ�������ǽڵ������û�б�ɾ��." + msg;

            return msg;
        }
    }
}
