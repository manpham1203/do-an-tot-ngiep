using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.ViewModels.Brand
{
    public class UpdateBrandVM
    {
        [Required]
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int? Ordinal { get; set; }
    }
}
