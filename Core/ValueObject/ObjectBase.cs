using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.Core.ValueObject
{
    /// <summary>
    /// 功能描述：值对象基类
    /// 创建　人：陈国迎
    /// 创建日期：2011-4-6 20:50:03
    /// </summary>
    public class ObjectBase
    {
        public ObjectBase()
		{
            this.SORT_TYPE  = ConstValues.SortType.ASC;
            this.SORT_FIELD = "";
            this.ROWNO      = "";
		}

        ///<summary>
        /// 排序类型
        /// </summary>
        public string SORT_TYPE
        {
            get;
            set;
        }

        ///<summary>
        /// 排序列
        /// </summary>
        public string SORT_FIELD
        {
            get;
            set;
        }

        public string ROWNO
        {
            get;
            set;
        }
    }
}
