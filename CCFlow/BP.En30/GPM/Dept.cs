using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.GPM
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DeptAttr : EntityTreeAttr
    {
        /// <summary>
        /// ���Ÿ�����
        /// </summary>
        public const string Leader = "Leader";
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// ��λȫ��
        /// </summary>
        public const string NameOfPath = "NameOfPath";
    }
    /// <summary>
    /// ����
    /// </summary>
    public class Dept : EntityTree
    {
        #region ����
        /// <summary>
        /// ȫ��
        /// </summary>
        public  string NameOfPath
        {
            get
            {
                return this.GetValStrByKey(DeptAttr.NameOfPath);
            }
            set
            {
                this.SetValByKey(DeptAttr.NameOfPath, value);
            }
        }
        /// <summary>
        /// ���ڵ��ID
        /// </summary>
        public new string ParentNo
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
        /// <summary>
        /// �쵼
        /// </summary>
        public string Leader
        {
            get
            {
                return this.GetValStrByKey(DeptAttr.Leader);
            }
            set
            {
                this.SetValByKey(DeptAttr.Leader, value);
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

                map.AddTBStringPK(DeptAttr.No, null, "���", true, true, 1, 50, 20);

                //����xx�ֹ�˾����
                map.AddTBString(DeptAttr.Name, null, "����", true, false, 0, 100, 30);

                //����:\\�۳Ҽ���\\�Ϸ��ֹ�˾\\����
                map.AddTBString(DeptAttr.NameOfPath, null, "����·��", false, false, 0, 300, 30);

                map.AddTBString(DeptAttr.ParentNo, null, "���ڵ���", false, false, 0, 100, 30);
                map.AddTBString(DeptAttr.TreeNo, null, "�����", false, false, 0, 100, 30);
                map.AddTBString(DeptAttr.Leader, null, "�쵼", false, false, 0, 100, 30);

                //����: ���񲿣���������������Դ��.
                map.AddTBString(DeptAttr.Tel, null, "��ϵ�绰", false, false, 0, 100, 30);

                map.AddTBInt(DeptAttr.Idx, 0, "Idx", false, false);
                map.AddTBInt(DeptAttr.IsDir, 0, "�Ƿ���Ŀ¼", false, false);

              //  map.AddDDLEntities(DeptAttr. null, "��������", new DeptTypes(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ���ɲ���ȫ����.
        /// </summary>
        public void GenerNameOfPath()
        {
            string name = this.Name;
            Dept dept = new Dept(this.ParentNo);
            while (true)
            {
                if (dept.IsRoot)
                    break;
                name = dept.Name + "\\\\" + name;
            }
            this.NameOfPath = name;
            this.DirectUpdate();
        }
    }
    /// <summary>
    ///�õ�����
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
