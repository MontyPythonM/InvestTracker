namespace InvestTracker.Notifications.Infrastructure.Options;

public class EmailSenderOptions
{
    public bool Enabled { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public bool UseSsl { get; set; }
    public bool UseRedirectTo { get; set; }
    public string[] RedirectTo { get; set; }
}