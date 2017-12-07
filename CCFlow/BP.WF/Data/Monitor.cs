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
    /// ���̼��
	/// </summary>
    public class MonitorAttr
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
    /// ���̼��
	/// </summary>
	public class Monitor : Entity
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
                return MonitorAttr.WorkID;
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(MonitorAttr.FK_Flow);
			}
			set
			{
				SetValByKey(MonitorAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.BillNo);
            }
            set
            {
                SetValByKey(MonitorAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.FlowName);
            }
            set
            {
                SetValByKey(MonitorAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(MonitorAttr.PRI);
            }
            set
            {
                SetValByKey(MonitorAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(MonitorAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(MonitorAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.TodoEmps);
            }
            set
            {
                SetValByKey(MonitorAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.Emps);
            }
            set
            {
                SetValByKey(MonitorAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(MonitorAttr.TaskSta);
            }
            set
            {
                SetValByKey(MonitorAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.FK_Emp);
            }
            set
            {
                SetValByKey(MonitorAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(MonitorAttr.FK_Dept);
			}
			set
			{
				SetValByKey(MonitorAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(MonitorAttr.Title);
			}
			set
			{
				SetValByKey(MonitorAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.GuestNo);
            }
            set
            {
                SetValByKey(MonitorAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.GuestName);
            }
            set
            {
                SetValByKey(MonitorAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(MonitorAttr.RDT);
			}
			set
			{
				SetValByKey(MonitorAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(MonitorAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(MonitorAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(MonitorAttr.WorkID);
			}
			set
			{
				SetValByKey(MonitorAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(MonitorAttr.FID);
            }
            set
            {
                SetValByKey(MonitorAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MonitorAttr.CWorkID);
            }
            set
            {
                SetValByKey(MonitorAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.CFlowNo);
            }
            set
            {
                SetValByKey(MonitorAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MonitorAttr.PWorkID);
            }
            set
            {
                SetValByKey(MonitorAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(MonitorAttr.PNodeID);
            }
            set
            {
                SetValByKey(MonitorAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.PFlowNo);
            }
            set
            {
                SetValByKey(MonitorAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.PEmp);
            }
            set
            {
                SetValByKey(MonitorAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.Starter);
            }
            set
            {
                SetValByKey(MonitorAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.StarterName);
            }
            set
            {
                this.SetValByKey(MonitorAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.DeptName);
            }
            set
            {
                this.SetValByKey(MonitorAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(MonitorAttr.NodeName);
            }
            set
            {
                this.SetValByKey(MonitorAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(MonitorAttr.FK_Node);
            }
            set
            {
                SetValByKey(MonitorAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(MonitorAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(MonitorAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(MonitorAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(MonitorAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(MonitorAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(MonitorAttr.WFSta);
            }
            set
            {
                SetValByKey(MonitorAttr.WFSta, (int)value);
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
                return this.GetValStrByKey(MonitorAttr.GUID);
            }
            set
            {
                SetValByKey(MonitorAttr.GUID, value);
            }
        }
		#endregion

        #region ��������.
        #endregion ��������.

        #region ���캯��
        /// <summary>
		/// �����Ĺ�������
		/// </summary>
		public Monitor()
		{
		}
        public Monitor(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MonitorAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� Monitor [" + workId + "]�����ڡ�");
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
                map.EnDesc = "���̼��";
                map.EnType = EnType.View;

                map.AddTBIntPK(MonitorAttr.WorkID, 0, "����ID", true, true);
                map.AddTBInt(MonitorAttr.FID, 0, "FID", false, false);
                map.AddTBString(MonitorAttr.Title, null, "���̱���", true, false, 0, 300, 10,true);
                map.AddTBString(MonitorAttr.FK_Emp, null, "��ǰ������Ա", true, false, 0, 30, 10);
                map.AddDDLEntities(MonitorAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddDDLEntities(MonitorAttr.FK_Dept, null, "������", new BP.Port.Depts(), false);
                map.AddTBString(MonitorAttr.Starter, null, "�����˱��", true, false, 0, 30, 10);
                map.AddTBString(MonitorAttr.StarterName, null, "����", true, false, 0, 30, 10);
                map.AddTBString(MonitorAttr.NodeName, null, "ͣ���ڵ�", true, false, 0, 100, 10);
                map.AddTBString(MonitorAttr.TodoEmps, null, "������", true, false, 0, 100, 10);

                map.AddTBStringDoc(MonitorAttr.FlowNote, null, "��ע", true, false,true);
                map.AddTBMyNum();
                map.AddTBInt(MonitorAttr.FK_Node, 0, "FK_Node", false, false);
                map.AddTBString(MonitorAttr.WorkerDept, null, "������Ա���ű��", 
                    false, false, 0, 30, 10);

                //��ѯ����.
                map.AddSearchAttr(MonitorAttr.FK_Dept);
                map.AddSearchAttr(MonitorAttr.FK_Flow);

                ////�������صĲ�ѯ����.
                //AttrOfSearch search = new AttrOfSearch(MonitorAttr.WorkerDept, "����",
                //    MonitorAttr.WorkerDept, "=", BP.Web.WebUser.FK_Dept, 0, true);
                //map.AttrsOfSearch.Add(search);

                RefMethod rm = new RefMethod();
                rm.Title = "���̹켣";  
                rm.ClassMethodName = this.ToString() + ".DoTrack";
                rm.Icon = "/WF/Img/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = Glo.CCFlowAppPath + "WF/Img/Btn/CC.gif";
                rm.Title = "�ƽ�";
                rm.ClassMethodName = this.ToString() + ".DoShift";
                rm.HisAttrs.AddDDLEntities("ToEmp", null, "�ƽ���:", new BP.WF.Data.MyDeptEmps(),true);
                rm.HisAttrs.AddTBString("Note", null, "�ƽ�ԭ��", true, false, 0, 300, 100);
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = Glo.CCFlowAppPath + "WF/Img/Btn/Delete.gif";
                rm.Title = "ɾ��";
                rm.Warning = "��ȷ��Ҫɾ����������";
                rm.ClassMethodName = this.ToString() + ".DoDelete";
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
            if (BP.WF.Dev2Interface.Flow_IsCanViewTruck(this.FK_Flow, this.WorkID, this.FID) == false)
                return "��û�в������������ݵ�Ȩ��.";

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
        /// <summary>
        /// ִ��ɾ��
        /// </summary>
        /// <returns></returns>
        public string DoDelete()
        {
            if (BP.WF.Dev2Interface.Flow_IsCanViewTruck(this.FK_Flow, this.WorkID, this.FID) == false)
                return "��û�в������������ݵ�Ȩ��.";

            try
            {
                BP.WF.Dev2Interface.Flow_DoDeleteFlowByReal(this.FK_Flow, this.WorkID,true);
                return "ɾ���ɹ�";
            }
            catch (Exception ex)
            {
                return "ɾ��ʧ��@" + ex.Message;
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
    /// ���̼��s
	/// </summary>
	public class Monitors : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Monitor();
			}
		}
		/// <summary>
		/// ���̼�ؼ���
		/// </summary>
		public Monitors(){}
		#endregion
	}
	
}
