using CDOProspectClient.Contracts.Requirement;
using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Helpers.EnumSetup;
using CDOProspectClient.Infrastructure.Helpers.Upload;

namespace CDOProspectClient.Application.Repositories.RequirementRepo;

public class RequirementRepository : RequirementAbstractRepository
{
    private readonly IUploadService _uploadService;
    public RequirementRepository(
        ApplicationDbContext context,
        IUploadService uploadService) : base(context) 
    {
        _uploadService = uploadService;
    }

    public override async Task<(bool, string)> AlterStatus(RequirementStatusRequest requirementStatusRequest)
    {
        // to finalize Pending and Archive
        var status = EnumSetupService.DefineRequirementStatus(requirementStatusRequest.Status);
        var requirement = await _context.Requirements.FirstOrDefaultAsync(r => r.Id == requirementStatusRequest.RequirementId);
        Console.WriteLine(requirement?.Agent?.Id);
        if(requirement is null) return (false, $"Requirement with id of {requirementStatusRequest.RequirementId} is not found!");

        if(status == Status.Approved) 
        {
            return (false, "Only sales coordinator can approved");
        }

        if(status == Status.Forwarded)
        {
            if(requirement.Status == Status.Pending || requirement.Status == Status.Cancelled)
            {
                requirement.Status = Status.Forwarded;
                // _context.Requirements.Update(requirement);
                // some logic form putting requirement to Evaluation table in the database
                var queryEvaluation = await _context.Evaluations.FirstOrDefaultAsync(e => e.RequirementId == requirement.Id);
                if(queryEvaluation is null)
                {
                    await _context.Evaluations.AddAsync(new Evaluation
                    {
                        Status = EnumSetupService.DefineEvaluationStatus(0),
                        RequirementId = requirement.Id,
                        Date = DateTimeOffset.Now
                    });
                }
                else
                {
                    queryEvaluation.Status = EvaluationStatus.Pending;
                }
                
                await _context.SaveChangesAsync();

                return (true, "Successfully Forwarded");
            }
            else
            {
                return (true, "This requirement was already forwarded in Admin");
            }
        }
        else if(status == Status.Archived)
        {
            requirement.Status = Status.Archived;
            // await Update(requirement.Id, requirement);
            await _context.SaveChangesAsync();
            return (true, "Status updated to archived!");
        }
        else if(status == Status.Cancelled)
        {
            if(requirement.Status != Status.Approved || requirement.Status == Status.Cancelled)
            {
                try
                {
                    var evaluation = await _context.Evaluations.FirstAsync(e => e.RequirementId == requirement.Id);
                    // some logic or code for removing the requirement from Evaluation table
                    //......
                    requirement.Status = Status.Cancelled;

                    evaluation.Status = EnumSetupService.DefineEvaluationStatus(2);
                    await _context.SaveChangesAsync();
                    return (true, "Status updated to Cancelled");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            }
            else
            {
                return (false, "Failed to cancel requirement, it's already approved or cancelled");
            }
        }
        else if(status == Status.Pending)
        {
            requirement.Status = Status.Pending;
            // await Update(requirement.Id, requirement);
            await _context.SaveChangesAsync();
            return (true, "Status is came back to pending!");
        }
        else if(status == Status.Received)
        {
            if(requirement.Status == Status.Forwarded)
            {
                requirement.Status = EnumSetupService.DefineRequirementStatus(5);
                await _context.SaveChangesAsync();
                return (true, "Requirement received");
            }

            return (false, "Requirement is already receieved");
        }
        else
        {
            return (false, "Invalid status");
        }

        
    }

    public override async Task<IEnumerable<Requirement>> GetByAgentId(int agentId)
    {
        var requirements = await _context.Requirements
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
            .Include(r => r.Agent)
            .Where(r => r.Agent!.Id == agentId)
            .ToListAsync();
        
        return requirements;
    }

    public override async Task<Requirement?> Submit(RequirementRequest requirement)
    {
        try
        {
            var agent = await _context.Agents.FirstAsync(a => a.Id == requirement.AgentId);

            var newRequirement = new Requirement();
            newRequirement.Agent = agent;
            newRequirement.Status = Status.Pending;
            // Screening layer
            var newScreening = new Screening();
            newScreening.Requirement = newRequirement;
            newScreening.Date = DateTimeOffset.Now;
            newScreening.Remarks = requirement.Screening.Remarks;

            // uploading required files in screening
            var screeningInterviewedBy = await _uploadService.Image(requirement.Screening.InterviewedBy, "screening");
            var screeningConformeUrl = await _uploadService.Image(requirement.Screening.Conforme, "screening");

            newScreening.InterviewedBy = screeningInterviewedBy.SecureUrl.AbsoluteUri;
            newScreening.Conforme = screeningConformeUrl.SecureUrl.AbsoluteUri;


            // Briefing Layer
            var newBriefing = new Briefing();
            newBriefing.Requirement = newRequirement;
            newBriefing.ReservationDate = DateTime.Now;
            newBriefing.ProjectName = requirement.Briefing.ProjectName;
            newBriefing.PH = requirement.Briefing.PH;
            newBriefing.Block = requirement.Briefing.Block;
            newBriefing.Lot = requirement.Briefing.Lot;
            newBriefing.SalesChannel = requirement.Briefing.SalesChannel;
            newBriefing.Broker = requirement.Briefing.Broker;
            newBriefing.DirectSeller = requirement.Briefing.DirectSeller;
            newBriefing.Financing = EnumSetupService.DefineFinancing((int)requirement.Briefing.Financing);
            newBriefing.ReservationDocuments = requirement.Briefing.ReservationDocuments;
            newBriefing.ReferenceNumber = requirement.Briefing.ReferenceNumber;
            newBriefing.Date = DateTimeOffset.Now;

            // Briefing uploading required files
            var briefingConforme = await _uploadService.Image(requirement.Briefing.Conforme, "briefing");
            var briefedBy = await _uploadService.Image(requirement.Briefing.BriefedBy, "briefing");
            var witness = await _uploadService.Image(requirement.Briefing.Witness, "briefing");

            // get the briefing uploaded file urls
            newBriefing.Conforme = briefingConforme.SecureUrl.AbsoluteUri;
            newBriefing.BriefedBy = briefedBy.SecureUrl.AbsoluteUri;
            newBriefing.Witness = witness.SecureUrl.AbsoluteUri;


            // Buyer Information
            var buyer = new BuyerInformation();
            buyer.Screening = newScreening;
            buyer.Briefing = newBriefing;
            buyer.Name = requirement.Screening.BuyerInformation.Name;
            buyer.Address = requirement.Screening.BuyerInformation.Address;
            buyer.BirthDate = requirement.Screening.BuyerInformation.BirthDate;
            buyer.ContactNumber = requirement.Screening.BuyerInformation.ContactNumber;
            buyer.Citizenship = requirement.Screening.BuyerInformation.Citizenship;
            buyer.Facebook = requirement.Screening.BuyerInformation.Facebook;
            buyer.Email = requirement.Screening.BuyerInformation.Email;
            buyer.Gender = EnumSetupService.DefineGender((int) requirement.Screening.BuyerInformation.Gender!);
            buyer.CivilStatus = EnumSetupService.DefineCivilstatus((int)requirement.Screening.BuyerInformation.CivilStatus!);
            buyer.Financing = EnumSetupService.DefineFinancing((int)requirement.Screening.BuyerInformation.Financing!);

            // Titling Instruction
            var titlingInstruction = new TitlingInstruction();
            titlingInstruction.BuyerInformation = buyer;
            titlingInstruction.Data = requirement.Screening.BuyerInformation.TitlingInstruction.Data;
            titlingInstruction.TitlingInstructionOption = EnumSetupService.DefineTitlingInstruction((int)requirement.Screening.BuyerInformation.TitlingInstruction.TitlingInstructionOption!);
            buyer.TitlingInstruction = titlingInstruction;

            // Negative Data Bank Record
            var negativeDataBankRecord = new NegativeDataBankRecord();
            negativeDataBankRecord.BuyerInformation = buyer;
            negativeDataBankRecord.CancelledCreditCard = requirement.Screening.BuyerInformation.NegativeDataBankRecord.CancelledCreditCard;
            negativeDataBankRecord.BouncedCheck = requirement.Screening.BuyerInformation.NegativeDataBankRecord.BouncedCheck;
            negativeDataBankRecord.PendingCourtCases = requirement.Screening.BuyerInformation.NegativeDataBankRecord.PendingCourtCases;
            negativeDataBankRecord.UnpaidTelecomBill = requirement.Screening.BuyerInformation.NegativeDataBankRecord.UnpaidTelecomBill;
            negativeDataBankRecord.Others = requirement.Screening.BuyerInformation.NegativeDataBankRecord.Others;
            buyer.NegativeDataBankRecord = negativeDataBankRecord;

            // Pag-IBIG Membership
            var pagIbigMembership = new PagIbigMembership();
            pagIbigMembership.BuyerInformation = buyer;
            pagIbigMembership.PagIBIGMembership = requirement.Screening.BuyerInformation.PagIbigMembership.PagIBIGMembership;
            pagIbigMembership.NumberOfYears = requirement.Screening.BuyerInformation.PagIbigMembership.NumberOfYears;
            pagIbigMembership.WOHML = requirement.Screening.BuyerInformation.PagIbigMembership.WOHML;
            pagIbigMembership.Updated = requirement.Screening.BuyerInformation.PagIbigMembership.Updated;
            pagIbigMembership.WFHL = requirement.Screening.BuyerInformation.PagIbigMembership.WFHL;
            buyer.PagIbigMembership = pagIbigMembership;

            // Employer Details
            var employerDetails = new EmployerDetail();
            employerDetails.BuyerInformation = buyer;
            employerDetails.CompanyName = requirement.Screening.BuyerInformation.EmployerDetail.CompanyName;
            employerDetails.Address = requirement.Screening.BuyerInformation.EmployerDetail.Address;
            employerDetails.ContactNumber = requirement.Screening.BuyerInformation.EmployerDetail.ContactNumber;
            employerDetails.ImmedaiteSuperior = requirement.Screening.BuyerInformation.EmployerDetail.ImmedaiteSuperior;
            employerDetails.Email = requirement.Screening.BuyerInformation.EmployerDetail.Email;
            buyer.EmployerDetails = employerDetails;

            newScreening.BuyerInformation = buyer;

            // Document
            var document = new Document();
            document.Screening = newScreening;

            // Standard Document
            var standardDocument = new StandardDocument();
            standardDocument.Document = document;

            // upload required files in document
            string marriageCertificateUrl = "";
            string othersUrl = "";

            var validIds = await _uploadService.Images(requirement.Screening.Document.StandardDocument.GovtIssuedValidIds, "document");
            var spouseValidIds = await _uploadService.Images(requirement.Screening.Document.StandardDocument.GovtIssuedSpouseValidIds, "document");
            var tinNumber = await _uploadService.Image(requirement.Screening.Document.StandardDocument.TINNumber, "document");
            var birthCertificate = await _uploadService.Image(requirement.Screening.Document.StandardDocument.BirthCertificate, "document");
            var clearOneByOnePicture = await _uploadService.Image(requirement.Screening.Document.StandardDocument.ClearOneByOnePicture, "document");
            var proofOfMailingOrBilling = await _uploadService.Image(requirement.Screening.Document.StandardDocument.ProofOfMailingOrBilling, "document");
            var postDatedChecks = await _uploadService.Image(requirement.Screening.Document.StandardDocument.PostDatedChecks, "document");
            var authorizeRepresentative = await _uploadService.Image(requirement.Screening.Document.StandardDocument.AuthorizeRepresentative, "document");
            var spaNotarized = await _uploadService.Image(requirement.Screening.Document.StandardDocument.SPANotarizedAndConsularized, "document");
            var bankAndPagibigSPA = await _uploadService.Image(requirement.Screening.Document.StandardDocument.BankAndPagIbigSPA, "document");
            var oath = await _uploadService.Image(requirement.Screening.Document.StandardDocument.OathOfAllegianceOrAffidavitOfCitizenship, "document");
            
            if(requirement.Screening.Document.StandardDocument.MarriageCertificate is not null)
            {
                var marriageCertificate = await _uploadService.Image(requirement.Screening.Document.StandardDocument.MarriageCertificate, "document");
                marriageCertificateUrl = marriageCertificate.SecureUrl.AbsoluteUri;
            }

            if(requirement.Screening.Document.StandardDocument.Others is not null)
            {
                var others = await _uploadService.Image(requirement.Screening.Document.StandardDocument.Others, "document");
                othersUrl = others.SecureUrl.AbsoluteUri;
            }

            // set standard document uploaded file urls
            standardDocument.GovtIssuedValidIds = validIds.Select(vid => vid.SecureUrl.AbsoluteUri).ToList();
            standardDocument.GovtIssuedSpouseValidIds = spouseValidIds.Select(sid => sid.SecureUrl.AbsoluteUri).ToList();
            standardDocument.TINNumber = tinNumber.SecureUrl.AbsoluteUri;
            standardDocument.BirthCertificate = birthCertificate.SecureUrl.AbsoluteUri;
            standardDocument.ClearOneByOnePicture = clearOneByOnePicture.SecureUrl.AbsoluteUri;
            standardDocument.ProofOfMailingOrBilling = proofOfMailingOrBilling.SecureUrl.AbsoluteUri;
            standardDocument.PostDatedChecks = postDatedChecks.SecureUrl.AbsoluteUri;
            standardDocument.AuthorizeRepresentative = authorizeRepresentative.SecureUrl.AbsoluteUri;
            standardDocument.SPANotarizedAndConsularized = spaNotarized.SecureUrl.AbsoluteUri;
            standardDocument.BankAndPagIbigSPA = bankAndPagibigSPA.SecureUrl.AbsoluteUri;
            standardDocument.OathOfAllegianceOrAffidavitOfCitizenship = oath.SecureUrl.AbsoluteUri;
            standardDocument.MarriageCertificate = marriageCertificateUrl;
            standardDocument.Others = othersUrl;
            document.StandardDocument = standardDocument;

            // Source Of Income
            var sourceOfIncome = new SourceOfIncome();
            sourceOfIncome.Document = document;

            // locally employed
            var locallyEmployed = new LocallyEmployed();
            locallyEmployed.SourceOfIncome = sourceOfIncome;
            locallyEmployed.Compensation = requirement.Screening.Document.SourceOfIncome.LocallyEmployed.Compensation;
            locallyEmployed.LatestITR = requirement.Screening.Document.SourceOfIncome.LocallyEmployed.LatestITR;
            locallyEmployed.ThreeMonthsOfPayslips = requirement.Screening.Document.SourceOfIncome.LocallyEmployed.ThreeMonthsOfPayslips;
            sourceOfIncome.LocallyEmployed = locallyEmployed;

            // self employed
            var selfEmployed = new SelfEmployed();
            selfEmployed.SourceOfIncome = sourceOfIncome;

            // self employed -> formal
            var formal = new SelfEmployedFormal();
            formal.SelfEmployed = selfEmployed;
            formal.Latest2YearsITR = requirement.Screening.Document.SourceOfIncome.SelfEmployed.Formal.Latest2YearsITR;
            formal.Latest2YearsAFS = requirement.Screening.Document.SourceOfIncome.SelfEmployed.Formal.Latest2YearsAFS;
            formal.Latest6MonthsBankStatements = requirement.Screening.Document.SourceOfIncome.SelfEmployed.Formal.Latest6MonthsBankStatements;
            selfEmployed.Formal = formal;

            // self employed -> informal
            var informal = new SelfEmployedInformal();
            informal.SelfEmployed = selfEmployed;
            informal.COEIdOfSignatory = requirement.Screening.Document.SourceOfIncome.SelfEmployed.Informal.COEIdOfSignatory;
            informal.COEOtherAttachments = requirement.Screening.Document.SourceOfIncome.SelfEmployed.Informal.COEOtherAttachments;
            selfEmployed.Informal = informal;

            sourceOfIncome.SelfEmployed = selfEmployed;

            // OverseasFilipinoWorker
            var overseasFilipinoWorker = new OverseasFilipinoWorker();
            overseasFilipinoWorker.SourceOfIncome = sourceOfIncome;
            overseasFilipinoWorker.Country = requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker.Country;
            overseasFilipinoWorker.NCEC = requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker.NCEC;
            overseasFilipinoWorker.ThreeMonthsPayslipsOrRemittance = requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker.ThreeMonthsPayslipsOrRemittance;
            overseasFilipinoWorker.BankStatements = requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker.BankStatements;
            overseasFilipinoWorker.PassportWithEntryAndExit = requirement.Screening.Document.SourceOfIncome.OverseasFilipinoWorker.PassportWithEntryAndExit;
            sourceOfIncome.OverseasFilipinoWorker = overseasFilipinoWorker;

            document.SourceOfIncome = sourceOfIncome;
            newScreening.Document = document;

            // Computation
            var computation = new Computation();
            computation.Screening = newScreening;
            computation.SellingPrice = requirement.Screening.Computation.SellingPrice;
            computation.Discount = requirement.Screening.Computation.Discount;
            computation.NetSellingPrice = requirement.Screening.Computation.NetSellingPrice;
            computation.TaxesAndFees = requirement.Screening.Computation.TaxesAndFees;
            computation.TotalReceivable = requirement.Screening.Computation.TotalReceivable;
            computation.NumberOfDownpayments = requirement.Screening.Computation.NumberOfDownpayments;
            computation.EMA = requirement.Screening.Computation.EMA;
            computation.GrossIncome = requirement.Screening.Computation.GrossIncome;
            computation.MonthlyIncomeRatio = requirement.Screening.Computation.MonthlyIncomeRatio;

            newScreening.Computation = computation;

            newRequirement.Screening = newScreening;
            newRequirement.Briefing = newBriefing;

            var result = await Create(newRequirement);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}