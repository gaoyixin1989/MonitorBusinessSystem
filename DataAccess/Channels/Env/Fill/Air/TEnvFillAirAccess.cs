using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Fill.Air;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Air
{
    /// <summary>
    /// 功能：环境空气数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillAirAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIR " + this.BuildWhereStatement(tEnvFillAir);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirVo Details(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR " + this.BuildWhereStatement(tEnvFillAir));
            return SqlHelper.ExecuteObject(new TEnvFillAirVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirVo> SelectByObject(TEnvFillAirVo tEnvFillAir, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIR " + this.BuildWhereStatement(tEnvFillAir));
            return SqlHelper.ExecuteObjectList(tEnvFillAir, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirVo tEnvFillAir, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAir));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = "select * from T_ENV_FILL_AIR " + this.BuildWhereStatement(tEnvFillAir);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <returns></returns>
        public TEnvFillAirVo SelectByObject(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = "select * from T_ENV_FILL_AIR " + this.BuildWhereStatement(tEnvFillAir);
            return SqlHelper.ExecuteObject(new TEnvFillAirVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAir, TEnvFillAirVo.T_ENV_FILL_AIR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAir, TEnvFillAirVo.T_ENV_FILL_AIR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAir.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirVo tEnvFillAir_UpdateSet, TEnvFillAirVo tEnvFillAir_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAir_UpdateSet, TEnvFillAirVo.T_ENV_FILL_AIR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAir_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAirVo tEnvFillAir)
        {
            string strSQL = "delete from T_ENV_FILL_AIR ";
            strSQL += this.BuildWhereStatement(tEnvFillAir);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        

        #endregion

        #region// 构造填报表需要显示的信息
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
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "HOUR";
            //dr["name"] = "时";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "MINUTE";
            //dr["name"] = "分";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "TEMPERATRUE";
            //dr["name"] = "气温";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "PRESSURE";
            //dr["name"] = "气压";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "WIND_SPEED";
            //dr["name"] = "风速";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "WIND_DIRECTION";
            //dr["name"] = "风向";
            //dt.Rows.Add(dr);
             
            //dr = dt.NewRow();
            //dr["code"] = "API_CODE";
            //dr["name"] = "API指数";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AQI_CODE";
            dr["name"] = "空气质量指数";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AIR_LEVEL";
            dr["name"] = "空气质量级别";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AIR_STATE";
            dr["name"] = "空气质量状况";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MAIN_AIR";
            dr["name"] = "主要污染物";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region//获取环境空气数据
        /// <summary>
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow , string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string Columns = "";
            string PointIDs = "";
            bool b = true;
            b = UpdateFillDateEx(strWhere, ref PointIDs, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);          //根据点位表信息更新填报表
            if (b)
            {
                #region//获取填报表信息
                foreach (DataRow drShow in dtShow.Rows)
                {
                    Columns += drShow[0].ToString() + " " + FillTable + "@" + drShow[0].ToString() + "@" + drShow[1].ToString() + ",";
                }
                sql = "select ID,{0} from {1} where POINT_ID in({2})";
                #endregion
                sql = string.Format(sql, Columns.TrimEnd(','), FillTable, PointIDs);
                dtMain = ExecuteDataTable(sql);
                if (dtMain.Rows.Count > 0)
                {
                    #region //查询要填报的监测项
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
                    #endregion
                    dtAllItem = ExecuteDataTable(sql);

                    //把监测项拼接在表格中
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {

                        if (drAllItem["ITEM_NAME"].ToString().Equals("氮氧化物") || drAllItem["ITEM_NAME"].ToString().Equals("一氧化氮") || drAllItem["ITEM_NAME"].ToString().Equals("臭氧"))
                        {
                            dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));//把监测项目添加到的dtMain中
                        }
                        else
                        {
                            dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));//把监测项目添加到的dtMain中
                            dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数", typeof(string));//把监测项目对应的空气质量分指数也添加到dtMain中
                        }
                       
                    }

                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;

                    #region //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE,IAQI from {0} where FILL_ID in({1})";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtFillItem = ExecuteDataTable(sql);

                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                        #region //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            int flag = 1;
                            if (drAllItem["ITEM_NAME"].ToString().Equals("氮氧化物") || drAllItem["ITEM_NAME"].ToString().Equals("一氧化氮") || drAllItem["ITEM_NAME"].ToString().Equals("臭氧"))
                            {
                                flag = 0;
                            }

                            string itemId = drAllItem["ID"].ToString(); //监测项ID
                            var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值
                            if (itemValue.Count > 0)
                            {
                                if (flag == 0)
                                {
                                    drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                                }
                                else
                                {
                                    drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                                    drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString() + "污染分指数"] = itemValue[0]["IAQI"].ToString(); //填入监测项值对应的污染分指数的值
                                }
                            }
                           
                        }
                        #endregion
                    }
                    #endregion
                }
            }

            return dtMain;
        }

        /// <summary>
        /// 根据点位表信息更新填报表 create by weilin 2013-09-05
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <returns></returns>
        public bool UpdateFillDateEx(string strWhere, ref string PointIDs, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtFillItem = new DataTable();
            DataRow[] drFillItem;
            DataTable dtFill = new DataTable();
            DataRow[] drFill;
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            int Days = 0;

            //获取填报主表与子表的当前序列号与长度
            DataTable dt = new DataTable();
            int FillNumber = 0;
            int FillLength = 9;
            int FillINumber = 0;
            int FillILength = 9;
            sql = "select SERIAL_NUMBER,LENGTH from T_SYS_SERIAL where SERIAL_CODE='" + FillSerial + "'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                FillNumber = int.Parse(dt.Rows[0]["SERIAL_NUMBER"].ToString());
                FillLength = int.Parse(dt.Rows[0]["LENGTH"].ToString());
            }
            sql = "select SERIAL_NUMBER,LENGTH from T_SYS_SERIAL where SERIAL_CODE='" + FillISerial + "'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                FillINumber = int.Parse(dt.Rows[0]["SERIAL_NUMBER"].ToString());
                FillILength = int.Parse(dt.Rows[0]["LENGTH"].ToString());
            }

            //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                            from {0} a
                            where {1} and a.IS_DEL='0'";

            sql = string.Format(sql, PointTable, strWhere);

            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    Days = DateTime.DaysInMonth(int.Parse(drMain["YEAR"].ToString()), int.Parse(drMain["MONTH"].ToString()));
                    PointIDs += drMain["POINT_ID"].ToString() + ",";

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

                    //获取某测点的填报数据
                    sql = "select ID,DAY from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    dtFill = ExecuteDataTable(sql);//查询填报

                    for (int i = 0; i < Days; i++)
                    {
                        //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                        drFill = dtFill.Select("DAY='" + (i + 1).ToString() + "'");

                        if (drFill.Length > 0)
                        {
                            FillID = drFill[0]["ID"].ToString();
                        }
                        else
                        {
                            FillNumber++;
                            FillID = (FillNumber).ToString().PadLeft(FillLength, '0');

                            sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,DAY,AQI_CODE) values('{1}','{2}','{3}','{4}','{5}','{6}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), (i + 1).ToString(), "无效天");

                            list.Add(sql);
                        }

                        sql = "select ID,ITEM_ID from {0} where FILL_ID='{1}'";
                        sql = string.Format(sql, FillITable, FillID);
                        dtFillItem = ExecuteDataTable(sql);
                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                            drFillItem = dtFillItem.Select("ITEM_ID='" + drAllItem["ID"].ToString() + "'");

                            if (drFillItem.Length == 0)
                            {
                                FillINumber++;
                                FillIID = (FillINumber).ToString().PadLeft(FillILength, '0');
                                sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                                sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                                list.Add(sql);
                            }
                        }
                    }

                }
                //更新序列号表
                sql = "update T_SYS_SERIAL set SERIAL_NUMBER='" + FillNumber + "' where SERIAL_CODE='" + FillSerial + "'";
                list.Add(sql);
                sql = "update T_SYS_SERIAL set SERIAL_NUMBER='" + FillINumber + "' where SERIAL_CODE='" + FillISerial + "'";
                list.Add(sql);
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        /// <summary>
        /// 根据点位表信息更新填报表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <returns></returns>
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial )
        {
            string sql = "";
            bool flag = false;
            ArrayList arlist = new ArrayList();
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号 

            #region //获取垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";
            #endregion

            sql = string.Format(sql, PointTable, strWhere);
            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";

                    #region//判断填报表中是否存在在相应的垂线/测点数据，如果没有则插入数据
                    sql = "select ID from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    #endregion
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();

                        #region //查询每个点位要监测的监测项
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
                        #endregion
                        sql = string.Format(sql, ItemTable, pointid);
                        dtAllItem = ExecuteDataTable(sql);
                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                            sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                            sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                            dtTemp = ExecuteDataTable(sql);
                            if (dtTemp.Rows.Count == 0)
                            {
                                #region//根据点位ID查出点位在填报的数量
                                sql = "select count(*) as count from {0} where POINT_ID='{1}'";
                                sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                                dtTemp = ExecuteDataTable(sql);
                                if (dtTemp.Rows.Count > 0)
                                {
                                    for (int i = 1; i <= int.Parse(dtTemp.Rows[0][0].ToString()); i++)
                                    {
                                        FillIID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                                        sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                                        arlist.Add(sql);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region //判断月份
                        ArrayList Sqllist = new ArrayList();
                        if (drMain["MONTH"].ToString().Equals("1") || drMain["MONTH"].ToString().Equals("3") || drMain["MONTH"].ToString().Equals("5") || drMain["MONTH"].ToString().Equals("7") || drMain["MONTH"].ToString().Equals("8") || drMain["MONTH"].ToString().Equals("10") || drMain["MONTH"].ToString().Equals("12"))
                        {
                            Sqllist = this.RntListDay(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 31);
                        }
                        else if (drMain["MONTH"].ToString().Equals("4") || drMain["MONTH"].ToString().Equals("6") || drMain["MONTH"].ToString().Equals("9") || drMain["MONTH"].ToString().Equals("11"))
                        {
                            Sqllist = this.RntListDay(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 30);
                        }
                        else if (drMain["MONTH"].ToString().Equals("2"))
                        {
                            if (((int.Parse(drMain["YEAR"].ToString()) % 4 == 0) && (int.Parse(drMain["YEAR"].ToString()) % 100 != 0)) || (int.Parse(drMain["YEAR"].ToString()) % 400 == 0))
                            {
                                Sqllist = this.RntListDay(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 29);//闰年
                            }
                            else 
                            {
                                Sqllist = this.RntListDay(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 28);//平年
                            }
                        }
                        list.Add(Sqllist);
                        #endregion
                    }
                }
            }
            PointIDs = PointIDs.TrimEnd(',');

            #region//创建当前月的每一天数据
            if (list.Count == 0)
            {
                flag = true; //list.count==0,说明数据库里有数据，不用插入数据；或者程序报错；
            }
            else
            {
                foreach (ArrayList lists in list)
                {
                    flag = SqlHelper.ExecuteSQLByTransaction(lists);
                }
            }
            #endregion

            #region//有填报没有项目的填报项目
            if (arlist.Count > 0)
            { flag = SqlHelper.ExecuteSQLByTransaction(arlist); }
            #endregion
            return flag;
        }

        #region//环境空气(天)的数据
        /// <summary>
        /// 环境空气(天)的数据
        /// </summary>
        /// <param name="FillID"></param>
        /// <param name="FillIID"></param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="POINT_ID">点位ID</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        private ArrayList RntListDay(string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string POINT_ID, string year, string month, int DayCount)
        {
            string FillID = string.Empty;
            string FillIID = string.Empty;
            string sql = string.Empty;
            string pointid = string.Empty;
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtAllItem = new DataTable();

            #region//每天的数据
            for (int day = 1; day <= DayCount; day++)
            {
                FillID = GetSerialNumber(FillSerial);
                sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,DAY) values('{1}','{2}','{3}','{4}','{5}')";
                sql = string.Format(sql, FillTable, FillID, POINT_ID, year, month, day.ToString());
                list.Add(sql);

                #region//查询每个点位要监测的监测项
                pointid = POINT_ID;
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
                #endregion

                dtAllItem = ExecuteDataTable(sql);
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

            return list;
        }
        #endregion

        #endregion

        #region//根据监测值计算出空气分指数、空气指数、空气质量级别、空气质量状况和主要污染物
        /// <summary>
        /// 根据监测值计算出空气分指数、空气指数、空气质量级别、空气质量状况和主要污染物
        /// </summary>
        /// <param name="Fill_ID">环境空气的行ID</param>
        /// <param name="ItemVaule">监测值</param>
        /// <param name="ItemName">监测值得列名(规则:表明@监测值ID,"T_ENV_FILL_DUST_ITEM@000000232";)</param>
        public bool UpdateAirValue(string Fill_ID, string ItemVaule, string ItemName)
        {
            decimal CalculateResult = -1;//污染物的值
            ArrayList list = new ArrayList();
            string[] Arry_Colomn_Name = ItemName.Split('@');
            string Item_Name = Arry_Colomn_Name[0].ToString();//表明
            if (!Item_Name.Contains("ITEM"))
            {
                #region//更新填报
                string Get_Colomn_Name = Arry_Colomn_Name[1].ToString();//列名
                StringBuilder sb = new StringBuilder(256);
                sb.Append("update " + Item_Name + " set " + Get_Colomn_Name + "='" + ItemVaule + "' where ID='" + Fill_ID + "'");
                list.Add(sb.ToString());
                #endregion
            }
            else
            {
                #region//更新填报的监测项目

                #region// //根据监测项目ID查询监测项目管理的监测项目名称
                string Item_ID = Arry_Colomn_Name[1].ToString();//监测项目ID
                string strItemValue = "select ITEM_NAME from T_BASE_ITEM_INFO  where ID='" + Item_ID + "'";
                #endregion

                DataTable ItemValueDT = ExecuteDataTable(strItemValue);
                if (ItemValueDT.Rows.Count > 0)
                {
                    if (ItemValueDT.Rows[0][0].ToString().Equals("二氧化硫") || ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("SO2"))//按天计算
                    {
                        #region//二氧化硫的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnSo2ValueList = this.GetSO2PolluctionLimitValue(decimal.Parse(ItemVaule));//获取S02的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnSo2ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Equals("二氧化氮") || ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("NO2"))//按天计算
                    {
                        #region//二氧化氮的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnNo2ValueList = this.GetNO2PolluctionLimitValue(decimal.Parse(ItemVaule));//获取No2的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnNo2ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Equals("氮氧化物") || ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("NOx"))//按天计算
                    {
                        #region//氮氧化物的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnNOxValueList = this.GetNOxPolluctionLimitValue(decimal.Parse(ItemVaule));//获取No2的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnNOxValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Equals("一氧化氮") || ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("NO"))//按天计算
                    {
                        #region//一氧化氮的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                           ArrayList RtnNoValueList = this.GetNOPolluctionLimitValue(decimal.Parse(ItemVaule));//获取No2的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnNoValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Equals("一氧化碳") || ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("CO"))//按天计算
                    {
                        #region//一氧化碳的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnCoValueList = this.GetCOPolluctionLimitValue(decimal.Parse(ItemVaule));//获取Co的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnCoValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("PM10"))//颗粒物为10微克时，代表PM10;颗粒物为10微克时，代表PM2.5
                    {
                        #region//PM10的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnPM10ValueList = this.GetPM10PolluctionLimitValue(decimal.Parse(ItemVaule));//获取Co的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnPM10ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().ToUpper().Equals("PM2.5"))//颗粒物为10微克时，代表PM10;颗粒物为10微克时，代表PM2.5
                    {
                        #region//PM2.5的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnPM25ValueList = this.GetPM25PolluctionLimitValue(decimal.Parse(ItemVaule));//获取Co的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnPM25ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Contains("臭氧"))
                    {
                        #region//臭氧最大1小时的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnO31ValueList = this.GetO31PolluctionLimitValue(decimal.Parse(ItemVaule));//获取Co的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnO31ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }
                    else if (ItemValueDT.Rows[0][0].ToString().Contains("臭氧最大8小时"))
                    {
                        #region//臭氧最大8小时的计算
                        if (!string.IsNullOrEmpty(ItemVaule) && decimal.Parse(ItemVaule) >= 0)
                        {
                            ArrayList RtnO38ValueList = this.GetO38PolluctionLimitValue(decimal.Parse(ItemVaule));//获取Co的污染物的浓度限制值和空气质量分指数的值
                            CalculateResult = this.CalculateResult(RtnO38ValueList, ItemVaule);//获取计算结果
                        }
                        #endregion
                    }

                    #region//更新字段值
                    if (CalculateResult >= 0)
                    {
                        int i = this.UpdateItemValue(Item_Name, ItemVaule, CalculateResult.ToString(), Item_ID, Fill_ID);//更新监测值和分指数
                        if (i > 0)
                        {
                            this.UpdateAir(Fill_ID);//更新空气质量指数、空气质量级别、空气质量状况和首要污染物
                        }
                    }
                    #endregion
                }
                #endregion
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        #region//计算结果
        public decimal CalculateResult(ArrayList list, string ItemValue)
        {
            decimal FinallyResult = -1;
            decimal MinBPValue = decimal.Parse(list[0].ToString());//最小污染物分指数的值：BP（L）
            decimal MaxBPValue = decimal.Parse(list[1].ToString());//最大污染物分指数的值：BP（H）
            decimal MinAirValue = decimal.Parse(list[2].ToString());//最小空气质量分指数的值:IAQL（L）
            decimal MaxAirValue = decimal.Parse(list[3].ToString());//最大空气质量分指数的值:IAQL（H）
            if (MinBPValue >= 0 && MaxBPValue > 0 && MinAirValue >= 0 && MaxAirValue >= 0)
            {
                decimal RtnAirValue = MaxAirValue - MinAirValue;//(最大空气质量分指数的值-最小空气质量分指数的值）
                decimal RtnPolluteValue = MaxBPValue - MinBPValue;//(最大污染物分指数的值-最小污染物分指数的值)
                decimal restItemValue = decimal.Parse(ItemValue) - MinBPValue;//(监测值-最小污染物分指数的值)
                decimal DivResult = (RtnAirValue / RtnPolluteValue);
                decimal Result = DivResult * restItemValue + MinAirValue;
                FinallyResult = decimal.Round(Result,2);//转换成两位小数
            }
            return FinallyResult;
        }
        #endregion

        #region//更新监测值和分指数
        private int  UpdateItemValue(string Item_Name, string ItemVaule, string FinallyResult, string Item_ID, string Fill_ID)
        {
            StringBuilder sb = new StringBuilder(256);
            sb.Append("update " + Item_Name + " set ITEM_VALUE='" + ItemVaule + "',IAQI='" + FinallyResult.ToString() + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'");
            return  ExecuteNonQuery(sb.ToString());
        }
        #endregion

        #region//更新空气质量指数、空气质量级别、空气质量状况和首要污染物
        private int UpdateAir(string Fill_ID)
        {
                int update =-1;

            #region//根据填报监测项目和监测项目查询，空气指数和主要污染源(臭氧1小时不参与统计)
            StringBuilder strMaxValue = new StringBuilder(5000);
            strMaxValue.Append("select Convert(decimal(9,2),a.IAQI),b.ITEM_NAME ");
            strMaxValue.Append(" from T_ENV_FILL_AIR_Item a left join T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID ");
            strMaxValue.Append(" where a.FILL_ID='" + Fill_ID + "' and b.ITEM_NAME not like '%1小时%'");
            strMaxValue.Append(" order by Convert(decimal(9,2),a.IAQI) desc");
            #endregion

            DataTable GetMaxValueDT = ExecuteDataTable(strMaxValue.ToString());
            if (GetMaxValueDT.Rows.Count > 0)
            {
                string strMaxAQI = "";
                for (int i = 0; i < GetMaxValueDT.Rows.Count; i++)
                {
                    if (GetMaxValueDT.Rows[i][0].ToString().Length > 0 && decimal.Parse(GetMaxValueDT.Rows[i][0].ToString()) > (decimal.Parse(strMaxAQI == "" ? "0" : strMaxAQI)))
                    {
                        strMaxAQI = GetMaxValueDT.Rows[i][0].ToString();
                    }
                    if (string.IsNullOrEmpty(GetMaxValueDT.Rows[i][0].ToString()))
                    {
                        if (strMaxAQI == "" || decimal.Parse(strMaxAQI) <= 100)
                            strMaxAQI = "";
                    }
                }
                if (strMaxAQI.Length > 0)
                {
                    ArrayList Rtnlist = this.GetAirGrade(decimal.Parse(GetMaxValueDT.Rows[0][0].ToString()));//根据空气指数，获取空气质量级别和空气质量状况

                    #region//更新空气质量指数、空气质量级别、空气质量状况和首要污染物
                    StringBuilder StrUpdate = new StringBuilder(5000);
                    StrUpdate.Append("update T_ENV_FILL_AIR set AQI_CODE='" + GetMaxValueDT.Rows[0][0].ToString() + "',AIR_LEVEL='" + Rtnlist[0].ToString() + "',AIR_STATE='" + Rtnlist[1].ToString() + "',MAIN_AIR='" + GetMaxValueDT.Rows[0][1].ToString() + "' ");
                    StrUpdate.Append(" where ID='" + Fill_ID + "'");
                    #endregion

                    update = ExecuteNonQuery(StrUpdate.ToString());
                }
                else
                {
                    StringBuilder StrUpdate = new StringBuilder(5000);
                    StrUpdate.Append("update T_ENV_FILL_AIR set AQI_CODE='无效天',AIR_LEVEL='',AIR_STATE='',MAIN_AIR='' ");
                    StrUpdate.Append(" where ID='" + Fill_ID + "'");

                    update = ExecuteNonQuery(StrUpdate.ToString());
                }
            }
            return update;
        }
        #endregion

        #region//获取空气质量级别和空气质量状况
        public ArrayList GetAirGrade(decimal  AirCount)//空气质量指数
        {
            ArrayList list = new ArrayList();
            string Airgrade = string.Empty;//空气质量指数级别
            string AirType = string.Empty;//空气质量指数类别
            if (AirCount >= 0M && AirCount <= 50M)
            {
                Airgrade = "一级"; AirType = "优";
            }
            else if (AirCount > 51M && AirCount <= 100M)
            {
                Airgrade = "二级"; AirType = "良";
            }
            else if (AirCount >101M && AirCount <= 150M)
            {
                Airgrade = "三级"; AirType = "轻度污染";
            }
            else if (AirCount > 151M && AirCount <= 200M)
            {
                Airgrade = "四级"; AirType = "中度污染";
            }
            else if (AirCount > 201M && AirCount <= 300M)
            {
                Airgrade = "五级"; AirType = "重度污染";
            }
            else if (AirCount > 301M)
            {
                Airgrade = "六级"; AirType = "严重污染";
            }
            list.Add(Airgrade);
            list.Add(AirType);
            return list;
        }
        #endregion

        #region//获取S02的污染物的浓度限制值
        public ArrayList GetSO2PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 50M)
            {
                MinValue = 0M; MaxValue = 50M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue >50M && ItemValue <= 150M)
            {
                MinValue = 50M; MaxValue = 150M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 150M && ItemValue <= 475M)
            {
                MinValue = 150M; MaxValue = 475M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 475M && ItemValue <= 800M)
            {
                MinValue = 475M; MaxValue = 800M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 800M && ItemValue <= 1600M)
            {
                MinValue = 800M; MaxValue = 1600M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 1600M && ItemValue <= 2100M)
            {
                MinValue = 1600M; MaxValue = 2100M;
                MinAirValue =300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 2100M && ItemValue <= 2620M)
            {
                MinValue = 2100M; MaxValue = 2620M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取NOx的污染物的浓度限制值
        public ArrayList GetNOxPolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 40M)
            {
                MinValue = 0M; MaxValue = 20M;
                MinAirValue = 0M; MaxAirValue = 20M;
            }
            else if (ItemValue > 40M && ItemValue <= 80M)
            {
                MinValue = 20M; MaxValue = 40M;
                MinAirValue = 20M; MaxAirValue = 40M;
            }
            else if (ItemValue > 80M && ItemValue <= 180M)
            {
                MinValue = 40M; MaxValue = 80M;
                MinAirValue = 40M; MaxAirValue = 80M;
            }
            else if (ItemValue > 180M && ItemValue <= 280M)
            {
                MinValue = 80M; MaxValue = 140M;
                MinAirValue = 80M; MaxAirValue = 140M;
            }
            else if (ItemValue > 280M && ItemValue <= 565M)
            {
                MinValue = 280M; MaxValue = 565M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 565M && ItemValue <= 750M)
            {
                MinValue = 565M; MaxValue = 750M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 750M && ItemValue <= 940M)
            {
                MinValue = 750M; MaxValue = 940M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取NO2的污染物的浓度限制值
        public ArrayList GetNO2PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 40M)
            {
                MinValue = 0M; MaxValue = 40M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 40M && ItemValue <= 80M)
            {
                MinValue = 40M; MaxValue = 80M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 80M && ItemValue <= 180M)
            {
                MinValue = 80M; MaxValue = 180M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 180M && ItemValue <= 280M)
            {
                MinValue = 180M; MaxValue = 280M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 280M && ItemValue <= 565M)
            {
                MinValue = 280M; MaxValue = 565M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 565M && ItemValue <= 750M)
            {
                MinValue = 565M; MaxValue = 750M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 750M && ItemValue <= 940M)
            {
                MinValue = 750M; MaxValue = 940M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取NO的污染物的浓度限制值
        public ArrayList GetNOPolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 40M)
            {
                MinValue = 0M; MaxValue = 40M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 40M && ItemValue <= 80M)
            {
                MinValue = 40M; MaxValue = 80M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 80M && ItemValue <= 180M)
            {
                MinValue = 80M; MaxValue = 180M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 180M && ItemValue <= 280M)
            {
                MinValue = 180M; MaxValue = 280M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 280M && ItemValue <= 565M)
            {
                MinValue = 280M; MaxValue = 565M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 565M && ItemValue <= 750M)
            {
                MinValue = 565M; MaxValue = 750M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 750M && ItemValue <= 940M)
            {
                MinValue = 750M; MaxValue = 940M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取CO的污染物的浓度限制值
        public ArrayList GetCOPolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 2M)
            {
                MinValue = 0M; MaxValue = 2M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 2M && ItemValue <= 4M)
            {
                MinValue = 2M; MaxValue = 4M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 4M && ItemValue <= 14M)
            {
                MinValue = 4M; MaxValue = 14M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 14M && ItemValue <= 24M)
            {
                MinValue = 14M; MaxValue = 24M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 24M && ItemValue <= 36M)
            {
                MinValue = 24M; MaxValue = 36M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 36M && ItemValue <= 48M)
            {
                MinValue = 36M; MaxValue = 48M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 48M && ItemValue <= 60M)
            {
                MinValue = 48M; MaxValue = 60M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取PM10的污染物的浓度限制值
        public ArrayList GetPM10PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 50M)
            {
                MinValue = 0M; MaxValue = 50M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 50M && ItemValue <= 150M)
            {
                MinValue = 50M; MaxValue = 150M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 150M && ItemValue <= 250M)
            {
                MinValue = 150M; MaxValue = 250M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 250M && ItemValue <= 350M)
            {
                MinValue = 250M; MaxValue = 350M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 350M && ItemValue <= 420M)
            {
                MinValue = 350M; MaxValue = 420M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 420M && ItemValue <= 500M)
            {
                MinValue = 420M; MaxValue = 500M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 500M && ItemValue <= 600M)
            {
                MinValue = 500M; MaxValue = 600M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取PM2.5的污染物的浓度限制值
        public ArrayList GetPM25PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 35M)
            {
                MinValue = 0M; MaxValue = 35M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 35M && ItemValue <= 75M)
            {
                MinValue = 35M; MaxValue = 75M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 75M && ItemValue <= 115M)
            {
                MinValue = 75M; MaxValue = 115M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 115M && ItemValue <= 150M)
            {
                MinValue = 115M; MaxValue = 150M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 150M && ItemValue <= 250M)
            {
                MinValue = 150M; MaxValue = 250M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 250M && ItemValue <= 350M)
            {
                MinValue = 250M; MaxValue = 350M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 350M && ItemValue <= 500M)
            {
                MinValue = 48M; MaxValue = 500M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取臭氧最大1小时的污染物的浓度限制值
        private ArrayList GetO31PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 50M)
            {
                MinValue = 0M; MaxValue = 50M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 50M && ItemValue <= 150M)
            {
                MinValue = 50M; MaxValue = 150M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 150M && ItemValue <= 250M)
            {
                MinValue = 150M; MaxValue = 250M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 250M && ItemValue <= 350M)
            {
                MinValue = 250M; MaxValue = 350M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 350M && ItemValue <= 420M)
            {
                MinValue = 350M; MaxValue = 420M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            else if (ItemValue > 420M && ItemValue <= 500M)
            {
                MinValue = 420M; MaxValue = 500M;
                MinAirValue = 300M; MaxAirValue = 400M;
            }
            else if (ItemValue > 500M && ItemValue <= 600M)
            {
                MinValue = 500M; MaxValue = 600M;
                MinAirValue = 400M; MaxAirValue = 500M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #region//获取臭氧最大8小时的污染物的浓度限制值
        public ArrayList GetO38PolluctionLimitValue(decimal ItemValue)
        {
            ArrayList list = new ArrayList();
            decimal MinValue = -1; decimal MaxValue = -1;
            decimal MinAirValue = -1; decimal MaxAirValue = -1;
            if (ItemValue >= 0M && ItemValue <= 100M)
            {
                MinValue = 0M; MaxValue = 100M;
                MinAirValue = 0M; MaxAirValue = 50M;
            }
            else if (ItemValue > 100M && ItemValue <= 160M)
            {
                MinValue = 100M; MaxValue = 160M;
                MinAirValue = 50M; MaxAirValue = 100M;
            }
            else if (ItemValue > 160M && ItemValue <= 215M)
            {
                MinValue = 160M; MaxValue = 215M;
                MinAirValue = 100M; MaxAirValue = 150M;
            }
            else if (ItemValue > 215M && ItemValue <= 265M)
            {
                MinValue = 215M; MaxValue = 265M;
                MinAirValue = 150M; MaxAirValue = 200M;
            }
            else if (ItemValue > 265M && ItemValue <= 800M)
            {
                MinValue = 265M; MaxValue = 800M;
                MinAirValue = 200M; MaxAirValue = 300M;
            }
            list.Add(MinValue);
            list.Add(MaxValue);
            list.Add(MinAirValue);
            list.Add(MaxAirValue);
            return list;
        }
        #endregion

        #endregion

        #region//Excel导出前更新
        /// <param name="year">当前年</param>
        /// <param name="month">当前月</param>
        /// <param name="GetItem_Name">填报监测项目表明</param>
        public int BeforeOutUpdate(string year, string month, string GetItem_Name)
        {
            int Rtnresult = -1;
            #region//根据年、月、监测点查询信息
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("select a.FILL_ID as FILL_ID,a.ITEM_ID as ITEM_ID,c.ITEM_NAME as ITEM_NAME,a.ITEM_VALUE as ITEM_VALUE,a.IAQI as IAQI, ");
            sb.Append(" b.TEMPERATRUE as TEMPERATRUE,b.WIND_SPEED as WIND_SPEED,b.AIR_LEVEL as AIR_LEVEL,b.WIND_DIRECTION as WIND_DIRECTION ");
            sb.Append(" from T_ENV_FILL_AIR_ITEM a left join T_ENV_FILL_AIR  b on a.FILL_ID=b.ID  left join T_BASE_ITEM_INFO c on a.ITEM_ID=c.ID  ");
            sb.Append(" where b.YEAR='" + year + "' and b.Month='" + month + "' ");
            #endregion

            DataTable SelectDT = ExecuteDataTable(sb.ToString());
            if (SelectDT.Rows.Count > 0)
            {
                foreach (DataRow selectdr in SelectDT.Rows)
                {
                    #region//判断污染物类型
                    decimal CalculateResult = -1;
                    if (selectdr["ITEM_NAME"].ToString().Equals("二氧化硫"))
                    {
                        #region//So2的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnSo2ValueList = this.GetSO2PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnSo2ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("二氧化氮"))
                    {
                        #region//NO的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnNO2ValueList = this.GetNO2PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnNO2ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("一氧化碳"))
                    {
                        #region//CO的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnCoValueList = this.GetCOPolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnCoValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("PM2.5"))
                    {
                        #region//PM2.5"的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnPM25ValueList = this.GetPM25PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnPM25ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("PM10"))
                    {
                        #region//PM10的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnPM10ValueList = this.GetPM10PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnPM10ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("臭氧最大1小时平均"))
                    {
                        #region//O31的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnO31ValueList = this.GetO31PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnO31ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }
                    else if (selectdr["ITEM_NAME"].ToString().Equals("臭氧最大8小时滑动平均"))
                    {
                        #region//So2的计算
                        if (!string.IsNullOrEmpty(selectdr["ITEM_VALUE"].ToString()) && decimal.Parse(selectdr["ITEM_VALUE"].ToString()) >= 0)//监测值
                        {
                            //获取S02的污染物的浓度限制值和空气质量分指数的值
                            ArrayList RtnO38ValueList = this.GetO38PolluctionLimitValue(decimal.Parse(selectdr["ITEM_VALUE"].ToString()));
                            CalculateResult = this.CalculateResult(RtnO38ValueList, selectdr["ITEM_VALUE"].ToString());//获取计算结果
                        }
                        #endregion
                    }

                    #region//更新字段值
                    if (CalculateResult >= 0)
                    {
                        //更新污染物的值
                        int updateresult = this.UpdateItemValue(GetItem_Name, selectdr["ITEM_VALUE"].ToString(), CalculateResult.ToString(), selectdr["ITEM_ID"].ToString(), selectdr["FILL_ID"].ToString());
                        if (updateresult > 0)
                        {
                            Rtnresult = this.UpdateAir(selectdr["FILL_ID"].ToString());//更新空气质量指数、空气质量级别、空气质量状况和首要污染物
                        }
                    }
                    #endregion

                    #endregion
                }
            }
            return Rtnresult;
        }
        #endregion

        #region//环境系统导入数据（新）
        public string  EnvUpdateData(DataSet ds, string AirFillTable, string AirFillItemTable, string FillSerial, string FillISerial)
        {
            string error = string.Empty;
            ArrayList list = new ArrayList();
            string strsql = string.Empty;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    StringBuilder sb = new StringBuilder(5000);
                    sb.Append("select a.ID from  T_ENV_FILL_AIR  a left join T_ENV_P_AIR b on a.point_id=b.id ");
                    sb.Append(" where a.year='" + ds.Tables[0].Rows[i]["YEAR"].ToString() + "' and a.month='" + ds.Tables[0].Rows[i]["MONTH"].ToString() + "' ");
                    sb.Append(" and a.day='" + ds.Tables[0].Rows[i]["BEGIN_DAY"].ToString() + "' and b.point_name='" + ds.Tables[0].Rows[i]["POINT_NAME"].ToString() + "' ");
                    //string Fill_ID = SqlHelper.ExecuteScalar(sb.ToString()).ToString();
                    DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                        {
                            string Fill_ID = dt.Rows[0][0].ToString();
                            StringBuilder sb_update_fill = new StringBuilder();
                            sb_update_fill.Append("update T_ENV_FILL_AIR set API_CODE='" + ds.Tables[0].Rows[i]["type"].ToString() + "',AQI_CODE='" + ds.Tables[0].Rows[i]["aqi"].ToString() + "',AIR_LEVEL='" + ds.Tables[0].Rows[i]["level"].ToString() + "',");
                            sb_update_fill.Append(" AIR_STATE='" + ds.Tables[0].Rows[i]["condition"].ToString() + "',MAIN_AIR='" + ds.Tables[0].Rows[i]["primaryPollution"].ToString() + "' ");
                            sb_update_fill.Append(" where ID='" + Fill_ID + "'");
                            list.Add(sb_update_fill.ToString());

                            StringBuilder sel_item_sb = new StringBuilder();
                            sel_item_sb.Append("select  b.item_name,a.id,a.item_id ");
                            sel_item_sb.Append("  from T_ENV_FILL_AIR_ITEM  a left join T_BASE_ITEM_INFO b on a.item_id=b.id  left join T_BASE_MONITOR_TYPE_INFO c on b.monitor_id=c.id ");
                            sel_item_sb.Append(" where a.FILL_ID='" + Fill_ID + "'");
                            DataTable dt_result = SqlHelper.ExecuteDataTable(sel_item_sb.ToString());
                            if (dt_result.Rows.Count > 0)
                            {
                                #region//更新因子的值
                                foreach (DataRow dr in dt_result.Rows)
                                {
                                    if (dr["item_name"].ToString() == "PM2.5")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["PM2.5"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["IPM2.5"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "PM10")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["PM10"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["IPM10"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "臭氧最大1小时平均")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["O3"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["IO3"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "臭氧最大8小时滑动平均")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["O3_8"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["IO3_8"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "二氧化硫")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["SO2"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["ISO2"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "二氧化氮")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["NO2"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["INO2"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    else if (dr["item_name"].ToString() == "一氧化碳")
                                    {
                                        strsql = "update T_ENV_FILL_AIR_ITEM set item_value='" + ds.Tables[0].Rows[i]["CO"].ToString() + "',IAQI='" + ds.Tables[0].Rows[i]["ICO"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
                                    }
                                    list.Add(strsql);
                                }
                                #endregion
                            }
                        }
                    }
                }
                if (list.Count > 0)
                {
                    error = SqlHelper.ExecuteListSQLByTransaction(list);
                }
            }
            return error;
        }
        #endregion

        #region//环境系统导入数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="AirFillTable">填报表</param>
        /// <param name="AirFillItemTable">填报监测项目表</param>
        /// <param name="FillSerial">填报序号</param>
        /// <param name="FillISerial">填报监测项目序号</param>
        public string  EnvInsertData(DataSet ds,string AirFillTable,string AirFillItemTable, string FillSerial, string FillISerial) 
        {
            string error = string.Empty;
            string sql = string.Empty;
            string AirFillID = string.Empty;
            string AirFillItemID = string.Empty; 
            string strPointId = string.Empty;
            string strItemId = string.Empty;
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                #region//根据点位ID删除填报和填报项目的数据
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    #region//获取点位的ID
                    StringBuilder sb = new StringBuilder(5000);
                    sb.Append("SELECT ID FROM T_ENV_P_AIR");
                    sb.Append(" WHERE YEAR='" + ds.Tables[0].Rows[i]["YEAR"].ToString() + "' AND MONTH='" + ds.Tables[0].Rows[i]["MONTH"].ToString() + "' ");
                    sb.Append(" AND POINT_CODE='" + int.Parse(ds.Tables[0].Rows[i]["POINT_CODE"].ToString()) + "' AND POINT_NAME='" + ds.Tables[0].Rows[i]["POINT_NAME"].ToString() + "'");
                    DataTable objTable = SqlHelper.ExecuteDataTable(sb.ToString());
                    if (objTable.Rows.Count > 0)
                    {
                        strPointId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
                    }
                    #endregion
                    if (!string.IsNullOrEmpty(strPointId))
                    {
                        #region//删除填报和填报项目的数据
                        string strDeleteSqlFillItem = @"delete from {0} where exists (select *   from {1} where {1}.ID ={0}.FILL_ID and  {1}.POINT_ID='{2}' and {1}.Year='{3}' and {1}.Month='{4}' )";
                        strDeleteSqlFillItem = string.Format(strDeleteSqlFillItem, AirFillItemTable, AirFillTable, strPointId, ds.Tables[0].Rows[i]["YEAR"].ToString(), ds.Tables[0].Rows[i]["MONTH"].ToString());
                        list.Add(strDeleteSqlFillItem);
                        string strDeleteSqlFill = "delete from {0} where POINT_ID='{1}' AND YEAR='{2}' AND MONTH='{3}' ";
                        strDeleteSqlFill = string.Format(strDeleteSqlFill, AirFillTable, strPointId, ds.Tables[0].Rows[i]["YEAR"].ToString(), ds.Tables[0].Rows[i]["MONTH"].ToString());
                        list.Add(strDeleteSqlFill);
                        #endregion
                    }
                    else
                    {
                        error = "未找到点位ID"; break;
                    }
                }
                #endregion 

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow[] dr = ds.Tables[0].Select("YEAR='" + ds.Tables[0].Rows[i]["YEAR"].ToString() + "' AND MONTH='" + ds.Tables[0].Rows[i]["MONTH"].ToString() + "' AND POINT_CODE='" + ds.Tables[0].Rows[i]["POINT_CODE"].ToString() + "' AND POINT_NAME='" + ds.Tables[0].Rows[i]["POINT_NAME"].ToString() + "' AND BEGIN_DAY='" + ds.Tables[0].Rows[i]["BEGIN_DAY"].ToString() + "'");
                    if (dr.Length==1)
                    {
                        #region//获取点位的ID
                        StringBuilder sb = new StringBuilder(5000);
                        sb.Append("SELECT ID FROM T_ENV_P_AIR");
                        sb.Append(" WHERE YEAR='" + ds.Tables[0].Rows[i]["YEAR"].ToString() + "' AND MONTH='" + ds.Tables[0].Rows[i]["MONTH"].ToString() + "' ");
                        sb.Append(" AND POINT_CODE='" + int.Parse(ds.Tables[0].Rows[i]["POINT_CODE"].ToString()) + "' AND POINT_NAME='" + ds.Tables[0].Rows[i]["POINT_NAME"].ToString() + "'");
                        DataTable objTable = SqlHelper.ExecuteDataTable(sb.ToString());
                        if (objTable.Rows.Count > 0)
                        {
                            strPointId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
                        }
                        #endregion

                        if (!string.IsNullOrEmpty(strPointId))
                        {
                            #region//创建填报头
                            AirFillID = GetSerialNumber(FillISerial);//填报头ID
                            sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,DAY) values('{1}','{2}','{3}','{4}','{5}')";
                            sql = string.Format(sql, AirFillTable, AirFillID, strPointId, ds.Tables[0].Rows[i]["YEAR"].ToString(), int.Parse(ds.Tables[0].Rows[i]["MONTH"].ToString()), ds.Tables[0].Rows[i]["BEGIN_DAY"].ToString());
                            list.Add(sql);
                            #endregion

                            #region//创建填报监测项目

                            #region//一氧化碳的创建
                            if (ds.Tables[0].Columns.Contains("CO") || ds.Tables[0].Columns.Contains("co"))//一氧化碳
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "一氧化碳");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["CO"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "一氧化碳在点位监测表中未找到监测项目ID";
                                }
                            }
                            #endregion

                            #region//二氧化硫的创建
                            if (ds.Tables[0].Columns.Contains("SO2") || ds.Tables[0].Columns.Contains("so2"))//二氧化硫
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "二氧化硫");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["SO2"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "二氧化硫在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #region//二氧化氮的创建
                            if (ds.Tables[0].Columns.Contains("NO2") || ds.Tables[0].Columns.Contains("no2"))//二氧化氮
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "二氧化氮");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["NO2"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "二氧化氮在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #region//PM2.5的创建
                            if (ds.Tables[0].Columns.Contains("PM2.5") || ds.Tables[0].Columns.Contains("pm2.5"))//PM2.5 
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "PM2.5");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["PM2.5"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "PM25在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #region//PM10的创建
                            if (ds.Tables[0].Columns.Contains("PM10") || ds.Tables[0].Columns.Contains("pm10"))//PM10
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "PM10");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["PM10"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "PM10在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #region//臭氧最大1小时平均的创建
                            if (ds.Tables[0].Columns.Contains("O3") || ds.Tables[0].Columns.Contains("o3"))//O3
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "臭氧最大1小时平均");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["O3"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "臭氧最大1小时平均在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #region//臭氧最大8小时滑动平均的创建
                            if (ds.Tables[0].Columns.Contains("O3_8") || ds.Tables[0].Columns.Contains("o3_8"))//臭氧最大8小时滑动平均
                            {
                                strItemId = this.GetAirFillItemID(AirFillItemTable, strPointId, "臭氧最大8小时滑动平均");//获取监测项目ID 
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                    sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                                    sql = string.Format(sql, AirFillTable, AirFillID, strItemId);
                                    dtTemp = ExecuteDataTable(sql);
                                    if (dtTemp.Rows.Count == 0)
                                    {
                                        AirFillItemID = GetSerialNumber(FillISerial);
                                        sql = "insert into {0}(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('{1}','{2}','{3}','{4}')";
                                        sql = string.Format(sql, AirFillItemTable, AirFillItemID, AirFillID, strItemId, ds.Tables[0].Rows[i]["O3_8"].ToString());
                                        list.Add(sql);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error = "臭氧最大8小时滑动平均在点位监测表中未找到监测项目ID";
                                }
                            }


                            #endregion

                            #endregion
                        }
                        else
                        {
                            error = "未找到点位ID"; break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (list.Count > 0)
            {
                error = SqlHelper.ExecuteListSQLByTransaction(list);
            }
            return error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPointItemTableName">点位监测项目表</param>
        /// <param name="strPointId">点位id</param>
        /// <param name="strItemName">监测项目名称</param>
        /// <returns></returns>
        private string GetAirFillItemID(string strPointItemTableName, string strPointId, string strItemName)
        {
            string strItemId = "";
            string strSql = @"select ITEM_ID  from T_ENV_P_AIR_ITEM where POINT_ID = '{0}' and ITEM_ID in (select ID from T_BASE_ITEM_INFO  where ITEM_NAME = '{1}'  and IS_DEL = '0' and IS_SUB = '1')";                
            strSql = string.Format(strSql, strPointId, strItemName);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
                strItemId = objTable.Rows[0]["ITEM_ID"] == null ? "" : objTable.Rows[0]["ITEM_ID"].ToString();
            return strItemId;
        }

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAir"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirVo tEnvFillAir)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAir)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAir.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAir.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAir.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillAir.POINT_ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillAir.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAir.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillAir.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAir.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillAir.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillAir.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillAir.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillAir.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillAir.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillAir.MINUTE.ToString()));
                }
                //气温
                if (!String.IsNullOrEmpty(tEnvFillAir.TEMPERATRUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEMPERATRUE = '{0}'", tEnvFillAir.TEMPERATRUE.ToString()));
                }
                //气压
                if (!String.IsNullOrEmpty(tEnvFillAir.PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRESSURE = '{0}'", tEnvFillAir.PRESSURE.ToString()));
                }
                //风速
                if (!String.IsNullOrEmpty(tEnvFillAir.WIND_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_SPEED = '{0}'", tEnvFillAir.WIND_SPEED.ToString()));
                }
                //风向
                if (!String.IsNullOrEmpty(tEnvFillAir.WIND_DIRECTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_DIRECTION = '{0}'", tEnvFillAir.WIND_DIRECTION.ToString()));
                }
                //API指数
                if (!String.IsNullOrEmpty(tEnvFillAir.API_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND API_CODE = '{0}'", tEnvFillAir.API_CODE.ToString()));
                }
                //空气质量指数
                if (!String.IsNullOrEmpty(tEnvFillAir.AQI_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AQI_CODE = '{0}'", tEnvFillAir.AQI_CODE.ToString()));
                }
                //空气级别
                if (!String.IsNullOrEmpty(tEnvFillAir.AIR_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIR_LEVEL = '{0}'", tEnvFillAir.AIR_LEVEL.ToString()));
                }
                //空气质量状况
                if (!String.IsNullOrEmpty(tEnvFillAir.AIR_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIR_STATE = '{0}'", tEnvFillAir.AIR_STATE.ToString()));
                }
                //主要污染物
                if (!String.IsNullOrEmpty(tEnvFillAir.MAIN_AIR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_AIR = '{0}'", tEnvFillAir.MAIN_AIR.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAir.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAir.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAir.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAir.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAir.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAir.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAir.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAir.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAir.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAir.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion 

        #region//暂不用的代码
        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataTable GetAirFillData(string year, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from t_env_fill_air where 1=1 ");
            if (!string.IsNullOrEmpty(year))
                sqlStr.AppendFormat("and [year]='{0}' ", year);
            if (!string.IsNullOrEmpty(month))
                sqlStr.AppendFormat("and [month]='{0}'", month);

            return ExecuteDataTable(sqlStr.ToString());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAirFillData(DataTable dtData)
        {
            if (dtData.Rows.Count > 0)
            {
                string year = dtData.Rows[0]["year"].ToString();
                string month = dtData.Rows[0]["month"].ToString();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("begin Transaction T\n");
                strSql.Append("declare @errorCount as int\n");
                strSql.Append("set @errorCount=0\n");

                strSql.AppendFormat("delete from t_env_fill_air where year='{0}' and month='{1}'\n", year, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                foreach (DataRow drData in dtData.Rows)
                {
                    string pointCode = drData["point_code"].ToString();
                    string day = drData["day"].ToString();
                    string week = drData["week"].ToString();
                    string pointName = drData["point_name"].ToString();
                    string so2 = drData["so2"].ToString();
                    string nox = drData["nox"].ToString();
                    string no2 = drData["no2"].ToString();
                    string tsp = drData["tsp"].ToString();
                    string pm10 = drData["pm10"].ToString();
                    string pm25 = drData["pm25"].ToString();
                    string co = drData["co"].ToString();
                    string o3 = drData["o3"].ToString();
                    string o3_8 = drData["o3_8"].ToString();
                    string tp = drData["temperatrue"].ToString();
                    string pr = drData["pressure"].ToString();
                    string ws = drData["wind_speed"].ToString();
                    string wd = drData["wind_direction"].ToString();
                    string id = new TSysSerialAccess().GetSerialNumber("air_fill_id");

                    strSql.AppendFormat("insert into t_env_fill_air(id,point_code,year,month,day,week,point_name,so2,nox,no2,tsp,pm10,pm25,co,temperatrue,pressure,wind_speed,wind_direction,o3,o3_8) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", id, pointCode, year, month, day, week, pointName, so2, nox, no2, tsp, pm10, pm25, co, tp, pr, ws, wd, o3, o3_8);
                    strSql.Append("set @errorCount=@errorCount+@@error\n");
                }

                strSql.Append("IF @errorCount <> 0\n");
                strSql.Append("begin\n");
                strSql.Append("select 'fail'\n");
                strSql.Append("RollBack Transaction T\n");
                strSql.Append("end\n");
                strSql.Append("else\n");
                strSql.Append("begin\n");
                strSql.Append("select 'success'\n");
                strSql.Append("COMMIT Transaction T\n");
                strSql.Append("end\n");

                string result = ExecuteDataTable(strSql.ToString()).Rows[0][0].ToString();

                return result;
            }
            else
                return "";
        }
        #endregion 
    }
}
