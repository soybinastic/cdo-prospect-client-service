using CDOProspectClient.Application.Repositories.EvaluationRepo;
using CDOProspectClient.Contracts.Evaluation;
using CDOProspectClient.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.API.Controllers;

[Route("api/[controller]")]
public class EvaluationController : ControllerBase
{
    private readonly EvaluationAbstractRepository _evaluationRepository;
    public EvaluationController(EvaluationAbstractRepository evaluationRepository)
    {
        _evaluationRepository = evaluationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _evaluationRepository.FindAll();
        var json = JsonSerializerSetup.Serialize(results);
        return Ok(json);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var evaluation = await _evaluationRepository.FindOne(id);
        var json = JsonSerializerSetup.Serialize(evaluation!);
        return Ok(evaluation);
    }

    [HttpPut]
    public async Task<IActionResult> Evaluate(
        [FromBody]EvaluateRequest request)
    {
        (bool isSuccess, string message) = await _evaluationRepository.Evalaute(request);
        return isSuccess ? Ok(new { message }) : BadRequest(new { message });
    }
}