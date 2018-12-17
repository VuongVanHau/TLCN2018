using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Directory = Lucene.Net.Store.Directory;
using Util = Lucene.Net.Util;

namespace MyPham.Lucene
{
    public static class LuceneSearcher
    {
        private static string indexPath = @"C:\LuceneIndex";

        public static List<Product> Search(string criterion, string term)
        {
            List<Product> products = new List<Product>();
            try
            {
                Directory directory = FSDirectory.Open(new DirectoryInfo(indexPath));
                Analyzer analyzer = new StandardAnalyzer(Util.Version.LUCENE_30);

                IndexReader indexReader = IndexReader.Open(directory, true);
                Searcher searcher = new IndexSearcher(indexReader);

                var queryParser = new QueryParser(Util.Version.LUCENE_30, criterion, analyzer);
                var query = queryParser.Parse(term);

                TopDocs resultDocs = searcher.Search(query, indexReader.MaxDoc);

                var topDocs = resultDocs.ScoreDocs;
                
                foreach (var hit in topDocs)
                {
                    var documentFromSearch = searcher.Doc(hit.Doc);
                    products.Add(new Product
                    {
                        ProductID = Convert.ToInt32(documentFromSearch.Get("ProductID")),
                        Name = documentFromSearch.Get("Name"),
                        Detail = documentFromSearch.Get("Detail"),
                        Price = Convert.ToDecimal(documentFromSearch.Get("Price"))
                    });
                }
                return products;
            }
            catch (Exception ex)
            {
                return products;
            }
            
            
        }
    }
}