using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
	/// <summary>
	/// ���Ÿ�λ��Ӧ
	/// </summary>
	public class DeptStationAttr  
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
		#endregion	
	}
	/// <summary>
    /// ���Ÿ�λ��Ӧ ��ժҪ˵����
	/// </summary>
    public class DeptStation : Entity
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
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptStationAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptStationAttr.FK_Dept, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(DeptStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///��λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(DeptStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(DeptStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// �������Ÿ�λ��Ӧ
        /// </summary> 
        public DeptStation() { }
        /// <summary>
        /// ������Ա��λ��Ӧ
        /// </summary>
        /// <param name="_empoid">����</param>
        /// <param name="wsNo">��λ���</param> 	
        public DeptStation(string _empoid, string wsNo)
        {
            this.FK_Dept = _empoid;
            this.FK_Station = wsNo;
            if (this.Retrieve(DeptStationAttr.FK_Dept, this.FK_Dept, DeptStationAttr.FK_Station, this.FK_Station) == 0)
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

                Map map = new Map("Port_DeptStation");
                map.EnDesc = "���Ÿ�λ��Ӧ";
                map.EnType = EnType.Dot2Dot; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���

                map.AddTBStringPK(DeptStationAttr.FK_Dept, null, "����Ա", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(DeptStationAttr.FK_Station, null, "��λ", new Stations(), true);
                map.AddSearchAttr(DeptStationAttr.FK_Station);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ���Ÿ�λ��Ӧ 
	/// </summary>
	public class DeptStations : Entities
	{
		#region ����
		/// <summary>
		/// �������Ÿ�λ��Ӧ
		/// </summary>
		public DeptStations()
		{
		}
		/// <summary>
		/// ������Ա���λ����
		/// </summary>
		public DeptStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DeptStationAttr.FK_Station, stationNo);
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
				return new DeptStation();
			}
		}	
		#endregion 

		#region ��ѯ����
		/// <summary>
		/// ��λ��Ӧ�Ľڵ�
		/// </summary>
		/// <param name="stationNo">��λ���</param>
		/// <returns>�ڵ�s</returns>
		public Emps GetHisEmps(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DeptStationAttr.FK_Station, stationNo );
			qo.addOrderBy(DeptStationAttr.FK_Station);
			qo.DoQuery();

			Emps ens = new Emps();
			foreach(DeptStation en in this)
				ens.AddEntity( new Emp(en.FK_Dept)) ;
			
			return ens;
		}
		/// <summary>
		/// �������Ÿ�λ��Ӧs
		/// </summary>
		/// <param name="empId">empId</param>
		/// <returns>�������Ÿ�λ��Ӧs</returns> 
		public Stations GetHisStations(string empId)
		{
			Stations ens = new Stations();
			if ( Cash.IsExits("DeptStationsOf"+empId, Depositary.Application))
			{
				return (Stations)Cash.GetObjFormApplication("DeptStationsOf"+empId,null );				 
			}
			else
			{
				QueryObject qo = new QueryObject(this);
				qo.AddWhere(DeptStationAttr.FK_Dept,empId);
				qo.addOrderBy(DeptStationAttr.FK_Station);
				qo.DoQuery();				
				foreach(DeptStation en in this)
					ens.AddEntity( new Station(en.FK_Station) ) ;
				Cash.AddObj("DeptStationsOf"+empId,Depositary.Application,ens);
				return ens;
			}
		}
		#endregion
	}
}
