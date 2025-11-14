using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models.Dtos;

public class PostDto
{
  public int Id { get; set; }
  [Required]
  public string Title { get; set; }
  [Required]
  public int CategoryId { get; set; }
  public CategoryDto Category { get; set; }
  [Required]
  public DateTime PublishedOn { get; set; }
  [Required]
  public int RealTime { get; set; }
  [Required]
  public int AuthorId { get; set; }
  public AuthorDto Author { get; set; }
  [Required]
  public string Body { get; set; }
  [Required]
  public string SubTitle { get; set; }
}
