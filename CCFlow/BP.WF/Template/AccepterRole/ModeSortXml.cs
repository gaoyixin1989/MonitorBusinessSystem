using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ģʽ
    /// </summary>
	public class ModeSortXml:XmlEnNoName
	{
		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
        public ModeSortXml()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
                return new ModeSortXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ģʽs
	/// </summary>
	public class ModeSortXmls:XmlEns
	{
		#region ����
		/// <summary>
        /// ģʽs
		/// </summary>
        public ModeSortXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ModeSortXml();
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
                return "ModelSort";
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
