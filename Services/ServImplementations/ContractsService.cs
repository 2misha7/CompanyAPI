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

    public ContractsService(IContractsRepository contractsRepository, IVersionsRepository versionsRepository, IDiscountsRepository discountsRepository)
    {
        _contractsRepository = contractsRepository;
        _versionsRepository = versionsRepository;
        _discountsRepository = discountsRepository;
    }

    public async Task<ContractDto> CreateContractAsync(CreateContractDto createContractDto, CancellationToken cancellationToken)
    {
        await ValiateTimeRange(createContractDto.TimeRange);
        await ValiateExtraSupportPeriod(createContractDto.YearsOfAdditionalSupport);
        var clientType = await ClientHasSimilarContract(createContractDto.ClientID, createContractDto.IsIndividual, createContractDto.IsCompany, createContractDto.VersionID, cancellationToken);
        await ValidateVersionOfSoftware(createContractDto.SoftwareID, createContractDto.VersionID, cancellationToken);

        
        var clientHasContracts = false;
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

        var price = await _versionsRepository.GetPriceForVersion(createContractDto.VersionID, cancellationToken);
        var discountPercentage =  await _discountsRepository.FindHighestDiscount(cancellationToken);
        if (clientHasContracts)
        {
            discountPercentage += 5;
        }

        price -= price * discountPercentage / 100;
        var newContract = new Contract
        {
            IdSoftwareVersion = createContractDto.VersionID,
            ExtendedSupportPeriod = createContractDto.YearsOfAdditionalSupport,
            AmountPaid = 0,
            IdIndividual = IdIndividual,
            IdCompany = IdCompany,
            DateFrom = DateTime.Now,
            DateTo = DateTime.Now.AddDays(createContractDto.TimeRange),
            FullPrice = price
        };

    }

    private async Task ValidateVersionOfSoftware(int softwareId, int versionId, CancellationToken cancellationToken)
    {
        var version =  await _versionsRepository.VersionExists(softwareId, versionId, cancellationToken);
        if (version == null)
        {
            throw new ValidationException("This version does not exist");
        }
    }

    private async Task<string> ClientHasSimilarContract(int clientId, bool isIndividual, bool isCompany, int versionId, CancellationToken cancellationToken)
    {
        if (isIndividual)
        {
            var contract = await _contractsRepository.GetContractByVersionIndividual(clientId, versionId, cancellationToken);
            if (contract != null)
            {
                throw new ValidationException("This individual client already has an active contract for this product");
            }

            return "Individual";
        }else if (isCompany)
        {
            var contract = await _contractsRepository.GetContractByVersionCompany(clientId, versionId, cancellationToken);
            if (contract != null)
            {
                throw new ValidationException("This company already has an active contract for this product");
            }
            return "Company";
        }
        else
        {
            throw new ValidationException("Client type must be specified");
        }
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