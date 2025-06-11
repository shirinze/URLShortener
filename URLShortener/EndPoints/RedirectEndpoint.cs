using FastEndpoints;
using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener.EndPoints;

public class RedirectEndpoint(URLShortenerDbContext db):EndpointWithoutRequest
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;

    public override void Configure()
    {
        Get("/{code}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var code = Route<string>("code");

        var shortUrl = await collection.Find(x => x.Code == code).FirstOrDefaultAsync(ct);
        if(shortUrl is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendRedirectAsync(shortUrl.OriginalUrl, allowRemoteRedirects: true);
    }

}
