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
    public class DTSCheckFlowAll : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public DTSCheckFlowAll()
        {
            this.Title = "���ȫ������";
            this.Help = "ֻ�ܹ����뵥�����������ͬ��������̲����˺����ݡ�";
            this.Help += "<br>1���޸��ڵ�������̱��������";
            this.Help += "<br>2������Ԥ��������ڵ�������ݣ��Ӷ��Ż�����ִ���ٶȡ�";
            this.Help += "<br>3���޸����̱������ݡ�";
            this.Help += "<br>4��ϵͳ������ʾ�������";
            this.Help += "<br>5������ʱ�䳤���������������ڵ����������ֶζ����й�ϵ�������ĵȴ���";
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
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                fl.DoCheck();
            }

            return "��ʾ��"+fls.Count+"�����̲�������졣";
        }
    }
}
