using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Category
{
    public class CreateCategoryVM
    {
        [Required]
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool Published { get; set; }
        public int Ordinal { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<string> ImageNames { get; set; }
    }
}
