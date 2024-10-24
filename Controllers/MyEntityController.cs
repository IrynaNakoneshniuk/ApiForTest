using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiForTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyEntityController : ControllerBase
{
    private readonly TestContext _context;

    public MyEntityController(TestContext context)
    {
        _context = context;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedData()
    {
        var myEntity = new Author
        {
            Name = "Test",
            Contact = new List<ContactInfo>
                {
                    new ContactInfo { Phone = "123-456-7890", Email = "example1@example.com" },
                    new ContactInfo { Phone = "098-765-4321", Email = "example2@example.com" }
                }
        };

        await _context.Author.AddAsync(myEntity);
        await _context.SaveChangesAsync();

        return Ok("Test data seeded.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAll()
    {
        var myEntities = await _context.Author.Where(a => a.Contact.Contains(new ContactInfo() { Email = "example1@example.com", Phone = "123-456-7890" })).Select(p => p.Contact).ToListAsync();
        return Ok(myEntities);
    }
    
}

