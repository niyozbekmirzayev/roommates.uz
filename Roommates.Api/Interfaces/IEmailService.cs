using Roommates.Api.ViewModels;

namespace Roommates.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailViewModel viewModel);
    }
}
