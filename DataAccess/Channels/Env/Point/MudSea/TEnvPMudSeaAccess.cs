using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudSea;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudSeaAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = "select Count(*) from T_ENV_P_MUD_SEA " + this.BuildWhereStatement(tEnvPMudSea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVo Details(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA " + this.BuildWhereStatement(tEnvPMudSea));
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudSeaVo> SelectByObject(TEnvPMudSeaVo tEnvPMudSea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA " + this.BuildWhereStatement(tEnvPMudSea));
            return SqlHelper.ExecuteObjectList(tEnvPMudSea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudSeaVo tEnvPMudSea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_MUD_SEA {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPMudSea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA " + this.BuildWhereStatement(tEnvPMudSea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudSea">对象</param>
        /// <returns></returns>
        public TEnvPMudSeaVo SelectByObject(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA " + this.BuildWhereStatement(tEnvPMudSea);
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPMudSea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPMudSea, TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSea, TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPMudSea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVo tEnvPMudSea_UpdateSet, TEnvPMudSeaVo tEnvPMudSea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSea_UpdateSet, TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPMudSea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_MUD_SEA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPMudSeaVo tEnvPMudSea)
        {
            string strSQL = "delete from T_ENV_P_MUD_SEA ";
            strSQL += this.BuildWhereStatement(tEnvPMudSea);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPMudSea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPMudSeaVo tEnvPMudSea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPMudSea)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPMudSea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPMudSea.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPMudSea.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPMudSea.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPMudSea.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPMudSea.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPMudSea.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPMudSea.SATAIONS_ID.ToString()));
                }
                //断面代码
                if (!String.IsNullOrEmpty(tEnvPMudSea.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPMudSea.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPMudSea.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME = '{0}'", tEnvPMudSea.SECTION_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPMudSea.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPMudSea.AREA_ID.ToString()));
                }
                //所属省份ID
                if (!String.IsNullOrEmpty(tEnvPMudSea.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPMudSea.PROVINCE_ID.ToString()));
                }
                //控制级别ID
                if (!String.IsNullOrEmpty(tEnvPMudSea.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPMudSea.CONTRAL_LEVEL.ToString()));
                }
                //河流ID
                if (!String.IsNullOrEmpty(tEnvPMudSea.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPMudSea.RIVER_ID.ToString()));
                }
                //流域ID
                if (!String.IsNullOrEmpty(tEnvPMudSea.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPMudSea.VALLEY_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPMudSea.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPMudSea.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPMudSea.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPMudSea.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPMudSea.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPMudSea.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPMudSea.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPMudSea.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPMudSea.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPMudSea.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPMudSea.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPMudSea.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPMudSea.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPMudSea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPMudSea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPMudSea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPMudSea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPMudSea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPMudSea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPMudSea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPMudSea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPMudSea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPMudSea.REMARK5.ToString()));
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
        public bool Create(TEnvPMudSeaVo TEnvPMudSea, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPMudSea.SelectMonths.Split(';').ToList();
            TEnvPMudSea.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPMudSea.ID = GetSerialNumber(strSerial);
                TEnvPMudSea.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPMudSea, TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        /// <summary>
        /// 沉积物（海水）垂线监测项目的复制逻辑
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

            sql = "select * from " + TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE, row, "", "", strTID, strID);
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
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from " + TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE + " a inner join " + TEnvPMudSeaVVo.T_ENV_P_MUD_SEA_V_TABLE + " b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
