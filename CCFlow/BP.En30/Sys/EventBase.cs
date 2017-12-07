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
    /// �¼�����
    /// </summary>
    abstract public class EventBase
    {
        #region ����.
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
        private string _title = null;
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                if (_title == null)
                    _title = "δ����";
                return _title;
            }
            set
            {
                _title = value;
            }
        }
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
        /// <summary>
        /// �¼�����
        /// </summary>
        public string EventType
        {
            get
            {
                return this.GetValStr("EventType");
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
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStr("FK_Flow");
            }
        }
        /// <summary>
        /// �ڵ���
        /// </summary>
        public int FK_Node
        {
            get
            {
                try
                {
                    return this.GetValInt("FK_Node");
                }
                catch {
                    return 0;
                }
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
            string str= this.SysPara.GetValByKey(key).ToString();
            return DataType.ParseSysDateTime2DateTime(str);
        }
        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>���ΪNul,���߲����ھ��׳��쳣</returns>
        public string GetValStr(string key)
        {
            return this.SysPara.GetValByKey(key).ToString();
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
        #endregion ��ȡ��������

        /// <summary>
        /// �¼�����
        /// </summary>
        public EventBase()
        {
        }
        /// <summary>
        /// ִ���¼�
        /// 1���������������׳��쳣��Ϣ��ǰ̨����ͻ���ʾ���󲢲�����ִ�С�
        /// 2��ִ�гɹ�����ִ�еĽ������SucessInfo�������������Ҫ��ʾ�͸�ֵΪ�ջ���Ϊnull��
        /// 3�����еĲ��������Դ�  this.SysPara.GetValByKey�л�ȡ��
        /// </summary>
        abstract public void Do();
    }
}
