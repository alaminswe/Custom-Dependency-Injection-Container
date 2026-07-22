namespace DependencyInjection.Practice;

public interface IEmailService
{
    public void SendService(string to, string subject);
}

public class EmailService (): IEmailService
{
    public void SendService(string to, string subject)
    {
        Console.WriteLine($"Sending email to: {to}");
    }
}
public class SmsEmailService (): IEmailService
{
    public void SendService(string to, string subject)
    {
        Console.WriteLine($"Sending email to: {to}");
    }
}

// public class NotificationService(IEmailService service)
// {
//     public void NotifyUser(string userEmail)
//     {
//         service.SendService(userEmail, "Notification");
//     }
// }

public class NotificationService()
{
    public void NotifyUser(string userEmail)
    {
        Console.WriteLine($"Sending mail to -> {userEmail}");
    }
}