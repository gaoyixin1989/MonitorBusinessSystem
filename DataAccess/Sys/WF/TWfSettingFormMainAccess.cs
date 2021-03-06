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
    /// 功能：流程主表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormMainAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_FORM_MAIN " + this.BuildWhereStatement(tWfSettingFormMain);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormMainVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_MAIN  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingFormMainVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormMain">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormMainVo Details(TWfSettingFormMainVo tWfSettingFormMain)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_FORM_MAIN " + this.BuildWhereStatement(tWfSettingFormMain));
           return SqlHelper.ExecuteObject(new TWfSettingFormMainVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormMainVo> SelectByObject(TWfSettingFormMainVo tWfSettingFormMain, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_MAIN " + this.BuildWhereStatement(tWfSettingFormMain));
            return SqlHelper.ExecuteObjectList(tWfSettingFormMain, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormMainVo tWfSettingFormMain, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_FORM_MAIN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingFormMain));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormMain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_MAIN " + this.BuildWhereStatement(tWfSettingFormMain);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <returns></returns>
        public TWfSettingFormMainVo SelectByObject(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_MAIN " + this.BuildWhereStatement(tWfSettingFormMain);
            return SqlHelper.ExecuteObject(new TWfSettingFormMainVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingFormMain, TWfSettingFormMainVo.T_WF_SETTING_FORM_MAIN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormMain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormMain, TWfSettingFormMainVo.T_WF_SETTING_FORM_MAIN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingFormMain.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormMain_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingFormMain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormMainVo tWfSettingFormMain_UpdateSet, TWfSettingFormMainVo tWfSettingFormMain_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormMain_UpdateSet, TWfSettingFormMainVo.T_WF_SETTING_FORM_MAIN_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingFormMain_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_FORM_MAIN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingFormMainVo tWfSettingFormMain)
        {
            string strSQL = "delete from T_WF_SETTING_FORM_MAIN ";
	    strSQL += this.BuildWhereStatement(tWfSettingFormMain);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingFormMain"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingFormMainVo tWfSettingFormMain)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingFormMain)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingFormMain.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingFormMain.ID.ToString()));
				}	
				//主表单编号
				if (!String.IsNullOrEmpty(tWfSettingFormMain.UCM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_ID = '{0}'", tWfSettingFormMain.UCM_ID.ToString()));
				}	
				//主表单简称
				if (!String.IsNullOrEmpty(tWfSettingFormMain.UCM_CAPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_CAPTION = '{0}'", tWfSettingFormMain.UCM_CAPTION.ToString()));
				}	
				//主表单内编码
				if (!String.IsNullOrEmpty(tWfSettingFormMain.UCM_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_NOTE = '{0}'", tWfSettingFormMain.UCM_NOTE.ToString()));
				}	
				//主表单类型
				if (!String.IsNullOrEmpty(tWfSettingFormMain.UCM_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_TYPE = '{0}'", tWfSettingFormMain.UCM_TYPE.ToString()));
				}	
				//主表单描述
				if (!String.IsNullOrEmpty(tWfSettingFormMain.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tWfSettingFormMain.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
