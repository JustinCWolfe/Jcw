using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface IVector<T>
    {
        List<IVectorAggregate<T>> Aggregates { get; }
        string Name { get; }
        List<T> Data { get; }
    }
}
