using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace FrameworkApp.Models
{
    public class ExportDataModel
    {
        public string ExportDate { get; set; }
        public string ExportdBy { get; set; }
        public List<ProductData> Product { get; set; }
        public List<OrderData> Order { get; set; }
        public SummaryData Summary { get; set; }
    }

    public class ProductData
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Status { get; set; }
    }

    public class OrderData
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingStatus { get; set; }
    }

    public class SummaryData
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingOrders { get; set; }
    }
}