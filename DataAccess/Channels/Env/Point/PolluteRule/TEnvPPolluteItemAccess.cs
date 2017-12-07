using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using System.Data;
using System.Collections;
using i3.ValueObject.Channels.Env.Point.Offshore;

namespace i3.DataAccess.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_POLLUTE_ITEM " + this.BuildWhereStatement(tEnvPPolluteItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPPolluteItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteItemVo Details(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_ITEM " + this.BuildWhereStatement(tEnvPPolluteItem));
            return SqlHelper.ExecuteObject(new TEnvPPolluteItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteItemVo> SelectByObject(TEnvPPolluteItemVo tEnvPPolluteItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_ITEM " + this.BuildWhereStatement(tEnvPPolluteItem));
            return SqlHelper.ExecuteObjectList(tEnvPPolluteItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteItemVo tEnvPPolluteItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_POLLUTE_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPPolluteItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPolluteItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE_ITEM " + this.BuildWhereStatement(tEnvPPolluteItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <returns></returns>
        public TEnvPPolluteItemVo SelectByObject(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE_ITEM " + this.BuildWhereStatement(tEnvPPolluteItem);
            return SqlHelper.ExecuteObject(new TEnvPPolluteItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPPolluteItem, TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPolluteItem, TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPPolluteItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPolluteItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteItemVo tEnvPPolluteItem_UpdateSet, TEnvPPolluteItemVo tEnvPPolluteItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPolluteItem_UpdateSet, TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPPolluteItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_POLLUTE_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            string strSQL = "delete from T_ENV_P_POLLUTE_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPPolluteItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #endregion

        #region//监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)//strfid为源复制的监测项目ID，strTid为目标复制的监测项目ID
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;
            string originalType = this.GetType(strFID);
            string targetType = this.GetType(strTID);
            if (!string.IsNullOrEmpty(originalType) && !string.IsNullOrEmpty(targetType))
            {
                if (originalType == targetType)
                {
                    #region//都为相同类型时，可以复制
                    sql = "select * from " + TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
                    dt = SqlHelper.ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "delete from " + TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                        list.Add(sql);
                        foreach (DataRow row in dt.Rows)
                        {
                            strID = GetSerialNumber(strSerial);
                            sql = com.getCopySql(TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE, row, "", "", strTID, strID);
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
                    #endregion
                }
                else
                {
                    Msg = "两个监测点的类型不一致,不能复制";
                }
            }
            if (b)
                return "({result:true,msg:''})";
            else
                return "({result:false,msg:'" + Msg + "'})";
        }
        public string GetType(string id)
        {
            string strSQL = "SELECT B.TYPE_NAME FROM T_ENV_P_POLLUTE A LEFT JOIN  T_ENV_P_POLLUTE_TYPE B ON A.TYPE_ID=B.ID where A.ID='"+id+"'";
            return SqlHelper.ExecuteScalar(strSQL).ToString();
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPPolluteItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPPolluteItem)
            {

                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPPolluteItem.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPPolluteItem.POINT_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPPolluteItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPPolluteItem.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPPolluteItem.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPPolluteItem.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPPolluteItem.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPPolluteItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
