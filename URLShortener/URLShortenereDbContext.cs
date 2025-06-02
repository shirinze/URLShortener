using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener;

public class URLShortenereDbContext
{
    public IMongoCollection<ShortURL> ShortURLs { get;}
    public URLShortenereDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        ShortURLs = database.GetCollection<ShortURL>(configuration["DatabaseSettings: CollectionName"]);
      
    }
}
