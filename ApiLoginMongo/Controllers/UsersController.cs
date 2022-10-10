using ApiLoginMongo.Data;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiLoginMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<SearchResult<User>> Get(int page, int pageSize)
        {
            return await userService.GetAllAsync(page, pageSize);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(string id)
        {
            return await userService.GetAsync(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User obj)
        {
            var result = await userService.AddAsync(obj);
            return Ok(result);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User obj)
        {
            var result = await userService.UpdateAsync(id, obj);
            return Ok(result);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await userService.DeleteAsync(id);
            return NoContent();

        }
    }
}
