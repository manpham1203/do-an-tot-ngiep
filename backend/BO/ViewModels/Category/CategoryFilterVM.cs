using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Category
{
    public class CategoryFilterVM
    {
        public string Search { get; set; }
        public string ShortBy { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
