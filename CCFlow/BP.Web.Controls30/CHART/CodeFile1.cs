using System;
using System.IO;//�����ļ���ȡ 
using System.Data;//�������ݷ��� 
using System.Drawing;//�ṩ��GDI+ͼ�εĻ������� 
using System.Drawing.Text;//�ṩ��GDI+ͼ�εĸ߼����� 
using System.Drawing.Drawing2D;//�ṩ���߼���ά��ʸ��ͼ�ι��� 
using System.Drawing.Imaging;//�ṩ��GDI+ͼ�εĸ߼����� 
using BP.Sys;

namespace BP.Web.Controls
{
    public class PieChart
    {
        public PieChart()
        {
        }
        public void Render(string title, string subTitle, int width, int height, DataTable dt)
        {
            const int SIDE_LENGTH = 400;
            const int PIE_DIAMETER = 200;

            //ͨ�����������ȡ�ñ�ͼ�е��ܻ��� 
            float sumData = 0;
            foreach (DataRow dr in dt.Rows)
            {
                sumData += Convert.ToSingle(dr[1]);
            }
            //����һ��image���󣬲��ɴ˲���һ��Graphics���� 
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            //���ö���g������ 
            g.ScaleTransform((Convert.ToSingle(width)) / SIDE_LENGTH, (Convert.ToSingle(height)) / SIDE_LENGTH);
            g.SmoothingMode = SmoothingMode.Default;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //�����ͱߵ��趨 
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, 0, 0, SIDE_LENGTH - 1, SIDE_LENGTH - 1);
            //����ͼ���� 
            g.DrawString(title, new Font("Tahoma", 24), Brushes.Black, new PointF(5, 5));
            //����ͼ��ͼ�� 
            g.DrawString(subTitle, new Font("Tahoma", 14), Brushes.Black, new PointF(7, 35));
            //����ͼ 
            float curAngle = 0;
            float totalAngle = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                curAngle = Convert.ToSingle(dt.Rows[i][1]) / sumData * 360;

                g.FillPie(new SolidBrush(ChartUtil.GetChartItemColor(i)), 100, 65, PIE_DIAMETER, PIE_DIAMETER, totalAngle, curAngle);
                g.DrawPie(Pens.Black, 100, 65, PIE_DIAMETER, PIE_DIAMETER, totalAngle, curAngle);
                totalAngle += curAngle;
            }
            //��ͼ���������� 
            g.DrawRectangle(Pens.Black, 200, 300, 199, 99);
            g.DrawString("Legend", new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 300));

            //��ͼ������ 
            PointF boxOrigin = new PointF(210, 330);
            PointF textOrigin = new PointF(235, 326);
            float percent = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                g.FillRectangle(new SolidBrush(ChartUtil.GetChartItemColor(i)), boxOrigin.X, boxOrigin.Y, 20, 10);
                g.DrawRectangle(Pens.Black, boxOrigin.X, boxOrigin.Y, 20, 10);
                percent = Convert.ToSingle(dt.Rows[i][1]) / sumData * 100;
                g.DrawString(dt.Rows[i][0].ToString() + " - " + dt.Rows[i][1].ToString() + " (" + percent.ToString("0") + "%)", new Font("Tahoma", 10), Brushes.Black, textOrigin);
                boxOrigin.Y += 15;
                textOrigin.Y += 15;
            }
            //ͨ��Response.OutputStream����ͼ�ε����ݷ��͵������ 
            string file = SystemConfig.PathOfTemp + "Pie" + Web.WebUser.No + ".gif";
            bm.Save(file, ImageFormat.Gif);
            //������Դ 
            bm.Dispose();
            g.Dispose();
        }
    }

    //������ͼ 
    public class BarChart
    {
        public BarChart()
        {
        }
        public void Render(string title, string subTitle, int width, int height, DataTable dt, Stream target)
        {
            const int SIDE_LENGTH = 400;
            const int CHART_TOP = 75;
            const int CHART_HEIGHT = 200;
            const int CHART_LEFT = 50;
            const int CHART_WIDTH = 300;
           // DataTable dt = chartData.Tables[0];

            //������ߵĵ� 
            float highPoint = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (highPoint < Convert.ToSingle(dr[1]))
                {
                    highPoint = Convert.ToSingle(dr[1]);
                }
            }
            //����һ��Graphics����ʵ�� 
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            //������ͼͼ�κ��������� 
            g.ScaleTransform((Convert.ToSingle(width)) / SIDE_LENGTH, (Convert.ToSingle(height)) / SIDE_LENGTH);
            g.SmoothingMode = SmoothingMode.Default;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //�趨�����ͱ� 
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, 0, 0, SIDE_LENGTH - 1, SIDE_LENGTH - 1);
            //������� 
            g.DrawString(title, new Font("Tahoma", 24), Brushes.Black, new PointF(5, 5));
            //��С���� 
            g.DrawString(subTitle, new Font("Tahoma", 14), Brushes.Black, new PointF(7, 35));
            //������ͼ 
            float barWidth = CHART_WIDTH / (dt.Rows.Count * 2);
            PointF barOrigin = new PointF(CHART_LEFT + (barWidth / 2), 0);
            float barHeight = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                barHeight = Convert.ToSingle(dt.Rows[i][1]) * 200 / highPoint;
                barOrigin.Y = CHART_TOP + CHART_HEIGHT - barHeight;
                g.FillRectangle(new SolidBrush(ChartUtil.GetChartItemColor(i)), barOrigin.X, barOrigin.Y, barWidth, barHeight);
                barOrigin.X = barOrigin.X + (barWidth * 2);
            }
            //���ñ� 
            g.DrawLine(new Pen(Color.Black, 2), new Point(CHART_LEFT, CHART_TOP), new Point(CHART_LEFT, CHART_TOP + CHART_HEIGHT));
            g.DrawLine(new Pen(Color.Black, 2), new Point(CHART_LEFT, CHART_TOP + CHART_HEIGHT), new Point(CHART_LEFT + CHART_WIDTH, CHART_TOP + CHART_HEIGHT));
            //��ͼ��������� 
            g.DrawRectangle(new Pen(Color.Black, 1), 200, 300, 199, 99);
            g.DrawString("Legend", new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 300));

            //��ͼ�� 
            PointF boxOrigin = new PointF(210, 330);
            PointF textOrigin = new PointF(235, 326);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                g.FillRectangle(new SolidBrush(ChartUtil.GetChartItemColor(i)), boxOrigin.X, boxOrigin.Y, 20, 10);
                g.DrawRectangle(Pens.Black, boxOrigin.X, boxOrigin.Y, 20, 10);
                g.DrawString(dt.Rows[i][0].ToString() + " - " + dt.Rows[i][1].ToString(), new Font("Tahoma", 10), Brushes.Black, textOrigin);
                boxOrigin.Y += 15;
                textOrigin.Y += 15;
            }
            //���ͼ�� 
            bm.Save(BP.Sys.SystemConfig.PathOfTemp+"Bar"+BP.Web.WebUser.No+".gif", ImageFormat.Gif);

            //��Դ���� 
            bm.Dispose();
            g.Dispose();
        }
    }
    public class ChartUtil
    {
        public ChartUtil()
        {
        }
        public static Color GetChartItemColor(int itemIndex)
        {
            Color selectedColor;
            switch (itemIndex)
            {
                case 0:
                    selectedColor = Color.Blue;
                    break;
                case 1:
                    selectedColor = Color.Red;
                    break;
                case 2:
                    selectedColor = Color.Yellow;
                    break;
                case 3:
                    selectedColor = Color.Purple;
                    break;
                default:
                    selectedColor = Color.Green;
                    break;
            }
            return selectedColor;
        }
    }
}
