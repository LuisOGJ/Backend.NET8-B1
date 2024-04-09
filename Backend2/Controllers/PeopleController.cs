using Backend2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("PeopleService")]IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> getPeople() => Repository.people;

        [HttpGet("{id}")]
        //public People get(int id) => Repository.people.First(x => x.id == id);
        public ActionResult<People> get(int id) { 
            var people = Repository.people.FirstOrDefault(x => x.id == id);
            if(people == null)
            {
                return NotFound();
            }
            return Ok(people);
        }

        // Brinda mayor flixibilidad que ActionResult ya que no nos obliga a recibir un tipo de dato en <>
        [HttpPost]
        public IActionResult Add(People people) {
            if (!_peopleService.Validate(people)) {
                return BadRequest();
            }
            Repository.people.Add(people);

            // indica resultado ok-(cod:204 No Content) de operación pero no brinda más información
            return NoContent();
        }



        [HttpGet("search/{goal}")]
        public List<People> get(string goal) => Repository.people.Where(x => x.name.ToUpper().Contains(goal.ToUpper())).ToList();
    }

    public class Repository {
        public static List<People> people = new List<People>{
            new People(){
                id = 1,
                name = "Luis",
                Birthdate = new DateTime(1998, 2, 16),
            },
            new People(){
                id = 2,
                name = "Flor",
                Birthdate = new DateTime(1998, 2, 11),
            },
            new People(){
                id = 3,
                name = "Nala",
                Birthdate = new DateTime(2021, 12, 15),
            },
            new People(){
                id = 4,
                name = "Dala",
                Birthdate = new DateTime(2023, 7, 15),
            },
            new People(){
                id = 5,
                name = "Kira",
                Birthdate = new DateTime(1998, 2, 16),
            },
        };
    }

    public class People { 
        public int id { get; set; }
        public string name { get; set; }
        public DateTime Birthdate { get; set; }
    }


}
