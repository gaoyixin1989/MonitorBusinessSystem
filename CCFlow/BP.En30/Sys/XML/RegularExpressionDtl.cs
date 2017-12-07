using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;

namespace BP.XML
{
    /// <summary>
    ///  RegularExpressionDtl ������ģ��
    /// </summary>
	public class RegularExpressionDtl:XmlEn
	{
		#region ����
        /// <summary>
        /// ���
        /// </summary>
        public string ItemNo
        {
            get
            {
                return this.GetValStringByKey("ItemNo");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey("Name");
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey("Note");
            }
        }
        public string Exp
        {
            get
            {
                return this.GetValStringByKey("Exp");
            }
        }
        public string ForEvent
        {
            get
            {
                return this.GetValStringByKey("ForEvent");
            }
        }
        public string Msg
        {
            get
            {
                return this.GetValStringByKey("Msg");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
        public RegularExpressionDtl()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new RegularExpressionDtls();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class RegularExpressionDtls:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public RegularExpressionDtls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new RegularExpressionDtl();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfData + "\\XML\\RegularExpression.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Dtl";
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
