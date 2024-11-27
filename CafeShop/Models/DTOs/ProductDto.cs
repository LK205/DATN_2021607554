namespace CafeShop.Models.DTOs
{
    public class ProductDto : Product
    {
        public string? ImageUrl { get; set; }
        public string? ProductTypeName { get; set; }
        public decimal Price{ get; set; }
        public string? FormatPrice{ get; set; }
        public int TotalSales { get; set; }
        public List<ProductDetail>? ListDetails { get; set; }
        public List<int> ListFileIDs { get; set; }
    }
}
