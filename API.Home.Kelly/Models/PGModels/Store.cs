using System;
using System.Collections.Generic;

namespace API.Home.Kelly.Models.PGModels
{
    public partial class Store
    {
        public int Id { get; set; }
        public int? Item { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Url { get; set; }
    }
}
