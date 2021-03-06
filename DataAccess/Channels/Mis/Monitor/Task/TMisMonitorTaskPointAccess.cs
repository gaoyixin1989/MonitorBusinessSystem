using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务点位信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskPointAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK_POINT " + this.BuildWhereStatement(tMisMonitorTaskPoint);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskPointVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_POINT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskPointVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskPointVo Details(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_POINT " + this.BuildWhereStatement(tMisMonitorTaskPoint));
            return SqlHelper.ExecuteObject(new TMisMonitorTaskPointVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskPointVo> SelectByObject(TMisMonitorTaskPointVo tMisMonitorTaskPoint, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_POINT " + this.BuildWhereStatement(tMisMonitorTaskPoint));
            return SqlHelper.ExecuteObjectList(tMisMonitorTaskPoint, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskPointVo tMisMonitorTaskPoint, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_TASK_POINT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTaskPoint));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_POINT " + this.BuildWhereStatement(tMisMonitorTaskPoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskPointVo SelectByObject(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_POINT " + this.BuildWhereStatement(tMisMonitorTaskPoint);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskPointVo(), strSQL);
        }

        /// <summary>
        /// 根据监测子任务ID获取包含现场项目的点位信息
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <param name="ItemCondition">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptPoint(string strSubtaskID, string ItemCondition = "1",IList<string>sampleIdList=null)
        {
            string sqlItemCondition = getItemCondition_WithIn(ItemCondition);


            string strSQL = @"SELECT     T_MIS_MONITOR_TASK_POINT.*
                                FROM         T_MIS_MONITOR_TASK_POINT inner join T_MIS_MONITOR_SAMPLE_INFO on T_MIS_MONITOR_TASK_POINT.ID=T_MIS_MONITOR_SAMPLE_INFO.POINT_ID
                                where T_MIS_MONITOR_TASK_POINT.SUBTASK_ID='{0}' and T_MIS_MONITOR_TASK_POINT.IS_DEL='0' and  exists
                                (
                                select * from T_MIS_MONITOR_TASK_ITEM where  T_MIS_MONITOR_TASK_ITEM.TASK_POINT_ID= T_MIS_MONITOR_TASK_POINT.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where 1=1 {1} and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_TASK_ITEM.ITEM_ID
                                )
                                )";

            if (sampleIdList != null && sampleIdList.Count > 0)
            {
                var str = string.Join("','", sampleIdList);

                strSQL +=string.Format( " and T_MIS_MONITOR_SAMPLE_INFO.ID in ('{0}') ",str);
            }


            strSQL = string.Format(strSQL, strSubtaskID, sqlItemCondition);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据监测子任务ID获取包含现场项目的点位信息
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptPoint_QHD(string strSubtaskID)
        {
            string strSQL = @"SELECT     *
                                FROM         T_MIS_MONITOR_SAMPLE_INFO
                                where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID='{0}' and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE='0' and  exists
                                (
                                select * from T_MIS_MONITOR_RESULT where  T_MIS_MONITOR_RESULT.SAMPLE_ID= T_MIS_MONITOR_SAMPLE_INFO.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是' and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_RESULT.ITEM_ID
)
                                )";
            strSQL = string.Format(strSQL, strSubtaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTaskPoint, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskPoint, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTaskPoint.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskPointVo tMisMonitorTaskPoint_UpdateSet, TMisMonitorTaskPointVo tMisMonitorTaskPoint_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskPoint_UpdateSet, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTaskPoint_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK_POINT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK_POINT ";
            strSQL += this.BuildWhereStatement(tMisMonitorTaskPoint);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获取任务点位信息
        /// 创建时间：2012-12-5
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemType">监测类别</param>
        /// <returns></returns>
        public DataTable SelectByTableByTaskID(string strTaskID, string strItemType)
        {
            string strSQL = "select p.ID,p.POINT_NAME,s.SAMPLE_CODE,s.QC_TYPE from (select * from T_MIS_MONITOR_TASK_POINT where 1=1 {0}) p" +
                                            " left join T_MIS_MONITOR_SAMPLE_INFO s on s.QC_TYPE='0' and p.ID = s.POINT_ID";
            string strWhere = "";
            if (!string.IsNullOrEmpty(strItemType))
            {
                strWhere += " and MONITOR_ID='" + strItemType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskID))
            {
                strWhere += " and TASK_ID ='" + strTaskID + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取任务点位信息
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemType">监测类别</param>
        /// <returns></returns>
        public DataTable SelectByTableByTaskIDForLicense(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = @"select distinct ID,substring(POINT_NAME,0,len (POINT_NAME)-1) as POINT_NAME 
                                                from T_MIS_MONITOR_TASK_POINT ";
            strSQL += BuildWhereStatement(tMisMonitorTaskPoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取所有质控类型任务点位信息
        /// 创建时间：2012-12-5
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemType">监测类别</param>
        /// <returns></returns>
        public DataTable SelectAllQcTypePoint(string strTaskID, string strItemType)
        {
            string strSQL = "select p.ID,p.POINT_NAME,s.SAMPLE_CODE,s.QC_TYPE from (select * from T_MIS_MONITOR_TASK_POINT where 1=1 {0}) p" +
                                            " left join T_MIS_MONITOR_SAMPLE_INFO s on p.ID = s.POINT_ID";
            string strWhere = "";
            if (!string.IsNullOrEmpty(strItemType))
            {
                strWhere += " and MONITOR_ID='" + strItemType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskID))
            {
                strWhere += " and TASK_ID ='" + strTaskID + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 功能描述：获取点位项目信息
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTaskPoint"></param>
        /// <returns></returns>
        public DataTable SelectTaskItemByPoint(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            string strSQL = @"select item.*,point.POINT_NAME from T_MIS_MONITOR_TASK_ITEM item
                                                INNER JOIN (select ID,POINT_NAME from T_MIS_MONITOR_TASK_POINT {0}) point ON item.TASK_POINT_ID=point.ID";
            strSQL = string.Format(strSQL, BuildWhereStatement(tMisMonitorTaskPoint));
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        /// <summary>
        /// 拼接监测项目查询条件_包含
        /// </summary>
        /// <param name="ItemCondition">监测项目条件(逗号隔开)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public string getItemCondition_WithIn(string ItemCondition)
        {
            string sqlItemCondition = "";
            foreach (string value in ItemCondition.Split(','))
            {
                string curCondition = "";
                switch (value)
                {
                    case "1": curCondition = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'"; break;
                    case "2": curCondition = "T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='1'"; break;
                    default: break;
                }
                if (!string.IsNullOrEmpty(curCondition))
                    sqlItemCondition += (string.IsNullOrEmpty(sqlItemCondition) ? "" : " or ") + curCondition;
            }
            if (!string.IsNullOrEmpty(sqlItemCondition))
                sqlItemCondition = " and (" + sqlItemCondition + ") ";
            return sqlItemCondition;
        }
        /// <summary>
        /// 拼接监测项目查询条件_不包含
        /// </summary>
        /// <param name="ItemCondition">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public string getItemCondition_WithOut(string ItemCondition)
        {
            string sqlItemCondition = "";
            foreach (string value in ItemCondition.Split(','))
            {
                string curCondition = "";
                switch (value)
                {
                    case "1": curCondition = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT <> '是'"; break;
                    case "2": curCondition = "T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM <> '1'"; break;
                    default: break;
                }
                if (!string.IsNullOrEmpty(curCondition))
                    sqlItemCondition += " and " + curCondition;
            }
            return sqlItemCondition;
        }

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTaskPoint"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTaskPoint)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTaskPoint.ID.ToString()));
                }
                //任务ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorTaskPoint.TASK_ID.ToString()));
                }
                //监测子任务ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.SUBTASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorTaskPoint.SUBTASK_ID.ToString()));
                }
                //监测任务企业ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_ID = '{0}'", tMisMonitorTaskPoint.COMPANY_ID.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tMisMonitorTaskPoint.MONITOR_ID.ToString()));
                }
                //基础资料监测点ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tMisMonitorTaskPoint.POINT_ID.ToString()));
                }
                //委托书监测点ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.CONTRACT_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_POINT_ID = '{0}'", tMisMonitorTaskPoint.CONTRACT_POINT_ID.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tMisMonitorTaskPoint.POINT_NAME.ToString()));
                }
                //动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.DYNAMIC_ATTRIBUTE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DYNAMIC_ATTRIBUTE_ID = '{0}'", tMisMonitorTaskPoint.DYNAMIC_ATTRIBUTE_ID.ToString()));
                }
                //建成时间
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.CREATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_DATE = '{0}'", tMisMonitorTaskPoint.CREATE_DATE.ToString()));
                }
                //监测点位置
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tMisMonitorTaskPoint.ADDRESS.ToString()));
                }
                //经度
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.LONGITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tMisMonitorTaskPoint.LONGITUDE.ToString()));
                }
                //纬度
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.LATITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tMisMonitorTaskPoint.LATITUDE.ToString()));
                }
                //监测频次
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FREQ = '{0}'", tMisMonitorTaskPoint.FREQ.ToString()));
                }

                //采样频次
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.SAMPLE_FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FREQ = '{0}'", tMisMonitorTaskPoint.SAMPLE_FREQ.ToString()));
                }
                //点位描述
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tMisMonitorTaskPoint.DESCRIPTION.ToString()));
                }
                //国标条件项
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.NATIONAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NATIONAL_ST_CONDITION_ID = '{0}'", tMisMonitorTaskPoint.NATIONAL_ST_CONDITION_ID.ToString()));
                }
                //行标条件项ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.INDUSTRY_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INDUSTRY_ST_CONDITION_ID = '{0}'", tMisMonitorTaskPoint.INDUSTRY_ST_CONDITION_ID.ToString()));
                }
                //地标条件项_ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.LOCAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCAL_ST_CONDITION_ID = '{0}'", tMisMonitorTaskPoint.LOCAL_ST_CONDITION_ID.ToString()));
                }
                //是否删除
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisMonitorTaskPoint.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tMisMonitorTaskPoint.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorTaskPoint.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorTaskPoint.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorTaskPoint.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorTaskPoint.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorTaskPoint.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorTaskPoint.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion


    }
}
