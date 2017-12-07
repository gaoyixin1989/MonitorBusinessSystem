<%@ WebService Language="C#" Class="DataSrh" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class DataSrh  : System.Web.Services.WebService {

    [WebMethod]
    public string selCompanyLst(string strCompanyName)
    {
        TBaseCompanyInfoVo objCom = new TBaseCompanyInfoVo();
        TBaseCompanyInfoLogic objComLogic = new TBaseCompanyInfoLogic();
        objCom.COMPANY_NAME = strCompanyName;
        objCom.IS_DEL = "0";
        
        string strJson = "";
        DataTable dt = objComLogic.SelectDefinedTadble(objCom, 0, 0);
        foreach (DataRow dr in dt.Rows)
        {
            strJson += (strJson.Length>0?",":"")+ "{'ID':'" + dr["ID"].ToString() + "','name':'" + dr["COMPANY_NAME"].ToString() + "'}";
        }

        strJson = "[" + strJson + "]";

        return strJson;
    }

    [WebMethod]
    public string getCompanyItemType(string strID)
    {
        string strJson = "";

        DataTable dt = getPointDt(strID);

        string strMonitorIDs = getDtColValus(dt, "MONITOR_ID");
        string strMonitorNames = getDtColValus(dt, "Remark1");
        if (strMonitorIDs.Length == 0)
            return "[]";

        string[] arrMonitorID = strMonitorIDs.Split(',');
        string[] arrMonitorName = strMonitorNames.Split(',');

        for (int i = 0; i < arrMonitorID.Length; i++)
        {
            strJson += strJson.Length > 0 ? "," : "";
            //strJson += "{'MonitorID':'" + arrMonitorID[i] + "','监测类型'='" + arrMonitorName[i] + "'}";
            strJson += "{'id':'" + arrMonitorID[i] + "','name'='" + arrMonitorName[i] + "'}";
        }
        strJson = "[" + strJson + "]";

        return strJson;
    }
    
    [WebMethod]
    public string getCompanyPoint(string strID, string MonitorID)
    {
        string strJson = "";

        DataTable dt = getPointDt(strID);

        string strMonitorIDs = getDtColValus(dt, "MONITOR_ID");
        if (strMonitorIDs.Length == 0)
            return "[]";

        string[] arrMonitorID = strMonitorIDs.Split(',');
        foreach (string strMonitorID in arrMonitorID)
        {
            if (MonitorID == strMonitorID)
            {
                DataRow[] drs = dt.Select("MONITOR_ID='" + strMonitorID + "'");

                strJson += strJson.Length > 0 ? "," : "";
                //strJson += "{'监测类型':'" + strMonitorID + "','info':";
                strJson += "{'Rows':";
                strJson += "[";
                string strJsonP = "";
                foreach (DataRow dr in drs)
                {
                    strJsonP += strJsonP.Length > 0 ? "," : "";
                    //strJsonP += "{'id':'" + dr["ID"].ToString() + "','点位':'" + dr["POINT_NAME"].ToString() + "','委托类型':" + dr["POINT_TYPE"].ToString() + "'}";
                    strJsonP += "{'POINT_CODE':'" + dr["ID"].ToString() + "','POINT_NAME':'" + dr["POINT_NAME"].ToString() + "'}";
                }
                strJson += strJsonP;
                strJson += "],'Total':'" + drs.Length + "'";
                strJson += "}";
            }
        }
        //strJson = "[" + strJson + "]";

        return strJson;
    }

    [WebMethod]
    public string getPointItemS(string strPointIDs, int intPageIndex, int intPageSize)
    {
        TMisMonitorTaskItemLogic objLogic = new TMisMonitorTaskItemLogic();
        int intTotalCount = objLogic.GetSelectResultCount_forMobile(strPointIDs);//总计的数据条数
        DataTable dt = objLogic.SelectByTable_forMobile(strPointIDs, intPageIndex, intPageSize);
        string strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string getDataLst(string strSelPointIDs, string strSelItemS, string strBeginDate, string strEndDate)
    {
        DataTable dt = new TMisMonitorTaskItemLogic().SelectResult_ByTable_forMobile(strSelPointIDs, strSelItemS, strBeginDate, strEndDate);

        string strJson = getEnvDataJson(dt, true);
        if (!strJson.StartsWith("["))
            strJson = "[" + strJson + "]";

        return strJson;
    }

    [WebMethod]
    public string getEnvTypeLst()
    {
        string strJson = "";

        strJson += "{'id':'EnvRiver','name':'河流'},";
        strJson += "{'id':'EnvReservoir','name':'湖库'},";
        strJson += "{'id':'EnvDrinkUnder','name':'地下水'},";
        strJson += "{'id':'EnvDrinkingSource_Under','name':'地下饮用水源地'},";
        strJson += "{'id':'EnvDrinkingSource_Surface','name':'地表饮用水源地'},";
        strJson += "{'id':'EnvRain','name':'降水'},";
        strJson += "{'id':'EnvDust','name':'降尘'},";
        strJson += "{'id':'EnvAir','name':'环境空气'},";
        strJson += "{'id':'AreaNoise','name':'区域环境噪声'},";
        strJson += "{'id':'EnvRoadNoise','name':'道路交通噪声'},";
        strJson += "{'id':'FunctionNoise','name':'功能区噪声'},";
        strJson += "{'id':'EnvMudRiver','name':'底泥'},";
        strJson += "{'id':'EnvPSoild','name':'固废'},";
        strJson += "{'id':'EnvSoil','name':'土壤'}";

        strJson = "[" + strJson + "]";
        
        return strJson;
    }

    [WebMethod]
    public string getEnvDataLst(string strEnvTypeID, string strPointCodeS, string strPointNameS, string strItemIDS, string strBeginDate, string strEndDate)
    {
        string strJson = "";
        TMisMonitorTaskItemLogic objLogic = new TMisMonitorTaskItemLogic();
        string strSQLSel = "";

        strPointNameS = strPointNameS.Replace(",", "','");
        strPointNameS = "'" + strPointNameS + "'";
        strPointCodeS = strPointCodeS.Replace(",", "','");
        strPointCodeS = "'" + strPointCodeS + "'";
        strItemIDS = strItemIDS.Replace(",", "','");
        strItemIDS = "'" + strItemIDS + "'";

        //有断面
        string strSqlCon1 = "SECTION_NAME in (" + strPointNameS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        //string strSqlCon1 = "SECTION_CODE in (" + strPointCodeS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        string strSql1 = @"select p.SECTION_NAME as POINT_NAME,case when f.DAY is null then '' else CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY  AS datetime) end as SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_VALUE as ITEM_RESULT 
                             from {1} r  
	                            join {0} f on r.FILL_ID=f.ID  
                                join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID 
                                join {2} p on p.Id=f.SECTION_ID 
                             where r.ITEM_ID in ({3}) 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)>='{4}' 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)<'{5}' 
                                and f.SECTION_ID in (select ID from {2} where " + strSqlCon1 + ") ";
        //饮用水源地专用
        string strSql3 = @"select p.SECTION_NAME as POINT_NAME,case when f.DAY is null then '' else CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY  AS datetime) end as SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_VALUE as ITEM_RESULT 
                             from {1} r  
	                            join {0} f on r.FILL_ID=f.ID  
                                join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID 
                                join {2} p on p.Id=f.SECTION_ID 
                             where r.ITEM_ID in ({3}) 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)>='{4}' 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)<'{5}' 
                                and f.SECTION_ID in (select ID from {2} where NUM='{6}' and  " + strSqlCon1 + ") ";

        //无断面
        string strSqlCon2 = "POINT_NAME in (" + strPointNameS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        //string strSqlCon2 = "POINT_CODE in (" + strPointCodeS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        string strSql2 = @"select p.POINT_NAME,case when f.DAY is null then '' else CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY  AS datetime) end as SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_VALUE as ITEM_RESULT 
                             from {1} r  
	                            join {0} f on r.FILL_ID=f.ID  
                                join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID 
                                join {2} p on p.Id=f.POINT_ID 
                             where r.ITEM_ID in ({3}) 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)>='{4}' 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.DAY AS datetime)<'{5}' 
                                and f.POINT_ID in (select ID from {2} where " + strSqlCon2 + ") ";

        //降水、降尘、噪声专用 不使用“开始月”、“开始日”的填报数据格式
        string strSql4 = @"select p.POINT_NAME,case when f.BEGIN_DAY is null then '' else CAST(f.YEAR + '-' + f.MONTH + '-' + f.BEGIN_DAY  AS datetime) end as SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_VALUE as ITEM_RESULT 
                             from {1} r  
	                            join {0} f on r.FILL_ID=f.ID  
                                join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID 
                                join {2} p on p.Id=f.POINT_ID 
                             where r.ITEM_ID in ({3}) 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.BEGIN_DAY AS datetime)>='{4}' 
	                            and CAST(f.YEAR + '-' + f.MONTH + '-' + f.BEGIN_DAY AS datetime)<'{5}' 
                                and f.POINT_ID in (select ID from {2} where " + strSqlCon2 + ") ";       

        switch (strEnvTypeID)
        {
            case "EnvRiver":
                strSQLSel = string.Format(strSql1, "T_ENV_FILL_RIVER", "T_ENV_FILL_RIVER_ITEM", "T_ENV_P_RIVER", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvReservoir":
                strSQLSel = string.Format(strSql1, "T_ENV_FILL_LAKE", "T_ENV_FILL_LAKE_ITEM", "T_ENV_P_LAKE", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvDrinkUnder":
                strSQLSel = string.Format(strSql2, "T_ENV_FILL_DRINK_UNDER", "T_ENV_FILL_DRINK_UNDER_ITEM", "T_ENV_P_DRINK_UNDER", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvDrinkingSource_Under":
                strSQLSel = string.Format(strSql3, "T_ENV_FILL_DRINK_SRC", "T_ENV_FILL_DRINK_SRC_ITEM", "T_ENV_P_DRINK_SRC", strItemIDS, strBeginDate, strEndDate, "Under");
                break;
            case "EnvDrinkingSource_Surface":
                strSQLSel = string.Format(strSql3, "T_ENV_FILL_DRINK_SRC", "T_ENV_FILL_DRINK_SRC_ITEM", "T_ENV_P_DRINK_SRC", strItemIDS, strBeginDate, strEndDate, "Surface");
                break;
            case "EnvRain":
                strSQLSel = string.Format(strSql4, "T_ENV_FILL_RAIN", "T_ENV_FILL_RAIN_ITEM", "T_ENV_P_RAIN", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvDust":
                strSQLSel = string.Format(strSql4, "T_ENV_FILL_DUST", "T_ENV_FILL_DUST_ITEM", "T_ENV_P_DUST", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvAir":
                strSQLSel = string.Format(strSql2, "T_ENV_FILL_AIR", "T_ENV_FILL_AIR_ITEM", "T_ENV_P_AIR", strItemIDS, strBeginDate, strEndDate);
                break;
            case "AreaNoise":
                strSQLSel = string.Format(strSql4, "T_ENV_FILL_NOISE_AREA", "T_ENV_FILL_NOISE_AREA_ITEM", "T_ENV_P_NOISE_AREA", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvRoadNoise":
                strSQLSel = string.Format(strSql4, "T_ENV_FILL_NOISE_ROAD", "T_ENV_FILL_NOISE_ROAD_ITEM", "T_ENV_P_NOISE_ROAD", strItemIDS, strBeginDate, strEndDate);
                break;
            case "FunctionNoise":
                strSQLSel = string.Format(strSql4, "T_ENV_FILL_NOISE_FUNCTION", "T_ENV_FILL_NOISE_FUNCTION_ITEM", "T_ENV_P_NOISE_FUNCTION", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvMudRiver":
                strSQLSel = string.Format(strSql1, "T_ENV_FILL_MUD_RIVER", "T_ENV_FILL_MUD_RIVER_ITEM", "T_ENV_P_MUD_RIVER", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvPSoild":
                strSQLSel = string.Format(strSql2, "T_ENV_FILL_SOLID", "T_ENV_FILL_SOLID_ITEM", "T_ENV_P_SOLID", strItemIDS, strBeginDate, strEndDate);
                break;
            case "EnvSoil":
                strSQLSel = string.Format(strSql2, "T_ENV_FILL_SOIL", "T_ENV_FILL_SOIL_ITEM", "T_ENV_P_SOIL", strItemIDS, strBeginDate, strEndDate);
                break;
            default:
                break;
        }

        if (strSQLSel.Length == 0)
            return "[]";
        
        DataTable dt = objLogic.SelectSQL_ByTable_forMobile(strSQLSel, 0, 0);

        strJson = getEnvDataJson(dt, false);

        if (!strJson.StartsWith("["))
            strJson = "[" + strJson + "]";

        return strJson;
    }

    private string getEnvDataJson(DataTable dt,bool hasUnit)
    {
        string strJson = "";

        string strPointS = getDtColValus(dt, "POINT_NAME");
        string strItemS = getDtColValus(dt, "ITEM_NAME");

        string[] arrPoint = strPointS.Split(',');
        string[] arrItem = strItemS.Split(',');

        foreach (string strPoint in arrPoint)
        {
            if (!ifPointHasItem(dt, strPoint, arrItem))//判定该点位是否有选定的项目
                continue;

            strJson += strJson.Length > 0 ? "," : "";
            strJson += "{";
            strJson += "'监测点位':'" + strPoint + "',";
            strJson += "'item':";
            strJson += "[";

            string strItemJson = "";
            foreach (string strItem in arrItem)
            {
                DataRow[] drData = dt.Select("POINT_NAME='" + strPoint + "' and ITEM_NAME='" + strItem + "'");
                
                strItemJson += strItemJson.Length > 0 ? "," : "";
                strItemJson += "{";
                strItemJson += "'监测项目':'" + strItem + "',";

                if (hasUnit)
                {
                    if (drData.Length > 0)
                    {
                        strItemJson += "'单位':'" + drData[0]["unit"].ToString() + "'";
                        strItemJson += ",";
                    }
                    else
                    {
                        strItemJson += "'单位':''";
                        strItemJson += ",";
                    }
                }
                else
                {
                    strItemJson += "'单位':''";
                    strItemJson += ",";
                }
                strItemJson += "'data':";
                strItemJson += "[";

                string strDataJson = "";
                
                foreach (DataRow dr in drData)
                {
                    strDataJson += strDataJson.Length > 0 ? "," : "";
                    strDataJson += "{";

                    strDataJson += "'Date':'" + dr["SAMPLE_FINISH_DATE"].ToString().Replace("00:00:00.000", "").Replace("0:00:00", "").Trim() + "',";
                    strDataJson += "'Value':'" + dr["ITEM_RESULT"].ToString() + "'";
                    //if (hasUnit)
                    //{
                    //    strDataJson += ",";
                    //    strDataJson += "'单位':'" + dr["unit"].ToString() + "'";
                    //}

                    strDataJson += "}";
                }

                strItemJson += strDataJson;

                strItemJson += "]";
                strItemJson += "}";
            }

            strJson += strItemJson;

            strJson += "]";
            strJson += "}";
        }

        return strJson;
    }

    [WebMethod]
    public string getEnvPointItemLst(string strEnvTypeID,string strPointCodeS,string strPointNameS, int intPageIndex, int intPageSize)
    {
        string strJson = "";
        TMisMonitorTaskItemLogic objLogic = new TMisMonitorTaskItemLogic();
        string strSQLSel = "";
        string strSQLcount = "";

        strPointNameS = strPointNameS.Replace(",", "','");
        strPointNameS = "'" + strPointNameS + "'";
        strPointCodeS = strPointCodeS.Replace(",", "','");
        strPointCodeS = "'" + strPointCodeS + "'";
        
        string strSqlCon1 = "SECTION_NAME in (" + strPointNameS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        //string strSqlCon1 = "SECTION_CODE in (" + strPointCodeS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        string strSqlSel1 = @"select distinct i.ID,i.ITEM_Name from {2} vi
                                 join T_BASE_ITEM_INFO i on i.ID=vi.ITEM_ID
                                 where vi.POINT_ID in
                                     (select ID from {1}
                                        where SECTION_ID in (select ID from {0} where " + strSqlCon1 + "))";
        string strSqlCount1 = @"select count(*) from
                                 (select distinct i.ID,i.ITEM_Name from {2} vi
                                     join T_BASE_ITEM_INFO i on i.ID=vi.ITEM_ID
                                     where vi.POINT_ID in
                                         (select ID from {1}
                                            where SECTION_ID in (select ID from {0} where " + strSqlCon1 + ")))t";

        string strSqlCon2 = "POINT_NAME in (" + strPointNameS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        //string strSqlCon2 = "POINT_CODE in (" + strPointCodeS + ")";//用点位名strPointNameS查询，也可以改为点位编码strPointCodeS
        string strSqlSel2 = @"select distinct i.ID,i.ITEM_Name from {1} vi
                                 join T_BASE_ITEM_INFO i on i.ID=vi.ITEM_ID
                                 where vi.POINT_ID in
                                     (select ID from {0} where " + strSqlCon2 + ")";
        string strSqlCount2 = @"select count(*) from
                                 (select distinct i.ID,i.ITEM_Name from {1} vi
                                     join T_BASE_ITEM_INFO i on i.ID=vi.ITEM_ID
                                     where vi.POINT_ID in
                                         (select ID from {0} where " + strSqlCon2 + "))t";

        switch (strEnvTypeID)
        {
            case "EnvRiver":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_RIVER", "T_ENV_P_RIVER_V", "T_ENV_P_RIVER_V_ITEM");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_RIVER", "T_ENV_P_RIVER_V", "T_ENV_P_RIVER_V_ITEM");

                break;
            case "EnvReservoir":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_LAKE", "T_ENV_P_LAKE_V", "T_ENV_P_LAKE_V_ITEM");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_LAKE", "T_ENV_P_LAKE_V", "T_ENV_P_LAKE_V_ITEM");

                break;
            case "EnvDrinkUnder":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_DRINK_UNDER", "T_ENV_P_DRINK_UNDER_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_DRINK_UNDER", "T_ENV_P_DRINK_UNDER_ITEM");

                break;
            case "EnvDrinkingSource_Under":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_DRINK_SRC", "T_ENV_P_DRINK_SRC_V", "T_ENV_P_DRINK_SRC_V_ITEM");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_DRINK_SRC", "T_ENV_P_DRINK_SRC_V", "T_ENV_P_DRINK_SRC_V_ITEM");

                break;
            case "EnvDrinkingSource_Surface":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_DRINK_SRC", "T_ENV_P_DRINK_SRC_V", "T_ENV_P_DRINK_SRC_V_ITEM");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_DRINK_SRC", "T_ENV_P_DRINK_SRC_V", "T_ENV_P_DRINK_SRC_V_ITEM");

                break;
            case "EnvRain":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_RAIN", "T_ENV_P_RAIN_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_RAIN", "T_ENV_P_RAIN_ITEM");

                break;
            case "EnvDust":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_DUST", "T_ENV_P_DUST_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_DUST", "T_ENV_P_DUST_ITEM");

                break;
            case "EnvAir":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_AIR", "T_ENV_P_AIR_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_AIR", "T_ENV_P_AIR_ITEM");

                break;
            case "AreaNoise":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_NOISE_AREA", "T_ENV_P_NOISE_AREA_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_NOISE_AREA", "T_ENV_P_NOISE_AREA_ITEM");

                break;
            case "EnvRoadNoise":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_NOISE_ROAD", "T_ENV_P_NOISE_ROAD_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_NOISE_ROAD", "T_ENV_P_NOISE_ROAD_ITEM");

                break;
            case "FunctionNoise":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_NOISE_FUNCTION", "T_ENV_P_NOISE_FUNCTION_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_NOISE_FUNCTION", "T_ENV_P_NOISE_FUNCTION_ITEM");

                break;
            case "EnvMudRiver":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_MUD_RIVER", "T_ENV_P_MUD_RIVER_V", "T_ENV_P_MUD_RIVER_V_ITEM");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_MUD_RIVER", "T_ENV_P_MUD_RIVER_V", "T_ENV_P_MUD_RIVER_V_ITEM");

                break;
            case "EnvPSoild":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_SOLID", "T_ENV_P_SOLID_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_SOLID", "T_ENV_P_SOLID_ITEM");

                break;
            case "EnvSoil":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_SOIL", "T_ENV_P_SOIL_ITEM");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_SOIL", "T_ENV_P_SOIL_ITEM");

                break;
            default:
                break;
        }

        if (strSQLSel.Length == 0)
            return "[]";
        
        DataTable dt = objLogic.SelectSQL_ByTable_forMobile(strSQLSel, intPageIndex, intPageSize);
        int intTotalCount = objLogic.GetSelectSQL_ResultCount_forMobile(strSQLcount);

        strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string getEnvPointLst(string strEnvTypeID, int intPageIndex, int intPageSize)
    {
        string strJson = "";
        TMisMonitorTaskItemLogic objLogic = new TMisMonitorTaskItemLogic();
        string strSQLSel = "";
        string strSQLcount = "";

        string strSqlSel1 = "select Distinct {1},{2} from {0} where IS_DEL='0'";
        string strSqlCount1 = "select count(*) from (select Distinct {1},{2} from {0} where IS_DEL='0')t";
        string strSqlSel2 = "select Distinct {1},{2} from {0} where IS_DEL='0' and NUM='{3}'";
        string strSqlCount2 = "select count(*) from (select Distinct {1},{2} from {0} where IS_DEL='0' and NUM='{3}')t";
        
        switch (strEnvTypeID)
        {
            case "EnvRiver":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_RIVER", "SECTION_CODE as POINT_CODE", "SECTION_NAME as POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_RIVER", "SECTION_CODE", "SECTION_NAME");
                
                break;
            case "EnvReservoir":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_LAKE", "SECTION_CODE as POINT_CODE", "SECTION_NAME as POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_LAKE", "SECTION_CODE", "SECTION_NAME");
                
                break;
            case "EnvDrinkUnder":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_DRINK_UNDER", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_DRINK_UNDER", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvDrinkingSource_Under":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_DRINK_SRC", "SECTION_CODE as POINT_CODE", "SECTION_NAME as POINT_NAME", "Under");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_DRINK_SRC", "SECTION_CODE", "SECTION_NAME", "Under");

                break;
            case "EnvDrinkingSource_Surface":
                strSQLSel = string.Format(strSqlSel2, "T_ENV_P_DRINK_SRC", "SECTION_CODE as POINT_CODE", "SECTION_NAME as POINT_NAME", "Surface");
                strSQLcount = string.Format(strSqlCount2, "T_ENV_P_DRINK_SRC", "SECTION_CODE", "SECTION_NAME", "Surface");

                break;
            case "EnvRain":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_RAIN", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_RAIN", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvDust":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_DUST", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_DUST", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvAir":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_AIR", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_AIR", "POINT_CODE", "POINT_NAME");

                break;
            case "AreaNoise":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_NOISE_AREA", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_NOISE_AREA", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvRoadNoise":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_NOISE_ROAD", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_NOISE_ROAD", "POINT_CODE", "POINT_NAME");

                break;
            case "FunctionNoise":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_NOISE_FUNCTION", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_NOISE_FUNCTION", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvMudRiver":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_MUD_RIVER", "SECTION_CODE as POINT_CODE", "SECTION_NAME as POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_MUD_RIVER", "SECTION_CODE", "SECTION_NAME");

                break;
            case "EnvPSoild":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_SOLID", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_SOLID", "POINT_CODE", "POINT_NAME");

                break;
            case "EnvSoil":
                strSQLSel = string.Format(strSqlSel1, "T_ENV_P_SOIL", "POINT_CODE", "POINT_NAME");
                strSQLcount = string.Format(strSqlCount1, "T_ENV_P_SOIL", "POINT_CODE", "POINT_NAME");

                break; 
            default:
                break;
        }
        
        if (strSQLSel.Length == 0)
            return "[]";
        
        DataTable dt = objLogic.SelectSQL_ByTable_forMobile(strSQLSel, intPageIndex, intPageSize);
        int intTotalCount = objLogic.GetSelectSQL_ResultCount_forMobile(strSQLcount);

        strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);
        
        return strJson;
    }

    private bool ifPointHasItem(DataTable dt,string strPoint,string[] arrItem)
    {
        bool isHas = false;
        
        foreach (string strItem in arrItem)
        {
            DataRow[] drData = dt.Select("POINT_NAME='" + strPoint + "' and ITEM_NAME='" + strItem + "'");
            if (drData.Length > 0)
                isHas =true;
        }
        return isHas;
    }

    //在数据集中获取指定列名的列值集合
    private string getDtColValus(DataTable dt,string strColName)
    {
        string strResults = ",";
        foreach (DataRow dr in dt.Rows)
        {
            if (!strResults.Contains("," + dr[strColName].ToString() + ","))
            {
                strResults += dr[strColName].ToString() + ",";
            }
        }
        strResults = strResults.TrimEnd(',');
        strResults = strResults.TrimStart(',');

        return strResults;
    }

    private DataTable getPointDt(string strID)
    {
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        TBaseCompanyPointLogic objPointLogic = new TBaseCompanyPointLogic();

        objPoint.COMPANY_ID = strID;
        //objPoint.IS_DEL = "0";
        objPoint.SORT_FIELD = "MONITOR_ID,POINT_TYPE";
        objPoint.SORT_TYPE = "asc";
        DataTable dt = objPointLogic.SelectByTable(objPoint);


        List<TSysDictVo> dtDict = new TSysDictLogic().GetDataDictListByType("Contract_Type");
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < dtDict.Count; i++)
            {
                if (dr["POINT_TYPE"].ToString() == dtDict[i].DICT_CODE)
                    dr["POINT_TYPE"] = dtDict[i].DICT_TEXT;
            }
        }

        List<TBaseMonitorTypeInfoVo> dtM = new TBaseMonitorTypeInfoLogic().SelectByObject(new TBaseMonitorTypeInfoVo { IS_DEL = "0" },0,0);
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < dtM.Count; i++)
            {
                if (dr["MONITOR_ID"].ToString() == dtM[i].ID)
                    dr["REMARK1"] = dtM[i].MONITOR_TYPE_NAME;
            }
        }
        
        dt.AcceptChanges();

        return dt;
    }
}