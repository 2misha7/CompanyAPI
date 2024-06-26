namespace ApbdProject.Repositories.RepInterfaces;

public interface IDiscountsRepository
{
    Task<double> FindHighestDiscount(CancellationToken cancellationToken);
}