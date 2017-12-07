using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Alkali;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Alkali
{
    /// <summary>
    /// 功能：碳酸盐化速率填报监测项目 
    /// 创建日期：2013-06-24
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAlkaliItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ALKALI_ITEM " + this.BuildWhereStatement(tEnvFillAlkaliItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliItemVo Details(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI_ITEM " + this.BuildWhereStatement(tEnvFillAlkaliItem));
            return SqlHelper.ExecuteObject(new TEnvFillAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlkaliItemVo> SelectByObject(TEnvFillAlkaliItemVo tEnvFillAlkaliItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI_ITEM " + this.BuildWhereStatement(tEnvFillAlkaliItem));
            return SqlHelper.ExecuteObjectList(tEnvFillAlkaliItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlkaliItemVo tEnvFillAlkaliItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ALKALI_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAlkaliItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlkaliItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = "select * from T_ENV_FILL_ALKALI_ITEM " + this.BuildWhereStatement(tEnvFillAlkaliItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <returns></returns>
        public TEnvFillAlkaliItemVo SelectByObject(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = "select * from T_ENV_FILL_ALKALI_ITEM " + this.BuildWhereStatement(tEnvFillAlkaliItem);
            return SqlHelper.ExecuteObject(new TEnvFillAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAlkaliItem, TEnvFillAlkaliItemVo.T_ENV_FILL_ALKALI_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlkaliItem, TEnvFillAlkaliItemVo.T_ENV_FILL_ALKALI_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAlkaliItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkaliItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlkaliItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliItemVo tEnvFillAlkaliItem_UpdateSet, TEnvFillAlkaliItemVo tEnvFillAlkaliItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlkaliItem_UpdateSet, TEnvFillAlkaliItemVo.T_ENV_FILL_ALKALI_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAlkaliItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ALKALI_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            string strSQL = "delete from T_ENV_FILL_ALKALI_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillAlkaliItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAlkaliItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAlkaliItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAlkaliItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillAlkaliItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillAlkaliItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillAlkaliItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillAlkaliItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAlkaliItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAlkaliItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAlkaliItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAlkaliItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAlkaliItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAlkaliItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
