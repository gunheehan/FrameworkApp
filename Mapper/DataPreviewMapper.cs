using FrameworkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameworkApp.Mapper
{
    /// <summary>
    /// 데이터 미리보기용 Mapper
    /// Model 데이터를 동적 렌더링 가능한 ViewModel로 변환
    /// </summary>
    public class DataPreviewMapper
    {
        /// <summary>
        /// ExportDataModel을 DataPreviewViewModel으로 변환
        /// </summary>
        public static DataPreviewViewModel Map(ExportDataModel model)
        {
            var viewModel = new DataPreviewViewModel
            {
                BasicInfo = MapBasicInfo(model),
                Sections = new List<DataSection>()
            };

            // 요약 섹션 추가
            if (model.Summary != null)
            {
                viewModel.Sections.Add(MapSummarySection(model.Summary));
            }

            // 제품 섹션 추가
            if (model.Product != null && model.Product.Any())                                                                                                                                                                           
            {
                viewModel.Sections.Add(MapProductsSection(model.Product));
            }

            // 주문 섹션 추가
            if (model.Order != null && model.Order.Any())
            {
                viewModel.Sections.Add(MapOrdersSection(model.Order));
            }

            return viewModel;
        }

        /// <summary>
        /// 기본 정보 매핑
        /// </summary>
        private static Dictionary<string, string> MapBasicInfo(ExportDataModel model)
        {
            return new Dictionary<string, string>
            {
                { "내보내기 날짜", model.ExportDate },
                { "담당자", model.ExportdBy }
            };
        }

        /// <summary>
        /// 요약 섹션 매핑
        /// </summary>
        private static DataSection MapSummarySection(SummaryData summary)
        {
            return new DataSection
            {
                SectionId = "summary",
                SectionName = "요약",
                Type = SectionType.Cards,
                Rows = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "label", "전체 제품" },
                        { "value", summary.TotalProducts },
                        { "type", "primary" }
                    },
                    new Dictionary<string, object>
                    {
                        { "label", "전체 주문" },
                        { "value", summary.TotalOrders },
                        { "type", "success" }
                    },
                    new Dictionary<string, object>
                    {
                        { "label", "총 매출" },
                        { "value", summary.TotalRevenue },
                        { "type", "info" },
                        { "format", "currency" }
                    },
                    new Dictionary<string, object>
                    {
                        { "label", "대기 주문" },
                        { "value", summary.PendingOrders },
                        { "type", "warning" }
                    }
                }
            };
        }

        /// <summary>
        /// 제품 섹션 매핑
        /// </summary>
        private static DataSection MapProductsSection(List<ProductData> products)
        {
            return new DataSection
            {
                SectionId = "products",
                SectionName = "제품 목록",
                Type = SectionType.Table,
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition
                    {
                        Key = "ProductId",
                        DisplayName = "제품ID",
                        Type = ColumnType.Number,
                        Align = "center"
                    },
                    new ColumnDefinition
                    {
                        Key = "ProductName",
                        DisplayName = "제품명",
                        Type = ColumnType.Text,
                        Align = "left"
                    },
                    new ColumnDefinition
                    {
                        Key = "Category",
                        DisplayName = "카테고리",
                        Type = ColumnType.Text,
                        Align = "left"
                    },
                    new ColumnDefinition
                    {
                        Key = "Price",
                        DisplayName = "가격",
                        Type = ColumnType.Currency,
                        Align = "right",
                        Format = "currency"
                    },
                    new ColumnDefinition
                    {
                        Key = "Stock",
                        DisplayName = "재고",
                        Type = ColumnType.Number,
                        Align = "center"
                    },
                    new ColumnDefinition
                    {
                        Key = "Status",
                        DisplayName = "상태",
                        Type = ColumnType.Status,
                        Align = "center",
                        StyleRules = new List<StyleRule>
                        {
                            new StyleRule { Condition = "equals", Value = "재고있음", CssClass = "success" },
                            new StyleRule { Condition = "equals", Value = "품절", CssClass = "danger" }
                        }
                    }
                },
                Rows = products.Select(p => new Dictionary<string, object>
                {
                    { "ProductId", p.ProductID },
                    { "ProductName", p.ProductName },
                    { "Category", p.Category },
                    { "Price", p.Price },
                    { "Stock", p.Stock },
                    { "Status", p.Status }
                }).ToList()
            };
        }

        /// <summary>
        /// 주문 섹션 매핑
        /// </summary>
        private static DataSection MapOrdersSection(List<OrderData> orders)
        {
            return new DataSection
            {
                SectionId = "orders",
                SectionName = "주문 목록",
                Type = SectionType.Table,
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition
                    {
                        Key = "OrderId",
                        DisplayName = "주문ID",
                        Type = ColumnType.Number,
                        Align = "center"
                    },
                    new ColumnDefinition
                    {
                        Key = "OrderDate",
                        DisplayName = "주문일",
                        Type = ColumnType.Date,
                        Align = "center"
                    },
                    new ColumnDefinition
                    {
                        Key = "CustomerName",
                        DisplayName = "고객명",
                        Type = ColumnType.Text,
                        Align = "left"
                    },
                    new ColumnDefinition
                    {
                        Key = "ProductName",
                        DisplayName = "제품명",
                        Type = ColumnType.Text,
                        Align = "left"
                    },
                    new ColumnDefinition
                    {
                        Key = "Quantity",
                        DisplayName = "수량",
                        Type = ColumnType.Number,
                        Align = "center"
                    },
                    new ColumnDefinition
                    {
                        Key = "TotalAmount",
                        DisplayName = "금액",
                        Type = ColumnType.Currency,
                        Align = "right",
                        Format = "currency"
                    },
                    new ColumnDefinition
                    {
                        Key = "ShippingStatus",
                        DisplayName = "배송상태",
                        Type = ColumnType.Status,
                        Align = "center",
                        StyleRules = new List<StyleRule>
                        {
                            new StyleRule { Condition = "equals", Value = "배송완료", CssClass = "success" },
                            new StyleRule { Condition = "equals", Value = "배송중", CssClass = "info" },
                            new StyleRule { Condition = "equals", Value = "준비중", CssClass = "warning" }
                        }
                    }
                },
                Rows = orders.Select(o => new Dictionary<string, object>
                {
                    { "OrderId", o.OrderId },
                    { "OrderDate", o.OrderDate },
                    { "CustomerName", o.CustomerName },
                    { "ProductName", o.ProductName },
                    { "Quantity", o.Quantity },
                    { "TotalAmount", o.TotalAmount },
                    { "ShippingStatus", o.ShippingStatus }
                }).ToList()
            };
        }
    }
}