using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Fill.FillQry
{
    /// <summary>
    /// 功能：统计填报数据
    /// 创建日期：2013-07-05
    /// 创建人：魏林
    /// </summary>
    public class FillQryAccess :SqlHelper
    {
        /// <summary>
        /// 获取点位信息
        /// </summary>
        /// <param name="Select">获取的信息</param>
        /// <param name="TableName">表的来源</param>
        /// <param name="Where">条件</param>
        /// <returns></returns>
        public DataTable GetPointInfo(string Select, string TableName, string Where)
        {
            string sql = "";
            sql = "select " + Select + " from " + TableName + " where " + Where;

            return ExecuteDataTable(sql);
        }

        /// <summary>
        /// 获取填报统计数据
        /// </summary>
        /// <param name="type">类别</param>
        /// <param name="year">年份</param>
        /// <param name="months">月份</param>
        /// <param name="point">测点编号</param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public DataTable GetDataInfo(string type, string year, string months, string point, string temp)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            string Select = string.Empty;
            string TableName = string.Empty;
            string Where = string.Empty;
            string Group = string.Empty;

            Select = "a.YEAR,'" + temp + "' TEMP";
            Where = "a.YEAR='" + year + "' " + (months == "" ? "" : "and a.MONTH in(" + months + ") ");
            Group = "a.YEAR,d.ITEM_NAME";
            switch (type)
            {
                case "EnvRiver": //河流
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_RIVER a left join T_ENV_FILL_RIVER_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_RIVER c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "AreaNoise": //区域环境噪声
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_NOISE_AREA a left join T_ENV_FILL_NOISE_AREA_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_NOISE_AREA c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvRoadNoise": //道路交通噪声
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_NOISE_ROAD a left join T_ENV_FILL_NOISE_ROAD_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_NOISE_ROAD c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "FunctionNoise": //功能区噪声
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_NOISE_FUNCTION a left join T_ENV_FILL_NOISE_FUNCTION_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_NOISE_FUNCTION c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvAir":   //环境空气
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_AIR a left join T_ENV_FILL_AIR_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_AIR c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvDrinking": //地下饮用水
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_DRINK_UNDER a left join T_ENV_FILL_DRINK_UNDER_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_DRINK_UNDER c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvDrinkingSource": //饮用水源地
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_DRINK_SRC a left join T_ENV_FILL_DRINK_SRC_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_DRINK_SRC c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvDust": //降尘
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_DUST a left join T_ENV_FILL_DUST_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_DUST c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvDWAir": //双三十废气
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_AIR30 a left join T_ENV_FILL_AIR30_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_AIR30 c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvDWWater": //双三十废水
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_RIVER30 a left join T_ENV_FILL_RIVER30_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_RIVER30 c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvEstuaries": //入海河口
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_ESTUARIES a left join T_ENV_FILL_ESTUARIES_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_ESTUARIES c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvMudRiver": //沉积物（河流）
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_MUD_RIVER a left join T_ENV_FILL_MUD_RIVER_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_MUD_RIVER c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvMudSea": //沉积物（海水）
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_MUD_SEA a left join T_ENV_FILL_MUD_SEA_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_MUD_SEA c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvSoil": //土壤
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_SOIL a left join T_ENV_FILL_SOIL_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_SOIL c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvPSoild": //固废
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_SOLID a left join T_ENV_FILL_SOLID_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_SOLID c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvRain": //降水
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_RAIN a left join T_ENV_FILL_RAIN_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_RAIN c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvReservoir": //湖库
                    Select += ",c.SECTION_NAME POINT_NAME,";
                    TableName += "T_ENV_FILL_LAKE a left join T_ENV_FILL_LAKE_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_LAKE c on(a.SECTION_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.SECTION_CODE='" + point + "'";
                    Group += ",c.SECTION_NAME";
                    break;
                case "EnvSeaBath": //海水浴场
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_SEABATH a left join T_ENV_FILL_SEABATH_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_SEABATH c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvSear": //近岸海域
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_SEA a left join T_ENV_FILL_SEA_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_SEA c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvSource": //近岸直排
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_OFFSHORE a left join T_ENV_FILL_OFFSHORE_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_OFFSHORE c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvSpeed": //盐酸盐化速率
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_ALKALI a left join T_ENV_FILL_ALKALI_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_ALKALI c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
                case "EnvStbc": //生态补偿
                    Select += ",c.POINT_NAME,";
                    TableName += "T_ENV_FILL_PAYFOR a left join T_ENV_FILL_PAYFOR_ITEM b on(a.ID=b.FILL_ID) left join T_ENV_P_PAYFOR c on(a.POINT_ID=c.ID) ";
                    Where += point == "" ? "" : "and c.POINT_CODE='" + point + "'";
                    Group += ",c.POINT_NAME";
                    break;
            }

            Select += "d.ITEM_NAME,ROUND(avg(cast(isnull(b.ITEM_VALUE,'0') as float)),2) AVG_VALUE,max(cast(isnull(b.ITEM_VALUE,'0') as float)) MAX_VALUE,min(cast(isnull(b.ITEM_VALUE,'0') as float)) MIN_VALUE";
            TableName += "left join T_BASE_ITEM_INFO d on(b.ITEM_ID=d.ID)";

            sql = SelectSQL(Select, TableName, Where, Group);

            dt = ExecuteDataTable(sql);

            return dt;
        }

        /// <summary>
        /// 根据条件拼写SQL
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="TableName"></param>
        /// <param name="Where"></param>
        /// <param name="Group"></param>
        /// <returns></returns>
        private string SelectSQL(string Select, string TableName, string Where, string Group)
        {
            //string sql = "";
            //sql = "select " + Select + " from " + TableName + " " + (Where == "" ? "" : "where " + Where + " " + Group == "" ? "" : "Group by " + Group);

            StringBuilder sb = new StringBuilder(5000);
            sb.Append("select " + Select);
            sb.Append(" from " + TableName);
            if (!string.IsNullOrEmpty(Where))
            {
                sb.Append(" where " + Where);
            }
            if (!string.IsNullOrEmpty(Group))
            {
                sb.Append(" Group  by " + Group);
            }
            return sb.ToString();
        }

        #region 综合评价统计报表数据
        /// <summary>
        /// 获取综合评价统计报表信息
        /// </summary>
        /// <param name="type">填报类型</param>
        /// <param name="year">年份</param>
        /// <param name="months">月份</param>
        /// <param name="point_code">监测点CODE</param>
        /// <param name="river_id">河流ID</param>
        /// <returns></returns>
        public DataTable GetFillEvalData(string type, string year, string months, string point_code, string river_id)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            decimal SumValue = 0;
            decimal AvgValue = 0;
            string ZHEval = string.Empty;    //综合评价值
            string ConditionName = string.Empty;
            string strWhere = string.Empty;

            string FillTName = "";
            string FillItemTName = "";
            string PointTName = "";
            string PointIDField = "";
            string PointCodeField = "";
            string PointNameField = "";

            switch (type)
            {
                case "EnvRiver": //河流
                    FillTName = "T_ENV_FILL_RIVER";
                    FillItemTName = "T_ENV_FILL_RIVER_ITEM";
                    PointTName = "T_ENV_P_RIVER";
                    PointIDField = "SECTION_ID";
                    PointCodeField = "SECTION_CODE";
                    PointNameField = "SECTION_NAME";
                    break;
                case "EnvDrinking": //地下饮用水
                    FillTName = "T_ENV_FILL_DRINK_UNDER";
                    FillItemTName = "T_ENV_FILL_DRINK_UNDER_ITEM";
                    PointTName = "T_ENV_P_DRINK_UNDER";
                    PointIDField = "POINT_ID";
                    PointCodeField = "POINT_CODE";
                    PointNameField = "POINT_NAME";
                    break;
                case "EnvReservoir": //湖库
                    FillTName = "T_ENV_FILL_LAKE";
                    FillItemTName = "T_ENV_FILL_LAKE_ITEM";
                    PointTName = "T_ENV_P_LAKE";
                    PointIDField = "SECTION_ID";
                    PointCodeField = "SECTION_CODE";
                    PointNameField = "SECTION_NAME";
                    break;
            }
            //构造条件
            strWhere = "where 1=1";
            if (year.Length > 0)
                strWhere += " and " + PointTName + ".YEAR='" + year + "'";
            if (months.Length > 0)
                strWhere += " and " + PointTName + ".MONTH in(" + months + ")";
            if (point_code.Length > 0)
                strWhere += " and " + PointTName + "." + PointCodeField + "='" + point_code + "'";
            if (river_id.Length > 0)
                strWhere += " and " + PointTName + ".RIVER_ID='" + river_id + "'";

            sql = @"select {3}.{0} POINTCODE,{3}.{1} {3}@POINTNAME@监测点,{3}.CONDITION_ID from {2}
                  left join {3} on({2}.{4}={3}.ID) {5}
                  group by {3}.{0},{3}.{1},{3}.CONDITION_ID";

            sql = string.Format(sql, PointCodeField, PointNameField, FillTName, PointTName, PointIDField, strWhere);
            dtMain = ExecuteDataTable(sql);

            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                string PointCodes = "";
                foreach (DataRow drMain in dtMain.Rows)
                    PointCodes += "'" + drMain["POINTCODE"].ToString() + "',";

                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            a.ITEM_ID 
	                                            from 
		                                            {0} a left join {1} b on(a.FILL_ID=b.ID)
                                                    left join {2} on(b.{3}={2}.ID)
	                                                {5}
	                                            group by
		                                            a.ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                sql = string.Format(sql, FillItemTName, FillTName, PointTName, PointIDField, PointCodeField, strWhere);
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(FillTName + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "评价", typeof(string));
                }
                dtMain.Columns.Add(FillTName + "@eval" + "@综合评价", typeof(string));

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select a.ID,{3}.{0} POINTCODE,a.ITEM_ID,a.ITEM_VALUE 
                                                    from {1} a left join {2} b on(a.FILL_ID=b.ID)
                                                    left join {3} on(b.{4}={3}.ID)
	                                                {6}";
                sql = string.Format(sql, PointCodeField, FillItemTName, FillTName, PointTName, PointIDField, PointCodeField, strWhere);
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    ZHEval = "";
                    drFillItem = dtFillItem.Select("POINTCODE='" + drMain["POINTCODE"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                        {
                            SumValue = 0;
                            for (int i = 0; i < itemValue.Count; i++)
                            {
                                SumValue += decimal.Parse(itemValue[i]["ITEM_VALUE"].ToString() == "" ? "0" : itemValue[i]["ITEM_VALUE"].ToString());
                            }
                            AvgValue = SumValue / itemValue.Count;

                            if (drMain["CONDITION_ID"].ToString() != "")
                            {
                                DataTable dtStand = new DataTable();
                                DataRow[] dr;

                                sql = @"select a.ID,a.MONITOR_ID,isnull(a.DISCHARGE_UPPER,'0') UPPER,isnull(a.DISCHARGE_LOWER,'0') LOWER,b.CONDITION_NAME 
                                      from T_BASE_EVALUATION_CON_ITEM a left join T_BASE_EVALUATION_CON_INFO b on(a.CONDITION_ID=b.ID) 
                                      where a.STANDARD_ID='{0}' and a.ITEM_ID='{1}' and a.IS_DEL='0'";

                                sql = string.Format(sql, drMain["CONDITION_ID"].ToString(), itemId);
                                dtStand = ExecuteDataTable(sql);
                                //dtStand有数据时表示该监测项有设置评价标准
                                if (dtStand.Rows.Count > 0)
                                {
                                    dr = dtStand.Select("UPPER>=" + AvgValue + " and LOWER<=" + AvgValue);
                                    if (dr.Length > 0)
                                    {
                                        ConditionName = dr[0]["CONDITION_NAME"].ToString();
                                        ZHEval = new i3.DataAccess.Channels.Env.Point.Common.CommonAccess().CompareEval(ZHEval, ConditionName);
                                    }
                                    else
                                        ConditionName = "";

                                    if (ConditionName == "")
                                        ConditionName = "超出范围"; 
                                }
                                else
                                    ConditionName = "";
                            }
                            else
                            {
                                ConditionName = "";
                            }

                            drMain[FillTName + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "评价"] = ConditionName; //填入监测项值的评价范围
                        }
                        else
                        {
                            drMain[FillTName + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "评价"] = "--";
                        }
                    }

                    drMain[FillTName + "@eval" + "@综合评价"] = ZHEval;
                }
            }
            dtMain.Columns.Remove("POINTCODE");
            dtMain.Columns.Remove("CONDITION_ID");

            return dtMain;
        }
        #endregion

        #region 全市空气均值统计报表数据
        /// <summary>
        /// 获取全市空气均值统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirAvgData(string strDateS, string strDateE)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "";
            decimal Item_Value = 0;
            decimal AQI = 0;
            ArrayList AirList = new ArrayList();
            
            //构造条件
            strWhere = "where 1=1";
            if (strDateS.Length > 0 && strDateE.Length > 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) between '" + strDateS + "' and '" + strDateE + "'";
            else if (strDateS.Length > 0 && strDateE.Length == 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) >='" + strDateS + "'";
            else if (strDateS.Length == 0 && strDateE.Length > 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) <='" + strDateE + "'";

            sql = @"select YEAR,MONTH,DAY,ROUND(AVG(CAST(ISNULL(AQI_CODE,'0') as float)), 2) 'AQI'
                    from T_ENV_FILL_AIR {0}
                    group by YEAR,MONTH,DAY 
                    order by cast(DAY as int)";

            sql = string.Format(sql, strWhere);
            dtMain = ExecuteDataTable(sql);

            dtMain.Columns.Add("LEVEL", typeof(string));         //质量级别
            dtMain.Columns.Add("STATE", typeof(string));         //质量状况
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            a.ITEM_ID 
	                                            from 
		                                            T_ENV_FILL_AIR_ITEM a left join T_ENV_FILL_AIR b on(a.FILL_ID=b.ID)
		                                            {0}
	                                            group by
		                                            a.ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0' and b.ITEM_NAME not like '%1小时%'";
                sql = string.Format(sql, strWhere);
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }
                
                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select YEAR,MONTH,DAY,ITEM_ID,ITEM_VALUE from
                                                    T_ENV_FILL_AIR_ITEM a left join T_ENV_FILL_AIR b on(a.FILL_ID=b.ID)
	                                            {0}";
                sql = string.Format(sql, strWhere);
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("YEAR='" + drMain["YEAR"].ToString() + "' and MONTH='" + drMain["MONTH"].ToString() + "' and DAY='" + drMain["DAY"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                        {
                            Item_Value = 0;
                            for (int i = 0; i < itemValue.Count; i++)
                            {
                                Item_Value += decimal.Parse(itemValue[i]["ITEM_VALUE"].ToString() == "" ? "0" : itemValue[i]["ITEM_VALUE"].ToString());
                            }
                            Item_Value = Item_Value / itemValue.Count;


                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = Math.Round(Item_Value, 3); //填入监测项平均值
                        }
                        else
                        {
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                        }
                    }

                    AQI = decimal.Parse(drMain["AQI"].ToString() == "" ? "0" : drMain["AQI"].ToString());
                    AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetAirGrade(AQI);
                    drMain["LEVEL"] = AirList[0].ToString();
                    drMain["STATE"] = AirList[1].ToString();
                }
            }

            dtMain.Columns["YEAR"].ColumnName = "T_ENV_FILL_AIR@YEAR@年份";
            dtMain.Columns["MONTH"].ColumnName = "T_ENV_FILL_AIR@MONTH@月份";
            dtMain.Columns["DAY"].ColumnName = "T_ENV_FILL_AIR@DAY@日期";
            dtMain.Columns["AQI"].ColumnName = "T_ENV_FILL_AIR@AQI@空气质量指数";
            dtMain.Columns["LEVEL"].ColumnName = "T_ENV_FILL_AIR@LEVEL@空气质量级别";
            dtMain.Columns["STATE"].ColumnName = "T_ENV_FILL_AIR@STATE@空气质量状况";

            return dtMain;
        }
        #endregion

        #region 空气监测点均值情况统计报表数据
        /// <summary>
        /// 获取空气监测点均值情况统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPointAvgData(string strDateS, string strDateE, string strPoint)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "";
            decimal Item_Value = 0;
            decimal AQI = 0;
            ArrayList AirList = new ArrayList();

            //构造条件
            strWhere = "where 1=1";
            if (strDateS.Length > 0 && strDateE.Length > 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) between '" + strDateS + "' and '" + strDateE + "'";
            else if (strDateS.Length > 0 && strDateE.Length == 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) >='" + strDateS + "'";
            else if (strDateS.Length == 0 && strDateE.Length > 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) <='" + strDateE + "'";

            if (strPoint.Length > 0)
                strWhere += " and T_ENV_P_AIR.POINT_CODE='" + strPoint + "'";

            sql = @"select T_ENV_P_AIR.POINT_CODE,T_ENV_P_AIR.POINT_NAME,ROUND(AVG(CAST(ISNULL(T_ENV_FILL_AIR.AQI_CODE,'0') as float)), 2) 'AQI'
                    from T_ENV_FILL_AIR 
                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID)
                    {0}
                    group by T_ENV_P_AIR.POINT_CODE,T_ENV_P_AIR.POINT_NAME";

            sql = string.Format(sql, strWhere);
            dtMain = ExecuteDataTable(sql);

            dtMain.Columns.Add("LEVEL", typeof(string));         //质量级别
            dtMain.Columns.Add("STATE", typeof(string));         //质量状况
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            T_ENV_FILL_AIR_ITEM.ITEM_ID 
	                                            from 
		                                            T_ENV_FILL_AIR_ITEM 
                                                    left join T_ENV_FILL_AIR on(T_ENV_FILL_AIR_ITEM.FILL_ID=T_ENV_FILL_AIR.ID)
                                                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID)
		                                            {0}
	                                            group by
		                                            T_ENV_FILL_AIR_ITEM.ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0' and b.ITEM_NAME not like '%8小时%'";
                sql = string.Format(sql, strWhere);
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select T_ENV_P_AIR.POINT_CODE,T_ENV_FILL_AIR_ITEM.ITEM_ID,T_ENV_FILL_AIR_ITEM.ITEM_VALUE from
                                                    T_ENV_FILL_AIR_ITEM left join T_ENV_FILL_AIR on(T_ENV_FILL_AIR_ITEM.FILL_ID=T_ENV_FILL_AIR.ID)
                                                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID)
	                                            {0}";
                sql = string.Format(sql, strWhere);
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("POINT_CODE='" + drMain["POINT_CODE"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                        {
                            Item_Value = 0;
                            for (int i = 0; i < itemValue.Count; i++)
                            {
                                Item_Value += decimal.Parse(itemValue[i]["ITEM_VALUE"].ToString() == "" ? "0" : itemValue[i]["ITEM_VALUE"].ToString());
                            }
                            Item_Value = Item_Value / itemValue.Count;


                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = Math.Round(Item_Value, 3); //填入监测项平均值
                        }
                        else
                        {
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                        }
                    }

                    AQI = decimal.Parse(drMain["AQI"].ToString() == "" ? "0" : drMain["AQI"].ToString());
                    AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetAirGrade(AQI);
                    drMain["LEVEL"] = AirList[0].ToString();
                    drMain["STATE"] = AirList[1].ToString();
                }
            }

            dtMain.Columns.Remove("POINT_CODE");
            dtMain.Columns["POINT_NAME"].ColumnName = "T_ENV_P_AIR@POINT_NAME@监测点";
            dtMain.Columns["AQI"].ColumnName = "T_ENV_FILL_AIR@AQI@空气质量指数";
            dtMain.Columns["LEVEL"].ColumnName = "T_ENV_FILL_AIR@LEVEL@空气质量级别";
            dtMain.Columns["STATE"].ColumnName = "T_ENV_FILL_AIR@STATE@空气质量状况";

            return dtMain;
        }
        #endregion

        #region 全市空气首要污染物指数统计报表数据
        /// <summary>
        /// 获取全市空气首要污染物指数统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPullutionData(string strDateS, string strDateE)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "";
            decimal Item_Value = 0;
            string Item_Name = "";
            decimal AIR_Index = 0;
            decimal Temp_Index = 0;
            string PullutionName = "";
            ArrayList AirList = new ArrayList();

            //构造条件
            strWhere = "where 1=1";
            if (strDateS.Length > 0 && strDateE.Length > 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) between '" + strDateS + "' and '" + strDateE + "'";
            else if (strDateS.Length > 0 && strDateE.Length == 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) >='" + strDateS + "'";
            else if (strDateS.Length == 0 && strDateE.Length > 0)
                strWhere += " and CAST(YEAR+'-'+MONTH+'-'+DAY as DATETIME) <='" + strDateE + "'";
            strWhere += " and AIR_LEVEL<>''";

            sql = @"select AIR_LEVEL,AIR_STATE,COUNT(1) DAYS,case AIR_LEVEL when '一级' then 1 when '二级' then 2 when '三级' then 3 when '四级' then 4 when '五级' then 5 else 6 end SORT
                    from T_ENV_FILL_AIR 
                    {0}
                    group by AIR_LEVEL,AIR_STATE
                    order by SORT";

            sql = string.Format(sql, strWhere);
            dtMain = ExecuteDataTable(sql);

            dtMain.Columns.Add("PULLUTION", typeof(string));
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            a.ITEM_ID 
	                                            from 
		                                            T_ENV_FILL_AIR_ITEM a left join T_ENV_FILL_AIR b on(a.FILL_ID=b.ID)
		                                            {0}
	                                            group by
		                                            a.ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0' and b.ITEM_NAME not like '%1小时%'";
                sql = string.Format(sql, strWhere);
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数", typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select AIR_LEVEL,ITEM_ID,ITEM_VALUE from
                                                    T_ENV_FILL_AIR_ITEM a left join T_ENV_FILL_AIR b on(a.FILL_ID=b.ID)
	                                            {0}";
                sql = string.Format(sql, strWhere);
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    Temp_Index = 0;
                    PullutionName = "";
                    drFillItem = dtFillItem.Select("AIR_LEVEL='" + drMain["AIR_LEVEL"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        Item_Name = drAllItem["ITEM_NAME"].ToString(); //监测项名称
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                        {
                            Item_Value = 0;
                            AIR_Index = 0;
                            for (int i = 0; i < itemValue.Count; i++)
                            {
                                Item_Value += decimal.Parse(itemValue[i]["ITEM_VALUE"].ToString() == "" ? "0" : itemValue[i]["ITEM_VALUE"].ToString());
                            }
                            Item_Value = Item_Value / itemValue.Count;

                            //获取污染物的浓度限制值和空气质量分指数的值
                            if (Item_Name.Equals("二氧化硫"))
                            {
                                #region//So2的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetSO2PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("二氧化氮"))
                            {
                                #region//NO的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetNO2PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("一氧化碳"))
                            {
                                #region//CO的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetCOPolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("PM2.5"))
                            {
                                #region//PM2.5"的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetPM25PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("PM10"))
                            {
                                #region//PM10的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetPM10PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("臭氧最大8小时滑动平均"))
                            {
                                #region//So2的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetO38PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else
                            {
                                AirList[0] = AirList[1] = AirList[2] = AirList[3] = "0";
                            }
                            AIR_Index = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().CalculateResult(AirList, Item_Value.ToString());//获取计算结果
                            if (AIR_Index > Temp_Index)
                            {
                                Temp_Index = AIR_Index;
                                PullutionName = Item_Name;
                            }

                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = Math.Round(Item_Value, 3); //填入监测项平均值
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数"] = AIR_Index; //填入监测项污染分指数
                        }
                        else
                        {
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数"] = "--";
                        }
                    }

                    drMain["PULLUTION"] = PullutionName;
                }
                
            }

            dtMain.Columns.Remove("SORT");
            dtMain.Columns["AIR_LEVEL"].ColumnName = "T_ENV_FILL_AIR@AIR_LEVEL@空气质量级别";
            dtMain.Columns["AIR_STATE"].ColumnName = "T_ENV_FILL_AIR@AIR_STATE@空气质量状况";
            dtMain.Columns["DAYS"].ColumnName = "T_ENV_FILL_AIR@DAYS@污染天数";
            dtMain.Columns["PULLUTION"].ColumnName = "T_ENV_FILL_AIR@PULLUTION@主要污染物";

            return dtMain;
        }
        #endregion

        #region 空气监测点首要污染物指数统计报表数据
        /// <summary>
        /// 获取空气监测点首要污染物指数统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPointPullutionData(string strDateS, string strDateE, string strPointCode)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "";
            decimal Item_Value = 0;
            string Item_Name = "";
            decimal AIR_Index = 0;
            decimal Temp_Index = 0;
            string PullutionName = "";
            ArrayList AirList = new ArrayList();

            //构造条件
            strWhere = "where 1=1";
            if (strDateS.Length > 0 && strDateE.Length > 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) between '" + strDateS + "' and '" + strDateE + "'";
            else if (strDateS.Length > 0 && strDateE.Length == 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) >='" + strDateS + "'";
            else if (strDateS.Length == 0 && strDateE.Length > 0)
                strWhere += " and CAST(T_ENV_FILL_AIR.YEAR+'-'+T_ENV_FILL_AIR.MONTH+'-'+T_ENV_FILL_AIR.DAY as DATETIME) <='" + strDateE + "'";
            strWhere += " and T_ENV_FILL_AIR.AIR_LEVEL<>''";

            if (strPointCode.Length > 0)
                strWhere += " and T_ENV_P_AIR.POINT_CODE='" + strPointCode + "'";

            sql = @"select T_ENV_P_AIR.POINT_CODE,T_ENV_P_AIR.POINT_NAME,T_ENV_FILL_AIR.AIR_LEVEL,T_ENV_FILL_AIR.AIR_STATE,COUNT(1) DAYS,case AIR_LEVEL when '一级' then 1 when '二级' then 2 when '三级' then 3 when '四级' then 4 when '五级' then 5 else 6 end SORT
                    from T_ENV_FILL_AIR 
                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID) 
                    {0}
                    group by T_ENV_P_AIR.POINT_CODE,T_ENV_P_AIR.POINT_NAME,T_ENV_FILL_AIR.AIR_LEVEL,T_ENV_FILL_AIR.AIR_STATE
                    order by T_ENV_P_AIR.POINT_CODE,SORT";

            sql = string.Format(sql, strWhere);
            dtMain = ExecuteDataTable(sql);

            dtMain.Columns.Add("PULLUTION", typeof(string));
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            T_ENV_FILL_AIR_ITEM.ITEM_ID 
	                                            from 
                                                    T_ENV_FILL_AIR_ITEM 
                                                    left join T_ENV_FILL_AIR on(T_ENV_FILL_AIR_ITEM.FILL_ID=T_ENV_FILL_AIR.ID)
                                                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID)
		                                            {0}
	                                            group by
		                                            T_ENV_FILL_AIR_ITEM.ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0' and b.ITEM_NAME not like '%1小时%'";
                sql = string.Format(sql, strWhere);
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                    dtMain.Columns.Add("T_ENV_FILL_AIR@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数", typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select T_ENV_P_AIR.POINT_CODE,T_ENV_FILL_AIR.AIR_LEVEL,T_ENV_FILL_AIR_ITEM.ITEM_ID,T_ENV_FILL_AIR_ITEM.ITEM_VALUE from
                                                    T_ENV_FILL_AIR_ITEM left join T_ENV_FILL_AIR on(T_ENV_FILL_AIR_ITEM.FILL_ID=T_ENV_FILL_AIR.ID)
                                                    left join T_ENV_P_AIR on(T_ENV_FILL_AIR.POINT_ID=T_ENV_P_AIR.ID)
	                                            {0}";
                sql = string.Format(sql, strWhere);
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    Temp_Index = 0;
                    PullutionName = "";
                    drFillItem = dtFillItem.Select("POINT_CODE='" + drMain["POINT_CODE"].ToString() + "' and AIR_LEVEL='" + drMain["AIR_LEVEL"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        Item_Name = drAllItem["ITEM_NAME"].ToString(); //监测项名称
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                        {
                            Item_Value = 0;
                            AIR_Index = 0;
                            for (int i = 0; i < itemValue.Count; i++)
                            {
                                Item_Value += decimal.Parse(itemValue[i]["ITEM_VALUE"].ToString() == "" ? "0" : itemValue[i]["ITEM_VALUE"].ToString());
                            }
                            Item_Value = Item_Value / itemValue.Count;

                            //获取污染物的浓度限制值和空气质量分指数的值
                            if (Item_Name.Equals("二氧化硫"))
                            {
                                #region//So2的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetSO2PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("二氧化氮"))
                            {
                                #region//NO的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetNO2PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("一氧化碳"))
                            {
                                #region//CO的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetCOPolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("PM2.5"))
                            {
                                #region//PM2.5"的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetPM25PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("PM10"))
                            {
                                #region//PM10的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetPM10PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else if (Item_Name.Equals("臭氧最大8小时滑动平均"))
                            {
                                #region//So2的计算
                                AirList = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().GetO38PolluctionLimitValue(Item_Value);
                                #endregion
                            }
                            else
                            {
                                AirList[0] = AirList[1] = AirList[2] = AirList[3] = "0";
                            }
                            AIR_Index = new i3.DataAccess.Channels.Env.Fill.Air.TEnvFillAirAccess().CalculateResult(AirList, Item_Value.ToString());//获取计算结果
                            if (AIR_Index > Temp_Index)
                            {
                                Temp_Index = AIR_Index;
                                PullutionName = Item_Name;
                            }

                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = Math.Round(Item_Value, 3); //填入监测项平均值
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数"] = AIR_Index; //填入监测项污染分指数
                        }
                        else
                        {
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                            drMain["T_ENV_FILL_AIR@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数"] = "--";
                        }
                    }

                    drMain["PULLUTION"] = PullutionName;
                }

            }
            dtMain.Columns.Remove("POINT_CODE");
            dtMain.Columns.Remove("SORT");
            dtMain.Columns["POINT_NAME"].ColumnName = "T_ENV_P_AIR@POINT_NAME@监测点";
            dtMain.Columns["AIR_LEVEL"].ColumnName = "T_ENV_FILL_AIR@AIR_LEVEL@空气质量级别";
            dtMain.Columns["AIR_STATE"].ColumnName = "T_ENV_FILL_AIR@AIR_STATE@空气质量状况";
            dtMain.Columns["DAYS"].ColumnName = "T_ENV_FILL_AIR@DAYS@污染天数";
            dtMain.Columns["PULLUTION"].ColumnName = "T_ENV_FILL_AIR@PULLUTION@主要污染物";

            return dtMain;
        }
        #endregion
    }
}
