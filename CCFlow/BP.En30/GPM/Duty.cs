using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ְ��
    /// </summary>
    public class DutyAttr : EntityNoNameAttr
    {
    }
	/// <summary>
    ///  ְ��
	/// </summary>
	public class Duty :EntityNoName
    {
        #region ����
        #endregion
     
		#region ���췽��
		/// <summary>
		/// ְ��
		/// </summary>
		public Duty()
        {
        }
        /// <summary>
        /// ְ��
        /// </summary>
        /// <param name="_No"></param>
        public Duty(string _No) : base(_No) { }
		#endregion 

		/// <summary>
		/// ְ��Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_Duty");
                map.EnDesc = "ְ��";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(DutyAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(DutyAttr.Name, null, "����", true, false, 1, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
	}
	/// <summary>
    /// ְ��
	/// </summary>
    public class Dutys : EntitiesNoName
	{
		/// <summary>
		/// ְ��s
		/// </summary>
        public Dutys() { }
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
                return new Duty();
			}
		}
	}
}
