using System;
 

namespace BP.Web.Controls
{
	/// <summary>
	/// ��ť����
	/// </summary>
	public enum BtnType
	{
		/// <summary>
		/// ȷ�� ,��Ҫ����hit ��ֵ�� 
		/// �أأء�ȷ����
		/// </summary>
		ConfirmHit,
		/// <summary>
		/// ����
		/// </summary>
		Normal,			
		/// <summary>
		/// ȷ��
		/// </summary>
		Confirm,
		/// <summary>
		/// ����
		/// </summary>
		Save,
		/// <summary>
		/// ���沢�½�
		/// </summary>
		SaveAndNew,
		/// <summary>
		/// ����
		/// </summary>
		Search,
		/// <summary>
		/// ȡ��
		/// </summary>
		Cancel,
		/// <summary>
		/// ɾ��
		/// </summary>
		Delete,
		/// <summary>
		/// ����
		/// </summary>
		Update,
		/// <summary>
		/// ����
		/// </summary>
		Insert,
		/// <summary>
		/// �༭
		/// </summary>
		Edit,
		/// <summary>
		/// �½�
		/// </summary>
		New,
		/// <summary>
		/// ���
		/// </summary>
		View,
		/// <summary>
		/// �ر�
		/// </summary>
		Close,
		/// <summary>
		/// ����
		/// </summary>
		Export,
		/// <summary>
		/// ��ӡ
		/// </summary>
		Print,
		/// <summary>
		/// ����
		/// </summary>
		Add,
		/// <summary>
		/// һ��
		/// </summary>
		Reomve,
		/// <summary>
		/// ����
		/// </summary>
		Back,
		/// <summary>
		/// ˢ��
		/// </summary>
		Refurbish,
		/// <summary>
		/// ��������
		/// </summary>
		ApplyTask,
		/// <summary>
		/// ѡ��ȫ��
		/// </summary>
		SelectAll,
		/// <summary>
		/// ȫ��ѡ
		/// </summary>
		SelectNone
	} 
	/// <summary>
	/// TaxBox ����
	/// </summary>
	public enum TBType
	{
		/// <summary>
		/// Entities ��DataHelp, �������˵���ˣ�����Ens , ��Ҫָ��DataHelpKey. 
		/// ������ϵͳ�ͻ����Ҽ��������������
		/// </summary>
		Ens,
		/// <summary>
		/// Entities ��DataHelp, �������˵���ˣ�����Ens , ��Ҫָ��DataHelpKey. 
		/// ������ϵͳ�ͻ����Ҽ��������������
		/// ��������Ҫ���ֵ��ѡ�����⡣��ѡ����ֵ��ʱ�䣬����',' �����Ƿֿ����ء� 
		/// </summary>
		EnsOfMany,
		/// <summary>
		/// �Զ�������͡�
		/// </summary>
		Self,
		/// <summary>
		/// ������
		/// </summary>
		TB,
		/// <summary>
		/// Num
		/// </summary>
		Num,
		/// <summary>
		/// Int
		/// </summary>
		Int,		 
		/// <summary>
		/// Float
		/// </summary>
		Float,
		/// <summary>
		/// Decimal
		/// </summary>
		Decimal,
		/// <summary>
		/// Moneny
		/// </summary>
		Moneny,
		/// <summary>
		/// Date
		/// </summary>
		Date,
		/// <summary>
		/// DateTime
		/// </summary>
		DateTime,
		/// <summary>
		/// Email
		/// </summary>
		Email,		
		/// <summary>
		/// Key
		/// </summary>
		Key,
		Area

	} 
	/// <summary>
	/// AddAllLocation
	/// </summary>
	public enum AddAllLocation
	{
		/// <summary>
		/// ��ʾ�Ϸ�
		/// </summary>
		Top,
		/// <summary>
		/// ��ʾ�·�
		/// </summary>
		End,
		/// <summary>
		/// ��ʾ�Ϸ����·�
		/// </summary>
		TopAndEnd,
		/// <summary>
		/// ����ʾ
		/// </summary>
		None,
        /// <summary>
        /// ��ѡ
        /// </summary>
        TopAndEndWithMVal
	} 
	/// <summary>
	/// DDLShowType
	/// </summary>
	public enum DDLShowType
	{
		/// <summary>
		/// None
		/// </summary>
		None,
		/// <summary>
		/// Gender
		/// </summary>
		Gender,
		/// <summary>
		/// Boolean
		/// </summary>
		Boolean,
		/// <summary>
		/// 
		/// </summary> 
		SysEnum,
		/// <summary>
		/// Self
		/// </summary>
		Self,
		/// <summary>
		/// ʵ�弯��
		/// </summary>
		Ens,
		/// <summary>
		/// ��Table �����
		/// </summary>
		BindTable
	}
	/// <summary>
	/// DDLShowType
	/// </summary>
	public enum DDLShowType_del
	{
		/// <summary>
		/// None
		/// </summary>
		None,
		/// <summary>
		/// Gender
		/// </summary>
		Gender,
		/// <summary>
		/// Boolean
		/// </summary>
		Boolean,
		/// <summary>
		/// Year
		/// </summary>
		Year,
		/// <summary>
		/// Month
		/// </summary>
		Month,
		/// <summary>
		/// Day
		/// </summary>
		Day,
		/// <summary>
		/// hh
		/// </summary>
		hh,
		/// <summary>
		/// mm
		/// </summary>
		mm,
		/// <summary>
		/// ����
		/// </summary>
		Quarter,
		/// <summary>
		/// Week
		/// </summary>
		Week,
		/// <summary>
		/// ϵͳö������ SelfBindKey="ϵͳö��Key"
		/// </summary>
		SysEnum,
		/// <summary>
		/// Self
		/// </summary>
		Self,
		/// <summary>
		/// ʵ�弯��
		/// </summary>
		Ens,
		/// <summary>
		/// ��Table �����
		/// </summary>
		BindTable
	}
    /// <summary>
    /// ��ʾ��ʽ
    /// </summary>
    public enum FormShowType
    {
        /// <summary>
        /// δ����
        /// </summary>
        NotSet,
        /// <summary>
        /// ɵ�ϱ�
        /// </summary>
        FixForm,
        /// <summary>
        /// ���ɱ�
        /// </summary>
        FreeForm
    }
}