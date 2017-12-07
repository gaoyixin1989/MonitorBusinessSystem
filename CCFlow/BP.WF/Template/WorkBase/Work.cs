using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.Port;
using System.Security.Cryptography;
using System.Text;
using BP.WF.XML;

namespace BP.WF
{
    /// <summary>
    /// ϵͳԼ���ֶ��б�
    /// </summary>
    public class WorkSysFieldAttr
    {
        /// <summary>
        /// ������Ա�ֶ�
        /// ���ڽڵ㷢��ʱȷ����һ���ڵ������Ա, �����뷢���ʼ���ѡ�������.
        /// ��������һ���ڵ����Ե� ���ʹ�����ѡ�񡾰���SysSendEmps�ֶμ��㡿��Ч��
        /// </summary>
        public const string SysSendEmps = "SysSendEmps";
        /// <summary>
        /// ������Ա�ֶ�
        /// ��ǰ�Ĺ�����Ҫ����ʱ, ����Ҫ�ڵ�ǰ�ڵ���У����Ӵ��ֶΡ�
        /// �����ڽڵ����Եĳ��͹�����ѡ�񡾰���SysCCEmps�ֶμ��㡿��Ч��
        /// ����ж��������Ա���ֶεĽ���ֵ�ö��ŷֿ�������: zhangsan,lisi,wangwu
        /// </summary>
        public const string SysCCEmps = "SysCCEmps";
        /// <summary>
        /// ����Ӧ�������
        /// ˵�����ڿ�ʼ�ڵ�������Ӵ��ֶΣ�������Ǵ�����Ӧ����ɵ�����.
        /// �û��ڷ��ͺ�ͻ�Ѵ�ֵ��¼��WF_GenerWorkFlow �� SDTOfFlow ��.
        /// ���ֶ���ʾ�ڴ��죬������;��ɾ���������б���.
        /// </summary>
        public const string SysSDTOfFlow = "SysSDTOfFlow";
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// ˵�����ڿ�ʼ�ڵ�������Ӵ��ֶΣ�������Ǵ˽ڵ����һ���ڵ�Ӧ����ɵ�����.
        /// </summary>
        public const string SysSDTOfNode = "SysSDTOfNode";
        /// <summary>
        /// PWorkID ����
        /// </summary>
        public const string PWorkID = "PWorkID";
        /// <summary>
        /// FromNode
        /// </summary>
        public const string FromNode = "FromNode";
        /// <summary>
        /// �Ƿ���Ҫ�Ѷ���ִ
        /// </summary>
        public const string SysIsReadReceipts = "SysIsReadReceipts";

        #region ������������ص��ֶ�
        /// <summary>
        /// ���
        /// </summary>
        public const string EvalEmpNo = "EvalEmpNo";
        /// <summary>
        /// ����
        /// </summary>
        public const string EvalEmpName = "EvalEmpName";
        /// <summary>
        /// ��ֵ
        /// </summary>
        public const string EvalCent = "EvalCent";
        /// <summary>
        /// ����
        /// </summary>
        public const string EvalNote = "EvalNote";
        #endregion ������������ص��ֶ�
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class WorkAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ��¼��Text
        /// </summary>
        public const string RecText = "RecText";
        /// <summary>
        /// Emps
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// MyNum
        /// </summary>
        public const string MyNum = "MyNum";
        /// <summary>
        /// MD5
        /// </summary>
        public const string MD5 = "MD5";
        #endregion
    }
    /// <summary>
    /// WorkBase ��ժҪ˵����
    /// ����
    /// </summary>
    abstract public class Work : Entity
    {
        /// <summary>
        /// ���MD5ֵ�Ƿ�ͨ��
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsPassCheckMD5()
        {
            string md51 = this.GetValStringByKey(WorkAttr.MD5);
            string md52 = Glo.GenerMD5(this);
            if (md51 != md52)
                return false;
            return true;
        }

        #region ��������(���������)
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
        /// <summary>
        /// classID
        /// </summary>
        public override string ClassID
        {
            get
            {
                return "ND"+this.HisNode.NodeID;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public virtual Int64 FID
        {
            get
            {
                if (this.HisNode.HisRunModel != RunModel.SubThread)
                    return 0;
                return this.GetValInt64ByKey(WorkAttr.FID);
            }
            set
            {
                if (this.HisNode.HisRunModel != RunModel.SubThread)
                    this.SetValByKey(WorkAttr.FID, 0);
                else
                    this.SetValByKey(WorkAttr.FID, value);
            }
        }
        /// <summary>
        /// workid,����ǿյľͷ��� 0 . 
        /// </summary>
        public virtual Int64 OID
        {
            get
            {
                return this.GetValInt64ByKey(WorkAttr.OID);
            }
            set
            {
                this.SetValByKey(WorkAttr.OID, value);
            }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string CDT
        {
            get
            {
                string str = this.GetValStringByKey(WorkAttr.CDT);
                if (str.Length < 5)
                    this.SetValByKey(WorkAttr.CDT, DataType.CurrentDataTime);

                return this.GetValStringByKey(WorkAttr.CDT);
            }
        }
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.Emps);
            }
            set
            {
                this.SetValByKey(WorkAttr.Emps, value);
            }
        }
        public override int RetrieveFromDBSources()
        {
            try
            {
                return base.RetrieveFromDBSources();
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        public int RetrieveFID()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereIn(WorkAttr.OID, "(" + this.FID + "," + this.OID + ")");
            int i = qo.DoQuery();
            if (i == 0)
            {
                if (SystemConfig.IsDebug == false)
                {
                    this.CheckPhysicsTable();
                    throw new Exception("@�ڵ�[" + this.EnDesc + "]���ݶ�ʧ��WorkID=" + this.OID + " FID=" + this.FID + " sql=" + qo.SQL);
                }
            }
            return i;
        }
        public override int Retrieve()
        {
            try
            {
                return base.Retrieve();
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.RDT);
            }
        }
        public string RDT_Date
        {
            get
            {
                try
                {
                    return DataType.ParseSysDate2DateTime(this.RDT).ToString(DataType.SysDataFormat);
                }
                catch
                {
                    return DataType.CurrentData;
                }
            }
        }
        public DateTime RDT_DateTime
        {
            get
            {
                try
                {
                    return DataType.ParseSysDate2DateTime(this.RDT_Date);
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }
        public string Record_FK_NY
        {
            get
            {
                return this.RDT.Substring(0, 7);
            }
        }
        /// <summary>
        /// ��¼��
        /// </summary>
        public string Rec
        {
            get
            {
                string str = this.GetValStringByKey(WorkAttr.Rec);
                if (str == "")
                    this.SetValByKey(WorkAttr.Rec, BP.Web.WebUser.No);

                return this.GetValStringByKey(WorkAttr.Rec);
            }
            set
            {
                this.SetValByKey(WorkAttr.Rec, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public Emp RecOfEmp
        {
            get
            {
                return new Emp(this.Rec);
            }
        }
        /// <summary>
        /// ��¼������
        /// </summary>
        public string RecText
        {
            get
            {
                try
                {
                    return this.HisRec.Name;
                }
                catch
                {
                    return this.Rec;
                }
            }
            set
            {
                this.SetValByKey("RecText", value);
            }
        }
       
        private Node _HisNode = null;
        /// <summary>
        /// �����Ľڵ�.
        /// </summary>
        public Node HisNode
        {
            get
            {
                if (this._HisNode == null)
                {
                    this._HisNode = new Node(this.NodeID);
                }
                return _HisNode;
            }
            set
            {
                _HisNode = value;
            }
        }
        /// <summary>
        /// �ӱ�.
        /// </summary>
        public MapDtls HisMapDtls
        {
            get
            {
                return this.HisNode.MapData.MapDtls;
            }
        }
        /// <summary>
        /// �ӱ�.
        /// </summary>
        public FrmAttachments HisFrmAttachments
        {
            get
            {
                return this.HisNode.MapData.FrmAttachments;
            }
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// �������
        /// </summary>
        public int SpanDays
        {
            get
            {
                if (this.CDT == this.RDT)
                    return 0;
                return DataType.SpanDays(this.RDT, this.CDT);
            }
        }
        /// <summary>
        /// �õ��ӹ�����ɵ����ڵ�����
        /// </summary>
        /// <returns></returns>
        public int GetCDTSpanDays(string todata)
        {
            return DataType.SpanDays(this.CDT, todata);
        }
        /// <summary>
        /// ���ļ�¼��
        /// </summary>
        public Emp HisRec
        {
            get
            {
              //  return new Emp(this.Rec);
                Emp emp = this.GetValByKey("HisRec"+this.Rec) as Emp;
                if (emp == null)
                {
                    emp = new Emp(this.Rec);
                    this.SetValByKey("HisRec" + this.Rec, emp);
                }
                return emp;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        protected Work()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="oid">WFOID</param>		 
        protected Work(Int64 oid)
        {
            this.SetValByKey(EntityOIDAttr.OID, oid);
            this.Retrieve();
        }
        #endregion

        #region Node.xml Ҫ���õ���Ϣ.
        /// <summary>
        /// ���������������е��������
        /// ����һЩ��Ҫ������.
        /// </summary>
        /// <returns></returns>
        private string GenerParas_del()
        {
            string paras = "*WorkID" + this.OID + "*UserNo=" + this.Rec ;
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal)
                    continue;

                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyFieldType == FieldType.NormalVirtual)
                    continue;

                if (attr.Key == WorkAttr.Rec
                    || attr.Key == "OID"
                    )
                    continue;

                paras += "*" + attr.Key + "=" + this.GetValStringByKey(attr.Key);
            }
            return paras;
        }
        public virtual string WorkEndInfo
        {
            get
            {
                string tp = "";
                //FAppSets sets = new FAppSets(this.NodeID);
                //foreach (FAppSet set in sets)
                //{
                //    if (set.DoWhat.Contains("?"))
                //        tp += "[<a href=\"javascript:WinOpen('" + set.DoWhat + "&WorkID=" + this.OID + "' ,'sd');\" ><img src='/WF/Img/Btn/Do.gif' border=0/>" + set.Name + "</a>]";
                //    else
                //        tp += "[<a href=\"javascript:WinOpen('" + set.DoWhat + "?WorkID=" + this.OID + "' ,'sd');\" ><img src='/WF/Img/Btn/Do.gif' border=0/>" + set.Name + "</a>]";
                //}
                if (this.HisNode.IsHaveSubFlow)
                {
                    NodeFlows flows = new NodeFlows(this.HisNode.NodeID);
                    foreach (NodeFlow fl in flows)
                    {
                        tp += "[<a href='CallSubFlow.aspx?FID=" + this.OID + "&FK_Flow=" + fl.FK_Flow + "&FK_FlowFrom=" + this.HisNode.FK_Flow + "' ><img src='/WF/Img/Btn/Do.gif' border=0/>" + fl.FK_FlowT + "</a>]";
                    }
                }
                if (tp.Length > 0)
                    return "<div align=left>" + tp + "</div>";
                return tp;
            }
        }
        /// <summary>
        /// ����Ҫִ�е�url.
        /// </summary>
        public string GenerNextUrl()
        {
            string appName = BP.Sys.Glo.Request.ApplicationPath;
            string ip = SystemConfig.AppSettings["CIP"];
            if (ip == null || ip == "")
                throw new Exception("@��û������CIP");
            return "http://" + ip + "/" + appName + "/WF/Port.aspx?UserNo=" + BP.Web.WebUser.No + "&DoWhat=DoNode&WorkID=" + this.OID + "&FK_Node=" + this.HisNode.NodeID + "&Key=MyKey";
        }
        #endregion

        #region ��Ҫ����д�ķ���
        public void DoAutoFull(Attr attr)
        {
            if (this.OID == 0)
                return;

            if (attr.AutoFullDoc == null || attr.AutoFullDoc.Length == 0)
                return;

            string objval = null;

            // ���������Ҫ�ᴿ��������ȥ��
            switch (attr.AutoFullWay)
            {
                case BP.En.AutoFullWay.Way0:
                    return;
                case BP.En.AutoFullWay.Way1_JS:
                    break;
                case BP.En.AutoFullWay.Way2_SQL:
                    string sql = attr.AutoFullDoc;
                    Attrs attrs1 = this.EnMap.Attrs;
                    foreach (Attr a1 in attrs1)
                    {
                        if (a1.IsNum)
                            sql = sql.Replace("@" + a1.Key, this.GetValStringByKey(a1.Key));
                        else
                            sql = sql.Replace("@" + a1.Key, "'" + this.GetValStringByKey(a1.Key) + "'");
                    }

                    objval = DBAccess.RunSQLReturnString(sql);
                    break;
                case BP.En.AutoFullWay.Way3_FK:
                    try
                    {
                        string sqlfk = "SELECT @Field FROM @Table WHERE No=@AttrKey";
                        string[] strsFK = attr.AutoFullDoc.Split('@');
                        foreach (string str in strsFK)
                        {
                            if (str == null || str.Length == 0)
                                continue;

                            string[] ss = str.Split('=');
                            if (ss[0] == "AttrKey")
                                sqlfk = sqlfk.Replace('@' + ss[0], "'" + this.GetValStringByKey(ss[1]) + "'");
                            else
                                sqlfk = sqlfk.Replace('@' + ss[0], ss[1]);
                        }
                        sqlfk = sqlfk.Replace("''", "'");

                        objval = DBAccess.RunSQLReturnString(sqlfk);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@�ڴ����Զ���ɣ����[" + attr.Key + ";" + attr.Desc + "],ʱ���ִ����쳣��Ϣ��" + ex.Message);
                    }
                    break;
                case BP.En.AutoFullWay.Way4_Dtl:
                    string mysql = "SELECT @Way(@Field) FROM @Table WHERE RefPK='"+ this.OID+"'";
                    string[] strs = attr.AutoFullDoc.Split('@');
                    foreach (string str in strs)
                    {
                        if (str == null || str.Length == 0)
                            continue;

                        string[] ss = str.Split('=');
                        mysql = mysql.Replace('@' + ss[0], ss[1]);
                    }
                    objval = DBAccess.RunSQLReturnString(mysql);
                    break;
                default:
                    throw new Exception("δ�漰�������͡�");
            }
            if (objval == null)
                return;

            if (attr.IsNum)
            {
                try
                {
                    decimal d = decimal.Parse(objval);
                    this.SetValByKey(attr.Key, objval);
                }
                catch
                {
                }
            }
            else
            {
                this.SetValByKey(attr.Key, objval);
            }
            return;
        }
      
        #endregion

        #region  ��д����ķ�����
        /// <summary>
        /// ����ָ����OID Insert.
        /// </summary>
        public void InsertAsOID(Int64 oid)
        {
            this.SetValByKey("OID", oid);
            this.RunSQL(SqlBuilder.Insert(this));
        }
        /// <summary>
        /// ����ָ����OID ����
        /// </summary>
        /// <param name="oid"></param>
        public void SaveAsOID(Int64 oid)
        {
            this.SetValByKey("OID", oid);
            if (this.RetrieveNotSetValues().Rows.Count == 0)
                this.InsertAsOID(oid);
            this.Update();
        }
        /// <summary>
        /// ����ʵ����Ϣ
        /// </summary>
        public new int Save()
        {
            if (this.OID <= 10)
                throw new Exception("@û�и�WorkID��ֵ,���ܱ���.");
            if (this.Update() == 0)
            {
                this.InsertAsOID(this.OID);
                return 0;
            }
            return 1;
        }
        public override void Copy(DataRow dr)  {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.Key == WorkAttr.CDT
                   || attr.Key == WorkAttr.RDT
                   || attr.Key == WorkAttr.Rec
                   || attr.Key == WorkAttr.FID
                   || attr.Key == WorkAttr.OID
                   || attr.Key == "No"
                   || attr.Key == "Name")
                    continue;

                try
                {
                    this.SetValByKey(attr.Key, dr[attr.Key]);
                }
                catch
                {
                }
            }
        }
        public override void Copy(Entity fromEn)
        {
            if (fromEn == null)
                return;
            Attrs attrs = fromEn.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.Key == WorkAttr.CDT
                    || attr.Key == WorkAttr.RDT
                    || attr.Key == WorkAttr.Rec
                    || attr.Key == WorkAttr.FID
                    || attr.Key == WorkAttr.OID
                    || attr.Key == WorkAttr.Emps
                    || attr.Key == "No"
                    || attr.Key == "Name")
                    continue;
                this.SetValByKey(attr.Key, fromEn.GetValByKey(attr.Key));
            }
        }
        /// <summary>
        /// ɾ����������ҲҪɾ��������ϸ����
        /// </summary>
        protected override void afterDelete()
        {
            #warning ɾ������ϸ���п������������Ӱ��.
            //MapDtls dtls = this.HisNode.MapData.MapDtls;
            //foreach (MapDtl dtl in dtls)��
            //    DBAccess.RunSQL("DELETE FROM  " + dtl.PTable + " WHERE RefPK=" + this.OID);

            base.afterDelete();
        }
        #endregion

        #region  ��������
        /// <summary>
        /// ����֮ǰ
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdate()
        {
            #region ���⴦��
            try
            {
                if (this.GetValStrByKey("WFState") == "Runing")
                {
                    this.SetValByKey("WFState", (int)WFState.Runing);
                }
            }
            catch (Exception ex)
            {
            }
            #endregion

            return base.beforeUpdate();
        }
        /// <summary>
        /// ֱ�ӵı���ǰҪ���Ĺ���
        /// </summary>
        public virtual void BeforeSave()
        {
            //ִ���Զ�����.
            this.AutoFull();
            // ִ�б���ǰ���¼���

            this.HisNode.HisFlow.DoFlowEventEntity(EventListOfNode.SaveBefore, this.HisNode, this.HisNode.HisWork, null);
        }
        /// <summary>
        /// ֱ�ӵı���
        /// </summary>
        public new void DirectSave()
        {
            this.beforeUpdateInsertAction();
            if (this.DirectUpdate() == 0)
            {
                this.SetValByKey(WorkAttr.RDT, DateTime.Now.ToString("yyyy-MM-dd"));
                this.DirectInsert();
            }
        }
        public string NodeFrmID = "";
        protected int _nodeID = 0;
        public int NodeID
        {
            get
            {
                if (_nodeID == 0)
                    throw new Exception("��û�и�_Node��ֵ��");
                return this._nodeID;
            }
            set
            {
                if (this._nodeID != value)
                {
                    this._nodeID = value;
                    this._enMap = null;
                }
                this._nodeID = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���� ����
    /// </summary>
    abstract public class Works : EntitiesOID
    {
        #region ���췽��
        /// <summary>
        /// ��Ϣ�ɼ�����
        /// </summary>
        public Works()
        {
        }
        #endregion

        #region ��ѯ����
        /// <summary>
        /// ��ѯ����(���ʺ���˽ڵ��ѯ)
        /// </summary>
        /// <param name="empId">������Ա</param>
        /// <param name="nodeStat">�ڵ�״̬</param>
        /// <param name="fromdate">��¼���ڴ�</param>
        /// <param name="todate">��¼���ڵ�</param>
        /// <returns></returns>
        public int Retrieve(string key, string empId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
                qo.AddWhere(WorkAttr.Rec, empId);

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);

            if (key.Trim().Length == 0)
                return qo.DoQuery();
            else
            {
                if (key.IndexOf("%") == -1)
                    key = "%" + key + "%";
                Entity en = this.GetNewEntity;
                qo.addAnd();
                qo.addLeftBracket();
                qo.AddWhere(en.PK, " LIKE ", key);
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.RefText)
                        continue;
                    if (attr.UIContralType == UIContralType.DDL || attr.UIContralType == UIContralType.CheckBok)
                        continue;
                    qo.addOr();
                    qo.AddWhere(attr.Key, " LIKE ", key);
                }
                qo.addRightBracket();
                return qo.DoQuery();
            }
        }
        public int Retrieve(string fromDataTime, string toDataTime)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = 90000;
            qo.AddWhere(WorkAttr.RDT, " >=", fromDataTime);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, " <= ", toDataTime);
            return qo.DoQuery();
        }
        #endregion
    }
}
