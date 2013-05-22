using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface IHeirarchicalDataManager
    {
        IHeirarchical DataSource { get; set; }
    }

    public interface IHeirarchical
    {
        Dictionary<string, object> CellData { get; }
        List<IHeirarchical> Children { get; }
        IHeirarchical Parent { get; }
    }
}