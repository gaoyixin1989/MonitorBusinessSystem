using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;


namespace BP.Web.Comm
{
    public class RelationalMappingAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        public const string HelpFile = "HelpFile";
        public const string Img = "Img";
    }
	/// <summary>
	/// 
	/// </summary>
	public class RelationalMapping:XmlEn
	{

		#region ����
		public RelationalMapping()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
		public RelationalMapping(string no)
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new RelationalMappings();
            }
        }
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class RelationalMappings:XmlEns
	{
		#region ����
		/// <summary>
		/// ��ϵӳ��
		/// </summary>
        public RelationalMappings() 
        {
        }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new RelationalMapping();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\Helper\\RelationalMapping.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "RelationalMapping";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null; //new BP.ZF1.Helps();
			}
		}
		#endregion
		 
	}
}
