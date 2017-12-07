using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// �ڵ��¼�����
    /// </summary>
    abstract public class NodeEventBase
    {
        #region ����.
        public Node HisNode = null;
        public Entity HisEn = null;
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
            string str = this.GetValStr(key).ToString();
            return DataType.ParseSysDateTime2DateTime(str);
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
        public decimal GetValDecimal(string key)
        {
            return decimal.Parse(this.GetValStr(key));
        }
        #endregion ��ȡ��������

        #region ���췽��
        /// <summary>
        /// �¼�����
        /// </summary>
        public NodeEventBase()
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
        /// <summary>
        /// �ڵ����s, �����ж���ڵ���.
        /// ����ڵ������Ҫ�ö��ŷֿ�.
        /// </summary>
        abstract public string NodeMarks
        {
            get;
        }
        #endregion Ҫ��������д������.

        #region Ҫ��������д�ķ���(���¼�).
        /// <summary>
        /// ������ǰ
        /// </summary>
        abstract public string FrmLoadBefore();
        /// <summary>
        /// �������
        /// </summary>
        abstract public string FrmLoadAfter();
        /// <summary>
        /// ������ǰ
        /// </summary>
        abstract public string SaveBefore();
        /// <summary>
        /// �������
        /// </summary>
        abstract public string SaveAfter();
        #endregion Ҫ��������д�ķ���(���¼�).

        #region Ҫ��������д�ķ���(�ڵ��¼�).
        /// <summary>
        /// �ڵ㷢��ǰ
        /// </summary>
        abstract public string SendWhen();
        /// <summary>
        /// �ڵ㷢�ͳɹ���
        /// </summary>
        abstract public string SendSuccess();
        /// <summary>
        /// �ڵ㷢��ʧ�ܺ�
        /// </summary>
        abstract public string SendError();
        /// <summary>
        /// ���ڵ��˻�ǰ
        /// </summary>
        abstract public string ReturnBefore();
        /// <summary>
        /// ���ڵ��˺�
        /// </summary>
        abstract public string ReturnAfter();
        /// <summary>
        /// ���ڵ㳷������ǰ
        /// </summary>
        abstract public string UnSendBefore();
        /// <summary>
        /// ���ڵ㳷�����ͺ�
        /// </summary>
        abstract public string UnSendAfter();
        #endregion Ҫ��������д�ķ���(�ڵ��¼�).

        #region ���෽��.
        /// <summary>
        /// ִ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="en">ʵ�����</param>
        public string DoIt(string eventType, Node currNode, Entity en, string atPara)
        {
            //���Ľڵ�.
            this.HisNode = currNode;
            this.HisEn = en;

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
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString)
                {
                    string val = System.Web.HttpContext.Current.Request.QueryString[key];
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
                case EventListOfNode.SendWhen:
                    return this.SendWhen();
                case EventListOfNode.SendSuccess:
                    return this.SendSuccess();
                case EventListOfNode.SendError:
                    return this.SendError();
                case EventListOfNode.ReturnBefore:
                    return this.ReturnBefore();
                case EventListOfNode.ReturnAfter:
                    return this.ReturnAfter();
                case EventListOfNode.UndoneBefore:
                    return this.UnSendBefore();
                case EventListOfNode.UndoneAfter:
                    return this.UnSendAfter();
                case EventListOfNode.SaveBefore:
                    return this.SaveBefore();
                case EventListOfNode.SaveAfter:
                    return this.SaveAfter();
                case EventListOfNode.FrmLoadBefore:
                    return this.FrmLoadBefore();
                case EventListOfNode.FrmLoadAfter:
                    return this.FrmLoadAfter();
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
