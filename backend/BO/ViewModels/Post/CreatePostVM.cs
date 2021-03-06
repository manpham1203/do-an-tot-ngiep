using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Post
{
    public class CreatePostVM
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ImageName { get; set; }
        public bool Published { get; set; }
        public IFormFile File { get; set; }
    }
}
