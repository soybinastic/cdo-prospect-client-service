
using CDOProspectClient.Infrastructure.Data.Models;

namespace CDOProspectClient.Infrastructure.Helpers.EnumSetup;

public static class EnumSetupService
{
    public static Gender DefineGender(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return Gender.Male;
            case 1:
                return Gender.Female;
            default:
                return Gender.Male;
        }
    } 

    public static Financing DefineFinancing(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return Financing.Cash;
            case 1:
                return Financing.PagIBIG;
            case 2:
                return Financing.Bank;
            case 3:
                return Financing.Deffered;
            default: 
                return Financing.Cash;
        }
    }

    public static CivilStatus DefineCivilstatus(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return CivilStatus.Single;
            case 1:
                return CivilStatus.Married;
            case 2:
                return CivilStatus.WidowOrWidower;
            case 3:
                return CivilStatus.LegallySeparated;
            default:
                return CivilStatus.Single;
        }
    }

    public static TitlingInstructionOption DefineTitlingInstruction(int numberValue)
    {
        switch (numberValue)
        {
            case 0:
                return TitlingInstructionOption.Individual;
            case 1:
                return TitlingInstructionOption.MarriedTo;
            case 2:
                return TitlingInstructionOption.Spouses;
            case 3:
                return TitlingInstructionOption.CoOwners;
            default:
                return TitlingInstructionOption.Individual;
        }
    }
  
    public static EvaluationStatus DefineEvaluationStatus(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return EvaluationStatus.Pending;
            case 1:
                return EvaluationStatus.Approved;
            case 2:
                return EvaluationStatus.Cancelled;
            case 3:
                return EvaluationStatus.Rejected;
            default:
                return EvaluationStatus.Pending;
        }
    }

    public static Status DefineRequirementStatus(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return Status.Approved;
            case 1:
                return Status.Pending;
            case 2:
                return Status.Cancelled;
            case 3:
                return Status.Archived;
            case 4: 
                return Status.Forwarded;
            case 5:
                return Status.Received;
            case 6:
                return Status.Rejected;
            default:
                return Status.Pending;
        }
    }

    public static AppointmentStatus DefineAppointmentStatus(int numberValue)
    {
        switch(numberValue)
        {
            case 0:
                return AppointmentStatus.Pending;
            case 1:
                return AppointmentStatus.Confirm;
            case 2:
                return AppointmentStatus.Archived;
            default :
                return AppointmentStatus.Pending;
        }
    }
}