using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// �������ݴ洢 - ����
    /// </summary>
    public class FrmAttachmentDBAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_FrmAttachment = "FK_FrmAttachment";
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
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public const string IsRowLock = "IsRowLock";
        /// <summary>
        /// �ϴ���GUID
        /// </summary>
        public const string UploadGUID = "UploadGUID";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";

    }
    /// <summary>
    /// �������ݴ洢
    /// </summary>
    public class FrmAttachmentDB : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ���
        /// </summary>
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.Sort);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.Sort, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                string str = this.GetValStringByKey(FrmAttachmentDBAttr.RDT);
                return str.Substring(5,11);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.RDT, value);
            }
        }
        /// <summary>
        /// �ļ�
        /// </summary>
        public string FileFullName
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileFullName);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileFullName, value);
            }
        }
        /// <summary>
        /// �ϴ�GUID
        /// </summary>
        public string UploadGUID
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.UploadGUID);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.UploadGUID, value);
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
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileName);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileName, value);
            }
        }
        /// <summary>
        /// ������չ��
        /// </summary>
        public string FileExts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileExts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileExts, value.Replace(".",""));
            }
        }
        /// <summary>
        /// ��ظ���
        /// </summary>
        public string FK_FrmAttachment
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_FrmAttachment);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_FrmAttachment, value);
            }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string RefPKVal
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.RefPKVal);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.RefPKVal, value);
            }
        }
        /// <summary>
        /// MyNote
        /// </summary>
        public string MyNote
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.MyNote);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.MyNote, value);
            }
        }
        /// <summary>
        /// ��¼��
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.Rec);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.Rec, value);
            }
        }
        /// <summary>
        /// ��¼������
        /// </summary>
        public string RecName
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.RecName);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.RecName, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �ļ���С
        /// </summary>
        public float FileSize
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentDBAttr.FileSize);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileSize, value/1024);
            }
        }
        /// <summary>
        /// �Ƿ�������?
        /// </summary>
        public bool IsRowLock
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentDBAttr.IsRowLock);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.IsRowLock, value);
            }
        }
        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentDBAttr.Idx);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.Idx, value);
            }
        }
        /// <summary>
        /// ������չ��
        /// </summary>
        public string NodeID
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.NodeID);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.NodeID, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public AttachmentUploadType HisAttachmentUploadType
        {
            get
            {

                if (this.MyPK.Contains("_") && this.MyPK.Length < 32)
                    return AttachmentUploadType.Single;
                else
                    return AttachmentUploadType.Multi;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �������ݴ洢
        /// </summary>
        public FrmAttachmentDB()
        {
        }
        /// <summary>
        /// �������ݴ洢
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachmentDB(string mypk)
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
                Map map = new Map("Sys_FrmAttachmentDB");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�������ݴ洢";
                map.EnType = EnType.Sys;
                map.AddMyPK();
                map.AddTBString(FrmAttachmentDBAttr.FK_MapData, null,"FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmAttachmentDBAttr.FK_FrmAttachment, null, "�������", true, false, 1, 500, 20);
                map.AddTBString(FrmAttachmentDBAttr.RefPKVal, null, "ʵ������", true, false, 0, 50, 20);

                map.AddTBString(FrmAttachmentDBAttr.Sort, null, "���", true, false, 0, 200, 20);
                map.AddTBString(FrmAttachmentDBAttr.FileFullName, null, "�ļ�·��", true, false, 0, 700, 20);
                map.AddTBString(FrmAttachmentDBAttr.FileName, null,"����", true, false, 0, 500, 20);
                map.AddTBString(FrmAttachmentDBAttr.FileExts, null, "��չ", true, false, 0, 50, 20);
                map.AddTBFloat(FrmAttachmentDBAttr.FileSize, 0, "�ļ���С", true, false);

                map.AddTBDateTime(FrmAttachmentDBAttr.RDT, null, "��¼����", true, false);
                map.AddTBString(FrmAttachmentDBAttr.Rec, null, "��¼��", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentDBAttr.RecName, null, "��¼������", true, false, 0, 50, 20);
                map.AddTBStringDoc(FrmAttachmentDBAttr.MyNote, null, "��ע", true, false);
                map.AddTBString(FrmAttachmentDBAttr.NodeID, null, "�ڵ�ID", true, false, 0, 50, 20);

                map.AddTBInt(FrmAttachmentDBAttr.IsRowLock, 0, "�Ƿ�������", true, false);

                //˳��.
                map.AddTBInt(FrmAttachmentDBAttr.Idx, 0, "����", true, false);


                //���ֵ���ϴ�ʱ�����.
                map.AddTBString(FrmAttachmentDBAttr.UploadGUID, null, "�ϴ�GUID", 
                    true, false, 0, 50, 20);

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
            return base.beforeInsert();
        }
        #endregion
    }
    /// <summary>
    /// �������ݴ洢s
    /// </summary>
    public class FrmAttachmentDBs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// �������ݴ洢s
        /// </summary>
        public FrmAttachmentDBs()
        {
        }
        /// <summary>
        /// �������ݴ洢s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachmentDBs(string fk_mapdata,string pkval)
        {
            this.Retrieve(FrmAttachmentDBAttr.FK_MapData, fk_mapdata, 
                FrmAttachmentDBAttr.RefPKVal, pkval);
        }
        public FrmAttachmentDBs(string fk_mapdata, Int64 pkval)
        {
            this.Retrieve(FrmAttachmentDBAttr.FK_MapData, fk_mapdata,
                FrmAttachmentDBAttr.RefPKVal, pkval);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachmentDB();
            }
        }
        #endregion
    }
}
