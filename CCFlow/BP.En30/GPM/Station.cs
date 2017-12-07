using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// ��λ����
    /// </summary>
    public class StationAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��λ����
        /// </summary>
        public const string FK_StationType = "FK_StationType";
        /// <summary>
        /// ����Ҫ��
        /// </summary>
        public const string Makings = "Makings";
        /// <summary>
        /// ְ��Ҫ��
        /// </summary>
        public const string DutyReq = "DutyReq";
    }
    /// <summary>
    /// ��λ
    /// </summary>
    public class Station : EntityNoName
    {
        #region ����
        public string FK_StationType
        {
            get
            {
                return this.GetValStrByKey(StationAttr.FK_StationType);
            }
            set
            {
                this.SetValByKey(StationAttr.FK_StationType, value);
            }
        }
        #endregion

        #region ʵ�ֻ����ķ�����
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��λ
        /// </summary> 
        public Station()
        {
        }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <param name="_No"></param>
        public Station(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Station");
                map.EnDesc = "��λ"; // "��λ";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.CodeStruct = "2"; // ��󼶱��� 7 .

                map.AddTBStringPK(EmpAttr.No, null, "���", true, true, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 200);
                map.AddDDLEntities(StationAttr.FK_StationType, null, "����", new StationTypes(), true);
                map.AddTBStringDoc(StationAttr.DutyReq, null, "ְ��Ҫ��", true, false, true);
                map.AddTBStringDoc(StationAttr.Makings, null, "����Ҫ��", true, false, true);
                map.AddSearchAttr(StationAttr.FK_StationType);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��λs
    /// </summary>
    public class Stations : EntitiesNoName
    {
        /// <summary>
        /// ��λ
        /// </summary>
        public Stations() { }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BP.GPM.Station();
            }
        }
    }
}
