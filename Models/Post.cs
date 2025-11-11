using System.ComponentModel.DataAnnotations;

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
  public int AuthorId { get; set; }
  public Author Author { get; set; }
  [Required]
  public string Body { get; set; }
  [Required]
  public string SubTitle { get; set; }
}
