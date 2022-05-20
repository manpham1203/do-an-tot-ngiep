using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Order
{
    public class CompareOrderChartVM
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public List<OrderChartVM> Data { get; set; }
    }
}
