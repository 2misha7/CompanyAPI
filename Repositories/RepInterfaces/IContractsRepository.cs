using System.Collections;
using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface IContractsRepository
{
    Task<Contract?> GetContractByVersionIndividual(int clientId, int versionId, CancellationToken cancellationToken);
    Task<Contract?> GetContractByVersionCompany(int clientId, int versionId, CancellationToken cancellationToken);
    Task <bool> IndividualHasContracts(int clientId);
    Task<bool> CompanyHasContracts(int clientId);
    Task<Contract> AddContractAsync(Contract newContract, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Contract?> GetContract(int idContract, CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetAllSignedContracts(CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetAllSignedCreatedContracts(CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetAllSignedContractsBySoftware(int idProduct, CancellationToken cancellationToken);

    Task<IEnumerable<Contract>> GetAllSignedCreatedContractsBySoftware(int idProduct, CancellationToken cancellationToken);
}