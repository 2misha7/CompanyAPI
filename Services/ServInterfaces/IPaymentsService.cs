using ApbdProject.DTO.Requests;
using ApbdProject.DTO.Responses;

namespace ApbdProject.Services.ServInterfaces;

public interface IPaymentsService
{
    Task<PaymentDto> MakePayment(int idContract, double amount, CancellationToken cancellationToken);
}