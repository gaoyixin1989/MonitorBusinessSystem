using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.SUPPLIER;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商产品评价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierJudgeAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = "select Count(*) from T_OA_SUPPLIER_JUDGE " + this.BuildWhereStatement(tOaSupplierJudge);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSupplierJudgeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_SUPPLIER_JUDGE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaSupplierJudgeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSupplierJudge">对象条件</param>
        /// <returns>对象</returns>
        public TOaSupplierJudgeVo Details(TOaSupplierJudgeVo tOaSupplierJudge)
        {
           string strSQL = String.Format("select * from  T_OA_SUPPLIER_JUDGE " + this.BuildWhereStatement(tOaSupplierJudge));
           return SqlHelper.ExecuteObject(new TOaSupplierJudgeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSupplierJudgeVo> SelectByObject(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_SUPPLIER_JUDGE " + this.BuildWhereStatement(tOaSupplierJudge));
            return SqlHelper.ExecuteObjectList(tOaSupplierJudge, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_SUPPLIER_JUDGE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaSupplierJudge));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSupplierJudge"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = "select * from T_OA_SUPPLIER_JUDGE " + this.BuildWhereStatement(tOaSupplierJudge);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            string strSQL1 = " select * from T_OA_SUPPLIER_JUDGE {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tOaSupplierJudge));
            string strSQL = "select a.SUPPLIER_NAME,i.* from (" + strSQL1 + ")i";
            strSQL += " join T_OA_SUPPLIER_INFO a on a.id=i.SUPPLIER_ID";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <returns></returns>
        public TOaSupplierJudgeVo SelectByObject(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = "select * from T_OA_SUPPLIER_JUDGE " + this.BuildWhereStatement(tOaSupplierJudge);
            return SqlHelper.ExecuteObject(new TOaSupplierJudgeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaSupplierJudge, TOaSupplierJudgeVo.T_OA_SUPPLIER_JUDGE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierJudge">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSupplierJudge, TOaSupplierJudgeVo.T_OA_SUPPLIER_JUDGE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaSupplierJudge.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierJudge_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSupplierJudge_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierJudgeVo tOaSupplierJudge_UpdateSet, TOaSupplierJudgeVo tOaSupplierJudge_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSupplierJudge_UpdateSet, TOaSupplierJudgeVo.T_OA_SUPPLIER_JUDGE_TABLE);
            strSQL += this.BuildWhereStatement(tOaSupplierJudge_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_SUPPLIER_JUDGE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            string strSQL = "delete from T_OA_SUPPLIER_JUDGE ";
	    strSQL += this.BuildWhereStatement(tOaSupplierJudge);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSupplierJudge"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSupplierJudge)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaSupplierJudge.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaSupplierJudge.ID.ToString()));
				}	
				//供应商ID
				if (!String.IsNullOrEmpty(tOaSupplierJudge.SUPPLIER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUPPLIER_ID = '{0}'", tOaSupplierJudge.SUPPLIER_ID.ToString()));
				}	
				//物料名称
				if (!String.IsNullOrEmpty(tOaSupplierJudge.PARTNAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PARTNAME = '{0}'", tOaSupplierJudge.PARTNAME.ToString()));
				}	
				//规格型号
				if (!String.IsNullOrEmpty(tOaSupplierJudge.MODEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MODEL = '{0}'", tOaSupplierJudge.MODEL.ToString()));
				}	
				//参考价
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REFERENCEPRICE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REFERENCEPRICE = '{0}'", tOaSupplierJudge.REFERENCEPRICE.ToString()));
				}	
				//产品标准
				if (!String.IsNullOrEmpty(tOaSupplierJudge.PRODUCTSTANDARD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRODUCTSTANDARD = '{0}'", tOaSupplierJudge.PRODUCTSTANDARD.ToString()));
				}	
				//最短供货期
				if (!String.IsNullOrEmpty(tOaSupplierJudge.DELIVERYPERIOD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DELIVERYPERIOD = '{0}'", tOaSupplierJudge.DELIVERYPERIOD.ToString()));
				}	
				//供货数量
				if (!String.IsNullOrEmpty(tOaSupplierJudge.QUANTITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QUANTITY = '{0}'", tOaSupplierJudge.QUANTITY.ToString()));
				}	
				//供应商编码
				if (!String.IsNullOrEmpty(tOaSupplierJudge.ENTERPRISECODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENTERPRISECODE = '{0}'", tOaSupplierJudge.ENTERPRISECODE.ToString()));
				}	
				//质量保证体系
				if (!String.IsNullOrEmpty(tOaSupplierJudge.QUATITYSYSTEM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QUATITYSYSTEM = '{0}'", tOaSupplierJudge.QUATITYSYSTEM.ToString()));
				}	
				//合同信守情况
				if (!String.IsNullOrEmpty(tOaSupplierJudge.SINCERITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SINCERITY = '{0}'", tOaSupplierJudge.SINCERITY.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaSupplierJudge.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaSupplierJudge.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaSupplierJudge.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaSupplierJudge.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaSupplierJudge.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaSupplierJudge.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
