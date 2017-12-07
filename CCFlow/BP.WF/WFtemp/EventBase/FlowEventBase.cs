using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;
using BP.WF.XML;

namespace BP.WF
{
    /// <summary>
    /// �����¼�����
    /// </summary>
    abstract public class FlowEventBase
    {
        #region ����.
        /// <summary>
        /// ���Ͷ���
        /// </summary>
        public SendReturnObjs SendReturnObjs = null;
        /// <summary>
        /// ʵ�壬һ���ǹ���ʵ��
        /// </summary>
        public Entity HisEn = null;
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public Node HisNode = null;
        /// <summary>
        /// ��������.
        /// </summary>
        private Row _SysPara = null;
        /// <summary>
        /// ����
        /// </summary>
        public Row SysPara
        {
            get
            {
                if (_SysPara == null)
                    _SysPara = new Row();
                return _SysPara;
            }
            set
            {
                _SysPara = value;
            }
        }
        /// <summary>
        /// �ɹ���Ϣ
        /// </summary>
        public string SucessInfo = null;
        #endregion ����.

        #region ϵͳ����
        /// <summary>
        /// ��ID
        /// </summary>
        public string FK_Mapdata
        {
            get
            {
                return this.GetValStr("FK_MapData");
            }
        }
        #endregion

        #region ��������.
        /// <summary>
        /// ����ID
        /// </summary>
        public int OID
        {
            get
            {
                return this.GetValInt("OID");
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                if (this.OID == 0)
                    return this.GetValInt64("WorkID"); /*�п��ܿ�ʼ�ڵ��WorkID=0*/
                return this.OID;
            }
        }
        /// <summary>
        /// FID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64("FID");
            }
        }
        /// <summary>
        /// ��������WorkIDs���ϣ�������.
        /// </summary>
        public string WorkIDs
        {
            get
            {
                return this.GetValStr("WorkIDs");
            }
        }
        /// <summary>
        /// ��ż���s
        /// </summary>
        public string Nos
        {
            get
            {
                return this.GetValStr("Nos");
            }
        }
        #endregion ��������.

        #region ��ȡ��������
        public DateTime GetValDateTime(string key)
        {
            try
            {
                string str = this.SysPara.GetValByKey(key).ToString();
                return DataType.ParseSysDateTime2DateTime(str);
            }
            catch (Exception ex)
            {
                throw new Exception("@�����¼�ʵ���ڻ�ȡ�����ڼ���ִ�����ȷ���ֶ�(" + key + ")�Ƿ�ƴд��ȷ,������Ϣ:" + ex.Message);
            }
        }
        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>���ΪNul,���߲����ھ��׳��쳣</returns>
        public string GetValStr(string key)
        {
            try
            {
                return this.SysPara.GetValByKey(key).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("@�����¼�ʵ���ڻ�ȡ�����ڼ���ִ�����ȷ���ֶ�(" + key + ")�Ƿ�ƴд��ȷ,������Ϣ:" + ex.Message);
            }
        }
        /// <summary>
        /// ��ȡInt64����ֵ
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <returns>���ΪNul,���߲����ھ��׳��쳣</returns>
        public Int64 GetValInt64(string key)
        {
            return Int64.Parse(this.GetValStr(key));
        }
        /// <summary>
        /// ��ȡint����ֵ
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <returns>���ΪNul,���߲����ھ��׳��쳣</returns>
        public int GetValInt(string key)
        {
            return int.Parse(this.GetValStr(key));
        }
        public bool GetValBoolen(string key)
        {
            if (int.Parse(this.GetValStr(key)) == 0)
                return false;
            return true;
        }
        /// <summary>
        /// ��ȡdecimal����ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetValDecimal(string key)
        {
            return decimal.Parse(this.GetValStr(key));
        }
        #endregion ��ȡ��������

        #region ���췽��
        /// <summary>
        /// �����¼�����
        /// </summary>
        public FlowEventBase()
        {
        }
        #endregion ���췽��

        #region Ҫ������ǿ����д������.
        /// <summary>
        /// ���̱��
        /// </summary>
        abstract public string FlowMark
        {
            get;
        }
        #endregion Ҫ��������д������.

        #region �ڵ���¼�
        public virtual string FrmLoadAfter()
        {
            return null;
        }
        public virtual string FrmLoadBefore()
        {
            return null;
        }
        #endregion

        #region Ҫ��������д�ķ���(�����¼�).
        /// <summary>
        /// �������ǰ
        /// </summary>
        public virtual string FlowOverBefore()
        {
            return null;
        }
        /// <summary>
        /// ������
        /// </summary>
        public virtual string FlowOverAfter()
        {
            return null;
        }
        /// <summary>
        ///����ɾ��ǰ
        /// </summary>
        public virtual string BeforeFlowDel()
        {
            return null;
        }
        /// <summary>
        /// ����ɾ����
        /// </summary>
        public virtual string AfterFlowDel()
        {
            return null;
        }
        #endregion Ҫ��������д�ķ���(�����¼�).


        #region Ҫ��������д�ķ���(�ڵ��¼�).
        /// <summary>
        /// �����
        /// </summary>
        public virtual string SaveAfter()
        {
            return null;
        }
        /// <summary>
        /// ����ǰ
        /// </summary>
        public virtual string SaveBefore()
        {
            return null;
        }
        /// <summary>
        ///����ǰ
        /// </summary>
        public virtual string SendWhen()
        {
            return null;
        }
        /// <summary>
        /// ���ͳɹ�ʱ
        /// </summary>
        public virtual string SendSuccess()
        {
            return null;
        }
        /// <summary>
        /// ����ʧ��
        /// </summary>
        /// <returns></returns>
        public virtual string SendError() { return null; }
        public virtual string ReturnBefore() { return null; }
        public virtual string ReturnAfter() { return null; }
        public virtual string UndoneBefore() { return null; }
        public virtual string UndoneAfter() { return null; }
        /// <summary>
        /// �ƽ���
        /// </summary>
        /// <returns></returns>
        public virtual string ShiftAfter()
        {
            return null;
        }
        /// <summary>
        /// ��ǩ��
        /// </summary>
        /// <returns></returns>
        public virtual string AskerAfter()
        {
            return null;
        }
        /// <summary>
        /// ��ǩ�𸴺�
        /// </summary>
        /// <returns></returns>
        public virtual string AskerReAfter()
        {
            return null;
        }
        /// ���нڵ㷢�ͺ�
        /// </summary>
        /// <returns></returns>
        public virtual string QueueSendAfter() { return null; }
        #endregion Ҫ��������д�ķ���(�ڵ��¼�).


        #region ���෽��.
        /// <summary>
        /// ִ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="en">ʵ�����</param>
        public string DoIt(string eventType, Node currNode, Entity en, string atPara)
        {
            this.HisEn = en;
            this.HisNode = currNode;

            #region �������.
            Row r = en.Row;
            try
            {
                //ϵͳ����.
                r.Add("FK_MapData", en.ClassID);
            }
            catch
            {
                r["FK_MapData"] = en.ClassID;
            }

            if (atPara != null)
            {
                AtPara ap = new AtPara(atPara);
                foreach (string s in ap.HisHT.Keys)
                {
                    try
                    {
                        r.Add(s, ap.GetValStrByKey(s));
                    }
                    catch
                    {
                        r[s] = ap.GetValStrByKey(s);
                    }
                }
            }

            if (SystemConfig.IsBSsystem == true)
            {
                /*�����bsϵͳ, �ͼ����ⲿurl�ı���.*/
                foreach (string key in BP.Sys.Glo.Request.QueryString)
                {
                    string val = BP.Sys.Glo.Request.QueryString[key];
                    try
                    {
                        r.Add(key, val);
                    }
                    catch
                    {
                        r[key] = val;
                    }
                }
            }
            this.SysPara = r;
            #endregion �������.

            #region ִ���¼�.
            switch (eventType)
            {
                case EventListOfNode.FrmLoadAfter: // �ڵ���¼���
                    return this.FrmLoadAfter();
                case EventListOfNode.FrmLoadBefore: // �ڵ���¼���
                    return this.FrmLoadBefore();
                case EventListOfNode.SaveAfter: // �ڵ��¼� �����
                    return this.SaveAfter();
                case EventListOfNode.SaveBefore: // �ڵ��¼� - ����ǰ.��
                    return this.SaveBefore();
                case EventListOfNode.SendWhen: // �ڵ��¼� - ����ǰ��
                    return this.SendWhen();
                case EventListOfNode.SendSuccess: // �ڵ��¼� - ���ͳɹ�ʱ��
                    return this.SendSuccess();
                case EventListOfNode.SendError: // �ڵ��¼� - ����ʧ�ܡ�
                    return this.SendError();
                case EventListOfNode.ReturnBefore: // �ڵ��¼� - �˻�ǰ��
                    return this.ReturnBefore();
                case EventListOfNode.ReturnAfter: // �ڵ��¼� - �˻غ�
                    return this.ReturnAfter();
                case EventListOfNode.UndoneBefore: // �ڵ��¼� - ����ǰ��
                    return this.UndoneBefore();
                case EventListOfNode.UndoneAfter: // �ڵ��¼� - ������
                    return this.UndoneAfter();
                case EventListOfNode.ShitAfter:// �ڵ��¼�-�ƽ���
                    return this.ShiftAfter();
                case EventListOfNode.AskerAfter://�ڵ��¼� ��ǩ��
                    return this.AskerAfter();
                case EventListOfNode.AskerReAfter://�ڵ��¼���ǩ�ظ���
                    return this.FlowOverBefore();
                case EventListOfNode.QueueSendAfter://���нڵ㷢�ͺ�
                    return this.AskerReAfter();
                case EventListOfNode.FlowOverBefore: // �����¼� -------------------------------------------��
                    return this.FlowOverBefore();
                case EventListOfNode.FlowOverAfter: // �����¼���
                    return this.FlowOverAfter();
                case EventListOfNode.BeforeFlowDel: // �����¼���
                    return this.BeforeFlowDel();
                case EventListOfNode.AfterFlowDel: // �����¼���
                    return this.AfterFlowDel();
                default:
                    throw new Exception("@û���жϵ��¼�����:" + eventType);
                    break;
            }
            #endregion ִ���¼�.
            return null;
        }
        #endregion ���෽��.
    }
}
