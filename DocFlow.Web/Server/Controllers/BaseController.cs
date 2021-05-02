using Microsoft.AspNetCore.Mvc;

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
    }
}
