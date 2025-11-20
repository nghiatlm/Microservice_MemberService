

namespace MemberService.Service.Services
{
    public interface IAuthClient
    {
        // Check if account exists in Auth service. Caller must provide a bearer token.
        Task<bool> AccountExistsAsync(int id, string bearerToken);
    }
}