using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Resource;
using i3.DataAccess.Sys.Resource;

namespace i3.BusinessLogic.Sys.Resource
{
    /// <summary>
    /// 功能：字典项管理
    /// 创建日期：2012-10-25
    /// 创建人：熊卫华
    /// </summary>
    public class TSysDictLogic : LogicBase
    {

        TSysDictVo tSysDict = new TSysDictVo();
        TSysDictAccess access;

        public TSysDictLogic()
        {
            access = new TSysDictAccess();
        }

        public TSysDictLogic(TSysDictVo _tSysDict)
        {
            tSysDict = _tSysDict;
            access = new TSysDictAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysDictVo tSysDict)
        {
            return access.GetSelectResultCount(tSysDict);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysDictVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysDict">对象条件</param>
        /// <returns>对象</returns>
        public TSysDictVo Details(TSysDictVo tSysDict)
        {
            return access.Details(tSysDict);
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
            return access.SelectByObject(tSysDict, iIndex, iCount);

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
            return access.SelectByTable(tSysDict, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysDict"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysDictVo tSysDict)
        {
            return access.SelectByTable(tSysDict);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysDict">对象</param>
        /// <returns></returns>
        public TSysDictVo SelectByObject(TSysDictVo tSysDict)
        {
            return access.SelectByObject(tSysDict);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysDictVo tSysDict)
        {
            return access.Create(tSysDict);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDict">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDictVo tSysDict)
        {
            return access.Edit(tSysDict);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDict_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysDict_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDictVo tSysDict_UpdateSet, TSysDictVo tSysDict_UpdateWhere)
        {
            return access.Edit(tSysDict_UpdateSet, tSysDict_UpdateWhere);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysDictVo tSysDict)
        {
            return access.Delete(tSysDict);
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
            return access.GetDataDictByTableAndFieldsAll(strTableName, strDataTextField, strDataValueField);
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
            return access.GetDataDictByTableAndFieldsNotDeleted(strTableName, strDataTextField, strDataValueField);
        }

        /// <summary>
        /// 判断指定字典项类型是否存在
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>是/否</returns>
        public bool IsThisDataDictTypeExist(string strType)
        {
            return access.IsThisDataDictTypeExist(strType);
        }
        /// <summary>
        /// 根据字典类型获取自动加载的字曲项列表
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetAutoLoadDataListByType(string strType)
        {
            return access.GetAutoLoadDataListByType(strType);
        }
        public DataTable Dept_Info(string User_ID)
        {
            return access.Dept_Info(User_ID);
        }
        /// <summary>
        /// 根据字典类型加载字典项列表
        /// </summary>
        /// <param name="strType">类型</param>
        /// <returns>字典项列表</returns>
        public List<TSysDictVo> GetDataDictListByType(string strType)
        {
            return access.GetDataDictListByType(strType);
        }
        // <summary>
        /// 通过字典ID找到父类字典类型
        /// </summary>
        /// <param name="strID">字典ID</param>
        /// <returns>父类字典类型</returns>
        public string GetParentTypeById(string strID)
        {
            return access.GetParentTypeById(strID);
        }
        /// <summary>
        /// 根据字典类型和字典编码获得字典名称
        /// </summary>
        /// <param name="strDictCode">字典编码</param>
        /// <param name="strDictType">字典类型</param>
        /// <returns>字典名称</returns>
        public string GetDictNameByDictCodeAndType(string strDictCode, string strDictType)
        {
            return access.GetDictNameByDictCodeAndType(strDictCode, strDictType);
        }
        /// <summary>
        /// 根据字典类型和字典编码获得字典名称
        /// </summary>
        /// <param name="strDictCode">字典编码</param>
        /// <param name="strDictType">字典类型</param>
        /// <returns>字典名称</returns>
        public string GetDictNameByDictCodeAndTypes(string strDictCode)
        {
            return access.GetDictNameByDictCodeAndTypes(strDictCode);
        }
        /// <summary>
        /// 批量更新数据库
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool updateByTransaction(string strValue)
        {
            return access.updateByTransaction(strValue);
        }
          /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool deleteByTransaction(string strValue)
        {
            return access.deleteByTransaction(strValue);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tSysDict.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.DICT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.DICT_TEXT.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.DICT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.DICT_GROUP.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.PARENT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.PARENT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.RELATION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.GROUP_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.ORDER_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.AUTO_LOAD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.EXTENDION.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.EXTENDION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //隐藏标记,对用户屏蔽
            if (tSysDict.IS_HIDE.Trim() == "")
            {
                this.Tips.AppendLine("隐藏标记,对用户屏蔽不能为空");
                return false;
            }
            //
            if (tSysDict.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tSysDict.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
