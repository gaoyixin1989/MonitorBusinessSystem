using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityMyPKAttr
	/// </summary>
    public class EntityMyPKAttr
    {
        /// <summary>
        /// className
        /// </summary>
        public const string MyPK = "MyPK";
        public const string NoInt = "NoInt";
    }
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
    abstract public class EntityMyPK : Entity
    {
        #region ��������
        public override string PK
        {
            get
            {
                return "MyPK";
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(EntityMyPKAttr.MyPK);
            }
            set
            {
                this.SetValByKey(EntityMyPKAttr.MyPK, value);
            }
        }
        #endregion

        #region ����
        public EntityMyPK()
        {
        }
        /// <summary>
        /// class Name 
        /// </summary>
        /// <param name="_MyPK">_MyPK</param>
        protected EntityMyPK(string _MyPK)
        {
            this.MyPK = _MyPK;
            this.Retrieve();
        }
        #endregion
    }
    /// <summary>
    /// EntityMyPKMyFile
    /// </summary>
    abstract public class EntityMyPKMyFile : EntityMyPK
    {
        #region ����
        public override string PK
        {
            get
            {
                return "MyPK";
            }
        }
        public string MyFilePath
        {
            get
            {
                return this.GetValStringByKey("MyFilePath");
            }
            set
            {
                this.SetValByKey("MyFilePath", value);
            }
        }
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey("MyFileExt");
            }
            set
            {
                this.SetValByKey("MyFileExt", value.Replace(".","") );
            }
        }
        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey("MyFileName");
            }
            set
            {
                this.SetValByKey("MyFileName", value);
            }
        }
        public Int64 MyFileSize
        {
            get
            {
                return this.GetValInt64ByKey("MyFileSize");
            }
            set
            {
                this.SetValByKey("MyFileSize", value);
            }
        }
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey("MyFileH");
            }
            set
            {
                this.SetValByKey("MyFileH", value);
            }
        }
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey("MyFileW");
            }
            set
            {
                this.SetValByKey("MyFileW", value);
            }
        }
        public bool IsImg
        {
            get
            {
                return DataType.IsImgExt(this.MyFileExt);
            }
        }
        #endregion

        #region ����
        public EntityMyPKMyFile()
		{
		}
		/// <summary>
		/// class Name 
		/// </summary>
		/// <param name="_MyPK">_MyPK</param>
        protected EntityMyPKMyFile(string _MyPK)
        {
            this.MyPK = _MyPK;
            this.Retrieve();
        }
		#endregion
    }
	/// <summary>
	/// ����ʵ�弯��
	/// </summary>
    abstract public class EntitiesMyPK : Entities
    {
        /// <summary>
        /// ʵ�弯��
        /// </summary>
        public EntitiesMyPK()
        {
        }
    }
}
