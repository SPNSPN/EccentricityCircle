using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using CircleViewer;

namespace Program
{
    class Program
    {
		public static List<Tuple<double, double>> get_dummy_samples ()
		{
			var samples = new List<Tuple<double, double>>();
			samples.Add(new Tuple<double, double>(50.0, 0.0));
			samples.Add(new Tuple<double, double>(-45.0, 0.0));
			samples.Add(new Tuple<double, double>(0.0, 55.0));
			samples.Add(new Tuple<double, double>(0.0, -50.0));
			samples.Add(new Tuple<double, double>(35.3553, 35.3553));
			samples.Add(new Tuple<double, double>(35.3553, -35.3553));
			samples.Add(new Tuple<double, double>(-35.3553, 35.3553));
			samples.Add(new Tuple<double, double>(-35.3553, -35.3553));

			return samples;
		}

		[STAThread]
		public static void Main (string[] args)
		{
			var viewer = new CircleViewer.CircleViewer();

			var samples = get_dummy_samples();
			double acc = 0.0;
			foreach (var pt in samples)
			{
				acc += Math.Sqrt(Math.Pow(pt.Item1, 2)
								+ Math.Pow(pt.Item2, 2));
				viewer.add_sample(pt.Item1, pt.Item2);
			}
			viewer.radius = (float)(acc / samples.Count);
			viewer.rate = 2.5;

			Application.EnableVisualStyles();
			Application.Run(viewer);
		}
    }
}
