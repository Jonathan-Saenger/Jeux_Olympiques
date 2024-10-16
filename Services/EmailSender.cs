using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebPWrecover.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    /// <summary>
    /// Envoie un email asynchrone en utilisant l'API SendGrid.
    /// </summary>
    /// <returns>Une tâche asynchrone qui représente l'opération d'envoi de l'email.</returns>
    /// <remarks>
    /// Cette méthode vérifie si la clé SendGrid est présente. Si elle est absente, une exception est levée.
    /// Sinon, elle appelle la méthode <see cref="Execute"/> pour envoyer l'email.
    /// </remarks>

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(Options.SendGridKey, subject, message, toEmail);
    }

    /// <summary>
    /// Exécute l'envoi d'un email via l'API SendGrid.
    /// </summary>
    /// <param name="apiKey">Clé API SendGrid pour l'authentification.</param>
    /// <param name="subject">Objet de l'email.</param>
    /// <param name="message">Contenu de l'email (texte brut ou HTML).</param>
    /// <param name="toEmail">Adresse email du destinataire.</param>
    /// <returns>Une tâche asynchrone qui représente l'opération d'envoi de l'email.</returns>
    /// <remarks>
    /// Cette méthode crée un objet <see cref="SendGridMessage"/> avec les informations de l'email et utilise le client SendGrid pour envoyer l'email.
    /// Elle désactive également le suivi des clics (click tracking) pour cet email.
    /// Le logger est utilisé pour notifier la réussite ou l'échec de l'envoi.
    /// </remarks>
    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("jonathan.saenger.pro@gmail.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
    }
}