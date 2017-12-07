using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys
{
    public class ExpFucnXmlList
    {
        /// <summary>
        /// ��˵�
        /// </summary>
        public const string ActiveDDL = "ActiveDDL";
        /// <summary>
        /// ������֤
        /// </summary>
        public const string InputCheck = "InputCheck";
        /// <summary>
        /// �Զ����
        /// </summary>
        public const string AutoFull = "AutoFull";
        /// <summary>
        /// Pop����ֵ
        /// </summary>
        public const string PopVal = "PopVal";
        /// <summary>
        /// Func
        /// </summary>
        public const string Func = "Func";
    }
	public class ExpFucnXml:XmlEnNoName
	{
		#region ����
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public ExpFucnXml()
		{
		}
        public ExpFucnXml(string no)
        {
            
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new ExpFucnXmls();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class ExpFucnXmls:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public ExpFucnXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ExpFucnXml();
			}
		}
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "MapExt.xml";
            }
        }
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
                return "ExpFunc";
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
