using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteAir;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_POLLUTE_AIR " + this.BuildWhereStatement(tEnvFillPolluteAir);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirVo Details(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR " + this.BuildWhereStatement(tEnvFillPolluteAir));
            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteAirVo> SelectByObject(TEnvFillPolluteAirVo tEnvFillPolluteAir, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR " + this.BuildWhereStatement(tEnvFillPolluteAir));
            return SqlHelper.ExecuteObjectList(tEnvFillPolluteAir, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteAirVo tEnvFillPolluteAir, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_POLLUTE_AIR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPolluteAir));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_AIR " + this.BuildWhereStatement(tEnvFillPolluteAir);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteAirVo SelectByObject(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_AIR " + this.BuildWhereStatement(tEnvFillPolluteAir);
            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPolluteAir, TEnvFillPolluteAirVo.T_ENV_FILL_POLLUTE_AIR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteAir, TEnvFillPolluteAirVo.T_ENV_FILL_POLLUTE_AIR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPolluteAir.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirVo tEnvFillPolluteAir_UpdateSet, TEnvFillPolluteAirVo tEnvFillPolluteAir_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteAir_UpdateSet, TEnvFillPolluteAirVo.T_ENV_FILL_POLLUTE_AIR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPolluteAir_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_POLLUTE_AIR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            string strSQL = "delete from T_ENV_FILL_POLLUTE_AIR ";
            strSQL += this.BuildWhereStatement(tEnvFillPolluteAir);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            StringBuilder sb = new StringBuilder(256);
            sb.Append(" select A.ID,A.POINT_NAME     from  T_ENV_P_POLLUTE A left join  T_ENV_P_POLLUTE_TYPE B on A.TYPE_ID=B.ID ");
            sb.Append(" WHERE B.TYPE_NAME='废气' AND A.YEAR='" + strYear + "' AND A.MONTH='" + strMonth + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }

        #region//构造填报表需要显示的信息 (QHD)
        /// <summary>
        /// 构造填报表需要显示的信息 
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("code", typeof(String));
            dt.Columns.Add("name", typeof(String));

            dr = dt.NewRow();
            dr["code"] = "YEAR";
            dr["name"] = "年份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "EnterPrise_Name";
            dr["name"] = "企业名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Season";
            dr["name"] = "季度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Times";
            dr["name"] = "次数";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "断面名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OQty";
            dr["name"] = "排污设备名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "PollutePer";
            dr["name"] = "排放设备类型";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "PolluteCalPer";
            dr["name"] = "生产产品";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Is_Standard";
            dr["name"] = "生产能力";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AirQty";
            dr["name"] = "生产能力单位";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MO_Date";
            dr["name"] = "投产日期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Fuel_Type";
            dr["name"] = "燃料类型";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Fuel_Qty";
            dr["name"] = "燃料年消耗量";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Fuel_Model";
            dr["name"] = "锅炉燃烧方式";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Fuel_Tech";
            dr["name"] = "低炭燃烧技术";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Is_Fuel";
            dr["name"] = "是否循环流化床锅炉";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Discharge_Way";
            dr["name"] = "排放规律";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MO_Hour_Qty";
            dr["name"] = "日生产小时数";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Load_Mode";
            dr["name"] = "工况负荷";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Point_Temp";
            dr["name"] = "测点温度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Is_Run";
            dr["name"] = "处理设施前实测浓度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Measured";
            dr["name"] = "处理设施前实测废气排放量";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Waste_Air_Qty";
            dr["name"] = "治理设施是否正常运行";
            dt.Rows.Add(dr);


            return dt;
        }
        #endregion

        #region//获取环境质量数据填报数据
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PolluteTypeTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            string PointIDs = "";
            string Columns = "";
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            bool b = UpdateFillDate(strWhere, ref PointIDs, SectionTable, PolluteTypeTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);
            if (b)
            {
                #region //根据点位表信息更新填报表
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
                    {
                        FillIDs += "'" + drMain["ID"].ToString() + "',";
                    }
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
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "排放上限", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "排放下限", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "排放单位", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "超标倍数", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "含氧量", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物实测浓度", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物折算浓度", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "浓度是否达标", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "废气排放量", typeof(string));
                    }
                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;
                    //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE,Up_Line,Down_Line,Uom,Standard,OQty,PollutePer,PolluteCalPer,Is_Standard,AirQty   from {0} where FILL_ID in({1})";
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
                            {
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放上限"] = itemValue[0]["Up_Line"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放下限"] = itemValue[0]["Down_Line"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放单位"] = itemValue[0]["Uom"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "超标倍数"] = itemValue[0]["Standard"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "含氧量"] = itemValue[0]["OQty"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物实测浓度"] = itemValue[0]["PollutePer"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物折算浓度"] = itemValue[0]["PolluteCalPer"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "浓度是否达标"] = itemValue[0]["Is_Standard"].ToString();
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "废气排放量"] = itemValue[0]["AirQty"].ToString();
                            }
                            else
                            {
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                            }
                        }
                    }
                }
                #endregion
            }
            return dtMain;
        }
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string SectionTable, string PolluteTypeTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            #region  //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME,C.ENTER_NAME,A.Sewerage_Name,A.Equipment_Name,A.MO_Name,A.MO_Capacity,A.MO_UOM,A.MO_Date,A.Fuel_Type,A.Fuel_Qty,A.Fuel_Model,A.Fuel_Tech,A.Is_Fuel,A.Discharge_Way,
                                    A.MO_Hour_Qty,A.Load_Mode,A.Point_Temp,A.Is_Run,A.Measured,A.Waste_Air_Qty
                              from {0} A left join T_ENV_P_POLLUTE_TYPE B on A.TYPE_ID=B.ID  left join T_ENV_P_ENTERINFO C on B.SATAIONS_ID=C.ID
                              where {1} and A.IS_DEL='0' and  B.TYPE_NAME='废气' ";
            sql = string.Format(sql, PointTable, strWhere);
            dtMain = ExecuteDataTable(sql); //查询点位信息
            #endregion
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                    //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    sql = "select ID from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        #region//没有则插入数据
                        FillID = GetSerialNumber(FillSerial);
                        sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,EnterPrise_Name,OQty,PollutePer,PolluteCalPer,Is_Standard,AirQty,MO_Date,Fuel_Type,Fuel_Qty,Fuel_Model,Fuel_Tech,Is_Fuel,Discharge_Way,MO_Hour_Qty,Load_Mode,Point_Temp,Is_Run,Measured,Waste_Air_Qty) values('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')";
                        sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), drMain["ENTER_NAME"].ToString(), drMain["Sewerage_Name"].ToString(), drMain["Equipment_Name"].ToString(), drMain["MO_Name"].ToString(), drMain["MO_Capacity"].ToString(), drMain["MO_UOM"].ToString(), drMain["MO_Date"].ToString(), drMain["Fuel_Type"].ToString(), drMain["Fuel_Qty"].ToString(), drMain["Fuel_Model"].ToString(), drMain["Fuel_Tech"].ToString(), drMain["Is_Fuel"].ToString(), drMain["Discharge_Way"].ToString(), drMain["MO_Hour_Qty"].ToString(), drMain["Load_Mode"].ToString(), drMain["Point_Temp"].ToString(), drMain["Is_Run"].ToString(), drMain["Measured"].ToString(), drMain["Waste_Air_Qty"].ToString());
                        list.Add(sql);
                        #endregion
                    }
                    #region  //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    StringBuilder sb = new StringBuilder(256);
                    sb.Append("select D.ID,D.ITEM_NAME from T_ENV_P_POLLUTE_ITEM A ");
                    sb.Append(" left join  T_ENV_P_POLLUTE B on A.POINT_ID=B.ID ");
                    sb.Append(" left join  T_ENV_P_POLLUTE_TYPE C on B.TYPE_ID=C.ID ");
                    sb.Append(" left join   T_BASE_ITEM_INFO D on A.ITEM_ID=D.ID ");
                    sb.Append(" where C.TYPE_NAME='废气' and B.is_del='0' and A.POINT_ID='" + pointid + "'");
                    dtAllItem = ExecuteDataTable(sb.ToString());
                    if (dtAllItem.Rows.Count > 0)
                    {
                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
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
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPolluteAir"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPolluteAir)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPolluteAir.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillPolluteAir.POINT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.ENTERPRISE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTERPRISE_CODE = '{0}'", tEnvFillPolluteAir.ENTERPRISE_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.ENTERPRISE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTERPRISE_NAME = '{0}'", tEnvFillPolluteAir.ENTERPRISE_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillPolluteAir.YEAR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillPolluteAir.MONTH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillPolluteAir.DAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillPolluteAir.HOUR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.SEASON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEASON = '{0}'", tEnvFillPolluteAir.SEASON.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.TIMES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TIMES = '{0}'", tEnvFillPolluteAir.TIMES.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.OQTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OQTY = '{0}'", tEnvFillPolluteAir.OQTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.POLLUTEPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POLLUTEPER = '{0}'", tEnvFillPolluteAir.POLLUTEPER.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.POLLUTECALPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POLLUTECALPER = '{0}'", tEnvFillPolluteAir.POLLUTECALPER.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.IS_STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_STANDARD = '{0}'", tEnvFillPolluteAir.IS_STANDARD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.AIRQTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIRQTY = '{0}'", tEnvFillPolluteAir.AIRQTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.MO_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_DATE = '{0}'", tEnvFillPolluteAir.MO_DATE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.FUEL_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_TYPE = '{0}'", tEnvFillPolluteAir.FUEL_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.FUEL_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_QTY = '{0}'", tEnvFillPolluteAir.FUEL_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.FUEL_MODEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_MODEL = '{0}'", tEnvFillPolluteAir.FUEL_MODEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.FUEL_TECH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_TECH = '{0}'", tEnvFillPolluteAir.FUEL_TECH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.IS_FUEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_FUEL = '{0}'", tEnvFillPolluteAir.IS_FUEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.DISCHARGE_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DISCHARGE_WAY = '{0}'", tEnvFillPolluteAir.DISCHARGE_WAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.MO_HOUR_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MO_HOUR_QTY = '{0}'", tEnvFillPolluteAir.MO_HOUR_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.LOAD_MODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOAD_MODE = '{0}'", tEnvFillPolluteAir.LOAD_MODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.POINT_TEMP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_TEMP = '{0}'", tEnvFillPolluteAir.POINT_TEMP.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.IS_RUN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_RUN = '{0}'", tEnvFillPolluteAir.IS_RUN.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.MEASURED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURED = '{0}'", tEnvFillPolluteAir.MEASURED.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.WASTE_AIR_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WASTE_AIR_QTY = '{0}'", tEnvFillPolluteAir.WASTE_AIR_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPolluteAir.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPolluteAir.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPolluteAir.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPolluteAir.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAir.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPolluteAir.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
