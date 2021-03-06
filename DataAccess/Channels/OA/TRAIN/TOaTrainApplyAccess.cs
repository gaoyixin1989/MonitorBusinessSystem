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
    /// 功能：培训申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainApplyAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = "select Count(*) from T_OA_TRAIN_APPLY " + this.BuildWhereStatement(tOaTrainApply);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainApplyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_TRAIN_APPLY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaTrainApplyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainApply">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainApplyVo Details(TOaTrainApplyVo tOaTrainApply)
        {
           string strSQL = String.Format("select * from  T_OA_TRAIN_APPLY " + this.BuildWhereStatement(tOaTrainApply));
           return SqlHelper.ExecuteObject(new TOaTrainApplyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainApplyVo> SelectByObject(TOaTrainApplyVo tOaTrainApply, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_TRAIN_APPLY " + this.BuildWhereStatement(tOaTrainApply));
            return SqlHelper.ExecuteObjectList(tOaTrainApply, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainApplyVo tOaTrainApply, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_TRAIN_APPLY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaTrainApply));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainApply"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = "select * from T_OA_TRAIN_APPLY " + this.BuildWhereStatement(tOaTrainApply);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <returns></returns>
        public TOaTrainApplyVo SelectByObject(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = "select * from T_OA_TRAIN_APPLY " + this.BuildWhereStatement(tOaTrainApply);
            return SqlHelper.ExecuteObject(new TOaTrainApplyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaTrainApply, TOaTrainApplyVo.T_OA_TRAIN_APPLY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainApply">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainApply, TOaTrainApplyVo.T_OA_TRAIN_APPLY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaTrainApply.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainApply_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainApply_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainApplyVo tOaTrainApply_UpdateSet, TOaTrainApplyVo tOaTrainApply_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainApply_UpdateSet, TOaTrainApplyVo.T_OA_TRAIN_APPLY_TABLE);
            strSQL += this.BuildWhereStatement(tOaTrainApply_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_TRAIN_APPLY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaTrainApplyVo tOaTrainApply)
        {
            string strSQL = "delete from T_OA_TRAIN_APPLY ";
	    strSQL += this.BuildWhereStatement(tOaTrainApply);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaTrainApply"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaTrainApplyVo tOaTrainApply)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaTrainApply)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaTrainApply.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaTrainApply.ID.ToString()));
				}	
				//计划内或计划外
				if (!String.IsNullOrEmpty(tOaTrainApply.IF_PALN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IF_PALN = '{0}'", tOaTrainApply.IF_PALN.ToString()));
				}	
				//培训项目
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_PROJECT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_PROJECT = '{0}'", tOaTrainApply.TRAIN_PROJECT.ToString()));
				}	
				//培训内容
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_CONTENT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_CONTENT = '{0}'", tOaTrainApply.TRAIN_CONTENT.ToString()));
				}	
				//天时
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_DAYS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_DAYS = '{0}'", tOaTrainApply.TRAIN_DAYS.ToString()));
				}	
				//学时
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_HOURS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_HOURS = '{0}'", tOaTrainApply.TRAIN_HOURS.ToString()));
				}	
				//开始时间
				if (!String.IsNullOrEmpty(tOaTrainApply.BEGIN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BEGIN_DATE = '{0}'", tOaTrainApply.BEGIN_DATE.ToString()));
				}	
				//结束时间
				if (!String.IsNullOrEmpty(tOaTrainApply.FINISH_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FINISH_DATE = '{0}'", tOaTrainApply.FINISH_DATE.ToString()));
				}	
				//培训单位
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_DEPT = '{0}'", tOaTrainApply.TRAIN_DEPT.ToString()));
				}	
				//培训地点
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_PLACE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_PLACE = '{0}'", tOaTrainApply.TRAIN_PLACE.ToString()));
				}	
				//培训教师
				if (!String.IsNullOrEmpty(tOaTrainApply.TRAIN_TEACHER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_TEACHER = '{0}'", tOaTrainApply.TRAIN_TEACHER.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tOaTrainApply.LINK_MAN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_MAN = '{0}'", tOaTrainApply.LINK_MAN.ToString()));
				}	
				//联系电话
				if (!String.IsNullOrEmpty(tOaTrainApply.LINK_TEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_TEL = '{0}'", tOaTrainApply.LINK_TEL.ToString()));
				}	
				//发证单位
				if (!String.IsNullOrEmpty(tOaTrainApply.CERTIFICATION_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CERTIFICATION_DEPT = '{0}'", tOaTrainApply.CERTIFICATION_DEPT.ToString()));
				}	
				//考核办法,1笔试、2口试、3实际操作，复选
				if (!String.IsNullOrEmpty(tOaTrainApply.TEST_METHOD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEST_METHOD = '{0}'", tOaTrainApply.TEST_METHOD.ToString()));
				}	
				//有效性评价
				if (!String.IsNullOrEmpty(tOaTrainApply.JUDGE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tOaTrainApply.JUDGE.ToString()));
				}	
				//部门审批人ID
				if (!String.IsNullOrEmpty(tOaTrainApply.DEPT_APP_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_APP_ID = '{0}'", tOaTrainApply.DEPT_APP_ID.ToString()));
				}	
				//部门审批时间
				if (!String.IsNullOrEmpty(tOaTrainApply.DEPT_APP_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_APP_DATE = '{0}'", tOaTrainApply.DEPT_APP_DATE.ToString()));
				}	
				//部门审批意见
				if (!String.IsNullOrEmpty(tOaTrainApply.DEPT_APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_APP_INFO = '{0}'", tOaTrainApply.DEPT_APP_INFO.ToString()));
				}	
				//站长审批人ID
				if (!String.IsNullOrEmpty(tOaTrainApply.LEADER_APP_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LEADER_APP_ID = '{0}'", tOaTrainApply.LEADER_APP_ID.ToString()));
				}	
				//站长审批时间
				if (!String.IsNullOrEmpty(tOaTrainApply.LEADER_APP_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LEADER_APP_DATE = '{0}'", tOaTrainApply.LEADER_APP_DATE.ToString()));
				}	
				//站长审批意见
				if (!String.IsNullOrEmpty(tOaTrainApply.LEADER_APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LEADER_APP_INFO = '{0}'", tOaTrainApply.LEADER_APP_INFO.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaTrainApply.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaTrainApply.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaTrainApply.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaTrainApply.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaTrainApply.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaTrainApply.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaTrainApply.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaTrainApply.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaTrainApply.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaTrainApply.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
