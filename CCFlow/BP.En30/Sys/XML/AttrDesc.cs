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
    public class AttrDescAttr
    {
        /// <summary>
        /// ������Ϊ
        /// </summary>
        public const string Attr = "Attr";
        /// <summary>
        /// ���ʽ
        /// </summary>
        public const string Desc = "Desc";
        /// <summary>
        /// ����
        /// </summary>
        public const string For = "For";
        /// <summary>
        /// �߶�
        /// </summary>
        public const string Height = "Height";
        /// <summary>
        /// �Ƿ�����ļ�����
        /// </summary>
        public const string IsAcceptFile = "IsAcceptFile";
    }
	/// <summary>
	/// AttrDesc ��ժҪ˵�������Ե����á�
	/// </summary>
	public class AttrDesc:XmlEn
	{
		#region ����
		public string Attr
		{
			get
			{
				return this.GetValStringByKey(AttrDescAttr.Attr);
			}
		}
		public string For
		{
			get
			{
				return this.GetValStringByKey(AttrDescAttr.For);
			}
		}
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(AttrDescAttr.Desc);
            }
        }
        public bool IsAcceptFile
        {
            get
            {
                string s = this.GetValStringByKey(AttrDescAttr.IsAcceptFile);
                if (s == null || s == "" || s=="0")
                    return false;

                return true;
            }
        }
		public int Height
		{
            get
            {
                string str = this.GetValStringByKey(AttrDescAttr.Height);
                if (str == null || str == "")
                    return 200;
                return int.Parse(str);
            }
		}
		#endregion

		#region ����
		public AttrDesc()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AttrDescs();
			}
		}
		#endregion
	}
	/// <summary>
	/// ���Լ���
	/// </summary>
	public class AttrDescs:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public AttrDescs()
		{
		}
		public AttrDescs(string enName)
		{
			this.RetrieveBy(AttrDescAttr.For, enName);
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
				return new AttrDesc();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\AttrDesc\\";
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
