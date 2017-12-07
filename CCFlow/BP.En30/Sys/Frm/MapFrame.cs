using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ���
    /// </summary>
    public class MapFrameAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        public const string H = "H";
        public const string W = "W";
        /// <summary>
        /// �Ƿ��������Ӧ��С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        /// <summary>
        /// �ڲ����
        /// </summary>
        public const string NoOfObj = "NoOfObj";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
    }
    /// <summary>
    /// ���
    /// </summary>
    public class MapFrame : EntityMyPK
    {
        #region ����
        /// <summary>
        /// �Ƿ�����Ӧ��С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapFrameAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.IsAutoSize, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string NoOfObj
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.NoOfObj);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.NoOfObj, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.Name);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.Name, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string URL
        {
            get
            {
                string s= this.GetValStrByKey(MapFrameAttr.URL);
                if (string.IsNullOrEmpty(s))
                    return "http://ccflow.org";
                return s;
            }
            set
            {
                this.SetValByKey(MapFrameAttr.URL, value);
            }
        }
        /// <summary>
        /// �߶�
        /// </summary>
        public string H
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.H);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.H, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string W
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.W);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.W, value);
            }
        }
        public bool IsUse = false;
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.FK_MapData, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapFrameAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.RowIdx, value);
            }
        }
        
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapFrameAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.GroupID, value);
            }
        }
       
        #endregion

        #region ���췽��
        /// <summary>
        /// ���
        /// </summary>
        public MapFrame()
        {
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="no"></param>
        public MapFrame(string mypk)
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
                Map map = new Map("Sys_MapFrame");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "���";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(MapFrameAttr.NoOfObj, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapFrameAttr.Name, null, "����", true, false, 1, 200, 20);

                map.AddTBString(MapFrameAttr.FK_MapData, null, "����", true, false, 0, 30, 20);
                map.AddTBString(MapFrameAttr.URL, null, "URL", true, false, 0, 3000, 20);
                map.AddTBString(MapFrameAttr.W, null, "W", true, false, 0, 20, 20);
                map.AddTBString(MapFrameAttr.H, null, "H", true, false, 0, 20, 20);

                //map.AddTBInt(MapFrameAttr.H, 500, "�߶�", false, false);
                //map.AddTBInt(MapFrameAttr.W, 400, "���", false, false);

                map.AddBoolean(MapFrameAttr.IsAutoSize, true, "�Ƿ��Զ����ô�С", false, false);
                map.AddTBInt(MapFrameAttr.RowIdx, 99, "λ��", false, false);
                map.AddTBInt(MapFrameAttr.GroupID, 0, "GroupID", false, false);

                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                
                this._enMap = map;
                return this._enMap;
            }
        }
      
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            return base.beforeUpdateInsertAction();
        }
        #endregion
    }
    /// <summary>
    /// ���s
    /// </summary>
    public class MapFrames : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ���s
        /// </summary>
        public MapFrames()
        {
        }
        /// <summary>
        /// ���s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapFrames(string fk_mapdata)
        {
            this.Retrieve(MapFrameAttr.FK_MapData, fk_mapdata, MapFrameAttr.GroupID);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapFrame();
            }
        }
        #endregion
    }
}
