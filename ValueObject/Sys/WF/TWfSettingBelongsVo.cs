using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程分类
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingBelongsVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_BELONGS_TABLE = "T_WF_SETTING_BELONGS";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 分类编号
		/// </summary>
		public static string WF_CLASS_ID_FIELD = "WF_CLASS_ID";
		/// <summary>
		/// 分类父编号
		/// </summary>
		public static string WF_CLASS_PID_FIELD = "WF_CLASS_PID";
		/// <summary>
		/// 分类名称
		/// </summary>
		public static string WF_CLASS_NAME_FIELD = "WF_CLASS_NAME";
		/// <summary>
		/// 分类备注
		/// </summary>
		public static string WF_CLASS_NOTE_FIELD = "WF_CLASS_NOTE";
		/// <summary>
		/// 分类等级
		/// </summary>
		public static string WF_CLASS_LEVEL_FIELD = "WF_CLASS_LEVEL";
		/// <summary>
		/// 简述
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TWfSettingBelongsVo()
		{
			this.ID = "";
			this.WF_CLASS_ID = "";
			this.WF_CLASS_PID = "";
			this.WF_CLASS_NAME = "";
			this.WF_CLASS_NOTE = "";
			this.WF_CLASS_LEVEL = "";
			this.REMARK = "";
			
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
		/// 分类编号
		/// </summary>
		public string WF_CLASS_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分类父编号
		/// </summary>
		public string WF_CLASS_PID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分类名称
		/// </summary>
		public string WF_CLASS_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分类备注
		/// </summary>
		public string WF_CLASS_NOTE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分类等级
		/// </summary>
		public string WF_CLASS_LEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 简述
		/// </summary>
		public string REMARK
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}