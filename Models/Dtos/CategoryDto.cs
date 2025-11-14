using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models.Dtos;

public class CategoryDto
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public List<PostDto> Post { get; set; }
}
