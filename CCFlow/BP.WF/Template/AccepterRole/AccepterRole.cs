using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF.Template
{
    /// <summary>
    /// �����˹�������
    /// </summary>
    public class AccepterRoleAttr:BP.En.EntityOIDNameAttr
    {
        #region ��������
        /// <summary>
        /// �ڵ���
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ģʽ����
        /// </summary>
        public const string FK_ModeSort = "FK_ModeSort";
        /// <summary>
        /// ģʽ
        /// </summary>
        public const string FK_Mode = "FK_Mode";

        public const string Tag0 = "Tag0";
        public const string Tag1 = "Tag1";
        public const string Tag2 = "Tag2";
        public const string Tag3 = "Tag3";
        public const string Tag4 = "Tag4";
        public const string Tag5 = "Tag5";
        #endregion
    }
    /// <summary>
    /// ������ÿ�������˹������Ϣ.	 
    /// </summary>
    public class AccepterRole : EntityOID
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
        /// �ڵ���
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(AccepterRoleAttr.FK_Node);
            }
            set
            {
                SetValByKey(AccepterRoleAttr.FK_Node, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �����˹���
        /// </summary>
        public AccepterRole() { }
        /// <summary>
        /// �����˹���
        /// </summary>
        /// <param name="oid">�����˹���ID</param>	
        public AccepterRole(int oid)
        {
            this.OID = oid;
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

                Map map = new Map("WF_AccepterRole");
                map.EnDesc =   "�����˹���"; //"�����˹���";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();

                map.AddTBString(AccepterRoleAttr.Name, null, null, true, false, 0, 200, 10, true);
                map.AddTBString(AccepterRoleAttr.FK_Node, null, "�ڵ�", false, true, 0, 100, 10);
                map.AddTBInt(AccepterRoleAttr.FK_Mode, 0, "ģʽ����", false, true);

                map.AddTBString(AccepterRoleAttr.Tag0, null, "Tag0", false, true, 0, 999, 10);
                map.AddTBString(AccepterRoleAttr.Tag1, null, "Tag1", false, true, 0, 999, 10);
                map.AddTBString(AccepterRoleAttr.Tag2, null, "Tag2", false, true, 0, 999, 10);
                map.AddTBString(AccepterRoleAttr.Tag3, null, "Tag3", false, true, 0, 999, 10);
                map.AddTBString(AccepterRoleAttr.Tag4, null, "Tag4", false, true, 0, 999, 10);
                map.AddTBString(AccepterRoleAttr.Tag5, null, "Tag5", false, true, 0, 999, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

       
    }
    /// <summary>
    /// �����˹��򼯺�
    /// </summary>
    public class AccepterRoles : Entities
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new AccepterRole();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �����˹��򼯺�
        /// </summary>
        public AccepterRoles()
        {
        }
        /// <summary>
        /// �����˹��򼯺�.
        /// </summary>
        /// <param name="FlowNo"></param>
        public AccepterRoles(string FK_Node)
        {
            this.Retrieve(AccepterRoleAttr.FK_Node, FK_Node);
        }
        #endregion
    }
}
