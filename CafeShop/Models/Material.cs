using System;
using System.Collections.Generic;

namespace CafeShop.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int? UnitId { get; set; }
        public string? MaterialCode { get; set; }
        public byte[]? MaterialName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? MinQuantity { get; set; }
        public byte[]? Decription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}
