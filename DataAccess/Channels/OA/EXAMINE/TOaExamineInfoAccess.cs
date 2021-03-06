using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.EXAMINE;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = "select Count(*) from T_OA_EXAMINE_INFO " + this.BuildWhereStatement(tOaExamineInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaExamineInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EXAMINE_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaExamineInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaExamineInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaExamineInfoVo Details(TOaExamineInfoVo tOaExamineInfo)
        {
           string strSQL = String.Format("select * from  T_OA_EXAMINE_INFO " + this.BuildWhereStatement(tOaExamineInfo));
           return SqlHelper.ExecuteObject(new TOaExamineInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaExamineInfoVo> SelectByObject(TOaExamineInfoVo tOaExamineInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EXAMINE_INFO " + this.BuildWhereStatement(tOaExamineInfo));
            return SqlHelper.ExecuteObjectList(tOaExamineInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaExamineInfoVo tOaExamineInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EXAMINE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaExamineInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaExamineInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = "select * from T_OA_EXAMINE_INFO " + this.BuildWhereStatement(tOaExamineInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <returns></returns>
        public TOaExamineInfoVo SelectByObject(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = "select * from T_OA_EXAMINE_INFO " + this.BuildWhereStatement(tOaExamineInfo);
            return SqlHelper.ExecuteObject(new TOaExamineInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaExamineInfo, TOaExamineInfoVo.T_OA_EXAMINE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaExamineInfo, TOaExamineInfoVo.T_OA_EXAMINE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaExamineInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaExamineInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineInfoVo tOaExamineInfo_UpdateSet, TOaExamineInfoVo tOaExamineInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaExamineInfo_UpdateSet, TOaExamineInfoVo.T_OA_EXAMINE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaExamineInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EXAMINE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaExamineInfoVo tOaExamineInfo)
        {
            string strSQL = "delete from T_OA_EXAMINE_INFO ";
	    strSQL += this.BuildWhereStatement(tOaExamineInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaExamineInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaExamineInfoVo tOaExamineInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaExamineInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaExamineInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaExamineInfo.ID.ToString()));
				}	
				//用户ID
				if (!String.IsNullOrEmpty(tOaExamineInfo.USERID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USERID = '{0}'", tOaExamineInfo.USERID.ToString()));
				}	
				//考核类型，1事业单位工作人员年度考核，2专业技术人员年度考核
				if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_TYPE = '{0}'", tOaExamineInfo.EXAMINE_TYPE.ToString()));
				}	
				//考核时间
				if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_DATE = '{0}'", tOaExamineInfo.EXAMINE_DATE.ToString()));
				}	
				//考核年度
				if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_YEAR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_YEAR = '{0}'", tOaExamineInfo.EXAMINE_YEAR.ToString()));
				}	
				//考核状态,1未发送，2待审批，3已审批
				if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_STATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_STATUS = '{0}'", tOaExamineInfo.EXAMINE_STATUS.ToString()));
				}

                //个人考核内容
                if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_CONTENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXAMINE_CONTENT = '{0}'", tOaExamineInfo.EXAMINE_CONTENT.ToString()));
                }

                //考核等级
                if (!String.IsNullOrEmpty(tOaExamineInfo.EXAMINE_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXAMINE_LEVEL = '{0}'", tOaExamineInfo.EXAMINE_LEVEL.ToString()));
                }
				//部门考核评语
				if (!String.IsNullOrEmpty(tOaExamineInfo.DEPT_APP.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_APP = '{0}'", tOaExamineInfo.DEPT_APP.ToString()));
				}

                //部门考核评语负责人
                if (!String.IsNullOrEmpty(tOaExamineInfo.DEPT_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPT_APP_ID = '{0}'", tOaExamineInfo.DEPT_APP_ID.ToString()));
                }

                //部门考核评语时间
                if (!String.IsNullOrEmpty(tOaExamineInfo.DEPT_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPT_APP_DATE = '{0}'", tOaExamineInfo.DEPT_APP_DATE.ToString()));
                }	
				//单位考核评语
				if (!String.IsNullOrEmpty(tOaExamineInfo.LEADER_APP.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LEADER_APP = '{0}'", tOaExamineInfo.LEADER_APP.ToString()));
				}

                //单位考核评语责任人
                if (!String.IsNullOrEmpty(tOaExamineInfo.LEADER_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEADER_APP_ID = '{0}'", tOaExamineInfo.LEADER_APP_ID.ToString()));
                }

                //单位考核评语时间
                if (!String.IsNullOrEmpty(tOaExamineInfo.LEADER_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEADER_APP_DATE = '{0}'", tOaExamineInfo.LEADER_APP_DATE.ToString()));
                }	
				//主管单位意见
				if (!String.IsNullOrEmpty(tOaExamineInfo.SUPERIOR_APP.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUPERIOR_APP = '{0}'", tOaExamineInfo.SUPERIOR_APP.ToString()));
				}

                //主管单位意见负责人
                if (!String.IsNullOrEmpty(tOaExamineInfo.SUPERIOR_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUPERIOR_APP_ID = '{0}'", tOaExamineInfo.SUPERIOR_APP_ID.ToString()));
                }
                //主管单位意见时间
                if (!String.IsNullOrEmpty(tOaExamineInfo.SUPERIOR_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUPERIOR_APP_DATE = '{0}'", tOaExamineInfo.SUPERIOR_APP_DATE.ToString()));
                }	
				//个人意见
				if (!String.IsNullOrEmpty(tOaExamineInfo.OPINION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OPINION = '{0}'", tOaExamineInfo.OPINION.ToString()));
				}

                //个人意见时间
                if (!String.IsNullOrEmpty(tOaExamineInfo.OPINION_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OPINION_DATE = '{0}'", tOaExamineInfo.OPINION_DATE.ToString()));
                }	
				//复核或申诉情况说明
				if (!String.IsNullOrEmpty(tOaExamineInfo.APPEAL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPEAL = '{0}'", tOaExamineInfo.APPEAL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaExamineInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaExamineInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaExamineInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaExamineInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaExamineInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaExamineInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaExamineInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaExamineInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaExamineInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaExamineInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
