using System;
using System.Collections.Generic;
using System.Text;

using Jcw.SnapInAPI.Interfaces;

namespace Jcw.SnapInAPI
{
    public sealed class JcwRate : ICalculateRate
    {
        void ICalculateRate.CalculateRate ()
        {
            Console.WriteLine ("I'm calculating the rate in the Jcw way");
        }
    }
}
