using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionaisController : ControllerBase
    {
        // GET: api/<ProfissionaisController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProfissionaisController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProfissionaisController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<ProfissionaisController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<ProfissionaisController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
