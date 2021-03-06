using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Industry;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Industry
{
    /// <summary>
    /// 功能：行业信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseIndustryInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = "select Count(*) from T_BASE_INDUSTRY_INFO " + this.BuildWhereStatement(tBaseIndustryInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseIndustryInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_INDUSTRY_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseIndustryInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseIndustryInfoVo Details(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_INDUSTRY_INFO " + this.BuildWhereStatement(tBaseIndustryInfo));
           return SqlHelper.ExecuteObject(new TBaseIndustryInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseIndustryInfoVo> SelectByObject(TBaseIndustryInfoVo tBaseIndustryInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_INDUSTRY_INFO " + this.BuildWhereStatement(tBaseIndustryInfo));
            return SqlHelper.ExecuteObjectList(tBaseIndustryInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        ///获取指定行业类别的监测项目 胡方扬 2013-03-14
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByObjectForIndustry(TBaseIndustryInfoVo tBaseIndustryInfo,string strMonitorId, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.ID,A.INDUSTRY_ID,A.ITEM_ID,B.INDUSTRY_CODE,B.INDUSTRY_NAME,C.ITEM_NAME,C.ORDER_NUM FROM T_BASE_INDUSTRY_ITEM A
LEFT JOIN dbo.T_BASE_INDUSTRY_INFO B ON A.INDUSTRY_ID=B.ID
LEFT JOIN dbo.T_BASE_ITEM_INFO C ON A.ITEM_ID=C.ID  WHERE 1=1 AND B.ID='{0}'", tBaseIndustryInfo.ID);
            strSQL += String.Format(" AND C.MONITOR_ID='{0}'", strMonitorId); 
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 根据点位，行业类别，确定企业已选的监测项目 胡方扬 2013-03-14
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByObjectForFinishedIndustry(TBaseIndustryInfoVo tBaseIndustryInfo, string strPointId, string strMonitorId, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.ID,A.INDUSTRY_ID,A.ITEM_ID,B.INDUSTRY_CODE,B.INDUSTRY_NAME,C.ITEM_NAME,C.ORDER_NUM,
D.POINT_ID FROM T_BASE_INDUSTRY_ITEM A
LEFT JOIN dbo.T_BASE_INDUSTRY_INFO B ON A.INDUSTRY_ID=B.ID
LEFT JOIN dbo.T_BASE_ITEM_INFO C ON A.ITEM_ID=C.ID
LEFT JOIN dbo.T_BASE_COMPANY_POINT_ITEM D ON D.ITEM_ID=C.ID
LEFT JOIN dbo.T_BASE_COMPANY_POINT E ON E.ID=D.POINT_ID WHERE 1=1 AND B.ID='{0}'", tBaseIndustryInfo.ID);
            strSQL += String.Format("  AND C.MONITOR_ID='{0}' AND E.ID='{1}'  AND E.IS_DEL='0'", strMonitorId, strPointId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseIndustryInfoVo tBaseIndustryInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_INDUSTRY_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseIndustryInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseIndustryInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = "select * from T_BASE_INDUSTRY_INFO " + this.BuildWhereStatement(tBaseIndustryInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <returns></returns>
        public TBaseIndustryInfoVo SelectByObject(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = "select * from T_BASE_INDUSTRY_INFO " + this.BuildWhereStatement(tBaseIndustryInfo);
            return SqlHelper.ExecuteObject(new TBaseIndustryInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseIndustryInfo, TBaseIndustryInfoVo.T_BASE_INDUSTRY_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseIndustryInfo, TBaseIndustryInfoVo.T_BASE_INDUSTRY_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseIndustryInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseIndustryInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryInfoVo tBaseIndustryInfo_UpdateSet, TBaseIndustryInfoVo tBaseIndustryInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseIndustryInfo_UpdateSet, TBaseIndustryInfoVo.T_BASE_INDUSTRY_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseIndustryInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_INDUSTRY_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            string strSQL = "delete from T_BASE_INDUSTRY_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseIndustryInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseIndustryInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseIndustryInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseIndustryInfo.ID.ToString()));
				}	
				//行业代码
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.INDUSTRY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY_CODE = '{0}'", tBaseIndustryInfo.INDUSTRY_CODE.ToString()));
				}	
				//行业名称
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.INDUSTRY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY_NAME = '{0}'", tBaseIndustryInfo.INDUSTRY_NAME.ToString()));
				}
                //默认显示(1,显示）
                if (!String.IsNullOrEmpty(tBaseIndustryInfo.IS_SHOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_SHOW = '{0}'", tBaseIndustryInfo.IS_SHOW.ToString()));
                }	
                //删除标记
                if (!String.IsNullOrEmpty(tBaseIndustryInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseIndustryInfo.IS_DEL.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseIndustryInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseIndustryInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseIndustryInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseIndustryInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseIndustryInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseIndustryInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
