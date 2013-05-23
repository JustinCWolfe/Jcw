using System;
using System.Collections.Generic;
using System.Text;

using LuceneQueryParsers = Lucene.Net.QueryParsers;
using LuceneUtil = Lucene.Net.Util;

namespace Jcw.Search
{
    public class DatabaseSearcher : SearcherBase
    {
        #region Overrides

        protected override LuceneQueryParsers.QueryParser CreateQueryParser()
        {
            return new LuceneQueryParsers.MultiFieldQueryParser (
                LuceneUtil.Version.LUCENE_30,
                new string[] { 
                    DatabaseDocumentCreator.SearchableFields.AttrCode.ToString(),
                    DatabaseDocumentCreator.SearchableFields.AttrText.ToString(),
                    DatabaseDocumentCreator.SearchableFields.CategCode.ToString(),
                    DatabaseDocumentCreator.SearchableFields.CategText.ToString(),
                    DatabaseDocumentCreator.SearchableFields.TableName.ToString(),
                },
                IndexAnalyzer);
        }

        #endregion
    }
}
