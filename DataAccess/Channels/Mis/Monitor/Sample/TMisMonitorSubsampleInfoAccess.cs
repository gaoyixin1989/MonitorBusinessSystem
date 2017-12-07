using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：采样样品子样
    /// 创建日期：2013-04-08
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorSubsampleInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SUBSAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSubsampleInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubsampleInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBSAMPLE_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorSubsampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubsampleInfoVo Details(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBSAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSubsampleInfo));
            return SqlHelper.ExecuteObject(new TMisMonitorSubsampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSubsampleInfoVo> SelectByObject(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBSAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSubsampleInfo));
            return SqlHelper.ExecuteObjectList(tMisMonitorSubsampleInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_SUBSAMPLE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSubsampleInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBSAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSubsampleInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <returns></returns>
        public TMisMonitorSubsampleInfoVo SelectByObject(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBSAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSubsampleInfo);
            return SqlHelper.ExecuteObject(new TMisMonitorSubsampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSubsampleInfo, TMisMonitorSubsampleInfoVo.T_MIS_MONITOR_SUBSAMPLE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubsampleInfo, TMisMonitorSubsampleInfoVo.T_MIS_MONITOR_SUBSAMPLE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSubsampleInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubsampleInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo_UpdateSet, TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubsampleInfo_UpdateSet, TMisMonitorSubsampleInfoVo.T_MIS_MONITOR_SUBSAMPLE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSubsampleInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SUBSAMPLE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            string strSQL = "delete from T_MIS_MONITOR_SUBSAMPLE_INFO ";
            strSQL += this.BuildWhereStatement(tMisMonitorSubsampleInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 插入子样品数据 胡方扬2013-04-10
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo"></param>
        /// <param name="strCode"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public bool InsertSubSample(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo,string strCode, int Number) {
            ArrayList objVo = new ArrayList();
            if (Number > 0) {
                for (int i = 0; i < Number; i++)
                {
                    tMisMonitorSubsampleInfo.ID = GetSerialNumber("t_mis_monitor_SubSampleID");
                    tMisMonitorSubsampleInfo.SUBSAMPLE_NAME = strCode + "-" + (i + 1).ToString();
                    if (String.IsNullOrEmpty(tMisMonitorSubsampleInfo.ACTIONDATE))
                    {
                        tMisMonitorSubsampleInfo.ACTIONDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }
                    string strSQL = String.Format(@"INSERT INTO T_MIS_MONITOR_SUBSAMPLE_INFO(ID,SUBSAMPLE_NAME,SAMPLEID,ACTIONDATE) VALUES('{0}','{1}','{2}','{3}')", 
                        tMisMonitorSubsampleInfo.ID,tMisMonitorSubsampleInfo.SUBSAMPLE_NAME,tMisMonitorSubsampleInfo.SAMPLEID,tMisMonitorSubsampleInfo.ACTIONDATE);
                    objVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSubsampleInfo)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorSubsampleInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSubsampleInfo.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorSubsampleInfo.SUBSAMPLE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBSAMPLE_NAME = '{0}'", tMisMonitorSubsampleInfo.SUBSAMPLE_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorSubsampleInfo.SAMPLEID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLEID = '{0}'", tMisMonitorSubsampleInfo.SAMPLEID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorSubsampleInfo.ACTIONDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACTIONDATE = '{0}'", tMisMonitorSubsampleInfo.ACTIONDATE.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
