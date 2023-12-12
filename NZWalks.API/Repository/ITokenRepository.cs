using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
