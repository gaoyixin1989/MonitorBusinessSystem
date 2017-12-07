using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.WF.Data
{
    /// <summary>
    /// ���ɵ�����
    /// </summary>
    public enum BillFileType
    {
        /// <summary>
        /// Word
        /// </summary>
        Word=0,
        PDF=1,
        Excel=2,
        Html=3,
        RuiLang=5
    }
    /// <summary>
    /// ����ģ������
    /// </summary>
    public class BillTemplateAttr:BP.En.EntityNoNameAttr
    {
        public const string Url = "Url";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �Ƿ���Ҫ�ʹ�
        /// </summary>
        public const string IsNeedSend = "IsNeedSend";
        /// <summary>
        /// Ϊ���ɵ���ʹ��
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// Ҫ�ų����ֶ�
        /// </summary>
        public const string ExpField = "ExpField";
        /// <summary>
        /// Ҫ�滻��ֵ
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FK_BillType = "FK_BillType";
        /// <summary>
        /// �Ƿ�����PDF
        /// </summary>
        public const string BillFileType = "BillFileType";
    }
	/// <summary>
	/// ����ģ��
	/// </summary>
	public class BillTemplate : EntityNoName
    {
        #region  ����
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_BillType
        {
            get
            {
                return this.GetValStringByKey(BillTemplateAttr.FK_BillType);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.FK_BillType, value);
            }
        }
        /// <summary>
        /// Ҫ�滻��ֵ
        /// </summary>
        public string ReplaceVal
        {
            get
            {
                return this.GetValStringByKey(BillTemplateAttr.ReplaceVal);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.ReplaceVal, value);
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
        /// <summary>
        /// ���
        /// </summary>
        public new string No
        {
            get
            {
                string no = this.GetValStrByKey("No");
                no = no.Replace("\n", "");
                no = no.Replace(" ", "");
                return no;
            }
            set
            {
                this.SetValByKey("No", value);
                this.SetValByKey(BillTemplateAttr.Url, value);
            }
        }
        /// <summary>
        /// ���ɵĵ�������
        /// </summary>
        public BillFileType HisBillFileType
        {
            get
            {
                return (BillFileType)this.GetValIntByKey(BillTemplateAttr.BillFileType);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.BillFileType, (int)value);
            }
        }
        /// <summary>
        /// �򿪵�����
        /// </summary>
        public string Url
        {
            get
            {
                string s= this.GetValStrByKey(BillTemplateAttr.Url);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.Url, value);
            }
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                Node nd = new Node(this.NodeID);
                return nd.Name;
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(BillTemplateAttr.NodeID);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.NodeID, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ҫ�ʹ�
        /// </summary>
        public bool IsNeedSend_del
        {
            get
            {
                return this.GetValBooleanByKey(BillTemplateAttr.IsNeedSend); 
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����ģ��
		/// </summary>
		public BillTemplate(){}
        public BillTemplate(string no):base(no.Replace( "\n","" ).Trim() ) 
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
                Map map = new Map("WF_BillTemplate");
                map.EnDesc = "����ģ��"; // "����ģ��";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "6";

                map.AddTBStringPK(BillTemplateAttr.No, null, "No", true, false, 1, 300, 6);
                map.AddTBString(BillTemplateAttr.Name, null, "Name", true, false, 0, 200, 20);
                map.AddTBString(BillTemplateAttr.Url, null, "URL", true, false, 0, 200, 20);
                map.AddTBInt(BillTemplateAttr.NodeID, 0, "NodeID", true, false);

                map.AddDDLSysEnum(BillTemplateAttr.BillFileType, 0, "���ɵ��ļ�����", true, false,
                    "BillFileType","@0=Word@1=PDF@2=Excel(δ���)@3=Html(δ���)@5=���˱���");

                map.AddTBString(BillTemplateAttr.FK_BillType, null, "��������", true, false, 0, 4, 4);

                map.AddTBString("IDX", null, "IDX", false, false, 0, 200, 20);
                map.AddTBString(BillTemplateAttr.ExpField, null, "Ҫ�ų����ֶ�", false, false, 0, 800, 20);
                map.AddTBString(BillTemplateAttr.ReplaceVal, null, "Ҫ�滻��ֵ", false, false, 0, 3000, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
    /// ����ģ��s
	/// </summary>
	public class BillTemplates: EntitiesNoName
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BillTemplate();
			}
		}
		/// <summary>
		/// ����ģ��
		/// </summary>
        public BillTemplates()
        {
        }
		#endregion

        #region ��ѯ�빹��
        /// <summary>
        /// ���ڵ��ѯ
        /// </summary>
        /// <param name="nd"></param>
        public BillTemplates(Node nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BillTemplateAttr.NodeID, nd.NodeID);
            if (nd.IsStartNode)
            {
                qo.addOr();
                qo.AddWhere("No", "SLHZ");
            }
            qo.DoQuery();
        }
        /// <summary>
        /// �����̲�ѯ
        /// </summary>
        /// <param name="fk_flow">���̱��</param>
        public BillTemplates(string fk_flow)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(BillTemplateAttr.NodeID, "SELECT NodeID FROM WF_Node WHERE fk_flow='" + fk_flow + "'");
            qo.DoQuery();
        }
        /// <summary>
        /// ���ڵ��ѯ
        /// </summary>
        /// <param name="fk_node">�ڵ�ID</param>
        public BillTemplates(int fk_node)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BillTemplateAttr.NodeID, fk_node);
            qo.DoQuery();
        }
        #endregion ��ѯ�빹��

    }
	
}
