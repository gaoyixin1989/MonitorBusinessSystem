using System;
using System.Threading;
using System.Collections;
using BP.Web.Controls;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.WF;

namespace BP.FlowEvent
{
    /// <summary>
    /// ��������001
    /// </summary>
    public class F001: BP.WF.FlowEventBase
    {
        #region ����.
        /// <summary>
        /// ��д���̱��
        /// </summary>
        public override string FlowMark
        {
            get { return "BaoXiao";   }
        }
        #endregion ����.

        #region ����.
        /// <summary>
        /// ���������¼�
        /// </summary>
        public F001()
        {
        }
        #endregion ����.

        /// <summary>
        /// ��д����ǰ�¼�
        /// </summary>
        /// <returns></returns>
        public override string SendWhen()
        {
            switch (this.HisNode.NodeID)
            {
                case 101:
                    //string empstr = "zhangsan";
                    ////���ºϼ�Сд , �Ѻϼ�ת���ɴ�д.
                    //string sql = "UPDATE ND101 SET JIeshourn='" + empstr + "'  WHERE OID=" + this.OID;
                    //BP.DA.DBAccess.RunSQL(sql);
                    ////this.ND101_SaveAfter();
                    break;
                default:
                    break;
            }
            return base.SendWhen();
        }

        /// <summary>
        /// �����ִ�е��¼�
        /// </summary>
        /// <returns></returns>
        public override string SaveAfter()
        {
            switch (this.HisNode.NodeID)
            {
                case 101:
                    this.ND101_SaveAfter();
                    break;
                default:
                    break;
            }
            return base.SaveAfter();
        }
        /// <summary>
        /// �ڵ㱣����¼�
        /// ������������Ϊ:ND+�ڵ���_�¼���.
        /// </summary>
        public void ND101_SaveAfter()
        {
            //�����ϸ��ĺϼ�.
            float hj = BP.DA.DBAccess.RunSQLReturnValFloat("SELECT SUM(XiaoJi) as Num FROM ND101Dtl1 WHERE RefPK=" + this.OID, 0);

            //���ºϼ�Сд , �Ѻϼ�ת���ɴ�д.
            string sql = "UPDATE ND101 SET DaXie='" + BP.DA.DataType.ParseFloatToCash(hj) + "',HeJi="+hj+"  WHERE OID=" + this.OID;
            BP.DA.DBAccess.RunSQL(sql);

             

            //if (1 == 2)
            //    throw new Exception("@ִ�д���xxxxxx.");
            //�����Ҫ���û���ʾִ�гɹ�����Ϣ���͸�����ֵ������Ͳ��ظ�ֵ��
            //this.SucessInfo = "ִ�гɹ���ʾ.";
        }
        /// <summary>
        /// ���ͳɹ��¼������ͳɹ�ʱ�������̵Ĵ���д������ϵͳ��.
        /// </summary>
        /// <returns>����ִ�н�����������null�Ͳ���ʾ��</returns>
        public override string SendSuccess()
        {
            try
            {
                // ��֯��Ҫ�ı���.
                Int64 workid = this.WorkID; // ����id.
                string flowNo = this.HisNode.FK_Flow; // ���̱��.
                int currNodeID = this.SendReturnObjs.VarCurrNodeID; //��ǰ�ڵ�id
                int toNodeID = this.SendReturnObjs.VarToNodeID; // ����ڵ�id.
                string toNodeName = this.SendReturnObjs.VarToNodeName; // ����ڵ����ơ�
                string acceptersID = this.SendReturnObjs.VarAcceptersID; // ������Աid, �����Ա���� ���ŷֿ� ,���� zhangsan,lisi��
                string acceptersName = this.SendReturnObjs.VarAcceptersName; // ������Ա���ƣ������Ա���ö��ŷֿ�����:����,����.

                //ִ��������ϵͳд�����.
                /*
                 * ��������Ҫ��д���ҵ���߼�������������֯�ı���.
                 * 
                 */

                //����.
                return base.SendSuccess();
            }
            catch(Exception ex)
            {
                throw new Exception("������ϵͳд�����ʧ�ܣ���ϸ��Ϣ��"+ex.Message);
            }
        }
    }
}
