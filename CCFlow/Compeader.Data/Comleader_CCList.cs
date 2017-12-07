using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.WF;
using BP.En;

namespace Compeader.Data
{

    /// <summary>
    /// 抄送属性
    /// </summary>
    public class Comleader_CCListAttr
    {
        #region 基本属性
        /// <summary>
        /// 抄送ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 抄送PK
        /// </summary>
        public const string MyPk = "MyPk";
        /// <summary>
        /// 抄送节点
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 抄送人员
        /// </summary>
        public const string UserNo = "UserNo";

        public const string UserName = "UserName";

        #endregion
    }
    /// <summary>
    /// 抄送
    /// </summary>
    public class Comleader_CCList : Entity
    {
        #region 属性


        ///节点ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(Comleader_CCListAttr.NodeID);
            }
            set
            {
                this.SetValByKey(Comleader_CCListAttr.NodeID, value);
            }
        }
        public long OID
        {
            get
            {
                return this.GetValIntByKey(Comleader_CCListAttr.OID);
            }
            set
            {
                this.SetValByKey(Comleader_CCListAttr.OID, value);
            }
        }

        public string UserNo
        {
            get
            {
                return this.GetValStringByKey(Comleader_CCListAttr.UserNo);
            }
            set
            {
                this.SetValByKey(Comleader_CCListAttr.UserNo, value);
            }
        }

        public string UserName
        {
            get
            {
                return this.GetValStringByKey(Comleader_CCListAttr.UserName);
            }
            set
            {
                this.SetValByKey(Comleader_CCListAttr.UserName, value);
            }
        }

        public string MyPk
        {
            get
            {
                return this.GetValStringByKey(Comleader_CCListAttr.MyPk);
            }
            set
            {
                this.SetValByKey(Comleader_CCListAttr.MyPk, value);
            }
        }



        #endregion

        #region 构造函数
        /// <summary>
        /// CC
        /// </summary>
        public Comleader_CCList()
        {
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
                Map map = new Map("Comleader_CCList");
                map.EnDesc = "设置抄送列表";

                map.AddTBStringPK(Comleader_CCListAttr.MyPk, null, "主键", true, false, 0, 200, 10);

                map.AddTBInt(Comleader_CCListAttr.OID, 0, "OID", true, true);
                map.AddTBInt(Comleader_CCListAttr.NodeID, 0, "节点编号", true, true);

                map.AddTBString(Comleader_CCListAttr.UserNo, null, "抄送人员列表", true, false, 0, 2000, 10, true);
                map.AddTBString(Comleader_CCListAttr.UserName, null, "抄送人员列表", true, false, 0, 2000, 10, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 抄送s
    /// </summary>
    public class Comleader_CCLists : Entities
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Comleader_CCList();
            }
        }
        /// <summary>
        /// 抄送
        /// </summary>
        public Comleader_CCLists() { }
        #endregion
    }

}
