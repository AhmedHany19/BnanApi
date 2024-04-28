using BnanApi.DTOS;

namespace BnanApi.Services.Email
{
    public interface IMailingService
    {
        Task SendEmailAsync(EmailDTO request);
    }
}
