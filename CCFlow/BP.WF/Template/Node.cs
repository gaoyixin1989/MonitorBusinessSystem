using System;
using System.Data;
using BP.DA;
using BP.Sys;
using BP.En;
using System.Collections;
using BP.Port;
using BP.WF.Data;
using BP.WF.Template;

namespace BP.WF
{
    /// <summary>
    /// ������ÿ���ڵ����Ϣ.	 
    /// </summary>
    public class Node : Entity
    {

        #region ִ�нڵ��¼�.
        /// <summary>
        /// ִ���˶��¼�
        /// </summary>
        /// <param name="doType">�¼�����</param>
        /// <param name="en">ʵ�����</param>
        //public string DoNodeEventEntity(string doType,Node currNode, Entity en, string atPara)
        //{
        //    if (this.NDEventEntity != null)
        //        return NDEventEntity.DoIt(doType,currNode, en, atPara);

        //    return this.MapData.FrmEvents.DoEventNode(doType, en, atPara);
        //}
        //private BP.WF.NodeEventBase _NDEventEntity = null;
        ///// <summary>
        ///// �ڵ�ʵ���࣬û�оͷ���Ϊ��.
        ///// </summary>
        //private BP.WF.NodeEventBase NDEventEntity11
        //{
        //    get
        //    {
        //        if (_NDEventEntity == null && this.NodeMark!="" && this.NodeEventEntity!="" )
        //            _NDEventEntity = BP.WF.Glo.GetNodeEventEntityByEnName( this.NodeEventEntity);

        //        return _NDEventEntity;
        //    }
        //}
        #endregion ִ�нڵ��¼�.

        #region ��������
        /// <summary>
        /// �����������ƹ���
        /// </summary>
        public CondModel CondModel
        {
            get
            {
                return (CondModel)this.GetValIntByKey(NodeAttr.CondModel);
            }
        }
        /// <summary>
        /// ��ʱ����ʽ
        /// </summary>
        public OutTimeDeal HisOutTimeDeal
        {
            get
            {
                return (OutTimeDeal)this.GetValIntByKey(NodeAttr.OutTimeDeal);
            }
            set
            {
                this.SetValByKey(NodeAttr.OutTimeDeal, (int)value);
            }
        }
        /// <summary>
        /// ���߳�����
        /// </summary>
        public SubThreadType HisSubThreadType
        {
            get
            {
                return (SubThreadType)this.GetValIntByKey(NodeAttr.SubThreadType);
            }
            set
            {
                this.SetValByKey(NodeAttr.SubThreadType, (int)value);
            }
        }
        #endregion

        #region �������.
        public CC HisCC
        {
            get
            {
                CC obj = this.GetRefObject("HisCC") as CC;
                if (obj == null)
                {
                    obj = new CC();
                    obj.NodeID = this.NodeID;
                    obj.Retrieve();
                    this.SetRefObject("HisCC", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ���Ľ�Ҫת��ķ��򼯺�
        /// �����û�е�ת����,�����ǽ����ڵ�.
        /// û���������ڵĸ���,ȫ���Ľڵ�.
        /// </summary>
        public Nodes HisToNodes
        {
            get
            {
                Nodes obj = this.GetRefObject("HisToNodes") as Nodes;
                if (obj == null)
                {
                    obj = new Nodes();
                    obj.AddEntities(this.HisToNDs);
                    this.SetRefObject("HisToNodes", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// ���Ĺ���
        /// </summary>
        public Work HisWork
        {
            get
            {
                Work obj=null;
                if (this.IsStartNode)
                {
                    obj = new BP.WF.GEStartWork(this.NodeID,this.NodeFrmID);
                    obj.HisNode = this;
                    obj.NodeID = this.NodeID;
                    return obj;

                    //this.SetRefObject("HisWork", obj);
                }
                else
                {
                    obj = new BP.WF.GEWork( this.NodeID,this.NodeFrmID);
                    obj.HisNode = this;
                    obj.NodeID = this.NodeID;
                    return obj;
                    //this.SetRefObject("HisWork", obj);
                }
               // return obj;
                /* ���뻺���û�а취ִ�����ݵ�clone. */
               // Work obj = this.GetRefObject("HisWork") as Work;
               // if (obj == null)
               // {
               //     if (this.IsStartNode)
               //     {
               //         obj = new BP.WF.GEStartWork(this.NodeID);
               //         obj.HisNode = this;
               //         obj.NodeID = this.NodeID;
               //         this.SetRefObject("HisWork", obj);
               //     }
               //     else
               //     {
               //         obj = new BP.WF.GEWork(this.NodeID);
               //         obj.HisNode = this;
               //         obj.NodeID = this.NodeID;
               //         this.SetRefObject("HisWork", obj);
               //     }
               // }
               //// obj.GetNewEntities.GetNewEntity;
               //// obj.Row = null;
               // return obj;
            }
        }
        /// <summary>
        /// ���Ĺ���s
        /// </summary>
        public Works HisWorks
        {
            get
            {
                Works obj = this.HisWork.GetNewEntities as Works;
                return obj;
                ////Works obj = this.GetRefObject("HisWorks") as Works;
                ////if (obj == null)
                ////{
                //    this.SetRefObject("HisWorks",obj);
                //}
                //return obj;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                Flow  obj =this.GetRefObject("Flow") as Flow;
                if (obj == null)
                {
                    obj=new Flow(this.FK_Flow);
                    this.SetRefObject("Flow", obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// HisFrms
        /// </summary>
        public Frms HisFrms
        {
            get
            {
                Frms frms = new Frms();
                FrmNodes fns = new FrmNodes(this.FK_Flow,this.NodeID);
                foreach (FrmNode fn in fns)
                    frms.AddEntity(fn.HisFrm);
                return frms;

                //this.SetRefObject("HisFrms", obj);
                //Frms obj = this.GetRefObject("HisFrms") as Frms;
                //if (obj == null)
                //{
                //    obj = new Frms();
                //    FrmNodes fns = new FrmNodes(this.NodeID);
                //    foreach (FrmNode fn in fns)
                //        obj.AddEntity(fn.HisFrm);
                //    this.SetRefObject("HisFrms", obj);
                //}
                //return obj;
            }
        }
        /// <summary>
        /// ���Ľ�Ҫ���Եķ��򼯺�
        /// �����û�е����ķ���,�����ǿ�ʼ�ڵ�.
        /// </summary>
        public Nodes FromNodes
        {
            get
            {
                Nodes obj = this.GetRefObject("HisFromNodes") as Nodes;
                if (obj == null)
                {
                    // ���ݷ������ɵ���˽ڵ�Ľڵ㡣
                    Directions ens = new Directions();
                    if (this.IsStartNode)
                        obj = new Nodes();
                    else
                        obj = ens.GetHisFromNodes(this.NodeID);
                    this.SetRefObject("HisFromNodes", obj);
                }
                return obj;
            }
        }
        public BillTemplates BillTemplates
        {
            get
            {
                BillTemplates obj= this.GetRefObject("BillTemplates") as BillTemplates;
                if (obj == null)
                {
                    obj = new BillTemplates(this.NodeID);
                    this.SetRefObject("BillTemplates", obj);
                }
                return obj;
            }
        }
        public NodeStations NodeStations
        {
            get
            {
                NodeStations obj = this.GetRefObject("NodeStations") as NodeStations;
                if (obj == null)
                {
                    obj = new NodeStations(this.NodeID);
                    this.SetRefObject("NodeStations", obj);
                }
                return obj;
            }
        }
        public NodeDepts NodeDepts
        {
            get
            {
                NodeDepts obj = this.GetRefObject("NodeDepts") as NodeDepts;
                if (obj == null)
                {
                    obj = new NodeDepts(this.NodeID);
                    this.SetRefObject("NodeDepts", obj);
                }
                return obj;
            }
        }
        public NodeEmps NodeEmps
        {
            get
            {
                NodeEmps obj = this.GetRefObject("NodeEmps") as NodeEmps;
                if (obj == null)
                {
                    obj = new NodeEmps(this.NodeID);
                    this.SetRefObject("NodeEmps", obj);
                }
                return obj;
            }
        }
        public FrmNodes FrmNodes
        {
            get
            {
                FrmNodes obj = this.GetRefObject("FrmNodes") as FrmNodes;
                if (obj == null)
                {
                    obj = new FrmNodes(this.FK_Flow, this.NodeID);
                    this.SetRefObject("FrmNodes", obj);
                }
                return obj;
            }
        }
        public MapData MapData
        {
            get
            {
                MapData obj = this.GetRefObject("MapData") as MapData;
                if (obj == null)
                {
                    obj = new MapData("ND"+this.NodeID);
                    this.SetRefObject("MapData", obj);
                }
                return obj;
            }
        }
        #endregion

        #region ���Ի�ȫ�ֵ� Node
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// ���Ի�ȫ�ֵ�
        /// </summary>
        /// <returns></returns>
        public NodePosType GetHisNodePosType()
        {
            string nodeid = this.NodeID.ToString();
            if (nodeid.Substring(nodeid.Length - 2) == "01")
                return NodePosType.Start;

            if (this.FromNodes.Count == 0)
                return NodePosType.Mid;

            if (this.HisToNodes.Count == 0)
                return NodePosType.End;
            return NodePosType.Mid;
        }
        /// <summary>
        /// ������̣��޸���Ҫ�ļ����ֶ���Ϣ.
        /// </summary>
        /// <param name="fl">����</param>
        /// <returns>���ؼ����Ϣ</returns>
        public static string CheckFlow(Flow fl)
        {
            string sqls = "UPDATE WF_Node SET IsCCFlow=0";
            sqls += "@UPDATE WF_Node  SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond a WHERE a.NodeID= NodeID AND CondType=1 )";
            BP.DA.DBAccess.RunSQLs(sqls);
            // ɾ����Ҫ������.
            DBAccess.RunSQL("DELETE FROM WF_NodeEmp WHERE FK_Emp  not in (select No from Port_Emp)");
            DBAccess.RunSQL("DELETE FROM WF_Emp WHERE NO not in (select No from Port_Emp )");
            DBAccess.RunSQL("UPDATE WF_Emp set Name=(SELECT Name From Port_Emp where Port_Emp.No=WF_Emp.No),FK_Dept=(select FK_Dept from Port_Emp where Port_Emp.No=WF_Emp.No)");

            Nodes nds = new Nodes();
            nds.Retrieve(NodeAttr.FK_Flow, fl.No);

            FlowSort fs = new FlowSort(fl.FK_FlowSort);

            if (nds.Count == 0)
                return "����[" + fl.No + fl.Name + "]��û�нڵ����ݣ�����Ҫע��һ��������̡�";

            // �����Ƿ�������������Ľڵ㡣
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=0  WHERE FK_Flow='" + fl.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node=0 OR ToNode=0");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node  NOT IN (SELECT NODEID FROM WF_Node )");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE ToNode  NOT IN (SELECT NODEID FROM WF_Node) ");

            // ������Ϣ����λ���ڵ���Ϣ��
            foreach (Node nd in nds)
            {
                DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort='" + fl.FK_FlowSort + "',FK_FlowSortT='" + fs.Name + "'");

                BP.Sys.MapData md = new BP.Sys.MapData();
                md.No = "ND" + nd.NodeID;
                if (md.IsExits == false)
                {
                    nd.CreateMap();
                }

                // ������λ��
                NodeStations stas = new NodeStations(nd.NodeID);
                string strs = "";
                foreach (NodeStation sta in stas)
                    strs += "@" + sta.FK_Station;
                nd.HisStas = strs;

                // �������š�
                NodeDepts ndpts = new NodeDepts(nd.NodeID);
                strs = "";
                foreach (NodeDept ndp in ndpts)
                    strs += "@" + ndp.FK_Dept;

                nd.HisDeptStrs = strs;

                // ��ִ����Ա��
                NodeEmps ndemps = new NodeEmps(nd.NodeID);
                strs = "";
                foreach (NodeEmp ndp in ndemps)
                    strs += "@" + ndp.FK_Emp;
                //nd.HisEmps = strs;

                // �����̡�
                NodeFlows ndflows = new NodeFlows(nd.NodeID);
                strs = "";
                foreach (NodeFlow ndp in ndflows)
                    strs += "@" + ndp.FK_Flow;
                nd.HisSubFlows = strs;

                // �ڵ㷽��.
                strs = "";
                Directions dirs = new Directions(nd.NodeID, 0);
                foreach (Direction dir in dirs)
                    strs += "@" + dir.ToNode;
                nd.HisToNDs = strs;

                // ����
                strs = "";
                BillTemplates temps = new BillTemplates(nd);
                foreach (BillTemplate temp in temps)
                    strs += "@" + temp.No;
                nd.HisBillIDs = strs;

                // ���ڵ��λ�����ԡ�
                nd.HisNodePosType = nd.GetHisNodePosType();
                nd.DirectUpdate();
            }

            // �����λ����.
            string sql = "SELECT HisStas, COUNT(*) as NUM FROM WF_Node WHERE FK_Flow='" + fl.No + "' GROUP BY HisStas";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string stas = dr[0].ToString();
                string nodes = "";
                foreach (Node nd in nds)
                {
                    if (nd.HisStas == stas)
                        nodes += "@" + nd.NodeID;
                }

                foreach (Node nd in nds)
                {
                    if (nodes.Contains("@" + nd.NodeID.ToString()) == false)
                        continue;

                    nd.GroupStaNDs = nodes;
                    nd.DirectUpdate();
                }
            }

            /* �ж����̵����� */
            sql = "SELECT Name FROM WF_Node WHERE (NodeWorkType=" + (int)NodeWorkType.StartWorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFHL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkHL + ") AND (FK_Flow='" + fl.No + "')";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //if (dt.Rows.Count == 0)
            //    fl.HisFlowType = FlowType.Panel;
            //else
            //    fl.HisFlowType = FlowType.FHL;

            fl.DirectUpdate();
            return null;
        }
        
        protected override bool beforeUpdate()
        {
            if (this.IsStartNode)
            {
                this.SetValByKey(BtnAttr.ReturnRole, (int)ReturnRole.CanNotReturn);
                this.SetValByKey(BtnAttr.ShiftEnable, 0);
                //  this.SetValByKey(BtnAttr.CCRole, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
            }

            //��icon����Ĭ��ֵ.
            if (this.GetValStrByKey(NodeAttr.ICON)=="")
                this.ICON = "/WF/Data/NodeIcon/���.png";


            #region ��������ݺϲ�ģʽ����Ҫ���ڵ����Ƿ������̣߳���������߳̾���Ҫ�����ı�.
            if (this.HisRunModel == RunModel.SubThread)
            {
                MapData md = new MapData("ND" + this.NodeID);
                if (md.PTable != "ND" + this.NodeID)
                {
                    md.PTable = "ND" + this.NodeID;
                    md.Update();
                }
            }
            #endregion ��������ݺϲ�ģʽ����Ҫ���ڵ����Ƿ������̣߳���������߳̾���Ҫ�����ı�.

            //���°汾��.
            Flow.UpdateVer(this.FK_Flow);


            #region  //��� NEE ʵ��.
            //if (string.IsNullOrEmpty(this.NodeMark) == false)
            //{
            //    object obj = Glo.GetNodeEventEntityByNodeMark(fl.FlowMark,this.NodeMark);
            //    if (obj == null)
            //        throw new Exception("@�ڵ��Ǵ���û���ҵ��ýڵ���(" + this.NodeMark + ")�Ľڵ��¼�ʵ��.");
            //    this.NodeEventEntity = obj.ToString();
            //}
            //else
            //{
            //    this.NodeEventEntity = "";
            //}
            #endregion ͬ���¼�ʵ��.

            #region ���������ж������ı�ǡ�
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=0  WHERE FK_Flow='" + this.FK_Flow + "'");
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=1) AND FK_Flow='" + this.FK_Flow + "'");
            #endregion


            Flow fl = new Flow(this.FK_Flow);
            
            Node.CheckFlow(fl);
            this.FlowName = fl.Name;

            DBAccess.RunSQL("UPDATE Sys_MapData SET Name='" + this.Name + "' WHERE No='ND" + this.NodeID + "'");
            switch (this.HisRunModel)
            {
                case RunModel.Ordinary:
                    if (this.IsStartNode)
                        this.HisNodeWorkType = NodeWorkType.StartWork;
                    else
                        this.HisNodeWorkType = NodeWorkType.Work;
                    break;
                case RunModel.FL:
                    if (this.IsStartNode)
                        this.HisNodeWorkType = NodeWorkType.StartWorkFL;
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkFL;
                    break;
                case RunModel.HL:
                    //if (this.IsStartNode)
                    //    throw new Exception("@���������ÿ�ʼ�ڵ�Ϊ�����ڵ㡣");
                    //else
                    //    this.HisNodeWorkType = NodeWorkType.WorkHL;
                    break;
                case RunModel.FHL:
                    //if (this.IsStartNode)
                    //    throw new Exception("@���������ÿ�ʼ�ڵ�Ϊ�ֺ����ڵ㡣");
                    //else
                    //    this.HisNodeWorkType = NodeWorkType.WorkFHL;
                    break;
                case RunModel.SubThread:
                    this.HisNodeWorkType = NodeWorkType.SubThreadWork;
                    break;
                default:
                    throw new Exception("eeeee");
                    break;
            }
            //��������������
            FrmAttachment workCheckAth = new FrmAttachment();
            bool isHave = workCheckAth.RetrieveByAttr(FrmAttachmentAttr.MyPK, this.NodeID + "_FrmWorkCheck");
            //������������
            if (isHave == false)
            {
                workCheckAth = new FrmAttachment();
                /*���û�в�ѯ����,���п�����û�д���.*/
                workCheckAth.MyPK = this.NodeID + "_FrmWorkCheck";
                workCheckAth.FK_MapData = this.NodeID.ToString();
                workCheckAth.NoOfObj = this.NodeID + "_FrmWorkCheck";
                workCheckAth.Exts = "*.*";

                //�洢·��.
                workCheckAth.SaveTo = "/DataUser/UploadFile/";
                workCheckAth.IsNote = false; //����ʾnote�ֶ�.
                workCheckAth.IsVisable = false; // ������form �ϲ��ɼ�.

                //λ��.
                workCheckAth.X = (float)94.09;
                workCheckAth.Y = (float)333.18;
                workCheckAth.W = (float)626.36;
                workCheckAth.H = (float)150;

                //�฽��.
                workCheckAth.UploadType = AttachmentUploadType.Multi;
                workCheckAth.Name = "������";
                workCheckAth.SetValByKey("AtPara", "@IsWoEnablePageset=1@IsWoEnablePrint=1@IsWoEnableViewModel=1@IsWoEnableReadonly=0@IsWoEnableSave=1@IsWoEnableWF=1@IsWoEnableProperty=1@IsWoEnableRevise=1@IsWoEnableIntoKeepMarkModel=1@FastKeyIsEnable=0@IsWoEnableViewKeepMark=1@FastKeyGenerRole=@IsWoEnableTemplete=1");
                workCheckAth.Insert();
            }   
            return base.beforeUpdate();
        }
        #endregion

        #region ��������
        /// <summary>
        /// ������
        /// </summary>
        public BP.Sys.FrmWorkCheckSta FrmWorkCheckSta
        {
            get
            {
                return (Sys.FrmWorkCheckSta)this.GetValIntByKey(NodeAttr.FWCSta);
            }
        }
        /// <summary>
        /// �ڲ����
        /// </summary>
        public string No
        {
            get
            {
                try
                {
                    return this.NodeID.ToString().Substring(this.NodeID.ToString().Length - 2);
                }
                catch(Exception ex)
                {
                    Log.DefaultLogWriteLineInfo(ex.Message+" - "+this.NodeID);
                    throw new Exception("@û�л�ȡ������NodeID = "+this.NodeID);
                }
            }
        }
        /// <summary>
        /// �Զ���ת����0-�����˾����ύ��
        /// </summary>
        public bool AutoJumpRole0
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.AutoJumpRole0);
            }
            set
            {
                this.SetValByKey(NodeAttr.AutoJumpRole0, value);
            }
        }
        /// <summary>
        /// �Զ���ת����1-�������Ѿ����ֹ�
        /// </summary>
        public bool AutoJumpRole1
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.AutoJumpRole1);
            }
            set
            {
                this.SetValByKey(NodeAttr.AutoJumpRole1, value);
            }
        }
        /// <summary>
        /// �Զ���ת����2-����������һ����ͬ
        /// </summary>
        public bool AutoJumpRole2
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.AutoJumpRole2);
            }
            set
            {
                this.SetValByKey(NodeAttr.AutoJumpRole2, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SubFlowStartParas
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.SubFlowStartParas);
            }
            set
            {
                this.SetValByKey(NodeAttr.SubFlowStartParas, value);
            }
        }
        /// <summary>
        /// ���߳�������ʽ
        /// </summary>
        public SubFlowStartWay SubFlowStartWay
        {
            get
            {
                return (SubFlowStartWay)this.GetValIntByKey(NodeAttr.SubFlowStartWay);
            }
            set
            {
                this.SetValByKey(NodeAttr.SubFlowStartWay, (int)value);
            }
        }

        public NodeFormType HisFormType
        {
            get
            {
                return (NodeFormType)this.GetValIntByKey(NodeAttr.FormType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormType, (int)value);
            }
        }
        /// <summary>
        /// OID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        public bool IsEnableTaskPool
        {
            get
            {
                if (this.TodolistModel == WF.TodolistModel.Sharing)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �ڵ�ͷ��
        /// </summary>
        public string ICON
        {
            get
            {
                string s= this.GetValStrByKey(NodeAttr.ICON);
                if (string.IsNullOrEmpty(s))
                    if (this.IsStartNode)
                        return "/WF/Data/NodeIcon/���.png";
                    else
                        return "/WF/Data/NodeIcon/ǰ̨.png";
                return s;
            }
            set
            {
                this.SetValByKey(NodeAttr.ICON, value);
            }
        }
        /// <summary>
        /// FormUrl 
        /// </summary>
        public string FormUrl
        {
            get
            {
                string str= this.GetValStrByKey(NodeAttr.FormUrl);
                str = str.Replace("@SDKFromServHost", 
                    BP.Sys.SystemConfig.AppSettings["SDKFromServHost"]);
                return str;
            }
            set
            {
                this.SetValByKey(NodeAttr.FormUrl, value);
            }
        }
        public NodeFormType FormType
        {
            get
            {
                return (NodeFormType)this.GetValIntByKey(NodeAttr.FormType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormType, value);
            }
        }
        
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStrByKey(EntityOIDNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameAttr.Name, value);
            }
        }
        /// <summary>
        /// ��ҪСʱ�������ڣ�
        /// </summary>
        public float TSpanHour
        {
            get
            {
                float i = this.GetValFloatByKey(NodeAttr.TSpanHour);
                if (i == 0)
                    return 0;
                return i;
            }
            set
            {
                this.SetValByKey(NodeAttr.TSpanHour, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int TSpanDay
        {
            get
            {
                return  this.GetValIntByKey(NodeAttr.TSpanDay);
            }
            set
            {
                this.SetValByKey(NodeAttr.TSpanDay, value);
            }
        }
        /// <summary>
        /// �ϼƷ���.
        /// </summary>
        public int TSpanMinues
        {
            get
            {
                return  (int) (this.TSpanHour*60f);
            }
        }
        /// <summary>
        /// Ԥ��
        /// </summary>
        public float WarningHour
        {
            get
            {
                float i = this.GetValFloatByKey(NodeAttr.WarningHour);
                if (i == 0)
                    return 1;
                return i;
            }
            set
            {
                this.SetValByKey(NodeAttr.WarningHour, value);
            }
        }
        /// <summary>
        /// ���淽ʽ @0=���ڵ�� @1=�ڵ���NDxxxRtp��.
        /// </summary>
        public SaveModel SaveModel
        {
            get
            {
                return (SaveModel)this.GetValIntByKey(NodeAttr.SaveModel);
            }
            set
            {
                this.SetValByKey(NodeAttr.SaveModel, (int)value);
            }
        }
        /// <summary>
        /// ���̲���
        /// </summary>
        public int Step
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Step);
            }
            set
            {
                this.SetValByKey(NodeAttr.Step, value);
            }
        }

         
        /// <summary>
        /// �۷��ʣ���/�죩
        /// </summary>
        public float TCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.TCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.TCent, value);
            }
        }
        /// <summary>
        /// �Ƿ��ǿͻ�ִ�нڵ㣿
        /// </summary>
        public bool IsGuestNode
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsGuestNode);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsGuestNode, value);
            }
        }
        /// <summary>
        /// �Ƿ��ǿ�ʼ�ڵ�
        /// </summary>
        public bool IsStartNode
        {
            get
            {
                if (this.No == "01")
                    return true;
                return false;

                //if (this.HisNodePosType == NodePosType.Start)
                //    return true;
                //else
                //    return false;
            }
        }
        /// <summary>
        /// x
        /// </summary>
        public int X
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.X);
            }
            set
            {
                this.SetValByKey(NodeAttr.X, value);
            }
        }
       
        /// <summary>
        /// y
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Y);
            }
            set
            {
                this.SetValByKey(NodeAttr.Y, value);
            }
        }
        /// <summary>
        /// ˮִ������
        /// </summary>
        public int WhoExeIt
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.WhoExeIt);
            }
            set
            {
                this.SetValByKey(NodeAttr.WhoExeIt, value);
            }
        }
         
        /// <summary>
        /// λ��
        /// </summary>
        public NodePosType NodePosType
        {
            get
            {
                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public RunModel HisRunModel
        {
            get
            {
                return (RunModel)this.GetValIntByKey(NodeAttr.RunModel);
            }
            set
            {
                this.SetValByKey(NodeAttr.RunModel, (int)value);
            }
        }
        /// <summary>
        /// ������ʾ
        /// </summary>
        public string Tip
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.Tip);
            }
            set
            {
                this.SetValByKey(NodeAttr.Tip, value);
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string FocusField
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FocusField);
            }
            set
            {
                this.SetValByKey(NodeAttr.FocusField,value);
            }
        }
        /// <summary>
        /// �˻���Ϣ�ֶ�.
        /// </summary>
        public string ReturnField_del
        {
            get
            {
                return this.GetValStrByKey(BtnAttr.ReturnField);
            }
        }
        /// <summary>
        /// �ڵ��������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ��ȡ������һ���ķ�����
        /// </summary>
        private Node _GetHisPriFLNode(Nodes nds)
        {
            foreach (Node mynd in nds)
            {
                if (mynd.IsFL)
                    return mynd;
                else
                    return _GetHisPriFLNode(mynd.FromNodes);
            }
            return null;
        }
        /// <summary>
        /// ������һ�������ڵ�
        /// </summary>
        public Node HisPriFLNode
        {
            get
            {
                return _GetHisPriFLNode(this.FromNodes);
            }
        }
        public string TurnToDealDoc
        {
            get
            {
                string s= this.GetValStrByKey(NodeAttr.TurnToDealDoc);
                if (this.HisTurnToDeal == TurnToDeal.SpecUrl)
                {
                    if (s.Contains("?"))
                        s += "&1=1";
                    else
                        s += "?1=1";
                }
                return s;
            }
            set
            {
                SetValByKey(NodeAttr.TurnToDealDoc, value);
            }
        }
        /// <summary>
        /// ����ת�Ľڵ�
        /// </summary>
        public string JumpToNodes
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.JumpToNodes);
            }
            set
            {
                SetValByKey(NodeAttr.JumpToNodes, value);
            }
        }
        /// <summary>
        /// �ڵ��ID
        /// </summary>
        public string NodeFrmID
        {
            get
            {
                string str =this.GetValStrByKey(NodeAttr.NodeFrmID);
                if (string.IsNullOrEmpty(str))
                    return "ND" + this.NodeID;
                return str;
            }
        }
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FlowName);
            }
            set
            {
                SetValByKey(NodeAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        public PrintDocEnable HisPrintDocEnable
        {
            get
            {
                return (PrintDocEnable)this.GetValIntByKey(NodeAttr.PrintDocEnable);
            }
            set
            {
                this.SetValByKey(NodeAttr.PrintDocEnable, (int)value);
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public BatchRole HisBatchRole
        {
            get
            {
                return (BatchRole)this.GetValIntByKey(NodeAttr.BatchRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.BatchRole, (int)value);
            }
        }
        /// <summary>
        /// �����������
        /// @��ʾ���ֶ�.
        /// </summary>
        public string BatchParas
        {
            get
            {
                string str= this.GetValStringByKey(NodeAttr.BatchParas);

                //�滻Լ����URL.
                str = str.Replace("@SDKFromServHost", BP.Sys.SystemConfig.AppSettings["SDKFromServHost"]);
                //if (str.Length <=3)
                //    str="Title,RDT"
                return str;
            }
            set
            {
                this.SetValByKey(NodeAttr.BatchParas, value);
            }
        }
        /// <summary>
        /// �Ƿ����Զ����url,����������.
        /// </summary>
        public bool BatchParas_IsSelfUrl
        {
            get
            {
                if (this.BatchParas.Contains(".aspx") 
                    || this.BatchParas.Contains(".jsp")
                    || this.BatchParas.Contains(".htm") 
                    || this.BatchParas.Contains("http:"))
                    return true;
                return false;
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public int BatchListCount
        {
            get { return this.GetValIntByKey(NodeAttr.BatchListCount); }
            set { this.SetValByKey(NodeAttr.BatchListCount, value); }
        }
        public string PTable
        {
            get
            {
                
                return "ND" + this.NodeID;
            }
            set
            {
                SetValByKey(NodeAttr.PTable, value);
            }
        }
        /// <summary>
        /// Ҫ��ʾ�ں���ı�
        /// </summary>
        public string ShowSheets
        {
            get
            {
                string s = this.GetValStrByKey(NodeAttr.ShowSheets);
                if (s == "")
                    return "@";
                return s;
            }
            set
            {
                SetValByKey(NodeAttr.ShowSheets, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary> 
        public string Doc
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.Doc);
            }
            set
            {
                SetValByKey(NodeAttr.Doc, value);
            }
        }
        public string GroupStaNDs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.GroupStaNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.GroupStaNDs, value);
            }
        }
        /// <summary>
        /// ����Ľڵ�����.
        /// </summary>
        public int HisToNDNum
        {
            get
            {
                string[] strs = this.HisToNDs.Split('@');
                return strs.Length-1;
            }
        }
        public string HisToNDs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisToNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisToNDs, value);
            }
        }
        public string HisDeptStrs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisDeptStrs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisDeptStrs, value);
            }
        }
        public string HisStas
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisStas);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisStas, value);
            }
        }
        
        public string HisBillIDs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisBillIDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisBillIDs, value);
            }
        }
        /// <summary>
        /// ������ߴ���
        /// </summary>
        public string DocLeftWord
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.DocLeftWord);
            }
            set
            {
                this.SetValByKey(NodeAttr.DocLeftWord, value);
            }
        }
        /// <summary>
        /// �����ұߴ���
        /// </summary>
        public string DocRightWord
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.DocRightWord);
            }
            set
            {
                this.SetValByKey(NodeAttr.DocRightWord, value);
            }
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// �ǲ��Ƕ��λ�����ڵ�.
        /// </summary>
        public bool IsMultiStations
        {
            get
            {
                if (this.NodeStations.Count > 1)
                    return true;
                return false;
            }
        }
        public string HisStationsStr
        {
            get
            {
                string s = "";
                foreach (NodeStation ns in this.NodeStations)
                {
                    s += ns.FK_StationT + ",";
                }
                return s;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �õ�һ������dataʵ��
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <returns>���û�оͷ���null</returns>
        public Work GetWork(Int64 workId)
        {
            Work wk = this.HisWork;
            wk.SetValByKey("OID", workId);
            if (wk.RetrieveFromDBSources() == 0)
                return null;
            else
                return wk;
            return wk;
        }
        #endregion

        #region �ڵ�Ĺ�������
        /// <summary>
        /// ת����
        /// </summary>
        public TurnToDeal HisTurnToDeal
        {
            get
            {
                return (TurnToDeal)this.GetValIntByKey(NodeAttr.TurnToDeal);
            }
            set
            {
                this.SetValByKey(NodeAttr.TurnToDeal, (int)value);
            }
        }
        /// <summary>
        /// ���ʹ���
        /// </summary>
        public DeliveryWay HisDeliveryWay
        {
            get
            {
                return (DeliveryWay)this.GetValIntByKey(NodeAttr.DeliveryWay);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeliveryWay, (int)value);
            }
        }
        /// <summary>
        /// ���͹���
        /// </summary>
        public CCRole HisCCRole
        {
            get
            {
                return (CCRole)this.GetValIntByKey(NodeAttr.CCRole);
            }
        }
        /// <summary>
        /// ɾ�����̹���
        /// </summary>
        public DelWorkFlowRole HisDelWorkFlowRole
        {
            get
            {
                return (DelWorkFlowRole)this.GetValIntByKey(BtnAttr.DelEnable);
            }
        }
        /// <summary>
        /// δ�ҵ�������ʱ�ķ�ʽ
        /// </summary>
        public WhenNoWorker HisWhenNoWorker
        {
            get
            {
                return (WhenNoWorker)this.GetValIntByKey(NodeAttr.WhenNoWorker);
            }
            set
            {
                this.SetValByKey(NodeAttr.WhenNoWorker, (int)value);
            }
        }
         /// <summary>
        /// ��������
        /// </summary>
        public CancelRole HisCancelRole
        {
            get
            {
                return (CancelRole)this.GetValIntByKey(NodeAttr.CancelRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.CancelRole, (int)value);
            }
        }
        
        /// <summary>
        /// ����д�����
        /// </summary>
        public CCWriteTo CCWriteTo
        {
            get
            {
                return (CCWriteTo)this.GetValIntByKey(NodeAttr.CCWriteTo);
            }
            set
            {
                this.SetValByKey(NodeAttr.CCWriteTo, (int)value);
            }
        }
       
        /// <summary>
        /// Int type
        /// </summary>
        public NodeWorkType HisNodeWorkType
        {
            get
            {
#warning 2012-01-24�޶�,û���Զ�����������ԡ�
                switch (this.HisRunModel)
                {
                    case RunModel.Ordinary:
                        if (this.IsStartNode)
                            return NodeWorkType.StartWork;
                        else
                            return NodeWorkType.Work;
                    case RunModel.FL:
                        if (this.IsStartNode)
                            return NodeWorkType.StartWorkFL;
                        else
                            return NodeWorkType.WorkFL;
                    case RunModel.HL:
                            return NodeWorkType.WorkHL;
                    case RunModel.FHL:
                        return NodeWorkType.WorkFHL;
                    case RunModel.SubThread:
                        return NodeWorkType.SubThreadWork;
                    default:
                        throw new Exception("@û���ж�����NodeWorkType.");
                }
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeWorkType, (int)value);
            }
        }
        public string HisNodeWorkTypeT
        {
            get
            {
                return this.HisNodeWorkType.ToString();

                //Sys.SysEnum se = new Sys.SysEnum(NodeAttr.NodeWorkType, this.GetValIntByKey(NodeAttr.NodeWorkType));
                //return se.Lab;
            }
        }
        #endregion

        #region �������� (���ڽڵ�λ�õ��ж�)
       
        /// <summary>
        /// ����
        /// </summary>
        public NodePosType HisNodePosType
        {
            get
            {
                if (SystemConfig.IsDebug)
                {
                    this.SetValByKey(NodeAttr.NodePosType, (int)this.GetHisNodePosType());
                    return this.GetHisNodePosType();
                }
                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                if (value == NodePosType.Start)
                    if (this.No != "01")
                        value = NodePosType.Mid;

                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }
        }
        /// <summary>
        /// �ǲ��ǽ����ڵ�
        /// </summary>
        public bool IsEndNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.End)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �Ƿ��������߳̽�����Ա�ظ�(�����̵߳���Ч)?
        /// </summary>
        public bool IsAllowRepeatEmps
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsAllowRepeatEmps);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsAllowRepeatEmps, value);
            }
        }
        /// <summary>
        /// �Ƿ�������˻غ�ԭ·���أ�
        /// </summary>
        public bool IsBackTracking
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsBackTracking);
            }
        }
        /// <summary>
        /// �Ƿ������Զ����书��
        /// </summary>
        public bool IsRememberMe
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsRM);
            }
        }
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public bool IsCanDelFlow
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanDelFlow);
            }
        }

        /// <summary>
        /// ��ͨ�����ڵ㴦��ģʽ
        /// </summary>
        public TodolistModel TodolistModel
        {
            get
            {
                return (TodolistModel)this.GetValIntByKey(NodeAttr.TodolistModel);
            }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public BlockModel BlockModel
        {
            get
            {
                return (BlockModel)this.GetValIntByKey(NodeAttr.BlockModel);
            }
        }
        /// <summary>
        /// �����ı��ʽ
        /// </summary>
        public string  BlockExp
        {
            get
            {

                string str = this.GetValStringByKey(NodeAttr.BlockExp);

                if (string.IsNullOrEmpty(str))
                {
                    if (this.BlockModel == WF.BlockModel.CurrNodeAll)
                        return "����������û������������ύ,��Ҫ�ȵ����е���������ɺ������ܷ���.";

                    if (this.BlockModel == WF.BlockModel.SpecSubFlow)
                        return "����������û������������ύ,��Ҫ�ȵ����е���������ɺ������ܷ���.";
                }
                return str;
            }
        }
        /// <summary>
        /// ������ʱ��ʾ��Ϣ
        /// </summary>
        public string BlockAlert
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.BlockAlert);
            }
        }
        /// <summary>
        /// ���߳�ɾ������
        /// </summary>
        public ThreadKillRole ThreadKillRole
        {
            get
            {
                return (ThreadKillRole)this.GetValIntByKey(NodeAttr.ThreadKillRole);
            }
        }
        /// <summary>
        /// �Ƿ��ܲ���
        /// </summary>
        public bool IsSecret
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsSecret);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsSecret, value);
            }
        }
        /// <summary>
        /// ���ͨ����
        /// </summary>
        public decimal PassRate
        {
            get
            {
                return this.GetValDecimalByKey(NodeAttr.PassRate);
            }
        }
        /// <summary>
        /// �Ƿ�������乤��
        /// </summary>
        public bool IsTask
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsTask);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsTask, value);
            }
        }
        public bool IsCanOver
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanOver);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanOver, value);
            }
        }
        public bool IsCanRpt
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanRpt);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanRpt, value);
            }
        }
        /// <summary>
        /// �Ƿ�����ƽ�
        /// </summary>
        public bool IsHandOver
        {
            get
            {
                if (this.IsStartNode)
                    return false;

                return this.GetValBooleanByKey(NodeAttr.IsHandOver);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsHandOver, value);
            }
        }
        public bool IsCanHidReturn_del
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanHidReturn);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanHidReturn, value);
            }
        }
        public bool IsCanReturn
        {
            get
            {
                if (this.HisReturnRole == ReturnRole.CanNotReturn)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// �Ѷ���ִ
        /// </summary>
        public ReadReceipts ReadReceipts
        {
            get
            {
                return (ReadReceipts)this.GetValIntByKey(NodeAttr.ReadReceipts);
            }
            set
            {
                this.SetValByKey(NodeAttr.ReadReceipts, (int)value);
            }
        }
        /// <summary>
        /// �˻ع���
        /// </summary>
        public ReturnRole HisReturnRole
        {
            get
            {
                return (ReturnRole)this.GetValIntByKey(NodeAttr.ReturnRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.ReturnRole, (int)value);
            }
        }
        /// <summary>
        /// �ǲ����м�ڵ�
        /// </summary>
        public bool IsMiddleNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.Mid)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �Ƿ��ǹ����������˵�
        /// </summary>
        public bool IsEval
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsEval);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsEval, value);
            }
        }
        public string HisSubFlows
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisSubFlows);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisSubFlows, value);
            }
        }
        public string FrmAttr
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FrmAttr);
            }
            set
            {
                this.SetValByKey(NodeAttr.FrmAttr, value);
            }
        }
     
        /// <summary>
        /// ���Ƿ���������
        /// </summary>
        public bool IsHaveSubFlow
        {
            get
            {
                if (this.HisSubFlows.Length > 2)
                    return true;
                else
                    return false;
            }
        }
        public bool IsHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFHL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��Ƿ���
        /// </summary>
        public bool IsFL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsFLHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��������������
        /// </summary>
        public bool IsCCFlow
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCCFlow);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCCFlow, value);
            }
        }
        /// <summary>
        /// ������sql
        /// </summary>
        public string DeliveryParas
        {
            get
            {
                string s= this.GetValStringByKey(NodeAttr.DeliveryParas);
                s = s.Replace("~", "'");

                if (this.HisDeliveryWay == DeliveryWay.ByPreviousNodeFormEmpsField && string.IsNullOrEmpty(s) == true)
                    return "ToEmps";
                return s;
            }
            set
            {
                this.SetValByKey(NodeAttr.DeliveryParas, value);
            }
        }
        public bool IsExpSender
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsExpSender);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsExpSender, value);
            }
        }
        
        /// <summary>
        /// �ǲ���PC�����ڵ�
        /// </summary>
        public bool IsPCNode
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string NodeWorkTypeText
        {
            get
            {
                return this.HisNodeWorkType.ToString();
            }
        }
        #endregion

        #region �������� (�û�ִ�ж���֮��,��Ҫ���Ĺ���)
        /// <summary>
        /// �û�ִ�ж���֮��,��Ҫ���Ĺ���		 
        /// </summary>
        /// <returns>������Ϣ,���е���Ϣ</returns>
        public string AfterDoTask()
        {
            return "";
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node() { }
        /// <summary>
        /// �ڵ�
        /// </summary>
        /// <param name="_oid">�ڵ�ID</param>	
        public Node(int _oid)
        {
            this.NodeID = _oid;
            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
            else
            {
                // ȥ������.
                this.RetrieveFromDBSources();
                //if (this.Retrieve() <= 0)
                //    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
        }
        public Node(string ndName)
        {
            ndName = ndName.Replace("ND", "");
            this.NodeID = int.Parse(ndName);

            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
            }
        }
        public string EnName
        {
            get
            {
                return "ND" + this.NodeID;
            }
        }
        public string EnsName
        {
            get
            {
                return "ND" + this.NodeID + "s";
            }
        }
        /// <summary>
        /// �ڵ�������ƣ����Ϊ����ȡ�ڵ�����.
        /// </summary>
        public string FWCNodeName
        {
            get
            {
                string str = this.GetValStringByKey(FrmWorkCheckAttr.FWCNodeName);
                if (string.IsNullOrEmpty(str))
                    return this.Name;
                return str;
            }
        }

        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Node");
                map.EnDesc = "�ڵ�"; // "�ڵ�";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                #region ��������.
                map.AddTBIntPK(NodeAttr.NodeID, 0, "�ڵ�ID", true, true);
                map.AddTBString(NodeAttr.Name, null, "����", true, false, 0, 100, 10);
                map.AddTBString(NodeAttr.Tip, null, "������ʾ", true, true, 0, 100, 10, false);

                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work,  "���̲���", true, false);

                //ͷ��. "/WF/Data/NodeIcon/���.png"  "/WF/Data/NodeIcon/ǰ̨.png"
                map.AddTBString(NodeAttr.ICON, null, "�ڵ�ICONͼƬ·��", true, false, 0, 50, 10);

                map.AddTBInt(NodeAttr.NodeWorkType, 0, "�ڵ�����", false, false);
                map.AddTBInt(NodeAttr.SubThreadType, 0, "���߳�ID", false, false);

                map.AddTBString(NodeAttr.FK_Flow, null, "FK_Flow", false, false, 0, 3, 10);
                map.AddTBInt(NodeAttr.IsGuestNode, 0, "�Ƿ��ǿͻ�ִ�нڵ�", false, false);

                map.AddTBString(NodeAttr.FlowName, null, "������", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FK_FlowSort, null, "FK_FlowSort", false, true, 0, 4, 10);
                map.AddTBString(NodeAttr.FK_FlowSortT, null, "FK_FlowSortT", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FrmAttr, null, "FrmAttr", false, true, 0, 300, 10);
                #endregion ��������.

                #region ������.
                map.AddTBInt(NodeAttr.FWCSta, 0, "������", false, false);
                #endregion ������.


                #region ��������.

                map.AddTBFloat(NodeAttr.TSpanDay, 0, "����(��)", true, false); //"����(��)".
                map.AddTBFloat(NodeAttr.TSpanHour, 8, "Сʱ", true, false); //"����(��)".

                map.AddTBFloat(NodeAttr.WarningDay, 0, "����Ԥ��(��)", true, false);    // "��������(0������)"
                map.AddTBFloat(NodeAttr.WarningHour, 0, "����Ԥ��(Сʱ)", true, false); // "��������(0������)"
                map.SetHelperUrl(NodeAttr.WarningHour, "http://ccbpm.mydoc.io/?v=5404&t=17999");


                map.AddTBFloat(NodeAttr.TCent, 2, "�۷�(ÿ����1Сʱ)", false, false); 
                map.AddTBInt(NodeAttr.CHWay, 0, "���˷�ʽ", false, false); //"����(��)"
                #endregion ��������.

                map.AddTBString(FrmWorkCheckAttr.FWCNodeName, null, "�ڵ��������", true, false, 0, 100, 10);

                map.AddTBString(NodeAttr.Doc, null, "����", true, false, 0, 100, 10);
                map.AddBoolean(NodeAttr.IsTask, true, "������乤����?", true, true);

                map.AddTBInt(NodeAttr.ReturnRole, 2, "�˻ع���", true, true);
                map.AddTBInt(NodeAttr.DeliveryWay, 0, "���ʹ���", true, true);
                map.AddTBInt(NodeAttr.IsExpSender, 1, "���ڵ�����˲����������һ��������", true, true);

                map.AddTBInt(NodeAttr.CancelRole, 0, "��������", true, true);

                map.AddTBInt(NodeAttr.WhenNoWorker, 0, "δ�ҵ�������ʱ", true, true);
                map.AddTBString(NodeAttr.DeliveryParas, null, "���ʹ�������", true, false, 0, 500, 10);
                map.AddTBString(NodeAttr.NodeFrmID, null, "�ڵ��ID", true, false, 0, 50, 10);

                map.AddTBInt(NodeAttr.CCRole, 0, "���͹���", true, true);
                map.AddTBInt(NodeAttr.CCWriteTo, 0, "��������д�����", true, true);

                map.AddTBInt(BtnAttr.DelEnable, 0, "ɾ������", true, true);
                map.AddTBInt(NodeAttr.IsEval, 0, "�Ƿ�����������", true, true);
                map.AddTBInt(NodeAttr.SaveModel, 0, "����ģʽ", true, true);


                map.AddTBInt(NodeAttr.IsCanRpt, 1, "�Ƿ���Բ鿴��������?", true, true);
                map.AddTBInt(NodeAttr.IsCanOver, 0, "�Ƿ������ֹ����", true, true);
                map.AddTBInt(NodeAttr.IsSecret, 0, "�Ƿ��Ǳ��ܲ���", true, true);
                map.AddTBInt(NodeAttr.IsCanDelFlow, 0, "�Ƿ����ɾ������", true, true);

                map.AddTBInt(NodeAttr.ThreadKillRole, 0, "���߳�ɾ����ʽ", true, true);
                map.AddTBInt(NodeAttr.TodolistModel, 0, "�Ƿ��Ƕ��нڵ�", true, true);

                map.AddTBInt(NodeAttr.IsAllowRepeatEmps, 0, "�Ƿ��������߳̽�����Ա�ظ�(�����̵߳���Ч)?", true, true);
                map.AddTBInt(NodeAttr.IsBackTracking, 0, "�Ƿ�������˻غ�ԭ·����(ֻ�������˻ع��ܲ���Ч)", true, true);
                map.AddTBInt(NodeAttr.IsRM, 1, "�Ƿ�����Ͷ��·���Զ����书��?", true, true);
                map.AddBoolean(NodeAttr.IsHandOver, false, "�Ƿ�����ƽ�", true, true);
                map.AddTBDecimal(NodeAttr.PassRate, 100, "ͨ����", true, true);
                map.AddTBInt(NodeAttr.RunModel, 0, "����ģʽ(����ͨ�ڵ���Ч)", true, true);
                map.AddTBInt(NodeAttr.BlockModel, 0, "����ģʽ", true, true);
                map.AddTBString(NodeAttr.BlockExp, null, "�������ʽ", true, false, 0, 700, 10);
                map.AddTBString(NodeAttr.BlockAlert, null, "��������ʾ��Ϣ", true, false, 0, 700, 10);

                map.AddTBInt(NodeAttr.WhoExeIt, 0, "˭ִ����", true, true);
                map.AddTBInt(NodeAttr.ReadReceipts, 0, "�Ѷ���ִ", true, true);
                map.AddTBInt(NodeAttr.CondModel, 0, "�����������ƹ���", true, true);

                // �Զ���ת.
                map.AddTBInt(NodeAttr.AutoJumpRole0, 0, "�����˾����ύ��0", false, false);
                map.AddTBInt(NodeAttr.AutoJumpRole1, 0, "�������Ѿ����ֹ�1", false, false);
                map.AddTBInt(NodeAttr.AutoJumpRole2, 0, "����������һ����ͬ2", false, false);

                // ������.
                map.AddTBInt(NodeAttr.BatchRole, 0, "������", true, true);
                map.AddTBInt(NodeAttr.BatchListCount, 12, "����������", true, true);
                map.AddTBString(NodeAttr.BatchParas, null, "����", true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.PrintDocEnable, 0, "��ӡ��ʽ", true, true);
                

                //�������.
                map.AddTBInt(NodeAttr.OutTimeDeal, 0, "��ʱ����ʽ", false, false);
                map.AddTBString(NodeAttr.DoOutTime, null, "��ʱ��������", true, false, 0, 300, 10, true);

                map.AddTBInt(NodeAttr.FormType, 1, "������", false, false);
                map.AddTBString(NodeAttr.FormUrl, "http://", "��URL", true, false, 0, 2000, 10);
                map.AddTBString(NodeAttr.DeliveryParas, null, "������SQL", true, false, 0, 300, 10, true);
                map.AddTBInt(NodeAttr.TurnToDeal, 0, "ת����", false, false);
                map.AddTBString(NodeAttr.TurnToDealDoc, null, "���ͺ���ʾ��Ϣ", true, false, 0, 1000, 10, true);
                map.AddTBInt(NodeAttr.NodePosType, 0, "λ��", false, false);
                map.AddTBInt(NodeAttr.IsCCFlow, 0, "�Ƿ��������������", false, false);
                map.AddTBString(NodeAttr.HisStas, null, "��λ", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisDeptStrs, null, "����", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisToNDs, null, "ת���Ľڵ�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.HisBillIDs, null, "����IDs", false, false, 0, 200, 10);
              //  map.AddTBString(NodeAttr.HisEmps, null, "HisEmps", false, false, 0, 3000, 10);
                map.AddTBString(NodeAttr.HisSubFlows, null, "HisSubFlows", false, false, 0, 50, 10);
                map.AddTBString(NodeAttr.PTable, null, "�����", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.ShowSheets, null, "��ʾ�ı�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.GroupStaNDs, null, "��λ����ڵ�", false, false, 0, 200, 10);
                map.AddTBInt(NodeAttr.X, 0, "X����", false, false);
                map.AddTBInt(NodeAttr.Y, 0, "Y����", false, false);

                map.AddTBString(NodeAttr.FocusField, null, "�����ֶ�", false, false, 0, 30, 10);
                map.AddTBString(NodeAttr.JumpToNodes, null, "����ת�Ľڵ�", true, false, 0, 200, 10, true);

                //��ť���Ʋ���.
               // map.AddTBString(BtnAttr.ReturnField, "", "�˻���Ϣ��д�ֶ�", true, false, 0, 50, 10, true);
                map.AddTBAtParas(500);

                // �������̲߳��� 2013-01-04
                map.AddTBInt(NodeAttr.SubFlowStartWay, 0, "���߳�������ʽ", true, false);
                map.AddTBString(NodeAttr.SubFlowStartParas, null, "��������", true, false, 0, 100, 10);
                

                map.AddTBString(NodeAttr.DocLeftWord, null, "������ߴ���(�����@���ϸ���)", true, false, 0, 200, 10);
                map.AddTBString(NodeAttr.DocRightWord, null, "�����ұߴ���(�����@���ϸ���)", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ���ܴ���ǰ�Ľڵ���
        /// </summary>
        /// <returns></returns>
        public bool CanIdoIt()
        {
            return false;
        }
        #endregion

        

        /// <summary>
        /// ɾ��ǰ���߼�����.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeDelete()
        {
            //�ж��Ƿ���Ա�ɾ��. 
             int num = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkerlist WHERE FK_Node="+this.NodeID);
             if (num != 0)
                throw new Exception("@�ýڵ�["+this.NodeID+","+this.Name+"]�д��칤�����ڣ�������ɾ����.");

            // ɾ�����Ľڵ㡣
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();

            // ɾ������.
            BP.Sys.GroupFields gfs = new BP.Sys.GroupFields();
            gfs.Delete(BP.Sys.GroupFieldAttr.EnName, md.No);

            //ɾ��������ϸ��
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls(md.No);
            dtls.Delete();

            //ɾ�����
            BP.Sys.MapFrames frams = new BP.Sys.MapFrames(md.No);
            frams.Delete();

            // ɾ����ѡ
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms(md.No);
            m2ms.Delete();

            // ɾ����չ
            BP.Sys.MapExts exts = new BP.Sys.MapExts(md.No);
            exts.Delete();

            //ɾ���ڵ����λ�Ķ�Ӧ.
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_NodeStation WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_NodeEmp  WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_NodeDept WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_NodeFlow WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_FrmNode  WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE FROM WF_CCEmp  WHERE FK_Node=" + this.NodeID);
            //ɾ��������
            BP.DA.DBAccess.RunSQL("DELETE FROM SYS_FRMATTACHMENT  WHERE FK_MapData='" + this.NodeID+"'");
            return base.beforeDelete();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="md"></param>
        private void AddDocAttr(BP.Sys.MapData md)
        {
            /*����ǵ������̣� */
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

            //attr = new BP.Sys.MapAttr();
            //attr.FK_MapData = md.No;
            //attr.HisEditType = BP.En.EditType.UnDel;
            //attr.KeyOfEn = "Title";
            //attr.Name = "����";
            //attr.MyDataType = BP.DA.DataType.AppString;
            //attr.UIContralType = UIContralType.TB;
            //attr.LGType = FieldTypeS.Normal;
            //attr.UIVisible = true;
            //attr.UIIsEnable = true;
            //attr.MinLen = 0;
            //attr.MaxLen = 300;
            //attr.Idx = 1;
            //attr.UIIsLine = true;
            //attr.Idx = -100;
            //attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "KeyWord";
            attr.Name = "�����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.UIIsLine = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = -99;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "FZ";
            attr.Name = "��ע";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.Idx = -98;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = "DW_SW";
            attr.Name = "���ĵ�λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;
            attr.Idx = 1;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_FW";
            attr.Name = "���ĵ�λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_BS";
            attr.Name = "����(��)��λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_CS";
            attr.Name = "����(��)��λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "NumPrint";
            attr.Name = "ӡ�Ʒ���";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 10;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "JMCD";
            attr.Name = "���̶ܳ�";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.UIBindKey = "JMCD";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = "PRI";
            attr.Name = "�����̶�";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.UIIsLine = false;
            attr.UIBindKey = "PRI";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "GWWH";
            attr.Name = "�����ĺ�";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.Idx = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();
        }
        /// <summary>
        /// �޸�map
        /// </summary>
        public string RepareMap()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 0)
            {
                this.CreateMap();
                return "";
            }

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (attr.IsExit(MapAttrAttr.KeyOfEn, "OID", MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "OID";
                attr.Name = "WorkID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, "FID", MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.RDT, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.RDT;
                attr.Name = "����ʱ��";  //"����ʱ��";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Tag = "1";
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.CDT, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.CDT;
                if (this.IsStartNode)
                    attr.Name = "����ʱ��"; //"����ʱ��";
                else
                    attr.Name = "���ʱ��"; //"���ʱ��";

                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "@RDT";
                attr.Tag = "1";
                attr.Insert();
            }


            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.Rec, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.Rec;
                if (this.IsStartNode == false)
                    attr.Name = "��¼��"; // "��¼��";
                else
                    attr.Name = "������"; //"������";

                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 20;
                attr.MinLen = 0;
                attr.DefVal = "@WebUser.No";
                attr.Insert();
            }


            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.Emps, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.Emps;
                attr.Name = WorkAttr.Emps;
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 400;
                attr.MinLen = 0;
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.FK_Dept, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = StartWorkAttr.FK_Dept;
                attr.Name = "����Ա����"; //"����Ա����";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;
                attr.UIBindKey = "BP.Port.Depts";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Insert();
            }

            Flow fl = new Flow(this.FK_Flow);
            if (fl.IsMD5
                && attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.MD5, MapAttrAttr.FK_MapData, md.No) == false)
            {
                /* �����MD5��������. */
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = StartWorkAttr.MD5;
                attr.UIBindKey = attr.KeyOfEn;
                attr.Name = "MD5";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.UIVisible = false;
                attr.MinLen = 0;
                attr.MaxLen = 40;
                attr.Idx = -100;
                attr.Insert();
            }

            if (this.NodePosType == NodePosType.Start)
            {

                
                
                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.Title, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.Title;
                    attr.Name = "����"; // "���̱���";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = true;
                    attr.UIIsLine = true;
                    attr.UIWidth = 251;

                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.Idx = -100;
                    attr.X = (float)171.2;
                    attr.Y = (float)68.4;
                    attr.Insert();
                }

                //if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqiren", MapAttrAttr.FK_MapData, md.No) == false)
                //{
                //    attr = new BP.Sys.MapAttr();
                //    attr.FK_MapData = md.No;
                //    attr.HisEditType = BP.En.EditType.Edit;
                //    attr.KeyOfEn = "faqiren";
                //    attr.Name = "������"; // "������";
                //    attr.MyDataType = BP.DA.DataType.AppString;
                //    attr.UIContralType = UIContralType.TB;
                //    attr.LGType = FieldTypeS.Normal;
                //    attr.UIVisible = true;
                //    attr.UIIsEnable = false;
                //    attr.UIIsLine = false;
                //    attr.MinLen = 0;
                //    attr.MaxLen = 200;
                //    attr.Idx = -100;
                //    attr.DefVal = "@WebUser.No";
                //    attr.X = (float)159.2;
                //    attr.Y = (float)102.8;
                //    attr.Insert();
                //}

                //if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqishijian", MapAttrAttr.FK_MapData, md.No) == false)
                //{
                //    attr = new BP.Sys.MapAttr();
                //    attr.FK_MapData = md.No;
                //    attr.HisEditType = BP.En.EditType.Edit;
                //    attr.KeyOfEn = "faqishijian";
                //    attr.Name = "����ʱ��"; //"����ʱ��";
                //    attr.MyDataType = BP.DA.DataType.AppDateTime;
                //    attr.UIContralType = UIContralType.TB;
                //    attr.LGType = FieldTypeS.Normal;
                //    attr.UIVisible = true;
                //    attr.UIIsEnable = false;
                //    attr.DefVal = "@RDT";
                //    attr.Tag = "1";
                //    attr.X = (float)324;
                //    attr.Y = (float)102.8;
                //    attr.Insert();
                //}


                if (attr.IsExit(MapAttrAttr.KeyOfEn, "FK_NY", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "FK_NY";
                    attr.Name = "����"; //"����";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.LGType = FieldTypeS.Normal;
                    //attr.UIBindKey = "BP.Pub.NYs";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 7;
                    attr.Insert();
                }


                if (attr.IsExit(MapAttrAttr.KeyOfEn, "MyNum", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "MyNum";
                    attr.Name = "����"; // "����";
                    attr.DefVal = "1";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.Insert();
                }
            }
            string msg = "";
            if (this.FocusField != "")
            {
                if (attr.IsExit(MapAttrAttr.KeyOfEn, this.FocusField, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    msg += "@�����ֶ� " + this.FocusField + " ���Ƿ�ɾ����.";
                    //this.FocusField = "";
                    //this.DirectUpdate();
                }
            }
            return msg;
        }
        /// <summary>
        /// ����map
        /// </summary>
        public void CreateMap()
        {
            //�����ڵ��.
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();

            md.Name = this.Name;
            if (this.HisFlow.HisDataStoreModel == DataStoreModel.SpecTable)
                md.PTable = this.HisFlow.PTable;
            md.Insert();

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "OID";
            attr.Name = "WorkID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "0";
            attr.HisEditType = BP.En.EditType.Readonly;
            attr.Insert();

            if (this.HisFlow.FlowAppType == FlowAppType.DocFlow)
            {
                this.AddDocAttr(md);
            }

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "FID";
            attr.Name = "FID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.DefVal = "0";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.RDT;
            attr.Name = "����ʱ��";  //"����ʱ��";
            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.CDT;
            if (this.IsStartNode)
                attr.Name = "����ʱ��"; //"����ʱ��";
            else
                attr.Name = "���ʱ��"; //"���ʱ��";

            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "@RDT";
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.Rec;
            if (this.IsStartNode == false)
                attr.Name = "��¼��"; // "��¼��";
            else
                attr.Name = "������"; //"������";

            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MaxLen = 20;
            attr.MinLen = 0;
            attr.DefVal = "@WebUser.No";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.Emps;
            attr.Name = "Emps";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MaxLen = 400;
            attr.MinLen = 0;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = StartWorkAttr.FK_Dept;
            attr.Name = "����Ա����"; //"����Ա����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
         //   attr.UIBindKey = "BP.Port.Depts";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MinLen = 0;
            attr.MaxLen = 32;
            attr.Insert();

            if (this.NodePosType == NodePosType.Start)
            {
                //��ʼ�ڵ���Ϣ.
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
             //   attr.edit
                attr.KeyOfEn = "Title";
                attr.Name = "����"; // "���̱���";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.UIWidth = 251;

                attr.MinLen = 0;
                attr.MaxLen = 200;
                attr.Idx = -100;
                attr.X = (float)174.83;
                attr.Y = (float)54.4;
                attr.Insert();

                

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "FK_NY";
                attr.Name = "����"; //"����";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                //attr.UIBindKey = "BP.Pub.NYs";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 7;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = "����"; // "����";
                attr.DefVal = "1";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();
               
            }
        }
    }
    /// <summary>
    /// �ڵ㼯��
    /// </summary>
    public class Nodes : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Node();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ㼯��
        /// </summary>
        public Nodes()
        {
        }
        /// <summary>
        /// �ڵ㼯��.
        /// </summary>
        /// <param name="FlowNo"></param>
        public Nodes(string fk_flow)
        {
            //   Nodes nds = new Nodes();
            this.Retrieve(NodeAttr.FK_Flow, fk_flow, NodeAttr.Step);
            //this.AddEntities(NodesCash.GetNodes(fk_flow));
            return;
        }
        #endregion

        #region ��ѯ����
        /// <summary>
        /// RetrieveAll
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            Nodes nds = Cash.GetObj(this.ToString(), Depositary.Application) as Nodes;
            if (nds == null)
            {
                nds = new Nodes();
                QueryObject qo = new QueryObject(nds);
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT Node FROM WF_Direction ");
                qo.addOr();
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT ToNode FROM WF_Direction ");
                qo.DoQuery();

                Cash.AddObj(this.ToString(), Depositary.Application, nds);
                Cash.AddObj(this.GetNewEntity.ToString(), Depositary.Application, nds);
            }

            this.Clear();
            this.AddEntities(nds);
            return this.Count;
        }
        /// <summary>
        /// ��ʼ�ڵ�
        /// </summary>
        public void RetrieveStartNode()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeAttr.NodePosType, (int)NodePosType.Start);
            qo.addAnd();
            qo.AddWhereInSQL(NodeAttr.NodeID, "SELECT FK_Node FROM WF_NodeStation WHERE FK_STATION IN (SELECT FK_STATION FROM Port_EmpSTATION WHERE FK_Emp='" + BP.Web.WebUser.No + "')");

            qo.addOrderBy(NodeAttr.FK_Flow);
            qo.DoQuery();
        }
        #endregion
    }
}
