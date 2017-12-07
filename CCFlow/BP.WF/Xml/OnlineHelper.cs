using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys.XML
{
    /// <summary>
    /// ���߰���
    /// </summary>
	public class OnlineHelper:XmlEnNoName
	{
		#region ����
		/// <summary>
        /// ���߰���
		/// </summary>
		public OnlineHelper()
		{
		}
		/// <summary>
        /// ���߰���
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new OnlineHelpers();
			}
		}
		#endregion
	}
	/// <summary>
    /// ���߰���s
	/// </summary>
	public class OnlineHelpers:XmlEns
	{
		#region ����
		/// <summary>
        /// ���߰���s
		/// </summary>
        public OnlineHelpers() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new OnlineHelper();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\WF\\OnlineHepler\\";
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
