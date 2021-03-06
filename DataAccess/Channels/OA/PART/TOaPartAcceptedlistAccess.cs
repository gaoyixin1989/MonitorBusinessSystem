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
    /// 功能：物料验收单申请单关联表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartAcceptedlistAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = "select Count(*) from T_OA_PART_ACCEPTEDLIST " + this.BuildWhereStatement(tOaPartAcceptedlist);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedlistVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_ACCEPTEDLIST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartAcceptedlistVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedlistVo Details(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
           string strSQL = String.Format("select * from  T_OA_PART_ACCEPTEDLIST " + this.BuildWhereStatement(tOaPartAcceptedlist));
           return SqlHelper.ExecuteObject(new TOaPartAcceptedlistVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartAcceptedlistVo> SelectByObject(TOaPartAcceptedlistVo tOaPartAcceptedlist, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_ACCEPTEDLIST " + this.BuildWhereStatement(tOaPartAcceptedlist));
            return SqlHelper.ExecuteObjectList(tOaPartAcceptedlist, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_PART_ACCEPTEDLIST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartAcceptedlist));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = "select * from T_OA_PART_ACCEPTEDLIST " + this.BuildWhereStatement(tOaPartAcceptedlist);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <returns></returns>
        public TOaPartAcceptedlistVo SelectByObject(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = "select * from T_OA_PART_ACCEPTEDLIST " + this.BuildWhereStatement(tOaPartAcceptedlist);
            return SqlHelper.ExecuteObject(new TOaPartAcceptedlistVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartAcceptedlist, TOaPartAcceptedlistVo.T_OA_PART_ACCEPTEDLIST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAcceptedlist">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartAcceptedlist, TOaPartAcceptedlistVo.T_OA_PART_ACCEPTEDLIST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartAcceptedlist.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAcceptedlist_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartAcceptedlist_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedlistVo tOaPartAcceptedlist_UpdateSet, TOaPartAcceptedlistVo tOaPartAcceptedlist_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartAcceptedlist_UpdateSet, TOaPartAcceptedlistVo.T_OA_PART_ACCEPTEDLIST_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartAcceptedlist_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_ACCEPTEDLIST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            string strSQL = "delete from T_OA_PART_ACCEPTEDLIST ";
	    strSQL += this.BuildWhereStatement(tOaPartAcceptedlist);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取采购计划清单与验收清单
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist,TOaPartInfoVo tOaPartInfor, int iIndex, int iCount) {
            string strSQL = @"SELECT A.ID,A.REQUST_LST_ID,A.ACCEPTED_ID,B.REQUST_ID,B.STATUS,C.PART_ID,C.NEED_QUANTITY,C.USERDO,C.ENTERPRISE_NAME,
                                          C.RANGE,C.STANDARD,C.PRICE,C.AMOUNT,CONVERT(DATETIME, CONVERT(VARCHAR(100), C.RECIVEPART_DATE,23),111) AS RECIVEPART_DATE,CONVERT(DATETIME, CONVERT(VARCHAR(100), C.CHECK_DATE,23),111) AS CHECK_DATE,C.CHECK_RESULT,C.CHECK_USERID,C.REMARK1,D.PART_NAME,
                                          D.PART_CODE,D.PART_TYPE,D.UNIT,D.MODELS,D.INVENTORY,D.MEDIUM,D.PURE,D.ALARM,D.USEING,D.REQUEST,D.NARURE,
                                          E.REAL_NAME,E.USER_NAME,F.DICT_TEXT AS PARTTYPE,G.DICT_TEXT AS CHECKRESULT
                                          FROM dbo.T_OA_PART_ACCEPTEDLIST A
                                          LEFT JOIN dbo.T_OA_PART_BUY_REQUST_LST B ON A.REQUST_LST_ID=B.ID
                                          LEFT JOIN dbo.T_OA_PART_ACCEPTED C ON A.ACCEPTED_ID=C.ID
                                          LEFT JOIN dbo.T_OA_PART_INFO D ON D.ID=C.PART_ID
                                          LEFT JOIN dbo.T_SYS_USER E ON E.ID=C.CHECK_USERID 
                                          LEFT JOIN dbo.T_SYS_DICT F ON F.DICT_CODE=D.PART_TYPE AND F.DICT_TYPE='PART_TYPE'
                                          LEFT JOIN dbo.T_SYS_DICT G ON G.DICT_CODE=C.CHECK_RESULT AND G.DICT_TYPE='CheckResult'
                                          WHERE 1=1  AND D.IS_DEL='0'";
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REQUST_LST_ID)) {
                strSQL += String.Format(" AND A.REQUST_LST_ID='{0}'", tOaPartAcceptedlist.REQUST_LST_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ACCEPTED_ID))
            {
                strSQL += String.Format(" AND A.ACCEPTED_ID='{0}'", tOaPartAcceptedlist.ACCEPTED_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartAcceptedlist.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format(" AND D.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format(" AND D.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }

            if (!String.IsNullOrEmpty(tOaPartInfor.PART_TYPE))
            {
                strSQL += String.Format(" AND D.PART_TYPE in ({0})", tOaPartInfor.PART_TYPE);
            }

            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK4) && !String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), C.CHECK_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartAcceptedlist.REMARK4, tOaPartAcceptedlist.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 返回符合条件的采购计划与验收清单总记录数
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectUnionByTableCount(TOaPartAcceptedlistVo tOaPartAcceptedlist, TOaPartInfoVo tOaPartInfor)
        {
            string strSQL = @"SELECT A.ID,A.REQUST_LST_ID,A.ACCEPTED_ID,B.REQUST_ID,B.STATUS,C.PART_ID,C.NEED_QUANTITY,C.USERDO,C.ENTERPRISE_NAME,
                                          C.RANGE,C.STANDARD,C.PRICE,C.AMOUNT,CONVERT(DATETIME, CONVERT(VARCHAR(100), C.RECIVEPART_DATE,23),111) AS RECIVEPART_DATE,CONVERT(DATETIME, CONVERT(VARCHAR(100), C.CHECK_DATE,23),111) AS CHECK_DATE,C.CHECK_RESULT,C.CHECK_USERID,C.REMARK1,D.PART_NAME,
                                          D.PART_CODE,D.PART_TYPE,D.UNIT,D.MODELS,D.INVENTORY,D.MEDIUM,D.PURE,D.ALARM,D.USEING,D.REQUEST,D.NARURE,
                                          E.REAL_NAME,E.USER_NAME,F.DICT_TEXT AS PARTTYPE,G.DICT_TEXT AS CHECKRESULT
                                          FROM dbo.T_OA_PART_ACCEPTEDLIST A
                                          LEFT JOIN dbo.T_OA_PART_BUY_REQUST_LST B ON A.REQUST_LST_ID=B.ID
                                          LEFT JOIN dbo.T_OA_PART_ACCEPTED C ON A.ACCEPTED_ID=C.ID
                                          LEFT JOIN dbo.T_OA_PART_INFO D ON D.ID=C.PART_ID
                                          LEFT JOIN dbo.T_SYS_USER E ON E.ID=C.CHECK_USERID 
                                          LEFT JOIN dbo.T_SYS_DICT F ON F.DICT_CODE=D.PART_TYPE AND F.DICT_TYPE='PART_TYPE'
                                          LEFT JOIN dbo.T_SYS_DICT G ON G.DICT_CODE=C.CHECK_RESULT AND G.DICT_TYPE='CheckResult'
                                          WHERE 1=1  AND D.IS_DEL='0'";
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REQUST_LST_ID))
            {
                strSQL += String.Format(" AND A.REQUST_LST_ID='{0}'", tOaPartAcceptedlist.REQUST_LST_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ACCEPTED_ID))
            {
                strSQL += String.Format(" AND A.ACCEPTED_ID='{0}'", tOaPartAcceptedlist.ACCEPTED_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartAcceptedlist.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format(" AND D.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format(" AND D.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }

            if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK4) && !String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), C.CHECK_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartAcceptedlist.REMARK4, tOaPartAcceptedlist.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartAcceptedlist)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartAcceptedlist.ID.ToString()));
				}	
				//验收清单ID
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REQUST_LST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REQUST_LST_ID = '{0}'", tOaPartAcceptedlist.REQUST_LST_ID.ToString()));
				}	
				//采购申请清单ID
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.ACCEPTED_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPTED_ID = '{0}'", tOaPartAcceptedlist.ACCEPTED_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartAcceptedlist.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartAcceptedlist.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartAcceptedlist.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartAcceptedlist.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaPartAcceptedlist.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartAcceptedlist.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
