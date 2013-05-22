using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface IVectorAggregate<T>
    {
        string Name { get; set; }

        double AveValue { get; set; }
        T MaxValue { get; set; }
        T MinValue { get; set; }
    }
}
