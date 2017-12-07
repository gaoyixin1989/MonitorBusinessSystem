using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ���Ų�ѯȨ��
    /// </summary>
    public class DeptSearchScorpAttr
    {
        #region ��������
        /// <summary>
        /// ������ԱID
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        #endregion
    }
    /// <summary>
    /// ���Ų�ѯȨ�� ��ժҪ˵����
    /// </summary>
    public class DeptSearchScorp : Entity
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
        /// ������ԱID
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DeptSearchScorpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptSearchScorpAttr.FK_Emp, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(DeptSearchScorpAttr.FK_Dept);
            }
        }
        /// <summary>
        ///����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptSearchScorpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptSearchScorpAttr.FK_Dept, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ������Ա��λ
        /// </summary> 
        public DeptSearchScorp() { }
        /// <summary>
        /// ������Ա���Ŷ�Ӧ
        /// </summary>
        /// <param name="_empoid">������ԱID</param>
        /// <param name="wsNo">���ű��</param> 	
        public DeptSearchScorp(string _empoid, string wsNo)
        {
            this.FK_Emp = _empoid;
            this.FK_Dept = wsNo;
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

                Map map = new Map("Port_DeptSearchScorp");
                map.EnDesc = "���Ų�ѯȨ��";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(DeptSearchScorpAttr.FK_Emp, null, "����Ա", true, true, 1, 50, 11);
                map.AddDDLEntitiesPK(DeptSearchScorpAttr.FK_Dept, null, "����", new Depts(), true);
                // map.AddDDLEntitiesPK(DeptSearchScorpAttr.FK_Emp, null, "����Ա", new Emps(), true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ���ػ��෽��
        /// <summary>
        /// ����ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeInsert()
        {
            return base.beforeInsert();
        }
        /// <summary>
        /// ����ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeUpdate()
        {
            return base.beforeUpdate();
        }
        /// <summary>
        /// ɾ��ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeDelete()
        {
            return base.beforeDelete();
        }
        #endregion
    }
    /// <summary>
    /// ���Ų�ѯȨ�� 
    /// </summary>
    public class DeptSearchScorps : Entities
    {
        #region ����
        /// <summary>
        /// ���Ų�ѯȨ��
        /// </summary>
        public DeptSearchScorps() { }
        /// <summary>
        /// ���Ų�ѯȨ��
        /// </summary>
        /// <param name="FK_Emp">FK_Emp</param>
        public DeptSearchScorps(string FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptSearchScorpAttr.FK_Emp, FK_Emp);
            qo.DoQuery();
        }
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DeptSearchScorp();
            }
        }
        #endregion

        #region ��ѯ����

        #endregion

    }

}
