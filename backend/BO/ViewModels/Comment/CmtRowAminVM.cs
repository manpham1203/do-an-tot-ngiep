using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Comment
{
    public class CmtRowAminVM
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
        public int? Star { get; set; }
        public bool Published { get; set; }
    }
}
