using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;

namespace BP.Web.Controls
{
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:ImgEn runat=server></{0}:ImgEn>")]
	public class ImgEn : System.Web.UI.WebControls.WebControl , IPostBackEventHandler
	{
		private const string _BUTTONDEFAULTSTYLE = "BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; CURSOR:hand; BORDER-BOTTOM: gray 1px solid;";
		//ѡ��Label��ť��Ĭ����ʽs
��   ��///��ťĬ���ı�
		private const string _BUTTONDEFAULTTEXT = "...";
		private System.Web.UI.WebControls.Label _Label;
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
			_Label = new Label();
			_Label.ID = MyLabelID;
			_Label.Font.Size = FontUnit.Parse("9pt");
			_Label.Font.Name = "Verdana";

			this.Controls.Add(_Label);
		}
		[Category("Appearance"), //������������𣬲μ�ͼ
		DefaultValue(""), //����Ĭ��ֵ
		Description("���ø�Label�ؼ���ֵ��") //���Ե�����
		]
		public string Text
		{
			get
			{
				EnsureChildControls();
				return (string)ViewState["Text"];
			}
			set
			{
				ViewState["Text"] = value;
			}
		}
		//���ط������ؼ���Enabled���ԣ���ѡ��Label��ť��ң����ã�
		public override bool Enabled
		{
			get{EnsureChildControls();return ViewState["Enabled"] == null?true:(bool)ViewState["Enabled"];}
			set{EnsureChildControls();ViewState["Enabled"] = value;}
		}
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public string MyLabelID //���Ͽؼ�ID
		{
			get
			{
				EnsureChildControls();
				return this.ClientID+"_MyLabel";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public string MyLabelName //���Ͽؼ�����
		{
			get
			{
				EnsureChildControls();
				return this.UniqueID+":MyLabel";
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public string ImgEnInputID //���Ͽؼ���������ID
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
		public string ImgEnInputName //���Ͽؼ�������������
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
		public string ImgEnButtonID //���Ͽؼ��а�ť��ID
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

		public string ImgEnButtonName //���Ͽؼ��а�ť������
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
			output.AddAttribute(HtmlTextWriterAttribute.Id, ImgEnInputID);
			output.AddAttribute(HtmlTextWriterAttribute.Name, ImgEnInputName);
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
			output.AddAttribute(HtmlTextWriterAttribute.Id, ImgEnButtonID);
			output.AddAttribute(HtmlTextWriterAttribute.Name, ImgEnButtonName);
			output.AddAttribute(HtmlTextWriterAttribute.Value, ButtonText);
			output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			//output.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.GetPostBackEventReference(this)); //�����ťʱ��Ҫ�ش������������������OnClick�¼�

			//output.AddAttribute(HtmlTextWriterAttribute.Style, ButtonStyle);
			output.RenderBeginTag(HtmlTextWriterTag.Input); //�����ť
			output.RenderEndTag();
			output.RenderEndTag();

			output.RenderEndTag();
			output.RenderBeginTag(HtmlTextWriterTag.Tr);
			output.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
			output.RenderBeginTag(HtmlTextWriterTag.Td);
			_Label.RenderControl(output); //�������ӿؼ����
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
			//			//���ѡ�����ڰ�ťʱ����������ӿؼ�û����ʾ����ʾ���������ı����ֵ��ֵ�������ӿؼ�
			//			if (_Calendar.Attributes["display"] != "")
			//			{
			//				_Calendar.SelectedDate = DateTime.Parse(Text);
			//				_Calendar.Style.Add("display","");
			//			}
		}

		//���Ͽؼ��е������ؼ�Label�仯�¼�
		private void _Label_SelectionChanged(object sender, EventArgs e)
		{
			return ;

			//��ѡ���Label�仯ʱ������ѡLabel��ֵ���ı��򲢽������ӿؼ�����
			//Text = _Label.SelectedDate.ToString();
			//_Label.Style.Add("display","none");
		}
	}
}

	
	 
