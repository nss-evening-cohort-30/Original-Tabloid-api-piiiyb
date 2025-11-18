using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models;

public class SubAuthor
{
  public int Id { get; set; }
  [Required]
  public int UserId { get; set; }
  [Required]
  public int AuthorId { get; set; }
  public UserProfile Author {  get; set; }
}
