using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Estuaries;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Estuaries
{

    /// <summary>
    /// 功能：入海河口数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillEstuariesItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ESTUARIES_ITEM " + this.BuildWhereStatement(tEnvFillEstuariesItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillEstuariesItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesItemVo Details(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES_ITEM " + this.BuildWhereStatement(tEnvFillEstuariesItem));
            return SqlHelper.ExecuteObject(new TEnvFillEstuariesItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillEstuariesItemVo> SelectByObject(TEnvFillEstuariesItemVo tEnvFillEstuariesItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES_ITEM " + this.BuildWhereStatement(tEnvFillEstuariesItem));
            return SqlHelper.ExecuteObjectList(tEnvFillEstuariesItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillEstuariesItemVo tEnvFillEstuariesItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ESTUARIES_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillEstuariesItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillEstuariesItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = "select * from T_ENV_FILL_ESTUARIES_ITEM " + this.BuildWhereStatement(tEnvFillEstuariesItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <returns></returns>
        public TEnvFillEstuariesItemVo SelectByObject(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = "select * from T_ENV_FILL_ESTUARIES_ITEM " + this.BuildWhereStatement(tEnvFillEstuariesItem);
            return SqlHelper.ExecuteObject(new TEnvFillEstuariesItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillEstuariesItem, TEnvFillEstuariesItemVo.T_ENV_FILL_ESTUARIES_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillEstuariesItem, TEnvFillEstuariesItemVo.T_ENV_FILL_ESTUARIES_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillEstuariesItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuariesItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillEstuariesItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesItemVo tEnvFillEstuariesItem_UpdateSet, TEnvFillEstuariesItemVo tEnvFillEstuariesItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillEstuariesItem_UpdateSet, TEnvFillEstuariesItemVo.T_ENV_FILL_ESTUARIES_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillEstuariesItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ESTUARIES_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            string strSQL = "delete from T_ENV_FILL_ESTUARIES_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillEstuariesItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillEstuariesItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillEstuariesItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillEstuariesItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillEstuariesItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillEstuariesItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillEstuariesItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillEstuariesItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillEstuariesItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillEstuariesItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillEstuariesItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillEstuariesItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillEstuariesItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillEstuariesItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
