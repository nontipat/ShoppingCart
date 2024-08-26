using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    public class CartResponseModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public int? Amount { get; set; }
        public int? Price { get; set; }
    }
}
