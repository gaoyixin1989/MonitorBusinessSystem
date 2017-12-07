using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = "select Count(*) from T_ENV_P_POLLUTE " + this.BuildWhereStatement(tEnvPPollute);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPPolluteVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPollute">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteVo Details(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE " + this.BuildWhereStatement(tEnvPPollute));
            return SqlHelper.ExecuteObject(new TEnvPPolluteVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteVo> SelectByObject(TEnvPPolluteVo tEnvPPollute, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE " + this.BuildWhereStatement(tEnvPPollute));
            return SqlHelper.ExecuteObjectList(tEnvPPollute, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteVo tEnvPPollute, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_POLLUTE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPPollute));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPollute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE " + this.BuildWhereStatement(tEnvPPollute);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <returns></returns>
        public TEnvPPolluteVo SelectByObject(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE " + this.BuildWhereStatement(tEnvPPollute);
            return SqlHelper.ExecuteObject(new TEnvPPolluteVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPPollute, TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPollute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPollute, TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPPollute.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPollute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPollute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteVo tEnvPPollute_UpdateSet, TEnvPPolluteVo tEnvPPollute_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPollute_UpdateSet, TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPPollute_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_POLLUTE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = "delete from T_ENV_P_POLLUTE ";
            strSQL += this.BuildWhereStatement(tEnvPPollute);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool CreateInfo(TEnvPPolluteVo tEnvPPollute, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = tEnvPPollute.SelectMonths.Split(';').ToList();
            tEnvPPollute.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                tEnvPPollute.ID = GetSerialNumber(strSerial);
                tEnvPPollute.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPPollute, TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE);
                list.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        public string GetType(TEnvPPolluteVo tEnvPPollute)
        {
            string strSQL = "SELECT B.TYPE_NAME FROM T_ENV_P_POLLUTE A LEFT JOIN  T_ENV_P_POLLUTE_TYPE B ON A.TYPE_ID=B.ID where A.ID='" + tEnvPPollute .ID+ "'";
            return SqlHelper.ExecuteScalar(strSQL).ToString();
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPPollute"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPPolluteVo tEnvPPollute)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPPollute)
            {
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPPollute.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPPollute.YEAR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPPollute.MONTH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.TYPE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TYPE_ID = '{0}'", tEnvPPollute.TYPE_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPPollute.POINT_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPPollute.POINT_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.WATER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_CODE = '{0}'", tEnvPPollute.WATER_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.WATER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_NAME = '{0}'", tEnvPPollute.WATER_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.SEWERAGE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEWERAGE_NAME = '{0}'", tEnvPPollute.SEWERAGE_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.EQUIPMENT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EQUIPMENT_NAME = '{0}'", tEnvPPollute.EQUIPMENT_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MO_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_NAME = '{0}'", tEnvPPollute.MO_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MO_CAPACITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_CAPACITY = '{0}'", tEnvPPollute.MO_CAPACITY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MO_UOM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_UOM = '{0}'", tEnvPPollute.MO_UOM.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MO_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_DATE = '{0}'", tEnvPPollute.MO_DATE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.FUEL_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_TYPE = '{0}'", tEnvPPollute.FUEL_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.FUEL_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_QTY = '{0}'", tEnvPPollute.FUEL_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.FUEL_MODEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_MODEL = '{0}'", tEnvPPollute.FUEL_MODEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.FUEL_TECH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_TECH = '{0}'", tEnvPPollute.FUEL_TECH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.IS_FUEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_FUEL = '{0}'", tEnvPPollute.IS_FUEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.DISCHARGE_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DISCHARGE_WAY = '{0}'", tEnvPPollute.DISCHARGE_WAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MO_HOUR_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_HOUR_QTY = '{0}'", tEnvPPollute.MO_HOUR_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LOAD_MODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOAD_MODE = '{0}'", tEnvPPollute.LOAD_MODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.POINT_TEMP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_TEMP = '{0}'", tEnvPPollute.POINT_TEMP.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.IS_RUN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_RUN = '{0}'", tEnvPPollute.IS_RUN.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.MEASURED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURED = '{0}'", tEnvPPollute.MEASURED.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.WASTE_AIR_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WASTE_AIR_QTY = '{0}'", tEnvPPollute.WASTE_AIR_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPPollute.LONGITUDE_DEGREE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPPollute.LONGITUDE_MINUTE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPPollute.LONGITUDE_SECOND.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPPollute.LATITUDE_DEGREE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPPollute.LATITUDE_MINUTE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPPollute.LATITUDE_SECOND.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPPollute.IS_DEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPPollute.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPPollute.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPPollute.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPPollute.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPollute.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPPollute.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
