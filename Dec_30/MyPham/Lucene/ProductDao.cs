 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPham.Lucene
{
    public static class ProductDao
    {
        public static Product GetProduct(int productId)
        {
            string sql = @"SELECT ProductID, Name, Detail, Price FROM Products";
            var parms = new Dictionary<string, object>();
            parms.Add("@ProductID", productId);
            return AdoDataAccess.Read(sql, MakeDataObject, parms);
        }
        public static List<Product> GetProducts()
        {
            string sql = @"SELECT ProductID, Name, Detail, Price FROM Products";
            return AdoDataAccess.ReadList(sql, MakeDataObject);
        }
        private static readonly Func<IDataReader, Product> MakeDataObject = reader =>
            new Product
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                Name = reader["Name"].ToString().Trim(),
                Detail = reader["Detail"].ToString().Trim(),
                Price = Convert.ToDecimal(reader["Price"])
            };
        private static Dictionary<string, object> CreateParameters(Product product)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ProductID", product.ProductID);
            parameters.Add("@Name", product.Name);
            parameters.Add("@Detail", product.Detail);
            parameters.Add("@Price", product.Price);
            return parameters;
        }
    }
    public static class Extensions
    {
        public static string AsBase64String(this object item)
        {
            if (item == null) return null;
            return Convert.ToBase64String((byte[])item);
        }
        public static byte[] AsByteArray(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            return Convert.FromBase64String(s);
        }
    }
}