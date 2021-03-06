using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Company;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Company
{
    /// <summary>
    /// 功能：企业信息管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = "select Count(*) from T_BASE_COMPANY_INFO " + this.BuildWhereStatement(tBaseCompanyInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_COMPANY_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseCompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyInfoVo Details(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_COMPANY_INFO " + this.BuildWhereStatement(tBaseCompanyInfo));
           return SqlHelper.ExecuteObject(new TBaseCompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyInfoVo> SelectByObject(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_COMPANY_INFO " + this.BuildWhereStatement(tBaseCompanyInfo));
            return SqlHelper.ExecuteObjectList(tBaseCompanyInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_COMPANY_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseCompanyInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = "select * from T_BASE_COMPANY_INFO " + this.BuildWhereStatement(tBaseCompanyInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns></returns>
        public TBaseCompanyInfoVo SelectByObject(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = "select * from T_BASE_COMPANY_INFO " + this.BuildWhereStatement(tBaseCompanyInfo);
            return SqlHelper.ExecuteObject(new TBaseCompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseCompanyInfo, TBaseCompanyInfoVo.T_BASE_COMPANY_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyInfo, TBaseCompanyInfoVo.T_BASE_COMPANY_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseCompanyInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseCompanyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyInfoVo tBaseCompanyInfo_UpdateSet, TBaseCompanyInfoVo tBaseCompanyInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyInfo_UpdateSet, TBaseCompanyInfoVo.T_BASE_COMPANY_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseCompanyInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_COMPANY_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            string strSQL = "delete from T_BASE_COMPANY_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseCompanyInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_COMPANY_INFO WHERE IS_DEL='{0}'", tBaseCompanyInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_NAME))
            {
                strSQL += String.Format("  AND COMPANY_NAME LIKE '%{0}%'", tBaseCompanyInfo.COMPANY_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseCompanyInfo.AREA))
            {
                strSQL += String.Format("  AND AREA = '{0}'", tBaseCompanyInfo.AREA);
            }

            if (!String.IsNullOrEmpty(tBaseCompanyInfo.INDUSTRY))
            {
                strSQL += String.Format("   AND INDUSTRY = '{0}'", tBaseCompanyInfo.INDUSTRY);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseCompanyInfoVo tBaseCompanyInfo)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_COMPANY_INFO WHERE IS_DEL='{0}'", tBaseCompanyInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_NAME))
            {
                strSQL += String.Format("  AND COMPANY_NAME LIKE '%{0}%'", tBaseCompanyInfo.COMPANY_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseCompanyInfo.AREA))
            {
                strSQL += String.Format("  AND AREA = '{0}'", tBaseCompanyInfo.AREA);
            }

            if (!String.IsNullOrEmpty(tBaseCompanyInfo.INDUSTRY))
            {
                strSQL += String.Format("   AND INDUSTRY = '{0}'", tBaseCompanyInfo.INDUSTRY);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseCompanyInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseCompanyInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseCompanyInfo.ID.ToString()));
				}	
				//企业法人代码
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_CODE = '{0}'", tBaseCompanyInfo.COMPANY_CODE.ToString()));
				}	
				//企业名称
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tBaseCompanyInfo.COMPANY_NAME.ToString()));
				}	
				//拼音编码
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.PINYIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PINYIN = '{0}'", tBaseCompanyInfo.PINYIN.ToString()));
				}	
				//主管部门
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.DIRECTOR_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DIRECTOR_DEPT = '{0}'", tBaseCompanyInfo.DIRECTOR_DEPT.ToString()));
				}	
				//经济类型
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.ECONOMY_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ECONOMY_TYPE = '{0}'", tBaseCompanyInfo.ECONOMY_TYPE.ToString()));
				}	
				//所在区域
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.AREA.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AREA = '{0}'", tBaseCompanyInfo.AREA.ToString()));
				}	
				//企业规模
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.SIZE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SIZE = '{0}'", tBaseCompanyInfo.SIZE.ToString()));
				}	
				//污染源类型
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.POLLUTE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POLLUTE_TYPE = '{0}'", tBaseCompanyInfo.POLLUTE_TYPE.ToString()));
				}	
				//行业类别
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.INDUSTRY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY = '{0}'", tBaseCompanyInfo.INDUSTRY.ToString()));
				}	
				//废气控制级别
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.GAS_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GAS_LEAVEL = '{0}'", tBaseCompanyInfo.GAS_LEAVEL.ToString()));
				}	
				//废水控制级别
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.WATER_LEAVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WATER_LEAVEL = '{0}'", tBaseCompanyInfo.WATER_LEAVEL.ToString()));
				}	
				//开业时间
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.PRACTICE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRACTICE_DATE = '{0}'", tBaseCompanyInfo.PRACTICE_DATE.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.CONTACT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_NAME = '{0}'", tBaseCompanyInfo.CONTACT_NAME.ToString()));
				}	
				//联系部门
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.LINK_DEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_DEPT = '{0}'", tBaseCompanyInfo.LINK_DEPT.ToString()));
				}	
				//电子邮件
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.EMAIL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMAIL = '{0}'", tBaseCompanyInfo.EMAIL.ToString()));
				}	
				//联系电话
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.LINK_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_PHONE = '{0}'", tBaseCompanyInfo.LINK_PHONE.ToString()));
				}	
				//委托代理人
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.FACTOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FACTOR = '{0}'", tBaseCompanyInfo.FACTOR.ToString()));
				}	
				//办公电话
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE = '{0}'", tBaseCompanyInfo.PHONE.ToString()));
				}	
				//移动电话
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.MOBAIL_PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MOBAIL_PHONE = '{0}'", tBaseCompanyInfo.MOBAIL_PHONE.ToString()));
				}	
				//传真号码
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.FAX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FAX = '{0}'", tBaseCompanyInfo.FAX.ToString()));
				}	
				//邮政编码
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST = '{0}'", tBaseCompanyInfo.POST.ToString()));
				}	
				//联系地址
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.CONTACT_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTACT_ADDRESS = '{0}'", tBaseCompanyInfo.CONTACT_ADDRESS.ToString()));
				}	
				//监测地址
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.MONITOR_ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ADDRESS = '{0}'", tBaseCompanyInfo.MONITOR_ADDRESS.ToString()));
				}	
				//企业网址
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.WEB_SITE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WEB_SITE = '{0}'", tBaseCompanyInfo.WEB_SITE.ToString()));
				}	
				//经度
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.LONGITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tBaseCompanyInfo.LONGITUDE.ToString()));
				}	
				//纬度
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.LATITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tBaseCompanyInfo.LATITUDE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseCompanyInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseCompanyInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseCompanyInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseCompanyInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseCompanyInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseCompanyInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseCompanyInfo.REMARK5.ToString()));
				}
                //企业级别
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_LEVEL = '{0}'", tBaseCompanyInfo.COMPANY_LEVEL.ToString()));
                }
                //法人代表
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.COMPANY_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_MAN = '{0}'", tBaseCompanyInfo.COMPANY_MAN.ToString()));
                }
                //废水最终排放去向
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.WATER_FOLLOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_FOLLOW = '{0}'", tBaseCompanyInfo.WATER_FOLLOW.ToString()));
                }
                //现有工程环评批复时间及文号
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.CHECK_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_TIME = '{0}'", tBaseCompanyInfo.CHECK_TIME.ToString()));
                }
                //现有工程竣工环境保护验收时间
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.ACCEPTANCE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACCEPTANCE_TIME = '{0}'", tBaseCompanyInfo.ACCEPTANCE_TIME.ToString()));
                }
                //执行标准
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tBaseCompanyInfo.STANDARD.ToString()));
                }
                //主要环保设施名称、数量
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.MAIN_APPARATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_APPARATUS = '{0}'", tBaseCompanyInfo.MAIN_APPARATUS.ToString()));
                }
                //环保设施运行情况
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.APPARATUS_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_STATUS = '{0}'", tBaseCompanyInfo.APPARATUS_STATUS.ToString()));
                }
                //主要产品名称
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.MAIN_PROJECT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_PROJECT = '{0}'", tBaseCompanyInfo.MAIN_PROJECT.ToString()));
                }
                //主要生产原料
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.MAIN_GOOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_GOOD = '{0}'", tBaseCompanyInfo.MAIN_GOOD.ToString()));
                }
                //设计生产能力
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.DESIGN_ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESIGN_ANBILITY = '{0}'", tBaseCompanyInfo.DESIGN_ANBILITY.ToString()));
                }
                //实际生产能力
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.ANBILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANBILITY = '{0}'", tBaseCompanyInfo.ANBILITY.ToString()));
                }
                //监测期间生产负荷（%）
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.CONTRACT_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_PER = '{0}'", tBaseCompanyInfo.CONTRACT_PER.ToString()));
                }
                //全年平均生产负荷（%）
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.AVG_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AVG_PER = '{0}'", tBaseCompanyInfo.AVG_PER.ToString()));
                }
                //废水排放量
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.WATER_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_COUNT = '{0}'", tBaseCompanyInfo.WATER_COUNT.ToString()));
                }
                //年运行时间
                if (!String.IsNullOrEmpty(tBaseCompanyInfo.YEAR_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR_TIME = '{0}'", tBaseCompanyInfo.YEAR_TIME.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
