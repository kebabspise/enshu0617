using enshu0617asp.Models;
using enshu0617asp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;
    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Rooms.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null) return NotFound();
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Room room)
    {
        if(id != room.ID) return BadRequest();
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}