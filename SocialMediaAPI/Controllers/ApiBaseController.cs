using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SocialMediaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    //[ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    //[ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class ApiBaseController : ControllerBase
    {
    }
}
