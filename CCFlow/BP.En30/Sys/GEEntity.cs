
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ͨ��ʵ��
    /// </summary>
    public class GEEntity : Entity
    {
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
        public GEEntity()
        {
        }
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GEEntity(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        /// <summary>
        /// ͨ��ʵ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GEEntity(string fk_mapdata, object pk)
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
                    return new GEEntitys();
                return new GEEntitys(this.FK_MapData);
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
    public class GEEntitys : EntitiesOID
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
        public GEEntitys()
        {
        }
        /// <summary>
        /// ͨ��ʵ��ID
        /// </summary>
        /// <param name="fk_mapdtl"></param>
        public GEEntitys(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        #endregion
    }
}
