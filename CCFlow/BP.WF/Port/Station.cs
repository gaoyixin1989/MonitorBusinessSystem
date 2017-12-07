using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF.Port
{	 
	/// <summary>
	/// ��λ����
	/// </summary>
    public class StationAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string StaGrade = "StaGrade";
    }
	/// <summary>
	/// ��λ
	/// </summary>
    public class Station : EntityNoName
    {
        #region ʵ�ֻ����ķ�����
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string Name
        {
            get
            {
                return this.GetValStrByKey("Name");
            }
        }
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
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
                map.CodeStruct = "2"; // ��󼶱���7.

                map.AddTBStringPK(SimpleNoNameAttr.No, null, null, true, false, 2, 2, 2);
                map.AddTBString(SimpleNoNameAttr.Name, null, null, true, false, 2, 50, 250);
                map.AddDDLSysEnum(StationAttr.StaGrade, 0,
                    "����", true, false,
                    StationAttr.StaGrade, "@1=�߲��@2=�в��@3=ִ�и�");

           //     map.AddDDLSysEnum("StaNWB", 0,"��λ��־", true, true);
              //  map.AddDDLSysEnum("StaNWB", 0, "��λ��־", true, true, "StaNWB", "@1=�ڲ���@2=�ⲿ��");


                //switch (BP.Sys.SystemConfig.SysNo)
                //{
                //    case BP.SysNoList.WF:
                //        map.AddDDLSysEnum(StationAttr.StaGrade, 0, "����", true, false, StationAttr.StaGrade, "@1=�ܲ�@2=����@3=����");
                //        break;
                //    default:
                //        break;
                //}

                // map.AddTBInt(DeptAttr.Grade, 0, "����", true, true);
                //map.AddBoolean(DeptAttr.IsDtl, true, "�Ƿ���ϸ", true, true);
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
				return new Station();
			}
		}
	}
}
