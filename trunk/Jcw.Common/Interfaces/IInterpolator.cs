using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface IInterpolator
    {
        double Interpolate(double x0, double y0, double x1, double y1, double x);
    }
}
