using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        [Required]
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool Pulished { get; set; }
        public bool Deleted { get; set; }
        public int Ordinal { get; set; }
        public List<string> ImageNames { get; set; }
        public List<IFormFile> Files { get; set; }
        public UpdateCategoryVM()
        {
            ImageNames = new List<string>();
            Files = new List<IFormFile>();
        }
    }
}
