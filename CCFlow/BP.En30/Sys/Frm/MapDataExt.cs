using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using System.Collections.Generic;

namespace BP.Sys
{
    /// <summary>
    /// ӳ�����
    /// </summary>
    public class MapDataExt : EntityNoName
    {
        #region Ȩ�޿���.
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsDelete = false;
                    uac.IsUpdate = true;
                    return uac;
                }
                uac.Readonly();
                return uac;
            }
        }
        #endregion Ȩ�޿���.

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
        /// <summary>
        /// ӳ�����
        /// </summary>
        public MapDataExt()
        {
        }
        /// <summary>
        /// ӳ�����
        /// </summary>
        /// <param name="no">ӳ����</param>
        public MapDataExt(string no)
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
                map.EnDesc = "������";
                map.EnType = EnType.Sys;
                map.CodeStruct = "4";

                #region ��������.
                map.AddTBStringPK(MapDataAttr.No, null, "�����", true, false, 1, 20, 20);
                map.AddTBString(MapDataAttr.Name, null, "������", true, false, 0, 500, 20);

                //FrmType  @���ɱ���@ɵ�ϱ���@�Զ����. 
                map.AddDDLSysEnum(MapDataAttr.FrmType, 0, "������", true, true, MapDataAttr.FrmType,
                    "@0=���ɱ�@1=ɵ�ϱ�@2=silverlight��@3=URL��(�Զ���)@4=Word���ͱ�@5=Excel���ͱ�");
                map.AddTBString(MapDataAttr.PTable, null, "�洢��", true, false, 0, 500, 20);

                map.AddTBString(MapDataAttr.Url, null, "����(���Զ������Ч)", true, false, 0, 500, 20, true);
                #endregion ��������.

                // map.AddTBString(MapDataAttr.Dtls, null, "�ӱ�", true, false, 0, 500, 20);
                //��ʽΪ: @1=��������1@2=��������2@3=��������3
                // map.AddTBString(MapDataAttr.Slns, null, "�����ƽ������", true, false, 0, 500, 20);

                //map.AddTBInt(MapDataAttr.FrmW, 900, "FrmW", true, true);
                //map.AddTBInt(MapDataAttr.FrmH, 1200, "FrmH", true, true);

                //map.AddTBInt(MapDataAttr.TableCol, 4, "ɵ�ϱ���ʾ����", true, true);
                //map.AddTBInt(MapDataAttr.TableWidth, 600, "�����", true, true);

                //����Դ.
                //map.AddTBInt(MapDataAttr.DBURL, 0, "DBURL", true, false);

                //Tag
                //map.AddTBString(MapDataAttr.Tag, null, "Tag", true, false, 0, 500, 20);
                // ����Ϊ������ֶΡ�
                //map.AddTBString(MapDataAttr.FK_FrmSort, null, "�����", true, false, 0, 500, 20);
                //map.AddTBString(MapDataAttr.FK_FormTree, null, "�������", true, false, 0, 500, 20);
                // enumAppType
                //map.AddTBInt(MapDataAttr.AppType, 1, "Ӧ������", true, false);

                #region �������Ϣ.
                map.AddTBString(MapDataAttr.Designer, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerUnit, null, "��λ", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerContact, null, "��ϵ��ʽ", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Note, null, "��ע", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.GUID, null, "GUID", true, false, 0, 128, 20);
                map.AddTBString(MapDataAttr.Ver, null, "�汾��", true, false, 0, 30, 20);

                //���Ӳ����ֶ�.
                map.AddTBAtParas(4000);
                map.AddTBInt(MapDataAttr.Idx, 100, "˳���", true, true);
                #endregion �������Ϣ.




                RefMethod rm = new RefMethod();
                rm.Title = "��Ʊ�"; // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoDFrom";
                rm.Icon = "/WF/Img/Btn/DTS.gif";
                rm.Visable = true;
                rm.Target = "_blank";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "��չ����"; // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoMapExt";
                rm.Icon =  "/WF/Img/Btn/DTS.gif";
                rm.Visable = true;
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.Target = "_blank";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "���¼�"; // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoEvent";
                rm.Icon =  "/WF/Img/Btn/DTS.gif";
                rm.Visable = true;
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.Target = "_blank";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "����"; // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoExp";
                rm.Icon = "/WF/Img/Btn/DTS.gif";
                rm.Visable = true;
                rm.RefAttrLinkLabel = "���ÿ��˻صĽڵ�";
                rm.Target = "_blank";
                map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ����.
          /// <summary>
        /// ��Ʊ�
        /// </summary>
        /// <returns></returns>
        public string DoDFrom()
        {
            string url = "/WF/MapDef/CCForm/Frm.aspx?FK_MapData=" + this.No + "&T=sd";
            PubClass.WinOpen(url, 500, 550);
            return null;
        }
        /// <summary>
        /// ���¼�
        /// </summary>
        /// <returns></returns>
        public string DoEvent()
        {
            string url = "/WF/Admin/Action.aspx?FK_MapData=" + this.No + "&T=sd";
            return url;
        }
        
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoMapExt()
        {
            string url = "/WF/MapDef/MapExt.aspx?FK_MapData="+this.No+"&T=sd";
            return url;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            string urlExt = "/WF/Admin/XAP/DoPort.aspx?DoType=DownFormTemplete&FK_MapData=" + this.No;
            PubClass.WinOpen(urlExt, 900, 1000);
            return null;
        }
        #endregion ����.

    }
    /// <summary>
    /// ӳ�����s
    /// </summary>
    public class MapDataExts : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ӳ�����s
        /// </summary>
        public MapDataExts()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapDataExt();
            }
        }
        #endregion
    }
}
