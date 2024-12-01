namespace CafeShop.Config
{
    public static class Config
    {
        private const string strUrlServer = @"https://localhost:7116/";
        public static string Connection()
        {
            string conn = @"Data Source=LMK205\SQLEXPRESS;Initial Catalog=CafeShop;User ID=sa;Password=Leminhkhoi2003;Trust Server Certificate=True";
            return conn;
        }

        public static string ImageUrl()
        {
            string imageUrl = strUrlServer;
            return imageUrl;
        }

        public static string ProductImageUrl()
        {
            string imageUrl = $"{strUrlServer}Images/Product/";
            return imageUrl;
        }

    }
}
