namespace API.Home.Kelly.Models.UPCDModels
{
    public class UPCDReturn
    {
        public bool Success { get; set; }
        public int Barcode { get; set; }

        public string? Title { get; set; }

        public string? Alias { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public string? Manufacturer { get; set; }

        public double? MPN { get; set; }

        public double? MSRP { get; set; }

        public string? ASIN { get; set; }

        public string? Category { get; set; }

        public List<UPCDMetadata>? Metadata { get; set; }

        public List<UPCDStore>? Stores { get; set; }

        public List<string>? Images { get; set; }

        public List<UPCDReview>? Reviews { get; set; }

        public UPCDError? Error { get; set; }
    }
}
