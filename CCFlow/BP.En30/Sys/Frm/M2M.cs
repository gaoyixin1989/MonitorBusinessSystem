using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
    /// <summary>
    /// M2M
    /// </summary>
    public class M2MAttr : EntityMyPKAttr
    {
        public const string FK_MapData = "FK_MapData";
        public const string M2MNo = "M2MNo";
        public const string EnOID = "EnOID";
        public const string Doc = "Doc";
        public const string ValsSQL = "ValsSQL";
        public const string ValsName = "ValsName";
        public const string DtlObj = "DtlObj";
        /// <summary>
        /// ѡ����
        /// </summary>
        public const string NumSelected = "NumSelected";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
    }
	/// <summary>
    ///  M2M ���ݴ洢
	/// </summary>
    public class M2M : EntityMyPK
    {
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(M2MAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// ѡ����
        /// </summary>
        public int NumSelected
        {
            get
            {
                return this.GetValIntByKey(M2MAttr.NumSelected);
            }
            set
            {
                this.SetValByKey(M2MAttr.NumSelected, value);
            }
        }
        public Int64 EnOID
        {
            get
            {
                return this.GetValInt64ByKey(M2MAttr.EnOID);
            }
            set
            {
                this.SetValByKey(M2MAttr.EnOID, value);
            }
        }
        /// <summary>
        /// �����������m2mm��Ч
        /// </summary>
        public string DtlObj
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.DtlObj);
            }
            set
            {
                this.SetValByKey(M2MAttr.DtlObj, value);
            }
        }
        public string ValsSQL
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.ValsSQL);
            }
            set
            {
                this.SetValByKey(M2MAttr.ValsSQL, value);
            }
        }
        public string ValsName
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.ValsName);
            }
            set
            {
                this.SetValByKey(M2MAttr.ValsName, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Vals
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.Doc);
            }
            set
            {
                this.SetValByKey(M2MAttr.Doc, value);
            }
        }
        public string M2MNo
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.M2MNo);
            }
            set
            {
                this.SetValByKey(M2MAttr.M2MNo, value);
            }
        }
        #region ���췽��
        /// <summary>
        /// M2M���ݴ洢
        /// </summary>
        public M2M()
        {
        }
        /// <summary>
        /// M2M���ݴ洢
        /// </summary>
        /// <param name="fk_mapdata">��ID</param>
        /// <param name="m2mNo"></param>
        /// <param name="enOID"></param>
        /// <param name="dtlObj"></param>
        public M2M(string fk_mapdata, string m2mNo, Int64 enOID, string dtlObj)
        {
            this.FK_MapData = fk_mapdata;
            this.M2MNo = m2mNo;
            this.EnOID = enOID;
            this.DtlObj = dtlObj;
            this.InitMyPK();
            this.Retrieve();
        }
        /// <summary>
        /// M2M���ݴ洢
        /// </summary>
        public M2M(string fk_mapdata, string m2mNo, int enOID)
        {
            this.FK_MapData = fk_mapdata;
            this.M2MNo = m2mNo;
            this.EnOID = enOID;
            this.InitMyPK();
            this.Retrieve();
        }
        #endregion

        /// <summary>
        /// M2M���ݴ洢
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_M2M");
                map.EnDesc = "M2M���ݴ洢";
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBString(M2MAttr.FK_MapData, null, "FK_MapData", true, true, 0, 20, 20);
                map.AddTBString(M2MAttr.M2MNo, null, "M2MNo", true, true, 0, 20, 20);

                map.AddTBInt(M2MAttr.EnOID, 0, "ʵ��OID", true, false);
                map.AddTBString(M2MAttr.DtlObj, null, "DtlObj(����m2mm��Ч)", true, true, 0, 20, 20);

                map.AddTBStringDoc();
                map.AddTBStringDoc(M2MAttr.ValsName, null, "ValsName", true, true);
                map.AddTBStringDoc(M2MAttr.ValsSQL, null, "ValsSQL", true, true);

                map.AddTBInt(M2MAttr.NumSelected, 0, "ѡ����", true, false);
                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ����ǰ
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.InitMyPK();
            return base.beforeUpdateInsertAction();
        }
        public void InitMyPK()
        {   
            this.MyPK = this.FK_MapData + "_" + this.M2MNo + "_" + this.EnOID + "_" + this.DtlObj;
        }
    }
	/// <summary>
    /// M2M���ݴ洢
	/// </summary>
    public class M2Ms : SimpleNoNames
    {
        /// <summary>
        /// M2M���ݴ洢s
        /// </summary>
        public M2Ms() 
        {
        }
        /// <summary>
        /// M2M���ݴ洢s
        /// </summary>
        /// <param name="FK_MapData"></param>
        /// <param name="EnOID"></param>
        public M2Ms(string FK_MapData, Int64 EnOID)
        {
            this.Retrieve(M2MAttr.FK_MapData, FK_MapData, M2MAttr.EnOID, EnOID.ToString());
        }
        /// <summary>
        /// M2M���ݴ洢 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new M2M();
            }
        }
    }
}
