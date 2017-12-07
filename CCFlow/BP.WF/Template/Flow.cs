using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using BP.DA;
using BP.Sys;
using BP.Port;
using BP.En;
using BP.WF.Template;
using BP.WF.Data;
using BP.Web;
using Microsoft.Win32;

namespace BP.WF
{
    /// <summary>
    /// ����
    /// ��¼�����̵���Ϣ��
    /// ���̵ı�ţ����ƣ�����ʱ�䣮
    /// </summary>
    public class Flow : BP.En.EntityNoName
    {
        #region ��������.
        /// <summary>
        /// ���ֵx
        /// </summary>
        public int MaxX
        {
            get
            {
                int i = this.GetParaInt("MaxX");
                if (i == 0)
                    this.GenerMaxXY();
                else
                    return i;
                return this.GetParaInt("MaxX");
            }
            set
            {
                this.SetPara("MaxX", value);
            }
        }
        /// <summary>
        /// ���ֵY
        /// </summary>
        public int MaxY
        {
            get
            {
                int i= this.GetParaInt("MaxY");
                if (i == 0)
                    this.GenerMaxXY();
                else
                    return i;

                return this.GetParaInt("MaxY");
            }
            set
            {
                this.SetPara("MaxY", value);
            }
        }
        private void GenerMaxXY()
        {
            //int x1 = BP.DA.DBAccess.RunSQLReturnValInt("SELECT MAX(X) FROM WF_Node WHERE FK_Flow='" + this.No + "'", 0);
            //int x2 = BP.DA.DBAccess.RunSQLReturnValInt("SELECT MAX(X) FROM WF_NodeLabelNode WHERE FK_Flow='" + this.No + "'", 0);
            //this.MaxY = BP.DA.DBAccess.RunSQLReturnValInt("SELECT MAX(Y) FROM WF_Node WHERE FK_Flow='" + this.No + "'", 0);
        }
        #endregion ��������.

        #region ҵ�����ݱ�ͬ������.
        /// <summary>
        /// ͬ����ʽ
        /// </summary>
        public FlowDTSWay DTSWay
        {
            get
            {
                return (FlowDTSWay)this.GetValIntByKey(FlowAttr.DTSWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSWay, (int)value);
            }
        }
        public FlowDTSTime DTSTime
        {
            get
            {
                return (FlowDTSTime)this.GetValIntByKey(FlowAttr.DTSTime);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSTime, (int)value);
            }
        }
        public DTSField DTSField
        {
            get
            {
                return (DTSField)this.GetValIntByKey(FlowAttr.DTSField);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSField, (int)value);
            }
        }
        /// <summary>
        /// ҵ���
        /// </summary>
        public string DTSBTable
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DTSBTable);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSBTable, value);
            }
        }
        public string DTSBTablePK
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DTSBTablePK);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSBTablePK, value);
            }
        }
        /// <summary>
        /// Ҫͬ���Ľڵ�s
        /// </summary>
        public string DTSSpecNodes
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DTSSpecNodes);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSSpecNodes, value);
            }
        }
        /// <summary>
        /// ͬ�����ֶζ�Ӧ��ϵ.
        /// </summary>
        public string DTSFields
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DTSFields);
            }
            set
            {
                this.SetValByKey(FlowAttr.DTSFields, value);
            }
        }
        #endregion ҵ�����ݱ�ͬ������.

        #region ��������.
        /// <summary>
        /// �����¼�ʵ��
        /// </summary>
        public string FlowEventEntity
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FlowEventEntity);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowEventEntity, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FlowMark
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FlowMark);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowMark, value);
            }
        }

        /// <summary>
        /// �ڵ�ͼ������
        /// </summary>
        public int ChartType
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.ChartType);
            }
            set
            {
                this.SetValByKey(FlowAttr.ChartType, value);
            }
        }
        #endregion

        #region ��������.
        /// <summary>
        /// ��������.
        /// </summary>
        public StartLimitRole StartLimitRole
        {
            get
            {
                return (StartLimitRole)this.GetValIntByKey(FlowAttr.StartLimitRole);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartLimitRole, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string StartLimitPara
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.StartLimitPara);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartLimitPara, value);
            }
        }
        public string StartLimitAlert
        {
            get
            {
                string s = this.GetValStringByKey(FlowAttr.StartLimitAlert);
                if (s == "")
                    return "���Ѿ������������̣������ظ�������";
                return s;
            }
            set
            {
                this.SetValByKey(FlowAttr.StartLimitAlert, value);
            }
        }
        /// <summary>
        /// ���ƴ���ʱ��
        /// </summary>
        public StartLimitWhen StartLimitWhen
        {
            get
            {
                return (StartLimitWhen)this.GetValIntByKey(FlowAttr.StartLimitWhen);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartLimitWhen, (int)value);
            }
        }
        #endregion ��������.

        #region ����ģʽ
        /// <summary>
        /// ���𵼺���ʽ
        /// </summary>
        public StartGuideWay StartGuideWay
        {
            get
            {
                return (StartGuideWay)this.GetValIntByKey(FlowAttr.StartGuideWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartGuideWay, (int)value);
            }
        }
        /// <summary>
        /// ���̷������1
        /// </summary>
        public string StartGuidePara1
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.StartGuidePara1);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartGuidePara1, value);
            }
        }
        /// <summary>
        /// ���̷������2
        /// </summary>
        public string StartGuidePara2
        {
            get
            {
                string s = this.GetValStrByKey(FlowAttr.StartGuidePara2);
                if (string.IsNullOrEmpty(s) == null)
                {
                    if (this.StartGuideWay == BP.WF.Template.StartGuideWay.ByHistoryUrl)
                    {

                    }
                }
                return s;
            }
            set
            {
                this.SetValByKey(FlowAttr.StartGuidePara2, value);
            }
        }
        /// <summary>
        /// ���̷������3
        /// </summary>
        public string StartGuidePara3
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.StartGuidePara3);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartGuidePara3, value);
            }
        }
        /// <summary>
        /// �Ƿ������������ð�ť��
        /// </summary>
        public bool IsResetData
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsResetData);
            }
        }
        /// <summary>
        /// �Ƿ����õ�����ʷ���ݰ�ť?
        /// </summary>
        public bool IsImpHistory
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsImpHistory);
            }
        }
        /// <summary>
        /// �Ƿ��Զ�װ����һ������?
        /// </summary>
        public bool IsLoadPriData
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsLoadPriData);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �ݸ����
        /// </summary>
        public DraftRole DraftRole
        {
            get
            {
                return (DraftRole)this.GetValIntByKey(FlowAttr.Draft);
            }
            set
            {
                this.SetValByKey(FlowAttr.Draft, (int)value);
            }
        }
        public string Tag = null;
        /// <summary>
        /// ��������
        /// </summary>
        public FlowRunWay HisFlowRunWay
        {
            get
            {
                return (FlowRunWay)this.GetValIntByKey(FlowAttr.FlowRunWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowRunWay, (int)value);
            }
        }
        /// <summary>
        /// ���ж���
        /// </summary>
        public string RunObj
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.RunObj);
            }
            set
            {
                this.SetValByKey(FlowAttr.RunObj, value);
            }
        }
        /// <summary>
        /// ʱ������
        /// </summary>
        public TimelineRole HisTimelineRole
        {
            get
            {
                return (TimelineRole)this.GetValIntByKey(FlowAttr.TimelineRole);
            }
        }
        /// <summary>
        /// ���̲������ݲ�ѯȨ�޿��Ʒ�ʽ
        /// </summary>
        public FlowDeptDataRightCtrlType HisFlowDeptDataRightCtrlType
        {
            get
            {
                return (FlowDeptDataRightCtrlType)this.GetValIntByKey(FlowAttr.DRCtrlType);
            }
            set
            {
                this.SetValByKey(FlowAttr.DRCtrlType, value);
            }
        }
        /// <summary>
        /// ����Ӧ������
        /// </summary>
        public FlowAppType FlowAppType
        {
            get
            {
                return (FlowAppType)this.GetValIntByKey(FlowAttr.FlowAppType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowAppType, (int)value);
            }
        }
        /// <summary>
        /// �������̷�ʽ
        /// </summary>
        public CFlowWay CFlowWay
        {
            get
            {
                return (CFlowWay)this.GetValIntByKey(FlowAttr.CFlowWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.CFlowWay, (int)value);
            }
        }
        /// <summary>
        /// �������̲�����
        /// </summary>
        public string CFlowPara
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.CFlowPara);
            }
            set
            {
                this.SetValByKey(FlowAttr.CFlowPara, value);
            }
        }

        /// <summary>
        /// ���̱�ע�ı��ʽ
        /// </summary>
        public string FlowNoteExp
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.FlowNoteExp);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowNoteExp, value);
            }
        }
        #endregion ҵ����

        #region �����¹���.
        /// <summary>
        /// �����¹���web��ʽ���õ�
        /// </summary>
        /// <returns></returns>
        public Work NewWork()
        {
            return NewWork(WebUser.No);
        }
        /// <summary>
        /// �����¹���.web��ʽ���õ�
        /// </summary>
        /// <param name="empNo">��Ա���</param>
        /// <returns></returns>
        public Work NewWork(string empNo)
        {
            Emp emp = new Emp(empNo);
            return NewWork(emp, null);
        }
        /// <summary>
        /// ����һ����ʼ�ڵ���¹���
        /// </summary>
        /// <param name="emp">������</param>
        /// <param name="paras">��������,�����CS���ã�Ҫ���������̣�Ҫ������table��copy����,�Ͳ��ܴ�request����ȡ,���Դ���Ϊnull.</param>
        /// <returns>���ص�Work.</returns>
        public Work NewWork(Emp emp, Hashtable paras)
        {
            //�����bsϵͳ.
            if (paras == null)
                paras = new Hashtable();
            if (BP.Sys.SystemConfig.IsBSsystem == true)
            {
                foreach (string k in BP.Sys.Glo.Request.QueryString.AllKeys)
                {
                    if (paras.ContainsKey(k))
                        //  continue;
                        paras[k] = BP.Sys.Glo.Request.QueryString[k];
                    else
                        paras.Add(k, BP.Sys.Glo.Request.QueryString[k]);
                }
            }

            //��ʼ�ڵ�.
            BP.WF.Node nd = new BP.WF.Node(this.StartNodeID);

            //�Ӳݸ��￴���Ƿ����¹�����
            StartWork wk = (StartWork)nd.HisWork;
            wk.ResetDefaultVal();

            string dbstr = SystemConfig.AppCenterDBVarStr;

            Paras ps = new Paras();
            GERpt rpt = this.HisGERpt;

            //�Ƿ��´�����WorKID
            bool IsNewWorkID = false;
            /*���Ҫ���òݸ�,�ʹ���һ���µ�WorkID .*/
            if (this.DraftRole != Template.DraftRole.None && nd.IsStartNode)
                IsNewWorkID = true;

            try
            {
                //�ӱ������ѯ�������Ƿ���ڣ�
                if (this.IsGuestFlow == true && string.IsNullOrEmpty(GuestUser.No) == false)
                {
                    /*�ǿͻ���������̣����Ҿ��пͻ���½����Ϣ��*/
                    ps.SQL = "SELECT OID,FlowEndNode FROM " + this.PTable + " WHERE GuestNo=" + dbstr + "GuestNo AND WFState=" + dbstr + "WFState ";
                    ps.Add(GERptAttr.GuestNo, GuestUser.No);
                    ps.Add(GERptAttr.WFState, (int)WFState.Blank);
                    DataTable dt = DBAccess.RunSQLReturnTable(ps);
                    if (dt.Rows.Count > 0 && IsNewWorkID == false)
                    {
                        wk.OID = Int64.Parse(dt.Rows[0][0].ToString());
                        int nodeID = int.Parse(dt.Rows[0][1].ToString());
                        if (nodeID != this.StartNodeID)
                        {
                            string error = "@���������blank��״̬���������е������Ľڵ���ȥ�˵������";
                            Log.DefaultLogWriteLineError(error);
                            throw new Exception(error);
                        }
                    }
                }
                else
                {
                    ps.SQL = "SELECT OID,FlowEndNode FROM " + this.PTable + " WHERE FlowStarter=" + dbstr + "FlowStarter AND WFState=" + dbstr + "WFState ";
                    ps.Add(GERptAttr.FlowStarter, emp.No);
                    ps.Add(GERptAttr.WFState, (int)WFState.Blank);
                    // throw new Exception(ps.SQL);
                    DataTable dt = DBAccess.RunSQLReturnTable(ps);
                    //���û�����òݸ壬���Ҵ��ڲݸ��ȡ��һ�� by dgq 5.28
                    if (dt.Rows.Count > 0 && IsNewWorkID == false)
                    {
                        wk.OID = Int64.Parse(dt.Rows[0][0].ToString());
                        wk.RetrieveFromDBSources();
                        int nodeID = int.Parse(dt.Rows[0][1].ToString());
                        if (nodeID != this.StartNodeID)
                        {
                            string error = "@���������blank��״̬���������е������Ľڵ���ȥ�˵��������ǰͣ���ڵ�:" + nodeID;
                            Log.DefaultLogWriteLineError(error);
                            //     throw new Exception(error);
                        }
                    }
                }

                //���òݸ��հ׾ʹ���WorkID
                if (wk.OID == 0 || IsNewWorkID == true)
                {
                    /* ˵��û�пհ�,�ʹ���һ���հ�..*/
                    wk.ResetDefaultVal();
                    wk.Rec = WebUser.No;

                    wk.SetValByKey(StartWorkAttr.RecText, emp.Name);
                    wk.SetValByKey(StartWorkAttr.Emps, emp.No);

                    wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
                    wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
                    wk.SetValByKey(GERptAttr.WFState, (int)WFState.Blank);

                    wk.OID = DBAccess.GenerOID("WorkID"); /*�������WorkID ,����Ψһ����WorkID�ĵط�.*/

                    //�Ѿ������ܵ������ֶη��룬�������ֳ�������ֶ�����.
                    wk.SetValByKey(GERptAttr.FK_NY, BP.DA.DataType.CurrentYearMonth);
                    wk.SetValByKey(GERptAttr.FK_Dept, emp.FK_Dept);
                    wk.FID = 0;
                    wk.DirectInsert();

                    //��ʼ��ѡ�����Ա.
                    this.InitSelectAccper(nd, wk.OID);

                    //���ò���.
                    foreach (string k in paras.Keys)
                        rpt.SetValByKey(k, paras[k]);

                    if (this.PTable == wk.EnMap.PhysicsTable)
                    {
                        /*�����ʼ�ڵ�������̱������.*/
                        rpt.OID = wk.OID;
                        rpt.RetrieveFromDBSources();
                        rpt.FID = 0;
                        rpt.FlowStartRDT = BP.DA.DataType.CurrentDataTime;
                        rpt.MyNum = 0;
                        rpt.Title = WorkNode.GenerTitle(this, wk);
                        //WebUser.No + "," + BP.Web.WebUser.Name + "��" + DataType.CurrentDataCNOfShort + "����.";
                        rpt.WFState = WFState.Blank;
                        rpt.FlowStarter = emp.No;
                        rpt.FK_NY = DataType.CurrentYearMonth;
                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserNameOnly)
                            rpt.FlowEmps = "@" + emp.Name;

                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                            rpt.FlowEmps = "@" + emp.No;

                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                            rpt.FlowEmps = "@" + emp.No + "," + emp.Name;

                        rpt.FlowEnderRDT = BP.DA.DataType.CurrentDataTime;
                        rpt.FK_Dept = emp.FK_Dept;
                        rpt.FlowEnder = emp.No;
                        rpt.FlowEndNode = this.StartNodeID;

                        //���ɵ��ݱ��.
                        string billNoFormat = this.BillNoFormat.Clone() as string;
                        if (string.IsNullOrEmpty(billNoFormat) == false)
                            rpt.BillNo = BP.WF.Glo.GenerBillNo(billNoFormat, rpt.OID, rpt, this.PTable);

                        rpt.FID = 0;
                        rpt.DirectUpdate();
                    }
                    else
                    {
                        rpt.OID = wk.OID;
                        rpt.FID = 0;
                        rpt.FlowStartRDT = BP.DA.DataType.CurrentDataTime;
                        rpt.FlowEnderRDT = BP.DA.DataType.CurrentDataTime;
                        rpt.MyNum = 0;

                        rpt.Title = WorkNode.GenerTitle(this, wk);
                        // rpt.Title = WebUser.No + "," + BP.Web.WebUser.Name + "��" + DataType.CurrentDataCNOfShort + "����.";

                        rpt.WFState = WFState.Blank;
                        rpt.FlowStarter = emp.No;

                        rpt.FlowEndNode = this.StartNodeID;
                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserNameOnly)
                            rpt.FlowEmps = "@" + emp.Name;

                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                            rpt.FlowEmps = "@" + emp.No;

                        if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                            rpt.FlowEmps = "@" + emp.No + "," + emp.Name;


                        rpt.FK_NY = DataType.CurrentYearMonth;
                        rpt.FK_Dept = emp.FK_Dept;
                        rpt.FlowEnder = emp.No;
                        rpt.InsertAsOID(wk.OID);
                    }
                }
            }
            catch (Exception ex)
            {
                wk.CheckPhysicsTable();
                throw new Exception("@��������ʧ�ܣ��п�����������Ʊ�ʱ�������ӵĿؼ���û��Ԥ�����µģ�����ˢ��һ��Ӧ�ÿ��Խ����������Ϣ��" + ex.StackTrace + " @ ������Ϣ:" + ex.Message);
            }

            #region copy����.
            // ��¼���id ,���������ڸ���ʱ�䱻�޸ġ�
            Int64 newOID = wk.OID;
            if (IsNewWorkID == true)
            {
                // �����ݹ����Ĳ�����
                int i = 0;
                foreach (string k in paras.Keys)
                {
                    i++;
                    wk.SetValByKey(k, paras[k].ToString());
                }

                if (i >= 3)
                {
                    wk.OID = newOID;
                    wk.DirectUpdate();
                }
            }
            #endregion copy����.

            #region ����ɾ���ݸ������
            if (paras.ContainsKey(StartFlowParaNameList.IsDeleteDraft) && paras[StartFlowParaNameList.IsDeleteDraft].ToString() == "1")
            {
                /*�Ƿ�Ҫɾ��Draft */
                Int64 oid = wk.OID;
                try
                {
                    //wk.ResetDefaultValAllAttr();
                    wk.DirectUpdate();
                }
                catch (Exception ex)
                {
                    wk.Update();
                    BP.DA.Log.DebugWriteError("�����¹������󣬵����������쳣,����Ĭ��ֵ�����⣺" + ex.Message);
                }

                MapDtls dtls = wk.HisMapDtls;
                foreach (MapDtl dtl in dtls)
                    DBAccess.RunSQL("DELETE FROM " + dtl.PTable + " WHERE RefPK=" + oid);

                //ɾ���������ݡ�
                DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE FK_MapData='ND" + wk.NodeID + "' AND RefPKVal='" + wk.OID + "'");
                wk.OID = newOID;
            }
            #endregion ����ɾ���ݸ������

            #region ����ʼ�ڵ�, ������ݹ��� FromTableName ����Ҫ���������copy���ݡ�
            if (paras.ContainsKey("FromTableName"))
            {
                string tableName = paras["FromTableName"].ToString();
                string tablePK = paras["FromTablePK"].ToString();
                string tablePKVal = paras["FromTablePKVal"].ToString();

                DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM " + tableName + " WHERE " + tablePK + "='" + tablePKVal + "'");
                if (dt.Rows.Count == 0)
                    throw new Exception("@����table�������ݴ���û���ҵ�ָ���������ݣ��޷�Ϊ�û�������ݡ�");

                string innerKeys = ",OID,RDT,CDT,FID,WFState,";
                foreach (DataColumn dc in dt.Columns)
                {
                    if (innerKeys.Contains("," + dc.ColumnName + ","))
                        continue;

                    wk.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
                    rpt.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
                }
                rpt.Update();
            }
            #endregion ����ʼ�ڵ�, ������ݹ��� FromTableName ����Ҫ���������copy���ݡ�

            #region ��ȡ�����Ǳ���
            // ��ȡ�����Ǳ���.
            string PFlowNo = null;
            string PNodeIDStr = null;
            string PWorkIDStr = null;
            string PFIDStr = null;

            string CopyFormWorkID = null;
            if (paras.ContainsKey("CopyFormWorkID") == true)
            {
                CopyFormWorkID = paras["CopyFormWorkID"].ToString();
                PFlowNo = this.No;
                PNodeIDStr = paras["CopyFormNode"].ToString();
                PWorkIDStr = CopyFormWorkID;
                PFIDStr = "0";
            }

            if (paras.ContainsKey("PNodeID") == true)
            {
                PFlowNo = paras["PFlowNo"].ToString();
                PNodeIDStr = paras["PNodeID"].ToString();
                PWorkIDStr = paras["PWorkID"].ToString();
                PFIDStr = "0";
                if (paras.ContainsKey("PFID") == true)
                    PFIDStr = paras["PFID"].ToString(); //������.
            }
            #endregion ��ȡ�����Ǳ���

            #region  �ж��Ƿ�װ����һ������.
            if (this.IsLoadPriData == true && this.StartGuideWay == BP.WF.Template.StartGuideWay.None)
            {
                /* �����Ҫ����һ������ʵ����copy����. */
                string sql = "SELECT OID FROM " + this.PTable + " WHERE FlowStarter='" + WebUser.No + "' AND OID!=" + wk.OID + " ORDER BY OID DESC";
                string workidPri = DBAccess.RunSQLReturnStringIsNull(sql, "0");
                if (workidPri == "0")
                {
                    /*˵��û�е�һ������.*/
                }
                else
                {
                    PFlowNo = this.No;
                    PNodeIDStr = int.Parse(this.No) + "01";
                    PWorkIDStr = workidPri;
                    PFIDStr = "0";
                    CopyFormWorkID = workidPri;
                }
            }
            #endregion  �ж��Ƿ�װ����һ������.

            #region ��������֮������ݴ���1��
            if (string.IsNullOrEmpty(PNodeIDStr) == false && string.IsNullOrEmpty(PWorkIDStr) == false)
            {
                Int64 PWorkID = Int64.Parse(PWorkIDStr);
                Int64 PNodeID = 0;
                if (CopyFormWorkID != null)
                    PNodeID = Int64.Parse(PNodeIDStr);

                /* ����Ǵ������һ�������ϴ��ݹ����ģ��Ϳ���������������ݡ�*/

                #region copy ���ȴӸ����̵�NDxxxRpt copy.
                Int64 pWorkIDReal = 0;
                Flow pFlow = new Flow(PFlowNo);
                string pOID = "";
                if (string.IsNullOrEmpty(PFIDStr) == true || PFIDStr == "0")
                    pOID = PWorkID.ToString();
                else
                    pOID = PFIDStr;

                string sql = "SELECT * FROM " + pFlow.PTable + " WHERE OID=" + pOID;
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count != 1)
                    throw new Exception("@��Ӧ�ò�ѯ���������̵�����, ���ܵ����֮һ,��ȷ�ϸø����̵ĵ��ýڵ������̣߳�����û�а����̵߳�FID�������ݽ�����");


                wk.Copy(dt.Rows[0]);
                rpt.Copy(dt.Rows[0]);
                #endregion copy ���ȴӸ����̵�NDxxxRpt copy.

                #region �ӵ��õĽڵ���copy.
                BP.WF.Node fromNd = new BP.WF.Node(int.Parse(PNodeIDStr));
                Work wkFrom = fromNd.HisWork;
                wkFrom.OID = PWorkID;
                if (wkFrom.RetrieveFromDBSources() == 0)
                    throw new Exception("@�����̵Ĺ���ID����ȷ��û�в�ѯ������" + PWorkID);
                //wk.Copy(wkFrom);
                //rpt.Copy(wkFrom);
                #endregion �ӵ��õĽڵ���copy.

                #region ��ȡweb����.
                foreach (string k in paras.Keys)
                {
                    wk.SetValByKey(k, paras[k]);
                    rpt.SetValByKey(k, paras[k]);
                }
                #endregion ��ȡweb����.

                #region ���⸳ֵ.
                wk.OID = newOID;
                rpt.OID = newOID;

                // ��ִ��copy���п����������ֶλᱻ�����
                if (CopyFormWorkID != null)
                {
                    /*�������ִ�еĴ��Ѿ���ɵ�����copy.*/

                    wk.SetValByKey(StartWorkAttr.PFlowNo, PFlowNo);
                    wk.SetValByKey(StartWorkAttr.PNodeID, PNodeID);
                    wk.SetValByKey(StartWorkAttr.PWorkID, PWorkID);

                    rpt.SetValByKey(GERptAttr.PFlowNo, PFlowNo);
                    rpt.SetValByKey(GERptAttr.PNodeID, PNodeID);
                    rpt.SetValByKey(GERptAttr.PWorkID, PWorkID);

                    rpt.SetValByKey(GERptAttr.FID, 0);
                    rpt.SetValByKey(GERptAttr.FlowStartRDT, BP.DA.DataType.CurrentDataTime);
                    rpt.SetValByKey(GERptAttr.FlowEnderRDT, BP.DA.DataType.CurrentDataTime);
                    rpt.SetValByKey(GERptAttr.MyNum, 0);
                    rpt.SetValByKey(GERptAttr.WFState, (int)WFState.Blank);
                    rpt.SetValByKey(GERptAttr.FlowStarter, emp.No);
                    rpt.SetValByKey(GERptAttr.FlowEnder, emp.No);
                    rpt.SetValByKey(GERptAttr.FlowEndNode, this.StartNodeID);
                    rpt.SetValByKey(GERptAttr.FK_Dept, emp.FK_Dept);
                    rpt.SetValByKey(GERptAttr.FK_NY, DataType.CurrentYearMonth);

                    if (Glo.UserInfoShowModel == UserInfoShowModel.UserNameOnly)
                        rpt.SetValByKey(GERptAttr.FlowEmps, "@" + emp.Name);

                    if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                        rpt.SetValByKey(GERptAttr.FlowEmps, "@" + emp.No);

                    if (Glo.UserInfoShowModel == UserInfoShowModel.UserIDUserName)
                        rpt.SetValByKey(GERptAttr.FlowEmps, "@" + emp.No + "," + emp.Name);
                    //���ɵ��ݱ��.
                    string billNoFormat = this.BillNoFormat.Clone() as string;
                    if (string.IsNullOrEmpty(billNoFormat) == false)
                        rpt.SetValByKey(GERptAttr.BillNo, BP.WF.Glo.GenerBillNo(billNoFormat, rpt.OID, rpt, this.PTable));
                }

                if (rpt.EnMap.PhysicsTable != wk.EnMap.PhysicsTable)
                    wk.Update(); //���¹����ڵ�����.
                rpt.Update(); // �����������ݱ�.
                #endregion ���⸳ֵ.

                #region ���Ʊ���������.
                //������ϸ��
                MapDtls dtls = wk.HisMapDtls;
                if (dtls.Count > 0)
                {
                    MapDtls dtlsFrom = wkFrom.HisMapDtls;
                    int idx = 0;
                    if (dtlsFrom.Count == dtls.Count)
                    {
                        foreach (MapDtl dtl in dtls)
                        {
                            if (dtl.IsCopyNDData == false)
                                continue;

                            //new һ��ʵ��.
                            GEDtl dtlData = new GEDtl(dtl.No);
                            MapDtl dtlFrom = dtlsFrom[idx] as MapDtl;

                            GEDtls dtlsFromData = new GEDtls(dtlFrom.No);
                            dtlsFromData.Retrieve(GEDtlAttr.RefPK, PWorkID);
                            foreach (GEDtl geDtlFromData in dtlsFromData)
                            {
                                dtlData.Copy(geDtlFromData);
                                dtlData.RefPK = wk.OID.ToString();
                                if (this.No == PFlowNo)
                                    dtlData.InsertAsNew();
                                else
                                    dtlData.SaveAsOID(geDtlFromData.OID);
                            }
                        }
                    }
                }

                //���Ƹ������ݡ�
                if (wk.HisFrmAttachments.Count > 0)
                {
                    if (wkFrom.HisFrmAttachments.Count > 0)
                    {
                        int toNodeID = wk.NodeID;

                        //ɾ�����ݡ�
                        DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE FK_MapData='ND" + toNodeID + "' AND RefPKVal='" + wk.OID + "'");
                        FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + PNodeIDStr, PWorkID.ToString());

                        foreach (FrmAttachmentDB athDB in athDBs)
                        {
                            FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                            athDB_N.Copy(athDB);
                            athDB_N.FK_MapData = "ND" + toNodeID;
                            athDB_N.RefPKVal = wk.OID.ToString();
                            athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + PNodeIDStr,
                              "ND" + toNodeID);

                            if (athDB_N.HisAttachmentUploadType == AttachmentUploadType.Single)
                            {
                                /*����ǵ�����.*/
                                athDB_N.MyPK = athDB_N.FK_FrmAttachment + "_" + wk.OID;
                                if (athDB_N.IsExits == true)
                                    continue; /*˵����һ���ڵ�������߳��Ѿ�copy����, ���ǻ������߳�������㴫�����ݵĿ��ܣ����Բ�����break.*/
                                athDB_N.Insert();
                            }
                            else
                            {
                                athDB_N.MyPK = athDB_N.UploadGUID + "_" + athDB_N.FK_MapData + "_" + wk.OID;
                                athDB_N.Insert();
                            }
                        }
                    }
                }
                #endregion ���Ʊ���������.

            }
            #endregion ��������֮������ݴ���1��

            #region �����ݱ��.
            //���ɵ��ݱ��.
            if (this.BillNoFormat.Length > 3)
            {
                string billNoFormat = this.BillNoFormat.Clone() as string;

                if (billNoFormat.Contains("@"))
                {
                    foreach (string str in paras.Keys)
                        billNoFormat = billNoFormat.Replace("@" + str, paras[str].ToString());
                }

                //���ɵ��ݱ��.
                rpt.BillNo = BP.WF.Glo.GenerBillNo(billNoFormat, rpt.OID, rpt, this.PTable);
                //rpt.Update(GERptAttr.BillNo, rpt.BillNo);
                if (wk.Row.ContainsKey(GERptAttr.BillNo) == true)
                {
                    wk.SetValByKey(NDXRptBaseAttr.BillNo, rpt.BillNo);
                    // wk.Update(GERptAttr.BillNo, rpt.BillNo);
                }
            }
            #endregion �����ݱ��.

            #region ��������֮������ݴ���2, �����ֱ��Ҫ��ת��ָ���Ľڵ���ȥ.
            if (paras.ContainsKey("JumpToNode") == true)
            {
                wk.Rec = WebUser.No;
                wk.SetValByKey(StartWorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
                wk.SetValByKey(StartWorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
                wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wk.FK_Dept = emp.FK_Dept;
                wk.SetValByKey("FK_DeptName", emp.FK_DeptText);
                wk.SetValByKey("FK_DeptText", emp.FK_DeptText);
                wk.FID = 0;
                wk.SetValByKey(StartWorkAttr.RecText, emp.Name);

                int jumpNodeID = int.Parse(paras["JumpToNode"].ToString());
                Node jumpNode = new Node(jumpNodeID);

                string jumpToEmp = paras["JumpToEmp"].ToString();
                if (string.IsNullOrEmpty(jumpToEmp))
                    jumpToEmp = emp.No;

                WorkNode wn = new WorkNode(wk, nd);
                wn.NodeSend(jumpNode, jumpToEmp);

                WorkFlow wf = new WorkFlow(this, wk.OID, wk.FID);

                BP.WF.GenerWorkFlow gwf = new GenerWorkFlow(rpt.OID);
                rpt.WFState = WFState.Runing;
                rpt.Update();

                return wf.GetCurrentWorkNode().HisWork;
            }
            #endregion ��������֮������ݴ��ݡ�

            #region �������wk����.
            wk.Rec = emp.No;
            wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
            wk.FK_Dept = emp.FK_Dept;
            wk.SetValByKey("FK_DeptName", emp.FK_DeptText);
            wk.SetValByKey("FK_DeptText", emp.FK_DeptText);

            wk.SetValByKey(NDXRptBaseAttr.BillNo, rpt.BillNo);
            wk.FID = 0;
            wk.SetValByKey(StartWorkAttr.RecText, emp.Name);
            if (wk.IsExits == false)
                wk.DirectInsert();
            else
                wk.Update();
            #endregion ����������.

            #region ��generworkflow��ʼ������. add 2015-08-06
            GenerWorkFlow mygwf = new GenerWorkFlow();
            mygwf.WorkID = wk.OID;
            if (mygwf.RetrieveFromDBSources() == 0)
            {
                mygwf.Starter = WebUser.No;
                mygwf.StarterName = WebUser.Name;
                mygwf.FK_Dept = BP.Web.WebUser.FK_Dept;
                mygwf.DeptName = BP.Web.WebUser.FK_DeptName;
                mygwf.FK_Flow = this.No;
                mygwf.FK_FlowSort = this.FK_FlowSort;
                mygwf.FK_Node = nd.NodeID;
                mygwf.WorkID = wk.OID;
                mygwf.WFState = WFState.Blank;
                mygwf.FlowName = this.Name;
                mygwf.RDT = BP.DA.DataType.CurrentDataTime;
                mygwf.Insert();
            }
            #endregion ��generworkflow��ʼ������.


            return wk;
        }
        /// <summary>
        /// ϵͳ�����Ǿʹ�������ڵ�Ľ�����Ա.
        /// </summary>
        /// <param name="currND"></param>
        /// <param name="workid"></param>
        public void InitSelectAccper(Node currND, Int64 workid)
        {
            if (this.IsFullSA == false)
                return;

            //��ѯ�������еĽڵ�.
            Nodes nds = new Nodes(this.No);

            // ��ʼ�ڵ���Ҫ���⴦��
            /* ���������Ҫ����δ���Ĵ����� */
            SelectAccper sa = new SelectAccper();

            sa.FK_Emp = WebUser.No;
            sa.FK_Node = currND.NodeID;
            sa.WorkID = workid;
            sa.ResetPK();
            if (sa.RetrieveFromDBSources() == 0)
            {
                sa.AccType = 0;
                sa.EmpName = WebUser.Name;
                sa.Insert();
            }
            else
            {
                sa.AccType = 0;
                sa.EmpName = WebUser.Name;
                sa.Update();
            }


            foreach (Node item in nds)
            {
                if (item.IsStartNode == true)
                    continue;

                //������ո�λ���㣨Ĭ�ϵĵ�һ������.��
                if (item.HisDeliveryWay == DeliveryWay.ByStation)
                {
                    string sql = "SELECT No, Name FROM Port_Emp WHERE No IN (SELECT A.FK_Emp FROM " + BP.WF.Glo.EmpStation + " A, WF_NodeStation B WHERE A.FK_Station=B.FK_Station AND B.FK_Node=" + item.NodeID + ")";
                    DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count != 1)
                        continue;

                    string no = dt.Rows[0][0].ToString();
                    string name = dt.Rows[0][1].ToString();

                    // sa.Delete(SelectAccperAttr.FK_Node,item.NodeID, SelectAccperAttr.WorkID, workid); //ɾ���Ѿ����ڵ�����.
                    sa.FK_Emp = no;
                    sa.EmpName = name;
                    sa.FK_Node = item.NodeID;
                    sa.WorkID = workid;
                    sa.Info = "��";
                    sa.AccType = 0;
                    sa.ResetPK();
                    if (sa.IsExits)
                        continue;

                    sa.Insert();
                    continue;
                }


                //������ָ���ڵ���ͬ����Ա.
                if (item.HisDeliveryWay == DeliveryWay.BySpecNodeEmp
                   && item.DeliveryParas == currND.NodeID.ToString())
                {

                    sa.FK_Emp = WebUser.No;
                    sa.FK_Node = item.NodeID;
                    sa.WorkID = workid;
                    sa.Info = "��";
                    sa.AccType = 0;
                    sa.EmpName = WebUser.Name;

                    sa.ResetPK();
                    if (sa.IsExits)
                        continue;

                    sa.Insert();
                    continue;
                }

                //����󶨵Ľڵ���Ա..
                if (item.HisDeliveryWay == DeliveryWay.ByBindEmp)
                {
                    NodeEmps nes = new NodeEmps();
                    nes.Retrieve(NodeEmpAttr.FK_Node, item.NodeID);
                    foreach (NodeEmp ne in nes)
                    {
                        sa.FK_Emp = ne.FK_Emp;
                        sa.FK_Node = item.NodeID;
                        sa.WorkID = workid;
                        sa.Info = "��";
                        sa.AccType = 0;
                        sa.EmpName = ne.FK_EmpT;

                        sa.ResetPK();
                        if (sa.IsExits)
                            continue;

                        sa.Insert();
                    }
                }

                //���սڵ�� ��λ�벿�ŵĽ�������.
                #region ���������λ�Ľ�������.
                if (item.HisDeliveryWay == DeliveryWay.ByDeptAndStation)
                {
                    string dbStr = BP.Sys.SystemConfig.AppCenterDBVarStr;
                    string sql = string.Empty;
                    DataTable dt = null;

                    //added by liuxc,2015.6.30.
                    //���𼯳���BPMģʽ
                    if (BP.WF.Glo.OSModel == OSModel.WorkFlow)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No IN ";
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                        sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node1)";
                        sql += ")";
                        sql += "AND No IN ";
                        sql += "(";
                        sql += "SELECT FK_Emp FROM " + BP.WF.Glo.EmpStation + " WHERE FK_Station IN ";
                        sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node1 )";
                        sql += ") ORDER BY No ";

                        Paras ps = new Paras();
                        ps.Add("FK_Node1", item.NodeID);
                        ps.Add("FK_Node2", item.NodeID);
                        ps.SQL = sql;
                        dt = DBAccess.RunSQLReturnTable(ps);
                    }
                    else
                    {
                        sql = "SELECT pdes.FK_Emp AS No"
                              + " FROM   Port_DeptEmpStation pdes"
                              + "        INNER JOIN WF_NodeDept wnd"
                              + "             ON  wnd.FK_Dept = pdes.FK_Dept"
                              + "             AND wnd.FK_Node = " + item.NodeID
                              + "        INNER JOIN WF_NodeStation wns"
                              + "             ON  wns.FK_Station = pdes.FK_Station"
                              + "             AND wnd.FK_Node =" + item.NodeID
                              + " ORDER BY"
                              + "        pdes.FK_Emp";

                        dt = DBAccess.RunSQLReturnTable(sql);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        sa.FK_Emp = emp.No;
                        sa.FK_Node = item.NodeID;
                        sa.WorkID = workid;
                        sa.Info = "��";
                        sa.AccType = 0;
                        sa.EmpName = emp.Name;

                        sa.ResetPK();
                        if (sa.IsExits)
                            continue;

                        sa.Insert();
                    }
                }
                #endregion ���������λ�Ľ�������.
            }


            //Ԥ�Ƶ�ǰ�ڵ㵽��ڵ�����ݡ�
            Nodes toNDs = currND.HisToNodes;
            foreach (Node item in toNDs)
            {
                if (item.HisDeliveryWay == DeliveryWay.ByStation)
                {
                    /*������ո�λ����*/
                    #region ����ж� - ���ո�λ��ִ�С�
                    string dbStr = BP.Sys.SystemConfig.AppCenterDBVarStr;
                    string sql = "";
                    Paras ps = new Paras();
                    /* ���ִ�нڵ� �� ���ܽڵ��λ���ϲ�һ�� */
                    /* û�в�ѯ���������, �Ȱ��ձ����ż��㡣*/
                    if (this.FlowAppType == FlowAppType.Normal)
                    {
                        switch (BP.Sys.SystemConfig.AppCenterDBType)
                        {
                            case DBType.MySQL:
                            case DBType.MSSQL:
                                sql = "select No from Port_Emp x inner join (select FK_Emp from " + BP.WF.Glo.EmpStation + " a inner join WF_NodeStation b ";
                                sql += " on a.FK_Station=b.FK_Station where FK_Node=" + dbStr + "FK_Node) as y on x.No=y.FK_Emp inner join Port_EmpDept z on";
                                sql += " x.No=z.FK_Emp where z.FK_Dept =" + dbStr + "FK_Dept order by x.No";
                                break;
                            default:
                                sql = "SELECT No FROM Port_Emp WHERE NO IN "
                              + "(SELECT  FK_Emp  FROM " + BP.WF.Glo.EmpStation + " WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node) )"
                              + " AND  NO IN "
                              + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept =" + dbStr + "FK_Dept)";
                                sql += " ORDER BY No ";
                                break;
                        }

                        ps = new Paras();
                        ps.SQL = sql;
                        ps.Add("FK_Node", item.NodeID);
                        ps.Add("FK_Dept", WebUser.FK_Dept);
                    }

                    DataTable dt = DBAccess.RunSQLReturnTable(ps);
                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        sa.FK_Emp = emp.No;
                        sa.FK_Node = item.NodeID;
                        sa.WorkID = workid;
                        sa.Info = "��";
                        sa.AccType = 0;
                        sa.EmpName = emp.Name;

                        sa.ResetPK();
                        if (sa.IsExits)
                            continue;

                        sa.Insert();
                    }
                    #endregion  ���ո�λ��ִ�С�
                }
            }
        }
        #endregion �����¹���.

        #region ��ʼ��һ������.
        /// <summary>
        /// ��ʼ��һ������
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="fk_node"></param>
        /// <returns></returns>
        public Work GenerWork(Int64 workid, Node nd, bool isPostBack)
        {
            Work wk = nd.HisWork;
            wk.OID = workid;
            if (wk.RetrieveFromDBSources() == 0)
            {


                /*
                 * 2012-10-15 żȻ����һ�ι�����ʧ���, WF_GenerWorkerlist WF_GenerWorkFlow ����������ݣ�û�в�����ʧԭ�� stone.
                 * �����´����Զ��޸������ǻ���������copy����ȫ�����⡣
                 * */
#warning 2011-10-15 żȻ����һ�ι�����ʧ���.

                string fk_mapData = "ND" + int.Parse(this.No) + "Rpt";
                GERpt rpt = new GERpt(fk_mapData);
                rpt.OID = int.Parse(workid.ToString());
                if (rpt.RetrieveFromDBSources() >= 1)
                {
                    /*  ��ѯ����������.  */
                    wk.Copy(rpt);
                    wk.Rec = WebUser.No;
                    wk.InsertAsOID(workid);
                }
                else
                {
                    /*  û�в�ѯ����������.  */

#warning ���ﲻӦ�ó��ֵ��쳣��Ϣ.

                    string msg = "@��Ӧ�ó��ֵ��쳣.";
                    msg += "@��Ϊ�ڵ�NodeID=" + nd.NodeID + " workid:" + workid + " ��ȡ����ʱ.";
                    msg += "@��ȡ����Rpt������ʱ����Ӧ�ò�ѯ������";
                    msg += "@GERpt ��Ϣ: table:" + rpt.EnMap.PhysicsTable + "   OID=" + rpt.OID;

                    string sql = "SELECT count(*) FROM " + rpt.EnMap.PhysicsTable + " WHERE OID=" + workid;
                    int num = DBAccess.RunSQLReturnValInt(sql);

                    msg += " @SQL:" + sql;
                    msg += " ReturnNum:" + num;
                    if (num == 0)
                    {
                        msg += "�Ѿ���sql���Բ�ѯ���������ǲ�Ӧ�������ѯ������.";
                    }
                    else
                    {
                        /*���������sql ��ѯ����.*/
                        num = rpt.RetrieveFromDBSources();
                        msg += "@��rpt.RetrieveFromDBSources = " + num;
                    }

                    Log.DefaultLogWriteLineError(msg);

                    MapData md = new MapData("ND" + int.Parse(nd.FK_Flow) + "01");
                    sql = "SELECT * FROM " + md.PTable + " WHERE OID=" + workid;
                    DataTable dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 1)
                    {
                        rpt.Copy(dt.Rows[0]);
                        try
                        {
                            rpt.FlowStarter = dt.Rows[0][StartWorkAttr.Rec].ToString();
                            rpt.FlowStartRDT = dt.Rows[0][StartWorkAttr.RDT].ToString();
                            rpt.FK_Dept = dt.Rows[0][StartWorkAttr.FK_Dept].ToString();
                        }
                        catch
                        {
                        }

                        rpt.OID = int.Parse(workid.ToString());
                        try
                        {
                            rpt.InsertAsOID(rpt.OID);
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("@��Ӧ�ó����벻��ȥ rpt:" + rpt.EnMap.PhysicsTable + " workid=" + workid);
                            rpt.RetrieveFromDBSources();
                        }
                    }
                    else
                    {
                        Log.DefaultLogWriteLineError("@û���ҵ���ʼ�ڵ������, NodeID:" + nd.NodeID + " workid:" + workid);
                        throw new Exception("@û���ҵ���ʼ�ڵ������, NodeID:" + nd.NodeID + " workid:" + workid + " SQL:" + sql);
                    }

#warning ��Ӧ�ó��ֵĹ�����ʧ.
                    Log.DefaultLogWriteLineError("@����[" + nd.NodeID + " : " + wk.EnDesc + "], ��������WorkID=" + workid + " ��ʧ, û�д�NDxxxRpt���ҵ���¼,����ϵ����Ա��");

                    wk.Copy(rpt);
                    wk.Rec = WebUser.No;
                    wk.ResetDefaultVal();
                    wk.Insert();
                }
            }

            #region �ж��Ƿ���ɾ���ݸ������.
            if (SystemConfig.IsBSsystem == true && isPostBack == false && nd.IsStartNode && BP.Sys.Glo.Request.QueryString["IsDeleteDraft"] == "1")
            {

                /*��Ҫɾ���ݸ�.*/
                /*�Ƿ�Ҫɾ��Draft */
                string title = wk.GetValStringByKey("Title");
                wk.ResetDefaultValAllAttr();
                wk.OID = workid;
                wk.SetValByKey(GenerWorkFlowAttr.Title, title);
                wk.DirectUpdate();

                MapDtls dtls = wk.HisMapDtls;
                foreach (MapDtl dtl in dtls)
                    DBAccess.RunSQL("DELETE FROM " + dtl.PTable + " WHERE RefPK=" + wk.OID);

                //ɾ���������ݡ�
                DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE FK_MapData='ND" + wk.NodeID + "' AND RefPKVal='" + wk.OID + "'");

            }
            #endregion


            // ���õ�ǰ����Ա�Ѽ�¼�ˡ�
            wk.Rec = WebUser.No;
            wk.RecText = WebUser.Name;
            wk.Rec = WebUser.No;
            wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey(GERptAttr.WFState, WFState.Runing);
            wk.SetValByKey("FK_Dept", WebUser.FK_Dept);
            wk.SetValByKey("FK_DeptName", WebUser.FK_DeptName);
            wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
            wk.FID = 0;
            wk.SetValByKey("RecText", WebUser.Name);

            //�����ݱ��.
            if (nd.IsStartNode)
            {
                try
                {
                    string billNo = wk.GetValStringByKey(NDXRptBaseAttr.BillNo);
                    if (string.IsNullOrEmpty(billNo) && nd.HisFlow.BillNoFormat.Length > 2)
                    {
                        /*�����Զ����ɱ��*/
                        wk.SetValByKey(NDXRptBaseAttr.BillNo,
                            BP.WF.Glo.GenerBillNo(nd.HisFlow.BillNoFormat, wk.OID, wk, nd.HisFlow.PTable));
                    }
                }
                catch
                {
                    // ������û��billNo����ֶ�,Ҳ����Ҫ������.
                }
            }

            return wk;
        }
        #endregion ��ʼ��һ������

        #region ����ͨ�÷���.
        public string DoBTableDTS()
        {
            if (this.DTSWay == FlowDTSWay.None)
                return "ִ��ʧ�ܣ���û������ͬ����ʽ��";

            string info = "";
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.Retrieve(GenerWorkFlowAttr.FK_Flow, this.No);
            foreach (GenerWorkFlow gwf in gwfs)
            {
                GERpt rpt = this.HisGERpt;
                rpt.OID = gwf.WorkID;
                rpt.RetrieveFromDBSources();

                info += "@��ʼͬ��:" + gwf.Title + ",WorkID=" + gwf.WorkID;
                if (gwf.WFSta == WFSta.Complete)
                    info += this.DoBTableDTS(rpt, new Node(gwf.FK_Node), true);
                else
                    info += this.DoBTableDTS(rpt, new Node(gwf.FK_Node), false);
            }
            return info;
        }
        /// <summary>
        /// ͬ����ǰ���������ݵ�ҵ�����ݱ���.
        /// </summary>
        /// <param name="rpt">���̱���</param>
        /// <param name="currNode">��ǰ�ڵ�ID</param>
        /// <param name="isStopFlow">�����Ƿ����</param>
        /// <returns>����ͬ�����.</returns>
        public string DoBTableDTS(GERpt rpt, Node currNode, bool isStopFlow)
        {
            bool isActiveSave = false;
            // �ж��Ƿ������������ͬ������.
            switch (this.DTSTime)
            {
                case FlowDTSTime.AllNodeSend:
                    isActiveSave = true;
                    break;
                case FlowDTSTime.SpecNodeSend:
                    if (this.DTSSpecNodes.Contains(currNode.NodeID.ToString()) == true)
                        isActiveSave = true;
                    break;
                case FlowDTSTime.WhenFlowOver:
                    if (isStopFlow)
                        isActiveSave = true;
                    break;
                default:
                    break;
            }
            if (isActiveSave == false)
                return "";

            #region qinfaliang, ��дͬ����ҵ���߼�,ִ�д�����׳��쳣.

            string[] dtsArray = this.DTSFields.Split('@');

            string[] lcDTSFieldsArray = dtsArray[0].Split(',');
            string[] ywDTSFieldsArray = dtsArray[1].Split(',');

            string sql = "SELECT " + dtsArray[0] + " FROM " + this.PTable.ToUpper() + " WHERE OID=" + rpt.OID;
            DataTable lcDt = DBAccess.RunSQLReturnTable(sql);
            if (lcDt.Rows.Count == 0)
                return "";

            sql = "SELECT " + dtsArray[1] + " FROM " + this.DTSBTable.ToUpper();
            DataTable ywDt = DBAccess.RunSQLReturnTable(sql);

            string values = "";

            for (int i = 0; i < lcDTSFieldsArray.Length; i++)
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        break;
                    case DBType.MySQL:
                        break;
                    case DBType.Oracle:
                        if (ywDt.Columns[ywDTSFieldsArray[i]].DataType == typeof(DateTime))
                        {
                            if (!string.IsNullOrEmpty(lcDt.Rows[0][lcDTSFieldsArray[i].ToString()].ToString()))
                            {
                                values += "to_date('" + lcDt.Rows[0][lcDTSFieldsArray[i].ToString()] + "','YYYY-MM-DD'),";
                            }
                            else
                            {
                                values += "'',";
                            }
                            continue;
                        }
                        values += "'" + lcDt.Rows[0][lcDTSFieldsArray[i].ToString()] + "',";
                        continue;
                    default:
                        throw new Exception("��ʱ��֧����ʹ�õ����ݿ�����!");
                }
                values += "'" + lcDt.Rows[0][lcDTSFieldsArray[i].ToString()] + "',";
            }

            values = values.Substring(0, values.Length - 1);

            sql = "INSERT INTO " + this.DTSBTable + "(" + dtsArray[1] + ") VALUES(" + values + ")";
            try
            {
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion qinfaliang, ��дͬ����ҵ���߼�,ִ�д�����׳��쳣.

            return "ͬ���ɹ�.";
        }
        /// <summary>
        /// �Զ�����
        /// </summary>
        /// <returns></returns>
        public string DoAutoStartIt()
        {
            switch (this.HisFlowRunWay)
            {
                case BP.WF.FlowRunWay.SpecEmp: //ָ����Ա��ʱ���С�
                    string RunObj = this.RunObj;
                    string FK_Emp = RunObj.Substring(0, RunObj.IndexOf('@'));
                    BP.Port.Emp emp = new BP.Port.Emp();
                    emp.No = FK_Emp;
                    if (emp.RetrieveFromDBSources() == 0)
                        return "�����Զ��������̴��󣺷�����(" + FK_Emp + ")�����ڡ�";

                    BP.Web.WebUser.SignInOfGener(emp);
                    string info_send = BP.WF.Dev2Interface.Node_StartWork(this.No, null, null, 0, null, 0, null).ToMsgOfText();
                    if (WebUser.No != "admin")
                    {
                        emp = new BP.Port.Emp();
                        emp.No = "admin";
                        emp.Retrieve();
                        BP.Web.WebUser.SignInOfGener(emp);
                        return info_send;
                    }
                    return info_send;
                case BP.WF.FlowRunWay.DataModel: //�����ݼ���������ģʽִ�С�
                    break;
                default:
                    return "@��������û������Ϊ�Զ��������������͡�";
            }

            string msg = "";
            BP.Sys.MapExt me = new MapExt();
            me.MyPK = "ND" + int.Parse(this.No) + "01_" + MapExtXmlList.StartFlow;
            int i = me.RetrieveFromDBSources();
            if (i == 0)
            {
                BP.DA.Log.DefaultLogWriteLineError("û��Ϊ����(" + this.Name + ")�Ŀ�ʼ�ڵ����÷�������,��ο�˵������.");
                return "û��Ϊ����(" + this.Name + ")�Ŀ�ʼ�ڵ����÷�������,��ο�˵������.";
            }
            if (string.IsNullOrEmpty(me.Tag))
            {
                BP.DA.Log.DefaultLogWriteLineError("û��Ϊ����(" + this.Name + ")�Ŀ�ʼ�ڵ����÷�������,��ο�˵������.");
                return "û��Ϊ����(" + this.Name + ")�Ŀ�ʼ�ڵ����÷�������,��ο�˵������.";
            }

            // ��ȡ�ӱ�����.
            DataSet ds = new DataSet();
            string[] dtlSQLs = me.Tag1.Split('*');
            foreach (string sql in dtlSQLs)
            {
                if (string.IsNullOrEmpty(sql))
                    continue;

                string[] tempStrs = sql.Split('=');
                string dtlName = tempStrs[0];
                DataTable dtlTable = BP.DA.DBAccess.RunSQLReturnTable(sql.Replace(dtlName + "=", ""));
                dtlTable.TableName = dtlName;
                ds.Tables.Add(dtlTable);
            }

            #region �������Դ�Ƿ���ȷ.
            string errMsg = "";
            // ��ȡ��������.
            DataTable dtMain = BP.DA.DBAccess.RunSQLReturnTable(me.Tag);
            if (dtMain.Rows.Count == 0)
            {
                return "����(" + this.Name + ")��ʱ������,��ѯ���:" + me.Tag.Replace("'", "��");
            }

            msg += "@��ѯ��(" + dtMain.Rows.Count + ")������.";

            if (dtMain.Columns.Contains("Starter") == false)
                errMsg += "@��ֵ��������û��Starter��.";

            if (dtMain.Columns.Contains("MainPK") == false)
                errMsg += "@��ֵ��������û��MainPK��.";

            if (errMsg.Length > 2)
            {
                return "����(" + this.Name + ")�Ŀ�ʼ�ڵ����÷�������,������." + errMsg;
            }
            #endregion �������Դ�Ƿ���ȷ.

            #region �������̷���.

            string fk_mapdata = "ND" + int.Parse(this.No) + "01";

            MapData md = new MapData(fk_mapdata);
            int idx = 0;
            foreach (DataRow dr in dtMain.Rows)
            {
                idx++;

                string mainPK = dr["MainPK"].ToString();
                string sql = "SELECT OID FROM " + md.PTable + " WHERE MainPK='" + mainPK + "'";
                if (DBAccess.RunSQLReturnTable(sql).Rows.Count != 0)
                {
                    msg += "@" + this.Name + ",��" + idx + "��,��������֮ǰ�Ѿ���ɡ�";
                    continue; /*˵���Ѿ����ȹ���*/
                }

                string starter = dr["Starter"].ToString();
                if (WebUser.No != starter)
                {
                    BP.Web.WebUser.Exit();
                    BP.Port.Emp emp = new BP.Port.Emp();
                    emp.No = starter;
                    if (emp.RetrieveFromDBSources() == 0)
                    {
                        msg += "@" + this.Name + ",��" + idx + "��,���õķ�����Ա:" + emp.No + "������.";
                        msg += "@����������ʽ��������(" + this.Name + ")���õķ�����Ա:" + emp.No + "�����ڡ�";
                        continue;
                    }
                    WebUser.SignInOfGener(emp);
                }

                #region  ��ֵ.
                Work wk = this.NewWork();
                foreach (DataColumn dc in dtMain.Columns)
                    wk.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());

                if (ds.Tables.Count != 0)
                {
                    // MapData md = new MapData(nodeTable);
                    MapDtls dtls = md.MapDtls; // new MapDtls(nodeTable);
                    foreach (MapDtl dtl in dtls)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.TableName != dtl.No)
                                continue;

                            //ɾ��ԭ�������ݡ�
                            GEDtl dtlEn = dtl.HisGEDtl;
                            dtlEn.Delete(GEDtlAttr.RefPK, wk.OID.ToString());

                            // ִ�����ݲ��롣
                            foreach (DataRow drDtl in dt.Rows)
                            {
                                if (drDtl["RefMainPK"].ToString() != mainPK)
                                    continue;

                                dtlEn = dtl.HisGEDtl;
                                foreach (DataColumn dc in dt.Columns)
                                    dtlEn.SetValByKey(dc.ColumnName, drDtl[dc.ColumnName].ToString());

                                dtlEn.RefPK = wk.OID.ToString();
                                dtlEn.OID = 0;
                                dtlEn.Insert();
                            }
                        }
                    }
                }
                #endregion  ��ֵ.

                // ��������Ϣ.
                Node nd = this.HisStartNode;
                try
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    string infoSend = wn.NodeSend().ToMsgOfHtml();
                    BP.DA.Log.DefaultLogWriteLineInfo(msg);
                    msg += "@" + this.Name + ",��" + idx + "��,������Ա:" + WebUser.No + "-" + WebUser.Name + "�����.\r\n" + infoSend;
                    //this.SetText("@�ڣ�" + idx + "��������" + WebUser.No + " - " + WebUser.Name + "�Ѿ���ɡ�\r\n" + msg);
                }
                catch (Exception ex)
                {
                    msg += "@" + this.Name + ",��" + idx + "��,������Ա:" + WebUser.No + "-" + WebUser.Name + "����ʱ���ִ���.\r\n" + ex.Message;
                }
                msg += "<hr>";
            }
            return msg;
            #endregion �������̷���.
        }

        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// �޲�����������ͼ
        /// </summary>
        /// <returns></returns>
        public static string RepareV_FlowData_View()
        {
            string err = "";
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();

            if (fls.Count == 0)
                return null;

            string sql = "";
            sql = "CREATE VIEW V_FlowData (FK_FlowSort,FK_Flow,OID,FID,Title,WFState,CDT,FlowStarter,FlowStartRDT,FK_Dept,FK_NY,FlowDaySpan,FlowEmps,FlowEnder,FlowEnderRDT,FlowEndNode,MyNum, PWorkID,PFlowNo,BillNo,ProjNo) ";
            //     sql += "\t\n /*  WorkFlow Data " + DateTime.Now.ToString("yyyy-MM-dd") + " */ ";
            sql += " AS ";
            foreach (Flow fl in fls)
            {
                if (fl.IsCanStart == false)
                    continue;


                string mysql = "\t\n SELECT '" + fl.FK_FlowSort + "' AS FK_FlowSort,'" + fl.No + "' AS FK_Flow,OID,FID,Title,WFState,CDT,FlowStarter,FlowStartRDT,FK_Dept,FK_NY,FlowDaySpan,FlowEmps,FlowEnder,FlowEnderRDT,FlowEndNode,1 as MyNum,PWorkID,PFlowNo,BillNo,ProjNo FROM " + fl.PTable + " WHERE WFState >1 ";
                try
                {
                    DBAccess.RunSQLReturnTable(mysql);
                }
                catch (Exception ex)
                {
                    continue;
                    try
                    {
                        fl.DoCheck();
                        DBAccess.RunSQLReturnTable(mysql);
                    }
                    catch (Exception ex1)
                    {
                        err += ex1.Message;
                        continue;
                    }
                }

                if (fls.Count == 1)
                    break;

                sql += mysql;
                sql += "\t\n UNION ";
            }
            if (sql.Contains("SELECT") == false)
                return null;

            if (fls.Count > 1)
                sql = sql.Substring(0, sql.Length - 6);

            if (sql.Length > 20)
            {

                #region ɾ�� V_FlowData
                try
                {
                    DBAccess.RunSQL("DROP VIEW V_FlowData");
                }
                catch
                {
                    try
                    {
                        DBAccess.RunSQL("DROP table V_FlowData");
                    }
                    catch
                    {
                    }
                }
                #endregion ɾ�� V_FlowData

                #region ������ͼ.
                try
                {
                    DBAccess.RunSQL(sql);
                }
                catch
                {
                }
                #endregion ������ͼ.

            }
            return null;
        }
        /// <summary>
        /// У������
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            #region ������̱�
            FrmNodes fns = new FrmNodes();
            fns.Retrieve(FrmNodeAttr.FK_Flow, this.No);
            string frms = "";
            string err = "";
            foreach (FrmNode item in fns)
            {
                if (frms.Contains(item.FK_Frm + ","))
                    continue;
                frms += item.FK_Frm + ",";
                try
                {
                    MapData md = new MapData(item.FK_Frm);
                    md.RepairMap();
                    Entity en = md.HisEn;
                    en.CheckPhysicsTable();
                }
                catch (Exception ex)
                {
                    err += "@�ڵ�󶨵ı�:" + item.FK_Frm + ",�Ѿ���ɾ����.�쳣��Ϣ." + ex.Message;
                }
            }
            #endregion

            try
            {
                // ������������.
                DBAccess.RunSQL("UPDATE WF_Node SET FlowName = (SELECT Name FROM WF_Flow WHERE NO=WF_Node.FK_Flow)");

                //ɾ������,�Ƿ�����.
                string sqls = "DELETE FROM Sys_FrmSln where fk_mapdata not in (select no from sys_mapdata)";
                sqls += "@ DELETE FROM WF_Direction WHERE Node=ToNode";
                DBAccess.RunSQLs(sqls);

                //���¼�������.
                this.NumOfBill = DBAccess.RunSQLReturnValInt("SELECT count(*) FROM WF_BillTemplate WHERE NodeID IN (select NodeID from WF_Flow WHERE no='" + this.No + "')");
                this.NumOfDtl = DBAccess.RunSQLReturnValInt("SELECT count(*) FROM Sys_MapDtl WHERE FK_MapData='ND" + int.Parse(this.No) + "Rpt'");
                this.DirectUpdate();

                string msg = "@  =======  ���ڡ�" + this.Name + " �����̼�鱨��  ============";
                msg += "@��Ϣ�����Ϊ����: ��Ϣ  ����  ����. �����������Ĵ��������Ҫȥ�޸Ļ�������.";
                msg += "@���̼��Ŀǰ�����ܸ���100%�Ĵ���,��Ҫ�ֹ�������һ�β���ȷ��������Ƶ���ȷ��.";

                Nodes nds = new Nodes(this.No);

                //����ģ��.
                BillTemplates bks = new BillTemplates(this.No);

                //��������.
                Conds conds = new Conds(this.No);

                #region �Խڵ���м��
                //�ڵ���ֶ��������ͼ��--begin---------
                msg += CheckFormFields();
                //���ֶ��������ͼ��-------End-----

                foreach (Node nd in nds)
                {
                    //��������λ������.
                    nd.SetValByKey(NodeAttr.NodePosType, (int)nd.GetHisNodePosType());

                    msg += "@��Ϣ: -------- ��ʼ���ڵ�ID:(" + nd.NodeID + ")����:(" + nd.Name + ")��Ϣ -------------";

                    #region �޸����ڵ�����ݿ�.
                    msg += "@��Ϣ:��ʼ����&�޸��ڵ��Ҫ���ֶ�";
                    try
                    {
                        nd.RepareMap();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@�޸��ڵ���Ҫ�ֶ�ʱ���ִ���:" + nd.Name + " - " + ex.Message);
                    }

                    msg += "@��Ϣ:��ʼ�޸��ڵ������.";
                    DBAccess.RunSQL("UPDATE Sys_MapData SET Name='" + nd.Name + "' WHERE No='ND" + nd.NodeID + "'");
                    try
                    {
                        nd.HisWork.CheckPhysicsTable();
                    }
                    catch (Exception ex)
                    {
                        msg += "@���ڵ���ֶ�ʱ���ִ���:" + "NodeID" + nd.NodeID + " Table:" + nd.HisWork.EnMap.PhysicsTable + " Name:" + nd.Name + " , �ڵ�����NodeWorkTypeText=" + nd.NodeWorkTypeText + "���ִ���.@err=" + ex.Message;
                    }

                    // �ӱ��顣
                    Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                    foreach (Sys.MapDtl dtl in dtls)
                    {
                        msg += "@�����ϸ��:" + dtl.Name;
                        try
                        {
                            dtl.HisGEDtl.CheckPhysicsTable();
                        }
                        catch (Exception ex)
                        {
                            msg += "@�����ϸ��ʱ����ִ���" + ex.Message;
                        }
                    }
                    #endregion �޸����ڵ�����ݿ�.

                    MapAttrs mattrs = new MapAttrs("ND" + nd.NodeID);

                    #region �Խڵ�ķ��ʹ�����м��

                    msg += "@��Ϣ:��ʼ�Խڵ�ķ��ʹ�����м��.";

                    switch (nd.HisDeliveryWay)
                    {
                        case DeliveryWay.ByStation:
                            if (nd.NodeStations.Count == 0)
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ���λ��������û��Ϊ�ڵ�󶨸�λ��";
                            break;
                        case DeliveryWay.ByDept:
                            if (nd.NodeDepts.Count == 0)
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ����ţ�������û��Ϊ�ڵ�󶨲��š�";
                            break;
                        case DeliveryWay.ByBindEmp:
                            if (nd.NodeEmps.Count == 0)
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ���Ա��������û��Ϊ�ڵ����Ա��";
                            break;
                        case DeliveryWay.BySpecNodeEmp: /*��ָ���ĸ�λ����.*/
                        case DeliveryWay.BySpecNodeEmpStation: /*��ָ���ĸ�λ����.*/
                            if (nd.DeliveryParas.Trim().Length == 0)
                            {
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�ָ���ĸ�λ���㣬������û�����ýڵ���.</font>";
                            }
                            else
                            {
                                if (DataType.IsNumStr(nd.DeliveryParas) == false)
                                {
                                    msg += "@����:��û������ָ����λ�Ľڵ��ţ�Ŀǰ���õ�Ϊ{" + nd.DeliveryParas + "}";
                                }
                            }
                            break;
                        case DeliveryWay.ByDeptAndStation: /*���������λ�Ľ�������.*/
                            string mysql = string.Empty;
                            //added by liuxc,2015.6.30.
                            //���𼯳���BPMģʽ
                            if (BP.WF.Glo.OSModel == OSModel.WorkFlow)
                            {
                                mysql =
                                    "SELECT No FROM Port_Emp WHERE No IN (SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" +
                                    nd.NodeID + "))AND No IN (SELECT FK_Emp FROM " + BP.WF.Glo.EmpStation +
                                    " WHERE FK_Station IN ( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" +
                                    nd.NodeID + " )) ORDER BY No ";
                            }
                            else
                            {
                                mysql = "SELECT pdes.FK_Emp AS No"
                                        + " FROM   Port_DeptEmpStation pdes"
                                        + "        INNER JOIN WF_NodeDept wnd"
                                        + "             ON  wnd.FK_Dept = pdes.FK_Dept"
                                        + "             AND wnd.FK_Node = " + nd.NodeID
                                        + "        INNER JOIN WF_NodeStation wns"
                                        + "             ON  wns.FK_Station = pdes.FK_Station"
                                        + "             AND wnd.FK_Node =" + nd.NodeID
                                        + " ORDER BY"
                                        + "        pdes.FK_Emp";
                            }

                            DataTable mydt = DBAccess.RunSQLReturnTable(mysql);
                            if (mydt.Rows.Count == 0)
                                msg += "@����:���ո�λ�벿�ŵĽ����������û����Ա����{" + mysql + "}";
                            break;
                        case DeliveryWay.BySQL:
                        case DeliveryWay.BySQLAsSubThreadEmpsAndData:
                            if (nd.DeliveryParas.Trim().Length == 0)
                            {
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ��������û���ڽڵ����������ò�ѯsql����sql��Ҫ���ǲ�ѯ�������No,Name�����У�sql���ʽ��֧��@+�ֶα�������ϸ�ο������ֲ�.";
                            }
                            else
                            {
                                try
                                {
                                    string sql = nd.DeliveryParas;
                                    foreach (MapAttr item in mattrs)
                                    {
                                        if (item.IsNum)
                                            sql = sql.Replace("@" + item.KeyOfEn, "0");
                                        else
                                            sql = sql.Replace("@" + item.KeyOfEn, "'0'");
                                    }

                                    sql = sql.Replace("@WebUser.No", "'ss'");
                                    sql = sql.Replace("@WebUser.Name", "'ss'");
                                    sql = sql.Replace("@WebUser.FK_Dept", "'ss'");
                                    sql = sql.Replace("@WebUser.FK_DeptName", "'ss'");

                                    if (sql.Contains("@"))
                                        throw new Exception("����д��sql������д����ȷ��ʵ��ִ���У�û�б���ȫ�滻����" + sql);

                                    DataTable testDB = null;
                                    try
                                    {
                                        testDB = DBAccess.RunSQLReturnTable(sql);
                                    }
                                    catch (Exception ex)
                                    {
                                        msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ,ִ�д�������." + ex.Message;
                                    }

                                    if (testDB.Columns.Contains("No") == false || testDB.Columns.Contains("Name") == false)
                                    {
                                        msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ�����õ�sql�����Ϲ��򣬴�sql��Ҫ���ǲ�ѯ�������No,Name�����У�sql���ʽ��֧��@+�ֶα�������ϸ�ο������ֲ�.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    msg += ex.Message;
                                }
                            }
                            break;
                        case DeliveryWay.ByPreviousNodeFormEmpsField:
                            if (mattrs.Contains(BP.Sys.MapAttrAttr.KeyOfEn, nd.DeliveryParas) == false)
                            {
                                /*���ڵ��ֶ��Ƿ���FK_Emp�ֶ�*/
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�ָ���ڵ����Ա��������û���ڽڵ��������FK_Emp�ֶΣ���ϸ�ο������ֲ�.";
                            }
                            if (mattrs.Contains(BP.Sys.MapAttrAttr.KeyOfEn, "FK_Emp") == false)
                            {
                                /*���ڵ��ֶ��Ƿ���FK_Emp�ֶ�*/
                                msg += "@����:�������˸ýڵ�ķ��ʹ����ǰ�ָ���ڵ����Ա��������û���ڽڵ��������FK_Emp�ֶΣ���ϸ�ο������ֲ� .";
                            }
                            break;
                        case DeliveryWay.BySelected: /* ����һ��������Աѡ�� */
                            if (nd.IsStartNode)
                            {
                                msg += "@����:��ʼ�ڵ㲻������ָ����ѡ����Ա���ʹ���";
                                break;
                            }
                            break;
                        case DeliveryWay.ByPreviousNodeEmp: /* ����һ��������Աѡ�� */
                            if (nd.IsStartNode)
                            {
                                msg += "@����:�ڵ���ʹ������ô���:��ʼ�ڵ㣬��������������һ�ڵ�Ĺ�����Ա��ͬ.";
                                break;
                            }
                            break;
                        default:
                            break;
                    }
                    msg += "@�Խڵ�ķ��ʹ�����м�����....";
                    #endregion

                    #region ���ڵ�������������������Ķ���.
                    //������û�������������.
                    nd.IsCCFlow = false;

                    if (conds.Count != 0)
                    {
                        msg += "@��Ϣ:��ʼ���(" + nd.Name + ")��������:";
                        foreach (Cond cond in conds)
                        {
                            if (cond.FK_Node == nd.NodeID && cond.HisCondType == CondType.Flow)
                            {
                                nd.IsCCFlow = true;
                                nd.Update();
                            }

                            Node ndOfCond = new Node();
                            ndOfCond.NodeID = ndOfCond.NodeID;
                            if (ndOfCond.RetrieveFromDBSources() == 0)
                                continue;

                            try
                            {
                                if (cond.AttrKey.Length < 2)
                                    continue;
                                if (ndOfCond.HisWork.EnMap.Attrs.Contains(cond.AttrKey) == false)
                                    throw new Exception("@����:����:" + cond.AttrKey + " , " + cond.AttrName + " �����ڡ�");
                            }
                            catch (Exception ex)
                            {
                                msg += "@����:" + ex.Message;
                                ndOfCond.Delete();
                            }
                            msg += cond.AttrKey + cond.AttrName + cond.OperatorValue + "��";
                        }
                        msg += "@(" + nd.Name + ")��������������.....";
                    }
                    #endregion ���ڵ���������Ķ���.
                }
                #endregion

                msg += "@���̵Ļ�����Ϣ: ------ ";
                msg += "@���:  " + this.No + " ����:" + this.Name + " , �洢��:" + this.PTable;

                msg += "@��Ϣ:��ʼ���ڵ����̱���.";
                this.DoCheck_CheckRpt(this.HisNodes);

                #region ��齹���ֶ������Ƿ���Ч
                msg += "@��Ϣ:��ʼ���ڵ�Ľ����ֶ�";

                //���gerpt�ֶ�.
                GERpt rpt = this.HisGERpt;
                foreach (Node nd in nds)
                {
                    if (nd.FocusField.Trim() == "")
                    {
                        Work wk = nd.HisWork;
                        string attrKey = "";
                        foreach (Attr attr in wk.EnMap.Attrs)
                        {
                            if (attr.UIVisible == true && attr.UIIsDoc && attr.UIIsReadonly == false)
                                attrKey = attr.Desc + ":@" + attr.Key;
                        }
                        if (attrKey == "")
                            msg += "@����:�ڵ�ID:" + nd.NodeID + " ����:" + nd.Name + "������û�����ý����ֶΣ��ᵼ����Ϣд��켣��հף�Ϊ���ܹ���֤���̹켣�ǿɶ��������ý����ֶ�.";
                        else
                        {
                            msg += "@��Ϣ:�ڵ�ID:" + nd.NodeID + " ����:" + nd.Name + "������û�����ý����ֶΣ��ᵼ����Ϣд��켣��հף�Ϊ���ܹ���֤���̹켣�ǿɶ���ϵͳ�Զ������˽����ֶ�Ϊ" + attrKey + ".";
                            nd.FocusField = attrKey;
                            nd.DirectUpdate();
                        }
                        continue;
                    }

                    string strs = nd.FocusField.Clone() as string;
                    strs = Glo.DealExp(strs, rpt, "err");
                    if (strs.Contains("@") == true)
                    {
                        msg += "@����:�����ֶΣ�" + nd.FocusField + "���ڽڵ�(step:" + nd.Step + " ����:" + nd.Name + ")���������������Ч�����ﲻ���ڸ��ֶ�.";
                    }
                    else
                    {
                        msg += "@��ʾ:�ڵ��(" + nd.NodeID + "," + nd.Name + ")�����ֶΣ�" + nd.FocusField + "�������������ͨ��.";
                    }

                    if (this.IsMD5)
                    {
                        if (nd.HisWork.EnMap.Attrs.Contains(WorkAttr.MD5) == false)
                            nd.RepareMap();
                    }
                }
                msg += "@��Ϣ:���ڵ�Ľ����ֶ����.";
                #endregion

                #region ����������˵�.
                msg += "@��Ϣ:��ʼ����������˵�";
                foreach (Node nd in nds)
                {
                    if (nd.IsEval)
                    {
                        /*������������˵㣬���ڵ���Ƿ�߱��������˵��ر��ֶΣ�*/
                        string sql = "SELECT COUNT(*) FROM Sys_MapAttr WHERE FK_MapData='ND" + nd.NodeID + "' AND KeyOfEn IN ('EvalEmpNo','EvalEmpName','EvalEmpCent')";
                        if (DBAccess.RunSQLReturnValInt(sql) != 3)
                            msg += "@��Ϣ:�������˽ڵ�(" + nd.NodeID + "," + nd.Name + ")Ϊ�������˽ڵ㣬������û���ڸýڵ�������ñ�Ҫ�Ľڵ㿼���ֶ�.";
                    }
                }
                msg += "@����������˵����.";
                #endregion


                msg += "@���̱��������...";

                // �������.
                Node.CheckFlow(this);

                //���� V001 ��ͼ.
                CheckRptView(nds);
                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("@������̴���:" + ex.Message + " @" + ex.StackTrace);
            }
        }


        /// <summary>
        /// �ڵ���ֶ��������ͼ�飬������ͬ���ֶγ������Ͳ�ͬ�Ĵ����������ղ�ͬ��NDxxRpt����ͬ���ֶ�����Ϊ��׼
        /// </summary>
        /// <returns>�����</returns>
        private string CheckFormFields()
        {
            StringBuilder errorAppend = new StringBuilder();
            errorAppend.Append("@��Ϣ: -------- ���̽ڵ�����ֶ����ͼ��: ------ ");
            try
            {
                Nodes nds = new Nodes(this.No);
                string fk_mapdatas = "'ND" + int.Parse(this.No) + "Rpt'";
                foreach (Node nd in nds)
                {
                    fk_mapdatas += ",'ND" + nd.NodeID + "'";
                }

                //ɸѡ�����Ͳ�ͬ���ֶ�
                string checkSQL = "SELECT   AA.KEYOFEN, COUNT(*) AS MYNUM FROM ("
                                    + "  SELECT A.KEYOFEN,  MYDATATYPE,  COUNT(*) AS MYNUM"
                                    + "  FROM SYS_MAPATTR A WHERE FK_MAPDATA IN (" + fk_mapdatas + ") GROUP BY KEYOFEN, MYDATATYPE"
                                    + ")  AA GROUP BY  AA.KEYOFEN HAVING COUNT(*) > 1";
                DataTable dt_Fields = DBAccess.RunSQLReturnTable(checkSQL);
                foreach (DataRow row in dt_Fields.Rows)
                {
                    string keyOfEn = row["KEYOFEN"].ToString();
                    string myNum = row["MYNUM"].ToString();
                    int iMyNum = 0;
                    int.TryParse(myNum, out iMyNum);

                    //����2�������������ͣ����ֶ����е���
                    if (iMyNum > 2)
                    {
                        errorAppend.Append("@�����ֶ���" + keyOfEn + "�ڴ����̱�(" + fk_mapdatas + ")�д���2��������������(�磺int��float,varchar,datetime)�����ֶ��޸ġ�");
                        return errorAppend.ToString();
                    }

                    //����2���������ͣ��Բ�ͬ��NDxxRpt�ֶ�����Ϊ��
                    MapAttr baseMapAttr = new MapAttr();
                    MapAttr rptMapAttr = new MapAttr("ND" + int.Parse(this.No) + "Rpt", keyOfEn);

                    //Rpt���в����ڴ��ֶ�
                    if (rptMapAttr == null || rptMapAttr.MyPK == "")
                    {
                        this.DoCheck_CheckRpt(this.HisNodes);
                        rptMapAttr = new MapAttr("ND" + int.Parse(this.No) + "Rpt", keyOfEn);
                        this.HisGERpt.CheckPhysicsTable();
                    }

                    //Rpt���в����ڴ��ֶ�,ֱ�ӽ���
                    if (rptMapAttr == null || rptMapAttr.MyPK == "")
                        continue;

                    foreach (Node nd in nds)
                    {
                        MapAttr ndMapAttr = new MapAttr("ND" + nd.NodeID, keyOfEn);
                        if (ndMapAttr == null || ndMapAttr.MyPK == "")
                            continue;

                        //�ҳ���NDxxRpt�����ֶ��������Ͳ�ͬ�ı�
                        if (rptMapAttr.MyDataType != ndMapAttr.MyDataType)
                        {
                            baseMapAttr = ndMapAttr;
                            break;
                        }
                    }
                    errorAppend.Append("@������" + baseMapAttr.FK_MapData + "���ֶ�" + keyOfEn + "��������Ϊ��" + baseMapAttr.MyDataTypeStr);
                    //���ݻ����������޸��������Ͳ�ͬ�ı�
                    foreach (Node nd in nds)
                    {
                        MapAttr ndMapAttr = new MapAttr("ND" + nd.NodeID, keyOfEn);
                        //���������ֶεĽ��з���,������ͬ�Ľ��з���
                        if (ndMapAttr == null || ndMapAttr.MyPK == "" || baseMapAttr.MyPK == ndMapAttr.MyPK || baseMapAttr.MyDataType == ndMapAttr.MyDataType)
                            continue;

                        ndMapAttr.Name = baseMapAttr.Name;
                        ndMapAttr.MyDataType = baseMapAttr.MyDataType;
                        ndMapAttr.UIWidth = baseMapAttr.UIWidth;
                        ndMapAttr.UIHeight = baseMapAttr.UIHeight;
                        ndMapAttr.MinLen = baseMapAttr.MinLen;
                        ndMapAttr.MaxLen = baseMapAttr.MaxLen;
                        if (ndMapAttr.Update() > 0)
                            errorAppend.Append("@�޸���" + "ND" + nd.NodeID + " ���ֶ�" + keyOfEn + "�޸�Ϊ��" + baseMapAttr.MyDataTypeStr);
                        else
                            errorAppend.Append("@����:�޸�" + "ND" + nd.NodeID + " ���ֶ�" + keyOfEn + "�޸�Ϊ��" + baseMapAttr.MyDataTypeStr + "ʧ�ܡ�");
                    }
                    //�޸�NDxxRpt
                    rptMapAttr.Name = baseMapAttr.Name;
                    rptMapAttr.MyDataType = baseMapAttr.MyDataType;
                    rptMapAttr.UIWidth = baseMapAttr.UIWidth;
                    rptMapAttr.UIHeight = baseMapAttr.UIHeight;
                    rptMapAttr.MinLen = baseMapAttr.MinLen;
                    rptMapAttr.MaxLen = baseMapAttr.MaxLen;
                    if (rptMapAttr.Update() > 0)
                        errorAppend.Append("@�޸���" + "ND" + int.Parse(this.No) + "Rpt ���ֶ�" + keyOfEn + "�޸�Ϊ��" + baseMapAttr.MyDataTypeStr);
                    else
                        errorAppend.Append("@����:�޸�" + "ND" + int.Parse(this.No) + "Rpt ���ֶ�" + keyOfEn + "�޸�Ϊ��" + baseMapAttr.MyDataTypeStr + "ʧ�ܡ�");
                }
            }
            catch (Exception ex)
            {
                errorAppend.Append("@����:" + ex.Message);
            }
            return errorAppend.ToString();
        }
        #endregion ��������.

        #region ��������ģ�塣
        readonly static string PathFlowDesc;
        static Flow()
        {
            PathFlowDesc = SystemConfig.PathOfDataUser + "FlowDesc\\";
        }
        /// <summary>
        /// ��������ģ��
        /// </summary>
        /// <returns></returns>
        public string GenerFlowXmlTemplete()
        {
            string name = this.Name;
            name = BP.Tools.StringExpressionCalculate.ReplaceBadCharOfFileName(name);

            string path = this.No + "." + name;
            path = PathFlowDesc + path + "\\";
            this.DoExpFlowXmlTemplete(path);

            name = path + name + "." + this.Ver.Replace(":", "_") + ".xml";
            return name;
        }
        /// <summary>
        /// ��������ģ��
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataSet DoExpFlowXmlTemplete(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            DataSet ds = GetFlow(path);
            if (ds != null)
            {
                string name = this.Name;
                name = BP.Tools.StringExpressionCalculate.ReplaceBadCharOfFileName(name);
                name = name + "." + this.Ver.Replace(":", "_") + ".xml";
                string filePath = path + name;
                ds.WriteXml(filePath);
            }
            return ds;
        }

        //xml�ļ��Ƿ����ڲ�����
        static bool isXmlLocked;
        /// <summary>
        /// ���ݵ�ǰ���̵��û�xml�ļ�
        /// �û�ÿ�α���ʱ����
        /// �����쳣д����־,����ʧ�ܲ�Ӱ����������
        /// </summary>
        public void WriteToXml()
        {
            try
            {
                string name = this.No + "." + this.Name;
                name = BP.Tools.StringExpressionCalculate.ReplaceBadCharOfFileName(name);
                string path = PathFlowDesc + name + "\\";
                DataSet ds = GetFlow(path);
                if (ds == null)
                    return;

                string directory = this.No + "." + this.Name;
                directory = BP.Tools.StringExpressionCalculate.ReplaceBadCharOfFileName(directory);
                path = PathFlowDesc + directory + "\\";
                string xmlName = path + "Flow" + ".xml";

                if (!isXmlLocked)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path);
                        else if (System.IO.File.Exists(xmlName))
                        {
                            DateTime time = File.GetLastWriteTime(xmlName);
                            string xmlNameOld = path + "Flow" + time.ToString("@yyyyMMddHHmmss") + ".xml";

                            isXmlLocked = true;
                            if (File.Exists(xmlNameOld))
                                File.Delete(xmlNameOld);
                            File.Move(xmlName, xmlNameOld);
                        }
                    }

                    if (!string.IsNullOrEmpty(xmlName))
                    {
                        ds.WriteXml(xmlName);
                        isXmlLocked = false;
                    }
                }
            }
            catch (Exception e)
            {
                isXmlLocked = false;
                BP.DA.Log.DefaultLogWriteLineError("����ģ���ļ����ݴ���:" + e.Message);
            }
        }


        public DataSet GetFlow(string path)
        {
            // �����е����ݶ��洢�����
            DataSet ds = new DataSet();

            // ������Ϣ��
            string sql = "SELECT * FROM WF_Flow WHERE No='" + this.No + "'";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_Flow";
            ds.Tables.Add(dt);

            // �ڵ���Ϣ
            sql = "SELECT * FROM WF_Node WHERE FK_Flow='" + this.No + "'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_Node";
            ds.Tables.Add(dt);

            // ����ģ��. 
            BillTemplates tmps = new BillTemplates(this.No);
            string pks = "";
            foreach (BillTemplate tmp in tmps)
            {
                try
                {
                    if (path != null)
                        System.IO.File.Copy(SystemConfig.PathOfDataUser + @"\CyclostyleFile\" + tmp.No + ".rtf", path + "\\" + tmp.No + ".rtf", true);
                }
                catch
                {
                    pks += "@" + tmp.PKVal;
                    tmp.Delete();
                }
            }
            tmps.Remove(pks);
            ds.Tables.Add(tmps.ToDataTableField("WF_BillTemplate"));

            string sqlin = "SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "'";
            // ���̱���
            sql = "SELECT * FROM WF_FlowFormTree WHERE FK_Flow='" + this.No + "'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_FlowFormTree";
            ds.Tables.Add(dt);

            //// ���̱�
            //sql = "SELECT * FROM WF_FlowForm WHERE FK_Flow='" + this.No + "'";
            //dt = DBAccess.RunSQLReturnTable(sql);
            //dt.TableName = "WF_FlowForm";
            //ds.Tables.Add(dt);

            //// �ڵ��Ȩ��
            //sql = "SELECT * FROM WF_NodeForm WHERE FK_Node IN (" + sqlin + ")";
            //dt = DBAccess.RunSQLReturnTable(sql);
            //dt.TableName = "WF_NodeForm";
            //ds.Tables.Add(dt);

            // ������Ϣ
            sql = "SELECT * FROM WF_Cond WHERE FK_Flow='" + this.No + "'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_Cond";
            ds.Tables.Add(dt);

            // ת�����.
            sql = "SELECT * FROM WF_TurnTo WHERE FK_Flow='" + this.No + "'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_TurnTo";
            ds.Tables.Add(dt);

            // �ڵ������.
            sql = "SELECT * FROM WF_FrmNode WHERE FK_Flow='" + this.No + "'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_FrmNode";
            ds.Tables.Add(dt);

            // ������.
            sql = "SELECT * FROM Sys_FrmSln WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmSln";
            ds.Tables.Add(dt);

            // ����
            sql = "SELECT * FROM WF_Direction WHERE Node IN (" + sqlin + ") OR ToNode In (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_Direction";
            ds.Tables.Add(dt);

            //// Ӧ������ FAppSet
            //sql = "SELECT * FROM WF_FAppSet WHERE FK_Flow='" + this.No + "'";
            //dt = DBAccess.RunSQLReturnTable(sql);
            //dt.TableName = "WF_FAppSet";
            //ds.Tables.Add(dt);

            // ���̱�ǩ.
            LabNotes labs = new LabNotes(this.No);
            ds.Tables.Add(labs.ToDataTableField("WF_LabNote"));

            // ��Ϣ����.
            Listens lts = new Listens(this.No);
            ds.Tables.Add(lts.ToDataTableField("WF_Listen"));

            // ���˻صĽڵ㡣
            sql = "SELECT * FROM WF_NodeReturn WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeReturn";
            ds.Tables.Add(dt);

            // ��������
            sql = "SELECT * FROM WF_NodeToolbar WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeToolbar";
            ds.Tables.Add(dt);

            // �ڵ��벿�š�
            sql = "SELECT * FROM WF_NodeDept WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeDept";
            ds.Tables.Add(dt);


            // �ڵ����λȨ�ޡ�
            sql = "SELECT * FROM WF_NodeStation WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeStation";
            ds.Tables.Add(dt);

            // �ڵ�����Ա��
            sql = "SELECT * FROM WF_NodeEmp WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeEmp";
            ds.Tables.Add(dt);

            // ������Ա��
            sql = "SELECT * FROM WF_CCEmp WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_CCEmp";
            ds.Tables.Add(dt);

            // ���Ͳ��š�
            sql = "SELECT * FROM WF_CCDept WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_CCDept";
            ds.Tables.Add(dt);

            // ���Ͳ��š�
            sql = "SELECT * FROM WF_CCStation WHERE FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_CCStation";
            ds.Tables.Add(dt);

            //// ���̱���
            //WFRpts rpts = new WFRpts(this.No);
            //// rpts.SaveToXml(path + "WFRpts.xml");
            //ds.Tables.Add(rpts.ToDataTableField("WF_Rpt"));

            //// ���̱�������
            //RptAttrs rptAttrs = new RptAttrs();
            //rptAttrs.RetrieveAll();
            //ds.Tables.Add(rptAttrs.ToDataTableField("RptAttrs"));

            //// ���̱������Ȩ�ޡ�
            //RptStations rptStations = new RptStations(this.No);
            //rptStations.RetrieveAll();
            ////  rptStations.SaveToXml(path + "RptStations.xml");
            //ds.Tables.Add(rptStations.ToDataTableField("RptStations"));

            //// ���̱�����Ա����Ȩ�ޡ�
            //RptEmps rptEmps = new RptEmps(this.No);
            //rptEmps.RetrieveAll();

            // rptEmps.SaveToXml(path + "RptEmps.xml");
            // ds.Tables.Add(rptEmps.ToDataTableField("RptEmps"));

            int flowID = int.Parse(this.No);
            sql = "SELECT * FROM Sys_MapData WHERE " + Glo.MapDataLikeKey(this.No, "No");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapData";
            ds.Tables.Add(dt);


            // Sys_MapAttr.
            sql = "SELECT * FROM Sys_MapAttr WHERE  FK_MapData LIKE 'ND" + flowID + "%' ORDER BY FK_MapData,Idx";
            //sql = "SELECT * FROM Sys_MapAttr WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData") + "  ORDER BY FK_MapData,Idx";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapAttr";
            ds.Tables.Add(dt);

            // Sys_EnumMain
            //sql = "SELECT * FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData") +")";
            sql = "SELECT * FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%')";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_EnumMain";
            ds.Tables.Add(dt);

            // Sys_Enum
            sql = "SELECT * FROM Sys_Enum WHERE EnumKey IN ( SELECT No FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' ) )";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_Enum";
            ds.Tables.Add(dt);

            // Sys_MapDtl
            sql = "SELECT * FROM Sys_MapDtl WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapDtl";
            ds.Tables.Add(dt);

            // Sys_MapExt
            //sql = "SELECT * FROM Sys_MapExt WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            sql = "SELECT * FROM Sys_MapExt WHERE FK_MapData LIKE 'ND" + flowID + "%'";  // +Glo.MapDataLikeKey(this.No, "FK_MapData");

            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapExt";
            ds.Tables.Add(dt);

            // Sys_GroupField
            sql = "SELECT * FROM Sys_GroupField WHERE " + Glo.MapDataLikeKey(this.No, "EnName");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_GroupField";
            ds.Tables.Add(dt);

            // Sys_MapFrame
            sql = "SELECT * FROM Sys_MapFrame WHERE" + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapFrame";
            ds.Tables.Add(dt);

            // Sys_MapM2M
            sql = "SELECT * FROM Sys_MapM2M WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapM2M";
            ds.Tables.Add(dt);

            // Sys_FrmLine.
            sql = "SELECT * FROM Sys_FrmLine WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLine";
            ds.Tables.Add(dt);

            // Sys_FrmLab.
            sql = "SELECT * FROM Sys_FrmLab WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLab";
            ds.Tables.Add(dt);

            // Sys_FrmEle.
            sql = "SELECT * FROM Sys_FrmEle WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmEle";
            ds.Tables.Add(dt);

            // Sys_FrmLink.
            sql = "SELECT * FROM Sys_FrmLink WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLink";
            ds.Tables.Add(dt);

            // Sys_FrmRB.
            sql = "SELECT * FROM Sys_FrmRB WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmRB";
            ds.Tables.Add(dt);

            // Sys_FrmImgAth.
            sql = "SELECT * FROM Sys_FrmImgAth WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmImgAth";
            ds.Tables.Add(dt);

            // Sys_FrmImg.
            sql = "SELECT * FROM Sys_FrmImg WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmImg";
            ds.Tables.Add(dt);

            // Sys_FrmAttachment.
            sql = "SELECT * FROM Sys_FrmAttachment WHERE FK_Node=0 AND " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmAttachment";
            ds.Tables.Add(dt);

            // Sys_FrmEvent.
            sql = "SELECT * FROM Sys_FrmEvent WHERE " + Glo.MapDataLikeKey(this.No, "FK_MapData");
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmEvent";
            ds.Tables.Add(dt);

            return ds;
        }

        #endregion ��������ģ�塣

        #region �������÷���1
        /// <summary>
        /// ��������Rpt��
        /// </summary>
        public void CheckRptOfReset()
        {
            string fk_mapData = "ND" + int.Parse(this.No) + "Rpt";
            string sql = "DELETE FROM Sys_MapAttr WHERE FK_MapData='" + fk_mapData + "'";
            DBAccess.RunSQL(sql);

            sql = "DELETE FROM Sys_MapData WHERE No='" + fk_mapData + "'";
            DBAccess.RunSQL(sql);
            this.DoCheck_CheckRpt(this.HisNodes);
        }
        /// <summary>
        /// ����װ��
        /// </summary>
        /// <returns></returns>
        public string DoReloadRptData()
        {
            this.DoCheck_CheckRpt(this.HisNodes);

            // ��鱨�������Ƿ�ʧ��

            if (this.HisDataStoreModel != DataStoreModel.ByCCFlow)
                return "@����" + this.No + this.Name + "�����ݴ洢�ǹ켣ģʽ������������.";

            DBAccess.RunSQL("DELETE FROM " + this.PTable);

            string sql = "SELECT OID FROM ND" + int.Parse(this.No) + "01 WHERE  OID NOT IN (SELECT OID FROM  " + this.PTable + " ) ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            this.CheckRptData(this.HisNodes, dt);

            return "@����:" + dt.Rows.Count + "��(" + this.Name + ")���ݱ�װ�سɹ���";
        }
        /// <summary>
        /// ������޸���������
        /// </summary>
        /// <param name="nds"></param>
        /// <param name="dt"></param>
        private string CheckRptData(Nodes nds, DataTable dt)
        {
            GERpt rpt = new GERpt("ND" + int.Parse(this.No) + "Rpt");
            string err = "";
            foreach (DataRow dr in dt.Rows)
            {
                rpt.ResetDefaultVal();
                int oid = int.Parse(dr[0].ToString());
                rpt.SetValByKey("OID", oid);
                Work startWork = null;
                Work endWK = null;
                string flowEmps = "";
                foreach (Node nd in nds)
                {
                    try
                    {
                        Work wk = nd.HisWork;
                        wk.OID = oid;
                        if (wk.RetrieveFromDBSources() == 0)
                            continue;

                        rpt.Copy(wk);
                        if (nd.NodeID == int.Parse(this.No + "01"))
                            startWork = wk;

                        try
                        {
                            if (flowEmps.Contains("@" + wk.Rec + ","))
                                continue;

                            flowEmps += "@" + wk.Rec + "," + wk.RecOfEmp.Name;
                        }
                        catch
                        {
                        }
                        endWK = wk;
                    }
                    catch (Exception ex)
                    {
                        err += ex.Message;
                    }
                }

                if (startWork == null || endWK == null)
                    continue;

                rpt.SetValByKey("OID", oid);
                rpt.FK_NY = startWork.GetValStrByKey("RDT").Substring(0, 7);
                rpt.FK_Dept = startWork.GetValStrByKey("FK_Dept");
                if (string.IsNullOrEmpty(rpt.FK_Dept))
                {
                    string fk_dept = DBAccess.RunSQLReturnString("SELECT FK_Dept FROM Port_Emp WHERE No='" + startWork.Rec + "'");
                    rpt.FK_Dept = fk_dept;

                    startWork.SetValByKey("FK_Dept", fk_dept);
                    startWork.Update();
                }
                rpt.Title = startWork.GetValStrByKey("Title");
                string wfState = DBAccess.RunSQLReturnStringIsNull("SELECT WFState FROM WF_GenerWorkFlow WHERE WorkID=" + oid, "1");
                rpt.WFState = (WFState)int.Parse(wfState);
                rpt.FlowStarter = startWork.Rec;
                rpt.FlowStartRDT = startWork.RDT;
                rpt.FID = startWork.GetValIntByKey("FID");
                rpt.FlowEmps = flowEmps;
                rpt.FlowEnder = endWK.Rec;
                rpt.FlowEnderRDT = endWK.RDT;
                rpt.FlowEndNode = endWK.NodeID;
                rpt.MyNum = 1;

                //�޸������ֶΡ�
                WorkNode wn = new WorkNode(startWork, this.HisStartNode);
                rpt.Title = WorkNode.GenerTitle(this, startWork);
                try
                {
                    TimeSpan ts = endWK.RDT_DateTime - startWork.RDT_DateTime;
                    rpt.FlowDaySpan = ts.Days;
                }
                catch
                {
                }
                rpt.InsertAsOID(rpt.OID);
            } // ����ѭ����
            return err;
        }
        /// <summary>
        /// ������ϸ������Ϣ
        /// </summary>
        /// <param name="nds"></param>
        private void CheckRptDtl(Nodes nds)
        {
            MapDtls dtlsDtl = new MapDtls();
            dtlsDtl.Retrieve(MapDtlAttr.FK_MapData, "ND" + int.Parse(this.No) + "Rpt");
            foreach (MapDtl dtl in dtlsDtl)
            {
                dtl.Delete();
            }

            //  dtlsDtl.Delete(MapDtlAttr.FK_MapData, "ND" + int.Parse(this.No) + "Rpt");
            foreach (Node nd in nds)
            {
                if (nd.IsEndNode == false)
                    continue;

                // ȡ�����ӱ�.
                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                if (dtls.Count == 0)
                    continue;

                string rpt = "ND" + int.Parse(this.No) + "Rpt";
                int i = 0;
                foreach (MapDtl dtl in dtls)
                {
                    i++;
                    string rptDtlNo = "ND" + int.Parse(this.No) + "RptDtl" + i.ToString();
                    MapDtl rtpDtl = new MapDtl();
                    rtpDtl.No = rptDtlNo;
                    if (rtpDtl.RetrieveFromDBSources() == 0)
                    {
                        rtpDtl.Copy(dtl);
                        rtpDtl.No = rptDtlNo;
                        rtpDtl.FK_MapData = rpt;
                        rtpDtl.PTable = rptDtlNo;
                        rtpDtl.GroupID = -1;
                        rtpDtl.Insert();
                    }



                    MapAttrs attrsRptDtl = new MapAttrs(rptDtlNo);
                    MapAttrs attrs = new MapAttrs(dtl.No);
                    foreach (MapAttr attr in attrs)
                    {
                        if (attrsRptDtl.Contains(MapAttrAttr.KeyOfEn, attr.KeyOfEn) == true)
                            continue;

                        MapAttr attrN = new MapAttr();
                        attrN.Copy(attr);
                        attrN.FK_MapData = rptDtlNo;
                        switch (attr.KeyOfEn)
                        {
                            case "FK_NY":
                                attrN.UIVisible = true;
                                attrN.Idx = 100;
                                attrN.UIWidth = 60;
                                break;
                            case "RDT":
                                attrN.UIVisible = true;
                                attrN.Idx = 100;
                                attrN.UIWidth = 60;
                                break;
                            case "Rec":
                                attrN.UIVisible = true;
                                attrN.Idx = 100;
                                attrN.UIWidth = 60;
                                break;
                            default:
                                break;
                        }

                        attrN.Save();
                    }

                    GEDtl geDtl = new GEDtl(rptDtlNo);
                    geDtl.CheckPhysicsTable();
                }
            }
        }
        /// <summary>
        /// �������нڵ���ͼ
        /// </summary>
        /// <param name="nds"></param>
        private void CheckRptView(Nodes nds)
        {
            if (this.HisDataStoreModel == DataStoreModel.SpecTable)
                return;

            string viewName = "V" + this.No;
            string sql = "CREATE VIEW " + viewName + " ";
            sql += "/* CCFlow Auto Create :" + this.Name + " Date:" + DateTime.Now.ToString("yyyy-MM-dd") + " */ ";
            sql += "\r\n (MyPK,FK_Node,OID,FID,RDT,FK_NY,CDT,Rec,Emps,FK_Dept,MyNum) AS ";
            bool is1 = false;
            foreach (Node nd in nds)
            {
                //  nd.HisWork.CheckPhysicsTable();
                if (is1 == false)
                    is1 = true;
                else
                    sql += "\r\n UNION ";

                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle:
                    case DBType.Informix:
                        sql += "\r\n SELECT '" + nd.NodeID + "' || '_'|| OID||'_'|| FID  AS MyPK, '" + nd.NodeID + "' AS FK_Node,OID,FID,RDT,SUBSTR(RDT,1,7) AS FK_NY,CDT,Rec,Emps,FK_Dept, 1 AS MyNum FROM ND" + nd.NodeID + " ";
                        break;
                    case DBType.MySQL:
                        sql += "\r\n SELECT '" + nd.NodeID + "'+'_'+CHAR(OID)  +'_'+CHAR(FID)  AS MyPK, '" + nd.NodeID + "' AS FK_Node,OID,FID,RDT," + BP.Sys.SystemConfig.AppCenterDBSubstringStr + "(RDT,1,7) AS FK_NY,CDT,Rec,Emps,FK_Dept, 1 AS MyNum FROM ND" + nd.NodeID + " ";
                        break;
                    default:
                        sql += "\r\n SELECT '" + nd.NodeID + "'+'_'+CAST(OID AS varchar(10)) +'_'+CAST(FID AS VARCHAR(10)) AS MyPK, '" + nd.NodeID + "' AS FK_Node,OID,FID,RDT," + BP.Sys.SystemConfig.AppCenterDBSubstringStr + "(RDT,1,7) AS FK_NY,CDT,Rec,Emps,FK_Dept, 1 AS MyNum FROM ND" + nd.NodeID + " ";
                        break;
                }
            }
            if (SystemConfig.AppCenterDBType != DBType.Informix)
                sql += "\r\n GO ";

            try
            {
                if (DBAccess.IsExitsObject(viewName) == true)
                    DBAccess.RunSQL("DROP VIEW " + viewName);
            }
            catch
            {
            }

            try
            {
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                BP.DA.Log.DefaultLogWriteLineError(ex.Message);
            }
        }
        /// <summary>
        /// ������ݱ���.
        /// </summary>
        /// <param name="nds"></param>
        private void DoCheck_CheckRpt(Nodes nds)
        {
            string fk_mapData = "ND" + int.Parse(this.No) + "Rpt";
            string flowId = int.Parse(this.No).ToString();

            // ����track��.
            Track.CreateOrRepairTrackTable(flowId);

            #region �����ֶΡ�
            string sql = "";
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                case DBType.MSSQL:
                    sql = "SELECT distinct  KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
                    break;
                case DBType.Informix:
                    sql = "SELECT distinct  KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
                    break;
                case DBType.MySQL:
                    sql = "SELECT DISTINCT KeyOfEn FROM Sys_MapAttr  WHERE FK_MapData IN (SELECT X.No FROM ( SELECT CONCAT('ND',NodeID) AS No FROM WF_Node WHERE FK_Flow='" + this.No + "') AS X )";
                    break;
                default:
                    sql = "SELECT distinct  KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
                    break;
            }

            if (SystemConfig.AppCenterDBType == DBType.MySQL)
            {
                sql = "SELECT A.* FROM (" + sql + ") AS A ";
                string sql3 = "DELETE FROM Sys_MapAttr WHERE KeyOfEn NOT IN (" + sql + ") AND FK_MapData='" + fk_mapData + "' ";
                DBAccess.RunSQL(sql3); // ɾ�������ڵ��ֶ�.
            }
            else
            {
                string sql2 = "DELETE FROM Sys_MapAttr WHERE KeyOfEn NOT IN (" + sql + ") AND FK_MapData='" + fk_mapData + "' ";
                DBAccess.RunSQL(sql2); // ɾ�������ڵ��ֶ�.
            }


            // ������û���ֶΡ�
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                    sql = "SELECT MyPK, KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
                    break;
                case DBType.MySQL:
                    sql = "SELECT MyPK, KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN (SELECT X.No FROM ( SELECT CONCAT('ND',NodeID) AS No FROM WF_Node WHERE FK_Flow='" + this.No + "') AS X )";
                    break;
                default:
                    sql = "SELECT MyPK, KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
                    break;
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            sql = "SELECT KeyOfEn FROM Sys_MapAttr WHERE FK_MapData='ND" + flowId + "Rpt'";
            DataTable dtExits = DBAccess.RunSQLReturnTable(sql);
            string pks = "@";
            foreach (DataRow dr in dtExits.Rows)
                pks += dr[0] + "@";

            foreach (DataRow dr in dt.Rows)
            {
                string mypk = dr["MyPK"].ToString();
                if (pks.Contains("@" + dr["KeyOfEn"].ToString() + "@"))
                    continue;

                pks += dr["KeyOfEn"].ToString() + "@";

                BP.Sys.MapAttr ma = new BP.Sys.MapAttr(mypk);
                ma.MyPK = "ND" + flowId + "Rpt_" + ma.KeyOfEn;
                ma.FK_MapData = "ND" + flowId + "Rpt";
                ma.UIIsEnable = false;

                if (ma.DefValReal.Contains("@"))
                {
                    /*�����һ���б����Ĳ���.*/
                    ma.DefVal = "";
                }

                try
                {
                    ma.Insert();
                }
                catch
                {
                }
            }

            MapAttrs attrs = new MapAttrs(fk_mapData);

            // ����mapData.
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + flowId + "Rpt";
            if (md.RetrieveFromDBSources() == 0)
            {
                md.Name = this.Name;
                md.PTable = this.PTable;
                md.Insert();
            }
            else
            {
                md.Name = this.Name;
                md.PTable = this.PTable;
                md.Update();
            }
            #endregion �����ֶΡ�

            #region �����������ֶε�NDxxxRpt.
            int groupID = 0;
            foreach (MapAttr attr in attrs)
            {
                switch (attr.KeyOfEn)
                {
                    case StartWorkAttr.FK_Dept:
                        attr.UIBindKey = "BP.Port.Depts";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.GroupID = groupID;// gfs[0].GetValIntByKey("OID");
                        attr.UIIsEnable = false;
                        attr.DefVal = "";
                        attr.MaxLen = 100;
                        attr.Update();
                        break;
                    case "FK_NY":
                        attr.UIBindKey = "BP.Pub.NYs";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.UIIsEnable = false;
                        attr.GroupID = groupID;
                        attr.Update();
                        break;
                    case "FK_Emp":
                        break;
                    default:
                        break;
                }
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.OID) == false)
            {
                /* WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "OID";
                attr.Name = "WorkID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.WFState) == false)
            {
                /* ����״̬ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.WFState;
                attr.Name = "����״̬"; //  
                attr.MyDataType = DataType.AppInt;
                attr.UIBindKey = GERptAttr.WFState;
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.Enum;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.WFSta) == false)
            {
                /* ����״̬Ext */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.WFSta;
                attr.Name = "״̬"; //  
                attr.MyDataType = DataType.AppInt;
                attr.UIBindKey = GERptAttr.WFSta;
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.Enum;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEmps) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEmps; // "FlowEmps";
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowStarter) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStarter;
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;

                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;

                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowStartRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStartRDT; // "FlowStartRDT";
                attr.Name = "����ʱ��";
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEnder) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnder;
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;
                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEnderRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnderRDT; // "FlowStartRDT";
                attr.Name = "����ʱ��";
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEndNode) == false)
            {
                /* �����ڵ� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEndNode;
                attr.Name = "�����ڵ�";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowDaySpan) == false)
            {
                /* FlowDaySpan */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowDaySpan; // "FlowStartRDT";
                attr.Name = "���(��)";
                attr.MyDataType = DataType.AppMoney;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = true;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.PFlowNo) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PFlowNo;
                attr.Name = "�����̱��"; //  ���������̱��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 3;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.PNodeID) == false)
            {
                /* ������WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PNodeID;
                attr.Name = "�����������Ľڵ�";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.PWorkID) == false)
            {
                /* ������WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PWorkID;
                attr.Name = "������WorkID";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.PEmp) == false)
            {
                /* ���������̵���Ա */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PEmp;
                attr.Name = "���������̵���Ա";
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 32;
                attr.Idx = -100;
                attr.Insert();
            }


            if (attrs.Contains(md.No + "_" + GERptAttr.CWorkID) == false)
            {
                /* ��������WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.CWorkID;
                attr.Name = "��������WorkID";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.CFlowNo) == false)
            {
                /* �������̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.CFlowNo;
                attr.Name = "�������̱��";
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 3;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.BillNo) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.BillNo;
                attr.Name = "���ݱ��"; //  ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 100;
                attr.Idx = -100;
                attr.Insert();
            }


            if (attrs.Contains(md.No + "_MyNum") == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = "��";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "1";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.AtPara) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.AtPara;
                attr.Name = "����"; // ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 4000;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.GUID) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.GUID;
                attr.Name = "GUID"; // ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 32;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.PrjNo) == false)
            {
                /* ��Ŀ��� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PrjNo;
                attr.Name = "��Ŀ���"; //  ��Ŀ���
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 100;
                attr.Idx = -100;
                attr.Insert();
            }
            if (attrs.Contains(md.No + "_" + GERptAttr.PrjName) == false)
            {
                /* ��Ŀ���� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PrjName;
                attr.Name = "��Ŀ����"; //  ��Ŀ����
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 100;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowNote) == false)
            {
                /* ������Ϣ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowNote;
                attr.Name = "������Ϣ"; //  ���������̱��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 500;
                attr.Idx = -100;
                attr.Insert();
            }

            #endregion �����������ֶΡ�



            #region Ϊ�����ֶ����÷��顣
            try
            {
                string flowInfo = "������Ϣ";
                GroupField flowGF = new GroupField();
                int num = flowGF.Retrieve(GroupFieldAttr.EnName, fk_mapData, GroupFieldAttr.Lab, "������Ϣ");
                if (num == 0)
                {
                    flowGF = new GroupField();
                    flowGF.Lab = flowInfo;
                    flowGF.EnName = fk_mapData;
                    flowGF.Idx = -1;
                    flowGF.Insert();
                }
                sql = "UPDATE Sys_MapAttr SET GroupID=" + flowGF.OID + " WHERE  FK_MapData='" + fk_mapData + "'  AND KeyOfEn IN('" + GERptAttr.PFlowNo + "','" + GERptAttr.PWorkID + "','" + GERptAttr.MyNum + "','" + GERptAttr.FK_Dept + "','" + GERptAttr.FK_NY + "','" + GERptAttr.FlowDaySpan + "','" + GERptAttr.FlowEmps + "','" + GERptAttr.FlowEnder + "','" + GERptAttr.FlowEnderRDT + "','" + GERptAttr.FlowEndNode + "','" + GERptAttr.FlowStarter + "','" + GERptAttr.FlowStartRDT + "','" + GERptAttr.WFState + "')";
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError(ex.Message);
            }
            #endregion Ϊ�����ֶ����÷���

            #region β����.
            GERpt sw = this.HisGERpt;
            sw.CheckPhysicsTable();  //�ñ�����������.

            DBAccess.RunSQL("DELETE FROM Sys_GroupField WHERE EnName='" + fk_mapData + "' AND OID NOT IN (SELECT GroupID FROM Sys_MapAttr WHERE FK_MapData = '" + fk_mapData + "')");
            DBAccess.RunSQL("UPDATE Sys_MapAttr SET Name='�ʱ��' WHERE FK_MapData='ND" + flowId + "Rpt' AND KeyOfEn='CDT'");
            DBAccess.RunSQL("UPDATE Sys_MapAttr SET Name='������' WHERE FK_MapData='ND" + flowId + "Rpt' AND KeyOfEn='Emps'");
            #endregion β����.

            #region ������.
            string mapRpt = "ND" + int.Parse(No) + "MyRpt";
            MapData mapData = new MapData();
            mapData.No = mapRpt;
            if (mapData.RetrieveFromDBSources() == 0)
            {
                mapData.No = mapRpt;
                mapData.PTable = this.PTable;
                mapData.Name = this.Name + "����";
                mapData.Note = "Ĭ��.";

                //Ĭ�ϵĲ�ѯ�ֶ�.
                mapData.Insert();

                BP.WF.Rpt.MapRpt rpt = new Rpt.MapRpt();
                rpt.No = mapRpt;
                rpt.RetrieveFromDBSources();

                rpt.FK_Flow = this.No;
                rpt.ParentMapData = "ND" + int.Parse(this.No) + "Rpt";
                rpt.ResetIt();
                rpt.Update();
            }

            if (mapData.PTable != this.PTable)
            {
                mapData.PTable = this.PTable;
                mapData.Update();
            }

            //��������ֶ�.
            attrs = new MapAttrs(mapData.No);

            #region �����������ֶε�NDxxxRpt.
            foreach (MapAttr attr in attrs)
            {
                switch (attr.KeyOfEn)
                {
                    case StartWorkAttr.FK_Dept:
                        attr.UIBindKey = "BP.Port.Depts";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.GroupID = groupID;// gfs[0].GetValIntByKey("OID");
                        attr.UIIsEnable = false;
                        attr.DefVal = "";
                        attr.MaxLen = 100;
                        attr.Update();
                        break;
                    case "FK_NY":
                        attr.UIBindKey = "BP.Pub.NYs";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.UIIsEnable = false;
                        attr.GroupID = groupID;
                        attr.Update();
                        break;
                    case "FK_Emp":
                        break;
                    default:
                        break;
                }
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.OID) == false)
            {
                /* WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "OID";
                attr.Name = "WorkID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.Insert();
            }

            //if (attrs.Contains(mapData.No + "_" + GERptAttr.WFState) == false)
            //{
            //    /* ����״̬ */
            //    MapAttr attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.KeyOfEn = GERptAttr.WFState;
            //    attr.Name = "����״̬"; //  
            //    attr.MyDataType = DataType.AppInt;
            //    attr.UIBindKey = GERptAttr.WFState;
            //    attr.UIContralType = UIContralType.DDL;
            //    attr.LGType = FieldTypeS.Enum;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 1000;
            //    attr.Idx = -1;
            //    attr.Insert();
            //}

            if (attrs.Contains(mapData.No + "_" + GERptAttr.WFSta) == false)
            {
                /* ����״̬Ext */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.WFSta;
                attr.Name = "״̬"; //  
                attr.MyDataType = DataType.AppInt;
                attr.UIBindKey = GERptAttr.WFSta;
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.Enum;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowEmps) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEmps; // "FlowEmps";
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowStarter) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStarter;
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;

                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;

                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowStartRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStartRDT; // "FlowStartRDT";
                attr.Name = "����ʱ��";
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowEnder) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnder;
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;
                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Idx = -1;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowEnderRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnderRDT; // "FlowStartRDT";
                attr.Name = "����ʱ��";
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowEndNode) == false)
            {
                /* �����ڵ� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEndNode;
                attr.Name = "�����ڵ�";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowDaySpan) == false)
            {
                /* FlowDaySpan */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowDaySpan; // "FlowStartRDT";
                attr.Name = "���(��)";
                attr.MyDataType = DataType.AppMoney;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = true;
                attr.UIIsLine = false;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.PFlowNo) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PFlowNo;
                attr.Name = "�����̱��"; //  ���������̱��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 3;
                attr.Idx = -100;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.PNodeID) == false)
            {
                /* ������WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PNodeID;
                attr.Name = "�����������Ľڵ�";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.PWorkID) == false)
            {
                /* ������WorkID */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PWorkID;
                attr.Name = "������WorkID";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "0";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.PEmp) == false)
            {
                /* ���������̵���Ա */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.PEmp;
                attr.Name = "���������̵���Ա";
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 32;
                attr.Idx = -100;
                attr.Insert();
            }


            //if (attrs.Contains(mapData.No + "_" + GERptAttr.CWorkID) == false)
            //{
            //    /* ��������WorkID */
            //    MapAttr attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.KeyOfEn = GERptAttr.CWorkID;
            //    attr.Name = "��������WorkID";
            //    attr.MyDataType = DataType.AppInt;
            //    attr.DefVal = "0";
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = false;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.Idx = -101;
            //    attr.Insert();
            //}

            //if (attrs.Contains(mapData.No + "_" + GERptAttr.CFlowNo) == false)
            //{
            //    /* �������̱�� */
            //    MapAttr attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.KeyOfEn = GERptAttr.CFlowNo;
            //    attr.Name = "�������̱��";
            //    attr.MyDataType = DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = true;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 3;
            //    attr.Idx = -100;
            //    attr.Insert();
            //}

            if (attrs.Contains(mapData.No + "_" + GERptAttr.BillNo) == false)
            {
                /* ���ݱ�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.BillNo;
                attr.Name = "���ݱ��"; //  ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 100;
                attr.Idx = -100;
                attr.Insert();
            }


            if (attrs.Contains(mapData.No + "_MyNum") == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = "��";
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "1";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.Idx = -101;
                attr.Insert();
            }

            if (attrs.Contains(mapData.No + "_" + GERptAttr.AtPara) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.AtPara;
                attr.Name = "����"; // ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 4000;
                attr.Idx = -100;
                attr.Insert();
            }


            if (attrs.Contains(mapData.No + "_" + GERptAttr.GUID) == false)
            {
                /* ������ ���̱�� */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.GUID;
                attr.Name = "GUID"; // ���ݱ��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 32;
                attr.Idx = -100;
                attr.Insert();
            }

            //if (attrs.Contains(mapData.No + "_" + GERptAttr.PrjNo) == false)
            //{
            //    /* ��Ŀ��� */
            //    MapAttr attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.KeyOfEn = GERptAttr.PrjNo;
            //    attr.Name = "��Ŀ���"; //  ��Ŀ���
            //    attr.MyDataType = DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = false;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 100;
            //    attr.Idx = -100;
            //    attr.Insert();
            //}
            //if (attrs.Contains(mapData.No + "_" + GERptAttr.PrjName) == false)
            //{
            //    /* ��Ŀ���� */
            //    MapAttr attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = EditType.UnDel;
            //    attr.KeyOfEn = GERptAttr.PrjName;
            //    attr.Name = "��Ŀ����"; //  ��Ŀ����
            //    attr.MyDataType = DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = false;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 100;
            //    attr.Idx = -100;
            //    attr.Insert();
            //}

            if (attrs.Contains(mapData.No + "_" + GERptAttr.FlowNote) == false)
            {
                /* ������Ϣ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowNote;
                attr.Name = "������Ϣ"; //  ���������̱��
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 500;
                attr.Idx = -100;
                attr.Insert();
            }
            #endregion �����ϻ����ֶΡ�

            #endregion ������.
        }
        #endregion �������÷���1

        #region ִ�������¼�.
        /// <summary>
        /// ִ���˶��¼�
        /// </summary>
        /// <param name="doType">�¼�����</param>
        /// <param name="currNode">��ǰ�ڵ�</param>
        /// <param name="en">ʵ��</param>
        /// <param name="atPara">����</param>
        /// <param name="objs">���Ͷ��󣬿�ѡ</param>
        /// <returns>ִ�н��</returns>
        public string DoFlowEventEntity(string doType, Node currNode, Entity en, string atPara, SendReturnObjs objs)
        {
            if (currNode == null)
                return null;

            string str = null;
            if (this.FEventEntity != null)
            {
                this.FEventEntity.SendReturnObjs = objs;
                str = this.FEventEntity.DoIt(doType, currNode, en, atPara);
            }

            FrmEvents fes = currNode.MapData.FrmEvents;
            if (str == null)
                str = fes.DoEventNode(doType, en, atPara);


            //һ���Ǵ�������Ϣ��
            switch (doType)
            {
                case EventListOfNode.SendSuccess:
                case EventListOfNode.ShitAfter:
                case EventListOfNode.ReturnAfter:
                case EventListOfNode.UndoneAfter:
                case EventListOfNode.AskerReAfter:
                    break;
                default:
                    return str;
            }

            //�����Ϣʵ��.
            FrmEvent nev = fes.GetEntityByKey(FrmEventAttr.FK_Event, doType) as FrmEvent;
            if (nev == null)
            {
                nev = new FrmEvent();
                nev.FK_Event = doType;
            }

            //�����Ƿ��Ͷ��š�
            bool isSendEmail = false;
            bool isSendSMS = false;

            //�������.
            Row r = en.Row;
            try
            {
                //ϵͳ����.
                r.Add("FK_MapData", en.ClassID);
            }
            catch
            {
                r["FK_MapData"] = en.ClassID;
            }

            if (atPara != null)
            {
                AtPara ap = new AtPara(atPara);
                foreach (string s in ap.HisHT.Keys)
                {
                    try
                    {
                        r.Add(s, ap.GetValStrByKey(s));
                    }
                    catch
                    {
                        r[s] = ap.GetValStrByKey(s);
                    }
                }
            }

            //��ģʽ��������.
            switch (nev.MsgCtrl)
            {
                case MsgCtrl.BySet: /*�������ü���.*/
                    isSendEmail = nev.MsgMailEnable;
                    isSendSMS = nev.SMSEnable;
                    break;
                case MsgCtrl.BySDK: /*�������ü���.*/
                case MsgCtrl.ByFrmIsSendMsg: /*�������ü���.*/
                    if (r.ContainsKey("IsSendEmail") == true)
                        isSendEmail = r.GetBoolenByKey("IsSendEmail");
                    if (r.ContainsKey("IsSendSMS") == true)
                        isSendSMS = r.GetBoolenByKey("IsSendSMS");
                    break;
                default:
                    break;
            }

            // �ж��Ƿ�����Ϣ��
            if (isSendSMS == false && isSendEmail == false)
                return str;

            Int64 workid = Int64.Parse(en.PKVal.ToString());

            string title = "";
            try
            {
                title = en.GetValStrByKey("Title");
            }
            catch
            {
            }

            string hostUrl = Glo.HostURL;
            string sid = "{EmpStr}_" + workid + "_" + currNode.NodeID + "_" + DataType.CurrentDataTime;
            string openWorkURl = hostUrl + "WF/Do.aspx?DoType=OF&SID=" + sid;
            openWorkURl = openWorkURl.Replace("//", "/");
            openWorkURl = openWorkURl.Replace("//", "/");

            // ������Ϣ����.
            string mailTitleTmp = "";
            string mailDocTmp = "";
            if (isSendEmail)
            {
                // ����.
                mailTitleTmp = nev.MailTitle;
                mailTitleTmp = mailTitleTmp.Replace("{Title}", title);
                mailTitleTmp = mailTitleTmp.Replace("@WebUser.No", WebUser.No);
                mailTitleTmp = mailTitleTmp.Replace("@WebUser.Name", WebUser.Name);

                // ����.
                mailDocTmp = nev.MailDoc;
                mailDocTmp = mailDocTmp.Replace("{Url}", openWorkURl);
                mailDocTmp = mailDocTmp.Replace("{Title}", title);

                mailDocTmp = mailDocTmp.Replace("@WebUser.No", WebUser.No);
                mailDocTmp = mailDocTmp.Replace("@WebUser.Name", WebUser.Name);

                /*�����Ȼ��û���滻�����ı���.*/
                if (mailDocTmp.Contains("@"))
                    mailDocTmp = Glo.DealExp(mailDocTmp, en, null);
            }

            string smsDocTmp = "";
            if (isSendSMS)
            {
                smsDocTmp = nev.SMSDoc.Clone() as string;

                smsDocTmp = smsDocTmp.Replace("{Title}", title);
                smsDocTmp = smsDocTmp.Replace("{Url}", openWorkURl);
                smsDocTmp = smsDocTmp.Replace("@WebUser.No", WebUser.No);
                smsDocTmp = smsDocTmp.Replace("@WebUser.Name", WebUser.Name);

                /*�����Ȼ��û���滻�����ı���.*/
                if (smsDocTmp.Contains("@") == true)
                    smsDocTmp = Glo.DealExp(smsDocTmp, en, null);
            }

            //���Ҫ�����˵�ids.
            string toEmpIDs = "";
            switch (doType)
            {
                case EventListOfNode.SendSuccess:
                    toEmpIDs = objs.VarAcceptersID;
                    break;
                case EventListOfNode.ReturnAfter: // ������˻�,���ҵ��˻صĵ�����.
                    toEmpIDs = objs.VarAcceptersID;
                    break;
                default:
                    break;
            }

            // ִ�з�����Ϣ.
            string[] emps = toEmpIDs.Split(',');
            foreach (string emp in emps)
            {
                if (string.IsNullOrEmpty(emp))
                    continue;

                string mailDocReal = mailDocTmp.Clone() as string;
                mailDocReal = mailDocReal.Replace("{EmpStr}", emp);

                //������Ϣ.
                BP.WF.Dev2Interface.Port_SendMsg(emp, mailTitleTmp, mailDocReal, smsDocTmp, en.PKVal.ToString(),
                    doType, this.No, currNode.NodeID, Int64.Parse(en.PKVal.ToString()), 0, isSendEmail, isSendSMS);
            }
            return str;
        }
        /// <summary>
        /// ִ���˶��¼�
        /// </summary>
        /// <param name="doType">�¼�����</param>
        /// <param name="en">ʵ�����</param>
        public string DoFlowEventEntity(string doType, Node currNode, Entity en, string atPara)
        {
            return BP.DA.DataType.PraseGB2312_To_utf8(this.DoFlowEventEntity(doType, currNode, en, atPara, null));
        }
        private BP.WF.FlowEventBase _FDEventEntity = null;
        /// <summary>
        /// �ڵ�ʵ���࣬û�оͷ���Ϊ��.
        /// </summary>
        private BP.WF.FlowEventBase FEventEntity
        {
            get
            {
                if (_FDEventEntity == null && this.FlowMark != "" && this.FlowEventEntity != "")
                    _FDEventEntity = BP.WF.Glo.GetFlowEventEntityByEnName(this.FlowEventEntity);
                return _FDEventEntity;
            }
        }
        #endregion ִ�������¼�.

        #region ��������
        /// <summary>
        /// �Ƿ���MD5��������
        /// </summary>
        public bool IsMD5
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsMD5);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsMD5, value);
            }
        }
        /// <summary>
        /// �Ƿ��е���
        /// </summary>
        public int NumOfBill
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.NumOfBill);
            }
            set
            {
                this.SetValByKey(FlowAttr.NumOfBill, value);
            }
        }
        /// <summary>
        /// �������ɹ���
        /// </summary>
        public string TitleRole
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.TitleRole);
            }
            set
            {
                this.SetValByKey(FlowAttr.TitleRole, value);
            }
        }
        /// <summary>
        /// ��ϸ��
        /// </summary>
        public int NumOfDtl
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.NumOfDtl);
            }
            set
            {
                this.SetValByKey(FlowAttr.NumOfDtl, value);
            }
        }
        public decimal AvgDay
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.AvgDay);
            }
            set
            {
                this.SetValByKey(FlowAttr.AvgDay, value);
            }
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.No + "01");
                //return this.GetValIntByKey(FlowAttr.StartNodeID);
            }
        }
        /// <summary>
        /// add 2013-01-01.
        /// ҵ������(Ĭ��ΪNDxxRpt)
        /// </summary>
        public string PTable
        {
            get
            {
                string s = this.GetValStringByKey(FlowAttr.PTable);
                if (string.IsNullOrEmpty(s))
                    s = "ND" + int.Parse(this.No) + "Rpt";
                return s;
            }
            set
            {
                this.SetValByKey(FlowAttr.PTable, value);
            }
        }
        /// <summary>
        /// ��ʷ��¼��ʾ�ֶ�.
        /// </summary>
        public string HistoryFields
        {
            get
            {
                string strs = this.GetValStringByKey(FlowAttr.HistoryFields);
                if (string.IsNullOrEmpty(strs))
                    strs = "WFState,Title,FlowStartRDT,FlowEndNode";

                return strs;
            }
        }
        /// <summary>
        /// �Ƿ����ã�
        /// </summary>
        public bool IsGuestFlow
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsGuestFlow);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsGuestFlow, value);
            }
        }
        /// <summary>
        /// �Ƿ���Զ�������
        /// </summary>
        public bool IsCanStart
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsCanStart);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsCanStart, value);
            }
        }
        /// <summary>
        /// �Ƿ������������
        /// </summary>
        public bool IsBatchStart
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsBatchStart);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsBatchStart, value);
            }
        }
        /// <summary>
        /// �Ƿ��Զ�����δ���Ĵ�����
        /// </summary>
        public bool IsFullSA
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsFullSA);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsFullSA, value);
            }
        }
        /// <summary>
        /// ���������ֶ�
        /// </summary>
        public string BatchStartFields
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.BatchStartFields);
            }
            set
            {
                this.SetValByKey(FlowAttr.BatchStartFields, value);
            }
        }
        /// <summary>
        /// ���ݸ�ʽ
        /// </summary>
        public string BillNoFormat
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.BillNoFormat);
            }
            set
            {
                this.SetValByKey(FlowAttr.BillNoFormat, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FK_FlowSort);
            }
            set
            {
                this.SetValByKey(FlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Paras
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.Paras);
            }
            set
            {
                this.SetValByKey(FlowAttr.Paras, value);
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public string FK_FlowSortText
        {
            get
            {
                FlowSort fs = new FlowSort(this.FK_FlowSort);
                return fs.Name;
                //return this.GetValRefTextByKey(FlowAttr.FK_FlowSort);
            }
        }
        /// <summary>
        /// ����߱��
        /// </summary>
        public string DesignerNo1
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DesignerNo);
            }
            set
            {
                this.SetValByKey(FlowAttr.DesignerNo, value);
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public string DesignerName1
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.DesignerName);
            }
            set
            {
                this.SetValByKey(FlowAttr.DesignerName, value);
            }
        }
        /// <summary>
        /// �汾��
        /// </summary>
        public string Ver
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.Ver);
            }
            set
            {
                this.SetValByKey(FlowAttr.Ver, value);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������(�������)
        /// </summary>
        public int FlowType_del
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.FlowType);
            }
        }
        /// <summary>
        /// (��ǰ�ڵ�Ϊ������ʱ)�Ƿ���������������ɺ������Զ�����
        /// </summary>
        public bool IsAutoSendSubFlowOver
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsAutoSendSubFlowOver);
            }
        }
        public string Note
        {
            get
            {
                string s = this.GetValStringByKey("Note");
                if (s.Length == 0)
                {
                    return "��";
                }
                return s;
            }
        }
        public string NoteHtml
        {
            get
            {
                if (this.Note == "��" || this.Note == "")
                    return "���������Աû�б�д�����̵İ�����Ϣ����������-���򿪴�����-����ƻ����ϵ���Ҽ�-����������-����д���̰�����Ϣ��";
                else
                    return this.Note;
            }
        }
        /// <summary>
        /// �Ƿ���߳��Զ�����
        /// </summary>
        public bool IsMutiLineWorkFlow_del
        {
            get
            {
                return false;
                /*
                if (this.FlowType==2 || this.FlowType==1 )
                    return true;
                else
                    return false;
                    */
            }
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// Ӧ������
        /// </summary>
        public FlowAppType HisFlowAppType
        {
            get
            {
                return (FlowAppType)this.GetValIntByKey(FlowAttr.FlowAppType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowAppType, (int)value);
            }
        }
        /// <summary>
        /// ���ݴ洢ģʽ
        /// </summary>
        public DataStoreModel HisDataStoreModel
        {
            get
            {
                return (DataStoreModel)this.GetValIntByKey(FlowAttr.DataStoreModel);
            }
            set
            {
                this.SetValByKey(FlowAttr.DataStoreModel, (int)value);
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Nodes _HisNodes = null;
        /// <summary>
        /// ���Ľڵ㼯��.
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    _HisNodes = new Nodes(this.No);
                return _HisNodes;
            }
            set
            {
                _HisNodes = value;
            }
        }
        /// <summary>
        /// ���� Start �ڵ�
        /// </summary>
        public Node HisStartNode
        {
            get
            {

                foreach (Node nd in this.HisNodes)
                {
                    if (nd.IsStartNode)
                        return nd;
                }
                throw new Exception("@û���ҵ����Ŀ�ʼ�ڵ�,��������[" + this.Name + "]�������.");
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public FlowSort HisFlowSort
        {
            get
            {
                return new FlowSort(this.FK_FlowSort);
            }
        }
        /// <summary>
        /// flow data ����
        /// </summary>
        public BP.WF.Data.GERpt HisGERpt
        {
            get
            {
                try
                {
                    BP.WF.Data.GERpt wk = new BP.WF.Data.GERpt("ND" + int.Parse(this.No) + "Rpt");
                    return wk;
                }
                catch
                {
                    this.DoCheck();
                    BP.WF.Data.GERpt wk1 = new BP.WF.Data.GERpt("ND" + int.Parse(this.No) + "Rpt");
                    return wk1;
                }
            }
        }
        #endregion

        #region ���췽��

        /// <summary>
        /// ����
        /// </summary>
        public Flow()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public Flow(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("���̱�Ų�����");
            }
            else
            {
                this.Retrieve();
            }
        }
        protected override bool beforeUpdateInsertAction()
        {
            try
            {
                if (string.IsNullOrEmpty(this.FlowMark) == false)
                    this.FlowEventEntity = BP.WF.Glo.GetFlowEventEntityByFlowMark(this.FlowMark).ToString();
                else
                    this.FlowEventEntity = "";
            }
            catch
            {
            }

            DBAccess.RunSQL("UPDATE WF_Node SET FlowName='" + this.Name + "' WHERE FK_Flow='" + this.No + "'");
            DBAccess.RunSQL("UPDATE Sys_MapData SET  Name='" + this.Name + "' WHERE No='" + this.PTable + "'");
            return base.beforeUpdateInsertAction();
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

                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����";
                map.CodeStruct = "3";
                map.AddTBStringPK(FlowAttr.No, null, "���", true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, "����", true, false, 0, 500, 10);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", "�������", new FlowSorts(), false);
                //map.AddTBString(FlowAttr.FK_FlowSort, null, "�������", true, false, 0, 10, 10);
                map.AddTBInt(FlowAttr.FlowRunWay, 0, "���з�ʽ", false, false);

                //  map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", "�������", new FlowSorts(), false);
                //map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "���з�ʽ", false,
                //    false, FlowAttr.FlowRunWay,
                //    "@0=�ֹ�����@1=ָ����Ա��ʱ����@2=���ݼ���ʱ����@3=����ʽ����");

                map.AddTBString(FlowAttr.RunObj, null, "��������", true, false, 0, 3000, 10);
                map.AddTBString(FlowAttr.Note, null, "��ע", true, false, 0, 100, 10);
                map.AddTBString(FlowAttr.RunSQL, null, "���̽���ִ�к�ִ�е�SQL", true, false, 0, 2000, 10);

                map.AddTBInt(FlowAttr.NumOfBill, 0, "�Ƿ��е���", false, false);
                map.AddTBInt(FlowAttr.NumOfDtl, 0, "NumOfDtl", false, false);
                map.AddTBInt(FlowAttr.FlowAppType, 0, "��������", false, false);
                map.AddTBInt(FlowAttr.ChartType, 1, "�ڵ�ͼ������", false, false);

                // map.AddBoolean(FlowAttr.IsOK, true, "�Ƿ�����", true, true);
                map.AddBoolean(FlowAttr.IsCanStart, true, "���Զ���������", true, true, true);
                map.AddTBDecimal(FlowAttr.AvgDay, 0, "ƽ����������", false, false);


                map.AddTBInt(FlowAttr.IsFullSA, 0, "�Ƿ��Զ�����δ���Ĵ����ˣ�(���ú�,ccflow�ͻ�Ϊ��֪���Ľڵ���䴦���˵�WF_SelectAccper)", false, false);
                map.AddTBInt(FlowAttr.IsMD5, 0, "IsMD5", false, false);
                map.AddTBInt(FlowAttr.Idx, 0, "��ʾ˳���(�ڷ����б���)", true, false);
                map.AddTBInt(FlowAttr.TimelineRole, 0, "ʱЧ�Թ���", true, false);
                map.AddTBString(FlowAttr.Paras, null, "����", false, false, 0, 400, 10);

                // add 2013-01-01. 
                map.AddTBString(FlowAttr.PTable, null, "�������ݴ洢����", true, false, 0, 30, 10);


                // �ݸ���� "@0=��(����ݸ�)@1=���浽����@2=���浽�ݸ���"
                map.AddTBInt(FlowAttr.Draft, 0, "�ݸ����", true, false);

                // add 2013-01-01.
                map.AddTBInt(FlowAttr.DataStoreModel, 0, "���ݴ洢ģʽ", true, false);


                // add 2013-02-05.
                map.AddTBString(FlowAttr.TitleRole, null, "�������ɹ���", true, false, 0, 150, 10, true);

                // add 2013-02-14 
                map.AddTBString(FlowAttr.FlowMark, null, "���̱��", true, false, 0, 150, 10);
                map.AddTBString(FlowAttr.FlowEventEntity, null, "FlowEventEntity", true, false, 0, 100, 10, true);
                map.AddTBString(FlowAttr.HistoryFields, null, "��ʷ�鿴�ֶ�", true, false, 0, 500, 10, true);
                map.AddTBInt(FlowAttr.IsGuestFlow, 0, "�Ƿ��ǿͻ��������̣�", true, false);
                map.AddTBString(FlowAttr.BillNoFormat, null, "���ݱ�Ÿ�ʽ", true, false, 0, 50, 10, true);
                map.AddTBString(FlowAttr.FlowNoteExp, null, "��ע���ʽ", true, false, 0, 500, 10, true);

                //����Ȩ�޿�������,�������ڱ����п��Ƶ�.
                map.AddTBInt(FlowAttr.DRCtrlType, 0, "���Ų�ѯȨ�޿��Ʒ�ʽ", true, false);

                #region ������������
                map.AddTBInt(FlowAttr.StartLimitRole, 0, "�������ƹ���", true, false);
                map.AddTBString(FlowAttr.StartLimitPara, null, "��������", true, false, 0, 500, 10, true);
                map.AddTBString(FlowAttr.StartLimitAlert, null, "������ʾ", true, false, 0, 500, 10, false);
                map.AddTBInt(FlowAttr.StartLimitWhen, 0, "��ʾʱ��", true, false);
                #endregion ������������

                #region ������ʽ��
                map.AddTBInt(FlowAttr.StartGuideWay, 0, "ǰ�õ�����ʽ", false, false);

                map.AddTBString(FlowAttr.StartGuidePara1, null, "����1", true, false, 0, 500, 10, true);
                map.AddTBString(FlowAttr.StartGuidePara2, null, "����2", true, false, 0, 500, 10, true);
                map.AddTBString(FlowAttr.StartGuidePara3, null, "����3", true, false, 0, 500, 10, true);
                map.AddTBInt(FlowAttr.IsResetData, 0, "�Ƿ������������ð�ť��", true, false);
                //    map.AddTBInt(FlowAttr.IsImpHistory, 0, "�Ƿ����õ�����ʷ���ݰ�ť��", true, false);
                map.AddTBInt(FlowAttr.IsLoadPriData, 0, "�Ƿ�����һ�����ݣ�", true, false);
                #endregion ������ʽ��

                map.AddTBInt(FlowAttr.CFlowWay, 0, "�������̷�ʽ", true, false);
                map.AddTBString(FlowAttr.CFlowPara, null, "�������̲���", true, false, 0, 100, 10, true);

                //�������� add 2013-12-27. 
                map.AddTBInt(FlowAttr.IsBatchStart, 0, "�Ƿ������������", true, false);
                map.AddTBString(FlowAttr.BatchStartFields, null, "���������ֶ�(�ö��ŷֿ�)", true, false, 0, 500, 10, true);

                // map.AddTBInt(FlowAttr.IsEnableTaskPool, 0, "�Ƿ����ù��������", true, false);
                //map.AddDDLSysEnum(FlowAttr.TimelineRole, (int)TimelineRole.ByNodeSet, "ʱЧ�Թ���",
                // true, true, FlowAttr.TimelineRole, "@0=���ڵ�(�ɽڵ�����������)@1=��������(��ʼ�ڵ�SysSDTOfFlow�ֶμ���)");

                map.AddTBInt(FlowAttr.IsAutoSendSubFlowOver, 0, "(��ǰ�ڵ�Ϊ������ʱ)�Ƿ���������������ɺ������Զ�����", true, true);
                map.AddTBString(FlowAttr.Ver, null, "�汾��", true, true, 0, 20, 10);

                //����.
                map.AddTBAtParas(1000);


                #region ����ͬ������
                //����ͬ����ʽ.
                map.AddTBInt(FlowAttr.DTSWay, (int)FlowDTSWay.None, "ͬ����ʽ", true, true);
                map.AddTBString(FlowAttr.DTSBTable, null, "ҵ�����", true, false, 0, 200, 100, false);
                map.AddTBString(FlowAttr.DTSBTablePK, null, "ҵ�������", false, false, 0, 32, 10);

                map.AddTBInt(FlowAttr.DTSTime, (int)FlowDTSTime.AllNodeSend, "ִ��ͬ��ʱ���", true, true);
                map.AddTBString(FlowAttr.DTSSpecNodes, null, "ָ���Ľڵ�ID", true, false, 0, 200, 100, false);
                map.AddTBInt(FlowAttr.DTSField, (int)DTSField.SameNames, "Ҫͬ�����ֶμ��㷽ʽ", true, true);
                map.AddTBString(FlowAttr.DTSFields, null, "Ҫͬ�����ֶ�s,�м��ö��ŷֿ�.", false, false, 0, 2000, 100, false);
                #endregion ����ͬ������

                // map.AddSearchAttr(FlowAttr.FK_FlowSort);
                // map.AddSearchAttr(FlowAttr.FlowRunWay);

                RefMethod rm = new RefMethod();
                rm.Title = "��Ƽ�鱨��"; // "��Ƽ�鱨��";
                rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/WF/Img/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                //   rm = new RefMethod();
                //rm.Title = this.ToE("ViewDef", "��ͼ����"); //"��ͼ����";
                //rm.Icon = "/WF/Img/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoDRpt";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "��������"; // "��������";
                rm.Icon = "/WF/Img/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/WF/Img/Btn/Table.gif";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowDataOut", "����ת������");  //"����ת������";
                ////  rm.Icon = "/WF/Img/Btn/Table.gif";
                //rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "ɾ������";
                rm.Warning = "��ȷ��Ҫִ��ɾ������������";
                rm.ToolTip = "�����ʷ�������ݡ�";
                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);

                //map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow,
                //    FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "���͸�λ");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  ��������
        /// <summary>
        /// �������ת��
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            this.DoCheck();
            PubClass.WinOpen(Glo.CCFlowAppPath + "WF/Admin/Exp.aspx?CondType=0&FK_Flow=" + this.No, "����", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// ���屨��
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            this.DoCheck();
            PubClass.WinOpen(Glo.CCFlowAppPath + "WF/Admin/WFRpt.aspx?CondType=0&FK_Flow=" + this.No, "����", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// ���б���
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            return null;
        }
        public string DoDelData()
        {
            #region ɾ�����̱�������.
            string mysql = "SELECT OID FROM " + this.PTable;
            FrmNodes fns = new FrmNodes();
            fns.Retrieve(FrmNodeAttr.FK_Flow, this.No);
            string strs = "";
            foreach (FrmNode nd in fns)
            {
                if (strs.Contains("@" + nd.FK_Frm) == true)
                    continue;

                strs += "@" + nd.FK_Frm + "@";
                try
                {
                    MapData md = new MapData(nd.FK_Frm);
                    DBAccess.RunSQL("DELETE FROM " + md.PTable + " WHERE OID in (" + mysql + ")");
                }
                catch
                {
                }
            }
            #endregion ɾ�����̱�������.



            string sql = "  where FK_Node in (SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "')";
            string sql1 = " where NodeID in (SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "')";

            // DA.DBAccess.RunSQL("DELETE FROM WF_CHOfFlow WHERE FK_Flow='" + this.No + "'");

            DA.DBAccess.RunSQL("DELETE FROM WF_Bill WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'");

            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'");

            string sqlIn = " WHERE ReturnNode IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork " + sqlIn);
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper " + sql);
            DA.DBAccess.RunSQL("DELETE FROM WF_TransferCustom " + sql);
            // DA.DBAccess.RunSQL("DELETE FROM WF_FileManager " + sql);
            DA.DBAccess.RunSQL("DELETE FROM WF_RememberMe " + sql);

            try
            {
                DA.DBAccess.RunSQL("DELETE FROM ND" + int.Parse(this.No) + "Track ");
            }
            catch
            {
            }

            if (DBAccess.IsExitsObject(this.PTable))
                DBAccess.RunSQL("DELETE FROM " + this.PTable);

            //DA.DBAccess.RunSQL("DELETE FROM WF_WorkList WHERE FK_Flow='" + this.No + "'");
            //DA.DBAccess.RunSQL("DELETE FROM Sys_MapExt WHERE FK_MapData LIKE 'ND"+int.Parse(this.No)+"%'" );

            //ɾ���ڵ����ݡ�
            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                try
                {
                    Work wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE FROM " + wk.EnMap.PhysicsTable);
                }
                catch
                {
                }

                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                foreach (MapDtl dtl in dtls)
                {
                    try
                    {
                        DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                    }
                    catch
                    {
                    }
                }
            }
            MapDtls mydtls = new MapDtls("ND" + int.Parse(this.No) + "Rpt");
            foreach (MapDtl dtl in mydtls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                }
                catch
                {
                }
            }
            return "ɾ���ɹ�...";
        }

        /// <summary>
        /// װ������ģ��
        /// </summary>
        /// <param name="fk_flowSort">�������</param>
        /// <param name="path">��������</param>
        /// <returns></returns>
        public static Flow DoLoadFlowTemplate(string fk_flowSort, string path, ImpFlowTempleteModel model, int SpecialFlowNo = -1)
        {
            FileInfo info = new FileInfo(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            if (ds.Tables.Contains("WF_Flow") == false)
                throw new Exception("������󣬷�����ģ���ļ���");

            DataTable dtFlow = ds.Tables["WF_Flow"];
            Flow fl = new Flow();
            string oldFlowNo = dtFlow.Rows[0]["No"].ToString();
            string oldFlowName = dtFlow.Rows[0]["Name"].ToString();

            int oldFlowID = int.Parse(oldFlowNo);
            string timeKey = DateTime.Now.ToString("yyMMddhhmmss");

            //�ж����̱�ʾ.��������Ƿ���Թ���һ���࣬ע��
            //if (dtFlow.Columns.Contains("FlowMark") == true)
            //{
            //    string FlowMark = dtFlow.Rows[0]["FlowMark"].ToString();
            //    if (string.IsNullOrEmpty(FlowMark) == false)
            //    {
            //        if (fl.IsExit(FlowAttr.FlowMark, FlowMark))
            //            throw new Exception("@�����̱�ʾ:" + FlowMark + "�Ѿ�������ϵͳ��,�����ܵ���.");
            //    }
            //}

            switch (model)
            {
                case ImpFlowTempleteModel.AsNewFlow: /*��Ϊһ��������. */
                    fl.No = fl.GenerNewNo;
                    fl.DoDelData();
                    fl.DoDelete(); /*ɾ�����ܴ��ڵ�����.*/
                    break;
                case ImpFlowTempleteModel.AsTempleteFlowNo: /*������ģ���еı��*/
                    fl.No = oldFlowNo;
                    if (fl.IsExits)
                        throw new Exception("�������:����ģ��(" + oldFlowName + ")�еı��(" + oldFlowNo + ")��ϵͳ���Ѿ�����,��������Ϊ:" + dtFlow.Rows[0]["Name"].ToString());
                    else
                    {
                        fl.No = oldFlowNo;
                        fl.DoDelData();
                        fl.DoDelete(); /*ɾ�����ܴ��ڵ�����.*/
                    }
                    break;
                case ImpFlowTempleteModel.AsTempleteFlowNoOvrewaiteWhenExit: /*������ģ���еı�ţ�����в�������.*/
                    fl.No = oldFlowNo;
                    fl.DoDelData();
                    fl.DoDelete(); /*ɾ�����ܴ��ڵ�����.*/
                    break;
                case ImpFlowTempleteModel.AsSpecFlowNo:
                    if (SpecialFlowNo <= 0)
                    {
                        throw new Exception("@ָ����Ŵ���");
                    }
                    break;
                default:
                    throw new Exception("@û���ж�");
            }

            // string timeKey = fl.No;
            int idx = 0;
            string infoErr = "";
            string infoTable = "";
            int flowID = int.Parse(fl.No);

            #region �������̱�����
            foreach (DataColumn dc in dtFlow.Columns)
            {
                string val = dtFlow.Rows[0][dc.ColumnName] as string;
                switch (dc.ColumnName.ToLower())
                {
                    case "no":
                    case "fk_flowsort":
                        continue;
                    case "name":
                        val = "����:" + val + "_" + DateTime.Now.ToString("MM��dd��HHʱmm��");
                        break;
                    default:
                        break;
                }
                fl.SetValByKey(dc.ColumnName, val);
            }
            fl.FK_FlowSort = fk_flowSort;
            fl.Insert();
            #endregion �������̱�����

            #region ����OID �����ظ������� Sys_GroupField, Sys_MapAttr.
            DataTable mydtGF = ds.Tables["Sys_GroupField"];
            DataTable myDTAttr = ds.Tables["Sys_MapAttr"];
            DataTable myDTAth = ds.Tables["Sys_FrmAttachment"];
            DataTable myDTDtl = ds.Tables["Sys_MapDtl"];
            DataTable myDFrm = ds.Tables["Sys_MapFrame"];
            DataTable myDM2M = ds.Tables["Sys_MapM2M"];
            if (mydtGF != null)
            {
                //throw new Exception("@" + fl.No + fl.Name + ", ȱ�٣�Sys_GroupField");
                foreach (DataRow dr in mydtGF.Rows)
                {
                    Sys.GroupField gf = new Sys.GroupField();
                    foreach (DataColumn dc in mydtGF.Columns)
                    {
                        string val = dr[dc.ColumnName] as string;
                        gf.SetValByKey(dc.ColumnName, val);
                    }
                    int oldID = gf.OID;
                    gf.OID = DBAccess.GenerOID();
                    dr["OID"] = gf.OID;

                    // ���ԡ�
                    if (myDTAttr != null && myDTAttr.Columns.Contains("GroupID"))
                    {
                        foreach (DataRow dr1 in myDTAttr.Rows)
                        {
                            if (dr1["GroupID"] == null)
                                dr1["GroupID"] = 0;

                            if (dr1["GroupID"].ToString() == oldID.ToString())
                                dr1["GroupID"] = gf.OID;
                        }
                    }

                    if (myDTAth != null && myDTAth.Columns.Contains("GroupID"))
                    {
                        // ������
                        foreach (DataRow dr1 in myDTAth.Rows)
                        {
                            if (dr1["GroupID"] == null)
                                dr1["GroupID"] = 0;

                            if (dr1["GroupID"].ToString() == oldID.ToString())
                                dr1["GroupID"] = gf.OID;
                        }
                    }

                    if (myDTDtl != null && myDTDtl.Columns.Contains("GroupID"))
                    {
                        // �ӱ�
                        foreach (DataRow dr1 in myDTDtl.Rows)
                        {
                            if (dr1["GroupID"] == null)
                                dr1["GroupID"] = 0;

                            if (dr1["GroupID"].ToString() == oldID.ToString())
                                dr1["GroupID"] = gf.OID;
                        }
                    }

                    if (myDFrm != null && myDFrm.Columns.Contains("GroupID"))
                    {
                        // frm.
                        foreach (DataRow dr1 in myDFrm.Rows)
                        {
                            if (dr1["GroupID"] == null)
                                dr1["GroupID"] = 0;

                            if (dr1["GroupID"].ToString() == oldID.ToString())
                                dr1["GroupID"] = gf.OID;
                        }
                    }

                    if (myDM2M != null && myDM2M.Columns.Contains("GroupID"))
                    {
                        // m2m.
                        foreach (DataRow dr1 in myDM2M.Rows)
                        {
                            if (dr1["GroupID"] == null)
                                dr1["GroupID"] = 0;

                            if (dr1["GroupID"].ToString() == oldID.ToString())
                                dr1["GroupID"] = gf.OID;
                        }
                    }
                }
            }
            #endregion ����OID �����ظ������⡣ Sys_GroupField �� Sys_MapAttr.

            int timeKeyIdx = 0;
            foreach (DataTable dt in ds.Tables)
            {
                timeKeyIdx++;
                timeKey = timeKey + timeKeyIdx.ToString();

                infoTable = "@����:" + dt.TableName + " �����쳣��";
                switch (dt.TableName)
                {
                    case "WF_Flow": //ģ���ļ���
                        continue;
                    case "WF_FlowFormTree": //���̱�Ŀ¼ add 2013-12-03
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    FlowForm cd = new FlowForm();
                        //    foreach (DataColumn dc in dt.Columns)
                        //    {
                        //        string val = dr[dc.ColumnName] as string;
                        //        if (val == null)
                        //            continue;
                        //        switch (dc.ColumnName.ToLower())
                        //        {
                        //            case "fk_flow":
                        //                val = fl.No;
                        //                break;
                        //            default:
                        //                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                        //                break;
                        //        }
                        //        cd.SetValByKey(dc.ColumnName, val);
                        //    }
                        //    cd.Insert();
                        //}
                        break;
                    case "WF_FlowForm": //���̱��� add 2013-12-03
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    FlowForm cd = new FlowForm();
                        //    foreach (DataColumn dc in dt.Columns)
                        //    {
                        //        string val = dr[dc.ColumnName] as string;
                        //        if (val == null)
                        //            continue;
                        //        switch (dc.ColumnName.ToLower())
                        //        {
                        //            case "fk_flow":
                        //                val = fl.No;
                        //                break;
                        //            default:
                        //                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                        //                break;
                        //        }
                        //        cd.SetValByKey(dc.ColumnName, val);
                        //    }
                        //    cd.Insert();
                        //}
                        break;
                    case "WF_NodeForm": //�ڵ��Ȩ�ޡ� 2013-12-03
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeToolbar cd = new NodeToolbar();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "tonodeid":
                                    case "fk_node":
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }
                            cd.Insert();
                        }
                        break;
                    case "Sys_FrmSln": //���ֶ�Ȩ�ޡ� 2013-12-03
                        foreach (DataRow dr in dt.Rows)
                        {
                            FrmField cd = new FrmField();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "tonodeid":
                                    case "fk_node":
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }
                            cd.Insert();
                        }
                        break;
                    case "WF_NodeToolbar": //��������
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeToolbar cd = new NodeToolbar();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "tonodeid":
                                    case "fk_node":
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }
                            cd.OID = DA.DBAccess.GenerOID();
                            cd.DirectInsert();
                        }
                        break;
                    case "WF_BillTemplate":
                        continue; /*��Ϊʡ���� ��ӡģ��Ĵ���*/
                        foreach (DataRow dr in dt.Rows)
                        {
                            BillTemplate bt = new BillTemplate();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_flow":
                                        val = flowID.ToString();
                                        break;
                                    case "nodeid":
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                bt.SetValByKey(dc.ColumnName, val);
                            }
                            int i = 0;
                            string no = bt.No;
                            while (bt.IsExits)
                            {
                                bt.No = no + i.ToString();
                                i++;
                            }

                            try
                            {
                                File.Copy(info.DirectoryName + "\\" + no + ".rtf", BP.Sys.SystemConfig.PathOfWebApp + @"\DataUser\CyclostyleFile\" + bt.No + ".rtf", true);
                            }
                            catch (Exception ex)
                            {
                                // infoErr += "@�ָ�����ģ��ʱ���ִ���" + ex.Message + ",�п��������ڸ�������ģ��ʱû�и���ͬĿ¼�µĵ���ģ���ļ���";
                            }
                            bt.Insert();
                        }
                        break;
                    case "WF_FrmNode": //Conds.xml��
                        DBAccess.RunSQL("DELETE FROM WF_FrmNode WHERE FK_Flow='" + fl.No + "'");
                        foreach (DataRow dr in dt.Rows)
                        {
                            FrmNode fn = new FrmNode();
                            fn.FK_Flow = fl.No;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        break;
                                }
                                fn.SetValByKey(dc.ColumnName, val);
                            }
                            // ��ʼ���롣
                            fn.MyPK = fn.FK_Frm + "_" + fn.FK_Node;
                            fn.Insert();
                        }
                        break;
                    case "WF_FindWorkerRole": //���˹���
                        foreach (DataRow dr in dt.Rows)
                        {
                            FindWorkerRole en = new FindWorkerRole();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                }
                                en.SetValByKey(dc.ColumnName, val);
                            }

                            //����.
                            en.DirectInsert();
                        }
                        break;
                    case "WF_Cond": //Conds.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Cond cd = new Cond();
                            cd.FK_Flow = fl.No;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "tonodeid":
                                    case "fk_node":
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                        val = fl.No;
                                        break;
                                    default:
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }

                            cd.FK_Flow = fl.No;

                            //  return this.FK_MainNode + "_" + this.ToNodeID + "_" + this.HisCondType.ToString() + "_" + ConnDataFrom.Stas.ToString();
                            // ����ʼ���롣 
                            if (cd.MyPK.Contains("Stas"))
                            {
                                cd.MyPK = cd.FK_Node + "_" + cd.ToNodeID + "_" + cd.HisCondType.ToString() + "_" + ConnDataFrom.Stas.ToString();
                            }
                            else if (cd.MyPK.Contains("Dept"))
                            {
                                cd.MyPK = cd.FK_Node + "_" + cd.ToNodeID + "_" + cd.HisCondType.ToString() + "_" + ConnDataFrom.Depts.ToString();
                            }
                            else if (cd.MyPK.Contains("Paras"))
                            {
                                cd.MyPK = cd.FK_Node + "_" + cd.ToNodeID + "_" + cd.HisCondType.ToString() + "_" + ConnDataFrom.Paras.ToString();
                            }
                            else if (cd.MyPK.Contains("Url"))
                            {
                                cd.MyPK = cd.FK_Node + "_" + cd.ToNodeID + "_" + cd.HisCondType.ToString() + "_" + ConnDataFrom.Url.ToString();
                            }
                            else
                            {
                                cd.MyPK = DA.DBAccess.GenerOID().ToString() + DateTime.Now.ToString("yyMMddHHmmss");
                            }
                            cd.DirectInsert();
                        }
                        break;
                    case "WF_CCDept"://���͵����š�
                        foreach (DataRow dr in dt.Rows)
                        {
                            CCDept cd = new CCDept();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }

                            //��ʼ���롣
                            try
                            {
                                cd.Insert();
                            }
                            catch
                            {
                                cd.Update();
                            }
                        }
                        break;
                    case "WF_NodeReturn"://���˻صĽڵ㡣
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeReturn cd = new NodeReturn();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                    case "returnn":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                cd.SetValByKey(dc.ColumnName, val);
                            }

                            //��ʼ���롣
                            try
                            {
                                cd.Insert();
                            }
                            catch
                            {
                                cd.Update();
                            }
                        }
                        break;
                    case "WF_Direction": //����
                        foreach (DataRow dr in dt.Rows)
                        {
                            Direction dir = new Direction();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "node":
                                    case "tonode":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                dir.SetValByKey(dc.ColumnName, val);
                            }
                            dir.FK_Flow = fl.No;
                            dir.Insert();
                        }
                        break;
                    case "WF_TurnTo": //ת�����.
                        foreach (DataRow dr in dt.Rows)
                        {
                            TurnTo fs = new TurnTo();

                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                fs.SetValByKey(dc.ColumnName, val);
                            }
                            fs.FK_Flow = fl.No;
                            fs.Save();
                        }
                        break;
                    case "WF_FAppSet": //FAppSets.xml��
                        continue;
                    case "WF_LabNote": //LabNotes.xml��
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            LabNote ln = new LabNote();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                ln.SetValByKey(dc.ColumnName, val);
                            }
                            idx++;
                            ln.FK_Flow = fl.No;
                            ln.MyPK = ln.FK_Flow + "_" + ln.X + "_" + ln.Y + "_" + idx;
                            ln.DirectInsert();
                        }
                        break;
                    case "WF_NodeDept": //FAppSets.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeDept dir = new NodeDept();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                dir.SetValByKey(dc.ColumnName, val);
                            }
                            dir.Insert();
                        }
                        break;
                    case "WF_Node": //����ڵ���Ϣ.
                        foreach (DataRow dr in dt.Rows)
                        {
                            BP.WF.Template.NodeSheet nd = new BP.WF.Template.NodeSheet();

                            BP.WF.Template.CC cc = new CC(); // ������ص���Ϣ.
                            BP.Sys.FrmWorkCheck fwc = new FrmWorkCheck();

                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                //  NodeAttr.NodeFrmID
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "nodefrmid":
                                        if (val.Length == 5)
                                            val = "ND" + flowID + val.Substring(3);
                                        else if (val.Length == 6)
                                            val = "ND" + flowID + val.Substring(4);
                                        else if (val.Length == 7)
                                            val = "ND" + flowID + val.Substring(5);
                                        break;
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                    case "fk_flowsort":
                                        continue;
                                    case "showsheets":
                                    case "histonds":
                                    case "groupstands":
                                        string key = "@" + flowID;
                                        val = val.Replace(key, "");
                                        break;
                                    default:
                                        break;
                                }
                                nd.SetValByKey(dc.ColumnName, val);
                                cc.SetValByKey(dc.ColumnName, val);
                                fwc.SetValByKey(dc.ColumnName, val);
                            }

                            nd.FK_Flow = fl.No;
                            nd.FlowName = fl.Name;
                            try
                            {
                                if (nd.GetValStringByKey("OfficePrintEnable") == "��ӡ")
                                    nd.SetValByKey("OfficePrintEnable", 0);

                                nd.DirectInsert();

                                //�ѳ��͵���ϢҲ��������ȥ.
                                cc.DirectUpdate();
                                fwc.DirectUpdate();
                                DBAccess.RunSQL("DELETE FROM Sys_MapAttr WHERE FK_MapData='ND" + nd.NodeID + "'");
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@����ڵ�:FlowName:" + nd.FlowName + " nodeID: " + nd.NodeID + " , " + nd.Name + " ����:" + ex.Message);
                            }

                            //ɾ��mapdata.
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            Node nd = new Node();
                            nd.NodeID = int.Parse(dr[NodeAttr.NodeID].ToString());
                            nd.RetrieveFromDBSources();
                            nd.FK_Flow = fl.No;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "nodefrmid":
                                        if (val.Length == 5)
                                            val = "ND" + flowID + val.Substring(3);
                                        else if (val.Length == 6)
                                            val = "ND" + flowID + val.Substring(4);
                                        else if (val.Length == 7)
                                            val = "ND" + flowID + val.Substring(5);
                                        break;
                                    case "nodeid":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "fk_flow":
                                    case "fk_flowsort":
                                        continue;
                                    case "showsheets":
                                    case "histonds":
                                    case "groupstands":
                                        string key = "@" + flowID;
                                        val = val.Replace(key, "");
                                        break;
                                    default:
                                        break;
                                }
                                nd.SetValByKey(dc.ColumnName, val);
                            }
                            nd.FK_Flow = fl.No;
                            nd.FlowName = fl.Name;
                            nd.DirectUpdate();
                        }
                        break;
                    case "WF_NodeStation": //FAppSets.xml��
                        DBAccess.RunSQL("DELETE FROM WF_NodeStation WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + fl.No + "')");
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeStation ns = new NodeStation();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                ns.SetValByKey(dc.ColumnName, val);
                            }
                            ns.Insert();
                        }
                        break;
                    case "WF_Listen": // ��Ϣ������
                        foreach (DataRow dr in dt.Rows)
                        {
                            Listen li = new Listen();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "oid":
                                        continue;
                                        break;
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    case "nodes":
                                        string[] nds = val.Split('@');
                                        string valExt = "";
                                        foreach (string nd in nds)
                                        {
                                            if (nd == "" || nd == null)
                                                continue;
                                            string ndExt = nd.Clone() as string;
                                            if (ndExt.Length == 3)
                                                ndExt = flowID + ndExt.Substring(1);
                                            else if (val.Length == 4)
                                                ndExt = flowID + ndExt.Substring(2);
                                            else if (val.Length == 5)
                                                ndExt = flowID + ndExt.Substring(3);
                                            ndExt = "@" + ndExt;
                                            valExt += ndExt;
                                        }
                                        val = valExt;
                                        break;
                                    default:
                                        break;
                                }
                                li.SetValByKey(dc.ColumnName, val);
                            }
                            li.Insert();
                        }
                        break;
                    case "Sys_Enum": //RptEmps.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.SysEnum se = new Sys.SysEnum();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        break;
                                    default:
                                        break;
                                }
                                se.SetValByKey(dc.ColumnName, val);
                            }
                            se.MyPK = se.EnumKey + "_" + se.Lang + "_" + se.IntKey;
                            if (se.IsExits)
                                continue;
                            se.Insert();
                        }
                        break;
                    case "Sys_EnumMain": //RptEmps.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.SysEnumMain sem = new Sys.SysEnumMain();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                sem.SetValByKey(dc.ColumnName, val);
                            }
                            if (sem.IsExits)
                                continue;
                            sem.Insert();
                        }
                        break;
                    case "Sys_MapAttr": //RptEmps.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.MapAttr ma = new Sys.MapAttr();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_mapdata":
                                    case "keyofen":
                                    case "autofulldoc":
                                        if (val == null)
                                            continue;

                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                    default:
                                        break;
                                }
                                ma.SetValByKey(dc.ColumnName, val);
                            }
                            bool b = ma.IsExit(Sys.MapAttrAttr.FK_MapData, ma.FK_MapData,
                                Sys.MapAttrAttr.KeyOfEn, ma.KeyOfEn);

                            ma.MyPK = ma.FK_MapData + "_" + ma.KeyOfEn;
                            if (b == true)
                                ma.DirectUpdate();
                            else
                                ma.DirectInsert();
                        }
                        break;
                    case "Sys_MapData": //RptEmps.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.MapData md = new Sys.MapData();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + int.Parse(fl.No));
                                md.SetValByKey(dc.ColumnName, val);
                            }
                            md.Save();
                        }
                        break;
                    case "Sys_MapDtl": //RptEmps.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.MapDtl md = new Sys.MapDtl();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                md.SetValByKey(dc.ColumnName, val);
                            }
                            md.Save();
                        }
                        break;
                    case "Sys_MapExt":
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.MapExt md = new Sys.MapExt();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                md.SetValByKey(dc.ColumnName, val);
                            }
                            md.Save();
                        }
                        break;
                    case "Sys_FrmLine":
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLine en = new FrmLine();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }

                            en.MyPK = Guid.NewGuid().ToString();
                            // BP.DA.DBAccess.GenerOIDByGUID(); "LIE" + timeKey + "_" + idx;
                            //if (en.IsExitGenerPK())
                            //    continue;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmEle":
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmEle en = new FrmEle();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmImg":
                        idx = 0;
                        timeKey = DateTime.Now.ToString("yyyyMMddHHmmss");
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmImg en = new FrmImg();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }

                            try
                            {
                                en.MyPK = "I" + timeKey + "_" + idx;
                                en.Insert();
                            }
                            catch
                            {
                                en.MyPK = Guid.NewGuid().ToString();
                                en.Insert();
                            }
                        }
                        break;
                    case "Sys_FrmLab":
                        idx = 0;
                        timeKey = DateTime.Now.ToString("yyyyMMddHHmmss");
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLab en = new FrmLab();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }

                            //en.MyPK = Guid.NewGuid().ToString();
                            // �����ظ���
                            try
                            {
                                en.MyPK = "Lab" + timeKey + "_" + idx;
                                en.Insert();
                            }
                            catch
                            {
                                en.MyPK = Guid.NewGuid().ToString();
                                en.Insert();
                            }
                        }
                        break;
                    case "Sys_FrmLink":
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLink en = new FrmLink();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                if (val == null)
                                    continue;

                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.MyPK = Guid.NewGuid().ToString();
                            //en.MyPK = "LK" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmAttachment":
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmAttachment en = new FrmAttachment();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.MyPK = en.FK_MapData + "_" + en.NoOfObj;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmEvent": //�¼�.
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmEvent en = new FrmEvent();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.Insert();
                        }
                        break;
                    case "Sys_MapM2M": //Sys_MapM2M.
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapM2M en = new MapM2M();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmRB": //Sys_FrmRB.
                        idx = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmRB en = new FrmRB();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.Insert();
                        }
                        break;
                    case "WF_NodeEmp": //FAppSets.xml��
                        foreach (DataRow dr in dt.Rows)
                        {
                            NodeEmp ne = new NodeEmp();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                ne.SetValByKey(dc.ColumnName, val);
                            }
                            ne.Insert();
                        }
                        break;
                    case "Sys_GroupField": // 
                        foreach (DataRow dr in dt.Rows)
                        {
                            Sys.GroupField gf = new Sys.GroupField();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                switch (dc.ColumnName.ToLower())
                                {
                                    case "enname":
                                    case "keyofen":
                                        val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                        break;
                                    default:
                                        break;
                                }
                                gf.SetValByKey(dc.ColumnName, val);
                            }
                            //  int oid = DBAccess.GenerOID();
                            //  DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID=" + gf.OID + " WHERE FK_MapData='" + gf.EnName + "' AND GroupID=" + gf.OID);
                            gf.InsertAsOID(gf.OID);
                        }
                        break;
                    case "WF_CCEmp": // ����.
                        foreach (DataRow dr in dt.Rows)
                        {
                            CCEmp ne = new CCEmp();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                ne.SetValByKey(dc.ColumnName, val);
                            }
                            ne.Insert();
                        }
                        break;
                    case "WF_CCStation": // ����.
                        foreach (DataRow dr in dt.Rows)
                        {
                            CCStation ne = new CCStation();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                switch (dc.ColumnName.ToLower())
                                {
                                    case "fk_node":
                                        if (val.Length == 3)
                                            val = flowID + val.Substring(1);
                                        else if (val.Length == 4)
                                            val = flowID + val.Substring(2);
                                        else if (val.Length == 5)
                                            val = flowID + val.Substring(3);
                                        break;
                                    default:
                                        break;
                                }
                                ne.SetValByKey(dc.ColumnName, val);
                            }
                            ne.Insert();
                        }
                        break;
                    default:
                        // infoErr += "Error:" + dt.TableName;
                        break;
                    //    throw new Exception("@unhandle named " + dt.TableName);
                }
            }

            #region �������������ԡ�
            DBAccess.RunSQL("UPDATE WF_Cond SET FK_Node=NodeID WHERE FK_Node=0");
            DBAccess.RunSQL("UPDATE WF_Cond SET ToNodeID=NodeID WHERE ToNodeID=0");

            DBAccess.RunSQL("DELETE FROM WF_Cond WHERE NodeID NOT IN (SELECT NodeID FROM WF_Node)");
            DBAccess.RunSQL("DELETE FROM WF_Cond WHERE ToNodeID NOT IN (SELECT NodeID FROM WF_Node) ");
            DBAccess.RunSQL("DELETE FROM WF_Cond WHERE FK_Node NOT IN (SELECT NodeID FROM WF_Node) AND FK_Node > 0");
            #endregion

            if (infoErr == "")
            {
                //// ɾ���հ���.
                //BP.DTS.DeleteBlankGroupField en = new BP.DTS.DeleteBlankGroupField();
                //en.Do();

                infoTable = "";
                //   fl.DoCheck();

                //д��Ȩ��.
                //  fl.WritToGPM(fl.FK_FlowSort);

                return fl; // "��ȫ�ɹ���";
            }

            infoErr = "@ִ���ڼ�������·������Ĵ���\t\r" + infoErr + "@ " + infoTable;
            throw new Exception(infoErr);
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        fl.DoDelete();
            //        throw new Exception("@" + infoErr + " @table=" + infoTable + "@" + ex.Message);
            //    }
            //    catch (Exception ex1)
            //    {
            //        throw new Exception("@ɾ���Ѿ������Ĵ�������������ڼ����:" + ex1.Message );
            //    }
            //}
        }
        public Node DoNewNode(int x, int y)
        {
            Node nd = new Node();
            int idx = this.HisNodes.Count;
            if (idx == 0)
                idx++;

            while (true)
            {
                string strID = this.No + idx.ToString().PadLeft(2, '0');
                nd.NodeID = int.Parse(strID);
                if (!nd.IsExits)
                    break;
                idx++;
            }

            nd.HisNodeWorkType = NodeWorkType.Work;
            nd.Name = "�ڵ�" + idx;
            nd.HisNodePosType = NodePosType.Mid;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.X = x;
            nd.Y = y;
            nd.Step = idx;
            nd.Insert();

            nd.CreateMap();
            return nd;
        }
        /// <summary>
        /// ִ���½�
        /// </summary>
        /// <param name="flowSort">���</param>
        /// <param name="flowName">��������</param>
        /// <param name="model">���ݴ洢ģʽ</param>
        /// <param name="pTable">���ݴ洢�����</param>
        /// <param name="FlowMark">���̱��</param>
        public void DoNewFlow(string flowSort, string flowName,
            DataStoreModel model, string pTable, string FlowMark)
        {
            try
            {
                //��������������.
                if (string.IsNullOrEmpty(pTable) == false && pTable.Length >= 1)
                {
                    string c = pTable.Substring(0, 1);
                    if (DataType.IsNumStr(c) == true)
                        throw new Exception("@�Ƿ����������ݱ�(" + pTable + "),���ᵼ��ccflow���ܴ����ñ�.");
                }

                this.Name = flowName;
                if (string.IsNullOrWhiteSpace(this.Name))
                    this.Name = "�½�����" + this.No; //�½�����.

                this.No = this.GenerNewNoByKey(FlowAttr.No);
                this.HisDataStoreModel = model;
                this.PTable = pTable;
                this.FK_FlowSort = flowSort;
                this.FlowMark = FlowMark;

                if (string.IsNullOrEmpty(FlowMark) == false)
                {
                    if (this.IsExit(FlowAttr.FlowMark, FlowMark))
                        throw new Exception("@�����̱�ʾ:" + FlowMark + "�Ѿ�������ϵͳ��.");
                }

                /*����ʼֵ*/
                //this.Paras = "@StartNodeX=10@StartNodeY=15@EndNodeX=40@EndNodeY=10";
                this.Paras = "@StartNodeX=200@StartNodeY=50@EndNodeX=200@EndNodeY=350";
                this.Save();

                #region ɾ���п��ܴ��ڵ���ʷ����.
                Flow fl = new Flow(this.No);
                fl.DoDelData();
                fl.DoDelete();

                this.Save();
                #endregion ɾ���п��ܴ��ڵ���ʷ����.

                Node nd = new Node();
                nd.NodeID = int.Parse(this.No + "01");
                nd.Name = "��ʼ�ڵ�";//  "��ʼ�ڵ�"; 
                nd.Step = 1;
                nd.FK_Flow = this.No;
                nd.FlowName = this.Name;
                nd.HisNodePosType = NodePosType.Start;
                nd.HisNodeWorkType = NodeWorkType.StartWork;
                nd.X = 200;
                nd.Y = 150;
                nd.ICON = "ǰ̨";
                nd.Insert();

                nd.CreateMap();
                nd.HisWork.CheckPhysicsTable();

                nd = new Node();
                nd.NodeID = int.Parse(this.No + "02");
                nd.Name = "�ڵ�2"; // "�����ڵ�";
                nd.Step = 2;
                nd.FK_Flow = this.No;
                nd.FlowName = this.Name;
                nd.HisNodePosType = NodePosType.Mid;
                nd.HisNodeWorkType = NodeWorkType.Work;
                nd.X = 200;
                nd.Y = 250;
                nd.ICON = "���";
                nd.Insert();
                nd.CreateMap();
                nd.HisWork.CheckPhysicsTable();

                BP.Sys.MapData md = new BP.Sys.MapData();
                md.No = "ND" + int.Parse(this.No) + "Rpt";
                md.Name = this.Name;
                md.Save();

                // װ��ģ��.
                string file = BP.Sys.SystemConfig.PathOfDataUser + "XML\\TempleteSheetOfStartNode.xml";
                if (System.IO.File.Exists(file))
                {
                    /*������ڿ�ʼ�ڵ��ģ��*/
                    DataSet ds = new DataSet();
                    ds.ReadXml(file);

                    string nodeID = "ND" + int.Parse(this.No + "01");
                    BP.Sys.MapData.ImpMapData(nodeID, ds, false);
                }
                else
                {
                    #region ����CCForm ��װ��.
                    FrmImg img = new FrmImg();
                    img.MyPK = "Img" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No;
                    img.FK_MapData = "ND" + int.Parse(this.No + "01");
                    img.X = (float)577.26;
                    img.Y = (float)3.45;

                    img.W = (float)137;
                    img.H = (float)40;

                    img.ImgURL = "/ccform;component/Img/LogoBig.png";
                    img.LinkURL = "http://ccflow.org";
                    img.LinkTarget = "_blank";
                    img.Insert();

                    FrmLab lab = new FrmLab();


                    lab = new FrmLab();
                    lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 2;
                    lab.Text = "������";
                    lab.FK_MapData = "ND" + int.Parse(this.No + "01");
                    lab.X = (float)106.48;
                    lab.Y = (float)96.08;
                    lab.FontSize = 11;
                    lab.FontColor = "black";
                    lab.FontName = "Portable User Interface";
                    lab.FontStyle = "Normal";
                    lab.FontWeight = "normal";
                    lab.Insert();

                    lab = new FrmLab();
                    lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 3;
                    lab.Text = "����ʱ��";
                    lab.FK_MapData = "ND" + int.Parse(this.No + "01");
                    lab.X = (float)307.64;
                    lab.Y = (float)95.17;

                    lab.FontSize = 11;
                    lab.FontColor = "black";
                    lab.FontName = "Portable User Interface";
                    lab.FontStyle = "Normal";
                    lab.FontWeight = "normal";
                    lab.Insert();

                    lab = new FrmLab();
                    lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 4;
                    lab.Text = "�½��ڵ�(���޸ı���)";
                    lab.FK_MapData = "ND" + int.Parse(this.No + "01");

                    lab.X = (float)294.67;
                    lab.Y = (float)8.27;

                    lab.FontSize = 23;
                    lab.FontColor = "Blue";
                    lab.FontName = "Portable User Interface";
                    lab.FontStyle = "Normal";
                    lab.FontWeight = "normal";
                    lab.Insert();

                    lab = new FrmLab();
                    lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 5;
                    lab.Text = "˵��:����������ccflow�Զ������ģ��������޸�/ɾ������@Ϊ�˸�����������������Ե�http://ccflow.org�������ر�ģ��.";
                    lab.Text += "@��Ϊ��ǰ����������silverlight��������ʹ���ر�˵������:@";
                    lab.Text += "@1,�ı�ؼ�λ��: ";
                    lab.Text += "@  ���еĿؼ���֧�� wasd, ��Ϊ����������ƶ��ؼ���λ�ã� ���ֿؼ�֧�ַ����. ";
                    lab.Text += "@2, ����textbox, �ӱ�, dropdownlistbox, �Ŀ�� shift+ -> ��������ӿ�� shift + <- ��С���.";
                    lab.Text += "@3, ���� windows�� + s.  ɾ�� delete.  ���� ctrl+c   ճ��: ctrl+v.";
                    lab.Text += "@4, ֧��ȫѡ�������ƶ��� �����Ŵ���С����., �����ı��ߵĿ��.";
                    lab.Text += "@5, �ı��ߵĳ��ȣ� ѡ���ߣ�����ɫ��Բ�㣬��������.";
                    lab.Text += "@6, �Ŵ������С��label ������ , ѡ��һ�����label , �� A+ ���ߡ�A������ť.";
                    lab.Text += "@7, �ı��߻��߱�ǩ����ɫ�� ѡ��������󣬵㹤�����ϵĵ�ɫ��.";

                    lab.X = (float)168.24;
                    lab.Y = (float)163.7;
                    lab.FK_MapData = "ND" + int.Parse(this.No + "01");
                    lab.FontSize = 11;
                    lab.FontColor = "Red";
                    lab.FontName = "Portable User Interface";
                    lab.FontStyle = "Normal";
                    lab.FontWeight = "normal";
                    lab.Insert();

                    string key = "L" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No;
                    FrmLine line = new FrmLine();
                    line.MyPK = key + "_1";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)281.82;
                    line.Y1 = (float)81.82;
                    line.X2 = (float)281.82;
                    line.Y2 = (float)121.82;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_2";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)360;
                    line.Y1 = (float)80.91;
                    line.X2 = (float)360;
                    line.Y2 = (float)120.91;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_3";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)158.82;
                    line.Y1 = (float)41.82;
                    line.X2 = (float)158.82;
                    line.Y2 = (float)482.73;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_4";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)81.55;
                    line.Y1 = (float)80;
                    line.X2 = (float)718.82;
                    line.Y2 = (float)80;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();


                    line.MyPK = key + "_5";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)81.82;
                    line.Y1 = (float)40;
                    line.X2 = (float)81.82;
                    line.Y2 = (float)480.91;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_6";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)81.82;
                    line.Y1 = (float)481.82;
                    line.X2 = (float)720;
                    line.Y2 = (float)481.82;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_7";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)83.36;
                    line.Y1 = (float)40.91;
                    line.X2 = (float)717.91;
                    line.Y2 = (float)40.91;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_8";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)83.36;
                    line.Y1 = (float)120.91;
                    line.X2 = (float)717.91;
                    line.Y2 = (float)120.91;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();

                    line.MyPK = key + "_9";
                    line.FK_MapData = "ND" + int.Parse(this.No + "01");
                    line.X1 = (float)719.09;
                    line.Y1 = (float)40;
                    line.X2 = (float)719.09;
                    line.Y2 = (float)482.73;
                    line.BorderWidth = (float)2;
                    line.BorderColor = "Black";
                    line.Insert();
                    #endregion
                }

                //д��Ȩ��.
                WritToGPM(flowSort);

                this.DoCheck_CheckRpt(this.HisNodes);
                Flow.RepareV_FlowData_View();
            }
            catch (Exception ex)
            {
                ///ɾ����������.
                this.DoDelete();

                //��ʾ����.
                throw new Exception("�������̴���:" + ex.Message);
            }


        }

        /// <summary>
        /// д��Ȩ��
        /// </summary>
        /// <param name="flowSort"></param>
        public void WritToGPM(string flowSort)
        {

            return;

            #region д��Ȩ�޹���
            if (Glo.OSModel == OSModel.BPM)
            {
                string sql = "";

                try
                {
                    sql = "DELETE FROM GPM_Menu WHERE FK_App='" + SystemConfig.SysNo + "' AND Flag='Flow" + this.No + "'";
                    BP.DA.DBAccess.RunSQL(sql);
                }
                catch
                {
                }

                // ��ʼ��֯�������̵�����.
                // ȡ�ø����̵�Ŀ¼���.
                sql = "SELECT No FROM GPM_Menu WHERE Flag='FlowSort" + flowSort + "' AND FK_App='" + BP.Sys.SystemConfig.SysNo + "'";
                string parentNoOfMenu = DBAccess.RunSQLReturnStringIsNull(sql, null);
                if (parentNoOfMenu == null)
                    throw new Exception("@û���ҵ������̵�(" + BP.Sys.SystemConfig.SysNo + ")Ŀ¼��GPMϵͳ��,�������½���Ŀ¼��");

                // ȡ�øù��ܵ��������.
                string treeNo = DBAccess.GenerOID("BP.GPM.Menu").ToString();

                // ������������.
                string url = "/WF/MyFlow.aspx?FK_Flow=" + this.No + "&FK_Node=" + int.Parse(this.No) + "01";

                sql = "INSERT INTO GPM_Menu(No,Name,ParentNo,IsDir,MenuType,FK_App,IsEnable,Flag,Url)";
                sql += " VALUES('{0}','{1}','{2}',{3},{4},'{5}',{6},'{7}','{8}')";
                sql = string.Format(sql, treeNo, this.Name, parentNoOfMenu, 0, 4, SystemConfig.SysNo, 1, "Flow" + this.No, url);
                DBAccess.RunSQL(sql);
            }
            #endregion
        }
        /// <summary>
        /// ��鱨��
        /// </summary>
        public void CheckRpt()
        {
            this.DoCheck_CheckRpt(this.HisNodes);
        }
        /// <summary>
        /// ����֮ǰ�����
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdate()
        {
            this.Ver = BP.DA.DataType.CurrentDataTimess;
            Node.CheckFlow(this);
            return base.beforeUpdate();
        }

        /// <summary>
        /// ���°汾��
        /// </summary>
        public static void UpdateVer(string flowNo)
        {
            string sql = "UPDATE WF_Flow SET VER='" + BP.DA.DataType.CurrentDataTimess + "' WHERE No='" + flowNo + "'";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public void DoDelete()
        {
            //ɾ����������.
            this.DoDelData();

            string sql = "";
            //sql = " DELETE FROM WF_chofflow WHERE FK_Flow='" + this.No + "'";
            sql += "@ DELETE  FROM WF_GenerWorkerlist WHERE FK_Flow='" + this.No + "'";
            sql += "@ DELETE FROM  WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'";
            sql += "@ DELETE FROM  WF_Cond WHERE FK_Flow='" + this.No + "'";

            // ɾ����λ�ڵ㡣
            sql += "@ DELETE  FROM  WF_NodeStation WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ������
            sql += "@ DELETE FROM  WF_Direction WHERE FK_Flow='" + this.No + "'";

            //ɾ���ڵ����Ϣ.
            sql += "@ DELETE FROM WF_FrmNode  WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            sql += "@ DELETE FROM WF_NodeEmp  WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_CCEmp WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_CCDept WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_CCStation WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            sql += "@ DELETE FROM WF_NodeFlow WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_NodeReturn WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            sql += "@ DELETE FROM WF_NodeDept WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_NodeStation WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_NodeEmp WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            sql += "@ DELETE FROM WF_NodeToolbar WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_SelectAccper WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            sql += "@ DELETE FROM WF_TurnTo WHERE   FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";


            //ɾ������.
            sql += "@ DELETE FROM WF_Listen WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ��d2d����.
            //  sql += "@GO DELETE WF_M2M WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            //// ɾ������.
            //sql += "@ DELETE FROM WF_FAppSet WHERE NodeID IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ������.
            sql += "@ DELETE FROM WF_FlowEmp WHERE FK_Flow='" + this.No + "' ";

            //// �ⲿ��������
            //sql += "@ DELETE FROM WF_FAppSet WHERE  NodeID in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            //ɾ������.
            sql += "@ DELETE FROM WF_BillTemplate WHERE  NodeID in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            //ɾ��Ȩ�޿���.
            sql += "@ DELETE FROM Sys_FrmSln WHERE FK_Flow='" + this.No + "'";

            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                // ɾ���ڵ�������صĶ���.
                sql += "@ DELETE  FROM Sys_MapM2M WHERE FK_MapData='ND" + nd.NodeID + "'";
                nd.Delete();
            }

            sql += "@ DELETE  FROM WF_Node WHERE FK_Flow='" + this.No + "'";
            sql += "@ DELETE  FROM  WF_LabNote WHERE FK_Flow='" + this.No + "'";

            //ɾ��������Ϣ
            sql += "@ DELETE FROM Sys_GroupField WHERE EnName NOT IN(SELECT NO FROM Sys_MapData)";

            #region ɾ�����̱���,ɾ���켣
            MapData md = new MapData();
            md.No = "ND" + int.Parse(this.No) + "Rpt";
            md.Delete();

            //ɾ����ͼ.
            try
            {
                BP.DA.DBAccess.RunSQL("DROP VIEW V_" + this.No);
            }
            catch
            {
            }

            //ɾ���켣.
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE ND" + int.Parse(this.No) + "Track ");
            }
            catch
            {
            }
            #endregion ɾ�����̱���,ɾ���켣.

            // ִ��¼�Ƶ�sql scripts.
            BP.DA.DBAccess.RunSQLs(sql);
            this.Delete(); //ɾ����Ҫ�Ƴ�����.

            Flow.RepareV_FlowData_View();

            //ɾ��Ȩ�޹���
            if (BP.WF.Glo.OSModel == OSModel.BPM)
            {
                try
                {
                    DBAccess.RunSQL("DELETE FROM GPM_Menu WHERE Flag='Flow" + this.No + "' AND FK_App='" + SystemConfig.SysNo + "'");
                }
                catch
                {
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// ���̼���
    /// </summary>
    public class Flows : EntitiesNoName
    {
        #region ��ѯ
        public static void GenerHtmlRpts()
        {
            Flows fls = new Flows();
            fls.RetrieveAll();

            foreach (Flow fl in fls)
            {
                fl.DoCheck();
                fl.GenerFlowXmlTemplete();
            }

            // ������������
            string path = SystemConfig.PathOfWorkDir + @"\VisualFlow\DataUser\FlowDesc\";
            string msg = "";
            msg += "<html>";
            msg += "\r\n<title>.net��������������ƣ�����ģ��</title>";

            msg += "\r\n<body>";

            msg += "\r\n<h1>�۳�����ģ����</h1> <br><a href=index.htm >������ҳ</a> - <a href='http://ccFlow.org' >���ʳ۳ҹ������̹���ϵͳ������������ٷ���վ</a> ����ϵͳ��������ϵ:QQ:793719823,Tel:18660153393<hr>";

            foreach (Flow fl in fls)
            {
                msg += "\r\n <h3><b><a href='./" + fl.No + "/index.htm' target=_blank>" + fl.Name + "</a></b> - <a href='" + fl.No + ".gif' target=_blank  >" + fl.Name + "����ͼ</a></h3>";

                msg += "\r\n<UL>";
                Nodes nds = fl.HisNodes;
                foreach (Node nd in nds)
                {
                    msg += "\r\n<li><a href='./" + fl.No + "/" + nd.NodeID + "_" + nd.FlowName + "_" + nd.Name + "��.doc' target=_blank>����" + nd.Step + ", - " + nd.Name + "ģ��</a> -<a href='./" + fl.No + "/" + nd.NodeID + "_" + nd.Name + "_��ģ��.htm' target=_blank>Html��</a></li>";
                }
                msg += "\r\n</UL>";
            }
            msg += "\r\n</body>";
            msg += "\r\n</html>";

            try
            {
                string pathDef = SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\" + SystemConfig.CustomerNo + "_index.htm";
                DataType.WriteFile(pathDef, msg);

                pathDef = SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\index.htm";
                DataType.WriteFile(pathDef, msg);
                System.Diagnostics.Process.Start(SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\");
            }
            catch
            {
            }
        }
        #endregion ��ѯ

        #region ��ѯ
        /// <summary>
        /// �����ȫ�����Զ�����
        /// </summary>
        public void RetrieveIsAutoWorkFlow()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FlowType, 1);
            qo.addOrderBy(FlowAttr.No);
            qo.DoQuery();
        }
        /// <summary>
        /// ��ѯ����ȫ�����������ڼ��ڵ�����
        /// </summary>
        /// <param name="flowSort">�������</param>
        /// <param name="IsCountInLifeCycle">�ǲ��Ǽ����������ڼ��� true ��ѯ����ȫ���� </param>
        public void Retrieve(string flowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FK_FlowSort, flowSort);
            qo.addOrderBy(FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public Flows() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public Flows(string fk_sort)
        {
            this.Retrieve(FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region �õ�ʵ��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Flow();
            }
        }
        #endregion
    }
}

