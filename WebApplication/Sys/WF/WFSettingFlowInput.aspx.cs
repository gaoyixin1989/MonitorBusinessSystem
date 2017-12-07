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

public partial class Sys_WF_WFSettingFlowInput : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitDataDict();
        }

        if (!string.IsNullOrEmpty(Request.QueryString[TWfSettingBelongsVo.ID_FIELD]))
        {
            ID.Value = Request.QueryString[TWfSettingBelongsVo.ID_FIELD];
            if (!IsPostBack)
            {
                //执行绑定操作
                InitWFData(ID.Value);
            }
        }

    }

    public void InitDataDict()
    {
        DataTable dt = new TWfSettingBelongsLogic().SelectByTable(new TWfSettingBelongsVo());
        WF_CLASS_ID.DataSource = dt.DefaultView;
        WF_CLASS_ID.DataTextField = TWfSettingBelongsVo.WF_CLASS_NAME_FIELD;
        WF_CLASS_ID.DataValueField = TWfSettingBelongsVo.WF_CLASS_ID_FIELD;
        WF_CLASS_ID.DataBind();
    }

    public void InitWFData(string strID)
    {
        TWfSettingFlowVo twfsfv = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { ID = ID.Value });
        BindObjectToControlsMode(twfsfv);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (string.IsNullOrEmpty(ID.Value))
        {
            TWfSettingFlowVo twfsfv = new TWfSettingFlowVo();
            twfsfv = BindControlsToObjectMode(twfsfv) as TWfSettingFlowVo;
            //验证工作流数据完整性
            if (string.IsNullOrEmpty(twfsfv.WF_CAPTION) || string.IsNullOrEmpty(twfsfv.WF_ID))
            {
                this.Alert("工作流简称和代码不可为空");
                return;
            }
            //验证工作流简称是否重复
            if (!string.IsNullOrEmpty(new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_CAPTION = twfsfv.WF_CAPTION }).ID))
            {
                this.Alert("工作流简称重复，请再指定");
                return;
            }
            //验证工作流代码是否重复
            if (!string.IsNullOrEmpty(new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = twfsfv.WF_ID }).ID))
            {
                this.Alert("工作流代码重复，请再指定");
                return;
            }

            twfsfv.ID = string.IsNullOrEmpty(twfsfv.ID) ? this.GetGUID() : twfsfv.ID;
            twfsfv.WF_VERSION = WF_VERSION_FIELD;
            twfsfv.WF_STATE = "1";
            twfsfv.WF_FORM_MAIN = "";
            twfsfv.CREATE_DATE = this.GetDateTimeToStanString();
            twfsfv.CREATE_USER = base.LogInfo.UserInfo.ID;
            if (new TWfSettingFlowLogic().Create(twfsfv))
            {
                this.Alert("增加工作流成功");
                //以下是日志记录

                return;
            }
            else
            {
                this.Alert("增加工作流失败，请重试");
                //以下是日志记录
            }
        }
        else
        {
            //是修改的
            TWfSettingFlowVo twfsfv = new TWfSettingFlowVo();
            twfsfv = BindControlsToObjectMode(twfsfv) as TWfSettingFlowVo;
            twfsfv.ID = ID.Value;
            //验证工作流数据完整性
            if (string.IsNullOrEmpty(twfsfv.WF_CAPTION) || string.IsNullOrEmpty(twfsfv.WF_ID))
            {
                this.Alert("工作流简称和代码不可为空");
                return;
            }
            twfsfv.DEAL_DATE = this.GetDateTimeToStanString();
            twfsfv.DEAL_TYPE = "1";
            twfsfv.DEAL_USER = this.LogInfo.UserInfo.ID;
            if (new TWfSettingFlowLogic().Edit(twfsfv))
            {
                this.Alert("编辑工作流成功");
                //以下是日志记录

                return;
            }
            else
            {
                this.Alert("编辑工作流失败，请重试");
                //以下是日志记录
            }
        }



    }
}