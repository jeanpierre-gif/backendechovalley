using MediatR;
using Microsoft.AspNetCore.Mvc;
using training.Models;

namespace training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        public PostController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("GetPosts")]
        public async Task<ActionResult<List<PostModel>>> GetPosts([FromQuery] int pageSize = 10, [FromQuery] string searchQuery = null)
        {
            return await mediator.Send(new GetPostsQuery(pageSize, searchQuery));
        }
    }
}
