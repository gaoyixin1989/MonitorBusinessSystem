using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.WF.Port
{
    /// <summary>
    /// ���̲������ݲ�ѯȨ��
    /// </summary>
    public class DeptFlowSearchAttr
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
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        #endregion
    }
    /// <summary>
    /// ���̲������ݲ�ѯȨ�� ��ժҪ˵����
    /// </summary>
    public class DeptFlowSearch : EntityMyPK
    {
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
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
                return this.GetValStringByKey(DeptFlowSearchAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptFlowSearchAttr.FK_Emp, value);
            }
        }
        /// <summary>
        ///����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptFlowSearchAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptFlowSearchAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(DeptFlowSearchAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(DeptFlowSearchAttr.FK_Flow, value);
            }
        }
        #endregion
      

        #region ���캯��
        /// <summary>
        /// ���̲������ݲ�ѯȨ��
        /// </summary> 
        public DeptFlowSearch() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_DeptFlowSearch");
                map.EnDesc = "���̲������ݲ�ѯȨ��";
                map.AddMyPK();
                map.AddTBString(DeptFlowSearchAttr.FK_Emp, null, "����Ա", true, true, 1, 50, 11);
                map.AddTBString(DeptFlowSearchAttr.FK_Flow, null, "���̱��", true, true, 1, 50, 11);
                map.AddTBString(DeptFlowSearchAttr.FK_Dept, null, "���ű��", true, true, 1, 100, 11);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        
    }
    /// <summary>
    /// ���̲������ݲ�ѯȨ�� 
    /// </summary>
    public class DeptFlowSearchs : Entities
    {
        #region ����
        /// <summary>
        /// ���̲������ݲ�ѯȨ��
        /// </summary>
        public DeptFlowSearchs() { }
        /// <summary>
        /// ���̲������ݲ�ѯȨ��
        /// </summary>
        /// <param name="FK_Emp">FK_Emp</param>
        public DeptFlowSearchs(string FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptFlowSearchAttr.FK_Emp, FK_Emp);
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
                return new DeptFlowSearch();
            }
        }
        #endregion

        #region ��ѯ����
        #endregion
    }
}
