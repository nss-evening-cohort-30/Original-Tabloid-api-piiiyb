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
    return Ok(_dbContext.posts.Include(c => c.Category).Include(u => u.User).Select(p => new PostDto
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
      UserId = p.UserId,
      User = new UserProfileDto
      {
        Id = p.User.Id,
        FirstName = p.User.FirstName,
        LastName = p.User.LastName,
        UserName = p.User.UserName,
        Email = p.User.Email
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
      .Include(p => p.User)
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
      UserId = post.UserId,
      User = new UserProfileDto
      {
        Id = post.User.Id,
        FirstName = post.User.FirstName,
        LastName = post.User.LastName
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

  [HttpGet("tag/{tagId}")]
  // [Authorize]
  public IActionResult GetByTag(int tagId)
  {
    var posts = _dbContext.PostTags
      .Where(pt => pt.TagId == tagId)
      .Include(pt => pt.Post)
        .ThenInclude(p => p.Category)
      .Include(pt => pt.Post)
        .ThenInclude(p => p.User)
      .Select(pt => new PostDto
      {
        Id = pt.Post.Id,
        Title = pt.Post.Title,
        CategoryId = pt.Post.CategoryId,
        Category = new CategoryDto
        {
          Id = pt.Post.Category.Id,
          Name = pt.Post.Category.Name
        },
        PublishedOn = pt.Post.PublishedOn,
        RealTime = pt.Post.RealTime,
        UserId = pt.Post.UserId,
        User = new UserProfileDto
        {
          Id = pt.Post.User.Id,
          FirstName = pt.Post.User.FirstName,
          LastName = pt.Post.User.LastName,
          UserName = pt.Post.User.UserName,
          Email = pt.Post.User.Email
        },
        Body = pt.Post.Body,
        SubTitle = pt.Post.SubTitle
      })
      .ToList();

    return Ok(posts);
  }

[HttpGet("user/{userId}")]
//[Authorize]
public IActionResult GetByUser(int userId)
{
  var posts = _dbContext.posts
    .Include(p => p.Category)
    .Include(p => p.User)
    .Where(p => p.UserId == userId)
    .OrderByDescending(p => p.PublishedOn)
    .Select(p => new PostDto
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
      UserId = p.UserId,
      User = new UserProfileDto
      {
        Id = p.User.Id,
        FirstName = p.User.FirstName,
        LastName = p.User.LastName,
        UserName = p.User.UserName,
        Email = p.User.Email
      },
      Body = p.Body,
      SubTitle = p.SubTitle
    })
    .ToList();

  return Ok(posts);
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
    updatedPost.UserId = post.UserId;
    updatedPost.Body = post.Body;
    updatedPost.SubTitle = post.SubTitle;

    _dbContext.SaveChanges();
    return NoContent();
  }

[HttpPost]
// [Authorize]
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
//   public int UserId { get; set; }
//   public UserProfile User { get; set; }
//   [Required]
//   public string Body { get; set; }
//   [Required]
//   public string SubTitle { get; set; }
// }


// public class UserProfileDto
// {
//     public int Id { get; set; }

//     [Required]
//     [MaxLength(50)]
//     public string FirstName { get; set; }

//     [Required]
//     [MaxLength(50)]
//     public string LastName { get; set; }

//     [NotMapped]
//     public string UserName { get; set; }

//     [NotMapped]
//     public string Email { get; set; }

//     public DateTime CreateDateTime { get; set; }

//     [DataType(DataType.Url)]
//     [MaxLength(255)]
//     public string ImageLocation { get; set; }

//     public SubAuthorDto Subscriptions { get; set; }
// }
