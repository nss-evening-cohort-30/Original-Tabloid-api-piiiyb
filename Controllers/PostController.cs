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
  [Authorize]
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



  [HttpGet("{id}")]
  [Authorize]

  public IActionResult GetById(int id)
  {

    Post post = _dbContext.posts.Include(c => c.Category).Include(a => a.Author).SingleOrDefault(s => s.Id == id);

    if (post == null)
    {
      return NotFound();
    }
    return Ok(new PostDto
    {
      Id = post.Id,
      Title = post.Title,
      CategoryId = post.CategoryId,
      Category = new CategoryDto
      {
        Id = post.Category.Id,
        Name = post.Category.Name
      },
      PublishedOn = post.PublishedOn,
      RealTime = post.RealTime,
      AuthorId = post.AuthorId,
      Author = new AuthorDto
      {
        Id = post.Author.Id,
        Name = post.Author.Name
      },
      Body = post.Body,
      SubTitle = post.SubTitle
    });
  }


[HttpPut("{id}")]
[Authorize]
public IActionResult UpdatePost(Post post, int id)
  {
      Post updatedPost = _dbContext.posts.SingleOrDefault(up => up.Id == id);
      if (updatedPost == null)
    {
      return NotFound();
    }

    updatedPost.Title = post.Title;
    updatedPost.CategoryId = post.CategoryId;
    updatedPost.PublishedOn = post.PublishedOn;
    updatedPost.RealTime = post.RealTime;
    updatedPost.AuthorId = post.AuthorId;
    updatedPost.Body = post.Body;
    updatedPost.SubTitle = post.SubTitle;

    _dbContext.SaveChanges();
    return NoContent();
  }

[HttpPost]
[Authorize]
public IActionResult NewPost(Post post)
  {
    _dbContext.posts.Add(post);
    _dbContext.SaveChanges();
    return Created($"/api/post/{post.Id}", post);
  }

[HttpDelete("{id}")]
[Authorize]
public IActionResult RemovePost(int id)
  {
    var removedPost = _dbContext.posts.SingleOrDefault(rm => rm.Id == id);

    if (removedPost == null)
    {
      return NotFound();
    }

    _dbContext.posts.Remove(removedPost);
    _dbContext.SaveChanges();
     return NoContent();

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
