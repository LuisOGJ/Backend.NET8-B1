using Backend2.Controllers;

namespace Backend2.Services
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(People people)
        {
            if (string.IsNullOrEmpty(people.name)) { 
                return false;
            }
            return true;
        }
    }
}
