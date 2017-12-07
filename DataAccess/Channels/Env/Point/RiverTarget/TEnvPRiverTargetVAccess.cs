using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverTarget;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverTargetVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_TARGET_V " + this.BuildWhereStatement(tEnvPRiverTargetV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_TARGET_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverTargetVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVVo Details(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_TARGET_V " + this.BuildWhereStatement(tEnvPRiverTargetV));
            return SqlHelper.ExecuteObject(new TEnvPRiverTargetVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverTargetVVo> SelectByObject(TEnvPRiverTargetVVo tEnvPRiverTargetV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_TARGET_V " + this.BuildWhereStatement(tEnvPRiverTargetV));
            return SqlHelper.ExecuteObjectList(tEnvPRiverTargetV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverTargetVVo tEnvPRiverTargetV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_TARGET_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverTargetV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverTargetV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_TARGET_V " + this.BuildWhereStatement(tEnvPRiverTargetV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <returns></returns>
        public TEnvPRiverTargetVVo SelectByObject(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_TARGET_V " + this.BuildWhereStatement(tEnvPRiverTargetV);
            return SqlHelper.ExecuteObject(new TEnvPRiverTargetVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverTargetV, TEnvPRiverTargetVVo.T_ENV_P_RIVER_TARGET_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverTargetV, TEnvPRiverTargetVVo.T_ENV_P_RIVER_TARGET_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverTargetV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverTargetV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVVo tEnvPRiverTargetV_UpdateSet, TEnvPRiverTargetVVo tEnvPRiverTargetV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverTargetV_UpdateSet, TEnvPRiverTargetVVo.T_ENV_P_RIVER_TARGET_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverTargetV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_TARGET_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            string strSQL = "delete from T_ENV_P_RIVER_TARGET_V ";
            strSQL += this.BuildWhereStatement(tEnvPRiverTargetV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverTargetV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverTargetV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverTargetV.ID.ToString()));
                }
                //责任目标断面ID
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPRiverTargetV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPRiverTargetV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverTargetV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverTargetV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverTargetV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverTargetV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverTargetV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverTargetV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
