using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.Resource;
using i3.ValueObject;

namespace i3.DataAccess.Sys.Resource
{
    /// <summary>
    /// 功能：系统配置管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysConfigAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysConfigVo tSysConfig)
        {
            string strSQL = "select Count(*) from T_SYS_CONFIG " + this.BuildWhereStatement(tSysConfig);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysConfigVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_CONFIG  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysConfigVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysConfig">对象条件</param>
        /// <returns>对象</returns>
        public TSysConfigVo Details(TSysConfigVo tSysConfig)
        {
            string strSQL = "select * from  T_SYS_CONFIG " + this.BuildWhereStatement(tSysConfig);
            return SqlHelper.ExecuteObject(new TSysConfigVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysConfigVo> SelectByObject(TSysConfigVo tSysConfig, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.* from (select * from T_SYS_CONFIG) t " + this.BuildWhereStatement(tSysConfig)) + " order by id desc";
            return SqlHelper.ExecuteObjectList(tSysConfig, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysConfigVo tSysConfig, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_CONFIG t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysConfig));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysConfig"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysConfigVo tSysConfig)
        {
            string strSQL = "select * from T_SYS_CONFIG " + this.BuildWhereStatement(tSysConfig);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <returns></returns>
        public TSysConfigVo SelectByObject(TSysConfigVo tSysConfig)
        {
            string strSQL = "select * from T_SYS_CONFIG " + this.BuildWhereStatement(tSysConfig);
            return SqlHelper.ExecuteObject(new TSysConfigVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysConfigVo tSysConfig)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysConfig, TSysConfigVo.T_SYS_CONFIG_TABLE, TSysConfigVo.CREATE_TIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 常规过滤配置
        /// </summary>
        /// <param name="tSysConfig">配置对象</param>
        /// <param name="strUpLimit">上限</param>
        /// <param name="strDownLimit">下限</param>
        /// <returns></returns>
        public bool CreateConfig(TSysConfigVo tSysConfig, TSysConfigVo strUpLimit, TSysConfigVo strDownLimit)
        {
            ArrayList arr = new ArrayList() { };
            //常规过滤
            tSysConfig.ID = GetSerialNumber("config_id");
            string strFilterSQL = SqlHelper.BuildInsertExpress(tSysConfig, TSysConfigVo.T_SYS_CONFIG_TABLE, TSysConfigVo.CREATE_TIME_FIELD);
            arr.Add(strFilterSQL);
            //常规过滤上限
            strUpLimit.ID = GetSerialNumber("config_id");
            string strFilterUp = SqlHelper.BuildInsertExpress(strUpLimit, TSysConfigVo.T_SYS_CONFIG_TABLE);
            arr.Add(strFilterUp);
            //常规过滤下限
            strDownLimit.ID = GetSerialNumber("config_id");
            string strFilterDown = SqlHelper.BuildInsertExpress(strDownLimit, TSysConfigVo.T_SYS_CONFIG_TABLE);
            arr.Add(strFilterDown);

            return ExecuteSQLByTransaction(arr);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfig">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigVo tSysConfig)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysConfig, TSysConfigVo.T_SYS_CONFIG_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysConfig.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 常规过滤配置
        /// </summary>
        /// <param name="strUpLimit">上限</param>
        /// <param name="strDownLimit">下限</param>
        /// <returns></returns>
        public bool EditConfig(TSysConfigVo strUpLimit, TSysConfigVo strDownLimit)
        {
            ArrayList arr = new ArrayList() { };
            string strUpLimitSQL = SqlHelper.BuildUpdateExpress(strUpLimit, TSysConfigVo.T_SYS_CONFIG_TABLE);
            strUpLimitSQL += string.Format(" where ID='{0}' ", strUpLimit.ID);
            arr.Add(strUpLimitSQL);

            string strDownLimitSQL = SqlHelper.BuildUpdateExpress(strDownLimit, TSysConfigVo.T_SYS_CONFIG_TABLE);
            strDownLimitSQL += string.Format(" where ID='{0}' ", strDownLimit.ID);
            arr.Add(strDownLimitSQL);
            return ExecuteSQLByTransaction(arr);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_CONFIG where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysConfig"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysConfigVo tSysConfig)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysConfig)
            {

                //编号
                if (!String.IsNullOrEmpty(tSysConfig.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysConfig.ID.ToString()));
                }
                //配置名
                if (!String.IsNullOrEmpty(tSysConfig.CONFIG_TEXT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONFIG_TEXT like '%{0}%'", tSysConfig.CONFIG_TEXT.ToString()));
                }
                //配置编码
                if (!String.IsNullOrEmpty(tSysConfig.CONFIG_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONFIG_CODE = '{0}'", tSysConfig.CONFIG_CODE.ToString()));
                }
                //配置值
                if (!String.IsNullOrEmpty(tSysConfig.CONFIG_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONFIG_VALUE = '{0}'", tSysConfig.CONFIG_VALUE.ToString()));
                }
                //配置类型
                if (!String.IsNullOrEmpty(tSysConfig.CONFIG_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONFIG_TYPE = '{0}'", tSysConfig.CONFIG_TYPE.ToString()));
                }
                //创建人ID
                if (!String.IsNullOrEmpty(tSysConfig.CREATE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysConfig.CREATE_ID.ToString()));
                }
                //创建时间
                if (!String.IsNullOrEmpty(tSysConfig.CREATE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysConfig.CREATE_TIME.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tSysConfig.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysConfig.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysConfig.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysConfig.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysConfig.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysConfig.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysConfig.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysConfig.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
