using System;
using System.Collections.Generic;
using System.Text;

using LuceneQueryParsers = Lucene.Net.QueryParsers;
using LuceneUtil = Lucene.Net.Util;

namespace Jcw.Search
{
    public class FileSystemSearcher : SearcherBase
    {
        #region Overrides

        protected override LuceneQueryParsers.QueryParser CreateQueryParser()
        {
            return new LuceneQueryParsers.MultiFieldQueryParser (
                LuceneUtil.Version.LUCENE_30,
                new string[] { 
                    FileSystemDocumentCreator.SearchableFields.Contents.ToString(),
                    FileSystemDocumentCreator.SearchableFields.CreationTimeUtc.ToString(),
                    FileSystemDocumentCreator.SearchableFields.DirectoryName.ToString(),
                    FileSystemDocumentCreator.SearchableFields.Extension.ToString(),
                    FileSystemDocumentCreator.SearchableFields.FullName.ToString(),
                    FileSystemDocumentCreator.SearchableFields.LastWriteTimeUtc.ToString(),
                    FileSystemDocumentCreator.SearchableFields.Length.ToString(),
                    FileSystemDocumentCreator.SearchableFields.Name.ToString(),
                },
                IndexAnalyzer);
        }

        #endregion
    }
}
