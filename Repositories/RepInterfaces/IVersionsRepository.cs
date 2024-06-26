using Version = Project.Entities.Version;

namespace ApbdProject.Repositories.RepInterfaces;

public interface IVersionsRepository
{
    Task<Version?> VersionExists(int softwareId, int versionId, CancellationToken cancellationToken);
    Task<double> GetPriceForVersion(int versionId, CancellationToken cancellationToken);
    Task<string?> FindVersionName(int versionId, CancellationToken cancellationToken);
}