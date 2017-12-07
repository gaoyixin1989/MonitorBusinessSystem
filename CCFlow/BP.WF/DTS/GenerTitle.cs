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
    /// �������ɱ���
    /// </summary>
    public class GenerTitle : Method
    {
        /// <summary>
        /// �������ɱ���
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
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                if (BP.Web.WebUser.No == "admin")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            BP.WF.Template.FlowSheets ens = new BP.WF.Template.FlowSheets();
            foreach (BP.WF.Template.FlowSheet en in ens)
            {
                en.DoGenerTitle();
            }
            return "ִ�гɹ�...";
        }
    }
}
