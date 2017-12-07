using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Item
{
    /// <summary>
    /// 功能：采样仪器管理
    /// 创建日期：2013-06-25
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemSamplingInstrumentAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = "select Count(*) from T_BASE_ITEM_SAMPLING_INSTRUMENT " + this.BuildWhereStatement(tBaseItemSamplingInstrument);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemSamplingInstrumentVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_SAMPLING_INSTRUMENT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseItemSamplingInstrumentVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemSamplingInstrumentVo Details(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_SAMPLING_INSTRUMENT " + this.BuildWhereStatement(tBaseItemSamplingInstrument));
            return SqlHelper.ExecuteObject(new TBaseItemSamplingInstrumentVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemSamplingInstrumentVo> SelectByObject(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_ITEM_SAMPLING_INSTRUMENT " + this.BuildWhereStatement(tBaseItemSamplingInstrument));
            return SqlHelper.ExecuteObjectList(tBaseItemSamplingInstrument, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_ITEM_SAMPLING_INSTRUMENT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseItemSamplingInstrument));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = "select * from T_BASE_ITEM_SAMPLING_INSTRUMENT " + this.BuildWhereStatement(tBaseItemSamplingInstrument);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <returns></returns>
        public TBaseItemSamplingInstrumentVo SelectByObject(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = "select * from T_BASE_ITEM_SAMPLING_INSTRUMENT " + this.BuildWhereStatement(tBaseItemSamplingInstrument);
            return SqlHelper.ExecuteObject(new TBaseItemSamplingInstrumentVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseItemSamplingInstrument, TBaseItemSamplingInstrumentVo.T_BASE_ITEM_SAMPLING_INSTRUMENT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemSamplingInstrument, TBaseItemSamplingInstrumentVo.T_BASE_ITEM_SAMPLING_INSTRUMENT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseItemSamplingInstrument.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseItemSamplingInstrument_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument_UpdateSet, TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemSamplingInstrument_UpdateSet, TBaseItemSamplingInstrumentVo.T_BASE_ITEM_SAMPLING_INSTRUMENT_TABLE);
            strSQL += this.BuildWhereStatement(tBaseItemSamplingInstrument_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ITEM_SAMPLING_INSTRUMENT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            string strSQL = "delete from T_BASE_ITEM_SAMPLING_INSTRUMENT ";
            strSQL += this.BuildWhereStatement(tBaseItemSamplingInstrument);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 创建原因：返回指定监测项目ID的采样仪器，如果有默认则返回默认仪器，如果没有默认，但是有采样仪器记录则返回第一条
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strItemID">监测项目ID</param>
        /// <returns>返回 DataTable</returns>
        public DataTable GetItemSamplingInstrument(string strItemID)
        {
            string strSQL = String.Format(" SELECT * FROM dbo.T_BASE_ITEM_SAMPLING_INSTRUMENT WHERE ITEM_ID='{0}'",strItemID);
            DataTable dt = SqlHelper.ExecuteDataTable(strSQL);
            DataTable dtTemp=dt.Copy();
            dtTemp.Clear();
            if (dt.Rows.Count > 0) {
                DataRow[] drow = dt.Select(" IS_DEFAULT='1'");
                if (drow.Length > 0)
                {
                    foreach (DataRow dr in drow)
                    {
                        dtTemp.ImportRow(dr);
                    }
                }
                else {
                    DataRow drr = dt.Rows[0];
                    dtTemp.ImportRow(drr);
                }
                dtTemp.AcceptChanges();
            }

            return dtTemp;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseItemSamplingInstrument)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseItemSamplingInstrument.ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseItemSamplingInstrument.ITEM_ID.ToString()));
                }
                //0为不默认，1为默认
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.IS_DEFAULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEFAULT = '{0}'", tBaseItemSamplingInstrument.IS_DEFAULT.ToString()));
                }
                //采样仪器名称
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.INSTRUMENT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INSTRUMENT_NAME = '{0}'", tBaseItemSamplingInstrument.INSTRUMENT_NAME.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseItemSamplingInstrument.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseItemSamplingInstrument.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseItemSamplingInstrument.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseItemSamplingInstrument.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseItemSamplingInstrument.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseItemSamplingInstrument.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseItemSamplingInstrument.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}