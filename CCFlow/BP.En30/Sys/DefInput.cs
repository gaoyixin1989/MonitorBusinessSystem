using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class DefInputAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string AttrKey = "AttrKey";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public const string CurValue = "CurValue";
    }
    /// <summary>
    /// Ĭ��ֵ
    /// </summary>
    public class DefInput : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// �ڵ���
        /// </summary>
        public string EnName
        {
            get
            {
                return this.GetValStringByKey(DefInputAttr.EnName);
            }
            set
            {
                this.SetValByKey(DefInputAttr.EnName, value);
            }
        }
        /// <summary>
        /// �ֶ�
        /// </summary>
        public string AttrKey
        {
            get
            {
                return this.GetValStringByKey(DefInputAttr.AttrKey);
            }
            set
            {
                this.SetValByKey(DefInputAttr.AttrKey, value);
            }
        }
        /// <summary>
        /// ��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DefInputAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(DefInputAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public string CurValue
        {
            get
            {
                return this.GetValStringByKey(DefInputAttr.CurValue);
            }
            set
            {
                this.SetValByKey(DefInputAttr.CurValue, value);
            }
        }
        #endregion

        #region ���췽��

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public DefInput()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_DefInput");
                map.EnType = EnType.Sys;
                map.EnDesc = "Ĭ��ֵ";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(DefInputAttr.MyPK, null, "MyPK", false, false, 0, 50, 20);
                map.AddTBString(DefInputAttr.EnName, null, "ʵ������", false, false, 0, 50, 20);
                map.AddTBString(DefInputAttr.AttrKey, null, "�ֶ�", false, false, 0, 50, 20);
                map.AddTBString(DefInputAttr.CurValue, null, "�ڵ��ı�", false, true, 0, 4000, 10);
                map.AddTBString(DefInputAttr.FK_Emp, null, "��Ա", false, true, 0, 100, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            this.MyPK = BP.DA.DBAccess.GenerGUID();
            this.FK_Emp = BP.Web.WebUser.No;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// Ĭ��ֵs
    /// </summary>
    public class DefInputs : EntitiesNoName
    {
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public DefInputs()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DefInput();
            }
        }
    }
}
