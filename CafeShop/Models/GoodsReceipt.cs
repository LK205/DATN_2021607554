using System;
using System.Collections.Generic;

namespace CafeShop.Models
{
    public partial class GoodsReceipt
    {
        public int Id { get; set; }
        public int? AccoutId { get; set; }
        public string? GoodsReceiptCode { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierName { get; set; }
        public string? Decription { get; set; }
        public DateTime? ReceiptedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}
