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
        public bool Published { get; set; }
        public string ImageName { get; set; }
        public IFormFile File { get; set; }
    }
}

