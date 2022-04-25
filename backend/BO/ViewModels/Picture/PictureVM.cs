using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Picture
{
    public class PictureVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public bool Published { get; set; }
    }
}
