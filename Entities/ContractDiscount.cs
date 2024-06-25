namespace Project.Entities;

public class ContractDiscount
{
    public int IdContract { get; set; }
    public int IdDiscount { get; set; }
    public virtual Discount Discount { get; set; }
    public virtual Contract Contract { get; set; }
}