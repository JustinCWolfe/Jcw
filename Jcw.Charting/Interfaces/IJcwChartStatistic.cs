using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Charting.Interfaces
{
    public enum StatisticTypes
    {
        Minimum,
        Maximum,
        Average,
        Length,
    }

    public interface IJcwChartStatistic
    {
        string Name { get; set; }
        double Value { get; set; }
        bool CanIncludeInAggregate { get; set; }
        bool IncludeInAggregate { get; set; }
    }
}
