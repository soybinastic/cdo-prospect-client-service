using CDOProspectClient.Contracts.Evaluation;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.EnumSetup;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.EvaluationRepo;

public abstract class EvaluationAbstractRepository : Repository<Evaluation>
{
    protected EvaluationAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Evaluation?> FindOne(int id)
    {
        var evaluation = await _context.Evaluations
            .Include(e => e.Requirement)
            .Include(e => e.Requirement.Briefing)
            .Include(e => e.Requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker)
            .Include(e => e.Requirement.Screening.Document.SourceOfIncome.LocallyEmployed)
            .Include(e => e.Requirement.Screening.Document.SourceOfIncome.SelfEmployed.Formal)
            .Include(e => e.Requirement.Screening.Document.SourceOfIncome.SelfEmployed.Informal)
            .Include(e => e.Requirement.Screening.Document.StandardDocument)
            .Include(e => e.Requirement.Screening.BuyerInformation.EmployerDetails)
            .Include(e => e.Requirement.Screening.BuyerInformation.NegativeDataBankRecord)
            .Include(e => e.Requirement.Screening.BuyerInformation.PagIbigMembership)
            .Include(e => e.Requirement.Screening.BuyerInformation.TitlingInstruction)
            .Include(e => e.Requirement.Screening.Computation)
            .Include(e => e.Requirement.Agent!.Profile)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        return evaluation;
    }

    public override async Task<IEnumerable<Evaluation>> FindAll()
    {
        var evaluations = await _context.Evaluations
            .Include(e => e.Requirement)
            .Include(e => e.Requirement.Briefing)
            .Include(e => e.Requirement.Screening.BuyerInformation)
            .Include(e => e.Requirement.Agent!.Profile)
            .ToListAsync();
        return evaluations;
    }
    public abstract Task<(bool, string)> Evalaute(EvaluateRequest evaluateRequest);
}