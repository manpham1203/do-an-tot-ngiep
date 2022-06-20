using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Banner
{
    public class BannerPaginationVM
    {
        public int TotalResult { get; set; }
        public int TotalPage { get; set; }
        public List<string> BannerId { get; set; }
    }
}
