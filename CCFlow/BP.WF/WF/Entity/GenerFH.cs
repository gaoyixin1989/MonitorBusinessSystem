using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// �����ֺ����̿���
	/// </summary>
    public class GenerFHAttr
    {
        #region ��������
        /// <summary>
        /// ��˰���
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string GroupKey = "GroupKey";
        /// <summary>
        /// ��ǰ�������Ľڵ�.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ���͵���Ա
        /// </summary>
        public const string ToEmpsMsg = "ToEmpsMsg";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// �����ֺ����̿���
	/// </summary>
    public class GenerFH : Entity
    {
        #region ��������
        public override string PK
        {
            get
            {
                return "FID";
            }
        }
        /// <summary>
        /// HisFlow
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                return new Flow(this.FK_Flow);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GenerFHAttr.RDT);
            }
            set
            {
                SetValByKey(GenerFHAttr.RDT, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GenerFHAttr.Title);
            }
            set
            {
                SetValByKey(GenerFHAttr.Title, value);
            }
        }
        /// <summary>
        /// �������̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(GenerFHAttr.FK_Flow);
            }
            set
            {
                SetValByKey(GenerFHAttr.FK_Flow, value);
            }
        }
        public string ToEmpsMsg
        {
            get
            {
                return this.GetValStringByKey(GenerFHAttr.ToEmpsMsg);
            }
            set
            {
                SetValByKey(GenerFHAttr.ToEmpsMsg, value);
            }
        }
        
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerFHAttr.FID);
            }
            set
            {
                SetValByKey(GenerFHAttr.FID, value);
            }
        }
        public string GroupKey
        {
            get
            {
                return this.GetValStringByKey(GenerFHAttr.GroupKey);
            }
            set
            {
                this.SetValByKey(GenerFHAttr.GroupKey, value);
            }
        }
        public string FK_NodeText
        {
            get
            {
                Node nd = new Node(this.FK_Node);
                return nd.Name;
            }
        }
        /// <summary>
        /// ��ǰ�������Ľڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerFHAttr.FK_Node);
            }
            set
            {
                SetValByKey(GenerFHAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ��������״̬( 0, δ���,1 ���, 2 ǿ����ֹ 3, ɾ��״̬,) 
        /// </summary>
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(GenerFHAttr.WFState);
            }
            set
            {
                SetValByKey(GenerFHAttr.WFState, value);
            }
        }
        #endregion  

        #region ���캯��
        /// <summary>
        /// �����ֺ����̿�������
        /// </summary>
        public GenerFH()
        {
        }
        /// <summary>
        /// �����ֺ����̿�������
        /// </summary>
        /// <param name="FID"></param>
        public GenerFH(Int64 FID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerFHAttr.FID, FID);
            if (qo.DoQuery() == 0)
                throw new Exception("��ѯ GenerFH ����[" + FID + "]�����ڣ��������Ѿ���ɡ�");
        }
        /// <summary>
        /// �����ֺ����̿�������
        /// </summary>
        /// <param name="FID">��������ID</param>
        /// <param name="flowNo">���̱��</param>
        public GenerFH(Int64 FID, string flowNo)
        {
            try
            {
                this.FID = FID;
                this.FK_Flow = flowNo;
                this.Retrieve();
            }
            catch (Exception ex)
            {
                //WorkFlow wf = new WorkFlow(new Flow(flowNo), FID, FID);
                //StartWork wk = wf.HisStartWork;
                //if (wf.WFState == BP.WF.WFState.Complete)
                //{
                //    throw new Exception("@�Ѿ�������̣��������ڵ�ǰ������������Ҫ�õ������̵���ϸ����鿴��ʷ������������Ϣ:" + ex.Message);
                //}
                //else
                //{
                //    this.Copy(wk);
                //    //string msg = "@�����ڲ����󣬸��������Ĳ��㣬���ʾ��Ǹ����Ѵ����֪ͨ��ϵͳ����Ա��error code:0001�������Ϣ:" + ex.Message;
                //    string msg = "@�����ڲ����󣬸��������Ĳ��㣬���ʾ��Ǹ����Ѵ����֪ͨ��ϵͳ����Ա��error code:0001�������Ϣ:" + ex.Message;
                //    Log.DefaultLogWriteLine(LogType.Error, "@������ɺ���ʹ�����׳����쳣��" + msg);
                //    //throw new Exception(msg);
                //}
            }
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
                Map map = new Map("WF_GenerFH");
                map.EnDesc = "�ֺ����̿���";

                map.AddTBIntPK(GenerFHAttr.FID, 0, "����ID", true, true);

                map.AddTBString(GenerFHAttr.Title, null, "����", true, false, 0, 4000, 10);
                map.AddTBString(GenerFHAttr.GroupKey, null, "��������", true, false, 0, 3000, 10);
                map.AddTBString(GenerFHAttr.FK_Flow, null, "����", true, false, 0, 500, 10);
                map.AddTBString(GenerFHAttr.ToEmpsMsg, null, "������Ա", true, false, 0, 4000, 10);
                map.AddTBInt(GenerFHAttr.FK_Node, 0, "ͣ���ڵ�", true, false);
                map.AddTBInt(GenerFHAttr.WFState, 0, "WFState", true, false);
                map.AddTBDate(GenerFHAttr.RDT, null, "RDT", true, false);

                //RefMethod rm = new RefMethod();
                //rm.Title = "��������";  // "��������";
                //rm.ClassMethodName = this.ToString() + ".DoRpt";
                //rm.Icon = "../WF/Img/Btn/doc.gif";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSelfTest", "�����Լ�"); // "�����Լ�";
                //rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "�����Լ첢�޸�";
                //rm.ClassMethodName = this.ToString() + ".DoRepare";
                //rm.Warning = "��ȷ��Ҫִ�д˹����� \t\n 1)����Ƕ����̣�����ͣ���ڵ�һ���ڵ��ϣ�ϵͳΪִ��ɾ������\t\n 2)����Ƿǵص�һ���ڵ㣬ϵͳ�᷵�ص��ϴη����λ�á�";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ���ػ��෽��
        protected override void afterDelete()
        {
            base.afterDelete();
        }
        #endregion
    }
	/// <summary>
	/// �����ֺ����̿���s
	/// </summary>
	public class GenerFHs : Entities
	{
		/// <summary>
		/// ���ݹ�������,������ԱID ��ѯ��������ǰ�������Ĺ���.
		/// </summary>
		/// <param name="flowNo">���̱��</param>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.FID FROM WF_GenerFH a, WF_GenerWorkerlist b WHERE a.FID=b.FID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
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
				return new GenerFH();
			}
		}
		/// <summary>
		/// �����������̼���
		/// </summary>
		public GenerFHs(){}
		#endregion
	}
	
}
