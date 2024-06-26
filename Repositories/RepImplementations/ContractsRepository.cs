using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace ApbdProject.Repositories.RepImplementations;

public class ContractsRepository : IContractsRepository
{
    private readonly MyContext _dbContext;

    public ContractsRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Contract?> GetContractByVersionIndividual(int clientId, int versionId, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(x =>
            x.IdIndividual == clientId && x.IdSoftwareVersion == versionId, cancellationToken);
    }

    public async Task<Contract?> GetContractByVersionCompany(int clientId, int versionId, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(x =>
            x.IdCompany == clientId && x.IdSoftwareVersion == versionId, cancellationToken);
    }

    public async Task<bool> IndividualHasContracts(int clientId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.IdIndividual == clientId);
        return contract != null;
    }

    public async Task<bool> CompanyHasContracts(int clientId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.IdCompany == clientId);
        return contract != null;
    }
}