using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    public class LangAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
    /// <summary>
    /// ����
    /// </summary>
	public class Lang:XmlEnNoName
	{
		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public Lang()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Langs();
			}
		}
		#endregion
	}
	/// <summary>
	/// ����s
	/// </summary>
	public class Langs:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public Langs() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Lang();
			}
		}
        /// <summary>
        /// XML�ļ�λ��.
        /// </summary>
		public override string File
		{
			get
			{
                return SystemConfig.CCFlowAppPath + "WF\\Style\\Tools.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Lang";
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
