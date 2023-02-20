using System.Security.Claims;
using CDOProspectClient.Infrastructure.Data;
using CDOProspectClient.Infrastructure.Helpers.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public NotificationController(ApplicationDbContext context)
    {
       _context = context;
    }

    

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var notifications = await _context.Notifications
            .Include(n => n.NotificationObject)
            .ThenInclude(n => n.NotificationEntityType)
            .Where(n => n.NotifierId == userId)
            .OrderByDescending(n => n.DateNotified)
            .ToListAsync();
        var json = JsonSerializerSetup.Serialize(notifications);
        return Ok(json);
    }
}