using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：环境质量质控设置点位信息
    /// 创建日期：2013-06-25
    /// 创建人：胡方扬
    /// </summary>
    public class TMisPointQcsettingAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = "select Count(*) from T_MIS_POINT_QCSETTING " + this.BuildWhereStatement(tMisPointQcsetting);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisPointQcsettingVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_POINT_QCSETTING  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisPointQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisPointQcsetting">对象条件</param>
        /// <returns>对象</returns>
        public TMisPointQcsettingVo Details(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = String.Format("select * from  T_MIS_POINT_QCSETTING " + this.BuildWhereStatement(tMisPointQcsetting));
            return SqlHelper.ExecuteObject(new TMisPointQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisPointQcsettingVo> SelectByObject(TMisPointQcsettingVo tMisPointQcsetting, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_POINT_QCSETTING " + this.BuildWhereStatement(tMisPointQcsetting));
            return SqlHelper.ExecuteObjectList(tMisPointQcsetting, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisPointQcsettingVo tMisPointQcsetting, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_POINT_QCSETTING {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisPointQcsetting));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisPointQcsetting"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = "select * from T_MIS_POINT_QCSETTING " + this.BuildWhereStatement(tMisPointQcsetting);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <returns></returns>
        public TMisPointQcsettingVo SelectByObject(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = "select * from T_MIS_POINT_QCSETTING " + this.BuildWhereStatement(tMisPointQcsetting);
            return SqlHelper.ExecuteObject(new TMisPointQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisPointQcsetting, TMisPointQcsettingVo.T_MIS_POINT_QCSETTING_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointQcsetting">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisPointQcsetting, TMisPointQcsettingVo.T_MIS_POINT_QCSETTING_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisPointQcsetting.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointQcsetting_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisPointQcsetting_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointQcsettingVo tMisPointQcsetting_UpdateSet, TMisPointQcsettingVo tMisPointQcsetting_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisPointQcsetting_UpdateSet, TMisPointQcsettingVo.T_MIS_POINT_QCSETTING_TABLE);
            strSQL += this.BuildWhereStatement(tMisPointQcsetting_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_POINT_QCSETTING where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisPointQcsettingVo tMisPointQcsetting)
        {
            string strSQL = "delete from T_MIS_POINT_QCSETTING ";
            strSQL += this.BuildWhereStatement(tMisPointQcsetting);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 创建原因：插入环境质量质控设置点位表信息 
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="tMisPointQcsetting">质控设置其他信息</param>
        /// <param name="strPointId">点位数组</param>
        /// <param name="strPointName">点位名称数组</param>
        /// <returns></returns>
        public bool InsertPointInforForArry(TMisPointQcsettingVo tMisPointQcsetting, string[] strPointId, string[] strPointName)
        {
            string strSQL = "";
            ArrayList objArry = new ArrayList();
            if (strPointId.Length > 0 && strPointId.Length == strPointName.Length) {
                string strQcPointIdArr = GetSerialNumberList("t_mis_PointQcSetting_Id", strPointId.Length);
                string[] strQcPoint_Id = strQcPointIdArr.Split(',');
                for (int i = 0; i < strPointId.Length; i++) {
                    string strTempSQL = String.Format(@" SELECT * FROM T_MIS_POINT_QCSETTING WHERE POINT_ID='{0}' AND MONITOR_ID='{1}' AND YEAR='{2}' AND MONTH='{3}'", strPointId[i],tMisPointQcsetting.MONITOR_ID, tMisPointQcsetting.YEAR, tMisPointQcsetting.MONTH);
                    DataTable objDt = SqlHelper.ExecuteDataTable(strTempSQL);
                    if (objDt.Rows.Count <= 0)
                    {
                        strSQL = String.Format(@" INSERT INTO dbo.T_MIS_POINT_QCSETTING (ID, POINT_ID, POINT_NAME, MONITOR_ID, YEAR, MONTH, PROJECT_NAME) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            strQcPoint_Id[i], strPointId[i], strPointName[i], tMisPointQcsetting.MONITOR_ID, tMisPointQcsetting.YEAR, tMisPointQcsetting.MONTH, tMisPointQcsetting.PROJECT_NAME);
                        objArry.Add(strSQL);
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(objArry);
        
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisPointQcsetting"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisPointQcsettingVo tMisPointQcsetting)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisPointQcsetting)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisPointQcsetting.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisPointQcsetting.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tMisPointQcsetting.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tMisPointQcsetting.POINT_ID.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tMisPointQcsetting.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME LIKE '%{0}%'", tMisPointQcsetting.POINT_NAME.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tMisPointQcsetting.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tMisPointQcsetting.MONITOR_ID.ToString()));
                }
                //监测年度
                if (!String.IsNullOrEmpty(tMisPointQcsetting.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tMisPointQcsetting.YEAR.ToString()));
                }
                //监测月份
                if (!String.IsNullOrEmpty(tMisPointQcsetting.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tMisPointQcsetting.MONTH.ToString()));
                }
                //质控计划名称
                if (!String.IsNullOrEmpty(tMisPointQcsetting.PROJECT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_NAME = '{0}'", tMisPointQcsetting.PROJECT_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisPointQcsetting.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisPointQcsetting.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisPointQcsetting.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisPointQcsetting.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisPointQcsetting.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisPointQcsetting.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisPointQcsetting.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisPointQcsetting.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisPointQcsetting.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisPointQcsetting.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
