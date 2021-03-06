using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Method;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Method
{
    /// <summary>
    /// 功能：方法依据管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMethodInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = "select Count(*) from T_BASE_METHOD_INFO " + this.BuildWhereStatement(tBaseMethodInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMethodInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_METHOD_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseMethodInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMethodInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMethodInfoVo Details(TBaseMethodInfoVo tBaseMethodInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_METHOD_INFO " + this.BuildWhereStatement(tBaseMethodInfo));
           return SqlHelper.ExecuteObject(new TBaseMethodInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMethodInfoVo> SelectByObject(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_METHOD_INFO " + this.BuildWhereStatement(tBaseMethodInfo));
            return SqlHelper.ExecuteObjectList(tBaseMethodInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_METHOD_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseMethodInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMethodInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = "select * from T_BASE_METHOD_INFO " + this.BuildWhereStatement(tBaseMethodInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns></returns>
        public TBaseMethodInfoVo SelectByObject(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = "select * from T_BASE_METHOD_INFO " + this.BuildWhereStatement(tBaseMethodInfo);
            return SqlHelper.ExecuteObject(new TBaseMethodInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseMethodInfo, TBaseMethodInfoVo.T_BASE_METHOD_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMethodInfo, TBaseMethodInfoVo.T_BASE_METHOD_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseMethodInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseMethodInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodInfoVo tBaseMethodInfo_UpdateSet, TBaseMethodInfoVo tBaseMethodInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMethodInfo_UpdateSet, TBaseMethodInfoVo.T_BASE_METHOD_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseMethodInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_METHOD_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = "delete from T_BASE_METHOD_INFO ";
            strSQL += this.BuildWhereStatement(tBaseMethodInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            string strSQL = " select * from T_BASE_METHOD_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tBaseMethodInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseMethodInfoVo tBaseMethodInfo)
        {
            string strSQL = " select * from T_BASE_METHOD_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tBaseMethodInfo));
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        ///创建原因： 复制分析方法依据和分析方法
        /// 创建人：胡方扬
        /// 创建日期：2013-08-13  
        /// </summary>
        /// <param name="strSourceTypeId"></param>
        /// <param name="strToTypeId"></param>
        /// <returns></returns>
        public bool CopyInfor(string strSourceTypeId, string strToTypeId)
        {
            ArrayList arrVo = new ArrayList();
            string strSQL = @" SELECT ID,METHOD_CODE,METHOD_NAME,IS_DEL FROM T_BASE_METHOD_INFO WHERE MONITOR_ID='{0}' ";
            strSQL = String.Format(strSQL, strSourceTypeId);
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataTable(strSQL);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strMethodId = GetSerialNumber("method_id");
                    strSQL = @"INSERT INTO T_BASE_METHOD_INFO(ID,METHOD_CODE,METHOD_NAME,MONITOR_ID,IS_DEL) VALUES('{0}','{1}','{2}','{3}','0') ";
                    strSQL = String.Format(strSQL, strMethodId, dr["METHOD_CODE"].ToString(), dr["METHOD_NAME"].ToString(), strToTypeId);
                    arrVo.Add(strSQL);
                    strSQL = @" SELECT ANALYSIS_NAME,IS_DEL FROM T_BASE_METHOD_ANALYSIS WHERE METHOD_ID='{0}'";
                    strSQL = String.Format(strSQL, dr["ID"].ToString());
                    DataTable objDtNew = SqlHelper.ExecuteDataTable(strSQL);
                    if (objDtNew.Rows.Count > 0)
                    {
                        foreach (DataRow drr in objDtNew.Rows)
                        {
                            strSQL = @" INSERT INTO T_BASE_METHOD_ANALYSIS(ID,ANALYSIS_NAME,METHOD_ID,IS_DEL) VALUES('{0}','{1}','{2}','0')";
                            strSQL = String.Format(strSQL, GetSerialNumber("Analysis_Id"), drr["ANALYSIS_NAME"].ToString(), strMethodId);
                            arrVo.Add(strSQL);
                        }
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseMethodInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseMethodInfoVo tBaseMethodInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseMethodInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseMethodInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseMethodInfo.ID.ToString()));
				}	
				//方法依据编号
				if (!String.IsNullOrEmpty(tBaseMethodInfo.METHOD_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND METHOD_CODE = '{0}'", tBaseMethodInfo.METHOD_CODE.ToString()));
				}	
				//方法依据名称
				if (!String.IsNullOrEmpty(tBaseMethodInfo.METHOD_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND METHOD_NAME = '{0}'", tBaseMethodInfo.METHOD_NAME.ToString()));
				}	
				//监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
				if (!String.IsNullOrEmpty(tBaseMethodInfo.MONITOR_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseMethodInfo.MONITOR_ID.ToString()));
				}	
				//方法依据描述
				if (!String.IsNullOrEmpty(tBaseMethodInfo.DESCRIPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseMethodInfo.DESCRIPTION.ToString()));
				}	
				//0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseMethodInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseMethodInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseMethodInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseMethodInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseMethodInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseMethodInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseMethodInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        /// <summary>
        /// 根据对象构造条件语句 模糊查询，需要时自己修改 查询条件即可
        /// </summary>
        /// <param name="tBaseMethodInfo"></param>
        /// <returns></returns>
        public string BuildWhereLikeStatement(TBaseMethodInfoVo tBaseMethodInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseMethodInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseMethodInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseMethodInfo.ID.ToString()));
                }
                //方法依据编号
                if (!String.IsNullOrEmpty(tBaseMethodInfo.METHOD_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND METHOD_CODE LIKE '%{0}%'", tBaseMethodInfo.METHOD_CODE.ToString()));
                }
                //方法依据名称
                if (!String.IsNullOrEmpty(tBaseMethodInfo.METHOD_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND METHOD_NAME LIKE '%{0}%'", tBaseMethodInfo.METHOD_NAME.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tBaseMethodInfo.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseMethodInfo.MONITOR_ID.ToString()));
                }
                //方法依据描述
                if (!String.IsNullOrEmpty(tBaseMethodInfo.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseMethodInfo.DESCRIPTION.ToString()));
                }
                //0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseMethodInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseMethodInfo.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseMethodInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseMethodInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseMethodInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseMethodInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseMethodInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseMethodInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
