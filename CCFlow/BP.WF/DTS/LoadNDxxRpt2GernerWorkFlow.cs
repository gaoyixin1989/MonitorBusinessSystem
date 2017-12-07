using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
using BP.WF.Data;

namespace BP.WF.DTS
{
    /// <summary>
    /// װ���Ѿ���ɵ��������ݵ�WF_GenerWorkflow
    /// </summary>
    public class LoadNDxxRpt2GernerWorkFlow : Method
    {
        /// <summary>
        /// װ���Ѿ���ɵ��������ݵ�WF_GenerWorkflow
        /// </summary>
        public LoadNDxxRpt2GernerWorkFlow()
        {
            this.Title = "װ���Ѿ���ɵ��������ݵ�WF_GenerWorkflow��������չ�����������ģʽ�µľ����ݲ�ѯ���������⣩";
            this.Help = "������չ�����������ģʽ�µľ����ݲ�ѯ���������⡣";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
           
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                if (BP.Web.WebUser.No == "admin")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            BP.WF.Flows ens = new Flows();
            foreach (BP.WF.Flow en in ens)
            {
                string sql = "SELECT * FROM "+en.PTable+" WHERE OID NOT IN (SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Flow='"+en.No+"')";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = Int64.Parse(dr[NDXRptBaseAttr.OID].ToString());
                    gwf.FID = Int64.Parse(dr[NDXRptBaseAttr.FID].ToString());

                    gwf.FK_FlowSort = en.FK_FlowSort;
                    gwf.FK_Flow = en.No;
                    gwf.FlowName = en.Name;
                    gwf.Title = dr[NDXRptBaseAttr.Title].ToString();
                    gwf.WFState = (WFState)int.Parse(dr[NDXRptBaseAttr.WFState].ToString());
                    gwf.WFSta = WFSta.Complete;

                    gwf.Starter = dr[NDXRptBaseAttr.FlowStarter].ToString();
                    gwf.StarterName = dr[NDXRptBaseAttr.FlowStarter].ToString();
                    gwf.RDT = dr[NDXRptBaseAttr.FlowStartRDT].ToString();
                    gwf.FK_Node = int.Parse(dr[NDXRptBaseAttr.FlowEndNode].ToString());
                    gwf.FK_Dept = dr[NDXRptBaseAttr.FK_Dept].ToString();

                    Port.Dept dept=null;
                    try
                    {
                        dept=new Port.Dept(gwf.FK_Dept);
                        gwf.DeptName =dept.Name; 
                    }
                    catch
                    {
                        gwf.DeptName = gwf.FK_Dept;
                    }

                    gwf.PRI = int.Parse(dr[NDXRptBaseAttr.FK_Dept].ToString());

                    //  gwf.SDTOfNode = dr[NDXRptBaseAttr.FK_Dept].ToString();
                    // gwf.SDTOfFlow = dr[NDXRptBaseAttr.FK_Dept].ToString();

                    gwf.PFlowNo = dr[NDXRptBaseAttr.FK_Dept].ToString();
                    gwf.PWorkID = Int64.Parse(dr[NDXRptBaseAttr.FK_Dept].ToString());
                    gwf.PNodeID = int.Parse(dr[NDXRptBaseAttr.FK_Dept].ToString());
                    gwf.PEmp = dr[NDXRptBaseAttr.FK_Dept].ToString();

                    gwf.CFlowNo = dr[NDXRptBaseAttr.CFlowNo].ToString();
                    gwf.CWorkID = Int64.Parse(dr[NDXRptBaseAttr.CWorkID].ToString());

                    gwf.GuestNo = dr[NDXRptBaseAttr.GuestNo].ToString();
                    gwf.GuestName = dr[NDXRptBaseAttr.GuestName].ToString();
                    gwf.BillNo = dr[NDXRptBaseAttr.BillNo].ToString();
                    //gwf.FlowNote = dr[NDXRptBaseAttr.flowno].ToString();

                    gwf.SetValByKey("Emps", dr[NDXRptBaseAttr.FlowEmps].ToString());
                    //   gwf.AtPara = dr[NDXRptBaseAttr.FK_Dept].ToString();
                    //  gwf.GUID = dr[NDXRptBaseAttr.gu].ToString();
                    gwf.Insert();
                }

            }
            return "ִ�гɹ�...";
        }
    }
}
