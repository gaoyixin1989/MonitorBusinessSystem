using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudSea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudSeaVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = "select Count(*) from T_ENV_P_MUD_SEA_V " + this.BuildWhereStatement(tEnvPMudSeaV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPMudSeaVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVVo Details(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V " + this.BuildWhereStatement(tEnvPMudSeaV));
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudSeaVVo> SelectByObject(TEnvPMudSeaVVo tEnvPMudSeaV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V " + this.BuildWhereStatement(tEnvPMudSeaV));
            return SqlHelper.ExecuteObjectList(tEnvPMudSeaV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudSeaVVo tEnvPMudSeaV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_MUD_SEA_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPMudSeaV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudSeaV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA_V " + this.BuildWhereStatement(tEnvPMudSeaV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <returns></returns>
        public TEnvPMudSeaVVo SelectByObject(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA_V " + this.BuildWhereStatement(tEnvPMudSeaV);
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPMudSeaV, TEnvPMudSeaVVo.T_ENV_P_MUD_SEA_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSeaV, TEnvPMudSeaVVo.T_ENV_P_MUD_SEA_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPMudSeaV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudSeaV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVVo tEnvPMudSeaV_UpdateSet, TEnvPMudSeaVVo tEnvPMudSeaV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSeaV_UpdateSet, TEnvPMudSeaVVo.T_ENV_P_MUD_SEA_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPMudSeaV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_MUD_SEA_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            string strSQL = "delete from T_ENV_P_MUD_SEA_V ";
            strSQL += this.BuildWhereStatement(tEnvPMudSeaV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPMudSeaV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPMudSeaV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPMudSeaV.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPMudSeaV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPMudSeaV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPMudSeaV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPMudSeaV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPMudSeaV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPMudSeaV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPMudSeaV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPMudSeaV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
