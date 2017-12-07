using System; 
using System.Collections;
using BP.DA; 
using BP.Web.Controls;
using System.Reflection;
using BP.En;
namespace BP.En
{
    public enum MsgShowType
    {
        /// <summary>
        /// ������
        /// </summary>
        SelfAlert,
        /// <summary>
        /// ��ʾ��
        /// </summary>
        SelfMsgWindows,
        /// <summary>
        /// �´���
        /// </summary>
        Blank
    }
	/// <summary>
	/// Method ��ժҪ˵��
	/// </summary>
    abstract public class Method
    {
        /// <summary>
        /// ��Ϣ��ʾ����
        /// </summary>
        public MsgShowType HisMsgShowType = MsgShowType.Blank;

        #region Http
        public string Request(string key)
        {
            return BP.Sys.Glo.Request.QueryString[key];
        }
        /// <summary>
        /// ��ȡMyPK
        /// </summary>
        public string RequestRefMyPK
        {
            get
            {
                string s = Request("RefMyPK");
                if (s == null)
                    s = Request("RefPK");

                return s;
            }
        }
        public string RequestRefNo
        {
            get
            {
                return Request("RefNo");
            }
        }
        public int RequestRefOID
        {
            get
            {
                return int.Parse(Request("RefOID"));
            }
        }
        #endregion Http

        #region ROW
        /// <summary>
        /// ��ȡKeyֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>���</returns>
        public object GetValByKey(string key)
        {
            return this.Row.GetValByKey(key);
        }
        /// <summary>
        /// ��ȡstrֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>���</returns>
        public string GetValStrByKey(string key)
        {
            return this.GetValByKey(key).ToString();
        }
        /// <summary>
        /// ��ȡintֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>���</returns>
        public int GetValIntByKey(string key)
        {
            return (int)this.GetValByKey(key);
        }

        /// <summary>
        /// ��ȡdecimalֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>���</returns>
        public decimal GetValDecimalByKey(string key)
        {
            return (decimal)this.GetValByKey(key);
        }
        /// <summary>
        /// ��ȡboolֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>���</returns>
        public bool GetValBoolByKey(string key)
        {
            if (this.GetValIntByKey(key) == 1)
                return true;
            return false;
        }
        public void SetValByKey(string attrKey, int val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, Int64 val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, float val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, decimal val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, object val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        /// <summary>
        /// ʵ��� map ��Ϣ��	
        /// </summary>		
        //public abstract void EnMap();		
        private Row _row = null;
        public Row Row
        {
            get
            {
                if (this.HisAttrs == null)
                    return null;

                if (this._row == null)
                {
                    this._row = new Row();
                    this._row.LoadAttrs(this.HisAttrs);
                }

                return this._row;
            }
            set
            {
                this._row = value;
            }
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        public Method()
        {

        }

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        private Attrs _HisAttrs = null;
        public Attrs HisAttrs
        {
            get
            {
                if (_HisAttrs == null)
                    _HisAttrs = new Attrs();
                return _HisAttrs;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title = null;
        public string Help = null;

        /// <summary>
        /// ����ǰ��ʾ��Ϣ
        /// </summary>
        public string Warning = null;
        /// <summary>
        /// ͼ��
        /// </summary>
        public string Icon = null;
        public string GetIcon(string path)
        {
            if (this.Icon == null)
            {
                return "<img src='/WF/Img/Btn/Do.gif'  border=0 />";
            }
            else
            {
                return Icon;
                //return "<img src='" + path + Icon + "'  border=0 />";
            }
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public string ToolTip = null;
        /// <summary>
        /// Ŀ��
        /// </summary>
        public string Target = "OpenWin";
        /// <summary>
        /// �߶�
        /// </summary>
        public int Height = 600;
        /// <summary>
        /// ���
        /// </summary>
        public int Width = 800;
        /// <summary>
        /// ִ��
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public abstract object Do();
        public abstract void Init();
        /// <summary>
        /// Ȩ�޹���
        /// </summary>
        public abstract bool IsCanDo
        {
            get;
        }
        /// <summary>
        /// �Ƿ���ʾ�ڹ����б���
        /// </summary>
        public bool IsVisable = true;
        #endregion
    }
}
