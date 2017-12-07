using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// γ�ȱ���
    /// </summary>
    public class FrmRptAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// PTable
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// DtlOpenType
        /// </summary>
        public const string DtlOpenType = "DtlOpenType";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// ������
        /// </summary>
        public const string RowsOfList = "RowsOfList";
        /// <summary>
        /// �Ƿ���ʾ�ϼ�
        /// </summary>
        public const string IsShowSum = "IsShowSum";
        /// <summary>
        /// �Ƿ���ʾidx
        /// </summary>
        public const string IsShowIdx = "IsShowIdx";
        /// <summary>
        /// �Ƿ�����copy����
        /// </summary>
        public const string IsCopyNDData = "IsCopyNDData";
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public const string IsReadonly = "IsReadonly";
        /// <summary>
        /// WhenOverSize
        /// </summary>
        public const string WhenOverSize = "WhenOverSize";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDelete = "IsDelete";
        /// <summary>
        /// �Ƿ���Բ���
        /// </summary>
        public const string IsInsert = "IsInsert";
        /// <summary>
        /// �Ƿ���Ը���
        /// </summary>
        public const string IsUpdate = "IsUpdate";
        /// <summary>
        /// �Ƿ�����ͨ��
        /// </summary>
        public const string IsEnablePass = "IsEnablePass";
        /// <summary>
        /// �Ƿ��Ǻ�����������
        /// </summary>
        public const string IsHLDtl = "IsHLDtl";
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public const string IsShowTitle = "IsShowTitle";
        /// <summary>
        /// ��ʾ��ʽ
        /// </summary>
        public const string DtlShowModel = "DtlShowModel";
        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public const string IsView = "IsView";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// H�߶�
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// w���
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// 
        /// </summary>
        public const string FrmW = "FrmW";
        /// <summary>
        /// 
        /// </summary>
        public const string FrmH = "FrmH";
        /// <summary>
        /// �Ƿ���Ե���
        /// </summary>
        public const string IsExp = "IsExp";
        /// <summary>
        /// �Ƿ���Ե��룿
        /// </summary>
        public const string IsImp = "IsImp";
        /// <summary>
        /// �Ƿ����ö฽��
        /// </summary>
        public const string IsEnableAthM = "IsEnableAthM";
        /// <summary>
        /// �Ƿ�����һ�Զ�
        /// </summary>
        public const string IsEnableM2M = "IsEnableM2M";
        /// <summary>
        /// �Ƿ�����һ�Զ��
        /// </summary>
        public const string IsEnableM2MM = "IsEnableM2MM";
        /// <summary>
        /// ���ͷ��
        /// </summary>
        public const string MTR = "MTR";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// ����
        /// </summary>
        public const string GroupField = "GroupField";
        /// <summary>
        /// �Ƿ����÷����ֶ�
        /// </summary>
        /// 
        public const string IsEnableGroupField = "IsEnableGroupField";
        public const string SQLOfColumn = "SQLOfColumn";
        public const string SQLOfRow = "SQLOfRow";
    }
    /// <summary>
    /// γ�ȱ���
    /// </summary>
    public class FrmRpt : EntityNoName
    {
        #region �������
        /// <summary>
        /// ���
        /// </summary>
        public MapFrames MapFrames
        {
            get
            {
                MapFrames obj = this.GetRefObject("MapFrames") as MapFrames;
                if (obj == null)
                {
                    obj = new MapFrames(this.No);
                    this.SetRefObject("MapFrames", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public GroupFields GroupFields_del
        {
            get
            {
                GroupFields obj = this.GetRefObject("GroupFields") as GroupFields;
                if (obj == null)
                {
                    obj = new GroupFields(this.No);
                    this.SetRefObject("GroupFields", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// �߼���չ
        /// </summary>
        public MapExts MapExts
        {
            get
            {
                MapExts obj = this.GetRefObject("MapExts") as MapExts;
                if (obj == null)
                {
                    obj = new MapExts(this.No);
                    this.SetRefObject("MapExts", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// �¼�
        /// </summary>
        public FrmEvents FrmEvents
        {
            get
            {
                FrmEvents obj = this.GetRefObject("FrmEvents") as FrmEvents;
                if (obj == null)
                {
                    obj = new FrmEvents(this.No);
                    this.SetRefObject("FrmEvents", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// һ�Զ�
        /// </summary>
        public MapM2Ms MapM2Ms
        {
            get
            {
                MapM2Ms obj = this.GetRefObject("MapM2Ms") as MapM2Ms;
                if (obj == null)
                {
                    obj = new MapM2Ms(this.No);
                    this.SetRefObject("MapM2Ms", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// �ӱ�
        /// </summary>
        public FrmRpts FrmRpts
        {
            get
            {
                FrmRpts obj = this.GetRefObject("FrmRpts") as FrmRpts;
                if (obj == null)
                {
                    obj = new FrmRpts(this.No);
                    this.SetRefObject("FrmRpts", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public FrmLinks FrmLinks
        {
            get
            {
                FrmLinks obj = this.GetRefObject("FrmLinks") as FrmLinks;
                if (obj == null)
                {
                    obj = new FrmLinks(this.No);
                    this.SetRefObject("FrmLinks", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ��ť
        /// </summary>
        public FrmBtns FrmBtns
        {
            get
            {
                FrmBtns obj = this.GetRefObject("FrmLinks") as FrmBtns;
                if (obj == null)
                {
                    obj = new FrmBtns(this.No);
                    this.SetRefObject("FrmBtns", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// Ԫ��
        /// </summary>
        public FrmEles FrmEles
        {
            get
            {
                FrmEles obj = this.GetRefObject("FrmEles") as FrmEles;
                if (obj == null)
                {
                    obj = new FrmEles(this.No);
                    this.SetRefObject("FrmEles", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public FrmLines FrmLines
        {
            get
            {
                FrmLines obj = this.GetRefObject("FrmLines") as FrmLines;
                if (obj == null)
                {
                    obj = new FrmLines(this.No);
                    this.SetRefObject("FrmLines", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ��ǩ
        /// </summary>
        public FrmLabs FrmLabs
        {
            get
            {
                FrmLabs obj = this.GetRefObject("FrmLabs") as FrmLabs;
                if (obj == null)
                {
                    obj = new FrmLabs(this.No);
                    this.SetRefObject("FrmLabs", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public FrmImgs FrmImgs
        {
            get
            {
                FrmImgs obj = this.GetRefObject("FrmLabs") as FrmImgs;
                if (obj == null)
                {
                    obj = new FrmImgs(this.No);
                    this.SetRefObject("FrmLabs", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FrmAttachments FrmAttachments
        {
            get
            {
                FrmAttachments obj = this.GetRefObject("FrmAttachments") as FrmAttachments;
                if (obj == null)
                {
                    obj = new FrmAttachments(this.No);
                    this.SetRefObject("FrmAttachments", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public FrmImgAths FrmImgAths
        {
            get
            {
                FrmImgAths obj = this.GetRefObject("FrmImgAths") as FrmImgAths;
                if (obj == null)
                {
                    obj = new FrmImgAths(this.No);
                    this.SetRefObject("FrmImgAths", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ��ѡ��ť
        /// </summary>
        public FrmRBs FrmRBs
        {
            get
            {
                FrmRBs obj = this.GetRefObject("FrmRBs") as FrmRBs;
                if (obj == null)
                {
                    obj = new FrmRBs(this.No);
                    this.SetRefObject("FrmRBs", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public MapAttrs MapAttrs
        {
            get
            {
                MapAttrs obj = this.GetRefObject("MapAttrs") as MapAttrs;
                if (obj == null)
                {
                    obj = new MapAttrs(this.No);
                    this.SetRefObject("MapAttrs", obj);
                }
                return obj;
            }
        }
        #endregion

        #region ����
        public string SQLOfRow
        {
            get
            {
                return this.GetValStringByKey(FrmRptAttr.SQLOfRow);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.SQLOfRow, value);
            }
        }
        public string SQLOfColumn
        {
            get
            {
                return this.GetValStringByKey(FrmRptAttr.SQLOfColumn);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.SQLOfColumn, value);
            }
        }
        public GEDtls HisGEDtls_temp = null;
        public DtlShowModel HisDtlShowModel
        {
            get
            {
                return (DtlShowModel)this.GetValIntByKey(FrmRptAttr.DtlShowModel);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.DtlShowModel, (int)value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public WhenOverSize HisWhenOverSize
        {
            get
            {
                return (WhenOverSize)this.GetValIntByKey(FrmRptAttr.WhenOverSize);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.WhenOverSize, (int)value);
            }
        }
        public bool IsExp
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsExp);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsExp, value);
            }
        }
        public bool IsImp
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsImp);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsImp, value);
            }
        }
        public bool IsShowSum
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsShowSum);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsShowSum, value);
            }
        }
        public bool IsShowIdx
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsShowIdx);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsShowIdx, value);
            }
        }
        public bool IsReadonly_del
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsReadonly);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsReadonly, value);
            }
        }
        public bool IsShowTitle
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsShowTitle);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsShowTitle, value);
            }
        }
        /// <summary>
        /// �Ƿ��Ǻ�����������
        /// </summary>
        public bool IsHLDtl
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsHLDtl);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsHLDtl, value);
            }
        }
        public int _IsReadonly = 2;
        public bool IsReadonly
        {
            get
            {
                if (_IsReadonly != 2)
                {
                    if (_IsReadonly == 1)
                        return true;
                    else
                        return false;
                }

                if (this.IsDelete || this.IsInsert || this.IsUpdate)
                {
                    _IsReadonly = 0;
                    return false;
                }
                _IsReadonly = 1;
                return true;
            }
        }
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsDelete, value);
            }
        }
        public bool IsInsert
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsInsert);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsInsert, value);
            }
        }
        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public bool IsView
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsView);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsView, value);
            }
        }
        public bool IsUpdate
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsUpdate);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsUpdate, value);
            }
        }
        /// <summary>
        /// �Ƿ����ö฽��
        /// </summary>
        public bool IsEnableAthM
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsEnableAthM);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsEnableAthM, value);
            }
        }
        /// <summary>
        /// �Ƿ����÷����ֶ�
        /// </summary>
        public bool IsEnableGroupField
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsEnableGroupField);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsEnableGroupField, value);
            }
        }
        /// <summary>
        /// �Ƿ������������
        /// </summary>
        public bool IsEnablePass
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsEnablePass);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsEnablePass, value);
            }
        }
        public bool IsCopyNDData
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsCopyNDData);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsCopyNDData, value);
            }
        }
        /// <summary>
        /// �Ƿ�����һ�Զ�
        /// </summary>
        public bool IsEnableM2M
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsEnableM2M);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsEnableM2M, value);
            }
        }
        /// <summary>
        /// �Ƿ�����һ�Զ��
        /// </summary>
        public bool IsEnableM2MM
        {
            get
            {
                return this.GetValBooleanByKey(FrmRptAttr.IsEnableM2MM);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.IsEnableM2MM, value);
            }
        }

        public bool IsUse = false;
        /// <summary>
        /// �Ƿ�����Ա��Ȩ��
        /// </summary>
        public DtlOpenType DtlOpenType
        {
            get
            {
                return (DtlOpenType)this.GetValIntByKey(FrmRptAttr.DtlOpenType);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.DtlOpenType, (int)value);
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string GroupField
        {
            get
            {
                return this.GetValStrByKey(FrmRptAttr.GroupField);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.GroupField, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmRptAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.FK_MapData, value);
            }
        }
        public int RowsOfList
        {
            get
            {
                return this.GetValIntByKey(FrmRptAttr.RowsOfList);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.RowsOfList, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(FrmRptAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.RowIdx, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(FrmRptAttr.GroupID);
            }
            set
            {
                this.SetValByKey(FrmRptAttr.GroupID, value);
            }
        }
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(FrmRptAttr.PTable);
                if (s == "" || s == null)
                {
                    s = this.No;
                    if (s.Substring(0, 1) == "0")
                    {
                        return "T" + this.No;
                    }
                    else
                        return s;
                }
                else
                {
                    if (s.Substring(0, 1) == "0")
                    {
                        return "T" + this.No;
                    }
                    else
                        return s;
                }
            }
            set
            {
                this.SetValByKey(FrmRptAttr.PTable, value);
            }
        }
        /// <summary>
        /// ���ͷ
        /// </summary>
        public string MTR
        {
            get
            {
                string s= this.GetValStrByKey(FrmRptAttr.MTR);
                s = s.Replace("��","<");
                s = s.Replace( "��",">");
                s = s.Replace("��","'");
                return s;
            }
            set
            {
                string s = value;
                s = s.Replace("<","��");
                s = s.Replace(">", "��");
                s = s.Replace("'", "��");
                this.SetValByKey(FrmRptAttr.MTR, value);
            }
        }
        #endregion

        #region ���췽��
        public Map GenerMap()
        {
            bool isdebug = SystemConfig.IsDebug;

            if (isdebug == false)
            {
                Map m = BP.DA.Cash.GetMap(this.No);
                if (m != null)
                    return m;
            }

            MapAttrs mapAttrs = this.MapAttrs;
            Map map = new Map(this.PTable);
            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            BP.DA.Cash.SetMap(this.No, map);
            return map;
        }
        public GEDtl HisGEDtl
        {
            get
            {
                GEDtl dtl = new GEDtl(this.No);
                return dtl;
            }
        }
        public GEEntity GenerGEMainEntity(string mainPK)
        {
            GEEntity en = new GEEntity(this.FK_MapData, mainPK);
            return en;
        }
        /// <summary>
        /// γ�ȱ���
        /// </summary>
        public FrmRpt()
        {
        }
        public FrmRpt(string mypk)
        {
            this.No = mypk;
            this._IsReadonly = 2;
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
                Map map = new Map("Sys_FrmRpt");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "γ�ȱ���";
                map.EnType = EnType.Sys;
               
                map.AddTBStringPK(FrmRptAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(FrmRptAttr.Name, null, "����", true, false, 1, 50, 20);
                map.AddTBString(FrmRptAttr.FK_MapData, null, "����", true, false, 0, 30, 20);
                map.AddTBString(FrmRptAttr.PTable, null, "�����", true, false, 0, 30, 20);

                map.AddTBString(FrmRptAttr.SQLOfColumn, null, "�е�����Դ", true, false, 0, 300, 20);
                map.AddTBString(FrmRptAttr.SQLOfRow, null, "������Դ", true, false, 0, 300, 20);

                map.AddTBInt(FrmRptAttr.RowIdx, 99, "λ��", false, false);
                map.AddTBInt(FrmRptAttr.GroupID, 0, "GroupID", false, false);

                map.AddBoolean(FrmRptAttr.IsShowSum, true, "IsShowSum", false, false);
                map.AddBoolean(FrmRptAttr.IsShowIdx, true, "IsShowIdx", false, false);
                map.AddBoolean(FrmRptAttr.IsCopyNDData, true, "IsCopyNDData", false, false);
                map.AddBoolean(FrmRptAttr.IsHLDtl, false, "�Ƿ��Ǻ�������", false, false);

                map.AddBoolean(FrmRptAttr.IsReadonly, false, "IsReadonly", false, false);
                map.AddBoolean(FrmRptAttr.IsShowTitle, true, "IsShowTitle", false, false);
                map.AddBoolean(FrmRptAttr.IsView, true, "�Ƿ�ɼ�", false, false);

                map.AddBoolean(FrmRptAttr.IsExp, true, "IsExp", false, false);
                map.AddBoolean(FrmRptAttr.IsImp, true, "IsImp", false, false);

                map.AddBoolean(FrmRptAttr.IsInsert, true, "IsInsert", false, false);
                map.AddBoolean(FrmRptAttr.IsDelete, true, "IsDelete", false, false);
                map.AddBoolean(FrmRptAttr.IsUpdate, true, "IsUpdate", false, false);

                map.AddBoolean(FrmRptAttr.IsEnablePass, false, "�Ƿ�����ͨ����˹���?", false, false);
                map.AddBoolean(FrmRptAttr.IsEnableAthM, false, "�Ƿ����ö฽��", false, false);

                map.AddBoolean(FrmRptAttr.IsEnableM2M, false, "�Ƿ�����M2M", false, false);
                map.AddBoolean(FrmRptAttr.IsEnableM2MM, false, "�Ƿ�����M2M", false, false);

                map.AddDDLSysEnum(FrmRptAttr.WhenOverSize, 0, "WhenOverSize", true, true,
                 FrmRptAttr.WhenOverSize, "@0=������@1=����˳����@2=��ҳ��ʾ");

                map.AddDDLSysEnum(FrmRptAttr.DtlOpenType, 1, "���ݿ�������", true, true,
                    FrmRptAttr.DtlOpenType, "@0=����Ա@1=����ID@2=����ID");

                map.AddDDLSysEnum(FrmRptAttr.DtlShowModel, 0, "��ʾ��ʽ", true, true,
               FrmRptAttr.DtlShowModel, "@0=���@1=��Ƭ");

                map.AddTBFloat(FrmRptAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmRptAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmRptAttr.H, 150, "H", true, false);
                map.AddTBFloat(FrmRptAttr.W, 200, "W", false, false);

                map.AddTBFloat(FrmRptAttr.FrmW, 900, "FrmW", true, true);
                map.AddTBFloat(FrmRptAttr.FrmH, 1200, "FrmH", true, true);

                //MTR ���ͷ��.
                map.AddTBString(FrmRptAttr.MTR, null, "���ͷ��", true, false, 0, 3000, 20);
                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);


                this._enMap = map;
                return this._enMap;
            }
        }
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.X);
            }
        }
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.Y);
            }
        }
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.W);
            }
        }
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.H);
            }
        }
        public float FrmW
        {
            get
            {
                return this.GetValFloatByKey(FrmRptAttr.FrmW);
            }
        }
        public float FrmH
        {
            get
            {
                return this.GetValFloatByKey(FrmRptAttr.FrmH);
            }
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="fk_val"></param>
        /// <returns></returns>
        public int GetCountByFK(int workID)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE WorkID=" + workID);
        }

        public int GetCountByFK(string field, string val)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE " + field + "='" + val + "'");
        }
        public int GetCountByFK(string field, Int64 val)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE " + field + "=" + val);
        }
        public int GetCountByFK(string f1, Int64 val1, string f2, string val2)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(OID) from " + this.PTable + " WHERE " + f1 + "=" + val1 + " AND " + f2 + "='" + val2 + "'");
        }
        #endregion

        public void IntMapAttrs()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = this.No;
            if (md.RetrieveFromDBSources() == 0)
            {
                md.Name = this.Name;
                md.Insert();
            }

            MapAttrs attrs = new MapAttrs(this.No);
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (attrs.Contains(MapAttrAttr.KeyOfEn, "OID") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "OID";
                attr.Name = "����";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "RefPK") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "RefPK";
                attr.Name = "����ID";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }


            if (attrs.Contains(MapAttrAttr.KeyOfEn, "FID") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "RDT") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.UnDel;

                attr.KeyOfEn = "RDT";
                attr.Name = "��¼ʱ��";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Tag = "1";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "Rec") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "Rec";
                attr.Name = "��¼��";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 20;
                attr.MinLen = 0;
                attr.DefVal = "@WebUser.No";
                attr.Tag = "@WebUser.No";
                attr.Insert();
            }
        }
        private void InitExtMembers()
        {
            /* ��������˶฽��*/
            if (this.IsEnableAthM)
            {
                BP.Sys.FrmAttachment athDesc = new BP.Sys.FrmAttachment();
                athDesc.MyPK = this.No + "_AthM";
                if (athDesc.RetrieveFromDBSources() == 0)
                {
                    athDesc.FK_MapData = this.No;
                    athDesc.NoOfObj = "AthM";
                    athDesc.Name = this.Name;
                    athDesc.Insert();
                }
            }

            if (this.IsEnableM2M)
            {
                MapM2M m2m = new MapM2M();
                m2m.MyPK = this.No + "_M2M";
                m2m.Name = "M2M";
                m2m.NoOfObj = "M2M";
                m2m.FK_MapData = this.No;
                if (m2m.RetrieveFromDBSources() == 0)
                {
                    m2m.FK_MapData = this.No;
                    m2m.NoOfObj = "M2M";
                    m2m.Insert();
                }
            }

            if (this.IsEnableM2MM)
            {
                MapM2M m2m = new MapM2M();
                m2m.MyPK = this.No + "_M2MM";
                m2m.Name = "M2MM";
                m2m.NoOfObj = "M2MM";
                m2m.FK_MapData = this.No;
                if (m2m.RetrieveFromDBSources() == 0)
                {
                    m2m.FK_MapData = this.No;
                    m2m.NoOfObj = "M2MM";
                    m2m.Insert();
                }
            }
        }
        protected override bool beforeInsert()
        {
            this.InitExtMembers();
            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            if (this.IsEnablePass)
            {
                /*�ж��Ƿ���IsPass �ֶΡ�*/
                MapAttrs attrs = new MapAttrs(this.No);
                if (attrs.Contains(MapAttrAttr.KeyOfEn, "IsPass") == false)
                    throw new Exception("�������˴ӱ�(" + this.Name + ")���������ѡ����Ǹôӱ���ûIsPass�ֶΣ���ο������ĵ���");
            }
            return base.beforeUpdateInsertAction();
        }
        protected override bool beforeUpdate()
        {
            MapAttrs attrs = new MapAttrs(this.No);
            bool isHaveEnable = false;
            foreach (MapAttr attr in attrs)
            {
                if (attr.UIIsEnable && attr.UIContralType == UIContralType.TB)
                    isHaveEnable = true;
            }
            this.InitExtMembers();
            return base.beforeUpdate();
        }
        protected override bool beforeDelete()
        {
            string sql = "";
            sql += "@DELETE FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmLab WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmLink WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmImgAth WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmRB WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmAttachment WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapFrame WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapExt WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapAttr WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapData WHERE No='" + this.No + "'";
            sql += "@DELETE FROM Sys_GroupField WHERE EnName='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapM2M WHERE FK_MapData='" + this.No + "'";
            DBAccess.RunSQLs(sql);
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
    }
    /// <summary>
    /// γ�ȱ���s
    /// </summary>
    public class FrmRpts : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// γ�ȱ���s
        /// </summary>
        public FrmRpts()
        {
        }
        /// <summary>
        /// γ�ȱ���s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmRpts(string fk_mapdata)
        {
            this.Retrieve(FrmRptAttr.FK_MapData, fk_mapdata, FrmRptAttr.No);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmRpt();
            }
        }
        #endregion
    }
}
