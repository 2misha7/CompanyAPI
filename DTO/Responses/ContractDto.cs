namespace ApbdProject.DTO.Responses;

public class ContractDto
{
    public int IdContract { get; set; }
    public int IdClient { get; set; }
    public string Type { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string VersionName{ get; set; }
    public string SoftwareName { get; set; }
    public double FullPrice { get; set; }
    public double AmountPaid { get; set; }
    public string Status { get; set; }
    public int ExtendedSupportPeriod { get; set; }
}