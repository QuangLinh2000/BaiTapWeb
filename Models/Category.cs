﻿using System;
using System.Collections.Generic;

namespace BaiTapWeb.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
