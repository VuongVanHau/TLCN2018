using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
 
using Directory = Lucene.Net.Store.Directory;
using Util = Lucene.Net.Util;

namespace MyPham.Lucene
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Product _currentProduct;
        private int _selectedIndex;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<Product> Products { set; get; }
        public bool IsLoaded { private set; get; }
        public bool CanEdit
        {
            get { return IsLoaded && CurrentProduct != null; }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }
        public Product CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                if (_currentProduct != value)
                {
                    _currentProduct = value;
                    OnPropertyChanged("CurrentProduct");
                }
            }
        }
        public void LoadProducts()
        {
            List<Product> products = ProductDao.GetProducts();
            LuceneIndexer.RunIndex(products);

            Products = new ObservableCollection<Product>(ProductDao.GetProducts());

            if (Products.Count > 0)
                CurrentProduct = Products[0];

            IsLoaded = true;
        }
        public static class LuceneIndexer
        {
            private static string indexPath = @"C:\LuceneIndex";

            public static void RunIndex(IList<Product> entities)
            {
                try
                {
                    // delete index directory if it exists
                    if (System.IO.Directory.Exists(indexPath))
                    {
                        System.IO.Directory.Delete(indexPath, true);
                    }

                    Directory directory = FSDirectory.Open(new DirectoryInfo(indexPath));
                    Analyzer analyzer = new StandardAnalyzer(Util.Version.LUCENE_30);

                    using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED))
                    {
                        foreach (var product in entities)
                        {
                            var document = new Document();
                            document.Add(new Field("ProductID", product.ProductID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            document.Add(new Field("Name", product.Name, Field.Store.YES, Field.Index.ANALYZED));
                            document.Add(new Field("Detail", product.Detail, Field.Store.YES, Field.Index.ANALYZED));
                            document.Add(new Field("Price", product.Price.ToString(), Field.Store.YES, Field.Index.ANALYZED));

                            var allBuilder = new StringBuilder();
                            allBuilder.Append(product.Name);
                            allBuilder.Append(" ");
                            allBuilder.Append(product.Detail);
                            allBuilder.Append(" ");
                            allBuilder.Append(product.Price);
                            allBuilder.Append(" ");

                            document.Add(new Field("All", allBuilder.ToString(), Field.Store.YES, Field.Index.ANALYZED));

                            writer.AddDocument(document);
                        }
                        writer.Optimize();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}