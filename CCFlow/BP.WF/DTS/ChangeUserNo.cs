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
    /// �޸���Ա��� ��ժҪ˵��
    /// </summary>
    public class ChangeUserNo : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ChangeUserNo()
        {
            this.Title = "�޸���Ա��ţ�ԭ��һ�������б�Ž�A,�����޸ĳ�B��";
            this.Help = "������ִ�У�ִ��ǰ���ȱ������ݿ⣬ϵͳ������ɵ�SQL������־�����־�ļ�(" + BP.Sys.SystemConfig.PathOfDataUser + "\\Log)��Ȼ���ҵ���Щsql.";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            this.Warning = "��ȷ��Ҫִ����";
            HisAttrs.AddTBString("P1", null, "ԭ�û���", true, false, 0, 10, 10);
            HisAttrs.AddTBString("P2", null, "���û���", true, false, 0, 10, 10);
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
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string oldNo = this.GetValStrByKey("P1");
            string newNo = this.GetValStrByKey("P2");

            string sqls = "";

            sqls += "UPDATE Port_Emp Set No='" + newNo + "' WHERE No='" + oldNo + "'";
            sqls += "\t\n UPDATE Port_EmpDept Set FK_Emp='" + newNo + "' WHERE FK_Emp='" + oldNo + "'";
            sqls += "\t\n UPDATE " + BP.WF.Glo.EmpStation + " Set FK_Emp='" + newNo + "' WHERE FK_Emp='" + oldNo + "'";

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();

            foreach (MapData md in mds)
            {
                MapAttrs attrs = new MapAttrs(md.No);
                foreach (MapAttr attr in attrs)
                {
                    if (attr.UIIsEnable == false && attr.DefValReal == "@WebUser.No")
                    {
                        sqls += "\t\n UPDATE " + md.PTable + " SET ";
                    }
                    continue;

                }
                sqls += "UPDATE";

            }

            return "ִ�гɹ�..." + sqls;
        }
    }
}
