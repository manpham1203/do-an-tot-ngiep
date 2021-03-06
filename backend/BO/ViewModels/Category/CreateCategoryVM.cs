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
        public bool Published { get; set; }
        public IFormFile File { get; set; }
        public string ImageName { get; set; }
    }
}
