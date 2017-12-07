using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;


namespace BP.WF.XML
{
    /// <summary>
    /// sdk
    /// </summary>
	public class SDK:XmlEn
	{
		#region ����
        public string No
        {
            get
            {
                return this.GetValStringByKey("DoWhat");
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey("Url");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public SDK()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new SDKs();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class SDKs:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public SDKs() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new SDK();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfData + "\\XML\\SDK.xml";
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
				return null; //new BP.ZF1.AdminTools();
			}
		}
		#endregion
		 
	}
}
