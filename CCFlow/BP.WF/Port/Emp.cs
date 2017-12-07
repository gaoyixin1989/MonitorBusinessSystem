using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Port
{
	/// <summary>
	/// ������Ա����
	/// </summary>
	public class EmpAttr: BP.En.EntityNoNameAttr
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
        ///// <summary>
        ///// ��λ
        ///// </summary>
        //public const string FK_Unit = "FK_Unit";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// SID
        /// </summary>
        public const string SID = "SID";
		#endregion 
	}
	/// <summary>
	/// Emp ��ժҪ˵����
	/// </summary>
    public class Emp : EntityNoName
    {
        
        #region ��չ����
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
        /// ������λ���ϡ�
        /// </summary>
        public Stations HisStations
        {
            get
            {
                EmpStations sts = new EmpStations();
                Stations mysts = sts.GetHisStations(this.No);
                return mysts;
                //return new Station(this.FK_Station);
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
        public string FK_DeptText
        {
            get
            {
                return this.GetValRefTextByKey(EmpAttr.FK_Dept);
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
        #endregion

        public bool CheckPass(string pass)
        {
            if (this.Pass == pass)
                return true;
            return false;
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public Emp()
        {
        }
        /// <summary>
        /// ������Ա���
        /// </summary>
        /// <param name="_No">No</param>
        public Emp(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@Ҫ��ѯ�Ĳ���Ա���Ϊ�ա�");
            try
            {
                this.Retrieve();
            }
            catch (Exception ex1)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("@�û������������[" + no + "]�������ʺű�ͣ�á�@������Ϣ(���ڴ��в�ѯ���ִ���)��ex1=" + ex1.Message);
            }
        }
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
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

                #region ��������
                Map map = new Map();
                map.EnDBUrl =  new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "Port_Emp"; // Ҫ�����
                map.DepositaryOfMap = Depositary.Application;    //ʵ��map�Ĵ��λ��.
                map.DepositaryOfEntity = Depositary.Application; //ʵ����λ��
                map.EnDesc = "�û�"; // "�û�";       // ʵ�������.
                map.EnType = EnType.App;   //ʵ�����͡�
                #endregion

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 100);
                map.AddTBString(EmpAttr.Pass, "123", "����", false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, "����", new BP.Port.Depts(), true);
                map.AddTBString(EmpAttr.SID, null, "SID", false, false, 0, 20, 10);
                
              //  map.AddTBString(EmpAttr.FK_Unit, "1", "������λ", false, false, 0, 50, 10);
                #endregion �ֶ�

                map.AddSearchAttr(EmpAttr.FK_Dept); //��ѯ����.

                //���ӵ�Զ����� һ������Ա�Ĳ��Ų�ѯȨ�����λȨ��.
                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), 
                    EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "��λȨ��");
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp,
                    EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "��������");

                RefMethod rm = new RefMethod();
                rm.Title = "����";
                rm.Warning = "��ȷ��Ҫִ����?";
                rm.ClassMethodName = this.ToString() + ".DoDisableIt";
                map.AddRefMethod(rm);
                rm = new RefMethod();
                rm.Title = "����";
                rm.Warning = "��ȷ��Ҫִ����?";
                rm.ClassMethodName = this.ToString() + ".DoEnableIt";
                map.AddRefMethod(rm);
                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ִ�н���
        /// </summary>
        public string DoDisableIt()
        {
            WFEmp emp = new WFEmp(this.No);
            emp.UseSta = 0;
            emp.Update();
            return "�Ѿ�ִ��(����)�ɹ�";
        }
        /// <summary>
        /// ִ������
        /// </summary>
        public string DoEnableIt()
        {
            WFEmp emp = new WFEmp(this.No);
            emp.UseSta = 1;
            emp.Update();
            return "�Ѿ�ִ��(����)�ɹ�";
        }

        protected override bool beforeUpdate()
        {
            WFEmp emp = new WFEmp(this.No);
            emp.Update();
            return base.beforeUpdate();
        }
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
    }
	/// <summary>
	/// ������Ա
	/// </summary>
    public class Emps : EntitiesNoName
    {
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
        /// ������Աs
        /// </summary>
        public Emps()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
           return  base.RetrieveAll();

            //QueryObject qo = new QueryObject(this);
            //qo.AddWhere(EmpAttr.FK_Dept, " like ", BP.Web.WebUser.FK_Dept + "%");
            //qo.addOrderBy(EmpAttr.No);
            //return qo.DoQuery();
        }
    }
}
 