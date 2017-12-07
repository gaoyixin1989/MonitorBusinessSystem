using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.CodeRule;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.CodeRule
{
    /// <summary>
    /// 功能：编号规则
    /// 创建日期：2013-04-22
    /// 创建人：胡方扬
    /// </summary>
    public class TBaseSerialruleAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = "select Count(*) from T_BASE_SERIALRULE " + this.BuildWhereStatement(tBaseSerialrule);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseSerialruleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_SERIALRULE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseSerialruleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseSerialrule">对象条件</param>
        /// <returns>对象</returns>
        public TBaseSerialruleVo Details(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = String.Format("select * from  T_BASE_SERIALRULE " + this.BuildWhereStatement(tBaseSerialrule));
            return SqlHelper.ExecuteObject(new TBaseSerialruleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseSerialruleVo> SelectByObject(TBaseSerialruleVo tBaseSerialrule, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_SERIALRULE " + this.BuildWhereStatement(tBaseSerialrule));
            return SqlHelper.ExecuteObjectList(tBaseSerialrule, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseSerialruleVo tBaseSerialrule, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_SERIALRULE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseSerialrule));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = "select * from T_BASE_SERIALRULE " + this.BuildWhereStatement(tBaseSerialrule);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <returns></returns>
        public TBaseSerialruleVo SelectByObject(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = "select * from T_BASE_SERIALRULE " + this.BuildWhereStatement(tBaseSerialrule);
            return SqlHelper.ExecuteObject(new TBaseSerialruleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseSerialrule, TBaseSerialruleVo.T_BASE_SERIALRULE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseSerialrule">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseSerialrule, TBaseSerialruleVo.T_BASE_SERIALRULE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseSerialrule.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseSerialrule_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseSerialrule_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseSerialruleVo tBaseSerialrule_UpdateSet, TBaseSerialruleVo tBaseSerialrule_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseSerialrule_UpdateSet, TBaseSerialruleVo.T_BASE_SERIALRULE_TABLE);
            strSQL += this.BuildWhereStatement(tBaseSerialrule_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_SERIALRULE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL = "delete from T_BASE_SERIALRULE ";
            strSQL += this.BuildWhereStatement(tBaseSerialrule);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 验证是否跨年度，如果跨年，则把符合条件的（启用了跨年重新编号的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <param name="strNowYear"></param>
        /// <returns></returns>
        public bool UpdateInitStartNumForNewYear(TBaseSerialruleVo tBaseSerialrule,string strNowYear)
        {
            if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_YEAR)&& !String.IsNullOrEmpty(tBaseSerialrule.ID) && !String.IsNullOrEmpty(strNowYear)) {
                if (tBaseSerialrule.SERIAL_YEAR != strNowYear) {
                    string strSQL = String.Format(" UPDATE T_BASE_SERIALRULE SET SERIAL_START_NUM='1', SERIAL_YEAR='{0}' WHERE ID='{1}' AND STATUS='1'", strNowYear,tBaseSerialrule.ID);
                    if (SqlHelper.ExecuteNonQuery(strSQL) > 0) {
                        return true;
                    }
                }
            }
            return false;
        }



        /// <summary>
        /// 验证是否跨年度、日度，如果跨年、日，则把符合条件的（启用了跨年、跨日重新编号的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <param name="strNowYear"></param>
        /// <param name="strNowDay"></param>
        /// <returns></returns>
        public bool UpdateInitStartNumForNewYear(TBaseSerialruleVo tBaseSerialrule, string strNowYear,string strNowDay)
        {
            string strSQL = "";
            ArrayList objVo = new ArrayList();

            if ( !String.IsNullOrEmpty(tBaseSerialrule.ID) && !String.IsNullOrEmpty(strNowYear)&&String.IsNullOrEmpty(strNowDay))
            {
                if (tBaseSerialrule.SERIAL_YEAR != strNowYear)
                {
                    strSQL = String.Format(" UPDATE T_BASE_SERIALRULE SET SERIAL_START_NUM='1', SERIAL_YEAR='{0}' WHERE ID='{1}'", strNowYear, tBaseSerialrule.ID);
                    objVo.Add(strSQL);
                }
            }
            if (!String.IsNullOrEmpty(tBaseSerialrule.ID) && String.IsNullOrEmpty(strNowYear)&&!String.IsNullOrEmpty(strNowDay))
            {
                if (tBaseSerialrule.SERIAL_DAY != strNowDay)
                {
                    strSQL = String.Format(" UPDATE T_BASE_SERIALRULE SET SERIAL_START_NUM='1', SERIAL_DAY='{0}' WHERE ID='{1}'", strNowDay,tBaseSerialrule.ID);
                    objVo.Add(strSQL);
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        /// <summary>
        /// 如果编号被使用则，立刻更新初始化编号  胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <returns></returns>
        public bool UpdateSerialNum(TBaseSerialruleVo tBaseSerialrule)
        {
            string strSQL =String.Format(" UPDATE T_BASE_SERIALRULE SET SERIAL_START_NUM=SERIAL_START_NUM+1 WHERE ID='{0}'",tBaseSerialrule.ID);

            return SqlHelper.ExecuteNonQuery(strSQL)>0?true:false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseSerialruleVo tBaseSerialrule)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseSerialrule)
            {

                //
                if (!String.IsNullOrEmpty(tBaseSerialrule.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseSerialrule.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_NAME = '{0}'", tBaseSerialrule.SERIAL_NAME.ToString()));
                }
                //编码规则
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_RULE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_RULE = '{0}'", tBaseSerialrule.SERIAL_RULE.ToString()));
                }
                //类型
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_TYPE = '{0}'", tBaseSerialrule.SERIAL_TYPE.ToString()));
                }
                //编码位数
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_NUMBER_BIT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_NUMBER_BIT = '{0}'", tBaseSerialrule.SERIAL_NUMBER_BIT.ToString()));
                }
                //使用该编码的监测类别
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_TYPE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_TYPE_ID = '{0}'", tBaseSerialrule.SERIAL_TYPE_ID.ToString()));
                }
                //样品来源,1为抽样，2为自送样
                if (!String.IsNullOrEmpty(tBaseSerialrule.SAMPLE_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SOURCE = '{0}'", tBaseSerialrule.SAMPLE_SOURCE.ToString()));
                }
                // 初始编号
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_START_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_START_NUM = '{0}'", tBaseSerialrule.SERIAL_START_NUM.ToString()));
                }
                //最大编号
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_MAX_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_MAX_NUM = '{0}'", tBaseSerialrule.SERIAL_MAX_NUM.ToString()));
                }
                //是否启用年度重新编号
                if (!String.IsNullOrEmpty(tBaseSerialrule.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tBaseSerialrule.STATUS.ToString()));
                }

                //当前创建年度
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_YEAR = '{0}'", tBaseSerialrule.SERIAL_YEAR.ToString()));
                }
                //是否联合编号
                if (!String.IsNullOrEmpty(tBaseSerialrule.IS_UNION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_UNION = '{0}'", tBaseSerialrule.IS_UNION.ToString()));
                }
                //样品联合编号的组合编号ID
                if (!String.IsNullOrEmpty(tBaseSerialrule.UNION_SEARIAL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UNION_SEARIAL_ID = '{0}'", tBaseSerialrule.UNION_SEARIAL_ID.ToString()));
                }
                //辅助规则缺省值
                if (!String.IsNullOrEmpty(tBaseSerialrule.UNION_DEFAULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UNION_DEFAULT = '{0}'", tBaseSerialrule.UNION_DEFAULT.ToString()));
                }
                //跨天是否从新编号
                if (!String.IsNullOrEmpty(tBaseSerialrule.DAY_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY_STATUS = '{0}'", tBaseSerialrule.DAY_STATUS.ToString()));
                }
                //编号当天日期
                if (!String.IsNullOrEmpty(tBaseSerialrule.SERIAL_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_DAY = '{0}'", tBaseSerialrule.SERIAL_DAY.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
