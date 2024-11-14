using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Database.ModelsDb;

[Table("users")]
public class UserDb
{
    [Column("id")]
    public int Id { get; private set; } 
    
    [Column("login")]
    public string Login { get; set; }
    
    [Column("password")]

    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }
    
    [Column("isAdmin")]
    public bool isAdmin { get; set; }
    
    [Column("pathImage")]
    public string ImagePath { get; set; }
    
    [Column("createAt")]
    public DateTime CreatedAt { get; set; }
}