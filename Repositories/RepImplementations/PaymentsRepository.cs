using ApbdProject.Context;
using ApbdProject.Repositories.RepInterfaces;
using Project.Entities;

namespace ApbdProject.Repositories.RepImplementations;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly MyContext _dbContext;

    public PaymentsRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Payment> AddPaymentAsync(Payment newPayment, CancellationToken cancellationToken)
    {
        await _dbContext.Payments.AddAsync(newPayment, cancellationToken);
        return newPayment;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}