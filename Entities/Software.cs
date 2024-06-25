namespace Project.Entities;

public class Software
{
    public int IdSoftware { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int IdSoftwareCategory { get; set; }
    public virtual SoftwareCategory SoftwareCategory { get; set; }
    public virtual ICollection<Version> Versions { get; set; }
}