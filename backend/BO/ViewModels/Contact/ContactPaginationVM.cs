﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Contact
{
    public class ContactPaginationVM
    {
        public int TotalResult { get;set; }
        public int TotalPage { get; set; }
        public List<ContactVM> ContactVMs { get; set; }
    }
}