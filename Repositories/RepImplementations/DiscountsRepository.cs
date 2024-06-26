using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ApbdProject.Repositories.RepImplementations;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly MyContext _dbContext;

    public DiscountsRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> FindHighestDiscount(CancellationToken cancellationToken)
    {
       var discount = await _dbContext.Discounts
            .Where(d => d.DateTo > DateTime.Now)
            .MaxAsync(d => (double?)d.Percentage, cancellationToken);
       var maxDiscount = 0;
       if (discount == null)
       {
           discount = 0;
       }
       return (double)discount;
    }
}