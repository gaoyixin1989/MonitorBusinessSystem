using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
// using Security.Principal.WindowsIdentity;

namespace BP.WF.DTS
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class DTSDominInfo : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public DTSDominInfo()
        {
            this.Title = "����������";
            this.Help = "����������(δ���)";
           // this.HisAttrs.AddTBString("Path", "C:\\ccflow.Template", "���ɵ�·��", true, false, 1, 1900, 200);
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
            return "����δʵ�֡�";

            string domainHost = "127.0.0.1";

            string sqls = "";
            sqls += "@DELETE FROM Port_Emp";
            sqls += "@DELETE FROM Port_Dept";
            sqls += "@DELETE FROM Port_Station";
            sqls += "@DELETE FROM Port_EmpStation";
            sqls += "@DELETE FROM Port_EmpDept";
            DBAccess.RunSQLs(sqls);

           
            // �Ѳ��ŵ�������ȥ��

            //DirectoryEntry de = new DirectoryEntry("LDAP://" + domain, name, pass);
            //DirectorySearcher srch = new DirectorySearcher();
            //srch.Filter = ("(objectclass=User)");

            //srch.SearchRoot = de;
            //srch.SearchScope = SearchScope.Subtree;
            //srch.PropertiesToLoad.Add("sn");
            //srch.PropertiesToLoad.Add("givenName");
            //srch.PropertiesToLoad.Add("uid");
            //srch.PropertiesToLoad.Add("telephoneNumber");
            //srch.PropertiesToLoad.Add("employeeNumber");
            //foreach (SearchResult res in srch.FindAll())
            //{
            //    string[] strArray;
            //    string str;
            //    str = "";
            //    strArray = res.Path.Split(',');
            //    for (int j = strArray.Length; j > 0; j--)
            //    {
            //        if (strArray[j - 1].Substring(0, 3) == "OU=")
            //        {
            //            str = "��" + strArray[j - 1].Replace("OU=", "");
            //        }
            //    }
            //}

            return "���ɳɹ������ ��<br>������빲�������ѹ�����͵�template��ccflow.org";
        }
    }
}
