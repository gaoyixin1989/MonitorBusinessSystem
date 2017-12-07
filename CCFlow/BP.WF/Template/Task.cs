using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ���� ����
	/// </summary>
    public class TaskAttr : EntityMyPKAttr
    {
        #region ��������
        /// <summary>
        /// ������
        /// </summary>
        public const string Starter = "Starter";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����
        /// </summary>
        public const string Paras = "Paras";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string TaskSta = "TaskSta";
        /// <summary>
        /// Msg
        /// </summary>
        public const string Msg = "Msg";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string StartDT = "StartDT";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
    public class Task : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public string Paras
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Paras);
            }
            set
            {
                this.SetValByKey(TaskAttr.Paras, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Starter);
            }
            set
            {
                this.SetValByKey(TaskAttr.Starter, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TaskAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ����ʱ�䣨����Ϊ�գ�
        /// </summary>
        public string StartDT
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.StartDT);
            }
            set
            {
                this.SetValByKey(TaskAttr.StartDT, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// Task
        /// </summary>
        public Task()
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
                Map map = new Map("WF_Task");
                map.EnDesc = "����";
                map.EnType = EnType.Admin;

                map.AddMyPK(); //Ψһ������.
                map.AddTBString(TaskAttr.FK_Flow, null, "���̱��", true, false, 0, 200, 10);
                map.AddTBString(TaskAttr.Starter, null, "������", true, false, 0, 200, 10);
                map.AddTBString(TaskAttr.Paras, null, "����", true, false, 0, 4000, 10);

                // TaskSta 0=δ����1=�ɹ�����2=����ʧ��.
                map.AddTBInt(TaskAttr.TaskSta, 0, "����״̬", true, false);

                map.AddTBString(TaskAttr.Msg, null, "��Ϣ", true, false, 0, 4000, 10);
                map.AddTBString(TaskAttr.StartDT, null, "����ʱ��", true, false, 0, 20, 10);
                map.AddTBString(TaskAttr.RDT, null, "��������ʱ��", true, false, 0, 20, 10);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
	public class Tasks: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Task();
			}
		}
		/// <summary>
        /// ����
		/// </summary>
		public Tasks(){} 		 
		#endregion
	}
}
