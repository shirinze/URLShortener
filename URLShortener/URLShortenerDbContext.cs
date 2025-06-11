using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener;

public class URLShortenerDbContext
{
    public IMongoCollection<ShortURL> ShortURLs { get;}
    public URLShortenerDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        ShortURLs = database.GetCollection<ShortURL>(configuration["DatabaseSettings:CollectionName"]);
      
    }
}
