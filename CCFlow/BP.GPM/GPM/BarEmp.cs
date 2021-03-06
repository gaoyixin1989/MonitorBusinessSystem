﻿using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// 人员信息块
    /// </summary>
    public class BarEmpAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 信息快
        /// </summary>
        public const string FK_Bar = "FK_Bar";
        /// <summary>
        /// 人员
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 是否显示?
        /// </summary>
        public const string IsShow = "IsShow";
    }
    /// <summary>
    /// 人员信息块
    /// </summary>
    public class BarEmp : EntityMyPK
    {
        #region 属性
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(BarEmpAttr.Idx);
            }
            set
            {
                this.SetValByKey(BarEmpAttr.Idx, value);
            }
        }
        public string FK_Bar
        {
            get
            {
                return this.GetValStringByKey(BarEmpAttr.FK_Bar);
            }
            set
            {
                this.SetValByKey(BarEmpAttr.FK_Bar, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(BarEmpAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(BarEmpAttr.FK_Emp, value);
            }
        }
        public bool IsShow
        {
            get
            {
                return this.GetValBooleanByKey(BarEmpAttr.IsShow);
            }
            set
            {
                this.SetValByKey(BarEmpAttr.IsShow, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 人员信息块
        /// </summary>
        public BarEmp()
        {
        }
        /// <summary>
        /// 人员信息块
        /// </summary>
        /// <param name="mypk"></param>
        public BarEmp(string no)
        {
          //  this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GPM_BarEmp");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "人员信息块";
                map.EnType = EnType.Sys;

                map.AddMyPK(); // 主键是由:  FK_Bar+"_"+FK_Emp 组成的，它是一个复合主键.
                map.AddTBString(BarEmpAttr.FK_Bar, null, "信息块编号", true, false, 0, 90, 20);
                map.AddTBString(BarEmpAttr.FK_Emp, null, "人员编号", true, false, 0, 90, 20);
                map.AddTBInt(BarEmpAttr.IsShow, 0, "是否显示", false, true);
                map.AddTBInt(BarEmpAttr.Idx, 0, "显示顺序", false, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public void DoUp()
        {
            this.DoOrderUp(BarEmpAttr.FK_Emp, Web.WebUser.No, BarEmpAttr.Idx);
        }
        public void DoDown()
        {
            this.DoOrderDown(BarEmpAttr.FK_Emp, Web.WebUser.No, BarEmpAttr.Idx);
        }
        public void DoHidShow()
        {
            this.IsShow = !this.IsShow;
            this.Update();
        }
    }
    /// <summary>
    /// 人员信息块s
    /// </summary>
    public class BarEmps : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 人员信息块s
        /// </summary>
        public BarEmps()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BarEmp();
            }
        }
        #endregion

        public void InitMyBars()
        {
            Bars bars = new Bars();
            bars.RetrieveAll();
            foreach (Bar b in bars)
            {
                BarEmp be = new BarEmp();
                be.MyPK = BP.Web.WebUser.No + "_" + b.No;
                if (be.RetrieveFromDBSources() == 1)
                    continue;

                be.FK_Bar = b.No;
                be.FK_Emp = BP.Web.WebUser.No;
                be.IsShow = true;
                be.Insert();
            }
        }
    }
}
