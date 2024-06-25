namespace Project.Entities;

public class Payment
{
    public int IdPayment { get; set; }
    public int IdContract { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    
    public  virtual Contract Contract { get; set; }
}