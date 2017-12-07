using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书样品
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSampleAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_SAMPLE " + this.BuildWhereStatement(tMisContractSample);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSampleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractSampleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSample">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSampleVo Details(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE " + this.BuildWhereStatement(tMisContractSample));
            return SqlHelper.ExecuteObject(new TMisContractSampleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSampleVo> SelectByObject(TMisContractSampleVo tMisContractSample, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE " + this.BuildWhereStatement(tMisContractSample));
            return SqlHelper.ExecuteObjectList(tMisContractSample, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSampleVo tMisContractSample, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_SAMPLE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractSample));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSample"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLE " + this.BuildWhereStatement(tMisContractSample);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <returns></returns>
        public TMisContractSampleVo SelectByObject(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLE " + this.BuildWhereStatement(tMisContractSample);
            return SqlHelper.ExecuteObject(new TMisContractSampleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractSample, TMisContractSampleVo.T_MIS_CONTRACT_SAMPLE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSample">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSample, TMisContractSampleVo.T_MIS_CONTRACT_SAMPLE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractSample.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSample_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSample_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleVo tMisContractSample_UpdateSet, TMisContractSampleVo tMisContractSample_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSample_UpdateSet, TMisContractSampleVo.T_MIS_CONTRACT_SAMPLE_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractSample_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_SAMPLE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractSampleVo tMisContractSample)
        {
            string strSQL = "delete from T_MIS_CONTRACT_SAMPLE ";
            strSQL += this.BuildWhereStatement(tMisContractSample);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractSample"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractSampleVo tMisContractSample)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractSample)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractSample.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractSample.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisContractSample.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractSample.CONTRACT_ID.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tMisContractSample.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tMisContractSample.MONITOR_ID.ToString()));
                }
                //样品类型
                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_TYPE = '{0}'", tMisContractSample.SAMPLE_TYPE.ToString()));
                }
                //样品名称
                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_NAME = '{0}'", tMisContractSample.SAMPLE_NAME.ToString()));
                }
                //样品数量
                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_COUNT = '{0}'", tMisContractSample.SAMPLE_COUNT.ToString()));
                }
                //自送样样品接收时间/位置
                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_ACCEPT_DATEORACC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ACCEPT_DATEORACC = '{0}'", tMisContractSample.SAMPLE_ACCEPT_DATEORACC.ToString()));
                }
                //自送样样品原编号/名称
                if (!String.IsNullOrEmpty(tMisContractSample.SRC_CODEORNAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SRC_CODEORNAME = '{0}'", tMisContractSample.SRC_CODEORNAME.ToString()));
                }
                //自送样样品原始状态
                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_STATUS = '{0}'", tMisContractSample.SAMPLE_STATUS.ToString()));
                }

                if (!String.IsNullOrEmpty(tMisContractSample.SAMPLE_PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_PLAN_ID = '{0}'", tMisContractSample.SAMPLE_PLAN_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisContractSample.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractSample.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContractSample.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractSample.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContractSample.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractSample.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContractSample.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractSample.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisContractSample.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractSample.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
