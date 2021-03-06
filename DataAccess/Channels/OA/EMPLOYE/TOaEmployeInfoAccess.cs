using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工基本信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_INFO " + this.BuildWhereStatement(tOaEmployeInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeInfoVo Details(TOaEmployeInfoVo tOaEmployeInfo)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_INFO " + this.BuildWhereStatement(tOaEmployeInfo));
           return SqlHelper.ExecuteObject(new TOaEmployeInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeInfoVo> SelectByObject(TOaEmployeInfoVo tOaEmployeInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_INFO " + this.BuildWhereStatement(tOaEmployeInfo));
            return SqlHelper.ExecuteObjectList(tOaEmployeInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeInfoVo tOaEmployeInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = "select * from T_OA_EMPLOYE_INFO " + this.BuildWhereStatement(tOaEmployeInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns></returns>
        public TOaEmployeInfoVo SelectByObject(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = "select * from T_OA_EMPLOYE_INFO " + this.BuildWhereStatement(tOaEmployeInfo);
            return SqlHelper.ExecuteObject(new TOaEmployeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeInfo, TOaEmployeInfoVo.T_OA_EMPLOYE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeInfo, TOaEmployeInfoVo.T_OA_EMPLOYE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeInfoVo tOaEmployeInfo_UpdateSet, TOaEmployeInfoVo tOaEmployeInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeInfo_UpdateSet, TOaEmployeInfoVo.T_OA_EMPLOYE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = "delete from T_OA_EMPLOYE_INFO ";
	    strSQL += this.BuildWhereStatement(tOaEmployeInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

                /// <summary>
        /// 对象是否存在验证
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns>是否存在</returns>
        public bool IsExist(TOaEmployeInfoVo tOaEmployeInfo)
        {
            DataTable dt=new DataTable();
            int Record = 0;
            if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_CODE))
            {
                string strSQL = String.Format(" SELECT * FROM T_OA_EMPLOYE_INFO WHERE EMPLOYE_CODE='{0}'", tOaEmployeInfo.EMPLOYE_CODE);
                dt = SqlHelper.ExecuteDataTable(strSQL);

                Record = dt.Rows.Count;
            }
            return Record >0 ? true : false;
        }

        /// <summary>
        /// 自定义模糊查询 Create By Castle (胡方扬)  2013-01-10
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectDefineByTable(TOaEmployeInfoVo tOaEmployeInfo,int iIndex,int iCount) {
            string strSQL = " select * from T_OA_EMPLOYE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatementLike(tOaEmployeInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 自定义模糊查询返回记录数 Create By Castle (胡方扬)  2013-01-10
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int SelectDefineBytResult(TOaEmployeInfoVo tOaEmployeInfo)
        {
            string strSQL = " select * from T_OA_EMPLOYE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatementLike(tOaEmployeInfo));
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeInfoVo tOaEmployeInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeInfo.ID.ToString()));
				}	
				//USER_ID
				if (!String.IsNullOrEmpty(tOaEmployeInfo.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tOaEmployeInfo.USER_ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYE_CODE = '{0}'", tOaEmployeInfo.EMPLOYE_CODE.ToString()));
				}	
				//员工姓名
				if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYE_NAME = '{0}'", tOaEmployeInfo.EMPLOYE_NAME.ToString()));
				}	
				//身份证号
				if (!String.IsNullOrEmpty(tOaEmployeInfo.ID_CARD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID_CARD = '{0}'", tOaEmployeInfo.ID_CARD.ToString()));
				}	
				//性别
				if (!String.IsNullOrEmpty(tOaEmployeInfo.SEX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEX = '{0}'", tOaEmployeInfo.SEX.ToString()));
				}	
				//出生日期
				if (!String.IsNullOrEmpty(tOaEmployeInfo.BIRTHDAY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BIRTHDAY = '{0}'", tOaEmployeInfo.BIRTHDAY.ToString()));
				}	
				//
				if (!String.IsNullOrEmpty(tOaEmployeInfo.AGE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND AGE = '{0}'", tOaEmployeInfo.AGE.ToString()));
				}	
				//民族
				if (!String.IsNullOrEmpty(tOaEmployeInfo.NATION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NATION = '{0}'", tOaEmployeInfo.NATION.ToString()));
				}	
				//政治面貌
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POLITICALSTATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POLITICALSTATUS = '{0}'", tOaEmployeInfo.POLITICALSTATUS.ToString()));
				}	
				//文化程度
				if (!String.IsNullOrEmpty(tOaEmployeInfo.EDUCATIONLEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EDUCATIONLEVEL = '{0}'", tOaEmployeInfo.EDUCATIONLEVEL.ToString()));
				}	
				//所在部门
				if (!String.IsNullOrEmpty(tOaEmployeInfo.DEPART.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPART = '{0}'", tOaEmployeInfo.DEPART.ToString()));
				}	
				//
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST = '{0}'", tOaEmployeInfo.POST.ToString()));
				}	
				//岗位
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POSITION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tOaEmployeInfo.POSITION.ToString()));
				}	
				//级别
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_LEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_LEVEL = '{0}'", tOaEmployeInfo.POST_LEVEL.ToString()));
				}	
				//人员分类
				if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYE_TYPE = '{0}'", tOaEmployeInfo.EMPLOYE_TYPE.ToString()));
				}	
				//现任职时间
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_DATE = '{0}'", tOaEmployeInfo.POST_DATE.ToString()));
				}	
				//编制类别
				if (!String.IsNullOrEmpty(tOaEmployeInfo.ORGANIZATION_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ORGANIZATION_TYPE = '{0}'", tOaEmployeInfo.ORGANIZATION_TYPE.ToString()));
				}	
				//入编时间
				if (!String.IsNullOrEmpty(tOaEmployeInfo.ORGANIZATION_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ORGANIZATION_DATE = '{0}'", tOaEmployeInfo.ORGANIZATION_DATE.ToString()));
				}	
				//工作时间
				if (!String.IsNullOrEmpty(tOaEmployeInfo.ENTRYDATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENTRYDATE = '{0}'", tOaEmployeInfo.ENTRYDATE.ToString()));
				}	
				//聘任专业技术职务
				if (!String.IsNullOrEmpty(tOaEmployeInfo.TECHNOLOGY_POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TECHNOLOGY_POST = '{0}'", tOaEmployeInfo.TECHNOLOGY_POST.ToString()));
				}	
				//入本单位时间
				if (!String.IsNullOrEmpty(tOaEmployeInfo.APPLY_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPLY_DATE = '{0}'", tOaEmployeInfo.APPLY_DATE.ToString()));
				}	
				//毕业院校
				if (!String.IsNullOrEmpty(tOaEmployeInfo.GRADUATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GRADUATE = '{0}'", tOaEmployeInfo.GRADUATE.ToString()));
				}	
				//所学专业
				if (!String.IsNullOrEmpty(tOaEmployeInfo.SPECIALITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SPECIALITY = '{0}'", tOaEmployeInfo.SPECIALITY.ToString()));
				}	
				//毕业时间
				if (!String.IsNullOrEmpty(tOaEmployeInfo.GRADUATE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GRADUATE_DATE = '{0}'", tOaEmployeInfo.GRADUATE_DATE.ToString()));
				}	
				//专业技术等级
				if (!String.IsNullOrEmpty(tOaEmployeInfo.TECHNOLOGY_LEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TECHNOLOGY_LEVEL = '{0}'", tOaEmployeInfo.TECHNOLOGY_LEVEL.ToString()));
				}	
				//工勤技能等级
				if (!String.IsNullOrEmpty(tOaEmployeInfo.SKILL_LEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SKILL_LEVEL = '{0}'", tOaEmployeInfo.SKILL_LEVEL.ToString()));
				}	
				//工作状态,1在职、2离职、3退休
				if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_STATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_STATUS = '{0}'", tOaEmployeInfo.POST_STATUS.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tOaEmployeInfo.INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INFO = '{0}'", tOaEmployeInfo.INFO.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }


        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatementLike(TOaEmployeInfoVo tOaEmployeInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeInfo)
            {

                //编号
                if (!String.IsNullOrEmpty(tOaEmployeInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeInfo.ID.ToString()));
                }
                //USER_ID
                if (!String.IsNullOrEmpty(tOaEmployeInfo.USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tOaEmployeInfo.USER_ID.ToString()));
                }
                //员工编号
                if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPLOYE_CODE  LIKE '%{0}%'", tOaEmployeInfo.EMPLOYE_CODE.ToString()));
                }
                //员工姓名
                if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPLOYE_NAME LIKE '%{0}%'", tOaEmployeInfo.EMPLOYE_NAME.ToString()));
                }
                //身份证号
                if (!String.IsNullOrEmpty(tOaEmployeInfo.ID_CARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID_CARD = '{0}'", tOaEmployeInfo.ID_CARD.ToString()));
                }
                //性别
                if (!String.IsNullOrEmpty(tOaEmployeInfo.SEX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEX = '{0}'", tOaEmployeInfo.SEX.ToString()));
                }
                //出生日期
                if (!String.IsNullOrEmpty(tOaEmployeInfo.BIRTHDAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BIRTHDAY = '{0}'", tOaEmployeInfo.BIRTHDAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tOaEmployeInfo.AGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGE = '{0}'", tOaEmployeInfo.AGE.ToString()));
                }
                //民族
                if (!String.IsNullOrEmpty(tOaEmployeInfo.NATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NATION = '{0}'", tOaEmployeInfo.NATION.ToString()));
                }
                //政治面貌
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POLITICALSTATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POLITICALSTATUS = '{0}'", tOaEmployeInfo.POLITICALSTATUS.ToString()));
                }
                //文化程度
                if (!String.IsNullOrEmpty(tOaEmployeInfo.EDUCATIONLEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EDUCATIONLEVEL = '{0}'", tOaEmployeInfo.EDUCATIONLEVEL.ToString()));
                }
                //所在部门
                if (!String.IsNullOrEmpty(tOaEmployeInfo.DEPART.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPART = '{0}'", tOaEmployeInfo.DEPART.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POST.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST = '{0}'", tOaEmployeInfo.POST.ToString()));
                }
                //岗位
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POSITION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tOaEmployeInfo.POSITION.ToString()));
                }
                //级别
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST_LEVEL = '{0}'", tOaEmployeInfo.POST_LEVEL.ToString()));
                }
                //人员分类
                if (!String.IsNullOrEmpty(tOaEmployeInfo.EMPLOYE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPLOYE_TYPE = '{0}'", tOaEmployeInfo.EMPLOYE_TYPE.ToString()));
                }
                //现任职时间
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST_DATE = '{0}'", tOaEmployeInfo.POST_DATE.ToString()));
                }
                //编制类别
                if (!String.IsNullOrEmpty(tOaEmployeInfo.ORGANIZATION_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORGANIZATION_TYPE = '{0}'", tOaEmployeInfo.ORGANIZATION_TYPE.ToString()));
                }
                //入编时间
                if (!String.IsNullOrEmpty(tOaEmployeInfo.ORGANIZATION_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORGANIZATION_DATE = '{0}'", tOaEmployeInfo.ORGANIZATION_DATE.ToString()));
                }
                //工作时间
                if (!String.IsNullOrEmpty(tOaEmployeInfo.ENTRYDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTRYDATE = '{0}'", tOaEmployeInfo.ENTRYDATE.ToString()));
                }
                //聘任专业技术职务
                if (!String.IsNullOrEmpty(tOaEmployeInfo.TECHNOLOGY_POST.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TECHNOLOGY_POST = '{0}'", tOaEmployeInfo.TECHNOLOGY_POST.ToString()));
                }
                //入本单位时间
                if (!String.IsNullOrEmpty(tOaEmployeInfo.APPLY_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPLY_DATE = '{0}'", tOaEmployeInfo.APPLY_DATE.ToString()));
                }
                //毕业院校
                if (!String.IsNullOrEmpty(tOaEmployeInfo.GRADUATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GRADUATE = '{0}'", tOaEmployeInfo.GRADUATE.ToString()));
                }
                //所学专业
                if (!String.IsNullOrEmpty(tOaEmployeInfo.SPECIALITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SPECIALITY = '{0}'", tOaEmployeInfo.SPECIALITY.ToString()));
                }
                //毕业时间
                if (!String.IsNullOrEmpty(tOaEmployeInfo.GRADUATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GRADUATE_DATE = '{0}'", tOaEmployeInfo.GRADUATE_DATE.ToString()));
                }
                //专业技术等级
                if (!String.IsNullOrEmpty(tOaEmployeInfo.TECHNOLOGY_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TECHNOLOGY_LEVEL = '{0}'", tOaEmployeInfo.TECHNOLOGY_LEVEL.ToString()));
                }
                //工勤技能等级
                if (!String.IsNullOrEmpty(tOaEmployeInfo.SKILL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SKILL_LEVEL = '{0}'", tOaEmployeInfo.SKILL_LEVEL.ToString()));
                }
                //工作状态,1在职、2离职、3退休
                if (!String.IsNullOrEmpty(tOaEmployeInfo.POST_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST_STATUS = '{0}'", tOaEmployeInfo.POST_STATUS.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tOaEmployeInfo.INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INFO = '{0}'", tOaEmployeInfo.INFO.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaEmployeInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhereArr"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectTestDataLst(string[] strWhereArr, int iIndex, int iCount)
        {
            //throw new NotImplementedException();

            string strSQL = @"select r.ID, s.EMPLOYEID, st.EMPLOYEID, t.EMPLOYEID, c.EMPLOYEID as ID     
                         from T_OA_EMPLOYE_INFO r
                                    left join T_OA_EMPLOYE_QUALIFICATION s on s.EMPLOYEID=r.ID
                                    left join T_OA_EMPLOYE_RESULTORFAULT st on st.EMPLOYEID=r.ID
                                    left join T_OA_EMPLOYE_TRAINHISTORY t on t.EMPLOYEID=r.ID
                                    left join T_OA_EMPLOYE_WORKHISTORY c on c.EMPLOYEID=r.ID
                         where 1=1";

            if (strWhereArr[0].Length > 0)
                strSQL += string.Format(" and r.EMPLOYE_CODE='{0}'", strWhereArr[0]);
            if (strWhereArr[1].Length > 0)
                strSQL += string.Format(" and r.EMPLOYE_NAME='{0}'", strWhereArr[1]);
            if (strWhereArr[2].Length > 0)
                strSQL += string.Format(" and r.DEPART='{0}'", strWhereArr[2]);
            if (strWhereArr[3].Length > 0)
                strSQL += string.Format(" and r.POSITION='{0}'", strWhereArr[3]);
            if (strWhereArr[4].Length > 0)
                strSQL += string.Format(" and r.POST_STATUS='{0}'", strWhereArr[4]);
            //strSQL += " order by t.id,st.id,s.id";

            strSQL = string.Format(strSQL, strWhereArr);
            strSQL = BuildPagerExpress(strSQL, iIndex, iCount);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
