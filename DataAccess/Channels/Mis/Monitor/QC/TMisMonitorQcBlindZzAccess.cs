using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：标准盲样
    /// 创建日期：2013-07-02
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcBlindZzAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_BLIND_ZZ " + this.BuildWhereStatement(tMisMonitorQcBlindZz);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcBlindZzVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_BLIND_ZZ  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcBlindZzVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcBlindZzVo Details(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_BLIND_ZZ " + this.BuildWhereStatement(tMisMonitorQcBlindZz));
           return SqlHelper.ExecuteObject(new TMisMonitorQcBlindZzVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcBlindZzVo> SelectByObject(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_BLIND_ZZ " + this.BuildWhereStatement(tMisMonitorQcBlindZz));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcBlindZz, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_QC_BLIND_ZZ {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcBlindZz));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_BLIND_ZZ " + this.BuildWhereStatement(tMisMonitorQcBlindZz);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <returns></returns>
        public TMisMonitorQcBlindZzVo SelectByObject(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_BLIND_ZZ " + this.BuildWhereStatement(tMisMonitorQcBlindZz);
            return SqlHelper.ExecuteObject(new TMisMonitorQcBlindZzVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcBlindZz, TMisMonitorQcBlindZzVo.T_MIS_MONITOR_QC_BLIND_ZZ_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcBlindZz, TMisMonitorQcBlindZzVo.T_MIS_MONITOR_QC_BLIND_ZZ_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcBlindZz.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcBlindZz_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz_UpdateSet, TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcBlindZz_UpdateSet, TMisMonitorQcBlindZzVo.T_MIS_MONITOR_QC_BLIND_ZZ_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcBlindZz_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_BLIND_ZZ where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_BLIND_ZZ ";
	    strSQL += this.BuildWhereStatement(tMisMonitorQcBlindZz);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcBlindZz)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcBlindZz.ID.ToString()));
				}	
				//平行样分析结果 ID
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.RESULT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisMonitorQcBlindZz.RESULT_ID.ToString()));
				}	
				//标准值
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.STANDARD_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_VALUE = '{0}'", tMisMonitorQcBlindZz.STANDARD_VALUE.ToString()));
				}	
				//不确定度
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.UNCETAINTY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNCETAINTY = '{0}'", tMisMonitorQcBlindZz.UNCETAINTY.ToString()));
				}	
				//测定值
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.BLIND_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BLIND_VALUE = '{0}'", tMisMonitorQcBlindZz.BLIND_VALUE.ToString()));
				}	
				//偏移量（%）
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.OFFSET.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OFFSET = '{0}'", tMisMonitorQcBlindZz.OFFSET.ToString()));
				}	
				//是否合格
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.BLIND_ISOK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BLIND_ISOK = '{0}'", tMisMonitorQcBlindZz.BLIND_ISOK.ToString()));
				}	
				//质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标 11、标准盲样）
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.QC_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorQcBlindZz.QC_TYPE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcBlindZz.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcBlindZz.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcBlindZz.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcBlindZz.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorQcBlindZz.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcBlindZz.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
