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
    /// 功能：监测项目子项管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemSubItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = "select Count(*) from T_BASE_ITEM_SUB_ITEM " + this.BuildWhereStatement(tBaseItemSubItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_SUB_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseItemSubItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemSubItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo Details(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_SUB_ITEM " + this.BuildWhereStatement(tBaseItemSubItem));
            return SqlHelper.ExecuteObject(new TBaseItemSubItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemSubItemVo> SelectByObject(TBaseItemSubItemVo tBaseItemSubItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_ITEM_SUB_ITEM " + this.BuildWhereStatement(tBaseItemSubItem));
            return SqlHelper.ExecuteObjectList(tBaseItemSubItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemSubItemVo tBaseItemSubItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_ITEM_SUB_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseItemSubItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemSubItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = "select * from T_BASE_ITEM_SUB_ITEM " + this.BuildWhereStatement(tBaseItemSubItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <returns></returns>
        public TBaseItemSubItemVo SelectByObject(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = "select * from T_BASE_ITEM_SUB_ITEM " + this.BuildWhereStatement(tBaseItemSubItem);
            return SqlHelper.ExecuteObject(new TBaseItemSubItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseItemSubItem, TBaseItemSubItemVo.T_BASE_ITEM_SUB_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSubItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemSubItem, TBaseItemSubItemVo.T_BASE_ITEM_SUB_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseItemSubItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSubItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseItemSubItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSubItemVo tBaseItemSubItem_UpdateSet, TBaseItemSubItemVo tBaseItemSubItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemSubItem_UpdateSet, TBaseItemSubItemVo.T_BASE_ITEM_SUB_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tBaseItemSubItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ITEM_SUB_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseItemSubItemVo tBaseItemSubItem)
        {
            string strSQL = "delete from T_BASE_ITEM_SUB_ITEM ";
            strSQL += this.BuildWhereStatement(tBaseItemSubItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获得对象
        /// </summary>
        /// <param name="strItemiD">子项ID</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo getParentIDByItem(string strItemiD)
        {
            string strSQL = string.Format("select * from T_BASE_ITEM_SUB_ITEM where ITEM_ID='{0}'", strItemiD);

            return SqlHelper.ExecuteObject(new TBaseItemSubItemVo(), strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseItemSubItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseItemSubItemVo tBaseItemSubItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseItemSubItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseItemSubItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseItemSubItem.ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tBaseItemSubItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseItemSubItem.ITEM_ID.ToString()));
                }
                //监测父项ID
                if (!String.IsNullOrEmpty(tBaseItemSubItem.PARENT_ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_ITEM_ID = '{0}'", tBaseItemSubItem.PARENT_ITEM_ID.ToString()));
                }
                //0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseItemSubItem.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseItemSubItem.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseItemSubItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseItemSubItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseItemSubItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseItemSubItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseItemSubItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseItemSubItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseItemSubItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseItemSubItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseItemSubItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseItemSubItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
