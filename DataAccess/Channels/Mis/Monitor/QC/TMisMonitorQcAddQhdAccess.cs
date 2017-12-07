using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：空白加标(秦皇岛)
    /// 创建日期：2013-04-28
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcAddQhdAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_ADD_QHD " + this.BuildWhereStatement(tMisMonitorQcAddQhd);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddQhdVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD_QHD  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcAddQhdVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddQhdVo Details(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD_QHD " + this.BuildWhereStatement(tMisMonitorQcAddQhd));
            return SqlHelper.ExecuteObject(new TMisMonitorQcAddQhdVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcAddQhdVo> SelectByObject(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD_QHD " + this.BuildWhereStatement(tMisMonitorQcAddQhd));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcAddQhd, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_QC_ADD_QHD {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcAddQhd));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ADD_QHD " + this.BuildWhereStatement(tMisMonitorQcAddQhd);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcAddQhdVo SelectByObject(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ADD_QHD " + this.BuildWhereStatement(tMisMonitorQcAddQhd);
            return SqlHelper.ExecuteObject(new TMisMonitorQcAddQhdVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcAddQhd, TMisMonitorQcAddQhdVo.T_MIS_MONITOR_QC_ADD_QHD_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcAddQhd, TMisMonitorQcAddQhdVo.T_MIS_MONITOR_QC_ADD_QHD_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcAddQhd.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcAddQhd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd_UpdateSet, TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcAddQhd_UpdateSet, TMisMonitorQcAddQhdVo.T_MIS_MONITOR_QC_ADD_QHD_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcAddQhd_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_ADD_QHD where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_ADD_QHD ";
            strSQL += this.BuildWhereStatement(tMisMonitorQcAddQhd);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcAddQhd)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcAddQhd.ID.ToString()));
                }
                //平行样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.RESULT_ID_ADD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_ADD = '{0}'", tMisMonitorQcAddQhd.RESULT_ID_ADD.ToString()));
                }
                //加标量
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.QC_ADD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_ADD = '{0}'", tMisMonitorQcAddQhd.QC_ADD.ToString()));
                }
                //原始测定值
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.SRC_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SRC_RESULT = '{0}'", tMisMonitorQcAddQhd.SRC_RESULT.ToString()));
                }
                //加标测定值
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.ADD_RESULT_EX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_RESULT_EX = '{0}'", tMisMonitorQcAddQhd.ADD_RESULT_EX.ToString()));
                }
                //加标回收率（%）
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.ADD_BACK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_BACK = '{0}'", tMisMonitorQcAddQhd.ADD_BACK.ToString()));
                }
                //加标是否合格
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.ADD_ISOK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_ISOK = '{0}'", tMisMonitorQcAddQhd.ADD_ISOK.ToString()));
                }
                //质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标）
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorQcAddQhd.QC_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcAddQhd.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcAddQhd.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcAddQhd.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcAddQhd.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorQcAddQhd.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcAddQhd.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}