using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Authorize(Policy = "AtLeast21")]
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : Controller
    {
        [HttpGet("employee")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Ahmed", "Ali", "Ahsan" };
        }
    }
}
