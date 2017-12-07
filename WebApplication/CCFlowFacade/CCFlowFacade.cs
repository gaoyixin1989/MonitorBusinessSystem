using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using BP.WF;
using BP.DA;
using i3.BusinessLogic.Channels.Mis.ProcessMgm;
using i3.ValueObject.Channels.Mis.ProcessMgm;

namespace WebApplication
{
    public class CCFlowFacade
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns>用户编号</returns>
        public static string UserLogin(string userNo)
        {
            if (BP.Web.WebUser.No != userNo)
                return BP.WF.Dev2Interface.Port_Login(userNo);

            return BP.Web.WebUser.SID;
        }

        #region 与父子流程相关

        /// <summary>
        /// 创建WorkID
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="ht">表单参数，可以为null。</param>
        /// <param name="workDtls">明细表参数，可以为null。</param>
        /// <param name="starter">流程的发起人</param>
        /// <param name="title">创建工作时的标题，如果为null，就按设置的规则生成。</param>
        /// <param name="parentWorkID">父流程的WorkID,如果没有父流程就传入为0.</param>
        /// <param name="parentFID">父流程的FID,如果没有父流程就传入为0.</param>
        /// <param name="parentFlowNo">父流程的流程编号,如果没有父流程就传入为null.</param>
        /// <param name="jumpToNode">要跳转到的节点,如果没有则为0.</param>
        /// <param name="jumpToEmp">要跳转到的人员,如果没有则为null.</param>
        /// <param name="jumpToEmp">要跳转到的人员,如果没有则为null.</param>
        /// <param name="paras">参数,格式：@GroupMark=xxxx@IsCC=1</param>
        /// <returns>为开始节点创建工作后产生的WorkID.</returns>
        public static Int64 Node_CreateBlankWork(string userNo, string flowNo,
            string starter, string title, Int64 parentWorkID, Int64 parentFID, string parentFlowNo, int parentNodeID, string parentEmp,
            int jumpToNode, string jumpToEmp, string paras)
        {
            UserLogin(userNo);

            Int64 workid = BP.WF.Dev2Interface.Node_CreateBlankWork(flowNo, null, null,
              starter, title, parentWorkID, parentFID, parentFlowNo, parentNodeID, parentEmp,
              jumpToNode, jumpToEmp);

            BP.WF.Dev2Interface.Flow_SetFlowParas(flowNo, workid, paras);

            return workid;
        }

        #endregion

        public static string GetFlowIdentification(string userNo, long workId)
        {
            UserLogin(userNo);

            BP.WF.GenerWorkFlow gwf = new BP.WF.GenerWorkFlow(workId);


            return gwf.Paras_GroupMark;
        }

        /// <summary>
        /// 返回子线程启动的子流程对应的父线程ID
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="workId"></param>
        /// <returns></returns>
        /// <remarks>yinchengyi 2015-4-23 </remarks>
        public static long GetFatherThreadIDOfSubFlow(string userNo, long workId)
        {
            UserLogin(userNo);

            BP.WF.GenerWorkFlow gwf = new BP.WF.GenerWorkFlow(workId);

            return gwf.PWorkID;
        }

        /// <summary>
        /// 返回子线程启动的子流程对应的父流程ID
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="workId"></param>
        /// <returns></returns>
        /// <remarks> 2015-9-25 </remarks>
        public static long GetFatherIDOfSubFlow(string userNo, long workId)
        {
            UserLogin(userNo);

            BP.WF.GenerWorkFlow gwf = new BP.WF.GenerWorkFlow(workId);

            return gwf.PFID;
        }

        /// <summary>
        /// 运行中 0	
        /// 已完成 1
        /// 其他   2	
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="workId"></param>
        /// <returns></returns>
        public static GenerWorkFlow GetGenerWorkFlow(string userNo, long workId)
        {
            UserLogin(userNo);

            try
            {
                BP.WF.GenerWorkFlow gwf = new BP.WF.GenerWorkFlow(workId);

                return gwf;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCCFLowStatus(string userNo, DataTable dt, string strStatus, int intPageIdx, int intPageSize, out int totalCount)
        {
            dt.Columns.Add("CCFlowStatus", typeof(string));

            dt.Columns.Add("WorkID", typeof(string));
            dt.Columns.Add("FK_Flow", typeof(string));
            dt.Columns.Add("FID", typeof(string));
            dt.Columns.Add("FK_Node", typeof(string));

            var resultDt = dt.Clone();

            var rowList = new List<DataRow>();

            foreach (var row in dt.Rows)
            {
                var dtRow = row as DataRow;

                var gwf = GetGenerWorkFlow(userNo, Convert.ToInt64(dtRow["CCFLOW_ID1"]));

                var ccflowStatus = (gwf == null ? 99 : (long)gwf.WFSta);

                if (strStatus != null)
                {
                    if (strStatus == "0" && (ccflowStatus == 0 || ccflowStatus == 2))
                    {
                        dtRow["CCFlowStatus"] = ccflowStatus == 0 ? "运行中" : "其他";
                        rowList.Add(dtRow);
                    }

                    if (strStatus == "1" && ccflowStatus == 1)
                    {
                        dtRow["CCFlowStatus"] = "已完成";
                        rowList.Add(dtRow);
                    }

                    if (strStatus == "0" && ccflowStatus == 99)
                    {
                        dtRow["CCFlowStatus"] = "未走CCFLOW";
                        rowList.Add(dtRow);
                    }
                }
                else
                {
                    if (ccflowStatus == 0)
                    {
                        dtRow["CCFlowStatus"] = "运行中";
                    }
                    else if (ccflowStatus == 2)
                    {
                        dtRow["CCFlowStatus"] = "其他";
                    }
                    else if (ccflowStatus == 1)
                    {
                        dtRow["CCFlowStatus"] = "已完成";
                    }
                    else if (ccflowStatus == 99)
                    {
                        dtRow["CCFlowStatus"] = "未走CCFLOW";
                    }

                    rowList.Add(dtRow);
                }

                if (gwf != null)
                {
                    dtRow["WorkID"] = gwf.WorkID;
                    dtRow["FK_Flow"] = gwf.FK_Flow;
                    dtRow["FID"] = gwf.FID;
                    dtRow["FK_Node"] = gwf.FK_Node;
                }
            }

            totalCount = rowList.Count;

            var resultRowList = rowList.Skip((intPageIdx - 1) * intPageSize).Take(intPageSize).ToList();

            foreach (var row in resultRowList)
            {
                resultDt.Rows.Add(row.ItemArray);
            }

            return resultDt;
        }

        /// <summary>
        /// 获取当前用户的待办列表数量
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static int GetEmpWorkCount(string userNo)
        {
            UserLogin(userNo);
            DataTable table = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
            return table.Rows.Count;
        }

        /// <summary>
        /// 获取在途的总量
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static int GetRuningCount(string userNo)
        {
            UserLogin(userNo);

            DataTable table = BP.WF.Dev2Interface.DB_GenerRuning(userNo, null);
            return table.Rows.Count;
        }

        /// <summary>
        /// 获取共享任务池数量
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public int GetApplyCount(string userNo)
        {
            UserLogin(userNo);

            DataTable table = BP.WF.Dev2Interface.DB_TaskPool();
            return table.Rows.Count;
        }

        /// <summary>
        /// 获取当前用户抄送列表
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="isRead"></param>
        /// <returns></returns>
        public static int GetCCCount(string userNo, bool? isRead)
        {
            UserLogin(userNo);

            DataTable table = new DataTable();
            if (isRead.HasValue == false)
                table = BP.WF.Dev2Interface.DB_CCList(userNo);
            else if (isRead.Value == false)
                table = BP.WF.Dev2Interface.DB_CCList(userNo, BP.WF.Template.CCSta.UnRead);
            else
                table = BP.WF.Dev2Interface.DB_CCList(userNo, BP.WF.Template.CCSta.Read);

            return table.Rows.Count;
        }

        /// <summary>
        /// 获取当前用户的待办列表
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetEmpWorks(string userNo, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);
            DataTable table = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
            table.Columns.Add("WFStateText");
            table.Columns.Add("TitleUrl");
            table.Columns.Add("FormUrl");

            table.Columns.Add("BusinessStartTime");
            table.Columns.Add("BusinessEndTime");

            foreach (DataRow row in table.Rows)
            {
                var workID = row["WorkID"].ToString();
                var fid = row["FID"].ToString();
                var pWorkID = row["PWorkID"].ToString();

                var url = "/WF/MyFlow.aspx?FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&FID=" + row["FID"] + "&WorkID=" + row["WorkID"] + "&AtPara=" + row["AtPara"] + "&UserNo=" + userNo;
                row["TitleUrl"] = url;

                string node = row["FK_Node"] + "";

                BP.WF.Node wfNode = new BP.WF.Node();
                wfNode.NodeID = int.Parse(node);
                wfNode.Retrieve();

                row["FormUrl"] = wfNode.FormUrl;

                string businessStartTime;
                string businessEndTime;

                GetBusniessStartAndEndTime(userNo, wfNode.FormUrl, workID, fid, pWorkID, out businessStartTime, out businessEndTime);
                row["BusinessStartTime"] = businessStartTime;
                row["BusinessEndTime"] = businessEndTime;

                if (!string.IsNullOrEmpty(businessEndTime))
                {
                    row["SDT"] = businessEndTime;
                }

                string text = "";

                switch (row["WFState"] + "")
                {

                    case "2":
                        text = "运行中";
                        break;
                    case "3":
                        text = "已完成";
                        break;
                    case "4":
                        text = "挂起";
                        break;
                    case "5":
                        text = "退回";
                        break;
                    case "6":
                        text = "移交";
                        break;
                    case "7":
                        text = "删除(逻辑)";
                        break;
                    case "8":
                        text = "加签";
                        break;

                    case "9":
                        text = "冻结";
                        break;

                    case "10":
                        text = "批处理";
                        break;
                    case "11":
                        text = "加签回复";
                        break;
                    default:
                        break;
                }

                row["WFStateText"] = text;
            }


            DataTable filterTable = FilterTable(queryParams, table);

            DataTable finalTable = PageTable(page, pageSize, filterTable);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);

            return xml;
        }

        private static void GetBusniessStartAndEndTime(string userNo, string url, string workID, string fid, string pWorkID, out string businessStartTime, out string businessEndTime)
        {
            businessStartTime = "";
            businessEndTime = "";

            var requestParams = url.Split(new string[] { "&", "?" }, StringSplitOptions.RemoveEmptyEntries);
            var mgmType = "";

            foreach (var param in requestParams)
            {
                if (param.ToLower().StartsWith("mgmtype"))
                {
                    mgmType = param.Split('=')[1].Trim();

                    break;
                }
            }

            if (mgmType == "")
            {
                return;
            }

            var ccflowID = "";

            if (!string.IsNullOrEmpty(pWorkID) && pWorkID != "0")
            {
                ccflowID = GetFatherIDOfSubFlow(userNo, Convert.ToInt64(workID)).ToString();
            }
            else if (!string.IsNullOrEmpty(fid) && fid != "0")
            {
                ccflowID = fid;
            }
            else
            {
                ccflowID = workID;
            }

            TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
            TMisMonitorProcessMgmVo objVo = objLogic.DetailsByCCFlowIDAndType(ccflowID, mgmType);

            if (objVo.ID != "")
            {
                businessStartTime = objVo.MONITOR_TIME_START;
                businessEndTime = objVo.MONITOR_TIME_FINISH;
            }
        }


        public static string GetBatchWorks(string userNo)
        {
            UserLogin(userNo);

            string sql = "SELECT a.NodeID, a.Name,a.FlowName, COUNT(WorkID) AS NUM  FROM WF_Node a, WF_EmpWorks b WHERE A.NodeID=b.FK_Node AND B.FK_Emp='" + userNo + "' AND b.WFState NOT IN (7) AND a.BatchRole!=0 GROUP BY A.NodeID, a.Name,a.FlowName ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            return WriteTableToXml(dt, dt.Rows.Count);
        }

        /// <summary>
        /// 获取当前用户的发起列表
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        public static string GetEmpStart(string userNo)
        {
            string sid = UserLogin(userNo);

            DataTable table = BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfDataTable(userNo);
            table.Columns.Add("NameUrl");
            table.Columns.Add("FlowPic");
            string url = "";
            foreach (DataRow row in table.Rows)
            {
                url = "/WF/MyFlow.aspx?FK_Flow=" + row["No"] + "&FK_Node=" + row["No"] + "01" + "&UserNo=" + userNo + "&SID=" + sid;
                row["NameUrl"] = url;
                if (row["IsBatchStart"].ToString() == "1")
                {
                    url = "/WF/BatchStart.aspx?FK_Flow=" + row["No"] + "&UserNo=" + userNo;
                    row["IsBatchStart"] = url;
                }
                url = "/DataUser/FlowDesc/" + row["No"] + "." + row["Name"] + "/Flow.png";
                row["FlowPic"] = url;
            }

            string xml = WriteTableToXml(table, table.Rows.Count);
            return xml;


        }

        /// <summary>
        /// 获取用户的历史发起流程
        /// </summary>
        public static string GetEmpStartHistory(string userNo, string fk_flow, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);
            BP.WF.Flow startFlow = new BP.WF.Flow(fk_flow);

            string sql = "SELECT * FROM " + startFlow.PTable + " WHERE FlowStarter='" + userNo + "'  and WFState not in (" + (int)BP.WF.WFState.Blank + "," + (int)BP.WF.WFState.Draft + ")";
            DataTable table = startFlow.RunSQLReturnTable(sql);
            table.Columns.Add("TitleUrl");
            table.Columns.Add("WFStateText");
            var url = "";
            foreach (DataRow row in table.Rows)
            {
                url = "/WF/WFRpt.aspx?WorkID=" + row["OID"] + "&FK_Flow=" + fk_flow + "&FID=" + row["FID"] + "&UserNo=" + userNo;
                row["TitleUrl"] = url;
                string text = "";
                switch (row["WFState"] + "")
                {
                    case "2":
                        text = "运行中";
                        break;
                    case "3":
                        text = "已完成";
                        break;
                    case "4":
                        text = "挂起";
                        break;
                    case "5":
                        text = "退回";
                        break;
                    case "6":
                        text = "移交";
                        break;
                    case "7":
                        text = "删除(逻辑)";
                        break;
                    case "8":
                        text = "加签";
                        break;
                    case "9":
                        text = "冻结";
                        break;
                    case "10":
                        text = "批处理";
                        break;
                    case "11":
                        text = "加签回复";
                        break;
                    default:
                        break;
                }

                row["WFStateText"] = text;
            }

            DataTable filterTable = FilterTable(queryParams, table);

            DataTable finalTable = PageTable(page, pageSize, table);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);

            return xml;
        }

        /// <summary>
        /// 获取共享任务池
        /// </summary>
        /// <returns></returns>
        /// <returns>已申请</returns>
        public static string GetSharing(string userNo, bool isShare, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);
            DataTable table = new DataTable();
            if (isShare)
                table = BP.WF.Dev2Interface.DB_TaskPoolOfMyApply();
            else
                table = BP.WF.Dev2Interface.DB_TaskPool();

            string url = "";

            table.Columns.Add("Operation");
            table.Columns.Add("WFStateText");
            table.Columns.Add("TitleUrl");
            foreach (DataRow row in table.Rows)
            {
                string url2 = "/WF/MyFlow.aspx?FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&FID=" + row["FID"] + "&WorkID=" + row["WorkID"] + "&UserNo=" + userNo;
                row["TitleUrl"] = url2;
                if (!isShare)
                    url = "/WF/Do.aspx?ActionType=DoAppTask&WorkID=" + row["WorkID"] + "&UserNo=" + userNo;
                else
                    url = "/WF/Do.aspx?ActionType=PutOne&WorkID=" + row["WorkID"] + "&UserNo=" + userNo;
                row["Operation"] = url;

                string text = "";
                switch (row["WFState"] + "")
                {
                    case "2":
                        text = "运行中";
                        break;
                    case "3":
                        text = "已完成";
                        break;
                    case "4":
                        text = "挂起";
                        break;
                    case "5":
                        text = "退回";
                        break;
                    case "6":
                        text = "移交";
                        break;
                    case "7":
                        text = "删除(逻辑)";
                        break;
                    case "8":
                        text = "加签";
                        break;
                    case "9":
                        text = "冻结";
                        break;
                    case "10":
                        text = "批处理";
                        break;
                    case "11":
                        text = "加签回复";
                        break;
                    default:
                        break;
                }

                row["WFStateText"] = text;
            }

            DataTable filterTable = FilterTable(queryParams, table);

            DataTable finalTable = PageTable(page, pageSize, table);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);

            return xml;
        }

        /// <summary>
        /// 获取用户的在途信息
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetRunning(string userNo, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);

            DataTable table = BP.WF.Dev2Interface.DB_GenerRuning(userNo, null);
            string url = "";
            table.Columns.Add("TitleUrl");
            table.Columns.Add("WFStateText");
            table.Columns.Add("FlowEmps");
            table.Columns.Add("UnSendUrl");
            table.Columns.Add("PressUrl");

            foreach (DataRow row in table.Rows)
            {
                url = "/WF/WFRpt.aspx?WorkID=" + row["WorkID"] + "&FK_Flow=" + row["FK_Flow"] + "&FID=" + row["FID"] + "&UserNo=" + userNo;
                row["TitleUrl"] = url;

                string unSendUrl = "/AppDemoLigerUI/Base/DataService.aspx?method=unsend&FK_Flow=" + row["FK_Flow"] + "&WorkID=" + row["WorkID"] + "&UserNo=" + userNo;
                string pressUrl = "/WF/WorkOpt/Press.aspx?FID=" + row["FID"] + "&WorkID=" + row["WorkID"] + "&FK_Flow=" + row["FK_Flow"] + "&UserNo=" + userNo;
                //  row["Operation"] = "<a href=\"javascript:winOperation('" + url + "','false')\">撤销</a>-<a href=\"javascript:winOpen('" + pressUrl + "'))\">催办</a>";
                row["UnSendUrl"] = unSendUrl;
                row["PressUrl"] = pressUrl;

                string emps = row["Emps"] + "";

                foreach (string single in emps.Split('@'))
                {
                    if (!string.IsNullOrEmpty(single))
                    {
                        try
                        {
                            row["FlowEmps"] += new BP.Port.Emp(single).Name + ";";
                        }
                        catch (Exception ex)
                        {
                            row["FlowEmps"] += single + ";";
                        }
                    }
                }

                string text = "";
                switch (row["WFState"] + "")
                {

                    case "2":
                        text = "运行中";
                        break;
                    case "3":
                        text = "已完成";
                        break;
                    case "4":
                        text = "挂起";
                        break;
                    case "5":
                        text = "退回";
                        break;
                    case "6":
                        text = "移交";
                        break;
                    case "7":
                        text = "删除(逻辑)";
                        break;
                    case "8":
                        text = "加签";
                        break;
                    case "9":
                        text = "冻结";
                        break;
                    case "10":
                        text = "批处理";
                        break;
                    case "11":
                        text = "加签回复";
                        break;
                    default:
                        break;
                }

                row["WFStateText"] = text;
            }

            DataTable filterTable = FilterTable(queryParams, table);
            DataTable finalTable = PageTable(page, pageSize, table);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);
            return xml;
        }

        /// <summary>
        /// 获取用户已完成列表
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetCompleateWork(string userNo, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);

            string sql = string.Format("select * from WF_GenerWorkFlow  where WFState in({0}) and Emps like '%{1}%'", "'" + (int)BP.WF.WFState.Complete + "'", "@" + userNo);

            DataTable table = BP.DA.DBAccess.RunSQLReturnTable(sql);

            table.Columns.Add("TitleUrl");
            table.Columns.Add("FlowEmps");
            table.Columns.Add("FlowEnderRDT");
            string url = "";
            foreach (DataRow row in table.Rows)
            {
                url = "/WF/WFRpt.aspx?WorkID=" + row["WorkID"] + "&FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&UserNo=" + userNo;
                row["TitleUrl"] = url;

                string emps = row["Emps"] + "";

                foreach (string single in emps.Split('@'))
                {
                    if (!string.IsNullOrEmpty(single))
                    {
                        try
                        {
                            row["FlowEmps"] += new BP.Port.Emp(single).Name + ";";
                        }
                        catch (Exception ex)
                        {
                            row["FlowEmps"] += single + ";";
                        }
                    }
                }

                BP.WF.Flow flow = new BP.WF.Flow(row["FK_Flow"] + "");

                BP.WF.Data.GERpt rpt = flow.HisGERpt;
                rpt.OID = Int64.Parse(row["WorkID"] + "");
                int count = rpt.RetrieveFromDBSources();
                if (count > 0)
                {
                    row["FlowEnderRDT"] = rpt.FlowEnderRDT;
                }
            }

            DataTable filterTable = FilterTable(queryParams, table);
            DataTable finalTable = PageTable(page, pageSize, table);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);

            return xml;
        }

        /// <summary>
        /// 获取当前用户的抄送信息
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetCC(string userNo, string[][] queryParams, int? page, int? pageSize, bool isAllUser = false)
        {
            UserLogin(userNo);

            DataTable table;

            if (isAllUser)
            {
                table = BP.WF.Dev2Interface.DB_CCList(null);
            }
            else
            {
                table = BP.WF.Dev2Interface.DB_CCList(userNo);
            }

            string url = "";
            table.Columns.Add("TitleUrl");
            table.Columns.Add("StaText");

            foreach (DataRow row in table.Rows)
            {
                string sta = row["Sta"] + "";
                if (sta == "0")
                {
                    if (isAllUser)
                    {
                        url = "/WF/WorkOpt/OneWork/Track.aspx?FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&WorkID=" + row["WorkID"] + "&FID=" + row["FID"] + "&Sta=" + row["Sta"] + "&MyPK=" + row["MyPk"] + "&UserNo=" + userNo;
                    }
                    else
                    {
                        url = "/WF/Do.aspx?DoType=DoOpenCC&FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&WorkID=" + row["WorkID"] + "&FID=" + row["FID"] + "&Sta=" + row["Sta"] + "&MyPK=" + row["MyPk"] + "&UserNo=" + userNo;
                    }

                    row["StaText"] = "未读";
                }
                else
                {
                    url = "/WF/WorkOpt/OneWork/Track.aspx?FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&WorkID=" + row["WorkID"] + "&FID=" + row["FID"] + "&Sta=" + row["Sta"] + "&MyPK=" + row["MyPk"] + "&UserNo=" + userNo;
                    row["StaText"] = "已读";
                }
                row["TitleUrl"] = url;
            }

            DataTable filterTable = FilterTable(queryParams, table);

            DataView filterView = filterTable.DefaultView;

            filterView.Sort = "Sta asc,FK_Node asc";

            filterTable = filterView.ToTable();

            DataTable finalTable = PageTable(page, pageSize, filterTable);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);
            return xml;
        }

        /// <summary>
        /// 获取挂起信息
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetHungUp(string userNo, string[][] queryParams, int? page, int? pageSize)
        {
            UserLogin(userNo);

            DataTable table = BP.WF.Dev2Interface.DB_GenerHungUpList();
            string url = "";
            foreach (DataRow row in table.Rows)
            {
                url = "/WF/MyFlow.aspx?FK_Flow=" + row["FK_Flow"] + "&FK_Node=" + row["FK_Node"] + "&FID=" + row["FID"] + "&WorkID=" + row["WorkID"] + "&IsRead=0&UserNo=" + userNo;
                row["Title"] = url;
            }

            DataTable filterTable = FilterTable(queryParams, table);
            DataTable finalTable = PageTable(page, pageSize, table);

            string xml = WriteTableToXml(finalTable, filterTable.Rows.Count);

            return xml;
        }

        /// <summary>
        /// 设置下一步骤的接收人
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="nodeid"></param>
        /// <param name="emps"></param>
        /// <returns></returns>
        public static bool SetNextWork(string flowid, Int64 workid, int nodeid, string emps, int fid)
        {
            try
            {
                BP.WF.Nodes nodes = BP.WF.Dev2Interface.WorkOpt_GetToNodes(flowid, nodeid, workid, fid);
                int nodeID = 0;

                foreach (BP.WF.Node node in nodes)
                {
                    nodeID = node.NodeID;
                    break;
                }

                BP.WF.Dev2Interface.Node_AddNextStepAccepters(workid, nodeID, emps);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 设置下一节点的工作人员多维度
        /// </summary>
        /// <param name="flowid"></param>
        /// <param name="workid"></param>
        /// <param name="nodeid"></param>
        /// <param name="emps"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public static bool SetNextNodeFH(string flowid, Int64 workid, int nodeid, string emps, int fid)
        {
            try
            {                

                BP.WF.Nodes nodes = BP.WF.Dev2Interface.WorkOpt_GetToNodes(flowid, nodeid, workid, fid);
                int nodeID = 0;

                foreach (BP.WF.Node node in nodes)
                {
                    nodeID = node.NodeID;
                    break;
                }

                string[] empData = emps.Split(',');

                foreach (string emp in empData)
                {
                    Compeader.Data.Comleader_NodeWork nodeWork = new Compeader.Data.Comleader_NodeWork();

                    string[] empAndTag = emp.Split('@');

                    string userName = "";
                    string tag = "";
                    string userNo = "";
                    if (empAndTag.Length >= 2)
                    {
                        tag = empAndTag[1];
                        userNo = empAndTag[0];

                        try
                        {
                            BP.Port.Emp empSingle = new BP.Port.Emp(userNo);
                            userName = empSingle.Name;
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    nodeWork.MyPk = Guid.NewGuid().ToString("N");
                    nodeWork.OID = workid;
                    nodeWork.NodeID = nodeid;
                    nodeWork.UserNo = userNo;
                    nodeWork.Tag = tag;
                    nodeWork.UserName = userName;
                    nodeWork.Insert();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SetNextCC(Int64 workid, int nodeid, string emps)
        {
            try
            {
                Compeader.Data.Comleader_CCList cclist = new Compeader.Data.Comleader_CCList();

                cclist.CheckPhysicsTable();

                cclist.OID = workid;
                cclist.NodeID = nodeid;
                int i = cclist.RetrieveFromDBSources();
                if (i > 0)
                    cclist.DirectDelete();

                foreach (string emp in emps.Split(','))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(emp))
                        {
                            BP.Port.Emp empSingle = new BP.Port.Emp(emp);
                            cclist.OID = workid;
                            cclist.NodeID = nodeid;
                            cclist.UserNo = empSingle.No;
                            cclist.UserName = empSingle.Name;
                            cclist.MyPk = Guid.NewGuid().ToString("N");
                            cclist.DirectInsert();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取批处理
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static string GetBatch(string userNo)
        {
            UserLogin(userNo);

            string sql = "SELECT a.NodeID, a.Name,a.FlowName, COUNT(*) AS NUM  FROM WF_Node a, WF_EmpWorks b WHERE A.NodeID=b.FK_Node AND B.FK_Emp='" + BP.Web.WebUser.No + "' AND b.WFState NOT IN (7) AND a.BatchRole!=0 GROUP BY A.NodeID, a.Name,a.FlowName ";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

            dt.Columns.Add("BatchTitle");
            dt.Columns.Add("BatchTitleUrl");
            dt.Columns.Add("WFStateText");
            string url = "";

            foreach (DataRow dr in dt.Rows)
            {
                dr["BatchTitle"] = dr["FlowName"].ToString() + " --> " + dr["Name"].ToString() + "(" + dr["Num"] + ")";
                url = "/WF/Batch.aspx?FK_Node=" + dr["NodeID"];
                dr["BatchTitleUrl"] = url;

                string text = "";
                switch (dr["WFState"] + "")
                {
                    case "2":
                        text = "运行中";
                        break;
                    case "3":
                        text = "已完成";
                        break;
                    case "4":
                        text = "挂起";
                        break;
                    case "5":
                        text = "退回";
                        break;
                    case "6":
                        text = "移交";
                        break;
                    case "7":
                        text = "删除(逻辑)";
                        break;
                    case "8":
                        text = "加签";
                        break;
                    case "9":
                        text = "冻结";
                        break;
                    case "10":
                        text = "批处理";
                        break;
                    case "11":
                        text = "加签回复";
                        break;
                    default:
                        break;
                }

                dr["WFStateText"] = text;

            }

            string xml = WriteTableToXml(dt, dt.Rows.Count);
            return xml;
        }


        public static string GetSingleWork(Int64 workid)
        {
            string sql = string.Format("Select * from WF_Generwrokflow where WorkID='{0}'", workid);

            DataTable table = BP.DA.DBAccess.RunSQLReturnTable(sql);

            string xml = WriteTableToXml(table, table.Rows.Count);

            return xml;
        }

        /// <summary>
        /// 执行抄送
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="fk_node">节点编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="toEmpNo">抄送给人员编号,多个用逗号分开比如 zhangsan,lisi</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgDoc">消息内容</param>
        /// <param name="pFlowNo">父流程编号(可以为null)</param>
        /// <param name="pWorkID">父流程WorkID(可以为0)</param>
        /// <returns></returns>
        public static string Node_CC(string userNo, string sid, string fk_flow, int fk_node, Int64 workID, string toEmpNos, string msgTitle, string msgDoc, string pFlowNo, Int64 pWorkID)
        {
            UserLogin(userNo);

            toEmpNos = toEmpNos.Replace(";", ",");
            toEmpNos = toEmpNos.Replace("；", ",");
            toEmpNos = toEmpNos.Replace("，", ",");

            string[] toEmps = toEmpNos.Split(',');
            string strs = "";
            foreach (string item in toEmps)
            {
                if (string.IsNullOrEmpty(item) == true)
                    continue;
                BP.Port.Emp emp = new BP.Port.Emp(item);
                strs += emp.Name + " ";

                BP.WF.Dev2Interface.Node_CC(fk_flow, fk_node, workID, emp.No, emp.Name, msgTitle, msgDoc, pFlowNo, pWorkID);
            }

            return "执行抄送成功,抄送给:" + strs;

        }

        /// <summary>
        /// 将table 转换xml 格式
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static string WriteTableToXml(DataTable table, int count)
        {
            StringBuilder xmlStr = new StringBuilder();
            xmlStr.Append("<root>");

            xmlStr.Append("<count>");
            xmlStr.Append(count);
            xmlStr.Append("</count>");
            xmlStr.Append("<record>");
            foreach (DataRow row in table.Rows)
            {
                xmlStr.Append("<row>");

                foreach (DataColumn column in table.Columns)
                {
                    xmlStr.Append("<" + column.ColumnName + ">" + HttpUtility.UrlEncode(row[column.ColumnName] + "") + "</" + column.ColumnName + ">");
                }

                xmlStr.Append("</row>");
            }

            xmlStr.Append("</record>");

            xmlStr.Append("</root>");

            return xmlStr.ToString();
        }

        /// <summary>
        /// 对datatable 进行查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DataTable FilterTable(string[][] queryParams, DataTable dt)
        {
            string rowFileter = " 1=1 ";

            ///   queryParams[0] 列名, queryParams[1] 值, queryParams[2]过滤方式, 
            ///   过滤方式: eq : 等于,lt：小于,gt:大于，ge:大于等于,le:小于等于, like:模糊查询
            ///   生成RowFilter
            foreach (string[] query in queryParams)
            {
                if (query.Length < 3 || string.IsNullOrEmpty(query[0]) || string.IsNullOrEmpty(query[2]))
                    break;
                //组成RowFilter
                switch (query[2])
                {
                    case "eq":
                        rowFileter += " and " + query[0] + "='" + query[1] + "'  ";
                        break;
                    case "neq":
                        rowFileter += " and " + query[0] + " not in('" + query[1] + "')  ";
                        break;
                    case "lt":
                        rowFileter += " and " + query[0] + "<'" + query[1] + "'  ";
                        break;
                    case "gt":
                        rowFileter += " and " + query[0] + ">'" + query[1] + "'  ";
                        break;
                    case "ge":
                        rowFileter += " and " + query[0] + ">='" + query[1] + "'  ";
                        break;
                    case "le":
                        rowFileter += " and " + query[0] + "<='" + query[1] + "'  ";
                        break;
                    case "like":
                        rowFileter += " and " + query[0] + " like '%" + query[1] + "%'";
                        break;
                }
            }

            DataView filterView = dt.DefaultView;

            filterView.RowFilter = rowFileter;

            return filterView.ToTable();
        }

        /// <summary>
        ///  对datatable 进行分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable PageTable(int? page, int? pageSize, DataTable dt)
        {
            if (pageSize.HasValue == false || page.HasValue == false || page.Value < 0 || pageSize.Value == 0 || dt.Rows.Count == 0)//如果集合为空或者pageSize为空
                return dt;

            DataTable copyTable = dt.Copy();//组成一个新的Table
            copyTable.Clear();

            var rowBegin = (page.Value - 1) * pageSize.Value;//开始的数据
            var rowEnd = page.Value * pageSize.Value;//结束的数据

            if (rowEnd > dt.Rows.Count)
                rowEnd = dt.Rows.Count;

            //组成新的table数据。
            for (int i = rowBegin; i <= rowEnd - 1; i++)
            {
                DataRow newdr = copyTable.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                copyTable.Rows.Add(newdr);
            }
            return copyTable;
        }

        public static void SetFlowTitle(string userNo, string flowNo, long workId, string title)
        {
            UserLogin(userNo);

            try
            {
                Dev2Interface.Flow_SetFlowTitle(flowNo, workId, title);
            }
            catch
            {
                return;
            }
        }
    }
}