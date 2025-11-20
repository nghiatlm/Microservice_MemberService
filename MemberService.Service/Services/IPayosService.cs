

using Net.payOS.Types;

namespace MemberService.Service.Services
{
    public interface IPayosService
    {
        Task<string> CreatePaymentAsync(int id);
        Task<bool> VerifyPaymentAsync(WebhookType type);
    }
}