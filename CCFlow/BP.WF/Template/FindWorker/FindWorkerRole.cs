using System;
using System.Data;
using BP.DA;
using BP.En;
using System.Collections;
using BP.Web;
using BP.GPM;
using BP.Sys;

namespace BP.WF.Template
{
    public enum FindColleague
    {
        /// <summary>
        /// ���� 
        /// </summary>
        All,
        /// <summary>
        /// ָ��ְ��
        /// </summary>
        SpecDuty,
        /// <summary>
        /// ָ����λ
        /// </summary>
        SpecStation      
    }

    /// <summary>
    /// ���˹�������
    /// </summary>
    public class FindWorkerRoleAttr : BP.En.EntityOIDNameAttr
    {
        #region ��������
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";

        /// <summary>
        /// ����0ֵ
        /// </summary>
        public const string SortVal0 = "SortVal0";
        /// <summary>
        /// ����0��ǩ
        /// </summary>
        public const string SortText0 = "SortText0";

        /// <summary>
        /// ����1ֵ
        /// </summary>
        public const string SortVal1 = "SortVal1";
        /// <summary>
        /// ����1��ǩ
        /// </summary>
        public const string SortText1 = "SortText1";

        /// <summary>
        /// ����2ֵ
        /// </summary>
        public const string SortVal2 = "SortVal2";
        /// <summary>
        /// ����2��ǩ
        /// </summary>
        public const string SortText2 = "SortText2";


        /// ����2ֵ
        /// </summary>
        public const string SortVal3 = "SortVal3";
        /// <summary>
        /// ����2��ǩ
        /// </summary>
        public const string SortText3 = "SortText3";

        /// ����4ֵ
        /// </summary>
        public const string SortVal4 = "SortVal4";
        /// <summary>
        /// ����4��ǩ
        /// </summary>
        public const string SortText4 = "SortText4";


        /// <summary>
        /// Tag1ֵ
        /// </summary>
        public const string TagVal0 = "TagVal0";
        /// <summary>
        /// Tag1��ǩ
        /// </summary>
        public const string TagText0 = "TagText0";

       
        /// <summary>
        /// Tag1ֵ
        /// </summary>
        public const string TagVal1 = "TagVal1";
        /// <summary>
        /// Tag1��ǩ
        /// </summary>
        public const string TagText1 = "TagText1";
        /// <summary>
        /// Tag1ֵ
        /// </summary>
        public const string TagVal2 = "TagVal2";
        /// <summary>
        /// Tag1��ǩ
        /// </summary>
        public const string TagText2 = "TagText2";
        
        /// <summary>
        /// Tag1ֵ
        /// </summary>
        public const string TagVal3 = "TagVal3";
        /// <summary>
        /// Tag1��ǩ
        /// </summary>
        public const string TagText3 = "TagText3";
       

        /// <summary>
        /// ˳���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// �Ƿ���ã�
        /// </summary>
        public const string IsEnable = "IsEnable";
        #endregion
    }
    /// <summary>
    /// ���˹���
    /// </summary>
    public class FindWorkerRole : EntityOIDName
    {
        #region  ��ͬ��
        /// <summary>
        /// ��ͬ�¹���
        /// </summary>
        public FindColleague HisFindColleague
        {
            get
            {
                return (FindColleague)int.Parse(this.TagVal3);
            }
        }
        #endregion  ��ͬ��


        #region  ���쵼����
        /// <summary>
        /// Ѱ���쵼����
        /// </summary>
        public FindLeaderType HisFindLeaderType
        {
            get
            {
                return (FindLeaderType)int.Parse(this.SortVal1);
            }
        }
        /// <summary>
        /// ģʽ
        /// </summary>
        public FindLeaderModel HisFindLeaderModel
        {
            get
            {
                return (FindLeaderModel)int.Parse(this.SortVal2);
            }
        }
        #endregion


        #region ��������
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(FindWorkerRoleAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.IsEnable, value);
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
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// ���˹����������
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(FindWorkerRoleAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.FK_Node, value);
            }
        }

        /// <summary>
        /// ���0ֵ
        /// </summary>
        public string SortVal0
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortVal0);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortVal0, value);
            }
        }
        /// <summary>
        /// ���0Text
        /// </summary>
        public string SortText0
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortText0);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortText0, value);
            }
        }

        public string SortText3
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortText3);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortText3, value);
            }
        }

        /// <summary>
        /// ���1ֵ
        /// </summary>
        public string SortVal1
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortVal1);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortVal1, value);
            }
        }
        /// <summary>
        /// ���1Text
        /// </summary>
        public string SortText1
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortText1);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortText1, value);
            }
        }

        /// <summary>
        /// ���2ֵ
        /// </summary>
        public string SortVal2
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortVal2);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortVal2, value);
            }
        }
        /// <summary>
        /// ���2Text
        /// </summary>
        public string SortText2
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortText2);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortText2, value);
            }
        }
        /// <summary>
        /// ���3ֵ
        /// </summary>
        public string SortVal3
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortVal3);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortVal3, value);
            }
        }
        /// <summary>
        /// ���3Text
        /// </summary>
        public string SortText4
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.SortText4);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.SortText4, value);
            }
        }
        /// <summary>
        /// ����0
        /// </summary>
        public string TagVal0
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagVal0);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagVal0, value);
            }
        }
        /// <summary>
        /// ����1
        /// </summary>
        public string TagVal1
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagVal1);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagVal1, value);
            }
        }
        /// <summary>
        /// TagVal2
        /// </summary>
        public string TagVal2
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagVal2);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagVal2, value);
            }
        }
        /// <summary>
        /// TagVal3
        /// </summary>
        public string TagVal3
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagVal3);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagVal3, value);
            }
        }
        /// <summary>
        /// ����0
        /// </summary>
        public string TagText0
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagText0);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagText0, value);
            }
        }
        /// <summary>
        /// TagText1
        /// </summary>
        public string TagText1
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagText1);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagText1, value);
            }
        }

        /// <summary>
        /// ����1
        /// </summary>
        public string TagText2
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagText2);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagText2, value);
            }
        }
        /// <summary>
        /// TagText3
        /// </summary>
        public string TagText3
        {
            get
            {
                return this.GetValStringByKey(FindWorkerRoleAttr.TagText3);
            }
            set
            {
                this.SetValByKey(FindWorkerRoleAttr.TagText3, value);
            }
        }
        #endregion

        #region ����
        public WorkNode town = null;
        public WorkNode currWn = null;
        public Flow fl = null;
        string dbStr = BP.Sys.SystemConfig.AppCenterDBVarStr;
        public Paras ps = null;
        public Int64 WorkID = 0;
        public Node HisNode = null;
        #endregion ����

        #region ���캯��
        /// <summary>
        /// ���˹���
        /// </summary>
        public FindWorkerRole() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FindWorkerRole");
                map.EnDesc = "���˹���"; // "���˹���";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();

                map.AddTBString(FindWorkerRoleAttr.Name, null, "Name", true, false, 0, 200, 0);

                map.AddTBInt(FindWorkerRoleAttr.FK_Node, 0, "�ڵ�ID", false, false);

                // ����洢.
                map.AddTBString(FindWorkerRoleAttr.SortVal0, null, "SortVal0", true, false, 0, 200, 0);
                map.AddTBString(FindWorkerRoleAttr.SortText0, null, "SortText0", true, false, 0, 200, 0);

                map.AddTBString(FindWorkerRoleAttr.SortVal1, null, "SortVal1", true, false, 0, 200, 0);
                map.AddTBString(FindWorkerRoleAttr.SortText1, null, "SortText1", true, false, 0, 200, 0);

                map.AddTBString(FindWorkerRoleAttr.SortVal2, null, "SortText2", true, false, 0, 200, 0);
                map.AddTBString(FindWorkerRoleAttr.SortText2, null, "SortText2", true, false, 0, 200, 0);

                map.AddTBString(FindWorkerRoleAttr.SortVal3, null, "SortVal3", true, false, 0, 200, 0);
                map.AddTBString(FindWorkerRoleAttr.SortText3, null, "SortText3", true, false, 0, 200, 0);


                // ����ɼ���Ϣֵ�洢.
                map.AddTBString(FindWorkerRoleAttr.TagVal0, null, "TagVal0", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagVal1, null, "TagVal1", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagVal2, null, "TagVal2", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagVal3, null, "TagVal3", true, false, 0, 1000, 0);

                // TagText
                map.AddTBString(FindWorkerRoleAttr.TagText0, null, "TagText0", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagText1, null, "TagText1", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagText2, null, "TagText2", true, false, 0, 1000, 0);
                map.AddTBString(FindWorkerRoleAttr.TagText3, null, "TagText3", true, false, 0, 1000, 0);

                map.AddTBInt(FindWorkerRoleAttr.IsEnable, 1, "�Ƿ����", false, false);
                map.AddTBInt(FindWorkerRoleAttr.Idx, 0, "IDX", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region �ڲ�����.
        /// <summary>
        /// ����
        /// </summary>
        public void DoUp()
        {
            this.DoOrderUp(FindWorkerRoleAttr.FK_Node, this.FK_Node.ToString(), FindWorkerRoleAttr.Idx);
        }
        /// <summary>
        /// ����
        /// </summary>
        public void DoDown()
        {
            this.DoOrderDown(FindWorkerRoleAttr.FK_Node, this.FK_Node.ToString(), FindWorkerRoleAttr.Idx);
        }
        private string sql = "";
        #endregion �ڲ�����
 
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DataTable GenerWorkerOfDataTable()
        {
            DataTable dt = new DataTable();
            // �����жϵ�һ���
            switch (this.SortVal0)
            {
                case "ByDept":
                    return this.GenerByDept();
                case "Leader":
                case "SpecEmps":

                    #region   �����ҵ�2�����������ǵ�������˭��
                    string empNo = null;
                    string empDept = null;
                    switch (this.HisFindLeaderType)
                    {
                        case FindLeaderType.Submiter: // ��ǰ�ύ�˵�ֱ���쵼
                            empNo = BP.Web.WebUser.No;
                            empDept = BP.Web.WebUser.FK_Dept;
                            break;
                        case FindLeaderType.SpecNodeSubmiter: // ָ���ڵ��ύ�˵�ֱ���쵼.
                            sql = "SELECT FK_Emp,FK_Dept FROM WF_GenerWorkerlist WHERE WorkID=" + this.WorkID + " AND FK_Node=" + this.TagVal1;
                            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                            if (dt.Rows.Count == 0)
                                throw new Exception("@û���ҵ�ָ���ڵ����ݣ��뷴����ϵͳ����Ա��������Ϣ:" + sql);
                            empNo = dt.Rows[0][0] as string;
                            empDept = dt.Rows[0][1] as string;
                            break;
                        case FindLeaderType.BySpecField: //ָ���ڵ��ֶ���Ա��ֱ���쵼..
                            sql = " SELECT " + this.TagVal1 + " FROM " + this.HisNode.HisFlow.PTable + " WHERE OID=" + this.WorkID;
                            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                            empNo = dt.Rows[0][0] as string;
                            if (string.IsNullOrEmpty(empNo))
                                throw new Exception("@ָ���Ľڵ��ֶ�(" + this.TagVal1 + ")��ֵΪ��.");
                            //ָ����
                            Emp emp = new Emp();
                            emp.No = empNo;
                            if (emp.RetrieveFromDBSources() == 0)
                                throw new Exception("@ָ���Ľڵ��ֶ�(" + this.TagVal1 + ")��ֵ(" + empNo + ")�ǷǷ�����Ա���...");
                            empDept = emp.FK_Dept;
                            break;
                        default:
                            throw new Exception("@��δ�����Case:" + this.HisFindLeaderType);
                            break;
                    }
                    if (string.IsNullOrEmpty(empNo))
                        throw new Exception("@��©���жϲ��裬û���ҵ�ָ���Ĺ�����Ա.");
                    #endregion

                    if (this.SortVal0 == "Leader")
                        return GenerHisLeader(empNo, empDept); // ���������쵼������.
                    else
                        return GenerHisSpecEmps(empNo, empDept); // ���������ض���ͬ�²�����.
                default:
                    break;
            }
            return null;
        }

        #region �����Ų���
        private DataTable GenerByDept()
        {
            //���ű��.
            string deptNo = this.TagVal1;

            //ְ��-��λ��
            string objVal = this.TagVal2;

            string way = this.SortVal1;

            string sql = "";
            switch (way)
            {
                case "0": //��ְ����.
                    sql = "SELECT B.No,B.Name FROM Port_DeptEmp A, Port_Emp B WHERE A.FK_Dept='"+deptNo+"'  AND A.FK_Duty='"+objVal+"' AND B.No=A.FK_Emp";
                    break;
                case "1": //����λ��.
                    sql = "SELECT B.No,B.Name FROM Port_DeptEmpStation A, Port_Emp B WHERE A.FK_Dept='" + deptNo + "'  AND A.FK_Station='" + objVal + "' AND B.No=A.FK_Emp";
                    break;
                case "2": //���иò��ŵ���Ա.
                    sql = "SELECT B.No,B.Name FROM Port_DeptEmp A, Port_Emp B WHERE A.FK_Dept='" + deptNo + "' AND B.No=A.FK_Emp";
                    break;
                default:
                    break;
            }
            return DBAccess.RunSQLReturnTable(sql);
        }
        #endregion

        #region ��ͬ��
        /// <summary>
        /// ��ǰ�ύ�˵�ֱ���쵼
        /// </summary>
        /// <returns></returns>
        private DataTable GenerHisSpecEmps(string empNo, string empDept)
        {
            DeptEmp de = new DeptEmp();

            DataTable dt = new DataTable();
            string leader = null;
            string tempDeptNo = "";

            switch (this.HisFindColleague)
            {
                case FindColleague.All: // ���иò��������µ���Ա.
                    sql = "SELECT Leader FROM Port_DeptEmp WHERE FK_Emp='" + empNo + "' AND FK_Dept='" + empDept + "'";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    leader = dt.Rows[0][0] as string;
                    if (string.IsNullOrEmpty(leader))
                        throw new Exception("@ϵͳ����Աû�и�(" + empNo + ")�ڲ���(" + empDept + ")������ֱ���쵼.");
                    break;
                case FindColleague.SpecDuty: // �ض�ְ�񼶱���쵼.
                    tempDeptNo = empDept.Clone() as string;
                    while (true)
                    {
                        sql = "SELECT FK_Emp FROM Port_DeptEmp WHERE DutyLevel='" + this.TagVal2 + "' AND FK_Dept='" + tempDeptNo + "'";
                        DataTable mydt = DBAccess.RunSQLReturnTable(sql);
                        if (mydt.Rows.Count != 0)
                            return mydt; /*ֱ�ӷ���.*/

                        Dept d = new Dept(tempDeptNo);
                        if (d.ParentNo == "0")
                            return null; /*������˸��ڵ�.*/
                        tempDeptNo = d.ParentNo;
                    }
                    break;
                case FindColleague.SpecStation: // �ض���λ���쵼.
                    tempDeptNo = empDept.Clone() as string;
                    while (true)
                    {
                        sql = "SELECT FK_Emp FROM Port_DeptEmpStation WHERE FK_Station='" + this.TagVal2 + "' AND FK_Dept='" + tempDeptNo + "'";
                        DataTable mydt = DBAccess.RunSQLReturnTable(sql);
                        if (mydt.Rows.Count != 0)
                            return mydt; /*ֱ�ӷ���.*/

                        Dept d = new Dept(tempDeptNo);
                        if (d.ParentNo == "0")
                        {
                            /* ��ֱ���쵼��û���ҵ� */
                            return null; /*������˸��ڵ�.*/
                        }
                        tempDeptNo = d.ParentNo;
                    }
                    break;
                default:
                    break;
            }

            // ������.
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            DataRow dr = dt.NewRow();
            dr[0] = leader;
            dt.Rows.Add(dr);
            return dt;
        }
        public string ErrMsg = null;
        #endregion ֱ���쵼

        #region ֱ���쵼
        /// <summary>
        /// ��ǰ�ύ�˵�ֱ���쵼
        /// </summary>
        /// <returns></returns>
        private DataTable GenerHisLeader(string empNo,string empDept)
        {
            DeptEmp de = new DeptEmp();

            DataTable dt=new DataTable();
            string leader=null;
            string tempDeptNo = "";

            switch (this.HisFindLeaderModel)
            {
                case FindLeaderModel.DirLeader: // ֱ���쵼.
                    sql = "SELECT Leader FROM Port_DeptEmp WHERE FK_Emp='" + empNo + "' AND FK_Dept='" + empDept + "'";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    leader = dt.Rows[0][0] as string;
                    if (string.IsNullOrEmpty(leader))
                        throw new Exception("@ϵͳ����Աû�и�(" + empNo + ")�ڲ���(" + empDept + ")������ֱ���쵼.");
                    break;
                case FindLeaderModel.SpecDutyLevelLeader: // �ض�ְ�񼶱���쵼.
                    tempDeptNo = empDept.Clone() as string;
                    while (true)
                    {
                        sql = "SELECT FK_Emp FROM Port_DeptEmp WHERE DutyLevel='" + this.TagVal2 + "' AND FK_Dept='" + tempDeptNo + "'";
                        DataTable mydt = DBAccess.RunSQLReturnTable(sql);
                        if (mydt.Rows.Count != 0)
                            return mydt; /*ֱ�ӷ���.*/

                        Dept d = new Dept(tempDeptNo);
                        if (d.ParentNo == "0")
                            return null; /*������˸��ڵ�.*/
                        tempDeptNo = d.ParentNo;
                    }
                    break;
                case FindLeaderModel.DutyLeader: // �ض�ְ����쵼.
                    tempDeptNo = empDept.Clone() as string;
                    while (true)
                    {
                          sql = "SELECT FK_Emp FROM Port_DeptEmp WHERE FK_Duty='" + this.TagVal2 + "' AND FK_Dept='" + tempDeptNo + "'";
                          DataTable mydt = DBAccess.RunSQLReturnTable(sql);
                        if (mydt.Rows.Count != 0)
                            return mydt; /*ֱ�ӷ���.*/

                        Dept d = new Dept(tempDeptNo);
                        if (d.ParentNo == "0")
                            return null; /*������˸��ڵ�.*/
                        tempDeptNo = d.ParentNo;
                    }
                    break;
                case FindLeaderModel.SpecStation: // �ض���λ���쵼.
                    tempDeptNo = empDept.Clone() as string;
                    while (true)
                    {
                        sql = "SELECT FK_Emp FROM Port_DeptEmpStation WHERE FK_Station='" + this.TagVal2 + "' AND FK_Dept='" + tempDeptNo + "'";
                        DataTable mydt = DBAccess.RunSQLReturnTable(sql);
                        if (mydt.Rows.Count != 0)
                            return mydt; /*ֱ�ӷ���.*/

                        Dept d = new Dept(tempDeptNo);
                        if (d.ParentNo == "0")
                        {
                            /* ��ֱ���쵼��û���ҵ� */
                            return null; /*������˸��ڵ�.*/
                        }
                        tempDeptNo = d.ParentNo;
                    }
                    break;
                default:
                    break;
            }

            // ������.
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            DataRow dr = dt.NewRow();
            dr[0] = leader;
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion ֱ���쵼

        public string DBStr
        {
            get
            {
                return SystemConfig.AppCenterDBVarStr;
            }
        }
    }
    /// <summary>
    /// ���˹��򼯺�
    /// </summary>
    public class FindWorkerRoles : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FindWorkerRole();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���˹��򼯺�
        /// </summary>
        public FindWorkerRoles()
        {
        }
        /// <summary>
        /// ���˹��򼯺�
        /// </summary>
        /// <param name="nodeID"></param>
        public FindWorkerRoles(int nodeID)
        {
            this.Retrieve(FindWorkerRoleAttr.FK_Node, nodeID, FindWorkerRoleAttr.Idx);
        }
        #endregion
    }
}
