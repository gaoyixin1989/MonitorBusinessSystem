using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;


namespace BP.Web.Port.Xml
{
    public class ItemAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// HelpFile
        /// </summary>
        public const string HelpFile = "HelpFile";
    }
	/// <summary>
	/// 
	/// </summary>
	public class Item:XmlEn
	{
		#region ����
        public string HelpFile
        {
            get
            {
                return this.GetValStringByKey(ItemAttr.HelpFile);
            }
        }
		/// <summary>
		/// ���
		/// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(ItemAttr.No);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
                return this.GetValStringByKey(BP.Web.WebUser.SysLang );
			}
		}
		#endregion

		#region ����
		public Item()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
		public Item(string no)
		{

		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Items();
			}
		}
		#endregion

		#region  ��������
		 
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class Items:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
		public Items(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Item();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\Menu.xml";
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
				return null; //new BP.ZF1.Items();
			}
		}
		#endregion
		 
	}
}
