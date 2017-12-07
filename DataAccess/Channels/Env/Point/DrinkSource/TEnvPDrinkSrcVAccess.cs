using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = "select Count(*) from T_ENV_P_DRINK_SRC_V " + this.BuildWhereStatement(tEnvPDrinkSrcV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVVo Details(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V " + this.BuildWhereStatement(tEnvPDrinkSrcV));
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVVo> SelectByObject(TEnvPDrinkSrcVVo tEnvPDrinkSrcV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V " + this.BuildWhereStatement(tEnvPDrinkSrcV));
            return SqlHelper.ExecuteObjectList(tEnvPDrinkSrcV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVVo tEnvPDrinkSrcV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DRINK_SRC_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDrinkSrcV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrcV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC_V " + this.BuildWhereStatement(tEnvPDrinkSrcV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVVo SelectByObject(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC_V " + this.BuildWhereStatement(tEnvPDrinkSrcV);
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkSrcV, TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrcV, TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDrinkSrcV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrcV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVVo tEnvPDrinkSrcV_UpdateSet, TEnvPDrinkSrcVVo tEnvPDrinkSrcV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrcV_UpdateSet, TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrcV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DRINK_SRC_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            string strSQL = "delete from T_ENV_P_DRINK_SRC_V ";
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrcV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDrinkSrcV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDrinkSrcV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDrinkSrcV.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPDrinkSrcV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPDrinkSrcV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDrinkSrcV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDrinkSrcV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDrinkSrcV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDrinkSrcV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDrinkSrcV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
