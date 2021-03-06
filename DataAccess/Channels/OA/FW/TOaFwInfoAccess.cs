using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.FW;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.FW
{
    /// <summary>
    /// 功能：发文信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaFwInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = "select Count(*) from T_OA_FW_INFO " + this.BuildWhereStatement(tOaFwInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaFwInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_FW_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaFwInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaFwInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaFwInfoVo Details(TOaFwInfoVo tOaFwInfo)
        {
           string strSQL = String.Format("select * from  T_OA_FW_INFO " + this.BuildWhereStatement(tOaFwInfo));
           return SqlHelper.ExecuteObject(new TOaFwInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaFwInfoVo> SelectByObject(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_FW_INFO " + this.BuildWhereStatement(tOaFwInfo));
            return SqlHelper.ExecuteObjectList(tOaFwInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable,指定用户的传阅发文
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForRead(TOaFwInfoVo tOaFwInfo, string strUserID, int iIndex, int iCount)
        {
            string strSQL = " select fw.*,sr.SW_PLAN_DATE as READ_DATE,sr.IS_OK from T_OA_FW_INFO fw ";
            strSQL += " join T_OA_SW_READ sr on sr.SW_ID=fw.id and sr.SW_PLAN_ID='" + strUserID + "' {0}";
            strSQL += " and sr.REMARK1='1'";
            strSQL = String.Format(strSQL, BuildWhereStatement_ForRead(tOaFwInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tOaFwInfo, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSwInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement_ForRead(TOaFwInfoVo tOaFwInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            return strWhereStatement.ToString();
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {

            string strSQL = @" SELECT     t.ID, t.YWNO, t.FWNO, t.FW_TITLE, t.ZB_DEPT,t.ZS_DEPT,t.CB_DEPT,t.CS_DEPT,t.SUBJECT_WORD, t1.DICT_TEXT AS MJ, t.FW_DATE, t.START_DATE, t.END_DATE, t.DRAFT_ID, t.DRAFT_DATE, t.APP_ID, t.APP_DATE, 
                                    t.APP_INFO, t.CTS_ID, t.CTS_DATE, t.CTS_INFO,t.ISSUE_ID, t.ISSUE_DATE, t.ISSUE_INFO, t.REG_INFO, t.REG_ID, t.REG_DATE, t.CHECK_ID, t.SEAL_ID, t.PRINT_ID, t.PIGONHOLE_ID, 
                                    t.PIGONHOLE_DATE, t.FILE_BODY, t.FILE_PATH, t.FW_STATUS, t.REMARK1, t.REMARK2, t.REMARK3, t.REMARK4, t.REMARK5
                                    FROM         T_OA_FW_INFO AS t INNER JOIN
                                    T_SYS_DICT AS t1 ON t.MJ = t1.DICT_CODE AND t1.DICT_TYPE = 'FW_MJ'  {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaFwInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectTable(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder(5000000);
            sb.Append("SELECT t.ID, t.YWNO, t.FWNO, t.FW_TITLE, t.ZB_DEPT,t.ZS_DEPT,t.CB_DEPT,t.CS_DEPT,t.SUBJECT_WORD, t1.DICT_TEXT AS MJ, t.FW_DATE, t.START_DATE, t.END_DATE, t.DRAFT_ID, t.DRAFT_DATE, t.APP_ID, t.APP_DATE,");
            sb.Append("  t.APP_INFO, t.CTS_ID, t.CTS_DATE, t.CTS_INFO,t.ISSUE_ID, t.ISSUE_DATE, t.ISSUE_INFO, t.REG_INFO, t.REG_ID, t.REG_DATE, t.CHECK_ID, t.SEAL_ID, t.PRINT_ID, t.PIGONHOLE_ID, ");
            sb.Append("  t.PIGONHOLE_DATE, t.FILE_BODY, t.FILE_PATH, t.FW_STATUS, t.REMARK1, t.REMARK2, t.REMARK3, t.REMARK4, t.REMARK5");
            sb.Append(" FROM  T_OA_FW_INFO AS t INNER JOIN    T_SYS_DICT AS t1 ON t.MJ = t1.DICT_CODE AND t1.DICT_TYPE = 'FW_MJ'");
            sb.Append(" WHERE 1=1 ");
            if (tOaFwInfo.FW_DATE != "")//发文日期
            {
                sb.Append(" and   CONVERT(VARCHAR(11),t.DRAFT_DATE,120) ='" + tOaFwInfo.FW_DATE + "'");
            }
            if (tOaFwInfo.FW_STATUS != "")//状态
            {
                if (tOaFwInfo.FW_STATUS.Equals("4"))
                {
                    sb.Append(" and   t.FW_STATUS>='" + tOaFwInfo.FW_STATUS + "'");
                }
                else
                {
                    sb.Append(" and   t.FW_STATUS in (" + tOaFwInfo.FW_STATUS + ")");
                }
            }
            if (tOaFwInfo.FWNO != "")//发文编号
            {
                sb.Append(" and   t.FWNO='" + tOaFwInfo.FWNO + "'");
            }
            //sb.Append("  ORDER by ID desc");
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectCount(TOaFwInfoVo tOaFwInfo)
        {
            StringBuilder sb = new StringBuilder(5000000);
            sb.Append(" select Count(*) from T_OA_FW_INFO");
            sb.Append(" where 1=1 ");
            if (tOaFwInfo.FW_DATE != "")//日期
            {
                sb.Append(" and  CONVERT(VARCHAR(11),FW_DATE,120) ='" + tOaFwInfo.FW_DATE + "'");
            }
            if (tOaFwInfo.FW_STATUS != "")//状态
            {
                sb.Append(" and   FW_STATUS='" + tOaFwInfo.FW_STATUS + "'");
            }
            if (tOaFwInfo.FWNO != "")//发文编号
            {
                sb.Append(" and   FWNO='" + tOaFwInfo.FWNO + "'");
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sb.ToString()));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaFwInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = "select * from T_OA_FW_INFO " + this.BuildWhereStatement(tOaFwInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns></returns>
        public TOaFwInfoVo SelectByObject(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = "select * from T_OA_FW_INFO " + this.BuildWhereStatement(tOaFwInfo);
            return SqlHelper.ExecuteObject(new TOaFwInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaFwInfo, TOaFwInfoVo.T_OA_FW_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaFwInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaFwInfo, TOaFwInfoVo.T_OA_FW_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaFwInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaFwInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaFwInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaFwInfoVo tOaFwInfo_UpdateSet, TOaFwInfoVo tOaFwInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaFwInfo_UpdateSet, TOaFwInfoVo.T_OA_FW_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaFwInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_FW_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaFwInfoVo tOaFwInfo)
        {
            string strSQL = "delete from T_OA_FW_INFO ";
	    strSQL += this.BuildWhereStatement(tOaFwInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaFwInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaFwInfoVo tOaFwInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaFwInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaFwInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaFwInfo.ID.ToString()));
				}	
				//原文编号
				if (!String.IsNullOrEmpty(tOaFwInfo.YWNO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND YWNO = '{0}'", tOaFwInfo.YWNO.ToString()));
				}	
				//发文编号
				if (!String.IsNullOrEmpty(tOaFwInfo.FWNO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FWNO = '{0}'", tOaFwInfo.FWNO.ToString()));
				}	
				//发文标题
				if (!String.IsNullOrEmpty(tOaFwInfo.FW_TITLE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FW_TITLE = '{0}'", tOaFwInfo.FW_TITLE.ToString()));
				}	
				//主办单位
				if (!String.IsNullOrEmpty(tOaFwInfo.ZB_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ZB_DEPT = '{0}'", tOaFwInfo.ZB_DEPT.ToString()));
				}
                //主送单位
                if (!String.IsNullOrEmpty(tOaFwInfo.ZS_DEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ZS_DEPT = '{0}'", tOaFwInfo.ZS_DEPT.ToString()));
                }
                //抄报单位
                if (!String.IsNullOrEmpty(tOaFwInfo.CB_DEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CB_DEPT = '{0}'", tOaFwInfo.CB_DEPT.ToString()));
                }
                //抄送单位
                if (!String.IsNullOrEmpty(tOaFwInfo.CS_DEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CS_DEPT = '{0}'", tOaFwInfo.CS_DEPT.ToString()));
                }
                //主题词
                if (!String.IsNullOrEmpty(tOaFwInfo.SUBJECT_WORD.ToString().Trim()))
                {
                    string strSUBJECT_WORD = tOaFwInfo.SUBJECT_WORD.ToString().Replace("，", ",");
                    string[] str = strSUBJECT_WORD.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        strWhereStatement.Append(string.Format(" AND SUBJECT_WORD like '%{0}%'", str[i].ToString()));
                    }
                    //strWhereStatement.Append(string.Format(" AND SUBJECT_WORD = '{0}'", tOaFwInfo.SUBJECT_WORD.ToString()));
                }	
				//密级
				if (!String.IsNullOrEmpty(tOaFwInfo.MJ.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MJ = '{0}'", tOaFwInfo.MJ.ToString()));
				}
                //办理限期-开始时间
                if (!String.IsNullOrEmpty(tOaFwInfo.START_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND START_DATE = '{0}'", tOaFwInfo.START_DATE.ToString()));
                }
                //办理限期-结束时间
                if (!String.IsNullOrEmpty(tOaFwInfo.END_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DATE = '{0}'", tOaFwInfo.END_DATE.ToString()));
                }
				//发文日期
				if (!String.IsNullOrEmpty(tOaFwInfo.FW_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FW_DATE = '{0}'", tOaFwInfo.FW_DATE.ToString()));
				}	
				//拟稿人
				if (!String.IsNullOrEmpty(tOaFwInfo.DRAFT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DRAFT_ID = '{0}'", tOaFwInfo.DRAFT_ID.ToString()));
				}	
				//拟稿日期
				if (!String.IsNullOrEmpty(tOaFwInfo.DRAFT_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DRAFT_DATE = '{0}'", tOaFwInfo.DRAFT_DATE.ToString()));
				}	
				//核稿人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.APP_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_ID = '{0}'", tOaFwInfo.APP_ID.ToString()));
				}	
				//核稿日期
				if (!String.IsNullOrEmpty(tOaFwInfo.APP_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DATE = '{0}'", tOaFwInfo.APP_DATE.ToString()));
				}	
				//核稿意见
				if (!String.IsNullOrEmpty(tOaFwInfo.APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_INFO = '{0}'", tOaFwInfo.APP_INFO.ToString()));
				}
                //会签人ID
                if (!String.IsNullOrEmpty(tOaFwInfo.CTS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CTS_ID = '{0}'", tOaFwInfo.CTS_ID.ToString()));
                }
                //会签日期
                if (!String.IsNullOrEmpty(tOaFwInfo.CTS_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CTS_DATE = '{0}'", tOaFwInfo.CTS_DATE.ToString()));
                }
                //会签意见
                if (!String.IsNullOrEmpty(tOaFwInfo.CTS_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CTS_INFO = '{0}'", tOaFwInfo.CTS_INFO.ToString()));
                }	
				//签发人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.ISSUE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ISSUE_ID = '{0}'", tOaFwInfo.ISSUE_ID.ToString()));
				}	
				//签发日期
				if (!String.IsNullOrEmpty(tOaFwInfo.ISSUE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ISSUE_DATE = '{0}'", tOaFwInfo.ISSUE_DATE.ToString()));
				}	
				//签发意见
				if (!String.IsNullOrEmpty(tOaFwInfo.ISSUE_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ISSUE_INFO = '{0}'", tOaFwInfo.ISSUE_INFO.ToString()));
				}
                //登记意见
                if (!String.IsNullOrEmpty(tOaFwInfo.REG_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REG_INFO = '{0}'", tOaFwInfo.REG_INFO.ToString()));
                }	
				//登记人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.REG_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REG_ID = '{0}'", tOaFwInfo.REG_ID.ToString()));
				}	
				//登记日期
				if (!String.IsNullOrEmpty(tOaFwInfo.REG_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REG_DATE = '{0}'", tOaFwInfo.REG_DATE.ToString()));
				}	
				//校对人
				if (!String.IsNullOrEmpty(tOaFwInfo.CHECK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_ID = '{0}'", tOaFwInfo.CHECK_ID.ToString()));
				}	
				//用印人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.SEAL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEAL_ID = '{0}'", tOaFwInfo.SEAL_ID.ToString()));
				}	
				//缮印人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.PRINT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRINT_ID = '{0}'", tOaFwInfo.PRINT_ID.ToString()));
				}	
				//归档人ID
				if (!String.IsNullOrEmpty(tOaFwInfo.PIGONHOLE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PIGONHOLE_ID = '{0}'", tOaFwInfo.PIGONHOLE_ID.ToString()));
				}	
				//归档时间
				if (!String.IsNullOrEmpty(tOaFwInfo.PIGONHOLE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PIGONHOLE_DATE = '{0}'", tOaFwInfo.PIGONHOLE_DATE.ToString()));
				}	
                //发文状态
                if (!String.IsNullOrEmpty(tOaFwInfo.FW_STATUS.ToString().Trim()))
                {
                    if (tOaFwInfo.FW_STATUS == "99")
                        strWhereStatement.Append(string.Format(" AND FW_STATUS <> '0' AND FW_STATUS <> '9' ", tOaFwInfo.FW_STATUS.ToString()));
                    else
                        strWhereStatement.Append(string.Format(" AND FW_STATUS = '{0}'", tOaFwInfo.FW_STATUS.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tOaFwInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaFwInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaFwInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaFwInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaFwInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaFwInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaFwInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaFwInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaFwInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaFwInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
