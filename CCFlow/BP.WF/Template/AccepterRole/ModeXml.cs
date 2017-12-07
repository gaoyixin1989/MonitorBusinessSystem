using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// ģʽ
    /// </summary>
	public class ModeXml:XmlEnNoName
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public string SetDesc
        {
            get
            {
                return this.GetValStringByKey("SetDesc");
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_ModeSort
        {
            get
            {
                return this.GetValStringByKey("FK_ModeSort");
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey("Note");
            }
        }
        public string ParaType
        {
            get
            {
                return this.GetValStringByKey("ParaType");
            }
        }
        #endregion
      
		#region ����
		/// <summary>
		/// ģʽ
		/// </summary>
        public ModeXml()
		{
		}
        /// <summary>
        /// ģʽ
        /// </summary>
        public ModeXml(string no):base(no)
        {
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
                return new ModeXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ģʽs
	/// </summary>
	public class ModeXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// Ƥ��s
		/// </summary>
        public ModeXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ModeXml();
			}
		}
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\WF\\Admin\\AccepterRole\\AccepterRole.xml";
            }
        }
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
                return "Model";
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
