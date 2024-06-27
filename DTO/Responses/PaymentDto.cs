namespace ApbdProject.DTO.Responses;

public class PaymentDto
{
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public int IdContract { get; set; }
    public string ContractStatus { get; set; }
    public double RemainingAmount { get; set; }
}