using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Soil;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Soil
{
    /// <summary>
    /// 功能：土壤
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSoilAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = "select Count(*) from T_ENV_P_SOIL " + this.BuildWhereStatement(tEnvPSoil);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSoilVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOIL  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSoilVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSoil">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSoilVo Details(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOIL " + this.BuildWhereStatement(tEnvPSoil));
            return SqlHelper.ExecuteObject(new TEnvPSoilVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSoilVo> SelectByObject(TEnvPSoilVo tEnvPSoil, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SOIL " + this.BuildWhereStatement(tEnvPSoil));
            return SqlHelper.ExecuteObjectList(tEnvPSoil, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSoilVo tEnvPSoil, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SOIL {0}  order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSoil));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSoil"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = "select * from T_ENV_P_SOIL " + this.BuildWhereStatement(tEnvPSoil);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <returns></returns>
        public TEnvPSoilVo SelectByObject(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = "select * from T_ENV_P_SOIL " + this.BuildWhereStatement(tEnvPSoil);
            return SqlHelper.ExecuteObject(new TEnvPSoilVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSoil, TEnvPSoilVo.T_ENV_P_SOIL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoil">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSoil, TEnvPSoilVo.T_ENV_P_SOIL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSoil.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoil_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSoil_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilVo tEnvPSoil_UpdateSet, TEnvPSoilVo tEnvPSoil_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSoil_UpdateSet, TEnvPSoilVo.T_ENV_P_SOIL_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSoil_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SOIL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSoilVo tEnvPSoil)
        {
            string strSQL = "delete from T_ENV_P_SOIL ";
            strSQL += this.BuildWhereStatement(tEnvPSoil);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSoil"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSoilVo tEnvPSoil)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSoil)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSoil.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSoil.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPSoil.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPSoil.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPSoil.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPSoil.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSoil.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPSoil.SATAIONS_ID.ToString()));
                }
                //点位代码
                if (!String.IsNullOrEmpty(tEnvPSoil.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPSoil.POINT_CODE.ToString()));
                }
                //点位名称
                if (!String.IsNullOrEmpty(tEnvPSoil.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPSoil.POINT_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSoil.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPSoil.AREA_ID.ToString()));
                }
                //所属省份ID
                if (!String.IsNullOrEmpty(tEnvPSoil.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPSoil.PROVINCE_ID.ToString()));
                }
                //控制级别ID
                if (!String.IsNullOrEmpty(tEnvPSoil.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPSoil.CONTRAL_LEVEL.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPSoil.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPSoil.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPSoil.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPSoil.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPSoil.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPSoil.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPSoil.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPSoil.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPSoil.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPSoil.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPSoil.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPSoil.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPSoil.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPSoil.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPSoil.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPSoil.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPSoil.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPSoil.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSoil.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSoil.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSoil.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSoil.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSoil.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSoil.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSoil.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSoil.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSoil.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSoil.REMARK5.ToString()));
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
        public bool Create(TEnvPSoilVo TEnvPSoil, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPSoil.SelectMonths.Split(';').ToList();
            TEnvPSoil.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPSoil.ID = GetSerialNumber(strSerial);
                TEnvPSoil.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPSoil, TEnvPSoilVo.T_ENV_P_SOIL_TABLE);
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

            sql = "select * from " + TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE, row, "", "", strTID, strID);
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

        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select ID,POINT_NAME from " + TEnvPSoilVo.T_ENV_P_SOIL_TABLE + " where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
