
using Roommates.Service.ViewModels;

namespace Roommates.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailViewModel viewModel);
    }
}
