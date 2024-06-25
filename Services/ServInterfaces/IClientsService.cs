using ApbdProject.DTO.Requests;

namespace ApbdProject.Services.ServInterfaces;

public interface IClientsService
{
    Task<int> AddIndividualAsync(AddIndividualDto individualDto, CancellationToken cancellationToken);
    Task<int> AddCompanyAsync(AddCompanyDto companyDto, CancellationToken cancellationToken);
    Task DeleteIndividualAsync(int idClient, CancellationToken cancellationToken);
    Task UpdateIndividualAsync(int idClient, UpdateIndividualDto individualDto, CancellationToken cancellationToken);
    Task UpdateCompanyAsync(int idClient, UpdateCompanyDto companyDto, CancellationToken cancellationToken);
}