using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF.Template
{
    /// <summary>
    /// ��������
    /// </summary>
    public class TurnToAttr
    {
        /// <summary>
        /// ����Key
        /// </summary>
        public const string FK_Attr = "FK_Attr";
        /// <summary>
        /// ����
        /// </summary>
        public const string AttrT = "AttrT";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_Operator = "FK_Operator";
        /// <summary>
        /// �����ֵ
        /// </summary>
        public const string OperatorValue = "OperatorValue";
        /// <summary>
        /// ����ֵ
        /// </summary>
        public const string OperatorValueT = "OperatorValueT";
        /// <summary>
        /// Node
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��������
        /// </summary>
        public const string TurnToType = "TurnToType";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// TurnToURL
        /// </summary>
        public const string TurnToURL = "TurnToURL";
        /// <summary>
        /// AttrKey
        /// </summary>
        public const string AttrKey = "AttrKey";
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum TurnToType
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        Node,
        /// <summary>
        /// ����
        /// </summary>
        Flow
    }
    /// <summary>
    /// ����
    /// </summary>
    public class TurnTo : EntityMyPK
    {
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TurnToAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(TurnToAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(TurnToAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public TurnToType HisTurnToType
        {
            get
            {
                return (TurnToType)this.GetValIntByKey(TurnToAttr.TurnToType);
            }
            set
            {
                this.SetValByKey(TurnToAttr.TurnToType, (int)value);
            }
        }
        /// <summary>
        /// ת��URL
        /// </summary>
        public string TurnToURL
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.TurnToURL);
            }
            set
            {
                this.SetValByKey(TurnToAttr.TurnToURL, value);
            }
        }

        #region ʵ�ֻ����ķ�����
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Attr
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.FK_Attr);
            }
            set
            {
                if (value == null)
                    throw new Exception("FK_Attr��������Ϊnull");

                value = value.Trim();
                this.SetValByKey(TurnToAttr.FK_Attr, value);
                BP.Sys.MapAttr attr = new BP.Sys.MapAttr(value);
                this.SetValByKey(TurnToAttr.AttrKey, attr.KeyOfEn);
                this.SetValByKey(TurnToAttr.AttrT, attr.Name);
            }
        }
        /// <summary>
        /// ����Key
        /// </summary>
        public string AttrKey
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.AttrKey);
            }
        }
        /// <summary>
        /// ����Text
        /// </summary>
        public string AttrT
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.AttrT);
            }
        }
        /// <summary>
        /// ������ֵ
        /// </summary>
        public string OperatorValueT
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.OperatorValueT);
            }
            set
            {
                this.SetValByKey(TurnToAttr.OperatorValueT, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string FK_Operator
        {
            get
            {
                string s = this.GetValStringByKey(TurnToAttr.FK_Operator);
                if (s == null || s == "")
                    return "=";
                return s;
            }
            set
            {
                this.SetValByKey(TurnToAttr.FK_Operator, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string FK_OperatorExt
        {
            get
            {
                string s = this.FK_Operator.ToLower();
                switch (s)
                {
                    case "=":
                        return "dengyu";
                    case ">":
                        return "dayu";
                    case ">=":
                        return "dayudengyu";
                    case "<":
                        return "xiaoyu";
                    case "<=":
                        return "xiaoyudengyu";
                    case "!=":
                        return "budengyu";
                    case "like":
                        return "like";
                    default:
                        return s;
                }
            }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public object OperatorValue
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.OperatorValue);
            }
            set
            {
                this.SetValByKey(TurnToAttr.OperatorValue, value as string);
            }
        }
        /// <summary>
        /// ����ֵstr
        /// </summary>
        public string OperatorValueStr
        {
            get
            {
                return this.GetValStringByKey(TurnToAttr.OperatorValue);
            }
        }
        /// <summary>
        /// ����ֵint
        /// </summary>
        public int OperatorValueInt
        {
            get
            {
                return this.GetValIntByKey(TurnToAttr.OperatorValue);
            }
        }
        /// <summary>
        /// ����ֵboolen
        /// </summary>
        public bool OperatorValueBool
        {
            get
            {
                return this.GetValBooleanByKey(TurnToAttr.OperatorValue);
            }
        }
        /// <summary>
        /// workid
        /// </summary>
        public Int64 WorkID = 0;
        /// <summary>
        /// ת����Ϣ
        /// </summary>
        public string MsgOfTurnTo = "";
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public TurnTo() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="mypk">PK</param>
        public TurnTo(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        #endregion

        #region ��������
        /// <summary>
        /// ���Ĺ���
        /// </summary>
        public Work HisWork = null;
        /// <summary>
        /// ��������ܲ���ͨ��
        /// </summary>
        public virtual bool IsPassed
        {
            get
            {

                BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                attr.MyPK = this.FK_Attr;
                attr.RetrieveFromDBSources();

                if (this.HisWork.EnMap.Attrs.Contains(attr.KeyOfEn) == false)
                    throw new Exception("�ж�����������ִ���ʵ�壺" + this.HisWork.EnDesc + " ����" + this.FK_Attr + "�Ѿ�������.");

                this.MsgOfTurnTo = "@�Ա�ֵ�жϷ���ֵ " + this.HisWork.EnDesc + "." + this.FK_Attr + " (" + this.HisWork.GetValStringByKey(attr.KeyOfEn) + ") ������:(" + this.FK_Operator + ") �ж�ֵ:(" + this.OperatorValue.ToString() + ")";

                switch (this.FK_Operator.Trim().ToLower())
                {
                    case "=":  // ����� = 
                        if (this.HisWork.GetValStringByKey(attr.KeyOfEn) == this.OperatorValue.ToString())
                            return true;
                        else
                            return false;

                    case ">":
                        if (this.HisWork.GetValDoubleByKey(attr.KeyOfEn) > Double.Parse(this.OperatorValue.ToString()))
                            return true;
                        else
                            return false;

                    case ">=":
                        if (this.HisWork.GetValDoubleByKey(attr.KeyOfEn) >= Double.Parse(this.OperatorValue.ToString()))
                            return true;
                        else
                            return false;

                    case "<":
                        if (this.HisWork.GetValDoubleByKey(attr.KeyOfEn) < Double.Parse(this.OperatorValue.ToString()))
                            return true;
                        else
                            return false;

                    case "<=":
                        if (this.HisWork.GetValDoubleByKey(attr.KeyOfEn) <= Double.Parse(this.OperatorValue.ToString()))
                            return true;
                        else
                            return false;
                    case "!=":
                        if (this.HisWork.GetValDoubleByKey(attr.KeyOfEn) != Double.Parse(this.OperatorValue.ToString()))
                            return true;
                        else
                            return false;
                    case "like":
                        if (this.HisWork.GetValStringByKey(attr.KeyOfEn).IndexOf(this.OperatorValue.ToString()) == -1)
                            return false;
                        else
                            return true;
                    default:
                        throw new Exception("@û���ҵ���������..");
                }
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_TurnTo");
                map.EnDesc = "ת������";

                map.AddMyPK();
                map.AddTBInt(TurnToAttr.TurnToType, 0, "��������", true, true);
                map.AddTBString(TurnToAttr.FK_Flow, null, "����", true, true, 0, 60, 20);
                map.AddTBInt(TurnToAttr.FK_Node, 0, "�ڵ�ID", true, true);

                map.AddTBString(TurnToAttr.FK_Attr, null, "�������Sys_MapAttr", true, true, 0, 80, 20);
                map.AddTBString(TurnToAttr.AttrKey, null, "��ֵ", true, true, 0, 80, 20);
                map.AddTBString(TurnToAttr.AttrT, null, "��������", true, true, 0, 80, 20);

                map.AddTBString(TurnToAttr.FK_Operator, "=", "�������", true, true, 0, 60, 20);

                map.AddTBString(TurnToAttr.OperatorValue, "", "Ҫ�����ֵ", true, true, 0, 60, 20);
                map.AddTBString(TurnToAttr.OperatorValueT, "", "Ҫ�����ֵT", true, true, 0, 60, 20);

                map.AddTBString(TurnToAttr.TurnToURL, null, "Ҫת���URL", true, true, 0, 700, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ����s
    /// </summary>
    public class TurnTos : Entities
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public override Entity GetNewEntity
        {
            get { return new TurnTo(); }
        }
        /// <summary>
        /// ����.
        /// </summary>
        public bool IsAllPassed
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@û��Ҫ�жϵļ���.");

                foreach (TurnTo en in this)
                {
                    if (en.IsPassed == false)
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// �Ƿ�ͨ��
        /// </summary>
        public bool IsPass
        {
            get
            {
                if (this.Count == 1)
                    if (this.IsOneOfTurnToPassed)
                        return true;
                    else
                        return false;
                return false;
            }
        }
        public string MsgOfDesc
        {
            get
            {
                string msg = "";
                foreach (TurnTo c in this)
                {
                    msg += "@" + c.MsgOfTurnTo;
                }
                return msg;
            }
        }
        /// <summary>
        /// �ǲ������е�һ��passed. 
        /// </summary>
        public bool IsOneOfTurnToPassed
        {
            get
            {
                foreach (TurnTo en in this)
                {
                    if (en.IsPassed == true)
                        return true;
                }
                return false;
            }
        }
        /// <summary>
        /// ȡ������һ�������������. 
        /// </summary>
        public TurnTo GetOneOfTurnToPassed
        {
            get
            {
                foreach (TurnTo en in this)
                {
                    if (en.IsPassed == true)
                        return en;
                }
                throw new Exception("@û�����������");
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID = 0;
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public TurnTos()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        public TurnTos(string fk_flow)
        {
            this.Retrieve(TurnToAttr.FK_Flow, fk_flow);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="ct">����</param>
        /// <param name="nodeID">�ڵ�</param>
        public TurnTos(TurnToType ct, int nodeID, Int64 workid)
        {
            this.NodeID = nodeID;
            this.Retrieve(TurnToAttr.FK_Node, nodeID, TurnToAttr.TurnToType, (int)ct);

            foreach (TurnTo en in this)
                en.WorkID = workid;
        }
        /// <summary>
        /// ����
        /// </summary>
        public string TurnToitionDesc
        {
            get
            {
                return "";
            }
        }
        #endregion
    }
}
