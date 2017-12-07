using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 功能描述：序列号列表
/// 创建日期：2011-04-08 14:20
/// 创建人  ：郑义
/// 修改时间：2011-04-14 18:30
/// 修改人  ：郑义
/// 修改内容：更改所有符合开发规范
/// </summary>
public partial class Sys_Resource_SerialList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitSerialListData();
           
        }
    }
    #region 函数

    /// <summary>
    /// 加载序列号列表数据
    /// </summary>
    public void InitSerialListData()
    {
       
        TSysSerialVo voSerial = new TSysSerialVo();
        voSerial.SERIAL_NAME = txtSerialName.Text;
        //重新设定分页控件页数
        pager.RecordCount = Convert.ToInt32(new TSysSerialLogic().GetSelectResultCount(voSerial));
        grdList.DataSource = new TSysSerialLogic().SelectByObject(voSerial, pager.CurrentPageIndex, pager.PageSize);
        grdList.DataBind();
    }
    #endregion
 
    #region 事件

    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager_PageChanged(object sender, EventArgs e)
    {
        InitSerialListData();
    }
    /// <summary>
    /// 删除序列号,GridView自带操作
    /// </summary>
    protected void grdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        TSysSerialVo voSerial = new TSysSerialVo();
        voSerial = new TSysSerialLogic().Details(grdList.DataKeys[e.RowIndex].Value.ToString());
        //删除指定的记录
        if (new TSysSerialLogic().Delete(grdList.DataKeys[e.RowIndex].Value.ToString()))
            base.WriteLog(i3.ValueObject.ObjectBase.LogType.DelSerial, voSerial.ID, LogInfo.UserInfo.USER_NAME + "删除序列号" + voSerial.SERIAL_CODE + "成功!");
        //重新加载数据
        InitSerialListData();
    }

    /// <summary>
    /// 删除序列号事件，checkbox选择 By Castle (胡方扬) 2012-10-26
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] ids = this.txtHidden.Value.Trim().Split(',');
        foreach (string id in ids)
        {
             TSysSerialVo voSerial = new TSysSerialVo();
             voSerial = new TSysSerialLogic().Details(id);
            //删除指定的记录
            if (new TSysSerialLogic().Delete(id))
                base.WriteLog(i3.ValueObject.ObjectBase.LogType.DelSerial, voSerial.ID, LogInfo.UserInfo.USER_NAME + "删除序列号" + voSerial.SERIAL_CODE + "成功!");

            //InitSerialListData();
        }
        //重新加载数据
        InitSerialListData();
    }
    /// <summary>
    /// 查询按钮单击事件
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
         InitSerialListData();
    }
    #endregion
  
    
}