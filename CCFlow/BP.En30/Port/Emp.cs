using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;

namespace BP.Port
{
	/// <summary>
	/// ����Ա����
	/// </summary>
    public class EmpAttr : BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// sid
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
        /// �������ż���
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                EmpDepts sts = new EmpDepts();
                Depts dpts = sts.GetHisDepts(this.No);
                if (dpts.Count == 0)
                {
                    string sql = "select FK_Dept from Port_Emp where No='" + this.No + "' and FK_Dept in(select No from Port_Dept)";
                    string fk_dept = BP.DA.DBAccess.RunSQLReturnVal(sql) as string;
                    if (fk_dept == null)
                        return dpts;

                    Dept dept = new Dept(fk_dept);
                    dpts.AddEntity(dept);
                }
                return dpts;
            }
        }
        /// <summary>
        /// ���ű��
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
        /// ���ű��
        /// </summary>
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
        public string SID
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.SID);
            }
            set
            {
                this.SetValByKey(EmpAttr.SID, value);
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
            if (SystemConfig.IsDebug)
                return true;

            if (this.Pass == pass )
                return true;

            string gePass = SystemConfig.AppSettings["GenerPass"];
            if (string.IsNullOrEmpty(gePass) == true)
                return false;

            if (gePass == pass)
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
                //��½�ʺŲ�ѯ�����û���ʹ��ְ����Ų�ѯ��
                QueryObject obj = new QueryObject(this);
                obj.AddWhere(EmpAttr.No, no);
                int i = obj.DoQuery();
                if (i == 0)
                    i = this.RetrieveFromDBSources();
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
                map.EnDesc = "�û�"; // "�û�";
                map.EnType = EnType.App;   //ʵ�����͡�
                #endregion

                #region �ֶ�
                /* �����ֶ����Ե����� .. */
                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 30); 
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 200, 30);
                map.AddTBString(EmpAttr.Pass, "123", "����", false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, "����", new Port.Depts(), true);
                map.AddTBString(EmpAttr.SID, null, "��ȫУ����", false, false, 0, 36, 36);
                //map.AddTBString("Tel", null, "Tel", false, false, 0, 20, 10);
                //map.AddTBString(EmpAttr.PID, null, this.ToE("PID", "UKEY��PID"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.PIN, null, this.ToE("PIN", "UKEY��PIN"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.KeyPass, null, this.ToE("KeyPass", "UKEY��KeyPass"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.IsUSBKEY, null, this.ToE("IsUSBKEY", "�Ƿ�ʹ��usbkey"), true, false, 0, 100, 30);
                //map.AddDDLSysEnum("Sex", 0, "�Ա�", "@0=Ů@1=��");
                #endregion �ֶ�

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region ���Ӷ�Զ�����
                //���Ĳ���Ȩ��
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, 
                    EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "����Ȩ��");

                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, 
                    EmpStationAttr.FK_Station,DeptAttr.Name, DeptAttr.No, "��λȨ��");
                #endregion


                //RefMethod rm = new RefMethod();
                //rm.Title = "����";
                //rm.RefMethodType = RefMethodType.LinkeWinOpen;
                //rm.IsCanBatch =true;
                //rm.IsForEns = false;
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }

        }
        /// <summary>
        /// ����.
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            return null;
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
        #endregion ���캯��


        #region ��д����
        protected override bool beforeDelete()
        {
            // ɾ������Ա�ĸ�λ���������ͼ���п����׳��쳣��
            try
            {
                EmpStations ess = new EmpStations();
                ess.Delete(EmpDeptAttr.FK_Emp, this.No);
            }
            catch
            {
            }

            // ɾ������Ա�Ĳ��ţ��������ͼ���п����׳��쳣��
            try
            {
                EmpDepts eds = new EmpDepts();
                eds.Delete(EmpDeptAttr.FK_Emp, this.No);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
        #endregion ��д����

    }
	/// <summary>
	/// ����Ա
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
        #endregion ���췽��
    }
}
 