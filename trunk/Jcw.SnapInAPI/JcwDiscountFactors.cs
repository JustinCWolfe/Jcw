using System;
using System.Collections.Generic;
using System.Text;

using Jcw.SnapInAPI.Interfaces;

namespace Jcw.SnapInAPI
{
    public sealed class JcwDiscountFactors : ICalculateDiscountFactors
    {
        void ICalculateDiscountFactors.CalculateDiscountFactors ()
        {
            Console.WriteLine ("I'm calculating discount factors in the Jcw way");
        }
    }
}
