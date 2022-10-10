using ApiLoginMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }


    }
}