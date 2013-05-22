using System;
using System.Collections.Generic;
using System.Text;

using LuceneQueryParsers = Lucene.Net.QueryParsers;

namespace Jcw.Search
{
    public class DatabaseSearcher : SearcherBase
    {
        #region Overrides

        protected override LuceneQueryParsers.QueryParser CreateQueryParser ()
        {
            return new LuceneQueryParsers.MultiFieldQueryParser (
                new string[] { 
                    DatabaseDocumentCreator.SearchableFields.AttrCode.ToString(),
                    DatabaseDocumentCreator.SearchableFields.AttrText.ToString(),
                    DatabaseDocumentCreator.SearchableFields.CategCode.ToString(),
                    DatabaseDocumentCreator.SearchableFields.CategText.ToString(),
                    DatabaseDocumentCreator.SearchableFields.TableName.ToString(),
                },
                IndexAnalyzer );
        }

        #endregion
    }
}
