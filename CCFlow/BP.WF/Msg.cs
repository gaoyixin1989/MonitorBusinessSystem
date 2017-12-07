using System;
using System.Collections;
using System.Web.Hosting;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// MsgsManager
	/// </summary>
	public class MsgsManager
	{
		/// <summary> 
		/// ɾ������by����ID
		/// </summary>
		/// <param name="workId"></param>
        public static void DeleteByWorkID(Int64 workId)
        {
            System.Web.HttpContext.Current.Application.Lock();
            Msgs msgs = (Msgs)System.Web.HttpContext.Current.Application["WFMsgs"];
            if (msgs == null)
            {
                msgs = new Msgs();
                System.Web.HttpContext.Current.Application["WFMsgs"] = msgs;
            }
            // ���ȫ���Ĺ���ID=workid ����Ϣ��
            msgs.ClearByWorkID(workId);
            System.Web.HttpContext.Current.Application.UnLock();
        }
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="wls">�����߼���</param>
		/// <param name="flowName">��������</param>
		/// <param name="nodeName">�ڵ�����</param>
		/// <param name="title">����</param>
        public static void AddMsgs(GenerWorkerLists wls, string flowName, string nodeName, string title)
		{
			return;

			System.Web.HttpContext.Current.Application.Lock();
			Msgs msgs= (Msgs)System.Web.HttpContext.Current.Application["WFMsgs"];
			if (msgs==null)
			{
				msgs= new Msgs();
				System.Web.HttpContext.Current.Application["WFMsgs"]=msgs;
			}
			// ���ȫ���Ĺ���ID=workid ����Ϣ��
			msgs.ClearByWorkID(wls[0].GetValIntByKey("WorkID"));
            foreach (GenerWorkerList wl in wls)
			{
				if (wl.FK_Emp==WebUser.No)
					continue;
				//msgs.AddMsg(wl.WorkID,wl.FK_Node,wl.FK_Emp,"��������["+flowName+"]�ڵ�["+nodeName+"]�����ڵ����Ϊ["+title+"]����Ϣ��");
			}
			System.Web.HttpContext.Current.Application.UnLock();
		}
		/// <summary>
		/// sss
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public static Msgs GetMsgsByEmpID_del(int empId)
		{
			Msgs msgs= (Msgs)System.Web.HttpContext.Current.Application["WFMsgs"];
			if (msgs==null)
			{
				msgs= new Msgs();
				System.Web.HttpContext.Current.Application["WFMsgs"]=msgs;
			}
			return msgs.GetMsgsByEmpID_del(empId); 
		}
		/// <summary>
		/// ȡ����Ϣ�ĸ���
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public static int GetMsgsCountByEmpID(int empId)
		{
			string sql="select COUNT(*) from v_wf_msg WHERE FK_Emp='"+WebUser.No+"'";
			return DA.DBAccess.RunSQLReturnValInt(sql);
		}
		/// <summary>
		/// �����Ϣ
		/// </summary>
		/// <param name="empId"></param>
		public static void ClearMsgsByEmpID_(int empId)
		{
			System.Web.HttpContext.Current.Application.Lock();
			Msgs msgs= (Msgs)System.Web.HttpContext.Current.Application["WFMsgs"];
			msgs.ClearByEmpId_del(empId); 
			System.Web.HttpContext.Current.Application.UnLock();
		}
		/// <summary>
		/// ��ʼ��ȫ������Ϣ��
		/// </summary>
		public static void InitMsgs()
		{
		}	 
	}
	/// <summary>
	/// Msg ��ժҪ˵����
	/// </summary>
	public class Msg
	{
		#region ����
		/// <summary>
		/// �����ļ�
		/// </summary>
		private string  _SoundUrl="/WF/Sound/ring.wav";
		/// <summary>
		/// �����ļ�
		/// </summary>
		public string SoundUrl
		{
			get
			{
				return  _SoundUrl;
			}
			set
			{
				_SoundUrl=value;
			}
		}
		/// <summary>
		/// _IsOpenSound
		/// </summary>
		private bool  _IsOpenSound=true;
		/// <summary>
		/// IsOpenSound
		/// </summary>
		public bool IsOpenSound
		{
			get
			{
				if (this._IsOpenSound==false)
				{
					return false;
				}
				else
				{
					this._IsOpenSound=false;
					return true;
				}
			}
		}
		/// <summary>
		/// _WorkID
		/// </summary>
		private int _WorkID=0;
		/// <summary>
		/// _NodeId
		/// </summary>
		private int _NodeId=0;
		/// <summary>
		/// _Info
		/// </summary>
		private string  _Info="";
		/// <summary>
		/// _ToEmpId
		/// </summary>
		private int _ToEmpId=0;
		/// <summary>
		/// ��Ϣ
		/// </summary>
		public string Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				_Info=value;
			}
		}
		/// <summary>
		/// ����ID
		/// </summary>
		public int WorkID
		{
			get
			{
				return _WorkID;
			}
			set
			{
				_WorkID=value;
			}
		}
		/// <summary>
		/// NodeID
		/// </summary>
		public int NodeId
		{
			get
			{
				return _NodeId;
			}
			set
			{
				_NodeId=value;
			}
		}
		/// <summary>
		/// ToEmpId
		/// </summary>
		public int ToEmpId
		{
			get
			{
				return _ToEmpId;
			}
			set
			{
				_ToEmpId=value;
			}
		}		
		#endregion

		/// <summary>
		/// ��Ϣ
		/// </summary>
		public Msg(){}

     

		/// <summary>
		/// ��Ϣ
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="nodeId"></param>
		/// <param name="toEmpId"></param>
		/// <param name="info"></param>
		public Msg(int workId , int nodeId, int toEmpId,string info)
		{
			this.WorkID=workId;
			this.NodeId=nodeId;
			this.ToEmpId=toEmpId;
			this.Info=info;
		}
	}
	/// <summary>
	/// ��Ϣ����
	/// </summary>
	public class Msgs:ArrayList
	{

		#region ������Ϣ
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="nodeId"></param>
		/// <param name="toEmpId"></param>
		/// <param name="info"></param>
		public void AddMsg(int workId , int nodeId, int toEmpId,string info)
		{
			return ;
			Msg msg = new Msg();
			msg.WorkID=workId;
			msg.NodeId=nodeId;
			msg.ToEmpId=toEmpId;
			msg.Info=info;
			this.Add(msg);
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="msg">��Ϣ</param>
		public void AddMsg(Msg msg)
		{
			return ;
			this.Add(msg);
		}
		#endregion 

		#region ������Ϣ���ϵĲ���
		/// <summary>
		/// ������ID �����Ϣ��
		/// </summary>
		/// <param name="workId"></param>
        public void ClearByWorkID(Int64 workId)
		{
			return ;
			Msgs ens = this.GetMsgsByWorkID(workId);
			foreach(Msg msg in ens)
			{			 
				this.Remove(msg);
			} 
		}
		/// <summary>
		/// ���������Ա��Ϣ
		/// </summary>
		/// <param name="empId"></param>
		public void ClearByEmpId_del(int empId)
		{
			return ;
			Msgs ens = this.GetMsgsByEmpID_del(empId);
			foreach(Msg msg in ens)
			{
				this.Remove(msg);
			}
		}
		/// <summary>
		/// ���������Ա��Ϣ
		/// </summary>
		/// <param name="workId"></param>
		/// <returns></returns>
        public Msgs GetMsgsByWorkID(Int64 workId)
		{
			return null ;
			Msgs ens = new Msgs();
			foreach(Msg msg in this)
			{
				if (msg.WorkID==workId)
					ens.AddMsg(msg);
			}
			return ens;
		}
		/// <summary>
		/// sss
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public Msgs GetMsgsByEmpID_del(int empId)
		{
			//return ;
			Msgs ens = new Msgs();
			foreach(Msg msg in this)
			{
				if (msg.ToEmpId==empId)
					ens.AddMsg(msg);
			}
			return ens;
		}
		/// <summary>
		/// ȡ����Ϣ��������
		/// </summary>
		/// <param name="empId">������Ա</param>
		/// <returns>��Ϣ����</returns>
		public int GetMsgsCountByEmpID(int empId)
		{
			return 0;
			int i = 0 ;
			//bool isHaveNew=false;
			int newMsgNum=0; 
			foreach(Msg msg in this)
			{
				if (msg.ToEmpId==empId)
				{
					if (msg.IsOpenSound)
						newMsgNum++;
					i++;
				}
			}
			if (newMsgNum>0)
			{
                //if (WebUser.IsSoundAlert)				 
                //    System.Web.HttpContext.Current.Response.Write("<bgsound src='"+BP.Sys.Glo.Request.ApplicationPath+Web.WebUser.NoticeSound+"' loop=1 >"  );
                //if (WebUser.IsTextAlert)
                //    BP.Sys.PubClass.ResponseWriteBlueMsg("����["+newMsgNum+"]���¹���." );
				//System.Web.HttpContext.Current.Response.Write("<bgsound src='"+BP.Sys.Glo.Request.ApplicationPath+Web.WebUser.NoticeSound+"' loop=1 >"  );

			}
			return i;
		}
		#endregion

		/// <summary>
		/// ��Ϣs
		/// </summary>
		public Msgs()
		{
		}
        
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public new Msg this[int index]
		{
			get 
			{
				return (Msg)this[index];
			}
		}	 

	}
	/// <summary>
	/// �û���Ϣ
	/// </summary>
	public class UserMsgs
	{
		#region ����
		/// <summary>
		/// _IsOpenSound
		/// </summary>
		private bool  _IsOpenSound=false;
		/// <summary>
		/// _IsOpenSound
		/// </summary>
		public bool IsOpenSound
		{
			get
			{
				if (this._IsOpenSound==false)
				{
					return false;
				}
				else
				{
					this._IsOpenSound=false;
					return true;
				}
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// �û���Ϣ
		/// </summary>
		public UserMsgs()
		{
		}
		/// <summary>
		/// �û���Ϣ
		/// </summary>
		/// <param name="empId"></param>
		public UserMsgs(int empId)
		{
		}
		#endregion
	}
}
