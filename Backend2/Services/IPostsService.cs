using Backend2.DTOs;

namespace Backend2.Services
{
    public interface IPostsService
    {

        public Task<IEnumerable<PostDto>> Get();

    }
}
