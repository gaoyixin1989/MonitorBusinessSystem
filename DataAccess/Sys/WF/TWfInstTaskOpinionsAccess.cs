using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节附属评论表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskOpinionsAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = "select Count(*) from T_WF_INST_TASK_OPINIONS " + this.BuildWhereStatement(tWfInstTaskOpinions);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskOpinionsVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_TASK_OPINIONS  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfInstTaskOpinionsVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskOpinionsVo Details(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = String.Format("select * from  T_WF_INST_TASK_OPINIONS " + this.BuildWhereStatement(tWfInstTaskOpinions));
            return SqlHelper.ExecuteObject(new TWfInstTaskOpinionsVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskOpinionsVo> SelectByObject(TWfInstTaskOpinionsVo tWfInstTaskOpinions, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_INST_TASK_OPINIONS " + this.BuildWhereStatement(tWfInstTaskOpinions));
            return SqlHelper.ExecuteObjectList(tWfInstTaskOpinions, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstTaskOpinionsVo tWfInstTaskOpinions, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_INST_TASK_OPINIONS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstTaskOpinions));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskOpinions"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = "select * from T_WF_INST_TASK_OPINIONS " + this.BuildWhereStatement(tWfInstTaskOpinions);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载【和Detail左连接】
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskOpinions"></param>
        /// <returns></returns>
        public DataTable SelectByTableJoinDetail(string strInstWFID)
        {
            string strSQL = "SELECT * FROM  T_WF_INST_TASK_OPINIONS left join t_wf_inst_task_detail on T_WF_INST_TASK_OPINIONS.wf_inst_task_id = t_wf_inst_task_detail.id where 1=1 AND T_WF_INST_TASK_OPINIONS.WF_INST_ID ='" + strInstWFID + "' ";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <returns></returns>
        public TWfInstTaskOpinionsVo SelectByObject(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = "select * from T_WF_INST_TASK_OPINIONS " + this.BuildWhereStatement(tWfInstTaskOpinions);
            return SqlHelper.ExecuteObject(new TWfInstTaskOpinionsVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstTaskOpinions, TWfInstTaskOpinionsVo.T_WF_INST_TASK_OPINIONS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskOpinions">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskOpinions, TWfInstTaskOpinionsVo.T_WF_INST_TASK_OPINIONS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstTaskOpinions.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskOpinions_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstTaskOpinions_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskOpinionsVo tWfInstTaskOpinions_UpdateSet, TWfInstTaskOpinionsVo tWfInstTaskOpinions_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskOpinions_UpdateSet, TWfInstTaskOpinionsVo.T_WF_INST_TASK_OPINIONS_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstTaskOpinions_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_TASK_OPINIONS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            string strSQL = "delete from T_WF_INST_TASK_OPINIONS ";
            strSQL += this.BuildWhereStatement(tWfInstTaskOpinions);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstTaskOpinions"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstTaskOpinions)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstTaskOpinions.ID.ToString()));
                }
                //环节实例编号
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_INST_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_TASK_ID = '{0}'", tWfInstTaskOpinions.WF_INST_TASK_ID.ToString()));
                }
                //流程实例编号
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_INST_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_ID = '{0}'", tWfInstTaskOpinions.WF_INST_ID.ToString()));
                }
                //评论内容
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_IT_OPINION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_IT_OPINION = '{0}'", tWfInstTaskOpinions.WF_IT_OPINION.ToString()));
                }
                //评论类型
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_IT_OPINION_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_IT_OPINION_TYPE = '{0}'", tWfInstTaskOpinions.WF_IT_OPINION_TYPE.ToString()));
                }
                //评论人
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_IT_OPINION_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_IT_OPINION_USER = '{0}'", tWfInstTaskOpinions.WF_IT_OPINION_USER.ToString()));
                }
                //显示方式(只显示上一条,全显示)
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.WF_IT_SHOW_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_IT_SHOW_TYPE = '{0}'", tWfInstTaskOpinions.WF_IT_SHOW_TYPE.ToString()));
                }

                //增加评论时间
                if (!string.IsNullOrEmpty(tWfInstTaskOpinions.WF_IT_OPINION_TIME.Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_IT_OPINION_TIME = '{0}'", tWfInstTaskOpinions.WF_IT_OPINION_TIME.ToString()));
                }


                //删除标记
                if (!String.IsNullOrEmpty(tWfInstTaskOpinions.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tWfInstTaskOpinions.IS_DEL.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
