using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;


namespace BP.XML
{
    /// <summary>
    ///  RegularExpression ������ģ��
    /// </summary>
	public class RegularExpression:XmlEn
	{
		#region ����
        /// <summary>
        /// ���
        /// </summary>
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
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
        public string ForCtrl
        {
            get
            {
                return this.GetValStringByKey("ForCtrl");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
        public RegularExpression()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new RegularExpressions();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class RegularExpressions:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public RegularExpressions() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new RegularExpression();
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
