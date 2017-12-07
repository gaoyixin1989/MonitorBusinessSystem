using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Resource
{
    /// <summary>
    /// 功能：采样分析监测流程排序配置
    /// 创建日期：2013-04-01
    /// 创建人：邵世卓
    /// </summary>
    public class TSysConfigFlownumVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_SYS_CONFIG_FLOWNUM_TABLE = "T_SYS_CONFIG_FLOWNUM";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 环节名称
        /// </summary>
        public static string FLOW_NAME_FIELD = "FLOW_NAME";
        /// <summary>
        /// 第一编号
        /// </summary>
        public static string FIRST_FLOW_CODE_FIELD = "FIRST_FLOW_CODE";
        /// <summary>
        /// 第二编号
        /// </summary>
        public static string SECOND_FLOW_CODE_FIELD = "SECOND_FLOW_CODE";
        /// <summary>
        /// 第三编号
        /// </summary>
        public static string THIRD_FLOW_CODE_FIELD = "THIRD_FLOW_CODE";
        /// <summary>
        /// 环节序号
        /// </summary>
        public static string FLOW_NUM_FIELD = "FLOW_NUM";
        /// <summary>
        /// 是否并行
        /// </summary>
        public static string IS_COL_FIELD = "IS_COL";
        /// <summary>
        /// 是否删除
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
        /// <summary>
        /// 备注1
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 备注2
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 备注3
        /// </summary>
        public static string REMARK3_FIELD = "REMARK3";

        #endregion

        public TSysConfigFlownumVo()
        {
            this.ID = "";
            this.FLOW_NAME = "";
            this.FIRST_FLOW_CODE = "";
            this.SECOND_FLOW_CODE = "";
            this.THIRD_FLOW_CODE = "";
            this.FLOW_NUM = "";
            this.IS_COL = "";
            this.IS_DEL = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";

        }

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节名称
        /// </summary>
        public string FLOW_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 第一编号
        /// </summary>
        public string FIRST_FLOW_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 第二编号
        /// </summary>
        public string SECOND_FLOW_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 第三编号
        /// </summary>
        public string THIRD_FLOW_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 环节序号
        /// </summary>
        public string FLOW_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 是否并行
        /// </summary>
        public string IS_COL
        {
            set;
            get;
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }
        /// <summary>
        /// 备注1
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }


        #endregion

    }
}