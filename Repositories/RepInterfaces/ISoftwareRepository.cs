namespace ApbdProject.Repositories.RepInterfaces;

public interface ISoftwareRepository
{
    Task<string?> FindSoftwareName(int versionId, CancellationToken cancellationToken);
}