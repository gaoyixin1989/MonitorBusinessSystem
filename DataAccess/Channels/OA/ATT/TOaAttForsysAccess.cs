using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.ATT;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.ATT
{
    /// <summary>
    /// 功能：附件业务登记
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttForsysAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = "select Count(*) from T_OA_ATT_FORSYS " + this.BuildWhereStatement(tOaAttForsys);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaAttForsysVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ATT_FORSYS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaAttForsysVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAttForsys">对象条件</param>
        /// <returns>对象</returns>
        public TOaAttForsysVo Details(TOaAttForsysVo tOaAttForsys)
        {
           string strSQL = String.Format("select * from  T_OA_ATT_FORSYS " + this.BuildWhereStatement(tOaAttForsys));
           return SqlHelper.ExecuteObject(new TOaAttForsysVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaAttForsysVo> SelectByObject(TOaAttForsysVo tOaAttForsys, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_ATT_FORSYS " + this.BuildWhereStatement(tOaAttForsys));
            return SqlHelper.ExecuteObjectList(tOaAttForsys, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaAttForsysVo tOaAttForsys, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_ATT_FORSYS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaAttForsys));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaAttForsys"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = "select * from T_OA_ATT_FORSYS " + this.BuildWhereStatement(tOaAttForsys);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <returns></returns>
        public TOaAttForsysVo SelectByObject(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = "select * from T_OA_ATT_FORSYS " + this.BuildWhereStatement(tOaAttForsys);
            return SqlHelper.ExecuteObject(new TOaAttForsysVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaAttForsys, TOaAttForsysVo.T_OA_ATT_FORSYS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAttForsys">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaAttForsys, TOaAttForsysVo.T_OA_ATT_FORSYS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaAttForsys.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAttForsys_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaAttForsys_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttForsysVo tOaAttForsys_UpdateSet, TOaAttForsysVo tOaAttForsys_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaAttForsys_UpdateSet, TOaAttForsysVo.T_OA_ATT_FORSYS_TABLE);
            strSQL += this.BuildWhereStatement(tOaAttForsys_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ATT_FORSYS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaAttForsysVo tOaAttForsys)
        {
            string strSQL = "delete from T_OA_ATT_FORSYS ";
	    strSQL += this.BuildWhereStatement(tOaAttForsys);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaAttForsys"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaAttForsysVo tOaAttForsys)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaAttForsys)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaAttForsys.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaAttForsys.ID.ToString()));
				}	
				//业务ID
				if (!String.IsNullOrEmpty(tOaAttForsys.BUSINESSID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BUSINESSID = '{0}'", tOaAttForsys.BUSINESSID.ToString()));
				}	
				//附件名称
				if (!String.IsNullOrEmpty(tOaAttForsys.ATTACHEMNTNAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTACHEMNTNAME = '{0}'", tOaAttForsys.ATTACHEMNTNAME.ToString()));
				}	
				//附件路径
				if (!String.IsNullOrEmpty(tOaAttForsys.ATTACHPATH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTACHPATH = '{0}'", tOaAttForsys.ATTACHPATH.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tOaAttForsys.REMARKS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARKS = '{0}'", tOaAttForsys.REMARKS.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
