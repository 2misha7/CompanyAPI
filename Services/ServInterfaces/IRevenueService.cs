using Project.Entities;

namespace ApbdProject.Services.ServInterfaces;

public interface IRevenueService
{
    Task<double> GetCurrentCompanyRevenueAsync(string currency, CancellationToken cancellationToken);
    Task<double> GetCurrentProductRevenueAsync(string currencyCode, int idProduct, CancellationToken cancellationToken);
    Task<Software> GetProduct(int idProduct, CancellationToken cancellationToken);
    Task<double> GetPredictedCompanyRevenueAsync(string currencyCode, CancellationToken cancellationToken);
    Task<double> GetPredictedProductRevenueAsync(string currencyCode, int idProduct, CancellationToken cancellationToken);
}