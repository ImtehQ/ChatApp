using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.Authentication
{
    public interface IJWTAuthService
    {
        AuthenticationModel GetToken(User user);
    }
}
