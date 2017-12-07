using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Seabath;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Seabath
{
    /// <summary>
    /// 功能：海水浴场
    /// 创建日期：2013-06-18
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvPSeabathAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEABATH " + this.BuildWhereStatement(tEnvPSeabath);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSeabathVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEABATH  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSeabathVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSeabath">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSeabathVo Details(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEABATH " + this.BuildWhereStatement(tEnvPSeabath));
            return SqlHelper.ExecuteObject(new TEnvPSeabathVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSeabathVo> SelectByObject(TEnvPSeabathVo tEnvPSeabath, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEABATH " + this.BuildWhereStatement(tEnvPSeabath));
            return SqlHelper.ExecuteObjectList(tEnvPSeabath, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSeabathVo tEnvPSeabath, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEABATH {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSeabath));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

     
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <returns></returns>
        public TEnvPSeabathVo SelectByObject(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = "select * from T_ENV_P_SEABATH " + this.BuildWhereStatement(tEnvPSeabath);
            return SqlHelper.ExecuteObject(new TEnvPSeabathVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSeabath, TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathVo tEnvPSeabath, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPSeabath.SelectMonths.Split(';').ToList();
            tEnvPSeabath.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPSeabath.ID = GetSerialNumber(Number);
                tEnvPSeabath.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPSeabath, TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabath">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSeabath, TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSeabath.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabath_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSeabath_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathVo tEnvPSeabath_UpdateSet, TEnvPSeabathVo tEnvPSeabath_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSeabath_UpdateSet, TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSeabath_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEABATH where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = "delete from T_ENV_P_SEABATH ";
            strSQL += this.BuildWhereStatement(tEnvPSeabath);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSeabathVo tEnvPSeabath)
        {
            string strSQL = "select * from T_ENV_P_SEABATH " + this.BuildWhereStatement(tEnvPSeabath);
            return SqlHelper.ExecuteDataTable(strSQL);
        }



        #region//监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// <param name="tEnvPSeabath"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSeabathVo tEnvPSeabath)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSeabath)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPSeabath.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSeabath.ID.ToString()));
                }
                //年份
                if (!String.IsNullOrEmpty(tEnvPSeabath.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPSeabath.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPSeabath.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPSeabath.MONTH.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tEnvPSeabath.POINT_NAME.ToString().Trim())) 
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPSeabath.POINT_NAME.ToString()));
                }
                //
                //监测点代码
                if (!String.IsNullOrEmpty(tEnvPSeabath.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPSeabath.POINT_NAME.ToString()));
                }
                //
                //功能区代码
                if (!String.IsNullOrEmpty(tEnvPSeabath.FUNCTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_CODE = '{0}'", tEnvPSeabath.FUNCTION_CODE.ToString()));
                }
                //海区ID(字典项)
                if (!String.IsNullOrEmpty(tEnvPSeabath.SEA_AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEA_AREA_ID = '{0}'", tEnvPSeabath.SEA_AREA_ID.ToString()));
                }
                //重点海域ID(字典项)
                if (!String.IsNullOrEmpty(tEnvPSeabath.KEY_SEA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KEY_SEA_ID = '{0}'", tEnvPSeabath.KEY_SEA_ID.ToString()));
                }
                //国家编号
                if (!String.IsNullOrEmpty(tEnvPSeabath.COUNTRY_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COUNTRY_CODE = '{0}'", tEnvPSeabath.COUNTRY_CODE.ToString()));
                }
                //省份编号
                if (!String.IsNullOrEmpty(tEnvPSeabath.PROVINCE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_CODE = '{0}'", tEnvPSeabath.PROVINCE_CODE.ToString()));
                }
                //点位类别
                if (!String.IsNullOrEmpty(tEnvPSeabath.POINT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_TYPE = '{0}'", tEnvPSeabath.POINT_TYPE.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPSeabath.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPSeabath.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPSeabath.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPSeabath.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPSeabath.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPSeabath.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPSeabath.LATITUDE_SECOND.ToString()));
                }
                //具体位置
                if (!String.IsNullOrEmpty(tEnvPSeabath.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPSeabath.LOCATION.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPSeabath.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPSeabath.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPSeabath.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPSeabath.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPSeabath.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPSeabath.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSeabath.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSeabath.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSeabath.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSeabath.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSeabath.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSeabath.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSeabath.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSeabath.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSeabath.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSeabath.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
