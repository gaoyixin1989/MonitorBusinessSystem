using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using System.Collections.Generic;

namespace BP.Sys
{
    /// <summary>
    /// �����ڲ�ѯ��ʽ
    /// </summary>
    public enum DTSearchWay
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// ������
        /// </summary>
        ByDate,
        /// <summary>
        /// ������ʱ��
        /// </summary>
        ByDateTime
    }
    /// <summary>
    /// Ӧ������
    /// </summary>
    public enum AppType
    {
        /// <summary>
        /// ���̱�
        /// </summary>
        Application = 0,
        /// <summary>
        /// �ڵ��
        /// </summary>
        Node = 1
    }
    public enum FrmFrom
    {
        Flow,
        Node,
        Dtl
    }
    /// <summary>
    /// ������
    /// </summary>
    public enum FrmType
    {
        /// <summary>
        /// ���ɱ�
        /// </summary>
        FreeFrm = 0,
        /// <summary>
        /// ɵ�ϱ�
        /// </summary>
        Column4Frm = 1,
        /// <summary>
        /// silverlight
        /// </summary>
        SLFrm = 2,
        /// <summary>
        /// URL��(�Զ���)
        /// </summary>
        Url = 3,
        /// <summary>
        /// Word���ͱ�
        /// </summary>
        WordFrm = 4,
        /// <summary>
        /// Excel���ͱ�
        /// </summary>
        ExcelFrm = 5
    }
    /// <summary>
    /// ӳ�����
    /// </summary>
    public class MapDataAttr : EntityNoNameAttr
    {
        public const string PTable = "PTable";
        public const string Dtls = "Dtls";
        public const string EnPK = "EnPK";
        public const string FrmW = "FrmW";
        public const string FrmH = "FrmH";
        /// <summary>
        /// �����(��ɵ�ϱ���Ч)
        /// </summary>
        public const string TableCol = "TableCol";
        /// <summary>
        /// �����
        /// </summary>
        public const string TableWidth = "TableWidth";
        /// <summary>
        /// ��Դ
        /// </summary>
        public const string FrmFrom = "FrmFrom";
        /// <summary>
        /// DBURL
        /// </summary>
        public const string DBURL = "DBURL";
        /// <summary>
        /// �����
        /// </summary>
        public const string Designer = "Designer";
        /// <summary>
        /// ����ߵ�λ
        /// </summary>
        public const string DesignerUnit = "DesignerUnit";
        /// <summary>
        /// �������ϵ��ʽ
        /// </summary>
        public const string DesignerContact = "DesignerContact";
        /// <summary>
        /// �����
        /// </summary>
        public const string FK_FrmSort = "FK_FrmSort";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FormTree = "FK_FormTree";
        /// <summary>
        /// Ӧ������
        /// </summary>
        public const string AppType = "AppType";
        /// <summary>
        /// ������
        /// </summary>
        public const string FrmType = "FrmType";
        /// <summary>
        /// Url(�����Զ������Ч)
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// Tag
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// �汾��
        /// </summary>
        public const string Ver = "Ver";

        #region ��������(�����ķ�ʽ�洢).
        /// <summary>
        /// �Ƿ�ؼ��ֲ�ѯ
        /// </summary>
        public const string RptIsSearchKey = "RptIsSearchKey";
        /// <summary>
        /// ʱ��β�ѯ��ʽ
        /// </summary>
        public const string RptDTSearchWay = "RptDTSearchWay";
        /// <summary>
        /// ʱ���ֶ�
        /// </summary>
        public const string RptDTSearchKey = "RptDTSearchKey";
        /// <summary>
        /// ��ѯ���ö���ֶ�
        /// </summary>
        public const string RptSearchKeys = "RptSearchKeys";
        #endregion ��������(�����ķ�ʽ�洢).

        #region �����������ԣ������洢.
        /// <summary>
        /// ����ߵ�ֵ
        /// </summary>
        public const string MaxLeft = "MaxLeft";
        /// <summary>
        /// ���ұߵ�ֵ
        /// </summary>
        public const string MaxRight = "MaxRight";
        /// <summary>
        /// ��ͷ����ֵ
        /// </summary>
        public const string MaxTop = "MaxTop";
        /// <summary>
        /// ��ײ���ֵ
        /// </summary>
        public const string MaxEnd = "MaxEnd";
        #endregion �����������ԣ������洢.


        #region weboffice���ԡ�
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public const string IsRowLock = "IsRowLock";
        /// <summary>
        /// �Ƿ�����weboffice
        /// </summary>
        public const string IsWoEnableWF = "IsWoEnableWF";
        /// <summary>
        /// �Ƿ����ñ���
        /// </summary>
        public const string IsWoEnableSave = "IsWoEnableSave";
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public const string IsWoEnableReadonly = "IsWoEnableReadonly";
        /// <summary>
        /// �Ƿ������޶�
        /// </summary>
        public const string IsWoEnableRevise = "IsWoEnableRevise";
        /// <summary>
        /// �Ƿ�鿴�û�����
        /// </summary>
        public const string IsWoEnableViewKeepMark = "IsWoEnableViewKeepMark";
        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        public const string IsWoEnablePrint = "IsWoEnablePrint";
        /// <summary>
        /// �Ƿ�����ǩ��
        /// </summary>
        public const string IsWoEnableSeal = "IsWoEnableSeal";
        /// <summary>
        /// �Ƿ������׺�
        /// </summary>
        public const string IsWoEnableOver = "IsWoEnableOver";
        /// <summary>
        /// �Ƿ����ù���ģ��
        /// </summary>
        public const string IsWoEnableTemplete = "IsWoEnableTemplete";
        /// <summary>
        /// �Ƿ��Զ�д�������Ϣ
        /// </summary>
        public const string IsWoEnableCheck = "IsWoEnableCheck";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string IsWoEnableInsertFlow = "IsWoEnableInsertFlow";
        /// <summary>
        /// �Ƿ������յ�
        /// </summary>
        public const string IsWoEnableInsertFengXian = "IsWoEnableInsertFengXian";
        /// <summary>
        /// �Ƿ���������ģʽ
        /// </summary>
        public const string IsWoEnableMarks = "IsWoEnableMarks";
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public const string IsWoEnableDown = "IsWoEnableDown";
        #endregion weboffice���ԡ�
    }
    /// <summary>
    /// ӳ�����
    /// </summary>
    public class MapData : EntityNoName
    {
        //public new string No
        //{
        //    get
        //    {
        //        return this.GetValStringByKey(MapDataAttr.No);
        //    }
        //    set
        //    {
        //        if (value.ToLower().Contains("Dtl") == true)
        //            throw new Exception("@���ܸ�");
        //    }
        //}

        #region weboffice�ĵ�����(��������)
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public bool IsRowLock
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsRowLock, false);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsRowLock, value);
            }
        }

        /// <summary>
        /// �Ƿ����ô�ӡ
        /// </summary>
        public bool IsWoEnablePrint
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnablePrint);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnablePrint, value);
            }
        }
        /// <summary>
        /// �Ƿ�����ֻ��
        /// </summary>
        public bool IsWoEnableReadonly
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableReadonly);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableReadonly, value);
            }
        }
        /// <summary>
        /// �Ƿ������޶�
        /// </summary>
        public bool IsWoEnableRevise
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableRevise);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableRevise, value);
            }
        }
        /// <summary>
        /// �Ƿ����ñ���
        /// </summary>
        public bool IsWoEnableSave
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableSave);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableSave, value);
            }
        }
        /// <summary>
        /// �Ƿ�鿴�û�����
        /// </summary>
        public bool IsWoEnableViewKeepMark
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableViewKeepMark);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableViewKeepMark, value);
            }
        }
        /// <summary>
        /// �Ƿ�����weboffice
        /// </summary>
        public bool IsWoEnableWF
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableWF);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableWF, value);
            }
        }

        /// <summary>
        /// �Ƿ������׺�
        /// </summary>
        public bool IsWoEnableOver
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableOver);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableOver, value);
            }
        }

        /// <summary>
        /// �Ƿ�����ǩ��
        /// </summary>
        public bool IsWoEnableSeal
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableSeal);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableSeal, value);
            }
        }

        /// <summary>
        /// �Ƿ����ù���ģ��
        /// </summary>
        public bool IsWoEnableTemplete
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableTemplete);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableTemplete, value);
            }
        }

        /// <summary>
        /// �Ƿ��¼�ڵ���Ϣ
        /// </summary>
        public bool IsWoEnableCheck
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableCheck);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableCheck, value);
            }
        }

        /// <summary>
        /// �Ƿ��������ͼ
        /// </summary>
        public bool IsWoEnableInsertFlow
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableInsertFlow);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableInsertFlow, value);
            }
        }

        /// <summary>
        /// �Ƿ������յ�
        /// </summary>
        public bool IsWoEnableInsertFengXian
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableInsertFengXian);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableInsertFengXian, value);
            }
        }

        /// <summary>
        /// �Ƿ���������ģʽ
        /// </summary>
        public bool IsWoEnableMarks
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableMarks);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableMarks, value);
            }
        }

        /// <summary>
        /// �Ƿ������յ�
        /// </summary>
        public bool IsWoEnableDown
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsWoEnableDown);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsWoEnableDown, value);
            }
        }

        #endregion weboffice�ĵ�����

        #region �Զ���������.
        public float MaxLeft
        {
            get
            {
                return this.GetParaFloat(MapDataAttr.MaxLeft);
            }
            set
            {
                this.SetPara(MapDataAttr.MaxLeft, value);
            }
        }
        public float MaxRight
        {
            get
            {
                return this.GetParaFloat(MapDataAttr.MaxRight);
            }
            set
            {
                this.SetPara(MapDataAttr.MaxRight, value);
            }
        }
        public float MaxTop
        {
            get
            {
                return this.GetParaFloat(MapDataAttr.MaxTop);
            }
            set
            {
                this.SetPara(MapDataAttr.MaxTop, value);
            }
        }
        public float MaxEnd
        {
            get
            {
                return this.GetParaFloat(MapDataAttr.MaxEnd);
            }
            set
            {
                this.SetPara(MapDataAttr.MaxEnd, value);
            }
        }
        #endregion �Զ���������.

        #region ��������(������ʽ�洢).
        /// <summary>
        /// �Ƿ�ؼ��ֲ�ѯ
        /// </summary>
        public bool RptIsSearchKey
        {
            get
            {
                return this.GetParaBoolen(MapDataAttr.RptIsSearchKey, true);
            }
            set
            {
                this.SetPara(MapDataAttr.RptIsSearchKey, value);
            }
        }
        /// <summary>
        /// ʱ��β�ѯ��ʽ
        /// </summary>
        public DTSearchWay RptDTSearchWay
        {
            get
            {
                return (DTSearchWay)this.GetParaInt(MapDataAttr.RptDTSearchWay);
            }
            set
            {
                this.SetPara(MapDataAttr.RptDTSearchWay, (int)value);
            }
        }
        /// <summary>
        /// ʱ���ֶ�
        /// </summary>
        public string RptDTSearchKey
        {
            get
            {
                return this.GetParaString(MapDataAttr.RptDTSearchKey);
            }
            set
            {
                this.SetPara(MapDataAttr.RptDTSearchKey, value);
            }
        }
        /// <summary>
        /// ��ѯ���ö���ֶ�
        /// </summary>
        public string RptSearchKeys
        {
            get
            {
                return this.GetParaString(MapDataAttr.RptSearchKeys,"*");
            }
            set
            {
                this.SetPara(MapDataAttr.RptSearchKeys, value);
            }
        }
        #endregion ��������(������ʽ�洢).

        #region �������
        public string Ver
        {
            get
            {
                return this.GetValStringByKey(MapDataAttr.Ver);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Ver, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(MapDataAttr.Idx);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Idx, value);
            }
        }
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
        /// ����
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

        public static Boolean IsEditDtlModel
        {
            get
            {
                string s = BP.Web.WebUser.GetSessionByKey("IsEditDtlModel", "0");
                if (s == "0")
                    return false;
                else
                    return true;
            }
            set
            {
                BP.Web.WebUser.SetSessionByKey("IsEditDtlModel", "1");
            }
        }


        #region ����
        /// <summary>
        /// �����
        /// </summary>
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.PTable);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.PTable, value);
            }
        }
        /// <summary>
        /// URL
        /// </summary>
        public string Url
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Url);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Url, value);
            }
        }
        public DBUrlType HisDBUrl
        {
            get
            {
                return DBUrlType.AppCenterDSN;
                // return (DBUrlType)this.GetValIntByKey(MapDataAttr.DBURL);
            }
        }
        public int HisFrmTypeInt
        {
            get
            {
                return this.GetValIntByKey(MapDataAttr.FrmType);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmType, value);
            }
        }
        public FrmType HisFrmType
        {
            get
            {
                return (FrmType)this.GetValIntByKey(MapDataAttr.FrmType);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmType, (int)value);
            }
        }
        public AppType HisAppType
        {
            get
            {
                return (AppType)this.GetValIntByKey(MapDataAttr.AppType);
            }
            set
            {
                this.SetValByKey(MapDataAttr.AppType, (int)value);
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Note);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Note, value);
            }
        }
        /// <summary>
        /// �Ƿ���CA.
        /// </summary>
        public bool IsHaveCA
        {
            get
            {
                return this.GetParaBoolen("IsHaveCA", false);

            }
            set
            {
                this.SetPara("IsHaveCA", value);
            }
        }
        /// <summary>
        /// ��𣬿���Ϊ��.
        /// </summary>
        public string FK_FrmSort
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.FK_FrmSort);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FK_FrmSort, value);
            }
        }
        /// <summary>
        /// ��𣬿���Ϊ��.
        /// </summary>
        public string FK_FormTree
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.FK_FormTree);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FK_FormTree, value);
            }
        }
        /// <summary>
        /// �ӱ���.
        /// </summary>
        public string Dtls
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Dtls);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Dtls, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EnPK
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.EnPK);
                if (string.IsNullOrEmpty(s))
                    return "OID";
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.EnPK, value);
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
        public float FrmW
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmW);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmW, value);
            }
        }
        ///// <summary>
        ///// �����Ʒ���
        ///// </summary>
        //public string Slns
        //{
        //    get
        //    {
        //        return this.GetValStringByKey(MapDataAttr.Slns);
        //    }
        //    set
        //    {
        //        this.SetValByKey(MapDataAttr.Slns, value);
        //    }
        //}
        public float FrmH
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmH);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmH, value);
            }
        }
        /// <summary>
        /// �����ʾ����
        /// </summary>
        public int TableCol
        {
            get
            {
                int i = this.GetValIntByKey(MapDataAttr.TableCol);
                if (i == 0 || i == 1)
                    return 4;
                return i;
            }
            set
            {
                this.SetValByKey(MapDataAttr.TableCol, value);
            }
        }
        public string TableWidth
        {
            get
            {
                //switch (this.TableCol)
                //{
                //    case 2:
                //        return
                //        labCol = 25;
                //        ctrlCol = 75;
                //        break;
                //    case 4:
                //        labCol = 20;
                //        ctrlCol = 30;
                //        break;
                //    case 6:
                //        labCol = 15;
                //        ctrlCol = 30;
                //        break;
                //    case 8:
                //        labCol = 10;
                //        ctrlCol = 15;
                //        break;
                //    default:
                //        break;
                //}


                int i = this.GetValIntByKey(MapDataAttr.TableWidth);
                if (i <= 50)
                    return "100%";
                return i + "px";
            }
        }
        #endregion

        #region ���췽��
        public Map GenerHisMap()
        {
            MapAttrs mapAttrs = this.MapAttrs;
            if (mapAttrs.Count == 0)
            {
                this.RepairMap();
                mapAttrs = this.MapAttrs;
            }

            Map map = new Map(this.PTable);
            DBUrl u = new DBUrl(this.HisDBUrl);
            map.EnDBUrl = u;
            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            // �����ӱ�
            MapDtls dtls = this.MapDtls; // new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                GEDtls dtls1 = new GEDtls(dtl.No);
                map.AddDtl(dtls1, "RefPK");
            }

            #region ��ѯ����.
            map.IsShowSearchKey = this.RptIsSearchKey; //�Ƿ����ùؼ��ֲ�ѯ.
            // �����ڲ�ѯ.
            map.DTSearchWay = this.RptDTSearchWay; //���ڲ�ѯ��ʽ.
            map.DTSearchKey = this.RptDTSearchKey; //�����ֶ�.

            //���������ѯ�ֶ�.
            string[] keys = this.RptSearchKeys.Split('*');
            foreach (string key in keys)
            {
                if (string.IsNullOrEmpty(key))
                    continue;

                map.AddSearchAttr(key);
            }
            #endregion ��ѯ����.

            return map;
        }
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
        /// ����map.
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static Map GenerHisMap(string no)
        {
            if (SystemConfig.IsDebug)
            {
                MapData md = new MapData();
                md.No = no;
                md.Retrieve();
                return md.GenerHisMap();
            }
            else
            {
                Map map = BP.DA.Cash.GetMap(no);
                if (map == null)
                {
                    MapData md = new MapData();
                    md.No = no;
                    md.Retrieve();
                    map = md.GenerHisMap();
                    BP.DA.Cash.SetMap(no, map);
                }
                return map;
            }
        }
        /// <summary>
        /// ӳ�����
        /// </summary>
        public MapData()
        {
        }
        /// <summary>
        /// ӳ�����
        /// </summary>
        /// <param name="no">ӳ����</param>
        public MapData(string no)
            : base(no)
        {
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
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ӳ�����";
                map.EnType = EnType.Sys;
                map.CodeStruct = "4";

                map.AddTBStringPK(MapDataAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapDataAttr.Name, null, "����", true, false, 0, 500, 20);

                map.AddTBString(MapDataAttr.EnPK, null, "ʵ������", true, false, 0, 10, 20);
                map.AddTBString(MapDataAttr.PTable, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Url, null, "����(���Զ������Ч)", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Dtls, null, "�ӱ�", true, false, 0, 500, 20);

                //��ʽΪ: @1=��������1@2=��������2@3=��������3
                // map.AddTBString(MapDataAttr.Slns, null, "�����ƽ������", true, false, 0, 500, 20);

                map.AddTBInt(MapDataAttr.FrmW, 900, "FrmW", true, true);
                map.AddTBInt(MapDataAttr.FrmH, 1200, "FrmH", true, true);

                map.AddTBInt(MapDataAttr.TableCol, 4, "ɵ�ϱ���ʾ����", true, true);
                map.AddTBInt(MapDataAttr.TableWidth, 600, "�����", true, true);

                //����Դ.
                map.AddTBInt(MapDataAttr.DBURL, 0, "DBURL", true, false);

                //Tag
                map.AddTBString(MapDataAttr.Tag, null, "Tag", true, false, 0, 500, 20);

                //FrmType  @���ɱ���@ɵ�ϱ���@�Զ����.
                map.AddTBInt(MapDataAttr.FrmType, 0, "������", true, false);


                // ����Ϊ������ֶΡ�
                map.AddTBString(MapDataAttr.FK_FrmSort, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.FK_FormTree, null, "�������", true, false, 0, 500, 20);

                // enumAppType
                map.AddTBInt(MapDataAttr.AppType, 1, "Ӧ������", true, false);

                map.AddTBString(MapDataAttr.Note, null, "��ע", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Designer, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerUnit, null, "��λ", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerContact, null, "��ϵ��ʽ", true, false, 0, 500, 20);

                //���Ӳ����ֶ�.
                map.AddTBAtParas(4000);

                map.AddTBInt(MapDataAttr.Idx, 100, "˳���", true, true);
                map.AddTBString(MapDataAttr.GUID, null, "GUID", true, false, 0, 128, 20);
                map.AddTBString(MapDataAttr.Ver, null, "�汾��", true, false, 0, 30, 20);
                this._enMap = map;
                return this._enMap;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void DoUp()
        {
            this.DoOrderUp(MapDataAttr.FK_FormTree, this.FK_FormTree, MapDataAttr.Idx);
        }
        /// <summary>
        /// ����
        /// </summary>
        public void DoOrderDown()
        {
            this.DoOrderDown(MapDataAttr.FK_FormTree, this.FK_FormTree, MapDataAttr.Idx);
        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static MapData ImpMapData(DataSet ds)
        {
            return ImpMapData(ds, true);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isSetReadony"></param>
        /// <returns></returns>
        public static MapData ImpMapData(DataSet ds, bool isSetReadony)
        {
            string errMsg = "";
            if (ds.Tables.Contains("WF_Flow") == true)
                errMsg += "@��ģ���ļ�Ϊ����ģ�塣";

            if (ds.Tables.Contains("Sys_MapAttr") == false)
                errMsg += "@ȱ�ٱ�:Sys_MapAttr";

            if (ds.Tables.Contains("Sys_MapData") == false)
                errMsg += "@ȱ�ٱ�:Sys_MapData";
            if (errMsg != "")
                throw new Exception(errMsg);

            DataTable dt = ds.Tables["Sys_MapData"];
            string fk_mapData = dt.Rows[0]["No"].ToString();
            MapData md = new MapData();
            md.No = fk_mapData;
            if (md.IsExits)
                throw new Exception("�Ѿ�����(" + fk_mapData + ")�����ݡ�");

            //����.
            return ImpMapData(fk_mapData, ds, isSetReadony);
        }
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="fk_mapdata">��ID</param>
        /// <param name="ds">������</param>
        /// <param name="isSetReadonly">�Ƿ�����ֻ����</param>
        /// <returns></returns>
        public static MapData ImpMapData(string fk_mapdata, DataSet ds, bool isSetReadonly)
        {
            
                #region ��鵼��������Ƿ�����.
                string errMsg = "";
                //if (ds.Tables[0].TableName != "Sys_MapData")
                //    errMsg += "@�Ǳ�ģ�塣";

                if (ds.Tables.Contains("WF_Flow") == true)
                    errMsg += "@��ģ���ļ�Ϊ����ģ�塣";

                if (ds.Tables.Contains("Sys_MapAttr") == false)
                    errMsg += "@ȱ�ٱ�:Sys_MapAttr";

                if (ds.Tables.Contains("Sys_MapData") == false)
                    errMsg += "@ȱ�ٱ�:Sys_MapData";

                DataTable dtCheck = ds.Tables["Sys_MapAttr"];
                bool isHave = false;
                foreach (DataRow dr in dtCheck.Rows)
                {
                    if (dr["KeyOfEn"].ToString() == "OID")
                    {
                        isHave = true;
                        break;
                    }
                }

                if (isHave == false)
                    errMsg += "@ȱ����:OID";

                if (errMsg != "")
                    throw new Exception("���´��󲻿ɵ��룬���ܵ�ԭ���ǷǱ�ģ���ļ�:" + errMsg);
                #endregion

                // ���������ִ�е�sql.
                string endDoSQL = "";

                //����Ƿ����OID�ֶ�.
                MapData mdOld = new MapData();
                mdOld.No = fk_mapdata;
                mdOld.RetrieveFromDBSources();
                mdOld.Delete();

                // ���dataset��map.
                string oldMapID = "";
                DataTable dtMap = ds.Tables["Sys_MapData"];
                if (dtMap.Rows.Count == 1)
                {
                    oldMapID = dtMap.Rows[0]["No"].ToString();
                }
                else
                {
                    foreach (DataRow dr in dtMap.Rows)
                    {
                        if (dr["No"].ToString().Contains("Dtl"))
                            continue; /*�����жϲ���ȷ.*/
                        oldMapID = dr["No"].ToString();
                    }
                    if (string.IsNullOrEmpty(oldMapID) == true)
                    {
                        oldMapID = dtMap.Rows[0]["No"].ToString();
                    }
                    //  throw new Exception("@û���ҵ� oldMapID ");
                }

                string timeKey = DateTime.Now.ToString("MMddHHmmss");
                // string timeKey = fk_mapdata;
                foreach (DataTable dt in ds.Tables)
                {
                    int idx = 0;
                    switch (dt.TableName)
                    {
                        case "Sys_MapDtl":
                            foreach (DataRow dr in dt.Rows)
                            {
                                MapDtl dtl = new MapDtl();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    dtl.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                if (isSetReadonly)
                                {
                                    //dtl.IsReadonly = true;

                                    dtl.IsInsert = false;
                                    dtl.IsUpdate = false;
                                    dtl.IsDelete = false;
                                }

                                dtl.Insert();
                            }
                            break;
                        case "Sys_MapData":
                            foreach (DataRow dr in dt.Rows)
                            {
                                MapData md = new MapData();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                   

                                    md.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                    //md.SetValByKey(dc.ColumnName, val);
                                }
                                if (string.IsNullOrEmpty(md.PTable.Trim()))
                                    md.PTable = md.No;

                                if (string.IsNullOrEmpty(mdOld.FK_FormTree) == false)
                                    md.FK_FormTree = mdOld.FK_FormTree;

                                if (string.IsNullOrEmpty(mdOld.FK_FrmSort) == false)
                                    md.FK_FrmSort = mdOld.FK_FrmSort;

                                if (string.IsNullOrEmpty(mdOld.PTable) == false)
                                    md.PTable = mdOld.PTable;

                                md.DirectInsert();
                            }
                            break;
                        case "Sys_FrmBtn":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmBtn en = new FrmBtn();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                               

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                if (isSetReadonly == true)
                                    en.IsEnable = false;


                                en.MyPK = "Btn_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_FrmLine":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmLine en = new FrmLine();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                   

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                en.MyPK = "LE_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_FrmLab":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmLab en = new FrmLab();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;
 

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                //  en.FK_MapData = fk_mapdata; ɾ�����н���ӱ�lab�����⡣
                                en.MyPK = "LB_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_FrmLink":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmLink en = new FrmLink();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                  

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                en.MyPK = "LK_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_FrmEle":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmEle en = new FrmEle();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;
 
                                 

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                if (isSetReadonly == true)
                                    en.IsEnable = false;

                                en.Insert();
                            }
                            break;
                        case "Sys_FrmImg":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmImg en = new FrmImg();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                  
 
                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                en.MyPK = "Img_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_FrmImgAth":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmImgAth en = new FrmImgAth();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;
                                   
                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }

                                if (string.IsNullOrEmpty(en.CtrlID))
                                    en.CtrlID = "ath" + idx;

                                en.Insert();
                            }
                            break;
                        case "Sys_FrmRB":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmRB en = new FrmRB();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;
                                  
                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }


                                try
                                {
                                    en.Save();
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case "Sys_FrmAttachment":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                FrmAttachment en = new FrmAttachment();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                en.MyPK = "Ath_" + idx + "_" + fk_mapdata;
                                if (isSetReadonly == true)
                                {
                                    en.IsDeleteInt = 0;
                                    en.IsUpload = false;
                                }

                                try
                                {
                                    en.Insert();
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case "Sys_MapM2M":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                MapM2M en = new MapM2M();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                //   en.NoOfObj = "M2M_" + idx + "_" + fk_mapdata;
                                if (isSetReadonly == true)
                                {
                                    en.IsDelete = false;
                                    en.IsInsert = false;
                                }
                                try
                                {
                                    en.Insert();
                                }
                                catch
                                {
                                    en.Update();
                                }
                            }
                            break;
                        case "Sys_MapFrame":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                MapFrame en = new MapFrame();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;
                                   

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                en.NoOfObj = "Fra_" + idx + "_" + fk_mapdata;
                                en.Insert();
                            }
                            break;
                        case "Sys_MapExt":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                MapExt en = new MapExt();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    if (string.IsNullOrEmpty(val.ToString()) == true)
                                        continue;


                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }
                                try
                                {
                                    en.Insert();
                                }
                                catch
                                {
                                    en.MyPK = "Ext_" + idx + "_" + fk_mapdata;
                                    en.Insert();
                                }
                            }
                            break;
                        case "Sys_MapAttr":
                            foreach (DataRow dr in dt.Rows)
                            {
                                MapAttr en = new MapAttr();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                }

                                if (isSetReadonly == true)
                                {
                                    if (en.DefValReal != null
                                        && (en.DefValReal.Contains("@WebUser.")
                                        || en.DefValReal.Contains("@RDT")))
                                        en.DefValReal = "";

                                    switch (en.UIContralType)
                                    {
                                        case UIContralType.DDL:
                                            en.UIIsEnable = false;
                                            break;
                                        case UIContralType.TB:
                                            en.UIIsEnable = false;
                                            break;
                                        case UIContralType.RadioBtn:
                                            en.UIIsEnable = false;
                                            break;
                                        case UIContralType.CheckBok:
                                            en.UIIsEnable = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                en.MyPK = en.FK_MapData + "_" + en.KeyOfEn;
                                en.DirectInsert();
                            }
                            break;
                        case "Sys_GroupField":
                            foreach (DataRow dr in dt.Rows)
                            {
                                idx++;
                                GroupField en = new GroupField();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    object val = dr[dc.ColumnName] as object;
                                    if (val == null)
                                        continue;

                                    try
                                    {
                                        en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                    }
                                    catch
                                    {
                                        throw new Exception("val:" + val.ToString() + "oldMapID:" + oldMapID + "fk_mapdata:"+fk_mapdata);
                                    }
                                }
                                int beforeID = en.OID;
                                en.OID = 0;
                                en.Insert();
                                endDoSQL += "@UPDATE Sys_MapAttr SET GroupID=" + en.OID + " WHERE FK_MapData='" + fk_mapdata + "' AND GroupID=" + beforeID;
                            }
                            break;
                        case "Sys_Enum":
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnum se = new Sys.SysEnum();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    se.SetValByKey(dc.ColumnName, val);
                                }
                                se.MyPK = se.EnumKey + "_" + se.Lang + "_" + se.IntKey;
                                if (se.IsExits)
                                    continue;
                                se.Insert();
                            }
                            break;
                        case "Sys_EnumMain":
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnumMain sem = new Sys.SysEnumMain();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    if (val == null)
                                        continue;
                                    sem.SetValByKey(dc.ColumnName, val);
                                }
                                if (sem.IsExits)
                                    continue;
                                sem.Insert();
                            }
                            break;
                        case "WF_Node":
                            if (dt.Rows.Count > 0)
                            {
                                endDoSQL += "@UPDATE WF_Node SET FWCSta=2"
                                    + ",FWC_X=" + dt.Rows[0]["FWC_X"]
                                    + ",FWC_Y=" + dt.Rows[0]["FWC_Y"]
                                    + ",FWC_H=" + dt.Rows[0]["FWC_H"]
                                    + ",FWC_W=" + dt.Rows[0]["FWC_W"]
                                    + ",FWCType=" + dt.Rows[0]["FWCType"]
                                    + " WHERE NodeID=" + fk_mapdata.Replace("ND", "");
                            }
                            break;
                        default:
                            break;
                    }
                }
                //ִ����������sql.
                DBAccess.RunSQLs(endDoSQL);

                MapData mdNew = new MapData(fk_mapdata);
                mdNew.RepairMap();
                //  mdNew.FK_FrmSort = fk_sort;
                mdNew.Update();
                return mdNew;
           
        }
        public void RepairMap()
        {
            GroupFields gfs = new GroupFields(this.No);
            if (gfs.Count == 0)
            {
                GroupField gf = new GroupField();
                gf.EnName = this.No;
                gf.Lab = this.Name;
                gf.Insert();
                string sqls = "";
                sqls += "@UPDATE Sys_MapDtl SET GroupID=" + gf.OID + " WHERE FK_MapData='" + this.No + "'";
                sqls += "@UPDATE Sys_MapAttr SET GroupID=" + gf.OID + " WHERE FK_MapData='" + this.No + "'";
                sqls += "@UPDATE Sys_MapFrame SET GroupID=" + gf.OID + " WHERE FK_MapData='" + this.No + "'";
                sqls += "@UPDATE Sys_MapM2M SET GroupID=" + gf.OID + " WHERE FK_MapData='" + this.No + "'";
                sqls += "@UPDATE Sys_FrmAttachment SET GroupID=" + gf.OID + " WHERE FK_MapData='" + this.No + "'";
                DBAccess.RunSQLs(sqls);
            }
            else
            {
                GroupField gfFirst = gfs[0] as GroupField;
                string sqls = "";
                sqls += "@UPDATE Sys_MapDtl SET GroupID=" + gfFirst.OID + "        WHERE  No   IN (SELECT X.No FROM (SELECT No FROM Sys_MapDtl WHERE GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.No + "')) AS X ) AND FK_MapData='" + this.No + "'";
                sqls += "@UPDATE Sys_MapAttr SET GroupID=" + gfFirst.OID + "       WHERE  MyPK IN (SELECT X.MyPK FROM (SELECT MyPK FROM Sys_MapAttr       WHERE GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.No + "')) AS X) AND FK_MapData='" + this.No + "' ";
                sqls += "@UPDATE Sys_MapFrame SET GroupID=" + gfFirst.OID + "      WHERE  MyPK IN (SELECT X.MyPK FROM (SELECT MyPK FROM Sys_MapFrame      WHERE GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.No + "')) AS X) AND FK_MapData='" + this.No + "' ";
                sqls += "@UPDATE Sys_MapM2M SET GroupID=" + gfFirst.OID + "        WHERE  MyPK IN (SELECT X.MyPK FROM (SELECT MyPK FROM Sys_MapM2M        WHERE GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.No + "')) AS X) AND FK_MapData='" + this.No + "' ";
                sqls += "@UPDATE Sys_FrmAttachment SET GroupID=" + gfFirst.OID + " WHERE  MyPK IN (SELECT X.MyPK FROM (SELECT MyPK FROM Sys_FrmAttachment WHERE GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.No + "')) AS X) AND FK_MapData='" + this.No + "' ";

#warning ��Щsql ����Oracle �����⣬���ǲ�Ӱ��ʹ��.
                try
                {
                    DBAccess.RunSQLs(sqls);
                }
                catch
                {

                }
            }

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (this.EnPK == "OID")
            {
                if (attr.IsExit(MapAttrAttr.KeyOfEn, "OID", MapAttrAttr.FK_MapData, this.No) == false)
                {
                    attr.FK_MapData = this.No;
                    attr.KeyOfEn = "OID";
                    attr.Name = "OID";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "0";
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.Insert();
                }
            }
            if (this.EnPK == "No" || this.EnPK == "MyPK")
            {
                if (attr.IsExit(MapAttrAttr.KeyOfEn, this.EnPK, MapAttrAttr.FK_MapData, this.No) == false)
                {
                    attr.FK_MapData = this.No;
                    attr.KeyOfEn = this.EnPK;
                    attr.Name = this.EnPK;
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "0";
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.Insert();
                }
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, "RDT", MapAttrAttr.FK_MapData, this.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "RDT";
                attr.Name = "����ʱ��";

                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "@RDT";
                attr.Tag = "1";
                attr.Insert();
            }
        }
        protected override bool beforeInsert()
        {
            this.PTable = PubClass.DealToFieldOrTableNames(this.PTable);
            return base.beforeInsert();
        }
        /// <summary>
        /// ����Para ����.
        /// </summary>
        public void ResetMaxMinXY()
        {
            if (this.HisFrmType != FrmType.FreeFrm)
                return;


            #region ���������,�����ұߵ�ֵ��
            // �������.
            float i1 = DBAccess.RunSQLReturnValFloat("SELECT MIN(X1) FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'", 0);
            if (i1 == 0) /*û���ߣ�ֻ��ͼƬ������¡�*/
                i1 = DBAccess.RunSQLReturnValFloat("SELECT MIN(X) FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'", 0);

            float i2 = DBAccess.RunSQLReturnValFloat("SELECT MIN(X)  FROM Sys_FrmLab  WHERE FK_MapData='" + this.No + "'", 0);
            if (i1 > i2)
                this.MaxLeft = i2;
            else
                this.MaxLeft = i1;

            // �����ұ�.
            i1 = DBAccess.RunSQLReturnValFloat("SELECT Max(X2) FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'", 0);
            if (i1 == 0)
            {
                /*û���ߵ����������ͼƬ�����㡣*/
                i1 = DBAccess.RunSQLReturnValFloat("SELECT Max(X+W) FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'", 0);
            }
            this.MaxRight = i1;

            // ����top.
            i1 = DBAccess.RunSQLReturnValFloat("SELECT MIN(Y1) FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'", 0);
            i2 = DBAccess.RunSQLReturnValFloat("SELECT MIN(Y)  FROM Sys_FrmLab  WHERE FK_MapData='" + this.No + "'", 0);

            if (i1 > i2)
                this.MaxTop = i2;
            else
                this.MaxTop = i1;

            // ����end.
            i1 = DBAccess.RunSQLReturnValFloat("SELECT Max(Y1) FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'", 0);
            /*С�������2014/10/23-----------------------START*/
            if (i1 == 0) /*û���ߣ�ֻ��ͼƬ������¡�*/
                i1 = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+H) FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'", 0);

            /*С�������2014/10/23-----------------------END*/
            i2 = DBAccess.RunSQLReturnValFloat("SELECT Max(Y)  FROM Sys_FrmLab  WHERE FK_MapData='" + this.No + "'", 0);
            if (i2 == 0)
                i2 = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+H) FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'", 0);
            //�����ײ��� ����
            float endFrmAtt = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+H)  FROM Sys_FrmAttachment  WHERE FK_MapData='" + this.No + "'", 0);
            //�����ײ�����ϸ��
            float endFrmDtl = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+H)  FROM Sys_MapDtl  WHERE FK_MapData='" + this.No + "'", 0);

            //�����ײ�����չ�ؼ�
            float endFrmEle = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+H)  FROM Sys_FrmEle  WHERE FK_MapData='" + this.No + "'", 0);
            //�����ײ���textbox
            float endFrmAttr = DBAccess.RunSQLReturnValFloat("SELECT Max(Y+UIHeight)  FROM  Sys_MapAttr  WHERE FK_MapData='" + this.No + "' and UIVisible='1'", 0);

            if (i1 > i2)
                this.MaxEnd = i1;
            else
                this.MaxEnd = i2;

            this.MaxEnd = this.MaxEnd > endFrmAtt ? this.MaxEnd : endFrmAtt;
            this.MaxEnd = this.MaxEnd > endFrmDtl ? this.MaxEnd : endFrmDtl;
            this.MaxEnd = this.MaxEnd > endFrmEle ? this.MaxEnd : endFrmEle;
            this.MaxEnd = this.MaxEnd > endFrmAtt ? this.MaxEnd : endFrmAttr;

            #endregion

            this.DirectUpdate();
        }

        /// <summary>
        /// ��λ��.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="scrWidth"></param>
        /// <returns></returns>
        public static float GenerSpanWeiYi(MapData md, float scrWidth)
        {
            if (scrWidth == 0)
                scrWidth = 900;

            float left = md.MaxLeft;
            if (left == 0)
            {
                md.ResetMaxMinXY();
                md.RetrieveFromDBSources();
                md.Retrieve();

                left = md.MaxLeft;
            }
            //ȡ�������ο�ֵ���򲻽���λ��
            if (left == 0)
                return left;

            float right = md.MaxRight;
            float withFrm = right - left;
            if (withFrm >= scrWidth)
            {
                /* ���ʵ�ʱ���ȴ�����Ļ��� */
                return -left;
            }
            
            //����λ�ƴ�С
            float space = (scrWidth - withFrm) / 2; //�հ׵ĵط�.

            return -(left - space);
        }
        /// <summary>
        /// ����Ļ���
        /// </summary>
        /// <param name="md"></param>
        /// <param name="scrWidth"></param>
        /// <returns></returns>
        public static float GenerSpanWidth(MapData md, float scrWidth)
        {
            if (scrWidth == 0)
                scrWidth = 900;
            float left = md.MaxLeft;
            if (left == 0)
            {
                md.ResetMaxMinXY();
                left = md.MaxLeft;
            }

            float right = md.MaxRight;
            float withFrm = right - left;
            if (withFrm >= scrWidth)
            {
                return withFrm;
            }
            return scrWidth;
        }
        /// <summary>
        /// ����Ļ�߶�
        /// </summary>
        /// <param name="md"></param>
        /// <param name="scrWidth"></param>
        /// <returns></returns>
        public static float GenerSpanHeight(MapData md, float scrHeight)
        {
            if (scrHeight == 0)
                scrHeight = 1200;

            float end = md.MaxEnd;
            if (end > scrHeight)
                return end + 10;
            else
                return scrHeight;
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.PTable = PubClass.DealToFieldOrTableNames(this.PTable);
            MapAttrs.Retrieve(MapAttrAttr.FK_MapData, PTable);

            //���°汾��.
            this.Ver = DataType.CurrentDataTimess;

            #region  ����Ƿ���ca��֤����.
            bool isHaveCA = false;
            foreach (MapAttr item in this.MapAttrs)
            {
                if (item.SignType == SignType.CA)
                {
                    isHaveCA = true;
                    break;
                }
            }
            this.IsHaveCA = isHaveCA;
            if (IsHaveCA == true)
            {
                //�����������ֶ�.
                //MapAttr attr = new BP.Sys.MapAttr();
                // attr.MyPK = this.No + "_SealData";
                // attr.FK_MapData = this.No;
                // attr.HisEditType = BP.En.EditType.UnDel;
                //attr.KeyOfEn = "SealData";
                // attr.Name = "SealData";
                // attr.MyDataType = BP.DA.DataType.AppString;
                // attr.UIContralType = UIContralType.TB;
                //  attr.LGType = FieldTypeS.Normal;
                // attr.UIVisible = false;
                // attr.UIIsEnable = false;
                // attr.MaxLen = 4000;
                // attr.MinLen = 0;
                // attr.Save();
            }
            #endregion  ����Ƿ���ca��֤����.

            return base.beforeUpdateInsertAction();
        }
        /// <summary>
        /// ���°汾
        /// </summary>
        public void UpdateVer()
        {
            string sql = "UPDATE Sys_MapData SET VER='" + BP.DA.DataType.CurrentDataTimess + "' WHERE No='" + this.No + "'";
            BP.DA.DBAccess.RunSQL(sql);
        }
        protected override bool beforeDelete()
        {
            string sql = "";
            sql = "SELECT * FROM Sys_MapDtl WHERE FK_MapData ='" + this.No + "'";
            DataTable Sys_MapDtl = DBAccess.RunSQLReturnTable(sql);
            string ids = "'" + this.No + "'";
            foreach (DataRow dr in Sys_MapDtl.Rows)
                ids += ",'" + dr["No"] + "'";

            string where = " FK_MapData IN (" + ids + ")";

            #region ɾ����ص����ݡ�
            sql += "@DELETE FROM Sys_MapDtl WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmLine WHERE " + where;
            sql += "@DELETE FROM Sys_FrmEle WHERE " + where;
            sql += "@DELETE FROM Sys_FrmEvent WHERE " + where;
            sql += "@DELETE FROM Sys_FrmBtn WHERE " + where;
            sql += "@DELETE FROM Sys_FrmLab WHERE " + where;
            sql += "@DELETE FROM Sys_FrmLink WHERE " + where;
            sql += "@DELETE FROM Sys_FrmImg WHERE " + where;
            sql += "@DELETE FROM Sys_FrmImgAth WHERE " + where;
            sql += "@DELETE FROM Sys_FrmRB WHERE " + where;
            sql += "@DELETE FROM Sys_FrmAttachment WHERE " + where;
            sql += "@DELETE FROM Sys_MapM2M WHERE " + where;
            sql += "@DELETE FROM Sys_MapFrame WHERE " + where;
            sql += "@DELETE FROM Sys_MapExt WHERE " + where;
            sql += "@DELETE FROM Sys_MapAttr WHERE " + where;
            sql += "@DELETE FROM Sys_GroupField WHERE EnName IN (" + ids + ")";
            sql += "@DELETE FROM Sys_MapData WHERE No IN (" + ids + ")";
            sql += "@DELETE FROM Sys_MapM2M WHERE " + where;
            sql += "@DELETE FROM Sys_M2M WHERE " + where;
            DBAccess.RunSQLs(sql);
            #endregion ɾ����ص����ݡ�

            #region ɾ�������
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }

            MapDtls dtls = new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    DBAccess.RunSQL("DROP TABLE " + dtl.PTable);
                }
                catch
                {
                }
                dtl.Delete();
            }
            #endregion

            return base.beforeDelete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public GEEntity GenerHisEn(MapExts mes, GEEntity en)
        {
            return en;
        }

        public System.Data.DataSet GenerHisDataSet()
        {
            return GenerHisDataSet(this.No);
        }
        public static System.Data.DataSet GenerHisDataSet(string FK_MapData)
        {

            // Sys_MapDtl.
            string sql = "SELECT FK_MapData,No,X,Y,W,H  FROM Sys_MapDtl WHERE FK_MapData ='{0}'";
            sql = string.Format(sql, FK_MapData);
            DataTable dtMapDtl = DBAccess.RunSQLReturnTable(sql);
            dtMapDtl.TableName = "Sys_MapDtl";

            string ids = string.Format("'{0}'", FK_MapData);
            foreach (DataRow dr in dtMapDtl.Rows)
            {
                ids += ",'" + dr["No"] + "'";
            }
            string sqls = string.Empty;
            List<string> listNames = new List<string>();
            // Sys_GroupField.
            listNames.Add("Sys_GroupField");
            sql = "SELECT * FROM Sys_GroupField WHERE  EnName IN (" + ids + ")";
            sqls += sql;

            // Sys_Enum
            listNames.Add("Sys_Enum");
            sql = "@SELECT * FROM Sys_Enum WHERE EnumKey IN ( SELECT UIBindKey FROM Sys_MapAttr WHERE FK_MapData IN (" + ids + ") )";
            sqls += sql;

            // ������
            string nodeIDstr = FK_MapData.Replace("ND", "");
            if (DataType.IsNumStr(nodeIDstr))
            {
                // ������״̬:0 ����;1 ����;2 ֻ��;
                listNames.Add("WF_Node");
                sql = "@SELECT NodeID,FWC_X,FWC_Y,FWC_W,FWC_H,FWCSta,FWCType FROM WF_Node WHERE NodeID=" + nodeIDstr + " AND  FWCSta in(1,2)";
                sqls +=  sql;
            }

            string where = " FK_MapData IN (" + ids + ")";

            // Sys_MapData.
            listNames.Add("Sys_MapData");
            sql = "@SELECT No,Name,FrmW,FrmH FROM Sys_MapData WHERE No='" + FK_MapData + "'";
            sqls +=  sql;


            // Sys_MapAttr.
            listNames.Add("Sys_MapAttr");
            sql = "@SELECT UIVisible,FK_MapData,MyPK,KeyOfEn,Name,DefVal,UIContralType,MyDataType,LGType,X,Y,UIBindKey,UIWidth,UIHeight "
                +" FROM Sys_MapAttr WHERE " + where + " AND KeyOfEn NOT IN('WFState') ORDER BY FK_MapData,IDX ";
            sqls +=sql;

            // Sys_MapM2M.
            listNames.Add("Sys_MapM2M");
            sql = "@SELECT MyPK,FK_MapData,NoOfObj,M2MTYPE,X,Y,W,H FROM Sys_MapM2M WHERE " + where;
            sqls +=  sql;

            // Sys_MapExt.
            listNames.Add("Sys_MapExt");
            sql = "@SELECT * FROM Sys_MapExt WHERE " + where;
            sqls +=  sql;

            // line.
            listNames.Add("Sys_FrmLine");
            sql = "@SELECT MyPK,FK_MapData,X,X1,X2,Y,Y1,Y2,BorderColor,BorderWidth from Sys_FrmLine WHERE " + where;
            sqls +=  sql;

            // ele.
            listNames.Add("Sys_FrmEle");
            sql = "@SELECT FK_MapData,MyPK,EleType,EleID,EleName,X,Y,W,H FROM Sys_FrmEle WHERE " + where;
            sqls += sql;

            // link.
            listNames.Add("Sys_FrmLink");
            sql = "@SELECT FK_MapData,MyPK,Text,URL,Target,FontSize,FontColor,X,Y from Sys_FrmLink WHERE " + where;
            sqls +=  sql;

            // btn.
            listNames.Add("Sys_FrmBtn");
            sql = "@SELECT FK_MapData,MyPK,Text,BtnType,EventType,EventContext,MsgErr,MsgOK,X,Y FROM Sys_FrmBtn WHERE " + where;
            sqls +=sql;

            // Sys_FrmImg.
            listNames.Add("Sys_FrmImg");
            sql = "@SELECT * FROM Sys_FrmImg WHERE " + where;
            sqls +=  sql;

            // Sys_FrmLab.
            listNames.Add("Sys_FrmLab");
            sql = "@SELECT MyPK,FK_MapData,Text,X,Y,FontColor,FontName,FontSize,FontStyle,FontWeight,IsBold,IsItalic from Sys_FrmLab WHERE " + where;
            sqls +=  sql;

            // Sys_FrmRB.
            listNames.Add("Sys_FrmRB");
            sql = "@SELECT * FROM Sys_FrmRB WHERE " + where;
            sqls +=  sql;


            // Sys_FrmAttachment. 
            listNames.Add("Sys_FrmAttachment");
            sql = "@SELECT MyPK,FK_MapData,UploadType,X,Y,W,H,NoOfObj,Name,Exts,SaveTo,IsUpload,IsDelete,IsDownload "
                +" FROM Sys_FrmAttachment WHERE " + where + " AND FK_Node=0";
            sqls += sql;

            // Sys_FrmImgAth.
            listNames.Add("Sys_FrmImgAth");
            sql = "@SELECT * FROM Sys_FrmImgAth WHERE " + where;
            sqls +=  sql;

           //// sqls.Replace(";", ";" + Environment.NewLine);
           // DataSet ds = DA.DBAccess.RunSQLReturnDataSet(sqls);
           // if (ds != null && ds.Tables.Count == listNames.Count)
           //     for (int i = 0; i < listNames.Count; i++)
           //     {
           //         ds.Tables[i].TableName = listNames[i];
           //     }

            string[] strs = sqls.Split('@');
            DataSet ds = new DataSet();
          
            if (strs != null && strs.Length == listNames.Count)
                for (int i = 0; i < listNames.Count; i++)
                {
                    string s = strs[i];
                    if (string.IsNullOrEmpty(s))
                        continue;
                    DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(s);
                    dt.TableName = listNames[i];
                    ds.Tables.Add(dt);
                }

            foreach (DataTable item in ds.Tables)
            {
                if (item.TableName == "Sys_MapAttr" && item.Rows.Count == 0)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = FK_MapData;
                    attr.KeyOfEn = "OID";
                    attr.Name = "OID";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "0";
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.Insert();
                }
            }

            ds.Tables.Add(dtMapDtl);
            return ds;
        }


        /// <summary>
        /// �����Զ��ģ�����
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="attrs"></param>
        /// <param name="attr"></param>
        /// <param name="tbPer"></param>
        /// <returns></returns>
        public static string GenerAutoFull(string pk, MapAttrs attrs, MapExt me, string tbPer)
        {
            string left = "\n document.forms[0]." + tbPer + "_TB" + me.AttrOfOper + "_" + pk + ".value = ";
            string right = me.Doc;
            foreach (MapAttr mattr in attrs)
            {
                right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + tbPer + "_TB_" + mattr.KeyOfEn + "_" + pk + ".value) ");
            }
            return " alert( document.forms[0]." + tbPer + "_TB" + me.AttrOfOper + "_" + pk + ".value ) ; \t\n " + left + right;
        }
    }
    /// <summary>
    /// ӳ�����s
    /// </summary>
    public class MapDatas : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ӳ�����s
        /// </summary>
        public MapDatas()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapData();
            }
        }
        #endregion
    }
}
