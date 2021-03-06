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
    /// 功能：员工培训记录
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = "select Count(*) from T_OA_TRAIN_INFO " + this.BuildWhereStatement(tOaTrainInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_TRAIN_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaTrainInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainInfoVo Details(TOaTrainInfoVo tOaTrainInfo)
        {
           string strSQL = String.Format("select * from  T_OA_TRAIN_INFO " + this.BuildWhereStatement(tOaTrainInfo));
           return SqlHelper.ExecuteObject(new TOaTrainInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainInfoVo> SelectByObject(TOaTrainInfoVo tOaTrainInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_TRAIN_INFO " + this.BuildWhereStatement(tOaTrainInfo));
            return SqlHelper.ExecuteObjectList(tOaTrainInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainInfoVo tOaTrainInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_TRAIN_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaTrainInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = "select * from T_OA_TRAIN_INFO " + this.BuildWhereStatement(tOaTrainInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <returns></returns>
        public TOaTrainInfoVo SelectByObject(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = "select * from T_OA_TRAIN_INFO " + this.BuildWhereStatement(tOaTrainInfo);
            return SqlHelper.ExecuteObject(new TOaTrainInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaTrainInfo, TOaTrainInfoVo.T_OA_TRAIN_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainInfo, TOaTrainInfoVo.T_OA_TRAIN_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaTrainInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainInfoVo tOaTrainInfo_UpdateSet, TOaTrainInfoVo tOaTrainInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainInfo_UpdateSet, TOaTrainInfoVo.T_OA_TRAIN_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaTrainInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_TRAIN_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaTrainInfoVo tOaTrainInfo)
        {
            string strSQL = "delete from T_OA_TRAIN_INFO ";
	    strSQL += this.BuildWhereStatement(tOaTrainInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaTrainInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaTrainInfoVo tOaTrainInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaTrainInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaTrainInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaTrainInfo.ID.ToString()));
				}	
				//培训ID
				if (!String.IsNullOrEmpty(tOaTrainInfo.TRAIN_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAIN_ID = '{0}'", tOaTrainInfo.TRAIN_ID.ToString()));
				}	
				//员工ID
				if (!String.IsNullOrEmpty(tOaTrainInfo.EMPLOYE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYE_ID = '{0}'", tOaTrainInfo.EMPLOYE_ID.ToString()));
				}	
				//培训教材评估
				if (!String.IsNullOrEmpty(tOaTrainInfo.ENTRYDATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENTRYDATE = '{0}'", tOaTrainInfo.ENTRYDATE.ToString()));
				}	
				//培训教师评估
				if (!String.IsNullOrEmpty(tOaTrainInfo.TRAINDATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAINDATE = '{0}'", tOaTrainInfo.TRAINDATE.ToString()));
				}	
				//培训成绩
				if (!String.IsNullOrEmpty(tOaTrainInfo.TRAINPROJECT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAINPROJECT = '{0}'", tOaTrainInfo.TRAINPROJECT.ToString()));
				}	
				//培训结果
				if (!String.IsNullOrEmpty(tOaTrainInfo.TRAINRESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAINRESULT = '{0}'", tOaTrainInfo.TRAINRESULT.ToString()));
				}	
				//证书编号
				if (!String.IsNullOrEmpty(tOaTrainInfo.CERTIFICATECODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CERTIFICATECODE = '{0}'", tOaTrainInfo.CERTIFICATECODE.ToString()));
				}	
				//自我总结
				if (!String.IsNullOrEmpty(tOaTrainInfo.TRAINCONTENT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TRAINCONTENT = '{0}'", tOaTrainInfo.TRAINCONTENT.ToString()));
				}	
				//总结时间
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARKS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARKS = '{0}'", tOaTrainInfo.REMARKS.ToString()));
				}	
				//理论掌握能力评估
				if (!String.IsNullOrEmpty(tOaTrainInfo.THEORY_SKILL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND THEORY_SKILL = '{0}'", tOaTrainInfo.THEORY_SKILL.ToString()));
				}	
				//实际操作能力评估
				if (!String.IsNullOrEmpty(tOaTrainInfo.OPER_SKILL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OPER_SKILL = '{0}'", tOaTrainInfo.OPER_SKILL.ToString()));
				}	
				//样品和质控样分析能力评估
				if (!String.IsNullOrEmpty(tOaTrainInfo.TEST_SKILL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEST_SKILL = '{0}'", tOaTrainInfo.TEST_SKILL.ToString()));
				}	
				//评价结论
				if (!String.IsNullOrEmpty(tOaTrainInfo.JUDGE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tOaTrainInfo.JUDGE.ToString()));
				}	
				//评价人ID
				if (!String.IsNullOrEmpty(tOaTrainInfo.JUDGE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND JUDGE_ID = '{0}'", tOaTrainInfo.JUDGE_ID.ToString()));
				}	
				//评价日期
				if (!String.IsNullOrEmpty(tOaTrainInfo.JUDGE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND JUDGE_DATE = '{0}'", tOaTrainInfo.JUDGE_DATE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaTrainInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaTrainInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaTrainInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaTrainInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaTrainInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaTrainInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
