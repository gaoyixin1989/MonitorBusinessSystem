using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.WF.Template;
using BP.WF;
namespace BP.Sys
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum FWCAth
    {
        /// <summary>
        /// ʹ�ø���
        /// </summary>
        None,
        /// <summary>
        /// �฽��
        /// </summary>
        MinAth,
        /// <summary>
        /// ������
        /// </summary>
        SingerAth,
        /// <summary>
        /// ͼƬ����
        /// </summary>
        ImgAth
    }
    /// <summary>
    /// ����
    /// </summary>
    public enum FWCType
    {
        /// <summary>
        /// ������
        /// </summary>
        Check,
        /// <summary>
        /// ��־���
        /// </summary>
        DailyLog,
        /// <summary>
        /// �ܱ�
        /// </summary>
        WeekLog,
        /// <summary>
        /// �±�
        /// </summary>
        MonthLog
    }
    /// <summary>
    /// ��ʾ��ʽ
    /// </summary>
    public enum FrmWorkShowModel
    {
        /// <summary>
        /// ���
        /// </summary>
        Table,
        /// <summary>
        /// ������ʾ
        /// </summary>
        Free
    }
    /// <summary>
    /// ������״̬
    /// </summary>
    public enum FrmWorkCheckSta
    {
        /// <summary>
        /// ������
        /// </summary>
        Disable,
        /// <summary>
        /// ����
        /// </summary>
        Enable,
        /// <summary>
        /// ֻ��
        /// </summary>
        Readonly
    }
    /// <summary>
    /// ������
    /// </summary>
    public class FrmWorkCheckAttr : EntityNoAttr
    {
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string FWCSta = "FWCSta";
        /// <summary>
        /// X
        /// </summary>
        public const string FWC_X = "FWC_X";
        /// <summary>
        /// Y
        /// </summary>
        public const string FWC_Y = "FWC_Y";
        /// <summary>
        /// H
        /// </summary>
        public const string FWC_H = "FWC_H";
        /// <summary>
        /// W
        /// </summary>
        public const string FWC_W = "FWC_W";
        /// <summary>
        /// Ӧ������
        /// </summary>
        public const string FWCType = "FWCType";
        /// <summary>
        /// ����
        /// </summary>
        public const string FWCAth = "FWCAth";
        /// <summary>
        /// ��ʾ��ʽ.
        /// </summary>
        public const string FWCShowModel = "FWCShowModel";
        /// <summary>
        /// �켣ͼ�Ƿ���ʾ?
        /// </summary>
        public const string FWCTrackEnable = "FWCTrackEnable";
        /// <summary>
        /// ��ʷ�����Ϣ�Ƿ���ʾ?
        /// </summary>
        public const string FWCListEnable = "FWCListEnable";
        /// <summary>
        /// �Ƿ���ʾ���еĲ��裿
        /// </summary>
        public const string FWCIsShowAllStep = "FWCIsShowAllStep";
        /// <summary>
        /// Ĭ�������Ϣ
        /// </summary>
        public const string FWCDefInfo = "FWCDefInfo";
        /// <summary>
        /// �ڵ��������
        /// </summary>
        public const string FWCNodeName = "FWCNodeName";

        /// <summary>
        /// ����û�δ����Ƿ���Ĭ�������䣿
        /// </summary>
        public const string FWCIsFullInfo = "FWCIsFullInfo";
        /// <summary>
        /// ��������(��ˣ��󶨣����ģ���ʾ)
        /// </summary>
        public const string FWCOpLabel = "FWCOpLabel";
        /// <summary>
        /// �������Ƿ���ʾ����ǩ��
        /// </summary>
        public const string SigantureEnabel = "SigantureEnabel";
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public const string FWCFields = "FWCFields";
    }
    /// <summary>
    /// ������
    /// </summary>
    public class FrmWorkCheck : Entity
    {
        #region ����
        public string No
        {
            get
            {
                return "ND" + this.NodeID;
            }
            set
            {
                string nodeID = value.Replace("ND", "");
                this.NodeID = int.Parse(nodeID);
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public FrmWorkCheckSta HisFrmWorkCheckSta
        {
            get
            {
                return (FrmWorkCheckSta)this.GetValIntByKey(FrmWorkCheckAttr.FWCSta);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCSta, (int)value);
            }
        }
        /// <summary>
        /// ��ʾ��ʽ(0=���,1=����.)
        /// </summary>
        public FrmWorkShowModel HisFrmWorkShowModel
        {
            get
            {
                return (FrmWorkShowModel)this.GetValIntByKey(FrmWorkCheckAttr.FWCShowModel);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCShowModel, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FWCAth FWCAth
        {
            get
            {
                return (FWCAth)this.GetValIntByKey(FrmWorkCheckAttr.FWCAth);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCAth, (int)value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public FWCType HisFrmWorkCheckType
        {
            get
            {
                return (FWCType)this.GetValIntByKey(FrmWorkCheckAttr.FWCType);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCType, (int)value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float FWC_Y
        {
            get
            {
                return this.GetValFloatByKey(FrmWorkCheckAttr.FWC_Y);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWC_Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float FWC_X
        {
            get
            {
                return this.GetValFloatByKey(FrmWorkCheckAttr.FWC_X);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWC_X, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float FWC_W
        {
            get
            {
                return this.GetValFloatByKey(FrmWorkCheckAttr.FWC_W);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWC_W, value);
            }
        }
        public string FWC_Wstr
        {
            get
            {
                if (this.FWC_W == 0)
                    return "100%";
                return this.FWC_W + "px";
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float FWC_H
        {
            get
            {
                return this.GetValFloatByKey(FrmWorkCheckAttr.FWC_H);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWC_H, value);
            }
        }
        public string FWC_Hstr
        {
            get
            {
                if (this.FWC_H == 0)
                    return "100%";
                return this.FWC_H + "px";
            }
        }
        /// <summary>
        /// �켣ͼ�Ƿ���ʾ?
        /// </summary>
        public bool FWCTrackEnable
        {
            get
            {
                return this.GetValBooleanByKey(FrmWorkCheckAttr.FWCTrackEnable);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCTrackEnable, value);
            }
        }
        /// <summary>
        /// ��ʷ�����Ϣ�Ƿ���ʾ?
        /// </summary>
        public bool FWCListEnable
        {
            get
            {
                return this.GetValBooleanByKey(FrmWorkCheckAttr.FWCListEnable);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCListEnable, value);
            }
        }
        /// <summary>
        /// �ڹ켣�����Ƿ���ʾ���еĲ��裿
        /// </summary>
        public bool FWCIsShowAllStep
        {
            get
            {
                return this.GetValBooleanByKey(FrmWorkCheckAttr.FWCIsShowAllStep);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCIsShowAllStep, value);
            }
        }
        /// <summary>
        /// ����û�δ����Ƿ���Ĭ��������?
        /// </summary>
        public bool FWCIsFullInfo
        {
            get
            {
                return this.GetValBooleanByKey(FrmWorkCheckAttr.FWCIsFullInfo);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCIsFullInfo, value);
            }
        }
        /// <summary>
        /// Ĭ�������Ϣ
        /// </summary>
        public string FWCDefInfo
        {
            get
            {
                return this.GetValStringByKey(FrmWorkCheckAttr.FWCDefInfo);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCDefInfo, value);
            }
        }
        /// <summary>
        /// �ڵ�����.
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey("Name");
            }
        }
        /// <summary>
        /// �ڵ�������ƣ����Ϊ����ȡ�ڵ�����.
        /// </summary>
        public string FWCNodeName
        {
            get
            {
                string str = this.GetValStringByKey(FrmWorkCheckAttr.FWCNodeName);
                if (string.IsNullOrEmpty(str))
                    return this.Name;
                return str;
            }
        }
        /// <summary>
        /// ��������(��ˣ��󶨣����ģ���ʾ)
        /// </summary>
        public string FWCOpLabel
        {
            get
            {
                return this.GetValStringByKey(FrmWorkCheckAttr.FWCOpLabel);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCOpLabel, value);
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string FWCFields
        {
            get
            {
                return this.GetValStringByKey(FrmWorkCheckAttr.FWCFields);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.FWCFields, value);
            }
        }
        /// <summary>
        /// �Ƿ���ʾ����ǩ����
        /// </summary>
        public bool SigantureEnabel
        {
            get
            {
                return this.GetValBooleanByKey(FrmWorkCheckAttr.SigantureEnabel);
            }
            set
            {
                this.SetValByKey(FrmWorkCheckAttr.SigantureEnabel, value);
            }
        }
        #endregion

        #region ���췽��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                uac.IsDelete = false;
                uac.IsInsert = false;
                return uac;
            }
        }
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public FrmWorkCheck()
        {
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="no"></param>
        public FrmWorkCheck(string mapData)
        {
            if (mapData.Contains("ND") == false)
            {
                this.HisFrmWorkCheckSta = FrmWorkCheckSta.Disable;
                return;
            }

            string mapdata = mapData.Replace("ND", "");
            if (DataType.IsNumStr(mapdata) == false)
            {
                this.HisFrmWorkCheckSta = FrmWorkCheckSta.Disable;
                return;
            }

            try
            {
                this.NodeID = int.Parse(mapdata);
            }
            catch
            {
                return;
            }
            this.Retrieve();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="no"></param>
        public FrmWorkCheck(int nodeID)
        {
            this.NodeID = nodeID;
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
                Map map = new Map("WF_Node");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "������";
                map.EnType = EnType.Sys;

                map.AddTBIntPK(NodeAttr.NodeID, 0, "�ڵ�ID", true, true);
                map.AddTBString(NodeAttr.Name, null, "�ڵ�����", true, true, 0, 100, 10);

                #region �˴������ NodeSheet���еģ�map �����ò���ҲҪ���.
                map.AddDDLSysEnum(FrmWorkCheckAttr.FWCSta, (int)FrmWorkCheckSta.Disable, "������״̬",
                   true, true, FrmWorkCheckAttr.FWCSta, "@0=����@1=����@2=ֻ��");
                map.AddDDLSysEnum(FrmWorkCheckAttr.FWCShowModel, (int)FrmWorkShowModel.Free, "��ʾ��ʽ",
                    true, true, FrmWorkCheckAttr.FWCShowModel, "@0=���ʽ@1=����ģʽ"); //��������ʱû����.

                map.AddDDLSysEnum(FrmWorkCheckAttr.FWCType, (int)FWCType.Check, "������", true, true,
                    FrmWorkCheckAttr.FWCType, "@0=������@1=��־���@2=�ܱ����@3=�±����");

                map.AddTBString(FrmWorkCheckAttr.FWCNodeName, null, "�ڵ��������", true, false, 0, 100, 10);

                map.AddDDLSysEnum(FrmWorkCheckAttr.FWCAth, (int)FWCAth.None, "�����ϴ�", true, true,
                   FrmWorkCheckAttr.FWCAth, "@0=������@1=�฽��@2=������(�ݲ�֧��)@3=ͼƬ����(�ݲ�֧��)");
                map.SetHelperAlert(FrmWorkCheckAttr.FWCAth,
                    "������ڼ䣬�Ƿ������ϴ�����������ʲô���ĸ�����ע�⣺�����������ڽڵ�������á�"); //ʹ��alert�ķ�ʽ��ʾ������Ϣ.

                map.AddBoolean(FrmWorkCheckAttr.FWCTrackEnable, true, "�켣ͼ�Ƿ���ʾ��", true, true, false);


                map.AddBoolean(FrmWorkCheckAttr.FWCListEnable, true, "��ʷ�����Ϣ�Ƿ���ʾ��(��,��ʷ��Ϣ�����������)", true, true, true);
                map.AddBoolean(FrmWorkCheckAttr.FWCIsShowAllStep, false, "�ڹ켣�����Ƿ���ʾ���еĲ��裿", true, true);

                map.AddTBString(FrmWorkCheckAttr.FWCOpLabel, "���", "��������(���/����/��ʾ)", true, false, 0, 50, 10);
                map.AddTBString(FrmWorkCheckAttr.FWCDefInfo, "ͬ��", "Ĭ�������Ϣ", true, false, 0, 50, 10);
                map.AddBoolean(FrmWorkCheckAttr.SigantureEnabel, false, "�������Ƿ���ʾΪͼƬǩ����", true, true);
                map.AddBoolean(FrmWorkCheckAttr.FWCIsFullInfo, true, "����û�δ����Ƿ���Ĭ�������䣿", true, true, true);


                map.AddTBFloat(FrmWorkCheckAttr.FWC_X, 5, "λ��X", true, false);
                map.AddTBFloat(FrmWorkCheckAttr.FWC_Y, 5, "λ��Y", true, false);

                map.AddTBFloat(FrmWorkCheckAttr.FWC_H, 300, "�߶�", true, false);
                map.AddTBFloat(FrmWorkCheckAttr.FWC_W, 400, "���", true, false);


                map.AddTBString(FrmWorkCheckAttr.FWCFields, null, "������ʽ�ֶ�", true, false, 0, 1000, 10);

                #endregion �˴������ NodeSheet���еģ�map �����ò���ҲҪ���.

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            FrmAttachment workCheckAth = new FrmAttachment();
            bool isHave = workCheckAth.RetrieveByAttr(FrmAttachmentAttr.MyPK, this.NodeID + "_FrmWorkCheck");
            //������������
            if (isHave == false)
            {
                workCheckAth = new FrmAttachment();
                /*���û�в�ѯ����,���п�����û�д���.*/
                workCheckAth.MyPK = this.NodeID + "_FrmWorkCheck";
                workCheckAth.FK_MapData = this.NodeID.ToString();
                workCheckAth.NoOfObj = this.NodeID + "_FrmWorkCheck";
                workCheckAth.Exts = "*.*";

                //�洢·��.
                workCheckAth.SaveTo = "/DataUser/UploadFile/";
                workCheckAth.IsNote = false; //����ʾnote�ֶ�.
                workCheckAth.IsVisable = false; // ������form �ϲ��ɼ�.

                //λ��.
                workCheckAth.X = (float)94.09;
                workCheckAth.Y = (float)333.18;
                workCheckAth.W = (float)626.36;
                workCheckAth.H = (float)150;

                //�฽��.
                workCheckAth.UploadType = AttachmentUploadType.Multi;
                workCheckAth.Name = "������";
                workCheckAth.SetValByKey("AtPara", "@IsWoEnablePageset=1@IsWoEnablePrint=1@IsWoEnableViewModel=1@IsWoEnableReadonly=0@IsWoEnableSave=1@IsWoEnableWF=1@IsWoEnableProperty=1@IsWoEnableRevise=1@IsWoEnableIntoKeepMarkModel=1@FastKeyIsEnable=0@IsWoEnableViewKeepMark=1@FastKeyGenerRole=@IsWoEnableTemplete=1");
                workCheckAth.Insert();
            }   
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ������s
    /// </summary>
    public class FrmWorkChecks : Entities
    {
        #region ����
        /// <summary>
        /// ������s
        /// </summary>
        public FrmWorkChecks()
        {
        }
        /// <summary>
        /// ������s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmWorkChecks(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve("No", fk_mapdata);
            else
                this.RetrieveFromCash("No", (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmWorkCheck();
            }
        }
        #endregion
    }
}
