using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��Ԫ����չDB
    /// </summary>
    public class FrmEleDBAttr : EntityMyPKAttr
    {
        /// <summary>
        /// RefPKVal
        /// </summary>
        public const string RefPKVal = "RefPKVal";
        /// <summary>
        /// EleID
        /// </summary>
        public const string EleID = "EleID";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// Tag1
        /// </summary>
        public const string Tag1 = "Tag1";
        /// <summary>
        /// Tag2
        /// </summary>
        public const string Tag2 = "Tag2";
        /// <summary>
        /// Tag3
        /// </summary>
        public const string Tag3 = "Tag3";
        /// <summary>
        /// Tag4
        /// </summary>
        public const string Tag4 = "Tag4";
    }
    /// <summary>
    /// ��Ԫ����չDB
    /// </summary>
    public class FrmEleDB : EntityMyPK
    {
        #region ����
        /// <summary>
        /// EleID
        /// </summary>
        public string EleID
        {
            get
            {
                return this.GetValStrByKey(FrmEleDBAttr.EleID);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.EleID, value);
            }
        }
        /// <summary>
        /// Tag1
        /// </summary>
        public string Tag1
        {
            get
            {
                return this.GetValStringByKey(FrmEleDBAttr.Tag1);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.Tag1, value);
            }
        }
        /// <summary>
        /// Tag2
        /// </summary>
        public string Tag2
        {
            get
            {
                return this.GetValStringByKey(FrmEleDBAttr.Tag2);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.Tag2, value);
            }
        }
        /// <summary>
        /// Tag3
        /// </summary>
        public string Tag3
        {
            get
            {
                return this.GetValStringByKey(FrmEleDBAttr.Tag3);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.Tag3, value);
            }
        }
        /// <summary>
        /// Tag4
        /// </summary>
        public string Tag4
        {
            get
            {
                return this.GetValStringByKey(FrmEleDBAttr.Tag4);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.Tag4, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmEleDBAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// RefPKVal
        /// </summary>
        public string RefPKVal
        {
            get
            {
                return this.GetValStrByKey(FrmEleDBAttr.RefPKVal);
            }
            set
            {
                this.SetValByKey(FrmEleDBAttr.RefPKVal, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ԫ����չDB
        /// </summary>
        public FrmEleDB()
        {
        }
        /// <summary>
        /// ��Ԫ����չDB
        /// </summary>
        /// <param name="mypk"></param>
        public FrmEleDB(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_FrmEleDB");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��Ԫ����չDB";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(FrmEleDBAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmEleDBAttr.EleID, null, "EleID", true, false, 0, 50, 20);
                map.AddTBString(FrmEleDBAttr.RefPKVal, null, "RefPKVal", true, false, 0, 50, 20);

                map.AddTBString(FrmEleDBAttr.Tag1, null, "Tag1", true, false, 0, 4000, 20);
                map.AddTBString(FrmEleDBAttr.Tag2, null, "Tag2", true, false, 0, 4000, 20);
                map.AddTBString(FrmEleDBAttr.Tag3, null, "Tag3", true, false, 0, 4000, 20);
                map.AddTBString(FrmEleDBAttr.Tag4, null, "Tag4", true, false, 0, 4000, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            //this.MyPK = this.FK_MapData + "_" + this.EleID + "_" + this.RefPKVal;
            this.GenerPKVal();
            return base.beforeUpdateInsertAction();
        }
        public void GenerPKVal()
        {
            this.MyPK = this.FK_MapData + "_" + this.EleID + "_" + this.RefPKVal;
        }
    }
    /// <summary>
    /// ��Ԫ����չDBs
    /// </summary>
    public class FrmEleDBs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ��Ԫ����չDBs
        /// </summary>
        public FrmEleDBs()
        {
        }
        /// <summary>
        /// ��Ԫ����չDBs
        /// </summary>
        /// <param name="fk_mapdata"></param>
        /// <param name="pkval"></param>
        public FrmEleDBs(string fk_mapdata, string pkval)
        {
            this.Retrieve(FrmAttachmentDBAttr.FK_MapData, fk_mapdata,
            FrmAttachmentDBAttr.RefPKVal, pkval);
        }
        /// <summary>
        /// ��Ԫ����չDBs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmEleDBs(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmEleDB();
            }
        }
        #endregion
    }
}
