using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common.Interpolation
{
    public enum InterpolationType
    {
        Linear,
    }

    public static class InterpolationFactory
    {
        public static IInterpolator Create(InterpolationType type)
        {
            switch (type)
            {
                case InterpolationType.Linear:
                    return new LinearInterpolator ();
                default:
                    return null;
            }
        }
    }
}
