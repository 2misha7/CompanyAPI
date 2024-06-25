using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface ICompaniesRepository
{
    Task<Company?> ClientWithNumberExists(string requestPhoneNumber, CancellationToken cancellationToken);
    Task<Company?> ClientWithEmailExists(string requestEmail, CancellationToken cancellationToken);
    Task<Company?> ClientWithKrs(string requestKrs, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task AddCompanyAsync(Company newCompany, CancellationToken cancellationToken);
    Task<Company?> GetCompanyAsync(int idClient, CancellationToken cancellationToken);
}