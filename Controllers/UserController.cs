using AutoMapper;
using Inventory_Management_API.DTOs;
using Inventory_Management_API.Models;
using Inventory_Management_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public UserController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/User
    [HttpGet]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return userDTOs;
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
    {
        if (id != userDTO.UserId)
        {
            return BadRequest();
        }

        var user = _mapper.Map<User>(userDTO);
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/User
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userDTO);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/User/display
    [HttpGet("display")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ActionResult<IEnumerable<UserDisplayDTO>>> GetUsersDisplay()
    {
        var users = await _context.Users.ToListAsync();
        var userDisplayDTOs = _mapper.Map<List<UserDisplayDTO>>(users);
        return userDisplayDTOs;
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserId == id);
    }
}
