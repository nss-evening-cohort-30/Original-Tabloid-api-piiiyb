using Microsoft.AspNetCore.Mvc;
using Tabloid.Models;
using Tabloid.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Tabloid.Models.Dtos;
using System.Runtime.Intrinsics.X86;

namespace Tabloid.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PostCommentController : ControllerBase
{
    private TabloidDbContext _dbContext;

    public PostCommentController(TabloidDbContext context)
    {
        _dbContext = context;
    }

  [HttpGet("{postId}")]
    public IActionResult GetByPostId(int postId)
  {
    return Ok(_dbContext.postComments
      .Where(pc => pc.PostId == postId)
      .Include(pc => pc.User)
      .Select(pc => new PostCommentDto
      {
        Id = pc.Id,
        Comment = pc.Comment,
        PostedOne = pc.PostedOne,
        UserId = pc.UserId,
        User = new UserProfileDto
        {
          Id = pc.User.Id,
          FirstName = pc.User.FirstName,
          LastName = pc.User.LastName
        }
      }).ToList());
  }

  [HttpPost]
    public IActionResult Create(PostComment PostCommentToCreate)
  {
    _dbContext.postComments.Add(PostCommentToCreate);
    _dbContext.SaveChanges();
    return Created($"api/PostComment/{PostCommentToCreate.Id}", PostCommentToCreate);
  }

  [HttpDelete("{id}")]
    public IActionResult Delete(int id)
  {
    PostComment PostCommentToDelete = _dbContext.postComments.FirstOrDefault(c => c.Id == id);
    if (PostCommentToDelete == null)
    {
      return NotFound();
    }
    _dbContext.postComments.Remove(PostCommentToDelete);
    _dbContext.SaveChanges();
      return NoContent();
  }

  [HttpPut("{id}")]
    public IActionResult Update(int id, PostComment Update)
  {
    PostComment PostCommentToUpdate = _dbContext.postComments.FirstOrDefault(c => c.Id == id);
    if (PostCommentToUpdate == null)
    {
      return NotFound();
    }
    PostCommentToUpdate.Comment = Update.Comment;
    _dbContext.SaveChanges();
    return NoContent();
  }
}
