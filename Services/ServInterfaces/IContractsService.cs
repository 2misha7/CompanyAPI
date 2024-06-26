using ApbdProject.DTO.Requests;
using ApbdProject.DTO.Responses;

namespace ApbdProject.Services.ServInterfaces;

public interface IContractsService
{
    Task<ContractDto> CreateContractAsync(CreateContractDto createContractDto, CancellationToken cancellationToken);
}