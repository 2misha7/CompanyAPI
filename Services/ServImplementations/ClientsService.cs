using ApbdProject.DTO.Requests;
using ApbdProject.Exceptions;
using ApbdProject.Repositories.RepInterfaces;
using ApbdProject.Services.ServInterfaces;
using Project.Entities;

namespace ApbdProject.Services.ServImplementations;

public class ClientsService : IClientsService
{
    private readonly IIndividualsRepository _individualsRepository;
    private readonly ICompaniesRepository _companiesRepository;

    public ClientsService(IIndividualsRepository individualsRepository, ICompaniesRepository companiesRepository)
    {
        _individualsRepository = individualsRepository;
        _companiesRepository = companiesRepository;
    }

    public async Task<int> AddIndividualAsync(AddIndividualDto request, CancellationToken cancellationToken)
    {
        await ValidateEmail(request.Email, cancellationToken);
        await ValidatePesel(request.PESEL, cancellationToken);
        await ValidatePhoneNumber(request.PhoneNumber, cancellationToken);

        var newIndividual = new Individual
        {
            Address = request.Address,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PESEL = request.PESEL,
            PhoneNumber = request.PhoneNumber,
            Contracts = new List<Contract>()
        };
        
        await _individualsRepository.AddIndividualAsync(newIndividual, cancellationToken);
        await _individualsRepository.SaveChangesAsync(cancellationToken);
        
        return newIndividual.IdIndividual;
    }

    public async Task<int> AddCompanyAsync(AddCompanyDto request, CancellationToken cancellationToken)
    {
        await ValidateEmail(request.Email, cancellationToken);
        await ValidatePhoneNumber(request.PhoneNumber, cancellationToken);
        await ValidateKrs(request.KRS, cancellationToken);

        var newCompany = new Company
        {
            CompanyName = request.CompanyName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            Contracts = new List<Contract>(),
            Email = request.Email,
            KRS = request.KRS
        };
        await _companiesRepository.AddCompanyAsync(newCompany, cancellationToken);
        await _companiesRepository.SaveChangesAsync(cancellationToken);
        return newCompany.IdCompany;
    }

    public async Task DeleteIndividualAsync(int idClient, CancellationToken cancellationToken)
    {
        var client = await GetIndividualAsync(idClient, cancellationToken);
        client.IsDeleted = true;
        await _individualsRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<Individual?> GetIndividualAsync(int idClient, CancellationToken cancellationToken)
    {
        var client = await _individualsRepository.GetIndividualAsync(idClient, cancellationToken);
        if (client == null)
        {
            throw new ValidationException("Client not found");
        }

        return client;
    }

    private async Task ValidateKrs(string requestKrs, CancellationToken cancellationToken)
    {
        var client = await _companiesRepository.ClientWithKrs(requestKrs, cancellationToken);
        if (client != null)
        {
            throw new ValidationException("Client with this KRS already exists");
        }
    }

    private async Task ValidatePhoneNumber(String phoneNumber, CancellationToken cancellationToken)
    {
        var client = await _companiesRepository.ClientWithNumberExists(phoneNumber, cancellationToken);
        if (client != null)
        {
            throw new ValidationException("Client with this phone number already exists");
        }
        var client1 = await _individualsRepository.ClientWithNumberExists(phoneNumber, cancellationToken);
        if (client1 != null)
        {
            throw new ValidationException("Client with this phone number already exists");
        }
    }

    private async Task ValidatePesel(String pesel, CancellationToken cancellationToken)
    {
        var client = await _individualsRepository.ClientWithPeselExists(pesel, cancellationToken);
        if (client != null)
        {
            throw new ValidationException("Client with this PESEL already exists");
        }
    }

    private async Task ValidateEmail(String email, CancellationToken cancellationToken)
    {
        var client = await _companiesRepository.ClientWithEmailExists(email, cancellationToken);
        if (client != null)
        {
            throw new ValidationException("Client with this email number already exists");
        }
        var client1 = await _individualsRepository.ClientWithEmailExists(email, cancellationToken);
        if (client1 != null)
        {
            throw new ValidationException("Client with this email number already exists");
        }
    }
}