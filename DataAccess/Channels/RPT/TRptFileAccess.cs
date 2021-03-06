using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.RPT;
using i3.ValueObject;
using System.Data.SqlClient;

namespace i3.DataAccess.Channels.RPT
{
    /// <summary>
    /// 功能：报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptFileVo tRptFile)
        {
            string strSQL = "select Count(*) from T_RPT_FILE " + this.BuildWhereStatement(tRptFile);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptFileVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_FILE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TRptFileVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptFile">对象条件</param>
        /// <returns>对象</returns>
        public TRptFileVo Details(TRptFileVo tRptFile)
        {
            string strSQL = String.Format("select * from  T_RPT_FILE " + this.BuildWhereStatement(tRptFile));
            return SqlHelper.ExecuteObject(new TRptFileVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptFileVo> SelectByObject(TRptFileVo tRptFile, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_RPT_FILE " + this.BuildWhereStatement(tRptFile));
            return SqlHelper.ExecuteObjectList(tRptFile, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptFileVo tRptFile, int iIndex, int iCount)
        {

            string strSQL = " select * from T_RPT_FILE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptFile));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptFileVo tRptFile)
        {
            string strSQL = "select * from T_RPT_FILE " + this.BuildWhereStatement(tRptFile);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <returns></returns>
        public TRptFileVo SelectByObject(TRptFileVo tRptFile)
        {
            string strSQL = "select * from T_RPT_FILE " + this.BuildWhereStatement(tRptFile);
            return SqlHelper.ExecuteObject(new TRptFileVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptFileVo tRptFile)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptFile, TRptFileVo.T_RPT_FILE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileVo tRptFile)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptFile, TRptFileVo.T_RPT_FILE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptFile.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFile_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileVo tRptFile_UpdateSet, TRptFileVo tRptFile_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptFile_UpdateSet, TRptFileVo.T_RPT_FILE_TABLE);
            strSQL += this.BuildWhereStatement(tRptFile_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_FILE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptFileVo tRptFile)
        {
            string strSQL = "delete from T_RPT_FILE ";
            strSQL += this.BuildWhereStatement(tRptFile);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region WebOffice
        /// <summary>
        /// 调取文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>文件文件流</returns>
        public byte[] LoadFile(string FileID)
        {
            byte[] objResult = null;
            string strSQL = "select FILE_BODY from T_RPT_FILE where id='{0}'";
            strSQL = String.Format(strSQL, FileID);

            objConnection = new SqlConnection(strConnection);
            objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();

                if (objReader.Read())
                {
                    objResult = objReader.GetSqlBinary(0).Value;
                }
            }
            catch (Exception ex)
            {
                Tips.Append(ex.Message);
            }
            finally
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                objReader.Dispose();
            }

            return objResult;
        }

        /// <summary>
        /// 保存文件
        /// 1、如果存在，覆盖原文件；
        /// 2、如果不存在，插入该文件；
        /// </summary>
        /// <param name="template">文件对象</param>
        /// <returns>是否成功</returns>
        public bool SaveFile(TRptFileVo file)
        {
            string strSQL = String.Empty;
            bool bolResult = false;
            //bool bolContract = false;

            if (ExistThisFile(file.ID))
            {
                strSQL = " {0} WHERE ID = '{1}' ";
                strSQL = string.Format(strSQL, SqlHelper.BuildUpdateExpress(file, TRptFileVo.T_RPT_FILE_TABLE), file.ID);
            }
            else
            {
                //设置序列号
                if (String.IsNullOrEmpty(file.ID))
                {
                    file.ID = base.GetSerialNumber("Rpt_Id");
                }

                strSQL = SqlHelper.BuildInsertExpress(file, TRptFileVo.T_RPT_FILE_TABLE);
            }

            //FileBody是Image数据类型，在此进行特别处理
            if (!String.IsNullOrEmpty(strSQL))
            {
                string NewFiled = "@" + TRptFileVo.FILE_BODY_FIELD;
                strSQL = strSQL.Replace("'System.Byte[]'", NewFiled);

                objConnection = new SqlConnection(base.strConnection);
                objCommand = new SqlCommand(strSQL, objConnection);

                objCommand.Parameters.Add(new SqlParameter(NewFiled, SqlDbType.Image));
                objCommand.Parameters[NewFiled].Value = file.FILE_BODY;

                try
                {
                    objConnection.Open();
                    bolResult = objCommand.ExecuteNonQuery() > 0 ? true : false;
                    //bolContract = (file.CONTRACT_ID.Trim().Length > 0) ? UpdateContractInfo(file.CONTRACT_ID, file.ID) : true;
                }
                catch (Exception ex)
                {
                    Tips.Append(ex.Message);
                }
                finally
                {
                    objConnection.Close();
                    objConnection.Dispose();
                    objCommand.Dispose();
                }
            }

            //return bolResult && bolContract;
            return bolResult;
        }

        /// <summary>
        /// 判断是否存在该文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>是否存在</returns>
        public bool ExistThisFile(string FileID)
        {
            string strSQL = "select id from T_RPT_FILE where id = '{0}'";
            strSQL = String.Format(strSQL, FileID);

            return null != SqlHelper.ExecuteScalar(strSQL) ? true : false;
        }

        /// <summary>
        /// 功能描述：根据业务ID获取最新报告文件信息
        /// 创建时间：2012-12-12
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractId">监测任务ID</param>
        /// <returns></returns>
        public TRptFileVo getNewReportByContractID(string strContractId)
        {
            string strSQL = string.Format("select * from T_RPT_FILE where CONTRACT_ID='{0}' order by ID desc", strContractId);
            return SqlHelper.ExecuteObject(new TRptFileVo(), strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptFile"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptFileVo tRptFile)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptFile)
            {

                //ID
                if (!String.IsNullOrEmpty(tRptFile.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptFile.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tRptFile.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tRptFile.CONTRACT_ID.ToString()));
                }
                //文件名
                if (!String.IsNullOrEmpty(tRptFile.FILE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_NAME = '{0}'", tRptFile.FILE_NAME.ToString()));
                }
                //文件类型
                if (!String.IsNullOrEmpty(tRptFile.FILE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_TYPE = '{0}'", tRptFile.FILE_TYPE.ToString()));
                }
                //文件大小
                if (!String.IsNullOrEmpty(tRptFile.FILE_SIZE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_SIZE = '{0}'", tRptFile.FILE_SIZE.ToString()));
                }
                //文件内容
                //if (!String.IsNullOrEmpty(tRptFile.FILE_BODY.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND FILE_BODY = '{0}'", tRptFile.FILE_BODY.ToString()));
                //}
                //文件路径
                if (!String.IsNullOrEmpty(tRptFile.FILE_PATH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_PATH = '{0}'", tRptFile.FILE_PATH.ToString()));
                }
                //文件描述
                if (!String.IsNullOrEmpty(tRptFile.FILE_DESC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_DESC = '{0}'", tRptFile.FILE_DESC.ToString()));
                }
                //添加日期
                if (!String.IsNullOrEmpty(tRptFile.ADD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_TIME = '{0}'", tRptFile.ADD_TIME.ToString()));
                }
                //添加人
                if (!String.IsNullOrEmpty(tRptFile.ADD_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_USER = '{0}'", tRptFile.ADD_USER.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tRptFile.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tRptFile.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tRptFile.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tRptFile.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tRptFile.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tRptFile.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
