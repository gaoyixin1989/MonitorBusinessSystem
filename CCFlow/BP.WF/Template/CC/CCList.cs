using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// ���� ����
	/// </summary>
    public class CCListAttr : EntityMyPKAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// ���͵Ľڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ӽڵ�
        /// </summary>
        public const string NDFrom = "NDFrom";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        public const string FlowName = "FlowName";
        public const string NodeName = "NodeName";
        /// <summary>
        /// �Ƿ��ȡ
        /// </summary>
        public const string Sta = "Sta";
        public const string WorkID = "WorkID";
        public const string FID = "FID";
        /// <summary>
        /// ���͸�
        /// </summary>
        public const string CCTo = "CCTo";
        /// <summary>
        /// ���͸���Ա����
        /// </summary>
        public const string CCToName = "CCToName";
        /// <summary>
        /// ���͸�������
        /// </summary>
        public const string CCToDept = "CCToDept";
        /// <summary>
        /// ������
        /// </summary>
        public const string CheckNote = "CheckNote";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ������Ա����
        /// </summary>
        public const string RecDept = "RecDept";

        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ������ID
        /// </summary>
        public const string PWorkID = "PWorkID";
        /// <summary>
        /// �����̱��
        /// </summary>
        public const string PFlowNo = "PFlowNo";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// �Ƿ��������б�
        /// </summary>
	    public const string InEmpWorks = "InEmpWorks";
        #endregion
    }
    public enum CCSta
    {
        /// <summary>
        /// δ��
        /// </summary>
        UnRead,
        /// <summary>
        /// �Ѷ�ȡ
        /// </summary>
        Read,
        /// <summary>
        /// �Ѿ��ظ�
        /// </summary>
        CheckOver,
        /// <summary>
        /// ��ɾ��
        /// </summary>
        Del
    }
	/// <summary>
	/// ����
	/// </summary>
    public class CCList : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ״̬
        /// </summary>
        public CCSta HisSta
        {
            get
            {
                return (CCSta)this.GetValIntByKey(CCListAttr.Sta);
            }
            set
            {
                if (value == CCSta.Read)
                    this.CDT = DataType.CurrentDataTime;
                this.SetValByKey(CCListAttr.Sta, (int)value);
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
                if (BP.Web.WebUser.No != "admin")
                {
                    uac.IsView = false;
                    return uac;
                }
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
            }
        }
        public string CCTo
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.CCTo);
            }
            set
            {
                this.SetValByKey(CCListAttr.CCTo, value);
            }
        }
        /// <summary>
        /// ���Ͳ���
        /// </summary>
        public string CCToDept
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.CCToDept);
            }
            set
            {
                this.SetValByKey(CCListAttr.CCToDept, value);
            }
        }
        /// <summary>
        /// ���͸�Name
        /// </summary>
        public string CCToName
        {
            get
            {
                string s= this.GetValStringByKey(CCListAttr.CCToName);
                if (string.IsNullOrEmpty(s))
                    s=this.CCTo;
                return s;
            }
            set
            {
                this.SetValByKey(CCListAttr.CCToName, value);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string CheckNote
        {
            get
            {
                string s = this.GetValStringByKey(CCListAttr.CheckNote);
                if (string.IsNullOrEmpty(s))
                    return "��";
                return s;
            }
            set
            {
                this.SetValByKey(CCListAttr.CheckNote, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string CheckNoteHtml
        {
            get
            {
                string s = this.GetValStringByKey(CCListAttr.CheckNote);
                if (string.IsNullOrEmpty(s))
                    return "��";
                return DataType.ParseText2Html(s);
            }
        }
        /// <summary>
        /// ��ȡʱ��
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.CDT);
            }
            set
            {
                this.SetValByKey(CCListAttr.CDT, value);
            }
        }
        /// <summary>
        /// ���������ڵĽڵ���
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(CCListAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(CCListAttr.FK_Node, value);
            }
        }
        public int NDFrom
        {
            get
            {
                return this.GetValIntByKey(CCListAttr.NDFrom);
            }
            set
            {
                this.SetValByKey(CCListAttr.NDFrom, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CCListAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CCListAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(CCListAttr.FID);
            }
            set
            {
                this.SetValByKey(CCListAttr.FID, value);
            }
        }
        /// <summary>
        /// �����̹���ID
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(CCListAttr.PWorkID);
            }
            set
            {
                this.SetValByKey(CCListAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̱��
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.PFlowNo);
            }
            set
            {
                this.SetValByKey(CCListAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_FlowT
        {
            get
            {
                return this.GetValRefTextByKey(CCListAttr.FK_Flow);
            }
        }
        public string FlowName
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.FlowName);
            }
            set
            {
                this.SetValByKey(CCListAttr.FlowName, value);
            }
        }
        public string NodeName
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.NodeName);
            }
            set
            {
                this.SetValByKey(CCListAttr.NodeName, value);
            }
        }
        /// <summary>
        /// ���ͱ���
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.Title);
            }
            set
            {
                this.SetValByKey(CCListAttr.Title, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.Doc);
            }
            set
            {
                this.SetValByKey(CCListAttr.Doc, value);
            }
        }
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CCListAttr.Doc);
            }
        }
        /// <summary>
        /// ���Ͷ���
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CCListAttr.FK_Flow, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.Rec);
            }
            set
            {
                this.SetValByKey(CCListAttr.Rec, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CCListAttr.RDT);
            }
            set
            {
                this.SetValByKey(CCListAttr.RDT, value);
            }
        }
        /// <summary>
        /// �Ƿ��������б�
        /// </summary>
	    public bool InEmpWorks
	    {
            get { return this.GetValBooleanByKey(CCListAttr.InEmpWorks); }
            set { this.SetValByKey(CCListAttr.InEmpWorks, value); }
	    }
        #endregion

        #region ���캯��
        /// <summary>
        /// CCList
        /// </summary>
        public CCList()
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
                Map map = new Map("WF_CCList");
                map.EnDesc = "�����б�";
                map.EnType = EnType.Admin;
                map.AddMyPK();
                map.AddTBString(CCListAttr.Title, null, "����", true, true, 0, 500, 10, true);
                map.AddTBString(CCListAttr.FK_Flow, null, "���̱��", true, true, 0, 3, 10, true);
                map.AddTBString(CCListAttr.FlowName, null, "��������", true, true, 0, 200, 10, true);
                map.AddTBInt(CCListAttr.NDFrom, 0, "�ӽڵ�", true, true);
                map.AddTBInt(CCListAttr.FK_Node, 0, "�ڵ�", true, true);
                map.AddTBString(CCListAttr.NodeName, null, "�ڵ�����", true, true, 0, 500, 10, true);
                map.AddTBInt(CCListAttr.WorkID, 0, "����ID", true, true);
                map.AddTBInt(CCListAttr.FID, 0, "FID", true, true);
                map.AddTBStringDoc();

                map.AddTBString(CCListAttr.Rec, null, "������Ա", true, true, 0, 50, 10, true);
                map.AddTBString(CCListAttr.RecDept, null, "������Ա����", 
                    true, true, 0, 50, 10, true);

                map.AddTBDateTime(CCListAttr.RDT, null, "��¼����", true, false);

                map.AddTBInt(CCListAttr.Sta, 0, "״̬", true, true);

                map.AddTBString(CCListAttr.CCTo, null, "���͸�", true, false, 0, 50, 10, true);
                map.AddTBString(CCListAttr.CCToDept, null, "���͵�����", true, false, 0, 50, 10, true);
                map.AddTBString(CCListAttr.CCToName, null, "���͸�(��Ա����)", true, false, 0, 50, 10, true);
                map.AddTBString(CCListAttr.CheckNote, null, "������", true, false, 0, 600, 10, true);
                map.AddTBDateTime(CCListAttr.CDT, null, "��ʱ��", true, false);


                map.AddTBString(CCListAttr.PFlowNo, null, "�����̱��", true, true, 0, 100, 10, true);
                map.AddTBInt(CCListAttr.PWorkID, 0, "������WorkID", true, true);
                //added by liuxc,2015.7.6����ʶ�Ƿ��ڴ����б�����ʾ
                map.AddBoolean(CCListAttr.InEmpWorks, false, "�Ƿ��������б�", true, true);
                 
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
	public class CCLists: EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CCList();
			}
		}
		/// <summary>
        /// ����
		/// </summary>
		public CCLists(){}


        /// <summary>
        /// ��ѯ�������еĳ�����Ϣ
        /// </summary>
        /// <param name="flowNo"></param>
        /// <param name="workid"></param>
        /// <param name="fid"></param>
        public CCLists(string flowNo, Int64 workid, Int64 fid)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCListAttr.FK_Flow, flowNo);
            qo.addAnd();
            if (fid != 0)
                qo.AddWhereIn(CCListAttr.WorkID, "(" + workid + "," + fid + ")");
            else
                qo.AddWhere(CCListAttr.WorkID, workid);
            qo.DoQuery();
        } 		 
		#endregion
	}
}
