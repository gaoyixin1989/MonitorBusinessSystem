using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sulfate;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Sulfate
{
    /// <summary>
    /// 功能：硫酸盐化速率监测点表
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    /// </summary>
    public class TEnvPAlkaliAccess : SqlHelper 
    {
        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = "select Count(*) from T_ENV_P_ALKALI " + this.BuildWhereStatement(tEnvPAlkali);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ALKALI  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAlkali">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliVo Details(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ALKALI " + this.BuildWhereStatement(tEnvPAlkali));
            return SqlHelper.ExecuteObject(new TEnvPAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAlkaliVo> SelectByObject(TEnvPAlkaliVo tEnvPAlkali, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_ALKALI " + this.BuildWhereStatement(tEnvPAlkali));
            return SqlHelper.ExecuteObjectList(tEnvPAlkali, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAlkaliVo tEnvPAlkali, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_ALKALI {0} ";
            if (!string.IsNullOrEmpty(tEnvPAlkali.SORT_FIELD))
            {
                strSQL += " order by " + tEnvPAlkali.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPAlkali.SORT_TYPE))
            {
                strSQL += " " + tEnvPAlkali.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPAlkali));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <returns></returns>
        public TEnvPAlkaliVo SelectByObject(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = "select * from T_ENV_P_ALKALI " + this.BuildWhereStatement(tEnvPAlkali);
            return SqlHelper.ExecuteObject(new TEnvPAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPAlkali, TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/15)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliVo tEnvPAir, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPAir.SelectMonths.Split(';').ToList();
            tEnvPAir.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPAir.ID = GetSerialNumber(Number);
                tEnvPAir.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkali">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAlkali, TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPAlkali.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkali_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAlkali_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliVo tEnvPAlkali_UpdateSet, TEnvPAlkaliVo tEnvPAlkali_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAlkali_UpdateSet, TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPAlkali_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_ALKALI where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = "delete from T_ENV_P_ALKALI ";
            strSQL += this.BuildWhereStatement(tEnvPAlkali);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAlkaliVo tEnvPAlkali)
        {
            string strSQL = "select * from T_ENV_P_ALKALI " + this.BuildWhereStatement(tEnvPAlkali);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region// 硫酸盐化速率监测点监测项目表复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE, row, "","", strTID, strID);
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

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPAlkali"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPAlkaliVo tEnvPAlkali)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPAlkali)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPAlkali.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPAlkali.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPAlkali.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPAlkali.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPAlkali.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPAlkali.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPAlkali.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPAlkali.SATAIONS_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPAlkali.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPAlkali.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPAlkali.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPAlkali.POINT_NAME.ToString()));
                }
                //行政区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPAlkali.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPAlkali.AREA_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPAlkali.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPAlkali.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPAlkali.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPAlkali.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPAlkali.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPAlkali.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPAlkali.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPAlkali.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPAlkali.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPAlkali.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPAlkali.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPAlkali.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPAlkali.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPAlkali.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPAlkali.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPAlkali.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPAlkali.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPAlkali.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPAlkali.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPAlkali.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPAlkali.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPAlkali.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPAlkali.REMARK5.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPAlkali.COLUMN_21.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COLUMN_21 = '{0}'", tEnvPAlkali.COLUMN_21.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
