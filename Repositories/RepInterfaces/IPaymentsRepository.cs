using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface IPaymentsRepository 
{
    Task<Payment> AddPaymentAsync(Payment newPayment, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}