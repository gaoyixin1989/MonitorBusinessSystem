using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：空白加标(秦皇岛)
    /// 创建日期：2013-04-28
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcAddQhdVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_QC_ADD_QHD_TABLE = "T_MIS_MONITOR_QC_ADD_QHD";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 平行样分析结果 ID
        /// </summary>
        public static string RESULT_ID_ADD_FIELD = "RESULT_ID_ADD";
        /// <summary>
        /// 加标量
        /// </summary>
        public static string QC_ADD_FIELD = "QC_ADD";
        /// <summary>
        /// 原始测定值
        /// </summary>
        public static string SRC_RESULT_FIELD = "SRC_RESULT";
        /// <summary>
        /// 加标测定值
        /// </summary>
        public static string ADD_RESULT_EX_FIELD = "ADD_RESULT_EX";
        /// <summary>
        /// 加标回收率（%）
        /// </summary>
        public static string ADD_BACK_FIELD = "ADD_BACK";
        /// <summary>
        /// 加标是否合格
        /// </summary>
        public static string ADD_ISOK_FIELD = "ADD_ISOK";
        /// <summary>
        /// 质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标）
        /// </summary>
        public static string QC_TYPE_FIELD = "QC_TYPE";
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
        /// <summary>
        /// 备注4
        /// </summary>
        public static string REMARK4_FIELD = "REMARK4";
        /// <summary>
        /// 备注5
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";

        #endregion

        public TMisMonitorQcAddQhdVo()
        {
            this.ID = "";
            this.RESULT_ID_ADD = "";
            this.QC_ADD = "";
            this.SRC_RESULT = "";
            this.ADD_RESULT_EX = "";
            this.ADD_BACK = "";
            this.ADD_ISOK = "";
            this.QC_TYPE = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

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
        /// 平行样分析结果 ID
        /// </summary>
        public string RESULT_ID_ADD
        {
            set;
            get;
        }
        /// <summary>
        /// 加标量
        /// </summary>
        public string QC_ADD
        {
            set;
            get;
        }
        /// <summary>
        /// 原始测定值
        /// </summary>
        public string SRC_RESULT
        {
            set;
            get;
        }
        /// <summary>
        /// 加标测定值
        /// </summary>
        public string ADD_RESULT_EX
        {
            set;
            get;
        }
        /// <summary>
        /// 加标回收率（%）
        /// </summary>
        public string ADD_BACK
        {
            set;
            get;
        }
        /// <summary>
        /// 加标是否合格
        /// </summary>
        public string ADD_ISOK
        {
            set;
            get;
        }
        /// <summary>
        /// 质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标）
        /// </summary>
        public string QC_TYPE
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
        /// <summary>
        /// 备注4
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 备注5
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }


        #endregion

    }
}