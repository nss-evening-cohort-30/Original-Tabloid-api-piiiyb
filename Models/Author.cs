using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models;

public class Author
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string ProfilePic { get; set; }
}
