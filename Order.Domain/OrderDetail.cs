namespace Order.Domain
{
    public class OrderDetail
    {
        
        public int OrderDetailId { get; set; }

        public int OrderMasterId { get; set; }
        
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public OrderMaster OrderMaster { get; set; }
    }
}
