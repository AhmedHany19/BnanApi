using BnanApi.DTOS;

namespace BnanApi.Services.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailDTO request);
    }
}
