using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// ѡ�����������
	/// </summary>
    public class SelectAccperAttr
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ����Ա����
        /// </summary>
        public const string EmpName = "EmpName";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// �������  ��Ϣ
        /// </summary>
        public const string Info = "Info";
        /// <summary>
        /// �Ժ����Ƿ񰴴˼���
        /// </summary>
        public const string IsRemember = "IsRemember";
        /// <summary>
        /// ˳���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// ����(@0=������@1=������)
        /// </summary>
        public const string AccType = "AccType";
        /// <summary>
        /// ά�ȱ��
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// ʱ����
        /// </summary>
        public const string TSpanDay = "TSpanDay";
        /// <summary>
        /// ʱ��Сʱ
        /// </summary>
        public const string TSpanHour = "TSpanHour";
    }
	/// <summary>
	/// ѡ�������
	/// �ڵ�ĵ���Ա�����������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
    public class SelectAccper : EntityMyPK
    {
        #region ��������
        /// <summary>
        ///����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(SelectAccperAttr.WorkID);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.WorkID, value);
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(SelectAccperAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(SelectAccperAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStringByKey(SelectAccperAttr.Tag);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.Tag, value);
            }
        }
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string EmpName
        {
            get
            {
                string s= this.GetValStringByKey(SelectAccperAttr.EmpName);
                if (s == "" || s == null)
                    s = this.FK_Emp;
                return s;
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.EmpName, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(SelectAccperAttr.Rec);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.Rec, value);
            }
        }
        /// <summary>
        /// �������  ��Ϣ
        /// </summary>
        public string Info
        {
            get
            {
                return this.GetValStringByKey(SelectAccperAttr.Info);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.Info, value);
            }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsRemember
        {
            get
            {
                return this.GetValBooleanByKey(SelectAccperAttr.IsRemember);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.IsRemember, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(SelectAccperAttr.Idx);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.Idx, value);
            }
        }
        /// <summary>
        ///  ����(@0=������@1=������)
        /// </summary>
        public int AccType
        {
            get
            {
                return this.GetValIntByKey(SelectAccperAttr.AccType);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.AccType, value);
            }
        }
        /// <summary>
        /// ʱ��
        /// </summary>
        public float TSpanHour
        {
            get
            {
                return this.GetValFloatByKey(SelectAccperAttr.TSpanHour);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.TSpanHour, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ѡ�������
        /// </summary>
        public SelectAccper()
        {
        }
        public SelectAccper(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
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

                Map map = new Map("WF_SelectAccper");
                map.EnDesc = "ѡ�����/��������Ϣ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();

                map.AddTBInt(SelectAccperAttr.FK_Node, 0, "�����˽ڵ�", true, false);
                map.AddTBInt(SelectAccperAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBString(SelectAccperAttr.FK_Emp, null, "FK_Emp", true, false, 0, 20, 10);
                map.AddTBString(SelectAccperAttr.EmpName, null, "EmpName", true, false, 0, 20, 10);

                map.AddTBInt(SelectAccperAttr.AccType, 0, "����(@0=������@1=������)", true, false);
                map.AddTBString(SelectAccperAttr.Rec, null, "��¼��", true, false, 0, 20, 10);
                map.AddTBString(SelectAccperAttr.Info, null, "���������Ϣ", true, false, 0, 200, 10);

                map.AddTBInt(SelectAccperAttr.IsRemember, 0, "�Ժ����Ƿ񰴱��μ���", true, false);
                map.AddTBInt(SelectAccperAttr.Idx, 0, "˳���(�����������̶������ģʽ)", true, false);
                /*
                 *  add 2015-1-12.
                 * Ϊ�˽����ά�ȵ���Ա����.
                 * �ڷ��������·���ʱ, һ���˿��Է����������񣬵������������Ҫһ��ά�������֡�
                 * ���ά�ȣ��п�����һ��������Ρ�
                 */
                map.AddTBString(SelectAccperAttr.Tag, null, "ά����ϢTag", true, false, 0, 200, 10);

                map.AddTBInt(SelectAccperAttr.TSpanDay, 0, "ʱ��-��", true, false);
                map.AddTBFloat(SelectAccperAttr.TSpanHour, 0, "ʱ��-Сʱ", true, false);

                

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeInsert()
        {
            this.ResetPK();
            return base.beforeInsert();
        }

        public void ResetPK()
        {
            this.MyPK = this.FK_Node + "_" + this.WorkID + "_" + this.FK_Emp;
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.ResetPK();
            this.Rec = BP.Web.WebUser.No;
            return base.beforeUpdateInsertAction();
        }
        //protected override bool beforeUpdateInsertAction()
        //{
        //    this.Rec = BP.Web.WebUser.No;
        //    return base.beforeUpdateInsertAction();
        //}
    }
	/// <summary>
	/// ѡ�������
	/// </summary>
    public class SelectAccpers : EntitiesMyPK
    {
        /// <summary>
        /// �Ƿ�����´�ѡ��
        /// </summary>
        public bool IsSetNextTime
        {
            get
            {
                if (this.Count == 0)
                    return false;

                foreach (SelectAccper item in this)
                {
                    if (item.IsRemember == true)
                        return item.IsRemember;
                }
                return false;
            }
        }
        /// <summary>
        /// ��ѯ������,���û�����þͲ�ѯ��ʷ��¼���õĽ�����.
        /// </summary>
        /// <param name="fk_node"></param>
        /// <param name="Rec"></param>
        /// <returns></returns>
        public int QueryAccepter(int fk_node, string rec, Int64 workid)
        {
            //��ѯ������ǰ������.
            int i = this.Retrieve(SelectAccperAttr.FK_Node, fk_node,
                 SelectAccperAttr.WorkID, workid);
            if (i != 0)
                return i; /*���û�о�������workid.*/

            //�ҳ�����Ĺ���ID
            int maxWorkID = BP.DA.DBAccess.RunSQLReturnValInt("SELECT Max(WorkID) FROM WF_SelectAccper WHERE Rec='" + rec + "' AND FK_Node=" + fk_node, 0);
            if (maxWorkID == 0)
                return 0;

            //��ѯ����������.
            this.Retrieve(SelectAccperAttr.FK_Node, fk_node,
           SelectAccperAttr.WorkID, maxWorkID);

            //���ز�ѯ���.
            return this.Count;
        }
        /// <summary>
        /// ��ѯ�ϴε�����
        /// </summary>
        /// <param name="fk_node">�ڵ���</param>
        /// <param name="rec">��ǰ��Ա</param>
        /// <param name="workid">����ID</param>
        /// <returns></returns>
        public int QueryAccepterPriSetting(int fk_node)
        {
            //�ҳ�����Ĺ���ID.
            int maxWorkID = BP.DA.DBAccess.RunSQLReturnValInt("SELECT Max(WorkID) FROM WF_SelectAccper WHERE " + SelectAccperAttr.IsRemember + "=1 AND Rec='" + BP.Web.WebUser.No + "' AND FK_Node=" + fk_node, 0);
            if (maxWorkID == 0)
                return 0;

            //��ѯ����������.
            this.Retrieve(SelectAccperAttr.FK_Node, fk_node,
           SelectAccperAttr.WorkID, maxWorkID);

            //���ز�ѯ���.
            return this.Count;
        }
        /// <summary>
        /// ���ĵ���Ա
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (SelectAccper ns in this)
                {
                    ens.AddEntity(new Emp(ns.FK_Emp));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (SelectAccper ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// ѡ�������
        /// </summary>
        public SelectAccpers() { }
        /// <summary>
        /// ��ѯ����ѡ�����Ա
        /// </summary>
        /// <param name="fk_flow"></param>
        /// <param name="workid"></param>
        public SelectAccpers( Int64 workid)
        {
            BP.En.QueryObject qo = new QueryObject(this);
            qo.AddWhere(SelectAccperAttr.WorkID, workid);
            qo.addOrderByDesc(SelectAccperAttr.FK_Node,SelectAccperAttr.Idx);
            qo.DoQuery();

          //  this.Retrieve(SelectAccperAttr.WorkID, workid, SelectAccperAttr.Idx);
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SelectAccper();
            }
        }
    }
}
