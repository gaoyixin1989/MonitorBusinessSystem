using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器资料备份
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusFilebakAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_FILEBAK " + this.BuildWhereStatement(tBaseApparatusFilebak);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusFilebakVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_FILEBAK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseApparatusFilebakVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusFilebakVo Details(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_FILEBAK " + this.BuildWhereStatement(tBaseApparatusFilebak));
            return SqlHelper.ExecuteObject(new TBaseApparatusFilebakVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusFilebakVo> SelectByObject(TBaseApparatusFilebakVo tBaseApparatusFilebak, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_APPARATUS_FILEBAK " + this.BuildWhereStatement(tBaseApparatusFilebak));
            return SqlHelper.ExecuteObjectList(tBaseApparatusFilebak, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusFilebakVo tBaseApparatusFilebak, int iIndex, int iCount)
        {

            //string strSQL = " select * from T_BASE_APPARATUS_FILEBAK {0} ";
            string strSQL = @" select *,(select APPARATUS_CODE from T_BASE_APPARATUS_INFO where ID=t.APPARATUS_CODE) as APPARATUS_ID,
		                         (select NAME from T_BASE_APPARATUS_INFO where ID=t.APPARATUS_CODE) as APPARATUS_NAME
			                        from T_BASE_APPARATUS_FILEBAK t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusFilebak));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusFilebak"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = "select * from T_BASE_APPARATUS_FILEBAK " + this.BuildWhereStatement(tBaseApparatusFilebak);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <returns></returns>
        public TBaseApparatusFilebakVo SelectByObject(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = "select * from T_BASE_APPARATUS_FILEBAK " + this.BuildWhereStatement(tBaseApparatusFilebak);
            return SqlHelper.ExecuteObject(new TBaseApparatusFilebakVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseApparatusFilebak, TBaseApparatusFilebakVo.T_BASE_APPARATUS_FILEBAK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusFilebak">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusFilebak, TBaseApparatusFilebakVo.T_BASE_APPARATUS_FILEBAK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseApparatusFilebak.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusFilebak_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseApparatusFilebak_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusFilebakVo tBaseApparatusFilebak_UpdateSet, TBaseApparatusFilebakVo tBaseApparatusFilebak_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusFilebak_UpdateSet, TBaseApparatusFilebakVo.T_BASE_APPARATUS_FILEBAK_TABLE);
            strSQL += this.BuildWhereStatement(tBaseApparatusFilebak_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_APPARATUS_FILEBAK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            string strSQL = "delete from T_BASE_APPARATUS_FILEBAK ";
            strSQL += this.BuildWhereStatement(tBaseApparatusFilebak);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusFilebak"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusFilebak)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusFilebak.ID.ToString()));
                }
                //仪器ID
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.APPARATUS_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_CODE = '{0}'", tBaseApparatusFilebak.APPARATUS_CODE.ToString()));
                }
                //资料编号
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.APPARATUS_FILE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_FILE_CODE = '{0}'", tBaseApparatusFilebak.APPARATUS_FILE_CODE.ToString()));
                }
                //资料名称
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.APPARATUS_ATT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_ATT_NAME = '{0}'", tBaseApparatusFilebak.APPARATUS_ATT_NAME.ToString()));
                }
                //资料保存目录ID
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.APPARATUS_ATT_FOLDER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_ATT_FOLDER_ID = '{0}'", tBaseApparatusFilebak.APPARATUS_ATT_FOLDER_ID.ToString()));
                }
                //资料保存文件ID
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.APPARATUS_ATT_FILE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_ATT_FILE_ID = '{0}'", tBaseApparatusFilebak.APPARATUS_ATT_FILE_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusFilebak.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusFilebak.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusFilebak.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusFilebak.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseApparatusFilebak.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusFilebak.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
