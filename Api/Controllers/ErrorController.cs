using Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
        
    }
}