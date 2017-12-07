using System;
using System.Collections;
using BP.En;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// ����
	/// </summary>
	public class EntityNoAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string No="No";
	}
    public class EntityNoMyFileAttr : EntityNoAttr
    {
        /// <summary>
        /// MyFileName
        /// </summary>
        public const string MyFileName = "MyFileName";
        /// <summary>
        /// MyFilePath
        /// </summary>
        public const string MyFilePath = "MyFilePath";
        /// <summary>
        /// MyFileExt
        /// </summary>
        public const string MyFileExt = "MyFileExt";
        public const string WebPath = "WebPath";
        public const string MyFileH = "MyFileH";
        public const string MyFileW = "MyFileW";
        public const string MyFileNum = "MyFileNum";
    }
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
	abstract public class EntityNo: Entity
	{
		#region �ṩ������
        public override string PK
        {
            get
            {
                return "No";
            }
        }
		/// <summary>
		/// ���
		/// </summary>
		public string No
		{
            get
            {
                return this.GetValStringByKey(EntityNoNameAttr.No);
            }
            set
            {
                this.SetValByKey(EntityNoNameAttr.No, value);
            }
		}
		#endregion

		#region �����йص��߼�����(�������ֻ��dict EntityNo, �����й�ϵ��)
		/// <summary>
		/// Insert ֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {

            Attr attr = this.EnMap.GetAttrByKey("No");
            if (attr.UIVisible == true && attr.UIIsReadonly && this.EnMap.IsAutoGenerNo && this.No.Length==0)
                this.No = this.GenerNewNo;

            return base.beforeInsert();
            ////if (this.EnMap.IsAutoGenerNo == true && (this.No == "" || this.No == null || this.No == "�Զ�����"))
            ////{
            ////    this.No = this.GenerNewNo;
            ////}
            //if (this.EnMap.IsAllowRepeatNo == false)
            //{
            //    string field = attr.Field;

            //    Paras ps = new Paras();
            //    ps.Add("no", No);
            //    string sql = "SELECT " + field + " FROM " + this.EnMap.PhysicsTable + " WHERE " + field + "=:no";
            //    if (DBAccess.IsExits(sql, ps))
            //        throw new Exception("@[" + this.EnMap.EnDesc + " , " + this.EnMap.PhysicsTable + "] ���[" + No + "]�ظ���");
            //}

            //// �ǲ��Ǽ���ŵĳ��ȡ�
            //if (this.EnMap.IsCheckNoLength)
            //{
            //    if (this.No.Length!=this.EnMap.CodeLength )
            //        throw new Exception("@ ["+this.EnMap.EnDesc+"]���["+this.No+"]���󣬳��Ȳ�����ϵͳҪ�󣬱�����["+this.EnMap.CodeLength.ToString()+"]λ���������г�����["+this.No.Length.ToString()+"]λ��");
            //}
            //return base.beforeInsert();
        }
		#endregion 
		 
		#region ���캭��
		/// <summary>
		/// ������һ��ʵ��
		/// </summary>
		public EntityNo()
		{
		}
		/// <summary>
		/// ͨ����ŵõ�ʵ�塣
		/// </summary>
		/// <param name="_no">���</param>
		public EntityNo(string _no)  
		{
			if (_no==null || _no=="")
				throw new Exception( this.EnDesc+"@�Ա�["+this.EnDesc+"]���в�ѯǰ����ָ����š�");

			this.No = _no ;
			if (this.Retrieve()==0) 
			{				
				throw new Exception("@û��"+this._enMap.PhysicsTable+", No = "+No+"�ļ�¼��");
			}
		}
        public override int Save()
        {
            /*���������š� */
            if (this.IsExits)
            {
                return this.Update();
            }
            else
            {
                if (this.EnMap.IsAutoGenerNo
                    && this.EnMap.GetAttrByKey("No").UIIsReadonly)
                    this.No = this.GenerNewNo;

                this.Insert();
                return 0;
            }

           // return base.Save();
        }
		#endregion		

		#region �ṩ�Ĳ�Ѱ����
		/// <summary>
		/// ����һ�����
		/// </summary>
		public string GenerNewNo
		{
            get
            {
                return this.GenerNewNoByKey("No");
            }
		}
		/// <summary>
		/// �� No ��ѯ��
		/// </summary>
		/// <returns></returns>
        public int RetrieveByNo()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EntityNoAttr.No, this.No);
            return qo.DoQuery();
        }
		/// <summary>
		/// �� No ��ѯ��
		/// </summary>
		/// <param name="_No">No</param>
		/// <returns></returns>
		public int RetrieveByNo(string _No) 
		{
			this.No = _No ;
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EntityNoAttr.No,this.No);
			return qo.DoQuery();
		}
		#endregion
	}
	/// <summary>
	/// ���ʵ�弯�ϡ�
	/// </summary>
	abstract public class EntitiesNo : Entities
	{
        public override int RetrieveAllFromDBSource()
        {
            QueryObject qo = new QueryObject(this);
            qo.addOrderBy("No");
            return qo.DoQuery();
        }
	}
}
