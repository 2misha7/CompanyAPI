using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface IIndividualsRepository
{
    Task<Individual?> ClientWithNumberExists(string requestPhoneNumber, CancellationToken cancellationToken);
    Task<Individual?> ClientWithPeselExists(string requestPhoneNumber, CancellationToken cancellationToken);
    Task<Individual?> ClientWithEmailExists(string requestEmail, CancellationToken cancellationToken);
    Task AddIndividualAsync(Individual newIndividual, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Individual?> GetIndividualAsync(int idClient, CancellationToken cancellationToken);
}