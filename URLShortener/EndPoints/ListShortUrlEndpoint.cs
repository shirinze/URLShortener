using FastEndpoints;
using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener.EndPoints;

public class ListShortUrlResponse
{
    public List<ShortURL> ShortURLs { get; set; } = default!;
}
public class ListShortUrlEndpoint(URLShortenerDbContext db):EndpointWithoutRequest
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }
    public async override Task HandleAsync(CancellationToken ct)
    {
        var result = await collection.Find(FilterDefinition<ShortURL>.Empty).ToListAsync(ct);

        await SendAsync(new ListShortUrlResponse { ShortURLs = result }, StatusCodes.Status200OK, ct);
    }
}
