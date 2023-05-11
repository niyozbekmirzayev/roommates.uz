using Roommates.Api.Service.ViewModels.IdentityService;

namespace Roommates.Api.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailViewModel viewModel);
    }
}
