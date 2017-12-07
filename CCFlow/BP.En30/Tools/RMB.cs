
using System;

namespace BP.Tools
{ 
	/// 
	/// ���ܣ��ַ����������� 
	/// 
	public class DealString
	{ 
		#region ˽�г�Ա 
		/// 
		/// �����ַ��� 
		/// 
		private string inputString=null; 
		/// 
		/// ����ַ��� 
		/// 
		private string outString=null; 
		/// 
		/// ��ʾ��Ϣ 
		/// 
		private string noteMessage=null; 
		#endregion 

		#region �������� 
		/// 
		/// �����ַ��� 
		/// 
		public string InputString 
		{ 
			get{return inputString;} 
			set{inputString=value;} 
		} 
		/// 
		/// ����ַ��� 
		/// 
		public string OutString 
		{ 
			get{return outString;} 
			set{outString=value;} 
		} 
		/// 
		/// ��ʾ��Ϣ 
		/// 
		public string NoteMessage 
		{ 
			get{return noteMessage;} 
			set{noteMessage=value;} 
		} 
		#endregion 

		#region ���캯�� 
        public DealString() 
		{ 
			// 
			// TODO: �ڴ˴���ӹ��캯���߼� 
			// 
		} 
		#endregion 

		#region �������� 
		public void ConvertToChineseNum() 
		{ 
			string numList="��Ҽ��������½��ƾ�"; 
			string rmbList = "�ֽ�Ԫʰ��Ǫ��ʰ��Ǫ��ʰ��Ǫ��"; 
			double number=0; 
			string tempOutString=null; 

			try 
			{ 
				number=double.Parse(this.inputString); 
			} 
			catch 
			{ 
				this.noteMessage="������������֣�"; 
				return; 
			} 

			if(number>9999999999999.99) 
				this.noteMessage="������Χ�������ֵ"; 

			//��С��ת��Ϊ�����ַ��� 
			string tempNumberString=Convert.ToInt64(number*100).ToString(); 
			int tempNmberLength=tempNumberString.Length; 
			int i=0; 
			while( i<tempNmberLength ) 
			{ 
				int oneNumber=Int32.Parse(tempNumberString.Substring(i,1)); 
				string oneNumberChar=numList.Substring(oneNumber,1); 
				string oneNumberUnit=rmbList.Substring(tempNmberLength-i-1,1); 
				if(oneNumberChar!="��") 
					tempOutString+=oneNumberChar+oneNumberUnit; 
				else 
				{ 
					if(oneNumberUnit=="��"||oneNumberUnit=="��"||oneNumberUnit=="Ԫ"||oneNumberUnit=="��") 
					{ 
						while (tempOutString.EndsWith("��")) 
						{ 
							tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
						} 

					}
                    if (oneNumberUnit == "��" || (oneNumberUnit == "��" && !tempOutString.EndsWith("��")) || oneNumberUnit == "Ԫ")
                    {
                        tempOutString += oneNumberUnit;
                    }
                    else
                    {
                        bool tempEnd = tempOutString.EndsWith("��");
                        bool zeroEnd = tempOutString.EndsWith("��");
                        if (tempOutString.Length > 1)
                        {
                            bool zeroStart = tempOutString.Substring(tempOutString.Length - 2, 2).StartsWith("��");
                            if (!zeroEnd && (zeroStart || !tempEnd))
                                tempOutString += oneNumberChar;
                        }
                        else
                        {
                            if (!zeroEnd && !tempEnd)
                                tempOutString += oneNumberChar;
                        }
                    } 
				} 
				i+=1; 
			} 

			while (tempOutString.EndsWith("��")) 
			{ 
				tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
			} 

			while(tempOutString.EndsWith("Ԫ")) 
			{ 
				tempOutString=tempOutString+"��"; 
			} 

			this.outString=tempOutString; 


		} 
		#endregion 
	} 
} 
