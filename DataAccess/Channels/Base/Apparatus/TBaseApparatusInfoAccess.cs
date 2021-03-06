using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strApparatusName">仪器名</param>
        /// <param name="strApparatusCode">仪器编号</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSelectApparatus_inItem(string strApparatusName, string strApparatusCode, int iIndex, int iCount)
        {

            string strSQL = " select ID,APPARATUS_CODE,[NAME],MODEL from T_BASE_APPARATUS_INFO ";
            strSQL += " where IS_DEL='0' ";
            if (strApparatusName.Length > 0)
                strSQL += " and [NAME] like '%" + strApparatusName + "%'";
            if (strApparatusCode.Length > 0)
                strSQL += " and APPARATUS_CODE like '%" + strApparatusCode + "%'";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="strApparatusName">仪器名</param>
        /// <param name="strApparatusCode">仪器编号</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSelectApparatus_inItem(string strApparatusName, string strApparatusCode)
        {
            string strSQL = " select ID,APPARATUS_CODE,[NAME],MODEL from T_BASE_APPARATUS_INFO ";
            strSQL += " where IS_DEL='0' ";
            if (strApparatusName.Length > 0)
                strSQL += " and [NAME] like '%" + strApparatusName + "%'";
            if (strApparatusCode.Length > 0)
                strSQL += " and APPARATUS_CODE like '%" + strApparatusCode + "%'";

            strSQL = "select Count(*) from (" + strSQL + ")t";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_INFO " + this.BuildWhereStatement(tBaseApparatusInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseApparatusInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusInfoVo Details(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_INFO " + this.BuildWhereStatement(tBaseApparatusInfo));
            return SqlHelper.ExecuteObject(new TBaseApparatusInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusInfoVo> SelectByObject(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_APPARATUS_INFO " + this.BuildWhereStatement(tBaseApparatusInfo));
            return SqlHelper.ExecuteObjectList(tBaseApparatusInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_APPARATUS_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tBaseApparatusInfo, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = "select * from T_BASE_APPARATUS_INFO " + this.BuildWhereStatement(tBaseApparatusInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns></returns>
        public TBaseApparatusInfoVo SelectByObject(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = "select * from T_BASE_APPARATUS_INFO " + this.BuildWhereStatement(tBaseApparatusInfo);
            return SqlHelper.ExecuteObject(new TBaseApparatusInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseApparatusInfo, TBaseApparatusInfoVo.T_BASE_APPARATUS_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusInfo, TBaseApparatusInfoVo.T_BASE_APPARATUS_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseApparatusInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseApparatusInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusInfoVo tBaseApparatusInfo_UpdateSet, TBaseApparatusInfoVo tBaseApparatusInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusInfo_UpdateSet, TBaseApparatusInfoVo.T_BASE_APPARATUS_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseApparatusInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_APPARATUS_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            string strSQL = "delete from T_BASE_APPARATUS_INFO ";
            strSQL += this.BuildWhereStatement(tBaseApparatusInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_APPARATUS_INFO WHERE IS_DEL='{0}'", tBaseApparatusInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_CODE))
            {
                strSQL += String.Format("  AND APPARATUS_CODE LIKE '%{0}%'", tBaseApparatusInfo.APPARATUS_CODE);
            }
            if (!String.IsNullOrEmpty(tBaseApparatusInfo.NAME))
            {
                strSQL += String.Format("  AND NAME LIKE '%{0}%'", tBaseApparatusInfo.NAME);
            }

            if (!String.IsNullOrEmpty(tBaseApparatusInfo.MODEL))
            {
                strSQL += String.Format("   AND MODEL LIKE '%{0}%'", tBaseApparatusInfo.MODEL);
            }

            if (!String.IsNullOrEmpty(tBaseApparatusInfo.FITTINGS_PROVIDER))
            {
                strSQL += String.Format("   AND FITTINGS_PROVIDER LIKE '%{0}%'", tBaseApparatusInfo.FITTINGS_PROVIDER);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tBaseApparatusInfo, strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseApparatusInfoVo tBaseApparatusInfo)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_APPARATUS_INFO WHERE IS_DEL='{0}'", tBaseApparatusInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_CODE))
            {
                strSQL += String.Format("  AND APPARATUS_CODE LIKE '%{0}%'", tBaseApparatusInfo.APPARATUS_CODE);
            }
            if (!String.IsNullOrEmpty(tBaseApparatusInfo.NAME))
            {
                strSQL += String.Format("  AND NAME LIKE '%{0}%'", tBaseApparatusInfo.NAME);
            }

            if (!String.IsNullOrEmpty(tBaseApparatusInfo.MODEL))
            {
                strSQL += String.Format("   AND MODEL LIKE '%{0}%'", tBaseApparatusInfo.MODEL);
            }

            if (!String.IsNullOrEmpty(tBaseApparatusInfo.FITTINGS_PROVIDER))
            {
                strSQL += String.Format("   AND FITTINGS_PROVIDER LIKE '%{0}%'", tBaseApparatusInfo.FITTINGS_PROVIDER);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// 批量增加
        /// </summary>
        /// <param name="arrVo"></param>
        /// <returns></returns>
        public bool SaveData(ArrayList arrVo)
        {
            string strSQL = "";
            ArrayList arrSql = new ArrayList();
            foreach (TBaseApparatusInfoVo objvo in arrVo)
            {
                objvo.ID = GetSerialNumber("Apparatus_Id");
                strSQL = SqlHelper.BuildInsertExpress(objvo, TBaseApparatusInfoVo.T_BASE_APPARATUS_INFO_TABLE);
                arrSql.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }


        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <param name="intSrcType">1，全部；2，仅检定/校准时间；3，仅期间核查时间；</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount, int intIsMonth, int intSrcType)
        {
            string strStartTime = "";
            string strEndTime = "";
            GetEndTime(intIsMonth, ref strStartTime, ref strEndTime);

            string strSQL = " select *";
            strSQL += ",case  when END_TIME < '{1}' then '1' else '0' end as is_END_TIME";
            strSQL += ",case  when VERIFICATION_END_TIME < '{1}' then '1' else '0' end as is_VERIFICATION_END_TIME";
            strSQL += " from T_BASE_APPARATUS_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatementEx(tBaseApparatusInfo, intIsMonth, intSrcType), strEndTime);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable Create By weilin 2013-07-24
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="intIsMonth">1，按月，本月要报废；2，按季度，本季度要报废；3，提前一个月要报废；</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount, int intIsMonth)
        {
            string strStartTime = "";
            string strEndTime = "";
            GetEndTime(intIsMonth, ref strStartTime, ref strEndTime);

            string strSQL = " select *";
            strSQL += ",case  when SCRAP_TIME < '{1}' then '1' else '0' end as is_SCRAP_TIME";
            strSQL += " from T_BASE_APPARATUS_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusInfo) + " and SCRAP_TIME < '" + strEndTime + "'", strEndTime);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <param name="intSrcType">1，全部；2，仅检定/校准时间；3，仅期间核查时间；</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusInfoVo tBaseApparatusInfo, int intIsMonth, int intSrcType)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_INFO " + this.BuildWhereStatementEx(tBaseApparatusInfo, intIsMonth, intSrcType);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusInfo.ID.ToString()));
                }
                //仪器编号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_CODE = '{0}'", tBaseApparatusInfo.APPARATUS_CODE.ToString()));
                }
                //档案类别
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ARCHIVES_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ARCHIVES_TYPE = '{0}'", tBaseApparatusInfo.ARCHIVES_TYPE.ToString()));
                }
                //类别1(辅助，非辅助)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SORT1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SORT1 = '{0}'", tBaseApparatusInfo.SORT1.ToString()));
                }
                //类别2(l流量计，离子计,汽车尾气.......)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SORT2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SORT2 = '{0}'", tBaseApparatusInfo.SORT2.ToString()));
                }
                //所属仪器或者项目
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.BELONG_TO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BELONG_TO = '{0}'", tBaseApparatusInfo.BELONG_TO.ToString()));
                }
                //仪器名称
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NAME = '{0}'", tBaseApparatusInfo.NAME.ToString()));
                }
                //规格型号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.MODEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODEL = '{0}'", tBaseApparatusInfo.MODEL.ToString()));
                }
                //出厂编号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SERIAL_NO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_NO = '{0}'", tBaseApparatusInfo.SERIAL_NO.ToString()));
                }
                //仪器供应商
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_PROVIDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_PROVIDER = '{0}'", tBaseApparatusInfo.APPARATUS_PROVIDER.ToString()));
                }
                //配件供应商
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.FITTINGS_PROVIDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITTINGS_PROVIDER = '{0}'", tBaseApparatusInfo.FITTINGS_PROVIDER.ToString()));
                }
                //仪器供应商网址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.WEB_SITE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEB_SITE = '{0}'", tBaseApparatusInfo.WEB_SITE.ToString()));
                }
                //仪器性能(合格,一级合格,正常)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.CAPABILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CAPABILITY = '{0}'", tBaseApparatusInfo.CAPABILITY.ToString()));
                }
                //联系人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.LINK_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LINK_MAN = '{0}'", tBaseApparatusInfo.LINK_MAN.ToString()));
                }
                //联系电话
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.LINK_PHONE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LINK_PHONE = '{0}'", tBaseApparatusInfo.LINK_PHONE.ToString()));
                }
                //邮编
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.POST.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST = '{0}'", tBaseApparatusInfo.POST.ToString()));
                }
                //联系地址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tBaseApparatusInfo.ADDRESS.ToString()));
                }
                //量值溯源方式(校准、自校、检定)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.CERTIFICATE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CERTIFICATE_TYPE = '{0}'", tBaseApparatusInfo.CERTIFICATE_TYPE.ToString()));
                }
                //溯源结果(合格，不合格)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.TRACE_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRACE_RESULT = '{0}'", tBaseApparatusInfo.TRACE_RESULT.ToString()));
                }
                //检定方式(不检，来检,送检,暂不能检，不详)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.TEST_MODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_MODE = '{0}'", tBaseApparatusInfo.TEST_MODE.ToString()));
                }
                //校正周期
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFY_CYCLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFY_CYCLE = '{0}'", tBaseApparatusInfo.VERIFY_CYCLE.ToString()));
                }
                //使用科室
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPT = '{0}'", tBaseApparatusInfo.DEPT.ToString()));
                }
                //保管人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.KEEPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KEEPER = '{0}'", tBaseApparatusInfo.KEEPER.ToString()));
                }
                //放置地点
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.POSITION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tBaseApparatusInfo.POSITION.ToString()));
                }
                //使用状况(在用，未用)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tBaseApparatusInfo.STATUS.ToString()));
                }
                //档案上传地址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ARCHIVES_ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ARCHIVES_ADDRESS = '{0}'", tBaseApparatusInfo.ARCHIVES_ADDRESS.ToString()));
                }
                //最近检定/校准时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.BEGIN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_TIME = '{0}'", tBaseApparatusInfo.BEGIN_TIME.ToString()));
                }
                //到期检定/校准时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.END_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_TIME = '{0}'", tBaseApparatusInfo.END_TIME.ToString()));
                }
                //扩展不确定度
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.EXPANDED_UNCETAINTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXPANDED_UNCETAINTY = '{0}'", tBaseApparatusInfo.EXPANDED_UNCETAINTY.ToString()));
                }
                //测量范围
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.MEASURING_RANGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURING_RANGE = '{0}'", tBaseApparatusInfo.MEASURING_RANGE.ToString()));
                }
                //检定单位
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.EXAMINE_DEPARTMENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXAMINE_DEPARTMENT = '{0}'", tBaseApparatusInfo.EXAMINE_DEPARTMENT.ToString()));
                }
                //检定单位电话
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPARTMENT_PHONE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPARTMENT_PHONE = '{0}'", tBaseApparatusInfo.DEPARTMENT_PHONE.ToString()));
                }
                //检定单位联系人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPARTMENT_LINKMAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPARTMENT_LINKMAN = '{0}'", tBaseApparatusInfo.DEPARTMENT_LINKMAN.ToString()));
                }
                //期间核查方式
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_WAY = '{0}'", tBaseApparatusInfo.VERIFICATION_WAY.ToString()));
                }
                //期间核查结果
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_RESULT = '{0}'", tBaseApparatusInfo.VERIFICATION_RESULT.ToString()));
                }
                //最近期间核查时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_BEGIN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_BEGIN_TIME = '{0}'", tBaseApparatusInfo.VERIFICATION_BEGIN_TIME.ToString()));
                }
                //最近期间核查时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_END_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_END_TIME = '{0}'", tBaseApparatusInfo.VERIFICATION_END_TIME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseApparatusInfo.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusInfo"></param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <param name="intSrcType">1，全部；2，仅检定/校准时间；3，仅期间核查时间；</param>
        /// <returns></returns>
        public string BuildWhereStatementEx(TBaseApparatusInfoVo tBaseApparatusInfo,int intIsMonth,int intSrcType)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusInfo)
            {
                #region other srh condiction
                //ID
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusInfo.ID.ToString()));
                }
                //仪器编号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_CODE = '{0}'", tBaseApparatusInfo.APPARATUS_CODE.ToString()));
                }
                //档案类别
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ARCHIVES_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ARCHIVES_TYPE = '{0}'", tBaseApparatusInfo.ARCHIVES_TYPE.ToString()));
                }
                //类别1(辅助，非辅助)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SORT1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SORT1 = '{0}'", tBaseApparatusInfo.SORT1.ToString()));
                }
                //类别2(l流量计，离子计,汽车尾气.......)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SORT2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SORT2 = '{0}'", tBaseApparatusInfo.SORT2.ToString()));
                }
                //所属仪器或者项目
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.BELONG_TO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BELONG_TO = '{0}'", tBaseApparatusInfo.BELONG_TO.ToString()));
                }
                //仪器名称
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NAME = '{0}'", tBaseApparatusInfo.NAME.ToString()));
                }
                //规格型号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.MODEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODEL = '{0}'", tBaseApparatusInfo.MODEL.ToString()));
                }
                //出厂编号
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.SERIAL_NO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SERIAL_NO = '{0}'", tBaseApparatusInfo.SERIAL_NO.ToString()));
                }
                //仪器供应商
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.APPARATUS_PROVIDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPARATUS_PROVIDER = '{0}'", tBaseApparatusInfo.APPARATUS_PROVIDER.ToString()));
                }
                //配件供应商
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.FITTINGS_PROVIDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITTINGS_PROVIDER = '{0}'", tBaseApparatusInfo.FITTINGS_PROVIDER.ToString()));
                }
                //仪器供应商网址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.WEB_SITE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEB_SITE = '{0}'", tBaseApparatusInfo.WEB_SITE.ToString()));
                }
                //仪器性能(合格,一级合格,正常)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.CAPABILITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CAPABILITY = '{0}'", tBaseApparatusInfo.CAPABILITY.ToString()));
                }
                //联系人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.LINK_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LINK_MAN = '{0}'", tBaseApparatusInfo.LINK_MAN.ToString()));
                }
                //联系电话
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.LINK_PHONE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LINK_PHONE = '{0}'", tBaseApparatusInfo.LINK_PHONE.ToString()));
                }
                //邮编
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.POST.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POST = '{0}'", tBaseApparatusInfo.POST.ToString()));
                }
                //联系地址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tBaseApparatusInfo.ADDRESS.ToString()));
                }
                //量值溯源方式(校准、自校、检定)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.CERTIFICATE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CERTIFICATE_TYPE = '{0}'", tBaseApparatusInfo.CERTIFICATE_TYPE.ToString()));
                }
                //溯源结果(合格，不合格)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.TRACE_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRACE_RESULT = '{0}'", tBaseApparatusInfo.TRACE_RESULT.ToString()));
                }
                //检定方式(不检，来检,送检,暂不能检，不详)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.TEST_MODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_MODE = '{0}'", tBaseApparatusInfo.TEST_MODE.ToString()));
                }
                //校正周期
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFY_CYCLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFY_CYCLE = '{0}'", tBaseApparatusInfo.VERIFY_CYCLE.ToString()));
                }
                //使用科室
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPT = '{0}'", tBaseApparatusInfo.DEPT.ToString()));
                }
                //保管人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.KEEPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KEEPER = '{0}'", tBaseApparatusInfo.KEEPER.ToString()));
                }
                //放置地点
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.POSITION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tBaseApparatusInfo.POSITION.ToString()));
                }
                //使用状况(在用，未用)
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tBaseApparatusInfo.STATUS.ToString()));
                }
                //档案上传地址
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.ARCHIVES_ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ARCHIVES_ADDRESS = '{0}'", tBaseApparatusInfo.ARCHIVES_ADDRESS.ToString()));
                }
                //最近检定/校准时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.BEGIN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_TIME = '{0}'", tBaseApparatusInfo.BEGIN_TIME.ToString()));
                }
                
                //扩展不确定度
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.EXPANDED_UNCETAINTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXPANDED_UNCETAINTY = '{0}'", tBaseApparatusInfo.EXPANDED_UNCETAINTY.ToString()));
                }
                //测量范围
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.MEASURING_RANGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURING_RANGE = '{0}'", tBaseApparatusInfo.MEASURING_RANGE.ToString()));
                }
                //检定单位
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.EXAMINE_DEPARTMENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXAMINE_DEPARTMENT = '{0}'", tBaseApparatusInfo.EXAMINE_DEPARTMENT.ToString()));
                }
                //检定单位电话
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPARTMENT_PHONE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPARTMENT_PHONE = '{0}'", tBaseApparatusInfo.DEPARTMENT_PHONE.ToString()));
                }
                //检定单位联系人
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.DEPARTMENT_LINKMAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPARTMENT_LINKMAN = '{0}'", tBaseApparatusInfo.DEPARTMENT_LINKMAN.ToString()));
                }
                //期间核查方式
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_WAY = '{0}'", tBaseApparatusInfo.VERIFICATION_WAY.ToString()));
                }
                //期间核查结果
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_RESULT = '{0}'", tBaseApparatusInfo.VERIFICATION_RESULT.ToString()));
                }
                //最近期间核查时间
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.VERIFICATION_BEGIN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_BEGIN_TIME = '{0}'", tBaseApparatusInfo.VERIFICATION_BEGIN_TIME.ToString()));
                }
                
                //备注1
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseApparatusInfo.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseApparatusInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusInfo.REMARK5.ToString()));
                }
                #endregion

                string strStartTime = "";
                string strEndTime = "";
                GetEndTime(intIsMonth, ref strStartTime, ref strEndTime);

                if (intSrcType == 1)
                {
                    strWhereStatement.Append(string.Format(" AND (END_TIME < '{0}'", strEndTime));
                    strWhereStatement.Append(string.Format(" OR VERIFICATION_END_TIME < '{0}')", strEndTime));
                }
                if (intSrcType == 2)
                {
                    strWhereStatement.Append(string.Format(" AND END_TIME < '{0}'", strEndTime));
                }
                if (intSrcType == 3)
                {
                    strWhereStatement.Append(string.Format(" AND VERIFICATION_END_TIME < '{0}'", strEndTime));
                }
            }
            return strWhereStatement.ToString();
        }

        private void GetEndTime(int intIsMonth,ref string strStartTime, ref string strEndTime)
        {
            DateTime dtNow = System.DateTime.Now;
            if (intIsMonth == 1)
            {
                DateTime dtEnd = dtNow.AddMonths(1);
                strStartTime = dtNow.Year.ToString() + "-" + dtNow.Month.ToString().PadLeft(2, '0') + "-01";
                strEndTime = dtEnd.Year.ToString() + "-" + dtEnd.Month.ToString().PadLeft(2, '0') + "-01";

            }
            if (intIsMonth == 2)
            {
                string strNowMonth = dtNow.Month.ToString().PadLeft(2, '0');
                if ("01,02,03".Contains(strNowMonth))
                {
                    strStartTime = dtNow.Year.ToString() + "-01-01";
                    strEndTime = dtNow.Year.ToString() + "-04-01";
                }
                if ("04,05,06".Contains(strNowMonth))
                {
                    strStartTime = dtNow.Year.ToString() + "-04-01";
                    strEndTime = dtNow.Year.ToString() + "-07-01";
                }
                if ("07,08,09".Contains(strNowMonth))
                {
                    strStartTime = dtNow.Year.ToString() + "-07-01";
                    strEndTime = dtNow.Year.ToString() + "-10-01";
                }
                if ("10,11,12".Contains(strNowMonth))
                {
                    strStartTime = dtNow.Year.ToString() + "-10-01";
                    strEndTime = dtNow.AddYears(1).Year.ToString() + "-01-01";
                }
            }
            if (intIsMonth == 3)
            {
                DateTime dtEnd = dtNow.AddMonths(1);
                strStartTime = dtNow.Year.ToString() + "-" + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0');
                strEndTime = dtEnd.Year.ToString() + "-" + dtEnd.Month.ToString().PadLeft(2, '0') + dtEnd.Day.ToString().PadLeft(2, '0');
            }
        }
        #endregion
    }
}
