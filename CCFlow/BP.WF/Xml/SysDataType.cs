using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys.XML
{
    /// <summary>
    /// ��������
    /// </summary>
	public class SysDataType:XmlEn
	{
		#region ����
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
		/// <summary>
		/// ͼƬ
		/// </summary>
		public string Desc
		{
			get
			{
                return this.GetValStringByKey("Desc");
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public SysDataType()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new SysDataTypes();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class SysDataTypes:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public SysDataTypes() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new SysDataType();
			}
		}
        public override string File
        {
            get
            {
              //  return SystemConfig.PathOfWebApp + "\\WF\\MapDef\\Style\\SysDataType.xml";
                return SystemConfig.PathOfData + "\\XML\\SysDataType.xml";
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
				return null; //new BP.ZF1.AdminTools();
			}
		}
		#endregion
		 
	}
}
