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
	public class ShortKeyAttr
	{
		/// <summary>
		/// ������Ϊ
		/// </summary>
		public const string No="No";
		/// <summary>
		/// Name
		/// </summary>
		public const string Name="Name";
		/// <summary>
		/// ���ʽ
		/// </summary>
		public const string URL="URL";
		/// <summary>
		/// ����
		/// </summary>
		public const string DFor="DFor";
		/// <summary>
		/// ͼƬ
		/// </summary>
		public const string Img="Img";
        /// <summary>
        /// Target
        /// </summary>
        public const string Target = "Target";
	}
	/// <summary>
	/// ShortKey ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ ShortKey ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
	public class ShortKey:XmlEn
	{
		#region ����
		public string No
		{
			get
			{
				return this.GetValStringByKey(ShortKeyAttr.No);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string DFor
		{
			get
			{
				return this.GetValStringByKey(ShortKeyAttr.DFor);
			}
		}
		public string Name
		{
			get
			{
                return this.GetValStringByKey(BP.Web.WebUser.SysLang );
			}
		}
		/// <summary>
		/// URL
		/// </summary>
		public string URL
		{
			get
			{
				return this.GetValStringByKey(ShortKeyAttr.URL);
			}
		}
		/// <summary>
		/// ͼƬ
		/// </summary>
		public string Img
		{
			get
			{
				return this.GetValStringByKey(ShortKeyAttr.Img);
			}
		}
        public string Target
        {
            get
            {
                return this.GetValStringByKey(ShortKeyAttr.Target);
            }
        }
		#endregion

		#region ����
		public ShortKey()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new ShortKeys();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class ShortKeys:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public ShortKeys(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ShortKey();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Menu.xml";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "ShortKey";
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
