using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys
{
    public class MapExtXmlList
    {
        /// <summary>
        /// ��ȡ�������ⲿ����
        /// </summary>
        public const string AutoFull = "AutoFull";
        /// <summary>
        /// ��˵�
        /// </summary>
        public const string ActiveDDL = "ActiveDDL";
        /// <summary>
        /// ������֤
        /// </summary>
        public const string InputCheck = "InputCheck";
        /// <summary>
        /// �ı����Զ����
        /// </summary>
        public const string TBFullCtrl = "TBFullCtrl";
        /// <summary>
        /// Pop����ֵ
        /// </summary>
        public const string PopVal = "PopVal";
        /// <summary>
        /// Func
        /// </summary>
        public const string Func = "Func";
        /// <summary>
        /// (��̬��)���������
        /// </summary>
        public const string AutoFullDLL = "AutoFullDLL";
        /// <summary>
        /// �������Զ����
        /// </summary>
        public const string DDLFullCtrl = "DDLFullCtrl";
        /// <summary>
        /// ��װ�����
        /// </summary>
        public const string PageLoadFull = "PageLoadFull";
        /// <summary>
        /// ��������
        /// </summary>
        public const string StartFlow = "StartFlow";
        /// <summary>
        /// ������.
        /// </summary>
        public const string Link = "Link";
        /// <summary>
        /// �Զ����ɱ��
        /// </summary>
        public const string AotuGenerNo = "AotuGenerNo";
        /// <summary>
        /// ������ʽ
        /// </summary>
        public const string RegularExpression = "RegularExpression";


        public const string WordFrm = "WordFrm";
        public const string ExcelFrm = "ExcelFrm";

    }
	public class MapExtXml:XmlEnNoName
	{
		#region ����
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStringByKey("URL");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public MapExtXml()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new MapExtXmls();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class MapExtXmls:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public MapExtXmls() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new MapExtXml();
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
				return "FieldExt";
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
