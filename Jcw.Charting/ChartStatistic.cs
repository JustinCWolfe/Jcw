using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting
{
    public class ChartStatistic : IJcwChartStatistic
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public bool CanIncludeInAggregate { get; set; }
        public bool IncludeInAggregate { get; set; }
    }
}
