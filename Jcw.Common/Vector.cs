using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common
{
    public class Vector<T> : IVector<T>
    {
        public List<IVectorAggregate<T>> Aggregates { get; private set; }
        public string Name { get; private set; }
        public List<T> Data { get; private set; }

        public Vector (string name)
        {
            Aggregates = new List<IVectorAggregate<T>> ();
            Data = new List<T> ();
            Name = name;
        }
    }
}
