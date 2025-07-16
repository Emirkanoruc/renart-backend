namespace RenartApi.Models
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double PopularityOutOf5 { get; set; }
        public ProductImages Images { get; set; }
    }
}
