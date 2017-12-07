using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.UnderDrinkSource;
using System.Collections;
using i3.ValueObject.Channels.Env.Point.DrinkSource;

namespace i3.DataAccess.Channels.Env.Fill.UnderDrinkSource
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-26
    /// 创建人：
    /// </summary>
    public class TEnvFillUnderdrinkSrcAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_UNDERDRINK_SRC " + this.BuildWhereStatement(tEnvFillUnderdrinkSrc);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEvnFillUnderDrinkSourceVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEvnFillUnderDrinkSourceVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEvnFillUnderDrinkSourceVo Details(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC " + this.BuildWhereStatement(tEnvFillUnderdrinkSrc));
            return SqlHelper.ExecuteObject(new TEvnFillUnderDrinkSourceVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEvnFillUnderDrinkSourceVo> SelectByObject(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC " + this.BuildWhereStatement(tEnvFillUnderdrinkSrc));
            return SqlHelper.ExecuteObjectList(tEnvFillUnderdrinkSrc, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_UNDERDRINK_SRC {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillUnderdrinkSrc));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = "select * from T_ENV_FILL_UNDERDRINK_SRC " + this.BuildWhereStatement(tEnvFillUnderdrinkSrc);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <returns></returns>
        public TEvnFillUnderDrinkSourceVo SelectByObject(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = "select * from T_ENV_FILL_UNDERDRINK_SRC " + this.BuildWhereStatement(tEnvFillUnderdrinkSrc);
            return SqlHelper.ExecuteObject(new TEvnFillUnderDrinkSourceVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillUnderdrinkSrc, TEvnFillUnderDrinkSourceVo.T_ENV_FILL_UNDERDRINK_SRC_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillUnderdrinkSrc, TEvnFillUnderDrinkSourceVo.T_ENV_FILL_UNDERDRINK_SRC_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillUnderdrinkSrc.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillUnderdrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc_UpdateSet, TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillUnderdrinkSrc_UpdateSet, TEvnFillUnderDrinkSourceVo.T_ENV_FILL_UNDERDRINK_SRC_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillUnderdrinkSrc_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_UNDERDRINK_SRC where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            string strSQL = "delete from T_ENV_FILL_UNDERDRINK_SRC ";
            strSQL += this.BuildWhereStatement(tEnvFillUnderdrinkSrc);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable_Und(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from " + TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE + " a inner join " + TEnvPDrinkSrcVVo.T_ENV_P_DRINK_SRC_V_TABLE + " b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0' and NUM='Under' ";
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
                //sb.Append("select a.ID,a.YEAR T_ENV_FILL_UNDERDRINK_SRC@YEAR@年份, b.SATAIONS_ID as T_ENV_FILL_UNDERDRINK_SRC@SATAIONS_ID@测站编码,c.dict_text as T_ENV_FILL_UNDERDRINK_SRC@dict_text@测站名称, ");
                //sb.Append("  a.SECTION_ID T_ENV_FILL_UNDERDRINK_SRC@SECTION_ID@断面名称,b.SECTION_CODE T_ENV_FILL_UNDERDRINK_SRC@SECTION_CODE@断面代码, ");
                //sb.Append("   a.POINT_ID T_ENV_FILL_UNDERDRINK_SRC@POINT_ID@垂线名称, a.MONTH T_ENV_FILL_UNDERDRINK_SRC@MONTH@月份,");
                //sb.Append("   a.DAY T_ENV_FILL_UNDERDRINK_SRC@DAY@日期,  a.KPF T_ENV_FILL_UNDERDRINK_SRC@KPF@水期代码  ");
                //sb.Append(" from T_ENV_FILL_UNDERDRINK_SRC a left join T_ENV_P_DRINK_SRC b on a.section_id=b.id  Left join T_SYS_DICT c on b.SATAIONS_ID=c.dict_code ");
                //sb.Append("  where  a.POINT_ID in(" + PointIDs + ")");
                //dtMain = ExecuteDataTable(sb.ToString());
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
                              where {2} and a.IS_DEL='0' and NUM='Under'  ";

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

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillUnderdrinkSrc)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillUnderdrinkSrc.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillUnderdrinkSrc.SECTION_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillUnderdrinkSrc.POINT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillUnderdrinkSrc.SAMPLING_DAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillUnderdrinkSrc.YEAR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillUnderdrinkSrc.MONTH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillUnderdrinkSrc.DAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillUnderdrinkSrc.HOUR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillUnderdrinkSrc.MINUTE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillUnderdrinkSrc.KPF.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillUnderdrinkSrc.JUDGE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillUnderdrinkSrc.OVERPROOF.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillUnderdrinkSrc.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillUnderdrinkSrc.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillUnderdrinkSrc.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillUnderdrinkSrc.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrc.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillUnderdrinkSrc.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

} 
