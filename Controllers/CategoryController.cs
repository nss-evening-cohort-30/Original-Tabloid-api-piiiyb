using Microsoft.AspNetCore.Mvc;
using Tabloid.Models;
using Tabloid.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Tabloid.Models.Dtos;

namespace Tabloid.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private TabloidDbContext _dbContext;

    public CategoryController(TabloidDbContext context)
    {
        _dbContext = context;
    }

  [HttpGet]
    public IActionResult Get()
    {
        return Ok(_dbContext.categorys.Select(c => new CategoryDto {Name = c.Name, Id = c.Id}).ToList());
    }

  [HttpDelete("{id}")]
    public IActionResult Delete(int id)
  {
    Category CategoryToDelete = _dbContext.categorys.FirstOrDefault(c => c.Id == id);
    if (CategoryToDelete == null)
    {
      return NotFound();
    };
    _dbContext.categorys.Remove(CategoryToDelete);
    _dbContext.SaveChanges();
    return NoContent();
  }

  [HttpPost]
    public IActionResult Post(Category CategoryToCreate)
  {
    _dbContext.categorys.Add(CategoryToCreate);
    _dbContext.SaveChanges();
    return Created($"api/Category/{CategoryToCreate.Id}", CategoryToCreate);
  }
}
