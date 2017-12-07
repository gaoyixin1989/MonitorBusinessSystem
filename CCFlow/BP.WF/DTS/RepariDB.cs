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
    /// �޸����ݿ� ��ժҪ˵��
    /// </summary>
    public class RepariDB : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariDB()
        {
            this.Title = "�޸����ݿ�";
            this.Help = "�����µİ汾���뵱ǰ�����ݱ�ṹ����һ���Զ��޸�, �޸����ݣ�ȱ���У�ȱ����ע�ͣ���ע�Ͳ����������б仯��";
            this.Help += "<br>��Ϊ��������Ĵ��󣬶�ʧ���ֶΣ�ͨ����Ҳ�����Զ��޸���";
            this.Help += "<br><a href='/'>�������������</a>";
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
            string rpt =PubClass.DBRpt(BP.DA.DBCheckLevel.High);

            //// �ֶ�����. 2011-07-08 ����ڵ��ֶη���.
            //string sql = "DELETE FROM Sys_EnCfg WHERE No='BP.WF.Template.NodeSheet'";
            //BP.DA.DBAccess.RunSQL(sql);

            //sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Template.NodeSheet','NodeID=��������@WarningHour=��������@SendLab=���ܰ�ť��ǩ��״̬')";
            //BP.DA.DBAccess.RunSQL(sql);

            // �޸���bug��ʧ���ֶ�.
            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData md in mds)
            {
                string nodeid = md.No.Replace("ND","");
                try
                {
                    BP.WF.Node nd = new Node(int.Parse(nodeid));
                    nd.RepareMap();
                    continue;
                }
                catch(Exception ex)
                {

                }

                MapAttr attr = new MapAttr();
                if (attr.IsExit(MapAttrAttr.KeyOfEn, "OID", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr.FK_MapData = md.No;
                    attr.KeyOfEn = "OID";
                    attr.Name = "OID";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "0";
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.Insert();
                }
            }
            return "ִ�гɹ�...";
        }
    }
}
