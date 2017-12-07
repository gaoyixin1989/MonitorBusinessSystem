using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.WF.Template;
using BP.Port;

namespace BP.WF.Template
{
    /// <summary>
    /// ���Ϳ��Ʒ�ʽ
    /// </summary>
    public enum CtrlWay
    {
        /// <summary>
        /// ���ո�λ
        /// </summary>
        ByStation,
        /// <summary>
        /// ������
        /// </summary>
        ByDept,
        /// <summary>
        /// ����Ա
        /// </summary>
        ByEmp,
        /// <summary>
        /// ��SQL
        /// </summary>
        BySQL
    }
	/// <summary>
	/// ��������
	/// </summary>
    public class CCAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string CCTitle = "CCTitle";
        /// <summary>
        /// ��������
        /// </summary>
        public const string CCDoc = "CCDoc";
        /// <summary>
        /// ���Ϳ��Ʒ�ʽ
        /// </summary>
        public const string CCCtrlWay = "CCCtrlWay";
        /// <summary>
        /// ���Ͷ���
        /// </summary>
        public const string CCSQL = "CCSQL";
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
    public class CC : Entity
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public DataTable GenerCCers(Entity rpt)
        {
            string sql = "";
            switch (this.HisCtrlWay)
            {
                case CtrlWay.BySQL:
                    sql = this.CCSQL.Clone() as string;
                    sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                    sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                    sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                    if (sql.Contains("@"))
                    {
                        foreach (Attr attr in rpt.EnMap.Attrs)
                        {
                            if (sql.Contains("@") == false)
                                break;
                            sql = sql.Replace("@" + attr.Key, rpt.GetValStrByKey(attr.Key));
                        }
                    }
                    break;
                case CtrlWay.ByEmp:
                    sql = "SELECT No,Name FROM Port_Emp WHERE No IN (SELECT FK_Emp FROM WF_CCEmp WHERE FK_Node=" + this.NodeID + ")";
                    break;
                case CtrlWay.ByDept:
                    sql = "SELECT No,Name FROM Port_Emp WHERE No IN (SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_CCDept WHERE FK_Node=" + this.NodeID + "))";
                    break;
                case CtrlWay.ByStation:
                    sql = "SELECT No,Name FROM Port_Emp WHERE No IN (SELECT FK_Emp FROM " + BP.WF.Glo.EmpStation + " WHERE FK_Station IN ( SELECT FK_Station FROM WF_CCStation WHERE FK_Node=" + this.NodeID + "))";
                    break;
                default:
                    throw new Exception("δ������쳣");
            }
            DataTable dt= DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                throw new Exception("@���̽ڵ���ƴ���δ�ҵ�������Ա�����ͷ�ʽ:"+this.HisCtrlWay+" SQL:"+sql);

            return dt;
        }
        /// <summary>
        ///�ڵ�ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No != "admin")
                {
                    uac.IsView = false;
                    return uac;
                }
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// ���ͱ���
        /// </summary>
        public string CCTitle
        {
            get
            {
                string s= this.GetValStringByKey(CCAttr.CCTitle);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�ĳ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(CCAttr.CCTitle, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string CCDoc
        {
            get
            {
                string s = this.GetValStringByKey(CCAttr.CCDoc);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�ĳ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(CCAttr.CCDoc, value);
            }
        }
        /// <summary>
        /// ���Ͷ���
        /// </summary>
        public string CCSQL
        {
            get
            {
                string sql= this.GetValStringByKey(CCAttr.CCSQL);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("��", "'");
                sql = sql.Replace("��", "'");
                sql = sql.Replace("''", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(CCAttr.CCSQL, value);
            }
        }
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        public CtrlWay HisCtrlWay
        {
            get
            {
                return (CtrlWay)this.GetValIntByKey(CCAttr.CCCtrlWay);
            }
            set
            {
                this.SetValByKey(CCAttr.CCCtrlWay, (int)value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// CC
        /// </summary>
        public CC()
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Node");
                map.EnDesc = "���͹���";
                map.EnType = EnType.Admin;
                map.AddTBString(NodeAttr.Name, null, "�ڵ�����", true, true, 0, 100, 10, true);
                map.AddTBIntPK(NodeAttr.NodeID, 0,"�ڵ�ID", true, true);

                map.AddDDLSysEnum(CCAttr.CCCtrlWay, 0, "���Ʒ�ʽ",true, true,"CtrlWay");
                map.AddTBString(CCAttr.CCSQL, null, "SQL���ʽ", true, false, 0, 500, 10, true);
                map.AddTBString(CCAttr.CCTitle, null, "���ͱ���", true, false, 0, 500, 10,true);
                map.AddTBStringDoc(CCAttr.CCDoc, null, "��������(����������֧�ֱ���)", true, false,true);

                map.AddSearchAttr(CCAttr.CCCtrlWay);

                // ��ع��ܡ�
                map.AttrsOfOneVSM.Add(new BP.WF.Template.CCStations(), new BP.WF.Port.Stations(),
                    NodeStationAttr.FK_Node, NodeStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, "���͸�λ");

                map.AttrsOfOneVSM.Add(new BP.WF.Template.CCDepts(), new BP.WF.Port.Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name,
                DeptAttr.No,  "���Ͳ���" );

                map.AttrsOfOneVSM.Add(new BP.WF.Template.CCEmps(), new BP.WF.Port.Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                    DeptAttr.No, "������Ա");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����s
	/// </summary>
	public class CCs: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CC();
			}
		}
		/// <summary>
        /// ����
		/// </summary>
		public CCs(){} 		 
		#endregion
	}
}
