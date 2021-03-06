using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务企业信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskCompanyAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK_COMPANY " + this.BuildWhereStatement(tMisMonitorTaskCompany);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskCompanyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_COMPANY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskCompanyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskCompanyVo Details(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_COMPANY " + this.BuildWhereStatement(tMisMonitorTaskCompany));
           return SqlHelper.ExecuteObject(new TMisMonitorTaskCompanyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskCompanyVo> SelectByObject(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_COMPANY " + this.BuildWhereStatement(tMisMonitorTaskCompany));
            return SqlHelper.ExecuteObjectList(tMisMonitorTaskCompany, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_TASK_COMPANY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTaskCompany));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskCompany"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_COMPANY " + this.BuildWhereStatement(tMisMonitorTaskCompany);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskCompanyVo SelectByObject(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_COMPANY " + this.BuildWhereStatement(tMisMonitorTaskCompany);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskCompanyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTaskCompany, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskCompany, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTaskCompany.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskCompany_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorTaskCompany_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany_UpdateSet, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskCompany_UpdateSet, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTaskCompany_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK_COMPANY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK_COMPANY ";
	    strSQL += this.BuildWhereStatement(tMisMonitorTaskCompany);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTaskCompany"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTaskCompany)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTaskCompany.ID.ToString()));
				}	
				//任务ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorTaskCompany.TASK_ID.ToString()));
				}
                //委托书企业ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_ID = '{0}'", tMisMonitorTaskCompany.COMPANY_ID.ToString()));
				}	
				//企业法人代码
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_CODE = '{0}'", tMisMonitorTaskCompany.COMPANY_CODE.ToString()));
				}	
				//企业名称
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tMisMonitorTaskCompany.COMPANY_NAME.ToString()));
				}	
				//拼音编码
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.PINYIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PINYIN = '{0}'", tMisMonitorTaskCompany.PINYIN.ToString()));
				}	
				//主管部门
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.DIRECTOR_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DIRECTOR_DEPT = '{0}'", tMisMonitorTaskCompany.DIRECTOR_DEPT.ToString()));
				}	
				//经济类型
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.ECONOMY_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ECONOMY_TYPE = '{0}'", tMisMonitorTaskCompany.ECONOMY_TYPE.ToString()));
				}	
				//所在区域
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.AREA.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AREA = '{0}'", tMisMonitorTaskCompany.AREA.ToString()));
				}	
				//企业规模
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.SIZE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SIZE = '{0}'", tMisMonitorTaskCompany.SIZE.ToString()));
				}	
				//污染源类型
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.POLLUTE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POLLUTE_TYPE = '{0}'", tMisMonitorTaskCompany.POLLUTE_TYPE.ToString()));
				}	
				//行业类别
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.INDUSTRY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY = '{0}'", tMisMonitorTaskCompany.INDUSTRY.ToString()));
				}	
				//废气控制级别
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.GAS_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GAS_LEAVEL = '{0}'", tMisMonitorTaskCompany.GAS_LEAVEL.ToString()));
				}	
				//废水控制级别
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.WATER_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WATER_LEAVEL = '{0}'", tMisMonitorTaskCompany.WATER_LEAVEL.ToString()));
				}	
				//开业时间
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.PRACTICE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRACTICE_DATE = '{0}'", tMisMonitorTaskCompany.PRACTICE_DATE.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.CONTACT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_NAME = '{0}'", tMisMonitorTaskCompany.CONTACT_NAME.ToString()));
				}	
				//联系部门
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.LINK_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_DEPT = '{0}'", tMisMonitorTaskCompany.LINK_DEPT.ToString()));
				}	
				//电子邮件
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.EMAIL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMAIL = '{0}'", tMisMonitorTaskCompany.EMAIL.ToString()));
				}	
				//联系电话
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.LINK_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_PHONE = '{0}'", tMisMonitorTaskCompany.LINK_PHONE.ToString()));
				}	
				//委托代理人
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.FACTOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FACTOR = '{0}'", tMisMonitorTaskCompany.FACTOR.ToString()));
				}	
				//办公电话
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE = '{0}'", tMisMonitorTaskCompany.PHONE.ToString()));
				}	
				//移动电话
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.MOBAIL_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MOBAIL_PHONE = '{0}'", tMisMonitorTaskCompany.MOBAIL_PHONE.ToString()));
				}	
				//传真号码
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.FAX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FAX = '{0}'", tMisMonitorTaskCompany.FAX.ToString()));
				}	
				//邮政编码
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST = '{0}'", tMisMonitorTaskCompany.POST.ToString()));
				}	
				//联系地址
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.CONTACT_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_ADDRESS = '{0}'", tMisMonitorTaskCompany.CONTACT_ADDRESS.ToString()));
				}	
				//监测地址
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.MONITOR_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ADDRESS = '{0}'", tMisMonitorTaskCompany.MONITOR_ADDRESS.ToString()));
				}	
				//企业网址
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.WEB_SITE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WEB_SITE = '{0}'", tMisMonitorTaskCompany.WEB_SITE.ToString()));
				}	
				//经度
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.LONGITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tMisMonitorTaskCompany.LONGITUDE.ToString()));
				}	
				//纬度
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.LATITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tMisMonitorTaskCompany.LATITUDE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisMonitorTaskCompany.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorTaskCompany.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorTaskCompany.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorTaskCompany.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorTaskCompany.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorTaskCompany.REMARK5.ToString()));
				}
                //企业级别
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_LEVEL = '{0}'", tMisMonitorTaskCompany.COMPANY_LEVEL.ToString()));
                }
                //法人代表
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_MAN = '{0}'", tMisMonitorTaskCompany.COMPANY_MAN.ToString()));
                }
                //废水最终排放去向
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.WATER_FOLLOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_FOLLOW = '{0}'", tMisMonitorTaskCompany.WATER_FOLLOW.ToString()));
                }
                //现有工程环评批复时间及文号
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.CHECK_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_TIME = '{0}'", tMisMonitorTaskCompany.CHECK_TIME.ToString()));
                }
                //现有工程竣工环境保护验收时间
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.ACCEPTANCE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACCEPTANCE_TIME = '{0}'", tMisMonitorTaskCompany.ACCEPTANCE_TIME.ToString()));
                }
                //执行标准
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tMisMonitorTaskCompany.STANDARD.ToString()));
                }
                //主要环保设施名称、数量
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.MAIN_APPARATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_APPARATUS = '{0}'", tMisMonitorTaskCompany.MAIN_APPARATUS.ToString()));
                }
                //环保设施运行情况
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.APPARATUS_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_STATUS = '{0}'", tMisMonitorTaskCompany.APPARATUS_STATUS.ToString()));
                }
                //主要产品名称
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.MAIN_PROJECT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_PROJECT = '{0}'", tMisMonitorTaskCompany.MAIN_PROJECT.ToString()));
                }
                //主要生产原料
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.MAIN_GOOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_GOOD = '{0}'", tMisMonitorTaskCompany.MAIN_GOOD.ToString()));
                }
                //设计生产能力
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.DESIGN_ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESIGN_ANBILITY = '{0}'", tMisMonitorTaskCompany.DESIGN_ANBILITY.ToString()));
                }
                //实际生产能力
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANBILITY = '{0}'", tMisMonitorTaskCompany.ANBILITY.ToString()));
                }
                //监测期间生产负荷（%）
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.CONTRACT_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_PER = '{0}'", tMisMonitorTaskCompany.CONTRACT_PER.ToString()));
                }
                //全年平均生产负荷（%）
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.AVG_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AVG_PER = '{0}'", tMisMonitorTaskCompany.AVG_PER.ToString()));
                }
                //废水排放量
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.WATER_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_COUNT = '{0}'", tMisMonitorTaskCompany.WATER_COUNT.ToString()));
                }
                //年运行时间
                if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.YEAR_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR_TIME = '{0}'", tMisMonitorTaskCompany.YEAR_TIME.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
