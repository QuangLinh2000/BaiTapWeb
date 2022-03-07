using System;
using System.Collections.Generic;

namespace BaiTapWeb.Models
{
    public partial class VProduct
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string NameCategory { get; set; } = null!;
    }
}
