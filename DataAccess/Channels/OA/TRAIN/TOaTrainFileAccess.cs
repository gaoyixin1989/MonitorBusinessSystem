using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.TRAIN;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训文件附件信息
    /// 创建日期：2013-01-28
    /// 创建人：胡方扬
    /// </summary>
    public class TOaTrainFileAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = "select Count(*) from T_OA_TRAIN_FILE " + this.BuildWhereStatement(tOaTrainFile);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainFileVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_TRAIN_FILE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaTrainFileVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainFile">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainFileVo Details(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = String.Format("select * from  T_OA_TRAIN_FILE " + this.BuildWhereStatement(tOaTrainFile));
            return SqlHelper.ExecuteObject(new TOaTrainFileVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainFileVo> SelectByObject(TOaTrainFileVo tOaTrainFile, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_TRAIN_FILE " + this.BuildWhereStatement(tOaTrainFile));
            return SqlHelper.ExecuteObjectList(tOaTrainFile, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainFileVo tOaTrainFile, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_TRAIN_FILE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaTrainFile));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = "select * from T_OA_TRAIN_FILE " + this.BuildWhereStatement(tOaTrainFile);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <returns></returns>
        public TOaTrainFileVo SelectByObject(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = "select * from T_OA_TRAIN_FILE " + this.BuildWhereStatement(tOaTrainFile);
            return SqlHelper.ExecuteObject(new TOaTrainFileVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaTrainFile, TOaTrainFileVo.T_OA_TRAIN_FILE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainFile, TOaTrainFileVo.T_OA_TRAIN_FILE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaTrainFile.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainFile_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaTrainFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainFileVo tOaTrainFile_UpdateSet, TOaTrainFileVo tOaTrainFile_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaTrainFile_UpdateSet, TOaTrainFileVo.T_OA_TRAIN_FILE_TABLE);
            strSQL += this.BuildWhereStatement(tOaTrainFile_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_TRAIN_FILE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = "delete from T_OA_TRAIN_FILE ";
            strSQL += this.BuildWhereStatement(tOaTrainFile);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 获取指定培训计划的所有附件信息 Create By 胡方扬 2013-1-28
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public DataTable TrainFileByTableList(TOaTrainFileVo tOaTrainFile,int iIndex,int iCount)
        {
            string strSQL = @"SELECT A.ID,  B.ID AS ATTID,B.BUSINESS_ID,B.BUSINESS_TYPE,B.ATTACH_NAME,B.ATTACH_TYPE,B.UPLOAD_DATE,B.UPLOAD_PERSON 
                                       FROM T_OA_TRAIN_FILE A LEFT JOIN  T_OA_ATT  B ON B.BUSINESS_ID=A.ID WHERE 1=1";
            strSQL += String.Format(" AND A.TRAIN_PLAN_ID='{0}'", tOaTrainFile.TRAIN_PLAN_ID);
            strSQL += String.Format(" AND B.BUSINESS_TYPE='{0}'", tOaTrainFile.REMARK4);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress( strSQL,iIndex,iCount));
        }

                /// <summary>
        /// 获取指定培训计划的所有附件信息记录 Create By 胡方扬 2013-1-28
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public int SelectTrainFileByTableCount(TOaTrainFileVo tOaTrainFile)
        {
            string strSQL = @"SELECT A.ID AS TRAIN_FILEID,  B.ID AS ATTID,B.BUSINESS_ID,B.BUSINESS_TYPE,B.ATTACH_NAME,B.ATTACH_TYPE,B.UPLOAD_DATE,B.UPLOAD_PERSON 
                                       FROM T_OA_TRAIN_FILE A LEFT JOIN  T_OA_ATT  B ON B.BUSINESS_ID=A.ID WHERE 1=1";
            strSQL += String.Format(" AND A.TRAIN_PLAN_ID='{0}'", tOaTrainFile.TRAIN_PLAN_ID);
            strSQL += String.Format(" AND B.BUSINESS_TYPE='{0}'", tOaTrainFile.REMARK4);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaTrainFileVo tOaTrainFile)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaTrainFile)
            {

                //
                if (!String.IsNullOrEmpty(tOaTrainFile.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaTrainFile.ID.ToString()));
                }
                //培训计划ID
                if (!String.IsNullOrEmpty(tOaTrainFile.TRAIN_PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRAIN_PLAN_ID = '{0}'", tOaTrainFile.TRAIN_PLAN_ID.ToString()));
                }

                if (!String.IsNullOrEmpty(tOaTrainFile.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaTrainFile.REMARK1.ToString()));
                }
                if (!String.IsNullOrEmpty(tOaTrainFile.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2= '{0}'", tOaTrainFile.REMARK2.ToString()));
                }
                if (!String.IsNullOrEmpty(tOaTrainFile.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3= '{0}'", tOaTrainFile.REMARK3.ToString()));
                }
                if (!String.IsNullOrEmpty(tOaTrainFile.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaTrainFile.REMARK4.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
