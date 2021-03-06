using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_ITEM_INFO_TABLE = "T_BASE_ITEM_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测项目名称
		/// </summary>
		public static string ITEM_NAME_FIELD = "ITEM_NAME";
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public static string MONITOR_ID_FIELD = "MONITOR_ID";
		/// <summary>
		/// 废水类，现场室填写项目
		/// </summary>
		public static string IS_SAMPLEDEPT_FIELD = "IS_SAMPLEDEPT";
		/// <summary>
		/// 是否包含监测子项
		/// </summary>
		public static string HAS_SUB_ITEM_FIELD = "HAS_SUB_ITEM";
		/// <summary>
		/// 是否是监测子项
		/// </summary>
		public static string IS_SUB_FIELD = "IS_SUB";
        /// <summary>
        /// 项目代码，用于秦皇岛交接单打印
        /// </summary>
        public static string ITEM_NUM_FIELD = "ITEM_NUM";
		/// <summary>
		/// 监测单价
		/// </summary>
		public static string CHARGE_FIELD = "CHARGE";
		/// <summary>
		/// 开机费用
		/// </summary>
		public static string TEST_POWER_FEE_FIELD = "TEST_POWER_FEE";
		/// <summary>
		/// 实验室认可
		/// </summary>
		public static string LAB_CERTIFICATE_FIELD = "LAB_CERTIFICATE";
		/// <summary>
		/// 计量认可
		/// </summary>
		public static string MEASURE_CERTIFICATE_FIELD = "MEASURE_CERTIFICATE";
		/// <summary>
		/// 平行上限
		/// </summary>
		public static string TWIN_VALUE_FIELD = "TWIN_VALUE";
		/// <summary>
		/// 加标下限
		/// </summary>
		public static string ADD_MIN_FIELD = "ADD_MIN";
		/// <summary>
		/// 加标上限
		/// </summary>
		public static string ADD_MAX_FIELD = "ADD_MAX";
		/// <summary>
		/// 序号
		/// </summary>
		public static string ORDER_NUM_FIELD = "ORDER_NUM";
		/// <summary>
        /// 删除标记
		/// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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
		
		public TBaseItemInfoVo()
		{
			this.ID = "";
			this.ITEM_NAME = "";
			this.MONITOR_ID = "";
			this.IS_SAMPLEDEPT = "";
			this.HAS_SUB_ITEM = "";
			this.IS_SUB = "";
            this.ITEM_NUM = "";
            this.TEST_POINT_NUM = "";
            this.ANALYSE_NUM = "";
            this.PRETREATMENT_FEE = "";
            this.TEST_ANSY_FEE = "";
			this.CHARGE = "";
			this.TEST_POWER_FEE = "";
			this.LAB_CERTIFICATE = "";
			this.MEASURE_CERTIFICATE = "";
			this.TWIN_VALUE = "";
			this.ADD_MIN = "";
			this.ADD_MAX = "";
			this.ORDER_NUM = "";
            this.IS_DEL = "";
            this.IS_ANYSCENE_ITEM = "";
            this.ORI_CATALOG_TABLEID = "";
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
		/// 监测项目名称
		/// </summary>
		public string ITEM_NAME
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
		/// 废水类，现场室填写项目
		/// </summary>
		public string IS_SAMPLEDEPT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否包含监测子项
		/// </summary>
		public string HAS_SUB_ITEM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否是监测子项
		/// </summary>
		public string IS_SUB
		{
			set ;
			get ;
		}
        /// <summary>
        /// 项目代码，用于秦皇岛交接单打印
        /// </summary>
        public string ITEM_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 测点数，用于清远项目费用
        /// </summary>
        public string TEST_POINT_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 分析批次数，用于清远项目费用
        /// </summary>
        public string ANALYSE_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 前处理费，用于清远项目费用
        /// </summary>
        public string PRETREATMENT_FEE
        {
            set;
            get;
        }
        /// <summary>
        /// 测试分析费，用于清远项目费用
        /// </summary>
        public string TEST_ANSY_FEE
        {
            set;
            get;
        }
		/// <summary>
		/// 监测单价
		/// </summary>
		public string CHARGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开机费用
		/// </summary>
		public string TEST_POWER_FEE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实验室认可
		/// </summary>
		public string LAB_CERTIFICATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 计量认可
		/// </summary>
		public string MEASURE_CERTIFICATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 平行上限
		/// </summary>
		public string TWIN_VALUE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 加标下限
		/// </summary>
		public string ADD_MIN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 加标上限
		/// </summary>
		public string ADD_MAX
		{
			set ;
			get ;
		}
		/// <summary>
		/// 序号
		/// </summary>
		public string ORDER_NUM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 0为在使用、1为停用
		/// </summary>
        public string IS_DEL
		{
			set ;
			get ;
		}
        /// <summary>
        /// 0为否、1为是 标识是否为分析类现场监测项目
        /// </summary>
        public string IS_ANYSCENE_ITEM
        {
            set;
            get;
        }
        /// <summary>
        /// 原始记录单使用表ID
        /// </summary>
        public string ORI_CATALOG_TABLEID 
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