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
    /// 功能：维护工程师管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysEngineerAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysEngineerVo tSysEngineer)
        {
            string strSQL = "select Count(*) from T_SYS_ENGINEER " + this.BuildWhereStatement(tSysEngineer);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysEngineerVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_ENGINEER  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysEngineerVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysEngineer">对象条件</param>
        /// <returns>对象</returns>
        public TSysEngineerVo Details(TSysEngineerVo tSysEngineer)
        {
           string strSQL = String.Format("select * from  T_SYS_ENGINEER " + this.BuildWhereStatement(tSysEngineer));
           return SqlHelper.ExecuteObject(new TSysEngineerVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysEngineerVo> SelectByObject(TSysEngineerVo tSysEngineer, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_ENGINEER t " + this.BuildWhereStatement(tSysEngineer));
            return SqlHelper.ExecuteObjectList(tSysEngineer, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysEngineerVo tSysEngineer, int iIndex, int iCount)
        {

            string strSQL = "  select t.*,rownum rowno from (select * from T_SYS_ENGINEER order by id desc ) t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysEngineer));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysEngineer"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysEngineerVo tSysEngineer)
        {
            string strSQL = "select * from T_SYS_ENGINEER " + this.BuildWhereStatement(tSysEngineer);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <returns></returns>
        public TSysEngineerVo SelectByObject(TSysEngineerVo tSysEngineer)
        {
            string strSQL = "select * from T_SYS_ENGINEER " + this.BuildWhereStatement(tSysEngineer);
            return SqlHelper.ExecuteObject(new TSysEngineerVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysEngineerVo tSysEngineer)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysEngineer, TSysEngineerVo.T_SYS_ENGINEER_TABLE, TSysEngineerVo.CREATE_TIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysEngineer">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysEngineerVo tSysEngineer)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysEngineer, TSysEngineerVo.T_SYS_ENGINEER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysEngineer.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_ENGINEER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysEngineer"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysEngineerVo tSysEngineer)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysEngineer)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysEngineer.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysEngineer.ID.ToString()));
				}	
				//工程师编码
				if (!String.IsNullOrEmpty(tSysEngineer.ENGINEER_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENGINEER_CODE = '{0}'", tSysEngineer.ENGINEER_CODE.ToString()));
				}	
				//真实姓名
				if (!String.IsNullOrEmpty(tSysEngineer.ENGINEERL_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENGINEERL_NAME like '%{0}%'", tSysEngineer.ENGINEERL_NAME.ToString()));
				}	
				//手机号码
				if (!String.IsNullOrEmpty(tSysEngineer.PHONE_MOBILE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE_MOBILE = '{0}'", tSysEngineer.PHONE_MOBILE.ToString()));
				}	
				//办公电话
				if (!String.IsNullOrEmpty(tSysEngineer.PHONE_OFFICE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE_OFFICE = '{0}'", tSysEngineer.PHONE_OFFICE.ToString()));
				}	
				//家庭电话
				if (!String.IsNullOrEmpty(tSysEngineer.PHONE_HOME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE_HOME = '{0}'", tSysEngineer.PHONE_HOME.ToString()));
				}	
				//业务代码
				if (!String.IsNullOrEmpty(tSysEngineer.BUSINESS_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BUSINESS_CODE = '{0}'", tSysEngineer.BUSINESS_CODE.ToString()));
				}	
				//单位编码
				if (!String.IsNullOrEmpty(tSysEngineer.UNITS_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNITS_CODE = '{0}'", tSysEngineer.UNITS_CODE.ToString()));
				}	
				//地区编码
				if (!String.IsNullOrEmpty(tSysEngineer.REGION_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REGION_CODE = '{0}'", tSysEngineer.REGION_CODE.ToString()));
				}	
				//职务编码
				if (!String.IsNullOrEmpty(tSysEngineer.DUTY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DUTY_CODE = '{0}'", tSysEngineer.DUTY_CODE.ToString()));
				}	
				//启用标记,1启用,0停用
				if (!String.IsNullOrEmpty(tSysEngineer.IS_USE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_USE = '{0}'", tSysEngineer.IS_USE.ToString()));
				}	
				//删除标记,1为删除,
				if (!String.IsNullOrEmpty(tSysEngineer.IS_DEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysEngineer.IS_DEL.ToString()));
				}	
				//创建人ID
				if (!String.IsNullOrEmpty(tSysEngineer.CREATE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysEngineer.CREATE_ID.ToString()));
				}	
				//创建时间
				if (!String.IsNullOrEmpty(tSysEngineer.CREATE_TIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysEngineer.CREATE_TIME.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysEngineer.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysEngineer.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysEngineer.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysEngineer.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysEngineer.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysEngineer.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysEngineer.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysEngineer.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
