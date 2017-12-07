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
    /// 功能：环境质量质控设置监测项目信息
    /// 创建日期：2013-06-25
    /// 创建人：胡方扬
    /// </summary>
    public class TMisPointitemQcsettingAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = "select Count(*) from T_MIS_POINTITEM_QCSETTING " + this.BuildWhereStatement(tMisPointitemQcsetting);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisPointitemQcsettingVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_POINTITEM_QCSETTING  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisPointitemQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象条件</param>
        /// <returns>对象</returns>
        public TMisPointitemQcsettingVo Details(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = String.Format("select * from  T_MIS_POINTITEM_QCSETTING " + this.BuildWhereStatement(tMisPointitemQcsetting));
            return SqlHelper.ExecuteObject(new TMisPointitemQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisPointitemQcsettingVo> SelectByObject(TMisPointitemQcsettingVo tMisPointitemQcsetting, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_POINTITEM_QCSETTING " + this.BuildWhereStatement(tMisPointitemQcsetting));
            return SqlHelper.ExecuteObjectList(tMisPointitemQcsetting, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisPointitemQcsettingVo tMisPointitemQcsetting, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_POINTITEM_QCSETTING {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisPointitemQcsetting));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisPointitemQcsetting"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = "select * from T_MIS_POINTITEM_QCSETTING " + this.BuildWhereStatement(tMisPointitemQcsetting);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <returns></returns>
        public TMisPointitemQcsettingVo SelectByObject(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = "select * from T_MIS_POINTITEM_QCSETTING " + this.BuildWhereStatement(tMisPointitemQcsetting);
            return SqlHelper.ExecuteObject(new TMisPointitemQcsettingVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisPointitemQcsetting, TMisPointitemQcsettingVo.T_MIS_POINTITEM_QCSETTING_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointitemQcsetting">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisPointitemQcsetting, TMisPointitemQcsettingVo.T_MIS_POINTITEM_QCSETTING_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisPointitemQcsetting.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointitemQcsetting_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisPointitemQcsetting_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointitemQcsettingVo tMisPointitemQcsetting_UpdateSet, TMisPointitemQcsettingVo tMisPointitemQcsetting_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisPointitemQcsetting_UpdateSet, TMisPointitemQcsettingVo.T_MIS_POINTITEM_QCSETTING_TABLE);
            strSQL += this.BuildWhereStatement(tMisPointitemQcsetting_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_POINTITEM_QCSETTING where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = "delete from T_MIS_POINTITEM_QCSETTING ";
            strSQL += this.BuildWhereStatement(tMisPointitemQcsetting);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 创建原因：插入环境质量质控设置点位监测项目表信息 
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="tMisPointitemQcsetting">质控设置监测信息</param>
        /// <param name="strPointItemsId">监测项目ID</param>
        /// <param name="strPointItemsName">监测项目名称</param>
        /// <returns></returns>
        public bool InsertQcSettingPointItems(TMisPointitemQcsettingVo tMisPointitemQcsetting, string strPointItemsId,string strPointItemsName) {
            string strSQL = "";
            ArrayList objVo = new ArrayList();
            string[] objPointItemsArr = strPointItemsId.Split(';');
            string[] objPointItemsNameArr = strPointItemsName.Split(';');
            string strTempSql = String.Format(@" DELETE T_MIS_POINTITEM_QCSETTING WHERE POINT_QCSETTING_ID='{0}' AND QC_TYPE='{1}'", tMisPointitemQcsetting.POINT_QCSETTING_ID, tMisPointitemQcsetting.QC_TYPE);
            int intFlag = SqlHelper.ExecuteNonQuery(strTempSql);
            if (objPointItemsArr.Length > 0 &&objPointItemsArr.Length==objPointItemsNameArr.Length)
            {
                for (int i = 0; i < objPointItemsArr.Length;i++ )
                {
                    strSQL = String.Format(" INSERT INTO T_MIS_POINTITEM_QCSETTING(ID,POINT_QCSETTING_ID,ITEM_ID,QC_TYPE,ITEM_NAME) VALUES('{0}','{1}','{2}','{3}','{4}')", GetSerialNumber("t_mis_PointitemQcSetting_Id"), tMisPointitemQcsetting.POINT_QCSETTING_ID, objPointItemsArr[i], tMisPointitemQcsetting.QC_TYPE, objPointItemsNameArr[i]);
                    objVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }

        /// <summary>
        /// 创建原因：动态获取环境质量监测点位的监测项目信息
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItems(string strTableName, string strKeyColumns, string strPointId) {
            string strSQL = "";
            strSQL = String.Format(@" SELECT A.ITEM_ID AS ID,B.ITEM_NAME FROM {0} A", strTableName);
            strSQL += String.Format(@" LEFT JOIN T_BASE_ITEM_INFO B ON B.ID=A.ITEM_ID WHERE A.{0}='{1}'", strKeyColumns, strPointId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 创建原因：动态获取环境质量监测点位的监测项目信息 有父表的数据
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItemsForFather(string strTableName,string strFatherTable,string strFatherKeyColumns, string strKeyColumns, string strPointId)
        {
            string strSQL = "";
            strSQL = String.Format(@" SELECT A.ITEM_ID AS ID,C.ITEM_NAME FROM {0} A", strTableName);
            strSQL += String.Format(@" LEFT JOIN {0} B ON B.ID=A.{1}", strFatherTable,strKeyColumns);
            strSQL += String.Format(@" LEFT JOIN T_BASE_ITEM_INFO C ON C.ID=A.ITEM_ID WHERE B.{0}='{1}'", strFatherKeyColumns, strPointId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 创建原因：获取指定质控点位下的监测项目信息
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItemsForQcSetting(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            string strSQL = "";
            strSQL = String.Format(@" SELECT A.ITEM_ID AS ID,B.ITEM_NAME FROM T_MIS_POINTITEM_QCSETTING A ");
            strSQL += String.Format(@" LEFT JOIN T_BASE_ITEM_INFO B ON B.ID=A.ITEM_ID WHERE A.POINT_QCSETTING_ID='{0}' AND QC_TYPE='{1}'", tMisPointitemQcsetting.POINT_QCSETTING_ID, tMisPointitemQcsetting.QC_TYPE);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisPointitemQcsetting"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisPointitemQcsetting)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisPointitemQcsetting.ID.ToString()));
                }
                //环境质量质控计划ID
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.POINT_QCSETTING_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_QCSETTING_ID = '{0}'", tMisPointitemQcsetting.POINT_QCSETTING_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisPointitemQcsetting.ITEM_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.ITEM_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_NAME = '{0}'", tMisPointitemQcsetting.ITEM_NAME.ToString()));
                }
                //现场平行0，现场空白1
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisPointitemQcsetting.QC_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisPointitemQcsetting.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisPointitemQcsetting.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisPointitemQcsetting.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisPointitemQcsetting.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisPointitemQcsetting.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisPointitemQcsetting.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
