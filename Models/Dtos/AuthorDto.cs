using System.ComponentModel.DataAnnotations;

public class AuthorDto
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string ProfilePic { get; set; }
}
