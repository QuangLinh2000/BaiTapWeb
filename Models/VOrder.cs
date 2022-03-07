using System;
using System.Collections.Generic;

namespace BaiTapWeb.Models
{
    public partial class VOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Name { get; set; } = null!;
    }
}
