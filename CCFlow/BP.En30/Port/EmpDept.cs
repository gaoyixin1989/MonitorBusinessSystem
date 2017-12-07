using System;
using System.Data;
using BP.DA;
using BP.En;


namespace BP.Port
{
	/// <summary>
	/// ��Ա���Ŷ�Ӧ -����
	/// </summary>
	public class EmpDeptAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";		 
		#endregion	
	}
	/// <summary>
    /// ��Ա���Ŷ�Ӧ
	/// </summary>
	public class EmpDept :EntityMM
	{

		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				if (BP.Web.WebUser.No== "admin"   )
				{
					uac.IsView=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsAdjunct=true;
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
				return this.GetValStringByKey(EmpDeptAttr.FK_Emp);
			}
			set
			{
				SetValByKey(EmpDeptAttr.FK_Emp,value);
			}
		}
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(EmpDeptAttr.FK_Dept);
            }
        }
		/// <summary>
		///����
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(EmpDeptAttr.FK_Dept);
			}
			set
			{
				SetValByKey(EmpDeptAttr.FK_Dept,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public EmpDept(){}
		/// <summary>
		/// ������Ա���Ŷ�Ӧ
		/// </summary>
		/// <param name="_empoid">������ԱID</param>
		/// <param name="wsNo">���ű��</param> 	
		public EmpDept(string _empoid,string wsNo)
		{
			this.FK_Emp  = _empoid;
			this.FK_Dept = wsNo ;
			if (this.Retrieve()==0)
				this.Insert();
		}		
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("Port_EmpDept");
				map.EnDesc="������Ա���Ŷ�Ӧ��Ϣ";	
				map.EnType=EnType.Dot2Dot; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���

             //   map.AddTBStringPK(EmpDeptAttr.FK_Emp, null, "Emp", false, false, 1, 15,1);
                //map.AddTBStringPK(EmpDeptAttr.FK_Dept, null, "Dept", false, false, 1, 15,1);
                //map.AddDDLEntitiesPK(EmpDeptAttr.FK_Emp,null,"����Ա",new Emps(),true);

                map.AddTBStringPK(EmpDeptAttr.FK_Emp, null, "����Ա", false, false, 1, 20, 1);
				map.AddDDLEntitiesPK(EmpDeptAttr.FK_Dept,null,"����",new Depts(),true);


                //map.AddDDLEntitiesPK(EmpDeptAttr.FK_Emp,0, DataType.AppInt,"����Ա",new �ؾ�(),"OID","Name",true);
				//map.AddSearchAttr(EmpDeptAttr.FK_Emp);
				//map.AddSearchAttr(EmpDeptAttr.FK_Dept);

				this._enMap=map;
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
    /// ��Ա���Ŷ�Ӧs -���� 
	/// </summary>
	public class EmpDepts : EntitiesMM
	{
		#region ����
		/// <summary>
		/// ������Ա�벿�ż���
		/// </summary>
		public EmpDepts(){}
		/// <summary>
		/// ������Ա�벿�ż���
		/// </summary>
		/// <param name="FK_Emp">FK_Emp</param>
		public EmpDepts(string  FK_Emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpDeptAttr.FK_Emp, FK_Emp);
			qo.DoQuery();
		}
		#endregion

		#region ��д����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EmpDept();
			}
		}	
		#endregion 

		#region ��ѯ����
		/// <summary>
		/// ���Ŷ�Ӧ�Ľڵ�
		/// </summary>
		/// <param name="stationNo">���ű��</param>
		/// <returns>�ڵ�s</returns>
		public Emps GetHisEmps(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpDeptAttr.FK_Dept, stationNo );
			qo.DoQuery();

			Emps ens = new Emps();
			foreach(EmpDept en in this)
			{
				ens.AddEntity( new Emp(en.FK_Emp )) ;
			}
			return ens;
		}
		/// <summary>
		/// �õ����Ĳ���Ȩ��
		/// </summary>
		/// <param name="empid">empid </param>
		/// <returns>Depts</returns> 
		public Depts GetHisDepts(string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpDeptAttr.FK_Emp,empid);
			qo.DoQuery();
			Depts ens = new Depts();
			foreach(EmpDept en in this)
			{				 
				ens.AddEntity( new Dept(en.FK_Dept) ) ;				 
			}			 
			return ens;
		}
		#endregion
				
	}
	
}
