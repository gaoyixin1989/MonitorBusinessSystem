using System;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Web;
using BP.Sys;
using BP.WF.Template;
using BP.WF.Data;

namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵��.
    /// ������. 
    /// �����������������  
    /// ��������Ϣ��
    /// ���̵���Ϣ.
    /// </summary>
    public class WorkNode
    {
        #region Ȩ���ж�
        /// <summary>
        /// �ж�һ�����ܲ��ܶ���������ڵ���в�����
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        private bool IsCanOpenCurrentWorkNode(string empId)
        {
            WFState stat = this.HisGenerWorkFlow.WFState;
            if (stat == WFState.Runing)
            {
                if (this.HisNode.IsStartNode)
                {
                    /*����ǿ�ʼ�����ڵ㣬�ӹ�����λ�ж�����û�й�����Ȩ�ޡ�*/
                    return WorkFlow.IsCanDoWorkCheckByEmpStation(this.HisNode.NodeID, empId);
                }
                else
                {
                    /* ����ǳ�ʼ���׶�,�ж����ĳ�ʼ���ڵ� */
                    GenerWorkerList wl = new GenerWorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Emp = empId;

                    Emp myEmp = new Emp(empId);
                    wl.FK_EmpText = myEmp.Name;

                    wl.FK_Node = this.HisNode.NodeID;
                    wl.FK_NodeText = this.HisNode.Name;
                    return wl.IsExits;
                }
            }
            else
            {
                /* ����ǳ�ʼ���׶� */
                return false;
            }
        }
        #endregion

        #region ����/����.
        /// <summary>
        /// ���߳��Ƿ��з����־.
        /// </summary>
        private bool IsHaveSubThreadGroupMark = false;


        private string _execer = null;
        /// <summary>
        /// ʵ��ִ���ˣ�ִ�й�������ʱ����ʱ��ǰ WebUser.No ����ʵ�ʵ�ִ���ˡ�
        /// </summary>
        public string Execer
        {
            get
            {
                if (_execer == null)
                    _execer = WebUser.No;
                return _execer;
            }
            set
            {
                _execer = value;
            }
        }
        private string _execerName = null;
        /// <summary>
        /// ʵ��ִ��������(��ο�ʵ��ִ����)
        /// </summary>
        public string ExecerName
        {
            get
            {
                if (_execerName == null)
                    _execerName = WebUser.Name;
                return _execerName;
            }
            set
            {
                _execerName = value;
            }
        }
        private string _execerDeptName = null;
        /// <summary>
        /// ʵ��ִ��������(��ο�ʵ��ִ����)
        /// </summary>
        public string ExecerDeptName
        {
            get
            {
                if (_execerDeptName == null)
                    _execerDeptName = WebUser.FK_DeptName;
                return _execerDeptName;
            }
            set
            {
                _execerDeptName = value;
            }
        }
        private string _execerDeptNo = null;
        /// <summary>
        /// ʵ��ִ��������(��ο�ʵ��ִ����)
        /// </summary>
        public string ExecerDeptNo
        {
            get
            {
                if (_execerDeptNo == null)
                    _execerDeptNo = WebUser.FK_Dept;
                return _execerDeptNo;
            }
            set
            {
                _execerDeptNo = value;
            }
        }
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        private string _VirPath = null;
        /// <summary>
        /// ����Ŀ¼��·�� 
        /// </summary>
        public string VirPath
        {
            get
            {
                if (_VirPath == null && BP.Sys.SystemConfig.IsBSsystem)
                    _VirPath = Glo.CCFlowAppPath;//BP.Sys.Glo.Request.ApplicationPath;
                return _VirPath;
            }
        }
        private string _AppType = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string AppType
        {
            get
            {
                if (BP.Sys.SystemConfig.IsBSsystem == false)
                {
                    return "CCFlow";
                }

                if (_AppType == null && BP.Sys.SystemConfig.IsBSsystem)
                {
                    if (BP.Web.WebUser.IsWap)
                        _AppType = "WF/WAP";
                    else
                    {
                        bool b = BP.Sys.Glo.Request.RawUrl.ToLower().Contains("oneflow");
                        if (b)
                            _AppType = "WF/OneFlow";
                        else
                            _AppType = "WF";
                    }
                }
                return _AppType;
            }
        }
        private string nextStationName = "";
        public WorkNode town = null;
        private bool IsFindWorker = false;
        public bool IsSubFlowWorkNode
        {
            get
            {
                if (this.HisWork.FID == 0)
                    return false;
                else
                    return true;
            }
        }
        #endregion ����/����.

        #region GenerWorkerList ��ط���.
        //��ѯ��ÿ���ڵ����Ľ����˼��ϣ�Emps����
        public string GenerEmps(Node nd)
        {
            string str = "";
            foreach (GenerWorkerList wl in this.HisWorkerLists)
                str = wl.FK_Emp + ",";
            return str;
        }
        /// <summary>
        /// �������Ĺ�����.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public GenerWorkerLists Func_GenerWorkerLists(WorkNode town)
        {
            this.town = town;

            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string FK_Emp;

            // ���ָ���ض�����Ա����
            if (string.IsNullOrEmpty(JumpToEmp) == false)
            {
                string[] emps = JumpToEmp.Split(',');
                foreach (string emp in emps)
                {
                    if (string.IsNullOrEmpty(emp))
                        continue;
                    DataRow dr = dt.NewRow();
                    dr[0] = emp;
                    dt.Rows.Add(dr);
                }


                /*�����������߹���.*/

                // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ��,������Ϊ�˱������
                ps = new Paras();
                ps.Add("WorkID", this.HisWork.OID);
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node =" + dbStr + "FK_Node";
                DBAccess.RunSQL(ps);

                return InitWorkerLists(town, dt);
            }

            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ��,������Ϊ�˱������
            ps = new Paras();
            ps.Add("WorkID", this.HisWork.OID);
            ps.Add("FK_Node", town.HisNode.NodeID);
            ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node =" + dbStr + "FK_Node";
            DBAccess.RunSQL(ps);

            if (this.town.HisNode.HisDeliveryWay == DeliveryWay.ByCCFlowBPM 
                || 1 == 1)
            {
                /*��������˰�ccflow��BPMģʽ*/
                while (true)
                {
                    FindWorker fw = new FindWorker();
                    dt = fw.DoIt(this.HisFlow, this, town);
                    if (dt == null)
                        throw new Exception("@û���ҵ�������.");

                    return InitWorkerLists(town, dt);
                }
            }
            throw new Exception("@�˲��ִ����Ѿ��Ƴ���.");
        }
        /// <summary>
        /// ���ݲ��Ż�ȡ��һ���Ĳ���Ա
        /// </summary>
        /// <param name="deptNo"></param>
        /// <param name="emp1"></param>
        /// <returns></returns>
        public GenerWorkerLists Func_GenerWorkerList_DiGui(string deptNo, string empNo)
        {
            string sql = "SELECT NO FROM Port_Emp WHERE No IN "
                + "(SELECT  FK_Emp  FROM "+BP.WF.Glo.EmpStation+" WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )"
                + " AND  NO IN "
                + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept=" + dbStr + "FK_Dept )"
                + " AND No!=" + dbStr + "FK_Emp";

            ps = new Paras();
            ps.SQL = sql;
            ps.Add("FK_Node", town.HisNode.NodeID);
            ps.Add("FK_Emp", empNo);
            ps.Add("FK_Dept", deptNo);

            DataTable dt = DBAccess.RunSQLReturnTable(ps);
            if (dt.Rows.Count == 0)
            {
                NodeStations nextStations = town.HisNode.NodeStations;
                if (nextStations.Count == 0)
                    throw new Exception("@�ڵ�û�и�λ:" + town.HisNode.NodeID + "  " + town.HisNode.Name);

                sql = "SELECT No FROM Port_Emp WHERE No IN ";
                sql += "(SELECT  FK_Emp  FROM "+BP.WF.Glo.EmpStation+" WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )";
                sql += " AND No IN ";

                if (deptNo == "1")
                    sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!=" + dbStr + "FK_Emp ) ";
                else
                {
                    Dept deptP = new Dept(deptNo);
                    sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!=" + dbStr + "FK_Emp AND FK_Dept = '" + deptP.ParentNo + "')";
                }

                ps = new Paras();
                ps.SQL = sql;
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.Add("FK_Emp", empNo);

                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                {
                    sql = "SELECT No FROM Port_Emp WHERE No!=" + dbStr + "FK_Emp AND No IN ";
                    sql += "(SELECT  FK_Emp  FROM "+BP.WF.Glo.EmpStation+" WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )";
                    ps = new Paras();
                    ps.SQL = sql;
                    ps.Add("FK_Emp", empNo);
                    ps.Add("FK_Node", town.HisNode.NodeID);
                    dt = DBAccess.RunSQLReturnTable(ps);
                    if (dt.Rows.Count == 0)
                        throw new Exception("@��λ(" + town.HisNode.HisStationsStr + ")��û����Ա����Ӧ�ڵ�:" + town.HisNode.Name);
                }
                return InitWorkerLists(town, dt);
            }
            else
            {
                return InitWorkerLists(town, dt);
            }
            return null;
        }
        #endregion GenerWorkerList ��ط���.

        /// <summary>
        /// ����һ�� word
        /// </summary>
        public void DoPrint()
        {
            string tempFile = SystemConfig.PathOfTemp + "\\" + this.WorkID + ".doc";
            Work wk = this.HisNode.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();
            Glo.GenerWord(tempFile, wk);
            PubClass.OpenWordDocV2(tempFile, this.HisNode.Name + ".doc");
        }
        string dbStr = SystemConfig.AppCenterDBVarStr;
        public Paras ps = new Paras();
        /// <summary>
        /// �ݹ�ɾ�������ڵ�֮�������
        /// </summary>
        /// <param name="nds">����Ľڵ㼯��</param>
        public void DeleteToNodesData(Nodes nds)
        {
            if (this.HisFlow.HisDataStoreModel == DataStoreModel.SpecTable)
                return;

            /*��ʼ��������Ľڵ㼯��*/
            foreach (Node nd in nds)
            {
                Work wk = nd.HisWork;
                if (wk.EnMap.PhysicsTable == this.HisFlow.PTable)
                    continue;

                wk.OID = this.WorkID;
                if (wk.Delete() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.EnMap.PhysicsTable == this.HisFlow.PTable)
                        continue;
                    if (wk.Delete(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                #region ɾ����ǰ�ڵ����ݣ�ɾ��������Ϣ��
                // ɾ����ϸ����Ϣ��
                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                foreach (MapDtl dtl in dtls)
                {
                    ps = new Paras();
                    ps.SQL = "DELETE FROM " + dtl.PTable + " WHERE RefPK=" + dbStr + "WorkID";
                    ps.Add("WorkID", this.WorkID.ToString());
                    BP.DA.DBAccess.RunSQL(ps);
                }

                // ɾ����������Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE RefPKVal=" + dbStr + "WorkID AND FK_MapData=" + dbStr + "FK_MapData ",
                    "WorkID", this.WorkID.ToString(), "FK_MapData", "ND" + nd.NodeID);
                // ɾ��ǩ����Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmEleDB WHERE RefPKVal=" + dbStr + "WorkID AND FK_MapData=" + dbStr + "FK_MapData ",
                    "WorkID", this.WorkID.ToString(), "FK_MapData", "ND" + nd.NodeID);
                #endregion ɾ����ǰ�ڵ����ݡ�

                /*˵��:�Ѿ�ɾ���ýڵ����ݡ�*/
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + dbStr + "WorkID1 OR FID=" + dbStr + "WorkID2 ) AND FK_Node=" + dbStr + "FK_Node",
                    "WorkID1", this.WorkID, "WorkID2", this.WorkID, "FK_Node", nd.NodeID);

                if (nd.IsFL)
                {
                    /* ����Ƿ��� */
                    GenerWorkerLists wls = new GenerWorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(GenerWorkerListAttr.FID, this.WorkID);
                    qo.addAnd();

                    string[] ndsStrs = nd.HisToNDs.Split('@');
                    string inStr = "";
                    foreach (string s in ndsStrs)
                    {
                        if (s == "" || s == null)
                            continue;
                        inStr += "'" + s + "',";
                    }
                    inStr = inStr.Substring(0, inStr.Length - 1);
                    if (inStr.Contains(",") == true)
                        qo.AddWhere(GenerWorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(GenerWorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (GenerWorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);
                        subWK.Delete();

                        //ɾ�������²���Ľڵ���Ϣ.
                        DeleteToNodesData(subNd.HisToNodes);
                    }

                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + dbStr + "WorkID",
                        "WorkID", this.WorkID);
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE FID=" + dbStr + "WorkID",
                        "WorkID", this.WorkID);
                    DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + dbStr + "WorkID",
                        "WorkID", this.WorkID);
                }
                DeleteToNodesData(nd.HisToNodes);
            }
        }

        #region ���ݹ�����λ���ɹ�����
        private Node _ndFrom = null;
        private Node ndFrom
        {
            get
            {
                if (_ndFrom == null)
                    _ndFrom = this.HisNode;
                return _ndFrom;
            }
            set
            {
                _ndFrom = value;
            }
        }
        private GenerWorkerLists InitWorkerLists(WorkNode town, DataTable dt)
        {
            return InitWorkerLists(town, dt, 0);
        }
        private GenerWorkerLists InitWorkerLists(WorkNode town, DataTable dt, Int64 fid)
        {
            if (dt.Rows.Count == 0)
                throw new Exception("������Ա�б�Ϊ��"); // ������Ա�б�Ϊ��

            this.HisGenerWorkFlow.TodoEmpsNum = -1;

            #region �жϷ��͵����ͣ�������ص�FID.
            // ������һ���ڵ�Ľ����˵� FID �� WorkID.
            Int64 nextUsersWorkID = this.WorkID;
            Int64 nextUsersFID = this.HisWork.FID;

            // �Ƿ��Ƿ��������̡߳�
            bool isFenLiuToSubThread = false;
            if (this.HisNode.IsFLHL == true
                && town.HisNode.HisRunModel == RunModel.SubThread)
            {
                isFenLiuToSubThread = true;
                nextUsersWorkID = 0;
                nextUsersFID = this.HisWork.OID;
            }


            // ���߳� �� ������or �ֺ�����.
            bool isSubThreadToFenLiu = false;
            if (this.HisNode.HisRunModel == RunModel.SubThread
                && town.HisNode.IsFLHL == true)
            {
                nextUsersWorkID = this.HisWork.FID;
                nextUsersFID = 0;
                isSubThreadToFenLiu = true;
            }

            // ���̵߳����߳�.
            bool isSubthread2Subthread = false;
            if (this.HisNode.HisRunModel == RunModel.SubThread && town.HisNode.HisRunModel == RunModel.SubThread)
            {
                nextUsersWorkID = this.HisWork.OID;
                nextUsersFID = this.HisWork.FID;
                isSubthread2Subthread = true;
            }
            #endregion �жϷ��͵����ͣ�������ص�FID.

            int toNodeId = town.HisNode.NodeID;
            this.HisWorkerLists = new GenerWorkerLists();
            this.HisWorkerLists.Clear();

            #region ����ʱ��  town.HisNode.DeductDays-1
            // 2008-01-22 ֮ǰ�Ķ�����
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , ����Ԥ�����ڵ����⡣

            DateTime dtOfShould;
            if (this.HisFlow.HisTimelineRole == TimelineRole.ByFlow)
            {
                /*������������ǰ��������ü��㡣*/
                dtOfShould = DataType.ParseSysDateTime2DateTime(this.HisGenerWorkFlow.SDTOfFlow);
            }
            else
            {
                int day = 0;
                int hh = 0;
                if (town.HisNode.DeductDays < 1)
                    day = 0;
                else
                    day = int.Parse(town.HisNode.DeductDays.ToString());

                dtOfShould = DataType.AddDays(DateTime.Now, day);
            }

            DateTime dtOfWarning = DateTime.Now;
            if (town.HisNode.WarningDays > 0)
                dtOfWarning = DataType.AddDays(dtOfShould, -int.Parse(town.HisNode.WarningDays.ToString())); // dtOfShould.AddDays(-town.HisNode.WarningDays);

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFHL:
                case NodeWorkType.WorkFL:
                case NodeWorkType.WorkHL:
                    break;
                default:
                    this.HisGenerWorkFlow.FK_Node = town.HisNode.NodeID;
                    this.HisGenerWorkFlow.SDTOfNode = dtOfShould.ToString(DataType.CurrentDataTime );
                    this.HisGenerWorkFlow.TodoEmpsNum = dt.Rows.Count;
                    break;
            }
            #endregion ����ʱ��  town.HisNode.DeductDays-1

            #region ���� ��Ա�б� ����Դ��
            // �����Ƿ��з���mark. ��������У���˵���ü������з���mark. ����Ҫ����һ���˶�����߳ǵ����.
            if (dt.Columns.Count == 3 && town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                this.IsHaveSubThreadGroupMark = false;
            else
                this.IsHaveSubThreadGroupMark = true;

            //�����4���в�����һ���ڵ��Ƕ�̬�����ڵ�.No,Name,BatchNo,FrmIDs �������ĸ��У��������߳Ƿ���.
            if (dt.Columns.Count == 4 && town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                this.IsHaveSubThreadGroupMark = true;

            if (dt.Columns.Count <= 2 && town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                throw new Exception("@��֯������Դ����ȷ������Ľڵ�" + town.HisNode.Name + ",�������Ƕ�̬���������ٷ���3������ʶ��ID.");

            if (dt.Columns.Count <= 2)
                this.IsHaveSubThreadGroupMark = false;

            if (dt.Rows.Count == 1)
            {
                /* ���ֻ��һ����Ա */
                GenerWorkerList wl = new GenerWorkerList();
                if (isFenLiuToSubThread)
                {
                    /*  ˵�����Ƿ��������·���
                     *  �����������ʱ��workid.
                     */
                    wl.WorkID = DBAccess.GenerOIDByGUID();
                }
                else
                {
                    wl.WorkID = nextUsersWorkID;
                }

                wl.FID = nextUsersFID;
                wl.FK_Node = toNodeId;
                wl.FK_NodeText = town.HisNode.Name;

                wl.FK_Emp = dt.Rows[0][0].ToString();

                Emp emp = new Emp(wl.FK_Emp);
                wl.FK_EmpText = emp.Name;
                wl.FK_Dept = emp.FK_Dept;
                wl.WarningDays = town.HisNode.WarningDays;
                wl.SDT = dtOfShould.ToString(DataType.SysDataTimeFormat);

                wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataTimeFormat);
                wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                //  wl.Sender = this.Execer;

                // and 2015-01-14 , ��������У���Լ��Ϊ���һ���Ƿ����־�� �б�־�Ͱ��������־��.
                if (this.IsHaveSubThreadGroupMark == true)
                    wl.GroupMark = dt.Rows[0][2].ToString(); //��3�����Ƿ�����.

                if (this.IsHaveSubThreadGroupMark == true && town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                {
                    /*�Ƿ����ǣ��������Զ�����.*/
                    wl.GroupMark = dt.Rows[0][2].ToString(); //��3�����Ƿ�����.
                    wl.FrmIDs = dt.Rows[0][3].ToString(); //��4�����ǿ���ִ�еı�IDs.
                }

                if (dt.Columns.Count == 3 && town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                {
                    /*���Զ�����,ֻ��3���У�˵�����һ�о��Ǳ�IDs.*/
                    wl.FrmIDs = dt.Rows[0][2].ToString(); //��3�����ǿ���ִ�еı�IDs.
                }

                //���÷�����.
                wl.Sender = WebUser.No + "," + WebUser.Name;
                wl.DirectInsert();
                this.HisWorkerLists.AddEntity(wl);

                RememberMe rm = new RememberMe(); // this.GetHisRememberMe(town.HisNode);
                rm.Objs = "@" + wl.FK_Emp + "@";
                rm.ObjsExt += BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);
                rm.Emps = "@" + wl.FK_Emp + "@";
                rm.EmpsExt = BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);
                this.HisRememberMe = rm;
            }
            else
            {
                /* ����ж����Ա����Ҫ���ǽ������Ƿ�������Ե����⡣ */
                RememberMe rm = this.GetHisRememberMe(town.HisNode);

                #region �Ƿ���Ҫ��ռ�������.
                // �������ѡ�����Ա�������������ļ���Ϊ�ա�2011-11-06 ����糧���� .
                if (this.town.HisNode.HisDeliveryWay == DeliveryWay.BySelected
                    || this.town.HisNode.IsAllowRepeatEmps == true  /*���������Ա�ظ�*/
                    || town.HisNode.IsRememberMe == false)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                if (this.HisNode.IsFL)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                if (this.IsHaveSubThreadGroupMark == false && rm != null && rm.Objs != "")
                {
                    /*���������б��Ƿ����˱仯,����仯�ˣ���Ҫ����Ч�Ľ�������գ�������������.*/
                    string emps = "@";
                    foreach (DataRow dr in dt.Rows)
                        emps += dr[0].ToString() + "@";

                    if (rm.Emps != emps)
                    {
                        // �б����˱仯.
                        rm.Emps = emps;
                        rm.Objs = ""; //�����Ч�Ľ����˼���.
                    }
                }
                #endregion �Ƿ���Ҫ��ռ�������.

                string myemps = "";
                Emp emp = null;
                foreach (DataRow dr in dt.Rows)
                {
                    string fk_emp = dr[0].ToString();
                    if (IsHaveSubThreadGroupMark == true)
                    {
                        /*����з���Mark ,�Ͳ������ظ���Ա������.*/
                    }
                    else
                    {
                        // ������Ա�ظ��ģ���Ȼ�ᵼ��generworkerlist��pk����
                        if (myemps.IndexOf("@" + dr[0].ToString() + ",") != -1)
                            continue;
                        myemps += "@" + dr[0].ToString() + ",";
                    }

                    GenerWorkerList wl = new GenerWorkerList();

                    #region ���ݼ����Ƿ����øò���Ա�������
                    if (rm != null)
                    {
                        if (rm.Objs == "")
                        {
                            /*����ǿյ�.*/
                            wl.IsEnable = true;
                        }
                        else
                        {
                            if (rm.Objs.Contains("@" + fk_emp + "@") == true)
                                wl.IsEnable = true; /* �����������˵�����Ѿ�����*/
                            else
                                wl.IsEnable = false;
                        }
                    }
                    else
                    {
                        wl.IsEnable = false;
                    }
                    #endregion ���ݼ����Ƿ����øò���Ա�������

                    wl.FK_Node = toNodeId;
                    wl.FK_NodeText = town.HisNode.Name;
                    wl.FK_Emp = dr[0].ToString();
                    try
                    {
                        emp = new Emp(wl.FK_Emp);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@Ϊ��Ա���乤��ʱ���ִ���:" + wl.FK_Emp + ",û��ִ�гɹ�,�쳣��Ϣ." + ex.Message);
                    }

                    wl.FK_EmpText = emp.Name;
                    wl.FK_Dept = emp.FK_Dept;
                    wl.WarningDays = town.HisNode.WarningDays;
                    wl.SDT = dtOfShould.ToString(DataType.SysDataTimeFormat);
                    wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataTimeFormat);
                    wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                    wl.FK_Flow = town.HisNode.FK_Flow;
                    if (this.IsHaveSubThreadGroupMark == true)
                    {
                        //���÷�����Ϣ.
                        object val = dr[2];
                        if (val == null)
                            throw new Exception("@�����־����ΪNull.");
                        wl.GroupMark = val.ToString();

                        if (dt.Columns.Count == 4 && this.town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                        {
                            wl.FrmIDs = dr[3].ToString();
                            if (string.IsNullOrEmpty(dr[3].ToString()))
                                throw new Exception("@��֯�Ľ���������Դ����ȷ,��IDs����Ϊ��.");
                        }
                    }
                    else
                    {
                        if (dt.Columns.Count == 3 && this.town.HisNode.HisFormType == NodeFormType.SheetAutoTree)
                        {
                            /*���ֻ������, �����Ƕ�̬����.*/
                            wl.FrmIDs = dr[2].ToString(); //
                            if (string.IsNullOrEmpty(dr[2].ToString()))
                                throw new Exception("@��֯�Ľ���������Դ����ȷ,��IDs����Ϊ��.");
                        }
                    }

                    wl.FID = nextUsersFID;
                    if (isFenLiuToSubThread)
                    {
                        /* ˵�����Ƿ��������·���
                         *  �����������ʱ��workid.
                         */
                        wl.WorkID = DBAccess.GenerOIDByGUID();
                    }
                    else
                    {
                        wl.WorkID = nextUsersWorkID;
                    }

                    try
                    {
                        wl.DirectInsert();
                        this.HisWorkerLists.AddEntity(wl);
                    }
                    catch (Exception ex)
                    {
                        Log.DefaultLogWriteLineError("��Ӧ�ó��ֵ��쳣��Ϣ��" + ex.Message);
                    }
                }

                //ִ�ж�rm�ĸ��¡�
                if (rm != null)
                {
                    string empExts = "";
                    string objs = "@"; // ��Ч�Ĺ�����Ա.
                    string objsExt = "@"; // ��Ч�Ĺ�����Ա.
                    foreach (GenerWorkerList wl in this.HisWorkerLists)
                    {
                        if (wl.IsEnable == false)
                            empExts += "<strike><font color=red>" + BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText) + "</font></strike>��";
                        else
                            empExts += BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);

                        if (wl.IsEnable == true)
                        {
                            objs += wl.FK_Emp + "@";
                            objsExt += BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);
                        }
                    }
                    rm.EmpsExt = empExts;

                    rm.Objs = objs;
                    rm.ObjsExt = objsExt;
                    //  rm.Save(); //����.
                    this.HisRememberMe = rm;
                }
            }

            if (this.HisWorkerLists.Count == 0)
                throw new Exception("@����[" + this.town.HisNode.HisRunModel + "]���Ų���������Ա���ִ�������[" + this.HisWorkFlow.HisFlow.Name + "],�нڵ�[" + town.HisNode.Name + "]�������,û���ҵ����ܴ˹����Ĺ�����Ա.");
            #endregion ���� ��Ա�б� ����Դ��

            #region ������������,��������ϢΪ������ṩ���ݡ�
            string hisEmps = "";
            int num = 0;
            foreach (GenerWorkerList wl in this.HisWorkerLists)
            {
                if (wl.IsPassInt == 0 && wl.IsEnable == true)
                {
                    num++;
                    hisEmps += wl.FK_Emp + "," + wl.FK_EmpText + ";";
                }
            }

            if (num == 0)
                throw new Exception("@��Ӧ�ò����Ľ������.");

            this.HisGenerWorkFlow.TodoEmpsNum = num;
            this.HisGenerWorkFlow.TodoEmps = hisEmps;
            #endregion

            #region  �����־���ͣ�����������С�
            ActionType at = ActionType.Forward;
            switch (town.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWork:
                case NodeWorkType.StartWorkFL:
                    at = ActionType.Start;
                    break;
                case NodeWorkType.Work:
                    if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkFL
                        || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
                        at = ActionType.ForwardFL;
                    else
                        at = ActionType.Forward;
                    break;
                case NodeWorkType.WorkHL:
                    at = ActionType.ForwardHL;
                    break;
                case NodeWorkType.SubThreadWork:
                    at = ActionType.SubFlowForward;
                    break;
                default:
                    break;
            }
            if (this.HisWorkerLists.Count == 1)
            {
                GenerWorkerList wl = this.HisWorkerLists[0] as GenerWorkerList;
                this.AddToTrack(at, wl.FK_Emp, wl.FK_EmpText, wl.FK_Node, wl.FK_NodeText, null, this.ndFrom);
            }
            else
            {
                string info = "��(" + this.HisWorkerLists.Count + ")�˽���\t\n";
                foreach (GenerWorkerList wl in this.HisWorkerLists)
                {
                    info += BP.WF.Glo.DealUserInfoShowModel(wl.FK_DeptT, wl.FK_EmpText) + "\t\n";
                }
                this.AddToTrack(at, this.Execer, "���˽���(����Ϣ��)", town.HisNode.NodeID, town.HisNode.Name, info, this.ndFrom);
            }
            #endregion

            #region �����ݼ��������.
            string ids = "";
            string names = "";
            string idNames = "";
            if (this.HisWorkerLists.Count == 1)
            {
                GenerWorkerList gwl = (GenerWorkerList)this.HisWorkerLists[0];
                ids = gwl.FK_Emp;
                names = gwl.FK_EmpText;

                //����״̬��
                this.HisGenerWorkFlow.TaskSta = TaskSta.None;
            }
            else
            {
                foreach (GenerWorkerList gwl in this.HisWorkerLists)
                {
                    ids += gwl.FK_Emp + ",";
                    names += gwl.FK_EmpText + ",";
                }

                //����״̬, ���������ʹ�������ù�������ء�
                if (town.HisNode.IsEnableTaskPool && this.HisNode.HisRunModel == RunModel.Ordinary)
                    this.HisGenerWorkFlow.TaskSta = TaskSta.Sharing;
                else
                    this.HisGenerWorkFlow.TaskSta = TaskSta.None;
            }

            this.addMsg(SendReturnMsgFlag.VarAcceptersID, ids, ids, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersName, names, names, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersNID, idNames, idNames, SendReturnMsgType.SystemMsg);
            #endregion

            return this.HisWorkerLists;
        }
        #endregion


        #region �ж�һ�˶ಿ�ŵ����
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        private Dept _HisDeptOfUse = null;
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        public Dept HisDeptOfUse
        {
            get
            {
                if (_HisDeptOfUse == null)
                {
                    //�ҵ�ȫ���Ĳ��š�
                    Depts depts;
                    if (this.HisWork.Rec == this.Execer)
                        depts = WebUser.HisDepts;
                    else
                        depts = this.HisWork.RecOfEmp.HisDepts;

                    if (depts.Count == 0)
                    {
                        throw new Exception("��û�и�[" + this.HisWork.Rec + "]���ò��š�");
                    }

                    if (depts.Count == 1) /* ���ȫ���Ĳ���ֻ��һ�����ͷ�������*/
                    {
                        _HisDeptOfUse = (Dept)depts[0];
                        return _HisDeptOfUse;
                    }

                    if (_HisDeptOfUse == null)
                    {
                        /* �����û�ҵ����ͷ��ص�һ�����š� */
                        _HisDeptOfUse = depts[0] as Dept;
                    }
                }
                return _HisDeptOfUse;
            }
        }
        #endregion

        #region ����
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// �ڵ�������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,���������Ա������ڵ��ϵ�����������.
        /// </summary>
        public Conds HisNodeCompleteConditions
        {
            get
            {
                if (this._HisNodeCompleteConditions == null)
                {
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.HisNode.NodeID, this.WorkID, this.rptGe);

                    return _HisNodeCompleteConditions;
                }
                return _HisNodeCompleteConditions;
            }
        }
        private Conds _HisFlowCompleteConditions = null;
        /// <summary>
        /// ����������������,�˽ڵ�������������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,�������������.
        /// </summary>
        public Conds HisFlowCompleteConditions
        {
            get
            {
                if (this._HisFlowCompleteConditions == null)
                {
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.HisNode.NodeID, this.WorkID, this.rptGe);
                }
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region ������������
        ///// <summary>
        ///// �õ���ǰ���Ѿ���ɵĹ����ڵ�.
        ///// </summary>
        ///// <returns></returns>
        //public WorkNodes GetHadCompleteWorkNodes()
        //{
        //    WorkNodes mywns = new WorkNodes();
        //    WorkNodes wns = new WorkNodes(this.HisNode.HisFlow, this.HisWork.OID);
        //    foreach (WorkNode wn in wns)
        //    {
        //        if (wn.IsComplete)
        //            mywns.Add(wn);
        //    }
        //    return mywns;
        //}
        #endregion

        #region ���̹�������
        private Flow _HisFlow = null;
        public Flow HisFlow
        {
            get
            {
                if (_HisFlow == null)
                    _HisFlow = this.HisNode.HisFlow;
                return _HisFlow;
            }
        }
        private Node JumpToNode = null;
        private string JumpToEmp = null;


        #region NodeSend �ĸ�������.
        public Node NodeSend_GenerNextStepNode()
        {
            //���Ҫ����ת���Ľڵ㣬�Զ���ת�������ͻ�ʧЧ��
            if (this.JumpToNode != null)
                return this.JumpToNode;

            #region delete by zhoupeng 14.11.12 ���ִ���������Ͳ���ִ���Զ���ת��.
            //Nodes toNDs = this.HisNode.HisToNodes;
            //if (toNDs.Count == 1)
            //{
            //    Node mynd = toNDs[0] as Node;
            //    //д�뵽����Ϣ.
            //    this.addMsg(SendReturnMsgFlag.VarToNodeID, mynd.NodeID.ToString(), mynd.NodeID.ToString(),
            //     SendReturnMsgType.SystemMsg);
            //    this.addMsg(SendReturnMsgFlag.VarToNodeName, mynd.Name, mynd.Name, SendReturnMsgType.SystemMsg);
            //    return mynd;
            //}
            #endregion delete by zhoupeng 14.11.12


            // �ж��Ƿ����û�ѡ��Ľڵ㡣
            if (this.HisNode.CondModel == CondModel.ByUserSelected)
            {
                // ��ȡ�û�ѡ��Ľڵ�.
                string nodes = this.HisGenerWorkFlow.Paras_ToNodes;
                if (string.IsNullOrEmpty(nodes))
                    throw new Exception("@�û�û��ѡ���͵��Ľڵ�.");

                string[] mynodes = nodes.Split(',');
                foreach (string item in mynodes)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    return new Node(int.Parse(item));
                }

                //������Ϊ��,�Է�ֹ��һ�η��ͳ��ִ���.
                this.HisGenerWorkFlow.Paras_ToNodes = "";
            }

            Node nd = NodeSend_GenerNextStepNode_Ext1();
            //д�뵽����Ϣ.
            this.addMsg(SendReturnMsgFlag.VarToNodeID, nd.NodeID.ToString(), nd.NodeID.ToString(),
             SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarToNodeName, nd.Name, nd.Name, SendReturnMsgType.SystemMsg);
            return nd;
        }
        /// <summary>
        /// ֪��ִ������ת.
        /// </summary>
        public bool IsSkip = false;
        /// <summary>
        /// ��ȡ��һ����Ĺ����ڵ�.
        /// </summary>
        /// <returns></returns>
        public Node NodeSend_GenerNextStepNode_Ext1()
        {
            //���Ҫ����ת���Ľڵ㣬�Զ���ת�������ͻ�ʧЧ��
            if (this.JumpToNode != null)
                return this.JumpToNode;

            Node mynd = this.HisNode;
            Work mywork = this.HisWork;

            this.ndFrom = this.HisNode;
            while (true)
            {
                //��һ���Ĺ����ڵ�.
                int prvNodeID = mynd.NodeID;
                if (mynd.IsEndNode)
                {
                    /*��������һ���ڵ���,��Ȼ�Ҳ�����һ���ڵ�...*/
                    this.IsStopFlow = true;
                    return mynd;
                }

                // ��ȡ������һ���ڵ�.
                Node nd = this.NodeSend_GenerNextStepNode_Ext(mynd);
                mynd = nd;
                Work skipWork = null;
                if (mywork.NodeFrmID != nd.NodeFrmID)
                {
                    /* ����ȥ�Ľڵ�ҲҪд�����ݣ���Ȼ�����ǩ������*/
                    skipWork = nd.HisWork;
                    skipWork.Copy(this.rptGe);
                    skipWork.Copy(mywork);

                    skipWork.OID = this.WorkID;
                    skipWork.Rec = this.Execer;
                    skipWork.SetValByKey(WorkAttr.RDT, DataType.CurrentDataTimess);
                    skipWork.SetValByKey(WorkAttr.CDT, DataType.CurrentDataTimess);
                    skipWork.ResetDefaultVal();

                    // �������Ĭ��ֵҲcopy��������ȥ.
                    rptGe.Copy(skipWork);

                    //������ھ��޸�
                    if (skipWork.IsExit(skipWork.PK, this.WorkID) == true)
                        skipWork.DirectUpdate();
                    else
                        skipWork.InsertAsOID(this.WorkID);

                    #region  ��ʼ������Ĺ����ڵ㡣

                    if (1 == 2)
                    {

#warning �� zhoupeng ɾ�� 2014-06-20, ��Ӧ�ô�������.
                        if (this.HisWork.EnMap.PhysicsTable == nd.HisWork.EnMap.PhysicsTable)
                        {
                            /*�������ݺϲ�ģʽ, �Ͳ�ִ��copy*/
                        }
                        else
                        {
                            /* �����������Դ����ȣ���ִ��copy�� */
                            #region ���Ƹ�����
                            FrmAttachments athDesc = this.HisNode.MapData.FrmAttachments;
                            if (athDesc.Count > 0 )
                            {
                                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                                      this.WorkID.ToString());
                                int idx = 0;
                                if (athDBs.Count > 0)
                                {
                                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + nd.NodeID,
                                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                                    /*˵����ǰ�ڵ��и�������*/
                                    foreach (FrmAttachmentDB athDB in athDBs)
                                    {
                                        idx++;
                                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                        athDB_N.Copy(athDB);
                                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                                        athDB_N.RefPKVal = this.WorkID.ToString();

                                        // athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                                        // if (athDB.dbt
                                        // athDB_N.MyPK = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + this.WorkID;

                                        athDB_N.MyPK = DBAccess.GenerGUID(); // athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + this.WorkID;

                                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                           "ND" + nd.NodeID);

                                        athDB_N.Save();
                                    }
                                }
                            }
                            #endregion ���Ƹ�����

                            #region ����ͼƬ�ϴ�������
                            if (this.HisNode.MapData.FrmImgAths.Count > 0)
                            {
                                FrmImgAthDBs athDBs = new FrmImgAthDBs("ND" + this.HisNode.NodeID,
                                      this.WorkID.ToString());
                                int idx = 0;
                                if (athDBs.Count > 0)
                                {
                                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + nd.NodeID,
                                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                                    /*˵����ǰ�ڵ��и�������*/
                                    foreach (FrmImgAthDB athDB in athDBs)
                                    {
                                        idx++;
                                        FrmImgAthDB athDB_N = new FrmImgAthDB();
                                        athDB_N.Copy(athDB);
                                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                                        athDB_N.RefPKVal = this.WorkID.ToString();
                                        athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                                        athDB_N.FK_FrmImgAth = athDB_N.FK_FrmImgAth.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                                        athDB_N.Save();
                                    }
                                }
                            }
                            #endregion ����ͼƬ�ϴ�������

                            #region ����Ele
                            if (this.HisNode.MapData.FrmEles.Count > 0)
                            {
                                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                                      this.WorkID.ToString());
                                if (eleDBs.Count > 0)
                                {
                                    eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + nd.NodeID,
                                        FrmEleDBAttr.RefPKVal, this.WorkID);

                                    /*˵����ǰ�ڵ��и�������*/
                                    foreach (FrmEleDB eleDB in eleDBs)
                                    {
                                        FrmEleDB eleDB_N = new FrmEleDB();
                                        eleDB_N.Copy(eleDB);
                                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                                        eleDB_N.GenerPKVal();
                                        eleDB_N.Save();
                                    }
                                }
                            }
                            #endregion ����Ele

                            #region ���ƶ�ѡ����
                            if (this.HisNode.MapData.MapM2Ms.Count > 0)
                            {
                                M2Ms m2ms = new M2Ms("ND" + this.HisNode.NodeID, this.WorkID);
                                if (m2ms.Count >= 1)
                                {
                                    foreach (M2M item in m2ms)
                                    {
                                        M2M m2 = new M2M();
                                        m2.Copy(item);
                                        m2.EnOID = this.WorkID;
                                        m2.FK_MapData = m2.FK_MapData.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                                        m2.InitMyPK();
                                        try
                                        {
                                            m2.DirectInsert();
                                        }
                                        catch
                                        {
                                            m2.DirectUpdate();
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region ������ϸ����
                            // int deBugDtlCount=
                            Sys.MapDtls dtls = this.HisNode.MapData.MapDtls;
                            string recDtlLog = "@��¼������ϸ��Copy����,�ӽڵ�ID:" + this.HisNode.NodeID + " WorkID:" + this.WorkID + ", ���ڵ�ID=" + nd.NodeID;
                            if (dtls.Count > 0)
                            {
                                Sys.MapDtls toDtls = nd.MapData.MapDtls;
                                recDtlLog += "@���ڵ���ϸ��������:" + dtls.Count + "��";

                                Sys.MapDtls startDtls = null;
                                bool isEnablePass = false; /*�Ƿ�����ϸ�������.*/
                                foreach (MapDtl dtl in dtls)
                                {
                                    if (dtl.IsEnablePass)
                                        isEnablePass = true;
                                }

                                if (isEnablePass) /* ����оͽ�������ʼ�ڵ������ */
                                    startDtls = new BP.Sys.MapDtls("ND" + int.Parse(nd.FK_Flow) + "01");

                                recDtlLog += "@����ѭ����ʼִ�������ϸ��copy.";
                                int i = -1;
                                foreach (Sys.MapDtl dtl in dtls)
                                {
                                    recDtlLog += "@����ѭ����ʼִ����ϸ��(" + dtl.No + ")copy.";

                                    //i++;
                                    //if (toDtls.Count <= i)
                                    //    continue;

                                    //Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];


                                    i++;
                                    //if (toDtls.Count <= i)
                                    //    continue;
                                    Sys.MapDtl toDtl = null;

                                    foreach (MapDtl todtl in toDtls)
                                    {
                                        if (todtl.Name.Substring(6, todtl.Name.Length - 6).Equals(dtl.Name.Substring(6, dtl.Name.Length - 6)))
                                        {
                                            toDtl = todtl;
                                            break;
                                        }
                                    }

                                    if (toDtl == null)
                                        continue;

                                    if (dtl.IsEnablePass == true)
                                    {
                                        /*����������Ƿ���ϸ������ͨ������,������copy�ڵ����ݡ�*/
                                        toDtl.IsCopyNDData = true;
                                    }

                                    if (toDtl.IsCopyNDData == false)
                                        continue;

                                    //��ȡ��ϸ���ݡ�
                                    GEDtls gedtls = new GEDtls(dtl.No);
                                    QueryObject qo = null;
                                    qo = new QueryObject(gedtls);
                                    switch (dtl.DtlOpenType)
                                    {
                                        case DtlOpenType.ForEmp:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForWorkID:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForFID:
                                            qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                            break;
                                    }
                                    qo.DoQuery();

                                    recDtlLog += "@��ѯ��������ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";

                                    int unPass = 0;
                                    // �Ƿ�������˻��ơ�
                                    isEnablePass = dtl.IsEnablePass;
                                    if (isEnablePass && this.HisNode.IsStartNode == false)
                                        isEnablePass = true;
                                    else
                                        isEnablePass = false;

                                    if (isEnablePass == true)
                                    {
                                        /*�жϵ�ǰ�ڵ����ϸ�����Ƿ��У�isPass ����ֶΣ����û���׳��쳣��Ϣ��*/
                                        if (gedtls.Count != 0)
                                        {
                                            GEDtl dtl1 = gedtls[0] as GEDtl;
                                            if (dtl1.EnMap.Attrs.Contains("IsPass") == false)
                                                isEnablePass = false;
                                        }
                                    }

                                    recDtlLog += "@ɾ��������ϸ��:" + dtl.No + ",����, ����ʼ������ϸ��,ִ��һ���е�copy.";
                                    DBAccess.RunSQL("DELETE FROM " + toDtl.PTable + " WHERE RefPK=" + dbStr + "RefPK", "RefPK", this.WorkID.ToString());

                                    // copy����.
                                    int deBugNumCopy = 0;
                                    foreach (GEDtl gedtl in gedtls)
                                    {
                                        if (isEnablePass)
                                        {
                                            if (gedtl.GetValBooleanByKey("IsPass") == false)
                                            {
                                                /*û�����ͨ���ľ� continue ���ǣ��������Ѿ�����ͨ����.*/
                                                continue;
                                            }
                                        }

                                        BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                                        dtCopy.Copy(gedtl);
                                        dtCopy.FK_MapDtl = toDtl.No;
                                        dtCopy.RefPK = this.WorkID.ToString();
                                        dtCopy.InsertAsOID(dtCopy.OID);
                                        dtCopy.RefPKInt64 = this.WorkID;
                                        deBugNumCopy++;

                                        #region  ������ϸ���� - ������Ϣ
                                        if (toDtl.IsEnableAthM)
                                        {
                                            /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                                            FrmAttachmentDBs athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                            if (athDBs.Count > 0)
                                            {
                                                i = 0;
                                                foreach (FrmAttachmentDB athDB in athDBs)
                                                {
                                                    i++;
                                                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                                    athDB_N.Copy(athDB);
                                                    athDB_N.FK_MapData = toDtl.No;
                                                    athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                                        "ND" + nd.NodeID);
                                                    athDB_N.RefPKVal = dtCopy.OID.ToString();
                                                    athDB_N.DirectInsert();
                                                }
                                            }
                                        }
                                        if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                                        {
                                            /*���������m2m */
                                            M2Ms m2ms = new M2Ms(dtl.No, gedtl.OID);
                                            if (m2ms.Count > 0)
                                            {
                                                i = 0;
                                                foreach (M2M m2m in m2ms)
                                                {
                                                    i++;
                                                    M2M m2m_N = new M2M();
                                                    m2m_N.Copy(m2m);
                                                    m2m_N.FK_MapData = toDtl.No;
                                                    m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                                    m2m_N.EnOID = gedtl.OID;
                                                    m2m.InitMyPK();
                                                    m2m_N.DirectInsert();
                                                }
                                            }
                                        }
                                        #endregion  ������ϸ���� - ������Ϣ

                                    }
#warning ��¼��־.
                                    if (gedtls.Count != deBugNumCopy)
                                    {
                                        recDtlLog += "@����ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";
                                        //��¼��־.
                                        Log.DefaultLogWriteLineInfo(recDtlLog);
                                        throw new Exception("@ϵͳ���ִ����뽫������Ϣ����������Ա,лл��: ������Ϣ:" + recDtlLog);
                                    }

                                    #region �����������˻���
                                    if (isEnablePass)
                                    {
                                        /* ������������ͨ�����ƣ��Ͱ�δ��˵�����copy����һ���ڵ���ȥ 
                                         * 1, �ҵ���Ӧ����ϸ��.
                                         * 2, ��δ���ͨ�������ݸ��Ƶ���ʼ��ϸ����.
                                         */
                                        string fk_mapdata = "ND" + int.Parse(nd.FK_Flow) + "01";
                                        MapData md = new MapData(fk_mapdata);
                                        string startUser = "SELECT Rec FROM " + md.PTable + " WHERE OID=" + this.WorkID;
                                        startUser = DBAccess.RunSQLReturnString(startUser);

                                        MapDtl startDtl = (MapDtl)startDtls[i];
                                        foreach (GEDtl gedtl in gedtls)
                                        {
                                            if (gedtl.GetValBooleanByKey("IsPass"))
                                                continue; /* �ų����ͨ���� */

                                            BP.Sys.GEDtl dtCopy = new GEDtl(startDtl.No);
                                            dtCopy.Copy(gedtl);
                                            dtCopy.OID = 0;
                                            dtCopy.FK_MapDtl = startDtl.No;
                                            dtCopy.RefPK = gedtl.OID.ToString(); //this.WorkID.ToString();
                                            dtCopy.SetValByKey("BatchID", this.WorkID);
                                            dtCopy.SetValByKey("IsPass", 0);
                                            dtCopy.SetValByKey("Rec", startUser);
                                            dtCopy.SetValByKey("Checker", this.ExecerName);
                                            dtCopy.RefPKInt64 = this.WorkID;
                                            dtCopy.SaveAsOID(gedtl.OID);
                                        }
                                        DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + this.Execer + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + this.Execer + "'");
                                    }
                                    #endregion �����������˻���
                                }
                            }
                            #endregion ������ϸ����
                        }
                    }
                    #endregion ��ʼ������Ĺ����ڵ�.

                    IsSkip = true;
                    mywork = skipWork;
                }

                //�ж��Ƿ�������ת�ˣ�û�����þͷ�����.
                if (nd.AutoJumpRole0 == false
                    && nd.AutoJumpRole1 == false
                    && nd.AutoJumpRole2 == false)
                    return nd;

                FindWorker fw = new FindWorker();
                WorkNode toWn = new WorkNode(this.WorkID, nd.NodeID);
                if (skipWork == null)
                    skipWork = toWn.HisWork;  

                DataTable dt = fw.DoIt(this.HisFlow, this, toWn); // �ҵ���һ����Ľ�����.
                if (dt == null || dt.Rows.Count == 0)
                {
                    if (nd.HisWhenNoWorker == WhenNoWorker.Skip)
                    {
                        this.AddToTrack(ActionType.Skip, this.Execer, this.ExecerName,
                            nd.NodeID, nd.Name, "�Զ���ת.(��û���ҵ�������ʱ)", ndFrom);
                        ndFrom = nd;
                        continue;
                    }
                    else
                        throw new Exception("@û���ҵ���.");
                }

                if (dt.Rows.Count == 0)
                    throw new Exception("@û���ҵ���һ���ڵ�(" + nd.Name + ")�Ĵ�����");

                if (nd.AutoJumpRole0)
                {
                    /*�����˾��Ƿ�����*/
                    bool isHave = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        // ��������� �����˾��Ƿ����˵����.
                        if (dr[0].ToString() == this.HisGenerWorkFlow.Starter)
                        {
                            #region ����ǩ������ǩ�������Ƿ����ˡ�
                           

                            Attrs attrs = skipWork.EnMap.Attrs;
                            bool isUpdate = false;
                            foreach (Attr attr in attrs)
                            {
                                if (attr.UIIsReadonly && attr.UIVisible == true
                                    )
                                {
                                    if (attr.DefaultValOfReal == "@WebUser.No")
                                    {
                                        skipWork.SetValByKey(attr.Key, this.HisGenerWorkFlow.Starter);
                                        isUpdate = true;
                                    }
                                    if (attr.DefaultValOfReal == "@WebUser.Name")
                                    {
                                        skipWork.SetValByKey(attr.Key, this.HisGenerWorkFlow.StarterName);
                                        isUpdate = true;
                                    }
                                    if (attr.DefaultValOfReal == "@WebUser.FK_Dept")
                                    {
                                        skipWork.SetValByKey(attr.Key, this.HisGenerWorkFlow.FK_Dept);
                                        isUpdate = true;
                                    }
                                    if (attr.DefaultValOfReal == "@WebUser.DeptName")
                                    {
                                        skipWork.SetValByKey(attr.Key, this.HisGenerWorkFlow.DeptName);
                                        isUpdate = true;
                                    }
                                }
                            }
                            if (isUpdate)
                                skipWork.DirectUpdate();
                            #endregion ����ǩ������ǩ�������Ƿ����ˡ�

                            isHave = true;
                            break;
                        }
                    }

                    if (isHave == true)
                    {
                        /*��������ˣ���ǰ��Ա���������˼���.*/
                        this.AddToTrack(ActionType.Skip, this.Execer, this.ExecerName, nd.NodeID, nd.Name, "�Զ���ת,(�����˾����ύ��)", ndFrom);
                        ndFrom = nd;
                        continue;
                    }

                    //���û����ת,�ж��Ƿ�,�������������Ƿ�����.
                    if (nd.AutoJumpRole1 == false && nd.AutoJumpRole2 == false)
                        return nd;
                }

                if (nd.AutoJumpRole1)
                {
                    /*�������Ѿ����ֹ�*/
                    bool isHave = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        // ��������˴����˾����ύ�˵����.
                        string sql = "SELECT COUNT(*) FROM WF_GenerWorkerList WHERE FK_Emp='" + dr[0].ToString() + "' AND WorkID=" + this.WorkID;
                        if (DBAccess.RunSQLReturnValInt(sql) == 1)
                        {
                            /*���ﲻ����ǩ��.*/
                            isHave = true;
                            break;
                        }
                    }
                    if (isHave == true)
                    {
                        this.AddToTrack(ActionType.Skip, this.Execer, this.ExecerName, nd.NodeID, nd.Name, "�Զ���ת.(�������Ѿ����ֹ�)", ndFrom);
                        ndFrom = nd;
                        continue;
                    }

                    //���û����ת,�ж��Ƿ�,�������������Ƿ�����.
                    if (nd.AutoJumpRole2 == false)
                        return nd;
                }

                if (nd.AutoJumpRole2)
                {
                    /* ����������һ����ͬ */
                    bool isHave = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string sql = "SELECT COUNT(*) FROM WF_GenerWorkerList WHERE FK_Emp='" + this.Execer + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + prvNodeID;
                        if (DBAccess.RunSQLReturnValInt(sql) == 1)
                        {
                            /*���ﲻ����ǩ��.*/
                            isHave = true;
                            break;
                        }
                    }

                    if (isHave == true)
                    {
                        this.AddToTrack(ActionType.Skip, this.Execer, this.ExecerName, nd.NodeID, nd.Name, "�Զ���ת.(����������һ����ͬ)", ndFrom);
                        ndFrom = nd;
                        continue;
                    }

                    //û������ת���������ͷ��ر���.
                    return nd;
                }

                mynd = nd;
                ndFrom = nd;
            }//����ѭ����

            throw new Exception("@�ҵ���һ���ڵ�.");
        }
        /// <summary>
        /// ����OrderTeamup�˻�ģʽ
        /// </summary>
        public void DealReturnOrderTeamup()
        {
            /*���Э����˳��ʽ.*/
            GenerWorkerList gwl = new GenerWorkerList();
            gwl.FK_Emp = WebUser.No;
            gwl.FK_Node = this.HisNode.NodeID;
            gwl.WorkID = this.WorkID;
            if (gwl.RetrieveFromDBSources() == 0)
                throw new Exception("@û���ҵ��Լ�����������.");
            gwl.IsPass = true;
            gwl.Update();

            gwl.FK_Emp = this.JumpToEmp;
            gwl.FK_Node = this.JumpToNode.NodeID;
            gwl.WorkID = this.WorkID;
            if (gwl.RetrieveFromDBSources() == 0)
                throw new Exception("@û���ҵ�����������������.");

            gwl.IsPass = false;
            gwl.Update();
            GenerWorkerLists ens = new GenerWorkerLists();
            ens.AddEntity(gwl);
            this.HisWorkerLists = ens;

            this.addMsg(SendReturnMsgFlag.VarAcceptersID, gwl.FK_Emp, gwl.FK_Emp, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersName, gwl.FK_EmpText, gwl.FK_EmpText, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.OverCurr, "��ǰ�����Ѿ����͸��˻���(" + gwl.FK_Emp + "," + gwl.FK_EmpText + ").", null, SendReturnMsgType.Info);

            this.HisGenerWorkFlow.WFState = WFState.Runing;
            this.HisGenerWorkFlow.FK_Node = gwl.FK_Node;
            this.HisGenerWorkFlow.NodeName = gwl.FK_NodeText;

            this.HisGenerWorkFlow.TodoEmps = gwl.FK_Emp;
            this.HisGenerWorkFlow.TodoEmpsNum = 0;
            this.HisGenerWorkFlow.TaskSta = TaskSta.None;
            this.HisGenerWorkFlow.Update();
        }
        /// <summary>
        /// ��ȡ��һ����Ĺ����ڵ�
        /// </summary>
        /// <returns></returns>
        private Node NodeSend_GenerNextStepNode_Ext(Node currNode)
        {
            // ��鵱ǰ��״̬���Ƿ����˻أ�.
            if (this.SendNodeWFState == WFState.ReturnSta)
            {

            }

            Nodes nds = currNode.HisToNodes;
            if (nds.Count == 1)
            {
                Node toND = (Node)nds[0];
                return toND;
            }

            if (nds.Count == 0)
                throw new Exception("û���ҵ��������˲��ڵ�.");

            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, currNode.NodeID, CondAttr.CondType, (int)CondType.Dir, CondAttr.PRI);
            if (dcsAll.Count == 0)
                throw new Exception("@û��Ϊ�ڵ�(" + currNode.NodeID + " , " + currNode.Name + ")���÷�������");

            #region ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.
            Nodes myNodes = new Nodes();
            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in nds)
            {
                Conds dcs = new Conds();
                foreach (Cond dc in dcsAll)
                {
                    if (dc.ToNodeID != nd.NodeID)
                        continue;

                    dc.WorkID = this.WorkID;
                    dc.FID = this.HisWork.FID;

                    dc.en = this.rptGe;

                    dcs.AddEntity(dc);
                }

                if (dcs.Count == 0)
                {
                    throw new Exception("@������ƴ��󣺴ӽڵ�(" + currNode.Name + ")���ڵ�(" + nd.Name + ")��û�����÷����������з�֧�Ľڵ�����з���������");
                    continue;
                    // throw new Exception(string.Format(this.ToE("WN10", "@����ڵ�ķ�����������:û�и���{0}�ڵ㵽{1},����ת������."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                }

                if (dcs.IsPass) // ���ͨ����.
                    myNodes.AddEntity(nd);
            }
            #endregion ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.

            // ���û���ҵ�.
            if (myNodes.Count == 0)
                throw new Exception("@��ǰ�û�(" + this.ExecerName + "),����ڵ�ķ�����������:��{" + currNode.NodeID + currNode.Name + "}�ڵ㵽�����ڵ�,���������ת��������������.");

            //����ҵ�1��.
            if (myNodes.Count == 1)
            {
                Node toND = myNodes[0] as Node;
                return toND;
            }


            //����ҵ��˶��.
            foreach (Cond dc in dcsAll)
            {
                foreach (Node myND in myNodes)
                {
                    if (dc.ToNodeID == myND.NodeID)
                    {
                        return myND;
                    }
                }
            }
            throw new Exception("@��Ӧ�ó��ֵ��쳣,��Ӧ�����е�����.");
        }
        /// <summary>
        /// ��ȡ��һ����Ľڵ㼯��
        /// </summary>
        /// <returns></returns>
        public Nodes Func_GenerNextStepNodes()
        {
            //�����ת�ڵ��Ѿ����˱���.
            if (this.JumpToNode != null)
            {
                Nodes myNodesTo = new Nodes();
                myNodesTo.AddEntity(this.JumpToNode);
                return myNodesTo;
            }

            if (this.HisNode.CondModel == CondModel.ByUserSelected)
            {
                // ��ȡ�û�ѡ��Ľڵ�.
                string nodes = this.HisGenerWorkFlow.Paras_ToNodes;
                if (string.IsNullOrEmpty(nodes))
                    throw new Exception("@�û�û��ѡ���͵��Ľڵ�.");

                Nodes nds = new Nodes();
                string[] mynodes = nodes.Split(',');
                foreach (string item in mynodes)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    nds.AddEntity(new Node(int.Parse(item)));
                }
                return nds;

                //������Ϊ��,�Է�ֹ��һ�η��ͳ��ִ���.
                this.HisGenerWorkFlow.Paras_ToNodes = "";
            }


            Nodes toNodes = this.HisNode.HisToNodes;

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return toNodes;
            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, this.HisNode.NodeID, CondAttr.PRI);

            #region ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.
            Nodes myNodes = new Nodes();
            int toNodeId = 0;
            int numOfWay = 0;

            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                foreach (Cond dc in dcsAll)
                {
                    if (dc.ToNodeID != nd.NodeID)
                        continue;

                    dc.WorkID = this.HisWork.OID;
                    dc.en = this.rptGe;
                    dcs.AddEntity(dc);
                }

                if (dcs.Count == 0)
                {
                    myNodes.AddEntity(nd);
                    continue;
                }

                if (dcs.IsPass) // ������ת����������һ������.
                {
                    myNodes.AddEntity(nd);
                    continue;
                }
            }
            #endregion ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.

            if (myNodes.Count == 0)
                throw new Exception(string.Format("@����ڵ�ķ�����������:û�и���{0}�ڵ㵽�����ڵ�,����ת���������������������ת��������������.",
                    this.HisNode.NodeID + this.HisNode.Name));
            return myNodes;
        }
        /// <summary>
        /// ���һ�������������.
        /// </summary>
        /// <returns></returns>
        private void Func_CheckCompleteCondition()
        {
            if (this.HisNode.HisRunModel == RunModel.SubThread)
                throw new Exception("@������ƴ��󣺲����������߳��������������������");

            this.IsStopFlow = false;
            this.addMsg("CurrWorkOver", string.Format("��ǰ����[{0}]�Ѿ����", this.HisNode.Name));

            #region �ж���������.
            try
            {
                if (this.HisNode.HisToNodes.Count == 0 && this.HisNode.IsStartNode)
                {
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������", this.HisNode, this.rptGe);
                    this.IsStopFlow = true;
                    this.addMsg("OneNodeFlowOver", "@�����Ѿ��ɹ�����(һ�����̵Ĺ���)��");
                }

                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    /*�����������������������������������ͨ���ġ�*/

                    string stopMsg = this.HisFlowCompleteConditions.ConditionDesc;
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������:" + stopMsg, this.HisNode, this.rptGe);
                    this.IsStopFlow = true;

                    // string path = BP.Sys.Glo.Request.ApplicationPath;
                    string mymsg = "@���Ϲ��������������" + stopMsg + "" + overMsg;
                    string mymsgHtml = mymsg + "@�鿴<img src='" + VirPath + "WF/Img/Btn/PrintWorkRpt.gif' ><a href='" + VirPath + "WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "' target='_self' >�����켣</a>";
                    this.addMsg(SendReturnMsgFlag.FlowOver, mymsg, mymsgHtml, SendReturnMsgType.Info);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("@�ж�����{0}����������ִ���." + ex.Message, this.HisNode.Name));
            }
            #endregion
        }
        private string Func_DoSetThisWorkOver()
        {
            //���ý�����.  
            this.rptGe.SetValByKey(GERptAttr.FK_Dept, this.HisGenerWorkFlow.FK_Dept); //��ֵ���ܱ仯.
            this.rptGe.SetValByKey(GERptAttr.FlowEnder, this.Execer);
            this.rptGe.SetValByKey(GERptAttr.FlowEnderRDT, DataType.CurrentDataTime);
            if (this.town == null)
                this.rptGe.SetValByKey(GERptAttr.FlowEndNode, this.HisNode.NodeID);
            else
            {
                if (this.HisNode.HisRunModel == RunModel.FL || this.HisNode.HisRunModel == RunModel.FHL)
                    this.rptGe.SetValByKey(GERptAttr.FlowEndNode, this.HisNode.NodeID);
                else
                    this.rptGe.SetValByKey(GERptAttr.FlowEndNode, this.town.HisNode.NodeID);
            }

            this.rptGe.SetValByKey(GERptAttr.FlowDaySpan,
                DataType.GetSpanDays(rptGe.FlowStartRDT, DataType.CurrentDataTime));

            //���������������.
            if (this.HisWork.EnMap.PhysicsTable != this.rptGe.EnMap.PhysicsTable)
            {
                // ����״̬��
                this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
                this.HisWork.Rec = this.Execer;

                //�ж��ǲ���MD5���̣�
                if (this.HisFlow.IsMD5)
                    this.HisWork.SetValByKey("MD5", Glo.GenerMD5(this.HisWork));

                if (this.HisNode.IsStartNode)
                    this.HisWork.SetValByKey(StartWorkAttr.Title, this.HisGenerWorkFlow.Title);

                this.HisWork.DirectUpdate();
            }

            #region 2014-08-02 ɾ����������Ա�Ĵ��죬������ IsPass=0 ����.
            // ��������Ĺ�����.
            ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE IsPass=0 AND FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID AND FK_Emp <> " + dbStr + "FK_Emp"; ;
            ps.Clear();
            ps.Add("FK_Node", this.HisNode.NodeID);
            ps.Add("WorkID", this.WorkID);
            ps.Add("FK_Emp", this.Execer);
            DBAccess.RunSQL(ps);
            #endregion 2014-08-02 ɾ����������Ա�Ĵ��죬������ IsPass=0 ����.

            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
            ps.Add("FK_Node", this.HisNode.NodeID);
            ps.Add("WorkID", this.WorkID);
            DBAccess.RunSQL(ps);

            // ��generworkflow��ֵ��
            if (this.IsStopFlow == true)
                this.HisGenerWorkFlow.WFState = WFState.Complete;
            else
                this.HisGenerWorkFlow.WFState = WFState.Runing;

            // ����Ӧ���ʱ�䡣
            if (this.HisWork.EnMap.Attrs.Contains(WorkSysFieldAttr.SysSDTOfFlow))
                this.HisGenerWorkFlow.SDTOfFlow = this.HisWork.GetValStrByKey(WorkSysFieldAttr.SysSDTOfFlow);

            // ��һ���ڵ�Ӧ���ʱ�䡣
            if (this.HisWork.EnMap.Attrs.Contains(WorkSysFieldAttr.SysSDTOfNode))
                this.HisGenerWorkFlow.SDTOfFlow = this.HisWork.GetValStrByKey(WorkSysFieldAttr.SysSDTOfNode);

            //ִ�и��¡�
            if (this.IsStopFlow == false)
                this.HisGenerWorkFlow.Update();

            return "@�����Ѿ����.";
        }
        #endregion ��������
        /// <summary>
        /// ��ͨ�ڵ㵽��ͨ�ڵ�
        /// </summary>
        /// <param name="toND">Ҫ�������һ���ڵ�</param>
        /// <returns>ִ����Ϣ</returns>
        private void NodeSend_11(Node toND)
        {
            string sql = "";
            string errMsg = "";
            Work toWK = toND.HisWork;
            toWK.OID = this.WorkID;
            toWK.FID = this.HisWork.FID;

            // ���ִ������ת.
            if (this.IsSkip == true)
                toWK.RetrieveFromDBSources(); //�п�������ת.

            #region ִ�����ݳ�ʼ��
            // town.
            WorkNode town = new WorkNode(toWK, toND);

            errMsg = "���Ի����ǵĹ�����Ա - �ڼ���ִ���.";

            // ���Ի����ǵĹ�����Ա��
            GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);
            if (town.HisNode.TodolistModel == TodolistModel.Order && gwls.Count > 1)
            {
                /*�������Ľڵ��Ƕ������̽ڵ㣬��Ҫ�������ǵĶ���˳��.*/
                int idx = 0;
                foreach (GenerWorkerList gwl in gwls)
                {
                    idx++;
                    if (idx == 1)
                        continue;
                    gwl.IsPassInt = idx + 100;
                    gwl.Update();
                }
            }


            #region ����Ŀ��ڵ�����.
            if (this.HisWork.EnMap.PhysicsTable != toWK.EnMap.PhysicsTable)
            {
                errMsg = "����Ŀ��ڵ����� - �ڼ���ִ���.";

                //Ϊ��һ�����ʼ������.
                GenerWorkerList gwl = gwls[0] as GenerWorkerList;
                toWK.Rec = gwl.FK_Emp;
                string emps = gwl.FK_Emp;
                if (gwls.Count != 1)
                {
                    foreach (GenerWorkerList item in gwls)
                        emps += item.FK_Emp + ",";
                }
                toWK.Emps = emps;

                try
                {
                    if (this.IsSkip == true)
                        toWK.DirectUpdate(); // ���ִ������ת.
                    else
                        toWK.DirectInsert();
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineInfo("@����SQL�쳣�п�����û���޸��������ظ�����. Ext=" + ex.Message);
                    try
                    {
                        toWK.CheckPhysicsTable();
                        toWK.DirectUpdate();
                    }
                    catch (Exception ex1)
                    {
                        Log.DefaultLogWriteLineInfo("@���湤������" + ex1.Message);
                        throw new Exception("@���湤������" + toWK.EnDesc + ex1.Message);
                    }
                }
            }
            #endregion ����Ŀ��ڵ�����.

            //@������Ϣ�����
            this.SendMsgToThem(gwls);

            string htmlInfo = string.Format("@�����Զ����͸�{0}����{1}λ������,{2}.", this.nextStationName,
                this.HisRememberMe.NumOfObjs.ToString(), this.HisRememberMe.EmpsExt);

            string textInfo = string.Format("@�����Զ����͸�{0}����{1}λ������,{2}.", this.nextStationName,
                this.HisRememberMe.NumOfObjs.ToString(), this.HisRememberMe.ObjsExt);

            this.addMsg(SendReturnMsgFlag.ToEmps, textInfo, htmlInfo);


            #region �����������,����������������������е� ���ڵ㣬����Ա��
            try
            {
                Paras ps = new Paras();
                ps.SQL = "UPDATE ND" + int.Parse(toND.FK_Flow) + "Track SET NDTo=" + dbStr + "NDTo,NDToT=" + dbStr + "NDToT,EmpTo=" + dbStr + "EmpTo,EmpToT=" + dbStr + "EmpToT WHERE NDFrom=" + dbStr + "NDFrom AND EmpFrom=" + dbStr + "EmpFrom AND WorkID=" + dbStr + "WorkID AND ActionType=" + (int)ActionType.WorkCheck;
                ps.Add(TrackAttr.NDTo, toND.NodeID);
                ps.Add(TrackAttr.NDToT, toND.Name);
                ps.Add(TrackAttr.EmpTo, this.HisRememberMe.EmpsExt);
                ps.Add(TrackAttr.EmpToT, this.HisRememberMe.EmpsExt);
                ps.Add(TrackAttr.NDFrom, this.HisNode.NodeID);
                ps.Add(TrackAttr.EmpFrom, WebUser.No);
                ps.Add(TrackAttr.WorkID, this.WorkID);
                BP.DA.DBAccess.RunSQL(ps);
            }
            catch (Exception ex)
            {
                #region  �������ʧ�ܣ����������������ֶδ�С����
                Flow flow = new Flow(toND.FK_Flow);

                string updateLengthSql = string.Format("  alter table {0} alter column {1} varchar(2000) ", "ND" + int.Parse(toND.FK_Flow) + "Track", "EmpFromT");
                BP.DA.DBAccess.RunSQL(updateLengthSql);

                updateLengthSql = string.Format("  alter table {0} alter column {1} varchar(2000) ", "ND" + int.Parse(toND.FK_Flow) + "Track", "EmpFrom");
                BP.DA.DBAccess.RunSQL(updateLengthSql);

                updateLengthSql = string.Format("  alter table {0} alter column {1} varchar(2000) ", "ND" + int.Parse(toND.FK_Flow) + "Track", "EmpTo");
                BP.DA.DBAccess.RunSQL(updateLengthSql);
                updateLengthSql = string.Format("  alter table {0} alter column {1} varchar(2000) ", "ND" + int.Parse(toND.FK_Flow) + "Track", "EmpToT");
                BP.DA.DBAccess.RunSQL(updateLengthSql);


                Paras ps = new Paras();
                ps.SQL = "UPDATE ND" + int.Parse(toND.FK_Flow) + "Track SET NDTo=" + dbStr + "NDTo,NDToT=" + dbStr + "NDToT,EmpTo=" + dbStr + "EmpTo,EmpToT=" + dbStr + "EmpToT WHERE NDFrom=" + dbStr + "NDFrom AND EmpFrom=" + dbStr + "EmpFrom AND WorkID=" + dbStr + "WorkID AND ActionType=" + (int)ActionType.WorkCheck;
                ps.Add(TrackAttr.NDTo, toND.NodeID);
                ps.Add(TrackAttr.NDToT, toND.Name);
                ps.Add(TrackAttr.EmpTo, this.HisRememberMe.EmpsExt);
                ps.Add(TrackAttr.EmpToT, this.HisRememberMe.EmpsExt);
                ps.Add(TrackAttr.NDFrom, this.HisNode.NodeID);
                ps.Add(TrackAttr.EmpFrom, WebUser.No);
                ps.Add(TrackAttr.WorkID, this.WorkID);
                BP.DA.DBAccess.RunSQL(ps);


                #endregion
            }
            #endregion �����������.

            //string htmlInfo = string.Format("@�����Զ����͸�{0}���´�����{1}.", this.nextStationName,this._RememberMe.EmpsExt);
            //string textInfo = string.Format("@�����Զ����͸�{0}���´�����{1}.", this.nextStationName,this._RememberMe.ObjsExt);

            if (this.HisWorkerLists.Count >= 2 && this.HisNode.IsTask)
            {
                if (WebUser.IsWap)
                    this.addMsg(SendReturnMsgFlag.AllotTask, null, "<a href=\"" + this.VirPath + "WF/WorkOpt/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + toND.NodeID + "&FK_Flow=" + toND.FK_Flow + "')\"><img src='" + VirPath + "WF/Img/AllotTask.gif' border=0/>ָ���ض��Ĵ����˴���</a>��", SendReturnMsgType.Info);
                else
                    this.addMsg(SendReturnMsgFlag.AllotTask, null, "<a href=\"javascript:WinOpen('" + VirPath + "WF/WorkOpt/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + toND.NodeID + "&FK_Flow=" + toND.FK_Flow + "')\"><img src='" + VirPath + "WF/Img/AllotTask.gif' border=0/>ָ���ض��Ĵ����˴���</a>��", SendReturnMsgType.Info);
            }

            //if (WebUser.IsWap == false)
            //    this.addMsg(SendReturnMsgFlag.ToEmpExt, null, "@<a href=\"javascript:WinOpen('" + VirPath + "WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + toND.NodeID + "');\" ><img src='" + VirPath + "WF/Img/SMS.gif' border=0 />���ֻ�����������(��)</a>", SendReturnMsgType.Info);

            if (this.HisNode.HisFormType != NodeFormType.SDKForm)
            {
                if (this.HisNode.IsStartNode)
                {
                    if (WebUser.IsWap)
                        this.addMsg(SendReturnMsgFlag.ToEmpExt, null, "@<a href='" + VirPath + "WF/Wap/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.HisWork.OID + "&FK_Flow=" + toND.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>�� <a href='" + VirPath + "WF/Wap/MyFlow.aspx?FK_Flow=" + toND.FK_Flow + "&FK_Node=" + toND.FK_Flow + "01'><img src='" + VirPath + "WF/Img/New.gif' border=0/>�½�����</a>��", SendReturnMsgType.Info);
                    else
                        this.addMsg(SendReturnMsgFlag.ToEmpExt, null, "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.HisWork.OID + "&FK_Flow=" + toND.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>�� <a href='" + VirPath + "WF/MyFlow.aspx?FK_Flow=" + toND.FK_Flow + "&FK_Node=" + toND.FK_Flow + "01'><img src='" + VirPath + "WF/Img/New.gif' border=0/>�½�����</a>��", SendReturnMsgType.Info);
                }
                else
                    this.addMsg(SendReturnMsgFlag.ToEmpExt, null, "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.HisWork.OID + "&FK_Flow=" + toND.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>��", SendReturnMsgType.Info);
            }


            this.HisGenerWorkFlow.FK_Node = toND.NodeID;
            this.HisGenerWorkFlow.NodeName = toND.Name;

            //ps = new Paras();
            //ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + (int)WFState.Runing + ", FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
            //ps.Add("FK_Node", toND.NodeID);
            //ps.Add("NodeName", toND.Name);
            //ps.Add("WorkID", this.HisWork.OID);
            //DBAccess.RunSQL(ps);

            if (this.HisNode.HisFormType == NodeFormType.SDKForm || this.HisNode.HisFormType == NodeFormType.SelfForm)
            {
            }
            else
            {
                this.addMsg(SendReturnMsgFlag.WorkRpt, null, "@<img src='" + VirPath + "WF/Img/Btn/PrintWorkRpt.gif' ><a href='" + VirPath + "WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + toND.FK_Flow + "' target='_self' >�����켣</a>��");
            }
            this.addMsg(SendReturnMsgFlag.WorkStartNode, "@��һ��[" + toND.Name + "]�����ɹ�����.", "@��һ��<font color=blue>[" + toND.Name + "]</font>�����ɹ�����.");
            #endregion

            #region  ��ʼ������Ĺ����ڵ㡣
            if (this.HisWork.EnMap.PhysicsTable == toWK.EnMap.PhysicsTable)
            {
                /*�������ݺϲ�ģʽ, �Ͳ�ִ��copy*/
                this.CopyData(toWK, toND, true);
            }
            else
            {
                /* �����������Դ����ȣ���ִ��copy�� */
                this.CopyData(toWK, toND, false);
            }
            #endregion ��ʼ������Ĺ����ڵ�.

            #region �ж��Ƿ����������ۡ�
            if (toND.IsEval)
            {
                /*�����������������*/
                toWK.SetValByKey(WorkSysFieldAttr.EvalEmpNo, this.Execer);
                toWK.SetValByKey(WorkSysFieldAttr.EvalEmpName, this.ExecerName);
                toWK.SetValByKey(WorkSysFieldAttr.EvalCent, 0);
                toWK.SetValByKey(WorkSysFieldAttr.EvalNote, "");
            }
            #endregion

        }
        private void NodeSend_2X_GenerFH()
        {
            #region GenerFH
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                try
                {
                    fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                }
                catch (Exception ex)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + this.HisNode.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "Title";
                    int i = attr.Retrieve(MapAttrAttr.FK_MapData, attr.FK_MapData, MapAttrAttr.KeyOfEn, attr.KeyOfEn);
                    if (i == 0)
                    {
                        attr.KeyOfEn = "Title";
                        attr.Name = "����"; // "���̱���";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.Idx = -100;
                        attr.Insert();
                    }
                    fh.Title = this.Execer + "-" + this.ExecerName + " @ " + DataType.CurrentDataTime + " ";
                }
                fh.RDT = DataType.CurrentData;
                fh.FID = this.WorkID;
                fh.FK_Flow = this.HisNode.FK_Flow;
                fh.FK_Node = this.HisNode.NodeID;
                fh.GroupKey = this.Execer;
                fh.WFState = 0;
                try
                {
                    fh.DirectInsert();
                }
                catch
                {
                    fh.DirectUpdate();
                }
            }
            #endregion GenerFH
        }
        /// <summary>
        /// ������������·��� to ���.
        /// </summary>
        /// <returns></returns>
        private void NodeSend_24_UnSameSheet(Nodes toNDs)
        {
            NodeSend_2X_GenerFH();

            /*�ֱ�����ÿ���ڵ����Ϣ.*/
            string msg = "";

            #region ��ѯ������ǰ���̽ڵ����ݣ�Ϊ���̵߳Ľڵ�copy�������á�
            //��ѯ������һ���ڵ�ĸ�����Ϣ����
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());
            //��ѯ������һ��Ele��Ϣ����
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());
            #endregion

            //����ϵͳ����.
            string workIDs = "";
            string empIDs = "";
            string empNames = "";
            string toNodeIDs = "";

            foreach (Node nd in toNDs)
            {
                msg += "@" + nd.Name + "�����Ѿ��������������ߣ�";

                //����һ��������Ϣ��
                Work wk = nd.HisWork;
                wk.Copy(this.HisWork);
                wk.FID = this.HisWork.OID;
                wk.OID = BP.DA.DBAccess.GenerOID("WorkID");
                wk.BeforeSave();
                wk.DirectInsert();

                //������Ĺ����ߡ�
                WorkNode town = new WorkNode(wk, nd);
                GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);
                if (gwls.Count == 0)
                {
                    msg += "@" + nd.Name + "�����Ѿ��������������ߣ�";
                    msg += "�ڵ�:" + nd.Name + "��û���ҵ��ɴ������Ա�����߳̽ڵ��޷�������";
                    wk.Delete();
                    continue;
                }

                #region ִ������copy.
                if (athDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.MyPK = BP.DA.DBAccess.GenerGUID();
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        athDB_N.RefPKVal = wk.OID.ToString();
                        athDB_N.Insert();
                    }
                }

                if (eleDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.Insert();
                    }
                }
                #endregion ִ������copy.

                foreach (GenerWorkerList wl in gwls)
                {
                    msg += wl.FK_Emp + "��" + wl.FK_EmpText + "��";
                    // ������������Ϣ��
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = wk.OID;
                    if (gwf.IsExits == false)
                    {
                        gwf.FID = this.WorkID;

                        //#warning ��Ҫ�޸ĳɱ������ɹ���
                        //#warning �������̵�Titlte�븸���̵�һ��.

                        gwf.Title = this.HisGenerWorkFlow.Title; // WorkNode.GenerTitle(this.rptGe);
                        gwf.WFState = WFState.Runing;
                        gwf.RDT = DataType.CurrentDataTime;
                        gwf.Starter = this.Execer;
                        gwf.StarterName = this.ExecerName;
                        gwf.FK_Flow = nd.FK_Flow;
                        gwf.FlowName = nd.FlowName;
                        gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;
                        gwf.FK_Node = nd.NodeID;
                        gwf.NodeName = nd.Name;
                        gwf.FK_Dept = wl.FK_Dept;
                        gwf.DeptName = wl.FK_DeptT;
                        gwf.TodoEmps = wl.FK_Emp + "," + wl.FK_EmpText;
                        gwf.DirectInsert();
                    }

                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerlist SET WorkID=" + dbStr + "WorkID1,FID=" + dbStr + "FID WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr + "WorkID2 AND FK_Node=" + dbStr + "FK_Node ";
                    ps.Add("WorkID1", wk.OID);
                    ps.Add("FID", this.WorkID);

                    ps.Add("FK_Emp", wl.FK_Emp);
                    ps.Add("WorkID2", wl.WorkID);
                    ps.Add("FK_Node", wl.FK_Node);
                    DBAccess.RunSQL(ps);

                    //��¼����.
                    workIDs += wk.OID.ToString() + ",";
                    empIDs += wl.FK_Emp + ",";
                    empNames += wl.FK_EmpText + ",";
                    toNodeIDs += gwf.FK_Node + ",";

                    //���¹�����Ϣ.
                    wk.Rec = wl.FK_Emp;
                    wk.Emps = "@" + wl.FK_Emp;
                    //wk.RDT = DataType.CurrentDataTimess;
                    wk.DirectUpdate();
                }
            }

            //��������������ʾ��Ϣ��
            this.addMsg("FenLiuUnSameSheet", msg);



            //���������
            this.addMsg(SendReturnMsgFlag.VarTreadWorkIDs, workIDs, workIDs, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersID, empIDs, empIDs, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersName, empNames, empNames, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarToNodeIDs, toNodeIDs, toNodeIDs, SendReturnMsgType.SystemMsg);
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="toWN"></param>
        /// <returns></returns>
        private GenerWorkerLists NodeSend_24_SameSheet_GenerWorkerList(WorkNode toWN)
        {
            return null;
        }
        /// <summary>
        /// ������������·��� to ͬ��.
        /// </summary>
        /// <param name="toNode">����ķ����ڵ�</param>
        private void NodeSend_24_SameSheet(Node toNode)
        {
            if (this.HisGenerWorkFlow.Title == "δ����")
                this.HisGenerWorkFlow.Title = WorkNode.GenerTitle(this.HisFlow, this.HisWork);

            #region ɾ������ڵ�����߳�����У���ֹ�˻���Ϣ������������,����˻ش�����������־Ͳ���Ҫ������.
            ps = new Paras();
            ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "FID  AND FK_Node=" + dbStr + "FK_Node";
            ps.Add("FID", this.HisWork.OID);
            ps.Add("FK_Node", toNode.NodeID);
            #endregion ɾ������ڵ�����߳�����У���ֹ�˻���Ϣ�����������⣬����˻ش�����������־Ͳ���Ҫ������.

            #region GenerFH
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                try
                {
                    fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                }
                catch (Exception ex)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + this.HisNode.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "Title";
                    int i = attr.Retrieve(MapAttrAttr.FK_MapData, attr.FK_MapData, MapAttrAttr.KeyOfEn, attr.KeyOfEn);
                    if (i == 0)
                    {
                        attr.KeyOfEn = "Title";
                        attr.Name = "����"; // "���̱���";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.Idx = -100;
                        attr.Insert();
                    }
                    fh.Title = this.Execer + "-" + this.ExecerName + " @ " + DataType.CurrentDataTime + " ";
                }
                fh.RDT = DataType.CurrentData;
                fh.FID = this.WorkID;
                fh.FK_Flow = this.HisNode.FK_Flow;
                fh.FK_Node = this.HisNode.NodeID;
                fh.GroupKey = this.Execer;
                fh.WFState = 0;
                fh.Save();
            }
            #endregion GenerFH

            #region ������һ����Ĺ�����Ա
            // ����.
            Work wk = toNode.HisWork;
            wk.Copy(this.rptGe);
            wk.Copy(this.HisWork);  //���ƹ������������Ϣ��
            wk.FID = this.HisWork.OID; // �Ѹù���FID���óɸ������ϵĹ���ID.

            // ����Ľڵ�.
            town = new WorkNode(wk, toNode);

            // ������һ����Ҫִ�е���Ա.
            GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);

            //����ǰ������Ա�����Ѿ��������ʷ����. add 2015-01-14,��������Ŀ�ľ��ǣ������÷����ڵ�ķ�����Ա����ÿ�����߳ǵ���;����.
            gwls.Delete(GenerWorkerListAttr.FK_Node, this.HisNode.NodeID, GenerWorkerListAttr.FID, this.WorkID); //�������.


            //�����ǰ�����ݣ��������η��͡�
            if (this.HisFlow.HisDataStoreModel == DataStoreModel.ByCCFlow)
                wk.Delete(WorkAttr.FID, this.HisWork.OID);

            // �жϷ����Ĵ���.�ǲ�����ʷ��¼�����з�����
            bool IsHaveFH = false;
            if (this.HisNode.IsStartNode == false)
            {
                ps = new Paras();
                ps.SQL = "SELECT COUNT(WorkID) FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "OID";
                ps.Add("OID", this.HisWork.OID);
                if (DBAccess.RunSQLReturnValInt(ps) != 0)
                    IsHaveFH = true;
            }
            #endregion ������һ����Ĺ�����Ա

            #region ��������.

            //��õ�ǰ�������ڵ�����.
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                                            this.WorkID.ToString());

            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                                           this.WorkID.ToString());

            MapDtls dtlsFrom = new MapDtls("ND" + this.HisNode.NodeID);
            if (dtlsFrom.Count > 1)
            {
                foreach (MapDtl d in dtlsFrom)
                    d.HisGEDtls_temp = null;
            }
            MapDtls dtlsTo = null;
            if (dtlsFrom.Count >= 1)
                dtlsTo = new MapDtls("ND" + toNode.NodeID);

            ///����ϵͳ����.
            string workIDs = "";

            DataTable dtWork = null;
            if (toNode.HisDeliveryWay == DeliveryWay.BySQLAsSubThreadEmpsAndData)
            {
                /*����ǰ��ղ�ѯ�ӣѣ̣�ȷ����ϸ��Ľ����������̵߳����ݡ�*/
                string sql = toNode.DeliveryParas;
                sql = Glo.DealExp(sql, this.HisWork, null);
                dtWork = BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
            if (toNode.HisDeliveryWay == DeliveryWay.ByDtlAsSubThreadEmps)
            {
                /*����ǰ�����ϸ��ȷ����ϸ��Ľ����������̵߳����ݡ�*/
                foreach (MapDtl dtl in dtlsFrom)
                {
                    //����˳�򣬷�ֹ�仯����Ա��ű仯��������ϸ���н������ظ������⡣
                    string sql = "SELECT * FROM " + dtl.PTable + " WHERE RefPK=" + this.WorkID + " ORDER BY OID";
                    dtWork = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    if (dtWork.Columns.Contains("UserNo"))
                        break;
                    else
                        dtWork = null;
                }
            }

            string groupMark = "";
            int idx = -1;
            foreach (GenerWorkerList wl in gwls)
            {
                if (this.IsHaveSubThreadGroupMark == true)
                {
                    /*������������δ���,���߳ǵ�����..*/
                    if (groupMark.Contains("@" + wl.FK_Emp + "," + wl.GroupMark) == false)
                        groupMark += "@" + wl.FK_Emp + "," + wl.GroupMark;
                    else
                    {
                        wl.Delete(); //ɾ��������������.
                        continue;
                    }
                }

                idx++;
                Work mywk = toNode.HisWork;

                #region ��������.
                mywk.Copy(this.rptGe);
                mywk.Copy(this.HisWork);  // ���ƹ�����Ϣ��
                if (dtWork != null)
                {
                    /*��IDX������Ϊ�˽������Ա�ظ�����������Դ���һ��ܸ���������Ӧ���ϡ�*/
                    DataRow dr = dtWork.Rows[idx];
                    if (dtWork.Columns.Contains("UserNo")
                        && dr["UserNo"].ToString() == wl.FK_Emp)
                    {
                        mywk.Copy(dr);
                    }

                    if (dtWork.Columns.Contains("No")
                       && dr["No"].ToString() == wl.FK_Emp)
                    {
                        mywk.Copy(dr);
                    }
                }
                #endregion ��������.

                bool isHaveEmp = false;
                if (IsHaveFH)
                {
                    /* ��������߹��������������ҵ�ͬһ����Աͬһ��FID�µ�OID �����⵱ǰ�̵߳�ID��*/
                    ps = new Paras();
                    ps.SQL = "SELECT WorkID,FK_Node FROM WF_GenerWorkerlist WHERE FK_Node!=" + dbStr + "FK_Node AND FID=" + dbStr + "FID AND FK_Emp=" + dbStr + "FK_Emp ORDER BY RDT DESC";
                    ps.Add("FK_Node", toNode.NodeID);
                    ps.Add("FID", this.WorkID);
                    ps.Add("FK_Emp", wl.FK_Emp);
                    DataTable dt = DBAccess.RunSQLReturnTable(ps);
                    if (dt.Rows.Count == 0)
                    {
                        /*û�з��֣���˵����ǰ�����ڵ���û������˵ķ�����Ϣ. */
                        mywk.OID = DBAccess.GenerOID("WorkID");
                    }
                    else
                    {
                        int workid_old = (int)dt.Rows[0][0];
                        int fk_Node_nearly = (int)dt.Rows[0][1];
                        Node nd_nearly = new Node(fk_Node_nearly);
                        Work nd_nearly_work = nd_nearly.HisWork;
                        nd_nearly_work.OID = workid_old;
                        if (nd_nearly_work.RetrieveFromDBSources() != 0)
                        {
                            mywk.Copy(nd_nearly_work);
                            mywk.OID = workid_old;
                        }
                        else
                        {
                            mywk.OID = DBAccess.GenerOID("WorkID");
                        }

                        // ��ϸ�����ݻ��ܱ�Ҫ���Ƶ����̵߳�������ȥ.
                        foreach (MapDtl dtl in dtlsFrom)
                        {
                            if (dtl.IsHLDtl == false)
                                continue;

                            string sql = "SELECT * FROM " + dtl.PTable + " WHERE Rec='" + wl.FK_Emp + "' AND RefPK='" + this.WorkID + "'";
                            DataTable myDT = DBAccess.RunSQLReturnTable(sql);
                            if (myDT.Rows.Count == 1)
                            {
                                Attrs attrs = mywk.EnMap.Attrs;
                                foreach (Attr attr in attrs)
                                {
                                    switch (attr.Key)
                                    {
                                        case GEDtlAttr.FID:
                                        case GEDtlAttr.OID:
                                        case GEDtlAttr.Rec:
                                        case GEDtlAttr.RefPK:
                                            continue;
                                        default:
                                            break;
                                    }

                                    if (myDT.Columns.Contains(attr.Field) == true)
                                        mywk.SetValByKey(attr.Key, myDT.Rows[0][attr.Field]);
                                }
                            }
                        }
                        isHaveEmp = true;
                    }
                }
                else
                {
                    //Ϊ���̲߳���WorkID.
                    mywk.OID = DBAccess.GenerOID("WorkID");  //BP.DA.DBAccess.GenerOID();
                }
                if (this.HisWork.FID == 0)
                    mywk.FID = this.HisWork.OID;

                mywk.Rec = wl.FK_Emp;
                mywk.Emps = wl.FK_Emp;
                mywk.BeforeSave();

                //�ж��ǲ���MD5���̣�
                if (this.HisFlow.IsMD5)
                    mywk.SetValByKey("MD5", Glo.GenerMD5(mywk));

                mywk.InsertAsOID(mywk.OID);

                //��ϵͳ������ֵ�����ڷ��ͺ󷵻ض�����.
                workIDs += mywk.OID + ",";

                #region  ���Ƹ�����Ϣ
                if (athDBs.Count > 0)
                {
                    /* ˵����ǰ�ڵ��и������� */
                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toNode.NodeID,
                        FrmAttachmentDBAttr.RefPKVal, mywk.OID);

                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + toNode.NodeID;
                        athDB_N.RefPKVal = mywk.OID.ToString();
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                          "ND" + toNode.NodeID);

                        if (athDB_N.HisAttachmentUploadType == AttachmentUploadType.Single)
                        {
                            //ע������ǵ��������������������ܱ仯������ᵼ����ǰ̨Լ����ȡ���ݴ���
                            athDB_N.MyPK = athDB_N.FK_FrmAttachment + "_" + mywk.OID;
                            try
                            {
                                athDB_N.DirectInsert();
                            }
                            catch
                            {
                                athDB_N.MyPK = BP.DA.DBAccess.GenerGUID();
                                athDB_N.Insert();
                            }
                        }
                        else
                        {
                            try
                            {
                                // �฽������: FK_MapData+���кŵķ�ʽ, �滻����������Ա���,�����ظ�.
                                athDB_N.MyPK = athDB_N.UploadGUID + "_" + athDB_N.FK_MapData + "_" + athDB_N.RefPKVal;
                                athDB_N.DirectInsert();
                            }
                            catch
                            {
                                athDB_N.MyPK = BP.DA.DBAccess.GenerGUID();
                                athDB_N.Insert();
                            }
                        }
                    }
                }
                #endregion  ���Ƹ�����Ϣ

                #region  ����ǩ����Ϣ
                if (eleDBs.Count > 0)
                {
                    /* ˵����ǰ�ڵ��и������� */
                    eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + toNode.NodeID,
                        FrmEleDBAttr.RefPKVal, mywk.OID);
                    int i = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        i++;
                        FrmEleDB athDB_N = new FrmEleDB();
                        athDB_N.Copy(eleDB);
                        athDB_N.FK_MapData = "ND" + toNode.NodeID;
                        athDB_N.RefPKVal = mywk.OID.ToString();
                        athDB_N.GenerPKVal();
                        athDB_N.DirectInsert();
                    }
                }
                #endregion  ���Ƹ�����Ϣ

                #region  ���ƴӱ���Ϣ.
                if (dtlsFrom.Count > 0)
                {
                    int i = -1;
                    foreach (Sys.MapDtl dtl in dtlsFrom)
                    {
                        i++;
                        if (dtlsTo.Count <= i)
                            continue;
                        Sys.MapDtl toDtl = (Sys.MapDtl)dtlsTo[i];
                        if (toDtl.IsCopyNDData == false)
                            continue;

                        if (toDtl.PTable == dtl.PTable)
                            continue;

                        //��ȡ��ϸ���ݡ�
                        GEDtls gedtls = null;
                        if (dtl.HisGEDtls_temp == null)
                        {
                            gedtls = new GEDtls(dtl.No);
                            QueryObject qo = null;
                            qo = new QueryObject(gedtls);
                            switch (dtl.DtlOpenType)
                            {
                                case DtlOpenType.ForEmp:
                                    qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                    break;
                                case DtlOpenType.ForWorkID:
                                    qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                    break;
                                case DtlOpenType.ForFID:
                                    qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                    break;
                            }
                            qo.DoQuery();
                            dtl.HisGEDtls_temp = gedtls;
                        }
                        gedtls = dtl.HisGEDtls_temp;

                        int unPass = 0;
                        DBAccess.RunSQL("DELETE FROM " + toDtl.PTable + " WHERE RefPK=" + dbStr + "RefPK", "RefPK", mywk.OID);
                        foreach (GEDtl gedtl in gedtls)
                        {
                            BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                            dtCopy.Copy(gedtl);
                            dtCopy.FK_MapDtl = toDtl.No;
                            dtCopy.RefPK = mywk.OID.ToString();
                            dtCopy.OID = 0;
                            dtCopy.Insert();

                            #region  ���ƴӱ��� - ������Ϣ - M2M- M2MM
                            if (toDtl.IsEnableAthM)
                            {
                                /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                                athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                if (athDBs.Count > 0)
                                {
                                    i = 0;
                                    foreach (FrmAttachmentDB athDB in athDBs)
                                    {
                                        i++;
                                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                        athDB_N.Copy(athDB);
                                        athDB_N.FK_MapData = toDtl.No;
                                        athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                            "ND" + toNode.NodeID);
                                        athDB_N.RefPKVal = dtCopy.OID.ToString();
                                        athDB_N.DirectInsert();
                                    }
                                }
                            }
                            if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                            {
                                /*���������m2m */
                                M2Ms m2ms = new M2Ms(dtl.No, gedtl.OID);
                                if (m2ms.Count > 0)
                                {
                                    i = 0;
                                    foreach (M2M m2m in m2ms)
                                    {
                                        i++;
                                        M2M m2m_N = new M2M();
                                        m2m_N.Copy(m2m);
                                        m2m_N.FK_MapData = toDtl.No;
                                        m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                        m2m_N.EnOID = gedtl.OID;
                                        m2m_N.InitMyPK();
                                        m2m_N.DirectInsert();
                                    }
                                }
                            }
                            #endregion  ���ƴӱ��� - ������Ϣ

                        }
                    }
                }
                #endregion  ���Ƹ�����Ϣ

                #region ������������Ϣ
                // ������������Ϣ��
                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.WorkID = mywk.OID;
                if (gwf.RetrieveFromDBSources() == 0)
                {
                    gwf.FID = this.WorkID;
                    gwf.FK_Node = toNode.NodeID;

                    if (this.HisNode.IsStartNode)
                        gwf.Title = WorkNode.GenerTitle(this.HisFlow, this.HisWork) + "(" + wl.FK_EmpText + ")";
                    else
                        gwf.Title = this.HisGenerWorkFlow.Title + "(" + wl.FK_EmpText + ")";

                    gwf.WFState = WFState.Runing;
                    gwf.RDT = DataType.CurrentDataTime;
                    gwf.Starter = this.Execer;
                    gwf.StarterName = this.ExecerName;
                    gwf.FK_Flow = toNode.FK_Flow;
                    gwf.FlowName = toNode.FlowName;
                    gwf.FID = this.WorkID;
                    gwf.FK_FlowSort = toNode.HisFlow.FK_FlowSort;
                    gwf.NodeName = toNode.Name;
                    gwf.FK_Dept = wl.FK_Dept;
                    gwf.DeptName = wl.FK_DeptT;
                    gwf.TodoEmps = wl.FK_Emp + "," + wl.FK_EmpText;
                    if (wl.GroupMark != "")
                        gwf.Paras_GroupMark = wl.GroupMark;

                    gwf.Sender = BP.WF.Glo.DealUserInfoShowModel(WebUser.No, WebUser.Name);

                    gwf.DirectInsert();
                }
                else
                {
                    if (wl.GroupMark != "")
                        gwf.Paras_GroupMark = wl.GroupMark;

                    gwf.Sender = BP.WF.Glo.DealUserInfoShowModel(WebUser.No, WebUser.Name);
                    gwf.FK_Node = toNode.NodeID;
                    gwf.NodeName = toNode.Name;
                    gwf.Update();
                }

                // ���뵱ǰ�����ڵ�Ĵ�����Ա,�����������;�￴������.
                wl.FK_Emp = WebUser.No;
                wl.FK_EmpText = WebUser.Name;
                wl.FK_Node = this.HisNode.NodeID;
                wl.Sender = WebUser.No + "," + WebUser.Name;
                wl.IsPassInt = -2; // -2; //��־�ýڵ��Ǹ�������Ա����Ľڵ�.
                //  wl.FID = 0; //����Ǹ�����
                wl.Insert();

                // ����ʱ��workid ���µ�
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET WorkID=" + dbStr + "WorkID1 WHERE WorkID=" + dbStr + "WorkID2";
                ps.Add("WorkID1", mywk.OID);
                ps.Add("WorkID2", wl.WorkID); //��ʱ��ID,�������µ�workid.
                int num = DBAccess.RunSQL(ps);
                if (num == 0)
                    throw new Exception("@��Ӧ�ø��²�������");

                #endregion ������������Ϣ

            }
            #endregion ��������.

            #region ������Ϣ��ʾ
            string info = "@�����ڵ�:{0}�Ѿ�����,@�����Զ����͸�����(" + this.HisRememberMe.NumOfObjs + ")��������{1}.";
            this.addMsg("FenLiuInfo",
                string.Format(info, toNode.Name, this.HisRememberMe.EmpsExt));

            //�����̵߳� WorkIDs ����ϵͳ����.
            this.addMsg(SendReturnMsgFlag.VarTreadWorkIDs, workIDs, workIDs, SendReturnMsgType.SystemMsg);

            // ����ǿ�ʼ�ڵ㣬�Ϳ�������ѡ������ˡ�
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2 && this.HisNode.IsTask)
                    this.addMsg("AllotTask", "@<img src='" + VirPath + "WF/Img/AllotTask.gif' border=0 /><a href=\"" + VirPath + "WF/WorkOpt/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "\" >�޸Ľ��ܶ���</a>.");
            }

            if (this.HisNode.IsStartNode)
            {
                if (WebUser.IsWap)
                    this.addMsg("UnDoNew", "@<a href='" + VirPath + "WF/Wap/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>�� <a href='" + VirPath + "WF/Wap/MyFlow.aspx?FK_Flow=" + toNode.FK_Flow + "&FK_Node=" + toNode.FK_Flow + "01' ><img src='" + VirPath + "WF/Img/New.gif' border=0/>�½�����</a>��");
                else
                    this.addMsg("UnDoNew", "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>�� <a href='" + this.VirPath + this.AppType + "/MyFlow.aspx?FK_Flow=" + toNode.FK_Flow + "&FK_Node=" + toNode.FK_Flow + "01' ><img src='" + VirPath + "WF/Img/New.gif' border=0/>�½�����</a>��");
            }
            else
            {
                this.addMsg("UnDo", "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo.aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + VirPath + "WF/Img/UnDo.gif' border=0/>�������η���</a>��");
            }

            this.addMsg("Rpt", "@<a href='" + VirPath + "WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_self' >�����켣</a>");
            #endregion ������Ϣ��ʾ
        }
        /// <summary>
        /// �����㵽��ͨ�㷢��
        /// 1. ����Ҫ��������.
        /// 2, ����ͨ�ڵ�����ͨ�ڵ㷢��.
        /// </summary>
        /// <returns></returns>
        private void NodeSend_31(Node nd)
        {
            //��������.

            // ��1-1һ�����߼�����.
            this.NodeSend_11(nd);
        }
        /// <summary>
        /// ���߳����·���
        /// </summary>
        /// <returns></returns>
        private string NodeSend_4x()
        {
            return null;
        }
        /// <summary>
        /// ���߳��������
        /// </summary>
        /// <returns></returns>
        private void NodeSend_53_SameSheet_To_HeLiu(Node toNode)
        {
            Work toNodeWK = toNode.HisWork;
            toNodeWK.Copy(this.HisWork);
            toNodeWK.OID = this.HisWork.FID;
            toNodeWK.FID = 0;
            this.town = new WorkNode(toNodeWK, toNode);

            // ��ȡ���ﵱǰ�����ڵ��� ����һ��������֮������߳̽ڵ�ļ��ϡ�
            string spanNodes = this.SpanSubTheadNodes(toNode);

            #region FID
#warning lost FID.

            Int64 fid = this.HisWork.FID;
            if (fid == 0)
            {
                if (this.HisNode.HisRunModel != RunModel.SubThread)
                    throw new Exception("@��ǰ�ڵ�����߳̽ڵ�.");

                string strs = BP.DA.DBAccess.RunSQLReturnStringIsNull("SELECT FID FROM WF_GenerWorkFlow WHERE WorkID=" + this.HisWork.OID, "0");
                if (strs == "0")
                    throw new Exception("@��ʧFID��Ϣ");
                fid = Int64.Parse(strs);

                this.HisWork.FID = fid;
            }
            #endregion FID

            GenerFH myfh = new GenerFH(fid);
            if (myfh.FK_Node == toNode.NodeID)
            {
                /* ˵�����ǵ�һ�ε�����ڵ�������, 
                 * ���磺һ�����̣�
                 * A����-> B��ͨ-> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ�Ѿ���һ���̵߳����C.
                 */

                /* 
                 * ����:�������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
                 * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
                 */

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID AND FK_Node=" + dbStr + "FK_Node AND IsPass=0";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);


                this.HisGenerWorkFlow.FK_Node = toNode.NodeID;
                this.HisGenerWorkFlow.NodeName = toNode.Name;

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow  SET  WFState=" + (int)WFState.Runing + ", FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", toNode.NodeID);
                ps.Add("NodeName", toNode.Name);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region ���������

                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr + "FK_Node AND FID=" + dbStr + "FID AND IsPass=1";
                ps.Add("FK_Node", this.HisNode.NodeID);
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];

                //������̵߳�������
                ps = new Paras();
                ps.SQL = "SELECT DISTINCT(WorkID) FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr + "FK_Node AND FID=" + dbStr + "FID AND IsPass=1";
                ps.Add("FK_Node", this.HisNode.NodeID);
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_thread = BP.DA.DBAccess.RunSQLReturnTable(ps);
                decimal ok = (decimal)dt_thread.Rows.Count;

                ps = new Paras();
                ps.SQL = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE   IsEnable=1 AND FID=" + dbStr + "FID AND FK_Node IN (" + spanNodes + ")";
                ps.Add("FID", this.HisWork.FID);
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(ps);
                if (all == 0)
                    throw new Exception("@��ȡ�����߳��������ִ���,�߳�����Ϊ0,ִ�е�sql:" + ps.SQL + " FID=" + this.HisWork.FID);

                decimal passRate = ok / all * 100;
                numStr = "@���ǵ�(" + ok + ")����˽ڵ��ϵĴ����ˣ���������(" + all + ")�������̡�";
                if (toNode.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID",
                        "FK_Node", toNode.NodeID, "WorkID", this.HisWork.FID);
                    numStr += "@��һ������(" + toNode.Name + ")�Ѿ�������";
                }
                #endregion ���������


                if (myfh.ToEmpsMsg.Contains("("))
                {
                    string FK_Emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('('));
                    this.AddToTrack(ActionType.ForwardHL, FK_Emp1, myfh.ToEmpsMsg, toNode.NodeID, toNode.Name, null);

                    //���ӱ���.
                    this.addMsg(SendReturnMsgFlag.VarAcceptersID, FK_Emp1, SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarAcceptersName, FK_Emp1, SendReturnMsgType.SystemMsg);
                }

                // �����������ܴӱ�����.
                this.GenerHieLiuHuiZhongDtlData_2013(toNode);

                this.addMsg("ToHeLiuEmp",
                    "@�����Ѿ����е������ڵ�[" + toNode.Name + "].@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��" + this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID) + numStr);
            }
            else
            {
                /* �Ѿ���FID��˵������ǰ�Ѿ��з������ߺ����ڵ㡣*/
                /*
                 * ���´������û�����̵����λ��
                 * ˵���ǵ�һ�ε�����ڵ�������.
                 * ���磺һ������:
                 * A����-> B��ͨ-> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ���ǵ�һ������C.
                 */

                // ���Ի����ǵĹ�����Ա��
                GenerWorkerLists gwls = this.Func_GenerWorkerLists(this.town);

                string FK_Emp = "";
                string toEmpsStr = "";
                string emps = "";
                foreach (GenerWorkerList wl in gwls)
                {
                    toEmpsStr += BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);

                    if (gwls.Count == 1)
                        emps = wl.FK_Emp;
                    else
                        emps += "@" + FK_Emp;
                }
                //���ӱ���.
                this.addMsg(SendReturnMsgFlag.VarAcceptersID, emps.Replace("@", ","), SendReturnMsgType.SystemMsg);
                this.addMsg(SendReturnMsgFlag.VarAcceptersName, toEmpsStr, SendReturnMsgType.SystemMsg);

                /* 
                * �������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
                * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
                */

                #region ���ø�����״̬ ���õ�ǰ�Ľڵ�Ϊ:
                myfh.Update(GenerFHAttr.FK_Node, toNode.NodeID,
                    GenerFHAttr.ToEmpsMsg, toEmpsStr);

                Work mainWK = town.HisWork;
                mainWK.OID = this.HisWork.FID;
                mainWK.RetrieveFromDBSources();


                // ���Ʊ�����������ݵ���������ȥ��
                DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM " + this.HisFlow.PTable + " WHERE OID=" + dbStr + "OID",
                    "OID", this.HisWork.FID);
                foreach (DataColumn dc in dt.Columns)
                    mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

                mainWK.Rec = FK_Emp;
                mainWK.Emps = emps;
                mainWK.OID = this.HisWork.FID;
                mainWK.Save();

                // �����������ܴӱ�����.
                this.GenerHieLiuHuiZhongDtlData_2013(toNode);

                /*��������ݵĸ��ơ�*/
                #region ���Ƹ�����
                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (athDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + toNode.NodeID;
                        athDB_N.RefPKVal = this.HisWork.FID.ToString();
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                          "ND" + toNode.NodeID);

                        if (athDB_N.HisAttachmentUploadType == AttachmentUploadType.Single)
                        {
                            /*����ǵ�����.*/
                            athDB_N.MyPK = athDB_N.FK_FrmAttachment + "_" + this.HisWork.FID;
                            if (athDB_N.IsExits == true)
                                continue; /*˵����һ���ڵ�������߳��Ѿ�copy����, ���ǻ������߳�������㴫�����ݵĿ��ܣ����Բ�����break.*/
                            athDB_N.Insert();
                        }
                        else
                        {
                            //�ж����guid ���ϴ��ļ��Ƿ��������߳�copy��ȥ�ˣ�
                            if (athDB_N.IsExit(FrmAttachmentDBAttr.UploadGUID, athDB_N.UploadGUID,
                                FrmAttachmentDBAttr.FK_MapData, athDB_N.FK_MapData) == true)
                                continue; /*����ǾͲ�Ҫcopy��.*/

                            athDB_N.MyPK = athDB_N.UploadGUID + "_" + athDB_N.FK_MapData;
                            athDB_N.Insert();
                        }
                    }
                }
                #endregion ���Ƹ�����

                #region ����Ele��
                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (eleDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + toNode.NodeID;
                        eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + toNode.NodeID);
                        eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                        eleDB_N.Save();
                    }
                }
                #endregion ���Ƹ�����

                /* ��������Ҫ�ȴ�����������ȫ�����������ܿ�������*/
                string sql1 = "";
                // "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
                // string sql1 = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE  IsPass=0 AND FID=" + this.HisWork.FID;

#warning ���ڶ���ֺ�������ܻ������⡣
                sql1 = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
                decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
                decimal passRate1 = 1 / numAll1 * 100;
                if (toNode.PassRate <= passRate1)
                {
                    /* ��ʱ�Ѿ�ͨ��,���������߳̿�������. */
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                    ps.Add("FK_Node", toNode.NodeID);
                    ps.Add("WorkID", this.HisWork.FID);
                    int num = DBAccess.RunSQL(ps);
                    if (num == 0)
                        throw new Exception("@��Ӧ�ø��²�����.");
                }
                else
                {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                    ps.Add("FK_Node", toNode.NodeID);
                    ps.Add("WorkID", this.HisWork.FID);
                    int num = DBAccess.RunSQL(ps);
                    if (num == 0)
                        throw new Exception("@��Ӧ�ø��²�����.");
                }

                this.HisGenerWorkFlow.FK_Node = toNode.NodeID;
                this.HisGenerWorkFlow.NodeName = toNode.Name;

                //�ı䵱ǰ���̵ĵ�ǰ�ڵ�.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + (int)WFState.Runing + ",  FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", toNode.NodeID);
                ps.Add("NodeName", toNode.Name);
                ps.Add("WorkID", this.HisWork.FID);
                DBAccess.RunSQL(ps);

                //���õ�ǰ���߳��Ѿ�ͨ��.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID AND IsPass=0";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                DBAccess.RunSQL(ps);
                #endregion ���ø�����״̬

                this.addMsg("InfoToHeLiu", "@�����Ѿ����е������ڵ�[" + toNode.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��@���ǵ�һ������˽ڵ�Ĵ�����.");
            }
        }
        private string NodeSend_55(Node toNode)
        {
            return null;
        }
        /// <summary>
        /// �ڵ������˶�
        /// </summary>
        private void NodeSend_Send_5_5()
        {
            //ִ�����õ�ǰ��Ա�����ʱ��. for: anhua 2013-12-18.
            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerlist SET CDT=" + dbstr + "CDT WHERE WorkID=" + dbstr + "WorkID AND FK_Node=" + dbstr + "FK_Node AND FK_Emp=" + dbstr + "FK_Emp";
            ps.Add(GenerWorkerListAttr.CDT, DataType.CurrentDataTimess);
            ps.Add(GenerWorkerListAttr.WorkID, this.WorkID);
            ps.Add(GenerWorkerListAttr.FK_Node, this.HisNode.NodeID);
            ps.Add(GenerWorkerListAttr.FK_Emp, this.Execer);
            BP.DA.DBAccess.RunSQL(ps);

            #region ��鵱ǰ��״̬���Ƿ����˻�,������˻ص�״̬���͸�����ֵ.
            // ��鵱ǰ��״̬���Ƿ����˻أ�.
            if (this.SendNodeWFState == WFState.ReturnSta)
            {
                /*�����˻��Ƿ���ԭ·����?*/
                ps = new Paras();
                ps.SQL = "SELECT ReturnNode,Returner,IsBackTracking FROM WF_ReturnWork WHERE WorkID=" + dbStr + "WorkID AND IsBackTracking=1 ORDER BY RDT DESC";
                ps.Add(ReturnWorkAttr.WorkID, this.WorkID);
                DataTable dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count != 0)
                {
                    //�п��ܲ�ѯ�����������Ϊ��ʱ�������ˣ�ֻȡ�����һ���˻صģ������Ƿ����˻ز�ԭ·���ص���Ϣ��

                    /*ȷ������˻أ����˻ز�ԭ·���� ,  �������ʼ�����Ĺ�����Ա, �뽫Ҫ���͵Ľڵ�. */
                    this.JumpToNode = new Node(int.Parse(dt.Rows[0]["ReturnNode"].ToString()));
                    this.JumpToEmp = dt.Rows[0]["Returner"].ToString();
                    this.IsSkip = true; //���������Ϊtrue, ����ɾ��Ŀ������.
                    //  this.NodeSend_11(this.JumpToNode);
                }
            }
            #endregion.

            switch (this.HisNode.HisRunModel)
            {
                case RunModel.Ordinary: /* 1�� ��ͨ�ڵ����·��͵�*/
                    Node toND = this.NodeSend_GenerNextStepNode();
                    if (this.IsStopFlow)
                        return;
                    switch (toND.HisRunModel)
                    {
                        case RunModel.Ordinary:   /*1-1 ��ͨ��to��ͨ�ڵ� */
                            this.NodeSend_11(toND);
                            break;
                        case RunModel.FL:  /* 1-2 ��ͨ��to������ */
                            this.NodeSend_11(toND);
                            break;
                        case RunModel.HL:  /*1-3 ��ͨ��to������   */
                            this.NodeSend_11(toND);
                            // throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治�����Ӻ����ڵ�(" + toND.Name + ").");
                            break;
                        case RunModel.FHL: /*1-4 ��ͨ�ڵ�to�ֺ����� */
                            this.NodeSend_11(toND);
                            break;
                        // throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治�����ӷֺ����ڵ�(" + toND.Name + ").");
                        case RunModel.SubThread: /*1-5 ��ͨ��to���̵߳� */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治���������߳̽ڵ�(" + toND.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND.Name + ")");
                            break;
                    }
                    break;
                case RunModel.FL: /* 2: �����ڵ����·��͵�*/
                    Nodes toNDs = this.Func_GenerNextStepNodes();
                    if (toNDs.Count == 1)
                    {
                        Node toND2 = toNDs[0] as Node;
                        //����ϵͳ����.
                        this.addMsg(SendReturnMsgFlag.VarToNodeID, toND2.NodeID.ToString(), toND2.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarToNodeName, toND2.Name, toND2.Name, SendReturnMsgType.SystemMsg);

                        switch (toND2.HisRunModel)
                        {
                            case RunModel.Ordinary:    /*2.1 ������to��ͨ�ڵ� */
                                this.NodeSend_11(toND2); /* ����ͨ�ڵ㵽��ͨ�ڵ㴦��. */
                                break;
                            case RunModel.FL:  /*2.2 ������to������  */
                            //  throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND2.Name + ").");
                            case RunModel.HL:  /*2.3 ������to������,�ֺ�����. */
                            case RunModel.FHL:
                                this.NodeSend_11(toND2); /* ����ͨ�ڵ㵽��ͨ�ڵ㴦��. */
                                break;
                            // throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND2.Name + ").");
                            case RunModel.SubThread: /* 2.4 ������to���̵߳�   */
                                if (toND2.HisSubThreadType == SubThreadType.SameSheet)
                                    NodeSend_24_SameSheet(toND2);
                                else
                                    NodeSend_24_UnSameSheet(toNDs); /*������ֻ����1�����*/
                                break;
                            default:
                                throw new Exception("@û���жϵĽڵ�����(" + toND2.Name + ")");
                                break;
                        }
                    }
                    else
                    {
                        /* ����ж���ڵ㣬���һ�����Ǳض������߳̽ڵ���򣬾�����ƴ���*/
                        bool isHaveSameSheet = false;
                        bool isHaveUnSameSheet = false;
                        foreach (Node nd in toNDs)
                        {
                            switch (nd.HisRunModel)
                            {
                                case RunModel.Ordinary:
                                    NodeSend_11(nd); /*����ͨ�ڵ㵽��ͨ�ڵ㴦��.*/
                                    break;
                                case RunModel.FL:
                                case RunModel.FHL:
                                case RunModel.HL:
                                    NodeSend_11(nd); /*����ͨ�ڵ㵽��ͨ�ڵ㴦��.*/
                                    break;
                                //    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + nd.Name + ").");
                                //case RunModel.FHL:
                                //    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷֺ����ڵ�(" + nd.Name + ").");
                                //case RunModel.HL:
                                //    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + nd.Name + ").");
                                default:
                                    break;
                            }
                            if (nd.HisSubThreadType == SubThreadType.SameSheet)
                                isHaveSameSheet = true;

                            if (nd.HisSubThreadType == SubThreadType.UnSameSheet)
                                isHaveUnSameSheet = true;
                        }

                        if (isHaveUnSameSheet && isHaveSameSheet)
                            throw new Exception("@��֧������ģʽ: �����ڵ�ͬʱ������ͬ�������߳�����������߳�.");

                        if (isHaveSameSheet == true)
                            throw new Exception("@��֧������ģʽ: �����ڵ�ͬʱ�����˶��ͬ�������߳�.");

                        //�������������߳̽ڵ�.
                        this.NodeSend_24_UnSameSheet(toNDs);
                    }
                    break;
                case RunModel.HL:  /* 3: �����ڵ����·��� */
                    Node toND3 = this.NodeSend_GenerNextStepNode();
                    if (this.IsStopFlow)
                        return;

                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND3.NodeID.ToString(), toND3.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND3.Name, toND3.Name, SendReturnMsgType.SystemMsg);

                    switch (toND3.HisRunModel)
                    {
                        case RunModel.Ordinary: /*3.1 ��ͨ�����ڵ� */
                            this.NodeSend_31(toND3); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        case RunModel.FL: /*3.2 ������ */
                            this.NodeSend_31(toND3); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        //throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND3.Name + ").");
                        case RunModel.HL: /*3.3 ������ */
                        case RunModel.FHL:
                            this.NodeSend_31(toND3); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        //throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND3.Name + ").");
                        case RunModel.SubThread:/*3.4 ���߳�*/
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治���������߳̽ڵ�(" + toND3.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND3.Name + ")");
                    }
                    break;
                case RunModel.FHL:  /* 4: �����ڵ����·��͵� */
                    Node toND4 = this.NodeSend_GenerNextStepNode();
                    if (this.IsStopFlow)
                        return;

                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND4.NodeID.ToString(), toND4.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND4.Name, toND4.Name, SendReturnMsgType.SystemMsg);

                    switch (toND4.HisRunModel)
                    {
                        case RunModel.Ordinary: /*4.1 ��ͨ�����ڵ� */
                            this.NodeSend_11(toND4); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        case RunModel.FL: /*4.2 ������ */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND4.Name + ").");
                        case RunModel.HL: /*4.3 ������ */
                        case RunModel.FHL:
                            this.NodeSend_11(toND4); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        //  throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND4.Name + ").");
                        case RunModel.SubThread:/*4.5 ���߳�*/
                            if (toND4.HisSubThreadType == SubThreadType.SameSheet)
                                NodeSend_24_SameSheet(toND4);
                            //else
                            //    NodeSend_24_UnSameSheet(toNDs); /*������ֻ����1�����*/
                            break;
                        //throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治���������߳̽ڵ�(" + toND4.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND4.Name + ")");
                    }
                    break;
                // throw new Exception("@û���жϵ�����:" + this.HisNode.HisNodeWorkTypeT);
                case RunModel.SubThread:  /* 5: ���߳̽ڵ����·��͵� */
                    Node toND5 = this.NodeSend_GenerNextStepNode();
                    if (this.IsStopFlow)
                        return;

                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND5.NodeID.ToString(), toND5.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND5.Name, toND5.Name, SendReturnMsgType.SystemMsg);

                    switch (toND5.HisRunModel)
                    {
                        case RunModel.Ordinary: /*5.1 ��ͨ�����ڵ� */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ���̵߳�(" + this.HisNode.Name + ")���治��������ͨ�ڵ�(" + toND5.Name + ").");
                            break;
                        case RunModel.FL: /*5.2 ������ */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ���̵߳�(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND5.Name + ").");
                        case RunModel.HL: /*5.3 ������ */
                        case RunModel.FHL: /*5.4 �ֺ����� */
                            if (this.HisNode.HisSubThreadType == SubThreadType.SameSheet)
                                this.NodeSend_53_SameSheet_To_HeLiu(toND5);
                            else
                                this.NodeSend_53_UnSameSheet_To_HeLiu(toND5);

                            //�Ѻ���������δ��.
                            ps = new Paras();
                            ps.SQL = "UPDATE WF_GenerWorkerList SET IsRead=0 WHERE WorkID=" + SystemConfig.AppCenterDBVarStr + "WorkID AND  FK_Node=" + SystemConfig.AppCenterDBVarStr + "FK_Node";
                            ps.Add("WorkID", this.HisWork.FID);
                            ps.Add("FK_Node", toND5.NodeID);
                            BP.DA.DBAccess.RunSQL(ps);
                            break;
                        case RunModel.SubThread: /*5.5 ���߳�*/
                            if (toND5.HisSubThreadType == this.HisNode.HisSubThreadType)
                            {
                                #region ɾ������ڵ�����߳�����У���ֹ�˻���Ϣ������������,����˻ش�����������־Ͳ���Ҫ������.
                                ps = new Paras();
                                ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "FID  AND FK_Node=" + dbStr + "FK_Node";
                                ps.Add("FID", this.HisWork.FID);
                                ps.Add("FK_Node", toND5.NodeID);
                                #endregion ɾ������ڵ�����߳�����У���ֹ�˻���Ϣ�����������⣬����˻ش�����������־Ͳ���Ҫ������.

                                this.NodeSend_11(toND5); /*����ͨ�ڵ�һ��.*/
                            }
                            else
                                throw new Exception("@�������ģʽ���������������̵߳����߳�ģʽ��һ�����ӽڵ�(" + this.HisNode.Name + ")���ڵ�(" + toND5.Name + ")");
                            break;
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND5.Name + ")");
                    }
                    break;
                default:
                    throw new Exception("@û���жϵ�����:" + this.HisNode.HisNodeWorkTypeT);
            }
        }

        #region ִ������copy.
        public void CopyData(Work toWK, Node toND, bool isSamePTable)
        {
            string errMsg = "�����������Դ����ȣ���ִ��copy - �ڼ���ִ���.";

            #region ��������copy.
            if (isSamePTable == false)
            {
                toWK.SetValByKey("OID", this.HisWork.OID); //�趨����ID.
                if (this.HisNode.IsStartNode == false)
                    toWK.Copy(this.rptGe);

                toWK.Copy(this.HisWork); // ִ�� copy ��һ���ڵ�����ݡ�
                toWK.Rec = this.Execer;

                //Ҫ����FID������.
                if (this.HisNode.HisRunModel == RunModel.SubThread
                    && toND.HisRunModel == RunModel.SubThread)
                    toWK.FID = this.HisWork.FID;

                try
                {
                    //�ж��ǲ���MD5���̣�
                    if (this.HisFlow.IsMD5)
                        toWK.SetValByKey("MD5", Glo.GenerMD5(toWK));

                    if (toWK.IsExits)
                        toWK.Update();
                    else
                        toWK.Insert();
                }
                catch (Exception ex)
                {
                    toWK.CheckPhysicsTable();
                    try
                    {
                        toWK.Copy(this.HisWork); // ִ�� copy ��һ���ڵ�����ݡ�
                        toWK.Rec = this.Execer;
                        toWK.SaveAsOID(toWK.OID);
                    }
                    catch (Exception ex11)
                    {
                        if (toWK.Update() == 0)
                            throw new Exception(ex.Message + " == " + ex11.Message);
                    }
                }
            }
            #endregion ��������copy.

            #region ���Ƹ�����
            if (this.HisNode.MapData.FrmAttachments.Count > 0)
            {
                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                int idx = 0;
                if (athDBs.Count > 0)
                {
                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toND.NodeID,
                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + toND.NodeID;
                        athDB_N.RefPKVal = this.HisWork.OID.ToString();
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                          "ND" + toND.NodeID);

                        if (athDB_N.HisAttachmentUploadType == AttachmentUploadType.Single)
                        {
                            /*����ǵ�����.*/
                            athDB_N.MyPK = athDB_N.FK_FrmAttachment + "_" + this.HisWork.OID;
                            //if (athDB_N.IsExits == true)
                            //    continue; /*˵����һ���ڵ�������߳��Ѿ�copy����, ���ǻ������߳�������㴫�����ݵĿ��ܣ����Բ�����break.*/
                            try
                            {
                                athDB_N.Insert();
                            }
                            catch
                            {
                                athDB_N.MyPK = BP.DA.DBAccess.GenerGUID();
                                athDB_N.Insert();
                            }
                        }
                        else
                        {
                            ////�ж����guid ���ϴ��ļ��Ƿ��������߳�copy��ȥ�ˣ�
                            //if (athDB_N.IsExit(FrmAttachmentDBAttr.UploadGUID, athDB_N.UploadGUID,
                            //    FrmAttachmentDBAttr.FK_MapData, athDB_N.FK_MapData) == true)
                            //    continue; /*����ǾͲ�Ҫcopy��.*/

                            athDB_N.MyPK = athDB_N.UploadGUID + "_" + athDB_N.FK_MapData + "_" + toWK.OID;
                            try
                            {
                                athDB_N.Insert();
                            }
                            catch
                            {
                                athDB_N.MyPK = BP.DA.DBAccess.GenerGUID();
                                athDB_N.Insert();
                            }
                        }
                    }
                }
            }
            #endregion ���Ƹ�����

            #region ����ͼƬ�ϴ�������
            if (this.HisNode.MapData.FrmImgAths.Count > 0)
            {
                FrmImgAthDBs athDBs = new FrmImgAthDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                int idx = 0;
                if (athDBs.Count > 0)
                {
                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toND.NodeID,
                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmImgAthDB athDB in athDBs)
                    {
                        idx++;
                        FrmImgAthDB athDB_N = new FrmImgAthDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + toND.NodeID;
                        athDB_N.RefPKVal = this.WorkID.ToString();
                        athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                        athDB_N.FK_FrmImgAth = athDB_N.FK_FrmImgAth.Replace("ND" + this.HisNode.NodeID, "ND" + toND.NodeID);
                        athDB_N.Save();
                    }
                }
            }
            #endregion ����ͼƬ�ϴ�������

            #region ����Ele
            if (this.HisNode.MapData.FrmEles.Count > 0)
            {
                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (eleDBs.Count > 0)
                {
                    eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + toND.NodeID,
                        FrmEleDBAttr.RefPKVal, this.WorkID);

                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + toND.NodeID;
                        eleDB_N.GenerPKVal();
                        eleDB_N.Save();
                    }
                }
            }
            #endregion ����Ele

            #region ���ƶ�ѡ����
            if (this.HisNode.MapData.MapM2Ms.Count > 0)
            {
                M2Ms m2ms = new M2Ms("ND" + this.HisNode.NodeID, this.WorkID);
                if (m2ms.Count >= 1)
                {
                    foreach (M2M item in m2ms)
                    {
                        M2M m2 = new M2M();
                        m2.Copy(item);
                        m2.EnOID = this.WorkID;
                        m2.FK_MapData = m2.FK_MapData.Replace("ND" + this.HisNode.NodeID, "ND" + toND.NodeID);
                        m2.InitMyPK();
                        try
                        {
                            m2.DirectInsert();
                        }
                        catch
                        {
                            m2.DirectUpdate();
                        }
                    }
                }
            }
            #endregion

            #region ������ϸ����
            // int deBugDtlCount=
            Sys.MapDtls dtls = this.HisNode.MapData.MapDtls;
            string recDtlLog = "@��¼������ϸ��Copy����,�ӽڵ�ID:" + this.HisNode.NodeID + " WorkID:" + this.WorkID + ", ���ڵ�ID=" + toND.NodeID;
            if (dtls.Count > 0)
            {
                Sys.MapDtls toDtls = toND.MapData.MapDtls;
                recDtlLog += "@���ڵ���ϸ��������:" + dtls.Count + "��";

                Sys.MapDtls startDtls = null;
                bool isEnablePass = false; /*�Ƿ�����ϸ�������.*/
                foreach (MapDtl dtl in dtls)
                {
                    if (dtl.IsEnablePass)
                        isEnablePass = true;
                }

                if (isEnablePass) /* ����оͽ�������ʼ�ڵ������ */
                    startDtls = new BP.Sys.MapDtls("ND" + int.Parse(toND.FK_Flow) + "01");

                recDtlLog += "@����ѭ����ʼִ�������ϸ��copy.";
                int i = -1;

                foreach (Sys.MapDtl dtl in dtls)
                {
                    recDtlLog += "@����ѭ����ʼִ����ϸ��(" + dtl.No + ")copy.";

                    //i++;
                    //if (toDtls.Count <= i)
                    //    continue;
                    //Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];

                    i++;
                    //if (toDtls.Count <= i)
                    //    continue;
                    Sys.MapDtl toDtl = null;
                    foreach (MapDtl todtl in toDtls)
                    {
                        string toDtlName = "";
                        string dtlName = "";
                        try
                        {

                            toDtlName = todtl.HisGEDtl.FK_MapDtl.Substring(todtl.HisGEDtl.FK_MapDtl.IndexOf("Dtl"), todtl.HisGEDtl.FK_MapDtl.Length - todtl.HisGEDtl.FK_MapDtl.IndexOf("Dtl"));
                            dtlName = dtl.HisGEDtl.FK_MapDtl.Substring(dtl.HisGEDtl.FK_MapDtl.IndexOf("Dtl"), dtl.HisGEDtl.FK_MapDtl.Length - dtl.HisGEDtl.FK_MapDtl.IndexOf("Dtl"));
                        }
                        catch
                        {
                            continue;
                        }

                        if (toDtlName == dtlName)
                        {
                            toDtl = todtl;
                            break;
                        }
                    }

                    if (dtl.IsEnablePass == true)
                    {
                        /*����������Ƿ���ϸ������ͨ������,������copy�ڵ����ݡ�*/
                        toDtl.IsCopyNDData = true;
                    }

                    if (toDtl == null || toDtl.IsCopyNDData == false)
                        continue;

                    if (dtl.PTable == toDtl.PTable)
                        continue;


                    //��ȡ��ϸ���ݡ�
                    GEDtls gedtls = new GEDtls(dtl.No);
                    QueryObject qo = null;
                    qo = new QueryObject(gedtls);
                    switch (dtl.DtlOpenType)
                    {
                        case DtlOpenType.ForEmp:
                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                            break;
                        case DtlOpenType.ForWorkID:
                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                            break;
                        case DtlOpenType.ForFID:
                            qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                            break;
                    }
                    qo.DoQuery();

                    recDtlLog += "@��ѯ��������ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";

                    int unPass = 0;
                    // �Ƿ�������˻��ơ�
                    isEnablePass = dtl.IsEnablePass;
                    if (isEnablePass && this.HisNode.IsStartNode == false)
                        isEnablePass = true;
                    else
                        isEnablePass = false;

                    if (isEnablePass == true)
                    {
                        /*�жϵ�ǰ�ڵ����ϸ�����Ƿ��У�isPass ����ֶΣ����û���׳��쳣��Ϣ��*/
                        if (gedtls.Count != 0)
                        {
                            GEDtl dtl1 = gedtls[0] as GEDtl;
                            if (dtl1.EnMap.Attrs.Contains("IsPass") == false)
                                isEnablePass = false;
                        }
                    }

                    recDtlLog += "@ɾ��������ϸ��:" + dtl.No + ",����, ����ʼ������ϸ��,ִ��һ���е�copy.";
                    DBAccess.RunSQL("DELETE FROM " + toDtl.PTable + " WHERE RefPK=" + dbStr + "RefPK", "RefPK", this.WorkID.ToString());

                    // copy����.
                    int deBugNumCopy = 0;
                    foreach (GEDtl gedtl in gedtls)
                    {
                        if (isEnablePass)
                        {
                            if (gedtl.GetValBooleanByKey("IsPass") == false)
                            {
                                /*û�����ͨ���ľ� continue ���ǣ��������Ѿ�����ͨ����.*/
                                continue;
                            }
                        }

                        BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                        dtCopy.Copy(gedtl);
                        dtCopy.FK_MapDtl = toDtl.No;
                        dtCopy.RefPK = this.WorkID.ToString();
                        dtCopy.InsertAsOID(dtCopy.OID);
                        dtCopy.RefPKInt64 = this.WorkID;
                        deBugNumCopy++;

                        #region  ������ϸ���� - ������Ϣ
                        if (toDtl.IsEnableAthM)
                        {
                            /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                            FrmAttachmentDBs athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                            if (athDBs.Count > 0)
                            {
                                i = 0;
                                foreach (FrmAttachmentDB athDB in athDBs)
                                {
                                    i++;
                                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                    athDB_N.Copy(athDB);
                                    athDB_N.FK_MapData = toDtl.No;
                                    athDB_N.MyPK = athDB.MyPK + "_" + dtCopy.OID + "_" + i.ToString();
                                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                        "ND" + toND.NodeID);
                                    athDB_N.RefPKVal = dtCopy.OID.ToString();
                                    try
                                    {
                                        athDB_N.DirectInsert();
                                    }
                                    catch
                                    {
                                        athDB_N.DirectUpdate();
                                    }

                                }
                            }
                        }
                        if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                        {
                            /*���������m2m */
                            M2Ms m2ms = new M2Ms(dtl.No, gedtl.OID);
                            if (m2ms.Count > 0)
                            {
                                i = 0;
                                foreach (M2M m2m in m2ms)
                                {
                                    i++;
                                    M2M m2m_N = new M2M();
                                    m2m_N.Copy(m2m);
                                    m2m_N.FK_MapData = toDtl.No;
                                    m2m_N.MyPK = m2m.MyPK + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                    m2m_N.EnOID = gedtl.OID;
                                    m2m.InitMyPK();
                                    try
                                    {
                                        m2m_N.DirectInsert();
                                    }
                                    catch
                                    {
                                        m2m_N.DirectUpdate();
                                    }
                                }
                            }
                        }
                        #endregion  ������ϸ���� - ������Ϣ

                    }
#warning ��¼��־.
                    if (gedtls.Count != deBugNumCopy)
                    {
                        recDtlLog += "@����ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";
                        //��¼��־.
                        Log.DefaultLogWriteLineInfo(recDtlLog);
                        throw new Exception("@ϵͳ���ִ����뽫������Ϣ����������Ա,лл��: ������Ϣ:" + recDtlLog);
                    }

                    #region �����������˻���
                    if (isEnablePass)
                    {
                        /* ������������ͨ�����ƣ��Ͱ�δ��˵�����copy����һ���ڵ���ȥ 
                         * 1, �ҵ���Ӧ����ϸ��.
                         * 2, ��δ���ͨ�������ݸ��Ƶ���ʼ��ϸ����.
                         */
                        string fk_mapdata = "ND" + int.Parse(toND.FK_Flow) + "01";
                        MapData md = new MapData(fk_mapdata);
                        string startUser = "SELECT Rec FROM " + md.PTable + " WHERE OID=" + this.WorkID;
                        startUser = DBAccess.RunSQLReturnString(startUser);

                        MapDtl startDtl = (MapDtl)startDtls[i];
                        foreach (GEDtl gedtl in gedtls)
                        {
                            if (gedtl.GetValBooleanByKey("IsPass"))
                                continue; /* �ų����ͨ���� */

                            BP.Sys.GEDtl dtCopy = new GEDtl(startDtl.No);
                            dtCopy.Copy(gedtl);
                            dtCopy.OID = 0;
                            dtCopy.FK_MapDtl = startDtl.No;
                            dtCopy.RefPK = gedtl.OID.ToString(); //this.WorkID.ToString();
                            dtCopy.SetValByKey("BatchID", this.WorkID);
                            dtCopy.SetValByKey("IsPass", 0);
                            dtCopy.SetValByKey("Rec", startUser);
                            dtCopy.SetValByKey("Checker", this.ExecerName);
                            dtCopy.RefPKInt64 = this.WorkID;
                            dtCopy.SaveAsOID(gedtl.OID);
                        }
                        DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + this.Execer + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + this.Execer + "'");
                    }
                    #endregion �����������˻���
                }
            }
            #endregion ������ϸ����
        }
        #endregion

        #region ���ض�����.
        private SendReturnObjs HisMsgObjs = null;
        public void addMsg(string flag, string msg)
        {
            addMsg(flag, msg, null, SendReturnMsgType.Info);
        }
        public void addMsg(string flag, string msg, SendReturnMsgType msgType)
        {
            addMsg(flag, msg, null, msgType);
        }
        public void addMsg(string flag, string msg, string msgofHtml, SendReturnMsgType msgType)
        {
            if (HisMsgObjs == null)
                HisMsgObjs = new SendReturnObjs();
            this.HisMsgObjs.AddMsg(flag, msg, msgofHtml, msgType);
        }
        public void addMsg(string flag, string msg, string msgofHtml)
        {
            addMsg(flag, msg, msgofHtml, SendReturnMsgType.Info);
        }
        #endregion ���ض�����.

        #region ����
        /// <summary>
        /// ����ʧ���ǳ������ݡ�
        /// </summary>
        public void DealEvalUn()
        {
            //���ݷ��͡�
            BP.WF.Data.Eval eval = new Eval();
            if (this.HisNode.IsFLHL == false)
            {
                eval.MyPK = this.WorkID + "_" + this.HisNode.NodeID;
                eval.Delete();
            }

            // �ֺ����������������ϸ��������������ۡ�
            MapDtls dtls = this.HisNode.MapData.MapDtls;
            foreach (MapDtl dtl in dtls)
            {
                if (dtl.IsHLDtl == false)
                    continue;

                //��ȡ��ϸ���ݡ�
                GEDtls gedtls = new GEDtls(dtl.No);
                QueryObject qo = null;
                qo = new QueryObject(gedtls);
                switch (dtl.DtlOpenType)
                {
                    case DtlOpenType.ForEmp:
                        qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                        break;
                    case DtlOpenType.ForWorkID:
                        qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                        break;
                    case DtlOpenType.ForFID:
                        qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                        break;
                }
                qo.DoQuery();

                foreach (GEDtl gedtl in gedtls)
                {
                    eval = new Eval();
                    eval.MyPK = gedtl.OID + "_" + gedtl.Rec;
                    eval.Delete();
                }
            }
        }
        /// <summary>
        /// ������������
        /// </summary>
        public void DealEval()
        {
            if (this.HisNode.IsEval == false)
                return;

            BP.WF.Data.Eval eval = new Eval();
            eval.CheckPhysicsTable();

            if (this.HisNode.IsFLHL == false)
            {
                eval.MyPK = this.WorkID + "_" + this.HisNode.NodeID;
                eval.Delete();

                eval.Title = this.HisGenerWorkFlow.Title;

                eval.WorkID = this.WorkID;
                eval.FK_Node = this.HisNode.NodeID;
                eval.NodeName = this.HisNode.Name;

                eval.FK_Flow = this.HisNode.FK_Flow;
                eval.FlowName = this.HisNode.FlowName;

                eval.FK_Dept = this.ExecerDeptNo;
                eval.DeptName = this.ExecerDeptName;

                eval.Rec = this.Execer;
                eval.RecName = this.ExecerName;

                eval.RDT = DataType.CurrentDataTime;
                eval.FK_NY = DataType.CurrentYearMonth;

                eval.EvalEmpNo = this.HisWork.GetValStringByKey(WorkSysFieldAttr.EvalEmpNo);
                eval.EvalEmpName = this.HisWork.GetValStringByKey(WorkSysFieldAttr.EvalEmpName);
                eval.EvalCent = this.HisWork.GetValStringByKey(WorkSysFieldAttr.EvalCent);
                eval.EvalNote = this.HisWork.GetValStringByKey(WorkSysFieldAttr.EvalNote);

                eval.Insert();
                return;
            }

            // �ֺ����������������ϸ��������������ۡ�
            Sys.MapDtls dtls = this.HisNode.MapData.MapDtls;
            foreach (MapDtl dtl in dtls)
            {
                if (dtl.IsHLDtl == false)
                    continue;

                //��ȡ��ϸ���ݡ�
                GEDtls gedtls = new GEDtls(dtl.No);
                QueryObject qo = null;
                qo = new QueryObject(gedtls);
                switch (dtl.DtlOpenType)
                {
                    case DtlOpenType.ForEmp:
                        qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                        break;
                    case DtlOpenType.ForWorkID:
                        qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                        break;
                    case DtlOpenType.ForFID:
                        qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                        break;
                }
                qo.DoQuery();

                foreach (GEDtl gedtl in gedtls)
                {
                    eval = new Eval();
                    eval.MyPK = gedtl.OID + "_" + gedtl.Rec;
                    eval.Delete();

                    eval.Title = this.HisGenerWorkFlow.Title;

                    eval.WorkID = this.WorkID;
                    eval.FK_Node = this.HisNode.NodeID;
                    eval.NodeName = this.HisNode.Name;

                    eval.FK_Flow = this.HisNode.FK_Flow;
                    eval.FlowName = this.HisNode.FlowName;

                    eval.FK_Dept = this.ExecerDeptNo;
                    eval.DeptName = this.ExecerDeptName;

                    eval.Rec = this.Execer;
                    eval.RecName = this.ExecerName;

                    eval.RDT = DataType.CurrentDataTime;
                    eval.FK_NY = DataType.CurrentYearMonth;

                    eval.EvalEmpNo = gedtl.GetValStringByKey(WorkSysFieldAttr.EvalEmpNo);
                    eval.EvalEmpName = gedtl.GetValStringByKey(WorkSysFieldAttr.EvalEmpName);
                    eval.EvalCent = gedtl.GetValStringByKey(WorkSysFieldAttr.EvalCent);
                    eval.EvalNote = gedtl.GetValStringByKey(WorkSysFieldAttr.EvalNote);
                    eval.Insert();
                }
            }
        }
        private void CallSubFlow()
        {
            // ��ȡ������Ϣ.
            string[] paras = this.HisNode.SubFlowStartParas.Split('@');
            foreach (string item in paras)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                string[] keyvals = item.Split(';');

                string FlowNo = ""; //���̱��
                string EmpField = ""; // ��Ա�ֶ�.
                string DtlTable = ""; //��ϸ��.
                foreach (string keyval in keyvals)
                {
                    if (string.IsNullOrEmpty(keyval))
                        continue;

                    string[] strs = keyval.Split('=');
                    switch (strs[0])
                    {
                        case "FlowNo":
                            FlowNo = strs[1];
                            break;
                        case "EmpField":
                            EmpField = strs[1];
                            break;
                        case "DtlTable":
                            DtlTable = strs[1];
                            break;
                        default:
                            throw new Exception("@������ƴ���,��ȡ�����������õķ������ʱ��δָ���ı��: " + strs[0]);
                    }

                    if (this.HisNode.SubFlowStartWay == SubFlowStartWay.BySheetField)
                    {
                        string emps = this.HisWork.GetValStringByKey(EmpField) + ",";
                        string[] empStrs = emps.Split(',');

                        string currUser = this.Execer;
                        Emps empEns = new Emps();
                        string msgInfo = "";
                        foreach (string emp in empStrs)
                        {
                            if (string.IsNullOrEmpty(emp))
                                continue;

                            //�Ե�ǰ��Ա����ݵ�¼.
                            Emp empEn = new Emp(emp);
                            BP.Web.WebUser.SignInOfGener(empEn);

                            // �����ݸ��Ƹ���.
                            Flow fl = new Flow(FlowNo);
                            Work sw = fl.NewWork();

                            Int64 workID = sw.OID;
                            sw.Copy(this.HisWork);
                            sw.OID = workID;
                            sw.Update();

                            WorkNode wn = new WorkNode(sw, new Node(int.Parse(FlowNo + "01")));
                            wn.NodeSend(null, this.Execer);
                            msgInfo += BP.WF.Dev2Interface.Node_StartWork(FlowNo, null, null, 0, emp, this.WorkID, FlowNo);
                        }
                    }

                }
            }


            //BP.WF.Dev2Interface.Flow_NewStartWork(
            DataTable dt;

        }
        #endregion

        #region ��ع���.
        /// <summary>
        /// ִ����Ϣ������ع���
        /// </summary>
        public void DoRefFunc_Listens()
        {
            Listens lts = new Listens();
            lts.RetrieveByLike(ListenAttr.Nodes, "%" + this.HisNode.NodeID + "%");
            string info = "";
            foreach (Listen lt in lts)
            {
                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE IsEnable=1 AND IsPass=1 AND FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", lt.FK_Node);
                ps.Add("WorkID", this.WorkID);
                DataTable dtRem = BP.DA.DBAccess.RunSQLReturnTable(ps);
                foreach (DataRow dr in dtRem.Rows)
                {
                    string FK_Emp = dr["FK_Emp"] as string;

                    string title = lt.Title.Clone() as string;
                    title = title.Replace("@WebUser.No", this.Execer);
                    title = title.Replace("@WebUser.Name", this.ExecerName);
                    title = title.Replace("@WebUser.FK_Dept", this.ExecerDeptNo);
                    title = title.Replace("@WebUser.FK_DeptName", this.ExecerDeptName);

                    string doc = lt.Doc.Clone() as string;
                    doc = doc.Replace("@WebUser.No", this.Execer);
                    doc = doc.Replace("@WebUser.Name", this.ExecerName);
                    doc = doc.Replace("@WebUser.FK_Dept", this.ExecerDeptNo);
                    doc = doc.Replace("@WebUser.FK_DeptName", this.ExecerDeptName);

                    Attrs attrs = this.rptGe.EnMap.Attrs;
                    foreach (Attr attr in attrs)
                    {
                        title = title.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                        doc = doc.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                    }

                    if (this.town == null)
                        BP.WF.Dev2Interface.Port_SendMsg(FK_Emp, title, doc,
                            "LS" + FK_Emp + "_" + this.WorkID, BP.WF.SMSMsgType.Self,
                            this.HisFlow.No, this.HisNode.NodeID, this.WorkID, 0);
                    else
                        BP.WF.Dev2Interface.Port_SendMsg(FK_Emp, title, doc,
                            "LS" + FK_Emp + "_" + this.WorkID, BP.WF.SMSMsgType.Self,
                        this.HisFlow.No, this.town.HisNode.NodeID, this.WorkID, 0);

                    info += dr[GenerWorkerListAttr.FK_EmpText].ToString() + "��";
                }
            }

            if (string.IsNullOrEmpty(info) == false)
            {
                //this.addMsg(SendReturnMsgFlag.End, "@�����Ѿ��ߵ����һ���ڵ㣬���̳ɹ�������");
                this.addMsg(SendReturnMsgFlag.ListenInfo, "@��ǰִ���Ѿ�֪ͨ��:" + info);
            }
        }
        #endregion ��ع���.

        /// <summary>
        /// ����������ҵ����
        /// </summary>
        public SendReturnObjs NodeSend()
        {
            return NodeSend(null, null);
        }
        /// <summary>
        /// ��������д����Ŀ.
        /// </summary>
        /// <returns></returns>
        public bool CheckFrmIsNotNull()
        {
            if (this.HisNode.HisFormType != NodeFormType.SheetTree)
                return true;

            //��ѯ�������е����á�
            FrmFields ffs = new FrmFields();

            QueryObject qo = new QueryObject(ffs);
            qo.AddWhere(FrmFieldAttr.FK_Node, this.HisNode.NodeID);
            qo.addAnd();
            qo.addLeftBracket();
            qo.AddWhere(FrmFieldAttr.IsNotNull, 1);
            qo.addOr();
            qo.AddWhere(FrmFieldAttr.IsWriteToFlowTable, 1);
            qo.addRightBracket();
            qo.DoQuery();

            if (ffs.Count == 0)
                return true;


            BP.WF.Template.FrmNodes frmNDs = new FrmNodes(this.HisNode.FK_Flow, this.HisNode.NodeID);
            string err = "";
            foreach (FrmNode item in frmNDs)
            {
                MapData md = new MapData(item.FK_Frm);

                //������url.
                if (md.HisFrmType == FrmType.Url)
                    continue;

                //����Ƿ��У�
                bool isHave = false;
                foreach (FrmField myff in ffs)
                {
                    if (myff.FK_MapData != item.FK_Frm)
                        continue;
                    isHave = true;
                    break;
                }
                if (isHave == false)
                    continue;

                // ��������.
                long pk = 0;// this.WorkID;

                switch (item.WhoIsPK)
                {
                    case WhoIsPK.FID:
                        pk = this.HisWork.FID;
                        break;
                    case WhoIsPK.OID:
                        pk = this.HisWork.OID;
                        break;
                    case WhoIsPK.PWorkID:
                        pk = this.rptGe.PWorkID;
                        break;
                    case WhoIsPK.CWorkID:
                        pk = this.HisGenerWorkFlow.CWorkID;
                        break;
                    default:
                        throw new Exception("@δ�жϵ�����.");
                }

                if (pk == 0)
                    throw new Exception("@δ�ܻ�ȡ������.");



                //��ȡ��ֵ
                ps = new Paras();
                ps.SQL = "SELECT * FROM " + md.PTable + " WHERE OID=" + ps.DBStr + "OID";
                ps.Add(WorkAttr.OID, pk);
                DataTable dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                {
                    err += "@��{" + md.Name + "}û���������ݡ�";
                    continue;
                }


                // ��������Ƿ�����.
                foreach (FrmField ff in ffs)
                {
                    if (ff.FK_MapData != item.FK_Frm)
                        continue;

                    //�������.
                    string val = string.Empty;
                    val = dt.Rows[0][ff.KeyOfEn].ToString();

                    if (ff.IsNotNull == true && Glo.IsEnableCheckFrmTreeIsNull == true)
                    {
                        /*����Ǽ�鲻��Ϊ�� */
                        if (string.IsNullOrEmpty(val) == true || val.Trim() == "")
                            err += "@��{" + md.Name + "}�ֶ�{" + ff.KeyOfEn + " ; " + ff.Name + "}������Ϊ�ա�";
                    }

                    //�ж��Ƿ���Ҫд���������ݱ�.
                    if (ff.IsWriteToFlowTable == true)
                    {
                        this.HisWork.SetValByKey(ff.KeyOfEn, val);
                        //this.rptGe.SetValByKey(ff.KeyOfEn, val);
                    }
                }
            }
            if (err != "")
                throw new Exception("���ύǰ��鵽���±����ֶ���д������:" + err);

            return true;
        }
        /// <summary>
        /// copy����������
        /// </summary>
        /// <returns></returns>
        public Work CopySheetTree()
        {
            if (this.HisNode.HisFormType != NodeFormType.SheetTree)
                return null;

            //��ѯ�������е����á�
            FrmFields ffs = new FrmFields();

            QueryObject qo = new QueryObject(ffs);
            qo.AddWhere(FrmFieldAttr.FK_Node, this.HisNode.NodeID);
            qo.DoQuery();


            BP.WF.Template.FrmNodes frmNDs = new FrmNodes(this.HisNode.FK_Flow, this.HisNode.NodeID);
            string err = "";
            foreach (FrmNode item in frmNDs)
            {
                MapData md = new MapData(item.FK_Frm);

                //������url.
                if (md.HisFrmType == FrmType.Url)
                    continue;

                //����Ƿ��У�
                bool isHave = false;
                foreach (FrmField myff in ffs)
                {
                    if (myff.FK_MapData != item.FK_Frm)
                        continue;
                    isHave = true;
                    break;
                }
                if (isHave == false)
                    continue;

                // ��������.
                long pk = 0;// this.WorkID;

                switch (item.WhoIsPK)
                {
                    case WhoIsPK.FID:
                        pk = this.HisWork.FID;
                        break;
                    case WhoIsPK.OID:
                        pk = this.HisWork.OID;
                        break;
                    case WhoIsPK.PWorkID:
                        pk = this.rptGe.PWorkID;
                        break;
                    case WhoIsPK.CWorkID:
                        pk = this.HisGenerWorkFlow.CWorkID;
                        break;
                    default:
                        throw new Exception("@δ�жϵ�����.");
                }

                if (pk == 0)
                    throw new Exception("@δ�ܻ�ȡ������.");

                //��ȡ��ֵ
                ps = new Paras();
                ps.SQL = "SELECT * FROM " + md.PTable + " WHERE OID=" + ps.DBStr + "OID";
                ps.Add(WorkAttr.OID, pk);
                DataTable dt = DBAccess.RunSQLReturnTable(ps);

                // ��������Ƿ�����.
                foreach (FrmField ff in ffs)
                {
                    if (ff.FK_MapData != item.FK_Frm)
                        continue;

                    //�������.
                    string val = string.Empty;
                    val = dt.Rows[0][ff.KeyOfEn].ToString();

                    //�ж��Ƿ���Ҫд���������ݱ�.
                    //if (ff.IsWriteToFlowTable == true)
                    //{
                    this.HisWork.SetValByKey(ff.KeyOfEn, val);
                    //this.rptGe.SetValByKey(ff.KeyOfEn, val);
                    //  }
                }
            }

            return this.HisWork;
        }
        /// <summary>
        /// ִ�г���
        /// </summary>
        public void DoCC()
        {
        }
        /// <summary>
        /// �����Э��.
        /// </summary>
        /// <returns></returns>
        public bool DealTeamUpNode()
        {
            GenerWorkerLists gwls = new GenerWorkerLists();
            gwls.Retrieve(GenerWorkerListAttr.WorkID, this.WorkID,
                GenerWorkerListAttr.FK_Node, this.HisNode.NodeID, GenerWorkerListAttr.IsPass);

            if (gwls.Count == 1)
                return false; /*��������ִ��,��Ϊֻ��һ���ˡ���û��˳�������.*/

            //�鿴�Ƿ��������һ����
            int num = 0;
            string todoEmps = ""; //��¼û�д������.
            foreach (GenerWorkerList item in gwls)
            {
                if (item.IsPassInt == 0)
                {
                    if (item.FK_Emp != WebUser.No)
                        todoEmps += BP.WF.Glo.DealUserInfoShowModel(item.FK_Emp, item.FK_EmpText) + " ";
                    num++;
                }
            }
            if (num == 1)
                return false; /*ֻ��һ������,˵���Լ���������һ����.*/

            //�ѵ�ǰ�Ĵ��������Ѱ죬������ʾδ������ˡ�
            foreach (GenerWorkerList gwl in gwls)
            {

                if (gwl.FK_Emp != WebUser.No)
                    continue;

                //���õ�ǰ��������.
                gwl.IsPassInt = 1;
                gwl.Update();

                //д����־.
                this.AddToTrack(ActionType.TeampUp, gwl.FK_Emp, todoEmps, this.HisNode.NodeID, this.HisNode.Name, "Э������");
                this.addMsg(SendReturnMsgFlag.OverCurr, "��ǰ����δ���������: " + todoEmps + " .", null, SendReturnMsgType.Info);
                return true;
            }

            throw new Exception("@��Ӧ�����е����");
        }
        /// <summary>
        /// ������нڵ�
        /// </summary>
        /// <returns>�Ƿ�������·���?</returns>
        public bool DealOradeNode()
        {
            GenerWorkerLists gwls = new GenerWorkerLists();
            gwls.Retrieve(GenerWorkerListAttr.WorkID, this.WorkID,
                GenerWorkerListAttr.FK_Node, this.HisNode.NodeID, GenerWorkerListAttr.IsPass);

            if (gwls.Count == 1)
                return false; /*��������ִ��,��Ϊֻ��һ���ˡ���û��˳�������.*/

            int idx = -100;
            foreach (GenerWorkerList gwl in gwls)
            {
                idx++;
                if (gwl.FK_Emp != WebUser.No)
                    continue;

                //���õ�ǰ��������.
                gwl.IsPassInt = idx;
                gwl.Update();
            }

            foreach (GenerWorkerList gwl in gwls)
            {
                if (gwl.IsPassInt > 10)
                {
                    /*�Ϳ�ʼ�������������. */
                    gwl.IsPassInt = 0;
                    gwl.Update();

                    //д����־.
                    this.AddToTrack(ActionType.Order, gwl.FK_Emp, gwl.FK_EmpText, this.HisNode.NodeID,
                        this.HisNode.Name, "���з���");

                    this.addMsg(SendReturnMsgFlag.VarAcceptersID, gwl.FK_Emp, gwl.FK_Emp, SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarAcceptersName, gwl.FK_EmpText, gwl.FK_EmpText, SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.OverCurr, "��ǰ�����Ѿ����͸�(" + gwl.FK_Emp + "," + gwl.FK_EmpText + ").", null, SendReturnMsgType.Info);

                    //ִ�и���.
                    if (this.HisGenerWorkFlow.Emps.Contains("@" + WebUser.No + "@") == false)
                        this.HisGenerWorkFlow.Emps = this.HisGenerWorkFlow.Emps + WebUser.No + "@";

                    this.rptGe.FlowEmps = this.HisGenerWorkFlow.Emps;
                    this.rptGe.WFState = WFState.Runing;

                    this.rptGe.Update(GERptAttr.FlowEmps, this.rptGe.FlowEmps, GERptAttr.WFState, (int)WFState.Runing);


                    this.HisGenerWorkFlow.WFState = WFState.Runing;
                    this.HisGenerWorkFlow.Update();
                    return true;
                }
            }

            // ��������һ������Ҫ�����·��͡�
            return false;
        }
        /// <summary>
        /// �������ģʽ
        /// </summary>
        /// <returns>�����Ƿ�������·���.</returns>
        private bool CheckBlockModel()
        {
            if (this.HisNode.BlockModel == BlockModel.None)
                return true;

            if (this.HisNode.BlockModel == BlockModel.CurrNodeAll)
            {
                /*������ü���Ƿ������̽���.*/
                GenerWorkFlows gwls = new GenerWorkFlows();
                if (this.HisNode.HisRunModel == RunModel.SubThread)
                {
                    /*��������߳�,��������Լ����߳��Ϸ����workid.*/
                    QueryObject qo = new QueryObject(gwls);
                    qo.AddWhere(GenerWorkFlowAttr.PWorkID, this.WorkID);
                    qo.addAnd();
                    qo.AddWhere(GenerWorkFlowAttr.PNodeID, this.HisNode.NodeID);
                    qo.addAnd();
                    qo.AddWhere(GenerWorkFlowAttr.PFlowNo, this.HisFlow.No);
                    qo.addAnd();
                    qo.AddWhere(GenerWorkFlowAttr.WFSta, (int)WFSta.Runing);
                    qo.DoQuery();
                    if (gwls.Count == 0)
                        return true;
                }
                else
                {
                    /*��飬��ǰ�����߳��Ƿ�������� ����ǰ�ķ����߳ǽڵ��Ƿ���������̡� */
                    QueryObject qo = new QueryObject(gwls);

                    qo.addLeftBracket();
                    qo.AddWhere(GenerWorkFlowAttr.PFID, this.WorkID);
                    qo.addOr();
                    qo.AddWhere(GenerWorkFlowAttr.PWorkID, this.WorkID);
                    qo.addRightBracket();

                    qo.addAnd();

                    qo.addLeftBracket();
                    qo.AddWhere(GenerWorkFlowAttr.PNodeID, this.HisNode.NodeID);
                    qo.addAnd();
                    qo.AddWhere(GenerWorkFlowAttr.PFlowNo, this.HisFlow.No);
                    qo.addAnd();
                    qo.AddWhere(GenerWorkFlowAttr.WFSta, (int)WFSta.Runing);
                    qo.addRightBracket();

                    qo.DoQuery();
                    if (gwls.Count == 0)
                        return true;
                }

                string err = "";
                err += "@����������û����ɣ��㲻�����·��͡�@---------------------------------";
                foreach (GenerWorkFlow gwf in gwls)
                    err += "@����ID=" + gwf.WorkID + ",����:" + gwf.Title + ",��ǰִ����:" + gwf.TodoEmps + ",���е��ڵ�:" + gwf.NodeName;

                if (string.IsNullOrEmpty(err) == true)
                    return true;

                err = Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null) + err;
                throw new Exception(err);
            }

            if (this.HisNode.BlockModel == BlockModel.SpecSubFlow)
            {
                /*��������ض��ĸ�ʽ�ж�����*/
                string exp = this.HisNode.BlockExp;
                if (exp.Contains("@") == false)
                    throw new Exception("@���ô��󣬸ýڵ���������ø�ʽ������ο������������");

                string[] strs = exp.Split('@');
                string err = "";
                foreach (string str in strs)
                {
                    if (string.IsNullOrEmpty(str) == true)
                        continue;

                    if (str.Contains("=") == false)
                        throw new Exception("@�������õĸ�ʽ����ȷ��" + str);

                    string[] nodeFlow = str.Split('=');
                    int nodeid = int.Parse(nodeFlow[0]); //���������̵Ľڵ�.
                    string subFlowNo = nodeFlow[1];

                    GenerWorkFlows gwls = new GenerWorkFlows();

                    if (this.HisNode.HisRunModel == RunModel.SubThread)
                    {
                        /* ��������߳ǣ��Ͳ���Ҫ�ܣ����ɽڵ�����⡣*/
                        QueryObject qo = new QueryObject(gwls);
                        qo.AddWhere(GenerWorkFlowAttr.PWorkID, this.WorkID);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.PNodeID, nodeid);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.PFlowNo, this.HisFlow.No);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.FK_Flow, subFlowNo);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.WFSta, (int)WFSta.Runing);

                        qo.DoQuery();
                        if (gwls.Count == 0)
                            continue;
                    }
                    else
                    {
                        /* �����߳ǣ�����Ҫ���ǣ��Ӹýڵ��ϣ���������̵߳� �����ɽڵ�����⡣*/
                        QueryObject qo = new QueryObject(gwls);

                        qo.addLeftBracket();
                        qo.AddWhere(GenerWorkFlowAttr.PFID, this.WorkID);
                        qo.addOr();
                        qo.AddWhere(GenerWorkFlowAttr.PWorkID, this.WorkID);
                        qo.addRightBracket();

                        qo.addAnd();

                        qo.addLeftBracket();
                        qo.AddWhere(GenerWorkFlowAttr.PNodeID, nodeid);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.PFlowNo, this.HisFlow.No);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.WFSta, (int)WFSta.Runing);
                        qo.addAnd();
                        qo.AddWhere(GenerWorkFlowAttr.FK_Flow, subFlowNo);
                        qo.addRightBracket();

                        qo.DoQuery();
                        if (gwls.Count == 0)
                            continue;

                    }

                    err += "@����������û����ɣ��㲻�����·��͡�@---------------------------------";
                    foreach (GenerWorkFlow gwf in gwls)
                        err += "@������ID=" + gwf.WorkID + ",����������" + gwf.FlowName + ",����:" + gwf.Title + ",��ǰִ����:" + gwf.TodoEmps + ",���е��ڵ�:" + gwf.NodeName;
                }

                if (string.IsNullOrEmpty(err) == true)
                    return true;

                err = Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null) + err;
                throw new Exception(err);
            }

            if (this.HisNode.BlockModel == BlockModel.BySQL)
            {
                /*�� sql �ж�����*/
                decimal d = DBAccess.RunSQLReturnValDecimal(Glo.DealExp(this.HisNode.BlockExp, this.rptGe, null), 0, 1);
                if (d >= 0)
                    throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));
                return true;
            }

            if (this.HisNode.BlockModel == BlockModel.ByExp)
            {
                /*�����ʽ����. ��ʽΪ: @ABC=123 */
                //this.MsgOfCond = "@�Ա�ֵ�жϷ���ֵ " + en.EnDesc + "." + this.AttrKey + " (" + en.GetValStringByKey(this.AttrKey) + ") ������:(" + this.FK_Operator + ") �ж�ֵ:(" + this.OperatorValue.ToString() + ")";
                string exp = this.HisNode.BlockExp;
                string[] strs = exp.Trim().Split(' ');

                string key = strs[0].Trim();
                string oper = strs[1].Trim();
                string val = strs[2].Trim();
                val = val.Replace("'", "");
                val = val.Replace("%", "");
                val = val.Replace("~", "");

                BP.En.Row row = this.rptGe.Row;

                string valPara = null;
                if (row.ContainsKey(key) == false)
                {
                    try
                    {
                        /*���������ָ���Ĺؼ���key, �͵�����������ȥ��. */
                        if (Glo.SendHTOfTemp.ContainsKey(key) == false)
                            throw new Exception("@�ж�����ʱ����,��ȷ�ϲ����Ƿ�ƴд����,û���ҵ���Ӧ�ı��ʽ:" + exp + " Key=(" + key + ") oper=(" + oper + ")Val=(" + val + ")");
                        valPara = Glo.SendHTOfTemp[key].ToString().Trim();
                    }
                    catch
                    {
                        //�п����ǳ���. 
                        valPara = key;
                    }
                }
                else
                {
                    valPara = row[key].ToString().Trim();
                }

                #region ��ʼִ���ж�.
                if (oper == "=")
                {
                    if (valPara == val)
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }

                if (oper.ToUpper() == "LIKE")
                {
                    if (valPara.Contains(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }

                if (oper == ">")
                {
                    if (float.Parse(valPara) > float.Parse(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }
                if (oper == ">=")
                {
                    if (float.Parse(valPara) >= float.Parse(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }
                if (oper == "<")
                {
                    if (float.Parse(valPara) < float.Parse(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }
                if (oper == "<=")
                {
                    if (float.Parse(valPara) <= float.Parse(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }

                if (oper == "!=")
                {
                    if (float.Parse(valPara) != float.Parse(val))
                        return true;
                    else
                        throw new Exception("@" + Glo.DealExp(this.HisNode.BlockAlert, this.rptGe, null));

                }
                throw new Exception("@������ʽ����:" + exp + " Key=" + key + " oper=" + oper + " Val=" + val);
                #endregion ��ʼִ���ж�.
            }

            throw new Exception("@������ģʽû��ʵ��...");
        }

        /// <summary>
        /// ����������ҵ����.
        /// ��������:2012-11-11.
        /// ����ԭ��:�����߼��Բ�����,����©�Ĵ���ģʽ.
        /// �޸���:zhoupeng.
        /// �޸ĵص�:����.
        /// ----------------------------------- ˵�� -----------------------------
        /// 1���������Ϊ���󲿷�: ����ǰ���\5*5�㷨\���ͺ��ҵ����.
        /// 2, ��ϸ��ο��������ϵ�˵��.
        /// 3, ���ͺ����ֱ�ӻ�ȡ����
        /// </summary>
        /// <param name="jumpToNode">Ҫ��ת�Ľڵ�</param>
        /// <param name="jumpToEmp">Ҫ��ת����</param>
        /// <returns>ִ�нṹ</returns>
        public SendReturnObjs NodeSend(Node jumpToNode, string jumpToEmp)
        {
            if (this.HisNode.IsGuestNode) 
            {
                if (this.Execer != "Guest")
                    throw new Exception("@��ǰ�ڵ㣨" + this.HisNode.Name + "���ǿͻ�ִ�нڵ㣬���Ե�ǰ��¼��ԱӦ����Guest,������:" + this.Execer);
            }

            #region ��ȫ�Լ��.
            // ��1: ����Ƿ���Դ���ǰ�Ĺ���.
            //if (this.HisNode.IsStartNode == false
            //    && BP.WF.Dev2Interface.Flow_IsCanDoCurrentWork(this.HisNode.FK_Flow, this.HisNode.NodeID,
            //    this.WorkID, this.Execer) == false)
            //    throw new Exception("@��ǰ�������Ѿ�������ɣ�������(" + this.Execer + " " + this.ExecerName + ")û�д���ǰ������Ȩ�ޡ�");

            // ��1.2: ���÷���ǰ���¼��ӿ�,�����û������ҵ���߼�.
            string sendWhen = this.HisFlow.DoFlowEventEntity(EventListOfNode.SendWhen, this.HisNode, this.HisWork, null);
            if (sendWhen != null)
            {
                /*˵�����¼�Ҫִ��,��ִ�к�����ݲ�ѯ��ʵ����*/
                this.HisWork.RetrieveFromDBSources();
                this.HisWork.ResetDefaultVal();
                this.HisWork.Rec = this.Execer;
                this.HisWork.RecText = this.ExecerName;
                if (string.IsNullOrEmpty(sendWhen) == false)
                {
                    sendWhen = System.Web.HttpUtility.UrlDecode(sendWhen);
                    if (sendWhen.StartsWith("false") || sendWhen.StartsWith("False") || sendWhen.StartsWith("error") || sendWhen.StartsWith("Error"))
                    {
                        this.addMsg(SendReturnMsgFlag.SendWhen, sendWhen);
                        sendWhen = sendWhen.Replace("false", "");
                        sendWhen = sendWhen.Replace("False", "");

                        throw new Exception("@ִ�з���ǰ�¼�ʧ��:" + sendWhen);
                    }
                }
            }
            #endregion ��ȫ�Լ��.

            //����ϵͳ����.
            this.addMsg(SendReturnMsgFlag.VarCurrNodeID, this.HisNode.NodeID.ToString(), this.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarCurrNodeName, this.HisNode.Name, this.HisNode.Name, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarWorkID, this.WorkID.ToString(), this.WorkID.ToString(), SendReturnMsgType.SystemMsg);


            //������ת�ڵ㣬����п���Ϊnull.
            this.JumpToNode = jumpToNode;
            this.JumpToEmp = jumpToEmp;

            string sql = null;
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = this.Execer;
            this.WorkID = this.HisWork.OID;


            #region ��һ��: ��鵱ǰ����Ա�Ƿ���Է���: �������� 3 ������.
            //��1.2.1: ����ǿ�ʼ�ڵ㣬��Ҫ��鷢��������������.
            if (this.HisNode.IsStartNode)
            {
                if (Glo.CheckIsCanStartFlow_SendStartFlow(this.HisFlow, this.HisWork) == false)
                    throw new Exception("@Υ�������̷�����������:" + Glo.DealExp(this.HisFlow.StartLimitAlert, this.HisWork, null));
            }

            // ��1.3: �жϵ�ǰ����״̬.
            if (this.HisNode.IsStartNode == false
                && this.HisGenerWorkFlow.WFState == WFState.Askfor)
            {
                /*����Ǽ�ǩ״̬, ���жϼ�ǩ���Ƿ�Ҫ���ظ�ִ�м�ǩ��.*/
                GenerWorkerLists gwls = new GenerWorkerLists();
                gwls.Retrieve(GenerWorkerListAttr.FK_Node, this.HisNode.NodeID,
                    GenerWorkerListAttr.WorkID, this.WorkID);

                bool isDeal = false;
                AskforHelpSta askForSta = AskforHelpSta.AfterDealSend;
                foreach (GenerWorkerList item in gwls)
                {
                    if (item.IsPassInt == (int)AskforHelpSta.AfterDealSend)
                    {
                        /*����Ǽ�ǩ��ֱ�ӷ��;Ͳ������ˡ�*/
                        isDeal = true;
                        askForSta = AskforHelpSta.AfterDealSend;

                        // ����workerlist, ����������Ա��״̬Ϊ�Ѿ������״̬,�����ߵ���һ����ȥ.
                        DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

                        //д����־.
                        this.AddToTrack(ActionType.ForwardAskfor, item.FK_Emp, item.FK_EmpText,
                            this.HisNode.NodeID, this.HisNode.Name, "��ǩ�����·��ͣ�ֱ�ӷ��͸���һ�������ˡ�");
                    }

                    if (item.IsPassInt == (int)AskforHelpSta.AfterDealSendByWorker)
                    {
                        /*����Ǽ�ǩ��������ֱ�ӷ��͡�*/
                        item.IsPassInt = 0;

                        isDeal = true;
                        askForSta = AskforHelpSta.AfterDealSendByWorker;

                        // ����workerlist, ����������Ա��״̬Ϊ�Ѿ������״̬.
                        DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

                        // �ѷ����ǩ��Ա��״̬���¹����������ɼ����칤��.
                        item.IsPassInt = 0;
                        item.Update();

                        // ��������״̬.
                        this.HisGenerWorkFlow.WFState = WFState.AskForReplay;
                        this.HisGenerWorkFlow.Update();

                        //�ü�ǩ�ˣ����óɹ���δ����
                        BP.WF.Dev2Interface.Node_SetWorkUnRead(this.HisNode.NodeID, this.WorkID, item.FK_Emp);

                        // ����ʱ�������ȡ�ظ���ǩ���.
                        string replyInfo = this.HisGenerWorkFlow.Paras_AskForReply;

                        ////д����־.
                        //this.AddToTrack(ActionType.ForwardAskfor, item.FK_Emp, item.FK_EmpText,
                        //    this.HisNode.NodeID, this.HisNode.Name,
                        //    "��ǩ�����·��ͣ���ת���ǩ�˷����ˣ�" + item.FK_Emp + "��" + item.FK_EmpText + "����<br>���:" + replyInfo);

                        //д����־.
                        this.AddToTrack(ActionType.ForwardAskfor, item.FK_Emp, item.FK_EmpText,
                            this.HisNode.NodeID, this.HisNode.Name, "�ظ����:" + replyInfo);

                        //����ϵͳ������
                        this.addMsg(SendReturnMsgFlag.VarToNodeID, this.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarToNodeName, this.HisNode.Name, SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarAcceptersID, item.FK_Emp, SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarAcceptersName, item.FK_EmpText, SendReturnMsgType.SystemMsg);

                        //������ʾ��Ϣ.
                        this.addMsg(SendReturnMsgFlag.SendSuccessMsg, "�Ѿ�ת������ǩ�ķ�����(" + item.FK_Emp + "," + item.FK_EmpText + ")", SendReturnMsgType.Info);

                        //ɾ����ǰ����Ա��ʱ���ӵĹ����б��¼, �����ɾ���ͻᵼ�µڶ��μ�ǩʧ��.
                        GenerWorkerList gwl = new GenerWorkerList();
                        gwl.Delete(GenerWorkerListAttr.FK_Node, this.HisNode.NodeID,
                            GenerWorkerListAttr.WorkID, this.WorkID, GenerWorkerListAttr.FK_Emp, this.Execer);

                        //���ط��Ͷ���.
                        return this.HisMsgObjs;
                    }
                }

                if (isDeal == false)
                    throw new Exception("@����������󣬲�Ӧ���Ҳ�����ǩ��״̬.");
            }


            // ��3: ������Ǻ����㣬�����߳�δ��ɵ����.
            if (this.HisNode.IsHL || this.HisNode.HisRunModel == RunModel.FHL)
            {
                /*   ����Ǻ����� ��鵱ǰ�Ƿ��Ǻ���������ǣ���������ϵ����߳��Ƿ���ɡ�*/
                /*����Ƿ������߳�û�н���*/
                Paras ps = new Paras();
                ps.SQL = "SELECT WorkID,FK_Emp,FK_EmpText,FK_NodeText FROM WF_GenerWorkerList WHERE FID=" + ps.DBStr + "FID AND IsPass=0 AND IsEnable=1";
                ps.Add(WorkAttr.FID, this.WorkID);

                DataTable dtWL = DBAccess.RunSQLReturnTable(ps);
                string infoErr = "";
                if (dtWL.Rows.Count != 0)
                {
                    if (this.HisNode.ThreadKillRole == ThreadKillRole.None
                        || this.HisNode.ThreadKillRole == ThreadKillRole.ByHand)
                    {
                        infoErr += "@���������·��ͣ����������߳�û����ɡ�";
                        foreach (DataRow dr in dtWL.Rows)
                        {
                            infoErr += "@����Ա���:" + dr["FK_Emp"] + "," + dr["FK_EmpText"] + ",ͣ���ڵ�:" + dr["FK_NodeText"];
                        }

                        if (this.HisNode.ThreadKillRole == ThreadKillRole.ByHand)
                            infoErr += "@��֪ͨ���Ǵ������,����ǿ��ɾ�����������������·���.";
                        else
                            infoErr += "@��֪ͨ���Ǵ������,���������·���.";

                        //�׳��쳣��ֹ�������˶���
                        throw new Exception(infoErr);
                    }

                    if (this.HisNode.ThreadKillRole == ThreadKillRole.ByAuto)
                    {
                        //ɾ��ÿ�����̣߳�Ȼ�������˶���
                        foreach (DataRow dr in dtWL.Rows)
                            BP.WF.Dev2Interface.Flow_DeleteSubThread(this.HisFlow.No, Int64.Parse(dr[0].ToString()), "�����㷢��ʱ�Զ�ɾ��");
                    }
                }
            }
            #endregion ��һ��: ��鵱ǰ����Ա�Ƿ���Է���


            //��ѯ������ǰ�ڵ�Ĺ�������.
            this.rptGe = this.HisNode.HisFlow.HisGERpt;
            this.rptGe.SetValByKey("OID", this.WorkID);
            this.rptGe.RetrieveFromDBSources();

            //����Ƿ��������̴���, ����о��������̷�����ȥ. and 2015-01-23. for gaoling.
            this.CheckBlockModel();


            // ���FormTree������Ŀ,�����һЩ��Ŀû����д���׳��쳣.
            this.CheckFrmIsNotNull();

            //�����ݸ��µ����ݿ���.
            this.HisWork.DirectUpdate();
            if (this.HisWork.EnMap.PhysicsTable != this.rptGe.EnMap.PhysicsTable)
            {
                // �п����ⲿ�������ݹ������£�rpt������û�з����仯��
                this.rptGe.Copy(this.HisWork);
            }

            //����Ƕ��нڵ�, ���жϲ�����.
            if (this.HisNode.TodolistModel == TodolistModel.Order)
            {
                if (this.DealOradeNode() == true)
                    return this.HisMsgObjs;
            }

            //�����Э��ģʽ�ڵ�, ���жϲ�����.
            if (this.HisNode.TodolistModel == TodolistModel.Teamup)
            {
                /*�����Э��.*/
                if (this.DealTeamUpNode() == true)
                    return this.HisMsgObjs;
            }

            // ��������, ����û��ʵ��.
            DBAccess.DoTransactionBegin();
            try
            {
                if (this.HisNode.IsStartNode)
                    InitStartWorkDataV2(); // ��ʼ����ʼ�ڵ�����, �����ǰ�ڵ��ǿ�ʼ�ڵ�.

                //�������ˣ��ѷ����˵���Ϣ����wf_generworkflow 2015-01-14. ԭ������WF_GenerWorkerList.
                oldSender = this.HisGenerWorkFlow.Sender; //�ɷ�����,�ڻع���ʱ��Ѹ÷����˸�ֵ����.
                this.HisGenerWorkFlow.Sender = BP.WF.Glo.DealUserInfoShowModel(WebUser.No, WebUser.Name);


                if (this.HisGenerWorkFlow.WFState == WFState.ReturnSta)
                {
                    /* �����˻��Ƿ���ԭ·����? */
                    Paras ps = new Paras();
                    ps.SQL = "SELECT ReturnNode,Returner,IsBackTracking FROM WF_ReturnWork WHERE WorkID=" + dbStr + "WorkID AND IsBackTracking=1 ORDER BY RDT DESC";
                    ps.Add(ReturnWorkAttr.WorkID, this.WorkID);
                    DataTable mydt = DBAccess.RunSQLReturnTable(ps);
                    if (mydt.Rows.Count != 0)
                    {
                        //�п��ܲ�ѯ�����������Ϊ��ʱ�������ˣ�ֻȡ�����һ���˻صģ������Ƿ����˻ز�ԭ·���ص���Ϣ��

                        /*ȷ������˻أ����˻ز�ԭ·���� ,  �������ʼ�����Ĺ�����Ա, �뽫Ҫ���͵Ľڵ�. */
                        this.JumpToNode = new Node(int.Parse(mydt.Rows[0]["ReturnNode"].ToString()));
                        this.JumpToEmp = mydt.Rows[0]["Returner"].ToString();

                        /*�����ǰ���˻�.*/
                        if (this.JumpToNode.TodolistModel == TodolistModel.Order
                            || this.JumpToNode.TodolistModel == TodolistModel.Teamup)
                        {
                            /*����Ƕ��˴���ڵ�.*/
                            this.DealReturnOrderTeamup();
                            return this.HisMsgObjs;
                        }
                    }
                }

                if (this.HisGenerWorkFlow.FK_Node != this.HisNode.NodeID)
                    throw new Exception("@���̳��ֵĴ���,����ID="+this.WorkID+",��ǰ���(" + this.HisGenerWorkFlow.FK_Node + "" + this.HisGenerWorkFlow.NodeName + ")�뷢�͵�("+this.HisNode.NodeID+this.HisNode.Name+")��һ��");

                // ������������
                if (jumpToNode != null && this.HisNode.IsEndNode)
                {
                    /*����ת����������������Ľڵ㣬�Ͳ�����������������*/
                }
                else
                {
                    this.CheckCompleteCondition();
                }

                // ������������. add by stone. 2014-11-23.
                if (jumpToNode == null && this.HisGenerWorkFlow.IsAutoRun == false)
                {
                    //���û��ָ��Ҫ��ת���Ľڵ㣬���ҵ�ǰ�����ֹ���Ԥ������״̬.
                    TransferCustom tc = TransferCustom.GetNextTransferCustom(this.WorkID, this.HisNode.NodeID);
                    if (tc == null)
                    {
                        /*��ʾִ�е������������.*/
                        this.IsStopFlow = true;
                    }
                    else
                    {
                        this.JumpToNode = new Node(tc.FK_Node);
                        this.JumpToEmp = tc.Worker;
                    }
                }


                // �����������ˣ��ڷ���ǰ��
                this.DealEval();

                // ����ϵͳ����.
                if (this.IsStopFlow)
                    this.addMsg(SendReturnMsgFlag.IsStopFlow, "1", "�����Ѿ�����", SendReturnMsgType.Info);
                else
                    this.addMsg(SendReturnMsgFlag.IsStopFlow, "0", "����δ����", SendReturnMsgType.SystemMsg);

                string mymsgHtml =  "@�鿴<img src='" + VirPath + "WF/Img/Btn/PrintWorkRpt.gif' ><a href='" + VirPath + "WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "' target='_self' >�����켣</a>";
                this.addMsg(SendReturnMsgFlag.MsgOfText, mymsgHtml );


                if (this.IsStopFlow == true)
                {
                    /*�ڼ����󣬷������ı�־�����Ѿ�ֹͣ�ˡ�*/
                    this.Func_DoSetThisWorkOver();
                    this.rptGe.WFState = WFState.Complete;
                    this.rptGe.Update();
                }
                else
                {
                    #region �ڶ���: ������ĵ�������ת��������. 5*5 �ķ�ʽ����ͬ�ķ������.
                    // ִ�нڵ����·��͵�25��������ж�.
                    this.NodeSend_Send_5_5();

                    if (this.IsStopFlow)
                    {
                        this.rptGe.WFState = WFState.Complete;
                        this.Func_DoSetThisWorkOver();
                    }
                    else
                    {
                        //������˻�״̬���Ͱ��Ƿ�ԭ·���صĹ켣ȥ��.
                        if (this.HisGenerWorkFlow.WFState == WFState.ReturnSta)
                            BP.DA.DBAccess.RunSQL("UPDATE WF_ReturnWork SET IsBackTracking=0 WHERE WorkID=" + this.WorkID);

                        this.Func_DoSetThisWorkOver();
                        if (town != null && town.HisNode.HisBatchRole == BatchRole.Group)
                        {
                            this.HisGenerWorkFlow.WFState = WFState.Batch;
                            this.HisGenerWorkFlow.Update();
                        }
                    }

                    this.rptGe.Update();
                    #endregion �ڶ���: 5*5 �ķ�ʽ����ͬ�ķ������.
                }

                #region ������: ������֮���ҵ���߼�.
                //�ѵ�ǰ�ڵ������copy���������ݱ���.
                this.DoCopyCurrentWorkDataToRpt();

                #endregion ������: ������֮���ҵ���߼�.

                #region �������߳�
                if (this.HisNode.IsStartNode && this.HisNode.SubFlowStartWay != SubFlowStartWay.None)
                    CallSubFlow();

                #endregion �������߳�

                #region ����������
                if (Glo.IsEnableSysMessage && this.IsStopFlow == false)
                    this.DoRefFunc_Listens(); // ����Ѿ���ֹworkflow,��Ϣ�����Ѿ�������.
                #endregion

                #region ���ɵ���
                if (this.HisNode.HisPrintDocEnable == PrintDocEnable.PrintRTF && this.HisNode.BillTemplates.Count == 0)
                {

                    BillTemplates reffunc = this.HisNode.BillTemplates;
                    #region ���ɵ�����Ϣ
                    Int64 workid = this.HisWork.OID;
                    int nodeId = this.HisNode.NodeID;
                    string flowNo = this.HisNode.FK_Flow;
                    #endregion

                    DateTime dtNow = DateTime.Now;
                    Flow fl = this.HisNode.HisFlow;
                    string year = dt.Year.ToString();
                    string billInfo = "";
                    foreach (BillTemplate func in reffunc)
                    {
                        if (func.HisBillFileType != BillFileType.RuiLang)
                        {
                            string file = year + "_" + this.ExecerDeptNo + "_" + func.No + "_" + workid + ".doc";
                            BP.Pub.RTFEngine rtf = new BP.Pub.RTFEngine();

                            Works works;
                            string[] paths;
                            string path;
                            try
                            {
                                #region �����ݷ��� �������档
                                rtf.HisEns.Clear(); //�������ݡ�
                                rtf.EnsDataDtls.Clear(); // ��ϸ������.

                                // �ҵ���������.
                                rtf.HisGEEntity = new GEEntity(this.rptGe.ClassID);
                                rtf.HisGEEntity.Row = rptGe.Row;

                                // ��ÿ���ڵ��ϵĹ������뵽�������档
                                rtf.AddEn(this.HisWork);
                                rtf.ensStrs += ".ND" + this.HisNode.NodeID;

                                //�ѵ�ǰwork��Dtl ���ݷŽ�ȥ�ˡ�
                                ArrayList al = this.HisWork.GetDtlsDatasOfArrayList();
                                foreach (Entities ens in al)
                                    rtf.AddDtlEns(ens);
                                #endregion �����ݷ��� �������档

                                #region ���ɵ���

                                paths = file.Split('_');
                                path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";
                                string billUrl = "/DataUser/Bill/" + path + file;
                                if (func.HisBillFileType == BillFileType.PDF)
                                {
                                    billUrl = billUrl.Replace(".doc", ".pdf");
                                    billInfo += "<img src='" + VirPath + "WF/Img/FileType/PDF.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                                }
                                else
                                {
                                    billInfo += "<img src='" + VirPath + "WF/Img/FileType/doc.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                                }

                                path = BP.WF.Glo.FlowFileBill + year + "\\" + this.ExecerDeptNo + "\\" + func.No + "\\";
                                // path = AppDomain.CurrentDomain.BaseDirectory + path;
                                if (System.IO.Directory.Exists(path) == false)
                                    System.IO.Directory.CreateDirectory(path);

                                rtf.MakeDoc(func.Url + ".rtf",
                                    path, file, func.ReplaceVal, false);
                                #endregion

                                #region ת����pdf.
                                if (func.HisBillFileType == BillFileType.PDF)
                                {
                                    string rtfPath = path + file;
                                    string pdfPath = rtfPath.Replace(".doc", ".pdf");
                                    try
                                    {
                                        Glo.Rtf2PDF(rtfPath, pdfPath);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.addMsg("RptError", "�����������ݴ���:" + ex.Message);
                                    }
                                }
                                #endregion

                                #region ���浥��
                                Bill bill = new Bill();
                                bill.MyPK = this.HisWork.FID + "_" + this.HisWork.OID + "_" + this.HisNode.NodeID + "_" + func.No;
                                bill.FID = this.HisWork.FID;
                                bill.WorkID = this.HisWork.OID;
                                bill.FK_Node = this.HisNode.NodeID;
                                bill.FK_Dept = this.ExecerDeptNo;
                                bill.FK_Emp = this.Execer;
                                bill.Url = billUrl;
                                bill.RDT = DataType.CurrentDataTime;
                                bill.FullPath = path + file;
                                bill.FK_NY = DataType.CurrentYearMonth;
                                bill.FK_Flow = this.HisNode.FK_Flow;
                                bill.FK_BillType = func.FK_BillType;
                                bill.FK_Flow = this.HisNode.FK_Flow;
                                bill.Emps = this.rptGe.GetValStrByKey("Emps");
                                bill.FK_Starter = this.rptGe.GetValStrByKey("Rec");
                                bill.StartDT = this.rptGe.GetValStrByKey("RDT");
                                bill.Title = this.rptGe.GetValStrByKey("Title");
                                bill.FK_Dept = this.rptGe.GetValStrByKey("FK_Dept");
                                try
                                {
                                    bill.Insert();
                                }
                                catch
                                {
                                    bill.Update();
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                BP.WF.DTS.InitBillDir dir = new BP.WF.DTS.InitBillDir();
                                dir.Do();
                                path = BP.WF.Glo.FlowFileBill + year + "\\" + this.ExecerDeptNo + "\\" + func.No + "\\";
                                string msgErr = "@" + string.Format("���ɵ���ʧ�ܣ����ù���Ա���Ŀ¼����") + "[" + BP.WF.Glo.FlowFileBill + "]��@Err��" + ex.Message + " @File=" + file + " @Path:" + path;
                                billInfo += "@<font color=red>" + msgErr + "</font>@<hr>@ϵͳ�Ѿ����˿����Ե��޸��������ڷ���һ�Σ����������Ȼ��������ϵ����Ա��";
                                throw new Exception(msgErr + "@������Ϣ:" + ex.Message);
                            }
                        }

                    } // end ����ѭ�����ݡ�

                    if (billInfo != "")
                        billInfo = "@" + billInfo;
                    this.addMsg(SendReturnMsgFlag.BillInfo, billInfo);
                }
                #endregion

                #region ִ�г���.
                //ִ�г���.
                if (!this.HisNode.IsEndNode)
                {
                    CCWork cc = new CCWork(this);
                }

                DBAccess.DoTransactionCommit(); //�ύ����.
                #endregion ������Ҫҵ���߼�.

                #region �����ͳɹ����¼�.
                try
                {
                    //�����ͳɹ�����¼����Ѳ��������ȥ��
                    if (this.SendHTOfTemp != null)
                    {
                        foreach (string key in this.SendHTOfTemp.Keys)
                        {
                            if (rptGe.Row.ContainsKey(key) == true)
                                this.rptGe.Row[key] = this.SendHTOfTemp[key].ToString();
                            else
                                this.rptGe.Row.Add(key, this.SendHTOfTemp[key].ToString());
                        }
                    }

                    //ִ�з���.
                    string sendSuccess = this.HisFlow.DoFlowEventEntity(EventListOfNode.SendSuccess,
                        this.HisNode, this.rptGe, null, this.HisMsgObjs);

                    //string SendSuccess = this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.SendSuccess, this.HisWork);
                    if (sendSuccess != null)
                        this.addMsg(SendReturnMsgFlag.SendSuccessMsg, sendSuccess);
                }
                catch (Exception ex)
                {
                    this.addMsg(SendReturnMsgFlag.SendSuccessMsgErr, ex.Message);
                }
                #endregion �����ͳɹ����¼�.

                #region ��������������ҵ��������ͬ��.
                if (this.HisFlow.DTSWay != FlowDTSWay.None)
                    this.HisFlow.DoBTableDTS(this.rptGe, this.HisNode, this.IsStopFlow);

                #endregion ��������������ҵ��������ͬ��.


                #region �����ͳɹ������Ϣ��ʾ
                if (this.HisNode.HisTurnToDeal == TurnToDeal.SpecMsg)
                {
                    string msgOfSend = this.HisNode.TurnToDealDoc;
                    if (msgOfSend.Contains("@"))
                    {
                        Attrs attrs = this.HisWork.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            if (msgOfSend.Contains("@") == false)
                                continue;
                            msgOfSend = msgOfSend.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                        }
                    }

                    if (msgOfSend.Contains("@") == true)
                    {
                        /*˵����һЩ������ϵͳ��������.*/
                        string msgOfSendText = msgOfSend.Clone() as string;
                        foreach (SendReturnObj item in this.HisMsgObjs)
                        {
                            if (string.IsNullOrEmpty(item.MsgFlag))
                                continue;

                            if (msgOfSend.Contains("@") == false)
                                continue;

                            msgOfSendText = msgOfSendText.Replace("@" + item.MsgFlag, item.MsgOfText);

                            if (item.MsgOfHtml != null)
                                msgOfSend = msgOfSend.Replace("@" + item.MsgFlag, item.MsgOfHtml);
                            else
                                msgOfSend = msgOfSend.Replace("@" + item.MsgFlag, item.MsgOfText);
                        }

                        this.HisMsgObjs.OutMessageHtml = msgOfSend;
                        this.HisMsgObjs.OutMessageText = msgOfSendText;
                    }
                    else
                    {
                        this.HisMsgObjs.OutMessageHtml = msgOfSend;
                        this.HisMsgObjs.OutMessageText = msgOfSend;
                    }

                    //return msgOfSend;
                }
                #endregion �����ͳɹ����¼�.

                #region �����Ҫ��ת.
                if (town != null)
                {
                    if (this.town.HisNode.HisRunModel == RunModel.SubThread && this.town.HisNode.HisRunModel == RunModel.SubThread)
                    {
                        this.addMsg(SendReturnMsgFlag.VarToNodeID, town.HisNode.NodeID.ToString(), town.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarToNodeName, town.HisNode.Name, town.HisNode.Name, SendReturnMsgType.SystemMsg);
                    }

#warning ��������������Զ���ת������ȥ����. 2014-11-07.
                    //if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
                    //{
                    //    town.NodeSend();
                    //    this.HisMsgObjs = town.HisMsgObjs;
                    //}
                }
                #endregion �����Ҫ��ת.

                #region ɾ���Ѿ����͵���Ϣ����Щ��Ϣ�Ѿ���Ϊ��������Ϣ.
                if (Glo.IsEnableSysMessage == true)
                {
                    Paras ps = new Paras();
                    ps.SQL = "DELETE FROM Sys_SMS WHERE MsgFlag=" + SystemConfig.AppCenterDBVarStr + "MsgFlag";
                    ps.Add("MsgFlag", "WKAlt" + this.HisNode.NodeID + "_" + this.WorkID);
                    BP.DA.DBAccess.RunSQL(ps);
                }
                #endregion

                #region �������̵ı��.
                if (this.HisNode.IsStartNode)
                {
                    if (this.rptGe.PWorkID != 0 && this.HisGenerWorkFlow.PWorkID == 0)
                    {
                        BP.WF.Dev2Interface.SetParentInfo(this.HisFlow.No, this.WorkID, this.rptGe.PFlowNo, this.rptGe.PWorkID, this.rptGe.PNodeID, this.rptGe.PEmp);

                        //д��track, �����˸�����.
                        Node pND = new Node(rptGe.PNodeID);
                        Int64 fid = 0;
                        if (pND.HisNodeWorkType == NodeWorkType.SubThreadWork)
                        {
                            GenerWorkFlow gwf = new GenerWorkFlow(this.rptGe.PWorkID);
                            fid = gwf.FID;
                        }

                        string paras = "@SubFlowNo=" + this.HisFlow.No + "@SubWorkID=" + this.WorkID;

                        Glo.AddToTrack(ActionType.StartChildenFlow, rptGe.PFlowNo, rptGe.PWorkID, fid, pND.NodeID, pND.Name,
                            WebUser.No, WebUser.Name,
                            pND.NodeID, pND.Name, WebUser.No, WebUser.Name,
                            "<a href='/WF/WFRpt.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "' target=_blank >��������</a>", paras);
                    }
                    else if (SystemConfig.IsBSsystem == true)
                    {
                        /*�����BSϵͳ*/
                        string pflowNo = BP.Sys.Glo.Request.QueryString["PFlowNo"];
                        if (string.IsNullOrEmpty(pflowNo) == false)
                        {
                            string pWorkID = BP.Sys.Glo.Request.QueryString["PWorkID"];
                            string pNodeID = BP.Sys.Glo.Request.QueryString["PNodeID"];
                            string pEmp = BP.Sys.Glo.Request.QueryString["PEmp"];

                            // ���óɸ����̹�ϵ.
                            BP.WF.Dev2Interface.SetParentInfo(this.HisFlow.No, this.WorkID, pflowNo, Int64.Parse(pWorkID), int.Parse(pNodeID), pEmp);

                            //д��track, �����˸�����.
                            Node pND = new Node(pNodeID);
                            Int64 fid = 0;
                            if (pND.HisNodeWorkType == NodeWorkType.SubThreadWork)
                            {
                                GenerWorkFlow gwf = new GenerWorkFlow(Int64.Parse(pWorkID));
                                fid = gwf.FID;
                            }
                            string paras = "@SubFlowNo=" + this.HisFlow.No + "@SubWorkID=" + this.WorkID;
                            Glo.AddToTrack(ActionType.StartChildenFlow, pflowNo, Int64.Parse(pWorkID), Int64.Parse(fid.ToString()), pND.NodeID, pND.Name, WebUser.No, WebUser.Name,
                                pND.NodeID, pND.Name, WebUser.No, WebUser.Name,
                                "<a href='/WF/WFRpt.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "' target=_blank >��������</a>", paras);
                        }
                    }
                }
                #endregion �������̵ı��.

                //�����������.
                return this.HisMsgObjs;
            }
            catch (Exception ex)
            {

                this.WhenTranscactionRollbackError(ex);
                DBAccess.DoTransactionRollback();
                throw new Exception("Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
            }
        }
        /// <summary>
        /// ִ��ҵ�������ͬ��.
        /// </summary>
        private void DTSBTable()
        {

        }
        /// <summary>
        /// �ֹ��Ļع��ύʧ����Ϣ.
        /// </summary>
        /// <param name="ex"></param>
        private void WhenTranscactionRollbackError(Exception ex)
        {
            /*���ύ���������£��ع����ݡ�*/

            #region ����Ƿ�������ͬ������ʧ���ٴη��;ͳ��ִ���
            if (this.town != null
                && this.town.HisNode.HisNodeWorkType == NodeWorkType.SubThreadWork
                && this.town.HisNode.HisSubThreadType == SubThreadType.SameSheet)
            {
                /*��������߳�*/
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE FID=" + this.WorkID + " AND FK_Node=" + this.town.HisNode.NodeID);
                //ɾ�����߳�����.
                DBAccess.RunSQL("DELETE FROM " + this.town.HisWork.EnMap.PhysicsTable + " WHERE FID=" + this.WorkID);
            }
            #endregion ����Ƿ�������ͬ������ʧ���ٴη��;ͳ��ִ���

            try
            {
                //ɾ����������־.
                DBAccess.RunSQL("DELETE FROM ND" + int.Parse(this.HisFlow.No) + "Track WHERE WorkID=" + this.WorkID +
                                " AND NDFrom=" + this.HisNode.NodeID + " AND ActionType=" + (int)ActionType.Forward);

                // ɾ��������Ϣ��
                this.DealEvalUn();

                // �ѹ�����״̬���û�����
                if (this.HisNode.IsStartNode)
                {
                    ps = new Paras();
                    ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + (int)WFState.Runing + " WHERE OID=" +
                             dbStr + "OID ";
                    ps.Add(GERptAttr.OID, this.WorkID);
                    DBAccess.RunSQL(ps);
                    //  this.HisWork.Update(GERptAttr.WFState, (int)WFState.Runing);
                }

                // �����̵�״̬���û�����
                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.WorkID = this.WorkID;
                if (gwf.RetrieveFromDBSources() == 0)
                    return;

                if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                {
                    /* ���������������һ���б仯��*/
                    gwf.FK_Node = this.HisNode.NodeID;
                    gwf.NodeName = this.HisNode.Name;
                    gwf.WFState = WFState.Runing;
                    this.HisGenerWorkFlow.Sender = BP.WF.Glo.DealUserInfoShowModel(oldSender, oldSender);
                    gwf.Update();
                }

                //ִ������.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr +
                         "WorkID AND FK_Node=" + dbStr + "FK_Node ";
                ps.AddFK_Emp();
                ps.Add("WorkID", this.WorkID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

                Node startND = this.HisNode.HisFlow.HisStartNode;
                StartWork wk = startND.HisWork as StartWork;
                switch (startND.HisNodeWorkType)
                {
                    case NodeWorkType.StartWorkFL:
                    case NodeWorkType.WorkFL:
                        break;
                    default:
                        /*
                         Ҫ����ɾ��WFState �ڵ��ֶε����⡣
                         */
                        //// �ѿ�ʼ�ڵ��װ̬���û�����
                        //DBAccess.RunSQL("UPDATE " + wk.EnMap.PhysicsTable + " SET WFState=0 WHERE OID="+this.WorkID+" OR OID="+this);
                        //wk.OID = this.WorkID;
                        //int i =wk.RetrieveFromDBSources();
                        //if (wk.WFState == WFState.Complete)
                        //{
                        //    wk.Update("WFState", (int)WFState.Runing);
                        //}
                        break;
                }
                Nodes nds = this.HisNode.HisToNodes;
                foreach (Node nd in nds)
                {
                    if (nd.NodeID == this.HisNode.NodeID)
                        continue;

                    Work mwk = nd.HisWork;
                    if (mwk.EnMap.PhysicsTable == this.HisFlow.PTable
                        || mwk.EnMap.PhysicsTable == this.HisWork.EnMap.PhysicsTable)
                        continue;

                    mwk.OID = this.WorkID;
                    try
                    {
                        mwk.DirectDelete();
                    }
                    catch
                    {
                        mwk.CheckPhysicsTable();
                        mwk.DirectDelete();
                    }
                }
                this.HisFlow.DoFlowEventEntity(EventListOfNode.SendError, this.HisNode, this.HisWork, null);

            }
            catch (Exception ex1)
            {
                if (this.town != null && this.town.HisWork != null)
                    this.town.HisWork.CheckPhysicsTable();

                if (this.rptGe != null)
                    this.rptGe.CheckPhysicsTable();
                throw new Exception(ex.Message + "@�ع�����ʧ�����ݳ��ִ���" + ex1.StackTrace + "@�п���ϵͳ�Ѿ��Զ��޸���������������ִ��һ�Ρ�");
            }
        }
        #endregion

        #region �û����ı���
        public GenerWorkerLists HisWorkerLists = null;
        private GenerWorkFlow _HisGenerWorkFlow;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                {
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID);
                    SendNodeWFState = _HisGenerWorkFlow.WFState; //���÷���ǰ�Ľڵ�״̬��
                }
                return _HisGenerWorkFlow;
            }
            set
            {
                _HisGenerWorkFlow = value;
            }
        }
        private Int64 _WorkID = 0;
        /// <summary>
        /// ����ID.
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return _WorkID;
            }
            set
            {
                _WorkID = value;
            }
        }
        /// <summary>
        /// ԭ���ķ�����.
        /// </summary>
        private string oldSender = null;
        #endregion

        /// <summary>
        /// ���ɱ���
        /// </summary>
        /// <param name="wk"></param>
        /// <param name="emp"></param>
        /// <param name="rdt"></param>
        /// <returns></returns>
        public static string GenerTitle(Flow fl, Work wk, Emp emp, string rdt)
        {
            string titleRole = fl.TitleRole.Clone() as string;
            if (string.IsNullOrEmpty(titleRole))
            {
                // Ϊ�˱�����ccflow4.5�ļ���,�ӿ�ʼ�ڵ��������ȡ.
                Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
                if (myattr == null)
                    myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

                if (myattr != null)
                    titleRole = myattr.DefaultVal.ToString();

                if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                    titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";
            }


            titleRole = titleRole.Replace("@WebUser.No", emp.No);
            titleRole = titleRole.Replace("@WebUser.Name", emp.Name);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", emp.FK_DeptText);
            titleRole = titleRole.Replace("@WebUser.FK_Dept", emp.FK_Dept);
            titleRole = titleRole.Replace("@RDT", rdt);
            if (titleRole.Contains("@")==true)
            {
                Attrs attrs = wk.EnMap.Attrs;

                // ���ȿ���������滻��
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsRefAttr == false)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }

                //�ڿ����������ֶ��滻.
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsRefAttr == true)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            titleRole = titleRole.Replace('~', '-');
            titleRole = titleRole.Replace("'", "��");

            if (titleRole.Contains("@"))
            {
                /*���û���滻�ɾ����Ϳ������û��ֶ�ƴд����*/
                throw new Exception("@�����Ƿ����ֶ�ƴд���󣬱������б���û�б��滻����. @" + titleRole);
            }
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        /// <summary>
        /// ���ɱ���
        /// </summary>
        /// <param name="wk"></param>
        /// <returns></returns>
        public static string GenerTitle(Flow fl, Work wk)
        {

            string titleRole = fl.TitleRole.Clone() as string;
            if (string.IsNullOrEmpty(titleRole))
            {
                // Ϊ�˱�����ccflow4.5�ļ���,�ӿ�ʼ�ڵ��������ȡ.
                Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
                if (myattr == null)
                    myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

                if (myattr != null)
                    titleRole = myattr.DefaultVal.ToString();

                if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                    titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";
            }

            if (titleRole == "@OutPara")
                titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";


            titleRole = titleRole.Replace("@WebUser.No", wk.Rec);
            titleRole = titleRole.Replace("@WebUser.Name", wk.RecText);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", wk.RecOfEmp.FK_DeptText);
            titleRole = titleRole.Replace("@WebUser.FK_Dept", wk.RecOfEmp.FK_Dept);
            titleRole = titleRole.Replace("@RDT", wk.RDT);

            if (titleRole.Contains("@"))
            {
                Attrs attrs = wk.EnMap.Attrs;

                // ���ȿ���������滻,��Ϊ����ı����ֶεĳ�����Խϳ���
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsRefAttr == false)
                        continue;
                     
                    string temp= wk.GetValStrByKey(attr.Key);
                    if (string.IsNullOrEmpty(temp))
                    {
                        wk.DirectUpdate();
                        wk.RetrieveFromDBSources();
                    }

                    titleRole = titleRole.Replace("@" + attr.Key, temp);
                }

                //�ڿ����������ֶ��滻.
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;

                    if (attr.IsRefAttr == true)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            titleRole = titleRole.Replace('~', '-');
            titleRole = titleRole.Replace("'", "��");

            // Ϊ��ǰ�Ĺ�������title.
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        /// <summary>
        /// ���ɱ���
        /// </summary>
        /// <param name="fl"></param>
        /// <param name="wk"></param>
        /// <returns></returns>
        public static string GenerTitle(Flow fl, GERpt wk)
        {
            string titleRole = fl.TitleRole.Clone() as string;
            if (string.IsNullOrEmpty(titleRole))
            {
                // Ϊ�˱�����ccflow4.5�ļ���,�ӿ�ʼ�ڵ��������ȡ.
                Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
                if (myattr == null)
                    myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

                if (myattr != null)
                    titleRole = myattr.DefaultVal.ToString();

                if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                    titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";
            }

            if (titleRole == "@OutPara")
                titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";


            titleRole = titleRole.Replace("@WebUser.No", wk.FlowStarter);
            titleRole = titleRole.Replace("@WebUser.Name", WebUser.Name);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);
            titleRole = titleRole.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
            titleRole = titleRole.Replace("@RDT", wk.FlowStartRDT);
            if (titleRole.Contains("@"))
            {
                Attrs attrs = wk.EnMap.Attrs;

                // ���ȿ���������滻,��Ϊ����ı����ֶεĳ�����Խϳ���
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsRefAttr == false)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }

                //�ڿ����������ֶ��滻.
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;

                    if (attr.IsRefAttr == true)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            titleRole = titleRole.Replace('~', '-');
            titleRole = titleRole.Replace("'", "��");

            // Ϊ��ǰ�Ĺ�������title.
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        public static string GenerTitle_Del(Work wk)
        {
            // ���ɱ���.
            Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
            if (myattr == null)
                myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

            string titleRole = "";
            if (myattr != null)
                titleRole = myattr.DefaultVal.ToString();

            if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";

            titleRole = titleRole.Replace("@WebUser.No", wk.Rec);
            titleRole = titleRole.Replace("@WebUser.Name", wk.RecText);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", wk.RecOfEmp.FK_DeptText);
            titleRole = titleRole.Replace("@WebUser.FK_Dept", wk.RecOfEmp.FK_Dept);
            titleRole = titleRole.Replace("@RDT", wk.RDT);
            if (titleRole.Contains("@"))
            {
                Attrs attrs = wk.EnMap.Attrs;

                // ���ȿ���������滻��
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsRefAttr == false)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }

                //�ڿ����������ֶ��滻.
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;

                    if (attr.IsRefAttr == true)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            titleRole = titleRole.Replace('~', '-');
            titleRole = titleRole.Replace("'", "��");
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        public GERpt rptGe = null;
        private void InitStartWorkDataV2()
        {
            /*����ǿ�ʼ�����ж��ǲ��Ǳ���������̣�����Ǿ�Ҫ������д��־��*/
            if (SystemConfig.IsBSsystem)
            {
                string fk_nodeFrom = BP.Sys.Glo.Request.QueryString["FromNode"];
                if (string.IsNullOrEmpty(fk_nodeFrom) == false)
                {
                    Node ndFrom = new Node(int.Parse(fk_nodeFrom));
                    string PWorkID = BP.Sys.Glo.Request.QueryString["PWorkID"];
                    if (string.IsNullOrEmpty(PWorkID))
                        PWorkID = BP.Sys.Glo.Request.QueryString["PWorkID"];

                    string pTitle = DBAccess.RunSQLReturnStringIsNull("SELECT Title FROM  ND" + int.Parse(ndFrom.FK_Flow) + "01 WHERE OID=" + PWorkID, "");

                    ////��¼��ǰ���̱�����
                    //  this.AddToTrack(ActionType.StartSubFlow, WebUser.No,
                    //  WebUser.Name, ndFrom.NodeID, ndFrom.FlowName + "\t\n" + ndFrom.FlowName, "��������(" + ndFrom.FlowName + ":" + pTitle + ")����.");

                    //��¼�����̱�����
                    BP.WF.Dev2Interface.WriteTrack(this.HisFlow.No, this.HisNode.NodeID, this.WorkID, 0,
                        "��{" + ndFrom.FlowName + "}����,������:" + this.ExecerName, ActionType.CallChildenFlow,
                        "@PWorkID=" + PWorkID + "@PFlowNo=" + ndFrom.HisFlow.No, "����������:" + this.HisFlow.Name, null);
                }
            }

            /* ������ʼ�������̼�¼. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;
            int srcNum = gwf.RetrieveFromDBSources();
            if (srcNum == 0)
            {
                gwf.WFState = WFState.Runing;
            }
            else
            {
                if (gwf.WFState == WFState.Blank)
                    gwf.WFState = WFState.Runing;

                SendNodeWFState = gwf.WFState; //���÷���ǰ�Ľڵ�״̬��
            }

            #region �������̱���.
            if (this.title == null)
            {
                if (this.HisFlow.TitleRole == "@OutPara")
                {
                    /*������ⲿ����,*/
                    gwf.Title = DBAccess.RunSQLReturnString("SELECT Title FROM " + this.HisFlow.PTable + " WHERE OID=" + this.WorkID);
                    if (string.IsNullOrEmpty(gwf.Title))
                        gwf.Title = this.Execer + "," + this.ExecerName + "��:" + DataType.CurrentDataTimeCN + "����.";
                    //throw new Exception("�����õ����̱������ɹ���Ϊ�ⲿ�����Ĳ����������������ڴ����հ׹���ʱ�����̱���ֵΪ�ա�");
                }
                else
                {
                    gwf.Title = WorkNode.GenerTitle(this.HisFlow, this.HisWork);
                }
            }
            else
            {
                gwf.Title = this.title;
            }

            //���̱���.
            this.rptGe.Title = gwf.Title;
            #endregion �������̱���.

            if (string.IsNullOrEmpty(rptGe.BillNo))
            {
                //�����ݱ��.
                string billNo = this.HisFlow.BillNoFormat.Clone() as string;
                if (string.IsNullOrEmpty(billNo) == false)
                {
                    billNo = BP.WF.Glo.GenerBillNo(billNo, this.WorkID, this.rptGe, this.HisFlow.PTable);
                    gwf.BillNo = billNo;
                    this.rptGe.BillNo = billNo;
                }
            }
            else
            {
                gwf.BillNo = rptGe.BillNo;
            }

            this.HisWork.SetValByKey("Title", gwf.Title);
            gwf.RDT = this.HisWork.RDT;
            gwf.Starter = this.Execer;
            gwf.StarterName = this.ExecerName;
            gwf.FK_Flow = this.HisNode.FK_Flow;
            gwf.FlowName = this.HisNode.FlowName;
            gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;
            gwf.FK_Node = this.HisNode.NodeID;
            gwf.NodeName = this.HisNode.Name;
            gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
            gwf.DeptName = this.HisWork.RecOfEmp.FK_DeptText;
            if (Glo.IsEnablePRI)
            {
                try
                {
                    gwf.PRI = this.HisWork.GetValIntByKey("PRI");
                }
                catch (Exception ex)
                {
                    this.HisNode.RepareMap();
                }
            }

            if (this.HisFlow.HisTimelineRole == TimelineRole.ByFlow)
            {
                try
                {
                    gwf.SDTOfFlow = this.HisWork.GetValStrByKey(WorkSysFieldAttr.SysSDTOfFlow);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError("������������ƴ���,��ȡ��ʼ�ڵ�{" + gwf.Title + "}����������Ӧ���ʱ���д���,�Ƿ����SysSDTOfFlow�ֶ�? �쳣��Ϣ:" + ex.Message);
                    /*��ȡ��ʼ�ڵ����������Ӧ���ʱ���д���,�Ƿ����SysSDTOfFlow�ֶ�? .*/
                    if (this.HisWork.EnMap.Attrs.Contains(WorkSysFieldAttr.SysSDTOfFlow) == false)
                        throw new Exception("������ƴ��������õ�����ʱЧ�����ǣ�����ʼ�ڵ��SysSDTOfFlow�ֶμ���������ǿ�ʼ�ڵ���������ֶ� SysSDTOfFlow , ϵͳ������Ϣ:" + ex.Message);
                    throw new Exception("��ʼ����ʼ�ڵ����ݴ���:" + ex.Message);
                }
            }

            //������������. 2013-02-17
            if (gwf.PWorkID != 0)
            {
                this.rptGe.PWorkID = gwf.PWorkID;
                this.rptGe.PFlowNo = gwf.PFlowNo;
                this.rptGe.PNodeID = gwf.PNodeID;
            }
            else
            {

                try { gwf.PWorkID = this.rptGe.PWorkID; }
                catch (Exception e) { gwf.PWorkID = 0; }
                try { gwf.PFlowNo = this.rptGe.PFlowNo; }
                catch (Exception e) { gwf.PFlowNo = ""; }
                try { gwf.PNodeID = this.rptGe.PNodeID; }
                catch (Exception e) { gwf.PNodeID = 0; }
            }

            // ����FlowNote
            string note = this.HisFlow.FlowNoteExp.Clone() as string;
            if (string.IsNullOrEmpty(note) == false)
                note = BP.WF.Glo.DealExp(note, this.rptGe, null);
            this.rptGe.FlowNote = note;
            gwf.FlowNote = note;


            if (srcNum == 0)
                gwf.DirectInsert();
            else
                gwf.DirectUpdate();

            StartWork sw = (StartWork)this.HisWork;

            #region ����  HisGenerWorkFlow

            this.HisGenerWorkFlow = gwf;

            #endregion HisCHOfFlow

            #region  ������ʼ������,�ܹ�ִ�����ǵ���Ա.
            GenerWorkerList wl = new GenerWorkerList();
            wl.WorkID = this.HisWork.OID;
            wl.FK_Node = this.HisNode.NodeID;
            wl.FK_Emp = this.Execer;
            wl.Delete();

            wl.FK_NodeText = this.HisNode.Name;
            wl.FK_EmpText = this.ExecerName;
            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.FK_Dept = this.ExecerDeptNo;
            wl.WarningDays = this.HisNode.WarningDays;
            wl.SDT = DataType.CurrentDataTime;
            wl.DTOfWarning = DataType.CurrentData;
            wl.RDT = DataType.CurrentDataTime;

            try
            {
                wl.Save();
            }
            catch
            {
                wl.CheckPhysicsTable();
                wl.Update();
            }
            #endregion
        }

        /// <summary>
        /// ִ�н���ǰ�����ڵ������copy��Rpt����ȥ.
        /// </summary>
        public void DoCopyCurrentWorkDataToRpt()
        {
            /* ���������һ�¾ͷ���..*/
            // �ѵ�ǰ�Ĺ�����Ա��������ȥ.
            string str = rptGe.GetValStrByKey(GERptAttr.FlowEmps);
            if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDOnly)
            {
                if (str.Contains("@" + this.Execer) == false)
                    rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + this.Execer);
            }

            if (Glo.UserInfoShowModel == UserInfoShowModel.UserNameOnly)
            {
                if (str.Contains("@" + WebUser.Name) == false)
                    rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + this.ExecerName);
            }

            if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
            {
                if (str.Contains("@" + this.Execer + "," + this.ExecerName) == false)
                    rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + this.Execer + "," + this.ExecerName);
            }

            rptGe.FlowEnder = this.Execer;
            rptGe.FlowEnderRDT = DataType.CurrentDataTime;

            rptGe.FlowDaySpan = DataType.GetSpanDays(this.rptGe.GetValStringByKey(GERptAttr.FlowStartRDT), DataType.CurrentDataTime);
            if (this.HisNode.IsEndNode || this.IsStopFlow)
                rptGe.WFState = WFState.Complete;
            else
                rptGe.WFState = WFState.Runing;

            if (this.HisWork.EnMap.PhysicsTable == this.HisFlow.PTable)
            {
                rptGe.DirectUpdate();
            }
            else
            {
                /*����ǰ�����Ը��Ƶ�rpt������ȥ.*/
                DoCopyRptWork(this.HisWork);
                rptGe.DirectUpdate();
            }
        }
        /// <summary>
        /// ִ������copy.
        /// </summary>
        /// <param name="fromWK"></param>
        public void DoCopyRptWork(Work fromWK)
        {
            foreach (Attr attr in fromWK.EnMap.Attrs)
            {
                switch (attr.Key)
                {
                    case BP.WF.Data.GERptAttr.FK_NY:
                    case BP.WF.Data.GERptAttr.FK_Dept:
                    case BP.WF.Data.GERptAttr.FlowDaySpan:
                    case BP.WF.Data.GERptAttr.FlowEmps:
                    case BP.WF.Data.GERptAttr.FlowEnder:
                    case BP.WF.Data.GERptAttr.FlowEnderRDT:
                    case BP.WF.Data.GERptAttr.FlowEndNode:
                    case BP.WF.Data.GERptAttr.FlowStarter:
                    case BP.WF.Data.GERptAttr.Title:
                    case BP.WF.Data.GERptAttr.WFSta:
                        continue;
                    default:
                        break;
                }

                object obj = fromWK.GetValByKey(attr.Key);
                if (obj == null)
                    continue;
                this.rptGe.SetValByKey(attr.Key, obj);
            }
            if (this.HisNode.IsStartNode)
                this.rptGe.SetValByKey("Title", fromWK.GetValByKey("Title"));
        }
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="at">����</param>
        /// <param name="toEmp">����Ա</param>
        /// <param name="toEmpName">����Ա����</param>
        /// <param name="toNDid">���ڵ�</param>
        /// <param name="toNDName">���ڵ�����</param>
        /// <param name="msg">��Ϣ</param>
        public void AddToTrack(ActionType at, string toEmp, string toEmpName, int toNDid, string toNDName, string msg)
        {
            AddToTrack(at, toEmp, toEmpName, toNDid, toNDName, msg, this.HisNode);
        }
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="at">����</param>
        /// <param name="toEmp">����Ա</param>
        /// <param name="toEmpName">����Ա����</param>
        /// <param name="toNDid">���ڵ�</param>
        /// <param name="toNDName">���ڵ�����</param>
        /// <param name="msg">��Ϣ</param>
        public void AddToTrack(ActionType at, string toEmp, string toEmpName, int toNDid, string toNDName, string msg, Node ndFrom)
        {
            Track t = new Track();
            t.WorkID = this.HisWork.OID;
            t.FID = this.HisWork.FID;
            t.RDT = DataType.CurrentDataTimess;
            t.HisActionType = at;

            t.NDFrom = ndFrom.NodeID;
            t.NDFromT = ndFrom.Name;

            t.EmpFrom = this.Execer;
            t.EmpFromT = this.ExecerName;
            t.FK_Flow = this.HisNode.FK_Flow;


            if (toNDid == 0)
            {
                toNDid = this.HisNode.NodeID;
                toNDName = this.HisNode.Name;
            }


            t.NDTo = toNDid;
            t.NDToT = toNDName;

            t.EmpTo = toEmp;
            t.EmpToT = toEmpName;
            t.Msg = msg;

            switch (at)
            {
                case ActionType.Forward:
                case ActionType.Start:
                case ActionType.UnSend:
                case ActionType.ForwardFL:
                case ActionType.ForwardHL:
                    //�ж��Ƿ��н����ֶΣ�����оͰ�����¼����־�
                    if (this.HisNode.FocusField.Length > 1)
                    {
                        string exp = this.HisNode.FocusField;
                        if (this.rptGe != null)
                            exp = Glo.DealExp(exp, this.rptGe, null);
                        else
                            exp = Glo.DealExp(exp, this.HisWork, null);

                        t.Msg += exp;
                        if (t.Msg.Contains("@"))
                            Log.DebugWriteError("@�ڽڵ�(" + this.HisNode.NodeID + " �� " + this.HisNode.Name + ")�����ֶα�ɾ����,���ʽΪ:" + this.HisNode.FocusField + " �滻�Ľ��Ϊ:" + t.Msg);
                    }
                    break;
                default:
                    break;
            }

            if (at == ActionType.Forward)
            {
                if (this.HisNode.IsFL)
                    at = ActionType.ForwardFL;
            }

            try
            {
                // t.MyPK = t.WorkID + "_" + t.FID + "_"  + t.NDFrom + "_" + t.NDTo +"_"+t.EmpFrom+"_"+t.EmpTo+"_"+ DateTime.Now.ToString("yyMMddHHmmss");
                t.Insert();
            }
            catch
            {
                t.CheckPhysicsTable();
            }
        }
        /// <summary>
        /// �����Ƿ�����Ϣ
        /// </summary>
        /// <param name="gwls"></param>
        public void SendMsgToThem(GenerWorkerLists gwls)
        {
            if (BP.WF.Glo.IsEnableSysMessage == false)
                return;

            //#region �ж��Ƿ���Է���.
            //bool isSendEmail = false;
            //bool isSendSMS = false;
            //MsgCtrl mc = this.HisNode.MsgCtrl;
            //switch (this.HisNode.MsgCtrl)
            //{
            //    case MsgCtrl.BySet:
            //        if (this.HisNode.MsgIsSend == false)
            //            return;
            //        if (this.HisNode.MsgMailEnable == false 
            //            && this.HisNode.MsgSMSEnable == false)
            //            return;

            //        isSendEmail = this.HisNode.MsgMailEnable;
            //        isSendSMS = this.HisNode.MsgSMSEnable;
            //        break;
            //    case MsgCtrl.ByFrmIsSendMsg:
            //        try
            //        {
            //            /*�ӱ��ֶ���ȡ����.*/
            //            if (this.HisWork.Row.ContainsKey("IsSendEmail") == true)
            //                isSendEmail = this.HisWork.GetValBooleanByKey("IsSendEmail");
            //            if (this.HisWork.Row.ContainsKey("IsSendSMS") == true)
            //                isSendSMS = this.HisWork.GetValBooleanByKey("IsSendSMS");

            //            if (isSendEmail == false || isSendSMS == false)
            //                return;
            //        }
            //        catch
            //        {
            //            if (this.HisWork.Row.ContainsKey("IsSendEmail") == false || this.HisWork.Row.ContainsKey("IsSendSMS") == false)
            //                throw new Exception("û����ccform����յ�IsSendEmail�� IsSendSMS ����.");
            //        }
            //        break;
            //    case MsgCtrl.BySDK:
            //        try
            //        {
            //            if (this.HisWork.GetValBooleanByKey("IsSendMsg") == false)
            //                return;
            //        }
            //        catch
            //        {
            //            if (this.HisWork.Row.ContainsKey("IsSendMsg") == false)
            //                throw new Exception("û�н��յ�IsSendMsg����.");
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //#endregion �ж��Ƿ���Է���.

            //// ȡ��ģ���ļ�.
            //string hostUrl = Glo.HostURL;
            //string mailDoc = this.HisNode.MsgMailDoc;
            //string mailEnd = "<a href='{0}'>�ü�����򿪹���</a>,��ַ:{0}.";
            //string mailTitle = this.HisNode.MsgMailTitle;
            //string msgTemp = this.HisNode.MsgSMSDoc;

            //foreach (GenerWorkerList wl in gwls)
            //{
            //    if (wl.IsEnable == false)
            //        continue;

            //    // �ʼ�����.
            //    string title = "";
            //    if (string.IsNullOrEmpty(mailTitle))
            //        title = string.Format("����:{0}.����:{1},������:{2},����:{3},��������.",this.HisNode.FlowName, wl.FK_NodeText, this.ExecerName, this.rptGe.Title);
            //    else
            //        title = Glo.DealExp(mailTitle, this.HisWork, null);

            //    //�ʼ�����.
            //    string sid = wl.FK_Emp + "_" + wl.WorkID + "_" + wl.FK_Node + "_" + wl.RDT;
            //    string url = hostUrl + "WF/Do.aspx?DoType=OF&SID=" + sid;
            //    url = url.Replace("//", "/");
            //    url = url.Replace("//", "/");
            //    mailDoc = Glo.DealExp(mailDoc, this.HisWork,null);
            //    mailDoc += "\t\n " + string.Format(mailEnd.Clone().ToString(), url);

            //    // ������Ϣ.
            //    if (string.IsNullOrEmpty(msgTemp) == true)
            //        msgTemp = "�¹���" + this.rptGe.Title + "������:" + WebUser.No + "," + WebUser.Name + ",����:" + this.HisFlow.Name;
            //    else
            //        msgTemp = Glo.DealExp(msgTemp, this.HisWork, null);


            //    BP.WF.Dev2Interface.Port_SendMsg(wl.FK_Emp, title, mailDoc,
            //        "WKAlt" + wl.FK_Node + "_" + wl.WorkID, BP.Sys.SMSMsgType.ToDo, wl.FK_Flow, wl.FK_Node, wl.WorkID, wl.FID);
            //}
        }
        /// <summary>
        /// ����ǰ������״̬��
        /// </summary>
        private WFState SendNodeWFState = WFState.Blank;
        /// <summary>
        /// �����ڵ��Ƿ�ȫ����ɣ�
        /// </summary>
        private bool IsOverMGECheckStand = false;
        private bool _IsStopFlow = false;
        private bool IsStopFlow
        {
            get
            {
                return _IsStopFlow;
            }
            set
            {
                _IsStopFlow = value;
                if (_IsStopFlow == true)
                {
                    this.rptGe.WFState = WFState.Complete;
                    this.rptGe.Update("WFState", (int)WFState.Complete);
                }
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        private void CheckCompleteCondition_IntCompleteEmps()
        {
            string sql = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerlist WHERE WorkID=" + this.WorkID + " AND IsEnable=1";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            string emps = "@";
            string flowEmps = "@";
            foreach (DataRow dr in dt.Rows)
            {
                if (emps.Contains("@" + dr[0].ToString() + "@"))
                    continue;

                emps = emps + dr[0].ToString() + "@";
                flowEmps = flowEmps + dr[1] + "," + dr[0].ToString() + "@";
            }

            // �����Ǹ�ֵ.
            this.rptGe.FlowEmps = flowEmps;
            this.HisGenerWorkFlow.Emps = emps;
        }
        /// <summary>
        /// ������̡��ڵ���������
        /// </summary>
        /// <returns></returns>
        private void CheckCompleteCondition()
        {
            // ִ�г�ʼ����Ա.
            this.CheckCompleteCondition_IntCompleteEmps();

            this.IsStopFlow = false;
            if (this.HisNode.IsEndNode)
            {
                /* ���������� */
                CCWork cc = new CCWork(this);
                // �����������ǰ������Ϣ����������WF_GenerWorkerlist��ɾ���ˡ�
                if (Glo.IsEnableSysMessage)
                    this.DoRefFunc_Listens();

                this.rptGe.WFState = WFState.Complete;

                string msg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "�����Ѿ��ߵ����һ���ڵ㣬���̳ɹ�������", this.HisNode, this.rptGe);
                this.addMsg(SendReturnMsgFlag.End, msg);

                this.IsStopFlow = true;
                this.HisGenerWorkFlow.WFState = WFState.Complete;
                return;
            }

            #region �жϽڵ��������
            this.addMsg(SendReturnMsgFlag.OverCurr, string.Format("��ǰ����[{0}]�Ѿ����", this.HisNode.Name));
            #endregion

            #region �ж���������.
            try
            {
                if (this.HisNode.HisToNodes.Count == 0 && this.HisNode.IsStartNode)
                {
                    // �����������ǰ������Ϣ����������WF_GenerWorkerlist��ɾ���ˡ�
                    if (Glo.IsEnableSysMessage)
                        this.DoRefFunc_Listens();

                    /* ���������� */
                    this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������", this.HisNode, this.rptGe);
                    this.IsStopFlow = true;
                    this.addMsg(SendReturnMsgFlag.OneNodeSheetver, "�����Ѿ��ɹ�����(һ�����̵Ĺ���)��",
                        "�����Ѿ��ɹ�����(һ�����̵Ĺ���)�� @�鿴<img src='" + VirPath + "WF/Img/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "' target='_self' >�����켣</a>", SendReturnMsgType.Info);
                    return;
                }

                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    // �����������ǰ������Ϣ����������WF_GenerWorkerlist��ɾ���ˡ�
                    if (Glo.IsEnableSysMessage)
                        this.DoRefFunc_Listens();

                    string stopMsg = this.HisFlowCompleteConditions.ConditionDesc;
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������:" + stopMsg, this.HisNode, this.rptGe);
                    this.IsStopFlow = true;

                    // string path = BP.Sys.Glo.Request.ApplicationPath;
                    this.addMsg(SendReturnMsgFlag.MacthFlowOver, "@���Ϲ��������������" + stopMsg + "" + overMsg,
                        "@���Ϲ��������������" + stopMsg + "" + overMsg + " @�鿴<img src='" + VirPath + "WF/Img/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "' target='_self' >�����켣</a>", SendReturnMsgType.Info);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("@�ж�����{0}����������ִ���." + ex.Message, this.HisNode.Name));
            }
            #endregion

        }

        #region ��������ڵ�

        /// <summary>
        /// ����Ϊʲô���͸�����
        /// </summary>
        /// <param name="fNodeID"></param>
        /// <param name="toNodeID"></param>
        /// <returns></returns>
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "";
            //return "@<a href='WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >" + this.ToE("WN20", "ΪʲôҪ���͸����ǣ�") + "</a>";
        }
        /// <summary>
        /// ��������ID
        /// </summary>
        public static Int64 FID = 0;
        /// <summary>
        /// û��FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithOutFID(Node nd)
        {
            throw new Exception("δ���:StartNextWorkNodeHeLiu_WithOutFID");
        }
        /// <summary>
        /// ������߳���������˶�
        /// </summary>
        /// <param name="nd"></param>
        private void NodeSend_53_UnSameSheet_To_HeLiu(Node nd)
        {

            Work heLiuWK = nd.HisWork;
            heLiuWK.OID = this.HisWork.FID;
            heLiuWK.RetrieveFromDBSources(); //��ѯ��������.

            heLiuWK.Copy(this.HisWork); // ִ��copy.

            heLiuWK.OID = this.HisWork.FID;
            heLiuWK.FID = 0;

            this.town = new WorkNode(heLiuWK, nd);

            //�����ڵ��ϵĹ��������ߡ�
            GenerWorkerLists gwls = new GenerWorkerLists(this.HisWork.FID, nd.NodeID);
            GenerFH myfh = new GenerFH(this.HisWork.FID);

            if (myfh.FK_Node == nd.NodeID && gwls.Count != 0)
            {
                /* ˵�����ǵ�һ�ε�����ڵ�������, 
                 * ���磺һ�����̣�
                 * A����-> B���߳� -> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ�Ѿ�������һ���̵߳����C.
                 */

                /* 
                 * ����:�������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
                 * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ����������Լ����Ѿ���ɵ�״̬.
                 */

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1 WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID AND FK_Node=" + dbStr + "FK_Node";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

                this.HisGenerWorkFlow.FK_Node = nd.NodeID;
                this.HisGenerWorkFlow.NodeName = nd.Name;

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region ���������
                Nodes fromNds = nd.FromNodes;
                string nearHLNodes = "";
                foreach (Node mynd in fromNds)
                {
                    if (mynd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                        nearHLNodes += "," + mynd.NodeID;
                }
                nearHLNodes = nearHLNodes.Substring(1);

                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node IN (" + nearHLNodes + ") AND FID=" + dbStr + "FID AND IsPass=1 AND IsEnable=1";
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];

                // �����߳�����.
                ps = new Paras();
                ps.SQL = "SELECT DISTINCT(WorkID) FROM WF_GenerWorkerList WHERE FK_Node IN (" + nearHLNodes + ") AND FID=" + dbStr + "FID AND IsPass=1 AND IsEnable=1";
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_thread = BP.DA.DBAccess.RunSQLReturnTable(ps);
                decimal ok = (decimal)dt_thread.Rows.Count;

                ps = new Paras();
                ps.SQL = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE IsEnable=1 AND FID=" + dbStr + "FID AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
                ps.Add("FID", this.HisWork.FID);
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(ps);
                decimal passRate = ok / all * 100;
                numStr += "@���ǵ�(" + ok + ")����˽ڵ��ϵĴ����ˣ���������(" + all + ")�������̡�";
                if (nd.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID ";
                    ps.Add("FK_Node", nd.NodeID);
                    ps.Add("WorkID", this.HisWork.FID);
                    DBAccess.RunSQL(ps);

                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkFlow SET   FK_Node=" + dbStr + "FK_Node WHERE  WorkID=" + dbStr + "WorkID ";
                    ps.Add("FK_Node", nd.NodeID);
                    ps.Add("WorkID", this.HisWork.FID);
                    DBAccess.RunSQL(ps);

                    numStr += "@��һ������(" + nd.Name + ")�Ѿ�������";
                }
                #endregion ���������

                if (myfh.ToEmpsMsg.Contains("("))
                {
                    string FK_Emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('('));
                    this.AddToTrack(ActionType.ForwardHL, FK_Emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);
                }
                this.addMsg("ToHeLiuInfo",
                    "@�����Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr);
            }
            else
            {
                // ˵����һ�ε�������ڵ㡣
                gwls = this.Func_GenerWorkerLists(this.town);
            }

            string FK_Emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (GenerWorkerList wl in gwls)
            {
                toEmpsStr += BP.WF.Glo.DealUserInfoShowModel(wl.FK_Emp, wl.FK_EmpText);
                if (gwls.Count == 1)
                    emps = wl.FK_Emp;
                else
                    emps += "@" + FK_Emp;
            }


            /* 
            * �������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
            * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
            */

            // ���ø�����״̬ ���õ�ǰ�Ľڵ�Ϊ:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);
            #region ��������ڵ�����ݡ�


            #region ������������. edit 2014-11-20 ��������������.
            //���Ƶ�ǰ�ڵ������.
            heLiuWK.FID = 0;
            heLiuWK.Rec = FK_Emp;
            heLiuWK.Emps = emps;
            heLiuWK.OID = this.HisWork.FID;
            heLiuWK.DirectUpdate(); //�ڸ���һ��.

            /* �����ݸ��Ƶ�rpt���ݱ���. */
            this.rptGe.OID = this.HisWork.FID;
            this.rptGe.RetrieveFromDBSources();
            this.rptGe.Copy(this.HisWork);
            this.rptGe.DirectUpdate();

            #endregion ������������.

            #region ���Ƹ�����
            if (this.HisNode.MapData.FrmAttachments.Count != 0)
            {
                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (athDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + idx;
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                           "ND" + nd.NodeID);
                        athDB_N.RefPKVal = this.HisWork.FID.ToString();
                        athDB_N.Save();
                    }
                }
            }
            #endregion ���Ƹ�����

            #region ����EleDB��
            if (this.HisNode.MapData.FrmEles.Count != 0)
            {
                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (eleDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);

                        eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                        eleDB_N.Save();
                    }
                }
            }
            #endregion ����EleDB��

            // ��������������ϸ������.
            this.GenerHieLiuHuiZhongDtlData_2013(nd);

            #endregion ��������ڵ������

            /* ��������Ҫ�ȴ�����������ȫ�����������ܿ�������*/
            string info = "";
            string sql1 = "";
#warning ���ڶ���ֺ�������ܻ������⡣
            ps = new Paras();
            ps.SQL = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + dbStr + "FID AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
            ps.Add("FID", this.HisWork.FID);
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(ps);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0,FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("WorkID", this.HisWork.FID);
                DBAccess.RunSQL(ps);

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("NodeName", nd.Name);
                ps.Add("WorkID", this.HisWork.FID);
                DBAccess.RunSQL(ps);

                info = "@��һ��������(" + nd.Name + ")�Ѿ�������";
            }
            else
            {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3,FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);
            }

            this.HisGenerWorkFlow.FK_Node = nd.NodeID;
            this.HisGenerWorkFlow.NodeName = nd.Name;

            //ps = new Paras();
            //ps.SQL = "UPDATE WF_GenerWorkFlow SET  WFState=" + (int)WFState.Runing + ", FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID;
            //ps.Add("FK_Node", nd.NodeID);
            //ps.Add("NodeName", nd.Name);
            //ps.Add("WorkID", this.HisWork.FID);
            //DBAccess.RunSQL(ps);
            this.addMsg(SendReturnMsgFlag.VarAcceptersID, emps, SendReturnMsgType.SystemMsg);

            if (myfh.FK_Node != nd.NodeID)
            {
                this.addMsg("HeLiuInfo",
                    "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��@���ǵ�һ������˽ڵ�Ĵ�����." + info);
            }
            else
            {
                this.addMsg("HeLiuInfo", "@��һ���Ĺ���������[" + emps + "]" + info, SendReturnMsgType.Info);
            }
        }
        /// <summary>
        /// ����������������
        /// �����̵߳��ӱ��������ݷŵ�������Ĵӱ���ȥ
        /// </summary>
        /// <param name="nd"></param>
        private void GenerHieLiuHuiZhongDtlData_2013(Node ndOfHeLiu)
        {
            MapDtls mydtls = ndOfHeLiu.HisWork.HisMapDtls;
            foreach (MapDtl dtl in mydtls)
            {
                if (dtl.IsHLDtl == false)
                    continue;

                GEDtl geDtl = dtl.HisGEDtl;
                geDtl.Copy(this.HisWork);
                geDtl.RefPK = this.HisWork.FID.ToString(); // RefPK ���ǵ�ǰ���̵߳�FID.
                geDtl.Rec = this.Execer;
                geDtl.RDT = DataType.CurrentDataTime;

                #region �ж��Ƿ�����������
                if (ndOfHeLiu.IsEval)
                {
                    /*�����������������*/
                    geDtl.SetValByKey(WorkSysFieldAttr.EvalEmpNo, this.Execer);
                    geDtl.SetValByKey(WorkSysFieldAttr.EvalEmpName, this.ExecerName);
                    geDtl.SetValByKey(WorkSysFieldAttr.EvalCent, 0);
                    geDtl.SetValByKey(WorkSysFieldAttr.EvalNote, "");
                }
                #endregion

                try
                {
                    geDtl.InsertAsOID(this.HisWork.OID);
                }
                catch
                {
                    geDtl.Update();
                }
                break;
            }
        }
        /// <summary>
        /// ���߳̽ڵ�
        /// </summary>
        private string _SpanSubTheadNodes = null;
        /// <summary>
        /// ��ȡ���������֮������߳̽ڵ㼯��.
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        private string SpanSubTheadNodes(Node toHLNode)
        {
            _SpanSubTheadNodes = "";
            SpanSubTheadNodes_DiGui(toHLNode.FromNodes);
            if (_SpanSubTheadNodes == "")
                throw new Exception("��ȡ�ֺ���֮������߳̽ڵ㼯��Ϊ�գ�����������ƣ��ڷֺ���֮��Ľڵ��������Ϊ���߳̽ڵ㡣");
            _SpanSubTheadNodes = _SpanSubTheadNodes.Substring(1);
            return _SpanSubTheadNodes;

        }
        private void SpanSubTheadNodes_DiGui(Nodes subNDs)
        {
            foreach (Node nd in subNDs)
            {
                if (nd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                {
                    //�ж��Ƿ��Ѿ���������Ȼ������ѭ��
                    if (_SpanSubTheadNodes.Contains("," + nd.NodeID))
                        continue;

                    _SpanSubTheadNodes += "," + nd.NodeID;
                    SpanSubTheadNodes_DiGui(nd.FromNodes);
                }
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        private Work _HisWork = null;
        /// <summary>
        /// ����
        /// </summary>
        public Work HisWork
        {
            get
            {
                return this._HisWork;
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        private Node _HisNode = null;
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node HisNode
        {
            get
            {
                return this._HisNode;
            }
        }
        private RememberMe HisRememberMe = null;
        public RememberMe GetHisRememberMe(Node nd)
        {
            if (HisRememberMe == null || HisRememberMe.FK_Node != nd.NodeID)
            {
                HisRememberMe = new RememberMe();
                HisRememberMe.FK_Emp = this.Execer;
                HisRememberMe.FK_Node = nd.NodeID;
                HisRememberMe.RetrieveFromDBSources();
            }
            return this.HisRememberMe;
        }
        private WorkFlow _HisWorkFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public WorkFlow HisWorkFlow
        {
            get
            {
                if (_HisWorkFlow == null)
                    _HisWorkFlow = new WorkFlow(this.HisNode.HisFlow, this.HisWork.OID, this.HisWork.FID);
                return _HisWorkFlow;
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�Ĺ����ǲ�����ɡ�
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (this.HisGenerWorkFlow.WFState == WFState.Complete)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����һ�������ڵ�����.
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <param name="nodeId">�ڵ�ID</param>
        public WorkNode(Int64 workId, int nodeId)
        {
            this.WorkID = workId;
            Node nd = new Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workId;
            int i = wk.RetrieveFromDBSources();
            if (i == 0)
            {
                this.rptGe = nd.HisFlow.HisGERpt;
                if (wk.FID != 0)
                    this.rptGe.OID = wk.FID;
                else
                    this.rptGe.OID = this.WorkID;

                this.rptGe.RetrieveFromDBSources();
                wk.Row = rptGe.Row;
            }
            this._HisWork = wk;
            this._HisNode = nd;
        }
        public Hashtable SendHTOfTemp = null;
        public string title = null;
        /// <summary>
        /// ����һ�������ڵ�����
        /// </summary>
        /// <param name="wk">����</param>
        /// <param name="nd">�ڵ�</param>
        public WorkNode(Work wk, Node nd)
        {
            this.WorkID = wk.OID;
            this._HisWork = wk;
            this._HisNode = nd;
        }
        #endregion

        #region ��������
        private void Repair()
        {
        }
        public WorkNode GetPreviousWorkNode_FHL(Int64 workid)
        {
            Nodes nds = this.HisNode.FromNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisRunModel == RunModel.SubThread)
                {
                    Work wk = nd.HisWork;
                    wk.OID = workid;
                    if (wk.RetrieveFromDBSources() != 0)
                    {
                        WorkNode wn = new WorkNode(wk, nd);
                        return wn;
                    }
                }
            }

            //WorkNodes wns = this.GetPreviousWorkNodes_FHL();
            //foreach (WorkNode wn in wns)
            //{
            //    if (wn.HisWork.OID == workid)
            //        return wn;
            //}
            return null;
        }
        public WorkNodes GetPreviousWorkNodes_FHL()
        {
            // ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
            if (this.HisNode.IsStartNode)
                throw new Exception("@�˽ڵ��ǿ�ʼ�ڵ�,û����һ������"); //�˽ڵ��ǿ�ʼ�ڵ�,û����һ������.

            if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkHL
               || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
            {
            }
            else
            {
                throw new Exception("@��ǰ������ - ���Ƿֺ����ڵ㡣");
            }

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.FromNodes;
            foreach (Node nd in nds)
            {
                Works wks = (Works)nd.HisWorks;
                wks.Retrieve(WorkAttr.FID, this.HisWork.OID);

                if (wks.Count == 0)
                    continue;

                foreach (Work wk in wks)
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    wns.Add(wn);
                }
            }
            return wns;
        }
        /// <summary>
        /// �õ�������һ������
        /// 1, �ӵ�ǰ���ҵ�������һ�������Ľڵ㼯��.		 
        /// ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
        /// </summary>
        /// <returns>�õ�������һ������</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + string.Format("�˽ڵ��ǿ�ʼ�ڵ�,û����һ������")); //�˽ڵ��ǿ�ʼ�ڵ�,û����һ������.

            string sql = "";

            //���ݵ�ǰ�ڵ��ȡ��һ���ڵ㣬���ù��Ǹ��˷��͵�
            sql = "SELECT NDFrom FROM ND" + int.Parse(this.HisNode.FK_Flow) + "Track WHERE WorkID=" + this.WorkID
                                                                                        + " AND NDTo='" + this.HisNode.NodeID
                                                                                        + "' AND ActionType=1 ORDER BY RDT DESC";
            int nodeid = DBAccess.RunSQLReturnValInt(sql, 0);
            if (nodeid == 0)
            {
                switch (this.HisNode.HisRunModel)
                {
                    case RunModel.HL:
                    case RunModel.FHL:
                        sql = "SELECT NDFrom FROM ND" + int.Parse(this.HisNode.FK_Flow) + "Track WHERE WorkID=" + this.WorkID
                                                                                       + " AND NDTo='" + this.HisNode.NodeID
                                                                                       + "' AND ActionType=" + (int)ActionType.ForwardHL + " ORDER BY RDT DESC";
                        break;
                    case RunModel.SubThread:
                        sql = "SELECT NDFrom FROM ND" + int.Parse(this.HisNode.FK_Flow) + "Track WHERE WorkID=" + this.WorkID
                                                                                       + " AND NDTo=" + this.HisNode.NodeID + " "
                                                                                       + " AND ActionType=" + (int)ActionType.SubFlowForward + " ORDER BY RDT DESC";
                        if(DBAccess.RunSQLReturnCOUNT(sql)==0)
                            sql = "SELECT NDFrom FROM ND" + int.Parse(this.HisNode.FK_Flow) + "Track WHERE WorkID=" + this.HisWork.FID
                                                                                      + " AND NDTo=" + this.HisNode.NodeID + " "
                                                                                      + " AND ActionType=" + (int)ActionType.SubFlowForward + " ORDER BY RDT DESC";

                        break;
                    default:
                        break;
                }
                nodeid = DBAccess.RunSQLReturnValInt(sql, 0);
            }
            if (nodeid == 0)
                throw new Exception("@����û���ҵ���һ���ڵ㡣" + sql);

            Node nd = new Node(nodeid);
            Work wk = nd.HisWork;
            wk.OID = this.WorkID;
            wk.RetrieveFromDBSources();

            WorkNode wn = new WorkNode(wk, nd);
            return wn;


            //WorkNodes wns = new WorkNodes();
            //Nodes nds = this.HisNode.FromNodes;
            //foreach (Node nd in nds)
            //{
            //    switch (this.HisNode.HisNodeWorkType)
            //    {
            //        case NodeWorkType.WorkHL: /* ����Ǻ��� */
            //            if (this.IsSubFlowWorkNode == false)
            //            {
            //                /* ��������߳� */
            //                Node pnd = nd.HisPriFLNode;
            //                if (pnd == null)
            //                    throw new Exception("@û��ȡ��������һ����ķ����ڵ㣬��ȷ������Ƿ����");

            //                Work wk1 = (Work)pnd.HisWorks.GetNewEntity;
            //                wk1.OID = this.HisWork.OID;
            //                if (wk1.RetrieveFromDBSources() == 0)
            //                    continue;
            //                WorkNode wn11 = new WorkNode(wk1, pnd);
            //                return wn11;
            //                break;
            //            }
            //            break;
            //        default:
            //            break;
            //    }

            //    Work wk = (Work)nd.HisWorks.GetNewEntity;
            //    wk.OID = this.HisWork.OID;
            //    if (wk.RetrieveFromDBSources() == 0)
            //        continue;

            //    string table = "ND" + int.Parse(this.HisNode.FK_Flow) + "Track";
            //    string actionSQL = "SELECT EmpFrom,EmpFromT,RDT FROM " + table + " WHERE WorkID=" + this.WorkID + " AND NDFrom=" + nd.NodeID + " AND ActionType=" + (int)ActionType.Forward;
            //    DataTable dt = DBAccess.RunSQLReturnTable(actionSQL);
            //    if (dt.Rows.Count == 0)
            //        continue;

            //    wk.Rec = dt.Rows[0]["EmpFrom"].ToString();
            //    wk.RecText = dt.Rows[0]["EmpFromT"].ToString();
            //    wk.SetValByKey("RDT", dt.Rows[0]["RDT"].ToString());

            //    WorkNode wn = new WorkNode(wk, nd);
            //    wns.Add(wn);
            //}
            //switch (wns.Count)
            //{
            //    case 0:
            //        throw new Exception("û���ҵ�������һ��������ϵͳ������֪ͨ����Ա��������������һ�������˳������͡������ñ����ع���Ա�û���½=�����칤��=�����̲�ѯ=���ڹؼ���������Workid��������ѡ��ȫ������ѯ��������ɾ������ @WorkID=" + this.WorkID);
            //    case 1:
            //        return (WorkNode)wns[0];
            //    default:
            //        break;
            //}
            //Node nd1 = wns[0].HisNode;
            //Node nd2 = wns[1].HisNode;
            //if (nd1.FromNodes.Contains(NodeAttr.NodeID, nd2.NodeID))
            //{
            //    return wns[0];
            //}
            //else
            //{
            //    return wns[1];
            //}
        }
        #endregion
    }
    /// <summary>
    /// �����ڵ㼯��.
    /// </summary>
    public class WorkNodes : CollectionBase
    {
        #region ����
        /// <summary>
        /// ���Ĺ���s
        /// </summary> 
        public Works GetWorks
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@��ʼ��ʧ�ܣ�û���ҵ��κνڵ㡣");

                Works ens = this[0].HisNode.HisWorks;
                ens.Clear();

                foreach (WorkNode wn in this)
                {
                    ens.AddEntity(wn.HisWork);
                }
                return ens;
            }
        }
        /// <summary>
        /// �����ڵ㼯��
        /// </summary>
        public WorkNodes()
        {
        }

        public int GenerByFID(Flow flow, Int64 fid)
        {
            this.Clear();

            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisRunModel == RunModel.SubThread)
                    continue;

                Work wk = nd.GetWork(fid);
                if (wk == null)
                    continue;


                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public int GenerByWorkID2014_01_06(Flow flow, Int64 oid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                Work wk = nd.GetWork(oid);
                if (wk == null)
                    continue;
                string table = "ND" + int.Parse(flow.No) + "Track";
                string actionSQL = "SELECT EmpFrom,EmpFromT,RDT FROM " + table + " WHERE WorkID=" + oid + " AND NDFrom=" + nd.NodeID + " AND ActionType=" + (int)ActionType.Forward;
                DataTable dt = DBAccess.RunSQLReturnTable(actionSQL);
                if (dt.Rows.Count == 0)
                    continue;

                wk.Rec = dt.Rows[0]["EmpFrom"].ToString();
                wk.RecText = dt.Rows[0]["EmpFromT"].ToString();
                wk.SetValByKey("RDT", dt.Rows[0]["RDT"].ToString());
                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        public int GenerByWorkID(Flow flow, Int64 oid)
        {
            string table = "ND" + int.Parse(flow.No) + "Track";
            string actionSQL = "SELECT EmpFrom,EmpFromT,RDT,NDFrom FROM " + table + " WHERE WorkID=" + oid + " AND (ActionType=" + (int)ActionType.Forward + " OR ActionType=" + (int)ActionType.ForwardFL + " OR ActionType=" + (int)ActionType.ForwardHL + " OR ActionType=" + (int)ActionType.SubFlowForward + " ) ORDER BY RDT";
            DataTable dt = DBAccess.RunSQLReturnTable(actionSQL);

            string nds = "";
            foreach (DataRow dr in dt.Rows)
            {
                Node nd = new Node(int.Parse(dr["NDFrom"].ToString()));
                Work wk = nd.GetWork(oid);
                if (wk == null)
                    wk = nd.HisWork;

                // �����ظ�������.
                if (nds.Contains(nd.NodeID.ToString() + ",") == true)
                    continue;
                nds += nd.NodeID.ToString() + ",";


                wk.Rec = dr["EmpFrom"].ToString();
                wk.RecText = dr["EmpFromT"].ToString();
                wk.SetValByKey("RDT", dr["RDT"].ToString());
                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// ɾ����������
        /// </summary>
        public void DeleteWorks()
        {
            foreach (WorkNode wn in this)
            {
                if (wn.HisFlow.HisDataStoreModel != DataStoreModel.ByCCFlow)
                    return;
                wn.HisWork.Delete();
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ��WorkNode
        /// </summary>
        /// <param name="wn">���� �ڵ�</param>
        public void Add(WorkNode wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkNode this[int index]
        {
            get
            {
                return (WorkNode)this.InnerList[index];
            }
        }
        #endregion
    }
}
