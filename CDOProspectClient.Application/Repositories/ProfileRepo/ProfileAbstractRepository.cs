using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.ProfileRepo;

public abstract class ProfileAbstractRepository : Repository<Profile>
{
    public ProfileAbstractRepository(ApplicationDbContext context) : base(context) {}

    public abstract Task<Profile?> FindByEmail(string email);
}