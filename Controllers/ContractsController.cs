using System.Net;
using ApbdProject.DTO.Requests;
using ApbdProject.Services.ServInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApbdProject.Controllers;


[ApiController]
[Route("api/contracts")]
public class ContractsController : ControllerBase
{
    private readonly IContractsService _contractsService;
    private readonly IPaymentsService _paymentsService;

    public ContractsController(IContractsService contractsService, IPaymentsService paymentsService)
    {
        _contractsService = contractsService;
        _paymentsService = paymentsService;
    }

    
   [HttpPost()]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto createContractDto, CancellationToken cancellationToken)
    {
        try
        {
            var newContract = await _contractsService.CreateContractAsync(createContractDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, newContract);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("{idContract}/payments")]
    public async Task<IActionResult> MakePayment([FromRoute] int idContract, double amount, CancellationToken cancellationToken)
    {
        try
        {
            var newPayment = await _paymentsService.MakePayment(idContract, amount, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, newPayment);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}