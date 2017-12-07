using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteWater;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_POLLUTE_WATER " + this.BuildWhereStatement(tEnvFillPolluteWater);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterVo Details(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER " + this.BuildWhereStatement(tEnvFillPolluteWater));
            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteWaterVo> SelectByObject(TEnvFillPolluteWaterVo tEnvFillPolluteWater, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER " + this.BuildWhereStatement(tEnvFillPolluteWater));
            return SqlHelper.ExecuteObjectList(tEnvFillPolluteWater, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterVo tEnvFillPolluteWater, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_POLLUTE_WATER {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPolluteWater));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteWater"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_WATER " + this.BuildWhereStatement(tEnvFillPolluteWater);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteWaterVo SelectByObject(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_WATER " + this.BuildWhereStatement(tEnvFillPolluteWater);
            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPolluteWater, TEnvFillPolluteWaterVo.T_ENV_FILL_POLLUTE_WATER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWater">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteWater, TEnvFillPolluteWaterVo.T_ENV_FILL_POLLUTE_WATER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPolluteWater.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWater_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteWater_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterVo tEnvFillPolluteWater_UpdateSet, TEnvFillPolluteWaterVo tEnvFillPolluteWater_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteWater_UpdateSet, TEnvFillPolluteWaterVo.T_ENV_FILL_POLLUTE_WATER_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPolluteWater_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_POLLUTE_WATER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            string strSQL = "delete from T_ENV_FILL_POLLUTE_WATER ";
            strSQL += this.BuildWhereStatement(tEnvFillPolluteWater);

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
            sb.Append(" WHERE B.TYPE_NAME='废水' AND A.YEAR='" + strYear + "' AND A.MONTH='" + strMonth + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }

        #region//获取环境质量数据填报数据
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable,string PolluteTypeTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            string PointIDs = "";
            string Columns = "";
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            bool b = UpdateFillDate(strWhere, ref PointIDs, SectionTable,PolluteTypeTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);
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
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物浓度是否达标", typeof(string));
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "废水处理设施进口浓度", typeof(string));
                    }
                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;
                    //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE,UpLine,DownLine,Uom,Standard,Is_Standard,Water_Per from {0} where FILL_ID in({1})";
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
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放上限"] = itemValue[0]["UpLine"].ToString(); //排放上限
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放下限"] = itemValue[0]["DownLine"].ToString(); //排放下限
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "排放单位"] = itemValue[0]["Uom"].ToString(); //排放单位
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "超标倍数"] = itemValue[0]["Standard"].ToString(); //超标倍数
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染物浓度是否达标"] = itemValue[0]["Is_Standard"].ToString(); //污染物浓度是否达标
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "废水处理设施进口浓度"] = itemValue[0]["Water_Per"].ToString(); //废水处理设施进口浓度
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
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string SectionTable,string PolluteTypeTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            #region  //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME,C.ENTER_NAME,a.Water_Code,a.Water_Name
                              from {0} a left join T_ENV_P_POLLUTE_TYPE B on a.TYPE_ID=B.ID  left join T_ENV_P_ENTERINFO C on B.SATAIONS_ID=C.ID
                              where {1} and a.IS_DEL='0' and  B.TYPE_NAME='废水' ";
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
                        sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,EnterPrise_Name,Water_Code,Water_Name) values('{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                        sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), drMain["ENTER_NAME"].ToString(), drMain["Water_Code"].ToString(), drMain["Water_Name"].ToString());
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
                    sb.Append(" where C.TYPE_NAME='废水' and B.is_del='0' and A.POINT_ID='" + pointid + "'");
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
            dr["name"] = "日期";
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
            dr["code"] = "Water_Code";
            dr["name"] = "受纳水体代码";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["code"] = "Water_Name";
            dr["name"] = "受纳水体名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WaterQty";
            dr["name"] = "废水排放量";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Is_Run";
            dr["name"] = "治理设施是否正常运行";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Load_Mode";
            dr["name"] = "工况负荷";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "In_Water_Qty";
            dr["name"] = "进口水量";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["code"] = "Is_Evaluate";
            dr["name"] = "是否参与评价";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region//计算上、下线、单位、超标倍数、污染物浓度是否达标
        public bool Calculate(TEnvFillPolluteWaterVo Fill_ID)
        {
            int count = 0;
            bool flag = false;
            decimal result = 0M;
            string sql = string.Empty;
            string unit = string.Empty;
            ArrayList list = new ArrayList();
            StringBuilder sb = new StringBuilder(256);
            sb.Append("select a.id,a.item_id,b.item_name,a.item_value  from T_ENV_FILL_POLLUTE_WATER_Item a  left join T_BASE_ITEM_INFO b on a.item_id=b.id ");
            sb.Append(" where  a.FILL_ID='" + Fill_ID.ID + "'");
            DataTable ItemDT = SqlHelper.ExecuteDataTable(sb.ToString());
            if (ItemDT.Rows.Count > 0)
            {
                foreach (DataRow dr in ItemDT.Rows)
                {
                    #region//计算
                    if (dr["item_name"].ToString().Equals("pH值"))
                    {
                        #region//PH的计算
                        count = 0;
                        StringBuilder sb1 = new StringBuilder(256);
                        sb1.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb1.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb1.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT1 = SqlHelper.ExecuteDataTable(sb1.ToString());
                        if (DT1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in DT1.Rows)
                            {
                                unit = dr1["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr1["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr1["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr1["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr1["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr1["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr1["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr1["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr1["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("五日生化需氧量"))
                    {
                        #region//五日生化需氧量
                        count = 0;
                        StringBuilder sb2 = new StringBuilder(256);
                        sb2.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb2.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb2.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT2 = SqlHelper.ExecuteDataTable(sb2.ToString());
                        if (DT2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in DT2.Rows)
                            {
                                unit = dr2["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr2["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr2["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr2["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr2["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr2["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr2["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr2["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr2["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("化学需氧量"))
                    {
                        #region//化学需氧量
                        count = 0;
                        StringBuilder sb3 = new StringBuilder(256);
                        sb3.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb3.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb3.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT3 = SqlHelper.ExecuteDataTable(sb3.ToString());
                        if (DT3.Rows.Count > 0)
                        {
                            foreach (DataRow dr3 in DT3.Rows)
                            {
                                unit = dr3["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr3["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr3["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr3["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr3["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr3["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr3["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr3["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr3["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("挥发酚"))
                    {
                        #region//挥发酚
                        count = 0;
                        StringBuilder sb4 = new StringBuilder(256);
                        sb4.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb4.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb4.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT4 = SqlHelper.ExecuteDataTable(sb4.ToString());
                        if (DT4.Rows.Count > 0)
                        {
                            foreach (DataRow dr4 in DT4.Rows)
                            {
                                unit = dr4["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr4["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr4["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr4["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr4["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr4["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr4["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='"+dr4["DISCHARGE_LOWER"].ToString()+"',UOM='" + dr4["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("氨氮") || dr["item_name"].ToString().Equals("氨氮（非离子氨）"))
                    {
                        #region//氨氮
                        count = 0;
                        StringBuilder sb5 = new StringBuilder(256);
                        sb5.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb5.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb5.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT5 = SqlHelper.ExecuteDataTable(sb5.ToString());
                        if (DT5.Rows.Count > 0)
                        {
                            foreach (DataRow dr5 in DT5.Rows)
                            {
                                unit = dr5["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr5["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr5["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr5["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr5["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr5["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr5["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='"+dr5["DISCHARGE_LOWER"].ToString()+"',UOM='" + dr5["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("硫化物"))
                    {
                        #region//硫化物
                        count = 0;
                        StringBuilder sb6 = new StringBuilder(256);
                        sb6.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb6.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb6.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT6 = SqlHelper.ExecuteDataTable(sb6.ToString());
                        if (DT6.Rows.Count > 0)
                        {
                            foreach (DataRow dr6 in DT6.Rows)
                            {
                                unit = dr6["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr6["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr6["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr6["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr6["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr6["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr6["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr6["DISCHARGE_LOWER"].ToString() +"',UOM='" + dr6["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("总磷"))
                    {
                        #region//总磷
                        count = 0;
                        StringBuilder sb7 = new StringBuilder(256);
                        sb7.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb7.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb7.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT7 = SqlHelper.ExecuteDataTable(sb7.ToString());
                        if (DT7.Rows.Count > 0)
                        {
                            foreach (DataRow dr7 in DT7.Rows)
                            {
                                unit = dr7["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr7["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr7["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr7["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr7["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr7["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr7["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr7["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr7["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("石油类"))
                    {
                        #region//石油类
                        count = 0;
                        StringBuilder sb8 = new StringBuilder(256);
                        sb8.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb8.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb8.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT8 = SqlHelper.ExecuteDataTable(sb8.ToString());
                        if (DT8.Rows.Count > 0)
                        {
                            foreach (DataRow dr8 in DT8.Rows)
                            {
                                unit = dr8["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr8["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr8["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr8["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr8["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr8["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr8["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr8["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr8["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("六价铬") || dr["item_name"].ToString().Equals("总铬") || dr["item_name"].ToString().Equals("铬"))
                    {
                        #region//六价铬
                        count = 0;
                        StringBuilder sb9 = new StringBuilder(256);
                        sb9.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb9.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb9.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT9 = SqlHelper.ExecuteDataTable(sb9.ToString());
                        if (DT9.Rows.Count > 0)
                        {
                            foreach (DataRow dr9 in DT9.Rows)
                            {
                                unit = dr9["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr9["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr9["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr9["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr9["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr9["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr9["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr9["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr9["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("总铜") || dr["item_name"].ToString().Equals("铜"))
                    {
                        #region//铜
                        count = 0;
                        StringBuilder sb10 = new StringBuilder(256);
                        sb10.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb10.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb10.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT10 = SqlHelper.ExecuteDataTable(sb10.ToString());
                        if (DT10.Rows.Count > 0)
                        {
                            foreach (DataRow dr10 in DT10.Rows)
                            {
                                unit = dr10["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr10["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr10["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr10["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr10["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr10["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr10["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr10["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr10["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("总汞") || dr["item_name"].ToString().Equals("汞"))
                    {
                        #region//汞
                        count = 0;
                        StringBuilder sb11 = new StringBuilder(256);
                        sb11.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb11.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb11.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT11= SqlHelper.ExecuteDataTable(sb11.ToString());
                        if (DT11.Rows.Count > 0)
                        {
                            foreach (DataRow dr11 in DT11.Rows)
                            {
                                unit = dr11["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr11["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr11["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr11["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr11["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr11["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr11["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr11["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr11["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    else if (dr["item_name"].ToString().Equals("氰化物"))
                    {
                        #region//氰化物
                        count = 0;
                        StringBuilder sb12 = new StringBuilder(256);
                        sb12.Append("SELECT a.DISCHARGE_UPPER,a.DISCHARGE_LOWER,a.UNIT,b.dict_text ");
                        sb12.Append(" FROM T_BASE_EVALUATION_CON_ITEM  a left join  T_SYS_DICT b on a.UNIT=b.dict_code ");
                        sb12.Append(" where b.DICT_TYPE = 'item_unit' and  a.ITEM_ID='" + dr["item_id"].ToString() + "'");
                        DataTable DT12 = SqlHelper.ExecuteDataTable(sb12.ToString());
                        if (DT12.Rows.Count > 0)
                        {
                            foreach (DataRow dr12 in DT12.Rows)
                            {
                                unit = dr12["dict_text"].ToString();//单位
                                if (!string.IsNullOrEmpty(dr12["DISCHARGE_UPPER"].ToString()) && !string.IsNullOrEmpty(dr12["DISCHARGE_LOWER"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(dr["item_value"].ToString()))
                                    {
                                        if (decimal.Parse(dr["item_value"].ToString()) >= decimal.Parse(dr12["DISCHARGE_LOWER"].ToString()) && decimal.Parse(dr["item_value"].ToString()) <= decimal.Parse(dr12["DISCHARGE_UPPER"].ToString()))
                                        {
                                            result = decimal.Parse(dr["item_value"].ToString()) / decimal.Parse(dr12["DISCHARGE_UPPER"].ToString());
                                            sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UPLINE='" + dr12["DISCHARGE_UPPER"].ToString() + "',DOWNLINE='" + dr12["DISCHARGE_LOWER"].ToString() + "',UOM='" + dr12["dict_text"].ToString() + "',Is_Standard='是',Standard='" + decimal.Round(result, 2).ToString() + "'  WHERE ID='" + dr["id"].ToString() + "'";
                                            count = 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                sql = "update T_ENV_FILL_POLLUTE_WATER_Item set UOM='" + unit + "',Is_Standard='否' WHERE ID='" + dr["id"].ToString() + "'";
                            }
                        }
                        #endregion
                    }
                    #endregion
                    list.Add(sql);
                }
            }
            if (list.Count > 0)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(list);
            }
            return flag;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPolluteWater"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPolluteWater)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPolluteWater.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillPolluteWater.POINT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.ENTERPRISE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTERPRISE_CODE = '{0}'", tEnvFillPolluteWater.ENTERPRISE_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.ENTERPRISE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTERPRISE_NAME = '{0}'", tEnvFillPolluteWater.ENTERPRISE_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillPolluteWater.YEAR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillPolluteWater.MONTH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillPolluteWater.DAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillPolluteWater.HOUR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.SEASON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEASON = '{0}'", tEnvFillPolluteWater.SEASON.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.TIMES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TIMES = '{0}'", tEnvFillPolluteWater.TIMES.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.WATERQTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATERQTY = '{0}'", tEnvFillPolluteWater.WATERQTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.IS_RUN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_RUN = '{0}'", tEnvFillPolluteWater.IS_RUN.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.LOAD_MODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOAD_MODE = '{0}'", tEnvFillPolluteWater.LOAD_MODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.IN_WATER_QTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IN_WATER_QTY = '{0}'", tEnvFillPolluteWater.IN_WATER_QTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.IS_EVALUATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_EVALUATE = '{0}'", tEnvFillPolluteWater.IS_EVALUATE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPolluteWater.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPolluteWater.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPolluteWater.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPolluteWater.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPolluteWater.REMARK5.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.WATER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_NAME = '{0}'", tEnvFillPolluteWater.WATER_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWater.WATER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_CODE = '{0}'", tEnvFillPolluteWater.WATER_CODE.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
