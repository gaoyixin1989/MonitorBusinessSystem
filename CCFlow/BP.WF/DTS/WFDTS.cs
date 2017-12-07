using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.Sys;
using BP.WF.Data;
using BP.WF.Template;
using BP.DTS;

namespace BP.WF.DTS
{
    public class CheckNodes :BP.DTS.DataIOEn
    {
        /// <summary>
        /// ������Ա,��λ,����
        /// </summary>
        public CheckNodes()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�޸��ڵ���Ϣ";
          //  this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {

            //MDCheck md = new MDCheck();
            //md.Do();

            //ִ�е��Ȳ��š�
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();

            // ������Ա��Ϣ��
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }

    public class UserPort : DataIOEn2
    {
        /// <summary>
        /// ������Ա,��λ,����
        /// </summary>
        public UserPort()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�������̲���(������ϵͳ��һ�ΰ�װʱ���߲��ű仯ʱ)";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            //ִ�е��Ȳ��š�
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();
            // ������Ա��Ϣ��
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }
    public class DelWorkFlowData : DataIOEn
    {
        public DelWorkFlowData()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=red><b>�����������</b></font>";
            //this.HisRunTimeType = RunTimeType.UnName;
            //this.FromDBUrl = DBUrlType.AppCenterDSN;
            //this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            if (BP.Web.WebUser.No != "admin")
                throw new Exception("�Ƿ��û���");

          //  DA.DBAccess.RunSQL("DELETE FROM WF_CHOfFlow");
            DA.DBAccess.RunSQL("DELETE FROM WF_Bill");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow");
          //  DA.DBAccess.RunSQL("DELETE FROM WF_WORKLIST");
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork");
            DA.DBAccess.RunSQL("DELETE FROM WF_GECheckStand");
            DA.DBAccess.RunSQL("DELETE FROM WF_GECheckMul");
        //    DA.DBAccess.RunSQL("DELETE FROM WF_ForwardWork");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper");

            // ɾ��.
            CCLists ens = new CCLists();
            ens.ClearTable();

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string msg = "";
            foreach (Node nd in nds)
            {
                
                Work wk =  null;
                try
                {
                    wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE FROM " + wk.EnMap.PhysicsTable);
                }
                catch (Exception ex)
                {
                    wk.CheckPhysicsTable();
                    msg += "@" + ex.Message;
                }
            }

            if (msg != "")
                throw new Exception(msg);
        }
    }
    public class InitBillDir : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public InitBillDir()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=green><b>��������Ŀ¼(������ÿ�θ��ĵ����ĺŻ�ÿ��һ��)</b></font>";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        /// <summary>
        /// ��������Ŀ¼
        /// </summary>
        public override void Do()
        {

            Depts Depts = new Depts();
            QueryObject qo = new QueryObject(Depts);
      //      qo.AddWhere("Grade", " < ", 4);
            qo.DoQuery();

            BillTemplates funcs = new BillTemplates();
            funcs.RetrieveAll();


            string path = BP.WF.Glo.FlowFileBill  ;
            string year = DateTime.Now.Year.ToString();

            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            if (System.IO.Directory.Exists(path + "\\\\" + year) == false)
                System.IO.Directory.CreateDirectory(path + "\\\\" + year);


            foreach (Dept Dept in Depts)
            {
                if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No) == false)
                    System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No);

                foreach (BillTemplate func in funcs)
                {
                    if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No) == false)
                        System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No);
                }
            }
        }
    }

    public class OutputSQLs : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public OutputSQLs()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "OutputSQLs for produces DTSCHofNode";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Log.DefaultLogWriteLine(LogType.Info, BP.Web.WebUser.Name + "��ʼ���ȿ�����Ϣ:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string infoMsg = "", errMsg = "";

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string fromDateTime = DateTime.Now.Year + "-01-01";
            fromDateTime = "2004-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            string insertSql = "";
            string delSQL = "";
            string updateSQL = "";

            string sqls = "";
            int i = 0;
            foreach (Node nd in nds)
            {
                if (nd.IsPCNode)  /* ����Ǽ�����ڵ�.*/
                    continue;
                i++;
                Map map = nd.HisWork.EnMap;
                delSQL = "\n DELETE FROM " + map.PhysicsTable + " WHERE  OID  NOT IN (SELECT WORKID FROM WF_GenerWorkFlow ) AND WFState= " + (int)WFState.Runing;

                sqls += "\n\n\n -- NO:" + i + "��" + nd.FK_Flow + nd.FlowName + " :  " + map.EnDesc + " \n" + delSQL + "; \n" + insertSql + "; \n" + updateSQL + ";";
            }
            Log.DefaultLogWriteLineInfo(sqls);
            return sqls;
        }
    }
    public class OutputSQLOfDeleteWork : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public OutputSQLOfDeleteWork()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "����ɾ���ڵ����ݵ�sql.";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            string delSQL = "";
            foreach (Node nd in nds)
            {
                delSQL += "\n DELETE FROM " + nd.PTable + "  ; ";
            }
            return delSQL;
        }
    }
	
    ///// <summary>
    ///// ������Ӧ�õ��ľ�̬������
    ///// </summary>
    //public class WFDTS
    //{
    //    /// <summary>
    //    /// ����ͳ�Ʒ���
    //    /// </summary>
    //    /// <param name="fromDateTime"></param>
    //    /// <returns></returns>
    //    public static string InitFlows(string fromDateTime)
    //    {
    //        return null; /* �����������Ӧ�����ˡ�*/
    //        //Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " ################# Start ִ��ͳ�� #####################");
    //        ////ɾ�����Ŵ��������
    //        ////DBAccess.RunSQL("DELETE FROM WF_BadWF WHERE BadFlag='FlowDeptBad'");
    //        //fromDateTime = "2004-01-01 00:00";
    //        //Flows fls = new Flows();
    //        //fls.RetrieveAll();
    //        //CHOfFlow fs = new CHOfFlow();
    //        //foreach (Flow fl in fls)
    //        //{
    //        //    Node nd = fl.HisStartNode;
    //        //    try
    //        //    {
    //        //        string sql = "INSERT INTO WF_CHOfFlow SELECT OID WorkID, " + fl.No + " as FK_Flow, WFState, ltrim(rtrim(Title)) as Title, Rec as FK_Emp,"
    //        //            + " RDT, CDT, 0 as SpanDays,'' FK_Dept,"
    //        //            + "'' as FK_Dept,'' AS FK_NY,'' as FK_AP,'' AS FK_ND, '' AS FK_YF, Rec ,'' as FK_XJ, '' as FK_Station   "
    //        //            + " FROM " + nd.HisWork.EnMap.PhysicsTable + " WHERE RDT>='" + fromDateTime + "' AND OID NOT IN ( SELECT WorkID FROM WF_CHOfFlow  )";
    //        //        DBAccess.RunSQL(sql);
    //        //    }
    //        //    catch (Exception ex)
    //        //    {
    //        //        throw new Exception(fl.Name + "   " + nd.Name + "" + ex.Message);
    //        //    }
    //        //}
    //        //DBAccess.RunSP("WF_UpdateCHOfFlow");
    //        //Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " End ִ��ͳ�Ƶ���");
    //        //return "";
    //    }
    //}

    
     
}
