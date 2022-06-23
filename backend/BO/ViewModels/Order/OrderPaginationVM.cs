using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Order
{
    public class OrderPaginationVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<OrderVM> OrderVMs { get; set; }
    }
}
