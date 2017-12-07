using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;
using BP.WF.Template;
using BP.WF;

namespace BP.Sys
{
	/// <summary>
	/// Frm����
	/// </summary>
    public class SysFormAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��������
        /// </summary>
        public const string FormRunType = "FormRunType";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_FormTree = "FK_FormTree";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// PTable
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// DBURL
        /// </summary>
        public const string DBURL = "DBURL";
    }
	/// <summary>
	/// ϵͳ��
	/// </summary>
    public class SysForm : EntityNoName
    {
        #region ��������
        public string PTable
        {
            get
            {
                return this.GetValStringByKey(SysFormAttr.PTable);
            }
            set
            {
                this.SetValByKey(SysFormAttr.PTable, value);
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStringByKey(SysFormAttr.URL);
            }
            set
            {
                this.SetValByKey(SysFormAttr.URL, value);
            }
        }
        public FormRunType HisFormRunType
        {
            get
            {
                return (FormRunType)this.GetValIntByKey(SysFormAttr.FormRunType);
            }
            set
            {
                this.SetValByKey(SysFormAttr.FormRunType, (int)value);
            }
        }
        public string FK_FormTree
        {
            get
            {
                return this.GetValStringByKey(SysFormAttr.FK_FormTree);
            }
            set
            {
                this.SetValByKey(SysFormAttr.FK_FormTree, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// Frm
        /// </summary>
        public SysForm()
        {
        }
        /// <summary>
        /// Frm
        /// </summary>
        /// <param name="no"></param>
        public SysForm(string no)
            : base(no)
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

                Map map = new Map("Sys_MapData");

                map.EnDesc = "ϵͳ��";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "4";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(SysFormAttr.No, null, null, true, true, 1, 4, 4);
                map.AddTBString(SysFormAttr.Name, null, null, true, false, 0, 50, 10);

                //������������.
                map.AddDDLSysEnum(SysFormAttr.FormRunType, (int)BP.WF.FormRunType.FreeForm, "��������",
                    true, false, SysFormAttr.FormRunType, "@0=ɵ�ϱ�@1=���ɱ�@2=�Զ����@4=Silverlight��");

                //�ñ���Ӧ�������
                map.AddTBString(SysFormAttr.PTable, null, "�����", true, false, 0, 50, 10);

                // FormRunType=�Զ����ʱ, ���ֶ���Ч. 
                map.AddTBString(SysFormAttr.URL, null, "Url", true, false, 0, 50, 10);

                //ϵͳ�����.
                map.AddTBString(SysFormAttr.FK_FormTree, null, "����", true, false, 0, 10, 20);

                map.AddTBInt(Sys.MapDataAttr.FrmW, 900, "ϵͳ�����", true, false);
                map.AddTBInt(Sys.MapDataAttr.FrmH, 1200, "ϵͳ���߶�", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        public int FrmW
        {
            get
            {
                return this.GetValIntByKey(Sys.MapDataAttr.FrmW);
            }
        }
        public int FrmH
        {
            get
            {
                return this.GetValIntByKey(Sys.MapDataAttr.FrmH);
            }
        }
        
        #endregion
    }
	/// <summary>
    /// ϵͳ��s
	/// </summary>
    public class SysForms : EntitiesNoName
    {
        /// <summary>
        /// Frm
        /// </summary>
        public SysForms()
        {

        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SysForm();
            }
        }
    }
}
