using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
//using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using BP.Sys;
using BP.DA;
using BP.En;
using BP;
using BP.Web;
using System.Security.Cryptography;
using System.Text;
using BP.Port;
using BP.WF.Rpt;
using BP.WF.Data;
using BP.WF.Template;

namespace BP.WF
{
    /// <summary>
    /// ȫ��(��������)
    /// </summary>
    public class Glo
    {
        public static string UserNo = null;

        #region ִ�а�װ/����.
        /// <summary>
        /// ִ������
        /// </summary>
        /// <returns></returns>
        public static string UpdataCCFlowVer()
        {
            #region ����Ƿ���Ҫ������������������ҵ���߼�.
            string val = "20150814";
            string updataNote = "";
            updataNote += "20150730.���ӿ��˱�.";
            updataNote += "20150725.�������̲鿴Ȩ�޹���.";
            updataNote += "20150613.����CCRole.";
            updataNote += "201505302.����FWCType.";
            updataNote += "2015051673.����������.";
            updataNote += "20150516. Ϊ������������������ͬ������.";
            updataNote += "20150508. ����ɾ��bpmģʽ��һ����ͼ by:DaiGuoQiang.";
            updataNote += "20150506. ������bpmģʽ��һ����ͼ.";
            updataNote += "20150505. �����˸�λö��ֵ������ by:zhoupeng.";
            updataNote += "20150424. �Ż������б��Ч��, by:zhoupeng.";

            /*
             * �����汾��¼:
             * 20150330: �Ż������б��Ч��, by:zhoupeng.
             * 2, ��������,֧�ֶ�̬����.
             * 1, ִ��һ��Sender�����˵�������ԭ����GenerWorkerList ת��WF_GenerWorkFlow.
             * 0, ��Ĭ������������.2014-12
             */
            string sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey='Ver'";
            string currVer = DBAccess.RunSQLReturnStringIsNull(sql, "");
            if (currVer == val)
                return null; //����Ҫ����.
            #endregion ����Ƿ���Ҫ������������������ҵ���߼�.

            string msg = "";
            try
            {
                // ���� PassRate.
                string mySQL = "UPDATE WF_Node SET PassRate=100 WHERE PassRate IS NULL";
                BP.DA.DBAccess.RunSQL(mySQL);

                //�����λö��ֵ������.
                if (BP.WF.Glo.OSModel == WF.OSModel.BPM)
                {
                    try
                    {
                        mySQL = "UPDATE PORT_STATION A SET A.FK_STATIONTYPE=A.STAGRADE WHERE A.FK_STATIONTYPE IS NULL";
                        BP.DA.DBAccess.RunSQL(mySQL);
                    }
                    catch
                    {
                    }
                }

                //#region  ����CAǩ��(2015-03-03)
                //BP.Tools.WFSealData sealData = new Tools.WFSealData();
                //sealData.CheckPhysicsTable();
                //sealData.UpdateColumn();
                //#endregion  ����CAǩ��.

                BP.WF.Data.CH ch = new CH();
                ch.CheckPhysicsTable();
                
                TransferCustom tc = new TransferCustom();
                tc.CheckPhysicsTable();

                //���Ӳ����ֶΡ�
                CCList cc = new CCList();
                cc.CheckPhysicsTable();
             
               
                #region �������ݸ���.
                //ɾ��ö��ֵ,�����Զ�����.
                string enumKey = "";
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey IN ('CCRole','FWCType','SelectAccepterEnable','NodeFormType','StartGuideWay','" + FlowAttr.StartLimitRole + "','BillFileType','EventDoType','FormType','BatchRole','StartGuideWay','NodeFormType')");

                Node wf_Node = new Node();
                wf_Node.CheckPhysicsTable();
                // ���ýڵ�ICON.
                sql = "UPDATE WF_Node SET ICON='/WF/Data/NodeIcon/���.png' WHERE ICON IS NULL";
                BP.DA.DBAccess.RunSQL(sql);

                BP.WF.Template.NodeSheet nodeSheet = new BP.WF.Template.NodeSheet();
                nodeSheet.CheckPhysicsTable();
                // �����ֻ�Ӧ��. 2014-08-02.
                sql = "UPDATE WF_Node SET MPhone_WorkModel=0,MPhone_SrcModel=0,MPad_WorkModel=0,MPad_SrcModel=0 WHERE MPhone_WorkModel IS NULL";
                BP.DA.DBAccess.RunSQL(sql);
                #endregion �������ݸ���.

                #region ��ǩ
                sql = "DELETE FROM Sys_EnCfg WHERE No='BP.WF.Template.NodeSheet'";
                BP.DA.DBAccess.RunSQL(sql);
                sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Template.NodeSheet','";
                sql += "@NodeID=��������";
                sql += "@FormType=��";
                sql += "@FWCSta=������,������sdk����������ccform�ϵ���������������.";
                sql += "@SendLab=��ťȨ��,���ƹ����ڵ�ɲ�����ť.";
                sql += "@RunModel=����ģʽ,�ֺ���,��������";
                sql += "@AutoJumpRole0=��ת,�Զ���ת���������ýڵ�ʱ��������Զ���ִ����һ��.";
                sql += "@MPhone_WorkModel=�ƶ�,���ֻ�ƽ�������ص�Ӧ������.";
                sql += "@TSpanDay=����,ʱЧ����,��������.";
                //  sql += "@MsgCtrl=��Ϣ,������Ϣ��Ϣ.";
                sql += "@OfficeOpenLab=���İ�ť,ֻ�е��ýڵ��ǹ�������ʱ����Ч";
                sql += "')";
                BP.DA.DBAccess.RunSQL(sql);

                sql = "DELETE FROM Sys_EnCfg WHERE No='BP.WF.Template.FlowSheet'";
                BP.DA.DBAccess.RunSQL(sql);
                sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Template.FlowSheet','";
                sql += "@No=��������";
                sql += "@FlowRunWay=������ʽ,���ù�����������Զ����𣬸�ѡ��Ҫ�����̷���һ��������Ч.";
                sql += "@StartLimitRole=�������ƹ���";
                sql += "@StartGuideWay=����ǰ�õ���";
                sql += "@CFlowWay=��������";
                sql += "@DTSWay=����������ҵ������ͬ��";
                sql += "@PStarter=�켣�鿴Ȩ��";
                sql += "')";
                BP.DA.DBAccess.RunSQL(sql);

                sql = "DELETE FROM Sys_EnCfg WHERE No='BP.Sys.MapDataExt'";
                BP.DA.DBAccess.RunSQL(sql);
                sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.Sys.MapDataExt','";
                sql += "@No=��������";
                sql += "@Designer=�������Ϣ";
                sql += "')";
                BP.DA.DBAccess.RunSQL(sql);

                #endregion

                #region �ѽڵ��toolbarExcel, word ��Ϣ����mapdata
                BP.WF.Template.NodeSheets nss = new Template.NodeSheets();
                nss.RetrieveAll();
                foreach (BP.WF.Template.NodeSheet ns in nss)
                {
                    ToolbarExcel te = new ToolbarExcel();
                    te.No = "ND" + ns.NodeID;
                    te.RetrieveFromDBSources();

                    //te.Copy(ns);
                    //te.Update();
                }
                #endregion

                #region ����SelectAccper
                Direction dir = new Direction();
                dir.CheckPhysicsTable();

                SelectAccper selectAccper = new SelectAccper();
                selectAccper.CheckPhysicsTable();
                #endregion

                #region ����wf-generworkerlist 2014-05-09
                GenerWorkerList gwl = new GenerWorkerList();
                gwl.CheckPhysicsTable();
                #endregion ����wf-generworkerlist 2014-05-09

                #region  ���� NodeToolbar
                FrmField ff = new FrmField();
                ff.CheckPhysicsTable();

                MapAttr attr = new MapAttr();
                attr.CheckPhysicsTable();

                NodeToolbar bar = new NodeToolbar();
                bar.CheckPhysicsTable();

                BP.WF.Template.FlowFormTree tree = new BP.WF.Template.FlowFormTree();
                tree.CheckPhysicsTable();

                FrmNode nff = new FrmNode();
                nff.CheckPhysicsTable();

                SysForm ssf = new SysForm();
                ssf.CheckPhysicsTable();

                SysFormTree ssft = new SysFormTree();
                ssft.CheckPhysicsTable();

                BP.Sys.FrmAttachment ath = new FrmAttachment();
                ath.CheckPhysicsTable();

                BP.Sys.FrmField ffs = new FrmField();
                ffs.CheckPhysicsTable();
                #endregion

                #region ִ��sql��
                BP.DA.DBAccess.RunSQL("delete  from Sys_Enum WHERE EnumKey in ('BillFileType','EventDoType','FormType','BatchRole','StartGuideWay','NodeFormType')");
                DBAccess.RunSQL("UPDATE Sys_FrmSln SET FK_Flow =(SELECT FK_FLOW FROM WF_Node WHERE NODEID=Sys_FrmSln.FK_Node) WHERE FK_Flow IS NULL");
                try
                {
                    DBAccess.RunSQL("UPDATE WF_Flow SET StartGuidePara1=StartGuidePara WHERE  " + BP.Sys.SystemConfig.AppCenterDBLengthStr + "(StartGuidePara) >=1 ");
                }
                catch
                {
                }

                try
                {
                    DBAccess.RunSQL("UPDATE WF_FrmNode SET MyPK=FK_Frm+'_'+convert(varchar,FK_Node )+'_'+FK_Flow");
                }
                catch
                {
                }
                #endregion

                #region ����Ҫ��������
                //����
                BP.Port.Dept d = new BP.Port.Dept();
                d.CheckPhysicsTable();

                FrmWorkCheck fwc = new FrmWorkCheck();
                fwc.CheckPhysicsTable();

                BP.WF.GenerWorkFlow gwf = new BP.WF.GenerWorkFlow();
                gwf.CheckPhysicsTable();

                Flow myfl = new Flow();
                myfl.CheckPhysicsTable();

                Node nd = new Node();
                nd.CheckPhysicsTable();
                //Sys_SFDBSrc
                SFDBSrc sfDBSrc = new SFDBSrc();
                sfDBSrc.CheckPhysicsTable();
                #endregion ����Ҫ��������

                #region ִ�и���.wf_node
                sql = "UPDATE WF_Node SET FWCType=0 WHERE FWCType IS NULL";
                sql += "@UPDATE WF_Node SET FWC_X=0 WHERE FWC_X IS NULL";
                sql += "@UPDATE WF_Node SET FWC_Y=0 WHERE FWC_Y IS NULL";
                sql += "@UPDATE WF_Node SET FWC_W=0 WHERE FWC_W IS NULL";
                sql += "@UPDATE WF_Node SET FWC_H=0 WHERE FWC_H IS NULL";
                BP.DA.DBAccess.RunSQLs(sql);
                #endregion ִ�и���.

                #region ִ�б�����ơ�
                Flows fls = new Flows();
                fls.RetrieveAll();
                foreach (Flow fl in fls)
                {
                    try
                    {
                        MapRpts rpts = new MapRpts(fl.No);
                    }
                    catch
                    {
                        fl.DoCheck();
                    }
                }
                #endregion

                #region ����վ����Ϣ�� 2013-10-20
                BP.WF.SMS sms = new SMS();
                sms.CheckPhysicsTable();
                #endregion

                #region ���±��ı߽�.2014-10-18
                MapDatas mds = new MapDatas();
                mds.RetrieveAll();

                foreach (MapData md in mds)
                    md.ResetMaxMinXY(); //���±߽�.
                #endregion ���±��ı߽�.

                #region ��������view WF_EmpWorks,  2013-08-06.
                try
                {
                    BP.DA.DBAccess.RunSQL("DROP VIEW WF_EmpWorks");
                    BP.DA.DBAccess.RunSQL("DROP VIEW V_FlowStarter");
                    BP.DA.DBAccess.RunSQL("DROP VIEW V_FlowStarterBPM");
                }
                catch
                {
                }

                try
                {
                    BP.DA.DBAccess.RunSQL("DROP VIEW WF_NodeExt");
                }
                catch
                {
                }

                string sqlscript = "";
                //ִ�б����sql.
                if (BP.Sys.SystemConfig.AppCenterDBType == DBType.Oracle)
                    sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\WF\\Data\\Install\\SQLScript\\InitCCFlowData_Ora.sql";
                else
                    sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\WF\\Data\\Install\\SQLScript\\InitCCFlowData.sql";

                BP.DA.DBAccess.RunSQLScript(sqlscript);
                #endregion

                #region ����Img
                FrmImg img = new FrmImg();
                img.CheckPhysicsTable();
                #endregion

                #region �޸� mapattr UIHeight, UIWidth ���ʹ���.
                switch (BP.Sys.SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle:
                        msg = "@Sys_MapAttr �޸��ֶ�";
                        break;
                    case DBType.MSSQL:
                        msg = "@�޸�sql server�ؼ��߶ȺͿ���ֶΡ�";
                        DBAccess.RunSQL("ALTER TABLE Sys_MapAttr ALTER COLUMN UIWidth float");
                        DBAccess.RunSQL("ALTER TABLE Sys_MapAttr ALTER COLUMN UIHeight float");
                        break;
                    default:
                        break;
                }
                #endregion

                #region �������ôʻ�
                switch (BP.Sys.SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle:
                        int i = DBAccess.RunSQLReturnCOUNT("SELECT * FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'SYS_DEFVAL' AND COLUMN_NAME = 'PARENTNO'");
                        if (i == 0)
                        {
                            DBAccess.RunSQL("drop table Sys_DefVal");
                            DefVal dv = new DefVal();
                            dv.CheckPhysicsTable();
                        }
                        msg = "@Sys_DefVal �޸��ֶ�";
                        break;
                    case DBType.MSSQL:
                        msg = "@�޸� Sys_DefVal��";
                        i = DBAccess.RunSQLReturnCOUNT("SELECT * FROM SYSCOLUMNS WHERE ID=OBJECT_ID('Sys_DefVal') AND NAME='ParentNo'");
                        if (i == 0)
                        {
                            DBAccess.RunSQL("drop table Sys_DefVal");
                            DefVal dv = new DefVal();
                            dv.CheckPhysicsTable();
                        }
                        break;
                    default:
                        break;
                }
                #endregion

                #region ��½���´���
                msg = "@��½ʱ����";
                DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey IN ('DeliveryWay','RunModel','OutTimeDeal','FlowAppType')");
                try
                {
                    DBAccess.RunSQL("UPDATE Port_Station SET StaGrade=FK_StationType WHERE StaGrade IS null ");
                }
                catch
                {

                }
                #endregion ��½���´���

                #region ��������
                // ���ȼ���Ƿ�������.
                sql = "SELECT * FROM Sys_FormTree WHERE No = '0'";
                DataTable formTree_dt = DBAccess.RunSQLReturnTable(sql);
                if (formTree_dt.Rows.Count == 0)
                {
                    /*û��������.���Ӹ��ڵ�*/
                    SysFormTree formTree = new SysFormTree();
                    formTree.No = "0";
                    formTree.Name = "����";
                    formTree.ParentNo = "";
                    formTree.TreeNo = "0";
                    formTree.Idx = 0;
                    formTree.IsDir = true;

                    try
                    {
                        formTree.DirectInsert();
                    }
                    catch
                    {
                    }
                    //�������е�����ת�����
                    SysFormTrees formSorts = new SysFormTrees();
                    formSorts.RetrieveAll();

                    foreach (SysFormTree item in formSorts)
                    {
                        if (item.No == "0")
                            continue;

                        SysFormTree subFormTree = new SysFormTree();
                        subFormTree.No = item.No;
                        subFormTree.Name = item.Name;
                        subFormTree.ParentNo = "0";
                        subFormTree.Save();
                    }
                    //���ڱ������й���
                    sql = "UPDATE Sys_MapData SET FK_FormTree=FK_FrmSort WHERE FK_FrmSort <> '' AND FK_FrmSort is not null";
                    DBAccess.RunSQL(sql);
                }
                #endregion

                #region ִ��admin��½. 2012-12-25 �°汾.
                Emp emp = new Emp();
                emp.No = "admin";
                if (emp.RetrieveFromDBSources() == 1)
                {
                    BP.Web.WebUser.SignInOfGener(emp, true);
                }
                else
                {
                    emp.No = "admin";
                    emp.Name = "admin";
                    emp.FK_Dept = "01";
                    emp.Pass = "123";
                    emp.Insert();
                    BP.Web.WebUser.SignInOfGener(emp, true);
                    //throw new Exception("admin �û���ʧ����ע���Сд��");
                }
                #endregion ִ��admin��½.

                #region �޸� Sys_FrmImg ���ֶ� ImgAppType Tag0
                switch (BP.Sys.SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle:
                        int i = DBAccess.RunSQLReturnCOUNT("SELECT * FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'SYS_FRMIMG' AND COLUMN_NAME = 'TAG0'");
                        if (i == 0)
                        {
                            DBAccess.RunSQL("ALTER TABLE SYS_FRMIMG ADD (ImgAppType number,TAG0 nvarchar(500))");
                        }
                        msg = "@Sys_FrmImg �޸��ֶ�";
                        break;
                    case DBType.MSSQL:
                        msg = "@�޸� Sys_FrmImg��";
                        i = DBAccess.RunSQLReturnCOUNT("SELECT * FROM SYSCOLUMNS WHERE ID=OBJECT_ID('Sys_FrmImg') AND NAME='Tag0'");
                        if (i == 0)
                        {
                            DBAccess.RunSQL("ALTER TABLE Sys_FrmImg ADD ImgAppType int");
                            DBAccess.RunSQL("ALTER TABLE Sys_FrmImg ADD Tag0 nvarchar(500)");
                        }
                        break;
                    default:
                        break;
                }
                #endregion

                // �����°汾�ţ�Ȼ�󷵻�.
                sql = "UPDATE Sys_Serial SET IntVal=" + val + " WHERE CfgKey='Ver'";
                if (DBAccess.RunSQL(sql) == 0)
                {
                    sql = "INSERT INTO Sys_Serial (CfgKey,IntVal) VALUES('Ver'," + val + ") ";
                    DBAccess.RunSQL(sql);
                }
                // ���ذ汾��.
                return val; // +"\t\n�������:" + updataNote;
            }
            catch (Exception ex)
            {
                string err= "�������:" + ex.Message + "<hr>" + msg + "<br>��ϸ��Ϣ:@" + ex.StackTrace + "<br>@<a href='../DBInstall.aspx' >�����ﵽϵͳ�������档</a>";
                BP.DA.Log.DebugWriteError("ϵͳ��������:"+err);
                return "0";
                //return "����ʧ��,��ϸ��鿴��־.\\DataUser\\Log\\";
            }
        }
        /// <summary>
        /// CCFlowAppPath
        /// </summary>
        public static string CCFlowAppPath
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("CCFlowAppPath", "/");
            }
        }
        /// <summary>
        /// ��װ��
        /// </summary>
        public static void DoInstallDataBase(string lang, bool isInstallFlowDemo)
        {
            ArrayList al = null;
            string info = "BP.En.Entity";
            al = BP.En.ClassFactory.GetObjects(info);

            #region 1, ����or�޸���
            foreach (Object obj in al)
            {
                Entity en = null;
                en = obj as Entity;
                if (en == null)
                    continue;

                if (isInstallFlowDemo == false)
                {
                    /*�������װdemo.*/
                    string clsName = en.ToString();
                    if (clsName != null)
                    {
                        if (clsName.Contains("BP.CN")
                            || clsName.Contains("BP.Demo"))
                            continue;
                    }
                }
                if (Glo.OSModel == WF.OSModel.WorkFlow)
                {
                    /*�������װgpm �Ͱ�bp.gpm �����ռ��ų���. */
                    string clsName = en.ToString();
                    if (clsName != null)
                    {
                        if (clsName.Contains("BP.GMP"))
                            continue;
                    }
                }

                string table = null;
                try
                {
                    table = en.EnMap.PhysicsTable;
                    if (table == null)
                        continue;
                }
                catch
                {
                    continue;
                }

                switch (table)
                {
                    case "WF_EmpWorks":
                    case "WF_GenerEmpWorkDtls":
                    case "WF_GenerEmpWorks":
                    case "WF_NodeExt":
                    case "V_FlowData":
                        continue;
                    case "Sys_Enum":
                        en.CheckPhysicsTable();
                        break;
                    default:
                        en.CheckPhysicsTable();
                        break;
                }
                en.PKVal = "123";
                try
                {
                    en.RetrieveFromDBSources();
                }
                catch (Exception ex)
                {
                    Log.DebugWriteWarning(ex.Message);
                    en.CheckPhysicsTable();
                }
            }
            #endregion �޸�

            #region 2, ע��ö������ SQL
            // 2, ע��ö�����͡�
            BP.Sys.Xml.EnumInfoXmls xmls = new BP.Sys.Xml.EnumInfoXmls();
            xmls.RetrieveAll();
            foreach (BP.Sys.Xml.EnumInfoXml xml in xmls)
            {
                BP.Sys.SysEnums ses = new BP.Sys.SysEnums();
                ses.RegIt(xml.Key, xml.Vals);
            }
            #endregion ע��ö������

            #region 3, ִ�л����� sql 
            if (isInstallFlowDemo == false)
            {
                SysFormTree frmSort = new SysFormTree();
                frmSort.No = "01";
                frmSort.Name = "�����1";
                frmSort.ParentNo = "0";
                frmSort.Insert();
            }

            string sqlscript = "";
            if (Glo.OSModel == BP.WF.OSModel.WorkFlow)
            {
                /*�����WorkFlowģʽ*/
                sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\WF\\Data\\Install\\SQLScript\\Port_Inc_CH_WorkFlow.sql";
                BP.DA.DBAccess.RunSQLScript(sqlscript);
            }

            if (Glo.OSModel == BP.WF.OSModel.BPM)
            {
                /*�����BPMģʽ*/
                sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\GPM\\SQLScript\\Port_Inc_CH_BPM.sql";
                BP.DA.DBAccess.RunSQLScript(sqlscript);
            }
            #endregion �޸�

            #region 4, ������ͼ������.
            //ִ�б����sql.
            if (BP.Sys.SystemConfig.AppCenterDBType == DBType.Oracle)
                sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\WF\\Data\\Install\\SQLScript\\InitCCFlowData_Ora.sql";
            else
                sqlscript = BP.Sys.SystemConfig.CCFlowAppPath + "\\WF\\Data\\Install\\SQLScript\\InitCCFlowData.sql";

            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion ������ͼ������

            #region 5, ��ʼ������.
            if (isInstallFlowDemo)
            {
                sqlscript = SystemConfig.PathOfData + "\\Install\\SQLScript\\InitPublicData.sql";
                BP.DA.DBAccess.RunSQLScript(sqlscript);
            }
            else
            {
                FlowSort fs = new FlowSort();
                fs.No = "02";
                fs.ParentNo = "99";
                fs.Name = "������";
                fs.DirectInsert();
            }
            #endregion ��ʼ������

            #region 6, ������ʱ��wf���ݡ�
            if (isInstallFlowDemo)
            {
                BP.Port.Emps emps = new BP.Port.Emps();
                emps.RetrieveAllFromDBSource();
                int i = 0;
                foreach (BP.Port.Emp emp in emps)
                {
                    i++;
                    BP.WF.Port.WFEmp wfEmp = new BP.WF.Port.WFEmp();
                    wfEmp.Copy(emp);
                    wfEmp.No = emp.No;

                    if (wfEmp.Email.Length == 0)
                        wfEmp.Email = emp.No + "@ccflow.org";

                    if (wfEmp.Tel.Length == 0)
                        wfEmp.Tel = "82374939-6" + i.ToString().PadLeft(2, '0');

                    if (wfEmp.IsExits)
                        wfEmp.Update();
                    else
                        wfEmp.Insert();
                }

                // ���ɼ�������.
                int oid = 1000;
                foreach (BP.Port.Emp emp in emps)
                {
                    //for (int myIdx = 0; myIdx < 6; myIdx++)
                    //{
                    //    BP.WF.Demo.Resume re = new Demo.Resume();
                    //    re.NianYue = "200" + myIdx + "��01��";
                    //    re.FK_Emp = emp.No;
                    //    re.GongZuoDanWei = "��������-" + myIdx;
                    //    re.ZhengMingRen = "��" + myIdx;
                    //    re.BeiZhu = emp.Name + "ͬ־��������.";
                    //    oid++;
                    //    re.InsertAsOID(oid);
                    //}
                }
                // ��������·�����.
                string sql = "";
                DateTime dtNow = DateTime.Now;
                for (int num = 0; num < 12; num++)
                {
                    sql = "INSERT INTO Pub_NY (No,Name) VALUES ('" + dtNow.ToString("yyyy-MM") + "','" + dtNow.ToString("yyyy-MM") + "')";
                    dtNow = dtNow.AddMonths(1);
                }
            }
            #endregion ��ʼ������

            #region ִ�в����sql, ��������ֶγ��ȶ����ó�100.
            DBAccess.RunSQL("UPDATE Sys_MapAttr SET maxlen=100 WHERE LGType=2 AND MaxLen<100");
            DBAccess.RunSQL("UPDATE Sys_MapAttr SET maxlen=100 WHERE KeyOfEn='FK_Dept'");

            //Nodes nds = new Nodes();
            //nds.RetrieveAll();
            //foreach (Node nd in nds)
            //    nd.HisWork.CheckPhysicsTable();

            #endregion ִ�в����sql, ��������ֶγ��ȶ����ó�100.

            // ɾ���հ׵��ֶη���.
            BP.WF.DTS.DeleteBlankGroupField dts = new DTS.DeleteBlankGroupField();
            dts.Do();
        }
        /// <summary>
        /// ��װCCIM
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="yunXingHuanjing"></param>
        /// <param name="isDemo"></param>
        public static void DoInstallCCIM()
        {
            string sqlscript = SystemConfig.PathOfData + "Install\\SQLScript\\CCIM.sql";
            BP.DA.DBAccess.RunSQLScriptGo(sqlscript);
        }
        public static void KillProcess(string processName) //ɱ�����̵ķ���
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                string name = pro.ProcessName + ".exe";
                if (name.ToLower() == processName.ToLower())
                    pro.Kill();
            }
        }
        /// <summary>
        /// �����µı��
        /// </summary>
        /// <param name="rptKey"></param>
        /// <returns></returns>
        public static string GenerFlowNo(string rptKey)
        {
            rptKey = rptKey.Replace("ND", "");
            rptKey = rptKey.Replace("Rpt", "");
            switch (rptKey.Length)
            {
                case 0:
                    return "001";
                case 1:
                    return "00" + rptKey;
                case 2:
                    return "0" + rptKey;
                case 3:
                    return rptKey;
                default:
                    return "001";
            }
            return rptKey;
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsShowFlowNum
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowFlowNum"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// ����word�ĵ�.
        /// </summary>
        /// <param name="wk"></param>
        public static void GenerWord(object filename, Work wk)
        {
            BP.WF.Glo.KillProcess("WINWORD.EXE");
            string enName = wk.EnMap.PhysicsTable;
            try
            {
                RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools\Text Converters\Import\",
                    true);
                delKey.DeleteValue("MSWord6.wpc");
                delKey.Close();
            }
            catch
            {
            }

            GroupField currGF = new GroupField();
            MapAttrs mattrs = new MapAttrs(enName);
            GroupFields gfs = new GroupFields(enName);
            MapDtls dtls = new MapDtls(enName);
            foreach (MapDtl dtl in dtls)
                dtl.IsUse = false;

            // ���������Ԫ���������
            int rowNum = 0;
            foreach (GroupField gf in gfs)
            {
                rowNum++;
                bool isLeft = true;
                foreach (MapAttr attr in mattrs)
                {
                    if (attr.UIVisible == false)
                        continue;

                    if (attr.GroupID != gf.OID)
                        continue;

                    if (attr.UIIsLine)
                    {
                        rowNum++;
                        isLeft = true;
                        continue;
                    }

                    if (isLeft == false)
                        rowNum++;
                    isLeft = !isLeft;
                }
            }

            rowNum = rowNum + 2 + dtls.Count;

            // ����Word�ĵ�
            string CheckedInfo = "";
            string message = "";
            Object Nothing = System.Reflection.Missing.Value;

            #region û�ô���
            //  object filename = fileName;

            //Word.Application WordApp = new Word.ApplicationClass();
            //Word.Document WordDoc = WordApp.Documents.Add(ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing);
            //try
            //{
            //    WordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
            //    WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;

            //    #region ����ҳü
            //    // ���ҳü ����ͼƬ
            //    string pict = SystemConfig.PathOfDataUser + "log.jpg"; // ͼƬ����·��
            //    if (System.IO.File.Exists(pict))
            //    {
            //        System.Drawing.Image img = System.Drawing.Image.FromFile(pict);
            //        object LinkToFile = false;
            //        object SaveWithDocument = true;
            //        object Anchor = WordDoc.Application.Selection.Range;
            //        WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pict, ref  LinkToFile,
            //            ref  SaveWithDocument, ref  Anchor);
            //        //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // ͼƬ���
            //        //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // ͼƬ�߶�
            //    }
            //    WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[�۳�ҵ�����̹���ϵͳ http://ccFlow.org]");
            //    WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; // �����Ҷ���
            //    WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument; // ����ҳü����
            //    WordApp.Selection.ParagraphFormat.LineSpacing = 15f; // �����ĵ����м��
            //    #endregion

            //    // �ƶ����㲢����
            //    object count = 14;
            //    object WdLine = Word.WdUnits.wdLine; // ��һ��;
            //    WordApp.Selection.MoveDown(ref  WdLine, ref  count, ref  Nothing); // �ƶ�����
            //    WordApp.Selection.TypeParagraph(); // �������

            //    // �ĵ��д������
            //    Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, rowNum, 4, ref  Nothing, ref  Nothing);

            //    // ���ñ����ʽ
            //    newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
            //    newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            //    newTable.Columns[1].Width = 100f;
            //    newTable.Columns[2].Width = 100f;
            //    newTable.Columns[3].Width = 100f;
            //    newTable.Columns[4].Width = 100f;

            //    // ���������
            //    newTable.Cell(1, 1).Range.Text = wk.EnDesc;
            //    newTable.Cell(1, 1).Range.Bold = 2; // ���õ�Ԫ��������Ϊ����

            //    // �ϲ���Ԫ��
            //    newTable.Cell(1, 1).Merge(newTable.Cell(1, 4));
            //    WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // ��ֱ����
            //    WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // ˮƽ����

            //    int groupIdx = 1;
            //    foreach (GroupField gf in gfs)
            //    {
            //        groupIdx++;
            //        // ���������
            //        newTable.Cell(groupIdx, 1).Range.Text = gf.Lab;
            //        newTable.Cell(groupIdx, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue; // ���õ�Ԫ����������ɫ
            //        newTable.Cell(groupIdx, 1).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;
            //        // �ϲ���Ԫ��
            //        newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
            //        WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            //        groupIdx++;

            //        bool isLeft = true;
            //        bool isColumns2 = false;
            //        int currColumnIndex = 0;
            //        foreach (MapAttr attr in mattrs)
            //        {
            //            if (attr.UIVisible == false)
            //                continue;

            //            if (attr.GroupID != gf.OID)
            //                continue;

            //            if (newTable.Rows.Count < groupIdx)
            //                continue;

            //            #region ���Ӵӱ�
            //            foreach (MapDtl dtl in dtls)
            //            {
            //                if (dtl.IsUse)
            //                    continue;

            //                if (dtl.RowIdx != groupIdx - 3)
            //                    continue;

            //                if (gf.OID != dtl.GroupID)
            //                    continue;

            //                GEDtls dtlsDB = new GEDtls(dtl.No);
            //                QueryObject qo = new QueryObject(dtlsDB);
            //                switch (dtl.DtlOpenType)
            //                {
            //                    case DtlOpenType.ForEmp:
            //                        qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
            //                        break;
            //                    case DtlOpenType.ForWorkID:
            //                        qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
            //                        break;
            //                    case DtlOpenType.ForFID:
            //                        qo.AddWhere(GEDtlAttr.FID, wk.OID);
            //                        break;
            //                }
            //                qo.DoQuery();

            //                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
            //                newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));

            //                Attrs dtlAttrs = dtl.GenerMap().Attrs;
            //                int colNum = 0;
            //                foreach (Attr attrDtl in dtlAttrs)
            //                {
            //                    if (attrDtl.UIVisible == false)
            //                        continue;
            //                    colNum++;
            //                }

            //                newTable.Cell(groupIdx, 1).Select();
            //                WordApp.Selection.Delete(ref Nothing, ref Nothing);
            //                Word.Table newTableDtl = WordDoc.Tables.Add(WordApp.Selection.Range, dtlsDB.Count + 1, colNum,
            //                    ref Nothing, ref Nothing);

            //                newTableDtl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            //                newTableDtl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            //                int colIdx = 1;
            //                foreach (Attr attrDtl in dtlAttrs)
            //                {
            //                    if (attrDtl.UIVisible == false)
            //                        continue;
            //                    newTableDtl.Cell(1, colIdx).Range.Text = attrDtl.Desc;
            //                    colIdx++;
            //                }

            //                int idxRow = 1;
            //                foreach (GEDtl item in dtlsDB)
            //                {
            //                    idxRow++;
            //                    int columIdx = 0;
            //                    foreach (Attr attrDtl in dtlAttrs)
            //                    {
            //                        if (attrDtl.UIVisible == false)
            //                            continue;
            //                        columIdx++;

            //                        if (attrDtl.IsFKorEnum)
            //                            newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValRefTextByKey(attrDtl.Key);
            //                        else
            //                        {
            //                            if (attrDtl.MyDataType == DataType.AppMoney)
            //                                newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValMoneyByKey(attrDtl.Key).ToString("0.00");
            //                            else
            //                                newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValStrByKey(attrDtl.Key);

            //                            if (attrDtl.IsNum)
            //                                newTableDtl.Cell(idxRow, columIdx).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            //                        }
            //                    }
            //                }

            //                groupIdx++;
            //                isLeft = true;
            //            }
            //            #endregion ���Ӵӱ�

            //            if (attr.UIIsLine)
            //            {
            //                currColumnIndex = 0;
            //                isLeft = true;
            //                if (attr.IsBigDoc)
            //                {
            //                    newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
            //                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
            //                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
            //                }
            //                else
            //                {
            //                    newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 4));
            //                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
            //                    newTable.Cell(groupIdx, 2).Range.Text = wk.GetValStrByKey(attr.KeyOfEn);
            //                }
            //                groupIdx++;
            //                continue;
            //            }
            //            else
            //            {
            //                if (attr.IsBigDoc)
            //                {
            //                    if (currColumnIndex == 2)
            //                    {
            //                        currColumnIndex = 0;
            //                    }

            //                    newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
            //                    if (currColumnIndex == 0)
            //                    {
            //                        newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 2));
            //                        newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
            //                        currColumnIndex = 3;
            //                        continue;
            //                    }
            //                    else if (currColumnIndex == 3)
            //                    {
            //                        newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 3));
            //                        newTable.Cell(groupIdx, 2).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
            //                        currColumnIndex = 0;
            //                        groupIdx++;
            //                        continue;
            //                    }
            //                    else
            //                    {
            //                        continue;
            //                    }
            //                }
            //                else
            //                {
            //                    string s = "";
            //                    if (attr.LGType == FieldTypeS.Normal)
            //                    {
            //                        if (attr.MyDataType == DataType.AppMoney)
            //                            s = wk.GetValDecimalByKey(attr.KeyOfEn).ToString("0.00");
            //                        else
            //                            s = wk.GetValStrByKey(attr.KeyOfEn);
            //                    }
            //                    else
            //                    {
            //                        s = wk.GetValRefTextByKey(attr.KeyOfEn);
            //                    }

            //                    switch (currColumnIndex)
            //                    {
            //                        case 0:
            //                            newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
            //                            if (attr.IsSigan)
            //                            {
            //                                string path = BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
            //                                if (System.IO.File.Exists(path))
            //                                {
            //                                    System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            //                                    object LinkToFile = false;
            //                                    object SaveWithDocument = true;
            //                                    //object Anchor = WordDoc.Application.Selection.Range;
            //                                    object Anchor = newTable.Cell(groupIdx, 2).Range;

            //                                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
            //                                        ref  SaveWithDocument, ref  Anchor);
            //                                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // ͼƬ���
            //                                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // ͼƬ�߶�
            //                                }
            //                                else
            //                                {
            //                                    newTable.Cell(groupIdx, 2).Range.Text = s;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                if (attr.IsNum)
            //                                {
            //                                    newTable.Cell(groupIdx, 2).Range.Text = s;
            //                                    newTable.Cell(groupIdx, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            //                                }
            //                                else
            //                                {
            //                                    newTable.Cell(groupIdx, 2).Range.Text = s;
            //                                }
            //                            }
            //                            currColumnIndex = 1;
            //                            continue;
            //                            break;
            //                        case 1:
            //                            newTable.Cell(groupIdx, 3).Range.Text = attr.Name;
            //                            if (attr.IsSigan)
            //                            {
            //                                string path = BP.Sys.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
            //                                if (System.IO.File.Exists(path))
            //                                {
            //                                    System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            //                                    object LinkToFile = false;
            //                                    object SaveWithDocument = true;
            //                                    object Anchor = newTable.Cell(groupIdx, 4).Range;
            //                                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
            //                                        ref  SaveWithDocument, ref  Anchor);
            //                                }
            //                                else
            //                                {
            //                                    newTable.Cell(groupIdx, 4).Range.Text = s;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                if (attr.IsNum)
            //                                {
            //                                    newTable.Cell(groupIdx, 4).Range.Text = s;
            //                                    newTable.Cell(groupIdx, 4).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            //                                }
            //                                else
            //                                {
            //                                    newTable.Cell(groupIdx, 4).Range.Text = s;
            //                                }
            //                            }
            //                            currColumnIndex = 0;
            //                            groupIdx++;
            //                            continue;
            //                            break;
            //                        default:
            //                            break;
            //                    }
            //                }
            //            }
            //        }
            //    }  //����ѭ��

            //    #region ���ҳ��
            //    WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter;
            //    WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("ģ����ccflow�Զ����ɣ��Ͻ�ת�ء������̵���ϸ��������� http://doc.ccFlow.org�� �������̹���ϵͳ���µ�: 0531-82374939  ");
            //    WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            //    #endregion

            //    // �ļ�����
            //    WordDoc.SaveAs(ref  filename, ref  Nothing, ref  Nothing, ref  Nothing,
            //        ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
            //        ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
            //        ref  Nothing, ref  Nothing, ref  Nothing);

            //    WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
            //    WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            //    try
            //    {
            //        string docFile = filename.ToString();
            //        string pdfFile = docFile.Replace(".doc", ".pdf");
            //        Glo.Rtf2PDF(docFile, pdfFile);
            //    }
            //    catch (Exception ex)
            //    {
            //        BP.DA.Log.DebugWriteInfo("@����pdfʧ��." + ex.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //    // WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            //    WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
            //    WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            //}
            #endregion
        }
        #endregion ִ�а�װ.

        #region ȫ�ֵķ�������
        public static List<string> FlowFields
        {
            get
            {
                return typeof(GERptAttr).GetFields().Select(o => o.Name).ToList();
            }
        }
        /// <summary>
        /// �������ִ����ͣ��뷢����
        /// </summary>
        /// <param name="note"></param>
        /// <param name="emps"></param>
        public static void DealNote(string note, BP.Port.Emps emps)
        {
            note = "���ۺϴ���֪�������ʾ������ޱ����������ʾ��";
            note = note.Replace("��֪", "��֪@");

            note = note.Replace("��", "@");
            note = note.Replace("��", "@");
            note = note.Replace("��", "@");
            string[] strs = note.Split('@');

            string ccTo = "";
            string sendTo = "";
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                if (str.Contains("��֪") == true
                    || str.Contains("�Ķ�") == true)
                {
                    /*���͵�.*/
                    foreach (BP.Port.Emp emp in emps)
                    {
                        if (str.Contains(emp.No) == false)
                            continue;
                        ccTo += emp.No + ",";
                    }
                    continue;
                }

                if (str.Contains("�Ĵ�") == true
                  || str.Contains("�İ�") == true)
                {
                    /*�����͵�.*/
                    foreach (BP.Port.Emp emp in emps)
                    {
                        if (str.Contains(emp.No) == false)
                            continue;
                        sendTo += emp.No + ",";
                    }
                    continue;
                }
            }
        }



        #region �������¼�ʵ�����.
        private static Hashtable Htable_FlowFEE = null;
        /// <summary>
        /// ��ýڵ��¼�ʵ��
        /// </summary>
        /// <param name="enName">ʵ������</param>
        /// <returns>��ýڵ��¼�ʵ��,���û�оͷ���Ϊ��.</returns>
        public static FlowEventBase GetFlowEventEntityByEnName(string enName)
        {
            if (Htable_FlowFEE == null || Htable_FlowFEE.Count == 0)
            {
                Htable_FlowFEE = new Hashtable();
                ArrayList al = BP.En.ClassFactory.GetObjects("BP.WF.FlowEventBase");
                foreach (FlowEventBase en in al)
                {
                    Htable_FlowFEE.Add(en.ToString(), en);
                }
            }
            FlowEventBase myen = Htable_FlowFEE[enName] as FlowEventBase;
            if (myen == null)
                throw new Exception("@���������ƻ�ȡ�����¼�ʵ��ʵ�����ִ���:" + enName + ",û���ҵ������ʵ��.");
            return myen;
        }
        /// <summary>
        /// ��ýڵ��¼�ʵ����ݽڵ����.
        /// </summary>
        /// <param name="NodeMark">�ڵ����</param>
        /// <returns>����ʵ�壬����null</returns>
        public static FlowEventBase GetFlowEventEntityByFlowMark(string flowMark)
        {
            if (Htable_FlowFEE == null || Htable_FlowFEE.Count == 0)
            {
                Htable_FlowFEE = new Hashtable();
                ArrayList al = BP.En.ClassFactory.GetObjects("BP.WF.FlowEventBase");
                Htable_FlowFEE.Clear();
                foreach (FlowEventBase en in al)
                {
                    Htable_FlowFEE.Add(en.ToString(), en);
                }
            }

            foreach (string key in Htable_FlowFEE.Keys)
            {
                FlowEventBase fee = Htable_FlowFEE[key] as FlowEventBase;
                if (fee.FlowMark == flowMark)
                    return fee;
            }

            //for (int i = 0; i < Htable_FlowFEE.Count; i++)
            //{
            //    FlowEventBase fee = Htable_FlowFEE[i] as FlowEventBase;
            //}
            return null;
        }
        #endregion �������¼�ʵ�����.

        /// <summary>
        /// ִ�з��͹��������ҵ���߼�
        /// �������̷��ͺ��¼�����.
        /// �������ʧ�ܣ��ͻ��׳��쳣.
        /// </summary>
        public static void DealBuinessAfterSendWork(string fk_flow, Int64 workid,
            string doFunc, string WorkIDs, string cFlowNo, int cNodeID, string cEmp)
        {
            if (doFunc == "SetParentFlow")
            {
                /* �����Ҫ�����Ӹ�������Ϣ.
                 * Ӧ���ںϲ�����,����������̺ϲ�����,��������һ��������.
                 */
                string[] workids = WorkIDs.Split(',');
                string okworkids = ""; //�ɹ����ͺ��workids.
                GenerWorkFlow gwf = new GenerWorkFlow();
                foreach (string id in workids)
                {
                    if (string.IsNullOrEmpty(id))
                        continue;

                    // ������copy������,��������Ҳ���Եõ������̵����ݡ�
                    Int64 workidC = Int64.Parse(id);

                    //���õ�ǰ���̵�ID
                    BP.WF.Dev2Interface.SetParentInfo(cFlowNo, workidC, fk_flow, workid, cNodeID, cEmp);

                    // �ж��Ƿ����ִ�У�����ִ��ҲҪ������ȥ.
                    gwf.WorkID = workidC;
                    if (gwf.RetrieveFromDBSources() == 0)
                        continue;

                    // �Ƿ����ִ�У�
                    if (BP.WF.Dev2Interface.Flow_IsCanDoCurrentWork(gwf.FK_Flow, gwf.FK_Node, workidC, WebUser.No) == false)
                        continue;

                    //ִ�����·���.
                    try
                    {
                        BP.WF.Dev2Interface.Node_SendWork(cFlowNo, workidC);
                        okworkids += workidC;
                    }
                    catch (Exception ex)
                    {
                        #region �����һ������ʧ�ܣ��ͳ����������븸����.
                        //���Ȱ������̳�������.
                        BP.WF.Dev2Interface.Flow_DoUnSend(fk_flow, workid);

                        //���Ѿ����ͳɹ��������̳�������.
                        string[] myokwokid = okworkids.Split(',');
                        foreach (string okwokid in myokwokid)
                        {
                            if (string.IsNullOrEmpty(id))
                                continue;

                            // ������copy������,��������Ҳ���Եõ������̵����ݡ�
                            workidC = Int64.Parse(id);
                            BP.WF.Dev2Interface.Flow_DoUnSend(cFlowNo, workidC);
                        }
                        #endregion �����һ������ʧ�ܣ��ͳ����������븸����.
                        throw new Exception("@��ִ��������(" + gwf.Title + ")����ʱ�������´���:" + ex.Message);
                    }
                }
            }

        }
        #endregion ȫ�ֵķ�������

        #region web.config ����.
        /// <summary>
        /// �������õ���Ϣ��ͬ���Ӳ�ͬ�ı����ȡ��Ա��λ��Ϣ��
        /// </summary>
        public static string EmpStation
        {
            get
            {
                if (BP.WF.Glo.OSModel == WF.OSModel.BPM)
                    return "Port_DeptEmpStation";
                else
                    return "Port_EmpStation";
            }
        }
        public static string EmpDept
        {
            get
            {
                if (BP.WF.Glo.OSModel == WF.OSModel.BPM)
                    return "Port_DeptEmp";
                else
                    return "Port_EmpDept";
            }
        }

        /// <summary>
        /// �Ƿ�admin
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["adminers"];
                if (string.IsNullOrEmpty(s))
                    s = "admin,";
                return s.Contains(BP.Web.WebUser.No);
            }
        }
        /// <summary>
        /// ��ȡmapdata�ֶβ�ѯLike��
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <param name="colName">�б��</param>
        public static string MapDataLikeKey(string flowNo, string colName)
        {
            flowNo = int.Parse(flowNo).ToString();
            string len = BP.Sys.SystemConfig.AppCenterDBLengthStr;
            if (flowNo.Length == 1)
                return " " + colName + " LIKE 'ND" + flowNo + "%' AND " + len + "(" + colName + ")=5";
            if (flowNo.Length == 2)
                return " " + colName + " LIKE 'ND" + flowNo + "%' AND " + len + "(" + colName + ")=6";
            if (flowNo.Length == 3)
                return " " + colName + " LIKE 'ND" + flowNo + "%' AND " + len + "(" + colName + ")=7";

            return " " + colName + " LIKE 'ND" + flowNo + "%' AND " + len + "(" + colName + ")=8";
        }
        /// <summary>
        /// ����ʱ�䷢�ʹ�
        /// Ĭ�ϴ� 8 �㿪ʼ.
        /// </summary>
        public static int SMSSendTimeFromHour
        {
            get
            {
                try
                {
                    return int.Parse(BP.Sys.SystemConfig.AppSettings["SMSSendTimeFromHour"]);
                }
                catch
                {
                    return 8;
                }
            }
        }
        /// <summary>
        /// ����ʱ�䷢�͵�
        /// Ĭ�ϵ� 20 �����.
        /// </summary>
        public static int SMSSendTimeToHour
        {
            get
            {
                try
                {
                    return int.Parse(BP.Sys.SystemConfig.AppSettings["SMSSendTimeToHour"]);
                }
                catch
                {
                    return 8;
                }
            }
        }
        #endregion webconfig����.

        #region ���÷���
        private static string html = "";
        private static ArrayList htmlArr = new ArrayList();
        private static string backHtml = "";
        private static Int64 workid = 0;
        /// <summary>
        /// ģ������
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <param name="empNo">Ҫִ�е���Ա.</param>
        /// <returns>ִ����Ϣ.</returns>
        public static string Simulation_RunOne(string flowNo, string empNo, string paras)
        {
            backHtml = "";//��Ҫ���¸���ֵ
            Hashtable ht = null;
            if (string.IsNullOrEmpty(paras) == false)
            {
                AtPara ap = new AtPara(paras);
                ht = ap.HisHT;
            }

            Emp emp = new Emp(empNo);
            backHtml += " **** ��ʼʹ��:" + Glo.GenerUserImgSmallerHtml(emp.No, emp.Name) + "��¼ģ��ִ�й�������";
            BP.WF.Dev2Interface.Port_Login(empNo);

            workid = BP.WF.Dev2Interface.Node_CreateBlankWork(flowNo, ht, null, emp.No, null);
            SendReturnObjs objs = BP.WF.Dev2Interface.Node_SendWork(flowNo, workid, ht);
            backHtml += objs.ToMsgOfHtml().Replace("@", "<br>@");  //��¼��Ϣ.


            string[] accepters = objs.VarAcceptersID.Split(',');


            foreach (string acce in accepters)
            {
                if (string.IsNullOrEmpty(acce) == true)
                    continue;

                // ִ�з���.
                Simulation_Run_S1(flowNo, workid, acce, ht, empNo);
                break;
            }
            //return html;
            //return htmlArr;
            return backHtml;
        }
        private static bool isAdd = true;
        private static void Simulation_Run_S1(string flowNo, Int64 workid, string empNo, Hashtable ht, string beginEmp)
        {
            //htmlArr.Add(html);
            Emp emp = new Emp(empNo);
            //html = "";
            backHtml += "empNo" + beginEmp;
            backHtml += "<br> **** ��:" + Glo.GenerUserImgSmallerHtml(emp.No, emp.Name) + "ִ��ģ���¼. ";
            // �����¼.
            BP.WF.Dev2Interface.Port_Login(empNo);

            //ִ�з���.
            SendReturnObjs objs = BP.WF.Dev2Interface.Node_SendWork(flowNo, workid, ht);
            backHtml += "<br>" + objs.ToMsgOfHtml().Replace("@", "<br>@");

            if (objs.VarAcceptersID == null)
            {
                isAdd = false;
                backHtml += " <br> **** ���̽���,�鿴<a href='/WF/WFRpt.aspx?WorkID=" + workid + "&FK_Flow=" + flowNo + "' target=_blank >���̹켣</a> ====";
                //htmlArr.Add(html);
                //backHtml += "nextEmpNo";
                return;
            }

            if (string.IsNullOrEmpty(objs.VarAcceptersID))//�˴����Ϊ���жϣ��������淽����ִ�У��������
            {
                return;
            }
            string[] accepters = objs.VarAcceptersID.Split(',');

            foreach (string acce in accepters)
            {
                if (string.IsNullOrEmpty(acce) == true)
                    continue;

                //ִ�з���.
                Simulation_Run_S1(flowNo, workid, acce, ht, beginEmp);
                break; //�Ͳ�����ִ����.
            }
        }
        /// <summary>
        /// �Ƿ��ֻ�����?
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            if (SystemConfig.IsBSsystem == false)
                return false;

            string agent = (BP.Sys.Glo.Request.UserAgent + "").ToLower().Trim();
            if (agent == "" || agent.IndexOf("mozilla") != -1 || agent.IndexOf("opera") != -1)
                return false;
            return true;
        }
        /// <summary>
        /// �������ݱ��
        /// </summary>
        /// <param name="billFormat"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GenerBillNo(string billNo, Int64 workid, Entity en, string flowPTable)
        {
            if (string.IsNullOrEmpty(billNo))
                return "";
            if (billNo.Contains("@"))
                billNo = BP.WF.Glo.DealExp(billNo, en, null);

            /*�����Bill �й��� */
            billNo = billNo.Replace("{YYYY}", DateTime.Now.ToString("yyyy"));
            billNo = billNo.Replace("{yyyy}", DateTime.Now.ToString("yyyy"));

            billNo = billNo.Replace("{yy}", DateTime.Now.ToString("yy"));
            billNo = billNo.Replace("{YY}", DateTime.Now.ToString("YY"));

            billNo = billNo.Replace("{MM}", DateTime.Now.ToString("MM"));
            billNo = billNo.Replace("{DD}", DateTime.Now.ToString("DD"));
            billNo = billNo.Replace("{dd}", DateTime.Now.ToString("dd"));
            billNo = billNo.Replace("{HH}", DateTime.Now.ToString("HH"));
            billNo = billNo.Replace("{mm}", DateTime.Now.ToString("mm"));
            billNo = billNo.Replace("{LSH}", workid.ToString());
            billNo = billNo.Replace("{WorkID}", workid.ToString());
            billNo = billNo.Replace("{OID}", workid.ToString());

            if (billNo.Contains("@WebUser.DeptZi"))
            {
                string val = DBAccess.RunSQLReturnStringIsNull("SELECT Zi FROM Port_Dept where no='" + WebUser.FK_Dept + "'", "");
                billNo = billNo.Replace("@WebUser.DeptZi", val.ToString());
            }

            if (billNo.Contains("{ParentBillNo}"))
            {
                string pWorkID = DBAccess.RunSQLReturnStringIsNull("SELECT PWorkID FROM " + flowPTable + " WHERE OID=" + workid, "0");
                string parentBillNo = DBAccess.RunSQLReturnStringIsNull("SELECT BillNo FROM WF_GenerWorkFlow WHERE WorkID=" + pWorkID, "");
                billNo = billNo.Replace("{ParentBillNo}", parentBillNo);

                string sql = "";
                int num = 0;
                for (int i = 2; i < 7; i++)
                {
                    if (billNo.Contains("{LSH" + i + "}") == false)
                        continue;

                    sql = "SELECT COUNT(OID) FROM " + flowPTable + " WHERE PWorkID =" + pWorkID;
                    num = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    billNo = billNo + num.ToString().PadLeft(i, '0');
                    billNo = billNo.Replace("{LSH" + i + "}", "");
                    break;
                }
            }
            else
            {
                string sql = "";
                int num = 0;
                for (int i = 2; i < 7; i++)
                {
                    if (billNo.Contains("{LSH" + i + "}") == false)
                        continue;

                    billNo = billNo.Replace("{LSH" + i + "}", "");
                    sql = "SELECT COUNT(*) FROM " + flowPTable + " WHERE BillNo LIKE '" + billNo + "%'";
                    num = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0) + 1;
                    billNo = billNo + num.ToString().PadLeft(i, '0');
                }
            }
            return billNo;
        }
        /// <summary>
        /// ����track
        /// </summary>
        /// <param name="at">�¼�����</param>
        /// <param name="flowNo">���̱��</param>
        /// <param name="workID">����ID</param>
        /// <param name="fid">����ID</param>
        /// <param name="fromNodeID">�ӽڵ���</param>
        /// <param name="fromNodeName">�ӽڵ�����</param>
        /// <param name="fromEmpID">����ԱID</param>
        /// <param name="fromEmpName">����Ա����</param>
        /// <param name="toNodeID">���ڵ���</param>
        /// <param name="toNodeName">���ڵ�����</param>
        /// <param name="toEmpID">����ԱID</param>
        /// <param name="toEmpName">����Ա����</param>
        /// <param name="note">��Ϣ</param>
        /// <param name="tag">������@�ֿ�</param>
        public static void AddToTrack(ActionType at, string flowNo, Int64 workID, Int64 fid, int fromNodeID, string fromNodeName, string fromEmpID, string fromEmpName,
            int toNodeID, string toNodeName, string toEmpID, string toEmpName, string note, string tag)
        {
            if (toNodeID == 0)
            {
                toNodeID = fromNodeID;
                toNodeName = fromNodeName;
            }

            Track t = new Track();
            t.WorkID = workID;
            t.FID = fid;
            t.RDT = DataType.CurrentDataTimess;
            t.HisActionType = at;

            t.NDFrom = fromNodeID;
            t.NDFromT = fromNodeName;

            t.EmpFrom = fromEmpID;
            t.EmpFromT = fromEmpName;
            t.FK_Flow = flowNo;

            t.NDTo = toNodeID;
            t.NDToT = toNodeName;

            t.EmpTo = toEmpID;
            t.EmpToT = toEmpName;
            t.Msg = note;

            //����.
            if (tag != null)
                t.Tag = tag;

            try
            {
                t.Insert();
            }
            catch
            {
                t.CheckPhysicsTable();
                t.Insert();
            }
        }
        /// <summary>
        /// ������ʽ�Ƿ�ͨ��(�����Ƿ���ȷ.)
        /// </summary>
        /// <param name="exp">���ʽ</param>
        /// <param name="en">ʵ��</param>
        /// <returns>true/false</returns>
        public static bool ExeExp(string exp, Entity en)
        {
            exp = exp.Replace("@WebUser.No", WebUser.No);
            exp = exp.Replace("@WebUser.Name", WebUser.Name);
            exp = exp.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
            exp = exp.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);


            string[] strs = exp.Split(' ');
            bool isPass = false;

            string key = strs[0].Trim();
            string oper = strs[1].Trim();
            string val = strs[2].Trim();
            val = val.Replace("'", "");
            val = val.Replace("%", "");
            val = val.Replace("~", "");
            BP.En.Row row = en.Row;
            foreach (string item in row.Keys)
            {
                if (key != item.Trim())
                    continue;

                string valPara = row[key].ToString();
                if (oper == "=")
                {
                    if (valPara == val)
                        return true;
                }

                if (oper.ToUpper() == "LIKE")
                {
                    if (valPara.Contains(val))
                        return true;
                }

                if (oper == ">")
                {
                    if (float.Parse(valPara) > float.Parse(val))
                        return true;
                }
                if (oper == ">=")
                {
                    if (float.Parse(valPara) >= float.Parse(val))
                        return true;
                }
                if (oper == "<")
                {
                    if (float.Parse(valPara) < float.Parse(val))
                        return true;
                }
                if (oper == "<=")
                {
                    if (float.Parse(valPara) <= float.Parse(val))
                        return true;
                }

                if (oper == "!=")
                {
                    if (float.Parse(valPara) != float.Parse(val))
                        return true;
                }

                throw new Exception("@������ʽ����:" + exp + " Key=" + key + " oper=" + oper + " Val=" + val);
            }

            return false;
        }
        /// <summary>
        /// ִ��PageLoadװ������
        /// </summary>
        /// <param name="item"></param>
        /// <param name="en"></param>
        /// <param name="mattrs"></param>
        /// <param name="dtls"></param>
        /// <returns></returns>
        public static Entity DealPageLoadFull(Entity en, MapExt item, MapAttrs mattrs, MapDtls dtls)
        {
            if (item == null)
                return en;

            DataTable dt = null;
            string sql = item.Tag;
            if (string.IsNullOrEmpty(sql) == false)
            {
                /* �������������sql  */
                sql = Glo.DealExp(sql, en, null);

                if (string.IsNullOrEmpty(sql) == false)
                {
                    if (sql.Contains("@"))
                        throw new Exception("���õ�sql�д��������û���滻�ı���:" + sql);
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (string.IsNullOrEmpty(en.GetValStringByKey(dc.ColumnName)) || en.GetValStringByKey(dc.ColumnName)=="0")
                                en.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(item.Tag1)
                || item.Tag1.Length < 15)
                return en;

            // ���ӱ�.
            foreach (MapDtl dtl in dtls)
            {
                string[] sqls = item.Tag1.Split('*');
                foreach (string mysql in sqls)
                {
                    if (string.IsNullOrEmpty(mysql))
                        continue;
                    if (mysql.Contains(dtl.No + "=") == false)
                        continue;
                    if (mysql.Equals(dtl.No + "=") == true)
                        continue;

                    #region ����sql.
                    sql = Glo.DealExp(mysql, en, null);
                    #endregion ����sql.

                    if (string.IsNullOrEmpty(sql))
                        continue;

                    if (sql.Contains("@"))
                        throw new Exception("���õ�sql�д��������û���滻�ı���:" + sql);

                    GEDtls gedtls = null;

                    try
                    {
                        gedtls = new GEDtls(dtl.No);
                        gedtls.Delete(GEDtlAttr.RefPK, en.PKVal);
                    }
                    catch (Exception ex)
                    {
                        (gedtls.GetNewEntity as GEDtl).CheckPhysicsTable();
                    }

                    dt =
                        DBAccess.RunSQLReturnTable(sql.StartsWith(dtl.No + "=")
                                                       ? sql.Substring((dtl.No + "=").Length)
                                                       : sql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        GEDtl gedtl = gedtls.GetNewEntity as GEDtl;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            gedtl.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());
                        }

                        gedtl.RefPK = en.PKVal.ToString();
                        gedtl.RDT = DataType.CurrentDataTime;
                        gedtl.Rec = WebUser.No;
                        gedtl.Insert();
                    }
                }
            }
            return en;
        }
        /// <summary>
        /// ������ʽ
        /// </summary>
        /// <param name="exp">���ʽ</param>
        /// <param name="en">����Դ</param>
        /// <param name="errInfo">����</param>
        /// <returns></returns>
        public static string DealExp(string exp, Entity en, string errInfo)
        {
            exp = exp.Replace("~", "'");

            //�����滻��; �ġ�
            exp = exp.Replace("@WebUser.No;", WebUser.No);
            exp = exp.Replace("@WebUser.Name;", WebUser.Name);
            exp = exp.Replace("@WebUser.FK_Dept;", WebUser.FK_Dept);
            exp = exp.Replace("@WebUser.FK_DeptName;", WebUser.FK_DeptName);

            // �滻û�� ; �� .
            exp = exp.Replace("@WebUser.No", WebUser.No);
            exp = exp.Replace("@WebUser.Name", WebUser.Name);
            exp = exp.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
            exp = exp.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

            if (exp.Contains("@") == false)
            {
                exp = exp.Replace("~", "'");
                return exp;
            }

            //���Ӷ��¹����֧��. @MyField; ��ʽ.
            foreach (Attr item in en.EnMap.Attrs)
            {
                if (exp.Contains("@" + item.Key + ";"))
                    exp = exp.Replace("@" + item.Key + ";", en.GetValStrByKey(item.Key));
            }
            if (exp.Contains("@") == false)
                return exp;

            #region �����������.
            Attrs attrs = en.EnMap.Attrs;
            string mystrs = "";
            foreach (Attr attr in attrs)
            {
                if (attr.MyDataType == DataType.AppString)
                    mystrs += "@" + attr.Key + ",";
                else
                    mystrs += "@" + attr.Key;
            }
            string[] strs = mystrs.Split('@');
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                DataRow dr = dt.NewRow();
                dr[0] = str;
                dt.Rows.Add(dr);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "No DESC";
            DataTable dtNew = dv.Table;
            #endregion  �����������.

            #region �滻����.
            foreach (DataRow dr in dtNew.Rows)
            {
                string key = dr[0].ToString();
                bool isStr = key.Contains(",");
                if (isStr == true)
                {
                    key = key.Replace(",", "");
                    exp = exp.Replace("@" + key, en.GetValStrByKey(key));
                }
                else
                {
                    exp = exp.Replace("@" + key, en.GetValStrByKey(key));
                }
            }

            // ����Para���滻.
            if (exp.Contains("@") && Glo.SendHTOfTemp != null)
            {
                foreach (string key in Glo.SendHTOfTemp.Keys)
                    exp = exp.Replace("@" + key, Glo.SendHTOfTemp[key].ToString());
            }

            if (exp.Contains("@") && SystemConfig.IsBSsystem == true)
            {
                /*�����bs*/
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.Keys)
                    exp = exp.Replace("@" + key, System.Web.HttpContext.Current.Request.QueryString[key]);
            }
            #endregion

            exp = exp.Replace("~", "'");
            //exp = exp.Replace("''", "'");
            //exp = exp.Replace("''", "'");
            //exp = exp.Replace("=' ", "=''");
            //exp = exp.Replace("= ' ", "=''");
            return exp;
        }
        /// <summary>
        /// ����MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GenerMD5(BP.WF.Work wk)
        {
            string s = null;
            foreach (Attr attr in wk.EnMap.Attrs)
            {
                switch (attr.Key)
                {
                    case WorkAttr.MD5:
                    case WorkAttr.RDT:
                    case WorkAttr.CDT:
                    case WorkAttr.Rec:
                    case StartWorkAttr.Title:
                    case StartWorkAttr.Emps:
                    case StartWorkAttr.FK_Dept:
                    case StartWorkAttr.PRI:
                    case StartWorkAttr.FID:
                        continue;
                    default:
                        break;
                }

                string obj = attr.DefaultVal as string;
                //if (obj == null)
                //    continue;
                if (obj != null && obj.Contains("@"))
                    continue;

                s += wk.GetValStrByKey(attr.Key);
            }
            s += "ccflow";
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
        }
        /// <summary>
        /// װ���������� 
        /// </summary>
        /// <param name="xlsFile"></param>
        public static string LoadFlowDataWithToSpecNode(string xlsFile)
        {
            DataTable dt = BP.DA.DBLoad.GetTableByExt(xlsFile);
            string err = "";
            string info = "";

            foreach (DataRow dr in dt.Rows)
            {
                string flowPK = dr["FlowPK"].ToString();
                string starter = dr["Starter"].ToString();
                string executer = dr["Executer"].ToString();
                int toNode = int.Parse(dr["ToNodeID"].ToString().Replace("ND", ""));
                Node nd = new Node();
                nd.NodeID = toNode;
                if (nd.RetrieveFromDBSources() == 0)
                {
                    err += "�ڵ�ID����:" + toNode;
                    continue;
                }
                string sql = "SELECT count(*) as Num FROM ND" + int.Parse(nd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);
                if (i == 1)
                    continue; // �������Ѿ������ˡ�

                #region ��������Ƿ�������
                BP.Port.Emp emp = new BP.Port.Emp();
                emp.No = executer;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@�˺�:" + starter + ",�����ڡ�";
                    continue;
                }
                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@�˺�:" + starter + ",û�в��š�";
                    continue;
                }

                emp.No = starter;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@�˺�:" + executer + ",�����ڡ�";
                    continue;
                }
                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@�˺�:" + executer + ",û�в��š�";
                    continue;
                }
                #endregion ��������Ƿ�������

                BP.Web.WebUser.SignInOfGener(emp);
                Flow fl = nd.HisFlow;
                Work wk = fl.NewWork();

                Attrs attrs = wk.EnMap.Attrs;
                //foreach (Attr attr in wk.EnMap.Attrs)
                //{
                //}

                foreach (DataColumn dc in dt.Columns)
                {
                    Attr attr = attrs.GetAttrByKey(dc.ColumnName.Trim());
                    if (attr == null)
                        continue;

                    string val = dr[dc.ColumnName].ToString().Trim();
                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                        case DataType.AppDate:
                        case DataType.AppDateTime:
                            wk.SetValByKey(attr.Key, val);
                            break;
                        case DataType.AppInt:
                        case DataType.AppBoolean:
                            wk.SetValByKey(attr.Key, int.Parse(val));
                            break;
                        case DataType.AppMoney:
                        case DataType.AppDouble:
                        case DataType.AppRate:
                        case DataType.AppFloat:
                            wk.SetValByKey(attr.Key, decimal.Parse(val));
                            break;
                        default:
                            wk.SetValByKey(attr.Key, val);
                            break;
                    }
                }

                wk.SetValByKey(WorkAttr.Rec, BP.Web.WebUser.No);
                wk.SetValByKey(StartWorkAttr.FK_Dept, BP.Web.WebUser.FK_Dept);
                wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wk.SetValByKey(WorkAttr.MyNum, 1);
                wk.Update();

                Node ndStart = nd.HisFlow.HisStartNode;
                WorkNode wn = new WorkNode(wk, ndStart);
                try
                {
                    info += "<hr>" + wn.NodeSend(nd, executer).ToMsgOfHtml();
                }
                catch (Exception ex)
                {
                    err += "<hr>" + ex.Message;
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal(true);
                    continue;
                }

                #region ���� ��һ���ڵ����ݡ�
                Work wkNext = nd.HisWork;
                wkNext.OID = wk.OID;
                wkNext.RetrieveFromDBSources();
                attrs = wkNext.EnMap.Attrs;
                foreach (DataColumn dc in dt.Columns)
                {
                    Attr attr = attrs.GetAttrByKey(dc.ColumnName.Trim());
                    if (attr == null)
                        continue;

                    string val = dr[dc.ColumnName].ToString().Trim();
                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                        case DataType.AppDate:
                        case DataType.AppDateTime:
                            wkNext.SetValByKey(attr.Key, val);
                            break;
                        case DataType.AppInt:
                        case DataType.AppBoolean:
                            wkNext.SetValByKey(attr.Key, int.Parse(val));
                            break;
                        case DataType.AppMoney:
                        case DataType.AppDouble:
                        case DataType.AppRate:
                        case DataType.AppFloat:
                            wkNext.SetValByKey(attr.Key, decimal.Parse(val));
                            break;
                        default:
                            wkNext.SetValByKey(attr.Key, val);
                            break;
                    }
                }

                wkNext.DirectUpdate();

                GERpt rtp = fl.HisGERpt;
                rtp.SetValByKey("OID", wkNext.OID);
                rtp.RetrieveFromDBSources();
                rtp.Copy(wkNext);
                rtp.DirectUpdate();

                #endregion ���� ��һ���ڵ����ݡ�
            }
            return info + err;
        }
        public static string LoadFlowDataWithToSpecEndNode(string xlsFile)
        {
            DataTable dt = BP.DA.DBLoad.GetTableByExt(xlsFile);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.WriteXml("C:\\�����.xml");

            string err = "";
            string info = "";
            int idx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string flowPK = dr["FlowPK"].ToString().Trim();
                if (string.IsNullOrEmpty(flowPK))
                    continue;

                string starter = dr["Starter"].ToString();
                string executer = dr["Executer"].ToString();
                int toNode = int.Parse(dr["ToNodeID"].ToString().Replace("ND", ""));
                Node ndOfEnd = new Node();
                ndOfEnd.NodeID = toNode;
                if (ndOfEnd.RetrieveFromDBSources() == 0)
                {
                    err += "�ڵ�ID����:" + toNode;
                    continue;
                }

                if (ndOfEnd.IsEndNode == false)
                {
                    err += "�ڵ�ID����:" + toNode + ", �ǽ����ڵ㡣";
                    continue;
                }

                string sql = "SELECT count(*) as Num FROM ND" + int.Parse(ndOfEnd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);
                if (i == 1)
                    continue; // �������Ѿ������ˡ�

                #region ��������Ƿ�������
                //�����˷���
                BP.Port.Emp emp = new BP.Port.Emp();
                emp.No = executer;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@�˺�:" + starter + ",�����ڡ�";
                    continue;
                }

                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@�˺�:" + starter + ",û�����ò��š�";
                    continue;
                }

                emp = new BP.Port.Emp();
                emp.No = starter;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@�˺�:" + starter + ",�����ڡ�";
                    continue;
                }
                else
                {
                    emp.RetrieveFromDBSources();
                    if (string.IsNullOrEmpty(emp.FK_Dept))
                    {
                        err += "@�˺�:" + starter + ",û�����ò��š�";
                        continue;
                    }
                }
                #endregion ��������Ƿ�������


                BP.Web.WebUser.SignInOfGener(emp);
                Flow fl = ndOfEnd.HisFlow;
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dt.Columns)
                    wk.SetValByKey(dc.ColumnName.Trim(), dr[dc.ColumnName].ToString().Trim());

                wk.SetValByKey(WorkAttr.Rec, BP.Web.WebUser.No);
                wk.SetValByKey(StartWorkAttr.FK_Dept, BP.Web.WebUser.FK_Dept);
                wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wk.SetValByKey(WorkAttr.MyNum, 1);
                wk.Update();

                Node ndStart = fl.HisStartNode;
                WorkNode wn = new WorkNode(wk, ndStart);
                try
                {
                    info += "<hr>" + wn.NodeSend(ndOfEnd, executer).ToMsgOfHtml();
                }
                catch (Exception ex)
                {
                    err += "<hr>��������:" + ex.Message;
                    DBAccess.RunSQL("DELETE FROM ND" + int.Parse(ndOfEnd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'");
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal(true);
                    continue;
                }

                //�����������
                emp = new BP.Port.Emp(executer);
                BP.Web.WebUser.SignInOfGener(emp);

                Work wkEnd = ndOfEnd.GetWork(wk.OID);
                foreach (DataColumn dc in dt.Columns)
                    wkEnd.SetValByKey(dc.ColumnName.Trim(), dr[dc.ColumnName].ToString().Trim());

                wkEnd.SetValByKey(WorkAttr.Rec, BP.Web.WebUser.No);
                wkEnd.SetValByKey(StartWorkAttr.FK_Dept, BP.Web.WebUser.FK_Dept);
                wkEnd.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wkEnd.SetValByKey(WorkAttr.MyNum, 1);
                wkEnd.Update();

                try
                {
                    WorkNode wnEnd = new WorkNode(wkEnd, ndOfEnd);
                    //  wnEnd.AfterNodeSave();
                    info += "<hr>" + wnEnd.NodeSend().ToMsgOfHtml();
                }
                catch (Exception ex)
                {
                    err += "<hr>��������(ϵͳֱ��ɾ����):" + ex.Message;
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal(true);
                    continue;
                }
            }
            return info + err;
        }
        /// <summary>
        /// �ж��Ƿ��½��ǰUserNo
        /// </summary>
        /// <param name="userNo"></param>
        public static void IsSingleUser(string userNo)
        {
            if (string.IsNullOrEmpty(WebUser.No) || WebUser.No != userNo)
            {
                if (!string.IsNullOrEmpty(userNo))
                {
                    BP.WF.Dev2Interface.Port_Login(userNo);
                }
            }
        }
        //public static void ResetFlowView()
        //{
        //    string sql = "DROP VIEW V_WF_Data ";
        //    try
        //    {
        //        BP.DA.DBAccess.RunSQL(sql);
        //    }
        //    catch
        //    {
        //    }

        //    Flows fls = new Flows();
        //    fls.RetrieveAll();
        //    sql = "CREATE VIEW V_WF_Data AS ";
        //    foreach (Flow fl in fls)
        //    {
        //        fl.CheckRpt();
        //        sql += "\t\n SELECT '" + fl.No + "' as FK_Flow, '" + fl.Name + "' AS FlowName, '" + fl.FK_FlowSort + "' as FK_FlowSort,CDT,Emps,FID,FK_Dept,FK_NY,";
        //        sql += "MyNum,OID,RDT,Rec,Title,WFState,FlowEmps,";
        //        sql += "FlowStarter,FlowStartRDT,FlowEnder,FlowEnderRDT,FlowDaySpan FROM ND" + int.Parse(fl.No) + "Rpt";
        //        sql += "\t\n  UNION";
        //    }
        //    sql = sql.Substring(0, sql.Length - 6);
        //    sql += "\t\n GO";
        //    BP.DA.DBAccess.RunSQL(sql);
        //}
        public static void Rtf2PDF(object pathOfRtf, object pathOfPDF)
        {
            //        Object Nothing = System.Reflection.Missing.Value;
            //        //����һ����ΪWordApp���������    
            //        Microsoft.Office.Interop.Word.Application wordApp =
            //new Microsoft.Office.Interop.Word.ApplicationClass();
            //        //����һ����ΪWordDoc���ĵ����󲢴�    
            //        Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref pathOfRtf, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
            // ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
            //ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            //        //���ñ���ĸ�ʽ    
            //        object filefarmat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            //        //����ΪPDF    
            //        doc.SaveAs(ref pathOfPDF, ref filefarmat, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
            //ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
            // ref Nothing, ref Nothing, ref Nothing);
            //        //�ر��ĵ�����    
            //        doc.Close(ref Nothing, ref Nothing, ref Nothing);
            //        //�Ƴ��齨    
            //        wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            //        GC.Collect();
        }
        #endregion ���÷���

        #region ����
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public static string SessionMsg
        {
            get
            {
                Paras p = new Paras();
                p.SQL = "SELECT Msg FROM WF_Emp where No=" + SystemConfig.AppCenterDBVarStr + "FK_Emp";
                p.AddFK_Emp();
                return DBAccess.RunSQLReturnString(p);

                //string SQL = "SELECT Msg FROM WF_Emp where No='"+BP.Web.WebUser.No+"'";
                //return DBAccess.RunSQLReturnString(SQL);
            }
            set
            {
                if (string.IsNullOrEmpty(value) == true)
                    return;

                Paras p = new Paras();
                p.SQL = "UPDATE WF_Emp SET Msg=" + SystemConfig.AppCenterDBVarStr + "v WHERE No=" + SystemConfig.AppCenterDBVarStr + "FK_Emp";
                p.Add("v", value);
                p.AddFK_Emp();

                int i = DBAccess.RunSQL(p);
                if (i == 0)
                {
                    /*���û�и��µ�.*/
                    BP.WF.Port.WFEmp emp = new Port.WFEmp();
                    emp.No = BP.Web.WebUser.No;
                    emp.Name = BP.Web.WebUser.Name;
                    emp.FK_Dept = BP.Web.WebUser.FK_Dept;
                    emp.Insert();
                    DBAccess.RunSQL(p);
                }

                //string SQL = "UPDATE WF_Emp SET Msg='" + value + "' WHERE No='" + BP.Web.WebUser.No + "'";
                //DBAccess.RunSQL(SQL);
            }
        }

        private static string _FromPageType = null;
        public static string FromPageType
        {
            get
            {
                _FromPageType = null;
                if (_FromPageType == null)
                {
                    try
                    {
                        string url = BP.Sys.Glo.Request.RawUrl;
                        int i = url.LastIndexOf("/") + 1;
                        int i2 = url.IndexOf(".aspx") - 6;

                        url = url.Substring(i);
                        url = url.Substring(0, url.IndexOf(".aspx"));
                        _FromPageType = url;
                        if (_FromPageType.Contains("SmallSingle"))
                            _FromPageType = "SmallSingle";
                        else if (_FromPageType.Contains("Small"))
                            _FromPageType = "Small";
                        else
                            _FromPageType = "";
                    }
                    catch (Exception ex)
                    {
                        _FromPageType = "";
                        //  throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                return _FromPageType;
            }
        }
        private static Hashtable _SendHTOfTemp = null;
        /// <summary>
        /// ��ʱ�ķ��ʹ������.
        /// </summary>
        public static Hashtable SendHTOfTemp
        {
            get
            {
                if (_SendHTOfTemp == null)
                    _SendHTOfTemp = new Hashtable();
                return _SendHTOfTemp[BP.Web.WebUser.No] as Hashtable;
            }
            set
            {
                if (_SendHTOfTemp == null)
                    _SendHTOfTemp = new Hashtable();
                _SendHTOfTemp[BP.Web.WebUser.No] = value;
            }
        }
        /// <summary>
        /// �������Լ���
        /// </summary>
        private static Attrs _AttrsOfRpt = null;
        /// <summary>
        /// �������Լ���
        /// </summary>
        public static Attrs AttrsOfRpt
        {
            get
            {
                if (_AttrsOfRpt == null)
                {
                    _AttrsOfRpt = new Attrs();
                    _AttrsOfRpt.AddTBInt(GERptAttr.OID, 0, "WorkID", true, true);
                    _AttrsOfRpt.AddTBInt(GERptAttr.FID, 0, "FlowID", false, false);

                    _AttrsOfRpt.AddTBString(GERptAttr.Title, null, "����", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStarter, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStartRDT, null, "����ʱ��", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.WFState, null, "״̬", true, false, 0, 10, 10);

                    //Attr attr = new Attr();
                    //attr.Desc = "����״̬";
                    //attr.Key = "WFState";
                    //attr.MyFieldType = FieldType.Enum;
                    //attr.UIBindKey = "WFState";
                    //attr.UITag = "@0=������@1=�Ѿ����";

                    _AttrsOfRpt.AddDDLSysEnum(GERptAttr.WFState, 0, "����״̬", true, true, GERptAttr.WFState);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEmps, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnder, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnderRDT, null, "����ʱ��", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBDecimal(GERptAttr.FlowEndNode, 0, "�����ڵ�", true, false);
                    _AttrsOfRpt.AddTBDecimal(GERptAttr.FlowDaySpan, 0, "���(��)", true, false);
                    //_AttrsOfRpt.AddTBString(GERptAttr.FK_NY, null, "�����·�", true, false, 0, 10, 10);
                }
                return _AttrsOfRpt;
            }
        }
        #endregion ����

        #region ��������.
        public static string GenerHelp(string helpId)
        {
            return "";
            switch (helpId)
            {
                case "Bill":
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../WF/Img/FileType/rm.gif' border=0/>����¼��</a>";
                case "FAppSet":
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../WF/Img/FileType/rm.gif' border=0/>����¼��</a>";
                default:
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../WF/Img/FileType/rm.gif' border=0/>����¼��</a>";
                    break;
            }
        }
        public static string NodeImagePath
        {
            get
            {
                return Glo.IntallPath + "\\Data\\Node\\";
            }
        }
        public static void ClearDBData()
        {
            string sql = "DELETE FROM WF_GenerWorkFlow WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "DELETE FROM WF_GenerWorkerlist WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public static string OEM_Flag = "CCS";
        public static string FlowFileBill
        {
            get { return Glo.IntallPath + "\\DataUser\\Bill\\"; }
        }
        private static string _IntallPath = null;
        public static string IntallPath
        {
            get
            {
                if (_IntallPath == null)
                {
                    if (SystemConfig.IsBSsystem == true)
                        _IntallPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                }

                if (_IntallPath == null)
                    throw new Exception("@û��ʵ����λ�� cs �µĸ�Ŀ¼.");

                return _IntallPath;
            }
            set
            {
                _IntallPath = value;
            }
        }
        private static string _ServerIP = null;
        public static string ServerIP
        {
            get
            {
                if (_ServerIP == null)
                {
                    string ip = "127.0.0.1";
                    System.Net.IPAddress[] addressList = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList;
                    if (addressList.Length > 1)
                        _ServerIP = addressList[1].ToString();
                    else
                        _ServerIP = addressList[0].ToString();
                }
                return _ServerIP;
            }
            set
            {
                _ServerIP = value;
            }
        }
        /// <summary>
        /// ���̿�������ť
        /// </summary>
        public static string FlowCtrlBtnPos
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["FlowCtrlBtnPos"] as string;
                if (s == null || s == "Top")
                    return "Top";
                return "Bottom";
            }
        }
        /// <summary>
        /// ȫ�ֵİ�ȫ��֤��
        /// </summary>
        public static string GloSID
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["GloSID"] as string;
                if (s == null || s == "")
                    s = "sdfq2erre-2342-234sdf23423-323";
                return s;
            }
        }
        /// <summary>
        /// �Ƿ����ü���û���״̬?
        /// ���������:��MyFlow.aspx��ÿ�ζ����鵱ǰ���û�״̬�Ƿ񱻽�
        /// �ã���������˾Ͳ���ִ���κβ����ˡ����ú󣬾���ζ��ÿ�ζ�Ҫ
        /// �������ݿ⡣
        /// </summary>
        public static bool IsEnableCheckUseSta
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["IsEnableCheckUseSta"] as string;
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// �Ƿ�������ʾ�ڵ�����
        /// </summary>
        public static bool IsEnableMyNodeName
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["IsEnableMyNodeName"] as string;
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// ���һ�µ�ǰ���û��Ƿ��Ծ���Чʹ�ã�
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsEnableWFEmp()
        {
            Paras ps = new Paras();
            ps.SQL = "SELECT UseSta FROM WF_Emp WHERE No=" + SystemConfig.AppCenterDBVarStr + "FK_Emp";
            ps.AddFK_Emp();
            string s = DBAccess.RunSQLReturnStringIsNull(ps, "1");
            if (s == "1" || s == null)
                return true;
            return false;
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string Language = "CH";
        public static bool IsQL
        {
            get
            {
                string s = BP.Sys.SystemConfig.AppSettings["IsQL"];
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// �Ƿ����ù�������أ�
        /// </summary>
        public static bool IsEnableTaskPool
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsEnableTaskPool", false);
            }
        }
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public static bool IsShowTitle
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsShowTitle", false);
            }
        }

        /// <summary>
        /// �û���Ϣ��ʾ��ʽ
        /// </summary>
        public static UserInfoShowModel UserInfoShowModel
        {
            get
            {
                return (UserInfoShowModel)BP.Sys.SystemConfig.GetValByKeyInt("UserInfoShowModel", 0);
            }
        }
        /// <summary>
        /// �����û�����ǩ��
        /// </summary>
        /// <returns></returns>
        public static string GenerUserSigantureHtml(string userNo, string userName)
        {
            return "<img src='" + CCFlowAppPath + "DataUser/Siganture/" + userNo + ".jpg' title='" + userName + "' border=0 onerror=\"src='" + CCFlowAppPath + "DataUser/UserICON/DefaultSmaller.png'\" />";
        }
        /// <summary>
        /// �����û�СͼƬ
        /// </summary>
        /// <returns></returns>
        public static string GenerUserImgSmallerHtml(string userNo, string userName)
        {
            return "<img src='" + CCFlowAppPath + "DataUser/UserICON/" + userNo + "Smaller.png' border=0 width='20px' height='20px' style='padding-right:5px;' align='middle' onerror=\"src='" + CCFlowAppPath + "DataUser/UserICON/DefaultSmaller.png'\" />" + userName;
        }
        /// <summary>
        /// �����û���ͼƬ
        /// </summary>
        /// <returns></returns>
        public static string GenerUserImgHtml(string userNo, string userName)
        {
            return "<img src='" + CCFlowAppPath + "DataUser/UserICON/" + userNo + ".png'  style='padding-right:5px;width:60px;height:80px;border:0px;text-align:middle' onerror=\"src='" + CCFlowAppPath + "DataUser/UserICON/Default.png'\" /><br>" + userName;
        }
        /// <summary>
        /// ���������SQL
        /// </summary>
        public static string UpdataMainDeptSQL
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("UpdataMainDeptSQL", "UPDATE Port_Emp SET FK_Dept=" + BP.Sys.SystemConfig.AppCenterDBVarStr + "FK_Dept WHERE No=" + BP.Sys.SystemConfig.AppCenterDBVarStr + "No");
            }
        }
        /// <summary>
        /// ����SID��SQL
        /// </summary>
        public static string UpdataSID
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("UpdataSID", "UPDATE Port_Emp SET SID=" + BP.Sys.SystemConfig.AppCenterDBVarStr + "SID WHERE No=" + BP.Sys.SystemConfig.AppCenterDBVarStr + "No");
            }
        }
        /// <summary>
        /// ����sl�ĵ�ַ
        /// </summary>
        public static string SilverlightDownloadUrl
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("SilverlightDownloadUrl", "http://go.microsoft.com/fwlink/?LinkID=124807");
            }
        }
        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <returns>��ʵ��ʽ</returns>
        public static string DealUserInfoShowModel(string no, string name)
        {
            switch (BP.WF.Glo.UserInfoShowModel)
            {
                case UserInfoShowModel.UserIDOnly:
                    return "(" + no + ")";
                case UserInfoShowModel.UserIDUserName:
                    return "(" + no + "," + name + ")";
                case UserInfoShowModel.UserNameOnly:
                    return "(" + name + ")";
                default:
                    throw new Exception("@û���жϵĸ�ʽ����.");
                    break;
            }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public static OSModel OSModel
        {
            get
            {
                OSModel os = (OSModel)BP.Sys.SystemConfig.GetValByKeyInt("OSModel", 0);
                return os;
            }
        }
        /// <summary>
        /// �Ƿ��Ǽ���ʹ��
        /// </summary>
        public static bool IsUnit
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsUnit", false);
            }
        }
        /// <summary>
        /// �Ƿ������ƶ�
        /// </summary>
        public static bool IsEnableZhiDu
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsEnableZhiDu", false);
            }
        }
        /// <summary>
        /// �Ƿ�ɾ������ע������ݣ�
        /// </summary>
        public static bool IsDeleteGenerWorkFlow
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsDeleteGenerWorkFlow", false);
            }
        }
        /// <summary>
        /// �Ƿ�������ֶ���д�Ƿ�Ϊ��
        /// </summary>
        public static bool IsEnableCheckFrmTreeIsNull
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsEnableCheckFrmTreeIsNull", true);
            }
        }

        /// <summary>
        /// �Ƿ���������ʱ���´���
        /// </summary>
        public static int IsWinOpenStartWork
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyInt("IsWinOpenStartWork", 1);
            }
        }
        /// <summary>
        /// �Ƿ�򿪴��칤��ʱ�򿪴���
        /// </summary>
        public static bool IsWinOpenEmpWorks
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsWinOpenEmpWorks", true);
            }
        }
        /// <summary>
        /// �Ƿ�������Ϣϵͳ��Ϣ��
        /// </summary>
        public static bool IsEnableSysMessage
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyBoolen("IsEnableSysMessage", true);
            }
        }
        /// <summary>
        /// ��ccflow���̷�����ص�����: ִ���Զ�����ڵ㣬�����ʱ�䣬�Է��Ӽ��㣬Ĭ��Ϊ2���ӡ�
        /// </summary>
        public static int AutoNodeDTSTimeSpanMinutes
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyInt("AutoNodeDTSTimeSpanMinutes", 2);
            }
        }
        /// <summary>
        /// ccim���ɵ����ݿ�.
        /// ��Ϊ����ccimд����Ϣ.
        /// </summary>
        public static string CCIMDBName
        {
            get
            {
                string baseUrl = BP.Sys.SystemConfig.AppSettings["CCIMDBName"];
                if (string.IsNullOrEmpty(baseUrl) == true)
                    baseUrl = "ccPort.dbo";
                return baseUrl;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string HostURL
        {
            get
            {
                string baseUrl = BP.Sys.SystemConfig.AppSettings["HostURL"];
                if (string.IsNullOrEmpty(baseUrl) == true)
                    baseUrl = BP.Sys.SystemConfig.AppSettings["BaseURL"];

                if (string.IsNullOrEmpty(baseUrl) == true)
                    baseUrl = "http://127.0.0.1/";

                if (baseUrl.Substring(baseUrl.Length - 1) != "/")
                    baseUrl = baseUrl + "/";
                return baseUrl;
            }
        }
        public static string CurrPageID
        {
            get
            {
                try
                {
                    string url = BP.Sys.Glo.Request.RawUrl;

                    int i = url.LastIndexOf("/") + 1;
                    int i2 = url.IndexOf(".aspx") - 6;
                    try
                    {
                        url = url.Substring(i);
                        return url.Substring(0, url.IndexOf(".aspx"));

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("��ȡ��ǰPageID����:" + ex.Message);
                }
            }
        }


        //�û���������
        public static string GetUserStyle
        {
            get
            {
                //BP.WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
                //if(string.IsNullOrEmpty(emp.Style) || emp.Style=="0")
                //{
                string userStyle = BP.Sys.SystemConfig.AppSettings["UserStyle"];
                if (string.IsNullOrEmpty(userStyle))
                    return "ccflowĬ��";
                else
                    return userStyle;
            }
        }
        #endregion 

        #region ʱ�����.
        /// <summary>
        /// ���óɹ���ʱ��
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static DateTime SetToWorkTime(DateTime dt)
        {
            if (BP.Sys.GloVar.Holidays.Contains(dt.ToString("MM-dd")))
            {
                dt = dt.AddDays(1);
                /*�����ǰ�ǽڼ��գ���Ҫ����һ����Ч�ڼ��㡣*/
                while (true)
                {
                    if (BP.Sys.GloVar.Holidays.Contains(dt.ToString("MM-dd")) == false)
                        break;
                    dt = dt.AddDays(1);
                }

                //����һ���ϰ�ʱ�����.
                dt = DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.AMFrom);
                return dt;
            }

            int timeInt= int.Parse(dt.ToString("HHmm"));

            //�ж��Ƿ���A����, ����ǣ��ͷ���A�����ʱ���.
            if (Glo.AMFromInt >=timeInt)
                return DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.AMFrom);


            //�ж��Ƿ���E����, ����Ǿͷ��ص�2����ϰ�ʱ���.
            if (Glo.PMToInt <= timeInt)
            {
                return DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo);
            }

            //���������ʱ����м�.
            if (Glo.AMToInt <= timeInt && Glo.PMFromInt > timeInt)
            {
                return DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMFrom);
            }
            return dt;
        }
        /// <summary>
        /// ��ָ��������������Сʱ����
        /// 1���۳����ݡ�
        /// 2���۳��ڼ��ա�
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(DateTime dt, int minutes)
        {
            //���û������,�ͷ���.
            if (minutes == 0)
                return dt;

            //���óɹ���ʱ��.
            dt = SetToWorkTime(dt);

            //�����ж��Ƿ�����һ������ʱ�����.
            if (minutes == Glo.AMPMHours*60)
            {
                 /*�����Ҫ��һ�����*/
                dt = DataType.AddDaysBak(dt, 1); 
                return dt;
            }

            //�ж��Ƿ���AM.
            bool isAM = false;
            int timeInt=int.Parse(dt.ToString("HHmm"));
            if (Glo.AMToInt > timeInt)
                isAM = true;

            #region ����ǵ�������.
            //����涨��ʱ���� 1��֮��.
            if (minutes/60/ Glo.AMPMHours < 1)
            {
                if (isAM == true)
                {
                    /*���������, ���絽������Ϣ֮���ʱ��. */

                    TimeSpan ts = DataType.ParseSysDateTime2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.AMTo) - dt;
                    if (ts.TotalMinutes >= minutes)
                    {
                        /*���ʣ��ķ��Ӵ��� Ҫ���ӵķ�����������˵+�Ϸ��Ӻ���Ȼ�����磬��ֱ��������������ӣ����䷵�ء�*/
                        return dt.AddMinutes(minutes);
                    }
                    else
                    {
                        // ������°�ʱ��ķ�������
                        TimeSpan myts = DataType.ParseSysDateTime2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo) - dt;

                        // �۳����ݵ�ʱ��.
                        int leftMuit = (int)(myts.TotalMinutes - Glo.AMPMTimeSpan * 60);
                        if (leftMuit - minutes >= 0)
                        {
                            /*˵�������ڵ����ʱ����.*/
                            DateTime mydt = DataType.ParseSysDateTime2DateTime( dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo);
                            return mydt.AddMinutes( minutes-leftMuit);
                        }

                        //˵��Ҫ�絽��2����ȥ��.
                        dt = DataType.AddDaysBak(dt, 1);
                        return Glo.AddMinutes(dt.ToString("yyyy-MM-dd") + " " + Glo.AMFrom, minutes-leftMuit );
                    }

                    // �ѵ�ǰ��ʱ�����ȥ.
                    dt = dt.AddMinutes(minutes);

                    //�ж��Ƿ�������.
                    bool isInAM = false;
                    timeInt = int.Parse(dt.ToString("HHmm"));
                    if (Glo.AMToInt >= timeInt)
                        isInAM = true;

                    if (isInAM == true)
                    {
                        // ����ʱ�����Ȼ������ͷ���.
                        return dt;
                    }

                    //�ӳ�һ������ʱ��.
                    dt = dt.AddHours(Glo.AMPMTimeSpan);

                    //�ж�ʱ����Ƿ�������E����.
                    timeInt = int.Parse(dt.ToString("HHmm"));
                    if (Glo.PMToInt <= timeInt)
                    {
                        /*���������E����.*/

                        // �����ʱ��㵽���°�֮��ķ�����.
                        TimeSpan tsE = dt - DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo);

                        //�Ӵ��յ��ϰ�ʱ�����+ ���ʱ���. 
                        dt = DataType.ParseSysDate2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo);
                        return dt.AddMinutes(tsE.TotalMinutes);
                    }
                    else
                    {
                        /*���˵�2���������٣��Ͳ�������.*/
                        return dt;
                    }
                }
                else
                {
                    /*���������, ��������������°໹����ٷ��ӣ������ӵķ���������Ƚ�. */
                    TimeSpan ts = DataType.ParseSysDateTime2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.PMTo) - dt;
                    if (ts.TotalMinutes >= minutes)
                    {
                        /*���ʣ��ķ��Ӵ��� Ҫ���ӵķ���������ֱ��������������ӣ����䷵�ء�*/
                        return dt.AddMinutes(minutes);
                    }
                    else
                    {
                        
                        //ʣ��ķ����� = �ܷ����� - ��������ʣ��ķ�����.
                        int leftMin= minutes - (int)ts.TotalMinutes;

                        /*����Ҫ���㵽��2����ȥ�ˣ� ����ʱ��Ҫ����һ����Ч�Ĺ������ϰ�ʱ�俪ʼ. */
                        dt = DataType.AddDaysBak(DataType.ParseSysDateTime2DateTime(dt.ToString("yyyy-MM-dd") + " " + Glo.AMFrom), 1);

                        //�ݹ����,�����ڴ��յ��ϰ�ʱ�������ӣ���������
                        return Glo.AddMinutes(dt, leftMin);
                    }
                     
                }
            }
            #endregion ����ǵ�������.
             
            return dt;
        }
        /// <summary>
        /// ���ӷ�����.
        /// </summary>
        /// <param name="sysdt"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(string sysdt, int minutes)
        {
            DateTime dt = DataType.ParseSysDate2DateTime(sysdt);
            return AddMinutes(dt, minutes);
        }
        #endregion ssxxx.

        #region �뿼�����.
        /// <summary>
        /// �����̷�����ȥ�Ժ󣬾Ϳ�ʼִ�п��ˡ�
        /// </summary>
        /// <param name="fl"></param>
        /// <param name="nd"></param>
        /// <param name="workid"></param>
        /// <param name="fid"></param>
        /// <param name="title"></param>
        public static void InitCH(Flow fl, Node nd, Int64 workid, Int64 fid, string title)
        {
            InitCH(fl, nd, workid, fid, title, null, null, DateTime.Now);
        }
        /// <summary>
        /// �����̷�����ȥ�Ժ󣬾Ϳ�ʼִ�п��ˡ�
        /// </summary>
        /// <param name="fl">����</param>
        /// <param name="nd">�ڵ�</param>
        /// <param name="workid">����ID</param>
        /// <param name="fid">FID</param>
        /// <param name="title">����</param>
        /// <param name="dtNow">����ĵ�ǰʱ�䣬���Ϊnull,��ȡ��ǰ����.</param>
        public static void InitCH(Flow fl, Node nd, Int64 workid, Int64 fid, string title, string prvRDT,string sdt, DateTime dtNow)
        {
            //��ʼ�ڵ㲻����.
            if (nd.IsStartNode)
                return;

            //�������Ϊ0 �򲻿���.
            if (nd.TSpanDay == 0 && nd.TSpanHour==0)
                return;

            if (dtNow == null)
                dtNow = DateTime.Now;

            if (sdt == null || prvRDT == null)
            {
                string dbstr = SystemConfig.AppCenterDBVarStr;
                Paras ps = new Paras();
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        ps.SQL = "SELECT TOP 1 RDT,SDT FROM WF_GENERWORKERLIST  WHERE WorkID=" + dbstr + "WorkID AND FK_Emp=" + dbstr + "FK_Emp AND FK_Node=" + dbstr + "FK_Node ORDER BY RDT DESC";
                        break;
                    case DBType.Oracle:
                    case DBType.MySQL:
                        ps.SQL = "SELECT  RDT,SDT FROM WF_GENERWORKERLIST  WHERE WorkID=" + dbstr + "WorkID AND FK_Emp=" + dbstr + "FK_Emp AND FK_Node=" + dbstr + "FK_Node AND ROWNUM=1 ORDER BY RDT DESC ";
                        break;
                    default:
                        break;
                }
                ps.Add("WorkID", workid);
                ps.Add("FK_Emp", WebUser.No);
                ps.Add("FK_Node", nd.NodeID);

                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                    return;

                prvRDT = dt.Rows[0][0].ToString();
                sdt = dt.Rows[0][1].ToString(); 
            }

            #region ��ʼ����������.
            BP.WF.Data.CH ch = new CH();
            ch.WorkID = workid;
            ch.FID = fid;
            ch.Title = title;

            int hh = (int)nd.TSpanHour;
            float mm = (nd.TSpanHour - hh)*60;
            ch.TSpan = nd.TSpanDay + "��" + hh + "ʱ" + mm + "��";
            ch.FK_NY = dtNow.ToString("yyyy-MM");

            ch.DTFrom = prvRDT;  //�����´�ʱ��.
            ch.SDT = sdt; //Ӧ�����ʱ��.

            ch.FK_Flow = nd.FK_Flow;
            ch.FK_FlowT = nd.FlowName;

            ch.FK_Node = nd.NodeID;
            ch.FK_NodeT = nd.Name;

            ch.FK_Dept = WebUser.FK_Dept;
            ch.FK_DeptT = WebUser.FK_DeptName;

            ch.FK_Emp = WebUser.No;
            ch.FK_EmpT = WebUser.Name;

            //����ǵڼ�����.
           // ch.Week = (int)dtNow.w;
            System.Globalization.CultureInfo myCI =  
                        new System.Globalization.CultureInfo("zh-CN");            
            ch.Week  = myCI.Calendar.GetWeekOfYear(dtNow,System.Globalization.CalendarWeekRule.FirstDay,System.DayOfWeek.Monday);

            //string weekName = dtNow.ToString("dddd",myCI);
            //Console.WriteLine("����Ϊ:{0},\n��{1},\n��һ���еĵ�{2}���ܣ���һ��ĵ�һ�����һ���ܣ�һ������һ��ʼ��",
            //    dateTime,weekName, weekOfYear);

            // mypk.
            ch.MyPK = nd.NodeID + "_" + workid + "_" + fid + "_" + WebUser.No;
            #endregion ��ʼ����������.


           // �������ʱ��� dtFrom.
            DateTime dtFrom = BP.DA.DataType.ParseSysDateTime2DateTime(ch.DTFrom);
            dtFrom = Glo.SetToWorkTime(dtFrom);

            //��ǰʱ��.  -���ý���ʱ�䵽.
            ch.DTTo = dtNow.ToString(DataType.SysDataTimeFormat); // dtto..
            dtNow = Glo.SetToWorkTime(dtNow);

            int dtHHmm = 0;
            if (dtFrom.Year == dtNow.Year && dtFrom.Month == dtNow.Month && dtFrom.Day == dtFrom.Day)
            {
                // ���㷢��ʱ���Ƿ�������?
                dtHHmm = int.Parse(dtFrom.ToString("HHmm"));
                bool isSendAM = false;
                if (dtHHmm >= Glo.AMFromInt && dtHHmm <= Glo.AMToInt)
                {
                    /*�����˷���ʱ��������, ���Ҵ����˴���ʱ��Ҳ������.*/
                    isSendAM = true;
                }

                // ���㴦��ʱ���Ƿ������磿
                dtHHmm = int.Parse(dtFrom.ToString("HHmm"));
                bool isCurrAM = false;
                if (dtHHmm >= Glo.AMFromInt && dtHHmm <= Glo.AMToInt)
                {
                    /*�����˷���ʱ��������, ���Ҵ����˴���ʱ��Ҳ������.*/
                    isCurrAM = true;
                }

                /* �����ͬһ��.  �����������.*/
                if (isSendAM && isCurrAM)
                {
                    TimeSpan ts = dtNow - dtFrom;
                    ch.UseMinutes += (int)ts.TotalMinutes; // �õ�ʵ���õ�ʱ��.
                }

                /* �����ͬһ��.  �����������.*/
                if (isSendAM == false && isCurrAM == false)
                {
                    TimeSpan ts = dtNow - dtFrom;

                    //����ʱ������ȥ���ݵ�ʱ��.
                    ch.UseMinutes += (int)ts.TotalMinutes; // �õ�ʵ���õ�ʱ��.
                }

                /* �����ͬһ��.  ���һ��������һ��������.*/
                if (isSendAM == true && isCurrAM == false)
                {
                    float f60 = 60;
                    TimeSpan ts = dtNow - dtFrom;
                    //ts.TotalMinutes
                    float hours = (float)ts.TotalHours - Glo.AMPMTimeSpan; // �õ�ʵ���õ�ʱ��.

                    // ʵ��ʹ��ʱ��.
                    ch.UseMinutes += (int)hours*60;
                }

                //�󳬹�ʱ��.
                ch.OverMinutes = ch.UseMinutes - nd.TSpanMinues;

                //����ʱ��״̬.
                if (ch.OverMinutes > 0)
                {   
                    /* �������������˵������һ��������ɵ�״̬. */
                    if (ch.OverMinutes / 2 > nd.TSpanMinues)
                        ch.CHSta = CHSta.ChaoQi; //���������ʱ���һ�룬���ǳ���.
                    else
                        ch.CHSta = CHSta.YuQi;   //�ڹ涨��ʱ���������ɣ�����Ԥ�ڡ�
                }
                else
                {
                    /* �Ǹ���������ǰ���. */
                    if (ch.OverMinutes / 2 > -nd.TSpanMinues)
                        ch.CHSta = CHSta.JiShi; //�����ǰ��һ���ʱ�䣬���Ǽ�ʱ���.
                    else
                        ch.CHSta = CHSta.AnQi; // ������ǰ������.
                }

                #region �����������ʶ��ķ�����.
                //��ʹ������.
                float day = ch.UseMinutes / 60f / Glo.AMPMHours;
                int dayInt = (int)day;

                //��Сʱ��.
                float hour = (ch.UseMinutes - dayInt * Glo.AMPMHours*60f)/60f;
                int hourInt = (int)hour;

                //�������.
                float minute = (hour - hourInt)*60;

                //ʹ��ʱ��.
                ch.UseTime = dayInt + "��" + hourInt + "ʱ" + minute + "��";


                //��Ԥ������.
                int overMinus = Math.Abs(ch.OverMinutes);
                day = overMinus / 60f / Glo.AMPMHours;
                  dayInt = (int)day;

                //��Сʱ��.
                  hour = (overMinus - dayInt * Glo.AMPMHours * 60f) / 60f;
                  hourInt = (int)hour;

                //�������.
                  minute = (hour - hourInt) * 60;

                //ʹ��ʱ��.
                if (ch.OverMinutes >0)
                    ch.OverTime = "Ԥ��:" + dayInt + "��" + hourInt + "Сʱ" + (int)minute + "��";
                else
                    ch.OverTime = "��ǰ:" + dayInt + "��" + hourInt + "Сʱ" + (int)minute + "��";

                #endregion �����������ʶ��ķ�����.

                  //ִ�б���.
                try
                {
                    ch.DirectInsert();
                }
                catch
                {
                    ch.CheckPhysicsTable();
                    ch.DirectUpdate();
                }
            }
        }
        /// <summary>
        /// ����ʱ���
        /// </summary>
        public static string AMFrom
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("AMFrom", "08:30");
            }
        }
        /// <summary>
        /// ����ʱ���
        /// </summary>
        public static int AMFromInt
        {
            get
            {
                return int.Parse(Glo.AMFrom.Replace(":", ""));
            }
        }
        /// <summary>
        /// һ����Ч�Ĺ���Сʱ��
        /// �����繤��Сʱ+���繤��Сʱ.
        /// </summary>
        public static float AMPMHours
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyFloat("AMPMHours", 8);
            }
        }
        /// <summary>
        /// ��������Сʱ��
        /// </summary>
        public static float AMPMTimeSpan
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKeyFloat("AMPMTimeSpan", 1);
            }
        }
        /// <summary>
        /// ����ʱ�䵽
        /// </summary>
        public static string AMTo
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("AMTo", "11:30");
            }
        }
        /// <summary>
        /// ����ʱ�䵽
        /// </summary>
        public static int AMToInt
        {
            get
            {
                return int.Parse(Glo.AMTo.Replace(":", ""));
            }
        }
        /// <summary>
        /// ����ʱ���
        /// </summary>
        public static string PMFrom
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("PMFrom", "13:30");
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public static int PMFromInt
        {
            get
            {
                return int.Parse(Glo.PMFrom.Replace(":", ""));
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public static string PMTo
        {
            get
            {
                return BP.Sys.SystemConfig.GetValByKey("PMTo", "17:30");
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public static int PMToInt
        {
            get
            {
                return int.Parse(Glo.PMTo.Replace(":", ""));
            }
        }
        #endregion �뿼�����.

        #region ����������
        /// <summary>
        /// ת����Ϣ��ʾ����.
        /// </summary>
        /// <param name="info"></param>
        public static void ToMsg(string info)
        {
            //string rowUrl = BP.Sys.Glo.Request.RawUrl;
            //if (rowUrl.Contains("&IsClient=1"))
            //{
            //    /*˵������vsto���õ�.*/
            //    return;
            //}

            System.Web.HttpContext.Current.Session["info"] = info;
            System.Web.HttpContext.Current.Response.Redirect(Glo.CCFlowAppPath + "WF/MyFlowInfo.aspx?Msg=" + DataType.CurrentDataTimess, false);
        }
        public static void ToMsgErr(string info)
        {
            info = "<font color=red>" + info + "</font>";
            System.Web.HttpContext.Current.Session["info"] = info;
            System.Web.HttpContext.Current.Response.Redirect(Glo.CCFlowAppPath + "WF/MyFlowInfo.aspx?Msg=" + DataType.CurrentDataTimess, false);
        }
        /// <summary>
        /// ������̷�������
        /// </summary>
        /// <param name="flow">����</param>
        /// <param name="wk">��ʼ�ڵ㹤��</param>
        /// <returns></returns>
        public static bool CheckIsCanStartFlow_InitStartFlow(Flow flow, Work wk)
        {
            StartLimitRole role = flow.StartLimitRole;
            if (role == StartLimitRole.None)
                return true;

            string sql = "";
            string ptable = flow.PTable;

            #region ����ʱ��ı����ǣ��ڱ����غ��ж�, �����û������Ƿ���ȷ.
            DateTime dtNow = DateTime.Now;
            if (role == StartLimitRole.Day)
            {
                /* ������һ�췢��һ�� */
                sql = "SELECT COUNT(*) as Num FROM " + ptable + " WHERE RDT LIKE '" + DataType.CurrentData + "%' AND WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                {
                    if (flow.StartLimitPara == "")
                        return true;

                    //�ж�ʱ���Ƿ������õķ���Χ��. ���õĸ�ʽΪ @11:00-12:00@15:00-13:45
                    string[] strs = flow.StartLimitPara.Split('@');
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;
                        string[] timeStrs = str.Split('-');
                        string tFrom = DateTime.Now.ToString("yyyy-MM-dd") + " " + timeStrs[0].Trim();
                        string tTo = DateTime.Now.ToString("yyyy-MM-dd") + " " + timeStrs[1].Trim();
                        if (DataType.ParseSysDateTime2DateTime(tFrom) <= dtNow && dtNow >= DataType.ParseSysDateTime2DateTime(tTo))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }

            if (role == StartLimitRole.Week)
            {
                /*
                 * 1, �ҳ���1 �����շֱ��ǵڼ���.
                 * 2, ���������Χȥ��ѯ,�����ѯ���������˵���Ѿ������ˡ�
                 */
                sql = "SELECT COUNT(*) as Num FROM " + ptable + " WHERE RDT >= '" + DataType.WeekOfMonday(dtNow) + "' AND WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                {
                    if (flow.StartLimitPara == "")
                        return true; /*���û��ʱ�������.*/

                    //�ж�ʱ���Ƿ������õķ���Χ��. 
                    // ���õĸ�ʽΪ @Sunday,11:00-12:00@Monday,15:00-13:45, ��˼��.���գ���һ��ָ����ʱ��㷶Χ�ڿ�����������.

                    string[] strs = flow.StartLimitPara.Split('@');
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;

                        string weekStr = DateTime.Now.DayOfWeek.ToString().ToLower();
                        if (str.ToLower().Contains(weekStr) == false)
                            continue; // �ж��Ƿ�ǰ����.

                        string[] timeStrs = str.Split(',');
                        string tFrom = DateTime.Now.ToString("yyyy-MM-dd") + " " + timeStrs[0].Trim();
                        string tTo = DateTime.Now.ToString("yyyy-MM-dd") + " " + timeStrs[1].Trim();
                        if (DataType.ParseSysDateTime2DateTime(tFrom) <= dtNow && dtNow >= DataType.ParseSysDateTime2DateTime(tTo))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }

            // #warning û�п��ǵ��ܵ���δ���.

            if (role == StartLimitRole.Month)
            {
                sql = "SELECT COUNT(*) as Num FROM " + ptable + " WHERE FK_NY = '" + DataType.CurrentYearMonth + "' AND WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                {
                    if (flow.StartLimitPara == "")
                        return true;

                    //�ж�ʱ���Ƿ������õķ���Χ��. ���ø�ʽ: @-01 12:00-13:11@-15 12:00-13:11 , ��˼�ǣ���ÿ�µ�1��,15�� 12:00-13:11������������.
                    string[] strs = flow.StartLimitPara.Split('@');
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;
                        string[] timeStrs = str.Split('-');
                        string tFrom = DateTime.Now.ToString("yyyy-MM-") + " " + timeStrs[0].Trim();
                        string tTo = DateTime.Now.ToString("yyyy-MM-") + " " + timeStrs[1].Trim();
                        if (DataType.ParseSysDateTime2DateTime(tFrom) <= dtNow && dtNow >= DataType.ParseSysDateTime2DateTime(tTo))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }

            if (role == StartLimitRole.JD)
            {
                sql = "SELECT COUNT(*) as Num FROM " + ptable + " WHERE FK_NY = '" + DataType.CurrentAPOfJD + "' AND WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                {
                    if (flow.StartLimitPara == "")
                        return true;

                    //�ж�ʱ���Ƿ������õķ���Χ��.
                    string[] strs = flow.StartLimitPara.Split('@');
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;
                        string[] timeStrs = str.Split('-');
                        string tFrom = DateTime.Now.ToString("yyyy-") + " " + timeStrs[0].Trim();
                        string tTo = DateTime.Now.ToString("yyyy-") + " " + timeStrs[1].Trim();
                        if (DataType.ParseSysDateTime2DateTime(tFrom) <= dtNow && dtNow >= DataType.ParseSysDateTime2DateTime(tTo))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }

            if (role == StartLimitRole.Year)
            {
                sql = "SELECT COUNT(*) as Num FROM " + ptable + " WHERE FK_NY LIKE '" + DataType.CurrentYear + "%' AND WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                {
                    if (flow.StartLimitPara == "")
                        return true;

                    //�ж�ʱ���Ƿ������õķ���Χ��.
                    string[] strs = flow.StartLimitPara.Split('@');
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;
                        string[] timeStrs = str.Split('-');
                        string tFrom = DateTime.Now.ToString("yyyy-") + " " + timeStrs[0].Trim();
                        string tTo = DateTime.Now.ToString("yyyy-") + " " + timeStrs[1].Trim();
                        if (DataType.ParseSysDateTime2DateTime(tFrom) <= dtNow && dtNow >= DataType.ParseSysDateTime2DateTime(tTo))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            #endregion ����ʱ��ı����ǣ��ڱ����غ��ж�, �����û������Ƿ���ȷ.

            return true;
        }
        /// <summary>
        /// ��Ҫ�����Ǽ�������Ƿ����������.
        /// </summary>
        /// <param name="flow">����</param>
        /// <param name="wk">��ʼ�ڵ㹤��</param>
        /// <returns></returns>
        public static bool CheckIsCanStartFlow_SendStartFlow(Flow flow, Work wk)
        {
            StartLimitRole role = flow.StartLimitRole;
            if (role == StartLimitRole.None)
                return true;

            string sql = "";
            string ptable = flow.PTable;

            if (role == StartLimitRole.ColNotExit)
            {
                /* ָ�����������ϲ����ڣ��ſ��Է������̡�*/

                //���ԭ����ֵ.
                string[] strs = flow.StartLimitPara.Split(',');
                string val = "";
                foreach (string str in strs)
                {
                    if (string.IsNullOrEmpty(str) == true)
                        continue;
                    try
                    {
                        val += wk.GetValStringByKey(str);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@������ƴ���,�����õļ�����(" + flow.StartLimitPara + "),�е���(" + str + ")�Ѿ������ڱ���.");
                    }
                }

                //�ҳ��Ѿ������ȫ������.
                sql = "SELECT " + flow.StartLimitPara + " FROM " + ptable + " WHERE  WFState NOT IN(0,1) AND FlowStarter='" + WebUser.No + "'";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    string v = dr[0] + "" + dr[1] + "" + dr[2];
                    if (v == val)
                        return false;
                }
                return true;
            }

            // ���õ�sql,ִ�к�,���ؽ���� 0 .
            if (role == StartLimitRole.ResultIsZero)
            {
                sql = BP.WF.Glo.DealExp(flow.StartLimitPara, wk, null);
                if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                    return true;
                else
                    return false;
            }

            // ���õ�sql,ִ�к�,���ؽ���� <> 0 .
            if (role == StartLimitRole.ResultIsNotZero)
            {
                sql = BP.WF.Glo.DealExp(flow.StartLimitPara, wk, null);
                if (DBAccess.RunSQLReturnValInt(sql, 0) != 0)
                    return true;
                else
                    return false;
            }
            return true;
        }
        #endregion ����������
        
    }
}
