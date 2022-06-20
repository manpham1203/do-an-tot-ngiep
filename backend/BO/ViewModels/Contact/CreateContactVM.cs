using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Contact
{
    public class CreateContactVM
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public bool Published { get; set; }
    }
}
