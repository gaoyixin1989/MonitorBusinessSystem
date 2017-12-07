<%@ WebService Language="C#" Class="cg" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using i3.ValueObject.Channels.OA.Message;
using i3.BusinessLogic.Channels.OA.Message;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class cg  : System.Web.Services.WebService {

    [WebMethod]
    public string selLst(string strUserID, string strIfConfirm, string strTaskName, string strSendUser, string strSendTimeBegin, string strSendTimeEnd, int intPageIndex, int intPageSize)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        string strState = TWfCommDict.StepState.StateNormal;//未办理 未确认
        if (strIfConfirm == "1")
            strState = TWfCommDict.StepState.StateConfirm;//已确认

        DataTable dt = logic.SelectByTableForUserTaskListEx_Mobile(strUserID, "PARTPLAN|WorkSubmit|OW_PARTPLAN|EP_PARTPLAN", strState, strTaskName, strSendUser, strSendTimeBegin, strSendTimeEnd, intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCountForUserTaskListEx_Mobile(strUserID, "PARTPLAN|WorkSubmit|OW_PARTPLAN|EP_PARTPLAN", strState, strTaskName, strSendUser, strSendTimeBegin, strSendTimeEnd);
        string strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string getAllCgInfo(string strID, string strWF_INST_ID, string strWF_TASK_ID, string strWF_ID)
    {
        string strReturn = "{";
        strReturn += getCgInfo(strID, strWF_INST_ID);
        strReturn += ",";

        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });
        string strCommandList = task.COMMAND_NAME;
        strReturn += "'fun':'" + strCommandList + "',";

        strReturn += "'ifEnd':'" + ifEndStep(strWF_TASK_ID, strWF_ID) + "'";

        strReturn += "}";

        return strReturn;
    }

    [WebMethod]
    public string getFunction(string strWF_TASK_ID, string strWF_ID)
    {
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });
        string strCommandList = task.COMMAND_NAME;
        return "{'fun':'" + strCommandList + "'}";
    }

    [WebMethod]
    public string getNextUser(string strWF_TASK_ID, string strWF_ID)
    {
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });
        TWfSettingTaskVo nextStep = new TWfSettingTaskLogic().GetNextStep(task);

        string strUserList = "";
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        DataTable dtUserInfo = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0", IS_USE = "1" });
        //增加限定处理人员信息，先将以职位存储的数据转换为系统用户
        if (nextStep.OPER_TYPE == "02")
        {
            foreach (string strPost in nextStep.OPER_VALUE.Split('|'))
                foreach (DataRow dr in dtUserPost.Rows)
                    if (dr[TSysUserPostVo.POST_ID_FIELD].ToString() == strPost)
                        if (strUserList.IndexOf(dr[TSysUserPostVo.USER_ID_FIELD].ToString()) < 0)
                        {
                            strUserList += dr[TSysUserPostVo.USER_ID_FIELD].ToString() + "|";
                            continue;
                        }
        }
        else
            strUserList = nextStep.OPER_VALUE;

        string strReturn = "[";
        foreach (string strUserID in strUserList.Split('|'))
        {
            foreach (DataRow dr in dtUserInfo.Rows)
                if (dr[TSysUserVo.ID_FIELD].ToString() == strUserID)
                {
                    string UserJson = "{'UserID':'" + strUserID + "','UserName':'" + dr[TSysUserVo.REAL_NAME_FIELD].ToString() + "'}";
                    strReturn += (strReturn.Length > 1 ? "," : "") + UserJson;
                }
        }
        strReturn += "]";

        return strReturn;
    }

    [WebMethod]
    public string getZUser(string strWF_TASK_ID, string strWF_ID)
    {
        TWfSettingTaskVo nextStep = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });

        string strUserList = "";
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        DataTable dtUserInfo = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0", IS_USE = "1" });

        //增加限定处理人员信息，先将以职位存储的数据转换为系统用户
        if (nextStep.OPER_TYPE == "02")
        {
            foreach (string strPost in nextStep.OPER_VALUE.Split('|'))
                foreach (DataRow dr in dtUserPost.Rows)
                    if (dr[TSysUserPostVo.POST_ID_FIELD].ToString() == strPost)
                        if (strUserList.IndexOf(dr[TSysUserPostVo.USER_ID_FIELD].ToString()) < 0)
                        {
                            strUserList += dr[TSysUserPostVo.USER_ID_FIELD].ToString() + "|";
                            continue;
                        }
        }
        else
            strUserList = nextStep.OPER_VALUE;

        string strReturn = "[";
        foreach (string strUserID in strUserList.Split('|'))
        {
            foreach (DataRow dr in dtUserInfo.Rows)
                if (dr[TSysUserVo.ID_FIELD].ToString() == strUserID)
                {
                    string UserJson = "{'UserID':'" + strUserID + "','UserName':'" + dr[TSysUserVo.REAL_NAME_FIELD].ToString() + "'}";
                    strReturn += (strReturn.Length > 1 ? "," : "") + UserJson;
                }
        }
        strReturn += "]";

        return strReturn;
    }

    [WebMethod]
    public string isEndStep(string strWF_TASK_ID, string strWF_ID)
    {
        return ifEndStep(strWF_TASK_ID, strWF_ID);
    }

    private string ifEndStep(string strWF_TASK_ID, string strWF_ID)
    {
        string strStep1 = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID }).WF_TASK_ID;
        DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strWF_ID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
        if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strStep1)
        {
            return "1";
        }
        return "0";
    }

    [WebMethod]
    public string setTaskConfirm(string strID, string strUserID)
    {
        TWfInstTaskDetailVo ttdv = new TWfInstTaskDetailVo();
        ttdv.ID = strID;

        ttdv.CFM_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ttdv.INST_TASK_STATE = TWfCommDict.StepState.StateConfirm;
        ttdv.CFM_USER = strUserID;

        return new TWfInstTaskDetailLogic().Edit(ttdv) ? "1" : "0";
    }

    [WebMethod]
    public string getInfo(string strID, string strWF_INST_ID)
    {
        string strReturn = "{" + getCgInfo(strID, strWF_INST_ID) + "}";

        return strReturn;
    }

    private string getCgInfo(string strID, string strWF_INST_ID)
    {
        string strObjId = new TWfInstTaskServiceLogic().Details(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }).SERVICE_KEY_VALUE;
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailLogic().Details(strID);

        TOaPartBuyRequstVo objPart = new TOaPartBuyRequstLogic().Details(strObjId);

        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = strObjId;
        TOaAttVo.BUSINESS_TYPE = "PartFile";
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string fileName = TOaAttVoTemp.ATTACH_NAME + TOaAttVoTemp.ATTACH_TYPE;//客户端保存的文件名 
        string filePath = mastPath + '\\' + TOaAttVoTemp.UPLOAD_PATH;
        string strHasAtt = "1";
        string strMobileFileName = "";
        if (File.Exists(filePath) == false)
        {
            strHasAtt = "0";
        }
        else
        {
            string serverPath = HttpRuntime.AppDomainAppPath + "TempFile";
            DateTime dtNow = System.DateTime.Now;
            string strDatetimeStr = dtNow.Year.ToString() + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');
            strMobileFileName = "Part" + strObjId + strDatetimeStr + TOaAttVoTemp.ATTACH_TYPE;
            try
            {
                File.Copy(filePath, serverPath + "\\" + strMobileFileName, true);
            }
            catch (Exception ex) { }
        }

        string strReturn = "";
        if (objTaskDetail.WF_ID == "PARTPLAN" || objTaskDetail.WF_ID == "OW_PARTPLAN" || objTaskDetail.WF_ID == "EP_PARTPLAN")
        {
            strReturn += "'采购类别':'领料单',";
            strReturn += "'申请部门':'" + new TSysDictLogic().Details(new TSysDictVo() { DICT_TYPE = "dept", DICT_CODE = objPart.APPLY_DEPT_ID }).DICT_TEXT + "',";
            strReturn += "'申请人':'" + new TSysUserLogic().Details(objPart.APPLY_USER_ID).REAL_NAME + "',";
            strReturn += "'申请日期':'" + objPart.APPLY_DATE + "',";
            strReturn += "'申请信息':'" + objPart.REMARK4 + "',";
        }
        else if (objTaskDetail.WF_ID == "WorkSubmit")
        {
            strReturn += "'采购类别':'工作呈报单',";
            strReturn += "'呈报部门':'" + new TSysDictLogic().Details(new TSysDictVo() { DICT_TYPE = "dept", DICT_CODE = objPart.APPLY_DEPT_ID }).DICT_TEXT + "',";
            strReturn += "'经办人':'" + new TSysUserLogic().Details(objPart.APPLY_USER_ID).REAL_NAME + "',";
            strReturn += "'日期':'" + objPart.APPLY_DATE + "',";
            strReturn += "'呈报信息':'" + objPart.REMARK4 + "',";
        }
        strReturn += "'HasAtt':'" + strHasAtt + "',";
        strReturn += "'AttFileName':'" + fileName + "',";
        strReturn += "'AttFilePath':'TempFile/" + strMobileFileName + "'";

        strReturn += "";

        return strReturn;
    }

    [WebMethod]
    public string getAppInfo(string strID, string strWF_INST_ID)
    {
        string strObjId = new TWfInstTaskServiceLogic().Details(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }).SERVICE_KEY_VALUE;

        TOaPartBuyRequstVo objPart = new TOaPartBuyRequstLogic().Details(strObjId);
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailLogic().Details(strID);

        string strReturn = "[";
        if (objTaskDetail.WF_ID == "PARTPLAN" || objTaskDetail.WF_ID == "OW_PARTPLAN" || objTaskDetail.WF_ID == "EP_PARTPLAN")
        {
            strReturn += "{";
            strReturn += "'step':'科室主任审核',";
            strReturn += "'appInfo':'" + objPart.APP_DEPT_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_DEPT_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_DEPT_DATE + "'";
            strReturn += "},";
            strReturn += "{";
            strReturn += "'step':'仓库管理员审核',";
            strReturn += "'appInfo':'" + objPart.APP_MANAGER_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_MANAGER_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_MANAGER_DATE + "'";
            strReturn += "}";
        }
        else if (objTaskDetail.WF_ID == "WorkSubmit")
        {
            strReturn += "{";
            strReturn += "'step':'科室意见',";
            strReturn += "'appInfo':'" + objPart.APP_DEPT_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_DEPT_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_DEPT_DATE + "'";
            strReturn += "},";
            strReturn += "{";
            strReturn += "'step':'主管领导意见',";
            strReturn += "'appInfo':'" + objPart.APP_MANAGER_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_MANAGER_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_MANAGER_DATE + "'";
            strReturn += "},";
            strReturn += "{";
            strReturn += "'step':'办公室意见',";
            strReturn += "'appInfo':'" + objPart.APP_OFFER_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_OFFER_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_OFFER_TIME + "'";
            strReturn += "},";
            strReturn += "{";
            strReturn += "'step':'站长意见',";
            strReturn += "'appInfo':'" + objPart.APP_LEADER_INFO + "',";
            strReturn += "'UserName':'" + new TSysUserLogic().Details(objPart.APP_LEADER_ID).REAL_NAME + "',";
            strReturn += "'appTime':'" + objPart.APP_LEADER_DATE + "'";
            strReturn += "}";
        }
        strReturn += "]";

        return strReturn;
    }

    [WebMethod]
    public string sendTask(string strID, string strWF_INST_ID, string strWF_ID, string strWF_TASK_ID, string strUserID, string strAppInfo, string strSendUserId)
    {
        string strObjId = new TWfInstTaskServiceLogic().Details(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }).SERVICE_KEY_VALUE;
        string strIsSendOrBack = "1";
        string strSERVICE_ROW_SIGN = "";

        //------------ 发文信息 start
        bool isSuccess = saveFwWhenSendOrBack(strID,strObjId, strIsSendOrBack, strAppInfo, strUserID, ref strSERVICE_ROW_SIGN);
        //------------ 发文信息 end

        //------------ 工作流 begin
        string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //开始组装新数据结构，插入数据库，然后更新控制表和当前环节明细表，即可
        List<string> strUserList = new List<string>();
        strUserList.Add(strSendUserId);

        //人员找到了，可以找生成数据了
        TWfSettingTaskVo taskNext = new TWfSettingTaskLogic().GetNextStep(new TWfSettingTaskVo() { WF_ID = strWF_ID, WF_TASK_ID = strWF_TASK_ID });
        List<TWfInstTaskDetailVo> detailList = new List<TWfInstTaskDetailVo>();

        //新的实例环节
        TWfInstTaskDetailVo taskTemp = new TWfInstTaskDetailVo();
        taskTemp.ID = this.GetGUID();
        taskTemp.INST_TASK_CAPTION = taskNext.TASK_CAPTION;
        taskTemp.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        taskTemp.INST_TASK_STARTTIME = strDateTimeNow;
        taskTemp.OBJECT_USER = strSendUserId;
        taskTemp.PRE_INST_TASK_ID = strID;
        taskTemp.PRE_TASK_ID = strWF_TASK_ID;
        taskTemp.REAL_USER = "";
        taskTemp.INST_NOTE = taskNext.TASK_NOTE;
        taskTemp.WF_ID = strWF_ID;
        taskTemp.WF_INST_ID = strWF_INST_ID;//这里应该填入寄存的流程实例编号
        taskTemp.WF_SERIAL_NO = new TWfInstControlLogic().Details(strWF_INST_ID).WF_SERIAL_NO;
        taskTemp.WF_TASK_ID = taskNext.WF_TASK_ID;
        //增加一个发送人的数据，发送人就是上个环节的实际处理人
        taskTemp.SRC_USER = strUserID;
        detailList.Add(taskTemp);

        //目前实例环节
        TWfInstTaskDetailVo INST_STEP_DETAIL = new TWfInstTaskDetailVo();
        INST_STEP_DETAIL.ID = strID;
        INST_STEP_DETAIL.INST_TASK_ENDTIME = strDateTimeNow;
        INST_STEP_DETAIL.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        INST_STEP_DETAIL.INST_TASK_DEAL_STATE = "";
        INST_STEP_DETAIL.REAL_USER = strUserID;

        //开始更新control表
        TWfInstControlVo INST_WF_CONTROL = new TWfInstControlVo();
        INST_WF_CONTROL.ID = strWF_INST_ID;
        INST_WF_CONTROL.WF_TASK_ID = taskNext.ID;
        INST_WF_CONTROL.WF_INST_TASK_ID = taskTemp.ID;

        //最后一环节
        bool bIsEndStep = (ifEndStep(strWF_TASK_ID, strWF_ID) == "1");
        if (bIsEndStep)
        {
            INST_WF_CONTROL.WF_ENDTIME = strDateTimeNow;
            INST_WF_CONTROL.WF_STATE = TWfCommDict.WfState.StateFinish;
            //结束节点，清除所有新增的环节信息
            detailList.Clear();
            //如果是最后一个环节，则此节点直接为空
            INST_WF_CONTROL.WF_INST_TASK_ID = i3.ValueObject.ConstValues.SpecialCharacter.EmptyValuesFillChar;
        }

        //开始更新所有数据
        TWfInstTaskDetailLogic logicTemp = new TWfInstTaskDetailLogic();
        //如果没有相关数据，则生成，如果存在则更新
        if (string.IsNullOrEmpty(logicTemp.Details(INST_STEP_DETAIL.ID).ID))
            logicTemp.Create(INST_STEP_DETAIL);
        else
            logicTemp.Edit((new TWfInstTaskDetailVo()
            {
                INST_TASK_ENDTIME = INST_STEP_DETAIL.INST_TASK_ENDTIME,
                INST_TASK_STATE = INST_STEP_DETAIL.INST_TASK_STATE,
                REAL_USER = INST_STEP_DETAIL.REAL_USER,
                INST_TASK_DEAL_STATE = INST_STEP_DETAIL.INST_TASK_DEAL_STATE,
                ID = strID
            }));

        TWfInstControlLogic logicWf = new TWfInstControlLogic();
        if (string.IsNullOrEmpty(logicWf.Details(new TWfInstControlVo() { ID = INST_WF_CONTROL.ID }).ID))
            logicWf.Create(INST_WF_CONTROL);
        else
            logicWf.Edit(new TWfInstControlVo()
            {
                ID = strWF_INST_ID,
                WF_TASK_ID = INST_WF_CONTROL.WF_TASK_ID,
                WF_STATE = INST_WF_CONTROL.WF_STATE,
                WF_ENDTIME = INST_WF_CONTROL.WF_ENDTIME,
                WF_INST_TASK_ID = INST_WF_CONTROL.WF_INST_TASK_ID
            });

        //写入流程产生的新数据
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            //在添加环节数据的同时，也需要将所有的业务数据都增加入数据库
            logicTemp.Create(td);
        }

        //业务数据。
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
            stemp.ID = this.GetGUID();
            stemp.WF_INST_ID = td.WF_INST_ID;
            stemp.WF_INST_TASK_ID = td.ID;
            stemp.SERVICE_NAME = "发文ID";
            stemp.SERVICE_KEY_NAME = "fw_id";
            stemp.SERVICE_KEY_VALUE = strObjId;
            stemp.SERVICE_ROW_SIGN = strSERVICE_ROW_SIGN;
            new TWfInstTaskServiceLogic().Create(stemp);
        }
        //------------ 工作流 end

        //----------短信begin
        if (!bIsEndStep)
        {
            string strMobileMsgContent = new TSysUserLogic().Details(strUserID).REAL_NAME + "给您发送了一条" + taskNext.TASK_CAPTION + "任务，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = strUserID;//发送人的user id
            string strMobileAccUserIDs = "";//接收人的USER ID，如果多个用逗号分隔
            for (int i = 0; i < strUserList.Count; i++)
            {
                if (strUserList[i].Length > 0)
                    strMobileAccUserIDs += (strMobileAccUserIDs.Length > 0 ? "," : "") + strUserList[i];
            }

            string strMobileErrMsg = "";
            //手机短信发送函数
            new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }
        //----------短信end

        if (isSuccess)
            return "1";
        else
            return "0";
    }

    [WebMethod]
    public string backTask(string strID, string strWF_INST_ID, string strWF_ID, string strWF_TASK_ID, string strUserID, string strAppInfo)
    {
        string strObjId = new TWfInstTaskServiceLogic().Details(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }).SERVICE_KEY_VALUE;
        string strIsSendOrBack = "0";
        string strSERVICE_ROW_SIGN = "";

        //------------ 发文信息 start
        bool isSuccess = saveFwWhenSendOrBack(strID, strObjId, strIsSendOrBack, strAppInfo, strUserID, ref strSERVICE_ROW_SIGN);
        //------------ 发文信息 end

        //------------ 工作流 begin
        string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        TWfInstTaskDetailLogic instTaskLogic = new TWfInstTaskDetailLogic();
        TWfInstTaskDetailVo taskInst = instTaskLogic.Details(new TWfInstTaskDetailVo() { ID = strID });
        //如果是跳转后的回退，则直接退回至上一个环节，就是跳转前的页面，而不是设定的前一个环节
        TWfSettingTaskVo taskPreSetting = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = taskInst.WF_ID, ID = taskInst.PRE_TASK_ID });
        TWfInstTaskDetailVo taskPre = instTaskLogic.Details(new TWfInstTaskDetailVo() { ID = taskInst.PRE_INST_TASK_ID });
        TWfInstControlVo wfInst = new TWfInstControlLogic().Details(taskInst.WF_INST_ID);

        //根据上一个环节的数据来重新生成所有数据
        TWfInstTaskDetailVo taskNew = new TWfInstTaskDetailVo();
        taskNew.ID = this.GetGUID();
        taskNew.INST_NOTE = taskPreSetting.TASK_NOTE;
        taskNew.INST_TASK_CAPTION = taskPreSetting.TASK_CAPTION;
        taskNew.INST_TASK_STARTTIME = strDateTimeNow;
        taskNew.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        taskNew.OBJECT_USER = taskPre.OBJECT_USER;//使用上环节的目标处理人
        taskNew.PRE_INST_TASK_ID = taskInst.ID;//上一个环节的编号，将成为本环节的上环节编号
        //涉及到连环退回的情况，只有一种可能会出错，就是退到起始节点了，还在退的情况，而这种情况是由配置不当引起，此刻不做处理。
        taskNew.PRE_TASK_ID = new TWfSettingTaskLogic().GetPreStep(new TWfSettingTaskVo() { WF_ID = taskInst.WF_ID, ID = taskPreSetting.WF_TASK_ID }).ID;
        taskNew.WF_ID = taskPreSetting.WF_ID;
        taskNew.WF_INST_ID = taskPre.WF_INST_ID;
        taskNew.WF_SERIAL_NO = taskPre.WF_SERIAL_NO;
        taskNew.WF_TASK_ID = taskPreSetting.ID;
        taskNew.SRC_USER = strUserID;//增加一个发送人数据

        //将原环节表的标志位更新为完成
        taskInst.REAL_USER = strUserID;
        taskInst.INST_TASK_ENDTIME = strDateTimeNow;
        taskInst.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        taskInst.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForBack;

        //更新控制表信息
        //退回时要把附件和评论信息放入数据库，业务数据也要全部退回
        //写入流程产生的新数据
        instTaskLogic.Create(taskNew);
        instTaskLogic.Edit(new TWfInstTaskDetailVo()
        {
            ID = taskInst.ID,
            INST_TASK_ENDTIME = taskInst.INST_TASK_ENDTIME,
            INST_TASK_STATE = taskInst.INST_TASK_STATE,
            INST_TASK_DEAL_STATE = taskInst.INST_TASK_DEAL_STATE,
            REAL_USER = taskInst.REAL_USER
        });

        TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
        instWFLogic.Edit(new TWfInstControlVo()
        {
            ID = wfInst.ID,
            WF_INST_TASK_ID = taskNew.ID,
            WF_TASK_ID = taskNew.WF_TASK_ID
        });

        //业务数据不需要整合，已预留对象供前台整理。
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();

        //业务数据。
        List<TWfInstTaskServiceVo> INST_STEP_SERVICE_LIST_FOR_OLD = new TWfInstTaskServiceLogic().SelectByObject(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }, 0, 100);
        if (null != INST_STEP_SERVICE_LIST_FOR_OLD)
            foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST_FOR_OLD)
            {
                //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                stemp.ID = this.GetGUID();
                stemp.WF_INST_ID = wfInst.ID;
                stemp.WF_INST_TASK_ID = taskNew.ID;
                stemp.SERVICE_NAME = service.SERVICE_NAME;
                stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                serviceList.Add(stemp);
            }

        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);
        //------------ 工作流 end

        //------------ 手机短信 beigin
        TWfInstControlVo INST_WF_CONTROL = new TWfInstControlLogic().Details(strWF_INST_ID);
        if (true)
        {
            string strMobileMsgContent = new TSysUserLogic().Details(strUserID).REAL_NAME + "给您退回了一条" + taskPreSetting.TASK_CAPTION + "任务，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = strUserID;//发送人的user id
            string strMobileAccUserIDs = taskPre.OBJECT_USER;//接收人的USER ID

            string strMobileErrMsg = "";
            //手机短信发送函数
            new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }
        //------------ 手机短信 end

        if (isSuccess)
            return "1";
        else
            return "0";
    }

    [WebMethod]
    public string zSendTask(string strID, string strWF_INST_ID, string strWF_ID, string strWF_TASK_ID, string strUserID, string strSendUserId)
    {
        List<string> strUserList = new List<string>();
        strUserList.Add(strSendUserId);
        string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //转发的意思就是把此环节的数据copyN份后，生成N份数据，系统还在当前环节
        //如果后续环节有路由环节的话，则还要遵循路由环节。如果没有路由环节，则相当于传阅的概念；
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = strWF_ID, WF_TASK_ID = strWF_TASK_ID });
        TWfInstTaskDetailVo taskInst = new TWfInstTaskDetailLogic().Details(new TWfInstTaskDetailVo() { ID = strID });

        //更新现有环节，将标志更新为
        taskInst.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        taskInst.INST_TASK_ENDTIME = strDateTimeNow;
        taskInst.REAL_USER = strUserID;
        taskInst.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForZSend;

        List<TWfInstTaskDetailVo> detailList = new List<TWfInstTaskDetailVo>();
        foreach (string strtmpUserID in strUserList)
        {
            TWfInstTaskDetailVo taskTemp = new TWfInstTaskDetailVo();
            taskTemp.ID = this.GetGUID();
            taskTemp.INST_TASK_CAPTION = task.TASK_CAPTION;
            taskTemp.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            taskTemp.INST_TASK_STARTTIME = strDateTimeNow;
            taskTemp.OBJECT_USER = strtmpUserID;
            taskTemp.PRE_INST_TASK_ID = taskInst.ID;//这里有一个衔接，上一个环节实例就是被转发的那个环节实例，这样可以追踪到
            taskTemp.PRE_TASK_ID = taskInst.PRE_TASK_ID;
            taskTemp.REAL_USER = "";
            taskTemp.INST_NOTE = task.TASK_NOTE;
            taskTemp.WF_ID = taskInst.WF_ID;
            taskTemp.WF_INST_ID = taskInst.WF_INST_ID;//这里应该填入寄存的流程实例编号
            taskTemp.WF_SERIAL_NO = taskInst.WF_SERIAL_NO;
            taskTemp.WF_TASK_ID = taskInst.WF_TASK_ID;
            //然后补充新节点的发送人
            taskTemp.SRC_USER = taskInst.REAL_USER;//增加一个发送人数据

            detailList.Add(taskTemp);
        }

        //先更新现有环节的数据
        TWfInstTaskDetailLogic logicTemp = new TWfInstTaskDetailLogic();
        logicTemp.Edit((new TWfInstTaskDetailVo()
        {
            INST_TASK_ENDTIME = taskInst.INST_TASK_ENDTIME,
            INST_TASK_STATE = taskInst.INST_TASK_STATE,
            REAL_USER = taskInst.REAL_USER,
            INST_TASK_DEAL_STATE = taskInst.INST_TASK_DEAL_STATE,
            ID = taskInst.ID
        }));

        TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
        instWFLogic.Edit(new TWfInstControlVo()
        {
            ID = detailList[0].WF_INST_ID,
            WF_INST_TASK_ID = detailList[0].ID,
            WF_TASK_ID = detailList[0].WF_TASK_ID
        });

        //写入流程产生的新数据
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            logicTemp.Create(td);
        }

        //业务数据
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();
        List<TWfInstTaskServiceVo> INST_STEP_SERVICE_LIST_FOR_OLD = new TWfInstTaskServiceLogic().SelectByObject(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = strID, WF_INST_ID = strWF_INST_ID }, 0, 100);
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            if (null != INST_STEP_SERVICE_LIST_FOR_OLD)
                foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST_FOR_OLD)
                {
                    //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                    TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                    stemp.ID = this.GetGUID();
                    stemp.WF_INST_ID = td.WF_INST_ID;
                    stemp.WF_INST_TASK_ID = td.ID;
                    stemp.SERVICE_NAME = service.SERVICE_NAME;
                    stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                    stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                    stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                    serviceList.Add(stemp);
                }
        }
        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);

        //短信
        if (true)
        {
            TWfInstControlVo INST_WF_CONTROL = new TWfInstControlLogic().Details(strWF_INST_ID);
            string strMobileMsgContent = new TSysUserLogic().Details(strUserID).REAL_NAME + "给您转发了一条" + task.TASK_CAPTION + "任务，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = strUserID;//发送人的user id
            string strMobileAccUserIDs = "";//接收人的USER ID，如果多个用逗号分隔
            for (int i = 0; i < strUserList.Count; i++)
            {
                if (strUserList[i].Length > 0)
                    strMobileAccUserIDs += (strMobileAccUserIDs.Length > 0 ? "," : "") + strUserList[i];
            }

            string strMobileErrMsg = "";
            //手机短信发送函数
            new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }

        return "1";
    }

    private bool saveFwWhenSendOrBack(string strID,string strObjId, string strIsSendOrBack, string strAppInfo, string strUserID, ref string strSERVICE_ROW_SIGN)
    {
        TOaPartBuyRequstVo objPart = new TOaPartBuyRequstLogic().Details(strObjId);
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailLogic().Details(strID);
        TWfSettingTaskVo objSetTask = new TWfSettingTaskLogic().Details(objTaskDetail.WF_TASK_ID);
        string strTask_Order = objSetTask.TASK_ORDER;

        objPart = new TOaPartBuyRequstVo();
        objPart.ID = strObjId;

        string strRemark1 = (objTaskDetail.WF_ID == "PARTPLAN") ? "0" : "1";
        if (string.IsNullOrEmpty(strTask_Order))
        {
            objPart.STATUS = "1";
            objPart.REMARK1 = strRemark1;
            strSERVICE_ROW_SIGN = "1";
        }
        if (strTask_Order.Equals("2"))
        {
            objPart.APP_DEPT_INFO = strAppInfo;
            objPart.APP_DEPT_ID = strUserID;
            objPart.APP_DEPT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objPart.REMARK1 = strRemark1;
            strSERVICE_ROW_SIGN = "2";
        }
        if (strTask_Order.Equals("3"))
        {
            objPart.APP_MANAGER_INFO = strAppInfo;
            objPart.APP_MANAGER_ID = strUserID;
            objPart.APP_MANAGER_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (objTaskDetail.WF_ID == "PARTPLAN")
                objPart.STATUS = "9";
            objPart.REMARK1 = strRemark1;
            strSERVICE_ROW_SIGN = "3";
        }
        if (strTask_Order.Equals("4"))
        {
            objPart.APP_OFFER_INFO = strAppInfo;
            objPart.APP_OFFER_ID = strUserID;
            objPart.APP_OFFER_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objPart.REMARK1 = strRemark1;
            strSERVICE_ROW_SIGN = "3";
        }
        if (strTask_Order.Equals("5"))
        {
            objPart.APP_LEADER_INFO = strAppInfo;
            objPart.APP_LEADER_ID = strUserID;
            objPart.APP_LEADER_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objPart.STATUS = "9";
            objPart.REMARK1 = strRemark1;
            strSERVICE_ROW_SIGN = "3";
        }
        
        return new TOaPartBuyRequstLogic().Edit(objPart);
    }
    
    private string GetGUID()
    {
        long i = 1;
        Guid guid = Guid.NewGuid();
        foreach (byte b in guid.ToByteArray())
            i *= ((int)b + 1);
        return string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
    }
}