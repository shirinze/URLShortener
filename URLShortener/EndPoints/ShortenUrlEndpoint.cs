﻿using FastEndpoints;
using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener.EndPoints;

public class ShortenUrlRequest
{
    public string OriginalUrl { get; set; } = default!;
}

public class ShortenUrlResponse
{
    public string ShortURL { get; set; } = default!;
}

public class ShortenUrlEndpoint(URLShortenerDbContext db,
    IHttpContextAccessor httpContextAccessor)
    :Endpoint<ShortenUrlRequest,ShortenUrlResponse>
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;
    public override void Configure()
    {
        Post("/shorten");
        AllowAnonymous();
    }
    public async override Task HandleAsync(ShortenUrlRequest req, CancellationToken ct)
    {
        var code = await CreateAsync(req.OriginalUrl, ct);

        var scheme = httpContextAccessor.HttpContext?.Request.Scheme;
        var host = httpContextAccessor.HttpContext?.Request.Host.Value;
        var result = $"{scheme}://{host}/{code}";

        await SendAsync(new ShortenUrlResponse { ShortURL = result }, StatusCodes.Status200OK, ct);
    }

    public async Task<string> CreateAsync(string originalURL, CancellationToken ct)
    {
        var code = GenerateCode();
        var shortURL = new ShortURL { Code = code, OriginalUrl = originalURL };

        var options = new InsertOneOptions
        {
            BypassDocumentValidation = false
        };
        try
        {
            await collection.InsertOneAsync(shortURL, options, ct);
        }
        catch (MongoWriteException)
        {
            code = await CreateAsync(originalURL, ct);
        }

        return code;
    }

    private static string GenerateCode(int length=3)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var resultchars = Enumerable
            .Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(chars.Length)])
            .ToArray();

        return new string(resultchars);
    }
}
