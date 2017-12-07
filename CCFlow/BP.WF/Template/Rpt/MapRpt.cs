using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Port;
using BP.En;
using BP.WF;
using BP.Sys;

namespace BP.WF.Rpt
{
    /// <summary>
    /// ����鿴Ȩ�޿��Ʒ�ʽ
    /// </summary>
    public enum RightViewWay
    {
        /// <summary>
        /// �κ��˶����Բ鿴
        /// </summary>
        AnyOne,
        /// <summary>
        /// ������֯�ṹȨ��
        /// </summary>
        ByPort,
        /// <summary>
        /// ����SQL����
        /// </summary>
        BySQL
    }
    /// <summary>
    /// ��������Ȩ�޿��Ʒ�ʽ
    /// </summary>
    public enum RightDeptWay
    {
        /// <summary>
        /// ���в��ŵ�����.
        /// </summary>
        All,
        /// <summary>
        /// �����ŵ�����.
        /// </summary>
        SelfDept,
        /// <summary>
        /// ���������Ӳ��ŵ�����.
        /// </summary>
        SelfDeptAndSubDepts,
        /// <summary>
        /// ָ�����ŵ�����.
        /// </summary>
        SpecDepts
    }

    /// <summary>
    /// �������
    /// </summary>
    public class MapRptAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��ѯ�������
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// �̳еı���FK_MapData
        /// </summary>
        public const string ParentMapData = "ParentMapData";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";

        #region Ȩ�޿��� 2014-12-18
        /// <summary>
        /// ����鿴Ȩ�޿��Ʒ�ʽ
        /// </summary>
        public const string RightViewWay = "RightViewWay";
        /// <summary>
        /// ���ݴ洢
        /// </summary>
        public const string RightViewTag = "RightViewTag";
        /// <summary>
        /// ��������Ȩ�޿��Ʒ�ʽ
        /// </summary>
        public const string RightDeptWay = "RightDeptWay";
        /// <summary>
        /// ���ݴ洢
        /// </summary>
        public const string RightDeptTag = "RightDeptTag";
        #endregion Ȩ�޿���
    }
    /// <summary>
    /// �������
    /// </summary>
    public class MapRpt : EntityNoName
    {
        #region ����Ȩ�޿��Ʒ�ʽ
        /// <summary>
        /// ����鿴Ȩ�޿���.
        /// </summary>
        public RightViewWay RightViewWay
        {
            get
            {
                return (RightViewWay)this.GetValIntByKey(MapRptAttr.RightViewWay);
            }
            set
            {
                this.SetValByKey(MapRptAttr.RightViewWay, (int)value);
            }
        }
        /// <summary>
        /// ����鿴Ȩ�޿���-����
        /// </summary>
        public string RightViewTag
        {
            get
            {
                return this.GetValStringByKey(MapRptAttr.RightViewTag);
            }
            set
            {
                this.SetValByKey(MapRptAttr.RightViewTag, value);
            }
        }
        /// <summary>
        /// ������Ȩ�޿���.
        /// </summary>
        public RightDeptWay RightDeptWay
        {
            get
            {
                return (RightDeptWay)this.GetValIntByKey(MapRptAttr.RightDeptWay);
            }
            set
            {
                this.SetValByKey(MapRptAttr.RightDeptWay, (int)value);
            }
        }
        /// <summary>
        /// ������Ȩ�޿���-����
        /// </summary>
        public string RightDeptTag
        {
            get
            {
                return this.GetValStringByKey(MapRptAttr.RightDeptTag);
            }
            set
            {
                this.SetValByKey(MapRptAttr.RightDeptTag, value);
            }
        }
        #endregion ����Ȩ�޿��Ʒ�ʽ

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
        public GroupFields GroupFields
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
        public MapDtls MapDtls
        {
            get
            {
                MapDtls obj = this.GetRefObject("MapDtls") as MapDtls;
                if (obj == null)
                {
                    obj = new MapDtls(this.No);
                    this.SetRefObject("MapDtls", obj);
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
        public string FK_Flow
        {
            get
            {
                string s = this.GetValStrByKey(MapRptAttr.FK_Flow);
                if (s == "" || s == null)
                {
                    s = this.ParentMapData;
                    if (string.IsNullOrEmpty(s))
                        throw new Exception("@���󱨱�" + this.No + "," + this.Name + " , �ֶ�ParentMapData:" + this.ParentMapData + " ��Ӧ��Ϊ��.");
                    s = s.Replace("ND", "");
                    s = s.Replace("Rpt", "");
                    s = s.PadLeft(3, '0');
                    return s;
                }
                return s;
            }
            set
            {
                this.SetValByKey(MapRptAttr.FK_Flow, value);
            }
        }
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapRptAttr.PTable);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(MapRptAttr.PTable, value);
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStrByKey(MapRptAttr.Note);
            }
            set
            {
                this.SetValByKey(MapRptAttr.Note, value);
            }
        }
        public string ParentMapData
        {
            get
            {
                return this.GetValStrByKey(MapRptAttr.ParentMapData);
            }
            set
            {
                this.SetValByKey(MapRptAttr.ParentMapData, value);
            }
        }
        
       
        public Entities _HisEns = null;
        public new Entities HisEns
        {
            get
            {
                if (_HisEns == null)
                {
                    _HisEns = BP.En.ClassFactory.GetEns(this.No);
                }
                return _HisEns;
            }
        }
        public Entity HisEn
        {
            get
            {
                return this.HisEns.GetNewEntity;
            }
        }
        #endregion

        #region ���췽��
        private GEEntity _HisEn = null;
        public GEEntity HisGEEn
        {
            get
            {
                if (this._HisEn == null)
                    _HisEn = new GEEntity(this.No);
                return _HisEn;
            }
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public GEEntity GenerGEEntityByDataSet(DataSet ds)
        {
            // New ����ʵ��.
            GEEntity en = this.HisGEEn;

            // ����table.
            DataTable dt = ds.Tables[this.No];

            //װ������.
            en.Row.LoadDataTable(dt, dt.Rows[0]);

            // dtls.
            MapDtls dtls = this.MapDtls;
            foreach (MapDtl item in dtls)
            {
                DataTable dtDtls = ds.Tables[item.No];
                GEDtls dtlsEn = new GEDtls(item.No);
                foreach (DataRow dr in dtDtls.Rows)
                {
                    // ��������Entity data.
                    GEDtl dtl = (GEDtl)dtlsEn.GetNewEntity;
                    dtl.Row.LoadDataTable(dtDtls, dr);

                    //�����������.
                    dtlsEn.AddEntity(dtl);
                }

                //���뵽���ļ�����.
                en.Dtls.Add(dtDtls);
            }
            return en;
        }
        /// <summary>
        /// �������
        /// </summary>
        public MapRpt()
        {
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="no">ӳ����</param>
        public MapRpt(string no)
        {
            this.No = no;
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
                Map map = new Map("Sys_MapData");
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�������";
                map.EnType = EnType.Sys;
                map.CodeStruct = "4";

                map.AddTBStringPK(MapRptAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapRptAttr.Name, null, "����", true, false, 0, 500, 20);
           //     map.AddTBString(MapRptAttr.SearchKeys, null, "��ѯ��", true, false, 0, 500, 20);
                map.AddTBString(MapRptAttr.PTable, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapRptAttr.FK_Flow, null, "���̱��", true, false, 0, 3, 3);

                //Tag
             //   map.AddTBString(MapRptAttr.Tag, null, "Tag", true, false, 0, 500, 20);

                //ʱ���ѯ:���ڱ����ѯ.
              //  map.AddTBInt(MapRptAttr.IsSearchKey, 0, "�Ƿ���Ҫ�ؼ��ֲ�ѯ", true, false);
             //   map.AddTBInt(MapRptAttr.DTSearchWay, 0, "ʱ���ѯ��ʽ", true, false);
             //   map.AddTBString(MapRptAttr.DTSearchKey, null, "ʱ���ѯ�ֶ�", true, false, 0, 200, 20);
                map.AddTBString(MapRptAttr.Note, null, "��ע", true, false, 0, 500, 20);
            
                map.AddTBString(MapRptAttr.ParentMapData, null, "ParentMapData", true, false, 0, 128, 20);


                #region Ȩ�޿���. 2014-12-18
                map.AddTBInt(MapRptAttr.RightViewWay, 0, "����鿴Ȩ�޿��Ʒ�ʽ", true, false);
                map.AddTBString(MapRptAttr.RightViewTag, null, "����鿴Ȩ�޿���Tag", true, false, 0, 4000, 20);

                map.AddTBInt(MapRptAttr.RightDeptWay, 0, "�������ݲ鿴���Ʒ�ʽ", true, false);
                map.AddTBString(MapRptAttr.RightDeptTag, null, "�������ݲ鿴����Tag", true, false, 0, 4000, 20);

                map.AttrsOfOneVSM.Add(new RptStations(), new Stations(), RptStationAttr.FK_Rpt, RptStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, "��λȨ��");
                map.AttrsOfOneVSM.Add(new RptDepts(), new Depts(), RptDeptAttr.FK_Rpt, RptDeptAttr.FK_Dept,
                    DeptAttr.Name, DeptAttr.No, "����Ȩ��");
                map.AttrsOfOneVSM.Add(new RptEmps(), new Emps(), RptEmpAttr.FK_Rpt, RptEmpAttr.FK_Emp,
                 DeptAttr.Name, DeptAttr.No, "��ԱȨ��");
                #endregion Ȩ�޿���.


                this._enMap = map;
                return this._enMap;
            }
        }

         
        #endregion

        public MapAttrs HisShowColsAttrs
        {
            get
            {
                MapAttrs mattrs = new MapAttrs(this.No);
                return mattrs;
            }
        }
        protected override bool beforeInsert()
        {
            this.ResetIt();
            return base.beforeInsert();
        }

        public void ResetIt()
        {
            MapData md = new MapData(this.No);
            md.RptIsSearchKey = true;
            md.RptDTSearchWay = DTSearchWay.None;
            md.RptDTSearchKey = "";
            md.RptSearchKeys = "*FK_Dept*WFSta*FK_NY*";

            MapData pmd = new MapData(this.ParentMapData);
            this.PTable = pmd.PTable;
            this.Update();

            string keys = "'OID','FK_Dept','FlowStarter','WFState','Title','FlowStartRDT','FlowEmps','FlowDaySpan','FlowEnder','FlowEnderRDT','FK_NY','FlowEndNode','WFSta'";
            MapAttrs attrs = new MapAttrs(this.ParentMapData);
            attrs.Delete(MapAttrAttr.FK_MapData, this.No); // ɾ���Ѿ��е��ֶΡ�
            foreach (MapAttr attr in attrs)
            {
                if (keys.Contains("'" + attr.KeyOfEn + "'") == false)
                    continue;
                attr.FK_MapData = this.No;
                attr.Insert();
            }
        }
        
        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs();
            attrs.Delete(MapAttrAttr.FK_MapData, this.No);
            return base.beforeDelete();
        }
    }
    /// <summary>
    /// �������s
    /// </summary>
    public class MapRpts : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// �������s
        /// </summary>
        public MapRpts()
        {
        }
        /// <summary>
        /// �������s
        /// </summary>
        /// <param name="fk_flow">���̱��</param>
        public MapRpts(string fk_flow)
        {
            string fk_Mapdata = "ND" + int.Parse(fk_flow) + "Rpt";
            int i = this.Retrieve(MapRptAttr.ParentMapData, fk_Mapdata);
            if (i == 0)
            {
                MapData mapData = new MapData(fk_Mapdata);
                mapData.No = "ND" + int.Parse(fk_flow) + "MyRpt";
                mapData.Name = "�ҵ�����";
                mapData.Note = "ϵͳ�Զ�����.";
                mapData.Insert();

                MapRpt rpt = new MapRpt(mapData.No);
                rpt.ParentMapData = fk_Mapdata;
                rpt.ResetIt();


                rpt.Update();
            }
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapRpt();
            }
        }
        #endregion
    }
}
