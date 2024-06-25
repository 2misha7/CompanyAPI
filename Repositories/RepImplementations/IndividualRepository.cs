using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace ApbdProject.Repositories.RepImplementations;

public class IndividualRepository : IIndividualsRepository
{
    private readonly MyContext _dbContext;

    public IndividualRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Individual?> ClientWithNumberExists(string requestPhoneNumber, CancellationToken cancellationToken)
    {
        return _dbContext.Individuals.FirstOrDefaultAsync(x => x.PhoneNumber == requestPhoneNumber, cancellationToken);
    }

    public Task<Individual?> ClientWithPeselExists(string requestPesel, CancellationToken cancellationToken)
    {
        return _dbContext.Individuals.FirstOrDefaultAsync(x => x.PESEL == requestPesel, cancellationToken);
    }

    public Task<Individual?> ClientWithEmailExists(string requestEmail, CancellationToken cancellationToken)
    {
        return _dbContext.Individuals.FirstOrDefaultAsync(x => x.PESEL == requestEmail, cancellationToken);
    }

    public async Task AddIndividualAsync(Individual newIndividual, CancellationToken cancellationToken)
    {
        await _dbContext.Individuals.AddAsync(newIndividual, cancellationToken);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Individual?> GetIndividualAsync(int idClient, CancellationToken cancellationToken)
    {
        return _dbContext.Individuals.FirstOrDefaultAsync(x => x.IdIndividual == idClient, cancellationToken);
    }
}