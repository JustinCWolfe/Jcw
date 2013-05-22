using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common
{
    public class VectorAggregate<T> : IVectorAggregate<T>
    {
        public string Name { get; set; }

        public double AveValue { get; set; }
        public T MaxValue { get; set; }
        public T MinValue { get; set; }
    }
}