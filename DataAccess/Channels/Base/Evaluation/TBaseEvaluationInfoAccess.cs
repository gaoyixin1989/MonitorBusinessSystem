using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Evaluation;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：监测类别管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = "select Count(*) from T_BASE_EVALUATION_INFO " + this.BuildWhereStatement(tBaseEvaluationInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseEvaluationInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationInfoVo Details(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_EVALUATION_INFO " + this.BuildWhereStatement(tBaseEvaluationInfo));
           return SqlHelper.ExecuteObject(new TBaseEvaluationInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationInfoVo> SelectByObject(TBaseEvaluationInfoVo tBaseEvaluationInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_INFO " + this.BuildWhereStatement(tBaseEvaluationInfo));
            return SqlHelper.ExecuteObjectList(tBaseEvaluationInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationInfoVo tBaseEvaluationInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_EVALUATION_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseEvaluationInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = "select * from T_BASE_EVALUATION_INFO " + this.BuildWhereStatement(tBaseEvaluationInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public TBaseEvaluationInfoVo SelectByObject(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = "select * from T_BASE_EVALUATION_INFO " + this.BuildWhereStatement(tBaseEvaluationInfo);
            return SqlHelper.ExecuteObject(new TBaseEvaluationInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseEvaluationInfo, TBaseEvaluationInfoVo.T_BASE_EVALUATION_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationInfo, TBaseEvaluationInfoVo.T_BASE_EVALUATION_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseEvaluationInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationInfoVo tBaseEvaluationInfo_UpdateSet, TBaseEvaluationInfoVo tBaseEvaluationInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationInfo_UpdateSet, TBaseEvaluationInfoVo.T_BASE_EVALUATION_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseEvaluationInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_EVALUATION_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            string strSQL = "delete from T_BASE_EVALUATION_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseEvaluationInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseEvaluationInfoVo tBaseEvaluationInfo,int iIndex,int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_EVALUATION_INFO WHERE IS_DEL='{0}'",tBaseEvaluationInfo.IS_DEL);
            if(!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_CODE))
            {
                strSQL+=String.Format("  AND STANDARD_CODE LIKE '%{0}%'",tBaseEvaluationInfo.STANDARD_CODE);
            }
            if(!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_NAME))
            {
                strSQL+=String.Format("  AND STANDARD_NAME LIKE '%{0}%'",tBaseEvaluationInfo.STANDARD_NAME);
            }

            if(!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_TYPE))
            {
                strSQL+=String.Format("   AND STANDARD_TYPE='{0}'",tBaseEvaluationInfo.STANDARD_TYPE);
            }

            if(!String.IsNullOrEmpty(tBaseEvaluationInfo.MONITOR_ID))
            {
                strSQL+=String.Format("   AND MONITOR_ID='{0}'",tBaseEvaluationInfo.MONITOR_ID);
            }

            return SqlHelper.ExecuteDataTable( BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_EVALUATION_INFO WHERE IS_DEL='{0}'", tBaseEvaluationInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_CODE))
            {
                strSQL += String.Format("  AND STANDARD_CODE LIKE '%{0}%'", tBaseEvaluationInfo.STANDARD_CODE);
            }
            if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_NAME))
            {
                strSQL += String.Format("  AND STANDARD_NAME LIKE '%{0}%'", tBaseEvaluationInfo.STANDARD_NAME);
            }

            if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_TYPE))
            {
                strSQL += String.Format("   AND STANDARD_TYPE='{0}'", tBaseEvaluationInfo.STANDARD_TYPE);
            }

            if (!String.IsNullOrEmpty(tBaseEvaluationInfo.MONITOR_ID))
            {
                strSQL += String.Format("   AND MONITOR_ID='{0}'", tBaseEvaluationInfo.MONITOR_ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseEvaluationInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseEvaluationInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseEvaluationInfo.ID.ToString()));
				}	
				//评价标准编号
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_CODE = '{0}'", tBaseEvaluationInfo.STANDARD_CODE.ToString()));
				}	
				//评价标准名称
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_NAME = '{0}'", tBaseEvaluationInfo.STANDARD_NAME.ToString()));
				}	
				//评价标准类别(国家标准、行业标准、地方标准、国际标准)
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.STANDARD_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_TYPE = '{0}'", tBaseEvaluationInfo.STANDARD_TYPE.ToString()));
				}	
				//监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.MONITOR_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseEvaluationInfo.MONITOR_ID.ToString()));
				}	
				//生效日期
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.EFFECTIVE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EFFECTIVE_DATE = '{0}'", tBaseEvaluationInfo.EFFECTIVE_DATE.ToString()));
				}	
				//附件路径
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.ATTACHMENT_URL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTACHMENT_URL = '{0}'", tBaseEvaluationInfo.ATTACHMENT_URL.ToString()));
				}	
				//评价标准描述
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.DESCRIPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseEvaluationInfo.DESCRIPTION.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseEvaluationInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseEvaluationInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseEvaluationInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseEvaluationInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseEvaluationInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseEvaluationInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseEvaluationInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseEvaluationInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
