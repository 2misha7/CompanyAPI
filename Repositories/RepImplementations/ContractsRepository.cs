﻿using ApbdProject.Context;
using ApbdProject.Exceptions;
using ApbdProject.Migrations;
using ApbdProject.Repositories.RepInterfaces;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Project.Entities;
using Version = Project.Entities.Version;

namespace ApbdProject.Repositories.RepImplementations;

public class ContractsRepository : IContractsRepository
{
    private readonly MyContext _dbContext;

    public ContractsRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Contract?> GetContractByVersionIndividual(int clientId, int versionId, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(x =>
            x.IdIndividual == clientId && x.IdSoftwareVersion == versionId, cancellationToken);
    }

    public async Task<Contract?> GetContractByVersionCompany(int clientId, int versionId, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(x =>
            x.IdCompany == clientId && x.IdSoftwareVersion == versionId, cancellationToken);
    }

    public async Task<bool> IndividualHasContracts(int clientId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.IdIndividual == clientId);
        return contract != null;
    }

    public async Task<bool> CompanyHasContracts(int clientId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.IdCompany == clientId);
        return contract != null;
    }

    public async Task<Contract> AddContractAsync(Contract newContract, CancellationToken cancellationToken)
    {
        await _dbContext.Contracts.AddAsync(newContract, cancellationToken);
        return newContract;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Contract?> GetContract(int idContract, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(x => x.IdContract == idContract, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Contract>> GetAllSignedContracts(CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts
            .Where(contract => contract.Status == "Signed")
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Contract>> GetAllSignedCreatedContracts(CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts
            .Where(contract => contract.Status == "Signed" || contract.Status == "Created" )
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Contract>> GetAllSignedContractsBySoftware(int idProduct, CancellationToken cancellationToken)
    {
        var signedContracts = await _dbContext.Contracts
            .Where(contract => contract.Status == "Signed" && 
                               _dbContext.Versions.Any(version => version.IdSoftware == idProduct && version.IdVersion == contract.IdSoftwareVersion))
            .ToListAsync(cancellationToken);

        if (!signedContracts.Any())
        {
            throw new ValidationException("There are no signed contracts available for this software.");
        }
        return signedContracts;
    }

    public async Task<IEnumerable<Contract>> GetAllSignedCreatedContractsBySoftware(int idProduct, CancellationToken cancellationToken)
    {
        var signedContracts = await _dbContext.Contracts
            .Where(contract => (contract.Status == "Signed" || contract.Status == "Created") && 
                               _dbContext.Versions.Any(version => version.IdSoftware == idProduct && version.IdVersion == contract.IdSoftwareVersion))
            .ToListAsync(cancellationToken);

        if (!signedContracts.Any())
        {
            throw new ValidationException("There are no signed contracts available for this software.");
        }
        return signedContracts;
    }
}