using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Contacts : ControllerBase
    {
        public Contacts()
        {
            
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateContact()
        //{
        //    var response = await 
        //}

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return null;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact()
        {
            return null;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact()
        {
            return null;
        }
    }
}
