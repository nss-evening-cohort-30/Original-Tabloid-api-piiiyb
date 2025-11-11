using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

public class PostComment
{
  public int Id { get; set; }
  [Required]
  public int PostId { get; set; }
  [Required]
  public int UserId { get; set; }
  [Required]
  public string Comment { get; set; }
  [Required]
  public DateTime PostedOne { get; set; }
}
