using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Point.Sea;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Sea
{
    /// <summary>
    /// 功能：近海海域监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointSeaAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEA " + this.BuildWhereStatement(tEnvPointSea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointSeaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointSeaVo Details(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEA " + this.BuildWhereStatement(tEnvPointSea));
           return SqlHelper.ExecuteObject(new TEnvPointSeaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointSeaVo> SelectByObject(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEA " + this.BuildWhereStatement(tEnvPointSea));
            return SqlHelper.ExecuteObjectList(tEnvPointSea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEA {0} ";
            if (!string.IsNullOrEmpty(tEnvPointSea.SORT_FIELD))
            {
                strSQL += " order by " + tEnvPointSea.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPointSea.SORT_TYPE))
            {
                strSQL += " " + tEnvPointSea.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointSea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = "select * from T_ENV_P_SEA " + this.BuildWhereStatement(tEnvPointSea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns></returns>
        public TEnvPointSeaVo SelectByObject(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = "select * from T_ENV_P_SEA " + this.BuildWhereStatement(tEnvPointSea);
            return SqlHelper.ExecuteObject(new TEnvPointSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointSea, TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaVo tEnvPointSea, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPointSea.SelectMonths.Split(';').ToList();
            tEnvPointSea.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPointSea.ID = GetSerialNumber(Number);
                tEnvPointSea.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPointSea, TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointSea, TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointSea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSea_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaVo tEnvPointSea_UpdateSet, TEnvPointSeaVo tEnvPointSea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointSea_UpdateSet, TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointSea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointSeaVo tEnvPointSea)
        {
            string strSQL = "delete from T_ENV_P_SEA ";
	    strSQL += this.BuildWhereStatement(tEnvPointSea);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_ENV_P_SEA WHERE IS_DEL='{0}'", tEnvPointSea.IS_DEL);
            if (!String.IsNullOrEmpty(tEnvPointSea.YEAR))
            {
                strSQL += String.Format("  AND YEAR LIKE '%{0}%'", tEnvPointSea.YEAR);
            }
            if (!String.IsNullOrEmpty(tEnvPointSea.POINT_NAME))
            {
                strSQL += String.Format("  AND POINT_NAME LIKE '%{0}%'", tEnvPointSea.POINT_NAME);
            }

            if (!String.IsNullOrEmpty(tEnvPointSea.FUNCTION_CODE))
            {
                strSQL += String.Format("  AND FUNCTION_CODE LIKE '%{0}%'", tEnvPointSea.FUNCTION_CODE);
            }

            if (!String.IsNullOrEmpty(tEnvPointSea.POINT_TYPE))
            {
                strSQL += String.Format("   AND POINT_TYPE = '{0}'", tEnvPointSea.POINT_TYPE);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointSeaVo tEnvPointSea)
        {

            string strSQL = String.Format("SELECT * FROM T_ENV_P_SEA WHERE IS_DEL='{0}'", tEnvPointSea.IS_DEL);
            if (!String.IsNullOrEmpty(tEnvPointSea.YEAR))
            {
                strSQL += String.Format("  AND YEAR LIKE '%{0}%'", tEnvPointSea.YEAR);
            }
            if (!String.IsNullOrEmpty(tEnvPointSea.POINT_NAME))
            {
                strSQL += String.Format("  AND POINT_NAME LIKE '%{0}%'", tEnvPointSea.POINT_NAME);
            }

            if (!String.IsNullOrEmpty(tEnvPointSea.FUNCTION_CODE))
            {
                strSQL += String.Format("  AND FUNCTION_CODE LIKE '%{0}%'", tEnvPointSea.FUNCTION_CODE);
            }

            if (!String.IsNullOrEmpty(tEnvPointSea.POINT_TYPE))
            {
                strSQL += String.Format("   AND POINT_TYPE = '{0}'", tEnvPointSea.POINT_TYPE);
            }


            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }


        #region//监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE, row, "", "", strTID, strID);
                    list.Add(sql);
                }
                if (SqlHelper.ExecuteSQLByTransaction(list))
                {
                    b = true;
                }
                else
                {
                    b = false;
                    Msg = "数据库更新失败";
                }
            }

            if (b)
                return "({result:true,msg:''})";
            else
                return "({result:false,msg:'" + Msg + "'})";
        }
        #endregion


        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointSea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointSeaVo tEnvPointSea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointSea)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointSea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointSea.ID.ToString()));
                }
                //年
                if (!String.IsNullOrEmpty(tEnvPointSea.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointSea.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPointSea.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPointSea.MONTH.ToString()));
                }	
                //监测点编码
                if (!String.IsNullOrEmpty(tEnvPointSea.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPointSea.POINT_CODE.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tEnvPointSea.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPointSea.POINT_NAME.ToString()));
                }
                //功能区代码
                if (!String.IsNullOrEmpty(tEnvPointSea.FUNCTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_CODE = '{0}'", tEnvPointSea.FUNCTION_CODE.ToString()));
                }
                //国家编号
                if (!String.IsNullOrEmpty(tEnvPointSea.COUNTRY_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COUNTRY_CODE = '{0}'", tEnvPointSea.COUNTRY_CODE.ToString()));
                }
                //省份编号
                if (!String.IsNullOrEmpty(tEnvPointSea.PROVINCE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_CODE = '{0}'", tEnvPointSea.PROVINCE_CODE.ToString()));
                }
                //点位类别
                if (!String.IsNullOrEmpty(tEnvPointSea.POINT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_TYPE = '{0}'", tEnvPointSea.POINT_TYPE.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointSea.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointSea.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointSea.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointSea.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointSea.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointSea.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointSea.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointSea.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointSea.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointSea.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointSea.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointSea.LATITUDE_SECOND.ToString()));
                }
                //具体位置
                if (!String.IsNullOrEmpty(tEnvPointSea.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPointSea.LOCATION.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointSea.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointSea.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointSea.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointSea.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointSea.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointSea.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointSea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointSea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointSea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointSea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointSea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointSea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointSea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointSea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointSea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointSea.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
