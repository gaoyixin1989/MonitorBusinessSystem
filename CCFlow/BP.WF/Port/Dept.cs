using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.WF.Port
{
	/// <summary>
	/// ��������
	/// </summary>
    public class DeptAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public const string ParentNo = "ParentNo";
        ///// <summary>
        ///// ������λ
        ///// </summary>
        //public const string FK_Unit = "FK_Unit";
    }
	/// <summary>
	/// ����
	/// </summary>
	public class Dept:EntityNoName
	{
		#region ����
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStrByKey(DeptAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(DeptAttr.ParentNo, value);
            }
        }
         
		#endregion

		#region ���캯��
		/// <summary>
		/// ����
		/// </summary>
		public Dept(){}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="no">���</param>
        public Dept(string no) : base(no){}
		#endregion

		#region ��д����
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //���ӵ����Ǹ����ݿ���. (Ĭ�ϵ���: AppCenterDSN )
                map.PhysicsTable = "Port_Dept";
                map.EnType = EnType.Admin;

                map.EnDesc = "����"; // "����";// ʵ�������.
                map.DepositaryOfEntity = Depositary.Application; //ʵ��map�Ĵ��λ��.
                map.DepositaryOfMap = Depositary.Application;    // Map �Ĵ��λ��.

                map.AdjunctType = AdjunctType.None;

                map.AddTBStringPK(DeptAttr.No, null, "���", true, false, 1, 30, 40);
                map.AddTBString(DeptAttr.Name, null,"����", true, false, 0, 60, 200);
                map.AddTBString(DeptAttr.ParentNo, null, "���ڵ���", true, false, 0, 30, 40);
              //  map.AddTBString(DeptAttr.FK_Unit, "1", "������λ", false, false, 0, 50, 10);
                
                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
	}
	/// <summary>
	///���ż���
	/// </summary>
    public class Depts : EntitiesNoName
    {
        /// <summary>
        /// ��ѯȫ����
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            if (BP.Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptAttr.No, " = ", BP.Web.WebUser.FK_Dept);
            qo.addOr();
            qo.AddWhere(DeptAttr.ParentNo, " = ", BP.Web.WebUser.FK_Dept);
            return qo.DoQuery();
        }
        /// <summary>
        /// �õ�һ����ʵ��
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Dept();
            }
        }
        /// <summary>
        /// create ens
        /// </summary>
        public Depts()
        {
        }
    }
}
