using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.BusinessLogic
{
    /// <summary>
    /// 功能描述：业务逻辑基类
    /// 创建日期：2011-4-6 20:38:53
    /// 创 建 人：陈国迎
    /// </summary>
    public abstract class LogicBase : i3.Core.BusinessLogic.LogicBase
    {
        //操作提示信息
        public StringBuilder Tips = new StringBuilder();

        /// <summary>
        /// 功能描述：控制层统一验证函数
        /// 创建日期：2011-4-6
        /// 创建人  ：陈国迎
        /// </summary>
        /// <returns>验证信息</returns>
        public abstract bool Validate();
    }
}
