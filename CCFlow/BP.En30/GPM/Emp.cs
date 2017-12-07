using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GPM
{
    /// <summary>
    /// ����Ա����
    /// </summary>
    public class EmpAttr : BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// Ա�����
        /// </summary>
        public const string EmpNo = "EmpNo";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ְ��
        /// </summary>
        public const string FK_Duty = "FK_Duty";
        /// <summary>
        /// FK_Unit
        /// </summary>
        public const string FK_Unit = "FK_Unit";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// sid
        /// </summary>
        public const string SID = "SID";
        /// <summary>
        /// �绰
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// ����
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// ��������
        /// </summary>
        public const string NumOfDept = "NumOfDept";
        /// <summary>
        /// ���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// �쵼
        /// </summary>
        public const string Leader = "Leader";
        #endregion
    }
    /// <summary>
    /// ����Ա ��ժҪ˵����
    /// </summary>
    public class Emp : EntityNoName
    {
        #region ��չ����
        /// <summary>
        /// Ա�����
        /// </summary>
        public string EmpNo
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.EmpNo);
            }
            set
            {
                this.SetValByKey(EmpAttr.EmpNo, value);
            }
        }
        /// <summary>
        /// ��Ҫ�Ĳ��š�
        /// </summary>
        public Dept HisDept
        {
            get
            {
                try
                {
                    return new Dept(this.FK_Dept);
                }
                catch (Exception ex)
                {
                    throw new Exception("@��ȡ����Ա" + this.No + "����[" + this.FK_Dept + "]���ִ���,������ϵͳ����Աû�и���ά������.@" + ex.Message);
                }
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(EmpAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ְ��
        /// </summary>
        public string FK_Duty
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.FK_Duty);
            }
            set
            {
                this.SetValByKey(EmpAttr.FK_Duty, value);
            }
        }
        
        public string FK_DeptText
        {
            get
            {
                return this.GetValRefTextByKey(EmpAttr.FK_Dept);
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Tel);
            }
            set
            {
                this.SetValByKey(EmpAttr.Tel, value);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Email);
            }
            set
            {
                this.SetValByKey(EmpAttr.Email, value);
            }
        }

        public string Leader
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Leader);
            }
            set
            {
                this.SetValByKey(EmpAttr.Leader, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Pass
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Pass);
            }
            set
            {
                this.SetValByKey(EmpAttr.Pass, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(EmpAttr.Idx);
            }
            set
            {
                this.SetValByKey(EmpAttr.Idx, value);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �������(������д�˷���)
        /// </summary>
        /// <param name="pass">����</param>
        /// <returns>�Ƿ�ƥ��ɹ�</returns>
        public bool CheckPass(string pass)
        {
            if (this.Pass == pass)
                return true;
            return false;
        }
        #endregion ��������

        #region ���캯��
        /// <summary>
        /// ����Ա
        /// </summary>
        public Emp()
        {
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        /// <param name="no">���</param>
        public Emp(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@Ҫ��ѯ�Ĳ���Ա���Ϊ�ա�");
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("@�û������������[" + no + "]�������ʺű�ͣ�á�@������Ϣ(���ڴ��в�ѯ���ִ���)��ex1=" + ex.Message);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
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

                Map map = new Map();

                #region ��������
                map.EnDBUrl =
                    new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "Port_Emp"; // Ҫ�����
                map.DepositaryOfMap = Depositary.Application;    //ʵ��map�Ĵ��λ��.
                map.DepositaryOfEntity = Depositary.Application; //ʵ����λ��
                map.EnDesc = "�û�"; // "�û�"; // ʵ�������.
                map.EnType = EnType.App;   //ʵ�����͡�
                #endregion

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 30);
                map.AddTBString(EmpAttr.EmpNo, null, "ְ�����", true, false, 0, 20, 30);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(EmpAttr.Pass, "123", "����", false, false, 0, 100, 10);

                //map.AddDDLEntities(EmpAttr.FK_Dept, null, "����", new Port.Depts(), true);

                map.AddTBString(EmpAttr.FK_Dept, null, "��ǰ����", false, false, 0, 20, 10);
                map.AddTBString(EmpAttr.FK_Duty, null, "��ǰְ��", false, false, 0, 20, 10);
                map.AddTBString(EmpAttr.Leader, null, "��ǰ�쵼", false, false, 0, 50, 1);

                map.AddTBString(EmpAttr.SID, null, "��ȫУ����", false, false, 0, 36, 36);
                map.AddTBString(EmpAttr.Tel, null, "�绰", true, false, 0, 20, 130);
                map.AddTBString(EmpAttr.Email, null, "����", true, false, 0, 100, 132);
                map.AddTBInt(EmpAttr.NumOfDept, 0, "��������", true, false);

                map.AddTBInt(EmpAttr.Idx, 0, "���", true, false);
                #endregion �ֶ�

                 map.AddSearchAttr(EmpAttr.FK_Dept);

                ////#region ���ӵ�Զ�����
                ////���Ĳ���Ȩ��
                //map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept,
                //    DeptAttr.Name, DeptAttr.No, "����Ȩ��");

                //map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station,
                //    DeptAttr.Name, DeptAttr.No, "��λȨ��");
                ////#endregion

                RefMethod rm = new RefMethod();
                rm.Title = "�޸�����";
                rm.ClassMethodName = this.ToString() + ".DoResetpassword";
                rm.HisAttrs.AddTBString("pass1", null, "��������", true, false, 0, 100, 100);
                rm.HisAttrs.AddTBString("pass2", null, "�ٴ�����", true, false, 0, 100, 100);
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }

        /// <summary>
        /// �����ƶ�
        /// </summary>
        public void DoUp()
        {
            this.DoOrderUp(EmpAttr.FK_Dept, this.FK_Dept, EmpAttr.Idx);
        }
        /// <summary>
        /// �����ƶ�
        /// </summary>
        public void DoDown()
        {
            this.DoOrderDown(EmpAttr.FK_Dept, this.FK_Dept, EmpAttr.Idx);
        }

        public string DoResetpassword(string pass1, string pass2)
        {
            if (pass1.Equals(pass2) == false)
                return "�������벻һ��";

            this.Pass = pass1;
            this.Update();
            return "�������óɹ�";
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
        #endregion ���캯��
    }
    /// <summary>
    /// ����Աs
    // </summary>
    public class Emps : EntitiesNoName
    {
        #region ���췽��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Emp();
            }
        }
        /// <summary>
        /// ����Աs
        /// </summary>
        public Emps()
        {
        }
        public override int RetrieveAll()
        {
            return base.RetrieveAll("Name");
        }
        #endregion ���췽��
    }
}
