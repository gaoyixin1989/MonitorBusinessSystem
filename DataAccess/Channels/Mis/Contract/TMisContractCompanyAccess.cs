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
    /// 功能：委托书企业信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractCompanyAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_COMPANY " + this.BuildWhereStatement(tMisContractCompany);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractCompanyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_COMPANY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractCompanyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractCompany">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractCompanyVo Details(TMisContractCompanyVo tMisContractCompany)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_COMPANY " + this.BuildWhereStatement(tMisContractCompany));
           return SqlHelper.ExecuteObject(new TMisContractCompanyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractCompanyVo> SelectByObject(TMisContractCompanyVo tMisContractCompany, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_COMPANY " + this.BuildWhereStatement(tMisContractCompany));
            return SqlHelper.ExecuteObjectList(tMisContractCompany, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractCompanyVo tMisContractCompany, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_COMPANY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractCompany));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractCompany"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = "select * from T_MIS_CONTRACT_COMPANY " + this.BuildWhereStatement(tMisContractCompany);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <returns></returns>
        public TMisContractCompanyVo SelectByObject(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = "select * from T_MIS_CONTRACT_COMPANY " + this.BuildWhereStatement(tMisContractCompany);
            return SqlHelper.ExecuteObject(new TMisContractCompanyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractCompany, TMisContractCompanyVo.T_MIS_CONTRACT_COMPANY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractCompany">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractCompany, TMisContractCompanyVo.T_MIS_CONTRACT_COMPANY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractCompany.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractCompany_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractCompany_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractCompanyVo tMisContractCompany_UpdateSet, TMisContractCompanyVo tMisContractCompany_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractCompany_UpdateSet, TMisContractCompanyVo.T_MIS_CONTRACT_COMPANY_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractCompany_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_COMPANY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = "delete from T_MIS_CONTRACT_COMPANY ";
	    strSQL += this.BuildWhereStatement(tMisContractCompany);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractCompany"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractCompanyVo tMisContractCompany)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractCompany)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisContractCompany.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractCompany.ID.ToString()));
				}	
				//合同ID
				if (!String.IsNullOrEmpty(tMisContractCompany.CONTRACT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractCompany.CONTRACT_ID.ToString()));
				}	
				//企业ID
				if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_ID = '{0}'", tMisContractCompany.COMPANY_ID.ToString()));
				}	
				//企业法人代码
				if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_CODE = '{0}'", tMisContractCompany.COMPANY_CODE.ToString()));
				}	
				//企业名称
				if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tMisContractCompany.COMPANY_NAME.ToString()));
				}	
				//拼音编码
				if (!String.IsNullOrEmpty(tMisContractCompany.PINYIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PINYIN = '{0}'", tMisContractCompany.PINYIN.ToString()));
				}	
				//主管部门
				if (!String.IsNullOrEmpty(tMisContractCompany.DIRECTOR_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DIRECTOR_DEPT = '{0}'", tMisContractCompany.DIRECTOR_DEPT.ToString()));
				}	
				//经济类型
				if (!String.IsNullOrEmpty(tMisContractCompany.ECONOMY_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ECONOMY_TYPE = '{0}'", tMisContractCompany.ECONOMY_TYPE.ToString()));
				}	
				//所在区域
				if (!String.IsNullOrEmpty(tMisContractCompany.AREA.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AREA = '{0}'", tMisContractCompany.AREA.ToString()));
				}	
				//企业规模
				if (!String.IsNullOrEmpty(tMisContractCompany.SIZE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SIZE = '{0}'", tMisContractCompany.SIZE.ToString()));
				}	
				//污染源类型
				if (!String.IsNullOrEmpty(tMisContractCompany.POLLUTE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POLLUTE_TYPE = '{0}'", tMisContractCompany.POLLUTE_TYPE.ToString()));
				}	
				//行业类别
				if (!String.IsNullOrEmpty(tMisContractCompany.INDUSTRY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY = '{0}'", tMisContractCompany.INDUSTRY.ToString()));
				}	
				//废气控制级别
				if (!String.IsNullOrEmpty(tMisContractCompany.GAS_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GAS_LEAVEL = '{0}'", tMisContractCompany.GAS_LEAVEL.ToString()));
				}	
				//废水控制级别
				if (!String.IsNullOrEmpty(tMisContractCompany.WATER_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WATER_LEAVEL = '{0}'", tMisContractCompany.WATER_LEAVEL.ToString()));
				}	
				//开业时间
				if (!String.IsNullOrEmpty(tMisContractCompany.PRACTICE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRACTICE_DATE = '{0}'", tMisContractCompany.PRACTICE_DATE.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tMisContractCompany.CONTACT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_NAME = '{0}'", tMisContractCompany.CONTACT_NAME.ToString()));
				}	
				//联系部门
				if (!String.IsNullOrEmpty(tMisContractCompany.LINK_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_DEPT = '{0}'", tMisContractCompany.LINK_DEPT.ToString()));
				}	
				//电子邮件
				if (!String.IsNullOrEmpty(tMisContractCompany.EMAIL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMAIL = '{0}'", tMisContractCompany.EMAIL.ToString()));
				}	
				//联系电话
				if (!String.IsNullOrEmpty(tMisContractCompany.LINK_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_PHONE = '{0}'", tMisContractCompany.LINK_PHONE.ToString()));
				}	
				//委托代理人
				if (!String.IsNullOrEmpty(tMisContractCompany.FACTOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FACTOR = '{0}'", tMisContractCompany.FACTOR.ToString()));
				}	
				//办公电话
				if (!String.IsNullOrEmpty(tMisContractCompany.PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE = '{0}'", tMisContractCompany.PHONE.ToString()));
				}	
				//移动电话
				if (!String.IsNullOrEmpty(tMisContractCompany.MOBAIL_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MOBAIL_PHONE = '{0}'", tMisContractCompany.MOBAIL_PHONE.ToString()));
				}	
				//传真号码
				if (!String.IsNullOrEmpty(tMisContractCompany.FAX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FAX = '{0}'", tMisContractCompany.FAX.ToString()));
				}	
				//邮政编码
				if (!String.IsNullOrEmpty(tMisContractCompany.POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST = '{0}'", tMisContractCompany.POST.ToString()));
				}	
				//联系地址
				if (!String.IsNullOrEmpty(tMisContractCompany.CONTACT_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_ADDRESS = '{0}'", tMisContractCompany.CONTACT_ADDRESS.ToString()));
				}	
				//监测地址
				if (!String.IsNullOrEmpty(tMisContractCompany.MONITOR_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ADDRESS = '{0}'", tMisContractCompany.MONITOR_ADDRESS.ToString()));
				}	
				//企业网址
				if (!String.IsNullOrEmpty(tMisContractCompany.WEB_SITE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WEB_SITE = '{0}'", tMisContractCompany.WEB_SITE.ToString()));
				}	
				//经度
				if (!String.IsNullOrEmpty(tMisContractCompany.LONGITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tMisContractCompany.LONGITUDE.ToString()));
				}	
				//纬度
				if (!String.IsNullOrEmpty(tMisContractCompany.LATITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tMisContractCompany.LATITUDE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tMisContractCompany.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisContractCompany.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractCompany.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractCompany.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractCompany.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractCompany.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractCompany.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractCompany.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractCompany.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractCompany.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractCompany.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractCompany.REMARK5.ToString()));
				}
                //企业级别
                if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_LEVEL = '{0}'", tMisContractCompany.COMPANY_LEVEL.ToString()));
                }
                //法人代表
                if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_MAN = '{0}'", tMisContractCompany.COMPANY_MAN.ToString()));
                }
                //废水最终排放去向
                if (!String.IsNullOrEmpty(tMisContractCompany.WATER_FOLLOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_FOLLOW = '{0}'", tMisContractCompany.WATER_FOLLOW.ToString()));
                }
                //现有工程环评批复时间及文号
                if (!String.IsNullOrEmpty(tMisContractCompany.CHECK_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_TIME = '{0}'", tMisContractCompany.CHECK_TIME.ToString()));
                }
                //现有工程竣工环境保护验收时间
                if (!String.IsNullOrEmpty(tMisContractCompany.ACCEPTANCE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACCEPTANCE_TIME = '{0}'", tMisContractCompany.ACCEPTANCE_TIME.ToString()));
                }
                //执行标准
                if (!String.IsNullOrEmpty(tMisContractCompany.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tMisContractCompany.STANDARD.ToString()));
                }
                //主要环保设施名称、数量
                if (!String.IsNullOrEmpty(tMisContractCompany.MAIN_APPARATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_APPARATUS = '{0}'", tMisContractCompany.MAIN_APPARATUS.ToString()));
                }
                //环保设施运行情况
                if (!String.IsNullOrEmpty(tMisContractCompany.APPARATUS_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_STATUS = '{0}'", tMisContractCompany.APPARATUS_STATUS.ToString()));
                }
                //主要产品名称
                if (!String.IsNullOrEmpty(tMisContractCompany.MAIN_PROJECT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_PROJECT = '{0}'", tMisContractCompany.MAIN_PROJECT.ToString()));
                }
                //主要生产原料
                if (!String.IsNullOrEmpty(tMisContractCompany.MAIN_GOOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_GOOD = '{0}'", tMisContractCompany.MAIN_GOOD.ToString()));
                }
                //设计生产能力
                if (!String.IsNullOrEmpty(tMisContractCompany.DESIGN_ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESIGN_ANBILITY = '{0}'", tMisContractCompany.DESIGN_ANBILITY.ToString()));
                }
                //实际生产能力
                if (!String.IsNullOrEmpty(tMisContractCompany.ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANBILITY = '{0}'", tMisContractCompany.ANBILITY.ToString()));
                }
                //监测期间生产负荷（%）
                if (!String.IsNullOrEmpty(tMisContractCompany.CONTRACT_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_PER = '{0}'", tMisContractCompany.CONTRACT_PER.ToString()));
                }
                //全年平均生产负荷（%）
                if (!String.IsNullOrEmpty(tMisContractCompany.AVG_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AVG_PER = '{0}'", tMisContractCompany.AVG_PER.ToString()));
                }
                //废水排放量
                if (!String.IsNullOrEmpty(tMisContractCompany.WATER_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_COUNT = '{0}'", tMisContractCompany.WATER_COUNT.ToString()));
                }
                //年运行时间
                if (!String.IsNullOrEmpty(tMisContractCompany.YEAR_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR_TIME = '{0}'", tMisContractCompany.YEAR_TIME.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
