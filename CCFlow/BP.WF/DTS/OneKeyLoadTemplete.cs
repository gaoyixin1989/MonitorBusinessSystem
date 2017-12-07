using System;
using System.IO;
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
    public class OneKeyLoadTemplete : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public OneKeyLoadTemplete()
        {
            this.Title = "һ���ָ�����ģ��Ŀ¼";
            this.Help = "�˹�����һ���������̵��������.";
            this.Help += "@ִ��ʱ��ע��";
            this.Help += "@1,ϵͳ���е��������ݡ�ģ�����ݡ���֯�⹹���ݡ����ᱻɾ����";
            this.Help += "@2,����װ��C:\\CCFlowTemplete �����ݡ�";
            this.Help += "@3,�˹���һ���ṩ��ccflow�Ŀ��������ڲ�ͬ�����ݿ�֮�����ֲ��";
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
                if (BP.Web.WebUser.No != "admin")
                    return false;

                return true;
            }
        }
        public override object Do()
        {
            string msg = "";

            #region ��������ļ��Ƿ�����.
            string path = "C:\\CCFlowTemplete";
            if (System.IO.Directory.Exists(path) == false)
                msg += "@����Լ����Ŀ¼�����ڷ�����" + path + ",��Ѵ�ccflow���ݵ��ļ�����" + path;

            //PortTables.
            string file = path + "\\PortTables.xml";
            if (System.IO.File.Exists(file) == false)
                msg += "@����Լ�����ļ������ڣ�" + file;

            //SysTables.
            file = path + "\\SysTables.xml";
            if (System.IO.File.Exists(file) == false)
                msg += "@����Լ�����ļ������ڣ�" + file;

            //FlowTables.
            file = path + "\\FlowTables.xml";
            if (System.IO.File.Exists(file) == false)
                msg += "@����Լ�����ļ������ڣ�" + file;
            #endregion ��������ļ��Ƿ�����.

            #region 1 װ�����̻���������.
            DataSet ds = new DataSet();
            ds.ReadXml(path + "\\FlowTables.xml");

            //�������.
            FlowSorts sorts = new FlowSorts();
            sorts.ClearTable();
            DataTable dt = ds.Tables["WF_FlowSort"];
           // sorts = QueryObject.InitEntitiesByDataTable(sorts, dt, null) as FlowSorts;
            foreach (FlowSort item in sorts)
            {
                item.DirectInsert(); //��������.
            }
            #endregion 1 װ�����̻���������.

            #region 2 ��֯�ṹ.
            ds = new DataSet();
            ds.ReadXml(path + "\\PortTables.xml");

            //Port_Emp.
            Emps emps = new Emps();
            emps.ClearTable();
            dt = ds.Tables["Port_Emp"];
            emps = QueryObject.InitEntitiesByDataTable(emps, dt, null) as Emps;
            foreach (Emp item in emps)
            {
                item.DirectInsert(); //��������.
            }

            //Depts.
            Depts depts = new Depts();
            depts.ClearTable();
            dt = ds.Tables["Port_Dept"];
            depts = QueryObject.InitEntitiesByDataTable(depts, dt, null) as Depts;
            foreach (Dept item in depts)
            {
                item.DirectInsert(); //��������.
            }

            //Stations.
            Stations stas = new Stations();
            stas.ClearTable();
            dt = ds.Tables["Port_Station"];
            stas = QueryObject.InitEntitiesByDataTable(stas, dt, null) as Stations;
            foreach (Station item in stas)
            {
                item.DirectInsert(); //��������.
            }


            //EmpDepts.
            EmpDepts eds = new EmpDepts();
            eds.ClearTable();
            dt = ds.Tables["Port_EmpDept"];
            eds = QueryObject.InitEntitiesByDataTable(eds, dt, null) as EmpDepts;
            foreach (EmpDept item in eds)
            {
                item.DirectInsert(); //��������.
            }

            //EmpStations.
            EmpStations ess = new EmpStations();
            ess.ClearTable();
            dt = ds.Tables["Port_EmpStation"];
            ess = QueryObject.InitEntitiesByDataTable(ess, dt, null) as EmpStations;
            foreach (EmpStation item in ess)
            {
                item.DirectInsert(); //��������.
            }
            #endregion 2 ��֯�ṹ.

            #region 3 �ָ�ϵͳ����.
            ds = new DataSet();
            ds.ReadXml(path + "\\SysTables.xml");

            //ö��Main.
            SysEnumMains sems = new SysEnumMains();
            sems.ClearTable();
            dt = ds.Tables["Sys_EnumMain"];
            sems = QueryObject.InitEntitiesByDataTable(sems, dt, null) as SysEnumMains;
            foreach (SysEnumMain item in sems)
            {
                item.DirectInsert(); //��������.
            }

            //ö��.
            SysEnums ses = new SysEnums();
            ses.ClearTable();
            dt = ds.Tables["Sys_Enum"];
            ses = QueryObject.InitEntitiesByDataTable(ses, dt, null) as SysEnums;
            foreach (SysEnum item in ses)
            {
                item.DirectInsert(); //��������.
            }

            ////Sys_FormTree.
            //BP.Sys.SysFormTrees sfts = new SysFormTrees();
            //sfts.ClearTable();
            //dt = ds.Tables["Sys_FormTree"];
            //sfts = QueryObject.InitEntitiesByDataTable(sfts, dt, null) as SysFormTrees;
            //foreach (SysFormTree item in sfts)
            //{
            //    try
            //    {
            //       item.DirectInsert(); //��������.
            //    }
            //    catch
            //    {
            //    }
            //}
            #endregion 3 �ָ�ϵͳ����.

            #region 4.���ݱ��������.
            if (1 == 2)
            {
                string pathOfTables = path + "\\SFTables";
                System.IO.Directory.CreateDirectory(pathOfTables);
                SFTables tabs = new SFTables();
                tabs.RetrieveAll();
                foreach (SFTable item in tabs)
                {
                    if (item.No.Contains("."))
                        continue;

                    string sql = "SELECT * FROM " + item.No;
                    ds = new DataSet();
                    ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
                    ds.WriteXml(pathOfTables + "\\" + item.No + ".xml");
                }
            }
            #endregion 4 ���ݱ��������.

            #region 5.�ָ�������.
            //ɾ�����е���������.
            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData fl in mds)
            {
                //if (fl.FK_FormTree.Length > 1 || fl.FK_FrmSort.Length > 1)
                //    continue;
                fl.Delete(); //ɾ������.
            }

            //�������.
            SysFormTrees fss = new SysFormTrees();
            fss.ClearTable();

            // ���ȱ��ļ���         
            string frmPath = path + "\\Form";
            DirectoryInfo dirInfo = new DirectoryInfo(frmPath);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo item in dirs)
            {
                if (item.FullName.Contains(".svn"))
                    continue;

                string[] fls = System.IO.Directory.GetFiles(item.FullName);
                if (fls.Length == 0)
                    continue;

                SysFormTree fs = new SysFormTree();
                fs.No = item.Name.Substring(0, item.Name.IndexOf('.') );
                fs.Name = item.Name.Substring(item.Name.IndexOf('.'));
                fs.ParentNo = "0";
                fs.Insert();

                foreach (string f in fls)
                {
                    try
                    {
                        msg += "@��ʼ���ȱ�ģ���ļ�:" + f;
                        System.IO.FileInfo info = new System.IO.FileInfo(f);
                        if (info.Extension != ".xml")
                            continue;

                        ds = new DataSet();
                        ds.ReadXml(f);

                        MapData md = MapData.ImpMapData(ds, false);
                        md.FK_FrmSort = fs.No;
                        md.Update();
                    }
                    catch (Exception ex)
                    {
                        msg += "@����ʧ��,�ļ�:"+f+",�쳣��Ϣ:" + ex.Message;
                    }
                }
            }
            #endregion 5.�ָ�������.

            #region 6.�ָ���������.
            //ɾ�����е���������.
            Flows flsEns = new Flows();
            flsEns.RetrieveAll();
            foreach (Flow fl in flsEns)
            {
                fl.DoDelete(); //ɾ������.
            }

            dirInfo = new DirectoryInfo(path + "\\Flow\\");
            dirs = dirInfo.GetDirectories();

            //ɾ������.
            FlowSorts fsRoots = new FlowSorts();
            fsRoots.ClearTable();

            //����������.
            FlowSort fsRoot = new FlowSort();
            fsRoot.No = "99";
            fsRoot.Name = "������";
            fsRoot.ParentNo = "0";
            fsRoot.DirectInsert();

            foreach (DirectoryInfo dir in dirs)
            {
                if (dir.FullName.Contains(".svn"))
                    continue;

                string[] fls = System.IO.Directory.GetFiles(dir.FullName);
                if (fls.Length == 0)
                    continue;

                FlowSort fs = new FlowSort();
                fs.No = dir.Name.Substring(0, dir.Name.IndexOf('.') );
                fs.Name = dir.Name.Substring(3);
                fs.ParentNo = fsRoot.No;
                fs.Insert();

                foreach (string filePath in fls)
                {
                    msg += "@��ʼ��������ģ���ļ�:" + filePath;
                    Flow myflow = BP.WF.Flow.DoLoadFlowTemplate(fs.No, filePath, ImpFlowTempleteModel.AsTempleteFlowNo);
                    msg += "@����:" + myflow.Name + "װ�سɹ���";

                    System.IO.FileInfo info = new System.IO.FileInfo(filePath);
                    myflow.Name = info.Name.Replace(".xml", "");
                    if (myflow.Name.Substring(2, 1) == ".")
                        myflow.Name = myflow.Name.Substring(3);

                    myflow.DirectUpdate();
                }
            }
            #endregion 6.�ָ���������.

            BP.DA.Log.DefaultLogWriteLineInfo(msg);

            //ɾ������Ŀո�.
            BP.WF.DTS.DeleteBlankGroupField dts = new DeleteBlankGroupField();
            dts.Do();

            //ִ������ǩ��.
            GenerSiganture gs = new GenerSiganture();
            gs.Do();

            return msg;
        }
    }
}
