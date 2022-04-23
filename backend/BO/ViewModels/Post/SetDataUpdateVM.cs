using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Post
{
    public class SetDataUpdateVM
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool Published { get; set; }
        public string Image { get; set; }
    }
}
