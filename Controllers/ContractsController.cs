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

    public ContractsController(IContractsService contractsService)
    {
        _contractsService = contractsService;
    }
    
    
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
}