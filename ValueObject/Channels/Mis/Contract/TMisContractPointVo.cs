using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测点信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_POINT_TABLE = "T_MIS_CONTRACT_POINT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 合同ID
		/// </summary>
		public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public static string MONITOR_ID_FIELD = "MONITOR_ID";
        /// <summary>
        ///基础资料监测点ID
        /// </summary>
        public static string POINT_ID_FIELD = "POINT_ID";
		/// <summary>
		/// 监测点名称
		/// </summary>
		public static string POINT_NAME_FIELD = "POINT_NAME";
		/// <summary>
		/// 动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
		/// </summary>
		public static string DYNAMIC_ATTRIBUTE_ID_FIELD = "DYNAMIC_ATTRIBUTE_ID";
		/// <summary>
		/// 建成时间
		/// </summary>
		public static string CREATE_DATE_FIELD = "CREATE_DATE";
		/// <summary>
		/// 监测点位置
		/// </summary>
		public static string ADDRESS_FIELD = "ADDRESS";
		/// <summary>
		/// 经度
		/// </summary>
		public static string LONGITUDE_FIELD = "LONGITUDE";
		/// <summary>
		/// 纬度
		/// </summary>
		public static string LATITUDE_FIELD = "LATITUDE";
		/// <summary>
		/// 监测频次
		/// </summary>
		public static string FREQ_FIELD = "FREQ";
		/// <summary>
		/// 点位描述
		/// </summary>
		public static string DESCRIPTION_FIELD = "DESCRIPTION";
		/// <summary>
		/// 国标条件项
		/// </summary>
		public static string NATIONAL_ST_CONDITION_ID_FIELD = "NATIONAL_ST_CONDITION_ID";
		/// <summary>
		/// 行标条件项ID
		/// </summary>
		public static string INDUSTRY_ST_CONDITION_ID_FIELD = "INDUSTRY_ST_CONDITION_ID";
		/// <summary>
		/// 地标条件项_ID
		/// </summary>
		public static string LOCAL_ST_CONDITION_ID_FIELD = "LOCAL_ST_CONDITION_ID";
		/// <summary>
        /// 是否删除
		/// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
		/// <summary>
		/// 序号
		/// </summary>
		public static string NUM_FIELD = "NUM";
		/// <summary>
		/// 备注1
		/// </summary>
		public static string REMARK1_FIELD = "REMARK1";
		/// <summary>
		/// 备注2
		/// </summary>
		public static string REMARK2_FIELD = "REMARK2";
		/// <summary>
		/// 备注3
		/// </summary>
		public static string REMARK3_FIELD = "REMARK3";
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		
		#endregion
		
		public TMisContractPointVo()
		{
			this.ID = "";
			this.CONTRACT_ID = "";
			this.MONITOR_ID = "";
            this.POINT_ID = "";
			this.POINT_NAME = "";
			this.DYNAMIC_ATTRIBUTE_ID = "";
			this.CREATE_DATE = "";
			this.ADDRESS = "";
			this.LONGITUDE = "";
			this.LATITUDE = "";
            this.SAMPLE_FREQ = "";
			this.FREQ = "";
			this.DESCRIPTION = "";
			this.NATIONAL_ST_CONDITION_ID = "";
			this.INDUSTRY_ST_CONDITION_ID = "";
			this.LOCAL_ST_CONDITION_ID = "";
            this.IS_DEL = "";
			this.NUM = "";
            this.SAMPLE_DAY = "";
            this.SAMPLENUM = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// ID
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 合同ID
		/// </summary>
		public string CONTRACT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public string MONITOR_ID
		{
			set ;
			get ;
		}
        /// <summary>
        /// 基础资料监测点ID
        /// </summary>
        public string POINT_ID
        {
            set;
            get;
        }
		/// <summary>
		/// 监测点名称
		/// </summary>
		public string POINT_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// <summary>
		/// 动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
		/// </summary>
		public string DYNAMIC_ATTRIBUTE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 建成时间
		/// </summary>
		public string CREATE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测点位置
		/// </summary>
		public string ADDRESS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 经度
		/// </summary>
		public string LONGITUDE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public string LATITUDE
		{
			set ;
			get ;
		}
        /// <summary>
        /// 采样频次
        /// </summary>
        public string SAMPLE_FREQ
        {
            set;
            get;
        }
		/// <summary>
		/// 监测频次
		/// </summary>
		public string FREQ
		{
			set ;
			get ;
		}
		/// <summary>
		/// 点位描述
		/// </summary>
		public string DESCRIPTION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 国标条件项
		/// </summary>
		public string NATIONAL_ST_CONDITION_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 行标条件项ID
		/// </summary>
		public string INDUSTRY_ST_CONDITION_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 地标条件项_ID
		/// </summary>
		public string LOCAL_ST_CONDITION_ID
		{
			set ;
			get ;
		}
		/// <summary>
        /// 是否删除
		/// </summary>
        public string IS_DEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 序号
		/// </summary>
		public string NUM
		{
			set ;
			get ;
		}
        /// <summary>
        /// 采样周期
        /// </summary>
        public string SAMPLE_DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 每次采几个样品
        /// </summary>
        public string SAMPLENUM 
        { 
            get; 
            set; 
        }
		/// <summary>
		/// 备注1
		/// </summary>
		public string REMARK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注2
		/// </summary>
		public string REMARK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注4
		/// </summary>
		public string REMARK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注5
		/// </summary>
		public string REMARK5
		{
			set ;
			get ;
		}
	
		
		#endregion

    }
}