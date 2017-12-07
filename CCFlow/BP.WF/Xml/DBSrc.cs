using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// ����Դ����
    /// </summary>
    public class DBSrcAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ����Դ����
        /// </summary>
        public const string SrcType = "SrcType";
        /// <summary>
        /// ����Դurl
        /// </summary>
        public const string Url = "Url";
    }
    /// <summary>
    /// ����Դ����
    /// </summary>
	public class DBSrc:XmlEnNoName
    {
        #region ����
        /// <summary>
        /// ����Դ����
        /// </summary>
        public string SrcType
        {
            get
            {
                return this.GetValStringByKey(DBSrcAttr.SrcType);
            }
        }
        /// <summary>
        /// ����Դ����URL
        /// </summary>
        public string Url
        {
            get
            {
                return this.GetValStringByKey(DBSrcAttr.Url);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����Դ����
		/// </summary>
		public DBSrc()
		{
		}
		/// <summary>
		/// ����Դ����s
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new DBSrcs();
			}
		}
		#endregion
	}
	/// <summary>
    /// ����Դ����s
	/// </summary>
	public class DBSrcs:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public DBSrcs() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new DBSrc();
			}
		}
        /// <summary>
        /// XML�ļ�λ��.
        /// </summary>
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\DataUser\\XML\\DBSrc.xml";
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
