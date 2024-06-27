using System.Security.Cryptography.Xml;

namespace Project.Entities;


public static class ContractStatuses
{
    public const string Created = "Created";
    public const string Signed = "Signed";
    public const string Cancelled = "Cancelled";
}

public class Contract
{ 
    public int IdContract { get; set; }
    public DateTime DateFrom { get; set; }
    public int? IdIndividual { get; set; }
    public int? IdCompany { get; set; }
    public DateTime DateTo { get; set; }
    public int IdSoftwareVersion{ get; set; }
    public double FullPrice { get; set; }
    public string Status { get; set; }
    public int ExtendedSupportPeriod { get; set; }
    public double AmountPaid { get; set; }
    public virtual Version Version { get; set; }
    public virtual Individual Individual { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
}