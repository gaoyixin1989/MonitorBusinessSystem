using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Air30;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.Air30
{
    /// <summary>
    /// 功能：双三十废气
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPAir30ItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = "select Count(*) from T_ENV_P_AIR30_ITEM " + this.BuildWhereStatement(tEnvPAir30Item);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAir30ItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR30_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPAir30ItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir30Item">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAir30ItemVo Details(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR30_ITEM " + this.BuildWhereStatement(tEnvPAir30Item));
            return SqlHelper.ExecuteObject(new TEnvPAir30ItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAir30ItemVo> SelectByObject(TEnvPAir30ItemVo tEnvPAir30Item, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_AIR30_ITEM " + this.BuildWhereStatement(tEnvPAir30Item));
            return SqlHelper.ExecuteObjectList(tEnvPAir30Item, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAir30ItemVo tEnvPAir30Item, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_AIR30_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPAir30Item));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir30Item"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = "select * from T_ENV_P_AIR30_ITEM " + this.BuildWhereStatement(tEnvPAir30Item);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <returns></returns>
        public TEnvPAir30ItemVo SelectByObject(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = "select * from T_ENV_P_AIR30_ITEM " + this.BuildWhereStatement(tEnvPAir30Item);
            return SqlHelper.ExecuteObject(new TEnvPAir30ItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir30Item, TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30Item">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir30Item, TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPAir30Item.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30Item_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir30Item_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30ItemVo tEnvPAir30Item_UpdateSet, TEnvPAir30ItemVo tEnvPAir30Item_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir30Item_UpdateSet, TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPAir30Item_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_AIR30_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            string strSQL = "delete from T_ENV_P_AIR30_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPAir30Item);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPAir30Item"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPAir30Item)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPAir30Item.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPAir30Item.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPAir30Item.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPAir30Item.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPAir30Item.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPAir30Item.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPAir30Item.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPAir30Item.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPAir30Item.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPAir30Item.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPAir30Item.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPAir30Item.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPAir30Item.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPAir30Item.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPAir30Item.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPAir30Item.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPAir30Item.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPAir30Item.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
