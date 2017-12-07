using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.WF.Port;
using BP.WF;

namespace BP.Sys
{
	/// <summary>
	/// Frm����
	/// </summary>
    public class FrmFieldAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string KeyOfEn = "KeyOfEn";
        /// <summary>
        /// FK_Node
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// FK_MapData
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public const string IsNotNull = "IsNotNull";
        /// <summary>
        /// ������ʽ
        /// </summary>
        public const string RegularExp = "RegularExp";
        /// <summary>
        /// ����
        /// </summary>
        public const string EleType = "EleType";
        /// <summary>
        /// �Ƿ�д�����̱�
        /// </summary>
        public const string IsWriteToFlowTable = "IsWriteToFlowTable";
    }
	/// <summary>
	/// ���ֶη���
	/// </summary>
    public class FrmField : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// Ԫ������.
        /// </summary>
        public string EleType
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.EleType);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.EleType, value);
            }
        }
        /// <summary>
        /// ������ʽ
        /// </summary>
        public string RegularExp
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.RegularExp);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.RegularExp, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.Name);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.Name, value);
            }
        }
        /// <summary>
        /// �Ƿ�Ϊ��
        /// </summary>
        public bool IsNotNull
        {
            get
            {
                return this.GetValBooleanByKey(FrmFieldAttr.IsNotNull);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.IsNotNull, value);
            }
        }
        /// <summary>
        /// �Ƿ�д���������ݱ�
        /// </summary>
        public bool IsWriteToFlowTable
        {
            get
            {
                return this.GetValBooleanByKey(FrmFieldAttr.IsWriteToFlowTable);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.IsWriteToFlowTable, value);
            }
        }
        
        /// <summary>
        /// ��ID
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �ֶ�
        /// </summary>
        public string KeyOfEn
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.KeyOfEn);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.KeyOfEn, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(FrmFieldAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(FrmFieldAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FrmFieldAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public bool UIVisible
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIVisible);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIVisible, value);
            }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool UIIsEnable
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsEnable);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsEnable, value);
            }
        }
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// �Ƿ�������ǩ��?
        /// </summary>
        public bool IsSigan
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.IsSigan);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IsSigan, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���ֶη���
        /// </summary>
        public FrmField()
        {
        }
        /// <summary>
        /// ���ֶη���
        /// </summary>
        /// <param name="no"></param>
        public FrmField(string mypk)
            : base(mypk)
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

                Map map = new Map("Sys_FrmSln");

                map.EnDesc = "���ֶη���";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "4";
                map.IsAutoGenerNo = false;

                map.AddMyPK();

                //�ñ���Ӧ�ı�ID
                map.AddTBString(FrmFieldAttr.FK_Flow, null, "���̱��", true, false, 0, 4, 4);
                map.AddTBInt(FrmFieldAttr.FK_Node, 0, "�ڵ�", true, false);

                map.AddTBString(FrmFieldAttr.FK_MapData, null, "��ID", true, false, 0, 100, 10);
                map.AddTBString(FrmFieldAttr.KeyOfEn, null, "�ֶ�", true, false, 0, 200, 20);
                map.AddTBString(FrmFieldAttr.Name, null, "�ֶ���", true, false, 0, 500, 20);
                map.AddTBString(FrmFieldAttr.EleType, null, "����", true, false, 0, 20, 20);

                //��������.
                map.AddBoolean(MapAttrAttr.UIIsEnable, true, "�Ƿ����", true, true);
                map.AddBoolean(MapAttrAttr.UIVisible, true, "�Ƿ�ɼ�", true, true);
                map.AddBoolean(MapAttrAttr.IsSigan, false, "�Ƿ�ǩ��", true, true);

                // Add 2013-12-26.
                map.AddTBInt(FrmFieldAttr.IsNotNull, 0, "�Ƿ�Ϊ��", true, false);
                map.AddTBString(FrmFieldAttr.RegularExp, null, "������ʽ", true, false, 0, 500, 20);

                // �Ƿ�д�����̱�? 2014-01-26������ǣ�������д��ýڵ�����ݱ�Ȼ��copy���������ݱ���
                // �ڽڵ㷢��ʱ��ccflow�Զ�д�룬д��Ŀ�ľ���Ϊ��
                map.AddTBInt(FrmFieldAttr.IsWriteToFlowTable, 0, "�Ƿ�д�����̱�", true, false);


                map.AddBoolean(MapAttrAttr.IsSigan, false, "�Ƿ�ǩ��", true, true);

                map.AddTBString(MapAttrAttr.DefVal, null, "Ĭ��ֵ", true, false, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            if (string.IsNullOrEmpty(this.EleType))
                this.EleType = FrmEleType.Field;

            this.MyPK = this.FK_MapData + "_" + this.FK_Flow + "_" + this.FK_Node + "_" + this.KeyOfEn + "_" + this.EleType;
            return base.beforeInsert();
        }
    }
	/// <summary>
    /// ���ֶη���s
	/// </summary>
    public class FrmFields : EntitiesMyPK
    {
        public FrmFields()
        {
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        public FrmFields(string fk_mapdata, int nodeID)
        {
            this.Retrieve(FrmFieldAttr.FK_MapData, fk_mapdata, 
                FrmFieldAttr.FK_Node, nodeID,FrmFieldAttr.EleType,  FrmEleType.Field);
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmField();
            }
        }
    }
}
