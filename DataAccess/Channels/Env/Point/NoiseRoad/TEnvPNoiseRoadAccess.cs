using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseRoad;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseRoadAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = "select Count(*) from T_ENV_P_NOISE_ROAD " + this.BuildWhereStatement(tEnvPNoiseRoad);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_ROAD  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPNoiseRoadVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadVo Details(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_ROAD " + this.BuildWhereStatement(tEnvPNoiseRoad));
            return SqlHelper.ExecuteObject(new TEnvPNoiseRoadVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseRoadVo> SelectByObject(TEnvPNoiseRoadVo tEnvPNoiseRoad, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_NOISE_ROAD " + this.BuildWhereStatement(tEnvPNoiseRoad));
            return SqlHelper.ExecuteObjectList(tEnvPNoiseRoad, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseRoadVo tEnvPNoiseRoad, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_NOISE_ROAD {0}   order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPNoiseRoad));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseRoad"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = "select * from T_ENV_P_NOISE_ROAD " + this.BuildWhereStatement(tEnvPNoiseRoad);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <returns></returns>
        public TEnvPNoiseRoadVo SelectByObject(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = "select * from T_ENV_P_NOISE_ROAD " + this.BuildWhereStatement(tEnvPNoiseRoad);
            return SqlHelper.ExecuteObject(new TEnvPNoiseRoadVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPNoiseRoad, TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoad">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseRoad, TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPNoiseRoad.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoad_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseRoad_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadVo tEnvPNoiseRoad_UpdateSet, TEnvPNoiseRoadVo tEnvPNoiseRoad_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseRoad_UpdateSet, TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPNoiseRoad_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_NOISE_ROAD where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            string strSQL = "delete from T_ENV_P_NOISE_ROAD ";
            strSQL += this.BuildWhereStatement(tEnvPNoiseRoad);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPNoiseRoad"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPNoiseRoad)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPNoiseRoad.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPNoiseRoad.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPNoiseRoad.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPNoiseRoad.SATAIONS_ID.ToString()));
                }
                //路段名称
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.ROAD_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ROAD_NAME = '{0}'", tEnvPNoiseRoad.ROAD_NAME.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPNoiseRoad.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPNoiseRoad.POINT_NAME.ToString()));
                }
                //路段长度
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.ROAD_LENGTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ROAD_LENGTH = '{0}'", tEnvPNoiseRoad.ROAD_LENGTH.ToString()));
                }
                //路段宽度
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.ROAD_WIDTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ROAD_WIDTH = '{0}'", tEnvPNoiseRoad.ROAD_WIDTH.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPNoiseRoad.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPNoiseRoad.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPNoiseRoad.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPNoiseRoad.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPNoiseRoad.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPNoiseRoad.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPNoiseRoad.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPNoiseRoad.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPNoiseRoad.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPNoiseRoad.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPNoiseRoad.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPNoiseRoad.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPNoiseRoad.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPNoiseRoad.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPNoiseRoad.REMARK5.ToString()));
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
        public bool Create(TEnvPNoiseRoadVo TEnvPNoiseRoad, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPNoiseRoad.SelectMonths.Split(';').ToList();
            TEnvPNoiseRoad.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPNoiseRoad.ID = GetSerialNumber(strSerial);
                TEnvPNoiseRoad.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPNoiseRoad, TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE);
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

            sql = "select * from " + TEnvPNoiseRoadItemVo.T_ENV_P_NOISE_ROAD_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPNoiseRoadItemVo.T_ENV_P_NOISE_ROAD_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPNoiseRoadItemVo.T_ENV_P_NOISE_ROAD_ITEM_TABLE, row, "", "", strTID, strID);
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
