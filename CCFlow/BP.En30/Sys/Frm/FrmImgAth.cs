using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ͼƬ����
    /// </summary>
    public class FrmImgAthAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Text
        /// </summary>
        public const string Text = "Text";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// W
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// H
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// �ؼ�ID
        /// </summary>
        public const string CtrlID = "CtrlID";
        /// <summary>
        /// �Ƿ�ɱ༭
        /// </summary>
        public const string IsEdit = "IsEdit";
    }
    /// <summary>
    /// ͼƬ����
    /// </summary>
    public class FrmImgAth : EntityMyPK
    {
        #region ����
        /// <summary>
        /// �ؼ�ID
        /// </summary>
        public string CtrlID
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthAttr.CtrlID);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.CtrlID, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAthAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAthAttr.X);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.X, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAthAttr.H);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.H, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAthAttr.W);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.W, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmImgAthAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �Ƿ�ɱ༭
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.GetValBooleanByKey(FrmImgAthAttr.IsEdit);
            }
            set
            {
                this.SetValByKey(FrmImgAthAttr.IsEdit, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public FrmImgAth()
        {
        }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        /// <param name="mypk"></param>
        public FrmImgAth(string mypk)
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
                Map map = new Map("Sys_FrmImgAth");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ͼƬ��������";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmImgAthAttr.FK_MapData, null, "��ID", true, false, 1, 30, 20);
                map.AddTBString(FrmImgAthAttr.CtrlID, null, "�ؼ�ID", true, false, 0, 200, 20);

              //map.AddTBString(FrmImgAthAttr.CtrlName, null, "�ؼ�����", true, false, 0, 200, 20);

                map.AddTBFloat(FrmImgAthAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAthAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmImgAthAttr.H, 200, "H", true, false);
                map.AddTBFloat(FrmImgAthAttr.W, 160, "W", false, false);

                map.AddTBInt(FrmImgAthAttr.IsEdit, 1, "�Ƿ�ɱ༭", true, true);
                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + this.CtrlID;
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ͼƬ����s
    /// </summary>
    public class FrmImgAths : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ͼƬ����s
        /// </summary>
        public FrmImgAths()
        {
        }
        /// <summary>
        /// ͼƬ����s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmImgAths(string fk_mapdata)
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
                return new FrmImgAth();
            }
        }
        #endregion
    }
}
