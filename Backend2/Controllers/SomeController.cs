using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {

        [HttpGet("sync")]
        public IActionResult GetSync() { 

            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Connection to database is finished");

            Thread.Sleep(1000);
            Console.WriteLine("Mail sended");

            stopwatch.Stop();


            return Ok(stopwatch.Elapsed);
        }


        [HttpGet("async")]
        public async Task<IActionResult> GetAsync() {
            var task1 = new Task<int>(() => {
                Thread.Sleep(1000);
                Console.WriteLine("Connection to database is finished");
                return 8;
            });

            // la tarea inicia en segundo plano (como un subproceso)
            task1.Start();


            Console.WriteLine("Hago otra cosa");

            // nos detenemos aquí a esperar que termine task1
            var result = await task1;

            Console.WriteLine("Finished");

            return Ok(result);

        }

    }
}
