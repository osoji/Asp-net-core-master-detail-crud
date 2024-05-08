using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ViewModel
{
    public class OrderMasterDTO
    {
        public int? orderMasterId { get; set; }
        public string customerName { get; set; } = default!;
        public string address { get; set; } = default!;
        public DateTime orderDate { get; set; }
        public string phoneNumber { get; set; } = default!;
        public string formmatedOrderDate
        {
            get
            {
                return this.orderDate.ToString("dd/MM/yyyy");
            }
        }
    }
}
