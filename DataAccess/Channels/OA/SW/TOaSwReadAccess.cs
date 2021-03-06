using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.SW;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文传阅
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwReadAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwReadVo tOaSwRead)
        {
            string strSQL = "select Count(*) from T_OA_SW_READ " + this.BuildWhereStatement(tOaSwRead);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwReadVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_SW_READ  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaSwReadVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="swid">收文ID</param>
        /// <returns>返回结果</returns>
        public List<TOaSwReadVo> SelectByReadUser(string swid)
        {
            string strSQL = String.Format("select * from  T_OA_SW_READ where SW_ID="+swid);
            return SqlHelper.ExecuteObjectList(new TOaSwReadVo(), BuildPagerExpress(strSQL, 0, 0));
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwRead">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwReadVo Details(TOaSwReadVo tOaSwRead)
        {
           string strSQL = String.Format("select * from  T_OA_SW_READ " + this.BuildWhereStatement(tOaSwRead));
           return SqlHelper.ExecuteObject(new TOaSwReadVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwReadVo> SelectByObject(TOaSwReadVo tOaSwRead, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_SW_READ " + this.BuildWhereStatement(tOaSwRead));
            return SqlHelper.ExecuteObjectList(tOaSwRead, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwReadVo tOaSwRead, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_SW_READ {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaSwRead));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwRead"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwReadVo tOaSwRead)
        {
            string strSQL = "select * from T_OA_SW_READ " + this.BuildWhereStatement(tOaSwRead);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <returns></returns>
        public TOaSwReadVo SelectByObject(TOaSwReadVo tOaSwRead)
        {
            string strSQL = "select * from T_OA_SW_READ " + this.BuildWhereStatement(tOaSwRead);
            return SqlHelper.ExecuteObject(new TOaSwReadVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwReadVo tOaSwRead)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaSwRead, TOaSwReadVo.T_OA_SW_READ_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwRead">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwReadVo tOaSwRead)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwRead, TOaSwReadVo.T_OA_SW_READ_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaSwRead.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwRead_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSwRead_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwReadVo tOaSwRead_UpdateSet, TOaSwReadVo tOaSwRead_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwRead_UpdateSet, TOaSwReadVo.T_OA_SW_READ_TABLE);
            strSQL += this.BuildWhereStatement(tOaSwRead_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_SW_READ where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaSwReadVo tOaSwRead)
        {
            string strSQL = "delete from T_OA_SW_READ ";
	    strSQL += this.BuildWhereStatement(tOaSwRead);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSwRead"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaSwReadVo tOaSwRead)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSwRead)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaSwRead.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaSwRead.ID.ToString()));
				}	
				//收文ID
				if (!String.IsNullOrEmpty(tOaSwRead.SW_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_ID = '{0}'", tOaSwRead.SW_ID.ToString()));
				}	
				//传阅人ID
				if (!String.IsNullOrEmpty(tOaSwRead.SW_PLAN_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_PLAN_ID = '{0}'", tOaSwRead.SW_PLAN_ID.ToString()));
				}	
				//传阅日期
				if (!String.IsNullOrEmpty(tOaSwRead.SW_PLAN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_PLAN_DATE = '{0}'", tOaSwRead.SW_PLAN_DATE.ToString()));
				}	
				//是否已阅
				if (!String.IsNullOrEmpty(tOaSwRead.IS_OK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_OK = '{0}'", tOaSwRead.IS_OK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaSwRead.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaSwRead.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaSwRead.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaSwRead.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaSwRead.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaSwRead.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaSwRead.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaSwRead.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaSwRead.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaSwRead.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
