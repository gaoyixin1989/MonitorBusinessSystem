using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��ǩ
    /// </summary>
    public class FrmLabAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Text
        /// </summary>
        public const string Text = "Text";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// ���
        /// </summary>
        public const string FontSize = "FontSize";
        /// <summary>
        /// ��ɫ
        /// </summary>
        public const string FontColor = "FontColor";
        /// <summary>
        /// ���
        /// </summary>
        public const string FontName = "FontName";
        /// <summary>
        /// ������
        /// </summary>
        public const string FontStyle = "FontStyle";
        /// <summary>
        /// ����
        /// </summary>
        public const string FontWeight = "FontWeight";
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public const string IsBold = "IsBold";
        /// <summary>
        /// б��
        /// </summary>
        public const string IsItalic = "IsItalic";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
    }
    /// <summary>
    /// ��ǩ
    /// </summary>
    public class FrmLab : EntityMyPK
    {
        #region ����
        /// <summary>
        /// FontStyle
        /// </summary>
        public string FontStyle
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontStyle);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontStyle, value);
            }
        }
        public string FontColorHtml
        {
            get
            {
                return PubClass.ToHtmlColor(this.FontColor);
            }
        }
        /// <summary>
        /// FontColor
        /// </summary>
        public string FontColor
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontColor);
            }
            set
            {
                switch (value)
                {
                    case "#FF000000":
                        this.SetValByKey(FrmLabAttr.FontColor, "Red");
                        return;
                    default:
                        break;
                }
                this.SetValByKey(FrmLabAttr.FontColor, value);
            }
        }
        public string FontWeight
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontWeight);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontWeight, value);
            }
        }
        public bool IsBold
        {
            get
            {
                return this.GetValBooleanByKey(FrmLabAttr.IsBold);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.IsBold, value);
            }
        }
        public bool IsItalic
        {
            get
            {
                return this.GetValBooleanByKey(FrmLabAttr.IsItalic);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.IsItalic, value);
            }
        }
        /// <summary>
        /// FontName
        /// </summary>
        public string FontName
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontName);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontName, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmLabAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmLabAttr.X);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.X, value);
            }
        }
        /// <summary>
        /// FontSize
        /// </summary>
        public int FontSize
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.FontSize);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontSize, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmLabAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return this.GetValStrByKey(FrmLabAttr.Text);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Text, value);
            }
        }
        public string TextHtml
        {
            get
            {
                if (this.IsBold)
                    return "<b>" + this.GetValStrByKey(FrmLabAttr.Text).Replace("@","<br>") + "</b>";
                else
                    return this.GetValStrByKey(FrmLabAttr.Text).Replace("@", "<br>");
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ǩ
        /// </summary>
        public FrmLab()
        {
        }
        /// <summary>
        /// ��ǩ
        /// </summary>
        /// <param name="mypk"></param>
        public FrmLab(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_FrmLab");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��ǩ";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(FrmLabAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmLabAttr.Text, "New Label", "Label", true, false, 0, 3900, 20);

                map.AddTBFloat(FrmLabAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmLabAttr.Y, 5, "Y", false, false);

                map.AddTBInt(FrmLabAttr.FontSize, 12, "�����С", false, false);
                map.AddTBString(FrmLabAttr.FontColor, "black", "��ɫ", true, false, 0, 50, 20);
                map.AddTBString(FrmLabAttr.FontName, null, "��������", true, false, 0, 50, 20);
                map.AddTBString(FrmLabAttr.FontStyle, "normal", "������", true, false, 0, 50, 20);
                map.AddTBString(FrmLabAttr.FontWeight, "normal", "������", true, false, 0, 50, 20);

                map.AddTBInt(FrmLabAttr.IsBold, 0, "�Ƿ����", false, false);
                map.AddTBInt(FrmLabAttr.IsItalic, 0, "�Ƿ�б��", false, false);
                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        /// <summary>
        /// �Ƿ������ͬ������?
        /// </summary>
        /// <returns></returns>
        public bool IsExitGenerPK()
        {
            string sql = "SELECT COUNT(*) FROM " + this.EnMap.PhysicsTable + " WHERE FK_MapData='" + this.FK_MapData + "' AND X=" + this.X + " AND Y=" + this.Y + "  and Text='" + this.Text+"'";
            if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                return false;
            return true;
        }

    }
    /// <summary>
    /// ��ǩs
    /// </summary>
    public class FrmLabs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ��ǩs
        /// </summary>
        public FrmLabs()
        {
        }
        /// <summary>
        /// ��ǩs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmLabs(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmLab();
            }
        }
        #endregion
    }
}
