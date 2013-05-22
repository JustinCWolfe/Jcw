using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Search.Interfaces
{
    public interface IIndexedItems<T>
    {
        HashSet<T> Items { get; set; }
    }
}
