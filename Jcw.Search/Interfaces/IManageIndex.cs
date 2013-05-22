using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jcw.Search.Interfaces
{
    public interface IManageIndex<T, U>
    {
        bool InDebug { get; }
        int IndexItemCount { get; }
        TimeSpan IndexLoadTime { get; }

        T ItemsToIndex { get; }
        U DocumentCreator { get; }

        void RunLoadIndex ();
        void RunUpdateIndex ();
    }

    public interface ISearchIndexAdapter<T, U>
    {
        T GetAnalyzer ();
        U GetIndexSearcher ();
    }

    public interface IGetDocument<T, U>
    {
        U GetDocument (T elementToIndex);
    }
}
