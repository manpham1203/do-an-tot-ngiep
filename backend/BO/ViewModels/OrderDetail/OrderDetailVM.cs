using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.OrderDetail
{
    public class OrderDetailVM
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
