using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ��չ
    /// </summary>
    public class MapExtAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// ExtType
        /// </summary>
        public const string ExtType = "ExtType";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �߶�
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// ���
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// �Ƿ��������Ӧ��С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        /// <summary>
        /// ���õ�����
        /// </summary>
        public const string AttrOfOper = "AttrOfOper";
        /// <summary>
        /// ���������
        /// </summary>
        public const string AttrsOfActive = "AttrsOfActive";
        /// <summary>
        /// ִ�з�ʽ
        /// </summary>
        public const string DoWay = "DoWay";
        /// <summary>
        /// Tag
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// Tag1
        /// </summary>
        public const string Tag1 = "Tag1";
        /// <summary>
        /// Tag2
        /// </summary>
        public const string Tag2 = "Tag2";
        /// <summary>
        /// Tag3
        /// </summary>
        public const string Tag3 = "Tag3";
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string DBSrc = "DBSrc";
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// ����
        /// </summary>
        public const string AtPara = "AtPara";
        /// <summary>
        /// ��������ȼ�
        /// </summary>
        public const string PRI = "PRI";
    }
    /// <summary>
    /// ��չ
    /// </summary>
    public class MapExt : EntityMyPK
    {
        #region ���� at ����
        /// <summary>
        /// Pop����.
        /// </summary>
        public int PopValFormat
        {
            get
            {
                return this.GetParaInt("PopValFormat");
            }
            set
            {
                this.SetPara("PopValFormat", value);
            }
        }
        /// <summary>
        /// pop ѡ��ʽ
        /// 0,��ѡ,1=��ѡ.
        /// </summary>
        public int PopValSelectModel
        {
            get
            {
                return this.GetParaInt("PopValSelectModel");
            }
            set
            {
                this.SetPara("PopValSelectModel", value);
            }
        }

        /// <summary>
        /// ����ģʽ
        /// 0=url, 1=����.
        /// </summary>
        public int PopValWorkModel
        {
            get
            {
                return this.GetParaInt("PopValWorkModel");
            }
            set
            {
                this.SetPara("PopValWorkModel", value);
            }
        }


        /// <summary>
        /// pop ���ַ�ʽ
        /// 0,���,1=Ŀ¼.
        /// </summary>
        public int PopValShowModel
        {
            get
            {
                return this.GetParaInt("PopValShowModel");
            }
            set
            {
                this.SetPara("PopValShowModel", value);
            }
        }
        #endregion

        #region ����
        public string ExtDesc
        {
            get
            {
                string dec = "";
                switch (this.ExtType)
                {
                    case MapExtXmlList.ActiveDDL:
                        dec += "�ֶ�" + this.AttrOfOper;
                        break;
                    case MapExtXmlList.TBFullCtrl:
                        dec += this.AttrOfOper;
                        break;
                    case MapExtXmlList.DDLFullCtrl:
                        dec += "" + this.AttrOfOper;
                        break;
                    case MapExtXmlList.InputCheck:
                        dec += "�ֶΣ�" + this.AttrOfOper + " ������ݣ�" + this.Tag1;
                        break;
                    case MapExtXmlList.PopVal:
                        dec += "�ֶΣ�" + this.AttrOfOper + " Url��" + this.Tag;
                        break;
                    default:
                        break;
                }
                return dec;
            }
        }
        /// <summary>
        /// �Ƿ�����Ӧ��С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapExtAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapExtAttr.IsAutoSize, value);
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string DBSrc
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.DBSrc);
            }
            set
            {
                this.SetValByKey(MapExtAttr.DBSrc, value);
            }
        }
        public string AtPara
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.AtPara);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AtPara, value);
            }
        }
      
        public string ExtType
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.ExtType);
            }
            set
            {
                this.SetValByKey(MapExtAttr.ExtType, value);
            }
        }
        public int DoWay
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.DoWay);
            }
            set
            {
                this.SetValByKey(MapExtAttr.DoWay, value);
            }
        }
        /// <summary>
        /// ������attrs
        /// </summary>
        public string AttrOfOper
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.AttrOfOper);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrOfOper, value);
            }
        }
        /// <summary>
        /// �����attrs
        /// </summary>
        public string AttrsOfActive
        {
            get
            {
              //  return this.GetValStrByKey(MapExtAttr.AttrsOfActive).Replace("~", "'");
                return this.GetValStrByKey(MapExtAttr.AttrsOfActive);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrsOfActive, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapExtAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary>
        public string Doc
        {
            get
            {
                return  this.GetValStrByKey("Doc").Replace("~","'");
            }
            set
            {
                this.SetValByKey("Doc", value);
            }
        }
        public string TagOfSQL_autoFullTB
        {
            get
            {
                if (string.IsNullOrEmpty(this.Tag))
                    return this.DocOfSQLDeal;
                

                string sql = this.Tag;
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                sql = sql.Replace("@WebUser.FK_DeptName", BP.Web.WebUser.FK_DeptName);
                return sql;
            }
        }

        public string DocOfSQLDeal
        {
            get
            {
                string sql = this.Doc;
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                sql = sql.Replace("@WebUser.FK_DeptName", BP.Web.WebUser.FK_DeptName);
                return sql;
            }
        }
        public string Tag
        {
            get
            {
                string s= this.GetValStrByKey("Tag").Replace("~", "'");

                s = s.Replace("\\\\", "\\");
                s = s.Replace("\\\\", "\\");

                s = s.Replace(@"CCFlow\Data\", @"CCFlow\WF\Data\");

                return s;
            }
            set
            {
                this.SetValByKey("Tag", value);
            }
        }
        public string Tag1
        {
            get
            {
                return this.GetValStrByKey("Tag1").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag1", value);
            }
        }
        public string Tag2
        {
            get
            {
                return this.GetValStrByKey("Tag2").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag2", value);
            }
        }
        public string Tag3
        {
            get
            {
                return this.GetValStrByKey("Tag3").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag3", value);
            }
        }
        public int H
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.H);
            }
            set
            {
                this.SetValByKey(MapExtAttr.H, value);
            }
        }
        public int W
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.W);
            }
            set
            {
                this.SetValByKey(MapExtAttr.W, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��չ
        /// </summary>
        public MapExt()
        {
        }
        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="no"></param>
        public MapExt(string mypk)
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

                Map map = new Map("Sys_MapExt");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��չ";
                map.EnType = EnType.Sys;

                map.AddMyPK();

                map.AddTBString(MapExtAttr.FK_MapData, null, "����", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.ExtType, null, "����", true, false, 0, 30, 20);
                map.AddTBInt(MapExtAttr.DoWay, 0, "ִ�з�ʽ", true, false);

                map.AddTBString(MapExtAttr.AttrOfOper, null, "������Attr", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.AttrsOfActive, null, "������ֶ�", true, false, 0, 900, 20);
                map.AddTBStringDoc();
                map.AddTBString(MapExtAttr.Tag, null, "Tag", true, false, 0, 2000, 20);
                map.AddTBString(MapExtAttr.Tag1, null, "Tag1", true, false, 0, 2000, 20);
                map.AddTBString(MapExtAttr.Tag2, null, "Tag2", true, false, 0, 2000, 20);
                map.AddTBString(MapExtAttr.Tag3, null, "Tag3", true, false, 0, 2000, 20);

                map.AddTBString(MapExtAttr.AtPara, null, "����", true, false, 0, 2000, 20);
                map.AddTBString(MapExtAttr.DBSrc, null, "����Դ", true, false, 0, 20, 20);


                map.AddTBInt(MapExtAttr.H, 500, "�߶�", false, false);
                map.AddTBInt(MapExtAttr.W, 400, "���", false, false);

                // add by stone 2013-12-21 ��������ȼ�,����js�ļ���.
                map.AddTBInt(MapExtAttr.PRI, 0, "PRI", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

    }
    /// <summary>
    /// ��չs
    /// </summary>
    public class MapExts : Entities
    {
        #region ����
        /// <summary>
        /// ��չs
        /// </summary>
        public MapExts()
        {
        }
        /// <summary>
        /// ��չs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapExts(string fk_mapdata)
        {
            this.Retrieve(MapExtAttr.FK_MapData, fk_mapdata, MapExtAttr.PRI);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapExt();
            }
        }
        #endregion
    }
}
