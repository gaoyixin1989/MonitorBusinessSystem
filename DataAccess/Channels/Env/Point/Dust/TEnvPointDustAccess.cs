using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Point.Dust;
using i3.ValueObject;
using System.Data.SqlClient;

namespace i3.DataAccess.Channels.Env.Point.Dust
{
    /// <summary>
    /// 功能：降尘监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointDustAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = "select Count(*) from T_ENV_P_DUST " + this.BuildWhereStatement(tEnvPointDust);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointDustVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DUST  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointDustVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointDust">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointDustVo Details(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DUST " + this.BuildWhereStatement(tEnvPointDust));
           return SqlHelper.ExecuteObject(new TEnvPointDustVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointDustVo> SelectByObject(TEnvPointDustVo tEnvPointDust, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DUST " + this.BuildWhereStatement(tEnvPointDust));
            return SqlHelper.ExecuteObjectList(tEnvPointDust, BuildPagerExpress(strSQL, iIndex, iCount));

        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointDust"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = "select * from T_ENV_P_DUST " + this.BuildWhereStatement(tEnvPointDust);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <returns></returns>
        public TEnvPointDustVo SelectByObject(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = "select * from T_ENV_P_DUST " + this.BuildWhereStatement(tEnvPointDust);
            return SqlHelper.ExecuteObject(new TEnvPointDustVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointDust, TEnvPointDustVo.T_ENV_POINT_DUST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointDustVo tEnvPointDust, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPointDust.SelectMonths.Split(';').ToList();
            tEnvPointDust.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPointDust.ID = GetSerialNumber(Number);
                tEnvPointDust.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPointDust, TEnvPointDustVo.T_ENV_POINT_DUST_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointDust">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointDust, TEnvPointDustVo.T_ENV_POINT_DUST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointDust.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointDust_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointDust_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointDustVo tEnvPointDust_UpdateSet, TEnvPointDustVo tEnvPointDust_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointDust_UpdateSet, TEnvPointDustVo.T_ENV_POINT_DUST_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointDust_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DUST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointDustVo tEnvPointDust)
        {
            string strSQL = "delete from T_ENV_P_DUST  order by YEAR desc,len(MONTH) desc,MONTH desc ";
	    strSQL += this.BuildWhereStatement(tEnvPointDust);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointDustVo tEnvPointDust, int iIndex, int iCount)
        {
            string strSQL = " select * from T_ENV_P_DUST {0} ";
            if (!string.IsNullOrEmpty(tEnvPointDust.SORT_FIELD))
            {
                strSQL += " order by " + tEnvPointDust.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPointDust.SORT_TYPE))
            {
                strSQL += " " + tEnvPointDust.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointDust));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        //private void selects()
        //{
        //    string strSQL = "SelectInfo";
        //    objConnection = new SqlConnection(strConnection);
        //    objCommand = new SqlCommand(strSQL, objConnection);
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.Parameters.Add("ID", SqlDbType.VarChar, 64);
        //    objCommand.Parameters.Add("serial_out", SqlDbType.VarChar, 128);
        //    objCommand.Parameters["ID"].Direction = ParameterDirection.Input;
        //    objCommand.Parameters["serial_out"].Direction = ParameterDirection.Output;
        //    objCommand.Parameters["ID"].Value = "001000437";
        //    try
        //    {
        //        objConnection.Open();
        //        objCommand.ExecuteNonQuery();
        //        DataTable dt  = objCommand.Parameters["serial_out"].Value.ToString();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        objConnection.Close();
        //        objCommand.Dispose();
        //    }
        //}

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointDust"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointDustVo tEnvPointDust)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointDust)
            {
			    	
				//主键ID
				if (!String.IsNullOrEmpty(tEnvPointDust.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointDust.ID.ToString()));
				}	
				//年度
				if (!String.IsNullOrEmpty(tEnvPointDust.YEAR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointDust.YEAR.ToString()));
				}
                //月度
                if (!String.IsNullOrEmpty(tEnvPointDust.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPointDust.MONTH.ToString()));
                }
				//测站ID（字典项）
				if (!String.IsNullOrEmpty(tEnvPointDust.SATAIONS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPointDust.SATAIONS_ID.ToString()));
				}	
				//测点编号
				if (!String.IsNullOrEmpty(tEnvPointDust.POINT_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPointDust.POINT_CODE.ToString()));
				}	
				//测点名称
				if (!String.IsNullOrEmpty(tEnvPointDust.POINT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPointDust.POINT_NAME.ToString()));
				}	
				//行政区ID（字典项）
				if (!String.IsNullOrEmpty(tEnvPointDust.AREA_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPointDust.AREA_ID.ToString()));
				}	
				//控制级别ID（字典项）
				if (!String.IsNullOrEmpty(tEnvPointDust.CONTRAL_LEVEL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL_ID = '{0}'", tEnvPointDust.CONTRAL_LEVEL_ID.ToString()));
				}	
				//经度（度）
				if (!String.IsNullOrEmpty(tEnvPointDust.LONGITUDE_DEGREE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointDust.LONGITUDE_DEGREE.ToString()));
				}	
				//经度（分）
				if (!String.IsNullOrEmpty(tEnvPointDust.LONGITUDE_MINUTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointDust.LONGITUDE_MINUTE.ToString()));
				}	
				//经度（秒）
				if (!String.IsNullOrEmpty(tEnvPointDust.LONGITUDE_SECOND.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointDust.LONGITUDE_SECOND.ToString()));
				}	
				//纬度（度）
				if (!String.IsNullOrEmpty(tEnvPointDust.LATITUDE_DEGREE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointDust.LATITUDE_DEGREE.ToString()));
				}	
				//纬度（分）
				if (!String.IsNullOrEmpty(tEnvPointDust.LATITUDE_MINUTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointDust.LATITUDE_MINUTE.ToString()));
				}	
				//纬度（秒）
				if (!String.IsNullOrEmpty(tEnvPointDust.LATITUDE_SECOND.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointDust.LATITUDE_SECOND.ToString()));
				}	
				//具体位置
				if (!String.IsNullOrEmpty(tEnvPointDust.LOCATION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPointDust.LOCATION.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointDust.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointDust.IS_DEL.ToString()));
				}	
				//序号
				if (!String.IsNullOrEmpty(tEnvPointDust.NUM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointDust.NUM.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tEnvPointDust.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointDust.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tEnvPointDust.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointDust.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tEnvPointDust.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointDust.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tEnvPointDust.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointDust.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tEnvPointDust.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointDust.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
