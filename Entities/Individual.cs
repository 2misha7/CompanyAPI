namespace Project.Entities;

public class Individual
{
    public int IdIndividual { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string PESEL { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
}