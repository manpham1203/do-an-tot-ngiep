using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Banner
{
    public class CreateBannerVM
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string SubContent { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile File { get; set; }
        public string ImageName { get; set; }
    }
}
