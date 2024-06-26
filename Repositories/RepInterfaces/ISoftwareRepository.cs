using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface ISoftwareRepository
{
    Task<string?> FindSoftwareName(int versionId, CancellationToken cancellationToken);
    Task<Software?> GetSoftware(int softwareId, CancellationToken cancellationToken);
}