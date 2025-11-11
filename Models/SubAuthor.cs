using System.ComponentModel.DataAnnotations;

public class SubAuthor
{
  public int Id { get; set; }
  [Required]
  public int UserId { get; set; }
  [Required]
  public int AuthorId { get; set; }
  public Author Author {  get; set; }
}
