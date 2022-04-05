using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace BO.ViewModels.Brand
{
    public class CreateBrandVM
    {
        [Required]
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool Pulished { get; set; }
        public int? Ordinal { get; set; }
        public List<string> imageName { get; set; }
        public List<IFormFile> formFile { get; set; }
    }
}

