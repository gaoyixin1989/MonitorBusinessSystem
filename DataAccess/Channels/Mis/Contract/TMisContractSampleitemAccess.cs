using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书样品项目明细
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSampleitemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_SAMPLEITEM " + this.BuildWhereStatement(tMisContractSampleitem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSampleitemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLEITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractSampleitemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSampleitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSampleitemVo Details(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLEITEM " + this.BuildWhereStatement(tMisContractSampleitem));
            return SqlHelper.ExecuteObject(new TMisContractSampleitemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSampleitemVo> SelectByObject(TMisContractSampleitemVo tMisContractSampleitem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLEITEM " + this.BuildWhereStatement(tMisContractSampleitem));
            return SqlHelper.ExecuteObjectList(tMisContractSampleitem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSampleitemVo tMisContractSampleitem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_SAMPLEITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractSampleitem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLEITEM " + this.BuildWhereStatement(tMisContractSampleitem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <returns></returns>
        public TMisContractSampleitemVo SelectByObject(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLEITEM " + this.BuildWhereStatement(tMisContractSampleitem);
            return SqlHelper.ExecuteObject(new TMisContractSampleitemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractSampleitem, TMisContractSampleitemVo.T_MIS_CONTRACT_SAMPLEITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSampleitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSampleitem, TMisContractSampleitemVo.T_MIS_CONTRACT_SAMPLEITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractSampleitem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSampleitem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSampleitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleitemVo tMisContractSampleitem_UpdateSet, TMisContractSampleitemVo tMisContractSampleitem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSampleitem_UpdateSet, TMisContractSampleitemVo.T_MIS_CONTRACT_SAMPLEITEM_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractSampleitem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_SAMPLEITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractSampleitemVo tMisContractSampleitem)
        {
            string strSQL = "delete from T_MIS_CONTRACT_SAMPLEITEM ";
            strSQL += this.BuildWhereStatement(tMisContractSampleitem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 获取自送样监测项目信息
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <param name="tMisContractSample"></param>
        /// <returns></returns>
        public DataTable SelectMonitorForSample(TMisContractSampleitemVo tMisContractSampleitem,TMisContractSampleVo tMisContractSample) 
                {
                        string strSQL = String.Format("SELECT A.ID,A.CONTRACT_SAMPLE_ID,A.ITEM_ID,B.CONTRACT_ID,B.MONITOR_ID,B.SAMPLE_NAME,B.SAMPLE_TYPE,B.SAMPLE_COUNT FROM dbo.T_MIS_CONTRACT_SAMPLEITEM A " +
                                                                        " INNER JOIN dbo.T_MIS_CONTRACT_SAMPLE B ON A.CONTRACT_SAMPLE_ID=B.ID WHERE 1=1");
                        if (!String.IsNullOrEmpty(tMisContractSampleitem.CONTRACT_SAMPLE_ID))
                        {
                            strSQL += String.Format(" AND A.CONTRACT_SAMPLE_ID='{0}'", tMisContractSampleitem.CONTRACT_SAMPLE_ID);
                        }
                        if (!String.IsNullOrEmpty(tMisContractSample.CONTRACT_ID))
                        {
                            strSQL += String.Format(" AND B.CONTRACT_ID='{0}'", tMisContractSample.CONTRACT_ID);
                        }
                        if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_PLAN_ID))
                        {
                            strSQL += String.Format(" AND B.SAMPLE_PLAN_ID='{0}'", tMisContractSample.SAMPLE_PLAN_ID);
                        }
                        if (!String.IsNullOrEmpty(tMisContractSample.MONITOR_ID))
                        {
                            strSQL += String.Format(" AND B.MONITOR_ID='{0}'", tMisContractSample.MONITOR_ID);
                        }

                        return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 删除自送样委托书监测点位的监测项目 Create By Castle （胡方扬） 2012-12-20
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveSampleItems(TMisContractSampleitemVo tMisContractSampleitem, string[] strMovePointItems)
        {
            ArrayList arrVo = new ArrayList();

            foreach (string strItemId in strMovePointItems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strSQL = String.Format("DELETE FROM T_MIS_CONTRACT_SAMPLEITEM WHERE CONTRACT_SAMPLE_ID='{0}' AND ITEM_ID = '{1}'", tMisContractSampleitem.CONTRACT_SAMPLE_ID, strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 向自送样委托书监测项目中插入保存了且不存在的数据  Create By Castle （胡方扬） 2012-12-20
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddPointItems"></param>
        /// <returns></returns>
        public bool EditSampleItems(TMisContractSampleitemVo tMisContractSampleitem, string[] strAddPointItems)
        {
            ArrayList arrVo = new ArrayList();

            foreach (string strItemId in strAddPointItems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    tMisContractSampleitem.ID = GetSerialNumber("t_mis_contract_sampleItemId");
                    string strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_SAMPLEITEM(ID,CONTRACT_SAMPLE_ID,ITEM_ID) VALUES('{0}','{1}','{2}')", tMisContractSampleitem.ID, tMisContractSampleitem.CONTRACT_SAMPLE_ID, strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取样品监测项目 胡方扬 2013-04-28
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <returns></returns>
        public DataTable GetSampleItemsInfor(TMisContractSampleitemVo tMisContractSampleitem) {
            string strSQ = @"SELECT A.ID,A.CONTRACT_SAMPLE_ID,A.ITEM_ID,B.ITEM_NAME,B.IS_SAMPLEDEPT,B.LAB_CERTIFICATE FROM T_MIS_CONTRACT_SAMPLEITEM A
                                   INNER JOIN T_BASE_ITEM_INFO B ON B.ID=A.ITEM_ID AND B.IS_DEL='0' WHERE 1=1";
            if(!String.IsNullOrEmpty(tMisContractSampleitem.ID)){
                strSQ+=String.Format(" AND A.ID='{0}'",tMisContractSampleitem.ID);
            }

            if(!String.IsNullOrEmpty(tMisContractSampleitem.CONTRACT_SAMPLE_ID)){
                strSQ+=String.Format(" AND A.CONTRACT_SAMPLE_ID='{0}'",tMisContractSampleitem.CONTRACT_SAMPLE_ID);
            }

            if (!String.IsNullOrEmpty(tMisContractSampleitem.ITEM_ID))
            {
                strSQ += String.Format(" AND A.ITEM_ID='{0}'", tMisContractSampleitem.ITEM_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQ);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractSampleitemVo tMisContractSampleitem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractSampleitem)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractSampleitem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractSampleitem.ID.ToString()));
                }
                //委托书样品ID
                if (!String.IsNullOrEmpty(tMisContractSampleitem.CONTRACT_SAMPLE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_SAMPLE_ID = '{0}'", tMisContractSampleitem.CONTRACT_SAMPLE_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tMisContractSampleitem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisContractSampleitem.ITEM_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisContractSampleitem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractSampleitem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContractSampleitem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractSampleitem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContractSampleitem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractSampleitem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContractSampleitem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractSampleitem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisContractSampleitem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractSampleitem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
