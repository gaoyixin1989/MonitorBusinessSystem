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
    /// 功能：附件信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaAttVo tOaAtt)
        {
            string strSQL = "select Count(*) from T_OA_ATT " + this.BuildWhereStatement(tOaAtt);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaAttVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ATT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaAttVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAtt">对象条件</param>
        /// <returns>对象</returns>
        public TOaAttVo Details(TOaAttVo tOaAtt)
        {
            string strSQL = String.Format("select * from  T_OA_ATT " + this.BuildWhereStatement(tOaAtt));
            return SqlHelper.ExecuteObject(new TOaAttVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaAttVo> SelectByObject(TOaAttVo tOaAtt, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_ATT " + this.BuildWhereStatement(tOaAtt));
            return SqlHelper.ExecuteObjectList(tOaAtt, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaAttVo tOaAtt, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_ATT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaAtt));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaAttVo tOaAtt)
        {
            string strSQL = "select * from T_OA_ATT " + this.BuildWhereStatement(tOaAtt);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <returns></returns>
        public TOaAttVo SelectByObject(TOaAttVo tOaAtt)
        {
            string strSQL = "select * from T_OA_ATT " + this.BuildWhereStatement(tOaAtt);
            return SqlHelper.ExecuteObject(new TOaAttVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaAttVo tOaAtt)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaAtt, TOaAttVo.T_OA_ATT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttVo tOaAtt)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaAtt, TOaAttVo.T_OA_ATT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaAtt.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttVo tOaAtt_UpdateSet, TOaAttVo tOaAtt_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaAtt_UpdateSet, TOaAttVo.T_OA_ATT_TABLE);
            strSQL += this.BuildWhereStatement(tOaAtt_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ATT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaAttVo tOaAtt)
        {
            string strSQL = "delete from T_OA_ATT ";
            strSQL += this.BuildWhereStatement(tOaAtt);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 查询站务管理---人事档案信息的考核历史记录，可以多个附件
        /// </summary>
        /// <param name="tOaAtt"></param>
        /// <returns></returns>
        public DataTable SelectUnionEmployeTable(TOaAttVo tOaAtt)
        {
            string strSQL = String.Format(" SELECT A.ID AS ATTID, A.BUSINESS_ID,A.BUSINESS_TYPE,A.ATTACH_NAME,A.ATTACH_TYPE,A.UPLOAD_PATH,A.UPLOAD_DATE,A.UPLOAD_PERSON,A.DESCRIPTION " +
                                    " FROM dbo.T_OA_ATT A" +
                                    " LEFT JOIN dbo.T_OA_EMPLOYE_EXAMINEHISTORY B ON B.ID=A.BUSINESS_ID" +
                                    " WHERE B.EMPLOYEID='{0}' AND A.BUSINESS_TYPE='{1}'", tOaAtt.REMARKS, tOaAtt.BUSINESS_TYPE);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取监测子任务中第一条附件信息
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="strBusinessType">附件类型</param>
        /// <returns></returns>
        public List<TOaAttVo> getImgByTask(string strTask, string strBusinessType)
        {
            string strSQL = @"select * from  T_OA_ATT where BUSINESS_TYPE='{0}' and BUSINESS_ID in 
                                                (select ID from T_MIS_MONITOR_SUBTASK where TASK_STATUS='09' and TASK_ID='{1}')";
            strSQL = string.Format(strSQL, strBusinessType, strTask);
            return SqlHelper.ExecuteObjectList(new TOaAttVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAtt">对象条件</param>
        /// <returns>对象</returns>
        public DataTable Detail(string Business_ID, string fill_ID)
        {
            string strSQL = String.Format("select * from  T_OA_ATT where BUSINESS_ID='" + Business_ID + "' and fill_ID='" + fill_ID + "'");
            return SqlHelper.ExecuteDataTable( strSQL);
        }
        #endregion

        public DataTable Detail_type(string Business_ID)
        {
            string strSQL = String.Format("select * from  T_OA_ATT where ID='" + Business_ID + "'");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public DataTable DetailZW(string id, string strFileType)
        {
            string strSQL = String.Format("select ID,UPLOAD_PATH,ATTACH_NAME,REMARKS,DESCRIPTION,BUSINESS_ID,ATTACH_TYPE  from  T_OA_ATT  where BUSINESS_TYPE='{1}' and BUSINESS_ID like '%{0}%' order by ID", id, strFileType);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public DataTable DetailFill(string id)
        {
            string strSQL = String.Format("select UPLOAD_PATH,ATTACH_NAME,REMARKS,DESCRIPTION,BUSINESS_ID,ATTACH_TYPE from  T_OA_ATT  where BUSINESS_ID='{0}'", id);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public DataTable DetailFill_ID(string id)
        {
            string strSQL = String.Format("select UPLOAD_PATH,ATTACH_NAME,REMARKS,DESCRIPTION,BUSINESS_ID,ATTACH_TYPE  from  T_OA_ATT  where FILL_ID='{0}'", id);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteDetail(string BusinessID)
        {
            string strSQL = "delete from T_OA_ATT where BUSINESS_ID='" + BusinessID + "'";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteInfo(string BusinessID)
        {
            string strSQL = "delete from T_OA_ATT where ID='" + BusinessID + "'";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaAtt"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaAttVo tOaAtt)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaAtt)
            {

                //编号
                if (!String.IsNullOrEmpty(tOaAtt.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaAtt.ID.ToString()));
                }
                //业务ID
                if (!String.IsNullOrEmpty(tOaAtt.BUSINESS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BUSINESS_ID = '{0}'", tOaAtt.BUSINESS_ID.ToString()));
                }
                //业务类型
                if (!String.IsNullOrEmpty(tOaAtt.BUSINESS_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BUSINESS_TYPE = '{0}'", tOaAtt.BUSINESS_TYPE.ToString()));
                }
                //附件名称
                if (!String.IsNullOrEmpty(tOaAtt.ATTACH_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ATTACH_NAME = '{0}'", tOaAtt.ATTACH_NAME.ToString()));
                }
                //附件类型
                if (!String.IsNullOrEmpty(tOaAtt.ATTACH_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ATTACH_TYPE = '{0}'", tOaAtt.ATTACH_TYPE.ToString()));
                }
                //附件路径
                if (!String.IsNullOrEmpty(tOaAtt.UPLOAD_PATH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOAD_PATH = '{0}'", tOaAtt.UPLOAD_PATH.ToString()));
                }
                //上传日期
                if (!String.IsNullOrEmpty(tOaAtt.UPLOAD_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOAD_DATE = '{0}'", tOaAtt.UPLOAD_DATE.ToString()));
                }
                //上传人ID
                if (!String.IsNullOrEmpty(tOaAtt.UPLOAD_PERSON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOAD_PERSON = '{0}'", tOaAtt.UPLOAD_PERSON.ToString()));
                }
                //附件说明
                if (!String.IsNullOrEmpty(tOaAtt.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tOaAtt.DESCRIPTION.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tOaAtt.REMARKS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARKS = '{0}'", tOaAtt.REMARKS.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tOaAtt.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tOaAtt.FILL_ID.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }
        #endregion
    }
}
