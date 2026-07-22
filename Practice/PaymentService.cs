namespace DependencyInjection.Practice;

public interface IPaymentService
{
    void MakePayment();
}

public class BkashPayment : IPaymentService
{
    public void MakePayment()
    {
        Console.WriteLine("Bks");
    }
}
public class NagadPayment : IPaymentService
{
    public void MakePayment()
    {
        Console.WriteLine("Ngd");
    }
}

public class OrderService
{
    private readonly IPaymentService _paymentService;
    public OrderService(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public void payment()
    {
        _paymentService.MakePayment();
    }
}