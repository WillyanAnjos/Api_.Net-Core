using ApiTeste.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password) {
            if (username == "admin" && password == "admin") {
                var token = TokenService.GenerateToken(new Models.Employee());
                return Ok(token);
            }  

            return BadRequest("Usuário ou senha inválidos");
        }
    }
}
