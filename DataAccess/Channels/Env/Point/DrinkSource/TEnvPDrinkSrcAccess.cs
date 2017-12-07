using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using System.Data;
using System.Collections;
using System.Reflection;

namespace i3.DataAccess.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = "select Count(*) from T_ENV_P_DRINK_SRC " + this.BuildWhereStatement(tEnvPDrinkSrc);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVo(), strSQL);
        }

        

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVo Details(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC " + this.BuildWhereStatement(tEnvPDrinkSrc));
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVo> SelectByObject(TEnvPDrinkSrcVo tEnvPDrinkSrc, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC " + this.BuildWhereStatement(tEnvPDrinkSrc));
            return SqlHelper.ExecuteObjectList(tEnvPDrinkSrc, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVo tEnvPDrinkSrc, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DRINK_SRC {0}  order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDrinkSrc));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC " + this.BuildWhereStatement(tEnvPDrinkSrc);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVo SelectByObject(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC " + this.BuildWhereStatement(tEnvPDrinkSrc);
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkSrc, TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrc, TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDrinkSrc.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVo tEnvPDrinkSrc_UpdateSet, TEnvPDrinkSrcVo tEnvPDrinkSrc_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrc_UpdateSet, TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrc_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DRINK_SRC where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            string strSQL = "delete from T_ENV_P_DRINK_SRC ";
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrc);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDrinkSrc"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDrinkSrc)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDrinkSrc.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPDrinkSrc.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPDrinkSrc.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPDrinkSrc.SATAIONS_ID.ToString()));
                }
                //断面代码
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPDrinkSrc.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME = '{0}'", tEnvPDrinkSrc.SECTION_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPDrinkSrc.AREA_ID.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPDrinkSrc.RIVER_ID.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPDrinkSrc.CONTRAL_LEVEL.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPDrinkSrc.PROVINCE_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPDrinkSrc.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPDrinkSrc.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPDrinkSrc.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPDrinkSrc.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPDrinkSrc.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPDrinkSrc.LATITUDE_SECOND.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPDrinkSrc.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPDrinkSrc.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPDrinkSrc.IS_HANDOVER.ToString()));
                }
                //水源地名称ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.WATER_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_SOURCE_ID = '{0}'", tEnvPDrinkSrc.WATER_SOURCE_ID.ToString()));
                }
                //水期
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.WATER_PERIOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_PERIOD = '{0}'", tEnvPDrinkSrc.WATER_PERIOD.ToString()));
                }
                //所属水系
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.RIVER_SYSTEM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_SYSTEM = '{0}'", tEnvPDrinkSrc.RIVER_SYSTEM.ToString()));
                }
                //水源地性质
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.WATER_PROPERTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_PROPERTY = '{0}'", tEnvPDrinkSrc.WATER_PROPERTY.ToString()));
                }
                //断面性质ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPDrinkSrc.SECTION_PORPERTIES_ID.ToString()));
                }
                //监测时段ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.MONITORING_TIME_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITORING_TIME_ID = '{0}'", tEnvPDrinkSrc.MONITORING_TIME_ID.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPDrinkSrc.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPDrinkSrc.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPDrinkSrc.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDrinkSrc.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDrinkSrc.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDrinkSrc.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDrinkSrc.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDrinkSrc.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDrinkSrc.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="strID">监测项目ID</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.ID,a.ITEM_NAME,b.ANALYSIS_METHOD_ID,d.METHOD_NAME METHOD,c.ANALYSIS_NAME ANALYSIS_METHOD,b.INSTRUMENT_ID,e.Name INSTRUMENT,f.DICT_TEXT UNIT,b.LOWER_CHECKOUT LOWER_CHECKOUT ");
            sql.Append("from T_BASE_ITEM_INFO a left join T_BASE_ITEM_ANALYSIS b on(a.ID=b.ITEM_ID) ");
            sql.Append("left join T_BASE_METHOD_ANALYSIS c on(b.ANALYSIS_METHOD_ID=c.ID) ");
            sql.Append("left join T_BASE_METHOD_INFO d on(c.METHOD_ID=d.ID) ");
            sql.Append("left join T_BASE_APPARATUS_INFO e on(b.INSTRUMENT_ID=e.ID) ");
            sql.Append("left join T_SYS_DICT f on(b.UNIT=f.ID and f.DICT_TYPE='item_unit') ");
            sql.Append("where a.ID='" + strID + "'");

            return SqlHelper.ExecuteDataTable(sql.ToString());
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVo tEnvPDrinkSrc, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = tEnvPDrinkSrc.SelectMonths.Split(';').ToList();
            tEnvPDrinkSrc.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                tEnvPDrinkSrc.ID = GetSerialNumber(strSerial);
                tEnvPDrinkSrc.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkSrc, TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }


        /// <summary>
        /// 饮用水源地垂线监测项目的复制逻辑
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

            sql = "select * from " + TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE, row, "", "", strTID, strID);
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
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from " + TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE + " a inner join " + TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE + " b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable_Sur(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from " + TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE + " a inner join " + TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE + " b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0' and NUM='Surface' ";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #region//获取环境质量数据填报数据
        /// <summary>
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string Columns = "";
            string PointIDs = "";
            bool b = true;
            b = UpdateFillDate(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
            #region //根据点位表信息更新填报表
            if (b)
            {
                //StringBuilder sb = new StringBuilder(256);
                //sb.Append("select a.ID,a.YEAR T_ENV_FILL_DRINK_SRC@YEAR@年份, b.SATAIONS_ID as T_T_ENV_FILL_DRINK_SRC@SATAIONS_ID@测站编码,c.dict_text as T_ENV_FILL_DRINK_SRC@dict_text@测站名称, ");
                //sb.Append("  a.SECTION_ID T_ENV_FILL_DRINK_SRC@SECTION_ID@断面名称,b.SECTION_CODE T_ENV_FILL_DRINK_SRC@SECTION_CODE@断面代码, ");
                //sb.Append("   a.POINT_ID T_ENV_FILL_DRINK_SRC@POINT_ID@垂线名称, a.MONTH T_ENV_FILL_DRINK_SRC@MONTH@月份,");
                //sb.Append("   a.DAY T_ENV_FILL_DRINK_SRC@DAY@日期,  a.KPF T_ENV_FILL_DRINK_SRC@KPF@水期代码  ");
                //sb.Append(" from T_ENV_FILL_DRINK_SRC a left join T_ENV_P_DRINK_SRC b on a.section_id=b.id  Left join T_SYS_DICT c on b.SATAIONS_ID=c.dict_code ");
                //sb.Append("  where  a.POINT_ID in(" + PointIDs + ")"); 
                //dtMain = ExecuteDataTable(sb.ToString());
                //获取填报表信息
                foreach (DataRow drShow in dtShow.Rows)
                {
                    Columns += drShow[0].ToString() + " " + FillTable + "@" + drShow[0].ToString() + "@" + drShow[1].ToString() + ",";
                }
                sql = "select ID,{0} from {1} where POINT_ID in({2})";

                sql = string.Format(sql, Columns.TrimEnd(','), FillTable, PointIDs);
                dtMain = ExecuteDataTable(sql);
              
                if (dtMain.Rows.Count > 0)
                {
                    //查询要填报的监测项
                    string FillIDs = "";
                    foreach (DataRow drMain in dtMain.Rows)
                        FillIDs += "'" + drMain["ID"].ToString() + "',";

                    sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1})
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtAllItem = ExecuteDataTable(sql);

                    //把监测项拼接在表格中
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                    }

                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;

                    //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1})";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtFillItem = ExecuteDataTable(sql);

                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                        //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            string itemId = drAllItem["ID"].ToString(); //监测项ID
                            var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                            if (itemValue.Count > 0)
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                            else
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                        }
                    }
                }
            }
            #endregion

            return dtMain;
        }
        #endregion

        #region//根据点位表信息更新填报表
        /// <summary>
        /// 根据点位表信息更新填报表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            //获取断面、垂线/测点信息
            if (mark == "1")
            {
                sql = @"select a.ID SECTION_ID,a.YEAR,a.MONTH,a.SECTION_NAME,b.ID POINT_ID,b.VERTICAL_NAME POINT_NAME
                              from {0} a
                              inner join {1} b on(a.ID=b.SECTION_ID) 
                              where {2} and a.IS_DEL='0' and NUM='Surface'  ";

                sql = string.Format(sql, SectionTable, PointTable, strWhere.Replace("ID", "a.ID"));
            }
            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                    //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    if (mark == "1")
                    {
                        sql = "select ID from {0} where SECTION_ID='{1}' and POINT_ID='{2}'";
                        sql = string.Format(sql, FillTable, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString());
                    }
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        FillID = GetSerialNumber(FillSerial);
                        if (mark == "1")
                        {
                            sql = "insert into {0}(ID,SECTION_ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}','{5}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());

                        }
                        list.Add(sql);
                    }

                    //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    dtAllItem = ExecuteDataTable(sql);
                    //循环每个点位的监测项
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                        sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                        sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count == 0)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                    }
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion
    }
}
