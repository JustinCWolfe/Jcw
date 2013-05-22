using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jcw.SnapInAPI.Interfaces;

namespace ThirdParty.Jcw.SnapIn
{
    public class DiscountFactors : ICalculateDiscountFactors
    {
        void ICalculateDiscountFactors.CalculateDiscountFactors ()
        {
            Console.WriteLine ("I'm calculating discount factors in the ThirdParty way");
        }
    }
}
