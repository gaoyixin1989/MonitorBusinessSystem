using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ��λ����
    /// </summary>
    public class StationTypeAttr : EntityNoNameAttr
    {
    }
	/// <summary>
    ///  ��λ����
	/// </summary>
	public class StationType :EntityNoName
    {
        #region ����
        public string FK_StationType
        {
            get
            {
                return this.GetValStrByKey(StationAttr.FK_StationType);
            }
            set
            {
                this.SetValByKey(StationAttr.FK_StationType, value);
            }
        }

        public string FK_StationTypeText
        {
            get
            {
                return this.GetValRefTextByKey(StationAttr.FK_StationType);
            }
        }

        #endregion
     
		#region ���췽��
		/// <summary>
		/// ��λ����
		/// </summary>
		public StationType()
        {
        }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <param name="_No"></param>
        public StationType(string _No) : base(_No) { }
		#endregion 

		/// <summary>
		/// ��λ����Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_StationType");
                map.EnDesc = "��λ����";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(StationTypeAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(StationTypeAttr.Name, null, "����", true, false, 1, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
	}
	/// <summary>
    /// ��λ����
	/// </summary>
    public class StationTypes : EntitiesNoName
	{
		/// <summary>
		/// ��λ����s
		/// </summary>
        public StationTypes() { }
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
                return new StationType();
			}
		}
	}
}
