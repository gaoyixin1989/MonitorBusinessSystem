using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// �ƽ���¼
	/// </summary>
    public class ShiftWorkAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ƽ�ԭ��
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// �ƽ���
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �ƽ�������
        /// </summary>
        public const string FK_EmpName = "FK_EmpName";
        /// <summary>
        /// �ƽ�ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// �ƽ���
        /// </summary>
        public const string ToEmp = "ToEmp";
        /// <summary>
        /// �ƽ�����Ա����
        /// </summary>
        public const string ToEmpName = "ToEmpName";
        #endregion
    }
	/// <summary>
	/// �ƽ���¼
	/// </summary>
	public class ShiftWork : EntityMyPK
	{		
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(ShiftWorkAttr.WorkID);
			}
			set
			{
				SetValByKey(ShiftWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// �����ڵ�
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(ShiftWorkAttr.FK_Node);
			}
			set
			{
				SetValByKey(ShiftWorkAttr.FK_Node,value);
			}
		}
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public bool IsRead
        {
            get
            {
                return this.GetValBooleanByKey(ShiftWorkAttr.IsRead);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.IsRead, value);
            }
        }
        /// <summary>
        /// ToEmpName
        /// </summary>
        public string ToEmpName
        {
            get
            {
                return this.GetValStringByKey(ShiftWorkAttr.ToEmpName);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.ToEmpName, value);
            }
        }
        /// <summary>
        /// �ƽ�������.
        /// </summary>
        public string FK_EmpName
        {
            get
            {
                return this.GetValStringByKey(ShiftWorkAttr.FK_EmpName);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.FK_EmpName, value);
            }
        }
        /// <summary>
        /// �ƽ�ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(ShiftWorkAttr.RDT);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.RDT, value);
            }
        }
        /// <summary>
        /// �ƽ����
        /// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(ShiftWorkAttr.Note);
			}
			set
			{
				SetValByKey(ShiftWorkAttr.Note,value);
			}
		}
        /// <summary>
        /// �ƽ����html��ʽ
        /// </summary>
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ShiftWorkAttr.Note);
            }
        }
        /// <summary>
        /// �ƽ���
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(ShiftWorkAttr.FK_Emp);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// �ƽ���
        /// </summary>
        public string ToEmp
        {
            get
            {
                return this.GetValStringByKey(ShiftWorkAttr.ToEmp);
            }
            set
            {
                SetValByKey(ShiftWorkAttr.ToEmp, value);
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// �ƽ���¼
		/// </summary>
		public ShiftWork()
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

                Map map = new Map("WF_ShiftWork");
                map.EnDesc = "�ƽ���¼";
                map.EnType = EnType.App;

                map.AddMyPK();

                map.AddTBInt(ShiftWorkAttr.WorkID, 0, "����ID", true, true);
                map.AddTBInt(ShiftWorkAttr.FK_Node, 0, "FK_Node", true, true);
                map.AddTBString(ShiftWorkAttr.FK_Emp, null, "�ƽ���", true, true, 0, 40, 10);
                map.AddTBString(ShiftWorkAttr.FK_EmpName, null, "�ƽ�������", true, true, 0, 40, 10);

                map.AddTBString(ShiftWorkAttr.ToEmp, null, "�ƽ���", true, true, 0, 40, 10);
                map.AddTBString(ShiftWorkAttr.ToEmpName, null, "�ƽ�������", true, true, 0, 40, 10);

                map.AddTBDateTime(ShiftWorkAttr.RDT, null, "�ƽ�ʱ��", true, true);
                map.AddTBString(ShiftWorkAttr.Note, null, "�ƽ�ԭ��", true, true, 0, 2000, 10);

                map.AddTBInt(ShiftWorkAttr.IsRead, 0, "�Ƿ��ȡ��", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeInsert()
        {
            this.MyPK = BP.DA.DBAccess.GenerOIDByGUID().ToString();
            this.RDT = DataType.CurrentDataTime;
            return base.beforeInsert();
        }
		#endregion	 
	}
	/// <summary>
	/// �ƽ���¼s 
	/// </summary>
	public class ShiftWorks : Entities
	{	 
		#region ����
		/// <summary>
		/// �ƽ���¼s
		/// </summary>
		public ShiftWorks()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ShiftWork();
			}
		}
		#endregion
	}
	
}
