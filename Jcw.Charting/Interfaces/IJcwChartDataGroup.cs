using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Charting.Interfaces
{
    public interface IJcwChartDataGroup<T> 
        where T : struct
    {
        IJcwChartVectorGroup<T> LabelData { get; }
        List<IJcwChartVectorGroup<T>> SeriesData { get; }
    }

    public class JcwChartDataGroup<T> : IJcwChartDataGroup<T> 
        where T : struct
    {
        public IJcwChartVectorGroup<T> LabelData { get; private set; }
        public List<IJcwChartVectorGroup<T>> SeriesData { get; private set; }

        public JcwChartDataGroup (IJcwChartVectorGroup<T> labelData)
        {
            LabelData = labelData;
            SeriesData = new List<IJcwChartVectorGroup<T>> ();
        }
    }
}
