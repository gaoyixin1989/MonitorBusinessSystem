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
    /// 功能：质控平行(秦皇岛)
    /// 创建日期：2013-04-28
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcTwinQhdAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_TWIN_QHD " + this.BuildWhereStatement(tMisMonitorQcTwinQhd);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinQhdVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN_QHD  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinQhdVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinQhdVo Details(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN_QHD " + this.BuildWhereStatement(tMisMonitorQcTwinQhd));
            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinQhdVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcTwinQhdVo> SelectByObject(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN_QHD " + this.BuildWhereStatement(tMisMonitorQcTwinQhd));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcTwinQhd, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_QC_TWIN_QHD {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcTwinQhd));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_TWIN_QHD " + this.BuildWhereStatement(tMisMonitorQcTwinQhd);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcTwinQhdVo SelectByObject(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_TWIN_QHD " + this.BuildWhereStatement(tMisMonitorQcTwinQhd);
            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinQhdVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcTwinQhd, TMisMonitorQcTwinQhdVo.T_MIS_MONITOR_QC_TWIN_QHD_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcTwinQhd, TMisMonitorQcTwinQhdVo.T_MIS_MONITOR_QC_TWIN_QHD_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcTwinQhd.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcTwinQhd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd_UpdateSet, TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcTwinQhd_UpdateSet, TMisMonitorQcTwinQhdVo.T_MIS_MONITOR_QC_TWIN_QHD_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcTwinQhd_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_TWIN_QHD where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_TWIN_QHD ";
            strSQL += this.BuildWhereStatement(tMisMonitorQcTwinQhd);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcTwinQhd)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcTwinQhd.ID.ToString()));
                }
                //原始样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.RESULT_ID_SRC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_SRC = '{0}'", tMisMonitorQcTwinQhd.RESULT_ID_SRC.ToString()));
                }
                //平行样分析结果ID1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.RESULT_ID_TWIN1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_TWIN1 = '{0}'", tMisMonitorQcTwinQhd.RESULT_ID_TWIN1.ToString()));
                }
                //平行样分析结果ID2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.RESULT_ID_TWIN2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_TWIN2 = '{0}'", tMisMonitorQcTwinQhd.RESULT_ID_TWIN2.ToString()));
                }
                //平行样测定值1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.TWIN_RESULT1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_RESULT1 = '{0}'", tMisMonitorQcTwinQhd.TWIN_RESULT1.ToString()));
                }
                //平行样测定值2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.TWIN_RESULT2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_RESULT2 = '{0}'", tMisMonitorQcTwinQhd.TWIN_RESULT2.ToString()));
                }
                //平行测定均值
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.TWIN_AVG.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_AVG = '{0}'", tMisMonitorQcTwinQhd.TWIN_AVG.ToString()));
                }
                //相对偏差（%）
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.TWIN_OFFSET.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_OFFSET = '{0}'", tMisMonitorQcTwinQhd.TWIN_OFFSET.ToString()));
                }
                //是否合格
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.TWIN_ISOK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_ISOK = '{0}'", tMisMonitorQcTwinQhd.TWIN_ISOK.ToString()));
                }
                //质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样  9、质控平行 10、空白加标）
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorQcTwinQhd.QC_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcTwinQhd.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcTwinQhd.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcTwinQhd.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcTwinQhd.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorQcTwinQhd.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcTwinQhd.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}