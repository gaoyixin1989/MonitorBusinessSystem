using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.DataAccess.Sys.WF;

namespace i3.BusinessLogic.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例附件明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstFileListLogic : LogicBase
    {

        TWfInstFileListVo tWfInstFileList = new TWfInstFileListVo();
        TWfInstFileListAccess access;

        public TWfInstFileListLogic()
        {
            access = new TWfInstFileListAccess();
        }

        public TWfInstFileListLogic(TWfInstFileListVo _tWfInstFileList)
        {
            tWfInstFileList = _tWfInstFileList;
            access = new TWfInstFileListAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstFileListVo tWfInstFileList)
        {
            return access.GetSelectResultCount(tWfInstFileList);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstFileListVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstFileList">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstFileListVo Details(TWfInstFileListVo tWfInstFileList)
        {
            return access.Details(tWfInstFileList);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstFileListVo> SelectByObject(TWfInstFileListVo tWfInstFileList, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfInstFileList, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstFileListVo tWfInstFileList, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfInstFileList, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstFileList"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstFileListVo tWfInstFileList)
        {
            return access.SelectByTable(tWfInstFileList);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <returns></returns>
        public TWfInstFileListVo SelectByObject(TWfInstFileListVo tWfInstFileList)
        {
            return access.SelectByObject(tWfInstFileList);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstFileListVo tWfInstFileList)
        {
            return access.Create(tWfInstFileList);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstFileList">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstFileListVo tWfInstFileList)
        {
            return access.Edit(tWfInstFileList);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstFileList_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstFileList_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstFileListVo tWfInstFileList_UpdateSet, TWfInstFileListVo tWfInstFileList_UpdateWhere)
        {
            return access.Edit(tWfInstFileList_UpdateSet, tWfInstFileList_UpdateWhere);
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
        public bool Delete(TWfInstFileListVo tWfInstFileList)
        {
            return access.Delete(tWfInstFileList);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfInstFileList.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //流程实例编号
            if (tWfInstFileList.WF_INST_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //流程编号
            if (tWfInstFileList.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //流水号
            if (tWfInstFileList.WF_SERIAL_NO.Trim() == "")
            {
                this.Tips.AppendLine("流水号不能为空");
                return false;
            }
            //文件全路径
            if (tWfInstFileList.WF_FILE_FULLNAME.Trim() == "")
            {
                this.Tips.AppendLine("文件全路径不能为空");
                return false;
            }
            //文件名称
            if (tWfInstFileList.WF_FILE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("文件名称不能为空");
                return false;
            }
            //文件图标
            if (tWfInstFileList.WF_FILE_ICO.Trim() == "")
            {
                this.Tips.AppendLine("文件图标不能为空");
                return false;
            }
            //删除标记
            if (tWfInstFileList.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }

            return true;
        }

    }
}
