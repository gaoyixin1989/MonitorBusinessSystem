using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys.Xml
{
	/// <summary>
	/// ����
	/// </summary>
	public class SearchAttr
	{
		/// <summary>
		/// ������Ϊ
		/// </summary>
		public const string Attr="Attr";
		/// <summary>
		/// ���ʽ
		/// </summary>
		public const string URL="URL";
		/// <summary>
		/// ����
		/// </summary>
		public const string For="For";
	}
	/// <summary>
	/// Search ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ Search ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
	public class Search:XmlEn
	{

		#region ����
		public string Attr
		{
			get
			{
				return this.GetValStringByKey(SearchAttr.Attr);
			}
		}
		public string For
		{
			get
			{
				return this.GetValStringByKey(SearchAttr.For);
			}
		}
		public string URL
		{
			get
			{
				return this.GetValStringByKey(SearchAttr.URL);
			}
		}
		#endregion

		#region ����
		public Search()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Searchs();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class Searchs:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public Searchs(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Search();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\Search.xml";
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
