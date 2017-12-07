using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using System.Data;

namespace i3.DataAccess.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品信息
    /// 创建日期：2013-09-12
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = "select Count(*) from T_OA_PARTSTAND_INFO " + this.BuildWhereStatement(tOaPartstandInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartstandInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PARTSTAND_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaPartstandInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartstandInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartstandInfoVo Details(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = String.Format("select * from  T_OA_PARTSTAND_INFO " + this.BuildWhereStatement(tOaPartstandInfo));
            return SqlHelper.ExecuteObject(new TOaPartstandInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartstandInfoVo> SelectByObject(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_PARTSTAND_INFO " + this.BuildWhereStatement(tOaPartstandInfo));
            return SqlHelper.ExecuteObjectList(tOaPartstandInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_PARTSTAND_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartstandInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableNew(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount, bool isZore)
        {
            string strSQL = " select * from T_OA_PARTSTAND_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartstandInfo));
            if (isZore)
                strSQL += " AND INVENTORY='0' order by " + tOaPartstandInfo.SORT_FIELD + " " + tOaPartstandInfo.SORT_TYPE;
            else
                strSQL += " AND INVENTORY<>'0' order by " + tOaPartstandInfo.SORT_FIELD + " " + tOaPartstandInfo.SORT_TYPE;
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartstandInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = "select * from T_OA_PARTSTAND_INFO " + this.BuildWhereStatement(tOaPartstandInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <returns></returns>
        public TOaPartstandInfoVo SelectByObject(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = "select * from T_OA_PARTSTAND_INFO " + this.BuildWhereStatement(tOaPartstandInfo);
            return SqlHelper.ExecuteObject(new TOaPartstandInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartstandInfo, TOaPartstandInfoVo.T_OA_PARTSTAND_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartstandInfo, TOaPartstandInfoVo.T_OA_PARTSTAND_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartstandInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartstandInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandInfoVo tOaPartstandInfo_UpdateSet, TOaPartstandInfoVo tOaPartstandInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartstandInfo_UpdateSet, TOaPartstandInfoVo.T_OA_PARTSTAND_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartstandInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PARTSTAND_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartstandInfoVo tOaPartstandInfo)
        {
            string strSQL = "delete from T_OA_PARTSTAND_INFO ";
            strSQL += this.BuildWhereStatement(tOaPartstandInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartstandInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartstandInfoVo tOaPartstandInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartstandInfo)
            {

                //编号
                if (!String.IsNullOrEmpty(tOaPartstandInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartstandInfo.ID.ToString()));
                }
                //物料编码
                if (!String.IsNullOrEmpty(tOaPartstandInfo.SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CODE = '{0}'", tOaPartstandInfo.SAMPLE_CODE.ToString()));
                }
                //物料类别
                if (!String.IsNullOrEmpty(tOaPartstandInfo.SAMPLE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_TYPE = '{0}'", tOaPartstandInfo.SAMPLE_TYPE.ToString()));
                }
                //物料名称
                if (!String.IsNullOrEmpty(tOaPartstandInfo.SAMPLE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_NAME = '{0}'", tOaPartstandInfo.SAMPLE_NAME.ToString()));
                }
                //单位
                if (!String.IsNullOrEmpty(tOaPartstandInfo.UNIT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UNIT = '{0}'", tOaPartstandInfo.UNIT.ToString()));
                }
                //规格型号
                if (!String.IsNullOrEmpty(tOaPartstandInfo.CLASS_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CLASS_TYPE = '{0}'", tOaPartstandInfo.CLASS_TYPE.ToString()));
                }
                //库存量
                if (!String.IsNullOrEmpty(tOaPartstandInfo.INVENTORY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INVENTORY = '{0}'", tOaPartstandInfo.INVENTORY.ToString()));
                }
                //介质/基体
                if (!String.IsNullOrEmpty(tOaPartstandInfo.TOTAL_INVENTORY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TOTAL_INVENTORY = '{0}'", tOaPartstandInfo.TOTAL_INVENTORY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tOaPartstandInfo.SAMPLE_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SOURCE = '{0}'", tOaPartstandInfo.SAMPLE_SOURCE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tOaPartstandInfo.POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POTENCY = '{0}'", tOaPartstandInfo.POTENCY.ToString()));
                }
                //分析纯/化学纯
                if (!String.IsNullOrEmpty(tOaPartstandInfo.BUY_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BUY_DATE = '{0}'", tOaPartstandInfo.BUY_DATE.ToString()));
                }
                //报警值
                if (!String.IsNullOrEmpty(tOaPartstandInfo.EFF_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EFF_DATE = '{0}'", tOaPartstandInfo.EFF_DATE.ToString()));
                }
                //用途
                if (!String.IsNullOrEmpty(tOaPartstandInfo.LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEVEL = '{0}'", tOaPartstandInfo.LEVEL.ToString()));
                }
                //技术要求
                if (!String.IsNullOrEmpty(tOaPartstandInfo.CARER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CARER = '{0}'", tOaPartstandInfo.CARER.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaPartstandInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartstandInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaPartstandInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartstandInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaPartstandInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartstandInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaPartstandInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartstandInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaPartstandInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartstandInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
