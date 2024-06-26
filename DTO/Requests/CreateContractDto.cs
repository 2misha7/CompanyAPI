namespace ApbdProject.DTO.Requests;

public class CreateContractDto
{
    public int ClientID { get; set; }
    public int SoftwareID { get; set; }
    public int VersionID { get; set; }
    public bool IsIndividual { get; set; }
    public bool IsCompany { get; set; }
    public int TimeRange { get; set; }
    public int YearsOfAdditionalSupport { get; set; }
}

