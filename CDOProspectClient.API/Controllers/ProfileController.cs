using System.Security.Claims;
using CDOProspectClient.API.Constants;
using CDOProspectClient.Application.Repositories.AdminRepo;
using CDOProspectClient.Application.Repositories.AgentRepo;
using CDOProspectClient.Contracts.Information;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.Upload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CDOProspectClient.API.Controllers;
// [ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly ProfileAbstractRepository _profileRepository;
    private readonly AgentAbstractRepository _agentRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUploadService _uploadService;
    private readonly AdminAbstractRepository _adminRepository;
    public ProfileController(
        ProfileAbstractRepository profileRepository,
        AgentAbstractRepository agentRepository,
        IUploadService uploadService,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AdminAbstractRepository adminRepository)
    {
        _profileRepository = profileRepository;
        _agentRepository = agentRepository;
        _uploadService = uploadService;
        _userManager = userManager;
        _roleManager = roleManager;
        _adminRepository = adminRepository;
    }

    [HttpPost("agents")]
    public async Task<IActionResult> CreateAgent([FromForm]CreateInformationRequest request)
    {
        var errors = new List<string>();
        if(!ModelState.IsValid)
        {
            errors.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList());

            if(!HttpContext.Request.Form.Files.Any()) 
            {
                errors.Add("Please upload profile picture");
            }

            return BadRequest(errors);
        } 

        if(!HttpContext.Request.Form.Files.Any()) 
        {
            errors.Add("Please upload profile picture");
            return BadRequest(errors);
        }

        var user = await _userManager.FindByNameAsync(request.Account.Username);
        if(!await _roleManager.RoleExistsAsync(ConstantVariables.AgentRole))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = ConstantVariables.AgentRole });
        }

        if(user is not null) 
        {
            errors.Add("Username is already exist!");
            return BadRequest(errors);
        }

        user = new IdentityUser 
        {
            UserName = request.Account.Username
        };

        var identityResult = await _userManager.CreateAsync(user, request.Account.Password);
        if(!identityResult.Succeeded)
        {
            errors.AddRange(identityResult.Errors.Select(e => e.Description));
            return BadRequest(errors);
        }

        await _userManager.AddToRoleAsync(user, ConstantVariables.AgentRole);
        

        var file = HttpContext.Request.Form.Files.FirstOrDefault()!;

        var uploadResult = await _uploadService.Image(file, "profiles");
        var profile = new Profile 
        {
            FirstName = request.Profile.Firstname,
            MiddleName = request.Profile.Middlename,
            LastName = request.Profile.Lastname,
            Address = request.Profile.Address,
            Email = request.Profile.Email,
            PhoneNumber = request.Profile.ContactNumber,
            ProfileImageLink = uploadResult.SecureUrl.AbsoluteUri
        };

        var createdProfile = await _profileRepository.Create(profile);
        var createdAgent = await _agentRepository.Create(new Agent { UserId = user.Id, Profile = createdProfile });
        
        return Created(
           nameof(GetAgent),
           createdAgent
        );
    }

    [HttpPost("admins")]
    public async Task<IActionResult> CreateAdmin([FromForm]CreateInformationRequest request)
    {
        var errors = new List<string>();
        if(!ModelState.IsValid)
        {
            errors.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList());

            if(!HttpContext.Request.Form.Files.Any()) 
            {
                errors.Add("Please upload profile picture");
            }

            return BadRequest(errors);
        } 

        if(!HttpContext.Request.Form.Files.Any()) 
        {
            errors.Add("Please upload profile picture");
            return BadRequest(errors);
        }

        var user = await _userManager.FindByNameAsync(request.Account.Username);
        if(!await _roleManager.RoleExistsAsync(ConstantVariables.AdminRole))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = ConstantVariables.AdminRole });
        }

        if(user is not null) 
        {
            errors.Add("Username is already exist!");
            return BadRequest(errors);
        }

        user = new IdentityUser 
        {
            UserName = request.Account.Username
        };

        var identityResult = await _userManager.CreateAsync(user, request.Account.Password);
        if(!identityResult.Succeeded)
        {
            errors.AddRange(identityResult.Errors.Select(e => e.Description));
            return BadRequest(errors);
        }

        await _userManager.AddToRoleAsync(user, ConstantVariables.AdminRole);
        

        var file = HttpContext.Request.Form.Files.FirstOrDefault()!;

        var uploadResult = await _uploadService.Image(file, "profiles");
        var profile = new Profile 
        {
            FirstName = request.Profile.Firstname,
            MiddleName = request.Profile.Middlename,
            LastName = request.Profile.Lastname,
            Address = request.Profile.Address,
            Email = request.Profile.Email,
            PhoneNumber = request.Profile.ContactNumber,
            ProfileImageLink = uploadResult.SecureUrl.AbsoluteUri
        };

        var createdProfile = await _profileRepository.Create(profile);
        var createdAdmin = await _adminRepository.Create(new Admin { UserId = user.Id, Profile = createdProfile });
        
        return Created(
           nameof(GetAgent),
           createdAdmin
        );
    }

    [HttpGet("agents")]
    public async Task<IActionResult> GetAllAgents()
    {
        var agents = await _agentRepository.FindAll();

        return Ok(agents);
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var admins = await _adminRepository.FindAll();

        return Ok(admins);
    }

    [HttpGet("agents/{id}")]
    public async Task<IActionResult> GetAgent([FromRoute]int id)
    {
        var agent = await _agentRepository.FindOne(id);
        return Ok(agent);
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile([FromQuery(Name = "type")]string type = "agent")
    {
        type = type.ToLower();
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        
        if(type.Equals("agent") || string.IsNullOrEmpty(type))
        {
            var agent = await _agentRepository.FindByUserId(userId);
            return Ok(agent);
        }
        else
        {
            var admin = await _adminRepository.FindByUserId(userId);
            return Ok(admin);
        }
    }

}