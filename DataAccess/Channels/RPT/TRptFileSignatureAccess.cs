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
    /// 功能：报告文件印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileSignatureAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = "select Count(*) from T_RPT_FILE_SIGNATURE " + this.BuildWhereStatement(tRptFileSignature);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptFileSignatureVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_FILE_SIGNATURE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TRptFileSignatureVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptFileSignature">对象条件</param>
        /// <returns>对象</returns>
        public TRptFileSignatureVo Details(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = String.Format("select * from  T_RPT_FILE_SIGNATURE " + this.BuildWhereStatement(tRptFileSignature));
            return SqlHelper.ExecuteObject(new TRptFileSignatureVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptFileSignatureVo> SelectByObject(TRptFileSignatureVo tRptFileSignature, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_RPT_FILE_SIGNATURE " + this.BuildWhereStatement(tRptFileSignature));
            return SqlHelper.ExecuteObjectList(tRptFileSignature, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptFileSignatureVo tRptFileSignature, int iIndex, int iCount)
        {

            string strSQL = " select * from T_RPT_FILE_SIGNATURE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptFileSignature));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptFileSignature"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = "select * from T_RPT_FILE_SIGNATURE " + this.BuildWhereStatement(tRptFileSignature);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <returns></returns>
        public TRptFileSignatureVo SelectByObject(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = "select * from T_RPT_FILE_SIGNATURE " + this.BuildWhereStatement(tRptFileSignature);
            return SqlHelper.ExecuteObject(new TRptFileSignatureVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptFileSignature, TRptFileSignatureVo.T_RPT_FILE_SIGNATURE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFileSignature">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptFileSignature, TRptFileSignatureVo.T_RPT_FILE_SIGNATURE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptFileSignature.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFileSignature_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptFileSignature_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileSignatureVo tRptFileSignature_UpdateSet, TRptFileSignatureVo tRptFileSignature_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptFileSignature_UpdateSet, TRptFileSignatureVo.T_RPT_FILE_SIGNATURE_TABLE);
            strSQL += this.BuildWhereStatement(tRptFileSignature_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_FILE_SIGNATURE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptFileSignatureVo tRptFileSignature)
        {
            string strSQL = "delete from T_RPT_FILE_SIGNATURE ";
            strSQL += this.BuildWhereStatement(tRptFileSignature);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region WebOffice
        /// <summary>
        /// 保存印章
        /// </summary>
        /// <param name="signature">印章对象</param>
        /// <returns>是否成功</returns>
        public bool SaveSignature(TRptFileSignatureVo signature)
        {
            //设置序列号
            signature.ID = base.GetSerialNumber("File_Sign_Id");
            string strSQL = SqlHelper.BuildInsertExpress(signature, TRptFileSignatureVo.T_RPT_FILE_SIGNATURE_TABLE);

            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 调取指定文档的所有印章
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>印章对象</returns>
        public TRptFileSignatureVo LoadSignature(string FileID)
        {
            string strSQL = " select mark_name,add_user,add_time,add_ip,mark_guid from T_RPT_FILE_SIGNATURE where file_id='{0}' ";
            strSQL = String.Format(strSQL, FileID);
            StringBuilder objMarkName = new StringBuilder();
            StringBuilder objAddUser = new StringBuilder();
            StringBuilder objAddTime = new StringBuilder();
            StringBuilder objAddIP = new StringBuilder();
            StringBuilder objMarkGuid = new StringBuilder();
            objConnection = new SqlConnection(strConnection);
            objCommand = new SqlCommand(strSQL, objConnection);

            TRptFileSignatureVo signature = new TRptFileSignatureVo();

            try
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();

                while (objReader.Read())
                {
                    objMarkName.Append(objReader[TRptFileSignatureVo.MARK_NAME_FIELD] + "\r\n");
                    objAddUser.Append(objReader[TRptFileSignatureVo.ADD_USER_FIELD] + "\r\n");
                    objAddTime.Append(objReader.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                    objAddIP.Append(objReader[TRptFileSignatureVo.ADD_IP_FIELD] + "\r\n");
                    objMarkGuid.Append(objReader[TRptFileSignatureVo.MARK_GUID_FIELD] + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Tips.Append(ex.Message);
            }
            finally
            {
                signature.MARK_NAME = objMarkName.ToString();
                signature.ADD_USER = objAddUser.ToString();
                signature.ADD_TIME = objAddTime.ToString();
                signature.ADD_IP = objAddIP.ToString();
                signature.MARK_GUID = objMarkGuid.ToString();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                objReader.Dispose();
            }

            return signature;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptFileSignature"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptFileSignatureVo tRptFileSignature)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptFileSignature)
            {

                //ID
                if (!String.IsNullOrEmpty(tRptFileSignature.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptFileSignature.ID.ToString()));
                }
                //文件ID
                if (!String.IsNullOrEmpty(tRptFileSignature.FILE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_ID = '{0}'", tRptFileSignature.FILE_ID.ToString()));
                }
                //印章名称
                if (!String.IsNullOrEmpty(tRptFileSignature.MARK_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_NAME = '{0}'", tRptFileSignature.MARK_NAME.ToString()));
                }
                //添加用户
                if (!String.IsNullOrEmpty(tRptFileSignature.ADD_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_USER = '{0}'", tRptFileSignature.ADD_USER.ToString()));
                }
                //添加日期
                if (!String.IsNullOrEmpty(tRptFileSignature.ADD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_TIME = '{0}'", tRptFileSignature.ADD_TIME.ToString()));
                }
                //添加IP
                if (!String.IsNullOrEmpty(tRptFileSignature.ADD_IP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_IP = '{0}'", tRptFileSignature.ADD_IP.ToString()));
                }
                //印章
                if (!String.IsNullOrEmpty(tRptFileSignature.MARK_GUID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_GUID = '{0}'", tRptFileSignature.MARK_GUID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tRptFileSignature.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tRptFileSignature.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tRptFileSignature.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tRptFileSignature.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tRptFileSignature.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tRptFileSignature.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
