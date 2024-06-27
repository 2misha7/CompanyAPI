using System.Runtime.InteropServices.JavaScript;
using ApbdProject.DTO.Requests;
using ApbdProject.DTO.Responses;
using ApbdProject.Exceptions;
using ApbdProject.Repositories.RepInterfaces;
using ApbdProject.Services.ServInterfaces;
using Project.Entities;

namespace ApbdProject.Services.ServImplementations;

public class ContractsService : IContractsService
{
    private readonly IContractsRepository _contractsRepository;
    private readonly IVersionsRepository _versionsRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly ISoftwareRepository _softwareRepository;

    public ContractsService(IContractsRepository contractsRepository, IVersionsRepository versionsRepository, IDiscountsRepository discountsRepository, ISoftwareRepository softwareRepository)
    {
        _contractsRepository = contractsRepository;
        _versionsRepository = versionsRepository;
        _discountsRepository = discountsRepository;
        _softwareRepository = softwareRepository;
    }

    public async Task<ContractDto> CreateContractAsync(CreateContractDto createContractDto, CancellationToken cancellationToken)
    {
        await ValidateVersionOfSoftware(createContractDto.SoftwareID, createContractDto.VersionID, cancellationToken);
        await ValiateSoftware(createContractDto.SoftwareID, cancellationToken);
        await ValiateTimeRange(createContractDto.TimeRange);
        await ValiateExtraSupportPeriod(createContractDto.YearsOfAdditionalSupport);
        var clientType = await ClientHasSimilarContract(createContractDto.YearsOfAdditionalSupport, createContractDto.ClientID, createContractDto.IsIndividual, createContractDto.IsCompany, createContractDto.VersionID, cancellationToken);
        

        
        bool clientHasContracts;
        int IdIndividual = 0;
        int IdCompany = 0;
        if (clientType == "Individual")
        {
            IdIndividual = createContractDto.ClientID;
            clientHasContracts = await _contractsRepository.IndividualHasContracts(createContractDto.ClientID);
        }
        else
        {
            IdCompany = createContractDto.ClientID;
            clientHasContracts = await _contractsRepository.CompanyHasContracts(createContractDto.ClientID);
        }

        Console.WriteLine(IdIndividual);
        Console.WriteLine(IdCompany);
        var price = await _versionsRepository.GetPriceForVersion(createContractDto.VersionID, cancellationToken);
        var discountPercentage =  await _discountsRepository.FindHighestDiscount(cancellationToken);
        if (clientHasContracts)
        {
            discountPercentage += 5;
        }

        price += createContractDto.YearsOfAdditionalSupport * 1000;
        price -= price * discountPercentage / 100;
        var newContract = new Contract
        {
            IdSoftwareVersion = createContractDto.VersionID,
            ExtendedSupportPeriod = createContractDto.YearsOfAdditionalSupport,
            AmountPaid = 0,
            DateFrom = DateTime.Now,
            DateTo = DateTime.Now.AddDays(createContractDto.TimeRange),
            FullPrice = price,
            Status = ContractStatuses.Created,
            Payments = new List<Payment>()
        };
        
        if (clientType == "Individual")
        {
            newContract.IdIndividual = IdIndividual;
        }
        else
        {
            newContract.IdCompany = IdCompany;
        }
        var contract = await _contractsRepository.AddContractAsync(newContract, cancellationToken);
        
        await _contractsRepository.SaveChangesAsync(cancellationToken);
        var versionName = await _versionsRepository.FindVersionName(createContractDto.VersionID, cancellationToken);
        var softwareName = await _softwareRepository.FindSoftwareName(createContractDto.SoftwareID, cancellationToken);

        var finalContract = new ContractDto
        {
            IdContract = contract.IdContract,
            IdClient = createContractDto.ClientID,
            Type = clientType,
            DateFrom = contract.DateFrom,
            DateTo = contract.DateTo,
            VersionName = versionName,
            SoftwareName = softwareName,
            FullPrice = contract.FullPrice,
            AmountPaid = contract.AmountPaid,
            Status = contract.Status,
            ExtendedSupportPeriod = contract.ExtendedSupportPeriod
        };
        return finalContract;
    }

    private async Task ValiateSoftware(int softwareId, CancellationToken cancellationToken)
    {
        var software = await _softwareRepository.GetSoftware(softwareId, cancellationToken);
        if (software == null)
        {
            throw new ValidationException("This software does not exist");
        }
    }

    private async Task ValidateVersionOfSoftware(int softwareId, int versionId, CancellationToken cancellationToken)
    {
        var version =  await _versionsRepository.VersionExists(softwareId, versionId, cancellationToken);
        if (version == null)
        {
            throw new ValidationException("This version does not exist");
        }
    }

    private async Task<string> ClientHasSimilarContract(int yearsOfSupport, int clientId, bool isIndividual, bool isCompany, int versionId, CancellationToken cancellationToken)
    {
        if (isIndividual)
        {
            var contract = await _contractsRepository.GetContractByVersionIndividual(clientId, versionId, cancellationToken);
            if (contract != null && contract.DateFrom.AddYears(1 + yearsOfSupport) > DateTime.Now)
            {
                throw new ValidationException("This individual client already has an active contract for this product");
            }

            return "Individual";
        }
        if (isCompany)
        {
            var contract = await _contractsRepository.GetContractByVersionCompany(clientId, versionId, cancellationToken);
            if (contract != null && contract.DateFrom.AddYears(1 + yearsOfSupport) > DateTime.Now)
            {
                throw new ValidationException("This company already has an active contract for this product");
            }
            return "Company";
        }
        throw new ValidationException("Client type must be specified");
    }

    private async Task ValiateExtraSupportPeriod(int yearsOfAdditionalSupport)
    {
        if (yearsOfAdditionalSupport < 0 || yearsOfAdditionalSupport > 3)
        {
            throw new ValidationException("Support can be extended only for 1, 2 or 3 years");
        }
    }

    private async Task ValiateTimeRange(int timeRange)
    {
        if (timeRange < 3 || timeRange > 30)
        {
            throw new ValidationException("Time range is not valid");
        }
    }
}