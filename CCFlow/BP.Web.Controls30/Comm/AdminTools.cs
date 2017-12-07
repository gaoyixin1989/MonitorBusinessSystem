using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;


namespace BP.Web.Port.Xml
{
	public class AdminToolAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string ICON="ICON";
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
		/// <summary>
		/// Url
		/// </summary>
		public const string Url="Url";
		/// <summary>
		/// DESC
		/// </summary>
		public const string DESC="DESC";
		/// <summary>
		/// 
		/// </summary>
		public const string Enable="Enable";
	}
	public class AdminTool:XmlEn
	{
		#region ����
		public string Enable
		{
			get
			{
				return this.GetValStringByKey(AdminToolAttr.Enable);
			}
		}
		public string Url
		{
			get
			{
				return this.GetValStringByKey(AdminToolAttr.Url);
			}
		}
		public string DESC
		{
			get
			{
				return this.GetValStringByKey(AdminToolAttr.DESC);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string ICON
		{
			get
			{
				return this.GetValStringByKey(AdminToolAttr.ICON);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(AdminToolAttr.Name);
			}
		}
		#endregion

		#region ����
		public AdminTool()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
		public AdminTool(string no)
		{

		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AdminTools();
			}
		}
		#endregion

		#region  ��������
		 
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class AdminTools:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
		public AdminTools(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new AdminTool();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\AdminTools.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "AdminTool";
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
