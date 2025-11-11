using System.ComponentModel.DataAnnotations;

public class CategoryDto
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public List<PostDto> Post { get; set; }
}
