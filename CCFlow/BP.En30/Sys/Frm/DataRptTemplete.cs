using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// �������ݴ洢ģ��
    /// </summary>
    public class DataRptAttr : EntityMyPKAttr
    {
        public const string RefOID = "RefOID";
        public const string ColCount = "ColCount";
        public const string RowCount = "RowCount";
        public const string Val = "Val";
    }
	/// <summary>
    ///  �������ݴ洢ģ��  
	/// </summary>
    public class DataRpt : EntityMyPK
    {
        #region ���췽��
        /// <summary>
        /// �������ݴ洢ģ��
        /// </summary>
        public DataRpt()
        {
        }
        #endregion

        /// <summary>
        /// �������ݴ洢ģ��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_DataRpt");
                map.EnDesc = "�������ݴ洢ģ��";
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBString(DataRptAttr.ColCount, null, "��", true, true, 0, 50, 20);
                map.AddTBString(DataRptAttr.RowCount, null, "��", true, true, 0, 50, 20);
                map.AddTBDecimal(DataRptAttr.Val, 0, "ֵ", true, false);
                map.AddTBDecimal(DataRptAttr.RefOID, 0, "������ֵ", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
	/// <summary>
    /// DataRpt���ݴ洢
	/// </summary>
    public class DataRpts : SimpleNoNames
    {
        /// <summary>
        /// DataRpt���ݴ洢s
        /// </summary>
        public DataRpts() 
        {
        }
        /// <summary>
        /// DataRpt���ݴ洢 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DataRpt();
            }
        }
    }
}
