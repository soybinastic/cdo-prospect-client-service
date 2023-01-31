using CDOProspectClient.Application.Repositories.RequirementRepo;
using CDOProspectClient.Contracts.Requirement;

namespace CDOProspectClient.API.Controllers;

[Route("api/[controller]")]
public class RequirementsController : ControllerBase
{
    private readonly RequirementAbstractRepository _requirementRepository;
    public RequirementsController(RequirementAbstractRepository requirementRepository)
    {
        _requirementRepository = requirementRepository;
    }
    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromForm]RequirementRequest request)
    {
        var errors = new List<string>();
        if(!ModelState.IsValid)
        {
            errors.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                .Select(m => m.ErrorMessage));
            return BadRequest(errors);
        }

        try
        {
            var result = await _requirementRepository.Submit(request);
            return Ok(new { Message = "Requirements submitted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery(Name = "agentId")]int agentId = 0)
    {
        var requirements = await _requirementRepository.FindAll();

        if(agentId != 0) 
        {
            requirements = requirements.AsQueryable().Where(r => r.Agent!.Id == agentId);
        }
        var json = JsonSerializerSetup.Serialize(requirements);
        return Ok(json);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var requirement = await _requirementRepository.FindOne(id);
        var json = JsonSerializerSetup.Serialize(requirement!);
        return Ok(json);
    }

    [HttpPut("status-modification")]
    public async Task<IActionResult> ModifiedStatus(
        [FromBody]RequirementStatusRequest request)
    {
        (bool isSuccess, string message) = await _requirementRepository.AlterStatus(request);
        return isSuccess ? Ok(new { message }) : BadRequest(new { message });
    }
}