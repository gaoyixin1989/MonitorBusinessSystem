using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF.Rpt
{
    /// <summary>
    /// �����λ
    /// </summary>
    public class RptStationAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FK_Rpt = "FK_Rpt";
        /// <summary>
        /// ��λ
        /// </summary>
        public const string FK_Station = "FK_Station";
        #endregion
    }
    /// <summary>
    /// RptStation ��ժҪ˵����
    /// </summary>
    public class RptStation : Entity
    {

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsView = true;
                    uac.IsDelete = true;
                    uac.IsInsert = true;
                    uac.IsUpdate = true;
                    uac.IsAdjunct = true;
                }
                return uac;
            }
        }

        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Rpt, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(RptStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///��λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// �����λ
        /// </summary> 
        public RptStation() { }
        /// <summary>
        /// �����λ��Ӧ
        /// </summary>
        /// <param name="_empoid">����ID</param>
        /// <param name="wsNo">��λ���</param> 	
        public RptStation(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Station = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("Sys_RptStation");
                map.EnDesc = "�����λ��Ӧ��Ϣ";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(RptStationAttr.FK_Rpt, null, "����", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(RptStationAttr.FK_Station, null, "��λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �����λ 
    /// </summary>
    public class RptStations : Entities
    {
        #region ����
        /// <summary>
        /// �������λ����
        /// </summary>
        public RptStations() { }
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new RptStation();
            }
        }
        #endregion
    }
}
