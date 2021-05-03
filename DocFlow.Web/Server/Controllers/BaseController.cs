using DocFlow.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace DocFlow.Web.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult Single<T>(T entity) where T : class
        {
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        // TODO: Move to extension methot ( from User.Identity )
        public CurrentRequestUser CurrentRequestUser
        {
            get
            {
                return new CurrentRequestUser
                {
                    UserId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                    UserName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value,
                    UserEmail = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value
                };
            }
        }
    }
}
