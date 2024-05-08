using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ViewModel
{
    public class OrderDetailDTO
    {
        public int? orderDetailId { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public decimal amount { get; set; }
        public string formattedAmount 
        { 
            get 
            { 
                return this.amount.ToString("#,##.00"); 
            } 
        }
    }
}
