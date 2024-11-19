using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("users")]
public class UserDb
{
    [Column("id")]
    public Guid Id { get; private set; } 
    
    [Column("login")]
    public string Login { get; set; }
    
    [Column("password")]

    public string Password { get; set; }

    [Column("email")]
    public string Email { get; set; }
    
    [Column("role")]
    public int Role { get; set; }
    
    [Column("pathImage")]
    public string ImagePath { get; set; }
    
    [Column("createdAt")]
    public DateTime CreatedAt { get; set; }
}