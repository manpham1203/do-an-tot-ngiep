using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Order
{
    public class OrderChartVM
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public Decimal? Value { get; set; }
    }
}
