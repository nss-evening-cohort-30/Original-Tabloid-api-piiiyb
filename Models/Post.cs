using System.ComponentModel.DataAnnotations;
namespace Tabloid.Models;

public class Post
{
  public int Id { get; set; }
  [Required]
  public string Title { get; set; }
  [Required]
  public int CategoryId { get; set; }
  public Category Category { get; set; }
  public DateTime PublishedOn { get; set; }
  [Required]
  public int RealTime { get; set; }
  [Required]
  public int UserId { get; set; }
  public UserProfile User { get; set; }
  [Required]
  public string Body { get; set; }
  [Required]
  public string SubTitle { get; set; }
}
