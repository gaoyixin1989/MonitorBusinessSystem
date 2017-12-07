using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Air;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Air
{
    /// <summary>
    /// 功能：环境空气数据填报监测项目
    /// 创建日期：2013-05-21
    /// 创建人：钟杰华 
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillAirHourAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIR_ITEM " + this.BuildWhereStatement(tEnvFillAirHour);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirHourVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirHourVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirHour">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirHourVo Details(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR_ITEM " + this.BuildWhereStatement(tEnvFillAirHour));
            return SqlHelper.ExecuteObject(new TEnvFillAirHourVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirHourVo> SelectByObject(TEnvFillAirHourVo tEnvFillAirHour, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIR_ITEM " + this.BuildWhereStatement(tEnvFillAirHour));
            return SqlHelper.ExecuteObjectList(tEnvFillAirHour, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirHourVo tEnvFillAirHour, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAirHour));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirHour"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = "select * from T_ENV_FILL_AIR_ITEM " + this.BuildWhereStatement(tEnvFillAirHour);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <returns></returns>
        public TEnvFillAirHourVo SelectByObject(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = "select * from T_ENV_FILL_AIR_ITEM " + this.BuildWhereStatement(tEnvFillAirHour);
            return SqlHelper.ExecuteObject(new TEnvFillAirHourVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAirHour, TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirHour">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirHourVo tEnvFillAirHour)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirHour, TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAirHour.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_ENV_FILL_AIR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">条件，每个条件间要用and/or来连接
        /// <returns></returns>
        public void DeleteByWhere(string where)
        {
            string strSql = string.Format("delete from T_ENV_FILL_AIR_ITEM where 1=1 {0}", where);
            SqlHelper.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 根据条件返回集合
        /// </summary>
        /// <param name="where">自己拼接的条件，每个条件要用and/or连接</param>
        /// <returns></returns>
        public List<TEnvFillAirHourVo> SelectByObject(string where)
        {
            string strWhere = string.Format("select * from T_ENV_FILL_AIR_ITEM where 1=1 {0}", where);
            return SqlHelper.ExecuteObjectList(new TEnvFillAirHourVo(), strWhere);
        }

        /// <summary>
        /// 根据条件返回DataTable
        /// </summary>
        /// <param name="where">自己拼接的条件，每个条件要用and/or连接</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            string strWhere = string.Format("select * from T_ENV_FILL_AIR_ITEM where 1=1 {0}", where);
            return SqlHelper.ExecuteDataTable(strWhere);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="model">条件实体</param>
        /// <returns></returns>
        public List<TEnvFillAirHourVo> SelectByList(TEnvFillAirHourVo model)
        {
            string strSql = string.Format("select * from T_ENV_FILL_AIR_ITEM {0}", this.BuildWhereStatement(model));
            return SqlHelper.ExecuteObjectList(new TEnvFillAirHourVo(), strSql);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAirItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirHourVo tEnvFillAirItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAirItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAirItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAirItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillAirItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillAirItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillAirItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillAirItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillAirItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillAirItem.ITEM_VALUE.ToString()));
                }
                //空气质量指数
                if (!String.IsNullOrEmpty(tEnvFillAirItem.IAQI.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IAQI = '{0}'", tEnvFillAirItem.IAQI.ToString()));
                }	
                //评价
                if (!String.IsNullOrEmpty(tEnvFillAirItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillAirItem.JUDGE.ToString()));
                }
                //超标倍数
                if (!String.IsNullOrEmpty(tEnvFillAirItem.UP_DOUBLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_DOUBLE = '{0}'", tEnvFillAirItem.UP_DOUBLE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAirItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAirItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAirItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAirItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAirItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAirItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAirItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAirItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAirItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAirItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
