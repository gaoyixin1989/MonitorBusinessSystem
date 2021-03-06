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
    /// 功能：序列号管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysSerialAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysSerialVo tSysSerial)
        {
            string strSQL = "select Count(*) from T_SYS_SERIAL " + this.BuildWhereStatement(tSysSerial);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysSerialVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_SERIAL  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysSerialVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysSerial">对象条件</param>
        /// <returns>对象</returns>
        public TSysSerialVo Details(TSysSerialVo tSysSerial)
        {
           string strSQL =  "select * from  T_SYS_SERIAL " + this.BuildWhereStatement(tSysSerial);
           return SqlHelper.ExecuteObject(new TSysSerialVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysSerialVo> SelectByObject(TSysSerialVo tSysSerial, int iIndex, int iCount)
        {
            string strSQL = " select t.* from (select * from T_SYS_SERIAL) t {0} order by id desc";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysSerial));
            return SqlHelper.ExecuteObjectList(tSysSerial, BuildPagerExpress(strSQL, iIndex, iCount));
            //string strSQL = String.Format("select * from  T_SYS_SERIAL " + this.BuildWhereStatement(tSysSerial));
            //return SqlHelper.ExecuteObjectList(tSysSerial, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysSerialVo tSysSerial, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_SERIAL t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysSerial));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysSerial"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysSerialVo tSysSerial)
        {
            string strSQL = "select * from T_SYS_SERIAL " + this.BuildWhereStatement(tSysSerial);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <returns></returns>
        public TSysSerialVo SelectByObject(TSysSerialVo tSysSerial)
        {
            string strSQL = "select * from T_SYS_SERIAL " + this.BuildWhereStatement(tSysSerial);
            return SqlHelper.ExecuteObject(new TSysSerialVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysSerialVo tSysSerial)
        {
            tSysSerial.ID = new Resource.TSysSerialAccess().GetSerialNumber(ObjectBase.SerialType.SerialId);
           // tSysSerial.ID = "11";
            string strSQL = SqlHelper.BuildInsertExpress(tSysSerial, TSysSerialVo.T_SYS_SERIAL_TABLE, TSysSerialVo.CREATE_TIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysSerial">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysSerialVo tSysSerial)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysSerial, TSysSerialVo.T_SYS_SERIAL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysSerial.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_SERIAL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysSerial"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysSerialVo tSysSerial)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysSerial)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysSerial.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysSerial.ID.ToString()));
				}	
				//编码
				if (!String.IsNullOrEmpty(tSysSerial.SERIAL_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERIAL_CODE = '{0}'", tSysSerial.SERIAL_CODE.ToString()));
				}	
				//名称
				if (!String.IsNullOrEmpty(tSysSerial.SERIAL_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERIAL_NAME like '%{0}%'", tSysSerial.SERIAL_NAME.ToString()));
				}	
				//分组
				if (!String.IsNullOrEmpty(tSysSerial.SERIAL_GROUP.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERIAL_GROUP = '{0}'", tSysSerial.SERIAL_GROUP.ToString()));
				}	
				//序列号
				if (!String.IsNullOrEmpty(tSysSerial.SERIAL_NUMBER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERIAL_NUMBER = '{0}'", tSysSerial.SERIAL_NUMBER.ToString()));
				}	
				//位数
				if (!String.IsNullOrEmpty(tSysSerial.LENGTH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LENGTH = '{0}'", tSysSerial.LENGTH.ToString()));
				}	
				//前缀
				if (!String.IsNullOrEmpty(tSysSerial.PREFIX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PREFIX = '{0}'", tSysSerial.PREFIX.ToString()));
				}	
				//粒度
				if (!String.IsNullOrEmpty(tSysSerial.GRANULARITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND GRANULARITY = '{0}'", tSysSerial.GRANULARITY.ToString()));
				}	
				//最小值
				if (!String.IsNullOrEmpty(tSysSerial.MIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MIN = '{0}'", tSysSerial.MIN.ToString()));
				}	
				//最大值
				if (!String.IsNullOrEmpty(tSysSerial.MAX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MAX = '{0}'", tSysSerial.MAX.ToString()));
				}	
				//创建人
				if (!String.IsNullOrEmpty(tSysSerial.CREATE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysSerial.CREATE_ID.ToString()));
				}	
				//创建日期
				if (!String.IsNullOrEmpty(tSysSerial.CREATE_TIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysSerial.CREATE_TIME.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysSerial.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysSerial.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysSerial.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysSerial.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysSerial.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysSerial.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysSerial.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysSerial.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
