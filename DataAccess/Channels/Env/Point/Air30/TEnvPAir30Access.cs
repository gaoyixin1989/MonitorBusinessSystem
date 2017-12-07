using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Air30;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Air30
{
    /// <summary>
    /// 功能：双三十废气
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPAir30Access : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = "select Count(*) from T_ENV_P_AIR30 " + this.BuildWhereStatement(tEnvPAir30);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAir30Vo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR30  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPAir30Vo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAir30Vo Details(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR30 " + this.BuildWhereStatement(tEnvPAir30));
            return SqlHelper.ExecuteObject(new TEnvPAir30Vo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAir30Vo> SelectByObject(TEnvPAir30Vo tEnvPAir30, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_AIR30 " + this.BuildWhereStatement(tEnvPAir30));
            return SqlHelper.ExecuteObjectList(tEnvPAir30, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAir30Vo tEnvPAir30, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_AIR30 {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPAir30));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = "select * from T_ENV_P_AIR30 " + this.BuildWhereStatement(tEnvPAir30);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <returns></returns>
        public TEnvPAir30Vo SelectByObject(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = "select * from T_ENV_P_AIR30 " + this.BuildWhereStatement(tEnvPAir30);
            return SqlHelper.ExecuteObject(new TEnvPAir30Vo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir30, TEnvPAir30Vo.T_ENV_P_AIR30_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir30, TEnvPAir30Vo.T_ENV_P_AIR30_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPAir30.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30Vo tEnvPAir30_UpdateSet, TEnvPAir30Vo tEnvPAir30_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAir30_UpdateSet, TEnvPAir30Vo.T_ENV_P_AIR30_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPAir30_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_AIR30 where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPAir30Vo tEnvPAir30)
        {
            string strSQL = "delete from T_ENV_P_AIR30 ";
            strSQL += this.BuildWhereStatement(tEnvPAir30);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPAir30"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPAir30Vo tEnvPAir30)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPAir30)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPAir30.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPAir30.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPAir30.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPAir30.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPAir30.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPAir30.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPAir30.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPAir30.SATAIONS_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPAir30.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPAir30.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPAir30.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPAir30.POINT_NAME.ToString()));
                }
                //行政区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPAir30.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPAir30.AREA_ID.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPAir30.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPAir30.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPAir30.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPAir30.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPAir30.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPAir30.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPAir30.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPAir30.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPAir30.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPAir30.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPAir30.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPAir30.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPAir30.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPAir30.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPAir30.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPAir30.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPAir30.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPAir30.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPAir30.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPAir30.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPAir30.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPAir30.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPAir30.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPAir30.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPAir30.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPAir30.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPAir30.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPAir30.REMARK5.ToString()));
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
        public bool Create(TEnvPAir30Vo TEnvPAir30, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPAir30.SelectMonths.Split(';').ToList();
            TEnvPAir30.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPAir30.ID = GetSerialNumber(strSerial);
                TEnvPAir30.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPAir30, TEnvPAir30Vo.T_ENV_P_AIR30_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }


        /// <summary>
        /// 监测项目的复制逻辑
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

            sql = "select * from " + TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE, row, "", "", strTID, strID);
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
    }

}
