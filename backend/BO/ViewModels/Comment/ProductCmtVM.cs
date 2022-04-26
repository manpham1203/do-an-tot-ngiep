﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Comment
{
    public class ProductCmtVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string OrderDetailId { get; set; }
        public int? Star { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}