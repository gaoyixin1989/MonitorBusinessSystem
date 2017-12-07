using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ����
	/// </summary>
    public class ContrastAttr
    {
        /// <summary>
        /// �Ա���Ŀ
        /// </summary>
        public const string ContrastKey = "ContrastKey";
        public const string KeyVal1 = "KeyVal1";
        public const string KeyVal2 = "KeyVal2";
        /// <summary>
        /// ��������
        /// </summary>
        public const string SortBy = "SortBy";
        /// <summary>
        /// �Աȵ�ֵ
        /// </summary>
        public const string KeyOfNum = "KeyOfNum";
        public const string GroupWay = "GroupWay";
        public const string OrderWay = "OrderWay";
    }
	/// <summary>
	/// �Ա�״̬�洢
	/// </summary>
    public class Contrast : EntityMyPK
    {
        #region ��������
       
        /// <summary>
        /// ����
        /// </summary>
        public string ContrastKey
        {
            get
            {
                string s= this.GetValStringByKey(ContrastAttr.ContrastKey);
                if (s == null || s == "")
                    s = "FK_NY";

                return s;
            }
            set
            {
                this.SetValByKey(ContrastAttr.ContrastKey, value);
            }
        }

        public string KeyVal1
        {
            get
            {
                return this.GetValStringByKey(ContrastAttr.KeyVal1);
            }
            set
            {
                this.SetValByKey(ContrastAttr.KeyVal1, value);
            }
        }
        public string KeyVal2
        {
            get
            {
                return this.GetValStringByKey(ContrastAttr.KeyVal2);
            }
            set
            {
                this.SetValByKey(ContrastAttr.KeyVal2, value);
            }
        }

        public string SortBy
        {
            get
            {
                return this.GetValStringByKey(ContrastAttr.SortBy);
            }
            set
            {
                this.SetValByKey(ContrastAttr.SortBy, value);
            }
        }

        public string KeyOfNum
        {
            get
            {
                return this.GetValStringByKey(ContrastAttr.KeyOfNum);
            }
            set
            {
                this.SetValByKey(ContrastAttr.KeyOfNum, value);
            }
        }

        public int GroupWay
        {
            get
            {
                return this.GetValIntByKey(ContrastAttr.GroupWay);
            }
            set
            {
                this.SetValByKey(ContrastAttr.GroupWay, value);
            }
        }
        public int OrderWay
        {
            get
            {
                return this.GetValIntByKey(ContrastAttr.OrderWay);
            }
            set
            {
                this.SetValByKey(ContrastAttr.OrderWay, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �Ա�״̬�洢
        /// </summary>
        public Contrast()
        {
        }
       
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) 
                    return this._enMap;

                Map map = new Map("Sys_Contrast");
                map.EnType = EnType.Sys;
                map.EnDesc = "�Ա�״̬�洢";
                map.DepositaryOfEntity = Depositary.None;
                map.AddMyPK();
                map.AddTBString(ContrastAttr.ContrastKey, null, "�Ա���Ŀ", false, true, 0, 20, 10);
                map.AddTBString(ContrastAttr.KeyVal1, null, "KeyVal1", false, true, 0, 20, 10);
                map.AddTBString(ContrastAttr.KeyVal2, null, "KeyVal2", false, true, 0, 20, 10);

                map.AddTBString(ContrastAttr.SortBy, null, "SortBy", false, true, 0, 20, 10);
                map.AddTBString(ContrastAttr.KeyOfNum, null, "KeyOfNum", false, true, 0, 20, 10);

                map.AddTBInt(ContrastAttr.GroupWay, 1, "��ʲô?SumAvg", false, true);
                map.AddTBInt(ContrastAttr.OrderWay, 1, "OrderWay", false, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        public override Entities GetNewEntities
        {
            get {  return new Contrasts(); }
        }
        #endregion
    }
	/// <summary>
	/// �Ա�״̬�洢s
	/// </summary>
    public class Contrasts : Entities
    {
        /// <summary>
        /// �Ա�״̬�洢s
        /// </summary>
        public Contrasts()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Contrast();
            }
        }
    }
}
