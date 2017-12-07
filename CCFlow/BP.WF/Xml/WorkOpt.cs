using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ����ѡ��
    /// </summary>
	public class WorkOptXml:XmlEnNoName
    {
        #region ����.
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public new string CSS
        {
            get
            {
                return this.GetValStringByKey("CSS");
            }
        }

        public string URL
        {
            get
            {
                return this.GetValStringByKey("URL");
            }
        }
        #endregion ����.

        #region ����
        /// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public WorkOptXml()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new WorkOptXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ����ѡ��s
	/// </summary>
	public class WorkOptXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// ����ѡ��s
		/// </summary>
        public WorkOptXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new WorkOptXml();
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
				return "WorkOpt";
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
