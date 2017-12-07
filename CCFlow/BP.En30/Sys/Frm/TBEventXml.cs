using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys
{
    /// <summary>
    /// �ı����¼�����
    /// </summary>
    public class TBEventXmlList
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Func = "Func";
        /// <summary>
        /// �¼�����
        /// </summary>
        public const string EventName = "EventName";
        /// <summary>
        /// Ϊ
        /// </summary>
        public const string DFor = "DFor";
    }
    /// <summary>
    /// �ı����¼�
    /// </summary>
	public class TBEventXml:XmlEn
	{
		#region ����
        /// <summary>
        /// �¼�����
        /// </summary>
        public string EventName
        {
            get
            {
                return this.GetValStringByKey(TBEventXmlList.EventName);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Func
        {
            get
            {
                return this.GetValStringByKey(TBEventXmlList.Func);
            }
        }
        /// <summary>
        /// ����Ϊ
        /// </summary>
        public string DFor
        {
            get
            {
                return this.GetValStringByKey(TBEventXmlList.DFor);
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ı����¼�
		/// </summary>
		public TBEventXml()
		{
		}
        public TBEventXml(string no)
        {
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new TBEventXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// �ı����¼�s
	/// </summary>
	public class TBEventXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// �ı����¼�s
		/// </summary>
        public TBEventXmls() { }
        /// <summary>
        /// �ı����¼�s
        /// </summary>
        /// <param name="dFor"></param>
        public TBEventXmls(string dFor)
        {
            this.Retrieve(TBEventXmlList.DFor, dFor);
        }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new TBEventXml();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfXML + "MapExt.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
                return "TBEvent";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null; //new BP.ZF1.AdminTools();
			}
		}
		#endregion
		 
	}
}
