using System;


namespace PushNotificationServer.Models
{
    public class Shopitem : FullAuditedEntity
    {

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ShoplistId { get; set; }
        public Shoplist Shoplist { get; set; }
        
        public Shopitem()
        {
            Quantity=0;
            Price = 0.0M;
            Description = "";
        }

    }
}