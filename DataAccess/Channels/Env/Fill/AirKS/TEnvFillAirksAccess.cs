using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirKS;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Fill.AirKS
{

    /// <summary>
    /// 功能：环境空气(科室)填报
    /// 创建日期：2013-07-03
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirksAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIRKS " + this.BuildWhereStatement(tEnvFillAirks);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirksVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirksVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirks">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirksVo Details(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS " + this.BuildWhereStatement(tEnvFillAirks));
            return SqlHelper.ExecuteObject(new TEnvFillAirksVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirksVo> SelectByObject(TEnvFillAirksVo tEnvFillAirks, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS " + this.BuildWhereStatement(tEnvFillAirks));
            return SqlHelper.ExecuteObjectList(tEnvFillAirks, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirksVo tEnvFillAirks, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIRKS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAirks));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirks"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = "select * from T_ENV_FILL_AIRKS " + this.BuildWhereStatement(tEnvFillAirks);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <returns></returns>
        public TEnvFillAirksVo SelectByObject(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = "select * from T_ENV_FILL_AIRKS " + this.BuildWhereStatement(tEnvFillAirks);
            return SqlHelper.ExecuteObject(new TEnvFillAirksVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAirks, TEnvFillAirksVo.T_ENV_FILL_AIRKS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirks">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirks, TEnvFillAirksVo.T_ENV_FILL_AIRKS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAirks.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirks_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirks_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksVo tEnvFillAirks_UpdateSet, TEnvFillAirksVo tEnvFillAirks_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirks_UpdateSet, TEnvFillAirksVo.T_ENV_FILL_AIRKS_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAirks_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIRKS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAirksVo tEnvFillAirks)
        {
            string strSQL = "delete from T_ENV_FILL_AIRKS ";
            strSQL += this.BuildWhereStatement(tEnvFillAirks);

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
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "起始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "起始时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MINUTE";
            dr["name"] = "起始分";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_DAY";
            dr["name"] = "结束日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_HOUR";
            dr["name"] = "结束时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_MINUTE";
            dr["name"] = "结束分";
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
        public DataTable GetFillData(string strWhere, DataTable dtShow, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
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
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));//把监测项目添加到的dtMain中
                    }

                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;

                    #region //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1})";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtFillItem = ExecuteDataTable(sql);

                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                        #region //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            string itemId = drAllItem["ID"].ToString(); //监测项ID
                            var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                            if (itemValue.Count > 0)
                            {
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
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
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            bool flag = false;
            ArrayList list = new ArrayList();
            DataTable dtMain = new DataTable();

            #region //获取测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";
            #endregion

            sql = string.Format(sql, PointTable, strWhere);
            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                #region//月份判断
                if (dtMain.Rows[0][2].ToString().Equals("1") || dtMain.Rows[0][2].ToString().Equals("3") || dtMain.Rows[0][2].ToString().Equals("5") || dtMain.Rows[0][2].ToString().Equals("7") || dtMain.Rows[0][2].ToString().Equals("8") || dtMain.Rows[0][2].ToString().Equals("10") || dtMain.Rows[0][2].ToString().Equals("12"))
                {
                    list = this.SqlList(dtMain, ItemTable, FillTable, FillITable, FillSerial, FillISerial, 31);
                }
                else if (dtMain.Rows[0][2].ToString().Equals("4") || dtMain.Rows[0][2].ToString().Equals("6") || dtMain.Rows[0][2].ToString().Equals("9") || dtMain.Rows[0][2].ToString().Equals("11"))
                {
                    list = this.SqlList(dtMain, ItemTable, FillTable, FillITable, FillSerial, FillISerial, 30);
                }
                else if (dtMain.Rows[0][2].ToString().Equals("2"))
                {
                    if (((int.Parse(dtMain.Rows[0][1].ToString()) % 4 == 0) && (int.Parse(dtMain.Rows[0][1].ToString()) % 100 != 0)) || (int.Parse(dtMain.Rows[0][1].ToString()) % 400 == 0))
                    {
                        list = this.SqlList(dtMain, ItemTable, FillTable, FillITable, FillSerial, FillISerial, 29);//闰年
                    }
                    else
                    {
                        list = this.SqlList(dtMain, ItemTable, FillTable, FillITable, FillSerial, FillISerial, 28);//平年
                    }
                #endregion
                }
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            if (list.Count == 0)
            { flag = true; }//list.count==0,说明数据库里有数据，不用插入数据；或者程序报错；
            else
            {
                flag = SqlHelper.ExecuteSQLByTransaction(list);
            }
            return flag;
        }

        private ArrayList SqlList(DataTable dtMain, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial,int DayCount)
        {
            string sql = "";
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号 
            DataTable dtTemp = new DataTable();
            DataTable dtAllItem = new DataTable();
            ArrayList list = new ArrayList();
            for (int i = 1; i <= DayCount; i++)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {

                    #region//判断填报表中是否存在在相应的垂线/测点数据，如果没有则插入数据
                    sql = "select ID from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    #endregion
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        #region//填报没有时，插入一条
                        FillID = GetSerialNumber(FillSerial);
                        sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,BEGIN_DAY,Remark1) values('{1}','{2}','{3}','{4}','{5}','1')";
                        sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), i.ToString());
                        list.Add(sql);
                        #endregion
                    }

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
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                        #endregion
                    }
                }
            }
            return list;
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
                    sql = "select ID,BEGIN_DAY from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    dtFill = ExecuteDataTable(sql);//查询填报

                    for (int i = 0; i < Days; i++)
                    {
                        //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                        drFill = dtFill.Select("BEGIN_DAY='" + (i + 1).ToString() + "'");

                        if (drFill.Length > 0)
                        {
                            FillID = drFill[0]["ID"].ToString();
                        }
                        else
                        {
                            FillNumber++;
                            FillID = (FillNumber).ToString().PadLeft(FillLength, '0');
                            
                            sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,BEGIN_DAY,Remark1) values('{1}','{2}','{3}','{4}','{5}','1')";
                            sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), (i + 1).ToString());

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

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAirks"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirksVo tEnvFillAirks)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAirks)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAirks.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAirks.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAirks.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillAirks.POINT_ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillAirks.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAirks.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvFillAirks.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAirks.MONTH.ToString()));
                }
                //监测起始月
                if (!String.IsNullOrEmpty(tEnvFillAirks.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillAirks.BEGIN_MONTH.ToString()));
                }
                //监测起始日
                if (!String.IsNullOrEmpty(tEnvFillAirks.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillAirks.BEGIN_DAY.ToString()));
                }
                //监测起始时
                if (!String.IsNullOrEmpty(tEnvFillAirks.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillAirks.BEGIN_HOUR.ToString()));
                }
                //监测起始分
                if (!String.IsNullOrEmpty(tEnvFillAirks.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillAirks.BEGIN_MINUTE.ToString()));
                }
                //监测结束月
                if (!String.IsNullOrEmpty(tEnvFillAirks.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillAirks.END_MONTH.ToString()));
                }
                //监测结束日
                if (!String.IsNullOrEmpty(tEnvFillAirks.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillAirks.END_DAY.ToString()));
                }
                //监测结束时
                if (!String.IsNullOrEmpty(tEnvFillAirks.END_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_HOUR = '{0}'", tEnvFillAirks.END_HOUR.ToString()));
                }
                //监测结束分
                if (!String.IsNullOrEmpty(tEnvFillAirks.END_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MINUTE = '{0}'", tEnvFillAirks.END_MINUTE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAirks.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAirks.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAirks.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAirks.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAirks.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAirks.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAirks.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAirks.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAirks.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAirks.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
