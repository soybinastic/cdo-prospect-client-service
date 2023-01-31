using System.Text.Json;
using CDOProspectClient.Application.Repositories.AgentRepo;
using CDOProspectClient.Application.Repositories.PropertyRepo;
using CDOProspectClient.Contracts.Property;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.Upload;
using Microsoft.AspNetCore.Authorization;

namespace CDOProspectClient.API.Controllers;

[Route("api/[controller]")]
// [Authorize]
public class PropertyController : ControllerBase
{
    private readonly PropertyAbstractRepository _propertyRepository;
    private readonly AgentAbstractRepository _agentRepository;
    private readonly IUploadService _uploadService;
    public PropertyController(
        PropertyAbstractRepository properytRepository,
        AgentAbstractRepository agentRepository,
        IUploadService uploadService)
    {
        _propertyRepository = properytRepository;
        _agentRepository = agentRepository;
        _uploadService = uploadService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]SavePropertyRequest request)
    {
        var errors = new List<string>();
        if(!ModelState.IsValid)
        {
            errors.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                .Select(m => m.ErrorMessage));

            if(!HttpContext.Request.Form.Files.Any())
            {
                errors.Add("Please upload property image.");
            }

            return BadRequest(errors);
        }

        var agent = await _agentRepository.FindOne(request.AgentId);
        if(agent is null) 
        {
            errors.Add("Agent not found!");
            return BadRequest(errors);
        }

        var type = await _propertyRepository.GetType(request.PropertyTypeId);
        if(type is null)
        {
            errors.Add("Property type not found!");
            return BadRequest(errors);
        }

        var uploadedResult = await _uploadService.Image(HttpContext.Request.Form.Files[0], "property");

        var result = await _propertyRepository.Create(new Property 
            { 
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Agent = agent,
                PropertyType = type,
                Available = true,
                ImageUrl = uploadedResult.SecureUrl.AbsoluteUri,
                DateCreated = DateTime.UtcNow
            });
        
        return Ok(new { Message = "Created successfully!" });
    }

    [HttpPut("details/{id}")]
    public async Task<IActionResult> UpdateDetails(
        [FromRoute]int id,
        [FromBody] SavePropertyRequest request)
    {
        var errors = new List<string>();

        var propertyToUpdate = await _propertyRepository.FindOne(id);

        if(propertyToUpdate is null)
        {
            errors.Add("Property that to be update is not found.");
            return BadRequest(errors);
        }

        var agent = await _agentRepository.FindOne(request.AgentId);
        if(agent is null) 
        {
            errors.Add("Agent not found!");
            return BadRequest(errors);
        }

        var type = await _propertyRepository.GetType(request.PropertyTypeId);
        if(type is null)
        {
            errors.Add("Property type not found!");
            return BadRequest(errors);
        }

        propertyToUpdate.Agent = agent;
        propertyToUpdate.PropertyType = type;
        propertyToUpdate.Name = request.Name;
        propertyToUpdate.Price = request.Price;
        propertyToUpdate.Description = request.Description;

        var result = await _propertyRepository.Update(propertyToUpdate.Id, propertyToUpdate);

        return Ok(new { Message = "Modified successfully." });
    }

    [HttpPut("profile-image")]
    public async Task<IActionResult> UpdateProfileImage([FromForm]UpdatePropertyImage request)
    {
        var errors = new List<string>();
        if(!HttpContext.Request.Form.Files.Any())
        {
            errors.Add("Please upload a photo.");
            return BadRequest(errors);
        }

        var propertyToUpdate = await _propertyRepository.FindOne(request.PropertyId);
        if(propertyToUpdate is null) 
        {
            errors.Add("Property that to be update is not found!");
            return BadRequest(errors);
        }
        
        var uploadedResult = await _uploadService.Image(HttpContext.Request.Form.Files[0], "property");
        propertyToUpdate.ImageUrl = uploadedResult.SecureUrl.AbsoluteUri;

        var result = await _propertyRepository.Update(propertyToUpdate.Id, propertyToUpdate);
        return Ok(new { Message = "Profile Image modified successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var property = await _propertyRepository.FindOne(id);
        var json = JsonSerializerSetup.Serialize(property!);
        return Ok(json);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyRepository.FindAll();
        // var json = JsonSerializer.Serialize(properties)
        var json = JsonSerializerSetup.Serialize(properties);
        return Ok(json);
    }
}