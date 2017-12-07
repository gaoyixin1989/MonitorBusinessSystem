using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// ����ָ���ķ�ʽ
    /// </summary>
    public enum PushWay
    {
        /// <summary>
        /// ָ���ڵ�Ĺ�����Ա
        /// </summary>
        NodeWorker,
        /// <summary>
        /// ִ�еĹ�����Աs
        /// </summary>
        SpecEmps,
        /// <summary>
        /// ָ���Ĺ�����λs
        /// </summary>
        SpecStations,
        /// <summary>
        /// ָ���Ĳ�����Ա
        /// </summary>
        SpecDepts,
        /// <summary>
        /// ָ����SQL
        /// </summary>
        SpecSQL,
        /// <summary>
        /// ����ϵͳָ�����ֶ�
        /// </summary>
        ByParas
    }
	/// <summary>
	/// ��Ϣ��������
	/// </summary>
    public class PushMsgAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �¼�
        /// </summary>
        public const string FK_Event = "FK_Event";
        /// <summary>
        /// ���ͷ�ʽ
        /// </summary>
        public const string PushWay = "PushWay";
        /// <summary>
        /// ���ʹ�������
        /// </summary>
        public const string PushDoc = "PushDoc";
        /// <summary>
        /// ���ʹ������� tag.
        /// </summary>
        public const string Tag = "Tag";

        #region ��Ϣ����.
        /// <summary>
        /// �Ƿ����÷����ʼ�
        /// </summary>
        public const string MsgMailEnable = "MsgMailEnable";
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public const string MailTitle = "MailTitle";
        /// <summary>
        /// ��Ϣ����ģ��
        /// </summary>
        public const string MailDoc = "MailDoc";
        /// <summary>
        /// �Ƿ����ö���
        /// </summary>
        public const string SMSEnable = "SMSEnable";
        /// <summary>
        /// ��������ģ��
        /// </summary>
        public const string SMSDoc = "SMSDoc";
        /// <summary>
        /// �Ƿ����ͣ�
        /// </summary>
        public const string MobilePushEnable = "MobilePushEnable";
        #endregion ��Ϣ����.
        
    }
	/// <summary>
	/// ��Ϣ����
	/// </summary>
    public class PushMsg : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// �¼�
        /// </summary>
        public string FK_Event
        {
            get
            {
                return this.GetValStringByKey(PushMsgAttr.FK_Event);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.FK_Event, value);
            }
        }
        public int PushWay
        {
            get
            {
                return this.GetValIntByKey(PushMsgAttr.PushWay);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.PushWay, value);
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(PushMsgAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.FK_Node, value);
            }
        }
        public string PushDoc
        {
            get
            {
                string s = this.GetValStringByKey(PushMsgAttr.PushDoc);
                if (string.IsNullOrEmpty(s) == true)
                    s = "";
                return s;
            }
            set
            {
                this.SetValByKey(PushMsgAttr.PushDoc, value);
            }
        }
        public string Tag
        {
            get
            {
                string s = this.GetValStringByKey(PushMsgAttr.Tag);
                if (string.IsNullOrEmpty(s) == true)
                    s = "";
                return s;
            }
            set
            {
                this.SetValByKey(PushMsgAttr.Tag, value);
            }
        }
        #endregion

        #region �¼���Ϣ.
        public bool MobilePushEnable
        {
            get
            {
                return this.GetValBooleanByKey(PushMsgAttr.MobilePushEnable);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.MobilePushEnable, value);
            }
        }
        /// <summary>
        /// �Ƿ������ʼ�����?
        /// </summary>
        public bool MsgMailEnable
        {
            get
            {
                return this.GetValBooleanByKey(PushMsgAttr.MsgMailEnable);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.MsgMailEnable, value);
            }
        }
        public string MailTitle
        {
            get
            {
                string str = this.GetValStrByKey(PushMsgAttr.MailTitle);
                if (string.IsNullOrEmpty(str) == false)
                    return str;
                switch (this.FK_Event)
                {
                    case EventListOfNode.SendSuccess:
                        return "�¹���{{Title}},������@WebUser.No,@WebUser.Name";
                    case EventListOfNode.ShitAfter:
                        return "�ƽ������¹���{{Title}},�ƽ���@WebUser.No,@WebUser.Name";
                    case EventListOfNode.ReturnAfter:
                        return "���˻���{{Title}},�˻���@WebUser.No,@WebUser.Name";
                    case EventListOfNode.UndoneAfter:
                        return "����������{{Title}},������@WebUser.No,@WebUser.Name";
                    case EventListOfNode.AskerReAfter:
                        return "��ǩ�¹���{{Title}},������@WebUser.No,@WebUser.Name";
                        break;
                    default:
                        throw new Exception("@���¼�����û�ж���Ĭ�ϵ���Ϣģ��:" + this.FK_Event);
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string MailTitle_Real
        {
            get
            {
                string str = this.GetValStrByKey(PushMsgAttr.MailTitle);
                return str;
            }
            set
            {
                this.SetValByKey(PushMsgAttr.MailTitle, value);
            }
        }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string MailDoc_Real
        {
            get
            {
                return this.GetValStrByKey(PushMsgAttr.MailDoc);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.MailDoc, value);
            }
        }
        public string MailDoc
        {
            get
            {
                string str = this.GetValStrByKey(PushMsgAttr.MailDoc);
                if (string.IsNullOrEmpty(str) == false)
                    return str;
                switch (this.FK_Event)
                {
                    case EventListOfNode.SendSuccess:
                        str += "\t\n����:";
                        str += "\t\n    ���¹���{{Title}}��Ҫ������, �������򿪹���{Url} .";
                        str += "\t\n��! ";
                        str += "\t\n    @WebUser.No, @WebUser.Name";
                        str += "\t\n    @RDT";
                        break;
                    case EventListOfNode.ReturnAfter:
                        str += "\t\n����:";
                        str += "\t\n    ����{{Title}}���˻�����, �������򿪹���{Url} .";
                        str += "\t\n ��! ";
                        str += "\t\n    @WebUser.No,@WebUser.Name";
                        str += "\t\n    @RDT";
                        break;
                    case EventListOfNode.ShitAfter:
                        str += "\t\n����:";
                        str += "\t\n    �ƽ������Ĺ���{{Title}}, �������򿪹���{Url} .";
                        str += "\t\n ��! ";
                        str += "\t\n    @WebUser.No,@WebUser.Name";
                        str += "\t\n    @RDT";
                        break;
                    case EventListOfNode.UndoneAfter:
                        str += "\t\n����:";
                        str += "\t\n    �ƽ������Ĺ���{{Title}}, �������򿪹���{Url} .";
                        str += "\t\n ��! ";
                        str += "\t\n    @WebUser.No,@WebUser.Name";
                        str += "\t\n    @RDT";
                        break;
                    case EventListOfNode.AskerReAfter: //��ǩ.
                        str += "\t\n����:";
                        str += "\t\n    �ƽ������Ĺ���{{Title}}, �������򿪹���{Url} .";
                        str += "\t\n ��! ";
                        str += "\t\n    @WebUser.No,@WebUser.Name";
                        str += "\t\n    @RDT";
                        break;
                    default:
                        throw new Exception("@���¼�����û�ж���Ĭ�ϵ���Ϣģ��:" + this.FK_Event);
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// �Ƿ����ö��ŷ���
        /// </summary>
        public bool SMSEnable
        {
            get
            {
                return this.GetValBooleanByKey(PushMsgAttr.SMSEnable);
            }
            set
            {
                this.SetValByKey(PushMsgAttr.SMSEnable, value);
            }
        }
        /// <summary>
        /// ����ģ������
        /// </summary>
        public string SMSDoc_Real
        {
            get
            {
                string str = this.GetValStrByKey(PushMsgAttr.SMSDoc);
                return str;
            }
            set
            {
                this.SetValByKey(PushMsgAttr.SMSDoc, value);
            }
        }
        /// <summary>
        /// ����ģ������
        /// </summary>
        public string SMSDoc
        {
            get
            {
                string str = this.GetValStrByKey(PushMsgAttr.SMSDoc);
                if (string.IsNullOrEmpty(str) == false)
                    return str;

                switch (this.FK_Event)
                {
                    case EventListOfNode.SendSuccess:
                        str = "���¹���{{Title}}��Ҫ������, ������:@WebUser.No, @WebUser.Name,��{Url} .";
                        break;
                    case EventListOfNode.ReturnAfter:
                        str = "����{{Title}}���˻�,�˻���:@WebUser.No, @WebUser.Name,��{Url} .";
                        break;
                    case EventListOfNode.ShitAfter:
                        str = "�ƽ�����{{Title}},�ƽ���:@WebUser.No, @WebUser.Name,��{Url} .";
                        break;
                    case EventListOfNode.UndoneAfter:
                        str = "��������{{Title}},������:@WebUser.No, @WebUser.Name,��{Url}.";
                        break;
                    case EventListOfNode.AskerReAfter: //��ǩ.
                        str = "������ǩ{{Title}},��ǩ��:@WebUser.No, @WebUser.Name,��{Url}.";
                        break;
                    default:
                        throw new Exception("@���¼�����û�ж���Ĭ�ϵ���Ϣģ��:" + this.FK_Event);
                        break;
                }
                return str;
            }
            set
            {
                this.SetValByKey(PushMsgAttr.SMSDoc, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public PushMsg()
        {

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

                Map map = new Map("WF_PushMsg");
                map.EnDesc = "��Ϣ����";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();

                map.AddTBInt(PushMsgAttr.FK_Node, 0, "�ڵ�", true, false);
                map.AddTBString(PushMsgAttr.FK_Event, null, "FK_Event", true, false, 0, 15, 10);
                //map.AddTBInt(PushMsgAttr.PushWay, 0, "���ͷ�ʽ", true, false);
                map.AddDDLSysEnum(PushMsgAttr.PushWay, 0, "���ͷ�ʽ", true, false, PushMsgAttr.PushWay, "@0=����ָ���ڵ�Ĺ�����Ա@1=����ָ���Ĺ�����Ա@2=����ָ���Ĺ�����λ@3=����ָ���Ĳ���@4=����ָ����SQL@5=����ϵͳָ�����ֶ�");

                //��������.
                map.AddTBString(PushMsgAttr.PushDoc, null, "���ͱ�������", true, false, 0, 3500, 10);
                map.AddTBString(PushMsgAttr.Tag, null, "Tag", true, false, 0, 500, 10);


                #region ��Ϣ����.
                map.AddBoolean(FrmEventAttr.MsgMailEnable, true, "�Ƿ������ʼ����ͣ�(������þ�Ҫ�����ʼ�ģ�棬֧��ccflow���ʽ��)", true, true, true);
                map.AddTBString(FrmEventAttr.MailTitle, null, "�ʼ�����ģ��", true, false, 0, 200, 20, true);
                map.AddTBStringDoc(FrmEventAttr.MailDoc, null, "�ʼ�����ģ��", true, false, true);

                //�Ƿ������ֻ����ţ�
                map.AddBoolean(FrmEventAttr.SMSEnable, false, "�Ƿ����ö��ŷ��ͣ�(������þ�Ҫ���ö���ģ�棬֧��ccflow���ʽ��)", true, true, true);
                map.AddTBStringDoc(FrmEventAttr.SMSDoc, null, "��������ģ��", true, false, true);
                map.AddBoolean(FrmEventAttr.MobilePushEnable, true, "�Ƿ����͵��ֻ���pad�ˡ�", true, true, true);
                #endregion ��Ϣ����.

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
             this.MyPK = this.FK_Event + "_" + this.FK_Node + "_" + this.PushWay;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// ��Ϣ����
	/// </summary>
    public class PushMsgs : EntitiesMyPK
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public PushMsgs() { }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="fk_flow"></param>
        public PushMsgs(string fk_flow)
        {
            

            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(PushMsgAttr.FK_Node, "SELECT NodeID FROM WF_Node WHERE FK_Flow='" + fk_flow + "'");
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PushMsg();
            }
        }
    }
}
