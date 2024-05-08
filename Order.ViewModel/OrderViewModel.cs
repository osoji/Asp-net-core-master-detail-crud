using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ViewModel
{
    public class OrderViewModel
    {
        public int? orderMasterId { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public IList<OrderDetailDTO> orderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
