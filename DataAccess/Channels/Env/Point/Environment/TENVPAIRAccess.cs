using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Environment
{
    /// <summary>
    /// 功能：环境空气监测点信息表
    /// 创建日期：2013-06-08
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvPAirAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAirVo tEnvPAir)
        {
            string strSQL = "select Count(*) from T_ENV_P_AIR " + this.BuildWhereStatement(tEnvPAir);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAirVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPAirVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAirVo Details(TEnvPAirVo tEnvPAir)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR " + this.BuildWhereStatement(tEnvPAir));
            return SqlHelper.ExecuteObject(new TEnvPAirVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAirVo> SelectByObject(TEnvPAirVo tEnvPAir, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_AIR " + this.BuildWhereStatement(tEnvPAir));
            return SqlHelper.ExecuteObjectList(tEnvPAir, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAirVo tEnvPAir, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_AIR {0} order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPAir));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAirVo tEnvPAir)
        {
            string strSQL = "select * from T_ENV_P_AIR " + this.BuildWhereStatement(tEnvPAir);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns></returns>
        public TEnvPAirVo SelectByObject(TEnvPAirVo tEnvPAir)
        {
            string strSQL = "select * from T_ENV_P_AIR " + this.BuildWhereStatement(tEnvPAir);
            return SqlHelper.ExecuteObject(new TEnvPAirVo(), strSQL);
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAirVo tEnvPAir, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPAir.SelectMonths.Split(';').ToList();
            tEnvPAir.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPAir.ID = GetSerialNumber(Number);
                tEnvPAir.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAirVo tEnvPAir)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPAir.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAirVo tEnvPAir_UpdateSet, TEnvPAirVo tEnvPAir_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir_UpdateSet, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPAir_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_AIR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPAirVo tEnvPAir)
        {
            string strSQL = "delete from T_ENV_P_AIR ";
            strSQL += this.BuildWhereStatement(tEnvPAir);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }



        #endregion

        

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
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPAir"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPAirVo tEnvPAir)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPAir)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPAir.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPAir.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPAir.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPAir.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPAir.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPAir.MONTH.ToString()));
                }
                //测站
                if (!string.IsNullOrEmpty(tEnvPAir.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID={0}", tEnvPAir.SATAIONS_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPAir.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPAir.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPAir.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPAir.POINT_NAME.ToString()));
                }
                //行政区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPAir.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPAir.AREA_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPAir.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPAir.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPAir.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPAir.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPAir.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPAir.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPAir.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPAir.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPAir.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPAir.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPAir.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPAir.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPAir.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPAir.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPAir.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPAir.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPAir.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPAir.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPAir.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPAir.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPAir.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPAir.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPAir.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPAir.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPAir.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPAir.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPAir.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPAir.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
