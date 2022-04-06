﻿using BO.ViewModels.CategoryImage;
using BO.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Category
{
    public class CategoryFullVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool Pulished { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Ordinal { get; set; }
        public List<ProductVM> ProductVMs { get; set; }
        public List<CategoryImageVM> CategoryImageVMs { get; set; }

        public CategoryFullVM()
        {
            ProductVMs = new List<ProductVM>();
            CategoryImageVMs = new List<CategoryImageVM>();
        }
    }
}