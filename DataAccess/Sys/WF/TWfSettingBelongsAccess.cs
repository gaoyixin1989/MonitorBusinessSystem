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
    /// 功能：流程分类
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingBelongsAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_BELONGS " + this.BuildWhereStatement(tWfSettingBelongs);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingBelongsVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_BELONGS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingBelongsVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingBelongs">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingBelongsVo Details(TWfSettingBelongsVo tWfSettingBelongs)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_BELONGS " + this.BuildWhereStatement(tWfSettingBelongs));
           return SqlHelper.ExecuteObject(new TWfSettingBelongsVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingBelongsVo> SelectByObject(TWfSettingBelongsVo tWfSettingBelongs, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_BELONGS " + this.BuildWhereStatement(tWfSettingBelongs));
            return SqlHelper.ExecuteObjectList(tWfSettingBelongs, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingBelongsVo tWfSettingBelongs, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_BELONGS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingBelongs));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingBelongs"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = "select * from T_WF_SETTING_BELONGS " + this.BuildWhereStatement(tWfSettingBelongs);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <returns></returns>
        public TWfSettingBelongsVo SelectByObject(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = "select * from T_WF_SETTING_BELONGS " + this.BuildWhereStatement(tWfSettingBelongs);
            return SqlHelper.ExecuteObject(new TWfSettingBelongsVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingBelongs, TWfSettingBelongsVo.T_WF_SETTING_BELONGS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingBelongs">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingBelongs, TWfSettingBelongsVo.T_WF_SETTING_BELONGS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingBelongs.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingBelongs_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingBelongs_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingBelongsVo tWfSettingBelongs_UpdateSet, TWfSettingBelongsVo tWfSettingBelongs_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingBelongs_UpdateSet, TWfSettingBelongsVo.T_WF_SETTING_BELONGS_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingBelongs_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_BELONGS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingBelongsVo tWfSettingBelongs)
        {
            string strSQL = "delete from T_WF_SETTING_BELONGS ";
	    strSQL += this.BuildWhereStatement(tWfSettingBelongs);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingBelongs"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingBelongsVo tWfSettingBelongs)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingBelongs)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingBelongs.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingBelongs.ID.ToString()));
				}	
				//分类编号
				if (!String.IsNullOrEmpty(tWfSettingBelongs.WF_CLASS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_ID = '{0}'", tWfSettingBelongs.WF_CLASS_ID.ToString()));
				}	
				//分类父编号
				if (!String.IsNullOrEmpty(tWfSettingBelongs.WF_CLASS_PID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_PID = '{0}'", tWfSettingBelongs.WF_CLASS_PID.ToString()));
				}	
				//分类名称
				if (!String.IsNullOrEmpty(tWfSettingBelongs.WF_CLASS_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_NAME = '{0}'", tWfSettingBelongs.WF_CLASS_NAME.ToString()));
				}	
				//分类备注
				if (!String.IsNullOrEmpty(tWfSettingBelongs.WF_CLASS_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_NOTE = '{0}'", tWfSettingBelongs.WF_CLASS_NOTE.ToString()));
				}	
				//分类等级
				if (!String.IsNullOrEmpty(tWfSettingBelongs.WF_CLASS_LEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_LEVEL = '{0}'", tWfSettingBelongs.WF_CLASS_LEVEL.ToString()));
				}	
				//简述
				if (!String.IsNullOrEmpty(tWfSettingBelongs.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tWfSettingBelongs.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
