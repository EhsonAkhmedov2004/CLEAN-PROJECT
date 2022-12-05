
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Domain.Entities.Product;

public class ProductModel
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }
    [Required]
    public string Title { get; set; }

    public string Color { get; set; } = "white";
    [Required]
    public int Cost { get; set; }

    [JsonIgnore]
    public UserModel? OwnerShip { get; set; } = null;
   

}

