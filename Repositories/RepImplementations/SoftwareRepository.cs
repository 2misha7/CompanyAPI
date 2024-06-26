using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace ApbdProject.Repositories.RepImplementations;

public class SoftwareRepository : ISoftwareRepository
{
    private readonly MyContext _dbContext;

    public SoftwareRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> FindSoftwareName(int softwareId, CancellationToken cancellationToken)
    {
        var software = await _dbContext.Softwares.FirstOrDefaultAsync(x => x.IdSoftware == softwareId, cancellationToken);
        return software?.Name;
    }

    public async Task<Software?> GetSoftware(int softwareId, CancellationToken cancellationToken)
    {
        return await _dbContext.Softwares.FirstOrDefaultAsync(x => x.IdSoftware == softwareId, cancellationToken);
    }
}