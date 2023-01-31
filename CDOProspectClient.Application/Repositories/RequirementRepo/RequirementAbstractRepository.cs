using CDOProspectClient.Contracts.Requirement;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.RequirementRepo;

public abstract class RequirementAbstractRepository : Repository<Requirement>
{
    public RequirementAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Requirement?> FindOne(int id)
    {
        var requirement = (await _context.Requirements
            .Include(r => r.Screening.BuyerInformation.TitlingInstruction)
            .Include(r => r.Screening.BuyerInformation.NegativeDataBankRecord)
            .Include(r => r.Screening.BuyerInformation.PagIbigMembership)
            .Include(r => r.Screening.BuyerInformation.EmployerDetails)
            .Include(r => r.Screening.Document.StandardDocument)
            .Include(r => r.Screening.Document.SourceOfIncome.LocallyEmployed)
            .Include(r => r.Screening.Document.SourceOfIncome.SelfEmployed.Formal)
            .Include(r => r.Screening.Document.SourceOfIncome.SelfEmployed.Informal)
            .Include(r => r.Screening.Document.SourceOfIncome.OverseasFilipinoWorker)
            .Include(r => r.Screening.Computation)
            .Include(r => r.Briefing.Buyer)
            .Include(r => r.Agent!.Profile)
            .Where(r => r.Id == id)
            .ToListAsync())
            .Select(r => 
                {
                    r.Briefing.Buyer = null!;
                    r.Agent!.Requirements = new List<Requirement>();
                    r.Screening.BuyerInformation.Briefing = null;
                    return r;
                }).FirstOrDefault();
            
        
        return requirement;
            
    }

    public override async Task<IEnumerable<Requirement>> FindAll()
    {
        // fix fetched data to the query.
        var requirements = (await _context.Requirements
            .Include(r => r.Screening.BuyerInformation.TitlingInstruction)
            .Include(r => r.Screening.BuyerInformation.NegativeDataBankRecord)
            .Include(r => r.Screening.BuyerInformation.PagIbigMembership)
            .Include(r => r.Screening.BuyerInformation.EmployerDetails)
            .Include(r => r.Screening.Document.StandardDocument)
            .Include(r => r.Screening.Document.SourceOfIncome.LocallyEmployed)
            .Include(r => r.Screening.Document.SourceOfIncome.SelfEmployed.Formal)
            .Include(r => r.Screening.Document.SourceOfIncome.SelfEmployed.Informal)
            .Include(r => r.Screening.Document.SourceOfIncome.OverseasFilipinoWorker)
            .Include(r => r.Screening.Computation)
            .Include(r => r.Briefing.Buyer)
            .Include(r => r.Agent!.Profile)
            .ToListAsync())
            .Select(r => 
                {
                    r.Briefing.Buyer = null!;
                    r.Agent!.Requirements = new List<Requirement>();
                    r.Screening.BuyerInformation.Briefing = null;
                    return r;
                });
        
        return requirements;
    }

    public abstract Task<Requirement?> Submit(RequirementRequest requirement);
    public abstract Task<IEnumerable<Requirement>> GetByAgentId(int agentId);
    public abstract Task<(bool, string)> AlterStatus(RequirementStatusRequest requirementStatusRequest);
}