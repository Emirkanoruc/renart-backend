namespace RenartApi.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public double PopularityScore { get; set; }
        public double Weight { get; set; }
        public ProductImages Images { get; set; }
    }
}
