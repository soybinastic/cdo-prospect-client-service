using System.Security.Claims;
using CDOProspectClient.Infrastructure.Helpers.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CDOProspectClient.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    public AuthController(
        IJwtService jwtService,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _jwtService = jwtService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if(user is null) 
        {
            return BadRequest(new BaseResponse<object>(
                false,
                new List<string> { "Invalid username" },
                null!
            ));
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if(!signInResult.Succeeded)
        {
            return BadRequest(new BaseResponse<object>(
                false,
                new List<string> { "Invalid credentials" },
                null!
            ));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user.Id, user.UserName!, roles.ToList());

        return Ok(new BaseResponse<LoginResponse>(
            true,
            null!,
            new LoginResponse(
                user.Id, 
                user.UserName!, 
                token)
        ));
    }

    [HttpGet]
    public IActionResult Me()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine(userId);
        return Ok(new BaseResponse<string>(
            true,
            null!,
            userId!
        ));
    }
}