using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
/// <summary>
/// 功能描述：登陆用户修改个人信息
/// 创建日期：2012-11-01
/// 创建人  ：Castle (胡方扬)
/// </summary>
public partial class Sys_General_PersonUserEdit : PageBase
{
    protected string strBrithday = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControlDate();
            InitUserData(LogInfo.UserInfo.ID);
           
        }
    }
    #region 函数
    /// <summary>
    /// 初始化加载页面控件信息
    /// </summary>
    protected void InitControlDate()
    {
        
        BindDataDictToContainer(Master.FindControl("cphInput"));
       
    }
    /// <summary>
    /// 初始化用户信息数据
    /// </summary>
    /// <param name="strUserID">用户ID</param>
    protected void InitUserData(string strUserID)
    {
        TSysUserVo objUserVo = new TSysUserLogic().Details(strUserID);
        strBrithday = objUserVo.BIRTHDAY;
        BindObjectToControlsMode(objUserVo);
    }
    /// <summary>
    /// 保存用户信息
    /// </summary>
    protected void SaveUserInfo()
    {
        TSysUserVo objUserVo = new TSysUserVo();
        
        objUserVo = (TSysUserVo)BindControlsToObjectMode(objUserVo);
        objUserVo.ID = LogInfo.UserInfo.ID;

        TSysUserLogic logicUser = new TSysUserLogic(objUserVo);

        if (logicUser.Edit(objUserVo))
        {
            //使用LigerUIDialog之前，确保页面上LigerDialog.js/ligerui-all.css/all.css已被引用
            // LigerDialogConfirm("确定要进行删除吗?","alert(result);");

            LigerDialogAlert("修改成功！", DialogMold.success.ToString());
            base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUser, objUserVo.ID, LogInfo.UserInfo.USER_NAME + "修改用户： " + objUserVo.USER_NAME + " 的资料成功!");
        }
        else
        {
            LigerDialogAlert("修改失败！", DialogMold.warn.ToString());
        }
    }
    #endregion

    #region 事件
    /// <summary>
    /// 保存用户信息按钮点击事件
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
         SaveUserInfo();
    }
    #endregion
}