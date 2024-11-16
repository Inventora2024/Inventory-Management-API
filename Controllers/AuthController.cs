using Microsoft.AspNetCore.Mvc;
using Inventory_Management_API.DTOs;
using Inventory_Management_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Inventory_Management_API;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public AuthController(TokenService tokenService, InventoryManagementDbContext context, IMapper mapper)
    {
        _tokenService = tokenService;
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password && u.Role == login.Role);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user.Email, user.Role);

        var loginReturnDto = _mapper.Map<LoginReturnDTO>(user);
        loginReturnDto.Token = token;

        return Ok(loginReturnDto);
    }
}
