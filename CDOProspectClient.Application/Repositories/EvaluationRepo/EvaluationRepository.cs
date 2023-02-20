using CDOProspectClient.Contracts.Evaluation;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.EnumSetup;

namespace CDOProspectClient.Application.Repositories.EvaluationRepo;


public class EvaluationRepository : EvaluationAbstractRepository
{
    public EvaluationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<(bool, string)> Evalaute(EvaluateRequest evaluateRequest)
    {
        var status = EnumSetupService.DefineEvaluationStatus(evaluateRequest.Status);

        if(status != EvaluationStatus.Approved && status != EvaluationStatus.Rejected)
        {
            return (false, "Invalid evaluation status");
        }

        var evaluation = await _context.Evaluations.FirstOrDefaultAsync(e => e.Id == evaluateRequest.EvaluationId);
        if(evaluation is null) return (false, "Evaluation not found.");
        if(evaluation.Status != EvaluationStatus.Pending) return (false, "Failed to evaluate");
        if(
            (evaluation.Status == EvaluationStatus.Approved && status == EvaluationStatus.Rejected) ||
            (evaluation.Status == EvaluationStatus.Rejected && status == EvaluationStatus.Approved))
        {
            return (false, $"The evaluation is already {(status == EvaluationStatus.Rejected ? "approved" : "rejected")} , you cannot modified the status");
        }

        if(evaluation.Status == status) 
        {
            return (
                false,
                status == EvaluationStatus.Approved ? "It's already approved" : "It's already rejected"
            );
        }

        try
        {
            var requirement = await _context.Requirements
                .FirstAsync(r => r.Id == evaluation.RequirementId);
            
            evaluation.Status = status == EvaluationStatus.Approved ? EvaluationStatus.Approved : EvaluationStatus.Rejected;
            requirement.Status = status == EvaluationStatus.Approved ? Status.Approved : Status.Rejected;

            await _context.SaveChangesAsync();

            // sending notification for evaluation
            var agentRequirment = await _context.Requirements
                .Include(r => r.Agent)
                .FirstOrDefaultAsync(r => r.Id == evaluation.RequirementId);
            
            // tempoarary actor or notification creator (admin)
            var admin = await _context.Admins.FirstOrDefaultAsync();

            // get entity type for notification
            var notifEntityType = await _context.NotificationEntityTypes
                .FirstOrDefaultAsync(net => net.Id == (status == EvaluationStatus.Approved ? 3 : 4));

            if(agentRequirment is not null && admin is not null && notifEntityType is not null)
            {
                var notifObject = new NotificationObject
                {
                    NotificationEntityType = notifEntityType,
                    EntityId = evaluation.Id
                };
                var notification = new Notification
                {
                    NotificationObject = notifObject,
                    ActorId = admin.UserId,
                    NotifierId = agentRequirment.Agent!.UserId,
                    DateNotified = DateTime.Now,
                    Status = NotificationStatus.Delivered
                };

                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
            return (
                true, 
                $"{(status == EvaluationStatus.Approved ? "Successfully Approved!" : "Your request is rejected!")}");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }

    }
}