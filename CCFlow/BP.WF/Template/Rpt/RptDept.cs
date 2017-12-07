using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Rpt
{
	/// <summary>
	/// ������
	/// </summary>
	public class RptDeptAttr  
	{
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
		public const  string FK_Rpt="FK_Rpt";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";		 
		#endregion	
	}
	/// <summary>
	/// RptDept ��ժҪ˵����
	/// </summary>
	public class RptDept :Entity
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
		/// ����ID
		/// </summary>
		public string FK_Rpt
		{
			get
			{
				return this.GetValStringByKey(RptDeptAttr.FK_Rpt);
			}
			set
			{
				SetValByKey(RptDeptAttr.FK_Rpt,value);
			}
		}
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(RptDeptAttr.FK_Dept);
            }
        }
		/// <summary>
		///����
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(RptDeptAttr.FK_Dept);
			}
			set
			{
				SetValByKey(RptDeptAttr.FK_Dept,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �����λ
		/// </summary> 
		public RptDept(){}
		/// <summary>
		/// �����Ŷ�Ӧ
		/// </summary>
		/// <param name="_empoid">����ID</param>
		/// <param name="wsNo">���ű��</param> 	
		public RptDept(string _empoid,string wsNo)
		{
			this.FK_Rpt  = _empoid;
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
				
				Map map = new Map("Sys_RptDept");
				map.EnDesc="�����Ŷ�Ӧ��Ϣ";	
				map.EnType=EnType.Dot2Dot;

                map.AddTBStringPK(RptDeptAttr.FK_Rpt, null, "����", false, false, 1, 15, 1);
				map.AddDDLEntitiesPK(RptDeptAttr.FK_Dept,null,"����",new Depts(),true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// ������ 
	/// </summary>
	public class RptDepts : Entities
	{
		#region ����
		/// <summary>
		/// �����벿�ż���
		/// </summary>
		public RptDepts(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RptDept();
			}
		}	
		#endregion 
	}
}
