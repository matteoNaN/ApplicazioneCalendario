namespace API_ApplicazioneCalendario.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject,
                            string body,
                            List<string>? attachmentsPaths = null,
                            List<string>? CCs = null,
                            List<string>? CCNs = null);

    }

    public class SmtpEmailService : IEmailService
    {
        
        public Task SendEmailAsync(string to, string subject, string body, List<string>? attachmentsPaths = null, List<string>? CCs = null, List<string>? CCNs = null)
        {
            throw new NotImplementedException();
        }
    }
}
