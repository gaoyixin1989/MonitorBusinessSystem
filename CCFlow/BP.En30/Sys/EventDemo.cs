using System;
using System.Threading;
using System.Collections;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web.Controls;
using BP.Web;

namespace BP.Sys
{
    /// <summary>
    /// �¼�Demo
    /// </summary>
    abstract public class EventDemo:EventBase
    {
        #region ����.
        #endregion ����.

        /// <summary>
        /// �¼�Demo
        /// </summary>
        public EventDemo()
        {
            this.Title = "�¼�demoִ����ʾ.";
        }
        /// <summary>
        /// ִ���¼�
        /// 1���������������׳��쳣��Ϣ��ǰ̨����ͻ���ʾ���󲢲�����ִ�С�
        /// 2��ִ�гɹ�����ִ�еĽ������SucessInfo�������������Ҫ��ʾ�͸�ֵΪ�ջ���Ϊnull��
        /// 3�����еĲ��������Դ�  this.SysPara.GetValByKey�л�ȡ��
        /// </summary>
        public override void Do()
        {
            if (1 == 2)
                throw new Exception("@ִ�д���xxxxxx.");


            //�����Ҫ���û���ʾִ�гɹ�����Ϣ���͸�����ֵ������Ͳ��ظ�ֵ��
            this.SucessInfo = "ִ�гɹ���ʾ.";
        }
    }
}
