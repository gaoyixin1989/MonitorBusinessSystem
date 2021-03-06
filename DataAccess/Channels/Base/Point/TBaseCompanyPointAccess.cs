using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Point;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Point
{
    /// <summary>
    /// 功能：监测点信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyPointAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = "select Count(*) from T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseCompanyPointVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointVo Details(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint));
            return SqlHelper.ExecuteObject(new TBaseCompanyPointVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyPointVo> SelectByObject(TBaseCompanyPointVo tBaseCompanyPoint, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint));
            return SqlHelper.ExecuteObjectList(tBaseCompanyPoint, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyPointVo tBaseCompanyPoint, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_COMPANY_POINT {0} order by MONITOR_ID,NUM";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseCompanyPoint));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = "select * from T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <returns></returns>
        public TBaseCompanyPointVo SelectByObject(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = "select * from T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint);
            return SqlHelper.ExecuteObject(new TBaseCompanyPointVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseCompanyPoint, TBaseCompanyPointVo.T_BASE_COMPANY_POINT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyPoint, TBaseCompanyPointVo.T_BASE_COMPANY_POINT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseCompanyPoint.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseCompanyPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointVo tBaseCompanyPoint_UpdateSet, TBaseCompanyPointVo tBaseCompanyPoint_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyPoint_UpdateSet, TBaseCompanyPointVo.T_BASE_COMPANY_POINT_TABLE);
            strSQL += this.BuildWhereStatement(tBaseCompanyPoint_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_COMPANY_POINT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = "delete from T_BASE_COMPANY_POINT ";
            strSQL += this.BuildWhereStatement(tBaseCompanyPoint);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// 批量增加
        /// </summary>
        /// <param name="arrVo"></param>
        /// <returns></returns>
        public bool SaveData(ArrayList arrVo)
        {
            string strSQL = "";
            ArrayList arrSql = new ArrayList();
            foreach (TBaseCompanyPointVo objvo in arrVo)
            {
                objvo.ID = GetSerialNumber("t_base_company_point_id");
                strSQL = SqlHelper.BuildInsertExpress(objvo, TBaseCompanyPointVo.T_BASE_COMPANY_POINT_TABLE);
                arrSql.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        public DataTable SelectByTableForPlan(TBaseCompanyPointVo tBaseCompanyPoint, string strPointIdList, int iIndex, int iCount)
        {
            string strPointId = strPointIdList.Replace(";", "','");

            string strSQL = " select * from T_BASE_COMPANY_POINT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseCompanyPoint));

            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND ID NOT IN('{0}')", strPointId);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        public int SelectByTableForPlanCount(TBaseCompanyPointVo tBaseCompanyPoint, string strPointIdList)
        {
            string strPointId = strPointIdList.Replace(";", "','");
            string strSQL = "select Count(*) from T_BASE_COMPANY_POINT " + this.BuildWhereStatement(tBaseCompanyPoint);
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND ID NOT IN('{0}')", strPointId);
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseCompanyPoint"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseCompanyPoint)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseCompanyPoint.ID.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tBaseCompanyPoint.POINT_NAME.ToString()));
                }
                //委托类型
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.POINT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_TYPE = '{0}'", tBaseCompanyPoint.POINT_TYPE.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseCompanyPoint.MONITOR_ID.ToString()));
                }
                //采样频次
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.SAMPLE_FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FREQ = '{0}'", tBaseCompanyPoint.SAMPLE_FREQ.ToString()));
                }
                //监测频次
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FREQ = '{0}'", tBaseCompanyPoint.FREQ.ToString()));
                }
                //所属企业ID
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_ID = '{0}'", tBaseCompanyPoint.COMPANY_ID.ToString()));
                }
                //动态属性ID
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.DYNAMIC_ATTRIBUTE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DYNAMIC_ATTRIBUTE_ID = '{0}'", tBaseCompanyPoint.DYNAMIC_ATTRIBUTE_ID.ToString()));
                }
                //建成时间
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.CREATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_DATE = '{0}'", tBaseCompanyPoint.CREATE_DATE.ToString()));
                }
                //监测点位置
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tBaseCompanyPoint.ADDRESS.ToString()));
                }
                //经度
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.LONGITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tBaseCompanyPoint.LONGITUDE.ToString()));
                }
                //纬度
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.LATITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tBaseCompanyPoint.LATITUDE.ToString()));
                }
                //点位描述
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseCompanyPoint.DESCRIPTION.ToString()));
                }
                //国标条件项
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.NATIONAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NATIONAL_ST_CONDITION_ID = '{0}'", tBaseCompanyPoint.NATIONAL_ST_CONDITION_ID.ToString()));
                }
                //行标条件项ID
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.INDUSTRY_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INDUSTRY_ST_CONDITION_ID = '{0}'", tBaseCompanyPoint.INDUSTRY_ST_CONDITION_ID.ToString()));
                }
                //地标条件项_ID
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.LOCAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCAL_ST_CONDITION_ID = '{0}'", tBaseCompanyPoint.LOCAL_ST_CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseCompanyPoint.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tBaseCompanyPoint.NUM.ToString()));
                }
                //监测周期
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.SAMPLE_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_DAY = '{0}'", tBaseCompanyPoint.SAMPLE_DAY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseCompanyPoint.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseCompanyPoint.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseCompanyPoint.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseCompanyPoint.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseCompanyPoint.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseCompanyPoint.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
