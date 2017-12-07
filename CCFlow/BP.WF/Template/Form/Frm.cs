using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// Frm����
	/// </summary>
    public class FrmAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FormRunType = "FormRunType";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// �Ƿ���Ը���
        /// </summary>
        public const string IsUpdate = "IsUpdate";
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
	/// ��
	/// </summary>
    public class Frm : EntityNoName
    {
        #region ��������
        public FrmNode HisFrmNode = null;
        public string PTable
        {
            get
            {
                return this.GetValStringByKey(FrmAttr.PTable);
            }
            set
            {
                this.SetValByKey(FrmAttr.PTable, value);
            }
        }
        public string FK_Flow11
        {
            get
            {
                return this.GetValStringByKey(FrmAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FrmAttr.FK_Flow, value);
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStringByKey(FrmAttr.URL);
            }
            set
            {
                this.SetValByKey(FrmAttr.URL, value);
            }
        }
        public FormRunType HisFormRunType
        {
            get
            {
                return (FormRunType)this.GetValIntByKey(FrmAttr.FormRunType);
            }
            set
            {
                this.SetValByKey(FrmAttr.FormRunType, (int)value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// Frm
        /// </summary>
        public Frm()
        {
        }
        /// <summary>
        /// Frm
        /// </summary>
        /// <param name="no"></param>
        public Frm(string no)
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

                map.EnDesc = "����";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "4";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(FrmAttr.No, null, null, true, true, 1, 4, 4);
                map.AddTBString(FrmAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddTBString(FrmAttr.FK_Flow, null, "���̱�����:FK_Flow", true, false, 0, 50, 10);
                map.AddDDLSysEnum(FrmAttr.FormRunType, 0, "���̱�����:��������", true, false, FrmAttr.FormRunType);
                map.AddTBString(FrmAttr.PTable, null, "�����", true, false, 0, 50, 10);
                map.AddTBInt(FrmAttr.DBURL, 0, "DBURL", true, false);

                // ����Ǹ��Զ����.
                map.AddTBString(FrmAttr.URL, null, "Url", true, false, 0, 50, 10);

                //�����.
                map.AddTBString(MapDataAttr.FK_FrmSort, "01", "�����", true, false, 0, 500, 20);

                map.AddTBInt(BP.Sys.MapDataAttr.FrmW, 900, "�����", true, false);
                map.AddTBInt(BP.Sys.MapDataAttr.FrmH, 1200, "���߶�", true, false);

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
                return this.GetValIntByKey(BP.Sys.MapDataAttr.FrmH);
            }
        }
        
        #endregion
    }
	/// <summary>
    /// ��s
	/// </summary>
    public class Frms : EntitiesNoName
    {
        /// <summary>
        /// Frm
        /// </summary>
        public Frms()
        {
        }
        /// <summary>
        /// Frm
        /// </summary>
        /// <param name="fk_flow"></param>
        public Frms(string fk_flow)
        {
            this.Retrieve(FrmAttr.FK_Flow, fk_flow);
        }
        public Frms(int fk_node)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(FrmAttr.No, "SELECT FK_Frm FROM WF_FrmNode WHERE FK_Node=" + fk_node);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Frm();
            }
        }
    }
}
