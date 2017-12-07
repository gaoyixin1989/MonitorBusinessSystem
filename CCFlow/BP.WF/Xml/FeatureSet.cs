using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
	/// <summary>
	/// ���Ի�����
	/// </summary>
	public class FeatureSet:XmlEnNoName
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
		public FeatureSet()
		{
		}
        public FeatureSet(string no)
        {
            
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new FeatureSets();
			}
		}
		#endregion
	}
	/// <summary>
    /// ���Ի�����s
	/// </summary>
	public class FeatureSets:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public FeatureSets() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new FeatureSet();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfXML + "FeatureSet.xml";
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
