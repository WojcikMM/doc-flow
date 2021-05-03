using DocFlow.Core.Domain;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DocFlow.Web.Server.Areas.Identity
{
    public class CustomProfileService : DefaultProfileService
    {
        private readonly UserManager<UserEntity> userManager;

        public CustomProfileService(ILogger<DefaultProfileService> logger, UserManager<UserEntity> userManager) : base(logger)
        {
            this.userManager = userManager;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var user = await userManager.GetUserAsync(context.Subject);

            var userClaims = new List<Claim>()
            {
                new Claim("name", user.UserName),
                new Claim("email", user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };

            Logger.LogInformation($"Adding custom claims for user: {user.Id}.");
            context.IssuedClaims.AddRange(userClaims);
        }
    }
}
