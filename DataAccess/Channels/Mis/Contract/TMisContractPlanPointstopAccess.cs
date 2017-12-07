using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：点位停产
    /// 创建日期：2013-03-13
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractPlanPointstopAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_PLAN_POINTSTOP " + this.BuildWhereStatement(tMisContractPlanPointstop);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointstopVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINTSTOP  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractPlanPointstopVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointstopVo Details(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINTSTOP " + this.BuildWhereStatement(tMisContractPlanPointstop));
            return SqlHelper.ExecuteObject(new TMisContractPlanPointstopVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanPointstopVo> SelectByObject(TMisContractPlanPointstopVo tMisContractPlanPointstop, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINTSTOP " + this.BuildWhereStatement(tMisContractPlanPointstop));
            return SqlHelper.ExecuteObjectList(tMisContractPlanPointstop, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanPointstopVo tMisContractPlanPointstop, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_PLAN_POINTSTOP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPlanPointstop));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN_POINTSTOP " + this.BuildWhereStatement(tMisContractPlanPointstop);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <returns></returns>
        public TMisContractPlanPointstopVo SelectByObject(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN_POINTSTOP " + this.BuildWhereStatement(tMisContractPlanPointstop);
            return SqlHelper.ExecuteObject(new TMisContractPlanPointstopVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPlanPointstop, TMisContractPlanPointstopVo.T_MIS_CONTRACT_PLAN_POINTSTOP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPointstop">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlanPointstop, TMisContractPlanPointstopVo.T_MIS_CONTRACT_PLAN_POINTSTOP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPlanPointstop.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPointstop_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlanPointstop_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointstopVo tMisContractPlanPointstop_UpdateSet, TMisContractPlanPointstopVo tMisContractPlanPointstop_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlanPointstop_UpdateSet, TMisContractPlanPointstopVo.T_MIS_CONTRACT_PLAN_POINTSTOP_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPlanPointstop_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_PLAN_POINTSTOP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = "delete from T_MIS_CONTRACT_PLAN_POINTSTOP ";
            strSQL += this.BuildWhereStatement(tMisContractPlanPointstop);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 记录停产原因时间等信息
        /// </summary>
        /// <param name="strSubTaskArrId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool InsertStopPointForSampleItems(string strSubTaskArrId, TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            bool flag = false;
            ArrayList AvoList = new ArrayList();
            DataTable dt=new DataTable();
            if (!String.IsNullOrEmpty(strSubTaskArrId))
            {
                //构造子任务点位ID SQL语法
                string strSubTaskId = strSubTaskArrId.Replace(",", ",'");
                //记录数执行查询
                string strSQLSearch = String.Format("SELECT A.PLAN_ID, A.POINT_FREQ_ID FROM T_MIS_CONTRACT_PLAN_POINT A LEFT JOIN " +
                                        " dbo.T_MIS_MONITOR_TASK B ON B.PLAN_ID=A.PLAN_ID" +
                                        " LEFT JOIN dbo.T_MIS_MONITOR_TASK_POINT C ON C.TASK_ID=B.ID WHERE C.ID IN ('{0}') AND C.CONTRACT_POINT_ID=A. CONTRACT_POINT_ID", strSubTaskId);
                dt = SqlHelper.ExecuteDataTable(strSQLSearch);
                if (dt.Rows.Count > 0) {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strStopPointId = GetSerialNumber("t_mis_contractPointStopID");
                        strSQLSearch = String.Format(@"INSERT INTO dbo.T_MIS_CONTRACT_PLAN_POINTSTOP(ID,CONTRACT_ID,CONTRACT_COMPANY_ID,CONTRACT_POINT_ID,STOPRESON,ACTIONDATE,ACTION_USERID)
SELECT '{0}', B.CONTRACT_ID,B.CONTRACT_COMPANY_ID,E.ID,'{1}','{2}','{3}'
FROM T_MIS_CONTRACT_PLAN_POINT A 
LEFT JOIN dbo.T_MIS_CONTRACT_PLAN B ON B.ID=A.PLAN_ID
LEFT JOIN dbo.T_MIS_CONTRACT C ON C.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY D ON D.ID=B.CONTRACT_COMPANY_ID
LEFT JOIN dbo.T_MIS_CONTRACT_POINT E ON E.ID=A.CONTRACT_POINT_ID
WHERE A.PLAN_ID='{4}' AND A.POINT_FREQ_ID='{5}'",strStopPointId, tMisContractPlanPointstop.STOPRESON,tMisContractPlanPointstop.ACTIONDATE,tMisContractPlanPointstop.ACTION_USERID,dt.Rows[i]["PLAN_ID"].ToString(), dt.Rows[i]["POINT_FREQ_ID"].ToString());
                        AvoList.Add(strSQLSearch);
                    }
                    flag = SqlHelper.ExecuteSQLByTransaction(AvoList);
                }
            }
            return flag;
        }

        /// <summary>
        /// 获取停产的点位列表
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public DataTable GetStopPointForSampleList(TMisContractPlanPointstopVo tMisContractPlanPointstop,int iIndex,int iCount)
        {
            string strSQL = String.Format(@"SELECT A.ID,A.CONTRACT_ID,A.CONTRACT_POINT_ID,A.CONTRACT_COMPANY_ID,A.ACTIONDATE,A.ACTION_USERID,
B.PROJECT_NAME,B.CONTRACT_CODE,C.POINT_NAME,D.COMPANY_NAME,E.REAL_NAME
FROM dbo.T_MIS_CONTRACT_PLAN_POINTSTOP A
LEFT JOIN dbo.T_MIS_CONTRACT B ON B.ID=A.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_POINT C ON C.ID=A.CONTRACT_POINT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY D ON D.ID=A.CONTRACT_COMPANY_ID
LEFT JOIN dbo.T_SYS_USER E ON E.ID=A.ACTION_USERID WHERE 1=1");
            if(!String.IsNullOrEmpty(tMisContractPlanPointstop.ACTION_USERID)){
                strSQL += String.Format(" AND E.REAL_NAME LIKE '%{0}%'", tMisContractPlanPointstop.ACTION_USERID);
            }
            if (!String.IsNullOrEmpty(tMisContractPlanPointstop.CONTRACT_POINT_ID))
            {
                strSQL += String.Format(" AND C.POINT_NAME LIKE '%{0}%'", tMisContractPlanPointstop.CONTRACT_POINT_ID);
            }

            if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK4) && !String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK5))
            {
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ACTIONDATE,23),111) BETWEEN BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tMisContractPlanPointstop.REMARK4,tMisContractPlanPointstop.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress( strSQL,iIndex,iCount));
        }

        /// <summary>
        /// 获取停产的点位列表总记录数
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public int GetStopPointForSampleListCount(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            string strSQL = String.Format(@"SELECT A.ID,A.CONTRACT_ID,A.CONTRACT_POINT_ID,A.CONTRACT_COMPANY_ID,A.ACTIONDATE,A.ACTION_USERID,
B.PROJECT_NAME,B.CONTRACT_CODE,C.POINT_NAME,D.COMPANY_NAME,E.REAL_NAME
FROM dbo.T_MIS_CONTRACT_PLAN_POINTSTOP A
LEFT JOIN dbo.T_MIS_CONTRACT B ON B.ID=A.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_POINT C ON C.ID=A.CONTRACT_POINT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY D ON D.ID=A.CONTRACT_COMPANY_ID
LEFT JOIN dbo.T_SYS_USER E ON E.ID=A.ACTION_USERID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPlanPointstop.ACTION_USERID))
            {
                strSQL += String.Format(" AND E.REAL_NAME LIKE '%{0}%'", tMisContractPlanPointstop.ACTION_USERID);
            }
            if (!String.IsNullOrEmpty(tMisContractPlanPointstop.CONTRACT_POINT_ID))
            {
                strSQL += String.Format(" AND C.POINT_NAME LIKE '%{0}%'", tMisContractPlanPointstop.CONTRACT_POINT_ID);
            }

            if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK4) && !String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK5))
            {
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ACTIONDATE,23),111) BETWEEN BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tMisContractPlanPointstop.REMARK4, tMisContractPlanPointstop.REMARK5);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion



        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPlanPointstop)
            {

                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPlanPointstop.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractPlanPointstop.CONTRACT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.CONTRACT_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_POINT_ID = '{0}'", tMisContractPlanPointstop.CONTRACT_POINT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.CONTRACT_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_COMPANY_ID = '{0}'", tMisContractPlanPointstop.CONTRACT_COMPANY_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.STOPRESON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STOPRESON = '{0}'", tMisContractPlanPointstop.STOPRESON.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.ACTIONDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACTIONDATE = '{0}'", tMisContractPlanPointstop.ACTIONDATE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.ACTION_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACTION_USERID = '{0}'", tMisContractPlanPointstop.ACTION_USERID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractPlanPointstop.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractPlanPointstop.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractPlanPointstop.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractPlanPointstop.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisContractPlanPointstop.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractPlanPointstop.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
