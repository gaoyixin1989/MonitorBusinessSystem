using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.DataAccess.Channels.OA.ARCHIVES;

namespace i3.BusinessLogic.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：文件目录管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDirectoryLogic : LogicBase
    {

        TOaArchivesDirectoryVo tOaArchivesDirectory = new TOaArchivesDirectoryVo();
        TOaArchivesDirectoryAccess access;

        public TOaArchivesDirectoryLogic()
        {
            access = new TOaArchivesDirectoryAccess();
        }

        public TOaArchivesDirectoryLogic(TOaArchivesDirectoryVo _tOaArchivesDirectory)
        {
            tOaArchivesDirectory = _tOaArchivesDirectory;
            access = new TOaArchivesDirectoryAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.GetSelectResultCount(tOaArchivesDirectory);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesDirectoryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesDirectoryVo Details(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.Details(tOaArchivesDirectory);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesDirectoryVo> SelectByObject(TOaArchivesDirectoryVo tOaArchivesDirectory, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesDirectory, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesDirectoryVo tOaArchivesDirectory, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesDirectory, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesDirectory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.SelectByTable(tOaArchivesDirectory);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns></returns>
        public TOaArchivesDirectoryVo SelectByObject(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.SelectByObject(tOaArchivesDirectory);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.Create(tOaArchivesDirectory);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDirectory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.Edit(tOaArchivesDirectory);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDirectory_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesDirectory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDirectoryVo tOaArchivesDirectory_UpdateSet, TOaArchivesDirectoryVo tOaArchivesDirectory_UpdateWhere)
        {
            return access.Edit(tOaArchivesDirectory_UpdateSet, tOaArchivesDirectory_UpdateWhere);
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
        public bool Delete(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.Delete(tOaArchivesDirectory);
        }

        /// <summary>
        /// 获取文档目录排序号
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns></returns>
        public string getNum(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            return access.getNum(tOaArchivesDirectory);
        }

        /// <summary>
        /// 删除文档目录及其子目录
        /// </summary>
        /// <param name="strId">目录ID</param>
        /// <returns></returns>
        public bool DeleteTran(string strAllID)
        {
            return access.DeleteTran(strAllID);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键
            if (tOaArchivesDirectory.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键不能为空");
                return false;
            }
            //目录名称
            if (tOaArchivesDirectory.DIRECTORY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("目录名称不能为空");
                return false;
            }
            //父目录ID，如果为根目录，则存储“0”
            if (tOaArchivesDirectory.PARENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("父目录ID，如果为根目录，则存储“0”不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tOaArchivesDirectory.IS_USE.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tOaArchivesDirectory.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesDirectory.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesDirectory.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesDirectory.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaArchivesDirectory.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaArchivesDirectory.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
