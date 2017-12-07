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
    public class EnumInfoXmlAttr
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
        /// ����
        /// </summary>
        public const string Vals = "Vals";
    }
	/// <summary>
	/// EnumInfoXml ��ժҪ˵�������Ե����á�
	/// </summary>
    public class EnumInfoXml : XmlEn
    {
        #region ����
        public string Key
        {
            get
            {
                return this.GetValStringByKey("Key");
            }
        }
        /// <summary>
        /// Vals
        /// </summary>
        public string Vals
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        #endregion

        #region ����
        public EnumInfoXml()
        {
        }
        public EnumInfoXml(string key)
        {
            this.RetrieveByPK("Key", key);
        }
        
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EnumInfoXmls();
            }
        }
        #endregion
    }
	/// <summary>
	/// ���Լ���
	/// </summary>
	public class EnumInfoXmls:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public EnumInfoXmls()
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
				return new EnumInfoXml();
			}
		}
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\Enum\\";
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
