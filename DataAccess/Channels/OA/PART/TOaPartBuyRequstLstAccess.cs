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
    /// 功能：物料采购申请清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstLstAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = "select Count(*) from T_OA_PART_BUY_REQUST_LST " + this.BuildWhereStatement(tOaPartBuyRequstLst);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstLstVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST_LST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartBuyRequstLstVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstLstVo Details(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
           string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST_LST " + this.BuildWhereStatement(tOaPartBuyRequstLst));
           return SqlHelper.ExecuteObject(new TOaPartBuyRequstLstVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartBuyRequstLstVo> SelectByObject(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST_LST " + this.BuildWhereStatement(tOaPartBuyRequstLst));
            return SqlHelper.ExecuteObjectList(tOaPartBuyRequstLst, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_PART_BUY_REQUST_LST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartBuyRequstLst));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartBuyRequstLst"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = "select * from T_OA_PART_BUY_REQUST_LST " + this.BuildWhereStatement(tOaPartBuyRequstLst);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <returns></returns>
        public TOaPartBuyRequstLstVo SelectByObject(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = "select * from T_OA_PART_BUY_REQUST_LST " + this.BuildWhereStatement(tOaPartBuyRequstLst);
            return SqlHelper.ExecuteObject(new TOaPartBuyRequstLstVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartBuyRequstLst, TOaPartBuyRequstLstVo.T_OA_PART_BUY_REQUST_LST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartBuyRequstLst, TOaPartBuyRequstLstVo.T_OA_PART_BUY_REQUST_LST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartBuyRequstLst.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequstLst_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartBuyRequstLst_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstLstVo tOaPartBuyRequstLst_UpdateSet, TOaPartBuyRequstLstVo tOaPartBuyRequstLst_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartBuyRequstLst_UpdateSet, TOaPartBuyRequstLstVo.T_OA_PART_BUY_REQUST_LST_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartBuyRequstLst_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_BUY_REQUST_LST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            string strSQL = "delete from T_OA_PART_BUY_REQUST_LST ";
	    strSQL += this.BuildWhereStatement(tOaPartBuyRequstLst);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        public DataTable SelectRemarks(string ID)
        {
            string strSQL = "select B.REMARK1 from T_OA_PART_BUY_REQUST_LST A  left join T_OA_PART_BUY_REQUST B on A.REQUST_ID=B.ID  where A.ID='{0}'";
            strSQL = string.Format(strSQL, ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable GetInfo(string ID)
        {
            string strSQL = "select B.APPLY_USER_ID as APPLY_USER_ID,A.PART_ID as PART_ID  from T_OA_PART_BUY_REQUST_LST A  left join T_OA_PART_BUY_REQUST B on A.REQUST_ID=B.ID  where A.ID='{0}'";
            strSQL = string.Format(strSQL, ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        //物料采购申请id
        public DataTable SelectREQUSTID(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select C.REQUST_ID ");
            sb.Append("  from T_OA_PART_ACCEPTEDLIST a ");
            sb.Append("   left join T_OA_PART_ACCEPTED b on a.ACCEPTED_ID=b.ID");
            sb.Append("    LEFT JOIN T_OA_PART_BUY_REQUST_LST C ON A.REQUST_LST_ID=C.ID");
            sb.Append("  where B.ID='" + ID + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }


        /// <summary>
        /// 获取用户采购计划列表
        /// 何海亮修改：添加两个字段
        /// </summary>
        /// <param name="tOaPartBuyRequstLst"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionPartByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst,TOaPartInfoVo tOaPartInfor,TOaPartBuyRequstVo tOaPartBuyRequest, int iIndex,int iCount)
        {
            string strSQL = @" SELECT A.ID,A.PART_ID,A.NEED_QUANTITY,A.BUDGET_MONEY,CONVERT(DATETIME, CONVERT(VARCHAR(100), A.DELIVERY_DATE,23),111)  AS DELIVERY_DATE,A.USERDO,A.STATUS," +
                                              " B.PART_CODE,B.PART_TYPE,B.PART_NAME,B.UNIT,B.MODELS,B.INVENTORY,B.MEDIUM,B.PURE," +
                                               " B.ALARM,B.REQUEST,B.USEING,B.NARURE," +
                                               " C.ID as APPLY_ID,C.APPLY_TYPE,C.APPLY_USER_ID,C.APPLY_DEPT_ID,C.APPLY_DATE,C.APPLY_TITLE,D.REAL_NAME,D.USER_NAME,E.DICT_TEXT AS DEPT_NAME" +
                                               " FROM dbo.T_OA_PART_BUY_REQUST_LST A " +
                                               " LEFT JOIN dbo.T_OA_PART_INFO B ON B.ID=A.PART_ID" +
                                               " LEFT JOIN dbo.T_OA_PART_BUY_REQUST C ON C.ID=A.REQUST_ID" +
                                               " LEFT JOIN dbo.T_SYS_USER D ON D.ID=C.APPLY_USER_ID" +
                                               " LEFT JOIN dbo.T_SYS_DICT E ON E.DICT_CODE=C.APPLY_DEPT_ID AND E.DICT_TYPE='dept'" +
                                               " WHERE 1=1 AND B.IS_DEL='0' ";

            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartBuyRequstLst.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID='{0}'", tOaPartBuyRequstLst.PART_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REQUST_ID))
            {
                strSQL += String.Format(" AND A.REQUST_ID='{0}'", tOaPartBuyRequstLst.REQUST_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.STATUS))
            {
                strSQL += String.Format(" AND A.STATUS='{0}'", tOaPartBuyRequstLst.STATUS);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequest.ID))
            {//何海亮修改
                strSQL += String.Format(" AND C.ID='{0}'", tOaPartBuyRequest.ID);
            }
            //else if (String.IsNullOrEmpty(tOaPartBuyRequest.ID) && String.IsNullOrEmpty(tOaPartBuyRequstLst.REQUST_ID))//何海亮修改
            //{
            //    strSQL += String.Format(" AND C.APPLY_TYPE='{0}'", "01");
            //}
            if (!String.IsNullOrEmpty(tOaPartBuyRequest.STATUS))
            {
                strSQL += String.Format(" AND C.STATUS='{0}'", tOaPartBuyRequest.STATUS);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK3))
            {
                strSQL += String.Format("  AND D.REAL_NAME LIKE '%{0}%' ", tOaPartBuyRequstLst.REMARK3);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequest.APP_DEPT_ID))
            {
                strSQL += String.Format("  AND C.APPLY_DEPT_ID LIKE '%{0}%'", tOaPartBuyRequest.APP_DEPT_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format("  AND B.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format("  AND B.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }

            //黄进军添加
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_TYPE))
            {
                strSQL += String.Format("  AND B.PART_TYPE in ({0})", tOaPartInfor.PART_TYPE);
            }

            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK4) && !String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK5))
            {
                strSQL += " CONVERT(DATETIME, CONVERT(VARCHAR(100), A.DELIVERY_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartBuyRequstLst.REMARK4, tOaPartBuyRequstLst.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress( strSQL,iIndex,iCount));
        }


                /// <summary>
        /// 获取用户采购计划列表
        /// </summary>
        /// <param name="tOaPartBuyRequstLst"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectUnionPartByTableResult(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, TOaPartInfoVo tOaPartInfor,TOaPartBuyRequstVo tOaPartBuyRequest)
        {
            string strSQL = @" SELECT A.ID,A.PART_ID,A.NEED_QUANTITY,A.BUDGET_MONEY,CONVERT(DATETIME, CONVERT(VARCHAR(100), A.DELIVERY_DATE,23),111)  AS DELIVERY_DATE,A.USERDO,A.STATUS," +
                                              " B.PART_CODE,B.PART_TYPE,B.PART_NAME,B.UNIT,B.MODELS,B.INVENTORY,B.MEDIUM,B.PURE," +
                                               " B.ALARM,B.REQUEST,B.USEING,B.NARURE," +
                                               " C.APPLY_USER_ID,C.APPLY_DEPT_ID,C.APPLY_DATE,C.APPLY_TITLE,D.REAL_NAME,D.USER_NAME,E.DICT_TEXT AS DEPT_NAME" +
                                               " FROM dbo.T_OA_PART_BUY_REQUST_LST A " +
                                               " LEFT JOIN dbo.T_OA_PART_INFO B ON B.ID=A.PART_ID" +
                                               " LEFT JOIN dbo.T_OA_PART_BUY_REQUST C ON C.ID=A.REQUST_ID" +
                                               " LEFT JOIN dbo.T_SYS_USER D ON D.ID=C.APPLY_USER_ID" +
                                               " LEFT JOIN dbo.T_SYS_DICT E ON E.DICT_CODE=C.APPLY_DEPT_ID AND E.DICT_TYPE='dept'" +
                                               " WHERE 1=1 AND B.IS_DEL='0'";

            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartBuyRequstLst.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.PART_ID))
            {
                strSQL += String.Format(" AND A.PART_ID='{0}'", tOaPartBuyRequstLst.PART_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REQUST_ID))
            {
                strSQL += String.Format(" AND A.REQUST_ID='{0}'", tOaPartBuyRequstLst.REQUST_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.STATUS))
            {
                strSQL += String.Format(" AND A.STATUS='{0}'", tOaPartBuyRequstLst.STATUS);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequest.STATUS))
            {
                strSQL += String.Format(" AND C.STATUS='{0}'", tOaPartBuyRequest.STATUS);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK3))
            {
                strSQL += String.Format("  AND D.REAL_NAME LIKE '%{0}%' ", tOaPartBuyRequstLst.REMARK3);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequest.APP_DEPT_ID))
            {
                strSQL += String.Format("  AND C.APPLY_DEPT_ID LIKE '%{0}%'", tOaPartBuyRequest.APP_DEPT_ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format("  AND B.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format("  AND B.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }
            if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK4) && !String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK5))
            {
                strSQL += " CONVERT(DATETIME, CONVERT(VARCHAR(100), A.DELIVERY_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartBuyRequstLst.REMARK4, tOaPartBuyRequstLst.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartBuyRequstLst"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartBuyRequstLst)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartBuyRequstLst.ID.ToString()));
				}	
				//申请单ID
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REQUST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REQUST_ID = '{0}'", tOaPartBuyRequstLst.REQUST_ID.ToString()));
				}	
				//物料ID
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.PART_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PART_ID = '{0}'", tOaPartBuyRequstLst.PART_ID.ToString()));
				}	
				//需求数量
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.NEED_QUANTITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NEED_QUANTITY = '{0}'", tOaPartBuyRequstLst.NEED_QUANTITY.ToString()));
				}	
				//采购用途
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.USERDO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USERDO = '{0}'", tOaPartBuyRequstLst.USERDO.ToString()));
				}	
				//要求交货期限
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.DELIVERY_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DELIVERY_DATE = '{0}'", tOaPartBuyRequstLst.DELIVERY_DATE.ToString()));
				}	
				//计划资金
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.BUDGET_MONEY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BUDGET_MONEY = '{0}'", tOaPartBuyRequstLst.BUDGET_MONEY.ToString()));
				}	
				//状态,1待审批，2待采购，3已采购
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.STATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tOaPartBuyRequstLst.STATUS.ToString()));
				}
				//备注1
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartBuyRequstLst.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartBuyRequstLst.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartBuyRequstLst.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartBuyRequstLst.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaPartBuyRequstLst.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartBuyRequstLst.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
