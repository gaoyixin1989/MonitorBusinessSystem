using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ͼƬӦ������
    /// </summary>
    public enum ImgAppType
    {
        /// <summary>
        /// ͼƬ
        /// </summary>
        Img,
        /// <summary>
        /// ����
        /// </summary>
        Seal
    }
    /// <summary>
    /// ͼƬ
    /// </summary>
    public class FrmImgAttr : EntityMyPKAttr
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
        /// W
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// H
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// URL
        /// </summary>
        public const string ImgURL = "ImgURL";
        /// <summary>
        /// �ļ�·��
        /// </summary>
        public const string ImgPath = "ImgPath";
        /// <summary>
        /// LinkURL
        /// </summary>
        public const string LinkURL = "LinkURL";
        /// <summary>
        /// LinkTarget
        /// </summary>
        public const string LinkTarget = "LinkTarget";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// Ӧ������
        /// </summary>
        public const string ImgAppType = "ImgAppType";
        /// <summary>
        /// ����
        /// </summary>
        public const string Tag0 = "Tag0";
        /// <summary>
        /// ������Դ���� 0=���� , 1=�ⲿ.
        /// </summary>
        public const string SrcType = "SrcType";
        /// <summary>
        /// �Ƿ���Ա༭
        /// </summary>
        public const string IsEdit = "IsEdit";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// Ӣ������
        /// </summary>
        public const string EnPK = "EnPK";
    }
    /// <summary>
    /// ͼƬ
    /// </summary>
    public class FrmImg : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.Name);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.Name, value);
            }
        }
        /// <summary>
        /// Ӣ������
        /// </summary>
        public string EnPK
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.EnPK);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.EnPK, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ա༭
        /// </summary>
        public int IsEdit
        {
            get
            {
                return this.GetValIntByKey(FrmImgAttr.IsEdit);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.IsEdit, (int)value);
            }
        }
        /// <summary>
        /// Ӧ������
        /// </summary>
        public ImgAppType HisImgAppType
        {
            get
            {
                return (ImgAppType)this.GetValIntByKey(FrmImgAttr.ImgAppType);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.ImgAppType, (int)value);
            }
        }
        /// <summary>
        /// ������Դ
        /// </summary>
        public int SrcType
        {
            get
            {
                return this.GetValIntByKey(FrmImgAttr.SrcType);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.SrcType, value);
            }
        }
        
        public string Tag0
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.Tag0);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.Tag0, value);
            }
        }
        public string LinkTarget
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.LinkTarget);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.LinkTarget, value);
            }
        }
        /// <summary>
        /// URL
        /// </summary>
        public string LinkURL
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.LinkURL);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.LinkURL, value);
            }
        }
        public string ImgPath
        {
            get
            {
                string src = this.GetValStringByKey(FrmImgAttr.ImgPath);
                if (string.IsNullOrEmpty(src))
                {
                    string appPath = BP.Sys.Glo.Request.ApplicationPath;
                    src = appPath + "DataUser/ICON/" + BP.Sys.SystemConfig.CompanyID + "/LogBiger.png";
                }
                return src;
            }
            set
            {
                this.SetValByKey(FrmImgAttr.ImgPath, value);
            }
        }
        public string ImgURL
        {
            get
            {
                string src = this.GetValStringByKey(FrmImgAttr.ImgURL);
                if (string.IsNullOrEmpty(src) || src.Contains("component/Img"))
                {
                    string appPath = BP.Sys.Glo.Request.ApplicationPath;
                    src = appPath + "DataUser/ICON/" + BP.Sys.SystemConfig.CompanyID + "/LogBiger.png";
                }
                return src;
            }
            set
            {
                this.SetValByKey(FrmImgAttr.ImgURL, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.X);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.X, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.H);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.H, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.W);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.W, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmImgAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.FK_MapData, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ͼƬ
        /// </summary>
        public FrmImg()
        {
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        /// <param name="mypk"></param>
        public FrmImg(string mypk)
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
                Map map = new Map("Sys_FrmImg");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ͼƬ";
                map.EnType = EnType.Sys;
                map.AddMyPK();
                map.AddTBString(FrmImgAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                map.AddTBInt(FrmImgAttr.ImgAppType, 0, "Ӧ������", false, false);
                
                map.AddTBFloat(FrmImgAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmImgAttr.H, 200, "H", true, false);
                map.AddTBFloat(FrmImgAttr.W, 160, "W", false, false);

                map.AddTBString(FrmImgAttr.ImgURL, null, "ImgURL", true, false, 0, 200, 20);
                map.AddTBString(FrmImgAttr.ImgPath, null, "ImgPath", true, false, 0, 200, 20);
                
                map.AddTBString(FrmImgAttr.LinkURL, null, "LinkURL", true, false, 0, 200, 20);
                map.AddTBString(FrmImgAttr.LinkTarget, "_blank", "LinkTarget", true, false, 0, 200, 20);

                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                // �����seal ���Ǹ�λ���ϡ�
                map.AddTBString(FrmImgAttr.Tag0, null, "����", true, false, 0, 500, 20);
                map.AddTBInt(FrmImgAttr.SrcType, 0, "ͼƬ��Դ0=����,1=URL", true, false);
                map.AddTBInt(FrmImgAttr.IsEdit, 0, "�Ƿ���Ա༭", true, false);
                map.AddTBString(FrmImgAttr.Name, null, "��������", true, false, 0, 500, 20);
                map.AddTBString(FrmImgAttr.EnPK, null, "Ӣ������", true, false, 0, 500, 20);
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
            string sql = "SELECT COUNT(*) FROM " + this.EnMap.PhysicsTable + " WHERE FK_MapData='" + this.FK_MapData + "' AND X=" + this.X + " AND Y=" + this.Y ;
            if (DBAccess.RunSQLReturnValInt(sql, 0) == 0)
                return false;
            return true;
        }

    }
    /// <summary>
    /// ͼƬs
    /// </summary>
    public class FrmImgs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ͼƬs
        /// </summary>
        public FrmImgs()
        {
        }
        /// <summary>
        /// ͼƬs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmImgs(string fk_mapdata)
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
                return new FrmImg();
            }
        }
        #endregion
    }
}
