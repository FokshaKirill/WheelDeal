namespace WheelDeal.Entities;

public class User
{
    public int Id { get; private set; } 
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool isAdmin { get; set; }
    public string ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
}