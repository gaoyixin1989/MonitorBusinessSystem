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
    /// 功能：模板表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptTemplateAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptTemplateVo tRptTemplate)
        {
            string strSQL = "select Count(*) from T_RPT_TEMPLATE " + this.BuildWhereStatement(tRptTemplate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptTemplateVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_TEMPLATE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TRptTemplateVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptTemplate">对象条件</param>
        /// <returns>对象</returns>
        public TRptTemplateVo Details(TRptTemplateVo tRptTemplate)
        {
            string strSQL = String.Format("select * from  T_RPT_TEMPLATE " + this.BuildWhereStatement(tRptTemplate));
            return SqlHelper.ExecuteObject(new TRptTemplateVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptTemplateVo> SelectByObject(TRptTemplateVo tRptTemplate, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_RPT_TEMPLATE " + this.BuildWhereStatement(tRptTemplate));
            return SqlHelper.ExecuteObjectList(tRptTemplate, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptTemplateVo tRptTemplate, int iIndex, int iCount)
        {

            string strSQL = " select * from T_RPT_TEMPLATE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptTemplate));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptTemplate"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptTemplateVo tRptTemplate)
        {
            string strSQL = "select * from T_RPT_TEMPLATE " + this.BuildWhereStatement(tRptTemplate) + "order by ID desc";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <returns></returns>
        public TRptTemplateVo SelectByObject(TRptTemplateVo tRptTemplate)
        {
            string strSQL = "select * from T_RPT_TEMPLATE " + this.BuildWhereStatement(tRptTemplate);
            return SqlHelper.ExecuteObject(new TRptTemplateVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptTemplateVo tRptTemplate)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptTemplate, TRptTemplateVo.T_RPT_TEMPLATE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplate">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateVo tRptTemplate)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptTemplate, TRptTemplateVo.T_RPT_TEMPLATE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptTemplate.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplate_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptTemplate_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateVo tRptTemplate_UpdateSet, TRptTemplateVo tRptTemplate_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptTemplate_UpdateSet, TRptTemplateVo.T_RPT_TEMPLATE_TABLE);
            strSQL += this.BuildWhereStatement(tRptTemplate_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_TEMPLATE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptTemplateVo tRptTemplate)
        {
            string strSQL = "delete from T_RPT_TEMPLATE ";
            strSQL += this.BuildWhereStatement(tRptTemplate);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region WebOffice
        /// <summary>
        /// 调取模板文件
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>模板文件流</returns>
        public byte[] LoadTemplate(string TemplateID)
        {
            byte[] objResult = null;
            string strSQL = "select FILE_BODY from T_RPT_TEMPLATE where id='{0}'";
            strSQL = String.Format(strSQL, TemplateID);
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
        /// 保存模板文件
        /// 1、如果存在，覆盖原模板；
        /// 2、如果不存在，插入该模板；
        /// </summary>
        /// <param name="template">模板对象</param>
        /// <returns>是否成功</returns>
        public bool SaveTemplate(TRptTemplateVo template)
        {
            string strSQL = String.Empty;
            bool bolResult = false;

            if (ExistThisTemplate(template.ID))
            {
                strSQL = " {0} WHERE ID = '{1}' ";
                strSQL = string.Format(strSQL, SqlHelper.BuildUpdateExpress(template, TRptTemplateVo.T_RPT_TEMPLATE_TABLE), template.ID);
            }
            else
            {
                //设置序列号
                template.ID = base.GetSerialNumber("Template_Id");
                strSQL = SqlHelper.BuildInsertExpress(template, TRptTemplateVo.T_RPT_TEMPLATE_TABLE);
            }

            //FileBody是Image数据类型，在此进行特别处理
            if (!String.IsNullOrEmpty(strSQL))
            {
                string NewFiled = "@" + TRptTemplateVo.FILE_BODY_FIELD;
                strSQL = strSQL.Replace("'System.Byte[]'", NewFiled);

                objConnection = new SqlConnection(base.strConnection);
                objCommand = new SqlCommand(strSQL, objConnection);

                objCommand.Parameters.Add(new SqlParameter(NewFiled, SqlDbType.Image));
                objCommand.Parameters[NewFiled].Value = template.FILE_BODY;

                try
                {
                    objConnection.Open();
                    bolResult = objCommand.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Tips.Append(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                }
            }

            return bolResult;
        }

        /// <summary>
        /// 判断是否存在该模板
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>是否存在</returns>
        public bool ExistThisTemplate(string TemplateID)
        {
            string strSQL = "select id from T_RPT_TEMPLATE where id = '{0}'";
            strSQL = String.Format(strSQL, TemplateID);

            return null != SqlHelper.ExecuteScalar(strSQL) ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptTemplate"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptTemplateVo tRptTemplate)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptTemplate)
            {

                //ID
                if (!String.IsNullOrEmpty(tRptTemplate.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptTemplate.ID.ToString()));
                }
                //文件名
                if (!String.IsNullOrEmpty(tRptTemplate.FILE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_NAME = '{0}'", tRptTemplate.FILE_NAME.ToString()));
                }
                //文件类型
                if (!String.IsNullOrEmpty(tRptTemplate.FILE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_TYPE = '{0}'", tRptTemplate.FILE_TYPE.ToString()));
                }
                //文件大小
                if (!String.IsNullOrEmpty(tRptTemplate.FILE_SIZE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_SIZE = '{0}'", tRptTemplate.FILE_SIZE.ToString()));
                }
                //文件内容
                //if (!String.IsNullOrEmpty(tRptTemplate.FILE_BODY.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND FILE_BODY = '{0}'", tRptTemplate.FILE_BODY.ToString()));
                //}
                //文件路径
                if (!String.IsNullOrEmpty(tRptTemplate.FILE_PATH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_PATH = '{0}'", tRptTemplate.FILE_PATH.ToString()));
                }
                //文件描述
                if (!String.IsNullOrEmpty(tRptTemplate.FILE_DESC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_DESC = '{0}'", tRptTemplate.FILE_DESC.ToString()));
                }
                //添加日期
                if (!String.IsNullOrEmpty(tRptTemplate.ADD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_TIME = '{0}'", tRptTemplate.ADD_TIME.ToString()));
                }
                //添加人
                if (!String.IsNullOrEmpty(tRptTemplate.ADD_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_USER = '{0}'", tRptTemplate.ADD_USER.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tRptTemplate.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tRptTemplate.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tRptTemplate.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tRptTemplate.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tRptTemplate.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tRptTemplate.REMARK3.ToString()));
                }
                //是否屏蔽
                if (!String.IsNullOrEmpty(tRptTemplate.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tRptTemplate.IS_DEL.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
