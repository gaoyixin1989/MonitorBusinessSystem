using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Rpt
{
    /// <summary>
    /// ������Ա
    /// </summary>
    public class RptEmpAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FK_Rpt = "FK_Rpt";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        #endregion
    }
    /// <summary>
    /// RptEmp ��ժҪ˵����
    /// </summary>
    public class RptEmp : Entity
    {

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsView = true;
                    uac.IsDelete = true;
                    uac.IsInsert = true;
                    uac.IsUpdate = true;
                    uac.IsAdjunct = true;
                }
                return uac;
            }
        }

        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Rpt, value);
            }
        }
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(RptEmpAttr.FK_Emp);
            }
        }
        /// <summary>
        ///��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ������Ա
        /// </summary> 
        public RptEmp() { }
        /// <summary>
        /// ������Ա��Ӧ
        /// </summary>
        /// <param name="_empoid">����ID</param>
        /// <param name="wsNo">��Ա���</param> 	
        public RptEmp(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Emp = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("Sys_RptEmp");
                map.EnDesc = "������Ա��Ӧ��Ϣ";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(RptEmpAttr.FK_Rpt, null, "����", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(RptEmpAttr.FK_Emp, null, "��Ա", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ������Ա 
    /// </summary>
    public class RptEmps : Entities
    {
        #region ����
        /// <summary>
        /// ��������Ա����
        /// </summary>
        public RptEmps() { }
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new RptEmp();
            }
        }
        #endregion
    }
}
