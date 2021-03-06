using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.EXAMINE;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核总结文件
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineFileAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = "select Count(*) from T_OA_EXAMINE_FILE " + this.BuildWhereStatement(tOaExamineFile);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaExamineFileVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EXAMINE_FILE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaExamineFileVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaExamineFile">对象条件</param>
        /// <returns>对象</returns>
        public TOaExamineFileVo Details(TOaExamineFileVo tOaExamineFile)
        {
           string strSQL = String.Format("select * from  T_OA_EXAMINE_FILE " + this.BuildWhereStatement(tOaExamineFile));
           return SqlHelper.ExecuteObject(new TOaExamineFileVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaExamineFileVo> SelectByObject(TOaExamineFileVo tOaExamineFile, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EXAMINE_FILE " + this.BuildWhereStatement(tOaExamineFile));
            return SqlHelper.ExecuteObjectList(tOaExamineFile, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaExamineFileVo tOaExamineFile, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EXAMINE_FILE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaExamineFile));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaExamineFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = "select * from T_OA_EXAMINE_FILE " + this.BuildWhereStatement(tOaExamineFile);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <returns></returns>
        public TOaExamineFileVo SelectByObject(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = "select * from T_OA_EXAMINE_FILE " + this.BuildWhereStatement(tOaExamineFile);
            return SqlHelper.ExecuteObject(new TOaExamineFileVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaExamineFile, TOaExamineFileVo.T_OA_EXAMINE_FILE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaExamineFile, TOaExamineFileVo.T_OA_EXAMINE_FILE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaExamineFile.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineFile_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaExamineFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineFileVo tOaExamineFile_UpdateSet, TOaExamineFileVo tOaExamineFile_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaExamineFile_UpdateSet, TOaExamineFileVo.T_OA_EXAMINE_FILE_TABLE);
            strSQL += this.BuildWhereStatement(tOaExamineFile_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EXAMINE_FILE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaExamineFileVo tOaExamineFile)
        {
            string strSQL = "delete from T_OA_EXAMINE_FILE ";
	    strSQL += this.BuildWhereStatement(tOaExamineFile);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaExamineFile"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaExamineFileVo tOaExamineFile)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaExamineFile)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaExamineFile.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaExamineFile.ID.ToString()));
				}	
				//业务编码
				if (!String.IsNullOrEmpty(tOaExamineFile.EXAMINE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EXAMINE_ID = '{0}'", tOaExamineFile.EXAMINE_ID.ToString()));
				}	
				//文件类型
				if (!String.IsNullOrEmpty(tOaExamineFile.FILE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FILE_TYPE = '{0}'", tOaExamineFile.FILE_TYPE.ToString()));
				}	
				//文件大小
				if (!String.IsNullOrEmpty(tOaExamineFile.FILE_SIZE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FILE_SIZE = '{0}'", tOaExamineFile.FILE_SIZE.ToString()));
				}	
				//文件内容
				if (!String.IsNullOrEmpty(tOaExamineFile.FILE_BODY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FILE_BODY = '{0}'", tOaExamineFile.FILE_BODY.ToString()));
				}	
				//文件路径
				if (!String.IsNullOrEmpty(tOaExamineFile.FILE_PATH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FILE_PATH = '{0}'", tOaExamineFile.FILE_PATH.ToString()));
				}	
				//文件描述
				if (!String.IsNullOrEmpty(tOaExamineFile.FILE_DESC.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FILE_DESC = '{0}'", tOaExamineFile.FILE_DESC.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaExamineFile.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaExamineFile.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaExamineFile.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaExamineFile.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaExamineFile.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaExamineFile.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaExamineFile.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaExamineFile.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaExamineFile.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaExamineFile.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
