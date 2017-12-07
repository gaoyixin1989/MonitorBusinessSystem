using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;

namespace BP.Web.Controls
{
    public enum CalendarEnum
    {
        None,
        LongDateTime
    }
	/// <summary>
	/// WebCustomControl1 ��ժҪ˵����
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
	public class DatePicker : System.Web.UI.WebControls.WebControl , IPostBackEventHandler
	{
		private const string _BUTTONDEFAULTSTYLE = "BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; CURSOR:hand; BORDER-BOTTOM: gray 1px solid;";
		//ѡ�����ڰ�ť��Ĭ����ʽs
��   ��///��ťĬ���ı�
		private const string _BUTTONDEFAULTTEXT = "...";
		private System.Web.UI.WebControls.Calendar _Calendar;

		/// <summary>
		/// Controls
		/// </summary>
		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls(); //ȷ���ӿؼ������ѱ�����
				return base.Controls;
			}
		}
		//�����ӿؼ��������������ؼ���
		protected override void CreateChildControls()
		{
			Controls.Clear();
			_Calendar = new Calendar();
			_Calendar.ID = MyCalendarID;
			_Calendar.SelectedDate = DateTime.Parse(Text);
			_Calendar.TitleFormat = TitleFormat.MonthYear;
			_Calendar.NextPrevFormat = NextPrevFormat.ShortMonth;
			_Calendar.CellSpacing = 0;
			_Calendar.Font.Size = FontUnit.Parse("9pt");
			_Calendar.Font.Name = "Verdana";
			_Calendar.SelectedDayStyle.BackColor = ColorTranslator.FromHtml("#333399");
			_Calendar.SelectedDayStyle.ForeColor = ColorTranslator.FromHtml("White");
			_Calendar.DayStyle.BackColor = ColorTranslator.FromHtml("#CCCCCC");
			_Calendar.TodayDayStyle.BackColor = ColorTranslator.FromHtml("#999999");
			_Calendar.TodayDayStyle.ForeColor = ColorTranslator.FromHtml("Aqua");
			_Calendar.DayHeaderStyle.Font.Size = FontUnit.Parse("8pt");
			_Calendar.DayHeaderStyle.Font.Bold = true;
			_Calendar.DayHeaderStyle.Height = Unit.Parse("8pt");
			_Calendar.DayHeaderStyle.ForeColor = ColorTranslator.FromHtml("#333333");
			_Calendar.NextPrevStyle.Font.Size = FontUnit.Parse("8pt"); 
			_Calendar.NextPrevStyle.Font.Bold = true;
			_Calendar.NextPrevStyle.ForeColor = ColorTranslator.FromHtml("White");
			_Calendar.TitleStyle.Font.Size = FontUnit.Parse("12pt"); 
			_Calendar.TitleStyle.Font.Bold = true;
			_Calendar.TitleStyle.Height = Unit.Parse("12pt");
			_Calendar.TitleStyle.ForeColor = ColorTranslator.FromHtml("White");
			_Calendar.TitleStyle.BackColor = ColorTranslator.FromHtml("#333399");
			_Calendar.OtherMonthDayStyle.ForeColor = ColorTranslator.FromHtml("#999999");
			_Calendar.NextPrevFormat = NextPrevFormat.CustomText;
			_Calendar.NextMonthText = "����";
			_Calendar.PrevMonthText = "����";
			_Calendar.Style.Add("display","none"); //Ĭ�ϲ���ʾ���������ؼ�
			_Calendar.SelectionChanged += new EventHandler(_Calendar_SelectionChanged);
			this.Controls.Add(_Calendar);
		}
		[Category("Appearance"), //������������𣬲μ�ͼ
		DefaultValue(""), //����Ĭ��ֵ
		Description("���ø����ڿؼ���ֵ��") //���Ե�����
		]
		public string Text
		{
			get
			{
				EnsureChildControls();
				return (ViewState["Text"] == null)?System.DateTime.Today.ToString("yyyy-MM-dd"):ViewState["Text"].ToString();
			}
			set
			{
				EnsureChildControls();
				DateTime dt = System.DateTime.Today;
				try
				{
					dt = DateTime.Parse(value);
				}
				catch
				{
					throw new ArgumentOutOfRangeException("�������������ַ��������磺1981-04-29����");
				}
				ViewState["Text"] = DateFormat == CalendarEnum.LongDateTime?dt.ToString("yyyy-MM-dd"):dt.ToString("yyyy-M-d");
			}
		}
		//���ط������ؼ���Enabled���ԣ���ѡ�����ڰ�ť��ң����ã�
		public override bool Enabled
		{
			get{EnsureChildControls();return ViewState["Enabled"] == null?true:(bool)ViewState["Enabled"];}
			set{EnsureChildControls();ViewState["Enabled"] = value;}
		}
		public string ButtonStyle
		{
			get
			{
				EnsureChildControls();
				object o = ViewState["ButtonSytle"];
				return (o == null)?_BUTTONDEFAULTSTYLE:o.ToString();
			}
			set
			{
				EnsureChildControls();
				ViewState["ButtonSytle"] = value;
			}
		}

		[
		DefaultValue(CalendarEnum.LongDateTime),
		]

		public CalendarEnum DateFormat
		{
			get
			{
				EnsureChildControls();
				object format = ViewState["DateFormat"];
				return format == null?CalendarEnum.LongDateTime:(CalendarEnum)format;
			}
			set
			{
				EnsureChildControls();
				ViewState["DateFormat"] = value;
				DateTime dt = DateTime.Parse(Text);
				Text=DateFormat == CalendarEnum.LongDateTime?dt.ToString("yyyy-MM-dd"):dt.ToString("yyyy-M-d");
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string MyCalendarID //���Ͽؼ�ID
		{
			get
			{
				EnsureChildControls();
				return this.ClientID+"_MyCalendar";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string MyCalendarName //���Ͽؼ�����
		{
			get
			{
				EnsureChildControls();
				return this.UniqueID+":MyCalendar";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string DatePickerInputID //���Ͽؼ���������ID
		{
			get
			{
				EnsureChildControls();
				return this.ClientID+"_DateInput";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string DatePickerInputName //���Ͽؼ�������������
		{
			get
			{
				EnsureChildControls();
				return this.UniqueID+":DateInput";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string DatePickerButtonID //���Ͽؼ��а�ť��ID
		{
			get
			{
				EnsureChildControls();
				return this.ClientID+"_DateButton";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]

		public string DatePickerButtonName //���Ͽؼ��а�ť������
		{
			get
			{
				EnsureChildControls();
				return this.UniqueID+":DateButton";
			}
		}

		public string ButtonText
		{
			get
			{
				EnsureChildControls();
				return ViewState["ButtonText"] == null?_BUTTONDEFAULTTEXT:(string)ViewState["ButtonText"];
			}
			set
			{
				EnsureChildControls();
				ViewState["ButtonText"] = value;
			}
		}

		/// <summary>
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>

		protected override void Render(HtmlTextWriter output)
		{
			//��ҳ��������ؼ�ʱ������һ����񣨶��ж��У��������Ǳ�����ʽ
			output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");

			output.AddStyleAttribute("LEFT", this.Style["LEFT"]);
			output.AddStyleAttribute("TOP", this.Style["TOP"]);
			output.AddStyleAttribute("POSITION", "absolute");

			if (Width != Unit.Empty)
			{
				output.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());
			}
			else
			{
				output.AddStyleAttribute(HtmlTextWriterStyle.Width, "200px");
			}

			output.RenderBeginTag(HtmlTextWriterTag.Table); //������
			output.RenderBeginTag(HtmlTextWriterTag.Tr); //����һ��
			output.AddAttribute(HtmlTextWriterAttribute.Width, "90%");
			output.RenderBeginTag(HtmlTextWriterTag.Td);

			//�����ǵ�һ�е�һ�����ı�������Լ�����ʽ����

			if (!Enabled)
			{
				output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
			}

			output.AddAttribute(HtmlTextWriterAttribute.Type, "Text");
			output.AddAttribute(HtmlTextWriterAttribute.Id, DatePickerInputID);
			output.AddAttribute(HtmlTextWriterAttribute.Name, DatePickerInputName);
			output.AddAttribute(HtmlTextWriterAttribute.Value, Text);
			output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			output.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			output.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, Font.Name);
			output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, Font.Size.ToString());
			output.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, Font.Bold?"bold":"");
			output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
			output.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(ForeColor));
			output.RenderBeginTag(HtmlTextWriterTag.Input); //����ı���
			output.RenderEndTag();
			output.RenderEndTag();
			output.AddAttribute(HtmlTextWriterAttribute.Width, "*");
			output.RenderBeginTag(HtmlTextWriterTag.Td);

			//�����ǵ�һ�еڶ����а�ť�����Լ�����ʽ����

			if (!Enabled)
			{
				output.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
			}

			output.AddAttribute(HtmlTextWriterAttribute.Type, "Submit");
			output.AddAttribute(HtmlTextWriterAttribute.Id, DatePickerButtonID);
			output.AddAttribute(HtmlTextWriterAttribute.Name, DatePickerButtonName);
			output.AddAttribute(HtmlTextWriterAttribute.Value, ButtonText);
			output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
//            output.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this)); //�����ťʱ��Ҫ�ش������������������OnClick�¼�

			output.AddAttribute(HtmlTextWriterAttribute.Style, ButtonStyle);
			output.RenderBeginTag(HtmlTextWriterTag.Input); //�����ť
			output.RenderEndTag();
			output.RenderEndTag();

			output.RenderEndTag();
			output.RenderBeginTag(HtmlTextWriterTag.Tr);
			output.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
			output.RenderBeginTag(HtmlTextWriterTag.Td);
			_Calendar.RenderControl(output); //�������ӿؼ����
			output.RenderEndTag();
			output.RenderEndTag();
			output.RenderEndTag();
		}

		//���Ͽؼ�����̳�IpostBackEventHandler�ӿڣ����ܼ̳�RaisePostBackEvent�¼�
		public void RaisePostBackEvent(string eventArgument)
		{
			OnClick(EventArgs.Empty);
		}
		protected virtual void OnClick(EventArgs e)
		{
			//���ѡ�����ڰ�ťʱ����������ӿؼ�û����ʾ����ʾ���������ı����ֵ��ֵ�������ӿؼ�
			if (_Calendar.Attributes["display"] != "")
			{
				_Calendar.SelectedDate = DateTime.Parse(Text);
				_Calendar.Style.Add("display","");
			}
		}

		//���Ͽؼ��е������ؼ����ڱ仯�¼�
		private void _Calendar_SelectionChanged(object sender, EventArgs e)
		{
			//��ѡ������ڱ仯ʱ������ѡ���ڸ�ֵ���ı��򲢽������ӿؼ�����
			Text = _Calendar.SelectedDate.ToString();
			_Calendar.Style.Add("display","none");
		}
	}
}

	
	 
