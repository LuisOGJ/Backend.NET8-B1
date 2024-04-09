using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        [HttpGet]
        public decimal get(decimal a, decimal b) { 
            return a + b;
        }

        [HttpPost]
        public decimal Add(Numbers numbers) {
            Console.WriteLine(numbers.a.ToString());
            return numbers.a + numbers.b;
        }

    }


    public class Numbers { 
        public decimal a { get; set; }
        public decimal b { get; set; }
    }
}
