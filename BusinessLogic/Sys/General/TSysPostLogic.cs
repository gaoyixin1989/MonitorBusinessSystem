using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.General;
using i3.DataAccess.Sys.General;

namespace i3.BusinessLogic.Sys.General
{
    /// <summary>
    /// 功能：职位管理
    /// 创建日期：2012-10-23
    /// 创建人：潘德军
    /// </summary>
    public class TSysPostLogic : LogicBase
    {

        TSysPostVo tSysPost = new TSysPostVo();
        TSysPostAccess access;

        public TSysPostLogic()
        {
            access = new TSysPostAccess();
        }

        public TSysPostLogic(TSysPostVo _tSysPost)
        {
            tSysPost = _tSysPost;
            access = new TSysPostAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysPostVo tSysPost)
        {
            return access.GetSelectResultCount(tSysPost);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysPostVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysPostVo Details(TSysPostVo tSysPost)
        {
            return access.Details(tSysPost);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysPostVo> SelectByObject(TSysPostVo tSysPost, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysPost, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysPostVo tSysPost, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysPost, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysPostVo tSysPost)
        {
            return access.SelectByTable(tSysPost);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable_byUser(string strUserId)
        {
            return access.SelectByTable_byUser(strUserId);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns></returns>
        public TSysPostVo SelectByObject(TSysPostVo tSysPost)
        {
            return access.SelectByObject(tSysPost);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysPostVo tSysPost)
        {
            return access.Create(tSysPost);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysPostVo tSysPost)
        {
            return access.Edit(tSysPost);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysPost_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysPostVo tSysPost_UpdateSet, TSysPostVo tSysPost_UpdateWhere)
        {
            return access.Edit(tSysPost_UpdateSet, tSysPost_UpdateWhere);
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
        public bool Delete(TSysPostVo tSysPost)
        {
            return access.Delete(tSysPost);
        }

        /// <summary>
        /// 删除职位，同时删除下级所有职位
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteNode(string Id)
        {
            return access.DeleteNode(Id);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //角色编号
            if (tSysPost.ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
            //职位名
            if (tSysPost.POST_NAME.Trim() == "")
            {
                this.Tips.AppendLine("职位名不能为空");
                return false;
            }
            //上级职位ID
            if (tSysPost.PARENT_POST_ID.Trim() == "")
            {
                this.Tips.AppendLine("上级职位ID不能为空");
                return false;
            }
            //行政级别
            if (tSysPost.POST_LEVEL_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政级别不能为空");
                return false;
            }
            //所属部门
            if (tSysPost.POST_DEPT_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属部门不能为空");
                return false;
            }
            //角色说明
            if (tSysPost.ROLE_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("角色说明不能为空");
                return false;
            }
            //树深度编号
            if (tSysPost.TREE_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("树深度编号不能为空");
                return false;
            }
            //排序
            if (tSysPost.NUM.Trim() == "")
            {
                this.Tips.AppendLine("排序不能为空");
                return false;
            }
            //删除标记,1为删除
            if (tSysPost.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记,1为删除不能为空");
                return false;
            }
            //创建人ID
            if (tSysPost.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人ID不能为空");
                return false;
            }
            //创建时间
            if (tSysPost.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建时间不能为空");
                return false;
            }
            //隐藏标记,对用户屏蔽
            if (tSysPost.IS_HIDE.Trim() == "")
            {
                this.Tips.AppendLine("隐藏标记,对用户屏蔽不能为空");
                return false;
            }
            //备注
            if (tSysPost.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysPost.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysPost.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysPost.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysPost.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysPost.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
