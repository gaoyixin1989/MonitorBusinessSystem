using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;

namespace BP.WF.DTS
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class OneKeyBackCCFlow : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public OneKeyBackCCFlow()
        {
            this.Title = "һ���������������";
            this.Help = "�����̡�������֯�ṹ���ݶ�����xml�ĵ����ݵ�C:\\CCFlowTemplete���档";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string path = "C:\\CCFlowTemplete" + DateTime.Now.ToString("yy��MM��dd��HHʱmm��ss��");
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            #region 1.�������������Ϣ
            DataSet dsFlows = new DataSet();
            //WF_FlowSort
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_FlowSort");
            dt.TableName = "WF_FlowSort";
            dsFlows.Tables.Add(dt);
            dsFlows.WriteXml(path + "\\FlowTables.xml");
            #endregion �������������Ϣ.

            #region 2.������֯�ṹ.
            DataSet dsPort = new DataSet();
            //emps
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Port_Emp");
            dt.TableName = "Port_Emp";
            dsPort.Tables.Add(dt);

            //Port_Dept
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Port_Dept");
            dt.TableName = "Port_Dept";
            dsPort.Tables.Add(dt);

            //Port_Station
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Port_Station");
            dt.TableName = "Port_Station";
            dsPort.Tables.Add(dt);

            //Port_EmpStation
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Port_EmpStation");
            dt.TableName = "Port_EmpStation";
            dsPort.Tables.Add(dt);

            //Port_EmpDept
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Port_EmpDept");
            dt.TableName = "Port_EmpDept";
            dsPort.Tables.Add(dt);

            dsPort.WriteXml(path + "\\PortTables.xml");
            #endregion ���ݱ��������.

            #region 3.����ϵͳ����
            DataSet dsSysTables = new DataSet();

            //Sys_EnumMain
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Sys_EnumMain");
            dt.TableName = "Sys_EnumMain";
            dsSysTables.Tables.Add(dt);

            //Sys_Enum
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Sys_Enum");
            dt.TableName = "Sys_Enum";
            dsSysTables.Tables.Add(dt);

            //Sys_FormTree
            dt = DBAccess.RunSQLReturnTable("SELECT * FROM Sys_FormTree");
            dt.TableName = "Sys_FormTree";
            dsSysTables.Tables.Add(dt);
            dsSysTables.WriteXml(path + "\\SysTables.xml");
            #endregion ����ϵͳ����.

            #region 4.���ݱ��������.
            string pathOfTables = path + "\\SFTables";
            System.IO.Directory.CreateDirectory(pathOfTables);
            SFTables tabs = new SFTables();
            tabs.RetrieveAll();
            foreach (SFTable item in tabs)
            {
                if (item.No.Contains("."))
                    continue;

                string sql = "SELECT * FROM " + item.No + " ";
                DataSet ds = new DataSet();
                ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
                ds.WriteXml(pathOfTables + "\\" + item.No + ".xml");
            }
            #endregion ���ݱ��������.

            #region 5.��������.
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                FlowSort fs = new FlowSort(fl.FK_FlowSort);
                string pathDir = path + "\\Flow\\" + fs.No + "." + fs.Name+"\\";
                if (System.IO.Directory.Exists(pathDir) == false)
                    System.IO.Directory.CreateDirectory(pathDir);

                fl.DoExpFlowXmlTemplete(pathDir);
            }
            #endregion ��������.

            #region 6.���ݱ�.
            MapDatas mds = new MapDatas();
            mds.RetrieveAllFromDBSource();
            foreach (MapData md in mds)
            {
                if (md.FK_FrmSort.Length < 2)
                    continue;

                BP.Sys.SysFormTree fs = new SysFormTree(md.FK_FormTree);
                string pathDir = path + "\\Form\\" + fs.No + "." + fs.Name;
                if (System.IO.Directory.Exists(pathDir) == false)
                    System.IO.Directory.CreateDirectory(pathDir);
                DataSet ds = md.GenerHisDataSet();
                ds.WriteXml(pathDir + "\\" + md.Name + ".xml");
            }
            #endregion ���ݱ�.

            return "ִ�гɹ�,���·��:" + path;
        }
    }
}
