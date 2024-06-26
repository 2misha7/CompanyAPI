using System.ComponentModel.DataAnnotations;

namespace Project.Entities;


public class Company
{
    public int IdCompany { get; set; }

    public string CompanyName { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string KRS { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}