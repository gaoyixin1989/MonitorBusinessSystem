using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    public enum AthCtrlWay
    {
        /// <summary>
        /// ������
        /// </summary>
        PK,
        /// <summary>
        /// FID
        /// </summary>
        FID,
        /// <summary>
        /// ������ID
        /// </summary>
        PWorkID
    }
    /// <summary>
    /// �����ϴ�����
    /// </summary>
    public enum AttachmentUploadType
    {
        /// <summary>
        /// ������
        /// </summary>
        Single,
        /// <summary>
        /// �����
        /// </summary>
        Multi,
        /// <summary>
        /// ָ����
        /// </summary>
        Specifically
    }
    /// <summary>
    /// �����ϴ���ʽ
    /// </summary>
    public enum AthUploadWay
    {
        /// <summary>
        /// �̳�ģʽ
        /// </summary>
        Inherit,
        /// <summary>
        /// Э��ģʽ
        /// </summary>
        Interwork
    }
    /// <summary>
    /// �ļ�չ�ַ�ʽ
    /// </summary>
    public enum FileShowWay
    {
        /// <summary>
        /// ���
        /// </summary>
        Table,
        /// <summary>
        /// ͼƬ
        /// </summary>
        Pict,
        /// <summary>
        /// ����ģʽ
        /// </summary>
        Free
    }

    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachmentAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Name
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// ���
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// �߶�
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// Ҫ���ϴ��ĸ�ʽ
        /// </summary>
        public const string Exts = "Exts";
        /// <summary>
        /// �������
        /// </summary>
        public const string NoOfObj = "NoOfObj";
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public const string IsUpload = "IsUpload";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsNote = "IsNote";
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDelete = "IsDelete";
        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public const string IsShowTitle = "IsShowTitle";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string IsDownload = "IsDownload";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string IsOrder = "IsOrder";
        /// <summary>
        /// ���ݴ洢��ʽ
        /// </summary>
        public const string SaveWay = "SaveWay";
        /// <summary>
        /// ���浽
        /// </summary>
        public const string SaveTo = "SaveTo";
        /// <summary>
        /// ���
        /// </summary>
        public const string Sort = "Sort";
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public const string UploadType = "UploadType";
        /// <summary>
        /// RowIdx
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �Զ����ƴ�С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// ���ݿ��Ʒ�ʽ(�Ը���������Ч��)
        /// </summary>
        public const string CtrlWay = "CtrlWay";
        /// <summary>
        /// �ϴ���ʽ(�Ը���������Ч��)
        /// </summary>
        public const string AthUploadWay = "AthUploadWay";
        /// <summary>
        /// �ļ�չ�ַ�ʽ
        /// </summary>
        public const string FileShowWay = "FileShowWay";
        /// <summary>
        /// �ϴ���ʽ
        /// 0�������ϴ���
        /// 1�������ϴ���
        /// </summary>
        public const string UploadCtrl = "UploadCtrl";
        /// <summary>
        /// �Ƿ�ɼ���
        /// </summary>
        public const string IsVisable = "IsVisable";

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

        #region ��ݼ�.
        /// <summary>
        /// �Ƿ����ÿ�ݼ�
        /// </summary>
        public const string FastKeyIsEnable = "FastKeyIsEnable";
        /// <summary>
        /// ��ݼ����ɹ���
        /// </summary>
        public const string FastKeyGenerRole = "FastKeyGenerRole";
        #endregion
    }
    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachment : EntityMyPK
    {
        #region ��������.
        /// <summary>
        /// �Ƿ�ɼ���
        /// </summary>
        public bool IsVisable
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.IsVisable, true);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.IsVisable, value);
            }
        }
        /// <summary>
        /// ʹ���ϴ������� - �ؼ�����
        /// 0=����.
        /// 1=������
        /// </summary>
        public int UploadCtrl
        {
            get
            {
                return this.GetParaInt(FrmAttachmentAttr.UploadCtrl);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.UploadCtrl, value);
            }
        }
        /// <summary>
        /// ���淽ʽ
        /// 0 =�ļ���ʽ���档
        /// 1 = ���浽���ݿ�.
        /// </summary>
        public int SaveWay
        {
            get
            {
                return this.GetParaInt(FrmAttachmentAttr.SaveWay);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.SaveWay, value);
            }
        }
        #endregion ��������.

        #region ����
        /// <summary>
        /// �ڵ���
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// �ϴ����ͣ������ģ������ָ���ģ�
        /// </summary>
        public AttachmentUploadType UploadType
        {
            get
            {
                return (AttachmentUploadType)this.GetValIntByKey(FrmAttachmentAttr.UploadType);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.UploadType, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string UploadTypeT
        {
            get
            {
                if (this.UploadType == AttachmentUploadType.Multi)
                    return "�฽��";
                if (this.UploadType == AttachmentUploadType.Single)
                    return "������";
                if (this.UploadType == AttachmentUploadType.Specifically)
                    return "ָ����";
                return "XXXXX";
            }
        }
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public bool IsUpload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsUpload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsUpload, value);
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsDownload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDownload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDownload, value);
            }
        }
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDelete, value);
            }
        }
        public int IsDeleteInt
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDelete, value);
            }
        }

        /// <summary>
        /// �Ƿ��������?
        /// </summary>
        public bool IsOrder
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsOrder);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsOrder, value);
            }
        }
        /// <summary>
        /// �Զ����ƴ�С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsAutoSize, value);
            }
        }
        /// <summary>
        /// IsShowTitle
        /// </summary>
        public bool IsShowTitle
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsShowTitle);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsShowTitle, value);
            }
        }
        /// <summary>
        /// ��ע��
        /// </summary>
        public bool IsNote
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsNote);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsNote, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Name);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Name, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Sort);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Sort, value);
            }
        }
        /// <summary>
        /// Ҫ��ĸ�ʽ
        /// </summary>
        public string Exts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Exts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Exts, value);
            }
        }
        public string SaveTo
        {
            get
            {
                string s = this.GetValStringByKey(FrmAttachmentAttr.SaveTo);
                if (s == "" || s == null)
                    s = SystemConfig.PathOfDataUser + @"\UploadFile\" + this.FK_MapData + "\\";
                return s;
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.SaveTo, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string NoOfObj
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.NoOfObj);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.NoOfObj, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.X);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.X, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.W);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.W, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.H);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.H, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.RowIdx, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.GroupID);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.GroupID, value);
            }
        }
        /// <summary>
        /// ���ݿ��Ʒ�ʽ
        /// </summary>
        public AthCtrlWay HisCtrlWay
        {
            get
            {
                return (AthCtrlWay)this.GetValIntByKey(FrmAttachmentAttr.CtrlWay);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.CtrlWay, (int)value);
            }
        }
        /// <summary>
        /// �ļ�չ�ַ�ʽ
        /// </summary>
        public FileShowWay FileShowWay
        {
            get
            {
                return (FileShowWay)this.GetParaInt(FrmAttachmentAttr.FileShowWay);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.FileShowWay, (int)value);
            }
        }
        /// <summary>
        /// �ϴ���ʽ�����ڸ���������Ч��
        /// </summary>
        public AthUploadWay AthUploadWay
        {
            get
            {
                return (AthUploadWay)this.GetValIntByKey(FrmAttachmentAttr.AthUploadWay);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.AthUploadWay, (int)value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmAttachmentAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.FK_MapData, value);
            }
        }
        #endregion

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

        #region ��ݼ�
        /// <summary>
        /// �Ƿ����ÿ�ݼ�
        /// </summary>
        public bool FastKeyIsEnable
        {
            get
            {
                return this.GetParaBoolen(FrmAttachmentAttr.FastKeyIsEnable);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.FastKeyIsEnable, value);
            }
        }
        /// <summary>
        /// ���ù���
        /// </summary>
        public string FastKeyGenerRole
        {
            get
            {
                return this.GetParaString(FrmAttachmentAttr.FastKeyGenerRole);
            }
            set
            {
                this.SetPara(FrmAttachmentAttr.FastKeyGenerRole, value);
            }
        }
        #endregion ��ݼ�

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public FrmAttachment()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachment(string mypk)
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
                Map map = new Map("Sys_FrmAttachment");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmAttachmentAttr.FK_MapData, null, "��ID", true, false, 1, 30, 20);
                map.AddTBString(FrmAttachmentAttr.NoOfObj, null, "�������", true, false, 0, 50, 20);
                map.AddTBInt(FrmAttachmentAttr.FK_Node, 0, "�ڵ����(��sln��Ч)", false, false);

                map.AddTBString(FrmAttachmentAttr.Name, null, "����", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.Exts, null, "Ҫ���ϴ��ĸ�ʽ", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.SaveTo, null, "���浽", true, false, 0, 150, 20);
                map.AddTBString(FrmAttachmentAttr.Sort, null, "���(��Ϊ��)", true, false, 0, 500, 20);

                map.AddTBFloat(FrmAttachmentAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmAttachmentAttr.Y, 5, "Y", false, false);
                map.AddTBFloat(FrmAttachmentAttr.W, 40, "TBWidth", false, false);
                map.AddTBFloat(FrmAttachmentAttr.H, 150, "H", false, false);

                map.AddBoolean(FrmAttachmentAttr.IsUpload, true, "�Ƿ�����ϴ�", false, false);
                map.AddTBInt(FrmAttachmentAttr.IsDelete, 1,
                    "����ɾ������(0=����ɾ��1=ɾ������2=ֻ��ɾ���Լ��ϴ���)", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsDownload, true, "�Ƿ��������", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsOrder, false, "�Ƿ��������", false, false);


                map.AddBoolean(FrmAttachmentAttr.IsAutoSize, true, "�Զ����ƴ�С", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsNote, true, "�Ƿ����ӱ�ע", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsShowTitle, true, "�Ƿ���ʾ������", false, false);
                map.AddTBInt(FrmAttachmentAttr.UploadType, 0, "�ϴ�����0����1���2ָ��", false, false);

                //���ڸ���������Ч.
                map.AddTBInt(FrmAttachmentAttr.CtrlWay, 0, "���Ƴ��ֿ��Ʒ�ʽ0=PK,1=FID,2=ParentID", false, false);
                map.AddTBInt(FrmAttachmentAttr.AthUploadWay, 0, "�����ϴ����Ʒ�ʽ0=�̳�ģʽ,1=Э��ģʽ.", false, false);

                //��������.
                map.AddTBAtParas(3000);

                map.AddTBInt(FrmAttachmentAttr.RowIdx, 0, "RowIdx", false, false);
                map.AddTBInt(FrmAttachmentAttr.GroupID, 0, "GroupID", false, false);
                map.AddTBString(FrmAttachmentAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public bool IsUse = false;
        protected override bool beforeUpdateInsertAction()
        {
            if (this.FK_Node == 0)
            {
                //��Ӧ������µĹ��� by dgq 
                if (!string.IsNullOrEmpty(this.NoOfObj) && this.NoOfObj.Contains(this.FK_MapData))
                    this.MyPK = this.NoOfObj;
                else
                    this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            }
            else
                this.MyPK = this.FK_MapData + "_" + this.NoOfObj + "_" + this.FK_Node;

            return base.beforeUpdateInsertAction();
        }
        protected override bool beforeInsert()
        {
            this.IsWoEnableWF = true;

            this.IsWoEnableSave = false;
            this.IsWoEnableReadonly = false;
            this.IsWoEnableRevise = false;
            this.IsWoEnableViewKeepMark = false;
            this.IsWoEnablePrint = false;
            this.IsWoEnableOver = false;
            this.IsWoEnableSeal = false;
            this.IsWoEnableTemplete = false;

            if (this.FK_Node == 0)
                this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            else
                this.MyPK = this.FK_MapData + "_" + this.NoOfObj + "_" + this.FK_Node;

            return base.beforeInsert();
        }
    }
    /// <summary>
    /// ����s
    /// </summary>
    public class FrmAttachments : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ����s
        /// </summary>
        public FrmAttachments()
        {
        }
        /// <summary>
        /// ����s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachments(string fk_mapdata)
        {
            this.Retrieve(FrmAttachmentAttr.FK_MapData, fk_mapdata, FrmAttachmentAttr.FK_Node, 0);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachment();
            }
        }
        #endregion
    }
}
