using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF
{
    /// <summary>
    /// �������ɱ��� ��ժҪ˵��
    /// </summary>
    public class GenerTitle : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public GenerTitle()
        {
            this.Title = "�������ɱ��⣨Ϊ���е����̣������µĹ����������̱��⣩";
            this.Help = "��Ҳ���Դ���������һ�����ĵ���ִ�С�";
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
            BP.WF.Ext.FlowSheets ens = new Ext.FlowSheets();
            foreach (BP.WF.Ext.FlowSheet en in ens)
            {
                en.DoGenerTitle();
            }
            return "ִ�гɹ�...";
        }
    }
}
