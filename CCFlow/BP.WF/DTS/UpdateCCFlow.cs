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
    public class UpdateCCFlow : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public UpdateCCFlow()
        {
            this.Title = "����ccflow";
            this.Help = "ִ�ж�ccflow������������������������µĴ��룬������Ҫִ�иù��ܣ����ж�ccflow�����ݿ�������";
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

            BP.WF.Glo.UpdataCCFlowVer();
 
            return "ִ�гɹ�,ϵͳ�Ѿ��޸������°汾�����ݿ�.";
        }
    }
}
