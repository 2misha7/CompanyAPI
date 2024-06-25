﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using ApbdProject.DTO.Requests;
using ApbdProject.Services.ServInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApbdProject.Controllers;


[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    
    
    //add new Individual Client to DB
    [HttpPost("/individuals")]
    public async Task<IActionResult> AddIndividual([FromBody] AddIndividualDto individualDto, CancellationToken cancellationToken)
    {
        try
        {
            var newIndividualID = await _clientsService.AddIndividualAsync(individualDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, newIndividualID);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    //add new Company Client to DB
    [HttpPost("/companies")]
    public async Task<IActionResult> AddCompany([FromBody] AddCompanyDto companyDto, CancellationToken cancellationToken)
    {
        try
        {
            var newCompanyId = await _clientsService.AddCompanyAsync(companyDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, newCompanyId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    //soft-delete an Individual client
    [HttpDelete("/individuals/{idClient}")]
    public async Task<IActionResult> DeleteIndividualClient(int idClient, CancellationToken cancellationToken)
    {
        try
        {
            await _clientsService.DeleteIndividualAsync(idClient, cancellationToken);
            return Ok("Individual client has been deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}