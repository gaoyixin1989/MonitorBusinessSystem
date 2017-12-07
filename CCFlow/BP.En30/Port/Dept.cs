using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.Port
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DeptAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ���ڵ�ı��
        /// </summary>
        public const string ParentNo = "ParentNo";
    }
    /// <summary>
    /// ����
    /// </summary>
    public class Dept : EntityNoName
    {
        #region ����
        /// <summary>
        /// ���ڵ��ID
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
        public int Grade
        {
            get
            {
                return 1;
            }
        }
        private Depts _HisSubDepts = null;
        /// <summary>
        /// �����ӽڵ�
        /// </summary>
        public Depts HisSubDepts
        {
            get
            {
                if (_HisSubDepts == null)
                    _HisSubDepts = new Depts(this.No);
                return _HisSubDepts;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public Dept() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="no">���</param>
        public Dept(string no) : base(no) { }
        #endregion

        #region ��д����
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

                map.EnDesc = "����"; //  ʵ�������.
                map.DepositaryOfEntity = Depositary.Application; //ʵ��map�Ĵ��λ��.
                map.DepositaryOfMap = Depositary.Application;    // Map �Ĵ��λ��.

                map.AddTBStringPK(DeptAttr.No, null, "���", true, false, 1, 50, 20);
                map.AddTBString(DeptAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(DeptAttr.ParentNo, null, "���ڵ���", true, true, 0, 100, 30);

                #region ���ӵ�Զ�����
                //���Ĳ���Ȩ��
               // map.AttrsOfOneVSM.Add(new DeptStations(), new Stations(), DeptStationAttr.FK_Dept, DeptStationAttr.FK_Station, StationAttr.Name, StationAttr.No, "��λȨ��");
                #endregion 

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    ///����s
    /// </summary>
    public class Depts : EntitiesNoName
    {
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
        /// ���ż���
        /// </summary>
        public Depts()
        {

        }
        /// <summary>
        /// ���ż���
        /// </summary>
        /// <param name="parentNo">������No</param>
        public Depts(string parentNo)
        {
            this.Retrieve(DeptAttr.ParentNo, parentNo);
        }
    }
}
