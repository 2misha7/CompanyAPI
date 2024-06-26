using ApbdProject.DTO.Requests;
using ApbdProject.Services.ServInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApbdProject.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _paymentsService;

    [HttpPost()]
    public async Task<IActionResult> MakePayment([FromBody] MakePaymentDto makePaymentDto, CancellationToken cancellationToken)
    {
        
    }
}