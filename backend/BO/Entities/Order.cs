using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal? Amount { get; set; }
        public int State { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
        public DateTime? ReceivedAt { get; set; }
    }
}
