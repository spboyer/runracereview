using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace runracereview.Model
{
  public class RaceRepository : IRaceRepository
  {
    private readonly IApplicationDbContext _context;

    public RaceRepository(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Race>> GetAllRaces()
    {
      return await _context
      .Races
      .Find(_ => true)
      .ToListAsync();
    }
    public Task<Race> GetRace(string name)
    {
      FilterDefinition<Race> filter = Builders<Race>.Filter.Eq(m => m.Name, name);
      return _context
      .Races

      .Find(filter)
      .FirstOrDefaultAsync();
    }
    public async Task Create(Race race)
    {
      await _context.Races.InsertOneAsync(race);
    }
    public async Task<bool> Update(Race race)
    {
      ReplaceOneResult updateResult =
      await _context
      .Races
      .ReplaceOneAsync( filter: g => g.Id == race.Id, replacement: race);
      return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(string name)
    {
      FilterDefinition<Race> filter = Builders<Race>.Filter.Eq(m => m.Name, name);
      DeleteResult deleteResult = await _context
      .Races
      .DeleteOneAsync(filter);
      return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
  }
}