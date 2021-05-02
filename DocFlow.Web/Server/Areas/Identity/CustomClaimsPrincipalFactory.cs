using DocFlow.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DocFlow.Web.Server.Areas.Identity
{
    class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserEntity>, IUserClaimsPrincipalFactory<UserEntity>
    {
        public CustomClaimsPrincipalFactory(UserManager<UserEntity> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserEntity user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));


            return claimsIdentity;
        }
    }
}
