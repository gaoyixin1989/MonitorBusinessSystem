using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.SW;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwInfoAccess : SqlHelper
    {
        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = "select Count(*) from T_OA_SW_INFO " + this.BuildWhereStatement(tOaSwInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_SW_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaSwInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwInfoVo Details(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = String.Format("select * from  T_OA_SW_INFO " + this.BuildWhereStatement(tOaSwInfo));
            return SqlHelper.ExecuteObject(new TOaSwInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwInfoVo> SelectByObject(TOaSwInfoVo tOaSwInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_SW_INFO " + this.BuildWhereStatement(tOaSwInfo));
            return SqlHelper.ExecuteObjectList(tOaSwInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwInfoVo tOaSwInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_SW_INFO {0} ";
            strSQL += " order by ID desc"; 
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaSwInfo));
          
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable,指定用户的传阅收文
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForRead(TOaSwInfoVo tOaSwInfo,string strUserID, int iIndex, int iCount)
        {
            string strSQL = " select sw.*,sr.SW_PLAN_DATE as READ_DATE,sr.IS_OK from T_OA_SW_INFO sw ";
            strSQL += " join T_OA_SW_READ sr on sr.SW_ID=sw.id and sr.SW_PLAN_ID='" + strUserID + "' {0}";
            strSQL += " and sr.REMARK1='0'";
            strSQL = String.Format(strSQL, BuildWhereStatement_ForRead(tOaSwInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tOaSwInfo,strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForRead(TOaSwInfoVo tOaSwInfo, string strUserID)
        {
            string strSQL = " select Count(*) from T_OA_SW_INFO sw ";
            strSQL += " join T_OA_SW_READ sr on sr.SW_ID=sw.id and sr.SW_PLAN_ID='" + strUserID + "' {0}";

            strSQL = String.Format(strSQL, BuildWhereStatement_ForRead(tOaSwInfo));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = "select * from T_OA_SW_INFO " + this.BuildWhereStatement(tOaSwInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns></returns>
        public TOaSwInfoVo SelectByObject(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = "select * from T_OA_SW_INFO " + this.BuildWhereStatement(tOaSwInfo);
            return SqlHelper.ExecuteObject(new TOaSwInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaSwInfo, TOaSwInfoVo.T_OA_SW_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwInfo, TOaSwInfoVo.T_OA_SW_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaSwInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaSwInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwInfoVo tOaSwInfo_UpdateSet, TOaSwInfoVo tOaSwInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwInfo_UpdateSet, TOaSwInfoVo.T_OA_SW_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaSwInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_SW_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaSwInfoVo tOaSwInfo)
        {
            string strSQL = "delete from T_OA_SW_INFO ";
            strSQL += this.BuildWhereStatement(tOaSwInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSwInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaSwInfoVo tOaSwInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSwInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tOaSwInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaSwInfo.ID.ToString()));
                }
                //原文编号
                if (!String.IsNullOrEmpty(tOaSwInfo.FROM_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FROM_CODE = '{0}'", tOaSwInfo.FROM_CODE.ToString()));
                }
                //收文编号
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_CODE = '{0}'", tOaSwInfo.SW_CODE.ToString()));
                }
                //来文单位
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_FROM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_FROM = '{0}'", tOaSwInfo.SW_FROM.ToString()));
                }
                //收文份数
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_COUNT = '{0}'", tOaSwInfo.SW_COUNT.ToString()));
                }
                //收文日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_DATE = '{0}'", tOaSwInfo.SW_DATE.ToString()));
                }
                //密级
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_MJ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_MJ = '{0}'", tOaSwInfo.SW_MJ.ToString()));
                }
                //收文标题
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_TITLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_TITLE like '%{0}%'", tOaSwInfo.SW_TITLE.ToString()));
                }
                //收文类别
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_TYPE = '{0}'", tOaSwInfo.SW_TYPE.ToString()));
                }
                //签收人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_SIGN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_SIGN_ID = '{0}'", tOaSwInfo.SW_SIGN_ID.ToString()));
                }
                //签收日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_SIGN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_SIGN_DATE = '{0}'", tOaSwInfo.SW_SIGN_DATE.ToString()));
                }
                //登记人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_REG_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_REG_ID = '{0}'", tOaSwInfo.SW_REG_ID.ToString()));
                }
                //登记日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_REG_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_REG_DATE = '{0}'", tOaSwInfo.SW_REG_DATE.ToString()));
                }
                //拟办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_ID = '{0}'", tOaSwInfo.SW_PLAN_ID.ToString()));
                }
                //拟办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_DATE = '{0}'", tOaSwInfo.SW_PLAN_DATE.ToString()));
                }
                //拟办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_INFO = '{0}'", tOaSwInfo.SW_PLAN_INFO.ToString()));
                }
                //批办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_APP_ID = '{0}'", tOaSwInfo.SW_PLAN_APP_ID.ToString()));
                }
                //批办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_APP_DATE = '{0}'", tOaSwInfo.SW_PLAN_APP_DATE.ToString()));
                }
                //批办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_PLAN_APP_INFO = '{0}'", tOaSwInfo.SW_PLAN_APP_INFO.ToString()));
                }
                //主办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_APP_ID = '{0}'", tOaSwInfo.SW_APP_ID.ToString()));
                }
                //主办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_APP_DATE = '{0}'", tOaSwInfo.SW_APP_DATE.ToString()));
                }
                //主办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_APP_INFO = '{0}'", tOaSwInfo.SW_APP_INFO.ToString()));
                }
                //归档人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.PIGONHOLE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PIGONHOLE_ID = '{0}'", tOaSwInfo.PIGONHOLE_ID.ToString()));
                }
                //归档时间
                if (!String.IsNullOrEmpty(tOaSwInfo.PIGONHOLE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PIGONHOLE_DATE = '{0}'", tOaSwInfo.PIGONHOLE_DATE.ToString()));
                }
                //办理状态
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_STATUS.ToString().Trim()))
                {
                    if (tOaSwInfo.SW_STATUS == "99")
                        strWhereStatement.Append(string.Format(" AND SW_STATUS <> '0' AND SW_STATUS <> '9' ", tOaSwInfo.SW_STATUS.ToString()));
                    else
                        strWhereStatement.Append(string.Format(" AND SW_STATUS in ({0})", tOaSwInfo.SW_STATUS.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaSwInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaSwInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaSwInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaSwInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaSwInfo.REMARK5.ToString()));
                }
                //主题词
                if (!String.IsNullOrEmpty(tOaSwInfo.SUBJECT_WORD.ToString().Trim()))
                {
                    string strSUBJECT_WORD = tOaSwInfo.SUBJECT_WORD.ToString().Replace("，", ",");
                    string[] str = strSUBJECT_WORD.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        strWhereStatement.Append(string.Format(" AND SUBJECT_WORD like '%{0}%'", str[i].ToString()));
                    }
                }
            }
            return strWhereStatement.ToString();
        }

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSwInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement_ForRead(TOaSwInfoVo tOaSwInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSwInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tOaSwInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.ID = '{0}'", tOaSwInfo.ID.ToString()));
                }
                //原文编号
                if (!String.IsNullOrEmpty(tOaSwInfo.FROM_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.FROM_CODE = '{0}'", tOaSwInfo.FROM_CODE.ToString()));
                }
                //收文编号
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_CODE = '{0}'", tOaSwInfo.SW_CODE.ToString()));
                }
                //来文单位
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_FROM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_FROM = '{0}'", tOaSwInfo.SW_FROM.ToString()));
                }
                //收文份数
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_COUNT = '{0}'", tOaSwInfo.SW_COUNT.ToString()));
                }
                //收文日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_DATE = '{0}'", tOaSwInfo.SW_DATE.ToString()));
                }
                //密级
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_MJ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_MJ = '{0}'", tOaSwInfo.SW_MJ.ToString()));
                }
                //收文标题
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_TITLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_TITLE like '%{0}%'", tOaSwInfo.SW_TITLE.ToString()));
                }
                //收文类别
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_TYPE = '{0}'", tOaSwInfo.SW_TYPE.ToString()));
                }
                //签收人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_SIGN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_SIGN_ID = '{0}'", tOaSwInfo.SW_SIGN_ID.ToString()));
                }
                //签收日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_SIGN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_SIGN_DATE = '{0}'", tOaSwInfo.SW_SIGN_DATE.ToString()));
                }
                //登记人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_REG_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_REG_ID = '{0}'", tOaSwInfo.SW_REG_ID.ToString()));
                }
                //登记日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_REG_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_REG_DATE = '{0}'", tOaSwInfo.SW_REG_DATE.ToString()));
                }
                //拟办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_ID = '{0}'", tOaSwInfo.SW_PLAN_ID.ToString()));
                }
                //拟办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_DATE = '{0}'", tOaSwInfo.SW_PLAN_DATE.ToString()));
                }
                //拟办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_INFO = '{0}'", tOaSwInfo.SW_PLAN_INFO.ToString()));
                }
                //批办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_APP_ID = '{0}'", tOaSwInfo.SW_PLAN_APP_ID.ToString()));
                }
                //批办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_APP_DATE = '{0}'", tOaSwInfo.SW_PLAN_APP_DATE.ToString()));
                }
                //批办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_PLAN_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_PLAN_APP_INFO = '{0}'", tOaSwInfo.SW_PLAN_APP_INFO.ToString()));
                }
                //主办人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_APP_ID = '{0}'", tOaSwInfo.SW_APP_ID.ToString()));
                }
                //主办日期
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_APP_DATE = '{0}'", tOaSwInfo.SW_APP_DATE.ToString()));
                }
                //主办意见
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_APP_INFO = '{0}'", tOaSwInfo.SW_APP_INFO.ToString()));
                }
                //归档人ID
                if (!String.IsNullOrEmpty(tOaSwInfo.PIGONHOLE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.PIGONHOLE_ID = '{0}'", tOaSwInfo.PIGONHOLE_ID.ToString()));
                }
                //归档时间
                if (!String.IsNullOrEmpty(tOaSwInfo.PIGONHOLE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.PIGONHOLE_DATE = '{0}'", tOaSwInfo.PIGONHOLE_DATE.ToString()));
                }
                //办理状态
                if (!String.IsNullOrEmpty(tOaSwInfo.SW_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sw.SW_STATUS in ({0})", tOaSwInfo.SW_STATUS.ToString()));
                }
                //备注1 传阅日期
                if (!String.IsNullOrEmpty(tOaSwInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND sr.SW_PLAN_DATE = '{0}'", tOaSwInfo.REMARK1.ToString()));
                }

                //主题词
                if (!String.IsNullOrEmpty(tOaSwInfo.SUBJECT_WORD.ToString().Trim()))
                {
                    string strSUBJECT_WORD = tOaSwInfo.SUBJECT_WORD.ToString().Replace("，", ",");
                    string[] str = strSUBJECT_WORD.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        strWhereStatement.Append(string.Format(" AND sw.SUBJECT_WORD like '%{0}%'", str[i].ToString()));
                    }
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取当前用户需要办理的收文DataTable
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectHandleTable(string userID, string where, int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,TASK_CODE,TASK_NAME,TASK_STATUS,SEND_USER,SEND_DATE from(");
            sb.Append("select a.ID,'SW'+replace(replace(replace(CONVERT(varchar, a.SW_REG_DATE, 120 ),'-',''),' ',''),':','') TASK_CODE,");
            sb.Append("''''+a.SW_TITLE+'''公文收取' TASK_NAME,a.SW_STATUS TASK_STATUS,c.REAL_NAME SEND_USER,b.STR_DATE SEND_DATE ");
            sb.Append("from T_OA_SW_INFO a inner join T_OA_SW_HANDLE b on(a.ID=b.SW_ID and a.SW_STATUS=b.SW_HANDER) ");
            sb.Append("left join T_SYS_USER c on(b.STR_USERID=c.ID) ");
            sb.Append("where (b.IS_OK='0' or b.IS_OK='2') and b.SW_PLAN_ID='" + userID + "') aa where " + where);

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }
        /// <summary>
        /// 获取当前用户需要办理的收文个数（ljn,2013/11/28,郑州需求）
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetSwResultCount(string UserID)
        {
            string strSQL = "select count(*)  from T_OA_SW_INFO a inner join T_OA_SW_HANDLE b on(a.ID=b.SW_ID and a.SW_STATUS=b.SW_HANDER) left join T_SYS_USER c on(b.STR_USERID=c.ID)  where (b.IS_OK='0' or b.IS_OK='2') and b.SW_PLAN_ID='" + UserID + "' ";
            return SqlHelper.ExecuteScalar(strSQL).ToString();
        }
        /// <summary>
        /// 获取当前用户需要办理的收文DataTable
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectHandleTable_Mobile(string userID, string strConfirm,string where, int iIndex, int iCount)
        {
            string strstrIsOk = "0";
            if (strConfirm == "1")
                strstrIsOk = "2";

            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,TASK_NAME,SEND_USER,SEND_DATE from(");
            sb.Append("select a.ID,'SW'+replace(replace(replace(CONVERT(varchar, a.SW_REG_DATE, 120 ),'-',''),' ',''),':','') TASK_CODE,");
            sb.Append("'“'+a.SW_TITLE+'”收文办理' TASK_NAME,a.SW_STATUS TASK_STATUS,c.REAL_NAME SEND_USER,b.STR_DATE SEND_DATE ");
            sb.Append("from T_OA_SW_INFO a inner join T_OA_SW_HANDLE b on(a.ID=b.SW_ID and a.SW_STATUS=b.SW_HANDER) ");
            sb.Append("left join T_SYS_USER c on(b.STR_USERID=c.ID) ");
            sb.Append("where b.IS_OK='" + strstrIsOk + "' and b.SW_PLAN_ID='" + userID + "') aa where " + where);

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }

        /// <summary>
        /// 获取当前用户需要办理的收文DataTable的 Count
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int SelectHandleTable_Count_Mobile(string userID, string strConfirm, string where)
        {
            string strstrIsOk = "0";
            if (strConfirm == "1")
                strstrIsOk = "2";

            StringBuilder sb = new StringBuilder();
            sb.Append("select Count(*) from(");
            sb.Append("select a.ID,'SW'+replace(replace(replace(CONVERT(varchar, a.SW_REG_DATE, 120 ),'-',''),' ',''),':','') TASK_CODE,");
            sb.Append("'“'+a.SW_TITLE+'”收文办理' TASK_NAME,a.SW_STATUS TASK_STATUS,c.REAL_NAME SEND_USER,b.STR_DATE SEND_DATE ");
            sb.Append("from T_OA_SW_INFO a inner join T_OA_SW_HANDLE b on(a.ID=b.SW_ID and a.SW_STATUS=b.SW_HANDER) ");
            sb.Append("left join T_SYS_USER c on(b.STR_USERID=c.ID) ");
            sb.Append("where b.IS_OK='" + strstrIsOk + "' and b.SW_PLAN_ID='" + userID + "') aa where " + where);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(sb.ToString()));
        }

        /// <summary>
        /// 获取收文单的详细信息
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public DataTable GetSWDetails(string strID)
        {
            string sql = "";
            DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();
            DataRow[] drTemp;
            string ReadUserID = "";
            string ReadUserName = "";
            string MakeUserID = "";
            string MakeUserName = "";
            string SW_PLAN2 = "";
            string SW_PLAN3 = "";
            string SW_PLAN4 = "";
            string SW_PLAN5 = "";

            sql = @"select FROM_CODE,SW_REG_DATE,SW_TITLE,SW_FROM,SW_COUNT,SW_MJ,SW_SIGN_ID,SW_SIGN_DATE,SW_CODE,SW_DATE,PIGONHOLE_DATE,SUBJECT_WORD,
                    '' ReadUserID,'' ReadUserName,'' MakeUserID,'' MakeUserName,'' SW_PLAN2,'' SW_PLAN3,'' SW_PLAN4,'' SW_PLAN5
                    from T_OA_SW_INFO where ID='" + strID + "'";

            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = @"select a.SW_PLAN_ID,b.REAL_NAME,SW_PLAN_DATE,SW_PLAN_APP_INFO,IS_OK,SW_HANDER 
                        from T_OA_SW_HANDLE a left join T_SYS_USER b on(a.SW_PLAN_ID=b.ID) where SW_ID='" + strID + "'";

                dtTemp = ExecuteDataTable(sql);

                //主任阅示信息
                drTemp = dtTemp.Select("SW_HANDER='1' and IS_OK='1'");
                for (int i = 0; i < drTemp.Length; i++)
                {
                    SW_PLAN2 += drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "\n";
                    SW_PLAN2 += "办理人：" + drTemp[i]["REAL_NAME"].ToString()+"\t";
                    SW_PLAN2 += "   办理时间：" + drTemp[i]["SW_PLAN_DATE"].ToString();
                }
                dt.Rows[0]["SW_PLAN2"] = SW_PLAN2;
                //站长阅示信息
                drTemp = dtTemp.Select("SW_HANDER='2' and IS_OK='1'");
                for (int i = 0; i < drTemp.Length; i++)
                {
                    SW_PLAN3 += drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "\n";
                    SW_PLAN3 += "办理人：" + drTemp[i]["REAL_NAME"].ToString()+"\t";
                    SW_PLAN3 += "   办理时间：" + drTemp[i]["SW_PLAN_DATE"].ToString();
                }
                dt.Rows[0]["SW_PLAN3"] = SW_PLAN3;
                //分管阅办信息
                drTemp = dtTemp.Select("SW_HANDER='3'");
                for (int i = 0; i < drTemp.Length; i++)
                {
                    ReadUserID += drTemp[i]["SW_PLAN_ID"].ToString() + ",";
                    ReadUserName += drTemp[i]["REAL_NAME"].ToString() + ",";
                    if (drTemp[i]["IS_OK"].ToString() == "1")
                    {
                        SW_PLAN4 += drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "\n";
                        SW_PLAN4 += "办理人：" + drTemp[i]["REAL_NAME"].ToString()+"\t";
                        SW_PLAN4 += "   办理时间：" + drTemp[i]["SW_PLAN_DATE"].ToString() + "\n";
                    }
                }
                dt.Rows[0]["ReadUserID"] = ReadUserID.TrimEnd(',');
                dt.Rows[0]["ReadUserName"] = ReadUserName.TrimEnd(',');
                dt.Rows[0]["SW_PLAN4"] = SW_PLAN4;
                //科室办结信息
                drTemp = dtTemp.Select("SW_HANDER='4'");
                for (int i = 0; i < drTemp.Length; i++)
                {
                    MakeUserID += drTemp[i]["SW_PLAN_ID"].ToString() + ",";
                    MakeUserName += drTemp[i]["REAL_NAME"].ToString() + ",";
                    if (drTemp[i]["IS_OK"].ToString() == "1")
                    {
                        SW_PLAN5 += drTemp[i]["SW_PLAN_APP_INFO"].ToString() + "\n";
                        SW_PLAN5 += "办理人：" + drTemp[i]["REAL_NAME"].ToString()+"\t";
                        SW_PLAN5 += "   办理时间：" + drTemp[i]["SW_PLAN_DATE"].ToString() + "\n";
                    }
                }
                dt.Rows[0]["MakeUserID"] = MakeUserID.TrimEnd(',');
                dt.Rows[0]["MakeUserName"] = MakeUserName.TrimEnd(',');
                dt.Rows[0]["SW_PLAN5"] = SW_PLAN5;
            }

            return dt;
        }

        /// <summary>
        /// 发送收文逻辑
        /// </summary>
        /// <param name="SWID">收文ID</param>
        /// <param name="SWStatus">收文新状态</param>
        /// <param name="SWPreStatus">收文前状态</param>
        /// <param name="LoginUser">当前登录人</param>
        /// <param name="Suggestion">批办意见</param>
        /// <param name="Handler">操作人</param>
        /// <param name="Reader">阅办人</param>
        /// <param name="Maker">办结人</param>
        /// <param name="Serial"></param>
        /// <returns></returns>
        public bool SendSW(string SWID, string SWStatus, string SWPreStatus, string LoginUser, string Suggestion, string Handler, string Reader, string Maker, string Serial)
        {
            ArrayList list = new ArrayList();
            string sql = string.Empty;
            string HandID = string.Empty;
            string[] str;
            DataTable dt = new DataTable();

            //收文登记——》主任阅示
            if (SWPreStatus == "0")
            {
                //修改收文状态
                sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                list.Add(sql);
                //插入主任阅示人
                HandID = GetSerialNumber(Serial);
                sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + Handler + "','0','" + SWStatus + "','" + LoginUser + "','" + DateTime.Now + "')";
                list.Add(sql);
            }
            //主任阅示——》站长阅示
            if (SWPreStatus == "1")
            {
                //修改收文状态
                sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                list.Add(sql);
                //插入站长阅示人
                HandID = GetSerialNumber(Serial);
                sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + Handler + "','0','" + SWStatus + "','" + LoginUser + "','" + DateTime.Now + "')";
                list.Add(sql);
                //更改主任阅示信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
                //删除收文的阅办人和办结人信息
                sql = "delete from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and (SW_HANDER='3' or SW_HANDER='4')";
                list.Add(sql);
                //插入阅办人信息
                if (Reader.Trim() != "")
                {
                    str = Reader.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','3','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
                //插入办结人信息
                if (Maker.Trim() != "")
                {
                    str = Maker.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','4','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
            }
            //站长阅示——》分管阅办
            if (SWPreStatus == "2" && SWStatus == "3")
            {
                //修改收文状态
                sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                list.Add(sql);
                //更改站长阅示信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
                //删除收文的阅办人和办结人信息
                sql = "delete from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and (SW_HANDER='3' or SW_HANDER='4')";
                list.Add(sql);
                //插入阅办人信息
                if (Reader.Trim() != "")
                {
                    str = Reader.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','3','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
                //插入办结人信息
                if (Maker.Trim() != "")
                {
                    str = Maker.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','4','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
            }
            //站长阅示——》科室办结
            if (SWPreStatus == "2" && SWStatus == "4")
            {
                //修改收文状态
                sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                list.Add(sql);
                //更改站长阅示信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
                //删除收文的办结人信息
                sql = "delete from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and (SW_HANDER='4')";
                list.Add(sql);
                //插入办结人信息
                if (Maker.Trim() != "")
                {
                    str = Maker.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','4','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
            }
            //分管阅办——》科室办结
            if (SWPreStatus == "3" && SWStatus == "4")
            {
                if (isLastOne(SWID, "3", LoginUser))
                {
                    //修改收文状态
                    sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                    list.Add(sql);
                }
                //更改分管阅办信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
                if (isFristOne(SWID, "3"))
                {
                    //删除收文的办结人信息
                    sql = "delete from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and (SW_HANDER='4')";
                    list.Add(sql);
                }
                else
                {
                    sql = "select SW_PLAN_ID from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and SW_HANDER='4'";
                    dt = ExecuteDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((Maker + ",").Contains(dt.Rows[i][0].ToString() + ","))
                        {
                            sql = "delete from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and SW_HANDER='4' and SW_PLAN_ID='" + dt.Rows[i][0].ToString() + "'";
                            list.Add(sql);
                        }
                    }
                }
                //插入办结人信息
                if (Maker.Trim() != "")
                {
                    str = Maker.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        HandID = GetSerialNumber(Serial);
                        sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                        values('" + HandID + "','" + SWID + "','" + str[i].ToString() + "','0','4','" + LoginUser + "','" + DateTime.Now + "')";
                        list.Add(sql);
                    }
                }
            }
            //分管阅办——》文件完结
            if (SWPreStatus == "3" && SWStatus == "5")
            {
                if (isLastOne(SWID, "3", LoginUser))
                {
                    //修改收文状态
                    sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                    list.Add(sql);
                    //插入归档人
                    HandID = GetSerialNumber(Serial);
                    sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + Handler + "','0','" + SWStatus + "','" + LoginUser + "','" + DateTime.Now + "')";
                    list.Add(sql);
                }
                //更改分管阅办信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
            }
            //科室办结——》文件完结
            if (SWPreStatus == "4" && SWStatus == "5")
            {
                if (isLastOne(SWID, "4", LoginUser))
                {
                    //修改收文状态
                    sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "' where ID='" + SWID + "'";
                    list.Add(sql);
                    //插入归档人
                    HandID = GetSerialNumber(Serial);
                    sql = @"insert into T_OA_SW_HANDLE(ID,SW_ID,SW_PLAN_ID,IS_OK,SW_HANDER,STR_USERID,STR_DATE) 
                      values('" + HandID + "','" + SWID + "','" + Handler + "','0','" + SWStatus + "','" + LoginUser + "','" + DateTime.Now + "')";
                    list.Add(sql);
                }
                //更改科室办结信息
                sql = "update T_OA_SW_HANDLE set SW_PLAN_APP_INFO='" + Suggestion + "',SW_PLAN_DATE='" + DateTime.Now + "',IS_OK='1' where SW_ID='" + SWID + "' and SW_PLAN_ID='" + LoginUser + "' and SW_HANDER='" + SWPreStatus + "'";
                list.Add(sql);
            }

            return ExecuteSQLByTransaction(list);
        }

        /// <summary>
        /// 判断当前登录人是否是最后一个处理人
        /// </summary>
        /// <param name="SWID">收文ID</param>
        /// <param name="Status">处理人标志</param>
        /// <param name="LoginUser">当前登录人ID</param>
        /// <returns>返回True表示当前人是最后一个处理人</returns>
        private bool isLastOne(string SWID,string Status,string LoginUser)
        {
            string sql = "";
            DataTable dt = new DataTable();
            sql = "select 1 from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and SW_HANDER='" + Status + "' and SW_PLAN_ID<>'" + LoginUser + "' and IS_OK='0'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 判断当前环节是否是有人处理过
        /// </summary>
        /// <param name="SWID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        private bool isFristOne(string SWID, string Status)
        {
            string sql = "";
            DataTable dt = new DataTable();
            sql = "select 1 from T_OA_SW_HANDLE where SW_ID='" + SWID + "' and SW_HANDER='" + Status + "' and IS_OK='1'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 完成（归档）收文
        /// </summary>
        /// <param name="SWID">收文ID</param>
        /// <param name="SWStatus">收文新状态</param>
        /// <param name="SWPreStatus">收文前状态</param>
        /// <param name="LoginUser">登录人ID</param>
        /// <returns></returns>
        public bool FinishSW(string SWID, string SWStatus, string SWPreStatus, string LoginUser)
        {
            string sql = "";
            ArrayList list = new ArrayList();

            sql = "update T_OA_SW_INFO set SW_STATUS='" + SWStatus + "',PIGONHOLE_ID='" + LoginUser + "',PIGONHOLE_DATE='" + DateTime.Now + "' where ID='" + SWID + "'";
            list.Add(sql);

            sql = "update T_OA_SW_HANDLE set IS_OK='1',SW_PLAN_DATE='" + DateTime.Now + "' where SW_ID='" + SWID + "' and SW_HANDER='" + SWPreStatus + "' and SW_PLAN_ID='" + LoginUser + "'";
            list.Add(sql);

            return ExecuteSQLByTransaction(list);
        }
    }

}