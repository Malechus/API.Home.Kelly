using System;
using System.Collections.Generic;

namespace API.Home.Kelly.Models.PGModels
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? Item { get; set; }
        public int? Thumbsup { get; set; }
        public int? Thumbsdown { get; set; }
    }
}
