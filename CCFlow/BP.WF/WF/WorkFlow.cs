using System;
using BP.En;
using BP.Web;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;
using BP.WF.Template;
using BP.WF.Data;

namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵����
    /// ������
    /// �����������������
    /// ��������Ϣ��
    /// ���̵���Ϣ��
    /// </summary>
    public class WorkFlow
    {
        #region ��ǰ����ͳ����Ϣ
        /// <summary>
        /// ������Χ�����еĸ�����
        /// </summary>
        public static int NumOfRuning(string FK_Emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_Emp='" + FK_Emp + "' AND WorkTimeState=0";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ���뾯�����޵ĸ���
        /// </summary>
        public static int NumOfAlert(string FK_Emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_Emp='" + FK_Emp + "' AND WorkTimeState=1";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ����
        /// </summary>
        public static int NumOfTimeout(string FK_Emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_Emp='" + FK_Emp + "' AND WorkTimeState=2";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        #endregion

        #region  Ȩ�޹���
        /// <summary>
        /// �ǲ����ܹ�����ǰ�Ĺ�����
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns>�ǲ����ܹ�����ǰ�Ĺ���</returns>
        public bool IsCanDoCurrentWork(string empId)
        {
            WorkNode wn = this.GetCurrentWorkNode();
            return BP.WF.Dev2Interface.Flow_IsCanDoCurrentWork(wn.HisNode.NodeID, wn.WorkID, empId);
            #region ʹ��dev2InterFace �е��㷨
            //return true;
            // �ҵ���ǰ�Ĺ����ڵ�

            // �ж��ǲ��ǿ�ʼ�����ڵ�..
            if (wn.HisNode.IsStartNode)
            {
                // ���������ж��ǲ��������Ȩ�ޡ�
                // return WorkFlow.IsCanDoWorkCheckByEmpStation(wn.HisNode.NodeID, empId);
                return true;
            }

            // �ж����Ĺ������ɵĹ�����.
            GenerWorkerLists gwls = new GenerWorkerLists(this.WorkID, wn.HisNode.NodeID);
            if (gwls.Count == 0)
            {
                //return true;
                //throw new Exception("@�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:����ID="+this.WorkID+",�ڵ�ID="+wn.HisNode.NodeID );
                throw new Exception("@�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:WorkID=" + this.WorkID + ",NodeID=" + wn.HisNode.NodeID);
            }

            foreach (GenerWorkerList en in gwls)
            {
                if (en.FK_Emp == empId)
                    return true;
            }
            return false;
            #endregion
        }
        #endregion

        #region ���̹�������
        /// <summary>
        /// ִ�в���
        /// Ӧ�ó���:��������ֺϵ㲵��ʱ
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="fk_node">�����صĽڵ�</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string DoReject(Int64 fid, int fk_node, string msg)
        {
            GenerWorkerList wl = new GenerWorkerList();
            int i = wl.Retrieve(GenerWorkerListAttr.FID, fid,
                GenerWorkerListAttr.WorkID, this.WorkID,
                GenerWorkerListAttr.FK_Node, fk_node);
            //if (i == 0)
            //    throw new Exception("ϵͳ����û���ҵ�Ӧ���ҵ������ݡ�");

            i = wl.Delete();
            //if (i == 0)
            //    throw new Exception("ϵͳ����û��ɾ��Ӧ��ɾ�������ݡ�");

            wl = new GenerWorkerList();
            i = wl.Retrieve(GenerWorkerListAttr.FID, fid,
                GenerWorkerListAttr.WorkID, this.WorkID,
                GenerWorkerListAttr.IsPass, 3);

            //if (i == 0)
            //    throw new Exception("ϵͳ�������ҵ��˻ص�ԭʼ���û���ҵ���");

            Node nd = new Node(fk_node);
            // ���µ�ǰ���̹��������õ�ǰ�Ľڵ㡣
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + fk_node + ", NodeName='" + nd.Name + "' WHERE WorkID=" + this.WorkID);

            wl.RDT = DataType.CurrentDataTime;
            wl.IsPass = false;
            wl.Update();

            return "�����Ѿ����ص�(" + wl.FK_Emp + " , " + wl.FK_EmpText + ")";
            // wl.HisNode
        }
        /// <summary>
        /// �߼�ɾ������
        /// </summary>
        /// <param name="msg">�߼�ɾ������ԭ�򣬿���Ϊ�ա�</param>
        public void DoDeleteWorkFlowByFlag(string msg)
        {
            try
            {
                //���ò����Ĺ�������Ϊ.
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
                gwf.WFState = BP.WF.WFState.Delete;
                gwf.Update();

                //��¼��־ ��л itdos and 888 , ��������bug.
                WorkNode wn = new WorkNode(WorkID, gwf.FK_Node);
                wn.AddToTrack(ActionType.DeleteFlowByFlag, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name,
                        msg);

                string sql = "UPDATE  " + this.HisFlow.PTable + " SET WFState=" + (int)WFState.Delete + " WHERE OID=" + this.WorkID;
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�߼�ɾ�����ִ���:" + ex.Message);
                throw new Exception("@�߼�ɾ�����ִ���:" + ex.Message);
            }
        }
        /// <summary>
        /// �ָ��߼�ɾ������
        /// </summary>
        /// <param name="msg">�ظ�ԭ��,����Ϊ��.</param>
        public void DoUnDeleteWorkFlowByFlag(string msg)
        {
            try
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=" + (int)WFState.Runing + " WHERE  WorkID=" + this.WorkID);

                //���ò����Ĺ�������Ϊ.
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
                gwf.WFState = BP.WF.WFState.Runing;
                gwf.Update();

                WorkNode wn = new WorkNode(WorkID, gwf.FK_Node);
                wn.AddToTrack(ActionType.UnDeleteFlowByFlag, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name,
                        msg);
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�߼�ɾ�����ִ���:" + ex.Message);
                throw new Exception("@�߼�ɾ�����ִ���:" + ex.Message);
            }
        }
        /// <summary>
        /// ɾ���Ѿ���ɵ�����
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <param name="workID">����ID</param>
        /// <param name="isDelSubFlow">�Ƿ�Ҫɾ��������</param>
        /// <param name="note">ɾ��ԭ��</param>
        /// <returns>ɾ����Ϣ</returns>
        public static string DoDeleteWorkFlowAlreadyComplete(string flowNo, Int64 workID, bool isDelSubFlow, string note)
        {
            Log.DebugWriteInfo("��ʼɾ������:���̱��:" + flowNo + "-WorkID:" + workID + "-" + ". �Ƿ�Ҫɾ��������:" + isDelSubFlow + ";ɾ��ԭ��:" + note);

            Flow fl = new Flow(flowNo);

            #region ��¼����ɾ����־
            GERpt rpt = new GERpt("ND" + int.Parse(flowNo) + "Rpt");
            rpt.SetValByKey(GERptAttr.OID, workID);
            rpt.Retrieve();
            WorkFlowDeleteLog log = new WorkFlowDeleteLog();
            log.OID = workID;
            try
            {
                log.Copy(rpt);
                log.DeleteDT = DataType.CurrentDataTime;
                log.OperDept = WebUser.FK_Dept;
                log.OperDeptName = WebUser.FK_DeptName;
                log.Oper = WebUser.No;
                log.DeleteNote = note;
                log.OID = workID;
                log.FK_Flow = flowNo;
                log.FK_FlowSort = fl.FK_FlowSort;
                log.InsertAsOID(log.OID);
            }
            catch (Exception ex)
            {
                log.CheckPhysicsTable();
                log.Delete();
                return ex.StackTrace;
            }
            #endregion ��¼����ɾ����־

            DBAccess.RunSQL("DELETE FROM ND" + int.Parse(flowNo) + "Track WHERE WorkID=" + workID);
            DBAccess.RunSQL("DELETE FROM " + fl.PTable + " WHERE OID=" + workID);
            DBAccess.RunSQL("DELETE FROM WF_CHEval WHERE  WorkID=" + workID); // ɾ�������������ݡ�

            string info = "";

            #region ������ɾ����Ϣ.
            string msg = "";
            try
            {
                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CCList WHERE WorkID=" + workID);

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + workID);
                // ɾ���˻�.
                DBAccess.RunSQL("DELETE FROM WF_ReturnWork WHERE WorkID=" + workID);
                // ɾ���ƽ�.
                // DBAccess.RunSQL("DELETE FROM WF_ForwardWork WHERE WorkID=" + workID);

                //ɾ�����Ĺ���.
                DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + workID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + workID + " OR FID=" + workID + " ) AND FK_Flow='" + flowNo + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + workID + " OR FID=" + workID + " ) AND FK_Flow='" + flowNo + "'");

                //ɾ�����нڵ��ϵ�����.
                Nodes nds = fl.HisNodes;
                foreach (Node nd in nds)
                {
                    try
                    {
                        DBAccess.RunSQL("DELETE FROM ND" + nd.NodeID + " WHERE OID=" + workID + " OR FID=" + workID);
                    }
                    catch (Exception ex)
                    {
                        msg += "@ delete data error " + ex.Message;
                    }
                }
                if (msg != "")
                {
                    Log.DebugWriteInfo(msg);
                }
            }
            catch (Exception ex)
            {
                string err = "@ɾ���������� Err " + ex.TargetSite;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
            info = "@ɾ������ɾ���ɹ�";
            #endregion ������ɾ����Ϣ.

            #region ɾ�������������������.
            if (isDelSubFlow)
            {
                GenerWorkFlows gwfs = new GenerWorkFlows();
                gwfs.Retrieve(GenerWorkFlowAttr.PWorkID, workID);
                foreach (GenerWorkFlow item in gwfs)
                    BP.WF.Dev2Interface.Flow_DoDeleteFlowByReal(item.FK_Flow, item.WorkID, true);
            }
            #endregion ɾ�������������������.

            BP.DA.Log.DefaultLogWriteLineInfo("@[" + fl.Name + "]���̱�[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]ɾ����WorkID[" + workID + "]��");
            return "�Ѿ���ɵ����̱���ɾ���ɹ�.";
        }
        /// <summary>
        /// ɾ�����߳�
        /// </summary>
        /// <returns>����ɾ�����.</returns>
        private string DoDeleteSubThread()
        {
            WorkNode wn = this.GetCurrentWorkNode();
            Emp empOfWorker = wn.HisWork.RecOfEmp;

            #region ������ɾ����Ϣ.
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ���̵� ID �����̱�� ���ִ���" + ex.Message);
            }

            try
            {
                // ɾ������������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CHEval WHERE WorkID=" + this.WorkID); // ɾ�������������ݡ�

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CCList WHERE WorkID=" + this.WorkID);

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + this.WorkID);
                // ɾ���˻�.
                DBAccess.RunSQL("DELETE FROM WF_ReturnWork WHERE WorkID=" + this.WorkID);
                // ɾ���ƽ�.
                // DBAccess.RunSQL("DELETE FROM WF_ForwardWork WHERE WorkID=" + this.WorkID);

                //ɾ�����Ĺ���.
                //DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");

                if (msg != "")
                    Log.DebugWriteInfo(msg);
            }
            catch (Exception ex)
            {
                string err = "@ɾ����������[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
            string info = "@ɾ������ɾ���ɹ�";
            #endregion ������ɾ����Ϣ.

            #region ���������ɾ������������ʵ����⡣
            if (1 == 2)
            {
                /* Ŀǰ��û�б�Ҫ����Ϊ�ڷ�����,���м�������ʵ�����. */
                string sql = "";
                /* 
                 * ȡ������ȡͣ����,û�л�ȡ��˵��û���κ����̵߳���������λ��.
                 */
                sql = "SELECT FK_Node FROM WF_GenerWorkerList WHERE WorkID=" + this.FID + " AND IsPass=3";
                int fk_node = DBAccess.RunSQLReturnValInt(sql, 0);
                if (fk_node != 0)
                {
                    /* ˵�����Ǵ�����״̬ */
                    Node nextNode = new Node(fk_node);
                    if (nextNode.PassRate > 0)
                    {
                        /* �ҵ��ȴ�����ڵ����һ���� */
                        Nodes priNodes = nextNode.FromNodes;
                        if (priNodes.Count != 1)
                            throw new Exception("@û��ʵ�������̲�ͬ�̵߳�����");

                        Node priNode = (Node)priNodes[0];

                        #region ���������
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + this.FID + " AND IsPass=1";
                        decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + this.FID;
                        decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        if (all == 0)
                        {
                            /*˵��:���е����̶߳���ɱ����, ��Ӧ���������̽�����*/
                            WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            info += "@���е����߳��Ѿ�������";
                            info += "@������������Ϣ��";
                            info += "@" + wf.DoFlowOver(ActionType.FlowOver, "���������̽���", null, null);
                        }

                        decimal passRate = ok / all * 100;
                        if (nextNode.PassRate <= passRate)
                        {
                            /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE IsPass=3  AND WorkID=" + this.FID + " AND FK_Node=" + fk_node);
                        }
                        #endregion ���������
                    }
                } /* �����д�����״̬�жϡ�*/

                if (fk_node == 0)
                {
                    /* ˵��:û���ҵ��ȴ����������ĺ����ڵ�. */
                    GenerWorkFlow gwf = new GenerWorkFlow(this.FID);
                    Node fND = new Node(gwf.FK_Node);
                    switch (fND.HisNodeWorkType)
                    {
                        case NodeWorkType.WorkHL: /*���������е�����������*/
                            break;
                        default:
                            ///* ���ɾ�����һ��������ʱҪ�Ѹ�����ҲҪɾ����*/
                            //sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" +this.HisGenerWorkFlow +" AND FID=" + this.FID;
                            //int num = DBAccess.RunSQLReturnValInt(sql);
                            //if (num == 0)
                            //{
                            //    /*˵��û���ӽ��̣���Ҫ���������ִ����ɡ�*/
                            //    WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            //    info += "@���е����߳��Ѿ�������";
                            //    info += "@������������Ϣ��";
                            //    info += "@" + wf.DoFlowOver(ActionType.FlowOver, "�����̽���");
                            //}
                            break;
                    }
                }
            }
            #endregion

            #region д��ɾ����־.
            wn.AddToTrack(ActionType.DeleteSubThread, empOfWorker.No, empOfWorker.Name,
             wn.HisNode.NodeID,
             wn.HisNode.Name, "���̱߳�:" + BP.Web.WebUser.Name + "ɾ��.");
            #endregion д��ɾ����־.

            return "���̱߳�ɾ���ɹ�.";
        }
        /// <summary>
        /// ɾ���Ѿ���ɵ�����
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <param name="workid">����ID</param>
        /// <param name="isDelSubFlow">�Ƿ�ɾ��������</param>
        /// <returns>ɾ��������׳��쳣</returns>
        public static void DeleteFlowByReal(string flowNo, Int64 workid, bool isDelSubFlow)
        {
            BP.WF.Flow fl = new Flow(flowNo);
            //��������Ƿ���ɣ����û����ɾ͵���workflow����ɾ��.
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = workid;
            if (gwf.RetrieveFromDBSources() != 0)
            {
                if (gwf.WFState != WFState.Complete)
                {
                    WorkFlow wf = new WorkFlow(flowNo, workid);
                    wf.DoDeleteWorkFlowByReal(isDelSubFlow);
                    return;
                }
            }

            #region ɾ�����̱�������.
            FrmNodes fns = new FrmNodes();
            fns.Retrieve(FrmNodeAttr.FK_Flow, flowNo);
            string strs = "";
            foreach (FrmNode nd in fns)
            {
                if (strs.Contains("@" + nd.FK_Frm) == true)
                    continue;

                strs += "@" + nd.FK_Frm + "@";
                try
                {
                    MapData md = new MapData(nd.FK_Frm);
                    DBAccess.RunSQL("DELETE FROM " + md.PTable + " WHERE OID=" + workid);
                }
                catch
                {
                }
            }
            #endregion ɾ�����̱�������.

            //ɾ����������.
            DBAccess.RunSQL("DELETE FROM ND" + int.Parse(flowNo) + "Track WHERE WorkID=" + workid);
            DBAccess.RunSQL("DELETE FROM " + fl.PTable + " WHERE OID=" + workid);
            DBAccess.RunSQL("DELETE FROM WF_CHEval WHERE  WorkID=" + workid); // ɾ�������������ݡ�

            #region ������ɾ����Ϣ.
            BP.DA.Log.DefaultLogWriteLineInfo("@[" + fl.Name + "]���̱�[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]ɾ����WorkID[" + workid + "]��");
            string msg = "";

            // ɾ��������Ϣ.
            DBAccess.RunSQL("DELETE FROM WF_CCList WHERE WorkID=" + workid);
            // ɾ��������Ϣ.
            DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + workid);
            // ɾ���˻�.
            DBAccess.RunSQL("DELETE FROM WF_ReturnWork WHERE WorkID=" + workid);

            //ɾ�����Ĺ���.
            DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + workid + " AND FK_Flow='" + flowNo + "'");
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + workid + " OR FID=" + workid + " ) AND FK_Flow='" + flowNo + "'");
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + workid + " OR FID=" + workid + " ) AND FK_Flow='" + flowNo + "'");

            //ɾ�����нڵ��ϵ�����.
            Nodes nds = new Nodes(flowNo); // this.HisFlow.HisNodes;
            foreach (Node nd in nds)
            {
                try
                {
                    DBAccess.RunSQL("DELETE FROM ND" + nd.NodeID + " WHERE OID=" + workid + " OR FID=" + workid);
                }
                catch (Exception ex)
                {
                    msg += "@ delete data error " + ex.Message;
                }
            }
            if (msg != "")
            {
                Log.DebugWriteInfo(msg);
            }
            #endregion ������ɾ����Ϣ.
        }
        /// <summary>
        /// ɾ�����߳�
        /// </summary>
        /// <returns>ɾ������Ϣ</returns>
        public string DoDeleteSubThread2015()
        {
            if (this.FID == 0)
                throw new Exception("@�����̷����߳�����ʵ��������ִ�и÷�����");


            #region ������ɾ����Ϣ.
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ���̵� ID �����̱�� ���ִ���" + ex.Message);
            }

            try
            {
                // ɾ������������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CHEval WHERE WorkID=" + this.WorkID); // ɾ�������������ݡ�

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CCList WHERE WorkID=" + this.WorkID);

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + this.WorkID);
                // ɾ���˻�.
                DBAccess.RunSQL("DELETE FROM WF_ReturnWork WHERE WorkID=" + this.WorkID);
                // ɾ���ƽ�.
                // DBAccess.RunSQL("DELETE FROM WF_ForwardWork WHERE WorkID=" + this.WorkID);

                //ɾ�����Ĺ���.
                //DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE WorkID=" + this.WorkID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE WorkID=" + this.WorkID);

                if (msg != "")
                    Log.DebugWriteInfo(msg);
            }
            catch (Exception ex)
            {
                string err = "@ɾ����������[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
            string info = "@ɾ������ɾ���ɹ�";
            #endregion ������ɾ����Ϣ.

            #region ���������ɾ������������ʵ����⡣
            if (1 == 2)
            {
#warning Ӧ��ɾ��һ�����߳Ǻ󣬾���Ҫ��������ʵ����⡣����ĿǰӦ�õ��ó�������,��Ϊɾ�����߳ǵĶ�����1�������㡣2�������㡣�ܹ�������������Ϣ��˵���Ѿ�������������ˡ�

                /* Ŀǰ��û�б�Ҫ����Ϊ�ڷ�����,���м�������ʵ�����. */
                string sql = "";
                /* 
                 * ȡ������ȡͣ����,û�л�ȡ��˵��û���κ����̵߳���������λ��.
                 */
                sql = "SELECT FK_Node FROM WF_GenerWorkerList WHERE WorkID=" + this.FID + " AND IsPass=3";
                int fk_node = DBAccess.RunSQLReturnValInt(sql, 0);
                if (fk_node != 0)
                {
                    /* ˵�����Ǵ�����״̬ */
                    Node nextNode = new Node(fk_node);
                    if (nextNode.PassRate > 0)
                    {
                        /* �ҵ��ȴ�����ڵ����һ���� */
                        Nodes priNodes = nextNode.FromNodes;
                        if (priNodes.Count != 1)
                            throw new Exception("@û��ʵ�������̲�ͬ�̵߳�����");

                        Node priNode = (Node)priNodes[0];

                        #region ���������
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + this.FID + " AND IsPass=1";
                        decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + this.FID;
                        decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        if (all == 0)
                        {
                            /*˵��:���е����̶߳���ɱ����, ��Ӧ���������̽�����*/
                            WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            info += "@���е����߳��Ѿ�������";
                            info += "@������������Ϣ��";
                            info += "@" + wf.DoFlowOver(ActionType.FlowOver, "���������̽���", null, null);
                        }

                        decimal passRate = ok / all * 100;
                        if (nextNode.PassRate <= passRate)
                        {
                            /* ˵��: ȫ������Ա������ˣ����ú�������ʾ����*/
                            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE IsPass=3  AND WorkID=" + this.FID + " AND FK_Node=" + fk_node);
                        }
                        #endregion ���������
                    }
                } /* �����д�����״̬�жϡ�*/

                if (fk_node == 0)
                {
                    /* ˵��:û���ҵ��ȴ����������ĺ����ڵ�. */
                    GenerWorkFlow gwf = new GenerWorkFlow(this.FID);
                    Node fND = new Node(gwf.FK_Node);
                    switch (fND.HisNodeWorkType)
                    {
                        case NodeWorkType.WorkHL: /*���������е�����������*/
                            break;
                        default:
                            ///* ���ɾ�����һ��������ʱҪ�Ѹ�����ҲҪɾ����*/
                            //sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" +this.HisGenerWorkFlow +" AND FID=" + this.FID;
                            //int num = DBAccess.RunSQLReturnValInt(sql);
                            //if (num == 0)
                            //{
                            //    /*˵��û���ӽ��̣���Ҫ���������ִ����ɡ�*/
                            //    WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            //    info += "@���е����߳��Ѿ�������";
                            //    info += "@������������Ϣ��";
                            //    info += "@" + wf.DoFlowOver(ActionType.FlowOver, "�����̽���");
                            //}
                            break;
                    }
                }
            }
            #endregion


            return "���̱߳�ɾ���ɹ�.";

        }

        /// <summary>
        /// ���׵�ɾ������
        /// </summary>
        /// <param name="isDelSubFlow">�Ƿ�Ҫɾ��������</param>
        /// <returns>ɾ������Ϣ</returns>
        public string DoDeleteWorkFlowByReal(bool isDelSubFlow)
        {
            if (this.FID != 0)
                return DoDeleteSubThread2015();

            string info = "";
            WorkNode wn = null;
            try
            {
                wn = this.GetCurrentWorkNode();
            }
            catch (Exception ex)
            {
            }
            // ����ɾ��ǰ�¼���
            if (wn != null)
                wn.HisFlow.DoFlowEventEntity(EventListOfNode.BeforeFlowDel, wn.HisNode, wn.HisWork, null);

            #region ɾ�����̱�������.
            FrmNodes fns = new FrmNodes();
            fns.Retrieve(FrmNodeAttr.FK_Flow, this.HisFlow.No);
            string strs = "";
            foreach (FrmNode nd in fns)
            {
                if (strs.Contains("@" + nd.FK_Frm) == true)
                    continue;

                strs += "@" + nd.FK_Frm + "@";
                try
                {
                    MapData md = new MapData(nd.FK_Frm);
                    DBAccess.RunSQL("DELETE FROM " + md.PTable + " WHERE OID=" + this.WorkID);
                }
                catch
                {
                }
            }
            #endregion ɾ�����̱�������.

            //ɾ����������.
            DBAccess.RunSQL("DELETE FROM ND" + int.Parse(this.HisFlow.No) + "Track WHERE WorkID=" + this.WorkID);
            DBAccess.RunSQL("DELETE FROM " + this.HisFlow.PTable + " WHERE OID=" + this.WorkID);
            DBAccess.RunSQL("DELETE FROM WF_CHEval WHERE  WorkID=" + this.WorkID); // ɾ�������������ݡ�

            #region ������ɾ����Ϣ.
            BP.DA.Log.DefaultLogWriteLineInfo("@[" + this.HisFlow.Name + "]���̱�[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]ɾ����WorkID[" + this.WorkID + "]��");
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ���̵� ID �����̱�� ���ִ���" + ex.Message);
            }

            try
            {
                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CCList WHERE WorkID=" + this.WorkID);
                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + this.WorkID);
                // ɾ���˻�.
                DBAccess.RunSQL("DELETE FROM WF_ReturnWork WHERE WorkID=" + this.WorkID);

                //ɾ�����Ĺ���.
                DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");

                //ɾ�����нڵ��ϵ�����.
                Nodes nds = this.HisFlow.HisNodes;
                foreach (Node nd in nds)
                {
                    try
                    {
                        DBAccess.RunSQL("DELETE FROM ND" + nd.NodeID + " WHERE OID=" + this.WorkID + " OR FID=" + this.WorkID);
                    }
                    catch (Exception ex)
                    {
                        msg += "@ delete data error " + ex.Message;
                    }
                }
                if (msg != "")
                {
                    Log.DebugWriteInfo(msg);
                }
            }
            catch (Exception ex)
            {
                string err = "@ɾ����������[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
            info = "@ɾ������ɾ���ɹ�";
            #endregion ������ɾ����Ϣ.

            #region ���������ɾ������������ʵ����⡣
            if (this.FID != 0)
            {
                string sql = "";
                /* 
                 * ȡ������ȡͣ����,û�л�ȡ��˵��û���κ����̵߳���������λ��.
                 */
                sql = "SELECT FK_Node FROM WF_GenerWorkerList WHERE WorkID=" + wn.HisWork.FID + " AND IsPass=3";
                int fk_node = DBAccess.RunSQLReturnValInt(sql, 0);
                if (fk_node != 0)
                {
                    /* ˵�����Ǵ�����״̬ */
                    Node nextNode = new Node(fk_node);
                    if (nextNode.PassRate > 0)
                    {
                        /* �ҵ��ȴ�����ڵ����һ���� */
                        Nodes priNodes = nextNode.FromNodes;
                        if (priNodes.Count != 1)
                            throw new Exception("@û��ʵ�������̲�ͬ�̵߳�����");

                        Node priNode = (Node)priNodes[0];

                        #region ���������
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + wn.HisWork.FID + " AND IsPass=1";
                        decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + wn.HisWork.FID;
                        decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        if (all == 0)
                        {
                            /*˵��:���е����̶߳���ɱ����, ��Ӧ���������̽�����*/
                            WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            info += "@���е����߳��Ѿ�������";
                            info += "@������������Ϣ��";
                            info += "@" + wf.DoFlowOver(ActionType.FlowOver, "���������̽���", null, null);
                        }

                        decimal passRate = ok / all * 100;
                        if (nextNode.PassRate <= passRate)
                        {
                            /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE IsPass=3  AND WorkID=" + wn.HisWork.FID + " AND FK_Node=" + fk_node);
                        }
                        #endregion ���������
                    }
                } /* �����д�����״̬�жϡ�*/

                if (fk_node == 0)
                {
                    /* ˵��:û���ҵ��ȴ����������ĺ����ڵ�. */
                    GenerWorkFlow gwf = new GenerWorkFlow(this.FID);
                    Node fND = new Node(gwf.FK_Node);
                    switch (fND.HisNodeWorkType)
                    {
                        case NodeWorkType.WorkHL: /*���������е�����������*/
                            break;
                        default:
                            /* ���ɾ�����һ��������ʱҪ�Ѹ�����ҲҪɾ����*/
                            sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + wn.HisNode.NodeID + " AND FID=" + wn.HisWork.FID;
                            int num = DBAccess.RunSQLReturnValInt(sql);
                            if (num == 0)
                            {
                                /*˵��û���ӽ��̣���Ҫ���������ִ����ɡ�*/
                                WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                                info += "@���е����߳��Ѿ�������";
                                info += "@������������Ϣ��";
                                info += "@" + wf.DoFlowOver(ActionType.FlowOver, "�����̽���", null, null);
                            }
                            break;
                    }
                }
            }
            #endregion

            #region ɾ�������������������.
            if (isDelSubFlow)
            {
                GenerWorkFlows gwfs = new GenerWorkFlows();
                gwfs.Retrieve(GenerWorkFlowAttr.PWorkID, this.WorkID);

                foreach (GenerWorkFlow item in gwfs)
                    BP.WF.Dev2Interface.Flow_DoDeleteFlowByReal(item.FK_Flow, item.WorkID, true);
            }
            #endregion ɾ�������������������.


            // ����ɾ��hou�¼���
            if (wn != null)
                wn.HisFlow.DoFlowEventEntity(EventListOfNode.AfterFlowDel, wn.HisNode, wn.HisWork, null);
            return info;
        }

        /// <summary>
        /// ɾ���������̼�¼��־���������˶��켣.
        /// </summary>
        /// <param name="isDelSubFlow">�Ƿ�Ҫɾ��������</param>
        /// <returns></returns>
        public string DoDeleteWorkFlowByWriteLog(string info, bool isDelSubFlow)
        {
            GERpt rpt = new GERpt("ND" + int.Parse(this.HisFlow.No) + "Rpt", this.WorkID);
            WorkFlowDeleteLog log = new WorkFlowDeleteLog();
            log.OID = this.WorkID;
            try
            {
                log.Copy(rpt);
                log.DeleteDT = DataType.CurrentDataTime;
                log.OperDept = WebUser.FK_Dept;
                log.OperDeptName = WebUser.FK_DeptName;
                log.Oper = WebUser.No;
                log.DeleteNote = info;
                log.OID = this.WorkID;
                log.FK_Flow = this.HisFlow.No;
                log.InsertAsOID(log.OID);
                return DoDeleteWorkFlowByReal(isDelSubFlow);
            }
            catch (Exception ex)
            {
                log.CheckPhysicsTable();
                log.Delete();
                return ex.StackTrace;
            }
        }

        #region ���̵�ǿ����ֹ\ɾ�� ���߻ָ�ʹ������,
        /// <summary>
        /// �ָ�����.
        /// </summary>
        /// <param name="msg">�ظ����̵�ԭ��</param>
        public void DoComeBackWorkFlow(string msg)
        {
            try
            {
                //���ò����Ĺ�������Ϊ
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
                gwf.WFState = WFState.Runing;
                gwf.DirectUpdate();

                // ������Ϣ 
                WorkNode wn = this.GetCurrentWorkNode();
                GenerWorkerLists wls = new GenerWorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
                if (wls.Count == 0)
                    throw new Exception("@�ָ����̳��ִ���,�����Ĺ������б�");
                BP.WF.MsgsManager.AddMsgs(wls, "�ָ�������", wn.HisNode.Name, "�ظ�������");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�ָ����̳��ִ���." + ex.Message);
                throw new Exception("@�ָ����̳��ִ���." + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// �õ���ǰ�Ľ����еĹ�����
        /// </summary>
        /// <returns></returns>		 
        public WorkNode GetCurrentWorkNode()
        {
            int currNodeID = 0;
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.WorkID;
            if (gwf.RetrieveFromDBSources() == 0)
            {
                this.DoFlowOver(ActionType.FlowOver, "������������û���ҵ���ǰ�����̼�¼��", null, null);
                throw new Exception("@" + string.Format("��������{0}�Ѿ���ɡ�", this.HisStartWork.Title));
            }

            Node nd = new Node(gwf.FK_Node);
            Work work = nd.HisWork;
            work.OID = this.WorkID;
            work.NodeID = nd.NodeID;
            work.SetValByKey("FK_Dept", BP.Web.WebUser.FK_Dept);
            if (work.RetrieveFromDBSources() == 0)
            {
                Log.DefaultLogWriteLineError("@WorkID=" + this.WorkID + ",FK_Node=" + gwf.FK_Node + ".��Ӧ�ó��ֲ�ѯ����������."); // û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��
                work.Rec = BP.Web.WebUser.No;
                try
                {
                    work.Insert();
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError("@û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣" + ex.Message + ",��Ӧ�ó���"); // û���ҵ���ǰ�Ĺ����ڵ������
                }
            }
            work.FID = gwf.FID;

            WorkNode wn = new WorkNode(work, nd);
            return wn;
        }
        /// <summary>
        /// ���������Ľڵ�
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string DoFlowOverFeiLiu(GenerWorkFlow gwf)
        {
            // ��ѯ��������û����ɵ����̡�
            int i = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkFlow WHERE FID=" + gwf.FID + " AND WFState!=1");
            switch (i)
            {
                case 0:
                    throw new Exception("@��Ӧ�õĴ���");
                case 1:
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow  WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + gwf.FID);

                    StartWork wk = this.HisFlow.HisStartNode.HisWork as StartWork;
                    wk.OID = gwf.FID;
                    wk.Update();

                    return "@��ǰ�Ĺ����Ѿ���ɣ������������еĹ������Ѿ���ɡ�";
                default:
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1 WHERE WorkID=" + this.WorkID);
                    return "@��ǰ�Ĺ����Ѿ���ɡ�";
            }
        }
        /// <summary>
        /// �������������.
        /// </summary>
        /// <returns></returns>
        public string DoFlowSubOver()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            Node nd = new Node(gwf.FK_Node);

            DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow   WHERE WorkID=" + this.WorkID);
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.WorkID);

            string sql = "SELECT count(*) FROM WF_GenerWorkFlow WHERE  FID=" + this.FID;
            int num = DBAccess.RunSQLReturnValInt(sql);
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /*˵���������һ��*/
                WorkFlow wf = new WorkFlow(gwf.FK_Flow, this.FID);
                wf.DoFlowOver(ActionType.FlowOver, "�����̽���", null, null);
                return "@��ǰ����������ɣ�����������ɡ�";
            }
            else
            {
                return "@��ǰ����������ɣ������̻���(" + num + ")��������δ��ɡ�";
            }
        }
        /// <summary>
        /// �ø��������Զ����͵���һ������ȥ.
        /// </summary>
        public string LetParentFlowAutoSendNextSetp()
        {
            if (this.HisGenerWorkFlow.PWorkID == 0)
                return "";

            if (this.HisFlow.IsAutoSendSubFlowOver == false)
                return "";

            // ����Ƿ�������һ��.
            int num = BP.WF.Dev2Interface.Flow_NumOfSubFlowRuning(this.HisGenerWorkFlow.PWorkID, this.HisGenerWorkFlow.WorkID);
            if (num != 0)
                return "";

            //��鸸�����Ƿ����?
            GenerWorkFlow pGWF = new GenerWorkFlow();
            pGWF.WorkID = this.HisGenerWorkFlow.PWorkID;
            if (pGWF.RetrieveFromDBSources() == 0)
                return ""; // �����̱�ɾ����Ҳ����ִ�С�

            if (pGWF.WFState == WFState.Complete)
                return ""; //�������Ѿ����Ҳ����ִ��.

            //��鸸���̵ĵ�ǰͣ���Ľڵ��Ƿ��Ƿ��������̵Ľڵ㣿
            if (this.HisGenerWorkFlow.PNodeID != pGWF.FK_Node)
                return "";

            //�ҵ����ø����̵��ˣ������жϲ��Ͻ�������ж���˴���ýڵ㣬��ֻ���ҵ���ǰ�˴����ˡ�
            //  string pEmp = DBAccess.RunSQLReturnStringIsNull("SELECT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.HisGenerWorkFlow.PWorkID + " AND FK_Node=" + this.HisGenerWorkFlow.FK_Node + " AND IsPass=0", null);
            //NDXRptBaseAttr

            // ��Ϊǰ���Ѿ��������и�ֱ�Ӹ�������������Ҫ���в�ѯ֮����ִ�и���.
            this.HisGenerWorkFlow.RetrieveFromDBSources();

            try
            {
                //ȡ�õ��������̵���Ա.
                string pEmp = this.HisGenerWorkFlow.PEmp;
                if (string.IsNullOrEmpty(pEmp) == true)
                    throw new Exception("@û���ҵ����������̵Ĺ�����Ա.");
                Emp emp = new Emp();
                emp.No = pEmp;
                if (emp.RetrieveFromDBSources() == 0)
                    throw new Exception("@�����������ϵ���Ա���(" + pEmp + ")�Ѳ�����,�޷�����������.");

                //�ı䵱ǰ�ڵ��״̬����Ȼ������������������е������̷�����ɺ�������е����ú󣬲��ܲ������䷢����.
                this.HisGenerWorkFlow.WFState = WFState.Complete;
                this.HisGenerWorkFlow.DirectUpdate();


                GERpt rpt = new GERpt("ND" + int.Parse(this.HisFlow.No) + "Rpt", this.WorkID);


                // �õ�ǰ��Ա���·��ͣ��������ַ���һ����Ҫ��鷢��Ȩ�ޣ�����Ļ��ͳ����󣬲��ܷ�����ȥ.
                SendReturnObjs objs = BP.WF.Dev2Interface.Node_SendWork(this.HisGenerWorkFlow.PFlowNo, pGWF.WorkID, rpt.Row, null, 0, null,
                    emp.No, emp.Name, emp.FK_Dept, emp.FK_DeptText, null);

                this.HisGenerWorkFlow.WFState = WFState.Complete;
                this.HisGenerWorkFlow.DirectUpdate();

                return "@��ǰ�ڵ��������̵����һ������, �ɹ��ø��������е���һ���ڵ�." + objs.ToMsgOfHtml();
            }
            catch (Exception ex)
            {
                this.HisGenerWorkFlow.WFState = WFState.Complete;
                this.HisGenerWorkFlow.DirectUpdate();
                return "@�����һ����������ɺ��ø����̵Ľڵ��Զ�����ʱ�����ִ���:" + ex.Message;
            }
        }
        /// <summary>
        /// ִ���������
        /// </summary>
        /// <param name="at"></param>
        /// <param name="stopMsg"></param>
        /// <returns></returns>
        public string DoFlowOver(ActionType at, string stopMsg, Node currNode, GERpt rpt)
        {
            if(null == currNode){
                return null;
            }
            //���ý���ǰ�¼�.
            this.HisFlow.DoFlowEventEntity(EventListOfNode.FlowOverBefore, currNode, rpt, null);

            if (string.IsNullOrEmpty(stopMsg))
                stopMsg += "���̽���";

            string exp = currNode.FocusField;
            if (string.IsNullOrEmpty(exp) == false && exp.Length > 1)
            {
                if (rpt != null)
                    stopMsg += Glo.DealExp(exp, rpt, null);
            }

            string msg = "";
            if (this.IsMainFlow == false)
            {
                /* �������������*/
                return this.DoFlowSubOver();
            }

            #region ������ϸ��Ļ���.
            Node currND = new Node(this.HisGenerWorkFlow.FK_Node);

            //������ϸ���ݵ�copy���⡣ ���ȼ�飺��ǰ�ڵ㣨���ڵ㣩�Ƿ�����ϸ��
            MapDtls dtls = currND.MapData.MapDtls; // new MapDtls("ND" + nd.NodeID);
            int i = 0;
            foreach (MapDtl dtl in dtls)
            {
                i++;
                // ��ѯ������ϸ���е����ݡ�
                GEDtls dtlDatas = new GEDtls(dtl.No);
                dtlDatas.Retrieve(GEDtlAttr.RefPK, this.WorkID);

                GEDtl geDtl = null;
                try
                {
                    // ����һ��Rpt����
                    geDtl = new GEDtl("ND" + int.Parse(this.HisFlow.No) + "RptDtl" + i.ToString());
                    geDtl.ResetDefaultVal();
                }
                catch
                {
#warning �˴���Ҫ�޸���
                    continue;
                }
            }
            this._IsComplete = 1;
            #endregion ������ϸ��Ļ���.

            #region ���������ҵ��.

            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "DELETE FROM WF_GenerFH WHERE FID=" + dbstr + "FID";
            ps.Add(GenerFHAttr.FID, this.WorkID);
            DBAccess.RunSQL(ps);

            if (Glo.IsDeleteGenerWorkFlow == true)
            {
                // �Ƿ�ɾ������ע�������ݣ�
                ps = new Paras();
                ps.SQL = "DELETE FROM WF_GenerWorkFlow WHERE WorkID=" + dbstr + "WorkID1 OR FID=" + dbstr + "WorkID2 ";
                ps.Add("WorkID1", this.WorkID);
                ps.Add("WorkID2", this.WorkID);
                DBAccess.RunSQL(ps);
            }
            else
            {
                //���������,�Է����Ѿ���ɵĹ�����ѯ.
                ps = new Paras();
                ps.SQL = "SELECT EmpFrom FROM ND" + int.Parse(this._HisFlow.No) + "Track WHERE WorkID=" + dbstr + "WorkID OR FID=" + dbstr + "FID ";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.WorkID);
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string emps = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (emps.Contains("@" + dr[0].ToString()) == true)
                        continue;
                    emps += "@" + dr[0].ToString();
                }
                emps = emps + "@";

                //��������ע����Ϣ.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + dbstr + "WFState,WFSta=" + dbstr + "WFSta,Emps=" + dbstr + "Emps,MyNum=1 WHERE WorkID=" + dbstr + "WorkID ";
                ps.Add("WFState", (int)WFState.Complete);
                ps.Add("WFSta", (int)WFSta.Complete);
                ps.Add("Emps", emps);
                ps.Add("WorkID", this.WorkID);
                DBAccess.RunSQL(ps);
            }

            // ���������.
            ps = new Paras();
            ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + dbstr + "WorkID1 OR FID=" + dbstr + "WorkID2 ";
            ps.Add("WorkID1", this.WorkID);
            ps.Add("WorkID2", this.WorkID);
            DBAccess.RunSQL(ps);

            // �����������״̬.
            ps = new Paras();
            ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + dbstr + "WFState WHERE OID=" + dbstr + "OID";
            ps.Add("WFState", (int)WFState.Complete);
            ps.Add("OID", this.WorkID);
            DBAccess.RunSQL(ps);

            //����켣.
            WorkNode wn = new WorkNode(WorkID, this.HisGenerWorkFlow.FK_Node);
            wn.AddToTrack(at, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name,
                    stopMsg);

            //���ý������¼�.
            this.HisFlow.DoFlowEventEntity(EventListOfNode.FlowOverAfter, currNode, rpt, null);
            #endregion ���������ҵ��.

            //ִ�����һ�������̷��ͺ�ļ�飬�����Ƿ�ɹ�����Ҫ���������̡�
            msg += this.LetParentFlowAutoSendNextSetp();

            //   string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;

            #region �����������,����������������������е� ���ڵ㣬����Ա��
            ps = new Paras();
            ps.SQL = "UPDATE ND" + int.Parse(currNode.FK_Flow) + "Track SET NDTo=" + dbstr + "NDTo,NDToT=" + dbstr + "NDToT,EmpTo=" + dbstr + "EmpTo,EmpToT=" + dbstr + "EmpToT WHERE NDFrom=" + dbstr + "NDFrom AND EmpFrom=" + dbstr + "EmpFrom AND WorkID=" + dbstr + "WorkID AND ActionType=" + (int)ActionType.WorkCheck;
            ps.Add(TrackAttr.NDTo, currNode.NodeID);
            ps.Add(TrackAttr.NDToT, "");
            ps.Add(TrackAttr.EmpTo, "");
            ps.Add(TrackAttr.EmpToT, "");

            ps.Add(TrackAttr.NDFrom, currNode.NodeID);
            ps.Add(TrackAttr.EmpFrom, WebUser.No);
            ps.Add(TrackAttr.WorkID, this.WorkID);
            BP.DA.DBAccess.RunSQL(ps);
            #endregion �����������.


            //if (string.IsNullOrEmpty(msg) == true)
            //    msg = "���̳ɹ�����.";
            return msg;
        }
        public string GenerFHStartWorkInfo()
        {
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Title,RDT,Rec,OID FROM ND" + this.StartNodeID + " WHERE FID=" + this.FID);
            switch (dt.Rows.Count)
            {
                case 0:
                    Node nd = new Node(this.StartNodeID);
                    throw new Exception("@û���ҵ����ǿ�ʼ�ڵ�����ݣ������쳣��FID=" + this.FID + "���ڵ㣺" + nd.Name + "�ڵ�ID��" + nd.NodeID);
                case 1:
                    msg = string.Format("@�����ˣ� {0}  ���ڣ�{1} ��������� ���⣺{2} ���Ѿ��ɹ���ɡ�",
                        dt.Rows[0]["Rec"].ToString(), dt.Rows[0]["RDT"].ToString(), dt.Rows[0]["Title"].ToString());
                    break;
                default:
                    msg = "@����(" + dt.Rows.Count + ")λ��Ա����������Ѿ���ɡ�";
                    foreach (DataRow dr in dt.Rows)
                    {
                        msg += "<br>�����ˣ�" + dr["Rec"] + " �������ڣ�" + dr["RDT"] + " ���⣺" + dr["Title"] + "<a href='./../../WF/WFRpt.aspx?WorkID=" + dr["OID"] + "&FK_Flow=" + this.HisFlow.No + "' target=_blank>��ϸ...</a>";
                    }
                    break;
            }
            return msg;
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.HisFlow.No + "01");
            }
        }
        /// <summary>
        ///  ���͵�
        /// </summary>
        /// <param name="dt"></param>
        public string CCTo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return "";

            string emps = "";
            string empsExt = "";

            string ip = "127.0.0.1";
            System.Net.IPAddress[] addressList = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList;
            if (addressList.Length > 1)
                ip = addressList[1].ToString();
            else
                ip = addressList[0].ToString();


            foreach (DataRow dr in dt.Rows)
            {
                string no = dr[0].ToString();
                string name = dr[1].ToString();

                emps += BP.WF.Glo.DealUserInfoShowModel(no, name);
            }

            Paras pss = new Paras();
            pss.Add("Sender", BP.Web.WebUser.No);
            pss.Add("Receivers", emps);
            pss.Add("Title", "���������ͣ���������:" + this.HisFlow.Name + "��������ˣ�" + BP.Web.WebUser.Name);
            pss.Add("Context", "�����켣 http://" + ip + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=0");

            try
            {
                DBAccess.RunSP("CCstaff", pss);
                return "@" + empsExt;
            }
            catch (Exception ex)
            {
                return "@���ͳ��ִ���û�аѸ����̵���Ϣ���͵�(" + empsExt + ")����ϵ����Ա���ϵͳ�쳣" + ex.Message;
            }
        }
        /// <summary>
        /// ִ�ж���
        /// </summary>
        /// <param name="msg">����ԭ��</param>
        public string DoFix(string fixMsg)
        {
            if (this.HisGenerWorkFlow.WFState == WFState.Fix)
                throw new Exception("@��ǰ�Ѿ��Ƕ����״̬������ִ���ٶ���.");

            if (string.IsNullOrEmpty(fixMsg))
                fixMsg = "��";


            ///* ��ȡ���Ĺ����ߣ������Ƿ�����Ϣ��*/
            //GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.HisFlow.No);

            //string url = Glo.ServerIP + "/" + this.VirPath + this.AppType + "/WorkOpt/OneWork/Track.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + this.HisGenerWorkFlow.FID + "&FK_Node=" + this.HisGenerWorkFlow.FK_Node;
            //string mailDoc = "��ϸ��Ϣ:<A href='" + url + "'>�����̹켣</A>.";
            //string title = "����:" + this.HisGenerWorkFlow.Title + " ��" + WebUser.Name + "����" + fixMsg;
            //string emps = "";
            //foreach (GenerWorkerList wl in wls)
            //{
            //    if (wl.IsEnable == false)
            //        continue; //�����͸����õ��ˡ�

            //    emps += wl.FK_Emp + "," + wl.FK_EmpText + ";";

            //    //д����Ϣ��
            //    BP.WF.Dev2Interface.Port_SendMsg(wl.FK_Emp, title, mailDoc, "Fix" + wl.WorkID, BP.Sys.SMSMsgType.Etc, wl.FK_Flow, wl.FK_Node, wl.WorkID, wl.FID);
            //}

            /* ִ�� WF_GenerWorkFlow ����. */
            int sta = (int)WFState.Fix;
            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + dbstr + "WFState WHERE WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkFlowAttr.WFState, sta);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            // �������̱����״̬�� 
            ps = new Paras();
            ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + dbstr + "WFState WHERE OID=" + dbstr + "OID";
            ps.Add(GERptAttr.WFState, sta);
            ps.Add(GERptAttr.OID, this.WorkID);
            DBAccess.RunSQL(ps);

            // ��¼��־..
            WorkNode wn = new WorkNode(this.WorkID, this.HisGenerWorkFlow.FK_Node);

            //wn.AddToTrack(ActionType.Info, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name, fixMsg,);

            return "�Ѿ��ɹ�ִ�ж���";
        }
        /// <summary>
        /// ִ�н������
        /// </summary>
        /// <param name="msg">����ԭ��</param>
        public string DoUnFix(string unFixMsg)
        {
            if (this.HisGenerWorkFlow.WFState != WFState.Fix)
                throw new Exception("@��ǰ�Ƕ����״̬������ִ�н������.");

            if (string.IsNullOrEmpty(unFixMsg))
                unFixMsg = "��";


            ///* ��ȡ���Ĺ����ߣ������Ƿ�����Ϣ��*/
            //GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.HisFlow.No);

            //string url = Glo.ServerIP + "/" + this.VirPath + this.AppType + "/WorkOpt/OneWork/Track.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + this.HisGenerWorkFlow.FID + "&FK_Node=" + this.HisGenerWorkFlow.FK_Node;
            //string mailDoc = "��ϸ��Ϣ:<A href='" + url + "'>�����̹켣</A>.";
            //string title = "����:" + this.HisGenerWorkFlow.Title + " ��" + WebUser.Name + "����" + unFixMsg;
            //string emps = "";
            //foreach (GenerWorkerList wl in wls)
            //{
            //    if (wl.IsEnable == false)
            //        continue; //�����͸����õ��ˡ�

            //    emps += wl.FK_Emp + "," + wl.FK_EmpText + ";";

            //    //д����Ϣ��
            //    BP.WF.Dev2Interface.Port_SendMsg(wl.FK_Emp, title, mailDoc, "Fix" + wl.WorkID, BP.Sys.SMSMsgType.Self, wl.FK_Flow, wl.FK_Node, wl.WorkID, wl.FID);
            //}

            /* ִ�� WF_GenerWorkFlow ����. */
            int sta = (int)WFState.Runing;
            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + dbstr + "WFState WHERE WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkFlowAttr.WFState, sta);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            // �������̱����״̬�� 
            ps = new Paras();
            ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + dbstr + "WFState WHERE OID=" + dbstr + "OID";
            ps.Add(GERptAttr.WFState, sta);
            ps.Add(GERptAttr.OID, this.WorkID);
            DBAccess.RunSQL(ps);

            // ��¼��־..
            WorkNode wn = new WorkNode(this.WorkID, this.HisGenerWorkFlow.FK_Node);
            //wn.AddToTrack(ActionType.Info, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name, unFixMsg);

            return "�Ѿ��ɹ�ִ�н������:";
        }
        #endregion

        #region ��������
        /// <summary>
        /// ���Ľڵ�
        /// </summary>
        private Nodes _HisNodes = null;
        /// <summary>
        /// �ڵ�s
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    this._HisNodes = this.HisFlow.HisNodes;
                return this._HisNodes;
            }
        }
        /// <summary>
        /// �����ڵ�s(��ͨ�Ĺ����ڵ�)
        /// </summary>
        private WorkNodes _HisWorkNodesOfWorkID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfWorkID
        {
            get
            {
                if (this._HisWorkNodesOfWorkID == null)
                {
                    this._HisWorkNodesOfWorkID = new WorkNodes();
                    this._HisWorkNodesOfWorkID.GenerByWorkID(this.HisFlow, this.WorkID);
                }
                return this._HisWorkNodesOfWorkID;
            }
        }
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        private WorkNodes _HisWorkNodesOfFID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfFID
        {
            get
            {
                if (this._HisWorkNodesOfFID == null)
                {
                    this._HisWorkNodesOfFID = new WorkNodes();
                    this._HisWorkNodesOfFID.GenerByFID(this.HisFlow, this.FID);
                }
                return this._HisWorkNodesOfFID;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private Flow _HisFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                return this._HisFlow;
            }
        }
        private GenerWorkFlow _HisGenerWorkFlow = null;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID);
                return _HisGenerWorkFlow;
            }
            set
            {
                _HisGenerWorkFlow = value;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _WorkID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this._WorkID;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _FID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this._FID;
            }
            set
            {
                this._FID = value;
            }
        }
        /// <summary>
        /// �Ƿ��Ǹ���
        /// </summary>
        public bool IsMainFlow
        {
            get
            {
                if (this.FID != 0 && this.FID != this.WorkID)
                    return false;
                else
                    return true;
            }
        }
        #endregion

        #region ���췽��
        public WorkFlow(string fk_flow, Int64 wkid)
        {
            this.HisGenerWorkFlow = new GenerWorkFlow();
            this.HisGenerWorkFlow.RetrieveByAttr(GenerWorkerListAttr.WorkID, wkid);
            this._FID = this.HisGenerWorkFlow.FID;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            Flow flow = new Flow(fk_flow);
            this._HisFlow = flow;
            this._WorkID = wkid;

        }

        public WorkFlow(Flow flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = wkid;
            gwf.RetrieveFromDBSources();

            this._FID = gwf.FID;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        /// <summary>
        /// ����һ������������
        /// </summary>
        /// <param name="flow">����No</param>
        /// <param name="wkid">����ID</param>
        public WorkFlow(Flow flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        public WorkFlow(string FK_flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;

            Flow flow = new Flow(FK_flow);
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        #endregion

        #region ��������

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private StartWork _HisStartWork = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public StartWork HisStartWork
        {
            get
            {
                if (_HisStartWork == null)
                {
                    StartWork en = (StartWork)this.HisFlow.HisStartNode.HisWork;
                    en.OID = this.WorkID;
                    en.FID = this.FID;
                    if (en.RetrieveFromDBSources() == 0)
                        en.RetrieveFID();
                    _HisStartWork = en;
                }
                return _HisStartWork;
            }
        }
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        private WorkNode _HisStartWorkNode = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public WorkNode HisStartWorkNode
        {
            get
            {
                if (_HisStartWorkNode == null)
                {
                    Node nd = this.HisFlow.HisStartNode;
                    StartWork en = (StartWork)nd.HisWork;
                    en.OID = this.WorkID;
                    en.Retrieve();

                    WorkNode wn = new WorkNode(en, nd);
                    _HisStartWorkNode = wn;

                }
                return _HisStartWorkNode;
            }
        }
        #endregion

        #region ��������
        public int _IsComplete = -1;
        /// <summary>
        /// �ǲ������
        /// </summary>
        public bool IsComplete
        {
            get
            {

                //  bool s = !DBAccess.IsExits("select workid from WF_GenerWorkFlow WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                GenerWorkFlow generWorkFlow = new GenerWorkFlow(this.WorkID);
                if (generWorkFlow.WFState == WFState.Complete)
                    return true;
                else
                    return false;

            }
        }
        /// <summary>
        /// �ǲ������
        /// </summary>
        public string IsCompleteStr
        {
            get
            {
                if (this.IsComplete)
                    return "��";
                else
                    return "δ";
            }
        }
        #endregion

        #region ��̬����

        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="empId">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpStation(int nodeId, string empId)
        {
            bool isCan = false;
            // �жϸ�λ��Ӧ��ϵ�ǲ����ܹ�ִ��.
            string sql = "SELECT a.FK_Node FROM WF_NodeStation a,  "+BP.WF.Glo.EmpStation+" b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " AND b.FK_Emp='" + empId + "' )";
            isCan = DBAccess.IsExits(sql);
            if (isCan)
                return true;
            // �ж�������Ҫ������λ�ܲ���ִ����.
            sql = "select FK_Node from WF_NodeStation WHERE FK_Node=" + nodeId + " AND ( FK_Station in (select FK_Station from "+BP.WF.Glo.EmpStation+" WHERE FK_Emp='" + empId + "') ) ";
            return DBAccess.IsExits(sql);
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="dutyNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDuty(int nodeId, string dutyNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDuty  a,  Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty) AND (a.FK_Node=" + nodeId + " AND b.FK_Duty=" + dutyNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="DeptNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDept(int nodeId, string DeptNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDept  a,  Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND (a.FK_Node=" + nodeId + " AND b.FK_Dept=" + DeptNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���������ܹ������������Ա��
        /// </summary>
        /// <param name="nodeId">�ڵ�ID</param>		 
        /// <returns></returns>
        public static DataTable CanDoWorkEmps(int nodeId)
        {
            string sql = "select a.FK_Node, b.EmpID from WF_NodeStation  a,  "+BP.WF.Glo.EmpStation+" b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " )";
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// GetEmpsBy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Emps GetEmpsBy(DataTable dt)
        {
            // �γ��ܹ��������������û����Ρ�
            Emps emps = new Emps();
            foreach (DataRow dr in dt.Rows)
            {
                emps.AddEntity(new Emp(dr["EmpID"].ToString()));
            }
            return emps;
        }

        #endregion

        #region ���̷���

        private string _AppType = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string AppType
        {
            get
            {
                if (_AppType == null)
                {
                    if (BP.Sys.SystemConfig.IsBSsystem == false)
                    {
                        _AppType = "WF";
                    }
                    else
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
                }
                return _AppType;
            }
        }
        private string _VirPath = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string VirPath
        {
            get
            {
                if (_VirPath == null)
                {
                    if (BP.Sys.SystemConfig.IsBSsystem)
                        _VirPath = BP.Sys.Glo.Request.ApplicationPath;
                    else
                        _VirPath = "";
                }
                return _VirPath;
            }
        }
        /// <summary>
        /// ִ�й���
        /// </summary>
        /// <param name="way">����ʽ</param>
        /// <param name="relData">�ͷ�����</param>
        /// <param name="hungNote">����ԭ��</param>
        /// <returns></returns>
        public string DoHungUp(HungUpWay way, string relData, string hungNote)
        {
            if (this.HisGenerWorkFlow.WFState == WFState.HungUp)
                throw new Exception("@��ǰ�Ѿ��ǹ����״̬������ִ���ڹ���.");

            if (string.IsNullOrEmpty(hungNote))
                hungNote = "��";

            if (way == HungUpWay.SpecDataRel)
                if (relData.Length < 10)
                    throw new Exception("@�����������ڲ���ȷ(" + relData + ")");
            if (relData == null)
                relData = "";

            HungUp hu = new HungUp();
            hu.FK_Node = this.HisGenerWorkFlow.FK_Node;
            hu.WorkID = this.WorkID;
            hu.MyPK = hu.FK_Node + "_" + hu.WorkID;
            hu.HungUpWay = way; //����ʽ.
            hu.DTOfHungUp = DataType.CurrentDataTime; // ����ʱ��
            hu.Rec = BP.Web.WebUser.No;  //������
            hu.DTOfUnHungUp = relData; // �������ʱ�䡣
            hu.Note = hungNote;
            hu.Insert();

            /* ��ȡ���Ĺ����ߣ������Ƿ�����Ϣ��*/
            GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.HisFlow.No);
            string url = Glo.ServerIP + "/" + this.VirPath + this.AppType + "/WorkOpt/OneWork/Track.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + this.HisGenerWorkFlow.FID + "&FK_Node=" + this.HisGenerWorkFlow.FK_Node;
            string mailDoc = "��ϸ��Ϣ:<A href='" + url + "'>�����̹켣</A>.";
            string title = "����:" + this.HisGenerWorkFlow.Title + " ��" + WebUser.Name + "����" + hungNote;
            string emps = "";
            foreach (GenerWorkerList wl in wls)
            {
                if (wl.IsEnable == false)
                    continue; //�����͸����õ��ˡ�

                //BP.WF.Port.WFEmp emp = new Port.WFEmp(wl.FK_Emp);
                emps += wl.FK_Emp + "," + wl.FK_EmpText + ";";

                //д����Ϣ��
                BP.WF.Dev2Interface.Port_SendMsg(wl.FK_Emp, title, mailDoc, "HungUp" + wl.WorkID, BP.WF.SMSMsgType.Self, wl.FK_Flow, wl.FK_Node, wl.WorkID, wl.FID);
            }

            /* ִ�� WF_GenerWorkFlow ����. */
            int hungSta = (int)WFState.HungUp;
            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + dbstr + "WFState WHERE WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkFlowAttr.WFState, hungSta);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            // �������̱����״̬�� 
            ps = new Paras();
            ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + dbstr + "WFState WHERE OID=" + dbstr + "OID";
            ps.Add(GERptAttr.WFState, hungSta);
            ps.Add(GERptAttr.OID, this.WorkID);
            DBAccess.RunSQL(ps);

            // ���¹����ߵĹ���ʱ�䡣
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerlist SET DTOfHungUp=" + dbstr + "DTOfHungUp,DTOfUnHungUp=" + dbstr + "DTOfUnHungUp, HungUpTimes=HungUpTimes+1 WHERE FK_Node=" + dbstr + "FK_Node AND WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkerListAttr.DTOfHungUp, DataType.CurrentDataTime);
            ps.Add(GenerWorkerListAttr.DTOfUnHungUp, relData);

            ps.Add(GenerWorkerListAttr.FK_Node, this.HisGenerWorkFlow.FK_Node);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            // ��¼��־..
            WorkNode wn = new WorkNode(this.WorkID, this.HisGenerWorkFlow.FK_Node);
            wn.AddToTrack(ActionType.HungUp, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name, hungNote);
            return "�Ѿ��ɹ�ִ�й���,�����Ѿ�֪ͨ��:" + emps;
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <returns></returns>
        public string DoUnHungUp()
        {
            if (this.HisGenerWorkFlow.WFState != WFState.HungUp)
                throw new Exception("@�ǹ���״̬,�����ܽ������.");

            /* ִ�н������. */
            int sta = (int)WFState.Runing;
            string dbstr = BP.Sys.SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET WFState=" + dbstr + "WFState WHERE WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkFlowAttr.WFState, sta);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            // �������̱����״̬�� 
            ps = new Paras();
            ps.SQL = "UPDATE " + this.HisFlow.PTable + " SET WFState=" + dbstr + "WFState WHERE OID=" + dbstr + "OID";
            ps.Add(GERptAttr.WFState, sta);
            ps.Add(GERptAttr.OID, this.WorkID);
            DBAccess.RunSQL(ps);

            // ���¹����ߵĹ���ʱ�䡣
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerlist SET  DTOfUnHungUp=" + dbstr + "DTOfUnHungUp WHERE FK_Node=" + dbstr + "FK_Node AND WorkID=" + dbstr + "WorkID";
            ps.Add(GenerWorkerListAttr.DTOfUnHungUp, DataType.CurrentDataTime);
            ps.Add(GenerWorkerListAttr.FK_Node, this.HisGenerWorkFlow.FK_Node);
            ps.Add(GenerWorkFlowAttr.WorkID, this.WorkID);
            DBAccess.RunSQL(ps);

            //���� HungUp
            HungUp hu = new HungUp();
            hu.FK_Node = this.HisGenerWorkFlow.FK_Node;
            hu.WorkID = this.HisGenerWorkFlow.WorkID;
            hu.MyPK = hu.FK_Node + "_" + hu.WorkID;
            if (hu.RetrieveFromDBSources() == 0)
                throw new Exception("@ϵͳ����û���ҵ������");

            hu.DTOfUnHungUp = DataType.CurrentDataTime; // ����ʱ��
            hu.Update();

            //��������������
            ps = new Paras();
            ps.SQL = "UPDATE WF_HungUp SET MyPK=" + SystemConfig.AppCenterDBVarStr + "MyPK WHERE MyPK=" + dbstr + "MyPK1";
            ps.Add("MyPK", BP.DA.DBAccess.GenerGUID());
            ps.Add("MyPK1", hu.MyPK);
            DBAccess.RunSQL(ps);


            /* ��ȡ���Ĺ����ߣ������Ƿ�����Ϣ��*/
            GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.HisFlow.No);
            string url = Glo.ServerIP + "/" + this.VirPath + this.AppType + "/WorkOpt/OneWork/Track.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + this.HisGenerWorkFlow.FID + "&FK_Node=" + this.HisGenerWorkFlow.FK_Node;
            string mailDoc = "��ϸ��Ϣ:<A href='" + url + "'>�����̹켣</A>.";
            string title = "����:" + this.HisGenerWorkFlow.Title + " ��" + WebUser.Name + "�������.";
            string emps = "";
            foreach (GenerWorkerList wl in wls)
            {
                if (wl.IsEnable == false)
                    continue; //�����͸����õ��ˡ�

                emps += wl.FK_Emp + "," + wl.FK_EmpText + ";";

                //д����Ϣ��
                BP.WF.Dev2Interface.Port_SendMsg(wl.FK_Emp, title, mailDoc,
                    "HungUp" + wl.FK_Node + this.WorkID, BP.WF.SMSMsgType.Self, HisGenerWorkFlow.FK_Flow, HisGenerWorkFlow.FK_Node, this.WorkID, this.FID);

                //д����Ϣ��
                //Glo.SendMsg(wl.FK_Emp, title, mailDoc);
            }


            // ��¼��־..
            WorkNode wn = new WorkNode(this.WorkID, this.HisGenerWorkFlow.FK_Node);
            wn.AddToTrack(ActionType.UnHungUp, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name, "�������,�Ѿ�֪ͨ��:" + emps);
            return null;
        }
        /// <summary>
        /// �����ƽ�
        /// </summary>
        /// <returns></returns>
        public string DoUnShift()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            GenerWorkerLists wls = new GenerWorkerLists();
            wls.Retrieve(GenerWorkerListAttr.WorkID, this.WorkID, GenerWorkerListAttr.FK_Node, gwf.FK_Node);
            if (wls.Count == 0)
                return "�ƽ�ʧ��û�е�ǰ�Ĺ�����";

            Node nd = new Node(gwf.FK_Node);
            Work wk1 = nd.HisWork;
            wk1.OID = this.WorkID;
            wk1.Retrieve();

            // ��¼��־.
            WorkNode wn = new WorkNode(wk1, nd);
            wn.AddToTrack(ActionType.UnShift, WebUser.No, WebUser.Name, nd.NodeID, nd.Name, "�����ƽ�");

            if (wls.Count == 1)
            {
                GenerWorkerList wl = (GenerWorkerList)wls[0];
                wl.FK_Emp = WebUser.No;
                wl.FK_EmpText = WebUser.Name;
                wl.IsEnable = true;
                wl.IsPass = false;
                wl.Update();
                return "@�����ƽ��ɹ���<a href='" + Glo.CCFlowAppPath + "WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&FK_Node=" + wl.FK_Node + "&FID=" + wl.FID + "&WorkID=" + this.WorkID + "'><img src='" + Glo.CCFlowAppPath + "WF/Img/Btn/Do.gif' border=0/>ִ�й���</A>";
            }

            GenerWorkerList mywl = null;
            foreach (GenerWorkerList wl in wls)
            {
                if (wl.FK_Emp == WebUser.No)
                {
                    wl.FK_Emp = WebUser.No;
                    wl.FK_EmpText = WebUser.Name;
                    wl.IsEnable = true;
                    wl.IsPass = false;
                    wl.Update();
                    mywl = wl;
                }
                else
                {
                    wl.Delete();
                }
            }
            if (mywl != null)
                return "@�����ƽ��ɹ���<a href='" + Glo.CCFlowAppPath + "WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&FK_Node=" + mywl.FK_Node + "&FID=" + mywl.FID + "&WorkID=" + this.WorkID + "'><img src='" + Glo.CCFlowAppPath + "WF/Img/Btn/Do.gif' border=0/>ִ�й���</A>";

            GenerWorkerList wk = (GenerWorkerList)wls[0];
            GenerWorkerList wkNew = new GenerWorkerList();
            wkNew.Copy(wk);
            wkNew.FK_Emp = WebUser.No;
            wkNew.FK_EmpText = WebUser.Name;
            wkNew.IsEnable = true;
            wkNew.IsPass = false;
            wkNew.Insert();

            //ɾ��������Ϣ.
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_ShiftWork WHERE WorkID="+this.WorkID+" AND FK_Node="+wk.FK_Node);

            return "@�����ƽ��ɹ���<a href='" + Glo.CCFlowAppPath + "WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&FK_Node=" + wk.FK_Node + "&FID=" + wk.FID + "&WorkID=" + this.WorkID + "'><img src='" + Glo.CCFlowAppPath + "WF/Img/Btn/Do.gif' border=0/>ִ�й���</A>";
        }
        #endregion
    }
    /// <summary>
    /// �������̼���.
    /// </summary>
    public class WorkFlows : CollectionBase
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="flow">���̱��</param>
        public WorkFlows(Flow flow)
        {
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            ens.RetrieveAll(10000);
            foreach (StartWork sw in ens)
            {
                this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            }
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        public WorkFlows()
        {
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        /// <param name="flow">����</param>
        /// <param name="flowState">����ID</param> 
        public WorkFlows(Flow flow, int flowState)
        {
            //StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            //QueryObject qo = new QueryObject(ens);
            //qo.AddWhere(StartWorkAttr.WFState, flowState);
            //qo.DoQuery();
            //foreach (StartWork sw in ens)
            //{
            //    this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            //}
        }

        #endregion

        #region ��ѯ����
        /// <summary>
        /// GetNotCompleteNode
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <returns>StartWorks</returns>
        public static StartWorks GetNotCompleteWork(string flowNo)
        {
            return null;

            //Flow flow = new Flow(flowNo);
            //StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            //QueryObject qo = new QueryObject(ens);
            //qo.AddWhere(StartWorkAttr.WFState, "!=", 1);
            //qo.DoQuery();
            //return ens;

            /*
            foreach(StartWork sw in ens)
            {
                ens.AddEntity( new WorkFlow( flow, sw.OID) ) ; 
            }
            */
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ����������
        /// </summary>
        /// <param name="wn">��������</param>
        public void Add(WorkFlow wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkFlow this[int index]
        {
            get
            {
                return (WorkFlow)this.InnerList[index];
            }
        }
        #endregion

        #region ���ڵ��ȵ��Զ�����
        /// <summary>
        /// ������ڵ㡣
        /// ���ڵ�Ĳ����������û��Ƿ��Ĳ���������ϵͳ���ִ洢���ϣ���ɵ������еĵ�ǰ�����ڵ�û�й�����Ա���Ӷ�����������������ȥ��
        /// ������ڵ㣬���ǰ����Ƿŵ����ڵ㹤���������档
        /// </summary>
        /// <returns></returns>
        public static string ClearBadWorkNode()
        {
            string infoMsg = "������ڵ����Ϣ��";
            string errMsg = "������ڵ�Ĵ�����Ϣ��";
            return infoMsg + errMsg;
        }
        #endregion
    }
}
