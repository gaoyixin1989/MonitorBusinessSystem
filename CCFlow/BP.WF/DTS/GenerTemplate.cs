using System;
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
    public class GenerTemplate : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public GenerTemplate()
        {
            this.Title = "��������ģ�����ģ��";
            this.Help = "��ϵͳ�е��������ת����ģ�����ָ����Ŀ¼�¡�";
            this.HisAttrs.AddTBString("Path", "C:\\ccflow.Template", "���ɵ�·��", true, false, 1, 1900, 200);
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
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string path = this.GetValStrByKey("Path") + "_" + DateTime.Now.ToString("yy��MM��dd��HHʱmm��");
            if (System.IO.Directory.Exists(path))
                return "ϵͳ����ִ���У����Ժ�";

            System.IO.Directory.CreateDirectory(path);
            System.IO.Directory.CreateDirectory(path + "\\Flow.����ģ��");
            System.IO.Directory.CreateDirectory(path + "\\Frm.��ģ��");

            Flows fls = new Flows();
            fls.RetrieveAll();
            FlowSorts sorts = new FlowSorts();
            sorts.RetrieveAll();

            // ��������ģ�塣
            foreach (FlowSort sort in sorts)
            {
                string pathDir = path + "\\Flow.����ģ��\\" + sort.No + "." + sort.Name;
                System.IO.Directory.CreateDirectory(pathDir);
                foreach (Flow fl in fls)
                {
                    fl.DoExpFlowXmlTemplete(pathDir);
                }
            }

            // ���ɱ�ģ�塣
            foreach (FlowSort sort in sorts)
            {
                string pathDir = path + "\\Frm.��ģ��\\" + sort.No + "." + sort.Name;
                System.IO.Directory.CreateDirectory(pathDir);
                foreach (Flow fl in fls)
                {
                    string pathFlowDir = pathDir + "\\" + fl.No + "." + fl.Name;
                    System.IO.Directory.CreateDirectory(pathFlowDir);
                    Nodes nds = new Nodes(fl.No);
                    foreach (Node nd in nds)
                    {
                        MapData md = new MapData("ND" + nd.NodeID);
                        System.Data.DataSet ds = md.GenerHisDataSet();
                        ds.WriteXml(pathFlowDir + "\\" + nd.NodeID + "." + nd.Name + ".Frm.xml");
                    }
                }
            }

            // ���̱�ģ��.
            SysFormTrees frmSorts = new SysFormTrees();
            frmSorts.RetrieveAll();
            foreach (SysFormTree sort in frmSorts)
            {
                string pathDir = path + "\\Frm.��ģ��\\" + sort.No + "." + sort.Name;
                System.IO.Directory.CreateDirectory(pathDir);

                MapDatas mds = new MapDatas();
                mds.Retrieve(MapDataAttr.FK_FrmSort, sort.No);
                foreach (MapData md in mds)
                {
                    System.Data.DataSet ds = md.GenerHisDataSet();
                    ds.WriteXml(pathDir + "\\" + md.No + "." + md.Name + ".Frm.xml");
                }
            }
            return "���ɳɹ������" + path + "��<br>������빲�������ѹ�����͵�template��ccflow.org";
        }
    }
}
