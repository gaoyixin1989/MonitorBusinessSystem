using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Solid;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Solid
{
    /// <summary>
    /// 功能：固废
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSolidAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = "select Count(*) from T_ENV_P_SOLID " + this.BuildWhereStatement(tEnvPSolid);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSolidVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOLID  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSolidVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSolid">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSolidVo Details(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOLID " + this.BuildWhereStatement(tEnvPSolid));
            return SqlHelper.ExecuteObject(new TEnvPSolidVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSolidVo> SelectByObject(TEnvPSolidVo tEnvPSolid, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SOLID " + this.BuildWhereStatement(tEnvPSolid));
            return SqlHelper.ExecuteObjectList(tEnvPSolid, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSolidVo tEnvPSolid, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SOLID {0}   order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSolid));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSolid"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = "select * from T_ENV_P_SOLID " + this.BuildWhereStatement(tEnvPSolid);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <returns></returns>
        public TEnvPSolidVo SelectByObject(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = "select * from T_ENV_P_SOLID " + this.BuildWhereStatement(tEnvPSolid);
            return SqlHelper.ExecuteObject(new TEnvPSolidVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSolid, TEnvPSolidVo.T_ENV_P_SOLID_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolid">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSolid, TEnvPSolidVo.T_ENV_P_SOLID_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSolid.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolid_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSolid_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidVo tEnvPSolid_UpdateSet, TEnvPSolidVo tEnvPSolid_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSolid_UpdateSet, TEnvPSolidVo.T_ENV_P_SOLID_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSolid_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SOLID where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSolidVo tEnvPSolid)
        {
            string strSQL = "delete from T_ENV_P_SOLID ";
            strSQL += this.BuildWhereStatement(tEnvPSolid);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSolid"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSolidVo tEnvPSolid)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSolid)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSolid.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSolid.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPSolid.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPSolid.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPSolid.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPSolid.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSolid.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPSolid.SATAIONS_ID.ToString()));
                }
                //点位代码
                if (!String.IsNullOrEmpty(tEnvPSolid.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPSolid.POINT_CODE.ToString()));
                }
                //点位名称
                if (!String.IsNullOrEmpty(tEnvPSolid.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPSolid.POINT_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPSolid.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPSolid.AREA_ID.ToString()));
                }
                //所属省份ID
                if (!String.IsNullOrEmpty(tEnvPSolid.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPSolid.PROVINCE_ID.ToString()));
                }
                //控制级别ID
                if (!String.IsNullOrEmpty(tEnvPSolid.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPSolid.CONTRAL_LEVEL.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPSolid.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPSolid.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPSolid.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPSolid.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPSolid.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPSolid.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPSolid.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPSolid.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPSolid.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPSolid.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPSolid.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPSolid.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPSolid.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPSolid.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPSolid.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPSolid.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPSolid.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPSolid.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSolid.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSolid.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSolid.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSolid.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSolid.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSolid.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSolid.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSolid.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSolid.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSolid.REMARK5.ToString()));
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
        public bool Create(TEnvPSolidVo TEnvPSolid, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPSolid.SelectMonths.Split(';').ToList();
            TEnvPSolid.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPSolid.ID = GetSerialNumber(strSerial);
                TEnvPSolid.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPSolid, TEnvPSolidVo.T_ENV_P_SOLID_TABLE);
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

            sql = "select * from " + TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE, row, "", "", strTID, strID);
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
            string strSQL = "select ID,POINT_NAME from " + TEnvPSolidVo.T_ENV_P_SOLID_TABLE + " where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
