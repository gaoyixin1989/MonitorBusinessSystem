using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Sys.Xml
{
    public class GlobalKeyValList
    {
        /// <summary>
        /// ϵ������s
        /// </summary>
        public const string Subjection = "Subjection";
    }
	/// <summary>
	/// ����
	/// </summary>
	public class GlobalKeyValAttr
	{
		/// <summary>
		/// �߶�
		/// </summary>
        public const string Key = "Key";
        /// <summary>
        /// �Ƿ�����ļ�����
        /// </summary>
        public const string Val = "Val";
	}
	/// <summary>
	/// GlobalKeyVal ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ GlobalKeyVal ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
	public class GlobalKeyVal:XmlEn
	{
		#region ����
        public string Key
		{
			get
			{
                return this.GetValStringByKey(GlobalKeyValAttr.Key);
			}
		}
        public string Val
		{
			get
			{
                return this.GetValStringByKey(GlobalKeyValAttr.Val);
			}
		}
		#endregion

		#region ����
		public GlobalKeyVal()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new GlobalKeyVals();
			}
		}
		#endregion
	}
	/// <summary>
	/// ȫ�ֵ�Key val ���͵ı�������
	/// </summary>
	public class GlobalKeyVals:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public GlobalKeyVals()
		{
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
				return new GlobalKeyVal();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\GlobalKeyVal.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Item";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		#endregion
	}
}
