using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.WF.Port
{
	/// <summary>
	/// ��Ա��λ
	/// </summary>
	public class EmpStationAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// ��Ա��λ ��ժҪ˵����
	/// </summary>
    public class EmpStation : Entity
    {
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


        #region ��������
        /// <summary>
        /// ������ԱID
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(EmpStationAttr.FK_Emp);
            }
            set
            {
                SetValByKey(EmpStationAttr.FK_Emp, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(EmpStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(EmpStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(EmpStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ������Ա��λ
        /// </summary> 
        public EmpStation() { }
        /// <summary>
        /// ������Ա������λ��Ӧ
        /// </summary>
        /// <param name="_empoid">������ԱID</param>
        /// <param name="wsNo">������λ���</param> 	
        public EmpStation(string _empoid, string wsNo)
        {
            this.FK_Emp = _empoid;
            this.FK_Station = wsNo;
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

                Map map = new Map("Port_EmpStation");
                map.EnDesc = "��Ա��λ";
                map.EnType = EnType.Dot2Dot; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���

              //  map.AddDDLEntitiesPK(EmpStationAttr.FK_Emp, null, "����Ա", new Emps(), true);

                map.AddTBStringPK(EmpDeptAttr.FK_Emp, null, "����Ա", false, false, 1, 15, 1);

                map.AddDDLEntitiesPK(EmpStationAttr.FK_Station, null, "������λ", new Stations(), true);

                map.AddSearchAttr(EmpStationAttr.FK_Station);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��Ա��λ 
	/// </summary>
	public class EmpStations : Entities
	{
		#region ����
		/// <summary>
		/// ������Ա��λ
		/// </summary>
		public EmpStations()
		{
		}
		/// <summary>
		/// ������Ա�빤����λ����
		/// </summary>
		public EmpStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpStationAttr.FK_Station, stationNo);
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
				return new EmpStation();
			}
		}	
		#endregion 

		#region ��ѯ����
		/// <summary>
		/// ������λ��Ӧ�Ľڵ�
		/// </summary>
		/// <param name="stationNo">������λ���</param>
		/// <returns>�ڵ�s</returns>
		public Emps GetHisEmps(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpStationAttr.FK_Station, stationNo );
			qo.addOrderBy(EmpStationAttr.FK_Station);
			qo.DoQuery();

			Emps ens = new Emps();
			foreach(EmpStation en in this)
				ens.AddEntity( new Emp(en.FK_Emp)) ;
			
			return ens;
		}
		/// <summary>
		/// ������Ա��λs
		/// </summary>
		/// <param name="empId">empId</param>
		/// <returns>������Ա��λs</returns> 
		public Stations GetHisStations(string empId)
		{
			Stations ens = new Stations();
			if ( Cash.IsExits("EmpStationsOf"+empId, Depositary.Application))
			{
				return (Stations)Cash.GetObjFormApplication("EmpStationsOf"+empId,null );				 
			}
			else
			{
				QueryObject qo = new QueryObject(this);
				qo.AddWhere(EmpStationAttr.FK_Emp,empId);
				qo.addOrderBy(EmpStationAttr.FK_Station);
				qo.DoQuery();				
				foreach(EmpStation en in this)
					ens.AddEntity( new Station(en.FK_Station) ) ;
				Cash.AddObj("EmpStationsOf"+empId,Depositary.Application,ens);
				return ens;
			}
		}
		#endregion
	}
}
