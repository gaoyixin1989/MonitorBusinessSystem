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
    /// �Ҳ��ŵĴ���
	/// </summary>
    public class MyDeptTodolistAttr
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
        public const string FK_Emp = "FK_Emp";
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
        /// �Ҳ���
        /// </summary>
        public const string WorkerDept = "WorkerDept";
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
    /// �Ҳ��ŵĴ���
	/// </summary>
	public class MyDeptTodolist : Entity
	{	
		#region ��������
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
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return MyDeptTodolistAttr.WorkID;
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(MyDeptTodolistAttr.FK_Flow);
			}
			set
			{
				SetValByKey(MyDeptTodolistAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.BillNo);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.FlowName);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(MyDeptTodolistAttr.PRI);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(MyDeptTodolistAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.TodoEmps);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.Emps);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(MyDeptTodolistAttr.TaskSta);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.FK_Emp);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(MyDeptTodolistAttr.FK_Dept);
			}
			set
			{
				SetValByKey(MyDeptTodolistAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(MyDeptTodolistAttr.Title);
			}
			set
			{
				SetValByKey(MyDeptTodolistAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.GuestNo);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.GuestName);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(MyDeptTodolistAttr.RDT);
			}
			set
			{
				SetValByKey(MyDeptTodolistAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(MyDeptTodolistAttr.WorkID);
			}
			set
			{
				SetValByKey(MyDeptTodolistAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptTodolistAttr.FID);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptTodolistAttr.CWorkID);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.CFlowNo);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptTodolistAttr.PWorkID);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(MyDeptTodolistAttr.PNodeID);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.PFlowNo);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.PEmp);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.Starter);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.StarterName);
            }
            set
            {
                this.SetValByKey(MyDeptTodolistAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.DeptName);
            }
            set
            {
                this.SetValByKey(MyDeptTodolistAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(MyDeptTodolistAttr.NodeName);
            }
            set
            {
                this.SetValByKey(MyDeptTodolistAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(MyDeptTodolistAttr.FK_Node);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(MyDeptTodolistAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(MyDeptTodolistAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(MyDeptTodolistAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(MyDeptTodolistAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(MyDeptTodolistAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(MyDeptTodolistAttr.WFSta);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.WFSta, (int)value);
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
                return this.GetValStrByKey(MyDeptTodolistAttr.GUID);
            }
            set
            {
                SetValByKey(MyDeptTodolistAttr.GUID, value);
            }
        }
		#endregion

        #region ��������.
        #endregion ��������.

        #region ���캯��
        /// <summary>
		/// �����Ĺ�������
		/// </summary>
		public MyDeptTodolist()
		{
		}
        public MyDeptTodolist(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MyDeptTodolistAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� MyDeptTodolist [" + workId + "]�����ڡ�");
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

                Map map = new Map("WF_EmpWorks");
                map.EnDesc = "�Ҳ��ŵĴ���";
                map.EnType = EnType.View;

                map.AddTBInt(MyDeptTodolistAttr.FID, 0, "FID", false, false);
                map.AddTBString(MyDeptTodolistAttr.Title, null, "���̱���", true, false, 0, 300, 10, true);
                map.AddDDLEntities(MyDeptTodolistAttr.FK_Flow, null, "����", new Flows(), false);
               
                map.AddTBString(MyDeptTodolistAttr.Starter, null, "�����˱��", true, false, 0, 30, 10);
                map.AddTBString(MyDeptTodolistAttr.StarterName, null, "����������", true, false, 0, 30, 10);
                map.AddTBString(MyDeptTodolistAttr.RDT, null, "����ʱ��", true, false, 0, 100, 10);

                map.AddTBString(MyDeptTodolistAttr.NodeName, null, "ͣ���ڵ�", true, false, 0, 100, 10);

                map.AddTBStringDoc(MyDeptTodolistAttr.FlowNote, null, "��ע", true, false,true);

                //��Ϊ�����ֶ�.
                map.AddTBString(MyDeptTodolistAttr.WorkerDept, null, "������Ա���ű��", 
                    false, false, 0, 30, 10);
                map.AddTBMyNum();
                map.AddDDLEntities(MyDeptTodolistAttr.FK_Emp, null, "��ǰ������", new BP.WF.Data.MyDeptEmps(), false);
                map.AddTBIntPK(MyDeptTodolistAttr.WorkID, 0, "����ID", true, true);

                //��ѯ����.
                map.AddSearchAttr(MyDeptTodolistAttr.FK_Flow);
                map.AddSearchAttr(MyDeptTodolistAttr.FK_Emp);


                //�������صĲ�ѯ����.
                AttrOfSearch search = new AttrOfSearch(MyDeptTodolistAttr.WorkerDept, "����",
                    MyDeptTodolistAttr.WorkerDept, "=", BP.Web.WebUser.FK_Dept, 0, true);
                map.AttrsOfSearch.Add(search);

                RefMethod rm = new RefMethod();
                rm.Title = "�켣";  
                rm.ClassMethodName = this.ToString() + ".DoTrack";
                rm.Icon = "/WF/Img/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = Glo.CCFlowAppPath + "WF/Img/Btn/CC.gif";
                rm.Title = "�ƽ�";
                rm.ClassMethodName = this.ToString() + ".DoShift";
                rm.HisAttrs.AddDDLEntities("ToEmp", null, "�ƽ���:", new BP.WF.Flows(), true);
                rm.HisAttrs.AddTBString("Note", null, "�ƽ�ԭ��", true, false, 0, 300, 100);
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "��ת";
                //rm.ClassMethodName = this.ToString() + ".DoSkip";
                //rm.Icon = "/WF/Img/FileType/doc.gif";
                //map.AddRefMethod(rm);
              
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region ִ�����
        public string DoTrack()
        {
            PubClass.WinOpen("/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow,900,800);
            return null;
        }
        /// <summary>
        /// ִ���ƽ�
        /// </summary>
        /// <param name="ToEmp"></param>
        /// <param name="Note"></param>
        /// <returns></returns>
        public string DoShift(string ToEmp, string Note)
        {
            try
            {
                BP.WF.Dev2Interface.Node_Shift(this.FK_Flow, this.FK_Node, this.WorkID, this.FID, ToEmp, Note);
                return "�ƽ��ɹ�";
            }
            catch(Exception ex)
            {
                return "�ƽ�ʧ��@" + ex.Message;
            }
        }
        public string DoSkip()
        {
            PubClass.WinOpen("/WF/Admin/FlowDB/FlowSkip.aspx?WorkID=" + this.WorkID + "&FID=" + this.FID + "&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node, 900, 800);
            return null;
        }
		#endregion
	}
	/// <summary>
    /// �Ҳ��ŵĴ���s
	/// </summary>
	public class MyDeptTodolists : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new MyDeptTodolist();
			}
		}
		/// <summary>
		/// �Ҳ��ŵĴ��켯��
		/// </summary>
		public MyDeptTodolists(){}
		#endregion
	}
	
}
