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
    /// 功能：用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserVo tSysUser)
        {
            string strSQL = @"select Count(*) from T_SYS_USER t {0} and ID<>'000000001'";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUser));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
     
        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysUserVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUser">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserVo Details(TSysUserVo tSysUser)
        {
            string strSQL = String.Format("select * from  T_SYS_USER " + this.BuildWhereStatement(tSysUser));
            return SqlHelper.ExecuteObject(new TSysUserVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserVo> SelectByObject(TSysUserVo tSysUser, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.* from  T_SYS_USER t " + this.BuildWhereStatement(tSysUser));
            return SqlHelper.ExecuteObjectList(tSysUser, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            string strSQL = @" select * from T_SYS_USER {0} and ID<>'000000001' order by ORDER_ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUser));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable Create By :weilin 
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            string strSQL = @" select * from T_SYS_USER where ID in(" + tSysUser.ID + ") and IS_USE='1' and IS_DEL='0' order by ORDER_ID";
            //strSQL = String.Format(strSQL, BuildWhereStatement(tSysUser));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ByDept(TSysUserVo tSysUser)
        {
            string strSQL = @"select Count(*) from T_SYS_USER t {0} and ID<>'000000001' {1}";
            string strDept = "";
            if (tSysUser.REMARK4.Length > 0)
            {
                strDept = " and ID in (select USER_ID from T_SYS_USER_POST where POST_ID in (select ID from T_SYS_POST where POST_DEPT_ID in ('" + tSysUser.REMARK4 + "')))";
            }
            string strREMARK4 = tSysUser.REMARK4;
            tSysUser.REMARK4 = "";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUser), strDept);
            tSysUser.REMARK4 = strREMARK4;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByDept(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            string strSQL = @" select * from T_SYS_USER {0} and ID<>'000000001' {1} order by ORDER_ID";
            string strDept = "";
            if (tSysUser.REMARK4.Length > 0)
            {
                strDept = " and ID in (select USER_ID from T_SYS_USER_POST where POST_ID in (select ID from T_SYS_POST where POST_DEPT_ID in ('" + tSysUser.REMARK4 + "')))";
            }
            string strREMARK4 = tSysUser.REMARK4;
            tSysUser.REMARK4 = "";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUser), strDept);
            tSysUser.REMARK4 = strREMARK4;
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 获取指定职位的所有下属职位
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectSubPost(string strPostID)
        {
            string strSQL = @"exec SelectSubPost '" + strPostID + "'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定职位的所有下属人员
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectUnderling_OfPost(string strPostID)
        {
            string strSQL = @"exec SelectUnderling_OfPost '" + strPostID + "'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定用户的所有下属人员
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectUnderling_OfUser(string strUserID)
        {
            string strSQL = @"exec SelectUnderling_OfUser '" + strUserID + "'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable,查询指定部门的用户
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableUnderDept(string strDeptID, int iIndex, int iCount)
        {
            string strSQL = @" select * from 
                              T_SYS_USER where ID in (select USER_ID from T_SYS_USER_POST where POST_ID
                                in (select ID from T_SYS_POST where 1=1 ";
            //当部门ID为空时查询所有的  Modify By Castle（胡方扬） 2012-11-20                    
            if(!String.IsNullOrEmpty(strDeptID))
            {
                strSQL += String.Format(" AND POST_DEPT_ID='{0}'", strDeptID);
            }
            strSQL+=" )) and IS_DEL='0' and IS_HIDE='0' order by ORDER_ID";

            //strSQL = string.Format(strSQL);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页,查询指定部门的用户
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectByTableUnderDept(string strDeptID)
        {

            string strSQL = @" select * from 
                              T_SYS_USER where ID in (select USER_ID from T_SYS_USER_POST where POST_ID
                                in (select ID from T_SYS_POST where 1=1 ";
            //当部门ID为空时查询所有的  Modify By Castle（胡方扬） 2012-11-20                    
            if (!String.IsNullOrEmpty(strDeptID))
            {
                strSQL += String.Format("  AND POST_DEPT_ID='{0}'", strDeptID);
            }
            strSQL += " )) and IS_DEL='0' and IS_HIDE='0' order by ORDER_ID";

            strSQL = "select Count(*) from (" + strSQL + ")";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserVo tSysUser)
        {
            string strSQL = "select * from T_SYS_USER " + this.BuildWhereStatement(tSysUser);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns></returns>
        public TSysUserVo SelectByObject(TSysUserVo tSysUser)
        {
            string strSQL = "select * from T_SYS_USER " + this.BuildWhereStatement(tSysUser);
            return SqlHelper.ExecuteObject(new TSysUserVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserVo tSysUser)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUser, TSysUserVo.T_SYS_USER_TABLE, TSysUserVo.CREATE_TIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserVo tSysUser)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUser, TSysUserVo.T_SYS_USER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysUser.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditByName(TSysUserVo tSysUser)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUser, TSysUserVo.T_SYS_USER_TABLE);
            strSQL += string.Format(" where USER_NAME='{0}' ", tSysUser.USER_NAME);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_USER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }

        /// <summary>
        /// 获取符合条件的用户
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionByDefineTable(TSysUserVo tSysUser,int iIndex,int iCount)
        {
            string strSQL = @"SELECT A.ID,A.REAL_NAME,A.USER_NAME,C.DICT_TEXT FROM dbo.T_SYS_USER A
LEFT JOIN dbo.T_SYS_USER_POST B ON B.USER_ID=A.ID
LEFT JOIN dbo.T_SYS_POST D ON D.ID=B.POST_ID
LEFT JOIN dbo.T_SYS_DICT C ON C.DICT_CODE=D.POST_DEPT_ID WHERE 1=1 AND A.IS_DEL='0' AND A.IS_HIDE='0' AND IS_USE='1'";
            if (!String.IsNullOrEmpty(tSysUser.REMARK5)) {
                strSQL += string.Format(" AND C.DICT_CODE='{0}'", tSysUser.REMARK5);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取符合条件的用户记录
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectUnionByDefineTableCount(TSysUserVo tSysUser)
        {
            string strSQL = @"SELECT A.ID,A.REAL_NAME,A.USER_NAME,C.DICT_TEXT FROM dbo.T_SYS_USER A
LEFT JOIN dbo.T_SYS_USER_POST B ON B.USER_ID=A.ID
LEFT JOIN dbo.T_SYS_POST D ON D.ID=B.POST_ID
LEFT JOIN dbo.T_SYS_DICT C ON C.DICT_CODE=D.POST_DEPT_ID WHERE 1=1 AND A.IS_DEL='0' AND A.IS_HIDE='0' AND IS_USE='1'";
            if (!String.IsNullOrEmpty(tSysUser.REMARK5))
            {
                strSQL += string.Format(" AND C.DICT_CODE='{0}'", tSysUser.REMARK5);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserVo tSysUser)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUser)
            {

                //用户编号
                if (!String.IsNullOrEmpty(tSysUser.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUser.ID.ToString()));
                }
                //用户登录名
                if (!String.IsNullOrEmpty(tSysUser.USER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_NAME = '{0}'", tSysUser.USER_NAME.ToString()));
                }
                //真实姓名
                if (!String.IsNullOrEmpty(tSysUser.REAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAL_NAME like '%{0}%'", tSysUser.REAL_NAME.ToString()));
                }
                //名次排序
                if (!String.IsNullOrEmpty(tSysUser.ORDER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORDER_ID = '{0}'", tSysUser.ORDER_ID.ToString()));
                }
                //用户类型
                if (!String.IsNullOrEmpty(tSysUser.USER_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_TYPE = '{0}'", tSysUser.USER_TYPE.ToString()));
                }
                //登录密码
                if (!String.IsNullOrEmpty(tSysUser.USER_PWD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_PWD = '{0}'", tSysUser.USER_PWD.ToString()));
                }
                //单位编码
                if (!String.IsNullOrEmpty(tSysUser.UNITS_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UNITS_CODE = '{0}'", tSysUser.UNITS_CODE.ToString()));
                }
                //地区编码
                if (!String.IsNullOrEmpty(tSysUser.REGION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REGION_CODE = '{0}'", tSysUser.REGION_CODE.ToString()));
                }
                //职务编码
                if (!String.IsNullOrEmpty(tSysUser.DUTY_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DUTY_CODE = '{0}'", tSysUser.DUTY_CODE.ToString()));
                }
                //业务代码
                if (!String.IsNullOrEmpty(tSysUser.BUSINESS_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BUSINESS_CODE = '{0}'", tSysUser.BUSINESS_CODE.ToString()));
                }
                //性别
                if (!String.IsNullOrEmpty(tSysUser.SEX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEX = '{0}'", tSysUser.SEX.ToString()));
                }
                //出生日期
                if (!String.IsNullOrEmpty(tSysUser.BIRTHDAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BIRTHDAY = '{0}'", tSysUser.BIRTHDAY.ToString()));
                }
                //邮件地址
                if (!String.IsNullOrEmpty(tSysUser.EMAIL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMAIL = '{0}'", tSysUser.EMAIL.ToString()));
                }
                //详细地址
                if (!String.IsNullOrEmpty(tSysUser.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tSysUser.ADDRESS.ToString()));
                }
                //邮编
                if (!String.IsNullOrEmpty(tSysUser.POSTCODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSTCODE = '{0}'", tSysUser.POSTCODE.ToString()));
                }
                //手机号码
                if (!String.IsNullOrEmpty(tSysUser.PHONE_MOBILE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PHONE_MOBILE = '{0}'", tSysUser.PHONE_MOBILE.ToString()));
                }
                //家庭电话
                if (!String.IsNullOrEmpty(tSysUser.PHONE_HOME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PHONE_HOME = '{0}'", tSysUser.PHONE_HOME.ToString()));
                }
                //办公电话
                if (!String.IsNullOrEmpty(tSysUser.PHONE_OFFICE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PHONE_OFFICE = '{0}'", tSysUser.PHONE_OFFICE.ToString()));
                }
                //限定登录IP
                if (!String.IsNullOrEmpty(tSysUser.ALLOW_IP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ALLOW_IP = '{0}'", tSysUser.ALLOW_IP.ToString()));
                }
                //启用标记,1启用,0停用
                if (!String.IsNullOrEmpty(tSysUser.IS_USE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_USE = '{0}'", tSysUser.IS_USE.ToString()));
                }
                //删除标记,1为删除,
                if (!String.IsNullOrEmpty(tSysUser.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysUser.IS_DEL.ToString()));
                }
                //隐藏标记,1为隐藏,
                if (!String.IsNullOrEmpty(tSysUser.IS_HIDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HIDE = '{0}'", tSysUser.IS_HIDE.ToString()));
                }

                //苹果手机MAC地址
                if (!String.IsNullOrEmpty(tSysUser.IOS_MAC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IOS_MAC = '{0}'", tSysUser.IOS_MAC.ToString()));
                }
                //苹果手机是否启用
                if (!String.IsNullOrEmpty(tSysUser.IF_IOS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_IOS = '{0}'", tSysUser.IF_IOS.ToString()));
                }
                //安卓手机MAC地址
                if (!String.IsNullOrEmpty(tSysUser.ANDROID_MAC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANDROID_MAC = '{0}'", tSysUser.ANDROID_MAC.ToString()));
                }
                //安卓手机是否启用
                if (!String.IsNullOrEmpty(tSysUser.IF_ANDROID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_ANDROID = '{0}'", tSysUser.IF_ANDROID.ToString()));
                }

                //创建人ID
                if (!String.IsNullOrEmpty(tSysUser.CREATE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysUser.CREATE_ID.ToString()));
                }
                //创建时间
                if (!String.IsNullOrEmpty(tSysUser.CREATE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysUser.CREATE_TIME.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tSysUser.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysUser.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysUser.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUser.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysUser.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUser.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysUser.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUser.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tSysUser.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysUser.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tSysUser.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysUser.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
