using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port ;
using BP.Sys;
using BP.En;

namespace BP.WF.Data
{
	/// <summary>
    /// ����ʵ��
	/// </summary>
    public class GenerWorkFlowViewAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ����״̬(��)
        /// </summary>
        public const string WFSta = "WFSta";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ������
        /// </summary>
        public const string Starter = "Starter";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �÷�
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// ��ǰ�������Ľڵ�.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��ǰ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// ����������
        /// </summary>
        public const string StarterName = "StarterName";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DeptName = "DeptName";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public const string SDTOfFlow = "SDTOfFlow";
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public const string SDTOfNode = "SDTOfNode";
        /// <summary>
        /// ������ID
        /// </summary>
        public const string PWorkID = "PWorkID";
        /// <summary>
        /// �����̱��
        /// </summary>
        public const string PFlowNo = "PFlowNo";
        /// <summary>
        /// �����̽ڵ�
        /// </summary>
        public const string PNodeID = "PNodeID";
        /// <summary>
        /// �����̵ĵ�����.
        /// </summary>
        public const string PEmp = "PEmp";
        /// <summary>
        /// �ͻ����(���ڿͻ������������Ч)
        /// </summary>
        public const string GuestNo = "GuestNo";
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public const string GuestName = "GuestName";
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public const string BillNo = "BillNo";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string TodoEmps = "TodoEmps";
        /// <summary>
        /// ������Ա����
        /// </summary>
        public const string TodoEmpsNum = "TodoEmpsNum";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string TaskSta = "TaskSta";
        /// <summary>
        /// �������̱��
        /// </summary>
        public const string CFlowNo = "CFlowNo";
        /// <summary>
        /// ��������ID
        /// </summary>
        public const string CWorkID = "CWorkID";
        /// <summary>
        /// ��ʱ��ŵĲ���
        /// </summary>
        public const string AtPara = "AtPara";
        /// <summary>
        /// ������
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        #endregion
    }
	/// <summary>
    /// ����ʵ��
	/// </summary>
	public class GenerWorkFlowView : Entity
	{	
		#region ��������
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return GenerWorkFlowViewAttr.WorkID;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string FlowNote
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.FlowNote);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.FlowNote, value);
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowViewAttr.FK_Flow);
			}
			set
			{
				SetValByKey(GenerWorkFlowViewAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.BillNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.FlowName);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowViewAttr.PRI);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowViewAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.TodoEmps);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.Emps);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(GenerWorkFlowViewAttr.TaskSta);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowViewAttr.FK_Dept);
			}
			set
			{
				SetValByKey(GenerWorkFlowViewAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowViewAttr.Title);
			}
			set
			{
				SetValByKey(GenerWorkFlowViewAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.GuestNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.GuestName);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowViewAttr.RDT);
			}
			set
			{
				SetValByKey(GenerWorkFlowViewAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(GenerWorkFlowViewAttr.WorkID);
			}
			set
			{
				SetValByKey(GenerWorkFlowViewAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowViewAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowViewAttr.CWorkID);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.CFlowNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowViewAttr.PWorkID);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowViewAttr.PNodeID);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.PFlowNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.PEmp);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.Starter);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.StarterName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowViewAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.DeptName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowViewAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.NodeName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowViewAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowViewAttr.FK_Node);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(GenerWorkFlowViewAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(GenerWorkFlowViewAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(GenerWorkFlowViewAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(GenerWorkFlowViewAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(GenerWorkFlowViewAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(GenerWorkFlowViewAttr.WFSta);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.WFSta, (int)value);
            }
        }
        public string WFStateText
        {
            get
            {
                BP.WF.WFState ws = (WFState)this.WFState;
                switch(ws)
                {
                    case WF.WFState.Complete:
                        return "�����";
                    case WF.WFState.Runing:
                        return "������";
                    case WF.WFState.HungUp:
                        return "����";
                    case WF.WFState.Askfor:
                        return "��ǩ";
                    default:
                        return "δ�ж�";
                }
            }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowViewAttr.GUID);
            }
            set
            {
                SetValByKey(GenerWorkFlowViewAttr.GUID, value);
            }
        }
		#endregion

        #region ��������.
        public string Paras_ToNodes
        {

            get
            {
                return this.GetParaString("ToNodes");
            }

            set
            {
                this.SetPara("ToNodes", value);
            }
        }
        /// <summary>
        /// ��ǩ��Ϣ
        /// </summary>
        public string Paras_AskForReply
        {

            get
            {
                return this.GetParaString("AskForReply");
            }

            set
            {
                this.SetPara("AskForReply", value);
            }
        }
        #endregion ��������.

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.Readonly();
                return uac;
            }
        }
        /// <summary>
		/// �����Ĺ�������
		/// </summary>
		public GenerWorkFlowView()
		{
		}
        public GenerWorkFlowView(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkFlowViewAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� GenerWorkFlowView [" + workId + "]�����ڡ�");
        }
        /// <summary>
        /// ִ���޸�
        /// </summary>
        public void DoRepair()
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

                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc = "���̲�ѯ";
                map.AddTBIntPK(GenerWorkFlowViewAttr.WorkID, 0, "WorkID", true, true);
              

                map.AddTBString(GenerWorkFlowViewAttr.StarterName, null, "������", true, false, 0, 30, 10);
                map.AddTBString(GenerWorkFlowViewAttr.Title, null, "����", true, false, 0, 100, 10,true);
                map.AddDDLSysEnum(GenerWorkFlowViewAttr.WFSta, 0, "����״̬", true, false, GenerWorkFlowViewAttr.WFSta, "@0=������@1=�����@2=����");
                map.AddTBString(GenerWorkFlowViewAttr.NodeName, null, "��ǰ�ڵ�����", true, false, 0, 100, 10);
                map.AddTBDateTime(GenerWorkFlowViewAttr.RDT, "��¼����", true, true);
                map.AddTBString(GenerWorkFlowViewAttr.BillNo, null, "���ݱ��", true, false, 0, 100, 10);
                map.AddTBStringDoc(GenerWorkFlowViewAttr.FlowNote, null, "��ע", true, false,true);

                map.AddDDLEntities(GenerWorkFlowViewAttr.FK_FlowSort, null, "���", new FlowSorts(), false);
                map.AddDDLEntities(GenerWorkFlowViewAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddDDLEntities(GenerWorkFlowViewAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);

                map.AddTBInt(GenerWorkFlowViewAttr.FID, 0, "FID", false, false);
                map.AddTBInt(GenerWorkFlowViewAttr.FK_Node, 0, "FK_Node", false, false);


                map.AddDDLEntities(GenerWorkFlowViewAttr.FK_NY, null, "�·�", new GenerWorkFlowViewNYs(), false);

                map.AddTBMyNum();

                //map.AddSearchAttr(GenerWorkFlowViewAttr.FK_Dept);
                map.AddSearchAttr(GenerWorkFlowViewAttr.FK_Flow);
                map.AddSearchAttr(GenerWorkFlowViewAttr.WFSta);
                map.AddSearchAttr(GenerWorkFlowViewAttr.FK_NY);


                RefMethod rm = new RefMethod();
                rm.Title = "�켣";
                rm.ClassMethodName = this.ToString() + ".DoTrack";
                rm.Icon = "/WF/Img/Track.png";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "ɾ��";
                rm.ClassMethodName = this.ToString() + ".DoDel";
                rm.Warning = "��ȷ��Ҫɾ����";
                rm.Icon = "/WF/Img/Btn/Delete.gif";
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = Glo.CCFlowAppPath + "WF/Img/Btn/CC.gif";
                rm.Title = "�ƽ�";
                rm.IsForEns = false;
                rm.ClassMethodName = this.ToString() + ".DoShift";
                rm.HisAttrs.AddTBString("ToEmp", null, "�ƽ���", true, false, 0, 300, 100);
               // rm.HisAttrs.AddDDLEntities("ToEmp", null, "�ƽ���:", new BP.WF.Flows(), true);
                rm.HisAttrs.AddTBString("Note", null, "�ƽ�ԭ��", true, false, 0, 300, 100);
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		 
	}
	/// <summary>
    /// ����ʵ��s
	/// </summary>
	public class GenerWorkFlowViews : Entities
	{
		/// <summary>
		/// ���ݹ�������,������Ա ID ��ѯ��������ǰ�������Ĺ���.
		/// </summary>
		/// <param name="flowNo">���̱��</param>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_GenerWorkFlowView a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
			return DBAccess.RunSQLReturnTable(sql);
		}


		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new GenerWorkFlowView();
			}
		}
		/// <summary>
		/// ����ʵ������
		/// </summary>
		public GenerWorkFlowViews(){}
		#endregion
	}
	
}
