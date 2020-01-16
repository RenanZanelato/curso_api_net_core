using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    [Route("v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDto login, [FromServices] ILoginService service)
        {
          if (!ModelState.IsValid)
             {
                return BadRequest(ModelState);
             }

            try
            {
                var result = await service.FindByLogin(login);
                if(result == null) 
                {
                    return NotFound(); 
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
      
    }
}
