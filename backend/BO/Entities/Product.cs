﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public int QuantityInStock { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string BrandId { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Brand Brand { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<Wishlist> Wishlists { get; set; }
    }
}
