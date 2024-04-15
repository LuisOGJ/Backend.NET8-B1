using Backend2.DTOs;
using Backend2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        IPostsService _tittleService;

        public PostsController(IPostsService tittleService) { 
            _tittleService = tittleService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get(){
            return await _tittleService.Get();
        }

    }
}
