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
    public class LoadTemplete : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public LoadTemplete()
        {
            this.Title = "װ��������ʾģ��";
            this.Help = "Ϊ�˰�����λ������ѧϰ������ccflow, ���ṩһЩ����ģ�����ģ���Է���ѧϰ��";
            this.Help += "@��Щģ���λ��" + SystemConfig.PathOfData + "\\FlowDemo\\";
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
                else
                    return false;
            }
        }
        public override object Do()
        {
            string msg = "";

            #region �����.
            // ���ȱ��ļ���
            SysFormTrees fss = new SysFormTrees();
            fss.ClearTable();

            //����root.
            SysFormTree root = new SysFormTree();
            root.No = "0";
            root.Name = "����";
            root.ParentNo = "-1";
            root.Insert();

            string frmPath = SystemConfig.PathOfData + "\\FlowDemo\\Form\\";
            DirectoryInfo dirInfo = new DirectoryInfo(frmPath);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            int i = 0;
            foreach (DirectoryInfo item in dirs)
            {
                if (item.FullName.Contains(".svn"))
                    continue;

                string[] fls = System.IO.Directory.GetFiles(item.FullName);
                if (fls.Length == 0)
                    continue;

                SysFormTree fs = new SysFormTree();
                fs.No = item.Name.Substring(0, 2);
                fs.Name = item.Name.Substring(3);
                fs.ParentNo = "0";
                fs.Idx = i++;
                fs.Insert();

                foreach (string f in fls)
                {
                    msg += "@��ʼ���ȱ�ģ���ļ�:" + f;
                    System.IO.FileInfo info = new System.IO.FileInfo(f);
                    if (info.Extension != ".xml")
                        continue;

                    DataSet ds = new DataSet();
                    ds.ReadXml(f);

                    try
                    {
                        MapData md = MapData.ImpMapData(ds, false);
                        md.FK_FrmSort = fs.No;
                        md.Update();
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("@װ��ģ���ļ�:"+f+"���ִ���,"+ex.Message+" <br> "+ex.StackTrace);
                    }
                }
            }
            #endregion �����.

            #region ��������.
            FlowSorts sorts = new FlowSorts();
            sorts.ClearTable();
              dirInfo = new DirectoryInfo(SystemConfig.PathOfData + "\\FlowDemo\\Flow\\");
            dirs = dirInfo.GetDirectories();

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
                fs.No = dir.Name.Substring(0, 2);
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


                //����������һ��Ŀ¼.
                DirectoryInfo dirSubInfo = new DirectoryInfo(SystemConfig.PathOfData + "\\FlowDemo\\Flow\\"+dir.Name);
                DirectoryInfo[] myDirs = dirSubInfo.GetDirectories();
                foreach (DirectoryInfo mydir in myDirs)
                {
                    if (mydir.FullName.Contains(".svn"))
                        continue;

                    string[] myfls = System.IO.Directory.GetFiles(mydir.FullName);
                    if (myfls.Length == 0)
                        continue;

                    // 
                    FlowSort subFlowSort = fs.DoCreateSubNode() as FlowSort;
                    subFlowSort.Name = mydir.Name.Substring(3);
                    subFlowSort.Update();

                    foreach (string filePath in myfls)
                    {
                        msg += "@��ʼ��������ģ���ļ�:" + filePath;
                        Flow myflow = BP.WF.Flow.DoLoadFlowTemplate(subFlowSort.No, filePath, ImpFlowTempleteModel.AsTempleteFlowNo);
                        msg += "@����:" + myflow.Name + "װ�سɹ���";

                        System.IO.FileInfo info = new System.IO.FileInfo(filePath);
                        myflow.Name = info.Name.Replace(".xml", "");
                        if (myflow.Name.Substring(2, 1) == ".")
                            myflow.Name = myflow.Name.Substring(3);
                        myflow.DirectUpdate();
                    }
                }

            }
            #endregion ��������.


            BP.DA.Log.DefaultLogWriteLineInfo(msg);

            //ɾ������Ŀո�.
            BP.WF.DTS.DeleteBlankGroupField dts = new DeleteBlankGroupField();
            dts.Do();

            return msg;
        }
    }
}
