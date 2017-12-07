using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace i3.DataAccess
{
    /// <summary>
    /// 功能描述：SqlHelper数据库访问基类
    /// 创建日期：2011-4-6 20:52:59
    /// 创 建 人：陈国迎
    /// </summary>
    public class SqlHelper : i3.Core.DataAccess.SqlHelper
    {
        public SqlHelper()
        {
            SetConnectionString();
        }

        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        public override void SetConnectionString()
        {
            strConnection           = ConfigurationManager.ConnectionStrings["i3OracleConnect"].ToString();
            strConnectionStatic = ConfigurationManager.ConnectionStrings["i3OracleConnect"].ToString();
        }
    }
}
