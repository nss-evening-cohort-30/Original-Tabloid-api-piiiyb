using System.ComponentModel.DataAnnotations;

public class SubAuthorDto
{
  public int Id { get; set; }
  [Required]
  public int UserId { get; set; }
  [Required]
  public int AuthorId { get; set; }
  public AuthorDto Author { get; set; }
}
