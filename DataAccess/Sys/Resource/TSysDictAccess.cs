using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using System.Linq;

using i3.ValueObject.Sys.Resource;
using i3.ValueObject;

namespace i3.DataAccess.Sys.Resource
{
    /// <summary>
    /// 功能：字典项管理
    /// 创建日期：2012-10-25
    /// 创建人：熊卫华
    /// </summary>
    public class TSysDictAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysDictVo tSysDict)
        {
            string strSQL = "select Count(*) from T_SYS_DICT " + this.BuildWhereStatement(tSysDict);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysDictVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_DICT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysDictVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysDict">对象条件</param>
        /// <returns>对象</returns>
        public TSysDictVo Details(TSysDictVo tSysDict)
        {
            string strSQL = String.Format("select * from  T_SYS_DICT " + this.BuildWhereStatement(tSysDict));
            return SqlHelper.ExecuteObject(new TSysDictVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysDictVo> SelectByObject(TSysDictVo tSysDict, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_SYS_DICT " + this.BuildWhereStatement(tSysDict));
            return SqlHelper.ExecuteObjectList(tSysDict, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysDictVo tSysDict, int iIndex, int iCount)
        {

            string strSQL = " select * from T_SYS_DICT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysDict));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress<TSysDictVo>(tSysDict, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysDict"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysDictVo tSysDict)
        {
            string strSQL = "select * from T_SYS_DICT " + this.BuildWhereStatement(tSysDict) + " order by CAST(order_ID as int)";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <returns></returns>
        public TSysDictVo SelectByObject(TSysDictVo tSysDict)
        {
            string strSQL = "select * from T_SYS_DICT " + this.BuildWhereStatement(tSysDict);
            return SqlHelper.ExecuteObject(new TSysDictVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysDictVo tSysDict)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysDict, TSysDictVo.T_SYS_DICT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDict">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDictVo tSysDict)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysDict, TSysDictVo.T_SYS_DICT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysDict.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDict_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysDict_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDictVo tSysDict_UpdateSet, TSysDictVo tSysDict_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysDict_UpdateSet, TSysDictVo.T_SYS_DICT_TABLE);
            strSQL += this.BuildWhereStatement(tSysDict_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_DICT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysDictVo tSysDict)
        {
            string strSQL = "delete from T_SYS_DICT ";
            strSQL += this.BuildWhereStatement(tSysDict);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 根据指定的表名及字段加载字典项（全部）
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strDataTextField">文本列</param>
        /// <param name="strDataValueField">值列</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetDataDictByTableAndFieldsAll(string strTableName, string strDataTextField, string strDataValueField)
        {
            string strSQL = "select {0} dict_text,{1} dict_code from {2}";
            strSQL = string.Format(strSQL, strDataTextField, strDataValueField, strTableName);
            return SqlHelper.ExecuteObjectList(new TSysDictVo(), strSQL);
        }
        /// <summary>
        /// 根据指定的表名及字段加载字典项(未删除）
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strDataTextField">文本列</param>
        /// <param name="strDataValueField">值列</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetDataDictByTableAndFieldsNotDeleted(string strTableName, string strDataTextField, string strDataValueField)
        {
            string strSQL = " select {0} dict_text,{1} dict_code from {2} where is_del = '0' ";
            strSQL = string.Format(strSQL, strDataTextField, strDataValueField, strTableName);
            return SqlHelper.ExecuteObjectList(new TSysDictVo(), strSQL);
        }
        /// <summary>
        /// 判断指定字典项类型是否存在
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>是/否</returns>
        public bool IsThisDataDictTypeExist(string strType)
        {
            if (string.IsNullOrEmpty(strType))
            {
                return false;
            }
            string strSQL = "select dict_text from T_SYS_DICT where upper(dict_type) = '{0}'";
            strSQL = string.Format(strSQL, strType.Trim().ToUpper());
            return SqlHelper.ExecuteScalar(strSQL) != null ? true : false;
        }
        /// <summary>
        /// 根据字典类型获取自动加载的字曲项列表
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetAutoLoadDataListByType(string strType)
        {
            string strSQL = "select dict_text,dict_code From T_SYS_DICT where upper(dict_type) = upper('{0}') and auto_load='1' order by cast(order_id as int)";
            if (strType != null && strType != "")
            {
                strSQL = string.Format(strSQL, strType.Trim());
            }
            else
            {
                strSQL = string.Format(strSQL, strType.Trim());
            }
            return SqlHelper.ExecuteObjectList(new TSysDictVo(), strSQL);
        }
        public DataTable Dept_Info(string User_ID)
        {
            string strSQL = "select c.dict_text,c.dict_code,* from T_SYS_POST a left join T_SYS_USER_POST b on a.id=b.post_id  left join T_SYS_DICT c on a.post_dept_id=c.dict_code  where A.IS_DEL='0' AND  b.USER_ID ='" + User_ID + "'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据字典类型加载字典项列表
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetDataDictListByType(string strType)
        {
            string strSQL = "select dict_text,dict_code From T_SYS_DICT where upper(dict_type) = upper('{0}') order by cast(order_id as int)";
            if (strType != null && strType != "")
            {
                strSQL = string.Format(strSQL, strType.Trim());
            }
            else
            {
                strSQL = string.Format(strSQL, strType.Trim());
            }

            return SqlHelper.ExecuteObjectList(new TSysDictVo(), strSQL);
        }
        /// <summary>
        /// 通过字典ID找到父类字典类型
        /// </summary>
        /// <param name="strID">字典ID</param>
        /// <returns>父类字典类型</returns>
        public string GetParentTypeById(string strID)
        {
            string strSQL = "select parent_type from T_SYS_DICT where id = '{0}' ";
            strSQL = String.Format(strSQL, strID);
            Object obj = SqlHelper.ExecuteScalar(strSQL);
            return obj == null ? "" : obj.ToString();
        }
        /// <summary>
        /// 根据字典类型和字典编码获得字典名称
        /// </summary>
        /// <param name="strDictCode">字典编码</param>
        /// <param name="strDictType">字典类型</param>
        /// <returns>字典名称</returns>
        public string GetDictNameByDictCodeAndType(string strDictCode, string strDictType)
        {
            string strSQL = "SELECT DICT_TEXT FROM t_sys_dict T WHERE T.DICT_TYPE='{1}' AND T.DICT_CODE='{0}' ";
            strSQL = string.Format(strSQL, strDictCode, strDictType);
            object objResult = SqlHelper.ExecuteScalar(strSQL);
            return null != objResult ? objResult.ToString() : "";
        }
        /// <summary>
        /// 根据字典类型和字典编码获得字典名称
        /// </summary>
        /// <param name="strDictCode">字典编码</param>
        /// <param name="strDictType">字典类型</param>
        /// <returns>字典名称</returns>
        public string GetDictNameByDictCodeAndTypes(string strDictCode)
        {
            string strSQL = "SELECT DICT_TEXT FROM t_sys_dict T WHERE  T.DICT_CODE='{0}' ";
            strSQL = string.Format(strSQL, strDictCode);
            object objResult = SqlHelper.ExecuteScalar(strSQL);
            return null != objResult ? objResult.ToString() : "";
        }
        /// <summary>
        /// 批量更新数据库
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool updateByTransaction(string strValue)
        {
            ArrayList arrVo = new ArrayList();
            string[] values = strValue.Split(',');
            foreach (string valueTemp in values)
            {
                string strOrderBy = valueTemp.Split('|')[0];
                string strId = valueTemp.Split('|')[1];
                string strParentId = valueTemp.Split('|')[2];

                string strsql = "update T_SYS_DICT set order_id = '" + strOrderBy + "', parent_code='" + strParentId + "' where id = '" + strId + "'";
                arrVo.Add(strsql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }
         /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool deleteByTransaction(string strValue)
        {
            ArrayList arrVo = new ArrayList();
            List<string> list = strValue.Split(',').ToList();
            foreach (string valueTemp in list)
            {
                string strsql = "delete from  T_SYS_DICT  where id = '" + valueTemp + "'";
                arrVo.Add(strsql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysDict"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysDictVo tSysDict)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysDict)
            {

                //
                if (!String.IsNullOrEmpty(tSysDict.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysDict.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.DICT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DICT_TYPE = '{0}'", tSysDict.DICT_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.DICT_TEXT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DICT_TEXT = '{0}'", tSysDict.DICT_TEXT.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.DICT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DICT_CODE in ({0})", tSysDict.DICT_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.DICT_GROUP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DICT_GROUP = '{0}'", tSysDict.DICT_GROUP.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.PARENT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_TYPE = '{0}'", tSysDict.PARENT_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.PARENT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_CODE = '{0}'", tSysDict.PARENT_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.RELATION_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RELATION_TYPE = '{0}'", tSysDict.RELATION_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.GROUP_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GROUP_CODE = '{0}'", tSysDict.GROUP_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.ORDER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORDER_ID = '{0}'", tSysDict.ORDER_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.AUTO_LOAD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AUTO_LOAD = '{0}'", tSysDict.AUTO_LOAD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.EXTENDION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXTENDION = '{0}'", tSysDict.EXTENDION.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.EXTENDION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EXTENDION_CODE = '{0}'", tSysDict.EXTENDION_CODE.ToString()));
                }
                //隐藏标记,对用户屏蔽
                if (!String.IsNullOrEmpty(tSysDict.IS_HIDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HIDE = '{0}'", tSysDict.IS_HIDE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysDict.REMARK.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysDict.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysDict.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tSysDict.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysDict.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
