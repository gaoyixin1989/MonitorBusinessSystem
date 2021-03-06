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
    /// 功能：委托书监测点项目明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointitemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_POINTITEM " + this.BuildWhereStatement(tMisContractPointitem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointitemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINTITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractPointitemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPointitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointitemVo Details(TMisContractPointitemVo tMisContractPointitem)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINTITEM " + this.BuildWhereStatement(tMisContractPointitem));
           return SqlHelper.ExecuteObject(new TMisContractPointitemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPointitemVo> SelectByObject(TMisContractPointitemVo tMisContractPointitem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINTITEM " + this.BuildWhereStatement(tMisContractPointitem));
            return SqlHelper.ExecuteObjectList(tMisContractPointitem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPointitemVo tMisContractPointitem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_POINTITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPointitem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINTITEM " + this.BuildWhereStatement(tMisContractPointitem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <returns></returns>
        public TMisContractPointitemVo SelectByObject(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINTITEM " + this.BuildWhereStatement(tMisContractPointitem);
            return SqlHelper.ExecuteObject(new TMisContractPointitemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPointitem, TMisContractPointitemVo.T_MIS_CONTRACT_POINTITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPointitem, TMisContractPointitemVo.T_MIS_CONTRACT_POINTITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPointitem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointitem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractPointitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointitemVo tMisContractPointitem_UpdateSet, TMisContractPointitemVo tMisContractPointitem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPointitem_UpdateSet, TMisContractPointitemVo.T_MIS_CONTRACT_POINTITEM_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPointitem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_POINTITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPointitemVo tMisContractPointitem)
        {
            string strSQL = "delete from T_MIS_CONTRACT_POINTITEM ";
	    strSQL += this.BuildWhereStatement(tMisContractPointitem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 删除委托书监测点位的监测项目 Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveItems(TMisContractPointitemVo tMisContractPointitem,string[] strMovePointItems)
        {
            ArrayList arrVo = new ArrayList();

            foreach (string strItemId in strMovePointItems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strSQL = String.Format("DELETE FROM T_MIS_CONTRACT_POINTITEM WHERE CONTRACT_POINT_ID='{0}' AND ITEM_ID = '{1}'", tMisContractPointitem.CONTRACT_POINT_ID, strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 向委托书监测项目中插入保存了且不存在的数据  Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddPointItems"></param>
        /// <returns></returns>
        public bool EditItems(TMisContractPointitemVo tMisContractPointitem, string[] strAddPointItems)
        {
            ArrayList arrVo = new ArrayList();

            foreach (string strItemId in strAddPointItems)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    tMisContractPointitem.ID = GetSerialNumber("t_mis_contract_pointItemId");
                    string strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_POINTITEM(ID,CONTRACT_POINT_ID,ITEM_ID) VALUES('{0}','{1}','{2}')",tMisContractPointitem.ID, tMisContractPointitem.CONTRACT_POINT_ID, strItemId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取点位排口下所有的监测项目信息
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable GetItemsForPoint(TMisContractPointitemVo tMisContractPointitem){
            string strSQL = String.Format(@"SELECT A.ID, A.CONTRACT_POINT_ID,B.ITEM_NAME,B.MONITOR_ID,B.IS_SAMPLEDEPT,B.LAB_CERTIFICATE FROM dbo.T_MIS_CONTRACT_POINTITEM A
INNER JOIN dbo.T_BASE_ITEM_INFO B ON B.ID=A.ITEM_ID
WHERE 1=1");
                    if (!String.IsNullOrEmpty(tMisContractPointitem.CONTRACT_POINT_ID)) {
                        strSQL += String.Format(" AND A.CONTRACT_POINT_ID='{0}'", tMisContractPointitem.CONTRACT_POINT_ID);
                    }
                    return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 插入环境质量点位及其相关点位项目
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPointItems(TMisContractPointitemVo tMisContractPointitem)
        {
            ArrayList objVo = new ArrayList();
            if (!String.IsNullOrEmpty(tMisContractPointitem.CONTRACT_POINT_ID) && !String.IsNullOrEmpty(tMisContractPointitem.ITEM_ID))
            {
                string[] itemArr = tMisContractPointitem.ITEM_ID.Split(';');
                if (itemArr.Length > 0)
                {

                    foreach (string str in itemArr)
                    {
                        string strID = GetSerialNumber("t_mis_contract_pointItemId");
                        string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINTITEM(ID,CONTRACT_POINT_ID,ITEM_ID) VALUES('{0}','{1}','{2}')",strID,tMisContractPointitem.CONTRACT_POINT_ID,str);
                        objVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        /// <summary>
        /// 插入环境质量点位及其相关点位项目 Create By:weilin 2014-11-16
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPointItemsEx(TMisContractPointitemVo tMisContractPointitem)
        {
            ArrayList objVo = new ArrayList();
            string strSQL = string.Empty;
            if (!String.IsNullOrEmpty(tMisContractPointitem.CONTRACT_POINT_ID) && !String.IsNullOrEmpty(tMisContractPointitem.ITEM_ID))
            {
                strSQL = String.Format(" DELETE FROM T_MIS_CONTRACT_POINTITEM WHERE CONTRACT_POINT_ID='{0}'", tMisContractPointitem.CONTRACT_POINT_ID);
                objVo.Add(strSQL);
                string[] itemArr = tMisContractPointitem.ITEM_ID.Split(';');
                if (itemArr.Length > 0)
                {
                    foreach (string str in itemArr)
                    {
                        string strID = GetSerialNumber("t_mis_contract_pointItemId");
                        strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINTITEM(ID,CONTRACT_POINT_ID,ITEM_ID) VALUES('{0}','{1}','{2}')", strID, tMisContractPointitem.CONTRACT_POINT_ID, str);
                        objVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPointitemVo tMisContractPointitem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPointitem)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisContractPointitem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPointitem.ID.ToString()));
				}	
				//合同监测点ID
				if (!String.IsNullOrEmpty(tMisContractPointitem.CONTRACT_POINT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRACT_POINT_ID = '{0}'", tMisContractPointitem.CONTRACT_POINT_ID.ToString()));
				}	
				//监测项目ID
				if (!String.IsNullOrEmpty(tMisContractPointitem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisContractPointitem.ITEM_ID.ToString()));
				}	
				//已选条件项ID
				if (!String.IsNullOrEmpty(tMisContractPointitem.CONDITION_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tMisContractPointitem.CONDITION_ID.ToString()));
				}	
				//条件项类型（1，国标；2，行标；3，地标）
				if (!String.IsNullOrEmpty(tMisContractPointitem.CONDITION_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_TYPE = '{0}'", tMisContractPointitem.CONDITION_TYPE.ToString()));
				}	
				//国标上限
				if (!String.IsNullOrEmpty(tMisContractPointitem.ST_UPPER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_UPPER = '{0}'", tMisContractPointitem.ST_UPPER.ToString()));
				}	
				//国标下限
				if (!String.IsNullOrEmpty(tMisContractPointitem.ST_LOWER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_LOWER = '{0}'", tMisContractPointitem.ST_LOWER.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractPointitem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractPointitem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractPointitem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractPointitem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractPointitem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractPointitem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractPointitem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractPointitem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractPointitem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractPointitem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
