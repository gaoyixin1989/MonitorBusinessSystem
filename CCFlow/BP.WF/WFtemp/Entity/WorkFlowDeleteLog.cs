using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.WF.Data;

namespace BP.WF
{
	/// <summary>
	/// ����ɾ����־
	/// </summary>
	public class WorkFlowDeleteLogAttr 
	{
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
		public const  string OID="OID";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
		/// <summary>
		/// ɾ����Ա
		/// </summary>
		public const  string Oper="Oper";
		/// <summary>
		/// ɾ��ԭ��
		/// </summary>
		public const  string DeleteNote="DeleteNote";
        /// <summary>
        /// ɾ������
        /// </summary>
        public const string DeleteDT = "DeleteDT";
        /// <summary>
        /// ɾ����Ա
        /// </summary>
        public const string OperDept = "OperDept";
        /// <summary>
        /// ɾ����Ա����
        /// </summary>
        public const string OperDeptName = "OperDeptName";
        /// <summary>
        /// ɾ���ڵ�ڵ�
        /// </summary>
        public const string DeleteNode = "DeleteNode";
        /// <summary>
        /// ɾ���ڵ�ڵ�����
        /// </summary>
        public const string DeleteNodeName = "DeleteNodeName";        
        /// <summary>
        /// ɾ���ڵ���Ƿ���Ҫԭ·���أ�
        /// </summary>
        public const string IsBackTracking = "IsBackTracking";
		#endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string FlowEmps = "FlowEmps";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ������ID
        /// </summary>
        public const string FlowStarter = "FlowStarter";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowStartDeleteDT = "FlowStartDeleteDT";
        /// <summary>
        /// �����˲���ID
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string MyNum = "MyNum";
        /// <summary>
        /// ������
        /// </summary>
        public const string FlowEnder = "FlowEnder";
        /// <summary>
        /// �������
        /// </summary>
        public const string FlowEnderDeleteDT = "FlowEnderDeleteDT";
        /// <summary>
        /// ���
        /// </summary>
        public const string FlowDaySpan = "FlowDaySpan";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FlowEndNode = "FlowEndNode";
        /// <summary>
        /// ������WorkID
        /// </summary>
        public const string PWorkID = "PWorkID";
        /// <summary>
        /// �����̱��
        /// </summary>
        public const string PFlowNo = "PFlowNo";
        #endregion 
    }
	/// <summary>
	/// ����ɾ����־
	/// </summary>
    public class WorkFlowDeleteLog : EntityOID
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 OID
        {
            get
            {
                return this.GetValInt64ByKey(WorkFlowDeleteLogAttr.OID);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.OID, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Oper
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.Oper);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.Oper, value);
            }
        }
        /// <summary>
        /// ɾ����Ա
        /// </summary>
        public string OperDept
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.OperDept);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.OperDept, value);
            }
        }
        public string OperDeptName
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.OperDeptName);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.OperDeptName, value);
            }
        }
        public string DeleteNote
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.DeleteNote);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.DeleteNote, value);
            }
        }
        public string DeleteNoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(WorkFlowDeleteLogAttr.DeleteNote);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string DeleteDT
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.DeleteDT);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.DeleteDT, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.FK_Flow);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(WorkFlowDeleteLogAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(WorkFlowDeleteLogAttr.FK_FlowSort, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����ɾ����־
        /// </summary>
        public WorkFlowDeleteLog() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_WorkFlowDeleteLog");
                map.EnDesc = "����ɾ����־";
                map.EnType = EnType.App;

                // ���̻������ݡ�
                map.AddTBIntPKOID(FlowDataAttr.OID, "WorkID");
                map.AddTBInt(FlowDataAttr.FID, 0, "FID", false, false);
                map.AddDDLEntities(FlowDataAttr.FK_Dept, null, "����", new Port.Depts(), false);
                map.AddTBString(FlowDataAttr.Title, null, "����", true, true, 0, 100, 100);
                map.AddTBString(FlowDataAttr.FlowStarter, null, "������", true, true, 0, 100, 100);
                map.AddTBDateTime(FlowDataAttr.FlowStartRDT, null, "��������", true, true);
                map.AddDDLEntities(FlowDataAttr.FK_NY, null, "����", new BP.Pub.NYs(), false);
                map.AddDDLEntities(FlowDataAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddTBDateTime(FlowDataAttr.FlowEnderRDT, null, "��������", true, true);
                map.AddTBInt(FlowDataAttr.FlowEndNode, 0, "�����ڵ�", true, true);
                map.AddTBInt(FlowDataAttr.FlowDaySpan, 0, "���(��)", true, true);
                map.AddTBInt(FlowDataAttr.MyNum, 1, "����", true, true);
                map.AddTBString(FlowDataAttr.FlowEmps, null, "������", false, false, 0, 100, 100);

                //ɾ����Ϣ.
                map.AddTBString(WorkFlowDeleteLogAttr.Oper, null, "ɾ����Ա", true, true, 0, 20, 10);
                map.AddTBString(WorkFlowDeleteLogAttr.OperDept, null, "ɾ����Ա����", true, true, 0, 20, 10);
                map.AddTBString(WorkFlowDeleteLogAttr.OperDeptName, null, "ɾ����Ա����", true, true, 0, 200, 10);
                map.AddTBString(WorkFlowDeleteLogAttr.DeleteNote, "", "ɾ��ԭ��", true, true, 0, 4000, 10);
                map.AddTBDateTime(WorkFlowDeleteLogAttr.DeleteDT, null, "ɾ������", true, true);

                //��ѯ.
                map.AddSearchAttr(FlowDataAttr.FK_Dept);
                map.AddSearchAttr(FlowDataAttr.FK_NY);
                map.AddSearchAttr(FlowDataAttr.FK_Flow);

               // map.AddHidden(FlowDataAttr.FlowEmps, " LIKE ", "'%@@WebUser.No%'");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����ɾ����־s 
	/// </summary>
	public class WorkFlowDeleteLogs : Entities
	{	 
		#region ����
		/// <summary>
		/// ����ɾ����־s
		/// </summary>
		public WorkFlowDeleteLogs()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkFlowDeleteLog();
			}
		}
		#endregion
	}
	
}
