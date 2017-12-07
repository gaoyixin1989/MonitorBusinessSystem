using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirHour;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.AirHour
{
    /// <summary>
    /// 功能：环境空气填报（小时）监测项目
    /// 创建日期：2013-06-27
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirhourItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIRHOUR_ITEM " + this.BuildWhereStatement(tEnvFillAirhourItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirhourItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourItemVo Details(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR_ITEM " + this.BuildWhereStatement(tEnvFillAirhourItem));
            return SqlHelper.ExecuteObject(new TEnvFillAirhourItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirhourItemVo> SelectByObject(TEnvFillAirhourItemVo tEnvFillAirhourItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR_ITEM " + this.BuildWhereStatement(tEnvFillAirhourItem));
            return SqlHelper.ExecuteObjectList(tEnvFillAirhourItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirhourItemVo tEnvFillAirhourItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIRHOUR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAirhourItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirhourItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = "select * from T_ENV_FILL_AIRHOUR_ITEM " + this.BuildWhereStatement(tEnvFillAirhourItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <returns></returns>
        public TEnvFillAirhourItemVo SelectByObject(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = "select * from T_ENV_FILL_AIRHOUR_ITEM " + this.BuildWhereStatement(tEnvFillAirhourItem);
            return SqlHelper.ExecuteObject(new TEnvFillAirhourItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAirhourItem, TEnvFillAirhourItemVo.T_ENV_FILL_AIRHOUR_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhourItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirhourItem, TEnvFillAirhourItemVo.T_ENV_FILL_AIRHOUR_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAirhourItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhourItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirhourItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourItemVo tEnvFillAirhourItem_UpdateSet, TEnvFillAirhourItemVo tEnvFillAirhourItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirhourItem_UpdateSet, TEnvFillAirhourItemVo.T_ENV_FILL_AIRHOUR_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAirhourItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIRHOUR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            string strSQL = "delete from T_ENV_FILL_AIRHOUR_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillAirhourItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAirhourItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAirhourItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAirhourItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillAirhourItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillAirhourItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillAirhourItem.ITEM_VALUE.ToString()));
                }
                //超标倍数
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.UP_DOUBLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_DOUBLE = '{0}'", tEnvFillAirhourItem.UP_DOUBLE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAirhourItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAirhourItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAirhourItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAirhourItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAirhourItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAirhourItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
