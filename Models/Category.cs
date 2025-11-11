
using System.ComponentModel.DataAnnotations;

public class Category
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public List<Post> Post { get; set; }
}
