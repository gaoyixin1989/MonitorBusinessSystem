using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkUnder;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.DrinkUnder
{
    /// <summary>
    /// 功能：地下饮用水
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPDrinkUnderAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = "select Count(*) from T_ENV_P_DRINK_UNDER " + this.BuildWhereStatement(tEnvPDrinkUnder);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderVo Details(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER " + this.BuildWhereStatement(tEnvPDrinkUnder));
            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkUnderVo> SelectByObject(TEnvPDrinkUnderVo tEnvPDrinkUnder, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER " + this.BuildWhereStatement(tEnvPDrinkUnder));
            return SqlHelper.ExecuteObjectList(tEnvPDrinkUnder, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkUnderVo tEnvPDrinkUnder, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DRINK_UNDER {0}  order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDrinkUnder));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkUnder"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = "select * from T_ENV_P_DRINK_UNDER " + this.BuildWhereStatement(tEnvPDrinkUnder);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <returns></returns>
        public TEnvPDrinkUnderVo SelectByObject(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = "select * from T_ENV_P_DRINK_UNDER " + this.BuildWhereStatement(tEnvPDrinkUnder);
            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkUnder, TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnder">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkUnder, TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDrinkUnder.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnder_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkUnder_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderVo tEnvPDrinkUnder_UpdateSet, TEnvPDrinkUnderVo tEnvPDrinkUnder_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkUnder_UpdateSet, TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDrinkUnder_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DRINK_UNDER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            string strSQL = "delete from T_ENV_P_DRINK_UNDER ";
            strSQL += this.BuildWhereStatement(tEnvPDrinkUnder);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDrinkUnder"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDrinkUnder)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDrinkUnder.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPDrinkUnder.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPDrinkUnder.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPDrinkUnder.SATAIONS_ID.ToString()));
                }
                //点位代码
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPDrinkUnder.POINT_CODE.ToString()));
                }
                //点位名称
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPDrinkUnder.POINT_NAME.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPDrinkUnder.CONTRAL_LEVEL.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPDrinkUnder.AREA_ID.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPDrinkUnder.PROVINCE_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPDrinkUnder.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPDrinkUnder.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPDrinkUnder.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPDrinkUnder.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPDrinkUnder.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPDrinkUnder.LATITUDE_SECOND.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPDrinkUnder.RIVER_ID.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPDrinkUnder.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPDrinkUnder.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPDrinkUnder.IS_HANDOVER.ToString()));
                }
                //水源地名称ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.WATER_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_SOURCE_ID = '{0}'", tEnvPDrinkUnder.WATER_SOURCE_ID.ToString()));
                }
                //断面性质ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPDrinkUnder.SECTION_PORPERTIES_ID.ToString()));
                }
                //监测时段ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.MONITORING_TIME_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITORING_TIME_ID = '{0}'", tEnvPDrinkUnder.MONITORING_TIME_ID.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPDrinkUnder.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPDrinkUnder.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPDrinkUnder.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDrinkUnder.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDrinkUnder.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDrinkUnder.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDrinkUnder.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDrinkUnder.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDrinkUnder.REMARK5.ToString()));
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
        public bool Create(TEnvPDrinkUnderVo TEnvPDrinkUnder, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPDrinkUnder.SelectMonths.Split(';').ToList();
            TEnvPDrinkUnder.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPDrinkUnder.ID = GetSerialNumber(strSerial);
                TEnvPDrinkUnder.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPDrinkUnder, TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        /// <summary>
        /// 河流垂线监测项目的复制逻辑
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

            sql = "select * from " + TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,POINT_NAME from "+TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE+" a  where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
