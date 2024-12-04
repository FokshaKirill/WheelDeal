namespace WheelDeal.Domain.Database.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Rate> Rates { get; set; }

    public User()
    {
        CreatedAt = DateTime.Now.ToUniversalTime();
        Role = 1;
        ImagePath = @"G:\Study\GitHub\Practica November-December\WheelDeal\wwwroot\images\avatars\default.png";
    }
}
