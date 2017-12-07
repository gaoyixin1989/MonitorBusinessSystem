using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseArea;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseAreaAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = "select Count(*) from T_ENV_P_NOISE_AREA " + this.BuildWhereStatement(tEnvPNoiseArea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaVo Details(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA " + this.BuildWhereStatement(tEnvPNoiseArea));
            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseAreaVo> SelectByObject(TEnvPNoiseAreaVo tEnvPNoiseArea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA " + this.BuildWhereStatement(tEnvPNoiseArea));
            return SqlHelper.ExecuteObjectList(tEnvPNoiseArea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseAreaVo tEnvPNoiseArea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_NOISE_AREA {0}  order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPNoiseArea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseArea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = "select * from T_ENV_P_NOISE_AREA " + this.BuildWhereStatement(tEnvPNoiseArea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <returns></returns>
        public TEnvPNoiseAreaVo SelectByObject(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = "select * from T_ENV_P_NOISE_AREA " + this.BuildWhereStatement(tEnvPNoiseArea);
            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPNoiseArea, TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseArea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseArea, TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPNoiseArea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseArea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseArea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaVo tEnvPNoiseArea_UpdateSet, TEnvPNoiseAreaVo tEnvPNoiseArea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseArea_UpdateSet, TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPNoiseArea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_NOISE_AREA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            string strSQL = "delete from T_ENV_P_NOISE_AREA ";
            strSQL += this.BuildWhereStatement(tEnvPNoiseArea);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPNoiseArea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPNoiseArea)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPNoiseArea.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPNoiseArea.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPNoiseArea.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPNoiseArea.SATAIONS_ID.ToString()));
                }
                //功能区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.FUNCTION_AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_AREA_ID = '{0}'", tEnvPNoiseArea.FUNCTION_AREA_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPNoiseArea.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPNoiseArea.POINT_NAME.ToString()));
                }
                //行政区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPNoiseArea.AREA_ID.ToString()));
                }
                //声源类型ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.SOUND_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SOUND_SOURCE_ID = '{0}'", tEnvPNoiseArea.SOUND_SOURCE_ID.ToString()));
                }
                //覆盖面积
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.COVER_AREA.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COVER_AREA = '{0}'", tEnvPNoiseArea.COVER_AREA.ToString()));
                }
                //覆盖人口
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.COVER_PUPILATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COVER_PUPILATION = '{0}'", tEnvPNoiseArea.COVER_PUPILATION.ToString()));
                }
                //网格大小（X）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.GRID_SIZE_X.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GRID_SIZE_X = '{0}'", tEnvPNoiseArea.GRID_SIZE_X.ToString()));
                }
                //网格大小（Y）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.GRID_SIZE_Y.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GRID_SIZE_Y = '{0}'", tEnvPNoiseArea.GRID_SIZE_Y.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPNoiseArea.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPNoiseArea.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPNoiseArea.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPNoiseArea.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPNoiseArea.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPNoiseArea.LATITUDE_SECOND.ToString()));
                }
                //测点位置
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPNoiseArea.LOCATION.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPNoiseArea.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPNoiseArea.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPNoiseArea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPNoiseArea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPNoiseArea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPNoiseArea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPNoiseArea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPNoiseArea.REMARK5.ToString()));
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
        public bool Create(TEnvPNoiseAreaVo TEnvPNoiseArea, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPNoiseArea.SelectMonths.Split(';').ToList();
            TEnvPNoiseArea.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPNoiseArea.ID = GetSerialNumber(strSerial);
                TEnvPNoiseArea.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPNoiseArea, TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE);
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

            sql = "select * from " + TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE, row, "", "", strTID, strID);
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
