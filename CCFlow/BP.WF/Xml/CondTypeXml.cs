using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// Ƥ��
    /// </summary>
	public class CondTypeXml:XmlEnNoName
	{
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
        public CondTypeXml()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
                return new CondTypeXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// Ƥ��s
	/// </summary>
	public class CondTypeXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// Ƥ��s
		/// </summary>
        public CondTypeXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new CondTypeXml();
			}
		}
        public override string File
        {
            get
            {
                return SystemConfig.PathOfData + "\\Xml\\WFAdmin.xml";
            }
        }
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "CondType";
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
