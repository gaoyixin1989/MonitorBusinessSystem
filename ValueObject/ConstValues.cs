using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i3.ValueObject
{
    /// <summary>
    /// 功能描述：系统常量
    /// 创建日期：2011-4-6 20:53:35
    /// 创 建 人：陈国迎
    /// </summary>
    public struct ConstValues
    {
        /// <summary>
        /// Session键值
        /// </summary>
        public struct SessionKeys
        {
            /// <summary>
            /// 用户登陆信息
            /// </summary>
            public static string UserInfo = "UserInfo";
            /// <summary>
            /// 主题目录
            /// </summary>
            public static string ThemeDir = "ThemeDir";
        }

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
            public static string ASC = "ASC";
            /// <summary>
            /// 倒序
            /// </summary>
            public static string DESC = "DESC";
        }
        
        /// <summary>
        /// 数据翻页数据
        /// </summary>
        public struct Pager
        {
            /// <summary>
            /// 每页显示数据条数
            /// </summary>
            public static int PageSize = 5;
        }
        
        /// <summary>
        /// 特殊字符
        /// </summary>
        public struct SpecialCharacter
        {
            //控件->对象绑定时，如果是空值，则填充如下字符串
            public static string EmptyValuesFillChar = "###";
        }

        /// <summary>
        /// 下拉框树形数据绑定
        /// </summary>
        public struct ValueBindToControlsSplit
        {
            public static string CheckBoxListSplit = "|";
            public static string CheckBoxWebShowSplit = "&nbsp;&nbsp;";
        }

        /// <summary>
        /// 针对多级别List的显示格式定义
        /// </summary>
        public struct ShowAttributeTypeListControlFormart
        {
            public static string ParentHaderText = "---";
            public static string ParentLeftText = "|—";
            public static string TableHeaderText = "";
            public static string AttrNameLeftText = "『";
            public static string AttrNameRightText = "』";
        }
    }
}
