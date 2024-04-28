using BnanApi.DTOS;
using BnanApi.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace BnanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IMailingService _mailingService;

        public EmailController(IEmailService emailService, IMailingService mailingService)
        {
            _emailService = emailService;
            _mailingService = mailingService;
        }

        //[HttpPost]
        //public async Task<IActionResult> SendEmail(EmailDTO request)
        //{
        //    await _emailService.SendEmail(request);
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] EmailDTO request)
        {
            await _mailingService.SendEmailAsync(request);
            return Ok();
        }
    }
}
