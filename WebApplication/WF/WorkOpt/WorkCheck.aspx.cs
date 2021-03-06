﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.DA;
using BP.WF.Template;
using BP.Sys;
using BP.Web;
using BP.En;
using BP.Web.Controls;

namespace CCFlow.WF.WorkOpt
{
    public partial class FrmWorkCheck : BP.Web.WebPage
    {
        #region 属性
        public bool IsHidden
        {
            get
            {
                try
                {
                    if (DoType == "View")
                        return true;
                    return bool.Parse(Request["IsHidden"]);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public int NodeID
        {
            get
            {
                try
                {
                    return int.Parse(this.Request.QueryString["FK_Node"]);
                }
                catch
                {
                    return int.Parse(this.FK_Flow + "01");
                }
            }
        }
        /// <summary>
        /// 工作ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                string workid = this.Request.QueryString["OID"];
                if (workid == null)
                    workid = this.Request.QueryString["WorkID"];
                return Int64.Parse(workid);
            }
        }
        public Int64 FID
        {
            get
            {
                string workid = this.Request.QueryString["FID"];
                if (string.IsNullOrEmpty(workid) == true)
                    return 0;
                return Int64.Parse(workid);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public int FK_Node
        {
            get
            {
                return this.NodeID;
            }
        }
        /// <summary>
        /// 操作View
        /// </summary>
        public string DoType
        {
            get
            {
                return this.Request.QueryString["DoType"];
            }
        }
        /// <summary>
        /// 是否是抄送.
        /// </summary>
        public bool IsCC
        {
            get
            {
                string s = this.Request.QueryString["Paras"];
                if (s == null)
                    return false;

                if (s.Contains("IsCC") == true)
                    return true;
                return false;
            }
        }
        #endregion 属性

        protected void Page_Load(object sender, EventArgs e)
        {
            //工作流编号不存在绑定空框架.
            if (this.FK_Flow == null)
            {
                //   ViewEmptyForm();
                return;
            }

            //审批节点.
            BP.Sys.FrmWorkCheck wcDesc = new BP.Sys.FrmWorkCheck(this.NodeID);

            if (wcDesc.HisFrmWorkShowModel == BP.Sys.FrmWorkShowModel.Free)
                this.BindFreeModel(wcDesc);
            else if (wcDesc.HisFrmWorkShowModel == BP.Sys.FrmWorkShowModel.Table)
                this.BindFreeModel(wcDesc);
        }
        /// <summary>
        /// 实现的功能：
        /// 1，显示轨迹表。
        /// 2，如果启用了审核，就把审核信息显示出来。
        /// 3，如果启用了抄送，就把抄送的人显示出来。
        /// 4，可以把子流程的信息与处理的结果显示出来。
        /// 5，可以把子线程的信息列出来。
        /// 6，可以把未来到达节点处理人显示出来。
        /// </summary>
        /// <param name="wcDesc"></param>
        public void BindFreeModel(BP.Sys.FrmWorkCheck wcDesc)
        {
            BP.WF.WorkCheck wc = null;
            if (FID != 0)
                wc = new WorkCheck(this.FK_Flow, this.NodeID, this.FID, 0);
            else
                wc = new WorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID);

            bool isCanDo = BP.WF.Dev2Interface.Flow_IsCanDoCurrentWork(this.FK_Flow, this.NodeID, this.WorkID, BP.Web.WebUser.No);

            #region 输出历史审核信息.
            if (wcDesc.FWCListEnable == true)
            {
                //求轨迹表.
                BP.WF.Tracks tks = wc.HisWorkChecks;

                //求抄送列表,把抄送的信息与抄送的读取状态显示出来.
                CCLists ccls = new CCLists(this.FK_Flow, this.WorkID, this.FID);

                //查询出来未来节点处理人信息,以方便显示未来没有运动到节点轨迹.
                Int64 wfid = this.WorkID;
                if (this.FID != 0)
                    wfid = this.FID;

                //获得 节点处理人数据。
                SelectAccpers accepts = new SelectAccpers(wfid);

                //取出来该流程的所有的节点。
                Nodes nds = new Nodes(this.FK_Flow);
                Nodes ndsOrder = new Nodes();
                //求出已经出现的步骤.
                string nodes = ""; //已经出现的步骤.
                foreach (BP.WF.Track tk in tks)
                {
                    switch (tk.HisActionType)
                    {
                        //case ActionType.Forward:
                        case ActionType.WorkCheck:
                            if (nodes.Contains(tk.NDFrom + ",") == false)
                            {
                                nodes += tk.NDFrom + ",";
                            }
                            break;
                        case ActionType.StartChildenFlow:
                            if (nodes.Contains(tk.NDFrom + ",") == false)
                            {
                                nodes += tk.NDFrom + ",";
                            }
                            break;
                        default:
                            continue;
                    }
                }

                int biaoji = 0;
                bool tableBoolen = true;
                int count = 0;
                foreach (BP.WF.Node nd in nds)
                {
                    if (nodes.Contains(nd.NodeID + ",") == true)
                    {
                        //输出发送审核信息与抄送信息.
                        string emps = "";
                        string empsorder = "";    //保存队列显示中的人员，做判断，避免重复显示
                        string empcheck = "";   //记录当前节点已经输出的
                        foreach (Track tk in tks)
                        {
                            if (tk.NDFrom != nd.NodeID)
                                continue;

                            #region 如果是前进，并且当前节点没有启用审核组件
                            if (tk.HisActionType == ActionType.Forward)
                                continue;
                            #endregion

                            if (tk.HisActionType != ActionType.WorkCheck
                                && tk.HisActionType != ActionType.StartChildenFlow)
                                continue;

                            emps += tk.EmpFrom + ",";

                            if (tk.HisActionType == ActionType.WorkCheck)
                            {
                                #region 显示出来队列流程中未审核的那些人.
                                if (nd.TodolistModel == TodolistModel.Order)
                                {
                                    /* 如果是队列流程就要显示出来未审核的那些人.*/
                                    string empsNodeOrder = "";  //记录当前节点队列访问未执行的人员
                                    GenerWorkerLists gwls = new GenerWorkerLists(this.WorkID);
                                    foreach (GenerWorkerList item in gwls)
                                    {
                                        if (item.FK_Node == nd.NodeID)
                                        {
                                            empsNodeOrder += item.FK_Emp;
                                        }
                                    }

                                    foreach (SelectAccper accper in accepts)
                                    {
                                        if (empsorder.Contains(accper.FK_Emp) == true)
                                            continue;
                                        if (empsNodeOrder.Contains(accper.FK_Emp) == false)
                                            continue;
                                        if (tk.EmpFrom == accper.FK_Emp)
                                        {
                                            /* 审核信息,首先输出它.*/
                                            this.Pub1.Add(tk.MsgHtml);
                                            this.Pub1.Add("<img src='../Img/Mail_Read.png' border=0/>" + tk.ActionTypeText);
                                            this.Pub1.Add(tk.RDT);
                                            this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(tk.EmpFrom, tk.EmpFromT));
                                            this.Pub1.AddHR();
                                            empcheck += tk.EmpFrom;
                                        }
                                        else
                                        {
                                            this.Pub1.AddTR();
                                            if (accper.AccType == 0)
                                                this.Pub1.Add(" <font style='color:Red;' >执行</font>");
                                            else
                                                this.Pub1.Add(" <font style='color:Red;' >抄送</font>");
                                            this.Pub1.Add("无");
                                            this.Pub1.Add(" <font style='color:Red;' >" + BP.WF.Glo.GenerUserImgSmallerHtml(accper.FK_Emp, accper.EmpName) + "</font>");
                                            this.Pub1.Add(" <font style='color:Red;' >" + accper.Info + "</font>");
                                            this.Pub1.AddHR();
                                            empsorder += accper.FK_Emp;
                                        }
                                    }
                                }
                                #endregion 显示出来队列流程中未审核的那些人.
                                else
                                {
                                    //用户签名信息，显示签名or图片
                                    string sigantrueHtml = wcDesc.SigantureEnabel == true ? BP.WF.Glo.GenerUserSigantureHtml(tk.EmpFrom, tk.EmpFromT) : BP.WF.Glo.GenerUserImgSmallerHtml(tk.EmpFrom, tk.EmpFromT);
                                    //审核组件附件数据
                                    FrmAttachmentDBs athDBs = new FrmAttachmentDBs();
                                    QueryObject obj_Ath = new QueryObject(athDBs);
                                    obj_Ath.AddWhere(FrmAttachmentDBAttr.FK_FrmAttachment, tk.NDFrom + "_FrmWorkCheck");
                                    obj_Ath.addAnd();
                                    obj_Ath.AddWhere(FrmAttachmentDBAttr.RefPKVal, this.WorkID);
                                    obj_Ath.addOrderBy(FrmAttachmentDBAttr.RDT);
                                    obj_Ath.DoQuery();
                                    /*审核信息,首先输出它.*/
                                    #region 根据类型加载标题  表格  自由
                                    switch (wcDesc.HisFrmWorkShowModel)
                                    {
                                        case FrmWorkShowModel.Table://表格模式
                                            this.Pub1.AddTable("style='padding:0px;width:100%;table-layout: fixed;' leftMargin=0 topMargin=0");
                                            this.Pub1.AddTR(" style='background-color: #E2F6FB' ");
                                            if (wcDesc.HisFrmWorkCheckType == FWCType.Check)  //审核组件
                                            {
                                                this.Pub1.AddTD(nd.FWCNodeName);
                                            }
                                            if (wcDesc.HisFrmWorkCheckType == FWCType.DailyLog)//日志组件
                                            {
                                                this.Pub1.AddTD(DateTime.Parse(tk.RDT).ToString("yyyy-MM-dd") + "日志");
                                            }
                                            if (wcDesc.HisFrmWorkCheckType == FWCType.WeekLog)//周报
                                            {
                                                string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                                                DateTime a = DateTime.Parse(tk.RDT);
                                                string week = weekdays[Convert.ToInt32(a.DayOfWeek)];
                                                this.Pub1.AddTD(week + "周报");
                                            }
                                            if (wcDesc.HisFrmWorkCheckType == FWCType.MonthLog)//月报
                                            {
                                                this.Pub1.AddTD(DateTime.Parse(tk.RDT).ToString("yyyy-MM") + "月报");
                                            }

                                            this.Pub1.AddTREnd();
                                            //审核组件配置字段
                                            BP.Sys.FrmWorkCheck frmWorkCheck = new BP.Sys.FrmWorkCheck(tk.NDFrom);
                                            //存在审核组件配置字段，则不显示审核意见框
                                            if (!string.IsNullOrEmpty(frmWorkCheck.FWCFields))
                                            {
                                                AtPara ap = new AtPara(tk.Msg.Replace(";", "@"));
                                                //字段生成表单
                                                Attrs fwcAttrs = new Attrs(frmWorkCheck.FWCFields);
                                                this.Pub1.AddTR();
                                                this.Pub1.AddTDBegin();
                                                this.Pub1.BindAttrsForHtml(fwcAttrs, ap);
                                                this.Pub1.AddTDEnd();
                                                this.Pub1.AddTREnd();
                                            }
                                            else
                                            {
                                                //审核意见
                                                this.Pub1.AddTR();
                                                this.Pub1.Add("<td style='WORD-WRAP: break-word;min-height:80px;'>" + tk.MsgHtml + "</td>");
                                                this.Pub1.AddTREnd();
                                            }
                                            //附件
                                            AddTDOfFrmAttachMent(athDBs, tk);
                                            //签名与日期
                                            this.Pub1.AddTR();
                                            this.Pub1.Add("<td style='text-align:right;height:35px;'>签名:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sigantrueHtml + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期:&nbsp;&nbsp;&nbsp;" + tk.RDT + "</td>");
                                            this.Pub1.AddTREnd();
                                            this.Pub1.AddTableEnd();
                                            break;
                                        case FrmWorkShowModel.Free://自由模式 审核组件不一样
                                            this.Pub1.AddTable("style='padding:0px;width:100%;table-layout: fixed;' leftMargin=0 topMargin=0");
                                            //处理节点名称分组列，合并多少行
                                            int rowspan = 3;
                                            this.Pub1.AddTR();
                                            if (wcDesc.HisFrmWorkCheckType == FWCType.Check)
                                            {
                                                this.Pub1.Add("<td rowspan='" + rowspan + "' style='width:20px;border:1px solid #D6DDE6;'>" + nd.FWCNodeName + "</td>");
                                            }
                                            else
                                            {
                                                if (tableBoolen)
                                                {
                                                    this.Pub1.Add("<td rowspan='" + nds.Count * rowspan + "' style='width:20px;border:1px solid #D6DDE6;'>" + nd.FWCNodeName + "</td>");
                                                }
                                                if (!tableBoolen)
                                                {
                                                    this.Pub1.AddTR();
                                                }
                                            }
                                            //审核组件配置字段
                                            frmWorkCheck = new BP.Sys.FrmWorkCheck(tk.NDFrom);
                                            //存在审核组件配置字段，则不显示审核意见框
                                            if (!string.IsNullOrEmpty(frmWorkCheck.FWCFields))
                                            {
                                                AtPara ap = new AtPara(tk.Msg.Replace(";", "@"));
                                                //字段生成表单
                                                Attrs fwcAttrs = new Attrs(frmWorkCheck.FWCFields);
                                                this.Pub1.AddTDBegin();
                                                this.Pub1.BindAttrsForHtml(fwcAttrs, ap);
                                                this.Pub1.AddTDEnd();
                                                this.Pub1.AddTREnd();
                                            }
                                            else
                                            {
                                                //审核意见
                                                this.Pub1.Add("<td style='WORD-WRAP: break-word;min-height:80px;'>" + tk.MsgHtml + "</td>");
                                                this.Pub1.AddTREnd();
                                            }
                                            //附件
                                            AddTDOfFrmAttachMent(athDBs, tk);
                                            this.Pub1.AddTR();
                                            this.Pub1.Add("<td style=' text-align:right;height:35px;'>签名:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sigantrueHtml + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期:&nbsp;&nbsp;&nbsp;" + tk.RDT + "</td>");
                                            this.Pub1.AddTREnd();

                                            if (wcDesc.HisFrmWorkCheckType == FWCType.Check)
                                                this.Pub1.AddTableEnd();
                                            if (nds.Count - 1 == count)
                                                this.Pub1.AddTableEnd();

                                            tableBoolen = false;
                                            break;
                                        default:
                                            break;
                                    }
                                    #endregion
                                    count += 1;
                                    empcheck += tk.EmpFrom;
                                }
                            }

                            #region 检查是否有调用子流程的情况。如果有就输出调用子流程信息. (手机部分的翻译暂时不考虑).
                            // int atTmp = (int)ActionType.StartChildenFlow;
                            BP.WF.WorkCheck wc2 = new WorkCheck(FK_Flow, tk.NDFrom, tk.WorkID, tk.FID);
                            if (wc2.FID != 0)
                            {
                                if (tk.HisActionType == ActionType.StartChildenFlow)
                                {
                                    /*说明有子流程*/
                                    /*如果是调用子流程,就要从参数里获取到都是调用了那个子流程，并把他们显示出来.*/
                                    string[] paras = tk.Tag.Split('@');
                                    string[] p1 = paras[1].Split('=');
                                    string fk_flow = p1[1]; //子流程编号

                                    string[] p2 = paras[2].Split('=');
                                    string workId = p2[1]; //子流程ID.

                                    BP.WF.WorkCheck subwc = new WorkCheck(fk_flow, int.Parse(fk_flow + "01"), Int64.Parse(workId), 0);

                                    Tracks subtks = subwc.HisWorkChecks;

                                    //取出来子流程的所有的节点。
                                    Nodes subNds = new Nodes(fk_flow);
                                    foreach (BP.WF.Node item in subNds)     //主要按顺序显示
                                    {
                                        foreach (BP.WF.Track mysubtk in subtks)
                                        {
                                            if (item.NodeID != mysubtk.NDFrom)
                                                continue;
                                            /*输出该子流程的审核信息，应该考虑子流程的子流程信息, 就不考虑那样复杂了.*/
                                            if (mysubtk.HisActionType == ActionType.WorkCheck)
                                            {
                                                //biaojie  发起多个子流程时，发起人只显示一次
                                                if (mysubtk.NDFrom == int.Parse(fk_flow + "01") && biaoji == 1)
                                                    continue;

                                                /*如果是审核.*/
                                                this.Pub1.Add(mysubtk.ActionTypeText + "<img src='../Img/Mail_Read.png' border=0/>");
                                                this.Pub1.Add(mysubtk.RDT);
                                                this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(mysubtk.EmpFrom, mysubtk.EmpFromT));

                                                this.Pub1.AddBR();
                                                this.Pub1.Add(mysubtk.MsgHtml);
                                                this.Pub1.AddBR();
                                                if (mysubtk.NDFrom == int.Parse(fk_flow + "01"))
                                                {
                                                    biaoji = 1;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            #endregion 检查是否有调用子流程的情况。如果有就输出调用子流程信息.
                        }

                        foreach (SelectAccper item in accepts)
                        {
                            if (item.FK_Node != nd.NodeID)
                                continue;
                            if (empcheck.Contains(item.FK_Emp) == true)
                                continue;
                            if (item.AccType == 0)
                                continue;
                            if (ccls.IsExits(CCListAttr.FK_Node, nd.NodeID) == true)
                                continue;


                            this.Pub1.Add("<font style='color:Red;'>执行</font>");

                            this.Pub1.Add("<font style='color:Red;'>无</font>");

                            this.Pub1.Add("<font style='color:Red;'>" + BP.WF.Glo.GenerUserImgSmallerHtml(item.FK_Emp, item.EmpName) + "</font>");
                            this.Pub1.AddBR(item.Info);
                            this.Pub1.AddHR();
                        }

                        #region 输出抄送
                        foreach (SelectAccper item in accepts)
                        {
                            if (item.FK_Node != nd.NodeID)
                                continue;
                            if (item.AccType != 1)
                                continue;
                            if (ccls.IsExits(CCListAttr.FK_Node, nd.NodeID) == false)
                            {
                                this.Pub1.Add("<font style='color:Red;'> 抄送</font>");
                                this.Pub1.Add("<font style='color:Red;'> 无</font>");
                                // 显示要执行的人员。
                                this.Pub1.Add("<font style='color:Red;'> " + BP.WF.Glo.GenerUserImgSmallerHtml(item.FK_Emp, item.EmpName) + "</font>");

                                //info.
                                this.Pub1.Add("<font style='color:Red;'>" + item.Info + "</font>");
                                this.Pub1.AddHR();

                            }
                            else
                            {
                                foreach (CCList cc in ccls)
                                {
                                    if (cc.FK_Node != nd.NodeID)
                                        continue;

                                    if (cc.HisSta == CCSta.CheckOver)
                                        continue;
                                    if (cc.CCTo != item.FK_Emp)
                                        continue;

                                    this.Pub1.AddTR();
                                    if (cc.HisSta == CCSta.Read)
                                    {
                                        if (nd.IsEndNode == true)
                                        {
                                            this.Pub1.Add("<img src='../Img/Mail_Read.png' border=0/>抄送已阅");

                                            this.Pub1.Add(cc.CDT); //读取时间.
                                            this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(cc.CCTo, cc.CCToName));
                                            this.Pub1.AddBR();
                                            this.Pub1.Add(cc.CheckNoteHtml);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (BP.Web.WebUser.No == cc.CCTo)
                                        {
                                            continue;

                                            /*如果打开的是我,*/
                                            if (cc.HisSta == CCSta.UnRead)
                                                BP.WF.Dev2Interface.Node_CC_SetRead(cc.MyPK);
                                            this.Pub1.Add("<img src='../Img/Mail_Read.png' border=0/>正在查阅");

                                        }
                                        else
                                        {
                                            this.Pub1.Add("<img src='../Img/Mail_UnRead.png' border=0/>抄送未阅");
                                        }

                                        this.Pub1.Add("无");
                                        this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(cc.CCTo, cc.CCToName));
                                        this.Pub1.Add("无");
                                    }
                                    this.Pub1.AddHR();
                                }
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        if (wcDesc.FWCIsShowAllStep == false)
                            continue;

                        /*判断该节点下是否有人访问，或者已经设置了抄送与接收人对象, 如果没有就不输出*/
                        if (accepts.IsExits(SelectAccperAttr.FK_Node, nd.NodeID) == false)
                            continue;


                        /*未出现的节点.*/
                        this.Pub1.Add(nd.Name);

                        //是否输出了.
                        foreach (SelectAccper item in accepts)
                        {
                            if (item.FK_Node != nd.NodeID)
                                continue;
                            if (item.AccType != 0)
                                continue;

                            this.Pub1.Add("<font style='color:Red;'>执行</font>");
                            this.Pub1.Add("<font style='color:Red;'>无</font>");

                            // 显示要执行的人员。
                            this.Pub1.Add("<font style='color:Red;'>" + BP.WF.Glo.GenerUserImgSmallerHtml(item.FK_Emp, item.EmpName) + "</font>");

                            //info.
                            this.Pub1.Add("<font style='color:Red;'>" + item.Info + "</font>");
                            this.Pub1.AddHR();
                        }

                        #region 输出抄送
                        foreach (SelectAccper item in accepts)
                        {
                            if (item.FK_Node != nd.NodeID)
                                continue;
                            if (item.AccType != 1)
                                continue;
                            if (ccls.IsExits(CCListAttr.FK_Node, nd.NodeID) == false)
                            {
                                this.Pub1.Add("<font style='color:Red;'>抄送</font>");
                                this.Pub1.Add("<font style='color:Red;'>无</font>");
                                // 显示要执行的人员。
                                this.Pub1.Add("<font style='color:Red;'>" + BP.WF.Glo.GenerUserImgSmallerHtml(item.FK_Emp, item.EmpName) + "</font>");

                                //info.
                                this.Pub1.Add("<font style='color:Red;'>" + item.Info + "</font>");
                                this.Pub1.AddHR();
                            }
                            else
                            {
                                foreach (CCList cc in ccls)
                                {
                                    if (cc.FK_Node != nd.NodeID)
                                        continue;

                                    if (cc.HisSta == CCSta.CheckOver)
                                        continue;
                                    if (cc.CCTo != item.FK_Emp)
                                        continue;

                                    this.Pub1.AddTR();
                                    if (cc.HisSta == CCSta.Read)
                                    {
                                        if (nd.IsEndNode == true)
                                        {
                                            this.Pub1.Add("<img src='../Img/Mail_Read.png' border=0/>抄送已阅");
                                            this.Pub1.Add(cc.CDT); //读取时间.
                                            this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(cc.CCTo, cc.CCToName));
                                            this.Pub1.Add(cc.CheckNoteHtml);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (BP.Web.WebUser.No == cc.CCTo)
                                        {
                                            continue;

                                            /*如果打开的是我,*/
                                            if (cc.HisSta == CCSta.UnRead)
                                                BP.WF.Dev2Interface.Node_CC_SetRead(cc.MyPK);
                                            this.Pub1.Add("<img src='../Img/Mail_Read.png' border=0/>正在查阅");
                                        }
                                        else
                                        {
                                            this.Pub1.Add("<img src='../Img/Mail_UnRead.png' border=0/>抄送未阅");
                                        }

                                        this.Pub1.Add("无");
                                        this.Pub1.Add(BP.WF.Glo.GenerUserImgSmallerHtml(cc.CCTo, cc.CCToName));
                                        this.Pub1.Add("无");
                                    }
                                    this.Pub1.AddHR();
                                }
                            }
                        }
                        #endregion

                    }
                }
            } // 输出轨迹.
            #endregion 输出轨迹

            #region 处理审核意见框.
            if (IsHidden == false
                && wcDesc.HisFrmWorkCheckSta == BP.Sys.FrmWorkCheckSta.Enable
                && isCanDo)
            {
                string lab = wcDesc.FWCOpLabel;
                lab = lab.Replace("@WebUser.No", BP.Web.WebUser.No);
                lab = lab.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                lab = lab.Replace("@WebUser.FK_DeptName", BP.Web.WebUser.FK_DeptName);

                //批量上传附件实现
                System.Text.StringBuilder uploadJS = new System.Text.StringBuilder();
                if (wcDesc.FWCAth == FWCAth.MinAth)
                {
                    uploadJS.Append("<script language='javascript' type='text/javascript'> ");

                    uploadJS.Append("\t\n  $(function() {");
                    uploadJS.Append("\t\n $('#file_upload').uploadify({");
                    //对应多附件模板MyPK
                    string AttachPK = this.NodeID + "_FrmWorkCheck";

                    uploadJS.Append("\t\n 'swf': '" + BP.WF.Glo.CCFlowAppPath + "WF/Scripts/Jquery-plug/fileupload/uploadify.swf',");
                    uploadJS.Append("\t\n 'uploader':  '" + BP.WF.Glo.CCFlowAppPath + "WF/CCForm/CCFormHeader.ashx?AttachPK=" + AttachPK + "&WorkID=" + this.WorkID + "&DoType=MoreAttach&FK_Node=" + this.FK_Node + "&EnsName=" + this.EnName + "&FK_Flow=" + this.FK_Flow + "&PKVal=" + this.WorkID + "',");
                    uploadJS.Append("\t\n   'auto': true,");
                    uploadJS.Append("\t\n 'fileTypeDesc':'请选择上传文件',");
                    uploadJS.Append("\t\n 'buttonText':'上传附件',");
                    //uploadJS.Append("\t\n hideButton:true,");
                    uploadJS.Append("\t\n 'width'     :60,");
                    uploadJS.Append("\t\n   'fileTypeExts':'*.*',");
                    uploadJS.Append("\t\n  'height'    :18,");
                    uploadJS.Append("\t\n  'multi'     :true,");
                    uploadJS.Append("\t\n    'queueSizeLimit':999,");
                    uploadJS.Append("\t\n  'onDialogOpen': function (a,b) {");
                    uploadJS.Append("\t\n   },");
                    uploadJS.Append("\t\n  'onQueueComplete': function (queueData ) {");
                    uploadJS.Append("\t\n       isChange = true;");
                    uploadJS.Append("\t\n       SaveDtlData();");
                    uploadJS.Append("\t\n  },");
                    uploadJS.Append("\t\n  'removeCompleted' : true");
                    uploadJS.Append("\t\n });");
                    uploadJS.Append("\t\n });");
                    uploadJS.Append("\t\n </script>");
                    uploadJS.Append("<div id='file_upload-queue' class='uploadify-queue'></div>");
                    uploadJS.Append("<div id='s' style='text-align:right;float:right;margin-right:10px;' ><input type='file' name='file_upload' id='file_upload' width='60' height='30' /></div>");
                }

                //意见输入框
                PostBackTextBox tb = new PostBackTextBox();
                tb.ID = "TB_Doc";
                tb.TextMode = TextBoxMode.MultiLine;
                tb.OnBlur += new EventHandler(btn_Save_Click);
                tb.Style["width"] = "99%";
                tb.Style["border-color"] = "Red";
                tb.Style["border-style"] = "solid";
                tb.Rows = 6;
                if (DoType != null && DoType == "View")
                {
                    tb.ReadOnly = true;
                }
                tb.Text = BP.WF.Dev2Interface.GetCheckInfo(this.FK_Flow, this.WorkID, this.NodeID);
                if (tb.Text == "同意")
                    tb.Text = "";

                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.Text = wcDesc.FWCDefInfo;

                    // 以下手机端都不要去处理
                    if (this.IsCC)
                    {
                        /*如果当前工作是抄送. */
                        BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, tb.Text, "抄送");
                        //设置当前已经审核完成.
                        BP.WF.Dev2Interface.Node_CC_SetSta(this.NodeID, this.WorkID, BP.Web.WebUser.No, CCSta.CheckOver);
                    }
                    else
                    {
                        if (wcDesc.FWCIsFullInfo == true)
                            BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, tb.Text, wcDesc.FWCOpLabel);
                    }
                    // 以上手机端都不要去处理.
                }

                this.Pub1.AddTable("  border=1 style='padding:0px;width:100%;' leftMargin=0 topMargin=0");
                //配置字段解析
                if (!string.IsNullOrEmpty(wcDesc.FWCFields))
                {
                    this.Pub1.AddTR();
                    //不需要常用词汇
                    this.Pub1.AddTD("<div style='float:left'>" + wcDesc.FWCOpLabel + "</div><div style='float:right' onmouseover='UploadFileChange()'>" + uploadJS.ToString() + "</div>");
                    this.Pub1.AddTREnd();
                    //添加字段
                    Attrs fwcAttrs = new Attrs(wcDesc.FWCFields);
                    string msg = DBAccess.RunSQLReturnStringIsNull("SELECT MSG FROM ND" + int.Parse(this.FK_Flow) + "Track WHERE  WorkID='" + this.WorkID + "'  AND NDFrom='" + this.NodeID + "' AND ActionType=" + (int)ActionType.WorkCheck, null);
                    this.Pub1.AddTR("style='border-color:Red;border-style:solid;'");
                    this.Pub1.AddTDBegin();
                    if (string.IsNullOrEmpty(msg) || msg == wcDesc.FWCDefInfo)
                    {
                        this.Pub1.BindAttrs(fwcAttrs);
                    }
                    else
                    {
                        AtPara ap = new AtPara(msg.Replace(";", "@"));
                        this.Pub1.BindAttrs(fwcAttrs, ap);
                    }
                    this.Pub1.AddTDEnd();
                    this.Pub1.AddTREnd();
                }
                else
                {
                    this.Pub1.AddTR();
                    //需要常用词汇
                    this.Pub1.AddTD("<div style='float:left'>" + wcDesc.FWCOpLabel + "</div><div style='float:left'><a href=javascript:TBHelp('WorkCheck_Doc','ND" + NodeID + "')" + "><img src='" + BP.WF.Glo.CCFlowAppPath + "WF/Img/Emps.gif' width='23px' align='middle' border=0 />选择词汇</a></div>"
                        +"<div style='float:right' onmouseover='UploadFileChange()'>" + uploadJS.ToString() + "</div>");
                    this.Pub1.AddTREnd();
                    //意见输入框
                    this.Pub1.AddTR();
                    this.Pub1.AddTD(tb);
                    this.Pub1.AddTREnd();
                }
                this.Pub1.AddTableEnd();
            }
            #endregion 处理审核意见框.
        }
        private bool CanEditor(string fileType)
        {
            try
            {
                string fileTypes = BP.Sys.SystemConfig.AppSettings["OpenTypes"];
                if (string.IsNullOrEmpty(fileTypes) == true)
                    fileTypes = "doc,docx,pdf,xls,xlsx";

                if (fileTypes.Contains(fileType.ToLower()))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InsertDefault(string msg)
        {
            //查看时取消保存
            if (DoType != null && DoType == "View")
                return;

            //内容为空，取消保存
            if (string.IsNullOrEmpty(msg))
                return;

            // 加入审核信息.
            BP.Sys.FrmWorkCheck wcDesc = new BP.Sys.FrmWorkCheck(this.NodeID);

            // 处理人大的需求，需要把审核意见写入到 FlowNote 里面去.
            string sql = "UPDATE WF_GenerWorkFlow SET FlowNote='" + msg + "' WHERE WorkID=" + this.WorkID;
            BP.DA.DBAccess.RunSQL(sql);

            // 判断是否是抄送?
            if (this.IsCC)
            {
                // 写入审核信息，有可能是update数据。
                BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);

                //设置抄送状态 - 已经审核完毕.
                BP.WF.Dev2Interface.Node_CC_SetSta(this.NodeID, this.WorkID, BP.Web.WebUser.No, CCSta.CheckOver);
            }
            else
            {
                BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            // 审核信息.
            string msg = "";
            //查看时取消保存
            if (DoType != null && DoType == "View")
                return;

            //内容为空，取消保存
            if (this.Pub1.GetTextBoxByID("TB_Doc") != null && string.IsNullOrEmpty(this.Pub1.GetTextBoxByID("TB_Doc").Text.Trim()))
            {
                PubClass.Alert("审核组件文本不可以为空!");
                return;
            }

            BP.Sys.FrmWorkCheck wcDesc = new BP.Sys.FrmWorkCheck(this.NodeID);
            if (string.IsNullOrEmpty(wcDesc.FWCFields) == false)
            {
                //循环属性获取值
                Attrs fwcAttrs = new Attrs(wcDesc.FWCFields);
                foreach (Attr attr in fwcAttrs)
                {
                    if (attr.UIContralType == UIContralType.TB)
                    {
                        TB TB_Key = this.Pub1.GetTextBoxByID("TB_" + attr.Key) as TB;
                        if (TB_Key == null)
                            continue;

                        msg += attr.Key + "=" + TB_Key.Text + ";";
                    }
                    else if (attr.UIContralType == UIContralType.CheckBok)
                    {
                        CheckBox CB_Key = this.Pub1.GetCBByID("CB_" + attr.Key);
                        if (CB_Key == null)
                            continue;
                        msg += attr.Key + "=" + Convert.ToInt32(CB_Key.Checked) + ";";
                    }
                    else if (attr.UIContralType == UIContralType.DDL)
                    {
                        DDL DDL_Key = this.Pub1.GetDDLByID("DDL_" + attr.Key);
                        if (DDL_Key == null)
                            continue;
                        msg += attr.Key + "=" + DDL_Key.SelectedValue + ";";
                    }
                }
            }
            else
            {
                // 加入审核信息.
                msg = this.Pub1.GetTextBoxByID("TB_Doc").Text;
            }

            // 处理人大的需求，需要把审核意见写入到FlowNote里面去.
            string sql = "UPDATE WF_GenerWorkFlow SET FlowNote='" + msg + "' WHERE WorkID=" + this.WorkID;
            BP.DA.DBAccess.RunSQL(sql);

            // 判断是否是抄送?
            if (this.IsCC)
            {
                // 写入审核信息，有可能是update数据。
                BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);

                //设置抄送状态 - 已经审核完毕.
                BP.WF.Dev2Interface.Node_CC_SetSta(this.NodeID, this.WorkID, BP.Web.WebUser.No, CCSta.CheckOver);
            }
            else
            {
                #region 根据类型写入数据  qin
                if (wcDesc.HisFrmWorkCheckType == FWCType.Check)  //审核组件
                {
                    BP.WF.Dev2Interface.WriteTrackWorkCheck(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);
                }
                if (wcDesc.HisFrmWorkCheckType == FWCType.DailyLog)//日志组件
                {
                    BP.WF.Dev2Interface.WriteTrackDailyLog(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);
                }
                if (wcDesc.HisFrmWorkCheckType == FWCType.WeekLog)//周报
                {
                    BP.WF.Dev2Interface.WriteTrackWeekLog(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);
                }
                if (wcDesc.HisFrmWorkCheckType == FWCType.MonthLog)//月报
                {
                    BP.WF.Dev2Interface.WriteTrackMonthLog(this.FK_Flow, this.NodeID, this.WorkID, this.FID, msg, wcDesc.FWCOpLabel);
                }
                #endregion
            }

            this.Response.Redirect("WorkCheck.aspx?s=2&OID=" + this.WorkID + "&FK_Node=" + this.NodeID + "&FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "&Paras=" + this.Request.QueryString["Paras"], true);
            //执行审批.
            //BP.Sys.PubClass.Alert("保存成功...");

            //关闭窗口.
            //BP.Sys.PubClass.WinClose();
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="athDBs"></param>
        /// <param name="tk"></param>
        private void AddTDOfFrmAttachMent(FrmAttachmentDBs athDBs, Track tk)
        {
            string strFiles = "";
            foreach (FrmAttachmentDB athDB in athDBs)
            {
                bool isDelete = false;
                if (athDB.FK_MapData != tk.NDFrom.ToString())
                    continue;
                //只有本节点和自己才可以删除
                if (athDB.FK_MapData == this.NodeID.ToString() && athDB.Rec == WebUser.No)
                    isDelete = true;
                if (DoType != null && DoType == "View")
                    isDelete = false;

                string href = GetFileAction(athDB);
                if (isDelete == true)
                {
                    strFiles += "<div style='margin:5px;'><img alt='删除' align='middle' src='../Img/Btn/Delete.gif' onclick=\"DelWorkCheckAth('" + athDB.MyPK + "')\" />&nbsp;&nbsp;<a style='color:Blue; font-size:14;' href=\"" + href + "\">" + athDB.FileName
                        + "&nbsp;&nbsp;<img src='../Img/FileType/" + athDB.FileExts + ".gif' border=0 onerror=\"src='../Img/FileType/Undefined.gif'\" /></a></div>";
                    continue;
                }
                strFiles += "<div style='margin:5px;'><a style='color:Blue; font-size:14;' href=\"" + href + "\">" + athDB.FileName + "&nbsp;&nbsp;<img src='../Img/FileType/" + athDB.FileExts + ".gif' border=0 onerror=\"src='../Img/FileType/Undefined.gif'\" /></a></div>";
            }
            //存在附件则显示
            if (strFiles != "")
            {
                this.Pub1.AddTR();
                this.Pub1.Add("<td style='WORD-WRAP: break-word;'><b>附件：</b>" + strFiles + "</td>");
                this.Pub1.AddTREnd();
            }
        }

        /// <summary>
        /// 获取文件打开方式
        /// </summary>
        /// <param name="athDB"></param>
        /// <returns></returns>
        private string GetFileAction(FrmAttachmentDB athDB)
        {
            if (athDB == null || athDB.FileExts == "") return "#";

            FrmAttachment athDesc = new FrmAttachment(athDB.FK_FrmAttachment);
            switch (athDB.FileExts)
            {
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                    return "javascript:AthOpenOfiice('" + athDB.FK_FrmAttachment + "','" + this.WorkID + "','" + athDB.MyPK + "','" + athDB.FK_MapData + "','" + athDB.FK_FrmAttachment + "','" + this.FK_Node + "')";
                case "txt":
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "bmp":
                case "ceb":
                    return "javascript:AthOpenView('" + athDB.RefPKVal + "','" + athDB.MyPK + "','" + athDB.FK_FrmAttachment + "','" + athDB.FileExts + "','" + this.FK_Flow + "','" + athDB.FK_MapData + "','" + this.WorkID + "','false')";
                case "pdf":
                    return athDesc.SaveTo + this.WorkID + "/" + athDB.MyPK + "." + athDB.FileName;
            }
            return "javascript:AthDown('" + athDB.FK_FrmAttachment + "','" + this.WorkID + "','" + athDB.MyPK + "','" + athDB.FK_MapData + "','" + this.FK_Flow + "','" + athDB.FK_FrmAttachment + "')";
        }

    }

    //自定义控件
    public class PostBackTextBox : System.Web.UI.WebControls.TextBox, System.Web.UI.IPostBackEventHandler
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Attributes["onblur"] = Page.GetPostBackEventReference(this);
            base.Render(writer);
        }

        public event EventHandler OnBlur;

        public virtual void RaisePostBackEvent(string eventArgument)
        {
            if (OnBlur != null)
            {
                OnBlur(this, null);
            }
        }
    }
}