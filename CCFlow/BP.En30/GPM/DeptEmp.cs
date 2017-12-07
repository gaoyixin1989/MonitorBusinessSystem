using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ������Ա��Ϣ
    /// </summary>
    public class DeptEmpAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ְ��
        /// </summary>
        public const string FK_Duty = "FK_Duty";
        /// <summary>
        /// ְ�񼶱�
        /// </summary>
        public const string DutyLevel = "DutyLevel";
        /// <summary>
        /// �����쵼
        /// </summary>
        public const string Leader = "Leader";
        #endregion
    }
    /// <summary>
    /// ������Ա��Ϣ ��ժҪ˵����
    /// </summary>
    public class DeptEmp : EntityMyPK
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
        /// ��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DeptEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptEmpAttr.FK_Emp, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Emp;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptEmpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptEmpAttr.FK_Dept, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Emp;
            }
        }
        public string FK_DutyT
        {
            get
            {
                return this.GetValRefTextByKey(DeptEmpAttr.FK_Duty);
            }
        }
        /// <summary>
        ///ְ��
        /// </summary>
        public string FK_Duty
        {
            get
            {
                return this.GetValStringByKey(DeptEmpAttr.FK_Duty);
            }
            set
            {
                SetValByKey(DeptEmpAttr.FK_Duty, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Duty + "_" + this.FK_Emp;
            }
        }
        /// <summary>
        /// �쵼
        /// </summary>
        public string Leader
        {
            get
            {
                return this.GetValStringByKey(DeptEmpAttr.Leader);
            }
            set
            {
                SetValByKey(DeptEmpAttr.Leader, value);
            }
        }
        /// <summary>
        /// ְ�����
        /// </summary>
        public int DutyLevel
        {
            get
            {
                return this.GetValIntByKey(DeptEmpAttr.DutyLevel);
            }
            set
            {
                this.SetValByKey(DeptEmpAttr.DutyLevel, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ����������Ա��Ϣ
        /// </summary> 
        public DeptEmp() { }
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="deptNo">���ű��</param>
        /// <param name="empNo">��Ա���</param>
        public DeptEmp(string deptNo, string empNo)
        {
            this.FK_Dept = deptNo;
            this.FK_Emp = empNo;
            this.MyPK = this.FK_Dept + "_" + this.FK_Emp;
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

                Map map = new Map("Port_DeptEmp");
                map.EnDesc = "������Ա��Ϣ";
                
                map.AddMyPK();
                map.AddTBString(DeptEmpAttr.FK_Emp, null, "����Ա", false, false, 1, 50, 1);
                map.AddTBString(DeptEmpAttr.FK_Dept, null, "����", false, false, 1, 50, 1);
                map.AddTBString(DeptEmpAttr.FK_Duty, null, "ְ��", false, false, 0, 50, 1);
                map.AddTBInt(DeptEmpAttr.DutyLevel, 0, "ְ�񼶱�", false, false);

                map.AddTBString(DeptEmpAttr.Leader, null, "�쵼", false, false, 0, 50, 1);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ����ǰ��������
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Dept + "_" + this.FK_Emp;
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ������Ա��Ϣ 
    /// </summary>
    public class DeptEmps : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ����������Ա��Ϣ
        /// </summary>
        public DeptEmps()
        {
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
                return new DeptEmp();
            }
        }
        #endregion

    }
}
