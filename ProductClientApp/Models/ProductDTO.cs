using System.Text.Json.Serialization;

namespace ProductClientApp.Models
{
    public class ProductDTO
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}