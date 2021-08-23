using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CircleViewer
{
	class CircleViewer : Form
	{
		private PictureBox pic;
		private TextBox text;
		private Button redraw;

		private readonly int DOT_SIZE;
		private readonly int CROSS_SIZE;
		private readonly double SCALE;
		private List<Tuple<double, double>> samples;

		public double rate;
		public float radius;

		public CircleViewer ()
		{
			this.pic = new PictureBox();
			this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Controls.Add(this.pic);

			this.text = new TextBox();
			this.text.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Controls.Add(this.text);

			this.redraw = new Button();
			this.redraw.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Controls.Add(this.redraw);

			this.samples = new List<Tuple<double, double>>();
			this.DOT_SIZE = 4;
			this.CROSS_SIZE = 32;
			this.SCALE = 1.5;
			this.rate = 1.0;
			this.radius = 1.0F;
		}

		public void add_sample (double x, double y)
		{
			this.samples.Add(new Tuple<double, double>(x, y));
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			var g = this.pic.CreateGraphics();
			var pen = new Pen(Color.Red, this.DOT_SIZE);
			var dash_pen = new Pen(Color.Blue, 2);
			dash_pen.DashStyle = DashStyle.Dash;
			var c = new Tuple<double, double>(
					this.pic.ClientSize.Width / 2.0
					, this.pic.ClientSize.Height / 2.0);

			g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0
						, this.pic.ClientSize.Width
						, this.pic.ClientSize.Height));

			// íÜêS
			g.DrawLine(dash_pen, (float)c.Item1
					, (float)(c.Item2 - this.CROSS_SIZE / 2)
					, (float)c.Item1
					, (float)(c.Item2 + this.CROSS_SIZE / 2));
			g.DrawLine(dash_pen, (float)(c.Item1 - this.CROSS_SIZE / 2)
					, (float)c.Item2
					, (float)(c.Item1 + this.CROSS_SIZE / 2)
					, (float)c.Item2);

			// äÓèÄâ~
			DrawCircleC(g, dash_pen, (float)c.Item1, (float)c.Item2, (float)(this.radius * this.SCALE));

			// äÓèÄâ~Ç∆ÇÃç∑Ç…ëŒÇµÇƒrateÇä|ÇØÇƒòcÇ›ÇägëÂÇµÇΩåvë™ì_Çï\é¶
			foreach (var pt in this.samples)
			{
				var ang = Math.Atan2(pt.Item2, pt.Item1);
				var r = Math.Sqrt(Math.Pow(pt.Item1, 2) + Math.Pow(pt.Item2, 2));
				var mod_r = (r - this.radius) * this.rate + this.radius;
				DrawCircleC(g, pen
						, (float)(mod_r * Math.Sin(ang) * this.SCALE + c.Item1)
						, (float)(mod_r * Math.Cos(ang) * this.SCALE + c.Item2)
						, (float)this.DOT_SIZE / 2);
			}

			pen.Dispose();
			dash_pen.Dispose();
		}

		// íÜêSç¿ïWäÓèÄÇ≈ê≥â~Çï`Ç≠
		private static void DrawCircleC (Graphics g, Pen p, float cx, float cy, float r)
		{
			g.DrawEllipse(p, (float)(cx - r), (float)(cy - r), r * 2, r * 2);
		}

	}
}
