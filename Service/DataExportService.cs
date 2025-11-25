using FrameworkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameworkApp.Service
{
    /// <summary>
    /// 데이터 내보내기 서비스
    /// 비즈니스 로직을 담당
    /// </summary>
    public interface IDataExportService
    {
        ExportDataModel GetExportData();
    }

    public class DataExportService : IDataExportService
    {
        /// <summary>
        /// 내보낼 데이터 조회
        /// </summary>
        public ExportDataModel GetExportData()
        {
            // 실제로는 Repository를 통해 DB에서 조회
            // 지금은 데모 데이터 반환
            return GenerateDemoData();
        }

        /// <summary>
        /// 데모 데이터 생성
        /// </summary>
        private ExportDataModel GenerateDemoData()
        {
            var products = new List<ProductData>
            {
                new ProductData
                {
                    ProductID = 1,
                    ProductName = "노트북 A",
                    Category = "전자제품",
                    Price = 1500000,
                    Stock = 25,
                    Status = "재고있음"
                },
                new ProductData
                {
                    ProductID = 2,
                    ProductName = "마우스 B",
                    Category = "전자제품",
                    Price = 35000,
                    Stock = 150,
                    Status = "재고있음"
                },
                new ProductData
                {
                    ProductID = 3,
                    ProductName = "키보드 C",
                    Category = "전자제품",
                    Price = 89000,
                    Stock = 0,
                    Status = "품절"
                },
                new ProductData
                {
                    ProductID = 4,
                    ProductName = "모니터 D",
                    Category = "전자제품",
                    Price = 450000,
                    Stock = 10,
                    Status = "재고있음"
                },
                new ProductData
                {
                    ProductID = 5,
                    ProductName = "헤드셋 E",
                    Category = "전자제품",
                    Price = 120000,
                    Stock = 45,
                    Status = "재고있음"
                }
            };

            var orders = new List<OrderData>
            {
                new OrderData
                {
                    OrderId = 1001,
                    OrderDate = "2024-11-20",
                    CustomerName = "김철수",
                    ProductName = "노트북 A",
                    Quantity = 2,
                    TotalAmount = 3000000,
                    ShippingStatus = "배송완료"
                },
                new OrderData
                {
                    OrderId = 1002,
                    OrderDate = "2024-11-22",
                    CustomerName = "이영희",
                    ProductName = "마우스 B",
                    Quantity = 5,
                    TotalAmount = 175000,
                    ShippingStatus = "배송중"
                },
                new OrderData
                {
                    OrderId = 1003,
                    OrderDate = "2024-11-23",
                    CustomerName = "박민수",
                    ProductName = "모니터 D",
                    Quantity = 1,
                    TotalAmount = 450000,
                    ShippingStatus = "준비중"
                },
                new OrderData
                {
                    OrderId = 1004,
                    OrderDate = "2024-11-24",
                    CustomerName = "최지원",
                    ProductName = "헤드셋 E",
                    Quantity = 3,
                    TotalAmount = 360000,
                    ShippingStatus = "배송완료"
                }
            };

            var summary = new SummaryData
            {
                TotalProducts = products.Count,
                TotalOrders = orders.Count,
                TotalRevenue = 3985000,
                PendingOrders = 2
            };
            return new ExportDataModel
            {
                ExportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ExportdBy = "Admin User",
                Product = products,
                Order = orders,
                Summary = summary
            };

        }
    }
}