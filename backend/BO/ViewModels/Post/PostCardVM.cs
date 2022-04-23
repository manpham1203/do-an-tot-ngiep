﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Post
{
    public class PostCardVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public int Views { get; set; }
        public string Image { get; set; }
        public string ImageSrc { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
