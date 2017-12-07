using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i3.Core
{
    /// <summary>
    /// 功能描述：Core层系统常量
    /// 创建时间：2011-4-6 20:50:43
    /// 创建人  ：陈国迎
    /// </summary>
    public struct ConstValues
    {
        /// <summary>
        /// 对象中的排序列和排序类型
        /// </summary>
        public struct SortInfo
        {
            public static string SORT_TYPE  = "SORT_TYPE";
            public static string SORT_FIELD = "SORT_FIELD";
        }

        /// <summary>
        /// 数据排序方式
        /// </summary>
        public struct SortType
        {
            /// <summary>
            /// 正序
            /// </summary>
            public static string ASC  = "ASC";
            /// <summary>
            /// 倒序
            /// </summary>
            public static string DESC = "DESC";
        }

        /// <summary>
        /// 用户截取字符串或者代替特殊意义字符的特殊定义
        /// </summary>
        public struct SpecialCharacter
        {
            //控件->对象绑定时，如果是空值，则填充如下字符
            public static string EmptyValuesFillChar = "###";
        }

        /// <summary>
        /// 对象中的排序列和排序类型
        /// </summary>
        public struct VoSortInfo
        {
            public static string SORT_TYPE  = "SORT_TYPE";
            public static string SORT_FIELD = "SORT_FIELD";
        }
    }
}
