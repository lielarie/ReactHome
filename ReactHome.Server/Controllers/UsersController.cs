using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactHome.Server.DTOs;
using ReactHome.Server.Infrastructure;
using ReactHome.Server.Models;

namespace ReactHome.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext db) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserById(Guid id, CancellationToken ct)
    {
        var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("byEmail")]
    public async Task<ActionResult<User>> GetUserByEmail(string email, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email is required.");

        var normalized = email.Trim().ToLowerInvariant();

        var user = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalized, ct);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateRequest req, CancellationToken ct)
    {
        var normalizedEmail = req.Email.Trim().ToLowerInvariant();
        var normalizedPhone = req.Phone.Trim();

        var emailExists = await db.Users.AnyAsync(u => u.Email.ToLower() == normalizedEmail, ct);

        if (emailExists) 
            return Conflict("User with this email already exists.");

        var phoneExists = await db.Users.AnyAsync(u => u.Phone.ToLower() == normalizedPhone, ct);

        if (phoneExists) 
            return Conflict("User with this phone already exists.");

        var user = new User
        {
            FullName = req.FullName,
            Phone = req.Phone,
            Email = req.Email.Trim()
        };

        db.Users.Add(user);
        await db.SaveChangesAsync(ct);

        return Ok($"Successfully created new user: {user.Id}");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UserUpdateRequest req, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        if (user is null) return NotFound();

        if (req.Email is not null)
        {
            var email = req.Email.Trim();

            if (email.Length == 0) 
                return BadRequest("Email cannot be empty.");

            var exists = await db.Users.AnyAsync(u => u.Email == email && u.Id != id, ct);

            if (exists) 
                return Conflict("Email already exists.");

            user.Email = email;
        }

        if (req.FullName is not null)
        {
            var fullName = req.FullName.Trim();

            if (fullName.Length == 0) 
                return BadRequest("FullName cannot be empty.");

            user.FullName = fullName;
        }

        if (req.Phone is not null)
        {
            var phone = req.Phone.Trim();

            if (phone.Length == 0) 
                return BadRequest("Phone cannot be empty.");

            user.Phone = phone;
        }

        await db.SaveChangesAsync(ct);

        return Ok($"Successfully updated user: {user.Id}");
    }
}
