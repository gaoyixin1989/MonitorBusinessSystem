using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例控制表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfCommDict : i3.Core.ValueObject.ObjectBase
    {
        public const string strFileUpLoadFolder = "\\Sys\\WF\\Upload\\";

        /// <summary>
        /// 流程处理优先级
        /// </summary>
        public struct WfPriority
        {
            public const string Priority_1 = "1";
            public const string Priority_2 = "2";
            public const string Priority_3 = "3";
            public const string Priority_4 = "4";
            public const string Priority_5 = "5";
        }

        /// <summary>
        /// 流程处理状态
        /// </summary>
        public struct WfState
        {
            public const string StateSave = "1A";
            public const string StateNormal = "1B";
            public const string StatePause = "1C";
            public const string StateHold = "1D";
            public const string StateBack = "1E";
            public const string StateKill = "1F";
            public const string StateFinish = "1G";
            public const string StateBak = "1H";
        }

        /// <summary>
        /// 环节状态
        /// </summary>
        public struct StepState
        {
            /// <summary>
            /// 未办理
            /// </summary>
            public const string StateNormal = "2A";
            /// <summary>
            /// 已完成
            /// </summary>
            public const string StateDown = "2B";

            /// <summary>
            /// 已保存
            /// </summary>
            public const string StateSave = "2C";

            /// <summary>
            /// 已确认
            /// </summary>
            public const string StateConfirm = "2D";
        }

        /// <summary>
        /// 附属功能列举
        /// </summary>
        public struct FunctionList
        {
            public const string ForFile = "32";
            public const string ForUser = "31";
            public const string ForOpinion = "33";
            public const string ForNote = "34";
        }


        /// <summary>
        /// 表单类型
        /// </summary>
        public struct FormType
        {
            public const string ForPage = "01";
            public const string ForForm = "02";
            public const string ForUserControl = "03";
            public const string ForComponents = "04";

        }

        /// <summary>
        /// 环节处理结果
        /// </summary>
        public struct StepDealState
        {
            public const string ForBack = "00";
            public const string ForSend = "01";
            public const string ForZSend = "02";
            public const string ForHold = "03";
            public const string ForReNormal = "04";
            public const string ForPause = "05";
            public const string ForToZero = "06";
            public const string ForCallBack = "07";
            public const string ForJump = "08";
            public const string ForKill = "09";

        }

        /// <summary>
        /// 一个用于判断字符串出现次数的类
        /// </summary>
        public class OperList
        {
            public string OperValue
            { get; set; }
            public int iTimes
            { get; set; }
            public OperList()
            {
                this.OperValue = "";
                this.iTimes = 0;
            }
        }


    }
}
