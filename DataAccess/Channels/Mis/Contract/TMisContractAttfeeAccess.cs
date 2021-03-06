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
    /// 功能：委托书附加费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_ATTFEE " + this.BuildWhereStatement(tMisContractAttfee);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractAttfeeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractAttfee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeVo Details(TMisContractAttfeeVo tMisContractAttfee)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEE " + this.BuildWhereStatement(tMisContractAttfee));
           return SqlHelper.ExecuteObject(new TMisContractAttfeeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractAttfeeVo> SelectByObject(TMisContractAttfeeVo tMisContractAttfee, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEE " + this.BuildWhereStatement(tMisContractAttfee));
            return SqlHelper.ExecuteObjectList(tMisContractAttfee, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractAttfeeVo tMisContractAttfee, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_ATTFEE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractAttfee));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractAttfee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_ATTFEE " + this.BuildWhereStatement(tMisContractAttfee);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <returns></returns>
        public TMisContractAttfeeVo SelectByObject(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_ATTFEE " + this.BuildWhereStatement(tMisContractAttfee);
            return SqlHelper.ExecuteObject(new TMisContractAttfeeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractAttfee, TMisContractAttfeeVo.T_MIS_CONTRACT_ATTFEE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractAttfee, TMisContractAttfeeVo.T_MIS_CONTRACT_ATTFEE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractAttfee.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractAttfee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeVo tMisContractAttfee_UpdateSet, TMisContractAttfeeVo tMisContractAttfee_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractAttfee_UpdateSet, TMisContractAttfeeVo.T_MIS_CONTRACT_ATTFEE_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractAttfee_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_ATTFEE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractAttfeeVo tMisContractAttfee)
        {
            string strSQL = "delete from T_MIS_CONTRACT_ATTFEE ";
	    strSQL += this.BuildWhereStatement(tMisContractAttfee);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 删除委托书监测点位的附加项目 Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveAttItems(TMisContractAttfeeVo tMisContractAttfee, string[] strMoveAttItems)
        {
            ArrayList arrVo = new ArrayList();

            foreach (string strItemId in strMoveAttItems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strSQL = String.Format("DELETE FROM T_MIS_CONTRACT_ATTFEE WHERE CONTRACT_ID='{0}' AND ATT_FEE_ITEM_ID = '{1}'", tMisContractAttfee.CONTRACT_ID, strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 向委托书附加项目中插入保存了且不存在的数据  Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddAtttems"></param>
        /// <returns></returns>
        public bool EditAttItems(TMisContractAttfeeVo tMisContractAttfee, string[] strAddAtttems)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string strItemId in strAddAtttems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    tMisContractAttfee.ID = GetSerialNumber("t_mis_contract_AttfeeId");
                    string strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_ATTFEE(ID,CONTRACT_ID,ATT_FEE_ITEM_ID,FEE)  SELECT '{0}','{1}','{2}',PRICE FROM T_MIS_CONTRACT_ATTFEEITEM WHERE ID='{3}'", tMisContractAttfee.ID, tMisContractAttfee.CONTRACT_ID, strItemId,strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractAttfee"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractAttfeeVo tMisContractAttfee)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractAttfee)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisContractAttfee.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractAttfee.ID.ToString()));
				}	
				//委托书ID
				if (!String.IsNullOrEmpty(tMisContractAttfee.CONTRACT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractAttfee.CONTRACT_ID.ToString()));
				}	
				//频次次数
				if (!String.IsNullOrEmpty(tMisContractAttfee.ATT_FEE_ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_FEE_ITEM_ID = '{0}'", tMisContractAttfee.ATT_FEE_ITEM_ID.ToString()));
				}	
				//实际附加费用
				if (!String.IsNullOrEmpty(tMisContractAttfee.FEE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FEE = '{0}'", tMisContractAttfee.FEE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractAttfee.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractAttfee.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractAttfee.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractAttfee.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractAttfee.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractAttfee.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractAttfee.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractAttfee.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractAttfee.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractAttfee.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
