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
    /// 功能：委托书附加费用单价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeitemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_ATTFEEITEM " + this.BuildWhereStatement(tMisContractAttfeeitem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeitemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEEITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractAttfeeitemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeitemVo Details(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEEITEM " + this.BuildWhereStatement(tMisContractAttfeeitem));
           return SqlHelper.ExecuteObject(new TMisContractAttfeeitemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractAttfeeitemVo> SelectByObject(TMisContractAttfeeitemVo tMisContractAttfeeitem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_ATTFEEITEM " + this.BuildWhereStatement(tMisContractAttfeeitem));
            return SqlHelper.ExecuteObjectList(tMisContractAttfeeitem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractAttfeeitemVo tMisContractAttfeeitem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_ATTFEEITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractAttfeeitem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractAttfeeitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_ATTFEEITEM " + this.BuildWhereStatement(tMisContractAttfeeitem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <returns></returns>
        public TMisContractAttfeeitemVo SelectByObject(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_ATTFEEITEM " + this.BuildWhereStatement(tMisContractAttfeeitem);
            return SqlHelper.ExecuteObject(new TMisContractAttfeeitemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractAttfeeitem, TMisContractAttfeeitemVo.T_MIS_CONTRACT_ATTFEEITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfeeitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractAttfeeitem, TMisContractAttfeeitemVo.T_MIS_CONTRACT_ATTFEEITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractAttfeeitem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfeeitem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractAttfeeitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeitemVo tMisContractAttfeeitem_UpdateSet, TMisContractAttfeeitemVo tMisContractAttfeeitem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractAttfeeitem_UpdateSet, TMisContractAttfeeitemVo.T_MIS_CONTRACT_ATTFEEITEM_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractAttfeeitem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_ATTFEEITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            string strSQL = "delete from T_MIS_CONTRACT_ATTFEEITEM ";
	    strSQL += this.BuildWhereStatement(tMisContractAttfeeitem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractAttfeeitem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractAttfeeitem)
            {
			    	
				//
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractAttfeeitem.ID.ToString()));
				}	
				//附加项目
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.ATT_FEE_ITEM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_FEE_ITEM = '{0}'", tMisContractAttfeeitem.ATT_FEE_ITEM.ToString()));
				}	
				//费用单价
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.PRICE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRICE = '{0}'", tMisContractAttfeeitem.PRICE.ToString()));
				}	
				//费用描述
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INFO = '{0}'", tMisContractAttfeeitem.INFO.ToString()));
				}
                //删除标记
                if (!String.IsNullOrEmpty(tMisContractAttfeeitem.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisContractAttfeeitem.IS_DEL.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractAttfeeitem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractAttfeeitem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractAttfeeitem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractAttfeeitem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractAttfeeitem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractAttfeeitem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
