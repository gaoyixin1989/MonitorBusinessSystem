using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_SAMPLE_INFO_TABLE = "T_MIS_MONITOR_SAMPLE_INFO";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测子任务ID
        /// </summary>
        public static string SUBTASK_ID_FIELD = "SUBTASK_ID";
        /// <summary>
        /// 点位
        /// </summary>
        public static string POINT_ID_FIELD = "POINT_ID";
        /// <summary>
        /// 样品号
        /// </summary>
        public static string SAMPLE_CODE_FIELD = "SAMPLE_CODE";
        /// <summary>
        /// 样品类型
        /// </summary>
        public static string SAMPLE_TYPE_FIELD = "SAMPLE_TYPE";
        /// <summary>
        /// 样品名称
        /// </summary>
        public static string SAMPLE_NAME_FIELD = "SAMPLE_NAME";
        /// <summary>
        /// 样品数量
        /// </summary>
        public static string SAMPLE_COUNT_FIELD = "SAMPLE_COUNT";
        /// <summary>
        /// 状态
        /// </summary>
        public static string STATUS_FIELD = "STATUS";
        /// <summary>
        /// 未采样
        /// </summary>
        public static string NOSAMPLE_FIELD = "NOSAMPLE";
        /// <summary>
        /// 未采样说明
        /// </summary>
        public static string NOSAMPLEREMARK_FIELD = "NOSAMPLEREMARK";
        /// <summary>
        /// 质控类型（现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行、标准样）
        /// </summary>
        public static string QC_TYPE_FIELD = "QC_TYPE";
        /// <summary>
        /// 质控原始样ID
        /// </summary>
        public static string QC_SOURCE_ID_FIELD = "QC_SOURCE_ID";
        /// <summary>
        /// 是否打印(未打印：0已打印：1)
        /// </summary>
        public static string PRINTED_FIELD = "PRINTED";
        /// <summary>
        /// 样品分析通知单是否打印
        /// </summary>
        public static string SAMPLES_ORDER_ISPRINTED_FIELD = "SAMPLES_ORDER_ISPRINTED";
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
        /// 备注5(清远：废气排放量)
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";
        /// <summary>
        /// 
        /// </summary>
        public static string SPECIALREMARK_FIELD = "SPECIALREMARK";
        /// <summary>
        /// 
        /// </summary>
        public static string SUBSAMPLE_NUM_FIELD = "SUBSAMPLE_NUM";
        /// <summary>
        /// 编号日期
        /// </summary>
        public static string SAMPLECODE_CREATEDATE_FIELD = "SAMPLECODE_CREATEDATE";
        /// <summary>
        /// 样品交接时间或地址
        /// </summary>
        public static string SAMPLE_ACCEPT_DATEORACC_FIELD = "SAMPLE_ACCEPT_DATEORACC";
        /// <summary>
        /// 样品状态
        /// </summary>
        public static string SAMPLE_STATUS_FIELD = "SAMPLE_STATUS";
        /// <summary>
        /// 样品原编码或名称
        /// </summary>
        public static string SRC_CODEORNAME_FIELD = "SRC_CODEORNAME";
        /// <summary>
        /// 昼间主要声源
        /// </summary>
        public static string D_SOURCE_FIELD = "D_SOURCE";
        /// <summary>
        /// 夜间主要声源
        /// </summary>
        public static string N_SOURCE_FIELD = "N_SOURCE";
        /// <summary>
        /// 环境质量（月）
        /// </summary>
        public static string ENV_MONTH_FIELD = "ENV_MONTH";
        /// <summary>
        /// 环境质量（日）
        /// </summary>
        public static string ENV_DAY_FIELD = "ENV_DAY";
        /// <summary>
        /// 环境质量（时）
        /// </summary>
        public static string ENV_HOUR_FIELD = "ENV_HOUR";
        /// <summary>
        /// 环境质量（分）
        /// </summary>
        public static string ENV_MINUTE_FIELD = "ENV_MINUTE";
        /// <summary>
        /// 环境质量（结束月）
        /// </summary>
        public static string ENV_MONTH_END_FIELD = "ENV_MONTH_END";
        /// <summary>
        /// 环境质量（结束日）
        /// </summary>
        public static string ENV_DAY_END_FIELD = "ENV_DAY_END";
        /// <summary>
        /// 环境质量（结束时）
        /// </summary>
        public static string ENV_HOUR_END_FIELD = "ENV_HOUR_END";
        /// <summary>
        /// 环境质量（结束分）
        /// </summary>
        public static string ENV_MINUTE_END_FIELD = "ENV_MINUTE_END";
        /// <summary>
        /// 环境质量（降雨类型）
        /// </summary>
        public static string SAMPLE_RAIN_TYPE_FIELD = "SAMPLE_RAIN_TYPE";
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public static string CCFLOW_ID1_FIELD = "CCFLOW_ID1";
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public static string CCFLOW_ID2_FIELD = "CCFLOW_ID2";
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public static string CCFLOW_ID3_FIELD = "CCFLOW_ID3";
        /// <summary>
        /// 样品条码
        /// </summary>
        public static string SAMPLE_BARCODE_FIELD = "SAMPLE_BARCODE";

        #endregion

        public TMisMonitorSampleInfoVo()
        {
            this.ID = "";
            this.SUBTASK_ID = "";
            this.POINT_ID = "";
            this.SAMPLE_CODE = "";
            this.SAMPLE_TYPE = "";
            this.SAMPLE_NAME = "";
            this.SAMPLE_COUNT = "";
            this.STATUS = "";
            this.NOSAMPLE = "";
            this.NOSAMPLEREMARK = "";
            this.QC_TYPE = "";
            this.QC_SOURCE_ID = "";
            this.PRINTED = "";
            this.SAMPLES_ORDER_ISPRINTED = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.SPECIALREMARK = "";
            this.SUBSAMPLE_NUM = "";
            this.SAMPLECODE_CREATEDATE = "";
            this.SAMPLE_ACCEPT_DATEORACC = "";
            this.SAMPLE_STATUS = "";
            this.SAMPLE_REMARK = "";
            this.SRC_CODEORNAME = "";
            this.D_SOURCE = "";
            this.N_SOURCE = "";
            this.ENV_MONTH = "";
            this.ENV_DAY = "";
            this.ENV_HOUR = "";
            this.ENV_MINUTE = "";
            this.ENV_MONTH_END = "";
            this.ENV_DAY_END = "";
            this.ENV_HOUR_END = "";
            this.ENV_MINUTE_END = "";
            this.SAMPLE_RAIN_TYPE = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";
            this.SAMPLE_BARCODE = "";

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
        /// 监测子任务ID
        /// </summary>
        public string SUBTASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 点位
        /// </summary>
        public string POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 样品号
        /// </summary>
        public string SAMPLE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品类型
        /// </summary>
        public string SAMPLE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string SAMPLE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 样品数量
        /// </summary>
        public string SAMPLE_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 未采样
        /// </summary>
        public string NOSAMPLE
        {
            set;
            get;
        }
        /// <summary>
        /// 未采样说明
        /// </summary>
        public string NOSAMPLEREMARK
        {
            set;
            get;
        }
        /// <summary>
        /// 质控类型（现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行、标准样）
        /// </summary>
        public string QC_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 质控原始样ID
        /// </summary>
        public string QC_SOURCE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 是否打印(未打印：0已打印：1)
        /// </summary>
        public string PRINTED
        {
            set;
            get;
        }
        /// <summary>
        /// 样品分析通知单是否打印
        /// </summary>
        public string SAMPLES_ORDER_ISPRINTED
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
        /// <summary>
        /// 
        /// </summary>
        public string SPECIALREMARK
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SUBSAMPLE_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 编号日期
        /// </summary>
        public string SAMPLECODE_CREATEDATE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品交接时间或地址
        /// </summary>
        public string SAMPLE_ACCEPT_DATEORACC
        {
            set;
            get;
        }
        /// <summary>
        /// 样品状态
        /// </summary>
        public string SAMPLE_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 样品原编码或名称
        /// </summary>
        public string SRC_CODEORNAME
        {
            set;
            get;
        }

        /// <summary>
        /// 样品前处理说明
        /// </summary>
        public string SAMPLE_REMARK
        {
            set;
            get;
        }
        /// <summary>
        /// 昼间主要声源
        /// </summary>
        public string D_SOURCE
        {
            set;
            get;
        }

        /// <summary>
        /// 夜间主要声源
        /// </summary>
        public string N_SOURCE
        {
            set;
            get;
        }

        /// <summary>
        /// 环境质量（月）
        /// </summary>
        public string ENV_MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（日）
        /// </summary>
        public string ENV_DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（时）
        /// </summary>
        public string ENV_HOUR
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（分）
        /// </summary>
        public string ENV_MINUTE
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（结束月）
        /// </summary>
        public string ENV_MONTH_END
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（结束日）
        /// </summary>
        public string ENV_DAY_END
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（结束时）
        /// </summary>
        public string ENV_HOUR_END
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（结束分）
        /// </summary>
        public string ENV_MINUTE_END
        {
            set;
            get;
        }
        /// <summary>
        /// 环境质量（降雨类型）
        /// </summary>
        public string SAMPLE_RAIN_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public string CCFLOW_ID1
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public string CCFLOW_ID2
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public string CCFLOW_ID3
        {
            set;
            get;
        }
        /// <summary>
        /// 样品条码
        /// </summary>
        public string SAMPLE_BARCODE
        {
            set;
            get;
        }
        #endregion



    }
}