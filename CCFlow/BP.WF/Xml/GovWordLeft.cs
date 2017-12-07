using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// �������ν��
    /// </summary>
    public class GovWordLeftAttr
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
    /// �������ν��
    /// </summary>
	public class GovWordLeft:XmlEnNoName
    {

        #region ����
        /// <summary>
        /// �������ν��
		/// </summary>
		public GovWordLeft()
		{
		}
		/// <summary>
		/// �������ν��s
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new GovWordLefts();
			}
		}
		#endregion
	}
	/// <summary>
    /// �������ν��s
	/// </summary>
	public class GovWordLefts:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public GovWordLefts() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new GovWordLeft();
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
                return "GovWordLeft";
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
