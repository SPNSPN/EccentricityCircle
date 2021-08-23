using System;
using System.Collections.Generic;
using System.Linq;

namespace EccentricityCircle
{
	class EccentricityCircle
	{
		private double r;
		private double e;

		public EccentricityCircle (double radius, double eccentricity)
		{
			this.r = radius;
			this.e = eccentricity;
		}

		// 余弦定理から
		// (i)   Re^2 = e^2 + R^2 - 2eR cos(Ar)
		// (ii)  S^2 = R^2 + R^2 - 2R^2 cos(Ar)
		// (iii) S^2 = (R-e)^2 + R^2 - 2(R-e)R cos(A)
		// Re: 偏中心からサンプル点までの距離
		// e:  偏心量
		// R:  半径
		// Ar: 真中心からみたサンプル点の方向
		// S:  0-A間の弦の長さ
		// A:  偏中心からみたサンプル点の方向
		//
		// (i)(ii)(iii)から
		// Re = sqrt(e^2 + R^2 + e(R-e)^2/R - eR - 2(R-e)e cos(A))
		public double RadiusFromAngle (double ang)
		{
			double m = this.r - this.e;

			return Math.Sqrt(Math.Pow(this.e, 2)
					+ Math.Pow(this.r, 2)
					+ this.e * Math.Pow(m, 2) / this.r
					- this.e * this.r
					- 2 * m * this.e * Math.Cos(ang));
			/*
			double r2 = Math.Pow(this.radius, 2);
			return Math.Sqrt(Math.Pow(this.eccentricity, 2) + r2 
					- 2 * this.eccentricity * this.radius
					* (1 - (Math.Pow(m, 2) + r2
					- 2 * m * this.radius * Math.Cos(ang)) / (2 * r2)));
			*/
		}

		// RadiusFromAngleの逆関数
		// A = acos((e^2 + R^2 - Re^2 - eR + e(R-e)^2/R) / -2(R-e)e)
		public double AngleFromRadius (double re)
		{
			double m = this.r - this.e;

			return Math.Acos((Math.Pow(this.e, 2)
						+ Math.Pow(this.r, 2)
						- Math.Pow(re, 2)
						- this.e * this.r
						+ this.e * Math.Pow(m, 2) / this.r)
					/ (-2 * m * this.e));
		}
	}
}
