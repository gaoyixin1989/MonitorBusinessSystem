using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��ť�¼����� - ��sl �����õ�Ҫ��ͬ��
    /// </summary>
    public enum BtnEventType
    {
        /// <summary>
        /// ����
        /// </summary>
        Disable = 0,
        /// <summary>
        /// ���д洢����
        /// </summary>
        RunSP = 1,
        /// <summary>
        /// ����sql
        /// </summary>
        RunSQL = 2,
        /// <summary>
        /// ִ��URL
        /// </summary>
        RunURL = 3,
        /// <summary>
        /// ����webservices
        /// </summary>
        RunWS = 4,
        /// <summary>
        /// ����Exe�ļ�.
        /// </summary>
        RunExe = 5,
        /// <summary>
        /// ����JS
        /// </summary>
        RunJS =6
    }    /// <summary>
    /// ��ť����
    /// </summary>
    public enum BtnUAC
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// ����Ա
        /// </summary>
        ByEmp,
        /// <summary>
        /// ����λ
        /// </summary>
        ByStation,
        /// <summary>
        /// ������
        /// </summary>
        ByDept,
        /// <summary>
        /// ��sql
        /// </summary>
        BySQL
    }
    /// <summary>
    /// ��ť����
    /// </summary>
    public enum BtnType
    {
        /// <summary>
        /// ����
        /// </summary>
        Save=0,
        /// <summary>
        /// ��ӡ
        /// </summary>
        Print=1,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete=2,
        /// <summary>
        /// ����
        /// </summary>
        Add=3,
        /// <summary>
        /// �Զ���
        /// </summary>
        Self=100
    }
    /// <summary>
    /// ��ť
    /// </summary>
    public class FrmBtnAttr : EntityMyPKAttr
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
        /// ���
        /// </summary>
        public const string BtnType = "BtnType";
        /// <summary>
        /// ��ɫ
        /// </summary>
        public const string IsView = "IsView";
        /// <summary>
        /// ���
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// ������
        /// </summary>
        public const string EventContext = "EventContext";
        /// <summary>
        /// ����
        /// </summary>
        public const string UACContext = "UACContext";
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public const string EventType = "EventType";
        /// <summary>
        /// ��������
        /// </summary>
        public const string UAC = "UAC";
        /// <summary>
        /// MsgOK
        /// </summary>
        public const string MsgOK = "MsgOK";
        /// <summary>
        /// MsgErr
        /// </summary>
        public const string MsgErr = "MsgErr";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
    }
    /// <summary>
    /// ��ť
    /// </summary>
    public class FrmBtn : EntityMyPK
    {
        #region ����
        public string MsgOK
        {
            get
            {
                return this.GetValStringByKey(FrmBtnAttr.MsgOK);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.MsgOK, value);
            }
        }
        public string MsgErr
        {
            get
            {
                return this.GetValStringByKey(FrmBtnAttr.MsgErr);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.MsgErr, value);
            }
        }
        /// <summary>
        /// EventContext
        /// </summary>
        public string EventContext
        {
            get
            {
                return this.GetValStringByKey(FrmBtnAttr.EventContext).Replace("#", "@");
                //return this.GetValStringByKey(FrmBtnAttr.EventContext);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.EventContext, value);
            }
        }
        public string IsViewHtml
        {
            get
            {
                return PubClass.ToHtmlColor(this.IsView);
            }
        }
        /// <summary>
        /// IsView
        /// </summary>
        public string IsView
        {
            get
            {
                return this.GetValStringByKey(FrmBtnAttr.IsView);
            }
            set
            {
                switch (value)
                {
                    case "#FF000000":
                        this.SetValByKey(FrmBtnAttr.IsView, "Red");
                        return;
                    default:
                        break;
                }
                this.SetValByKey(FrmBtnAttr.IsView, value);
            }
        }
        public string UACContext
        {
            get
            {
                return this.GetValStringByKey(FrmBtnAttr.UACContext);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.UACContext, value);
            }
        }
        public bool EventType
        {
            get
            {
                return this.GetValBooleanByKey(FrmBtnAttr.EventType);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.EventType, value);
            }
        }
        public bool UAC
        {
            get
            {
                return this.GetValBooleanByKey(FrmBtnAttr.UAC);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.UAC, value);
            }
        }
        /// <summary>
        /// IsEnable
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(FrmBtnAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmBtnAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmBtnAttr.X);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.X, value);
            }
        }
        public BtnEventType HisBtnEventType
        {
            get
            {
                return (BtnEventType)this.GetValIntByKey(FrmBtnAttr.EventType);
            }
        }
        /// <summary>
        /// BtnType
        /// </summary>
        public int BtnType
        {
            get
            {
                return this.GetValIntByKey(FrmBtnAttr.BtnType);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.BtnType, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmBtnAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return this.GetValStrByKey(FrmBtnAttr.Text);
            }
            set
            {
                this.SetValByKey(FrmBtnAttr.Text, value);
            }
        }
        public string TextHtml
        {
            get
            {
                if (this.EventType)
                    return "<b>" + this.GetValStrByKey(FrmBtnAttr.Text).Replace("@","<br>") + "</b>";
                else
                    return this.GetValStrByKey(FrmBtnAttr.Text).Replace("@", "<br>");
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ť
        /// </summary>
        public FrmBtn()
        {
        }
        /// <summary>
        /// ��ť
        /// </summary>
        /// <param name="mypk"></param>
        public FrmBtn(string mypk)
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

                Map map = new Map("Sys_FrmBtn");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��ť";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(FrmBtnAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmBtnAttr.Text, null, "��ǩ", true, false, 0, 3900, 20);

                map.AddTBFloat(FrmBtnAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmBtnAttr.Y, 5, "Y", false, false);

                map.AddTBInt(FrmBtnAttr.IsView, 0, "�Ƿ�ɼ�", false, false);
                map.AddTBInt(FrmBtnAttr.IsEnable, 0, "�Ƿ�����", false, false);

                map.AddTBInt(FrmBtnAttr.BtnType, 0, "����", false, false);

                map.AddTBInt(FrmBtnAttr.UAC, 0, "��������", false, false);
                map.AddTBString(FrmBtnAttr.UACContext, null, "��������", true, false, 0, 3900, 20);

                map.AddTBInt(FrmBtnAttr.EventType, 0, "�¼�����", false, false);
                map.AddTBString(FrmBtnAttr.EventContext, null, "�¼�����", true, false, 0, 3900, 20);

                map.AddTBString(FrmBtnAttr.MsgOK, null, "���гɹ���ʾ", true, false, 0, 500, 20);
                map.AddTBString(FrmBtnAttr.MsgErr, null, "����ʧ����ʾ", true, false, 0, 500, 20);

                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);
             
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ťs
    /// </summary>
    public class FrmBtns : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ��ťs
        /// </summary>
        public FrmBtns()
        {
        }
        /// <summary>
        /// ��ťs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmBtns(string fk_mapdata)
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
                return new FrmBtn();
            }
        }
        #endregion
    }
}
