using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.TRAIN;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训计划
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainPlanAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = "select Count(*) from T_OA_TRAIN_PLAN " + this.BuildWhereStatement(tOaTrainPlan);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainPlanVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_TRAIN_PLAN  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaTrainPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainPlan">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainPlanVo Details(TOaTrainPlanVo tOaTrainPlan)
        {
           string strSQL = String.Format("select * from  T_OA_TRAIN_PLAN " + this.BuildWhereStatement(tOaTrainPlan));
           return SqlHelper.ExecuteObject(new TOaTrainPlanVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainPlanVo> SelectByObject(TOaTrainPlanVo tOaTrainPlan, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_TRAIN_PLAN " + this.BuildWhereStatement(tOaTrainPlan));
            return SqlHelper.ExecuteObjectList(tOaTrainPlan, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainPlanVo tOaTrainPlan, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_TRAIN_PLAN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaTrainPlan));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = "select * from T_OA_TRAIN_PLAN " + this.BuildWhereStatement(tOaTrainPlan);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <returns></returns>
        public TOaTrainPlanVo SelectByObject(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = "select * from T_OA_TRAIN_PLAN " + this.BuildWhereStatement(tOaTrainPlan);
            return SqlHelper.ExecuteObject(new TOaTrainPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaTrainPlan, TOaTrainPlanVo.T_OA_TRAIN_PLAN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainPlan, TOaTrainPlanVo.T_OA_TRAIN_PLAN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaTrainPlan.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainPlan_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainPlanVo tOaTrainPlan_UpdateSet, TOaTrainPlanVo tOaTrainPlan_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainPlan_UpdateSet, TOaTrainPlanVo.T_OA_TRAIN_PLAN_TABLE);
            strSQL += this.BuildWhereStatement(tOaTrainPlan_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_TRAIN_PLAN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaTrainPlanVo tOaTrainPlan)
        {
            string strSQL = "delete from T_OA_TRAIN_PLAN ";
	    strSQL += this.BuildWhereStatement(tOaTrainPlan);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaTrainPlan"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaTrainPlanVo tOaTrainPlan)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaTrainPlan)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaTrainPlan.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaTrainPlan.ID.ToString()));
				}
                //培训主题
                if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_BT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRAIN_BT = '{0}'", tOaTrainPlan.TRAIN_BT.ToString()));
                }	
				//培训分类
				if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_TYPE = '{0}'", tOaTrainPlan.TRAIN_TYPE.ToString()));
				}	
				//培训对象
				if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_TO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_TO = '{0}'", tOaTrainPlan.TRAIN_TO.ToString()));
				}	
				//培训内容
				if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_INFO = '{0}'", tOaTrainPlan.TRAIN_INFO.ToString()));
				}	
				//培训目标
				if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_TARGET.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_TARGET = '{0}'", tOaTrainPlan.TRAIN_TARGET.ToString()));
				}	
				//培训时间
				if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_DATE = '{0}'", tOaTrainPlan.TRAIN_DATE.ToString()));
				}	
				//负责部门
				if (!String.IsNullOrEmpty(tOaTrainPlan.DEPT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_ID = '{0}'", tOaTrainPlan.DEPT_ID.ToString()));
				}	
				//考核办法
				if (!String.IsNullOrEmpty(tOaTrainPlan.EXAMINE_METHOD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_METHOD = '{0}'", tOaTrainPlan.EXAMINE_METHOD.ToString()));
				}	
				//计划年度
				if (!String.IsNullOrEmpty(tOaTrainPlan.PLAN_YEAR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PLAN_YEAR = '{0}'", tOaTrainPlan.PLAN_YEAR.ToString()));
				}
                //计划年度
                if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_COMPANY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRAIN_COMPANY = '{0}'", tOaTrainPlan.TRAIN_COMPANY.ToString()));
                }	
				//编制人
				if (!String.IsNullOrEmpty(tOaTrainPlan.DRAFT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DRAFT_ID = '{0}'", tOaTrainPlan.DRAFT_ID.ToString()));
				}	
				//编制时间
				if (!String.IsNullOrEmpty(tOaTrainPlan.DRAFT_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DRAFT_DATE = '{0}'", tOaTrainPlan.DRAFT_DATE.ToString()));
				}	
				//审批人
				if (!String.IsNullOrEmpty(tOaTrainPlan.APP_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_ID = '{0}'", tOaTrainPlan.APP_ID.ToString()));
				}	
				//审批时间
				if (!String.IsNullOrEmpty(tOaTrainPlan.APP_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DATE = '{0}'", tOaTrainPlan.APP_DATE.ToString()));
				}	
				//审批意见
				if (!String.IsNullOrEmpty(tOaTrainPlan.APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_INFO = '{0}'", tOaTrainPlan.APP_INFO.ToString()));
				}	
				//审批结果
				if (!String.IsNullOrEmpty(tOaTrainPlan.APP_RESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_RESULT = '{0}'", tOaTrainPlan.APP_RESULT.ToString()));
				}
                //审批环节
                if (!String.IsNullOrEmpty(tOaTrainPlan.APP_FLOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APP_FLOW = '{0}'", tOaTrainPlan.APP_FLOW.ToString()));
                }

                //流转状态
                if (!String.IsNullOrEmpty(tOaTrainPlan.FLOW_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FLOW_STATUS = '{0}'", tOaTrainPlan.FLOW_STATUS.ToString()));
                }
                //培训类别
                if (!String.IsNullOrEmpty(tOaTrainPlan.TYPES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TYPES = '{0}'", tOaTrainPlan.TYPES.ToString()));
                }
                //培训结果
                if (!String.IsNullOrEmpty(tOaTrainPlan.TRAIN_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRAIN_RESULT = '{0}'", tOaTrainPlan.TRAIN_RESULT.ToString()));
                }
                //培训结果录入人
                if (!String.IsNullOrEmpty(tOaTrainPlan.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tOaTrainPlan.RESULT_ID.ToString()));
                }
                //培训结果时间
                if (!String.IsNullOrEmpty(tOaTrainPlan.RESULT_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_DATE = '{0}'", tOaTrainPlan.RESULT_DATE.ToString()));
                }
                //技术负责人意见
                if (!String.IsNullOrEmpty(tOaTrainPlan.TECH_APP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TECH_APP = '{0}'", tOaTrainPlan.TECH_APP.ToString()));
                }
                //技术负责人ID
                if (!String.IsNullOrEmpty(tOaTrainPlan.TECH_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TECH_APP_ID = '{0}'", tOaTrainPlan.TECH_APP_ID.ToString()));
                }
                //技术负责人签署时间
                if (!String.IsNullOrEmpty(tOaTrainPlan.TECH_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TECH_APP_DATE = '{0}'", tOaTrainPlan.TECH_APP_DATE.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tOaTrainPlan.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaTrainPlan.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaTrainPlan.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaTrainPlan.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaTrainPlan.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaTrainPlan.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaTrainPlan.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaTrainPlan.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaTrainPlan.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaTrainPlan.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
