namespace CafeShop.Models.DTOs
{
    public class GoodReceiptResponeDTO : GoodsReceipt
    {
        public string FullName { get; set; }
        public decimal TotalMoney { get; set; }
    }
}
