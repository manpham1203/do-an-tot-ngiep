using BO.ViewModels.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Order
{
    public class CheckoutVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public string DeliveryPhone { get; set; }
        [Required]
        public string DeliveryEmail { get; set; }
        public List<OrderDetailVM> OrderDetailVMs { get; set; }
    }
}
