using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models.Dtos;

public class SubAuthorDto
{
  public int Id { get; set; }
  [Required]
  public int UserId { get; set; }
  [Required]
  public int AuthorId { get; set; }
  public UserProfileDto Author { get; set; }
}
