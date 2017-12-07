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
    public class GroupTitleAttr
    {
        public const string For = "For";
        public const string Key = "Key";
        public const string Title = "Title";
    }
	/// <summary>
	/// GroupTitle ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ GroupTitle ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
    public class GroupTitle : XmlEn
    {
        #region ����
        /// <summary>
        /// ѡ���������ʱ����Ҫ������
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GroupTitleAttr.Title);
            }
        }
        public string For
        {
            get
            {
                return this.GetValStringByKey(GroupTitleAttr.For);
            }
        }
        public string Key
        {
            get
            {
                return this.GetValStringByKey(GroupTitleAttr.Key);
            }
        }
        #endregion

        #region ����
        public GroupTitle()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new GroupTitles();
            }
        }
        #endregion
    }
	/// <summary>
	/// 
	/// </summary>
	public class GroupTitles:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public GroupTitles(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new GroupTitle();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\GroupTitle.xml";
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
