using System.ComponentModel.DataAnnotations;

namespace Order.Domain
{
    public class OrderMaster
    {
       
        public int OrderMasterId { get; set; }

       
        public string CustomerName { get; set; } = default!;

        
        public string Address { get; set; } = default!;

      
        public string PhoneNumber { get; set; } = default!;

        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

        
    }
}
