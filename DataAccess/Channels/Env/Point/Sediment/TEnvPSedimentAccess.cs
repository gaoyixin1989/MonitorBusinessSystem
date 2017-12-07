using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sediment;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Sediment
{
    /// <summary>
    /// 功能：底泥重金属
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSedimentAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEDIMENT " + this.BuildWhereStatement(tEnvPSediment);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSedimentVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSedimentVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSediment">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSedimentVo Details(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT " + this.BuildWhereStatement(tEnvPSediment));
            return SqlHelper.ExecuteObject(new TEnvPSedimentVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSedimentVo> SelectByObject(TEnvPSedimentVo tEnvPSediment, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT " + this.BuildWhereStatement(tEnvPSediment));
            return SqlHelper.ExecuteObjectList(tEnvPSediment, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSedimentVo tEnvPSediment, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEDIMENT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSediment));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSediment"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = "select * from T_ENV_P_SEDIMENT " + this.BuildWhereStatement(tEnvPSediment);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <returns></returns>
        public TEnvPSedimentVo SelectByObject(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = "select * from T_ENV_P_SEDIMENT " + this.BuildWhereStatement(tEnvPSediment);
            return SqlHelper.ExecuteObject(new TEnvPSedimentVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSediment, TEnvPSedimentVo.T_ENV_P_SEDIMENT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentVo TEnvPSedimentVo, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPSedimentVo.MONTH.Split(';').ToList();
            TEnvPSedimentVo.MONTH = "";
            foreach (string valueTemp in values)
            {
                TEnvPSedimentVo.ID = GetSerialNumber(strSerial);
                TEnvPSedimentVo.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPSedimentVo, TEnvPSedimentVo.T_ENV_P_SEDIMENT_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSediment">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSediment, TEnvPSedimentVo.T_ENV_P_SEDIMENT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSediment.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSediment_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSediment_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentVo tEnvPSediment_UpdateSet, TEnvPSedimentVo tEnvPSediment_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSediment_UpdateSet, TEnvPSedimentVo.T_ENV_P_SEDIMENT_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSediment_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEDIMENT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSedimentVo tEnvPSediment)
        {
            string strSQL = "delete from T_ENV_P_SEDIMENT ";
            strSQL += this.BuildWhereStatement(tEnvPSediment);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 底泥重金属监测项目的复制逻辑
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

            sql = "select * from " + TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// <param name="tEnvPSediment"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSedimentVo tEnvPSediment)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSediment)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSediment.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSediment.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPSediment.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPSediment.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPSediment.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPSediment.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPSediment.SATAIONS_ID.ToString()));
                }
                //点位代码
                if (!String.IsNullOrEmpty(tEnvPSediment.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPSediment.POINT_CODE.ToString()));
                }
                //点位名称
                if (!String.IsNullOrEmpty(tEnvPSediment.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPSediment.POINT_NAME.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPSediment.CONTRAL_LEVEL.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPSediment.AREA_ID.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPSediment.PROVINCE_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPSediment.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPSediment.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPSediment.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPSediment.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPSediment.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPSediment.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPSediment.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPSediment.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPSediment.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPSediment.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPSediment.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPSediment.LATITUDE_SECOND.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPSediment.RIVER_ID.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPSediment.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPSediment.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPSediment.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPSediment.IS_HANDOVER.ToString()));
                }
                //水源地名称ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.WATER_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_SOURCE_ID = '{0}'", tEnvPSediment.WATER_SOURCE_ID.ToString()));
                }
                //断面性质ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPSediment.SECTION_PORPERTIES_ID.ToString()));
                }
                //监测时段ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSediment.MONITORING_TIME_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITORING_TIME_ID = '{0}'", tEnvPSediment.MONITORING_TIME_ID.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPSediment.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPSediment.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPSediment.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPSediment.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPSediment.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPSediment.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSediment.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSediment.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSediment.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSediment.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSediment.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSediment.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSediment.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSediment.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSediment.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSediment.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,POINT_NAME from " + TEnvPSedimentVo.T_ENV_P_SEDIMENT_TABLE + " a  where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
