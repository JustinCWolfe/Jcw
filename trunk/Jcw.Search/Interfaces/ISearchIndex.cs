using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jcw.Search.Interfaces
{
    public interface IIndexQueryDefinition
    {
        string QueryText { get; set; }
        int TopDocsToFind { get; set; }
    }

    public interface ISearchIndex<T>
    {
        TimeSpan IndexSearchTime { get; }
        IIndexQueryDefinition IndexQueryDefinition { get; set; }
        T QueryHits { get; }

        void RunSearch ();
    }
}
