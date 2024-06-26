﻿using Project.Entities;

namespace ApbdProject.Repositories.RepInterfaces;

public interface IContractsRepository
{
    Task<Contract?> GetContractByVersionIndividual(int clientId, int versionId, CancellationToken cancellationToken);
    Task<Contract?> GetContractByVersionCompany(int clientId, int versionId, CancellationToken cancellationToken);
    Task <bool> IndividualHasContracts(int clientId);
    Task<bool> CompanyHasContracts(int clientId);
}