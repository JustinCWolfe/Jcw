using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common.Interpolation
{
    public class LinearInterpolator : IInterpolator
    {
        public double Interpolate(double x0, double y0, double x1, double y1, double x)
        {
            return y0 + (y1 - y0) * ((x - x0) / (x1 - x0));
        }
    }
}
