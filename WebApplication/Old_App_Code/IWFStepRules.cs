using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace i3.View
{
    /// <summary>
    /// 和业务处理页面交互的接口
    /// </summary>
    public interface IWFStepRules
    {
        /// <summary>
        /// 加载和显示业务数据
        /// </summary>
        void LoadAndViewBusinessData();

        /// <summary>
        /// 封装和验证业务数据的完整性接口
        /// </summary>
        bool BuildAndValidateBusinessData(out string strMsg);

        /// <summary>
        /// 创建和注册业务数据接口，创建指固化入库、注册指将业务数据的主键写入工作流节点
        /// </summary>
        void CreatAndRegisterBusinessData();

        /// <summary>
        /// 业务系统保存业务数据的接口，如果不启动工作流，仍需要保持业务，则只需要实现此方法即可
        /// </summary>
        void SaveBusinessDataFromPageControl();
    }
}