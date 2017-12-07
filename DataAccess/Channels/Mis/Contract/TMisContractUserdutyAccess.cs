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
    /// 功能：监测计划岗位信息
    /// 创建日期：2012-12-17
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractUserdutyAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_USERDUTY " + this.BuildWhereStatement(tMisContractUserduty);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractUserdutyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_USERDUTY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractUserdutyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractUserduty">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractUserdutyVo Details(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_USERDUTY " + this.BuildWhereStatement(tMisContractUserduty));
            return SqlHelper.ExecuteObject(new TMisContractUserdutyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractUserdutyVo> SelectByObject(TMisContractUserdutyVo tMisContractUserduty, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_USERDUTY " + this.BuildWhereStatement(tMisContractUserduty));
            return SqlHelper.ExecuteObjectList(tMisContractUserduty, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractUserdutyVo tMisContractUserduty, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_USERDUTY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractUserduty));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = "select * from T_MIS_CONTRACT_USERDUTY " + this.BuildWhereStatement(tMisContractUserduty);
            return SqlHelper.ExecuteDataTable(strSQL);
        }



        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <returns></returns>
        public TMisContractUserdutyVo SelectByObject(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = "select * from T_MIS_CONTRACT_USERDUTY " + this.BuildWhereStatement(tMisContractUserduty);
            return SqlHelper.ExecuteObject(new TMisContractUserdutyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractUserduty, TMisContractUserdutyVo.T_MIS_CONTRACT_USERDUTY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractUserduty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractUserduty, TMisContractUserdutyVo.T_MIS_CONTRACT_USERDUTY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractUserduty.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractUserduty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractUserduty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractUserdutyVo tMisContractUserduty_UpdateSet, TMisContractUserdutyVo tMisContractUserduty_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractUserduty_UpdateSet, TMisContractUserdutyVo.T_MIS_CONTRACT_USERDUTY_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractUserduty_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_USERDUTY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = "delete from T_MIS_CONTRACT_USERDUTY ";
            strSQL += this.BuildWhereStatement(tMisContractUserduty);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 保存监测计划岗位
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <param name="strMonitorId"></param>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public bool SaveContractPlanDuty(TMisContractUserdutyVo tMisContractUserduty, string[] strMonitorId, string[] strUserId)
        {
            ArrayList arrVo = new ArrayList();
            DataTable dt = new DataTable();
            int i = 0;
            foreach (string strMointor in strMonitorId)
            {
                if (!String.IsNullOrEmpty(strMointor))
                {
                    tMisContractUserduty.MONITOR_TYPE_ID = strMointor;
                    dt = SqlHelper.ExecuteDataTable(String.Format("SELECT * FROM T_MIS_CONTRACT_USERDUTY WHERE CONTRACT_ID='{0}' AND MONITOR_TYPE_ID='{1}' AND CONTRACT_PLAN_ID='{2}'", tMisContractUserduty.CONTRACT_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.CONTRACT_PLAN_ID));
                    if (dt.Rows.Count > 0)
                    {
                        if (i + 1 <= strUserId.Length)
                        {
                            if (!String.IsNullOrEmpty(strUserId[i]))
                            {
                                tMisContractUserduty.SAMPLING_MANAGER_ID = strUserId[i];
                                string strSQL = String.Format("UPDATE T_MIS_CONTRACT_USERDUTY SET SAMPLING_MANAGER_ID='{0}' WHERE CONTRACT_ID='{1}' AND MONITOR_TYPE_ID='{2}' AND CONTRACT_PLAN_ID='{3}'", tMisContractUserduty.SAMPLING_MANAGER_ID, tMisContractUserduty.CONTRACT_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.CONTRACT_PLAN_ID);
                                i++;
                                arrVo.Add(strSQL);
                            }

                        }
                    }
                    else
                    {
                        if (i + 1 <= strUserId.Length)
                        {
                            if (!String.IsNullOrEmpty(strUserId[i]))
                            {
                                tMisContractUserduty.SAMPLING_MANAGER_ID = strUserId[i];
                                tMisContractUserduty.ID = GetSerialNumber("t_mis_contract_userdutyId");
                                string strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_USERDUTY(ID,CONTRACT_PLAN_ID,CONTRACT_ID,MONITOR_TYPE_ID,SAMPLING_MANAGER_ID)  VALUES('{0}','{1}','{2}','{3}','{4}')",
                                    tMisContractUserduty.ID, tMisContractUserduty.CONTRACT_PLAN_ID, tMisContractUserduty.CONTRACT_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.SAMPLING_MANAGER_ID);
                                i++;
                                arrVo.Add(strSQL);
                            }
                        }
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 保存环境质量类监测计划岗位
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <param name="strMonitorId"></param>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public bool SaveContractPlanDutyForEnv(TMisContractUserdutyVo tMisContractUserduty, string[] strMonitorId, string[] strUserId)
        {
            ArrayList arrVo = new ArrayList();
            DataTable dt = new DataTable();
            int i = 0;
            foreach (string strMointor in strMonitorId)
            {
                if (!String.IsNullOrEmpty(strMointor))
                {
                    tMisContractUserduty.MONITOR_TYPE_ID = strMointor;
                    dt = SqlHelper.ExecuteDataTable(String.Format("SELECT * FROM T_MIS_CONTRACT_USERDUTY WHERE MONITOR_TYPE_ID='{0}' AND CONTRACT_PLAN_ID='{1}'", tMisContractUserduty.CONTRACT_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.CONTRACT_PLAN_ID));
                    if (dt.Rows.Count > 0)
                    {
                        if (i + 1 <= strUserId.Length)
                        {
                            if (!String.IsNullOrEmpty(strUserId[i]))
                            {
                                tMisContractUserduty.SAMPLING_MANAGER_ID = strUserId[i];
                                string strSQL = String.Format("UPDATE T_MIS_CONTRACT_USERDUTY SET SAMPLING_MANAGER_ID='{0}' WHER MONITOR_TYPE_ID='{1}' AND CONTRACT_PLAN_ID='{2}'", tMisContractUserduty.SAMPLING_MANAGER_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.CONTRACT_PLAN_ID);
                                i++;
                                arrVo.Add(strSQL);
                            }

                        }
                    }
                    else
                    {
                        if (i + 1 <= strUserId.Length)
                        {
                            if (!String.IsNullOrEmpty(strUserId[i]))
                            {
                                tMisContractUserduty.SAMPLING_MANAGER_ID = strUserId[i];
                                tMisContractUserduty.ID = GetSerialNumber("t_mis_contract_userdutyId");
                                string strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_USERDUTY(ID,CONTRACT_PLAN_ID,MONITOR_TYPE_ID,SAMPLING_MANAGER_ID)  VALUES('{0}','{1}','{2}','{3}')",
                                    tMisContractUserduty.ID, tMisContractUserduty.CONTRACT_PLAN_ID, tMisContractUserduty.MONITOR_TYPE_ID, tMisContractUserduty.SAMPLING_MANAGER_ID);
                                i++;
                                arrVo.Add(strSQL);
                            }
                        }
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取监测计划负责人 胡方扬
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <returns></returns>
        public DataTable SelectDutyUser(TMisContractUserdutyVo tMisContractUserduty)
        {
            string strSQL = String.Format("SELECT A.*,B.MONITOR_TYPE_NAME,C.REAL_NAME from T_MIS_CONTRACT_USERDUTY A" +
                                       " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO B ON B.ID=A.MONITOR_TYPE_ID" +
                                        " INNER JOIN dbo.T_SYS_USER C ON C.IS_DEL='0' and C.IS_USE='1'  AND C.IS_HIDE='0' AND C.ID=A.SAMPLING_MANAGER_ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractUserduty.CONTRACT_ID))
            {
                strSQL += String.Format(" AND CONTRACT_ID='{0}'", tMisContractUserduty.CONTRACT_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractUserduty.MONITOR_TYPE_ID))
            {
                strSQL += String.Format(" AND MONITOR_TYPE_ID='{0}'", tMisContractUserduty.MONITOR_TYPE_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractUserduty.CONTRACT_PLAN_ID))
            {
                strSQL += String.Format(" AND CONTRACT_PLAN_ID='{0}'", tMisContractUserduty.CONTRACT_PLAN_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractUserdutyVo tMisContractUserduty)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractUserduty)
            {

                //
                if (!String.IsNullOrEmpty(tMisContractUserduty.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractUserduty.ID.ToString()));
                }
                //监测计划ID
                if (!String.IsNullOrEmpty(tMisContractUserduty.CONTRACT_PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_PLAN_ID = '{0}'", tMisContractUserduty.CONTRACT_PLAN_ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisContractUserduty.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractUserduty.CONTRACT_ID.ToString()));
                }
                //监测类别
                if (!String.IsNullOrEmpty(tMisContractUserduty.MONITOR_TYPE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TYPE_ID = '{0}'", tMisContractUserduty.MONITOR_TYPE_ID.ToString()));
                }
                //默认负责人ID
                if (!String.IsNullOrEmpty(tMisContractUserduty.SAMPLING_MANAGER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_MANAGER_ID = '{0}'", tMisContractUserduty.SAMPLING_MANAGER_ID.ToString()));
                }
                //默认协同人ID
                if (!String.IsNullOrEmpty(tMisContractUserduty.SAMPLING_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_ID = '{0}'", tMisContractUserduty.SAMPLING_ID.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
