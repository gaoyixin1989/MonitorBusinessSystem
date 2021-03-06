using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.PART;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料领用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartCollarAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = "select Count(*) from T_OA_PART_COLLAR " + this.BuildWhereStatement(tOaPartCollar);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartCollarVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_COLLAR  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartCollarVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartCollar">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartCollarVo Details(TOaPartCollarVo tOaPartCollar)
        {
           string strSQL = String.Format("select * from  T_OA_PART_COLLAR " + this.BuildWhereStatement(tOaPartCollar));
           return SqlHelper.ExecuteObject(new TOaPartCollarVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartCollarVo> SelectByObject(TOaPartCollarVo tOaPartCollar, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_COLLAR " + this.BuildWhereStatement(tOaPartCollar));
            return SqlHelper.ExecuteObjectList(tOaPartCollar, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartCollarVo tOaPartCollar, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_PART_COLLAR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartCollar));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = "select * from T_OA_PART_COLLAR " + this.BuildWhereStatement(tOaPartCollar);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <returns></returns>
        public TOaPartCollarVo SelectByObject(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = "select * from T_OA_PART_COLLAR " + this.BuildWhereStatement(tOaPartCollar);
            return SqlHelper.ExecuteObject(new TOaPartCollarVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartCollar, TOaPartCollarVo.T_OA_PART_COLLAR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartCollar">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartCollar, TOaPartCollarVo.T_OA_PART_COLLAR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartCollar.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartCollar_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartCollar_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartCollarVo tOaPartCollar_UpdateSet, TOaPartCollarVo tOaPartCollar_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartCollar_UpdateSet, TOaPartCollarVo.T_OA_PART_COLLAR_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartCollar_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_COLLAR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartCollarVo tOaPartCollar)
        {
            string strSQL = "delete from T_OA_PART_COLLAR ";
	    strSQL += this.BuildWhereStatement(tOaPartCollar);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取领用物料明细
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionPartByTable(TOaPartCollarVo tOaPartCollar, TOaPartInfoVo tOaPartInfor,int iIndex,int iCount)
        {
            string strSQL = @" SELECT A.ID,A.PART_ID,A.USED_QUANTITY,A.USER_ID,A.LASTIN_DATE,A.REMARK1,A.REASON,B.PART_NAME,B.PART_CODE,B.UNIT,B.MODELS,C.REAL_NAME,C.USER_NAME
                                           FROM T_OA_PART_COLLAR A 
                                           JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0' 
                                           LEFT JOIN T_SYS_USER C ON C.ID=A.USER_ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tOaPartCollar.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartCollar.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.ID))
            {
                strSQL += String.Format(" AND B.ID = '{0}'", tOaPartInfor.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format(" AND B.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format(" AND B.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }

            if (!String.IsNullOrEmpty(tOaPartCollar.REMARK4)&&!String.IsNullOrEmpty(tOaPartCollar.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.LASTIN_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartCollar.REMARK4, tOaPartCollar.REMARK5);
          
            }
            if (!String.IsNullOrEmpty(tOaPartCollar.USER_ID)) 
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '%{0}%'", tOaPartCollar.USER_ID);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取领用物料明细总记录数
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetUnionPartByTableCount(TOaPartCollarVo tOaPartCollar, TOaPartInfoVo tOaPartInfor)
        {
            string strSQL = @" SELECT A.ID,A.PART_ID,A.USED_QUANTITY,A.USER_ID,A.LASTIN_DATE,A.REASON,B.PART_NAME,B.MODELS,B.PART_CODE,C.REAL_NAME,C.USER_NAME
                                           FROM T_OA_PART_COLLAR A 
                                           JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0'
                                           LEFT JOIN T_SYS_USER C ON C.ID=A.USER_ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tOaPartCollar.ID))
            {
                strSQL += String.Format(" AND A.ID IN ('{0}')", tOaPartCollar.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.ID))
            {
                strSQL += String.Format(" AND B.ID = '{0}'", tOaPartInfor.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_CODE))
            {
                strSQL += String.Format(" AND B.PART_CODE LIKE '%{0}%'", tOaPartInfor.PART_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartInfor.PART_NAME))
            {
                strSQL += String.Format(" AND B.PART_NAME LIKE '%{0}%'", tOaPartInfor.PART_NAME);
            }

            if (!String.IsNullOrEmpty(tOaPartCollar.REMARK4) && !String.IsNullOrEmpty(tOaPartCollar.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.LASTIN_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartCollar.REMARK4, tOaPartCollar.REMARK5);

            }

            if (!String.IsNullOrEmpty(tOaPartCollar.USER_ID))
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '{0}'", tOaPartCollar.USER_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
         #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartCollarVo tOaPartCollar)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartCollar)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaPartCollar.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartCollar.ID.ToString()));
				}	
				//物料ID
				if (!String.IsNullOrEmpty(tOaPartCollar.PART_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PART_ID = '{0}'", tOaPartCollar.PART_ID.ToString()));
				}	
				//领用数量
				if (!String.IsNullOrEmpty(tOaPartCollar.USED_QUANTITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USED_QUANTITY = '{0}'", tOaPartCollar.USED_QUANTITY.ToString()));
				}	
				//领用人ID
				if (!String.IsNullOrEmpty(tOaPartCollar.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tOaPartCollar.USER_ID.ToString()));
				}	
				//领用日期
				if (!String.IsNullOrEmpty(tOaPartCollar.LASTIN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LASTIN_DATE = '{0}'", tOaPartCollar.LASTIN_DATE.ToString()));
				}	
				//领用理由
				if (!String.IsNullOrEmpty(tOaPartCollar.REASON.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REASON = '{0}'", tOaPartCollar.REASON.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaPartCollar.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartCollar.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartCollar.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartCollar.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartCollar.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartCollar.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartCollar.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartCollar.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaPartCollar.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartCollar.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
