using ApbdProject.Services.ServInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApbdProject.Controllers;


[ApiController]
[Route("api/revenue")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }
    
    
}