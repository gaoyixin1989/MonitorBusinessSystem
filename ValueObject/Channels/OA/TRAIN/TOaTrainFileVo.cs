using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训文件附件信息
    /// 创建日期：2013-01-28
    /// 创建人：胡方扬
    /// </summary>
    public class TOaTrainFileVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_TRAIN_FILE_TABLE = "T_OA_TRAIN_FILE";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 培训计划ID
        /// </summary>
        public static string TRAIN_PLAN_ID_FIELD = "TRAIN_PLAN_ID";

        #endregion

        public TOaTrainFileVo()
        {
            this.ID = "";
            this.TRAIN_PLAN_ID = "";
            this.REMARK1 = "";
            this.REMARK2= "";
            this.REMARK3 = "";
            this.REMARK4= "";

        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 培训计划ID
        /// </summary>
        public string TRAIN_PLAN_ID
        {
            set;
            get;
        }


        #endregion


        public string REMARK1 { get; set; }

        public string REMARK4 { get; set; }

        public string REMARK3 { get; set; }

        public string REMARK2 { get; set; }
    }
}