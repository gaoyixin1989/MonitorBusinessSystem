using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// Ĭ��ֵ
    /// </summary>
	public class DefVal:XmlEnNoName
	{
		#region ����
        public string Val
        {
            get
            {
                return this.GetValStringByKey("Val");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public DefVal()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new DefVals();
			}
		}
		#endregion
	}
	/// <summary>
    /// Ĭ��ֵs
	/// </summary>
	public class DefVals:XmlEns
	{
		#region ����
        
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public DefVals() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new DefVal();
			}
		}
        public override string File
        {
            get
            {
                return SystemConfig.PathOfData + "\\Xml\\DefVal.xml";
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
				return null; //new BP.ZF1.AdminDefVals();
			}
		}
		#endregion
		 
	}
}
