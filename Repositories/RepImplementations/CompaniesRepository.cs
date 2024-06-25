using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace ApbdProject.Repositories.RepImplementations;

public class CompaniesRepository : ICompaniesRepository
{
    private readonly MyContext _dbContext;

    public CompaniesRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Company?> ClientWithNumberExists(string requestPhoneNumber, CancellationToken cancellationToken)
    {
        return _dbContext.Companies.FirstOrDefaultAsync(x => x.PhoneNumber == requestPhoneNumber, cancellationToken);
    }

    public Task<Company?> ClientWithEmailExists(string requestEmail, CancellationToken cancellationToken)
    {
        return _dbContext.Companies.FirstOrDefaultAsync(x => x.Email == requestEmail, cancellationToken);
    }

    public Task<Company?> ClientWithKrs(string requestKrs, CancellationToken cancellationToken)
    {
        return _dbContext.Companies.FirstOrDefaultAsync(x => x.KRS == requestKrs, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddCompanyAsync(Company newCompany, CancellationToken cancellationToken)
    {
        await _dbContext.Companies.AddAsync(newCompany, cancellationToken);
    }
}