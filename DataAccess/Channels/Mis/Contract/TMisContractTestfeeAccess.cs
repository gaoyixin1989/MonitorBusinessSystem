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
    /// 功能：委托书监测费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractTestfeeAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_TESTFEE " + this.BuildWhereStatement(tMisContractTestfee);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractTestfeeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_TESTFEE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractTestfeeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractTestfee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractTestfeeVo Details(TMisContractTestfeeVo tMisContractTestfee)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_TESTFEE " + this.BuildWhereStatement(tMisContractTestfee));
           return SqlHelper.ExecuteObject(new TMisContractTestfeeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractTestfeeVo> SelectByObject(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_TESTFEE " + this.BuildWhereStatement(tMisContractTestfee));
            return SqlHelper.ExecuteObjectList(tMisContractTestfee, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_TESTFEE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractTestfee));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_TESTFEE " + this.BuildWhereStatement(tMisContractTestfee);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <returns></returns>
        public TMisContractTestfeeVo SelectByObject(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_TESTFEE " + this.BuildWhereStatement(tMisContractTestfee);
            return SqlHelper.ExecuteObject(new TMisContractTestfeeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractTestfee, TMisContractTestfeeVo.T_MIS_CONTRACT_TESTFEE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractTestfee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractTestfee, TMisContractTestfeeVo.T_MIS_CONTRACT_TESTFEE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractTestfee.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractTestfee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractTestfee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractTestfeeVo tMisContractTestfee_UpdateSet, TMisContractTestfeeVo tMisContractTestfee_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractTestfee_UpdateSet, TMisContractTestfeeVo.T_MIS_CONTRACT_TESTFEE_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractTestfee_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_TESTFEE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractTestfeeVo tMisContractTestfee)
        {
            string strSQL = "delete from T_MIS_CONTRACT_TESTFEE ";
	    strSQL += this.BuildWhereStatement(tMisContractTestfee);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取指定委托监测费用明细 胡方扬 2013-04-11
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable GetContractConstFeeDetail(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {

            string strSQL = @"  select * from(
select  a.CONTRACT_ID, d.ID, e.MONITOR_TYPE_NAME, c.SAMPLE_NAME, f.ITEM_NAME
from T_MIS_MONITOR_TASK a 
left join T_MIS_MONITOR_SUBTASK b on(a.ID=b.TASK_ID) 
left join T_BASE_MONITOR_TYPE_INFO e on(b.MONITOR_ID=e.ID )
left join T_MIS_MONITOR_SAMPLE_INFO c on(b.ID=c.SUBTASK_ID) 
left join T_MIS_MONITOR_RESULT d on(c.ID=d.SAMPLE_ID)
left join T_BASE_ITEM_INFO f on (d.ITEM_ID=f.ID)
) A";
            if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_ID)) {
                strSQL += String.Format(" where A.CONTRACT_ID='{0}'", tMisContractTestfee.CONTRACT_ID);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取指定委托监测费用明细记录数 胡方扬 2013-04-11
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetContractConstFeeDetailCount(TMisContractTestfeeVo tMisContractTestfee)
        {

            string strSQL = @" SELECT A.*,C.POINT_NAME,D.MONITOR_TYPE_NAME FROM T_MIS_CONTRACT_TESTFEE A" +
                                        "  LEFT JOIN T_MIS_CONTRACT_POINTITEM B ON B.ID=A.CONTRACT_POINTITEM_ID" +
                                        "  LEFT JOIN T_MIS_CONTRACT_POINT C ON C.ID=B.CONTRACT_POINT_ID" +
                                        "  LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO D ON D.ID=C.MONITOR_ID";
            if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_ID))
            {
                strSQL += String.Format(" WHERE A.CONTRACT_ID='{0}'", tMisContractTestfee.CONTRACT_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractTestfeeVo tMisContractTestfee)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractTestfee)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisContractTestfee.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractTestfee.ID.ToString()));
				}	
				//委托书ID
				if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractTestfee.CONTRACT_ID.ToString()));
				}
                //点位ID
                if (!String.IsNullOrEmpty(tMisContractTestfee.PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_ID = '{0}'", tMisContractTestfee.PLAN_ID.ToString()));
                }	
				//频次次数，合同分几次执行
                if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_CODE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND CONTRACT_CODE = '{0}'", tMisContractTestfee.CONTRACT_CODE.ToString()));
				}	
				//监测项目ID
                if (!String.IsNullOrEmpty(tMisContractTestfee.TICKET_NUM.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND TICKET_NUM = '{0}'", tMisContractTestfee.TICKET_NUM.ToString()));
				}	
				//样品数，实际就是点位数
                if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_YEAR.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND CONTRACT_YEAR = '{0}'", tMisContractTestfee.CONTRACT_YEAR.ToString()));
				}
                //测点数
                if (!String.IsNullOrEmpty(tMisContractTestfee.PROJECT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_NAME = '{0}'", tMisContractTestfee.PROJECT_NAME.ToString()));
                }
                //测试分析费
                if (!String.IsNullOrEmpty(tMisContractTestfee.CONTRACT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_TYPE = '{0}'", tMisContractTestfee.CONTRACT_TYPE.ToString()));
                }	
				//分析单价
                if (!String.IsNullOrEmpty(tMisContractTestfee.TEST_TYPE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND TEST_TYPE = '{0}'", tMisContractTestfee.TEST_TYPE.ToString()));
				}	
				//分析费用，分析单价×频次×样品数
                if (!String.IsNullOrEmpty(tMisContractTestfee.TEST_PURPOSE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND TEST_PURPOSE = '{0}'", tMisContractTestfee.TEST_PURPOSE.ToString()));
				}	
				//开机费用单价
                if (!String.IsNullOrEmpty(tMisContractTestfee.CLIENT_COMPANY_ID.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND CLIENT_COMPANY_ID = '{0}'", tMisContractTestfee.CLIENT_COMPANY_ID.ToString()));
				}	
				//开机总费用，开机费用单价×频次
                if (!String.IsNullOrEmpty(tMisContractTestfee.TESTED_COMPANY_ID.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND TESTED_COMPANY_ID = '{0}'", tMisContractTestfee.TESTED_COMPANY_ID.ToString()));
				}	
				//小计，分析总费用+开机总费用
                if (!String.IsNullOrEmpty(tMisContractTestfee.CONSIGN_DATE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND CONSIGN_DATE = '{0}'", tMisContractTestfee.CONSIGN_DATE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractTestfee.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractTestfee.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractTestfee.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractTestfee.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractTestfee.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractTestfee.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractTestfee.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractTestfee.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractTestfee.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractTestfee.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
