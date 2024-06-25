namespace Project.Entities;

public class Version
{
    public int IdVersion { get; set; }
    public int IdSoftware { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; }
    public virtual Software Software { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
}