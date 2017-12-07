using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// ��������Ϣ����
	/// </summary>
    public class SelectInfoAttr
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ���ܽڵ�
        /// </summary>
        public const string AcceptNodeID = "AcceptNodeID";
        /// <summary>
        /// left��Ϣ
        /// </summary>
        public const string InfoLeft = "InfoLeft";
        /// <summary>
        /// �м���Ϣ
        /// </summary>
        public const string InfoCenter = "InfoCenter";
        public const string InfoRight = "InfoRight";
        public const string AccType = "AccType";
    }
	/// <summary>
	/// ��������Ϣ
	/// </summary>
    public class SelectInfo : EntityMyPK
    {
        #region ��������
        /// <summary>
        ///����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(SelectInfoAttr.WorkID);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.WorkID, value);
            }
        }
        /// <summary>
        ///ѡ��ڵ�
        /// </summary>
        public int AcceptNodeID
        {
            get
            {
                return this.GetValIntByKey(SelectInfoAttr.AcceptNodeID);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.AcceptNodeID, value);
            }
        }
        public int AccType
        {
            get
            {
                return this.GetValIntByKey(SelectInfoAttr.AccType);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.AccType, value);
            }
        }
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public string Info
        {
            get
            {
                return this.GetValStringByKey(SelectInfoAttr.InfoLeft);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.InfoLeft, value);
            }
        }
        public string InfoCenter
        {
            get
            {
                return this.GetValStringByKey(SelectInfoAttr.InfoCenter);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.InfoCenter, value);
            }
        }
        public string InfoRight
        {
            get
            {
                return this.GetValStringByKey(SelectInfoAttr.InfoRight);
            }
            set
            {
                this.SetValByKey(SelectInfoAttr.InfoRight, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public SelectInfo()
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_SelectInfo");
                map.EnDesc = "ѡ�����/�����˽ڵ���Ϣ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();
                map.AddTBInt(SelectInfoAttr.AcceptNodeID, 0, "���ܽڵ�", true, false);
                map.AddTBInt(SelectInfoAttr.WorkID, 0, "����ID", true, false);
                map.AddTBString(SelectInfoAttr.InfoLeft, null, "InfoLeft", true, false, 0, 200, 10);
                map.AddTBString(SelectInfoAttr.InfoCenter, null, "InfoCenter", true, false, 0, 200, 10);
                map.AddTBString(SelectInfoAttr.InfoRight, null, "InfoLeft", true, false, 0, 200, 10);
                map.AddTBInt(SelectAccperAttr.AccType, 0, "����(@0=������@1=������)", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.AcceptNodeID + "_" + this.WorkID + "_" + this.AccType; ;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// ��������Ϣ
	/// </summary>
    public class SelectInfos : EntitiesMyPK
    {
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public SelectInfos() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SelectInfo();
            }
        }
    }
}
