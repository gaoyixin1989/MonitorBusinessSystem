using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.PART;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料验收清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartAcceptedAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = "select Count(*) from T_OA_PART_ACCEPTED " + this.BuildWhereStatement(tOaPartAccepted);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_ACCEPTED  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartAcceptedVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartAccepted">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedVo Details(TOaPartAcceptedVo tOaPartAccepted)
        {
           string strSQL = String.Format("select * from  T_OA_PART_ACCEPTED " + this.BuildWhereStatement(tOaPartAccepted));
           return SqlHelper.ExecuteObject(new TOaPartAcceptedVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartAcceptedVo> SelectByObject(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_ACCEPTED " + this.BuildWhereStatement(tOaPartAccepted));
            return SqlHelper.ExecuteObjectList(tOaPartAccepted, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_PART_ACCEPTED {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartAccepted));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {

            string strSQL = @" select A.*,B.PART_NAME,B.PART_CODE,B.UNIT,B.MODELS,C.REAL_NAME,C.USER_NAME 
                                from T_OA_PART_ACCEPTED A  
                                JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0'  
                                LEFT JOIN T_SYS_USER C ON C.ID=A.CHECK_USERID WHERE 1=1 ";

            if (!String.IsNullOrEmpty(tOaPartAccepted.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID IN ('{0}')", tOaPartAccepted.PART_ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        public int GetSelectByTableExCount(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = @" select A.*,B.PART_NAME,B.PART_CODE,B.UNIT,B.MODELS,C.REAL_NAME,C.USER_NAME 
                                from T_OA_PART_ACCEPTED A  
                                JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0'  
                                LEFT JOIN T_SYS_USER C ON C.ID=A.CHECK_USERID WHERE 1=1 ";

            if (!String.IsNullOrEmpty(tOaPartAccepted.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID IN ('{0}')", tOaPartAccepted.PART_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取入库物料时间段对象DataTable 黄进军20141018
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTimeList(TOaPartAcceptedVo tOaPartAccepted, string startTime, string endTime, int iIndex, int iCount)
        {

            string strSQL = @" select A.*,B.PART_NAME,B.PART_CODE,B.UNIT,B.MODELS,C.REAL_NAME,C.USER_NAME 
                                from T_OA_PART_ACCEPTED A  
                                JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0'  
                                LEFT JOIN T_SYS_USER C ON C.ID=A.CHECK_USERID WHERE 1=1 ";

            if (!String.IsNullOrEmpty(tOaPartAccepted.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID IN ('{0}')", tOaPartAccepted.PART_ID);
            }
            if (startTime != "" && endTime != "")
            {
                strSQL += String.Format(" AND A.RECIVEPART_DATE >='{0}' and A.RECIVEPART_DATE<='{1}'", startTime, endTime);
            }
            if (startTime == "" || endTime == "")
            {
                if (startTime != "")
                {
                    strSQL += String.Format(" AND A.RECIVEPART_DATE ='{0}'", startTime);
                }
                if (endTime != "")
                {
                    strSQL += String.Format(" AND A.RECIVEPART_DATE ='{0}'", endTime);
                }
            }
            if (!String.IsNullOrEmpty(tOaPartAccepted.CHECK_USERID))
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '%{0}%'", tOaPartAccepted.CHECK_USERID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        //获取物料时间段分页 黄进军添加20141028
        public int GetSelectByTimeListCount(TOaPartAcceptedVo tOaPartAccepted, string startTime, string endTime)
        {
            string strSQL = @" select A.*,B.PART_NAME,B.PART_CODE,B.UNIT,B.MODELS,C.REAL_NAME,C.USER_NAME 
                                from T_OA_PART_ACCEPTED A  
                                JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0'  
                                LEFT JOIN T_SYS_USER C ON C.ID=A.CHECK_USERID WHERE 1=1 ";

            if (!String.IsNullOrEmpty(tOaPartAccepted.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID IN ('{0}')", tOaPartAccepted.PART_ID);
            }
            if (startTime != "" && endTime != "")
            {
                strSQL += String.Format(" AND A.RECIVEPART_DATE >='{0}' and A.RECIVEPART_DATE<='{1}'", startTime, endTime);
            }
            if (startTime == "" || endTime == "")
            {
                if (startTime != "")
                {
                    strSQL += String.Format(" AND A.RECIVEPART_DATE ='{0}'", startTime);
                }
                if (endTime != "")
                {
                    strSQL += String.Format(" AND A.RECIVEPART_DATE ='{0}'", endTime);
                }
            }
            if (!String.IsNullOrEmpty(tOaPartAccepted.CHECK_USERID))
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '%{0}%'",tOaPartAccepted.CHECK_USERID);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartAccepted"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = "select * from T_OA_PART_ACCEPTED " + this.BuildWhereStatement(tOaPartAccepted);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <returns></returns>
        public TOaPartAcceptedVo SelectByObject(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = "select * from T_OA_PART_ACCEPTED " + this.BuildWhereStatement(tOaPartAccepted);
            return SqlHelper.ExecuteObject(new TOaPartAcceptedVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartAccepted, TOaPartAcceptedVo.T_OA_PART_ACCEPTED_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAccepted">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartAccepted, TOaPartAcceptedVo.T_OA_PART_ACCEPTED_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartAccepted.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAccepted_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartAccepted_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedVo tOaPartAccepted_UpdateSet, TOaPartAcceptedVo tOaPartAccepted_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartAccepted_UpdateSet, TOaPartAcceptedVo.T_OA_PART_ACCEPTED_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartAccepted_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_ACCEPTED where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartAcceptedVo tOaPartAccepted)
        {
            string strSQL = "delete from T_OA_PART_ACCEPTED ";
	    strSQL += this.BuildWhereStatement(tOaPartAccepted);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartAccepted"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartAcceptedVo tOaPartAccepted)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartAccepted)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaPartAccepted.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartAccepted.ID.ToString()));
				}	
				//物料ID
				if (!String.IsNullOrEmpty(tOaPartAccepted.PART_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PART_ID = '{0}'", tOaPartAccepted.PART_ID.ToString()));
				}	
				//需求数量
				if (!String.IsNullOrEmpty(tOaPartAccepted.NEED_QUANTITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NEED_QUANTITY = '{0}'", tOaPartAccepted.NEED_QUANTITY.ToString()));
				}	
				//用途
				if (!String.IsNullOrEmpty(tOaPartAccepted.USERDO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USERDO = '{0}'", tOaPartAccepted.USERDO.ToString()));
				}	
				//供应商名称
				if (!String.IsNullOrEmpty(tOaPartAccepted.ENTERPRISE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENTERPRISE_NAME = '{0}'", tOaPartAccepted.ENTERPRISE_NAME.ToString()));
				}	
				//浓度范围
				if (!String.IsNullOrEmpty(tOaPartAccepted.RANGE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RANGE = '{0}'", tOaPartAccepted.RANGE.ToString()));
				}	
				//标准值/不确定度
				if (!String.IsNullOrEmpty(tOaPartAccepted.STANDARD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tOaPartAccepted.STANDARD.ToString()));
				}	
				//稀释倍数
				if (!String.IsNullOrEmpty(tOaPartAccepted.RATIO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RATIO = '{0}'", tOaPartAccepted.RATIO.ToString()));
				}	
				//单价
				if (!String.IsNullOrEmpty(tOaPartAccepted.PRICE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRICE = '{0}'", tOaPartAccepted.PRICE.ToString()));
				}	
				//金额
				if (!String.IsNullOrEmpty(tOaPartAccepted.AMOUNT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AMOUNT = '{0}'", tOaPartAccepted.AMOUNT.ToString()));
				}	
				//收货日期
				if (!String.IsNullOrEmpty(tOaPartAccepted.RECIVEPART_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RECIVEPART_DATE = '{0}'", tOaPartAccepted.RECIVEPART_DATE.ToString()));
				}	
				//检验日期
				if (!String.IsNullOrEmpty(tOaPartAccepted.CHECK_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_DATE = '{0}'", tOaPartAccepted.CHECK_DATE.ToString()));
				}	
				//验收情况
				if (!String.IsNullOrEmpty(tOaPartAccepted.CHECK_RESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_RESULT = '{0}'", tOaPartAccepted.CHECK_RESULT.ToString()));
				}	
				//验收人ID
				if (!String.IsNullOrEmpty(tOaPartAccepted.CHECK_USERID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_USERID = '{0}'", tOaPartAccepted.CHECK_USERID.ToString()));
				}
                //标识
                if (!String.IsNullOrEmpty(tOaPartAccepted.FLAG.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FLAG = '{0}'", tOaPartAccepted.FLAG.ToString()));
                }
				//备注1
				if (!String.IsNullOrEmpty(tOaPartAccepted.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartAccepted.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartAccepted.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartAccepted.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartAccepted.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartAccepted.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartAccepted.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartAccepted.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaPartAccepted.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartAccepted.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
