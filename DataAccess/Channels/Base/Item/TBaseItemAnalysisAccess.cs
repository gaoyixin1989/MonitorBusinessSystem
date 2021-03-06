using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemAnalysisAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = "select Count(*) from T_BASE_ITEM_ANALYSIS " + this.BuildWhereStatement(tBaseItemAnalysis);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemAnalysisVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_ANALYSIS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseItemAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemAnalysisVo Details(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
           string strSQL = String.Format("select * from  T_BASE_ITEM_ANALYSIS " + this.BuildWhereStatement(tBaseItemAnalysis));
           return SqlHelper.ExecuteObject(new TBaseItemAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemAnalysisVo> SelectByObject(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_ITEM_ANALYSIS " + this.BuildWhereStatement(tBaseItemAnalysis));
            return SqlHelper.ExecuteObjectList(tBaseItemAnalysis, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_ITEM_ANALYSIS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseItemAnalysis));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemAnalysis"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = "select * from T_BASE_ITEM_ANALYSIS " + this.BuildWhereStatement(tBaseItemAnalysis);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL1 = " select is_del,ID,ITEM_ID,ANALYSIS_METHOD_ID,INSTRUMENT_ID,UNIT as Unitcode,PRECISION,UPPER_LIMIT,LOWER_LIMIT,LOWER_CHECKOUT,IS_DEFAULT from T_BASE_ITEM_ANALYSIS {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tBaseItemAnalysis));
            string strSQL = "select a.ANALYSIS_NAME as ANALYSIS_METHOD,a.METHOD_ID,m.METHOD_CODE as METHOD,ai.[NAME] as INSTRUMENT,ii.ITEM_NAME,d.DICT_TEXT as UNIT";
            strSQL += ",i.* from (" + strSQL1 + ")i";
            strSQL += " join T_BASE_METHOD_ANALYSIS a on a.id=i.ANALYSIS_METHOD_ID";
            strSQL += " join T_BASE_METHOD_INFO m on m.id=a.METHOD_ID";
            strSQL += " join T_BASE_ITEM_INFO ii on ii.id=i.ITEM_ID";
            strSQL += " left join T_BASE_APPARATUS_INFO ai on ai.id=i.INSTRUMENT_ID and ai.is_del='0'";
            strSQL += " join T_SYS_DICT ai on d.DICT_CODE=i.Unitcode and d.DICT_TYPE='item_unit'";
            strSQL += " where d.is_del='0' and a.is_del='0' and m.is_del='0' and ii.is_del='0'";

            strSQL = "select Count(*) from (" + strSQL + ")h ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {

            string strSQL1 = " select is_del,ID,ITEM_ID,ANALYSIS_METHOD_ID,INSTRUMENT_ID,UNIT as Unitcode,PRECISION,UPPER_LIMIT,LOWER_LIMIT,LOWER_CHECKOUT,IS_DEFAULT from T_BASE_ITEM_ANALYSIS {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tBaseItemAnalysis));
            string strSQL = "select a.ANALYSIS_NAME as ANALYSIS_METHOD,a.METHOD_ID,m.METHOD_CODE as METHOD,ai.APPARATUS_CODE,ai.NAME as INSTRUMENT,ai.MODEL,ii.ITEM_NAME,d.DICT_TEXT as UNIT";
            strSQL += ",i.* from (" + strSQL1 + ")i";
            strSQL += " join T_BASE_METHOD_ANALYSIS a on a.id=i.ANALYSIS_METHOD_ID";
            strSQL += " join T_BASE_METHOD_INFO m on m.id=a.METHOD_ID";
            strSQL += " join T_BASE_ITEM_INFO ii on ii.id=i.ITEM_ID";
            strSQL += " left join T_BASE_APPARATUS_INFO ai on ai.id=i.INSTRUMENT_ID and ai.is_del='0'";
            strSQL += " left join T_SYS_DICT d on d.DICT_CODE=i.Unitcode and d.DICT_TYPE='item_unit'";
            strSQL += " where i.is_del='0' and a.is_del='0' and m.is_del='0' and ii.is_del='0'";
            strSQL += " order by i.id";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable,不分页 by 熊卫华2012.11.04
        /// </summary>
        /// <param name="tBaseItemAnalysis">分析方法对象</param>
        /// <returns></returns>
        public DataTable SelectByTable_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL1 = " select is_del,ID,ITEM_ID,ANALYSIS_METHOD_ID,INSTRUMENT_ID,UNIT as Unitcode,PRECISION,UPPER_LIMIT,LOWER_LIMIT,LOWER_CHECKOUT,IS_DEFAULT from T_BASE_ITEM_ANALYSIS {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tBaseItemAnalysis));
            string strSQL = "select a.ANALYSIS_NAME as ANALYSIS_METHOD,a.METHOD_ID,m.METHOD_CODE as METHOD,ai.[NAME] as INSTRUMENT,ii.ITEM_NAME,d.DICT_TEXT as UNIT";
            strSQL += ",i.* from (" + strSQL1 + ")i";
            strSQL += " join T_BASE_METHOD_ANALYSIS a on a.id=i.ANALYSIS_METHOD_ID";
            strSQL += " join T_BASE_METHOD_INFO m on m.id=a.METHOD_ID";
            strSQL += " join T_BASE_ITEM_INFO ii on ii.id=i.ITEM_ID";
            strSQL += " left join T_BASE_APPARATUS_INFO ai on ai.id=i.INSTRUMENT_ID and ai.is_del='0'";
            strSQL += " join T_SYS_DICT d on d.DICT_CODE=i.Unitcode and d.DICT_TYPE='item_unit'";
            strSQL += " where i.is_del='0' and a.is_del='0' and m.is_del='0' and ii.is_del='0'";
            strSQL += " order by i.id";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        //LJN 2013/6/14,环境空气
        public DataTable SelectByTable_ByJoin1(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.ID,a.ITEM_NAME,b.ANALYSIS_METHOD_ID,d.METHOD_NAME METHOD,c.ANALYSIS_NAME ANALYSIS_METHOD,b.INSTRUMENT_ID,e.Name INSTRUMENT,f.DICT_TEXT UNIT,b.LOWER_CHECKOUT LOWER_CHECKOUT ");
            sql.Append("from T_BASE_ITEM_INFO a left join T_BASE_ITEM_ANALYSIS b on(a.ID=b.ITEM_ID) ");
            sql.Append("left join T_BASE_METHOD_ANALYSIS c on(b.ANALYSIS_METHOD_ID=c.ID) ");
            sql.Append("left join T_BASE_METHOD_INFO d on(c.METHOD_ID=d.ID) ");
            sql.Append("left join T_BASE_APPARATUS_INFO e on(b.INSTRUMENT_ID=e.ID) ");
            sql.Append("left join T_SYS_DICT f on(b.UNIT=f.ID and f.DICT_TYPE='item_unit') ");
            sql.Append("where a.ID='" + ID + "'");
            return SqlHelper.ExecuteDataTable(sql.ToString());
        }
        //{ display: '单位', name: 'UNIT', width: 100, align: 'left', isSort: false },

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns></returns>
        public TBaseItemAnalysisVo SelectByObject(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = "select * from T_BASE_ITEM_ANALYSIS " + this.BuildWhereStatement(tBaseItemAnalysis);
            return SqlHelper.ExecuteObject(new TBaseItemAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseItemAnalysis, TBaseItemAnalysisVo.T_BASE_ITEM_ANALYSIS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemAnalysis">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemAnalysis, TBaseItemAnalysisVo.T_BASE_ITEM_ANALYSIS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseItemAnalysis.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemAnalysis_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseItemAnalysis_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemAnalysisVo tBaseItemAnalysis_UpdateSet, TBaseItemAnalysisVo tBaseItemAnalysis_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemAnalysis_UpdateSet, TBaseItemAnalysisVo.T_BASE_ITEM_ANALYSIS_TABLE);
            strSQL += this.BuildWhereStatement(tBaseItemAnalysis_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ITEM_ANALYSIS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            string strSQL = "delete from T_BASE_ITEM_ANALYSIS ";
	    strSQL += this.BuildWhereStatement(tBaseItemAnalysis);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseItemAnalysis"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseItemAnalysis)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseItemAnalysis.ID.ToString()));
				}	
				//监测项目ID
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseItemAnalysis.ITEM_ID.ToString()));
				}	
				//分析方法ID
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.ANALYSIS_METHOD_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tBaseItemAnalysis.ANALYSIS_METHOD_ID.ToString()));
				}	
				//监测仪器ID
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.INSTRUMENT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INSTRUMENT_ID = '{0}'", tBaseItemAnalysis.INSTRUMENT_ID.ToString()));
				}	
				//单位
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.UNIT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNIT = '{0}'", tBaseItemAnalysis.UNIT.ToString()));
				}	
				//小数点精度
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.PRECISION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRECISION = '{0}'", tBaseItemAnalysis.PRECISION.ToString()));
				}	
				//检测上限
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.UPPER_LIMIT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UPPER_LIMIT = '{0}'", tBaseItemAnalysis.UPPER_LIMIT.ToString()));
				}	
				//检测下限
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.LOWER_LIMIT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LOWER_LIMIT = '{0}'", tBaseItemAnalysis.LOWER_LIMIT.ToString()));
				}	
				//最低检出限
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.LOWER_CHECKOUT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LOWER_CHECKOUT = '{0}'", tBaseItemAnalysis.LOWER_CHECKOUT.ToString()));
				}	
				//0为不默认，1为默认
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.IS_DEFAULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEFAULT = '{0}'", tBaseItemAnalysis.IS_DEFAULT.ToString()));
				}	
				//0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseItemAnalysis.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseItemAnalysis.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseItemAnalysis.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseItemAnalysis.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseItemAnalysis.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseItemAnalysis.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseItemAnalysis.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseItemAnalysis.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
