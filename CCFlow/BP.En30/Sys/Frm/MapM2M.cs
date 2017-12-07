using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ��Զ������
    /// </summary>
    public enum M2MType
    {
        /// <summary>
        /// һ�Զ�
        /// </summary>
        M2M,
        /// <summary>
        /// һ�Զ�Զ�
        /// </summary>
        M2MM
    }
    /// <summary>
    /// ��Ե�
    /// </summary>
    public class MapM2MAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �Ƿ��������Ӧ��С
        /// </summary>
        public const string ShowWay = "ShowWay";
        /// <summary>
        /// ����
        /// </summary>
        public const string M2MType = "M2MType";
        /// <summary>
        /// DBOfLists (��һ�Զ�Զ�ģʽ��Ч��
        /// </summary>
        public const string DBOfLists = "DBOfLists";
        /// <summary>
        /// DBOfObjs
        /// </summary>
        public const string DBOfObjs = "DBOfObjs";
        public const string DBOfGroups = "DBOfGroups";
        public const string IsDelete = "IsDelete";
        public const string IsInsert = "IsInsert";
        /// <summary>
        /// �Ƿ���ʾѡ��ȫ��?
        /// </summary>
        public const string IsCheckAll = "IsCheckAll";
        public const string W = "W";
        public const string H = "H";
        public const string X = "X";
        public const string Y = "Y";
        public const string Cols = "Cols";
        /// <summary>
        /// ������
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
    /// ���ַ�ʽ
    /// </summary>
    public enum FrmShowWay
    {
        /// <summary>
        /// ����
        /// </summary>
        Hidden,
        /// <summary>
        /// �Զ���С
        /// </summary>
        FrmAutoSize,
        /// <summary>
        /// ָ����С
        /// </summary>
        FrmSpecSize,
        /// <summary>
        /// ������
        /// </summary>
        WinOpen
    }
    /// <summary>
    /// ��Ե�
    /// </summary>
    public class MapM2M : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ��ʾ��ʽ
        /// </summary>
        public FrmShowWay ShowWay
        {
            get
            {
                return (FrmShowWay)this.GetValIntByKey(MapM2MAttr.ShowWay);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.ShowWay, (int)value);
            }
        }
        /// <summary>
        /// �Ƿ���ʾѡ��ȫ����
        /// </summary>
        public bool IsCheckAll
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsCheckAll);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsCheckAll, value);
            }
        }
        
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsDelete, value);
            }
        }
        public bool IsInsert
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsInsert);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsInsert, value);
            }
        }
        public bool IsEdit
        {
            get
            {
                if (this.IsInsert || this.IsDelete)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// �б�(��һ�Զ�Զ�ģʽ��Ч��
        /// </summary>
        public string DBOfLists
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfLists);
                sql = sql.Replace("~", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfLists, value);
            }
        }
        /// <summary>
        /// �б�(��һ�Զ�Զ�ģʽ��Ч��
        /// </summary>
        public string DBOfListsRun
        {
            get
            {
                string sql = this.DBOfLists;
                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return sql;
            }
        }
        public string DBOfObjs
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfObjs);
                sql = sql.Replace("~", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfObjs, value);
            }
        }
        public string DBOfGroups
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfGroups);
                sql = sql.Replace("~", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfGroups, value);
            }
        }
        public string DBOfObjsRun
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfObjs);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfObjs, value);
            }
        }
        public string DBOfGroupsRun
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfGroups);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfGroups, value);
            }
        }
        /// <summary>
        /// �ڲ����
        /// </summary>
        public string NoOfObj
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.NoOfObj);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.NoOfObj, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.Name);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Name, value);
            }
        }
        public bool IsUse = false;
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.FK_MapData, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.RowIdx, value);
            }
        }
        public int Cols
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.Cols);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Cols, value);
            }
        }
        public M2MType HisM2MType
        {
            get
            {
                return (M2MType)this.GetValIntByKey(MapM2MAttr.M2MType);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.M2MType, (int)value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.GroupID, value);
            }
        }
        public float H
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.H);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.H, value);
            }
        }
        public float W
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.W);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.W, value);
            }
        }
        public float X
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.X);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.X, value);
            }
        }
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.Y);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Y, value);
            }
        }
        /// <summary>
        /// ��չ����
        /// </summary>
        public int FK_Node
        {
            get
            {
                return int.Parse(this.FK_MapData.Replace("ND", ""));
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ե�
        /// </summary>
        public MapM2M()
        {
        }
        public MapM2M(string myPK)
        {
            this.MyPK = myPK;
            this.Retrieve();
        }
        /// <summary>
        /// ��Ե�
        /// </summary>
        /// <param name="fk_mapdata"></param>
        /// <param name="noOfObj"></param>
        public MapM2M(string fk_mapdata, string noOfObj)
        {
            this.FK_MapData=fk_mapdata;
            this.NoOfObj=noOfObj;
            this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            this.RetrieveFromDBSources();
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
                Map map = new Map("Sys_MapM2M");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��ѡ";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(MapM2MAttr.FK_MapData, null, "����", true, false, 1, 30, 20);
                map.AddTBString(MapM2MAttr.NoOfObj, null, "���", true, false, 1, 20, 20);

                map.AddTBString(MapM2MAttr.Name, null, "����", true, false, 1, 200, 20);

                map.AddTBString(MapM2MAttr.DBOfLists, null, "�б�����Դ(��һ�Զ�Զ�ģʽ��Ч��", true, false, 0, 4000, 20);

                map.AddTBString(MapM2MAttr.DBOfObjs, null, "DBOfObjs", true, false, 0, 4000, 20);
                map.AddTBString(MapM2MAttr.DBOfGroups, null, "DBOfGroups", true, false, 0, 4000, 20);

                map.AddTBFloat(MapM2MAttr.H, 100, "H", false, false);
                map.AddTBFloat(MapM2MAttr.W, 160, "W", false, false);
                map.AddTBFloat(FrmImgAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAttr.Y, 5, "Y", false, false);

                map.AddTBInt(MapM2MAttr.ShowWay, (int)FrmShowWay.FrmAutoSize, "��ʾ��ʽ", false, false);

                map.AddTBInt(MapM2MAttr.M2MType, (int)M2MType.M2M, "����", false, false);


                map.AddTBInt(MapM2MAttr.RowIdx, 99, "λ��", false, false);
                map.AddTBInt(MapM2MAttr.GroupID, 0, "����ID", false, false);

                map.AddTBInt(MapM2MAttr.Cols, 4, "��¼��������", false, false);

                map.AddBoolean(MapM2MAttr.IsDelete, true, "��ɾ����", false, false);
                map.AddBoolean(MapM2MAttr.IsInsert, true, "�ɲ����", false, false);

                map.AddBoolean(MapM2MAttr.IsCheckAll, true, "�Ƿ���ʾѡ��ȫ��", false, false);


                //map.AddTBFloat(FrmImgAttr.H, 200, "H", true, false);
                //map.AddTBFloat(FrmImgAttr.W, 500, "W", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeInsert()
        {
            if (this.DBOfObjs.Trim().Length <= 5)
            {
                this.DBOfGroups = "SELECT No,Name FROM Port_Dept";
                this.DBOfObjs = "SELECT No,Name,FK_Dept FROM Port_Emp";
            }

            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            return base.beforeUpdateInsertAction();
        }
        #endregion
    }
    /// <summary>
    /// ��Ե�s
    /// </summary>
    public class MapM2Ms : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ��Ե�s
        /// </summary>
        public MapM2Ms()
        {
        }
        /// <summary>
        /// ��Ե�s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapM2Ms(string fk_mapdata)
        {
            this.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata, MapM2MAttr.GroupID);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapM2M();
            }
        }
        #endregion
    }
}
