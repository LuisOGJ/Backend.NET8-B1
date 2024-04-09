using Backend2.Controllers;

namespace Backend2.Services
{
    public class People2Service : IPeopleService
    {
        public bool Validate(People people)
        {
            if (string.IsNullOrEmpty(people.name) || people.name.Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
