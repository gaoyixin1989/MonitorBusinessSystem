using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// abc_afs
	/// </summary>
    public class GroupEnsTemplateAttr : EntityOIDAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// ����
        /// </summary> 
        public const string Attrs = "Attrs";
        /// <summary>
        /// ������
        /// </summary>
        public const string OperateCol = "OperateCol";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// EnsName
        /// </summary>
        public const string EnsName = "EnsName";
    }
	/// <summary>
	/// ����ģ��
	/// </summary>
	public class GroupEnsTemplate: EntityOID
	{
		#region ��������
		/// <summary>
		/// ����������
		/// </summary>
		public string EnsName
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.EnsName) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.EnsName,value) ; 
			}		
		}
		/// <summary>
		/// ʵ������
		/// </summary>
		public string OperateCol
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.OperateCol ) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.OperateCol,value) ; 
			}
		}
		/// <summary>
		/// ����Դ
		/// </summary>
		public string Attrs
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.Attrs ) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.Attrs,value) ; 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.Name,value) ; 
			}
		}
		public string EnName
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.EnName ) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.EnName,value) ; 
			}
		}
		public string Rec
		{
			get
			{
				return this.GetValStringByKey(GroupEnsTemplateAttr.Rec ) ; 
			}
			set
			{
				this.SetValByKey(GroupEnsTemplateAttr.Rec,value) ; 
			}
		}
		 
		#endregion

		#region ���췽��

		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.IsUpdate=true;
				uac.IsView=true;
				return base.HisUAC;
			}
		}

		/// <summary>
		/// ϵͳʵ��
		/// </summary>
		public GroupEnsTemplate()
		{
		}
       
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("Sys_GroupEnsTemplate");
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="����ģ��";
				map.EnType=EnType.Sys;
				map.AddTBIntPKOID();
				map.AddTBString(GroupEnsTemplateAttr.EnName,null,"���",false,false,0,500,20);
				map.AddTBString(GroupEnsTemplateAttr.Name,null,"������",true,false,0,500,20);
				map.AddTBString(GroupEnsTemplateAttr.EnsName,null,"��������",false,true,0,90,10);
				map.AddTBString(GroupEnsTemplateAttr.OperateCol,null,"��������",false,true,0,90,10);
				map.AddTBString(GroupEnsTemplateAttr.Attrs,null,"��������",false,true,0,90,10);
				map.AddTBString(GroupEnsTemplateAttr.Rec,null,"��¼��",false,true,0,90,10);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ��ѯ����
		/// <summary>
		/// ����ģ��
		/// </summary>
		/// <param name="fk_emp">fk_emp</param>
		/// <param name="className">className</param>
		/// <param name="attrs">attrs</param>
		/// <returns>��ѯ���ظ���</returns>
		public int Search(string fk_emp, string className, string attrs)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GroupEnsTemplateAttr.Rec, fk_emp);
			qo.addAnd();
			qo.AddWhere(GroupEnsTemplateAttr.Attrs, className);
			qo.addAnd();
			qo.AddWhere(GroupEnsTemplateAttr.EnsName, className);
			return qo.DoQuery();
		}
		#endregion
	}	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class GroupEnsTemplates : EntitiesOID
	{		
		#region ����
		public GroupEnsTemplates()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="emp"></param>
		public GroupEnsTemplates(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GroupEnsTemplateAttr.Rec, emp);
			qo.addOr();
			qo.AddWhere(GroupEnsTemplateAttr.Rec,"admin");
			qo.DoQuery();

		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new GroupEnsTemplate();
			}

		}
		#endregion

		#region ��ѯ����
		 
		#endregion
		
	}
}
