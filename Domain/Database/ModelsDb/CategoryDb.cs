using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Domain.Database.ModelsDb;

[Table("categories")]
public class CategoryDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("imagepath")]
    public string ImagePath { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("countposts")]
    public int CountPosts { get; set; }

    public ICollection<PostDb> Posts { get; set; }
}
