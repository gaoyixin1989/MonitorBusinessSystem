using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.En;

namespace BP.WF.Data
{
	/// <summary>
	/// ������������
	/// </summary>
	public class EvalAttr 
	{
		#region ��������
		/// <summary>
		/// ���̱��
		/// </summary>
		public const  string FK_Flow="FK_Flow";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// ���˵Ľڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DeptName = "DeptName";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_NY="FK_NY";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
		/// <summary>
		/// �����˵���Ա����
		/// </summary>
		public const  string EvalEmpName="EvalEmpName";
		/// <summary>
        /// �����˵���Ա���
		/// </summary>
		public const  string EvalEmpNo="EvalEmpNo";
        /// <summary>
        /// ���۷�ֵ
        /// </summary>
        public const string EvalCent = "EvalCent";
        /// <summary>
        /// ��������
        /// </summary>
        public const string EvalNote = "EvalNote";
		/// <summary>
		/// ������Ա
		/// </summary>
		public const  string Rec="Rec";
        /// <summary>
        /// ������Ա����
        /// </summary>
        public const string RecName = "RecName";
		#endregion
	}
	/// <summary>
	/// ������������
	/// </summary>
	public class Eval : EntityMyPK
	{
		#region ��������
        /// <summary>
        /// ���̱���
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.Title);
            }
            set
            {
                this.SetValByKey(EvalAttr.Title, value);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(EvalAttr.WorkID);
			}
			set
			{
				this.SetValByKey(EvalAttr.WorkID,value);
			}
		}
        /// <summary>
        /// �ڵ���
        /// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(EvalAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(EvalAttr.FK_Node,value);
			}
		}
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.NodeName);
            }
            set
            {
                this.SetValByKey(EvalAttr.NodeName, value);
            }
        }
        /// <summary>
        /// ��������Ա����
        /// </summary>
		public string  EvalEmpName
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.EvalEmpName);
			}
			set
			{
				this.SetValByKey(EvalAttr.EvalEmpName,value);
			}
		}
        /// <summary>
        /// ��¼����
        /// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.RDT);
			}
			set
			{
				this.SetValByKey(EvalAttr.RDT,value);
			}
		}
		/// <summary>
		/// ������������
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(EvalAttr.FK_Dept,value);
			}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.DeptName);
            }
            set
            {
                this.SetValByKey(EvalAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
		public string  FK_NY
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.FK_NY);
			}
			set
			{
				this.SetValByKey(EvalAttr.FK_NY,value);
			}
		}
        /// <summary>
        /// ���̱��
        /// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.FK_Flow);
			}
            set
            {
                this.SetValByKey(EvalAttr.FK_Flow, value);
            }
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
		{
			get
			{
                return this.GetValStringByKey(EvalAttr.FlowName);
			}
			set
			{
                this.SetValByKey(EvalAttr.FlowName, value);
			}
		}
        /// <summary>
        /// ������
        /// </summary>
		public string  Rec
		{
			get
			{
				return this.GetValStringByKey(EvalAttr.Rec);
			}
			set
			{
				this.SetValByKey(EvalAttr.Rec,value);
			}
		}
        /// <summary>
        /// ����������
        /// </summary>
        public string RecName
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.RecName);
            }
            set
            {
                this.SetValByKey(EvalAttr.RecName, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string EvalNote
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.EvalNote);
            }
            set
            {
                this.SetValByKey(EvalAttr.EvalNote, value);
            }
        }
        /// <summary>
        /// �����˵���Ա���
        /// </summary>
        public string EvalEmpNo
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.EvalEmpNo);
            }
            set
            {
                this.SetValByKey(EvalAttr.EvalEmpNo, value);
            }
        }
        /// <summary>
        /// ���۷�ֵ
        /// </summary>
        public string EvalCent
        {
            get
            {
                return this.GetValStringByKey(EvalAttr.EvalCent);
            }
            set
            {
                this.SetValByKey(EvalAttr.EvalCent, value);
            }
        }
		#endregion 

		#region ���캯��
		/// <summary>
		/// ������������
		/// </summary>
		public Eval()
        {
        }
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="FK_Node"></param>
		public Eval(int workid, int FK_Node)
		{
			this.WorkID=workid;
			this.FK_Node=FK_Node;
			this.Retrieve();
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
                Map map = new Map("WF_CHEval");
                map.EnDesc = "������������";
                map.EnType = EnType.App;

                map.AddMyPK();
                map.AddTBString(EvalAttr.Title, null, "����", false, true, 0, 500, 10);
                map.AddTBString(EvalAttr.FK_Flow, null, "���̱��", false, true, 0, 7, 10);
                map.AddTBString(EvalAttr.FlowName, null, "��������", false, true, 0, 100, 10);

                map.AddTBInt(EvalAttr.WorkID, 0, "����ID", false, true);
                map.AddTBInt(EvalAttr.FK_Node, 0, "���۽ڵ�", false, true);
                map.AddTBString(EvalAttr.NodeName, null, "�ڵ�����", false, true, 0, 100, 10);

                map.AddTBString(EvalAttr.Rec, null, "������", false, true, 0, 50, 10);
                map.AddTBString(EvalAttr.RecName, null, "����������", false, true, 0, 50, 10);

                map.AddTBDateTime(EvalAttr.RDT, "��������", true, true);

                map.AddTBString(EvalAttr.EvalEmpNo, null, "�����˵���Ա���", false, true, 0, 50, 10);
                map.AddTBString(EvalAttr.EvalEmpName, null, "�����˵���Ա����", false, true, 0, 50, 10);
                map.AddTBString(EvalAttr.EvalCent, null, "���۷�ֵ", false, true, 0, 20, 10);
                map.AddTBString(EvalAttr.EvalNote, null, "��������", false, true, 0, 20, 10);

                map.AddTBString(EvalAttr.FK_Dept, null, "����", false, true, 0, 50, 10);
                map.AddTBString(EvalAttr.DeptName, null, "��������", false, true, 0, 100, 10);
                map.AddTBString(EvalAttr.FK_NY, null, "����", false, true, 0, 7, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	/// <summary>
	/// ������������s BP.Port.FK.Evals
	/// </summary>
	public class Evals : EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Eval();
			}
		}
		/// <summary>
        /// ������������s
		/// </summary>
		public Evals(){}
		#endregion
	}
	
}
