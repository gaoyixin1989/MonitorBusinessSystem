using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseFun;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseFunctionAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = "select Count(*) from T_ENV_P_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvPNoiseFunction);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_FUNCTION  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionVo Details(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvPNoiseFunction));
            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseFunctionVo> SelectByObject(TEnvPNoiseFunctionVo tEnvPNoiseFunction, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvPNoiseFunction));
            return SqlHelper.ExecuteObjectList(tEnvPNoiseFunction, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionVo tEnvPNoiseFunction, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_NOISE_FUNCTION {0}   order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPNoiseFunction));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseFunction"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = "select * from T_ENV_P_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvPNoiseFunction);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <returns></returns>
        public TEnvPNoiseFunctionVo SelectByObject(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = "select * from T_ENV_P_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvPNoiseFunction);
            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPNoiseFunction, TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunction">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseFunction, TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPNoiseFunction.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunction_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseFunction_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionVo tEnvPNoiseFunction_UpdateSet, TEnvPNoiseFunctionVo tEnvPNoiseFunction_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseFunction_UpdateSet, TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPNoiseFunction_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_NOISE_FUNCTION where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            string strSQL = "delete from T_ENV_P_NOISE_FUNCTION ";
            strSQL += this.BuildWhereStatement(tEnvPNoiseFunction);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPNoiseFunction"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPNoiseFunction)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPNoiseFunction.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPNoiseFunction.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPNoiseFunction.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPNoiseFunction.SATAIONS_ID.ToString()));
                }
                //功能区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.FUNCTION_AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_AREA_ID = '{0}'", tEnvPNoiseFunction.FUNCTION_AREA_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPNoiseFunction.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPNoiseFunction.POINT_NAME.ToString()));
                }
                //标准昼间
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.STANDARD_LIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD_LIGHT = '{0}'", tEnvPNoiseFunction.STANDARD_LIGHT.ToString()));
                }
                //标准夜间
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.STANDARD_NIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD_NIGHT = '{0}'", tEnvPNoiseFunction.STANDARD_NIGHT.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPNoiseFunction.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPNoiseFunction.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPNoiseFunction.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPNoiseFunction.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPNoiseFunction.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPNoiseFunction.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPNoiseFunction.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPNoiseFunction.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPNoiseFunction.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPNoiseFunction.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPNoiseFunction.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPNoiseFunction.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPNoiseFunction.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPNoiseFunction.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPNoiseFunction.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseFunctionVo TEnvPNoiseFunction, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPNoiseFunction.SelectMonths.Split(';').ToList();
            TEnvPNoiseFunction.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPNoiseFunction.ID = GetSerialNumber(strSerial);
                TEnvPNoiseFunction.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPNoiseFunction, TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }


        /// <summary>
        /// 监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE, row, "", "", strTID, strID);
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

        /// <summary>
        /// 根据年份和月份、功能区获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth, string AreaCode)
        {
            string strSQL = "select ID,POINT_NAME from " + TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE + " where YEAR='" + strYear + "' and MONTH in(" + strMonth + ") and FUNCTION_AREA_ID='" + AreaCode + "' and IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
