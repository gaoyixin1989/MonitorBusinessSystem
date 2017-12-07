using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// �ٷֱ���ʾ��ʽ
	/// </summary>
	public enum PercentModel
	{
		/// <summary>
		/// ����ʾ
		/// </summary>
		None,
		/// <summary>
		/// ����
		/// </summary>
		Vertical,
		/// <summary>
		/// ����
		/// </summary>
		Transverse,
	}
	/// <summary>
	/// RptTemplateAttr
	/// </summary>
    public class RptTemplateAttr : EntityOIDAttr
    {
        /// <summary>
        /// ������
        /// </summary>
        public const string EnsName = "EnsName";
        /// <summary>
        /// ����
        /// </summary>
        public const string MyPK = "MyPK";
        /// <summary>
        /// D1
        /// </summary> 
        public const string D1 = "D1";
        /// <summary>
        /// d2
        /// </summary>
        public const string D2 = "D2";
        /// <summary>
        /// d3
        /// </summary>
        public const string D3 = "D3";
        /// <summary>
        /// Ҫ�����Ķ���s
        /// </summary>
        public const string AlObjs = "AlObjs";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Height = "Height";
        /// <summary>
        /// EnsName
        /// </summary>
        public const string Width = "Width";
        /// <summary>
        /// �Ƿ���ʾ��ϼ�
        /// </summary>
        public const string IsSumBig = "IsSumBig";
        /// <summary>
        /// �Ƿ���ʾС�ϼ�
        /// </summary>
        public const string IsSumLittle = "IsSumLittle";
        /// <summary>
        /// �Ƿ���ʾ�Һϼ�
        /// </summary>
        public const string IsSumRight = "IsSumRight";
        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        public const string PercentModel = "PercentModel";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
    }
	/// <summary>
	/// ����ģ��
	/// </summary>
	public class RptTemplate: Entity
	{
		#region ��������
		/// <summary>
		/// ����������
		/// </summary>
		public string EnsName
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.EnsName) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.EnsName,value) ; 
			}		
		}
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RptTemplateAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(RptTemplateAttr.FK_Emp, value);
            }
        }
		/// <summary>
		/// ����
		/// </summary>
		public string MyPK
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.MyPK ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.MyPK,value) ; 
			}
		}		 
		/// <summary>
		/// D1
		/// </summary>
		public string D1
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.D1) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.D1,value) ; 
			}
		}
		/// <summary>
		/// D2
		/// </summary>
		public string D2
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.D2) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.D2,value) ; 
			}
		}
		/// <summary>
		/// D3
		/// </summary>
		public string D3
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.D3) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.D3,value) ; 
			}
		}
		public string AlObjsText
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.AlObjs ) ; 
			}
		}
		/// <summary>
		/// �����Ķ���
		/// ���ݸ�ʽ @��������1@��������2@��������3@
		/// </summary>
		public string AlObjs
		{
			get
			{
				return this.GetValStringByKey(RptTemplateAttr.AlObjs) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.AlObjs,value) ; 
			}
		}
		public int Height
		{
			get
			{
				return this.GetValIntByKey(RptTemplateAttr.Height ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.Height,value) ; 
			}
		}
		public int Width
		{
			get
			{
				return this.GetValIntByKey(RptTemplateAttr.Width ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.Width,value) ; 
			}
		}
		/// <summary>
		/// �Ƿ���ʾ��ϼ�
		/// </summary>
		public bool IsSumBig
		{
			get
			{
				return this.GetValBooleanByKey(RptTemplateAttr.IsSumBig ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.IsSumBig,value) ; 
			}
		}
		/// <summary>
		/// С�ϼ�
		/// </summary>
		public bool IsSumLittle
		{
			get
			{
				return this.GetValBooleanByKey(RptTemplateAttr.IsSumLittle ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.IsSumLittle,value) ; 
			}
		}
		/// <summary>
		/// �Ƿ���ʵ�Һϼơ�
		/// </summary>
		public bool IsSumRight
		{
			get
			{
				return this.GetValBooleanByKey(RptTemplateAttr.IsSumRight ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.IsSumRight,value) ; 
			}
		}
		public PercentModel PercentModel
		{
			get
			{
				return (PercentModel)this.GetValIntByKey(RptTemplateAttr.PercentModel ) ; 
			}
			set
			{
				this.SetValByKey(RptTemplateAttr.PercentModel,(int)value) ; 
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
        /// 
        /// </summary>
		public RptTemplate()
		{
		}
        /// <summary>
        /// ��
        /// </summary>
        /// <param name="EnsName"></param>
        public RptTemplate(string ensName)
        {
            this.EnsName = ensName;
            this.FK_Emp = Web.WebUser.No;

            this.MyPK = Web.WebUser.No + "@" + EnsName;
            
            try
            {
                this.Retrieve();
            }
            catch
            {
                this.Insert();
            }
        }
		 
		/// <summary>
        /// ����ģ��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("Sys_RptTemplate");
				map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="����ģ��";
				map.EnType=EnType.Sys;

                map.AddMyPK();
				map.AddTBString(RptTemplateAttr.EnsName,null,"����",false,false,0,500,20);
                map.AddTBString(RptTemplateAttr.FK_Emp, null, "����Ա", true, false, 0, 20, 20);


				map.AddTBString(RptTemplateAttr.D1,null,"D1",false,true,0,90,10);
				map.AddTBString(RptTemplateAttr.D2,null,"D2",false,true,0,90,10);
				map.AddTBString(RptTemplateAttr.D3,null,"D3",false,true,0,90,10);

				map.AddTBString(RptTemplateAttr.AlObjs,null,"Ҫ�����Ķ���",false,true,0,90,10);

				map.AddTBInt(RptTemplateAttr.Height,600,"Height",false,true);
				map.AddTBInt(RptTemplateAttr.Width,800,"Width",false,true);

				map.AddBoolean(RptTemplateAttr.IsSumBig,false,"�Ƿ���ʾ��ϼ�",false,true);
				map.AddBoolean(RptTemplateAttr.IsSumLittle,false,"�Ƿ���ʾС�ϼ�",false,true);
				map.AddBoolean(RptTemplateAttr.IsSumRight,false,"�Ƿ���ʾ�Һϼ�",false,true);

				map.AddTBInt(RptTemplateAttr.PercentModel,0,"������ʾ��ʽ",false,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class RptTemplates : Entities
	{		
		#region ����
		public RptTemplates()
		{
		}
		
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="EnsName"></param>
		public RptTemplates(string EnsName)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(RptTemplateAttr.EnsName, EnsName);			 
			qo.DoQuery();
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new RptTemplate();
			}

		}
		#endregion

		#region ��ѯ����
		 
		#endregion
		
	}
}
