using Roommates.Api.Service.ViewModels;

namespace Roommates.Api.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailViewModel viewModel);
    }
}
