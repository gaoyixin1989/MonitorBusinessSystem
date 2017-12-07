
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    public class GEEntityWordFrmAttr
    {
        /// <summary>
        /// �ļ�·��
        /// </summary>
        public const string FilePath="FilePath";
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public const string RDT="RDT";
        /// <summary>
        /// ����޸���
        /// </summary>
        public const string LastEditer="LastEditer";

        public const string OID="OID";

    }
    /// <summary>
    /// ͨ��ʵ��
    /// </summary>
    public class GEEntityWordFrm  : Entity
    {
#region ���ԡ�
        public int OID
        {
            get
            {
               return this.GetValIntByKey(GEEntityWordFrmAttr.OID);
            }
            set
            {
                this.SetValByKey(GEEntityWordFrmAttr.OID,value);
            }
        }
        /// <summary>
        /// ����޸���
        /// </summary>
          public string LastEditer
        {
            get
            {
               return this.GetValStringByKey(GEEntityWordFrmAttr.LastEditer);
            }
            set
            {
                this.SetValByKey(GEEntityWordFrmAttr.LastEditer,value);
            }
        }
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
           public string RDT
        {
            get
            {
               return this.GetValStringByKey(GEEntityWordFrmAttr.RDT);
            }
            set
            {
                this.SetValByKey(GEEntityWordFrmAttr.RDT,value);
            }
        }

        /// <summary>
        /// �ļ�·��
        /// </summary>
           public string FilePath
           {
               get
               {
                   return this.GetValStringByKey(GEEntityWordFrmAttr.FilePath);
               }
               set
               {
                   this.SetValByKey(GEEntityWordFrmAttr.FilePath, value);
               }
           }
#endregion ���ԡ�


        #region ���캯��
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
        public override string PKField
        {
            get
            {
                return "OID";
            }
        }
        public override string ToString()
        {
            return this.FK_MapData;
        }
        public override string ClassID
        {
            get
            {
                return this.FK_MapData;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_MapData = null;
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        public GEEntityWordFrm()
        {
        }
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GEEntityWordFrm(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GEEntityWordFrm(string fk_mapdata, object pk)
        {
            this.FK_MapData = fk_mapdata;
            this.PKVal = pk;
            this.Retrieve();
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
                if (this._enMap != null)
                    return this._enMap;

                if (this.FK_MapData == null)
                    throw new Exception("û�и�" + this.FK_MapData + "ֵ�������ܻ�ȡ����Map��");

                this._enMap = BP.Sys.MapData.GenerHisMap(this.FK_MapData);
                return this._enMap;
            }
        }
        /// <summary>
        /// GEEntitys
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.FK_MapData == null)
                    return new GEEntityWordFrms();
                return new GEEntityWordFrms(this.FK_MapData);
            }
        }
        #endregion

        private ArrayList _Dtls = null;
        public ArrayList Dtls
        {
            get
            {
                if (_Dtls == null)
                    _Dtls = new ArrayList();
                return _Dtls;
            }
        }
    }
    /// <summary>
    /// ͨ��ʵ��s
    /// </summary>
    public class GEEntityWordFrms : EntitiesOID
    {
        #region ���ػ��෽��
        public override string ToString()
        {
            //if (this.FK_MapData == null)
            //    throw new Exception("@û���� FK_MapData ��ֵ��");
            return this.FK_MapData;
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_MapData = null;
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                //if (this.FK_MapData == null)
                //    throw new Exception("@û���� FK_MapData ��ֵ��");

                if (this.FK_MapData == null)
                    return new GEEntity();
                return new GEEntity(this.FK_MapData);
            }
        }
        /// <summary>
        /// ͨ��ʵ��ID
        /// </summary>
        public GEEntityWordFrms()
        {
        }
        /// <summary>
        /// ͨ��ʵ��ID
        /// </summary>
        /// <param name="fk_mapdtl"></param>
        public GEEntityWordFrms(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        #endregion
    }
}
