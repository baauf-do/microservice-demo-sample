namespace Shared.Configurations
{
    public class JwtSettings
    {
        /// <summary>
        /// key using
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// key for Product.API
        /// </summary>
        public string ProductKey { get; set; } = string.Empty;

        /// <summary>
        /// key for Basket.API
        /// </summary>
        public string BasketKey { get; set; } = string.Empty;
        /// <summary>
        /// key for Inventory.API
        /// </summary>
        public string InventoryKey { get; set; } = string.Empty;
        /// <summary>
        /// key for Customer.API
        /// </summary>
        public string CustomerKey { get; set; } = string.Empty;
        /// <summary>
        /// key for Ordering.API
        /// </summary>
        public string OrderingKey { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        //public bool Issuer { get; set; } = false;
        //public bool ExpirationTime { get; set; } = false;
    }
}
