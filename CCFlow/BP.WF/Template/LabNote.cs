using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF.Template;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// ��ǩ����
    /// </summary>
    public class LabNoteAttr:BP.En.EntityOIDNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// y
        /// </summary>
        public const string Y = "Y";
        #endregion
    }
    /// <summary>
    /// ��ǩ.	 
    /// </summary>
    public class LabNote : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                return uac;
            }
        }

        /// <summary>
        /// x
        /// </summary>
        public int X
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.X);
            }
            set
            {
                this.SetValByKey(NodeAttr.X, value);
            }
        }

        /// <summary>
        /// y
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Y);
            }
            set
            {
                this.SetValByKey(NodeAttr.Y, value);
            }
        }
        /// <summary>
        /// ��ǩ��������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.Name);
            }
            set
            {
                SetValByKey(NodeAttr.Name, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��ǩ
        /// </summary>
        public LabNote() { }
        /// <summary>
        /// ��ǩ
        /// </summary>
        /// <param name="_oid">��ǩID</param>	
        public LabNote(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
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

                Map map = new Map("WF_LabNote");
                map.EnDesc =   "��ǩ"; // "��ǩ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();

                map.AddTBString(NodeAttr.Name, null, null, true, false, 0, 3000, 10, true);
                map.AddTBString(NodeAttr.FK_Flow, null, "����", false, true, 0, 100, 10);

                map.AddTBInt(NodeAttr.X, 0, "X����", false, false);
                map.AddTBInt(NodeAttr.Y, 0, "Y����", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            this.MyPK = BP.DA.DBAccess.GenerOID().ToString();
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// ��ǩ����
    /// </summary>
    public class LabNotes : Entities
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new LabNote();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ǩ����
        /// </summary>
        public LabNotes()
        {
        }
        /// <summary>
        /// ��ǩ����.
        /// </summary>
        /// <param name="FlowNo"></param>
        public LabNotes(string fk_flow)
        {
            this.Retrieve(NodeAttr.FK_Flow, fk_flow);
        }
        #endregion
    }
}
