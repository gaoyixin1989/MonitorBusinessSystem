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
    /// 功能：标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptMarkAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptMarkVo tRptMark)
        {
            string strSQL = "select Count(*) from T_RPT_MARK " + this.BuildWhereStatement(tRptMark);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptMarkVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_MARK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TRptMarkVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptMark">对象条件</param>
        /// <returns>对象</returns>
        public TRptMarkVo Details(TRptMarkVo tRptMark)
        {
            string strSQL = String.Format("select * from  T_RPT_MARK " + this.BuildWhereStatement(tRptMark));
            return SqlHelper.ExecuteObject(new TRptMarkVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptMarkVo> SelectByObject(TRptMarkVo tRptMark, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_RPT_MARK " + this.BuildWhereStatement(tRptMark));
            return SqlHelper.ExecuteObjectList(tRptMark, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptMarkVo tRptMark, int iIndex, int iCount)
        {

            string strSQL = " select * from T_RPT_MARK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptMark));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptMark"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptMarkVo tRptMark)
        {
            string strSQL = "select * from T_RPT_MARK " + this.BuildWhereStatement(tRptMark);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <returns></returns>
        public TRptMarkVo SelectByObject(TRptMarkVo tRptMark)
        {
            string strSQL = "select * from T_RPT_MARK " + this.BuildWhereStatement(tRptMark);
            return SqlHelper.ExecuteObject(new TRptMarkVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptMarkVo tRptMark)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptMark, TRptMarkVo.T_RPT_MARK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptMark">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptMarkVo tRptMark)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptMark, TRptMarkVo.T_RPT_MARK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptMark.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptMark_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptMark_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptMarkVo tRptMark_UpdateSet, TRptMarkVo tRptMark_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptMark_UpdateSet, TRptMarkVo.T_RPT_MARK_TABLE);
            strSQL += this.BuildWhereStatement(tRptMark_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_MARK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptMarkVo tRptMark)
        {
            string strSQL = "delete from T_RPT_MARK ";
            strSQL += this.BuildWhereStatement(tRptMark);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region WebOffice
        /// <summary>
        /// 列出所有的标签
        /// </summary>
        /// <param name="MarkName">标签名称</param>
        /// <param name="MarkDesc">标签描述</param>
        /// <returns></returns>
        public void ListBookMarks(out string MarkName, out string MarkDesc)
        {
            string strSQL = " select mark_name,mark_desc from T_RPT_MARK ";
            StringBuilder objName = new StringBuilder();
            StringBuilder objDesc = new StringBuilder();
            objConnection = new SqlConnection(strConnection);
            objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();

                while (objReader.Read())
                {
                    objName.Append(objReader["mark_name"] + "\r\n");
                    objDesc.Append(objReader["mark_desc"] + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Tips.Append(ex.Message);
            }
            finally
            {
                //转化值对象
                MarkName = objName.ToString();
                MarkDesc = objDesc.ToString();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                objReader.Dispose();
            }
        }

        /// <summary>
        /// 调取模板的标签
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>标签对象列表</returns>
        public DataTable LoadBookMarks(string TemplateID)
        {
            string strSQL = " select template_id,mark_name,atrribute_name from T_RPT_MARK mark,T_RPT_TEMPLATE_MARK template where mark.id = template.mark_id  and template_id='{0}' ";
            strSQL = String.Format(strSQL, TemplateID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 保存模板标签
        /// </summary>
        /// <param name="MarkList">标签列表</param>
        /// <returns>是否成功</returns>
        public bool SaveBookMarks(string MarkNameList, string TemplateID)
        {
            //结果开关
            bool bolResult = true;

            //清除模板的标签
            string strSqlClean = "delete from T_RPT_TEMPLATE_MARK where template_id = '{0}'";
            strSqlClean = String.Format(strSqlClean, TemplateID);
            //清除
            SqlHelper.ExecuteNonQuery(strSqlClean);

            //保存新的模板标签
            string[] NameList = MarkNameList.Split('&');

            if (null != NameList && NameList.Length > 0)
            {
                for (int i = 0; i < NameList.Length; i++)
                {
                    string strSQL = "insert into T_RPT_TEMPLATE_MARK (id,template_id,mark_id) values ('{0}','{1}','{2}')";
                    strSQL = String.Format(strSQL, base.GetSerialNumber("Template_Mark_Id"), TemplateID, NameList[i]);

                    if (SqlHelper.ExecuteNonQuery(strSQL) == -1)
                    {
                        bolResult = false;
                    }
                }
            }

            return bolResult;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptMark"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptMarkVo tRptMark)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptMark)
            {

                //ID
                if (!String.IsNullOrEmpty(tRptMark.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptMark.ID.ToString()));
                }
                //标签名称
                if (!String.IsNullOrEmpty(tRptMark.MARK_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_NAME = '{0}'", tRptMark.MARK_NAME.ToString()));
                }
                //标签描述
                if (!String.IsNullOrEmpty(tRptMark.MARK_DESC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_DESC = '{0}'", tRptMark.MARK_DESC.ToString()));
                }
                //标签说明
                if (!String.IsNullOrEmpty(tRptMark.MARK_REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_REMARK = '{0}'", tRptMark.MARK_REMARK.ToString()));
                }
                //属性名称
                if (!String.IsNullOrEmpty(tRptMark.ATTRIBUTE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ATTRIBUTE_NAME = '{0}'", tRptMark.ATTRIBUTE_NAME.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tRptMark.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tRptMark.REMARK.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
