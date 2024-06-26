using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;
using Version = Project.Entities.Version;

namespace ApbdProject.Repositories.RepImplementations;

public class VersionsRepository : IVersionsRepository
{
    private readonly MyContext _dbCcntext;

    public VersionsRepository(MyContext dbCcntext)
    {
        _dbCcntext = dbCcntext;
    }

    public async Task<Version?> VersionExists(int softwareId, int versionId, CancellationToken cancellationToken)
    {
        return await _dbCcntext.Versions
            .FirstOrDefaultAsync(x => x.IdSoftware == softwareId && x.IdVersion == versionId, cancellationToken: cancellationToken);
    }

    public async Task<double> GetPriceForVersion(int versionId, CancellationToken cancellationToken)
    {
        var version = await _dbCcntext.Versions.FirstOrDefaultAsync(x => x.IdVersion == versionId);
        return version.Price;
    }
}