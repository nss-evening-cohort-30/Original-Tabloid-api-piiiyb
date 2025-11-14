using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models.Dtos;

public class AuthorDto
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string ProfilePic { get; set; }
}
