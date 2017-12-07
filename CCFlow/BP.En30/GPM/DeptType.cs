using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DeptTypeAttr : EntityNoNameAttr
    {
    }
	/// <summary>
    ///  ��������
	/// </summary>
	public class DeptType :EntityNoName
    {
        #region ����
        #endregion
     
		#region ���췽��
		/// <summary>
		/// ��������
		/// </summary>
		public DeptType()
        {
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_No"></param>
        public DeptType(string _No) : base(_No) { }
		#endregion 

		/// <summary>
		/// ��������Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_DeptType");
                map.EnDesc = "��������";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(DeptTypeAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(DeptTypeAttr.Name, null, "����", true, false, 1, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
	}
	/// <summary>
    /// ��������
	/// </summary>
    public class DeptTypes : EntitiesNoName
	{
		/// <summary>
		/// ��������s
		/// </summary>
        public DeptTypes() { }
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
                return new DeptType();
			}
		}
	}
}
