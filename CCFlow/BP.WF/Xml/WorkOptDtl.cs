using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ������ϸѡ��
    /// </summary>
	public class WorkOptDtlXml:XmlEnNoName
	{
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStringByKey("URL");
            }
        }

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public WorkOptDtlXml()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new WorkOptDtlXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ������ϸѡ��s
	/// </summary>
	public class WorkOptDtlXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// ������ϸѡ��s
		/// </summary>
        public WorkOptDtlXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new WorkOptDtlXml();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\WF\\Style\\Tools.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "WorkOptDtl";
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
