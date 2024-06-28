using ApbdProject.Services.ServInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities;

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

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentCompanyRevenue(string? currencyCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            currencyCode = "PLN";
        }
        try
        {
            var revenue = await _revenueService.GetCurrentCompanyRevenueAsync(currencyCode, cancellationToken);
            return (Ok("Current revenue for the entire company is: " + revenue + " " + currencyCode + "."));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredictedCompanyRevenue(string? currencyCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            currencyCode = "PLN";
        }
        try
        {
            var revenue = await _revenueService.GetPredictedCompanyRevenueAsync(currencyCode, cancellationToken);
            return (Ok("Predicted revenue for the entire company is: " + revenue + " " + currencyCode + "."));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [Authorize]
    [HttpGet("current/product/{idProduct}")]
    public async Task<IActionResult> GetCurrentProductRevenue(string? currencyCode, [FromRoute]int idProduct, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            currencyCode = "PLN";
        }
        try
        {
            var product = await _revenueService.GetProduct(idProduct, cancellationToken);
            var revenue = await _revenueService.GetCurrentProductRevenueAsync(currencyCode, idProduct, cancellationToken);
            return (Ok("Current revenue for the product " + product.Name + " is: " + revenue + " " + currencyCode + "."));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("predicted/product/{idProduct}")]
    public async Task<IActionResult> GetPredictedProductRevenue(string? currencyCode, [FromRoute]int idProduct, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            currencyCode = "PLN";
        }
        try
        {
            var product = await _revenueService.GetProduct(idProduct, cancellationToken);
            var revenue = await _revenueService.GetPredictedProductRevenueAsync(currencyCode, idProduct, cancellationToken);
            return (Ok("Predicted revenue for the product " + product.Name + " is: " + revenue + " " + currencyCode + "."));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}