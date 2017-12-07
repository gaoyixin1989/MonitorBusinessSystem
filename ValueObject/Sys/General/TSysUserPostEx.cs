using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 用户岗位信息扩展类
    /// 1、树形结构、存储用户标示、岗位标示
    /// 2、提供直接领导链查询
    /// 3、提供下属树结构查询
    /// </summary>
    public class TSysUserPostEx : TSysUserPostVo
    {
        /// <summary>
        /// 子岗位列表，其节点是支持递归嵌套的岗位集合
        /// 默认支持所有下属岗位及其子岗位
        /// </summary>
        public TSysUserPostExs ChildUserPosts
        {
            get;
            set;
        }

        /// <summary>
        ///  岗位树中某一个岗位 回溯路径至起点 所形成的父岗位序列。
        ///  [CurrentNode],[ParentNode],[ParentParentNode],[ParentParentParentNode]……[RootNode]
        ///  获取时，请根据索引判断是否是当前节点的父岗位，还是父父岗位，一直至岗位的源位置。
        ///  ParentUserPosts[0]就是父岗位，而ParentUserPosts[ParentUserPosts.Count-1]则为岗位源头。
        /// </summary>
        public TSysUserPostExs ParentUserPosts
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位树中直接父岗位。这个节点存储的是一个岗位。
        /// 根据此节点可遍历至此岗位组下的所有岗位信息(依赖于构造时的构造范围，默认仅支持一级父岗位)
        /// </summary>
        public TSysUserPostEx ParentUserPost
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户实际名称(显示名称)
        /// </summary>
        public string RealName
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 列表化的子集
    /// </summary>
    public class TSysUserPostExs : List<TSysUserPostEx>
    {
        //这里暂时没有需要扩展的。
    }
}
