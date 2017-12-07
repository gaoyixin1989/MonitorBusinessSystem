using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace BP.Sys
{
    public class SysFileManagerAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// EnName
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// ������key
        /// </summary>
        public const string RefVal = "RefVal";
        /// <summary>
        /// �ļ�·��
        /// </summary>
        public const string MyFilePath = "MyFilePath";
        /// <summary>
        /// �ļ���С
        /// </summary>
        public const string MyFileSize = "MyFileSize";
        /// <summary>
        /// MyFileExt
        /// </summary>
        public const string MyFileExt = "MyFileExt";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        public const string MyFileH = "MyFileH";
        public const string MyFileW = "MyFileW";
        public const string MyFileName = "MyFileName";
        public const string Doc = "Doc";
        public const string AttrFileName = "AttrFileName";
        public const string AttrFileNo = "AttrFileNo";
        public const string WebPath = "WebPath";
    }
    /// <summary>
    /// �ļ�������
    /// </summary>
    public class SysFileManager : EntityOID
    {
        #region ʵ�ֻ�������
        /// <summary>
        /// 
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.WebPath);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.WebPath, value);
            }
        }
        public string AttrFileNo
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.AttrFileNo);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.AttrFileNo, value);
            }
        }

        public string AttrFileName
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.AttrFileName);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.AttrFileName, value);
            }
        }

        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFileName, value);
            }
        }
        public string MyFileWebUrl
        {
            get
            {
                return this.WebPath;
            }
        }

        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFileExt, value);
            }
        }
 

        public string Rec
        {
            get
            {
                string s = this.GetValStringByKey(SysFileManagerAttr.Rec);
                if (s == null || s == "")
                    return null;
                return s;
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.Rec, value);
            }
        }

        public string RecText
        {
            get
            {
                return this.GetValRefTextByKey(SysFileManagerAttr.Rec);
            }
        }
        public string EnName
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.EnName);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.EnName, value);
            }
        }
        public object RefVal
        {
            get
            {
                return this.GetValByKey(SysFileManagerAttr.RefVal);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.RefVal, value);
            }
        }
        public string MyFilePath
        {
            get
            {
                return BP.Sys.SystemConfig.PathOfUsersFiles + this.GetValStringByKey(SysFileManagerAttr.MyFilePath);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFilePath, value);
            }
        }
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey(SysFileManagerAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFileH, value);
            }
        }
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey(SysFileManagerAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFileW, value);
            }
        }
        public float MyFileSize
        {
            get
            {
                return this.GetValIntByKey(SysFileManagerAttr.MyFileSize);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.MyFileSize, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.RDT);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.RDT, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(SysFileManagerAttr.Note);
            }
            set
            {
                this.SetValByKey(SysFileManagerAttr.Note, value);
            }
        }
        #endregion

        #region ���췽��
        public SysFileManager()
        {
        }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param MyFileName="_OID"></param>
        public SysFileManager(int _OID)
            : base(_OID)
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
                Map map = new Map("Sys_FileManager");
                map.EnDesc = "�ļ�������";
                map.AddTBIntPKOID();
                map.AddTBString(SysFileManagerAttr.AttrFileName, null, "ָ������", true, false, 0, 50, 20);
                map.AddTBString(SysFileManagerAttr.AttrFileNo, null, "ָ�����", true, false, 0, 50, 20);

                map.AddTBString(SysFileManagerAttr.EnName, null, "�����ı�", false, true, 1, 50, 20);
                map.AddTBString(SysFileManagerAttr.RefVal, null, "����ֵ", false, true, 1, 50, 10);
                map.AddTBString(SysFileManagerAttr.WebPath, null, "Web·��", false, true, 0, 100, 30);

                map.AddMyFile("�ļ�����");

                //map.AddTBString(SysFileManagerAttr.MyFileName, null, "�ļ�����", true, false, 1, 50, 20);
                //map.AddTBInt(SysFileManagerAttr.MyFileSize, 0, "�ļ���С", true, true);
                //map.AddTBInt(SysFileManagerAttr.MyFileH, 0, "Img�߶�", true, true);
                //map.AddTBInt(SysFileManagerAttr.MyFileW, 0, "Img���", true, true);
                //map.AddTBString(SysFileManagerAttr.MyFileExt, null, "�ļ�����", true, true, 0, 50, 20);

                map.AddTBString(SysFileManagerAttr.RDT, null, "�ϴ�ʱ��", true, true, 1, 50, 20);
                map.AddTBString(SysFileManagerAttr.Rec, null, "�ϴ���", true, true, 0, 50, 20);
                map.AddTBStringDoc();
                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeInsert()
        {
           this.Rec = BP.Web.WebUser.No;
            return base.beforeInsert();
        }
        protected override bool beforeDelete()
        {
            if (this.Rec == Web.WebUser.No)
            {
                return base.beforeDelete();
            }
            return base.beforeDelete();
        }
        #endregion

        #region�����÷���
        public void UpdateLoadFileOfAccess(string MyFilePath)
        {
            //FileInfo fi = new FileInfo(MyFilePath);// Replace with your file MyFileName
            //if (fi.Exists == false)
            //    throw new Exception("�ļ��Ѿ������ڡ�");

            //this.MyFileSize =int.Parse( fi.Length.ToString());
            //this.MyFilePath = fi.FullMyFileName;
            //this.MyFileName = fi.MyFileName;
            //this.Insert();

            //byte[] bData = null;
            ////int nNewFileID = 0;
            //// Read file data into buffer
            //using (FileStream fs = fi.OpenRead())
            //{
            //    bData = new byte[fi.Length];
            //    int nReadLength = fs.Read(bData, 0, (int)(fi.Length));
            //}

            ////			// Add file info into DB
            ////			string strQuery = "INSERT INTO FileInfo " 
            ////				+ " ( FileMyFileName, FullMyFileName, FileData ) "
            ////				+ " VALUES "
            ////				+ " ( @FileMyFileName, @FullMyFileName, @FileData ) "
            ////				+ " SELECT @@IDENTITY AS 'Identity'";

            //string strQuery = "UPDATE Sys_FileManager SET FileData=@FileData WHERE OID=" + this.OID;
            //OleDbConnection conn = (OleDbConnection)BP.DA.DBAccess.GetAppCenterDBConn;
            //conn.Open();

            //OleDbCommand sqlComm = new OleDbCommand(strQuery,
            //    conn);

            ////sqlComm.Parameters.Add( "@FileMyFileName", fi.MyFileName );
            ////sqlComm.Parameters.Add( "@FullMyFileName", fi.FullMyFileName );
            //sqlComm.Parameters.AddWithValue("@FileData", bData);
            //sqlComm.ExecuteNonQuery();

            //// Get new file ID
            ////	SqlDataReader sqlReader = sqlComm.ExecuteReader(); 
            ////			if( sqlReader.Read() )
            ////			{
            ////				nNewFileID = int.Parse(sqlReader.GetValue(0).ToString());
            ////			}
            ////
            ////			sqlReader.Close();
            ////			sqlComm.Dispose();
            ////
            ////			if( nNewFileID > 0 )
            ////			{
            ////				// Add new item in list view
            ////				//ListViewItem itmNew = lsvFileInfo.Items.Add( fi.MyFileName );
            ////				//itmNew.Tag = nNewFileID;
            ////			}
        }
        #endregion
    }
	/// <summary>
	/// �ļ������� 
	/// </summary>
    public class SysFileManagers : EntitiesOID
	{
        /// <summary>
        /// �ļ�������
        /// </summary>
		public SysFileManagers()
        {
        }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="EnName"></param>
        /// <param name="refval"></param>
        public SysFileManagers(string EnName, string refval)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(SysFileManagerAttr.EnName, EnName);
            qo.addAnd();
            qo.AddWhere(SysFileManagerAttr.RefVal, refval);
            qo.DoQuery();
        }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="EnName"></param>
        public SysFileManagers(string EnName)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(SysFileManagerAttr.EnName, EnName);
            qo.DoQuery();
        }
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysFileManager();
			}
		}
        public SysFileManager GetSysFileByAttrFileNo(string key)
        {
            foreach (SysFileManager en in this)
            {
                if (en.AttrFileNo == key)
                    return en;
            }
            return null;
        }
	}
}
