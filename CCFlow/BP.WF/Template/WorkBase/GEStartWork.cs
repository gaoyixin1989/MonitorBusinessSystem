using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ���״̬
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// ��ͣ
        /// </summary>
        Pause = 2,
        /// <summary>
        /// ͬ��
        /// </summary>
        Agree = 1,
        /// <summary>
        /// ��ͬ��
        /// </summary>
        Dissent = 0
    }
	/// <summary>
	/// ��ʼ�����ڵ�
	/// </summary>
	public class GEStartWorkAttr :WorkAttr
	{
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����text
        /// </summary>
        public const string FK_DeptText = "FK_DeptText";
	}
    /// <summary>
    /// ��ʼ�����ڵ�
    /// </summary>
    public class GEStartWork : StartWork
    {
        #region ���캯��
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        public GEStartWork()
        {
        }
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GEStartWork(int nodeid, string nodeFrmID)
        {
            this.NodeID = nodeid;
            this.NodeFrmID = nodeFrmID;
            this.SQLCash=null;

        }
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GEStartWork(int nodeid, string nodeFrmID,Int64 _oid)
        {
            this.NodeID = nodeid;
            this.NodeFrmID = nodeFrmID;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion

        #region Map
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                this._enMap = BP.Sys.MapData.GenerHisMap(this.NodeFrmID);
                return this._enMap;
            }
        }
        public override Entities GetNewEntities
        {
            get
            {
                if (this.NodeID == 0)
                    return new GEStartWorks();
                return new GEStartWorks(this.NodeID,this.NodeFrmID);
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ʼ�����ڵ�s
    /// </summary>
    public class GEStartWorks : Works
    {
        #region ���ػ��෽��
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID = 0;
        public string NodeFrmID = "";
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GEStartWork();
                return new GEStartWork(this.NodeID,this.NodeFrmID);
            }
        }
        /// <summary>
        /// ��ʼ�����ڵ�ID
        /// </summary>
        public GEStartWorks()
        {

        }
        /// <summary>
        /// ��ʼ�����ڵ�ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GEStartWorks(int nodeid,string nodeFrmID)
        {
            this.NodeID = nodeid;
            this.NodeFrmID = nodeFrmID;
        }
        #endregion
    }
}
