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

  [HttpGet("{id}")]
  // [Authorize]
  public IActionResult GetById(int id)
  {
    var post = _dbContext.posts
      .Include(p => p.Category)
      .Include(p => p.Author)
      .Include(p => p.PostTags)
        .ThenInclude(pt => pt.Tag)
      .FirstOrDefault(p => p.Id == id);

    if (post == null)
    {
      return NotFound();
    }

    var postDto = new PostDto
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
      SubTitle = post.SubTitle,
      Tags = post.PostTags?.Select(pt => new TagDto
      {
        Id = pt.Tag.Id,
        Name = pt.Tag.Name
      }).ToList() ?? new List<TagDto>()
    };

    return Ok(postDto);
  }

  [HttpGet("{postId}/tags")]
  // [Authorize]
  public IActionResult GetPostTags(int postId)
  {
    var post = _dbContext.posts
      .Include(p => p.PostTags)
        .ThenInclude(pt => pt.Tag)
      .FirstOrDefault(p => p.Id == postId);

    if (post == null)
    {
      return NotFound();
    }

    var tags = post.PostTags?.Select(pt => new TagDto
    {
      Id = pt.Tag.Id,
      Name = pt.Tag.Name
    }).ToList() ?? new List<TagDto>();

    return Ok(tags);
  }

  [HttpPut("{postId}/tags")]
  // [Authorize]
  public IActionResult UpdatePostTags(int postId, [FromBody] List<int> tagIds)
  {
    var post = _dbContext.posts
      .Include(p => p.PostTags)
      .FirstOrDefault(p => p.Id == postId);

    if (post == null)
    {
      return NotFound();
    }

    // Remove all existing tags
    _dbContext.PostTags.RemoveRange(post.PostTags);

    // Add new tags
    foreach (var tagId in tagIds)
    {
      var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
      if (tag != null)
      {
        post.PostTags.Add(new PostTag
        {
          PostId = postId,
          TagId = tagId
        });
      }
    }

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
