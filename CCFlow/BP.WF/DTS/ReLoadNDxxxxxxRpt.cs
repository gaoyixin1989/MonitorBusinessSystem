using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF.DTS
{
    /// <summary>
    /// �޸���������ֶγ��� ��ժҪ˵��
    /// </summary>
    public class ReLoadNDxxxxxxRpt : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ReLoadNDxxxxxxRpt()
        {
            this.Title = "���������װ�����̱���";
            this.Help = "ɾ��NDxxxRpt�����ݣ�����װ�أ��˹��ܹ���Ҫִ�кܳ�ʱ�䣬����������ϴ��п�����web������ִ��ʧ�ܡ�";
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
                else
                    return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string msg = "";
            msg+=Flow.RepareV_FlowData_View();

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                try
                {
                    msg += fl.DoReloadRptData();
                }
                catch(Exception ex)
                {
                    msg += "@�ڴ�������(" + fl.Name + ")�����쳣" + ex.Message;
                }
            }
            return "��ʾ��"+fls.Count+"�����̲�������죬��Ϣ���£�@"+msg;
        }
    }
}
