using Microsoft.AspNetCore.Mvc;
using Tabloid.Models;
using Tabloid.Models.Dtos;
using Tabloid.Data;
using Microsoft.AspNetCore.Authorization;

namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private TabloidDbContext _dbContext;

    public TagController(TabloidDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var tags = _dbContext.Tags
            .OrderBy(t => t.Name)
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToList();

        return Ok(tags);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetById(int id)
    {
        var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);

        if (tag == null)
        {
            return NotFound();
        }

        var tagDto = new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };

        return Ok(tagDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromBody] Tag tag)
    {
        if (string.IsNullOrWhiteSpace(tag.Name))
        {
            return BadRequest("Tag name is required.");
        }

        // Check if tag with same name already exists (case-insensitive)
        var existingTag = _dbContext.Tags
            .FirstOrDefault(t => t.Name.ToLower() == tag.Name.ToLower());

        if (existingTag != null)
        {
            return BadRequest("A tag with this name already exists.");
        }

        _dbContext.Tags.Add(tag);
        _dbContext.SaveChanges();

        var tagDto = new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };

        return Created($"/api/tag/{tag.Id}", tagDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(int id, [FromBody] Tag tag)
    {
        var existingTag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);

        if (existingTag == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(tag.Name))
        {
            return BadRequest("Tag name is required.");
        }

        // Check if another tag with same name already exists (case-insensitive)
        var duplicateTag = _dbContext.Tags
            .FirstOrDefault(t => t.Name.ToLower() == tag.Name.ToLower() && t.Id != id);

        if (duplicateTag != null)
        {
            return BadRequest("A tag with this name already exists.");
        }

        existingTag.Name = tag.Name;
        _dbContext.SaveChanges();

        var tagDto = new TagDto
        {
            Id = existingTag.Id,
            Name = existingTag.Name
        };

        return Ok(tagDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);

        if (tag == null)
        {
            return NotFound();
        }

        _dbContext.Tags.Remove(tag);
        _dbContext.SaveChanges();

        return NoContent();
    }
}
