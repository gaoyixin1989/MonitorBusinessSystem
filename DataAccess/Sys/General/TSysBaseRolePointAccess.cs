using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.General;
using i3.ValueObject;

namespace i3.DataAccess.Sys.General
{
    /// <summary>
    /// 功能：角色监测点权限
    /// 创建日期：2011-04-13
    /// 创建人：郑义
    /// </summary>
    public class TSysBaseRolePointAccess : SqlHelper 
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            string strSQL = "select Count(*) from T_SYS_BASE_ROLE_POINT " + this.BuildWhereStatement(tSysBaseRolePoint);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysBaseRolePointVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_BASE_ROLE_POINT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysBaseRolePointVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象条件</param>
        /// <returns>对象</returns>
        public TSysBaseRolePointVo Details(TSysBaseRolePointVo tSysBaseRolePoint)
        {
           string strSQL = String.Format("select * from  T_SYS_BASE_ROLE_POINT " + this.BuildWhereStatement(tSysBaseRolePoint));
           return SqlHelper.ExecuteObject(new TSysBaseRolePointVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysBaseRolePointVo> SelectByObject(TSysBaseRolePointVo tSysBaseRolePoint, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_BASE_ROLE_POINT t " + this.BuildWhereStatement(tSysBaseRolePoint));
            return SqlHelper.ExecuteObjectList(tSysBaseRolePoint, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysBaseRolePointVo tSysBaseRolePoint, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_BASE_ROLE_POINT t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysBaseRolePoint));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysBaseRolePoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            string strSQL = "select * from T_SYS_BASE_ROLE_POINT " + this.BuildWhereStatement(tSysBaseRolePoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <returns></returns>
        public TSysBaseRolePointVo SelectByObject(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            string strSQL = "select * from T_SYS_BASE_ROLE_POINT " + this.BuildWhereStatement(tSysBaseRolePoint);
            return SqlHelper.ExecuteObject(new TSysBaseRolePointVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysBaseRolePoint, TSysBaseRolePointVo.T_SYS_BASE_ROLE_POINT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysBaseRolePoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysBaseRolePoint, TSysBaseRolePointVo.T_SYS_BASE_ROLE_POINT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysBaseRolePoint.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_BASE_ROLE_POINT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteByRoleID(string strRoleID)
        {
            string strWhere = String.Format("delete from T_SYS_BASE_ROLE_POINT where Role_id='{0}'", strRoleID);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysBaseRolePoint"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysBaseRolePoint)
            {
			    	
				//备注3
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysBaseRolePoint.REMARK3.ToString()));
				}	
				//编号
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysBaseRolePoint.ID.ToString()));
				}	
				//角色编号
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.ROLE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_ID = '{0}'", tSysBaseRolePoint.ROLE_ID.ToString()));
				}	
				//监测点编号
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.POINT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tSysBaseRolePoint.POINT_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysBaseRolePoint.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysBaseRolePoint.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysBaseRolePoint.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysBaseRolePoint.REMARK2.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
