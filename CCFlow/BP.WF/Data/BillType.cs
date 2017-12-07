using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF.Data
{
	/// <summary>
    ///  ��������
	/// </summary>
    public class BillType : EntityNoName
    {
        #region ����.
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey("FK_Flow");
            }
            set
            {
                this.SetValByKey("FK_Flow", value);
            }
        }
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion ����.

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public BillType()
        {
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_No"></param>
        public BillType(string _No) : base(_No) { }
        /// <summary>
        /// ��������Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_BillType");
                map.EnDesc = "��������";
                map.CodeStruct = "2";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(SimpleNoNameAttr.Name, null, "����", true, false, 1, 50, 50);
                map.AddTBString("FK_Flow", null, "����", true, false, 1, 50, 50);

                map.AddTBInt("IDX", 0, "IDX", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��������
	/// </summary>
    public class BillTypes : SimpleNoNames
    {
        /// <summary>
        /// ��������s
        /// </summary>
        public BillTypes() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BillType();
            }
        }
    }
}
