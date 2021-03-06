using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.MonitorType
{
    /// <summary>
    /// 功能：监测类别管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMonitorTypeInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = "select Count(*) from T_BASE_MONITOR_TYPE_INFO " + this.BuildWhereStatement(tBaseMonitorTypeInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMonitorTypeInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_MONITOR_TYPE_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseMonitorTypeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMonitorTypeInfoVo Details(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = String.Format("select * from  T_BASE_MONITOR_TYPE_INFO " + this.BuildWhereStatement(tBaseMonitorTypeInfo));
            return SqlHelper.ExecuteObject(new TBaseMonitorTypeInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMonitorTypeInfoVo> SelectByObject(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_MONITOR_TYPE_INFO " + this.BuildWhereStatement(tBaseMonitorTypeInfo));
            return SqlHelper.ExecuteObjectList(tBaseMonitorTypeInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// select通用查询函数
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strWhere">where语句</param>
        /// <param name="strIdfield">id 列名</param>
        /// <param name="strTextField">Text 列名</param>
        /// <param name="strDistinct">是否distinct</param>
        /// <returns></returns>
        public DataTable SelectByTable(string strTableName, string strWhere, string strIdfield, string strTextField, string strDistinct, string strOrder)
        {
            string strSQL = "select "
                + ((strDistinct.Length > 0 && strDistinct == "1") ? "distinct" : "")
                + @"  {0} from [{1}] where 1=1 {2}";
            var strSqlselect = "";
            if (strIdfield.Length > 0)
            {
                strSqlselect += "[" + strIdfield + "] as [id]";
                strSqlselect += ",[" + strIdfield + "] as [value]";
            }
            if (strTextField.Length > 0)
            {
                strSqlselect += ",[" + strTextField + "] as [text]";
            }

            string strSqlWhere = "";
            if (strWhere.Length > 0)
            {
                string[] arrWhere = strWhere.Split('-');
                for (int i = 0; i < arrWhere.Length; i++)
                {
                    string[] arrWhereNode = arrWhere[i].Split('|');
                    strSqlWhere += " and " + arrWhereNode[0] + "='" + arrWhereNode[1] + "'";
                }
            }
            if (strOrder.Length > 0)
            {
                strSqlWhere += " order by " + strOrder;
            }


            strSQL = string.Format(strSQL, strSqlselect, strTableName, strSqlWhere);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_MONITOR_TYPE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseMonitorTypeInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress<TBaseMonitorTypeInfoVo>(tBaseMonitorTypeInfo, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = "select * from T_BASE_MONITOR_TYPE_INFO " + this.BuildWhereStatement(tBaseMonitorTypeInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <returns></returns>
        public TBaseMonitorTypeInfoVo SelectByObject(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = "select * from T_BASE_MONITOR_TYPE_INFO " + this.BuildWhereStatement(tBaseMonitorTypeInfo);
            return SqlHelper.ExecuteObject(new TBaseMonitorTypeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseMonitorTypeInfo, TBaseMonitorTypeInfoVo.T_BASE_MONITOR_TYPE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMonitorTypeInfo, TBaseMonitorTypeInfoVo.T_BASE_MONITOR_TYPE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseMonitorTypeInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseMonitorTypeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo_UpdateSet, TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMonitorTypeInfo_UpdateSet, TBaseMonitorTypeInfoVo.T_BASE_MONITOR_TYPE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseMonitorTypeInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_MONITOR_TYPE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            string strSQL = "delete from T_BASE_MONITOR_TYPE_INFO ";
            strSQL += this.BuildWhereStatement(tBaseMonitorTypeInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获得任务所监测类别(List)
        /// 创建时间：2012-12-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemTypeByTask(string strTaskID)
        {
            string strSQL = @"select MONITOR_TYPE_NAME,ID 
                                                from T_BASE_MONITOR_TYPE_INFO
                                                    where ID in (select MONITOR_ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}')";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        // <summary>
        /// 功能描述：获取监测类别环境质量除外
        /// 创建时间：2014-11-21
        /// 创建人：魏林
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable getMonitorType()
        {
            string strSQL = @"select * from T_BASE_MONITOR_TYPE_INFO where REMARK5='0' and IS_DEL='0' and ISNULL(REMARK1,'')=''";
            
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseMonitorTypeInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseMonitorTypeInfo.ID.ToString()));
                }
                //监测类别名称
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.MONITOR_TYPE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TYPE_NAME = '{0}'", tBaseMonitorTypeInfo.MONITOR_TYPE_NAME.ToString()));
                }
                ////监测类别样品编码规则
                //if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.IDENTIFY_CODE.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND IDENTIFY_CODE = '{0}'", tBaseMonitorTypeInfo.IDENTIFY_CODE.ToString()));
                //}
                //监测类别描述
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseMonitorTypeInfo.DESCRIPTION.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseMonitorTypeInfo.IS_DEL.ToString()));
                }
                //排列属性
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.SORT_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SORT_NUM = '{0}'", tBaseMonitorTypeInfo.SORT_NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseMonitorTypeInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseMonitorTypeInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseMonitorTypeInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseMonitorTypeInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseMonitorTypeInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseMonitorTypeInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
