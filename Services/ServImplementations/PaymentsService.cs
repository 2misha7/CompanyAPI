using ApbdProject.DTO.Requests;
using ApbdProject.DTO.Responses;
using ApbdProject.Exceptions;
using ApbdProject.Repositories.RepImplementations;
using ApbdProject.Repositories.RepInterfaces;
using ApbdProject.Services.ServInterfaces;
using Project.Entities;

namespace ApbdProject.Services.ServImplementations;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IContractsRepository _contractsRepository;

    public PaymentsService(IPaymentsRepository paymentsRepository, IContractsRepository contractsRepository)
    {
        _paymentsRepository = paymentsRepository;
        _contractsRepository = contractsRepository;
    }

    public async Task<PaymentDto> MakePayment(int idContract, double amount, CancellationToken cancellationToken)
    {
        var contract = await _contractsRepository.GetContract(idContract, cancellationToken);
        if (contract == null)
        {
            throw new ValidationException("Contract doesn't exist");
        }

        await ValidateDateOfPayment(contract, cancellationToken);
        await ValidateAmountAndProcessPayment(amount, contract, cancellationToken);

        var newPayment = new Payment()
        {
            IdContract = idContract,
            Date = DateTime.Now,
            Amount = amount
        };
        
        await _paymentsRepository.AddPaymentAsync(newPayment, cancellationToken);
        await _paymentsRepository.SaveChangesAsync(cancellationToken);

        var paymentDto = new PaymentDto
        {
            Date = newPayment.Date,
            IdContract = newPayment.IdContract,
            Amount  = newPayment.Amount,
            ContractStatus = contract.Status,
            RemainingAmount = contract.FullPrice - contract.AmountPaid
        };
        return paymentDto;
    }

    private async Task ValidateAmountAndProcessPayment(double amount, Contract contract, CancellationToken cancellationToken)
    {
        if (amount + contract.AmountPaid > contract.FullPrice)
        {
            var amountLeft = contract.FullPrice - contract.AmountPaid;
            throw new ValidationException("The value client is trying to pay is bigger than the price of a product.\n" +
                                          "It is not possible to process this payment.\n" +
                                          "The client needs to pay " + amountLeft);
        }
        if (amount + contract.AmountPaid == contract.FullPrice)
        {
            contract.AmountPaid = contract.FullPrice;
            contract.Status = ContractStatuses.Signed;
            await _contractsRepository.SaveChangesAsync(cancellationToken);
            return;
        }
        contract.AmountPaid += amount;
        await _contractsRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateDateOfPayment(Contract contract, CancellationToken cancellationToken)
    {
        if (DateTime.Now > contract.DateTo)
        {
            contract.AmountPaid = 0;
            contract.Status = ContractStatuses.Cancelled;
            await _contractsRepository.SaveChangesAsync(cancellationToken);
            throw new ValidationException(
                "Payment for a contract can't be accepted after the date specified within the contract.\n" +
                "All previous payments will be returned to the client.\n" +
                "Contract is cancelled.");
        }
    }
}