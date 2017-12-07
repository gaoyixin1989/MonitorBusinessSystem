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
    public class ActiveAttrAttr
    {
        /// <summary>
        /// AttrKey
        /// </summary>
        public const string AttrKey = "AttrKey";
        /// <summary>
        /// ���ʽ
        /// </summary>
        public const string AttrName = "AttrName";
        /// <summary>
        /// ����
        /// </summary>
        public const string Exp = "Exp";
        /// <summary>
        /// ExpApp
        /// </summary>
        public const string ExpApp = "ExpApp";
        /// <summary>
        /// for
        /// </summary>
        public const string For = "For";
        /// <summary>
        /// ����
        /// </summary>
        public const string Condition = "Condition";
    }
	/// <summary>
	/// ActiveAttr ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ ActiveAttr ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
    public class ActiveAttr : XmlEn
    {
        #region ����
        /// <summary>
        /// ѡ���������ʱ����Ҫ������
        /// </summary>
        public string Condition
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.Condition);
            }
        }
        public string AttrKey
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.AttrKey);
            }
        }
        public string AttrName
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.AttrName);
            }
        }
        public string Exp
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.Exp);
            }
        }
        public string ExpApp
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.ExpApp);
            }
        }
        public string For
        {
            get
            {
                return this.GetValStringByKey(ActiveAttrAttr.For);
            }
        }
        #endregion

        #region ����
        public ActiveAttr()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new ActiveAttrs();
            }
        }
        #endregion
    }
	/// <summary>
	/// 
	/// </summary>
	public class ActiveAttrs:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public ActiveAttrs(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ActiveAttr();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\ActiveAttr.xml";
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
