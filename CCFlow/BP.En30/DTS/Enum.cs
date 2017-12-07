using System;

namespace BP.DTS
{
	/// <summary>
	/// �������ͣ�
	/// </summary>
	public enum RunTimeType
	{
		/// <summary>
		/// ÿ����
		/// </summary>
		Minute,
		/// <summary>
		/// ÿСʱ
		/// </summary>
		Hour,
		/// <summary>
		/// ÿ��
		/// </summary>
		Day,
		/// <summary>
		/// ÿ��
		/// </summary>
		Month,		
		/// <summary>
		/// û��ָ��
		/// </summary>
		UnName
	}
	/// <summary>
	/// ��������
	/// </summary>
    public enum RunType
    {
        /// <summary>
        /// �м�㷽��
        /// </summary>
        Method,
        /// <summary>
        /// SQL�ı�
        /// </summary>
        SQL,
        /// <summary>
        /// �洢����
        /// </summary>
        SP,
        /// <summary>
        /// ���ݵ���
        /// </summary>
        DataIO
    }
}
