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
    /// 功能：监测任务属性值表
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskAttrbuteAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK_ATTRBUTE " + this.BuildWhereStatement(tMisMonitorTaskAttrbute);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskAttrbuteVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ATTRBUTE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskAttrbuteVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskAttrbuteVo Details(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ATTRBUTE " + this.BuildWhereStatement(tMisMonitorTaskAttrbute));
            return SqlHelper.ExecuteObject(new TMisMonitorTaskAttrbuteVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskAttrbuteVo> SelectByObject(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ATTRBUTE " + this.BuildWhereStatement(tMisMonitorTaskAttrbute));
            return SqlHelper.ExecuteObjectList(tMisMonitorTaskAttrbute, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_TASK_ATTRBUTE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTaskAttrbute));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_ATTRBUTE " + this.BuildWhereStatement(tMisMonitorTaskAttrbute);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskAttrbuteVo SelectByObject(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_ATTRBUTE " + this.BuildWhereStatement(tMisMonitorTaskAttrbute);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskAttrbuteVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTaskAttrbute, TMisMonitorTaskAttrbuteVo.T_MIS_MONITOR_TASK_ATTRBUTE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskAttrbute, TMisMonitorTaskAttrbuteVo.T_MIS_MONITOR_TASK_ATTRBUTE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTaskAttrbute.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskAttrbute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute_UpdateSet, TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskAttrbute_UpdateSet, TMisMonitorTaskAttrbuteVo.T_MIS_MONITOR_TASK_ATTRBUTE_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTaskAttrbute_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK_ATTRBUTE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK_ATTRBUTE ";
            strSQL += this.BuildWhereStatement(tMisMonitorTaskAttrbute);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTaskAttrbute)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTaskAttrbute.ID.ToString()));
                }
                //对象类型
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.OBJECT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OBJECT_TYPE = '{0}'", tMisMonitorTaskAttrbute.OBJECT_TYPE.ToString()));
                }
                //对象ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.OBJECT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OBJECT_ID = '{0}'", tMisMonitorTaskAttrbute.OBJECT_ID.ToString()));
                }
                //属性名称
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.ATTRBUTE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ATTRBUTE_CODE = '{0}'", tMisMonitorTaskAttrbute.ATTRBUTE_CODE.ToString()));
                }
                //属性值
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.ATTRBUTE_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ATTRBUTE_VALUE = '{0}'", tMisMonitorTaskAttrbute.ATTRBUTE_VALUE.ToString()));
                }
                //是否删除
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisMonitorTaskAttrbute.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorTaskAttrbute.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorTaskAttrbute.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorTaskAttrbute.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorTaskAttrbute.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorTaskAttrbute.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorTaskAttrbute.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
