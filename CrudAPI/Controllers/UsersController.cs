using CrudAPI.Data;
using CrudAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers;

public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User? user)
    {
        if (user is null) return BadRequest();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null) return NotFound("User not found.");

        user.Name = updatedUser.Name;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null) return NotFound("User not found.");

        _context.Remove(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}