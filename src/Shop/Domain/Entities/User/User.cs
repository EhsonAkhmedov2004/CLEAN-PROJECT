using System.ComponentModel.DataAnnotations;
namespace Domain.Entities.User;
public class UserModel
{

    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public byte[] Password { get; set; }
    [Required]
    public byte[] SaltPassword { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Balance { get; set; }
    public ICollection<ProductModel> Cart { get; set; } = new List<ProductModel>();




}