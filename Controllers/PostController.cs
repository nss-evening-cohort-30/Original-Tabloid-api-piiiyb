using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Microsoft.EntityFrameworkCore;
using Tabloid.Models;
using Tabloid.Models.Dtos;

namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PostController : ControllerBase
{
  private TabloidDbContext _dbContext;

  public PostController(TabloidDbContext context)
  {
    _dbContext = context;
  }

  [HttpGet]
  // [Authorize]
  public IActionResult Get()
  {
    return Ok(_dbContext.posts.Include(c => c.Category).Select(p => new PostDto
    {
      Id = p.Id,
      Title = p.Title,
      CategoryId = p.CategoryId,
      Category = new CategoryDto
      {
        Id = p.Category.Id,
        Name = p.Category.Name
      },
      PublishedOn = p.PublishedOn,
      RealTime = p.RealTime,
      AuthorId = p.AuthorId,
      Author = new AuthorDto
      {
        Id = p.Author.Id,
        Name = p.Author.Name
      },
      Body = p.Body,
      SubTitle = p.SubTitle
    }).ToList());

  }


}


// public class Post
// {
//   public int Id { get; set; }
//   [Required]
//   public string Title { get; set; }
//   [Required]
//   public int CategoryId { get; set; }
//   public Category Category { get; set; }
//   public DateTime PublishedOn { get; set; }
//   [Required]
//   public int RealTime { get; set; }
//   [Required]
//   public int AuthorId { get; set; }
//   public Author Author { get; set; }
//   [Required]
//   public string Body { get; set; }
//   [Required]
//   public string SubTitle { get; set; }
// }
