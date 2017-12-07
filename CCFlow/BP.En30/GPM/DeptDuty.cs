using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
	/// <summary>
	/// ����ְ��
	/// </summary>
	public class DeptDutyAttr  
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// ְ��
		/// </summary>
		public const  string FK_Duty="FK_Duty";		 
		#endregion	
	}
	/// <summary>
    /// ����ְ�� ��ժҪ˵����
	/// </summary>
    public class DeptDuty : Entity
    {
        #region ��������
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
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptDutyAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptDutyAttr.FK_Dept, value);
            }
        }
        public string FK_DutyT
        {
            get
            {
                return this.GetValRefTextByKey(DeptDutyAttr.FK_Duty);
            }
        }
        /// <summary>
        ///ְ��
        /// </summary>
        public string FK_Duty
        {
            get
            {
                return this.GetValStringByKey(DeptDutyAttr.FK_Duty);
            }
            set
            {
                SetValByKey(DeptDutyAttr.FK_Duty, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ����ְ��
        /// </summary> 
        public DeptDuty() { }
        /// <summary>
        /// ������Աְ���Ӧ
        /// </summary>
        /// <param name="_empoid">����</param>
        /// <param name="wsNo">ְ����</param> 	
        public DeptDuty(string _empoid, string wsNo)
        {
            this.FK_Dept = _empoid;
            this.FK_Duty = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("Port_DeptDuty");
                map.EnDesc = "����ְ��";
                map.EnType = EnType.Dot2Dot; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���

                map.AddTBStringPK(DeptDutyAttr.FK_Dept, null, "����", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(DeptDutyAttr.FK_Duty, null, "ְ��", new Dutys(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ����ְ�� 
	/// </summary>
	public class DeptDutys : Entities
	{
		#region ����
		/// <summary>
		/// ����ְ��
		/// </summary>
		public DeptDutys()
		{
		}
		/// <summary>
		/// ������Ա��ְ�񼯺�
		/// </summary>
		public DeptDutys(string DutyNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DeptDutyAttr.FK_Duty, DutyNo);
			qo.DoQuery();
		}		 
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DeptDuty();
			}
		}	
		#endregion 

		#region ��ѯ����
		#endregion
	}
}
