using CDOProspectClient.Application.Repositories.AppointmentRepo;
using CDOProspectClient.Contracts.Appointment;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.EnumSetup;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.API.Controllers;

[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentAbstractRepository _appointmentRepository;
    public AppointmentController(AppointmentAbstractRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _appointmentRepository.FindAll();
        var json = JsonSerializerSetup.Serialize(results);
        return Ok(json);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var result = await _appointmentRepository.FindOne(id);
        var json = JsonSerializerSetup.Serialize(result!);
        return Ok(json);
    }

    [HttpPost]
    public async Task<IActionResult> SetAppointment(
        [FromBody]AppointmentRequest request)
    {
        var errors = new List<string>();
        if(!ModelState.IsValid)
        {
            errors.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage).ToList());

            return BadRequest(errors);
        }

        var agent = await _appointmentRepository._context.Agents
            .FirstOrDefaultAsync(a => a.Id == request.AgentId);

        if(agent is null) return BadRequest(new { Message = "Agent not found" });

        var newAppointment = new Appointment
        {
            Agent = agent,
            DateAppointment = request.AppointmentDate,
            Status = AppointmentStatus.Pending
        };

        var newClient = new Client
        {
            Appointment = newAppointment,
            Name = request.Client.Name,
            PhoneNumber = request.Client.PhoneNumber,
            Occupation = request.Client.Occupation
        };

        newAppointment.Client = newClient;
        var createdAppointment = await _appointmentRepository.Create(newAppointment);
        return Ok(new { Message = "Appointment setted successfully!" });
    }

    [HttpPut("alter-status")]
    public async Task<IActionResult> AlterStatus(
        [FromBody]AlterAppointmentStatusRequest request
    )
    {
        var status = EnumSetupService.DefineAppointmentStatus(request.Status);
        var appointmentTobeUpdate = await _appointmentRepository.FindOne(request.AppointmentId);

        if(appointmentTobeUpdate is null) return BadRequest(new { Message = "Appointment not found!" });

        appointmentTobeUpdate.Status = status;
        await _appointmentRepository.Update(appointmentTobeUpdate.Id, appointmentTobeUpdate);
        return Ok(new { Message = "Status has been updated!" });
    }
}   