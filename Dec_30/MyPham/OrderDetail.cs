//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPham
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public long ProductID { get; set; }
        public long OrderID { get; set; }
        public long ColorID { get; set; }
        public long SizeID { get; set; }
        public Nullable<int> Quanlity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}