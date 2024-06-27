using ApbdProject.Exceptions;
using ApbdProject.Repositories.RepInterfaces;
using ApbdProject.Services.ServInterfaces;
using Project.Entities;

namespace ApbdProject.Services.ServImplementations;

public class RevenueService : IRevenueService
{
    private readonly IContractsRepository _contractsRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly CurrencyRatesClient _currencyRatesClient;
    public RevenueService(IContractsRepository contractsRepository, ISoftwareRepository softwareRepository)
    {
        _contractsRepository = contractsRepository;
        _softwareRepository = softwareRepository;
        _currencyRatesClient =  new CurrencyRatesClient();
    }

    public async Task<double> GetCurrentCompanyRevenueAsync(string currency, CancellationToken cancellationToken)
    {
        double revenue = 0;
        var signedContracts = await _contractsRepository.GetAllSignedContracts(cancellationToken);
        foreach (var contract in signedContracts)
        {
            revenue += contract.FullPrice;
        }

        if (currency.ToLower() != "pln")
        {
            string table = "A";

            decimal rate = 0;
            try
            {
                rate = _currencyRatesClient.GetExchangeRate(table, currency);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during while receiving rates from NPB API");
            }
            revenue *= (double)(1/rate);
        }
        return revenue;
    }
    
    public async Task<double> GetPredictedCompanyRevenueAsync(string currency, CancellationToken cancellationToken)
    {
        double revenue = 0;
        var contracts = await _contractsRepository.GetAllSignedCreatedContracts(cancellationToken);
        foreach (var contract in contracts)
        {
            revenue += contract.FullPrice;
        }

        if (currency.ToLower() != "pln")
        {
            string table = "A";

            decimal rate = 0;
            try
            {
                rate = _currencyRatesClient.GetExchangeRate(table, currency);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during while receiving rates from NPB API");
            }
            revenue *= (double)(1/rate);
        }
        return revenue;
    }

    public async Task<double> GetPredictedProductRevenueAsync(string currency, int idProduct, CancellationToken cancellationToken)
    {
        double revenue = 0; 
        var signedContracts = await _contractsRepository.GetAllSignedCreatedContractsBySoftware(idProduct, cancellationToken);
        foreach (var contract in signedContracts)
        {
            revenue += contract.FullPrice;
        }
        if (currency.ToLower() != "pln")
        {
            string table = "A";

            decimal rate = 0;
            try
            {
                rate = _currencyRatesClient.GetExchangeRate(table, currency);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during while receiving rates from NPB API");
            }
            revenue *= (double)(1/rate);
        }
        

        return revenue;
    }

    public async Task<double> GetCurrentProductRevenueAsync(string currency, int idProduct,
        CancellationToken cancellationToken)
    {
        double revenue = 0; 
        var signedContracts = await _contractsRepository.GetAllSignedContractsBySoftware(idProduct, cancellationToken);
        foreach (var contract in signedContracts)
        {
            revenue += contract.FullPrice;
        }
        if (currency.ToLower() != "pln")
        {
            string table = "A";

            decimal rate = 0;
            try
            {
                rate = _currencyRatesClient.GetExchangeRate(table, currency);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during while receiving rates from NPB API");
            }
            revenue *= (double)(1/rate);
        }
        

        return revenue;
    }
    

    public async Task<Software> GetProduct(int idProduct, CancellationToken cancellationToken)
    {
        var software = await _softwareRepository.GetSoftware(idProduct, cancellationToken);
        if (software == null)
        {
            throw new ValidationException("Software does not exist");
        }
        return software;
    }

    
}