using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.PART;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料基础信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = "select Count(*) from T_OA_PART_INFO " + this.BuildWhereStatement(tOaPartInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PART_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaPartInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartInfoVo Details(TOaPartInfoVo tOaPartInfo)
        {
           string strSQL = String.Format("select * from  T_OA_PART_INFO " + this.BuildWhereStatement(tOaPartInfo));
           return SqlHelper.ExecuteObject(new TOaPartInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartInfoVo> SelectByObject(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_PART_INFO " + this.BuildWhereStatement(tOaPartInfo));
            return SqlHelper.ExecuteObjectList(tOaPartInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_PART_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartInfo));
            strSQL += "order by PART_NAME";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        public DataTable GetLineInfo(string PART_NAME,string StartTime,string EndTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select  b.PART_CODE,b.PART_NAME,b.UNIT,b.MODELS,b.INVENTORY,a.need_quantity as Qty,b.ALARM,b.USEING ");
            sb.Append(" from T_OA_PART_ACCEPTED a left join T_OA_PART_INFO b ON B.ID=A.PART_ID and B.IS_DEL='0' ");
            sb.Append(" where b.id <>0 ");
            if (!string.IsNullOrEmpty(PART_NAME))
            {
                sb.Append(" and  b.PART_NAME='" + PART_NAME + "' ");
            }
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append(" and a.recivepart_date between '" + StartTime + "' and '" + EndTime + "' ");
            }
            sb.Append(" union all ");
            sb.Append("  SELECT b.PART_CODE,b.PART_NAME,b.UNIT,b.MODELS,b.INVENTORY,a.used_quantity as Qty,b.ALARM,b.USEING ");
            sb.Append("  FROM T_OA_PART_COLLAR A   JOIN T_OA_PART_INFO B ON B.ID=A.PART_ID and B.IS_DEL='0' ");
            sb.Append(" where b.id <>0 ");
            if (!string.IsNullOrEmpty(PART_NAME))
            {
                sb.Append(" and  b.PART_NAME='" + PART_NAME + "' ");
            }
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append(" and a.lastin_date between '" + StartTime + "' and '" + EndTime + "' ");
            }
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = "select * from T_OA_PART_INFO " + this.BuildWhereStatement(tOaPartInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            string strSQL1 = " select * from T_OA_PART_INFO {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tOaPartInfo));
            string strSQL = "select a.DICT_TEXT,i.* from (" + strSQL1 + ")i";
            strSQL += " join T_SYS_DICT a on a.DICT_CODE=i.PART_TYPE and a.DICT_TYPE='PART_TYPE'";
            if (tOaPartInfo.SORT_FIELD != "")
            {
                strSQL += " order by " + tOaPartInfo.SORT_FIELD + " " + tOaPartInfo.SORT_TYPE;
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <returns></returns>
        public TOaPartInfoVo SelectByObject(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = "select * from T_OA_PART_INFO " + this.BuildWhereStatement(tOaPartInfo);
            return SqlHelper.ExecuteObject(new TOaPartInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartInfo, TOaPartInfoVo.T_OA_PART_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartInfo, TOaPartInfoVo.T_OA_PART_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartInfoVo tOaPartInfo_UpdateSet, TOaPartInfoVo tOaPartInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartInfo_UpdateSet, TOaPartInfoVo.T_OA_PART_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PART_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartInfoVo tOaPartInfo)
        {
            string strSQL = "delete from T_OA_PART_INFO ";
	    strSQL += this.BuildWhereStatement(tOaPartInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartInfoVo tOaPartInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaPartInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartInfo.ID.ToString()));
				}	
				//物料编码
				if (!String.IsNullOrEmpty(tOaPartInfo.PART_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PART_CODE like '%{0}%'", tOaPartInfo.PART_CODE.ToString()));
				}	
				//物料类别
				if (!String.IsNullOrEmpty(tOaPartInfo.PART_TYPE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND PART_TYPE in ({0})", tOaPartInfo.PART_TYPE.ToString()));
				}	
				//物料名称
				if (!String.IsNullOrEmpty(tOaPartInfo.PART_NAME.ToString().Trim()))
				{
                    if (tOaPartInfo.REMARK1.ToString() == "query")//huangjinjun add
                    {
                        strWhereStatement.Append(string.Format(" AND PART_NAME Like '%{0}%'", tOaPartInfo.PART_NAME.ToString()));
                    }
                    else {
                        strWhereStatement.Append(string.Format(" AND PART_NAME = '{0}'", tOaPartInfo.PART_NAME.ToString()));
                    }	
					
				}	
				//单位
				if (!String.IsNullOrEmpty(tOaPartInfo.UNIT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNIT = '{0}'", tOaPartInfo.UNIT.ToString()));
				}	
				//规格型号
				if (!String.IsNullOrEmpty(tOaPartInfo.MODELS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MODELS = '{0}'", tOaPartInfo.MODELS.ToString()));
				}	
				//库存量
				if (!String.IsNullOrEmpty(tOaPartInfo.INVENTORY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INVENTORY = '{0}'", tOaPartInfo.INVENTORY.ToString()));
				}	
				//介质/基体
				if (!String.IsNullOrEmpty(tOaPartInfo.MEDIUM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MEDIUM = '{0}'", tOaPartInfo.MEDIUM.ToString()));
				}	
				//分析纯/化学纯
				if (!String.IsNullOrEmpty(tOaPartInfo.PURE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PURE = '{0}'", tOaPartInfo.PURE.ToString()));
				}	
				//报警值
				if (!String.IsNullOrEmpty(tOaPartInfo.ALARM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ALARM = '{0}'", tOaPartInfo.ALARM.ToString()));
				}	
				//用途
				if (!String.IsNullOrEmpty(tOaPartInfo.USEING.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USEING = '{0}'", tOaPartInfo.USEING.ToString()));
				}	
				//技术要求
				if (!String.IsNullOrEmpty(tOaPartInfo.REQUEST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REQUEST = '{0}'", tOaPartInfo.REQUEST.ToString()));
				}	
				//性质说明
				if (!String.IsNullOrEmpty(tOaPartInfo.NARURE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NARURE = '{0}'", tOaPartInfo.NARURE.ToString()));
				}	
                //删除标记
                if (!string.IsNullOrEmpty(tOaPartInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tOaPartInfo.IS_DEL.ToString()));
                }
				//备注1
                //if (!String.IsNullOrEmpty(tOaPartInfo.REMARK1.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartInfo.REMARK1.ToString()));
                //}	
				//备注2
				if (!String.IsNullOrEmpty(tOaPartInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaPartInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaPartInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaPartInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
