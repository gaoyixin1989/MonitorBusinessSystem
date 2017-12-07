using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
	/// <summary>
	/// ���Ÿ�λ��Ա��Ӧ
	/// </summary>
	public class DeptEmpStationAttr
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// ��λ
		/// </summary>
		public const  string FK_Station="FK_Station";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
		#endregion	
	}
	/// <summary>
    /// ���Ÿ�λ��Ա��Ӧ ��ժҪ˵����
	/// </summary>
    public class DeptEmpStation : EntityMyPK
    {
        #region ��������
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
        /// ��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DeptEmpStationAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptEmpStationAttr.FK_Emp, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Emp+"_"+this.FK_Station;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptEmpStationAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptEmpStationAttr.FK_Dept, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Emp + "_" + this.FK_Station;
            }
        }
        public string FK_StationT
        {
            get
            {
                //return this.GetValRefTextByKey(DeptEmpStationAttr.FK_Station);

                return this.GetValStringByKey(DeptEmpStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///��λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(DeptEmpStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(DeptEmpStationAttr.FK_Station, value);
                this.MyPK = this.FK_Dept + "_" + this.FK_Emp + "_" + this.FK_Station;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �������Ÿ�λ��Ա��Ӧ
        /// </summary> 
        public DeptEmpStation() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_DeptEmpStation");
                map.EnDesc = "���Ÿ�λ��Ա��Ӧ";

                map.AddMyPK();

                map.AddTBString(DeptEmpStationAttr.FK_Dept, null, "����", false, false, 1, 50, 1);
                map.AddTBString(DeptEmpStationAttr.FK_Station, null, "��λ", false, false, 1, 50, 1);
                map.AddTBString(DeptEmpStationAttr.FK_Emp, null, "����Ա", false, false, 1, 50, 1);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ����ɾ��ǰ��������
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Dept + "_" + this.FK_Emp + "_" + this.FK_Station;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
    /// ���Ÿ�λ��Ա��Ӧ 
	/// </summary>
	public class DeptEmpStations : EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �������Ÿ�λ��Ա��Ӧ
		/// </summary>
		public DeptEmpStations()
		{
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
				return new DeptEmpStation();
			}
		}	
		#endregion 
		
	}
}
