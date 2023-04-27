namespace IrishNFT.Models
{
    public class CartItem
    {
        public int ItemId { get; set; }

        public int CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Username { get; set; }
    }
}
