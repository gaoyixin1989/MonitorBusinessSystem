using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料基础信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_PART_INFO_TABLE = "T_OA_PART_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 物料编码
		/// </summary>
		public static string PART_CODE_FIELD = "PART_CODE";
		/// <summary>
		/// 物料类别
		/// </summary>
		public static string PART_TYPE_FIELD = "PART_TYPE";
		/// <summary>
		/// 物料名称
		/// </summary>
		public static string PART_NAME_FIELD = "PART_NAME";
		/// <summary>
		/// 单位
		/// </summary>
		public static string UNIT_FIELD = "UNIT";
		/// <summary>
		/// 规格型号
		/// </summary>
		public static string MODELS_FIELD = "MODELS";
		/// <summary>
		/// 库存量
		/// </summary>
		public static string INVENTORY_FIELD = "INVENTORY";
		/// <summary>
		/// 介质/基体
		/// </summary>
		public static string MEDIUM_FIELD = "MEDIUM";
		/// <summary>
		/// 分析纯/化学纯
		/// </summary>
		public static string PURE_FIELD = "PURE";
		/// <summary>
		/// 报警值
		/// </summary>
		public static string ALARM_FIELD = "ALARM";
		/// <summary>
		/// 用途
		/// </summary>
		public static string USEING_FIELD = "USEING";
		/// <summary>
		/// 技术要求
		/// </summary>
		public static string REQUEST_FIELD = "REQUEST";
		/// <summary>
		/// 性质说明
		/// </summary>
		public static string NARURE_FIELD = "NARURE";
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
		
		public TOaPartInfoVo()
		{
			this.ID = "";
			this.PART_CODE = "";
			this.PART_TYPE = "";
			this.PART_NAME = "";
			this.UNIT = "";
			this.MODELS = "";
			this.INVENTORY = "";
			this.MEDIUM = "";
			this.PURE = "";
			this.ALARM = "";
			this.USEING = "";
			this.REQUEST = "";
			this.NARURE = "";
            this.IS_DEL = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 物料编码
		/// </summary>
		public string PART_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 物料类别
		/// </summary>
		public string PART_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string PART_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string UNIT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string MODELS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 库存量
		/// </summary>
		public string INVENTORY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 介质/基体
		/// </summary>
		public string MEDIUM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分析纯/化学纯
		/// </summary>
		public string PURE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 报警值
		/// </summary>
		public string ALARM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用途
		/// </summary>
		public string USEING
		{
			set ;
			get ;
		}
		/// <summary>
		/// 技术要求
		/// </summary>
		public string REQUEST
		{
			set ;
			get ;
		}
		/// <summary>
		/// 性质说明
		/// </summary>
		public string NARURE
		{
			set ;
			get ;
		}
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
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