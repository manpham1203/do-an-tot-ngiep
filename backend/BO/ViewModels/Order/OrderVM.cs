using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Order
{
    public class OrderVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public float? Discount { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
