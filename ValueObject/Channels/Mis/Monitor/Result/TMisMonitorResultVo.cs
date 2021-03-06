using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_RESULT_TABLE = "T_MIS_MONITOR_RESULT";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 样品ID
        /// </summary>
        public static string SAMPLE_ID_FIELD = "SAMPLE_ID";
        /// <summary>
        /// 质控类型（原始样、现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行）
        /// </summary>
        public static string QC_TYPE_FIELD = "QC_TYPE";
        /// <summary>
        /// 质控原始样结果ID
        /// </summary>
        public static string QC_SOURCE_ID_FIELD = "QC_SOURCE_ID";
        /// <summary>
        /// 最初原始样ID，质控样可能是在原始样上做外控，然后在外控上做内控；或者在原始样上直接内控。那么最初原始样记录的是最早那个原始样的ID
        /// </summary>
        public static string SOURCE_ID_FIELD = "SOURCE_ID";
        /// <summary>
        /// 检验项
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";
        /// <summary>
        /// 检验项结果
        /// </summary>
        public static string ITEM_RESULT_FIELD = "ITEM_RESULT";
        /// <summary>
        /// 分析方法ID
        /// </summary>
        public static string ANALYSIS_METHOD_ID_FIELD = "ANALYSIS_METHOD_ID";
        /// <summary>
        /// 标准依据ID
        /// </summary>
        public static string STANDARD_ID_FIELD = "STANDARD_ID";
        /// <summary>
        /// 使用的质控手段,对该样采用的质控手段，（现场空白1、现场加标2、现场平行4、实验室密码平行8，实验室空白16、实验室加标32、实验室明码平行64），位运算
        /// </summary>
        public static string QC_FIELD = "QC";
        /// <summary>
        /// 空白批次表ID
        /// </summary>
        public static string EMPTY_IN_BAT_ID_FIELD = "EMPTY_IN_BAT_ID";
        /// <summary>
        /// 分样号
        /// </summary>
        public static string SUB_SAMPLE_CODE_FIELD = "SUB_SAMPLE_CODE";
        /// <summary>
        /// 任务状态类别(发送，退回)
        /// </summary>
        public static string TASK_TYPE_FIELD = "TASK_TYPE";
        /// <summary>
        /// 结果状态(分析任务分配：01分析结果填报：02，分析结果校核：03)
        /// </summary>
        public static string RESULT_STATUS_FIELD = "RESULT_STATUS";
        /// <summary>
        /// 仪器开始使用时间
        /// </summary>
        public static string APPARTUS_START_TIME_FIELD = "APPARTUS_START_TIME";
        /// <summary>
        /// 仪器结束使用时间
        /// </summary>
        public static string APPARTUS_END_TIME_FIELD = "APPARTUS_END_TIME";
        /// <summary>
        /// 仪器使用时长（分析复核时间）
        /// </summary>
        public static string APPARTUS_TIME_USED_FIELD = "APPARTUS_TIME_USED";
        /// <summary>
        /// 采样仪器
        /// </summary>
        public static string SAMPLING_INSTRUMENT_FIELD = "SAMPLING_INSTRUMENT_USED";
        /// <summary>
        /// 备注1(分析复核人)
        /// </summary>
        public static string REMARK_1_FIELD = "REMARK_1";
        /// <summary>
        /// 备注2
        /// </summary>
        public static string REMARK_2_FIELD = "REMARK_2";
        /// <summary>
        /// 备注3
        /// </summary>
        public static string REMARK_3_FIELD = "REMARK_3";
        /// <summary>
        /// 备注4
        /// </summary>
        public static string REMARK_4_FIELD = "REMARK_4";
        /// <summary>
        /// 备注5（该结果的类型，Poll:污染源 Air:大气）
        /// </summary>
        public static string REMARK_5_FIELD = "REMARK_5";
        /// <summary>
        /// 是否已打印
        /// </summary>
        public static string PRINTED_FIELD = "PRINTED";
        /// <summary>
        /// 结果检出限
        /// </summary>
        public static string RESULT_CHECKOUT_FIELD = "RESULT_CHECKOUT";
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

        #endregion

        public TMisMonitorResultVo()
        {
            this.ID = "";
            this.SAMPLE_ID = "";
            this.QC_TYPE = "";
            this.QC_SOURCE_ID = "";
            this.SOURCE_ID = "";
            this.ITEM_ID = "";
            this.ITEM_RESULT = "";
            this.ANALYSIS_METHOD_ID = "";
            this.STANDARD_ID = "";
            this.QC = "";
            this.EMPTY_IN_BAT_ID = "";
            this.SUB_SAMPLE_CODE = "";
            this.TASK_TYPE = "";
            this.RESULT_STATUS = "";
            this.APPARTUS_START_TIME = "";
            this.APPARTUS_END_TIME = "";
            this.APPARTUS_TIME_USED = "";
            this.SAMPLING_INSTRUMENT = "";
            this.REMARK_1 = "";
            this.REMARK_2 = "";
            this.REMARK_3 = "";
            this.REMARK_4 = "";
            this.REMARK_5 = "";
            this.PRINTED = "";
            this.RESULT_CHECKOUT = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";

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
        /// 样品ID
        /// </summary>
        public string SAMPLE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 质控类型（原始样、现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行）
        /// </summary>
        public string QC_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 质控原始样结果ID
        /// </summary>
        public string QC_SOURCE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 最初原始样ID，质控样可能是在原始样上做外控，然后在外控上做内控；或者在原始样上直接内控。那么最初原始样记录的是最早那个原始样的ID
        /// </summary>
        public string SOURCE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 检验项
        /// </summary>
        public string ITEM_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 检验项结果
        /// </summary>
        public string ITEM_RESULT
        {
            set;
            get;
        }
        /// <summary>
        /// 分析方法ID
        /// </summary>
        public string ANALYSIS_METHOD_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 标准依据ID
        /// </summary>
        public string STANDARD_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 使用的质控手段,对该样采用的质控手段，（现场空白1、现场加标2、现场平行4、实验室密码平行8，实验室空白16、实验室加标32、实验室明码平行64），位运算
        /// </summary>
        public string QC
        {
            set;
            get;
        }
        /// <summary>
        /// 空白批次表ID
        /// </summary>
        public string EMPTY_IN_BAT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 分样号
        /// </summary>
        public string SUB_SAMPLE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 任务状态类别(发送，退回)
        /// </summary>
        public string TASK_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 结果状态(分析任务分配：01分析结果填报：02，分析结果校核：03)
        /// </summary>
        public string RESULT_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 仪器开始使用时间
        /// </summary>
        public string APPARTUS_START_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 仪器结束使用时间
        /// </summary>
        public string APPARTUS_END_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 仪器使用时长
        /// </summary>
        public string APPARTUS_TIME_USED
        {
            set;
            get;
        }
        /// <summary>
        /// 采样仪器
        /// </summary>
        public string SAMPLING_INSTRUMENT
        {
            get;
            set;
        }
        /// <summary>
        /// 备注1
        /// </summary>
        public string REMARK_1
        {
            set;
            get;
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK_2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3
        /// </summary>
        public string REMARK_3
        {
            set;
            get;
        }
        /// <summary>
        /// 备注4
        /// </summary>
        public string REMARK_4
        {
            set;
            get;
        }
        /// <summary>
        /// 备注5（该结果的类型，Poll:污染源 Air:大气）
        /// </summary>
        public string REMARK_5
        {
            set;
            get;
        }
        /// <summary>
        /// 是否已打印
        /// </summary>
        public string PRINTED
        {
            set;
            get;
        }
        /// <summary>
        /// 结果检出限
        /// </summary>
        public string RESULT_CHECKOUT
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

        #endregion
    }
}