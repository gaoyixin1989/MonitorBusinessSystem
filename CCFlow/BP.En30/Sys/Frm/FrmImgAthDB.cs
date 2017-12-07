using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ����ͼƬ�������ݴ洢 - ����
    /// </summary>
    public class FrmImgAthDBAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_FrmImgAth = "FK_FrmImgAth";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// RefPKVal
        /// </summary>
        public const string RefPKVal = "RefPKVal";
        /// <summary>
        /// �ļ�����
        /// </summary>
        public const string FileName = "FileName";
        /// <summary>
        /// �ļ���չ
        /// </summary>
        public const string FileExts = "FileExts";
        /// <summary>
        /// �ļ���С
        /// </summary>
        public const string FileSize = "FileSize";
        /// <summary>
        /// ���浽
        /// </summary>
        public const string FileFullName = "FileFullName";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ��¼������
        /// </summary>
        public const string RecName = "RecName";
        /// <summary>
        /// ���
        /// </summary>
        public const string Sort = "Sort";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string MyNote = "MyNote";
    }
    /// <summary>
    /// ����ͼƬ�������ݴ洢
    /// </summary>
    public class FrmImgAthDB : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ���
        /// </summary>
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.Sort);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.Sort, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.RDT);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.RDT, value);
            }
        }
        /// <summary>
        /// �ļ�
        /// </summary>
        public string FileFullName
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.FileFullName);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FileFullName, value);
            }
        }
        /// <summary>
        /// ����·��
        /// </summary>
        public string FilePathName
        {
            get
            {
                return this.FileFullName.Substring(this.FileFullName.LastIndexOf('\\') + 1);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.FileName);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FileName, value);
            }
        }
        /// <summary>
        /// ������չ��
        /// </summary>
        public string FileExts
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.FileExts);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FileExts, value.Replace(".",""));
            }
        }
        /// <summary>
        /// ��ظ���
        /// </summary>
        public string FK_FrmImgAth
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.FK_FrmImgAth);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FK_FrmImgAth, value);
            }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string RefPKVal
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.RefPKVal);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.RefPKVal, value);
            }
        }
        /// <summary>
        /// MyNote
        /// </summary>
        public string MyNote
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.MyNote);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.MyNote, value);
            }
        }
        /// <summary>
        /// ��¼��
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.Rec);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.Rec, value);
            }
        }
        /// <summary>
        /// ��¼������
        /// </summary>
        public string RecName
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.RecName);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.RecName, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmImgAthDBAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �ļ���С
        /// </summary>
        public float FileSize
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAthDBAttr.FileSize);
            }
            set
            {
                this.SetValByKey(FrmImgAthDBAttr.FileSize, value/1024);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����ͼƬ�������ݴ洢
        /// </summary>
        public FrmImgAthDB()
        {
        }
        /// <summary>
        /// ����ͼƬ�������ݴ洢
        /// </summary>
        /// <param name="mypk"></param>
        public FrmImgAthDB(string mypk)
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

                Map map = new Map("Sys_FrmImgAthDB");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����ͼƬ�������ݴ洢";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                // ���������ֶ����һ������. FK_FrmImgAth+"_"+RefPKVal
                map.AddTBString(FrmImgAthDBAttr.FK_MapData, null, "����ID", true, false, 1, 30, 20);
                map.AddTBString(FrmImgAthDBAttr.FK_FrmImgAth, null, "ͼƬ�������", true, false, 1, 50, 20);
                map.AddTBString(FrmImgAthDBAttr.RefPKVal, null, "ʵ������", true, false, 1, 50, 20);

                map.AddTBString(FrmImgAthDBAttr.FileFullName, null, "�ļ�ȫ·��", true, false, 0, 700, 20);
                map.AddTBString(FrmImgAthDBAttr.FileName, null, "����", true, false, 0, 500, 20);
                map.AddTBString(FrmImgAthDBAttr.FileExts, null, "��չ��", true, false, 0, 50, 20);
                map.AddTBFloat(FrmImgAthDBAttr.FileSize, 0, "�ļ���С", true, false);

                map.AddTBDateTime(FrmImgAthDBAttr.RDT, null, "��¼����", true, false);
                map.AddTBString(FrmImgAthDBAttr.Rec, null, "��¼��", true, false, 0, 50, 20);
                map.AddTBString(FrmImgAthDBAttr.RecName, null, "��¼������", true, false, 0, 50, 20);
                map.AddTBStringDoc(FrmImgAthDBAttr.MyNote, null, "��ע", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ��д
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            this.MyPK = this.FK_FrmImgAth + "_" + this.RefPKVal;
            return base.beforeInsert();
        }
        /// <summary>
        /// ��д
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdate()
        {
            this.MyPK = this.FK_FrmImgAth + "_" + this.RefPKVal;
            return base.beforeUpdate();
        }
        #endregion
    }
    /// <summary>
    /// ����ͼƬ�������ݴ洢s
    /// </summary>
    public class FrmImgAthDBs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ����ͼƬ�������ݴ洢s
        /// </summary>
        public FrmImgAthDBs()
        {
        }
        /// <summary>
        /// ����ͼƬ�������ݴ洢s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmImgAthDBs(string fk_mapdata,string pkval)
        {
            this.Retrieve(FrmImgAthDBAttr.FK_MapData, fk_mapdata, 
                FrmImgAthDBAttr.RefPKVal, pkval);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmImgAthDB();
            }
        }
        #endregion
    }
}
