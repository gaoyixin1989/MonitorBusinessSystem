using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.ValueObject;
using System.Linq;
namespace i3.DataAccess.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口监测点表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointEstuariesAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = "select Count(*) from T_ENV_P_ESTUARIES " + this.BuildWhereStatement(tEnvPointEstuaries);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVo Details(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES " + this.BuildWhereStatement(tEnvPointEstuaries));
            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVo> SelectByObject(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES " + this.BuildWhereStatement(tEnvPointEstuaries));
            return SqlHelper.ExecuteObjectList(tEnvPointEstuaries, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_ESTUARIES {0} ";
            if (!string.IsNullOrEmpty(tEnvPointEstuaries.SORT_FIELD))
            {
                strSQL += " order by "+tEnvPointEstuaries.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPointEstuaries.SORT_TYPE))
            {
                strSQL += " " + tEnvPointEstuaries.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointEstuaries));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuaries"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = "select * from T_ENV_P_ESTUARIES " + this.BuildWhereStatement(tEnvPointEstuaries);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVo SelectByObject(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = "select * from T_ENV_P_ESTUARIES " + this.BuildWhereStatement(tEnvPointEstuaries);
            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointEstuaries, TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 创建入海河口（ljn/2013/6/17）
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVo tEnvPointEstuaries, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPointEstuaries.SelectMonths.Split(';').ToList();
            tEnvPointEstuaries.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                tEnvPointEstuaries.ID = GetSerialNumber(strSerial);
                tEnvPointEstuaries.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPointEstuaries, TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE);
                list.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuaries">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuaries, TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointEstuaries.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuaries_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointEstuaries_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVo tEnvPointEstuaries_UpdateSet, TEnvPointEstuariesVo tEnvPointEstuaries_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuaries_UpdateSet, TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointEstuaries_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_ESTUARIES where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = "delete from T_ENV_P_ESTUARIES ";
            strSQL += this.BuildWhereStatement(tEnvPointEstuaries);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {
            string strSQL = " select * from T_ENV_P_ESTUARIES {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tEnvPointEstuaries));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            string strSQL = " select * from T_ENV_P_ESTUARIES {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tEnvPointEstuaries));
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }



        #region// 硫酸盐化速率监测点监测项目表复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// <param name="tEnvPointEstuaries"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointEstuaries)
            {

                //主键
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointEstuaries.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointEstuaries.YEAR.ToString()));
                }
                //测站ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.STATION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATION_ID = '{0}'", tEnvPointEstuaries.STATION_ID.ToString()));
                }
                //河流ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPointEstuaries.RIVER_ID.ToString()));
                }
                //流域ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPointEstuaries.VALLEY_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPointEstuaries.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME = '{0}'", tEnvPointEstuaries.SECTION_NAME.ToString()));
                }
                //省份ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPointEstuaries.PROVINCE_ID.ToString()));
                }
                //所在地区
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID= '{0}'", tEnvPointEstuaries.AREA_ID.ToString()));
                }
                //控制级别
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPointEstuaries.CONTRAL_LEVEL.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointEstuaries.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointEstuaries.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointEstuaries.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointEstuaries.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointEstuaries.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointEstuaries.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointEstuaries.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointEstuaries.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointEstuaries.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointEstuaries.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointEstuaries.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointEstuaries.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointEstuaries.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointEstuaries.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }


        /// <summary>
        /// 根据对象构造条件语句 模糊查询，需要时自己修改 查询条件即可
        /// </summary>
        /// <param name="tEnvPointEstuaries"></param>
        /// <returns></returns>
        public string BuildWhereLikeStatement(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointEstuaries)
            {

                //主键
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointEstuaries.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointEstuaries.YEAR.ToString()));
                }
                //测站ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.STATION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATION_ID = '{0}'", tEnvPointEstuaries.STATION_ID.ToString()));
                }
                //河流ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPointEstuaries.RIVER_ID.ToString()));
                }
                //流域ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPointEstuaries.VALLEY_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPointEstuaries.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME LIKE '%{0}%'", tEnvPointEstuaries.SECTION_NAME.ToString()));
                }
                //省份ID
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPointEstuaries.PROVINCE_ID.ToString()));
                }
                //所在地区
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPointEstuaries.AREA_ID.ToString()));
                }
                //控制级别
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPointEstuaries.CONTRAL_LEVEL.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointEstuaries.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointEstuaries.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointEstuaries.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointEstuaries.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointEstuaries.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointEstuaries.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointEstuaries.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointEstuaries.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointEstuaries.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointEstuaries.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointEstuaries.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointEstuaries.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointEstuaries.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointEstuaries.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointEstuaries.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }
        #endregion
    }

}
