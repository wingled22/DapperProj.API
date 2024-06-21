using Microsoft.AspNetCore.Mvc;

namespace DapperProj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HttpContextSampleController : ControllerBase
    {
        // private readonly IHttpContextAccessor _http;
        // public HttpContextSampleController(IHttpContextAccessor httpContext)
        // {
        //     _http   = httpContext;
        // }


        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var request = HttpContext.Request;
            var user = HttpContext.User;
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

            // Example logic
            var info = new
            {
                RequestMethod = request.Method,
                RequestPath = request.Path,
                RemoteIP = remoteIpAddress?.ToString(),
                IsUserAuthenticated = user.Identity.IsAuthenticated,
                UserName = user.Identity.Name,
                StatusCode = 500,
            };

            return Ok(info);
        }
    }
}