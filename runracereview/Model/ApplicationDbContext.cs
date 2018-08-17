using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace runracereview.Model
{
  public class ApplicationDbContext : IApplicationDbContext
  {
    private readonly IMongoDatabase _db;
    public ApplicationDbContext(IOptions<Settings> options)
    {
      var client = new MongoClient(options.Value.ConnectionString);
      _db = client.GetDatabase(options.Value.Database);
    }
    public IMongoCollection<Race> Races => _db.GetCollection<Race>("Races");
  }

  public interface IApplicationDbContext
  {
    IMongoCollection<Race> Races { get; }
  }

}