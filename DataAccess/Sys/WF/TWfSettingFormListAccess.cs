using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：流程表单列表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormListAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_FORM_LIST " + this.BuildWhereStatement(tWfSettingFormList);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormListVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_LIST  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfSettingFormListVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormList">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormListVo Details(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_LIST " + this.BuildWhereStatement(tWfSettingFormList));
            return SqlHelper.ExecuteObject(new TWfSettingFormListVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormListVo> SelectByObject(TWfSettingFormListVo tWfSettingFormList, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_LIST " + this.BuildWhereStatement(tWfSettingFormList));
            return SqlHelper.ExecuteObjectList(tWfSettingFormList, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormListVo tWfSettingFormList, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_SETTING_FORM_LIST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingFormList));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormList"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_LIST " + this.BuildWhereStatement(tWfSettingFormList);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <returns></returns>
        public TWfSettingFormListVo SelectByObject(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_LIST " + this.BuildWhereStatement(tWfSettingFormList);
            return SqlHelper.ExecuteObject(new TWfSettingFormListVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingFormList, TWfSettingFormListVo.T_WF_SETTING_FORM_LIST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormList">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormList, TWfSettingFormListVo.T_WF_SETTING_FORM_LIST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingFormList.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormList_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingFormList_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormListVo tWfSettingFormList_UpdateSet, TWfSettingFormListVo tWfSettingFormList_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormList_UpdateSet, TWfSettingFormListVo.T_WF_SETTING_FORM_LIST_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingFormList_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_FORM_LIST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingFormListVo tWfSettingFormList)
        {
            string strSQL = "delete from T_WF_SETTING_FORM_LIST ";
            strSQL += this.BuildWhereStatement(tWfSettingFormList);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingFormList"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingFormListVo tWfSettingFormList)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingFormList)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfSettingFormList.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingFormList.ID.ToString()));
                }
                //列表编号
                if (!String.IsNullOrEmpty(tWfSettingFormList.UC_LIST_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UC_LIST_ID = '{0}'", tWfSettingFormList.UC_LIST_ID.ToString()));
                }
                //主表单编号
                if (!String.IsNullOrEmpty(tWfSettingFormList.UCM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UCM_ID = '{0}'", tWfSettingFormList.UCM_ID.ToString()));
                }
                //子表单编号
                if (!String.IsNullOrEmpty(tWfSettingFormList.UCS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UCS_ID = '{0}'", tWfSettingFormList.UCS_ID.ToString()));
                }
                //内部排序
                if (!String.IsNullOrEmpty(tWfSettingFormList.CTRL_ORDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CTRL_ORDER = '{0}'", tWfSettingFormList.CTRL_ORDER.ToString()));
                }
                //子表单状态
                if (!String.IsNullOrEmpty(tWfSettingFormList.CTRL_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CTRL_STATE = '{0}'", tWfSettingFormList.CTRL_STATE.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
