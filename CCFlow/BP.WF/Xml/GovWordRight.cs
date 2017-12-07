using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// �����ұ�ν��
    /// </summary>
    public class GovWordRightAttr
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
    /// �����ұ�ν��
    /// </summary>
	public class GovWordRight:XmlEnNoName
    {
        #region ����
        /// <summary>
        /// �����ұ�ν��
		/// </summary>
		public GovWordRight()
		{
		}
		/// <summary>
		/// �����ұ�ν��s
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new GovWordRights();
			}
		}
		#endregion
	}
	/// <summary>
    /// �����ұ�ν��s
	/// </summary>
	public class GovWordRights:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public GovWordRights() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new GovWordRight();
			}
		}
        /// <summary>
        /// XML�ļ�λ��.
        /// </summary>
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\WF\\Data\\XML\\XmlDB.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
                return "GovWordRight";
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
