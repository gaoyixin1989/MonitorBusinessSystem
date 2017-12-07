﻿using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Template
{
    /// <summary>
    /// 显示位置
    /// </summary>
    public enum ShowWhere
    {
        /// <summary>
        /// 树
        /// </summary>
        Tree,
        /// <summary>
        /// 工具栏
        /// </summary>
        Toolbar
    }
    /// <summary>
    /// 工具栏属性
    /// </summary>
    public class NodeToolbarAttr : BP.En.EntityOIDNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 节点
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 到达目标
        /// </summary>
        public const string Target = "Target";
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// url
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 顺序号
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 显示在那里？
        /// </summary>
        public const string ShowWhere = "ShowWhere";
        #endregion
    }
    /// <summary>
    /// 工具栏.	 
    /// </summary>
    public class NodeToolbar : EntityOID
    {
        #region 基本属性
        /// <summary>
        /// UI界面上的访问控制
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 工具栏的事务编号
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NodeToolbarAttr.FK_Node);
            }
            set
            {
                SetValByKey(NodeToolbarAttr.FK_Node, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(NodeToolbarAttr.Title);
            }
            set
            {
                SetValByKey(NodeToolbarAttr.Title, value);
            }
        }
        public string Url
        {
            get
            {
                string s= this.GetValStringByKey(NodeToolbarAttr.Url);
                if (s.Contains("?") == false)
                    s = s+"?1=2";
                return s;
            }
            set
            {
                SetValByKey(NodeToolbarAttr.Url, value);
            }
        }
        public string Target
        {
            get
            {
                return this.GetValStringByKey(NodeToolbarAttr.Target);
            }
            set
            {
                SetValByKey(NodeToolbarAttr.Target, value);
            }
        }
        /// <summary>
        /// 显示在那里？
        /// </summary>
        public ShowWhere ShowWhere
        {
            get
            {
                return (ShowWhere)this.GetValIntByKey(NodeToolbarAttr.ShowWhere);
            }
            set
            {
                SetValByKey(NodeToolbarAttr.ShowWhere, (int)value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 工具栏
        /// </summary>
        public NodeToolbar() { }
        /// <summary>
        /// 工具栏
        /// </summary>
        /// <param name="_oid">工具栏ID</param>	
        public NodeToolbar(int oid)
        {
            this.OID = oid;
            this.Retrieve();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeToolbar");
                map.EnDesc = "自定义工具栏"; 

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();

                map.AddTBString(NodeToolbarAttr.Title, null, "标题", true, false, 0, 100, 100, true);
                map.AddTBString(NodeToolbarAttr.Target, null, "目标", true, false, 0, 50, 50, true);
                map.AddTBString(NodeToolbarAttr.Url, null, "连接", true, false, 0, 500, 300, true);
                // 显示位置.
                map.AddDDLSysEnum(NodeToolbarAttr.ShowWhere, 0, "显示位置", true,
                    true, NodeToolbarAttr.ShowWhere,
                    "@0=树形表单@1=工具栏");


                map.AddTBInt(NodeToolbarAttr.Idx, 0, "显示顺序", true, false);
                map.AddTBInt(NodeToolbarAttr.FK_Node, 0, "节点", false,true);
                map.AddMyFile("图标");


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 工具栏集合
    /// </summary>
    public class NodeToolbars : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeToolbar();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 工具栏集合
        /// </summary>
        public NodeToolbars()
        {
        }
        /// <summary>
        /// 工具栏集合.
        /// </summary>
        /// <param name="fk_node"></param>
        public NodeToolbars(string fk_node)
        {
            this.Retrieve(NodeToolbarAttr.FK_Node, fk_node);
        }
        #endregion
    }
}
