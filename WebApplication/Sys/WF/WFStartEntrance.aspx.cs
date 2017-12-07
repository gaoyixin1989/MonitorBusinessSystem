using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
public partial class Sys_WF_WFStartEntrance : PageBaseForWF
{
    public DataTable FlowClass
    {
        get { return (DataTable)ViewState["FlowClass"]; }
        set { ViewState["FlowClass"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FlowClass = new TWfSettingBelongsLogic().SelectByTable(new TWfSettingBelongsVo());
            InitUserData();

        }
    }
    #region 函数
    /// <summary>
    /// 加载用户信息
    /// </summary>
    public void InitUserData()
    {
        TWfSettingFlowVo setting = new TWfSettingFlowVo() { WF_STATE = "1" };
        TWfSettingFlowLogic logic = new TWfSettingFlowLogic(setting);
        pager.RecordCount = logic.GetSelectResultCount(setting);
        DataTable dt = logic.SelectByTable(setting, pager.CurrentPageIndex, pager.PageSize);
        grdList.DataSource = dt.DefaultView;
        grdList.DataBind();
    }
    #endregion

    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager_PageChanged(object sender, EventArgs e)
    {
        InitUserData();
    }



    public string GetClassName(object objClassName)
    {
        DataTable dt = FlowClass;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr[TWfSettingBelongsVo.WF_CLASS_ID_FIELD].ToString().Trim().ToUpper() == objClassName.ToString().Trim().ToUpper())
            {
                return dr[TWfSettingBelongsVo.WF_CLASS_NAME_FIELD].ToString().Trim().ToUpper();
            }
        }
        return "未知分类";
    }

    public string GetLastTime(object obj1, object obj2)
    {
        if (obj1.ToString() == "")
            return "";
        if (obj2.ToString() == "")
            return obj1.ToString();
        if (obj1.ToString() != "" && obj2.ToString() != "")
            return obj2.ToString();
        return "";
    }
}