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
    /// 功能：物料采购申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstAccess : SqlHelper 
    {
         
         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = "select Count(*) from T_OA_PART_BUY_REQUST " + this.BuildWhereStatement(tOaPartBuyRequst);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartBuyRequstVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstVo Details(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
           string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST " + this.BuildWhereStatement(tOaPartBuyRequst));
           return SqlHelper.ExecuteObject(new TOaPartBuyRequstVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartBuyRequstVo> SelectByObject(TOaPartBuyRequstVo tOaPartBuyRequst, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_BUY_REQUST " + this.BuildWhereStatement(tOaPartBuyRequst));
            return SqlHelper.ExecuteObjectList(tOaPartBuyRequst, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartBuyRequstVo tOaPartBuyRequst, int iIndex, int iCount)
        {

            string strSQL = " select * FROM T_OA_PART_BUY_REQUST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartBuyRequst));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable,根据物料类型
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByPart(TOaPartBuyRequstVo tOaPartBuyRequst, TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            string strSQL = " select distinct C.* FROM T_OA_PART_BUY_REQUST_LST A LEFT JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID LEFT JOIN T_OA_PART_BUY_REQUST C ON C.ID=A.REQUST_ID {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatementTwo(tOaPartBuyRequst, tOaPartInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartBuyRequst"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = "select * from T_OA_PART_BUY_REQUST " + this.BuildWhereStatement(tOaPartBuyRequst);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <returns></returns>
        public TOaPartBuyRequstVo SelectByObject(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = "select * from T_OA_PART_BUY_REQUST " + this.BuildWhereStatement(tOaPartBuyRequst);
            return SqlHelper.ExecuteObject(new TOaPartBuyRequstVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartBuyRequst, TOaPartBuyRequstVo.T_OA_PART_BUY_REQUST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequst">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartBuyRequst, TOaPartBuyRequstVo.T_OA_PART_BUY_REQUST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartBuyRequst.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequst_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartBuyRequst_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstVo tOaPartBuyRequst_UpdateSet, TOaPartBuyRequstVo tOaPartBuyRequst_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartBuyRequst_UpdateSet, TOaPartBuyRequstVo.T_OA_PART_BUY_REQUST_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartBuyRequst_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_BUY_REQUST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            string strSQL = "delete from T_OA_PART_BUY_REQUST ";
	    strSQL += this.BuildWhereStatement(tOaPartBuyRequst);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        public DataTable  SelectRemarks(string ID)
        {
            string strSQL = "select remark1 from T_OA_PART_BUY_REQUST where ID='{0}'";
            strSQL = string.Format(strSQL,ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        public DataTable SelectUserName(string ID)
        {
            string strSQL = "select REAL_NAME from T_SYS_USER where ID='{0}'";
            strSQL = string.Format(strSQL, ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectItemInfo(string ID)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select C.PART_CODE,C.PART_NAME,C.MODELS,B.NEED_QUANTITY AS NEED_QUANTITY,C.UNIT  ");
            sb.Append(" from  T_OA_PART_BUY_REQUST  A  ");
            sb.Append(" left join T_OA_PART_BUY_REQUST_LST B on A.ID=B.REQUST_ID ");
            sb.Append("   left join T_OA_PART_INFO C on B.PART_ID=C.ID ");
            sb.Append(" where A.ID='" + ID + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }

        public DataTable SelectDept(string Code)
        {
            string strSQL = "select dict_text from T_SYS_DICT where dict_code='{0}'";
            strSQL = string.Format(strSQL, Code);
            return SqlHelper.ExecuteDataTable(strSQL.ToString());
        }

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartBuyRequst"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartBuyRequst)
            {	
				//编号
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartBuyRequst.ID.ToString()));
				}	
				//申请科室
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_DEPT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPLY_DEPT_ID = '{0}'", tOaPartBuyRequst.APPLY_DEPT_ID.ToString()));
				}	
				//申请人
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPLY_USER_ID = '{0}'", tOaPartBuyRequst.APPLY_USER_ID.ToString()));
				}	
				//申请时间
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPLY_DATE = '{0}'", tOaPartBuyRequst.APPLY_DATE.ToString()));
				}	
				//申请标题
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_TITLE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPLY_TITLE = '{0}'", tOaPartBuyRequst.APPLY_TITLE.ToString()));
				}	
				//部门审批人
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_DEPT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DEPT_ID = '{0}'", tOaPartBuyRequst.APP_DEPT_ID.ToString()));
				}	
				//部门审批时间
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_DEPT_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DEPT_DATE = '{0}'", tOaPartBuyRequst.APP_DEPT_DATE.ToString()));
				}	
				//部门审批意见
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_DEPT_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DEPT_INFO = '{0}'", tOaPartBuyRequst.APP_DEPT_INFO.ToString()));
				}	
				//技术负责人审批人
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_MANAGER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_MANAGER_ID = '{0}'", tOaPartBuyRequst.APP_MANAGER_ID.ToString()));
				}	
				//技术负责人审批时间
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_MANAGER_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_MANAGER_DATE = '{0}'", tOaPartBuyRequst.APP_MANAGER_DATE.ToString()));
				}	
				//技术负责人审批意见
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_MANAGER_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_MANAGER_INFO = '{0}'", tOaPartBuyRequst.APP_MANAGER_INFO.ToString()));
				}	
				//站长审批人
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_LEADER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_LEADER_ID = '{0}'", tOaPartBuyRequst.APP_LEADER_ID.ToString()));
				}	
				//站长审批时间
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_LEADER_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_LEADER_DATE = '{0}'", tOaPartBuyRequst.APP_LEADER_DATE.ToString()));
				}	
				//站长审批意见
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_LEADER_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_LEADER_INFO = '{0}'", tOaPartBuyRequst.APP_LEADER_INFO.ToString()));
				}	
				//状态,1待审批，2待采购，3已采购
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.STATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STATUS in ({0})", tOaPartBuyRequst.STATUS.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartBuyRequst.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartBuyRequst.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartBuyRequst.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartBuyRequst.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartBuyRequst.REMARK4.ToString()));
				}	
				//备注5
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartBuyRequst.REMARK5.ToString()));
                }
                //办公室意见
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_OFFER_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APP_OFFER_INFO = '{0}'", tOaPartBuyRequst.APP_OFFER_INFO.ToString()));
                }
                //办公室人
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_OFFER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APP_OFFER_ID = '{0}'", tOaPartBuyRequst.APP_OFFER_ID.ToString()));
                }
                //办公室时间
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APP_OFFER_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APP_OFFER_TIME = '{0}'", tOaPartBuyRequst.APP_OFFER_TIME.ToString()));
                }
                //物料类别
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPLY_TYPE in ({0})", tOaPartBuyRequst.APPLY_TYPE.ToString()));
                }
                //申购内容
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_CONTENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPLY_CONTENT = '{0}'", tOaPartBuyRequst.APPLY_CONTENT.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartBuyRequst"></param>
        /// <returns></returns>
        public string BuildWhereStatementTwo(TOaPartBuyRequstVo tOaPartBuyRequst, TOaPartInfoVo tOaPartInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartBuyRequst)
            {

                //物料类型
                if (!String.IsNullOrEmpty(tOaPartInfo.PART_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND B.PART_TYPE in ({0})", tOaPartInfo.PART_TYPE.ToString()));
                }

                //编号
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND C.ID = '{0}'", tOaPartBuyRequst.ID.ToString()));
                }
                //申请科室
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_DEPT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND C.APPLY_DEPT_ID = '{0}'", tOaPartBuyRequst.APPLY_DEPT_ID.ToString()));
                }
                //申请人
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND C.APPLY_USER_ID = '{0}'", tOaPartBuyRequst.APPLY_USER_ID.ToString()));
                }
                
                //状态,1待审批，2待采购，3已采购
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND C.STATUS in ({0})", tOaPartBuyRequst.STATUS.ToString()));
                }
               
                //物料类别
                if (!String.IsNullOrEmpty(tOaPartBuyRequst.APPLY_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND C.APPLY_TYPE in ({0})", tOaPartBuyRequst.APPLY_TYPE.ToString()));
                }
                
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
