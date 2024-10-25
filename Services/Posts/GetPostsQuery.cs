using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using training.Context;
using training.Models;

public class GetPostsQuery : IRequest<List<PostModel>>
{
    public int PageSize { get; set; }
    public string SearchQuery { get; set; }

    public GetPostsQuery(int pageSize, string searchQuery)
    {
        PageSize = pageSize;
        SearchQuery = searchQuery;
    }
}

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostModel>>
{
    private readonly MyDbContext _context; // Replace with your actual DbContext

    public GetPostsQueryHandler(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<PostModel>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        // Start building the query
        var query = _context.posts.AsQueryable();

        // If a search query is provided, filter the posts
        if (!string.IsNullOrEmpty(request.SearchQuery))
        {
            query = query.Where(p => p.Title.Contains(request.SearchQuery) ||
                                     p.Body.Contains(request.SearchQuery));
        }

        // Apply pagination by taking the specified page size
        var posts = await query
            .OrderBy(p => p.Id) // Adjust the ordering as needed
            .Take(request.PageSize) // Only take the specified page size
            .ToListAsync(cancellationToken);

        // Map the posts to PostDTO (you can use AutoMapper or manual mapping)
        var postDtos = posts.Select(p => new PostModel
        {
            Id = p.Id,
            Title = p.Title,
            Body = p.Body
        }).ToList();

        return postDtos;
    }
}
