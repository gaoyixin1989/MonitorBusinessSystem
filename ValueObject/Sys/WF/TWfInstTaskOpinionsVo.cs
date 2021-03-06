using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节附属评论表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskOpinionsVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_INST_TASK_OPINIONS_TABLE = "T_WF_INST_TASK_OPINIONS";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 环节实例编号
        /// </summary>
        public static string WF_INST_TASK_ID_FIELD = "WF_INST_TASK_ID";
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public static string WF_INST_ID_FIELD = "WF_INST_ID";
        /// <summary>
        /// 评论内容
        /// </summary>
        public static string WF_IT_OPINION_FIELD = "WF_IT_OPINION";
        /// <summary>
        /// 评论类型
        /// </summary>
        public static string WF_IT_OPINION_TYPE_FIELD = "WF_IT_OPINION_TYPE";
        /// <summary>
        /// 评论人
        /// </summary>
        public static string WF_IT_OPINION_USER_FIELD = "WF_IT_OPINION_USER";
        /// <summary>
        /// 显示方式(只显示上一条,全显示)
        /// </summary>
        public static string WF_IT_SHOW_TYPE_FIELD = "WF_IT_SHOW_TYPE";

        /// <summary>
        /// 增加评论时间
        /// </summary>
        public static string WF_IT_OPINION_TIME_FIELD = "WF_IT_OPINION_TIME";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";

        #endregion

        public TWfInstTaskOpinionsVo()
        {
            this.ID = "";
            this.WF_INST_TASK_ID = "";
            this.WF_INST_ID = "";
            this.WF_IT_OPINION = "";
            this.WF_IT_OPINION_TYPE = "";
            this.WF_IT_OPINION_USER = "";
            this.WF_IT_SHOW_TYPE = "";
            this.WF_IT_OPINION_TIME = "";
            this.IS_DEL = "";

        }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节实例编号
        /// </summary>
        public string WF_INST_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string WF_INST_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string WF_IT_OPINION
        {
            set;
            get;
        }
        /// <summary>
        /// 评论类型
        /// </summary>
        public string WF_IT_OPINION_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 评论人
        /// </summary>
        public string WF_IT_OPINION_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 显示方式(只显示上一条,全显示)
        /// </summary>
        public string WF_IT_SHOW_TYPE
        {
            set;
            get;
        }

        /// <summary>
        /// 评论时间
        /// </summary>
        public string WF_IT_OPINION_TIME
        {
            get;
            set;
        }

        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }


        #endregion

    }
}